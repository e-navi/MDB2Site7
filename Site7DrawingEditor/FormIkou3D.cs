using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace Site7DrawingEditor
{
    public partial class FormIkou3D : Form
    {
        public class GridMesh
        {
            public List<Point3D> Positions { get; } = new List<Point3D>();
            public List<int> TriangleIndices { get; } = new List<int>();
            public int ResolutionX { get; set; }
            public int ResolutionY { get; set; }
        }

        public class Danmen
        {
            public Point3D sp = new Point3D(0, 0, 0);
            public Point3D ep = new Point3D(0, 0, 0);
            public Point3D dp = new Point3D(0, 0, 0);
            public int cnt = 0;
            public List<(double Distance, double Elevation)> danmen = new List<(double, double)>();

            public List<Point3D> CalcDanmenP3()
            {
                if (cnt == 0 || danmen.Count == 0) return new List<Point3D>();
                double dx = (ep.X - sp.X) / cnt;
                double dy = (ep.Y - sp.Y) / cnt;
                var list = new List<Point3D>();

                for (int i = 0; i < cnt && i < danmen.Count; i++)
                {
                    list.Add(new Point3D(sp.X + dx * i, sp.Y + dy * i, danmen[i].Elevation));
                }
                return list;
            }

            public List<Point3D> CalcDanmenP2()
            {
                if (cnt == 0 || danmen.Count == 0) return new List<Point3D>();
                double dx = (ep.X - sp.X) / cnt;
                double dy = (ep.Y - sp.Y) / cnt;
                double len = Math.Sqrt(Math.Pow(ep.X - sp.X, 2) + Math.Pow(ep.Y - sp.Y, 2));
                if (len < 1e-6) return new List<Point3D>();

                double dx2 = (ep.X - sp.X) / len;
                double dy2 = (ep.Y - sp.Y) / len;
                var list = new List<Point3D>();

                double se = danmen[0].Elevation;
                for (int i = 0; i < cnt && i < danmen.Count; i++)
                {
                    double e = danmen[i].Elevation - se;
                    double x = dp.X + dx * i - dy2 * e;
                    double y = dp.Y + dy * i + dx2 * e;

                    list.Add(new Point3D(x, y, 0));
                }
                return list;
            }
        }

        public static class GridAlgorithm
        {
            public static GridMesh CreateGridMesh(Point3D minp, Point3D maxp, List<Point3D> sourcePoints, int resolutionX = 50, int resolutionY = 50, double power = 5.0)
            {
                var mesh = new GridMesh();
                mesh.ResolutionX = resolutionX;
                mesh.ResolutionY = resolutionY;
                if (sourcePoints.Count == 0) return mesh;

                double minX = minp.X;
                double maxX = maxp.X;
                double minY = minp.Y;
                double maxY = maxp.Y;

                double stepX = (maxX - minX) / Math.Max(1, resolutionX - 1);
                double stepY = (maxY - minY) / Math.Max(1, resolutionY - 1);
                double smoothing = Math.Sqrt(stepX * stepX + stepY * stepY) * 0.5;

                for (int j = 0; j < resolutionY; j++)
                {
                    for (int i = 0; i < resolutionX; i++)
                    {
                        double x = minX + i * stepX;
                        double y = minY + j * stepY;
                        double z = CalculateIdw(new Point3D(x, y, 0), sourcePoints, power, smoothing);
                        mesh.Positions.Add(new Point3D(x, y, z));
                    }
                }

                for (int j = 0; j < resolutionY - 1; j++)
                {
                    for (int i = 0; i < resolutionX - 1; i++)
                    {
                        int i0 = j * resolutionX + i;
                        int i1 = i0 + 1;
                        int i2 = i0 + resolutionX;
                        int i3 = i2 + 1;

                        mesh.TriangleIndices.Add(i0);
                        mesh.TriangleIndices.Add(i2);
                        mesh.TriangleIndices.Add(i1);

                        mesh.TriangleIndices.Add(i1);
                        mesh.TriangleIndices.Add(i2);
                        mesh.TriangleIndices.Add(i3);
                    }
                }

                return mesh;
            }

            public static double CalculateIdw(Point3D target, List<Point3D> points, double power = 2.0, double smoothing = 0.0)
            {
                double sumWeights = 0;
                double sumWeightedValues = 0;

                foreach (var p in points)
                {
                    double dx = target.X - p.X;
                    double dy = target.Y - p.Y;
                    double distSq = dx * dx + dy * dy;
                    double d = Math.Sqrt(distSq + smoothing * smoothing);

                    if (d < 0.0001) return p.Z;

                    double weight = 1.0 / Math.Pow(d, power);
                    sumWeights += weight;
                    sumWeightedValues += weight * p.Z;
                }

                return sumWeightedValues / (sumWeights + 1e-12);
            }

            public static Danmen CalcDanmen(Point3D start, Point3D end, Point3D dp, List<Point3D> sourcePoints, int cnt = 100, double power = 5.0)
            {
                var danmen = new Danmen();
                if (sourcePoints.Count == 0) return danmen;

                double dx = end.X - start.X;
                double dy = end.Y - start.Y;
                double totalDist = Math.Sqrt(dx * dx + dy * dy);
                double smoothing = totalDist / (cnt > 1 ? cnt - 1 : 1) * 0.5;

                for (int i = 0; i < cnt; i++)
                {
                    double t = (double)i / Math.Max(1, cnt - 1);
                    double x = start.X + dx * t;
                    double y = start.Y + dy * t;
                    double z = CalculateIdw(new Point3D(x, y, 0), sourcePoints, power, smoothing);
                    danmen.danmen.Add((t * totalDist, z));
                }

                danmen.sp = start;
                danmen.ep = end;
                danmen.dp = dp;
                danmen.cnt = cnt;
                return danmen;
            }
        }

        private readonly DrawingIkouModel _targetIkou;
        private readonly DanmenRec? _targetDanmenRec;
        private readonly List<Point3D> _allLocalPoints = new List<Point3D>();
        private GridMesh? _currentMesh;
        private Danmen? _currentDanmen;

        private bool _is3DViewMode = false;
        private int _danmenStep = 0; // 0: Idle, 1: Pick Sp, 2: Pick Ep, 3: Pick Dp
        private Point3D? _tempSp = null;
        private Point3D? _tempEp = null;
        private Point3D? _tempDp = null;

        private Point3D? _sectionStartPoint = null;
        private Point3D? _sectionEndPoint = null;
        private Point3D? _sectionPlacementPoint = null;

        private Point _mouseCanvasPos;
        private bool _isRightMouseDown = false;
        private bool _isMiddleMouseDown = false;
        private bool _isLeftMouseDownForPanOrRotate = false;
        private Point _lastDragMousePos;

        private double _zoom2D = 1.0;
        private double _pan2DX = 0.0;
        private double _pan2DY = 0.0;

        private double _zoom3D = 1.0;
        private double _pan3DX = 0.0;
        private double _pan3DY = 0.0;

        public DanmenRec? ResultDanmenRec { get; private set; }

        public FormIkou3D(DrawingIkouModel ikou, DanmenRec? targetDanmen = null)
        {
            InitializeComponent();
            _targetIkou = ikou;
            _targetDanmenRec = targetDanmen;

            // Transform all 3D points from Survey space into Crop Box Local Center Space (補間ポイントも含めて全3D点群をメッシュ生成用に登録)
            var spline = new Xross_Spline();
            foreach (var line in _targetIkou.LList)
            {
                if (line.Pnts.Count == 0) continue;
                var localPnts = line.Pnts.Select(pt =>
                {
                    var (lx, ly) = GeometryMath.SurveyToFeatureLocalCenter(pt.X, pt.Y, _targetIkou.P1, _targetIkou.P2, _targetIkou.P3);
                    return new Point3D(lx, ly, pt.Z);
                }).ToList();

                bool isClosed = (line.Flag == 1);
                if (!isClosed && localPnts.Count >= 3)
                {
                    var pFirst = localPnts[0];
                    var pLast = localPnts[^1];
                    double dist = Math.Sqrt(Math.Pow(pFirst.X - pLast.X, 2) + Math.Pow(pFirst.Y - pLast.Y, 2));
                    if (dist < 0.05) isClosed = true;
                }

                List<Point3D> effectiveLocalPnts = (localPnts.Count >= 3)
                    ? (isClosed ? spline.Calc3DCloseCurvePoints(localPnts, 5) : spline.Calc3DCurvePoints(localPnts, 5))
                    : localPnts;

                foreach (var pt in effectiveLocalPnts)
                {
                    _allLocalPoints.Add(pt);
                }
            }

            // Initialize section points from targetDanmen if specified
            if (_targetDanmenRec != null && _targetDanmenRec.Sp != null && _targetDanmenRec.Ep != null)
            {
                var (spLx, spLy) = GeometryMath.SurveyToFeatureLocalCenter(_targetDanmenRec.Sp.X, _targetDanmenRec.Sp.Y, _targetIkou.P1, _targetIkou.P2, _targetIkou.P3);
                var (epLx, epLy) = GeometryMath.SurveyToFeatureLocalCenter(_targetDanmenRec.Ep.X, _targetDanmenRec.Ep.Y, _targetIkou.P1, _targetIkou.P2, _targetIkou.P3);
                _sectionStartPoint = new Point3D(spLx, spLy, 0);
                _sectionEndPoint = new Point3D(epLx, epLy, 0);

                if (_targetDanmenRec.Dp != null)
                {
                    var (dpLx, dpLy) = GeometryMath.SurveyToFeatureLocalCenter(_targetDanmenRec.Dp.X, _targetDanmenRec.Dp.Y, _targetIkou.P1, _targetIkou.P2, _targetIkou.P3);
                    _sectionPlacementPoint = new Point3D(dpLx, dpLy, 0);
                }
            }

            cmbGridResolution.SelectedIndex = 1; // 中 (50分割)

            WireEventHandlers();
            PopulateSummaryInfo();
            CalculateGridMesh();
        }

        private void WireEventHandlers()
        {
            cmbGridResolution.SelectedIndexChanged += (s, e) => CalculateGridMesh();
            btnGridCalc.Click += (s, e) => CalculateGridMesh();
            chkIkouHeight.CheckedChanged += (s, e) => picCanvas3D.Invalidate();
            chkShowElevation.CheckedChanged += (s, e) => picCanvas3D.Invalidate();

            btnView2D.Click += (s, e) => SwitchViewMode(false);
            btnView3D.Click += (s, e) => SwitchViewMode(true);

            tbRotateV.ValueChanged += (s, e) => picCanvas3D.Invalidate();
            tbRotateH.ValueChanged += (s, e) => picCanvas3D.Invalidate();

            btnSectionPick.Click += (s, e) =>
            {
                _danmenStep = 1;
                _tempSp = null;
                _tempEp = null;
                _tempDp = null;
                SwitchViewMode(false); // Force 2D view for picking section line
            };

            btnDanmenSet.Click += (s, e) => { this.DialogResult = DialogResult.OK; this.Close(); };

            picCanvas3D.Paint += picCanvas3D_Paint;
            picCanvas3D.MouseDown += picCanvas3D_MouseDown;
            picCanvas3D.MouseMove += picCanvas3D_MouseMove;
            picCanvas3D.MouseUp += picCanvas3D_MouseUp;
            picCanvas3D.MouseWheel += picCanvas3D_MouseWheel;
            picCanvas3D.MouseDoubleClick += picCanvas3D_MouseDoubleClick;
        }

        public void SwitchViewMode(bool is3D)
        {
            _is3DViewMode = is3D;
            grpRotationControls.Visible = is3D;
            grpDanmenControls.Visible = !is3D;

            btnView2D.BackColor = !is3D ? Color.FromArgb(0, 180, 216) : SystemColors.Control;
            btnView2D.ForeColor = !is3D ? Color.White : Color.Black;

            btnView3D.BackColor = is3D ? Color.FromArgb(0, 180, 216) : SystemColors.Control;
            btnView3D.ForeColor = is3D ? Color.White : Color.Black;

            picCanvas3D.Invalidate();
        }

        private void PopulateSummaryInfo()
        {
            txtIkouName.Text = _targetIkou.Name;
            txtND.Text = _allLocalPoints.Count.ToString();
            txtN.Text = "--";

            if (_allLocalPoints.Count > 0)
            {
                double minX = _allLocalPoints.Min(p => p.X);
                double minY = _allLocalPoints.Min(p => p.Y);
                double maxX = _allLocalPoints.Max(p => p.X);
                double maxY = _allLocalPoints.Max(p => p.Y);

                txtMinX.Text = minX.ToString("0.000", CultureInfo.InvariantCulture);
                txtMinY.Text = minY.ToString("0.000", CultureInfo.InvariantCulture);
                txtMaxX.Text = maxX.ToString("0.000", CultureInfo.InvariantCulture);
                txtMaxY.Text = maxY.ToString("0.000", CultureInfo.InvariantCulture);
            }
        }

        private void CalculateGridMesh()
        {
            if (_allLocalPoints.Count == 0) return;

            // グリッド範囲をクリッピングされた遺構枠 (-widthM/2 ~ +widthM/2, -heightM/2 ~ +heightM/2) 内に限定
            var (_, widthM, heightM, _, _, _, _, _) = GeometryMath.CalculateCropBox(_targetIkou.P1, _targetIkou.P2, _targetIkou.P3);

            Point3D minp = new Point3D(-widthM / 2.0, -heightM / 2.0, 0);
            Point3D maxp = new Point3D(+widthM / 2.0, +heightM / 2.0, 0);

            int res = cmbGridResolution.SelectedIndex switch
            {
                0 => 25,
                2 => 100,
                _ => 50
            };

            _currentMesh = GridAlgorithm.CreateGridMesh(minp, maxp, _allLocalPoints, res, res);

            // Create default section line if none specified yet
            if (_sectionStartPoint == null || _sectionEndPoint == null)
            {
                _sectionStartPoint = new Point3D(-widthM / 3.0, 0, 0);
                _sectionEndPoint = new Point3D(+widthM / 3.0, 0, 0);
                _sectionPlacementPoint = new Point3D(-widthM / 3.0, -heightM / 2.0 + 0.2, 0);
            }

            UpdateSectionProfile();
            picCanvas3D.Invalidate();
        }

        private void UpdateSectionProfile()
        {
            if (_sectionStartPoint == null || _sectionEndPoint == null || _allLocalPoints.Count == 0) return;

            Point3D dp = _sectionPlacementPoint ?? new Point3D(_sectionStartPoint.X, _sectionStartPoint.Y - 1.0, 0);
            _currentDanmen = GridAlgorithm.CalcDanmen(_sectionStartPoint, _sectionEndPoint, dp, _allLocalPoints, 100);
        }

        private void picCanvas3D_Paint(object? sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Color.White);

            if (_allLocalPoints.Count == 0) return;

            int w = picCanvas3D.Width;
            int h = picCanvas3D.Height;

            if (_is3DViewMode)
            {
                Render3DView(g, w, h);
            }
            else
            {
                Render2DView(g, w, h);
            }
        }

        private void Render2DView(Graphics g, int w, int h)
        {
            var (_, widthM, heightM, _, _, _, _, _) = GeometryMath.CalculateCropBox(_targetIkou.P1, _targetIkou.P2, _targetIkou.P3);

            double dX = widthM;
            double dY = heightM;
            double cx = _pan2DX;
            double cy = _pan2DY;

            int margin = 80;
            double baseScale = Math.Min((w - margin * 2) / Math.Max(0.1, dX * 1.3), (h - margin * 2) / Math.Max(0.1, dY * 1.3));
            double scale = baseScale * _zoom2D;

            PointF LocalTo2D(double lx, double ly)
            {
                float px = (w / 2f) + (float)((lx - cx) * scale);
                float py = (h / 2f) - (float)((ly - cy) * scale);
                return new PointF(px, py);
            }

            // Crop Box Frame Vertices
            PointF ptBL = LocalTo2D(-widthM / 2.0, -heightM / 2.0);
            PointF ptBR = LocalTo2D(+widthM / 2.0, -heightM / 2.0);
            PointF ptTR = LocalTo2D(+widthM / 2.0, +heightM / 2.0);
            PointF ptTL = LocalTo2D(-widthM / 2.0, +heightM / 2.0);

            // Draw 2D Grid Mesh (クリッピングされた遺構枠内のみに描画)
            using (var clipPath = new GraphicsPath())
            {
                clipPath.AddPolygon(new[] { ptBL, ptBR, ptTR, ptTL });
                Region oldClip = g.Clip;
                g.SetClip(clipPath);

                if (_currentMesh != null && _currentMesh.Positions.Count > 0)
                {
                    using (var gridPen = new Pen(Color.FromArgb(220, 220, 225), 1f))
                    {
                        int rx = _currentMesh.ResolutionX;
                        int ry = _currentMesh.ResolutionY;

                        for (int j = 0; j < ry; j++)
                        {
                            var linePts = new List<PointF>();
                            for (int i = 0; i < rx; i++)
                            {
                                var pt = _currentMesh.Positions[j * rx + i];
                                linePts.Add(LocalTo2D(pt.X, pt.Y));
                            }
                            if (linePts.Count > 1) g.DrawLines(gridPen, linePts.ToArray());
                        }

                        for (int i = 0; i < rx; i++)
                        {
                            var linePts = new List<PointF>();
                            for (int j = 0; j < ry; j++)
                            {
                                var pt = _currentMesh.Positions[j * rx + i];
                                linePts.Add(LocalTo2D(pt.X, pt.Y));
                            }
                            if (linePts.Count > 1) g.DrawLines(gridPen, linePts.ToArray());
                        }

                        // 標高値の薄灰色表示 (chkShowElevation が ON の場合)
                        if (chkShowElevation.Checked)
                        {
                            using (var elevFont = new Font("Yu Gothic UI", 7.5F))
                            using (var elevBrush = new SolidBrush(Color.FromArgb(170, 170, 175)))
                            {
                                int step = rx >= 50 ? 2 : 1;
                                for (int j = 0; j < ry; j += step)
                                {
                                    for (int i = 0; i < rx; i += step)
                                    {
                                        var pt = _currentMesh.Positions[j * rx + i];
                                        PointF pt2D = LocalTo2D(pt.X, pt.Y);
                                        if (pt2D.X >= -10 && pt2D.X <= w + 10 && pt2D.Y >= -10 && pt2D.Y <= h + 10)
                                        {
                                            g.DrawString(pt.Z.ToString("0.000", CultureInfo.InvariantCulture), elevFont, elevBrush, pt2D.X - 14f, pt2D.Y - 6f);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                // Draw 2D Feature Lines (複数遺構指定時も枠内のみに厳密にクリッピング)
                var spline = new Xross_Spline();
                foreach (var line in _targetIkou.LList)
                {
                    if (line.Pnts.Count == 0) continue;
                    var localPnts = line.Pnts.Select(p =>
                    {
                        var (lx, ly) = GeometryMath.SurveyToFeatureLocalCenter(p.X, p.Y, _targetIkou.P1, _targetIkou.P2, _targetIkou.P3);
                        return new Point3D(lx, ly, p.Z);
                    }).ToList();

                    List<Point3D> renderPnts = (localPnts.Count >= 3)
                        ? (line.Flag == 1 ? spline.Calc3DCloseCurvePoints(localPnts, 5) : spline.Calc3DCurvePoints(localPnts, 5))
                        : localPnts;

                    var pts = renderPnts.Select(p => LocalTo2D(p.X, p.Y)).ToArray();
                    if (pts.Length > 1)
                    {
                        Color col = (line.Flag == 1 && _targetIkou.LList.IndexOf(line) > 0) ? Color.Red : Color.Black;
                        using (var pen = new Pen(col, 1.8f))
                        {
                            g.DrawLines(pen, pts);
                        }
                    }
                }

                g.Clip = oldClip;
            }

            // Draw Crop Box Frame (Light Gray Dashed Line)
            using (var framePen = new Pen(Color.FromArgb(170, 180, 185), 1.5f) { DashStyle = DashStyle.Dash })
            {
                g.DrawPolygon(framePen, new[] { ptBL, ptBR, ptTR, ptTL });
            }

            // Draw Section Cut Line, Baseline (Blue) & Section Profile Curve (Red)
            RenderSectionDrawing2D(g, scale, LocalTo2D);

            // 3点指示インタラクティブ・ラバーバンド描画 (1点目:Sp, 2点目:Ep, 3点目:Dp 対辺長方形ラバーバンド)
            if (_danmenStep > 0)
            {
                using (var rubberPen = new Pen(Color.FromArgb(255, 191, 0), 2.2f) { DashStyle = DashStyle.Dash })
                using (var font = new Font("Yu Gothic UI", 9F, FontStyle.Bold))
                using (var textBrush = new SolidBrush(Color.FromArgb(255, 191, 0)))
                {
                    if (_danmenStep == 2 && _tempSp != null)
                    {
                        PointF pt1 = LocalTo2D(_tempSp.X, _tempSp.Y);
                        g.DrawLine(rubberPen, pt1, _mouseCanvasPos);
                        g.FillEllipse(textBrush, pt1.X - 5f, pt1.Y - 5f, 10f, 10f);
                        g.DrawString("1.Sp (切断開始)", font, textBrush, pt1.X + 8f, pt1.Y - 8f);
                        g.DrawString("2.Ep (切断終了指示)", font, textBrush, _mouseCanvasPos.X + 8f, _mouseCanvasPos.Y - 8f);
                    }
                    else if (_danmenStep == 3 && _tempSp != null && _tempEp != null)
                    {
                        PointF p1 = LocalTo2D(_tempSp.X, _tempSp.Y);
                        PointF p2 = LocalTo2D(_tempEp.X, _tempEp.Y);

                        // 断面切断線 Sp -> Ep の方向ベクトルおよび垂直ベクトル
                        double dx = _tempEp.X - _tempSp.X;
                        double dy = _tempEp.Y - _tempSp.Y;
                        double len = Math.Max(1e-6, Math.Sqrt(dx * dx + dy * dy));
                        double ux = dx / len;
                        double uy = dy / len;
                        double vx = -uy;
                        double vy = ux;

                        // マウスカーソルのローカル座標から垂線オフセット高さ hOffset を算出
                        Point3D mouseLocal = CanvasToLocal(_mouseCanvasPos);
                        double pdx = mouseLocal.X - _tempSp.X;
                        double pdy = mouseLocal.Y - _tempSp.Y;
                        double hOffset = pdx * vx + pdy * vy;

                        // 3点目長方形ラバーバンドの4頂点 (対辺 Side 1: Sp->Ep, 対辺 Side 3: Dp_start->Dp_end)
                        Point3D v3Local = new Point3D(_tempEp.X + vx * hOffset, _tempEp.Y + vy * hOffset, 0);
                        Point3D v4Local = new Point3D(_tempSp.X + vx * hOffset, _tempSp.Y + vy * hOffset, 0);

                        PointF p3 = LocalTo2D(v3Local.X, v3Local.Y);
                        PointF p4 = LocalTo2D(v4Local.X, v4Local.Y);

                        // 長方形枠ラバーバンド描画 (対辺 Side 1: Sp->Ep, 対辺 Side 3: Dp_start->Dp_end)
                        g.DrawPolygon(rubberPen, new[] { p1, p2, p3, p4 });

                        g.FillEllipse(textBrush, p1.X - 4f, p1.Y - 4f, 8f, 8f);
                        g.FillEllipse(textBrush, p2.X - 4f, p2.Y - 4f, 8f, 8f);
                        g.DrawString("1.Sp (断面切断線)", font, textBrush, p1.X + 8f, p1.Y - 8f);
                        g.DrawString("2.Ep", font, textBrush, p2.X + 8f, p2.Y - 8f);

                        // 対辺プロファイル基線 (p4 -> p3) & 3点目配置目標表示
                        g.DrawString("3.Dp (断面対辺配置位置)", font, textBrush, _mouseCanvasPos.X + 10f, _mouseCanvasPos.Y - 8f);
                    }
                }
            }
        }

        private static void DrawRotatedString(Graphics g, string text, Font font, Brush brush, PointF pos, double angleDegrees, StringAlignment alignH = StringAlignment.Center, StringAlignment alignV = StringAlignment.Center)
        {
            double drawAngle = angleDegrees;
            while (drawAngle > 90) drawAngle -= 180;
            while (drawAngle < -90) drawAngle += 180;

            var state = g.Save();
            g.TranslateTransform(pos.X, pos.Y);
            g.RotateTransform((float)drawAngle);

            using (var format = new StringFormat())
            {
                format.Alignment = alignH;
                format.LineAlignment = alignV;
                g.DrawString(text, font, brush, 0, 0, format);
            }

            g.Restore(state);
        }

        // Draw Confirmed Section Cut Line (Red) & Section Profile Curve (Red) below Opposite Parallel Side (Blue)
        private void RenderSectionDrawing2D(Graphics g, double scale, Func<double, double, PointF> LocalTo2D)
        {
            if (_sectionStartPoint == null || _sectionEndPoint == null) return;

            PointF sp = LocalTo2D(_sectionStartPoint.X, _sectionStartPoint.Y);
            PointF ep = LocalTo2D(_sectionEndPoint.X, _sectionEndPoint.Y);

            double dxScr = ep.X - sp.X;
            double dyScr = ep.Y - sp.Y;
            double lenScr = Math.Max(1e-6, Math.Sqrt(dxScr * dxScr + dyScr * dyScr));

            double uX = dxScr / lenScr;
            double uY = dyScr / lenScr;

            // 90度垂直単位ベクトル (対辺方向)
            double vX = uY;
            double vY = -uX;

            double lineAngleDeg = Math.Atan2(dyScr, dxScr) * 180.0 / Math.PI;

            string secName = !string.IsNullOrWhiteSpace(_targetDanmenRec?.Name) ? _targetDanmenRec.Name.Trim() : "A";

            using (var cutLinePen = new Pen(Color.Red, 2.2f))
            using (var baseLinePen = new Pen(Color.Blue, 2.2f))
            using (var profilePen = new Pen(Color.Red, 2.5f))
            using (var font = new Font("Yu Gothic UI", 10F, FontStyle.Bold))
            using (var labelBrush = new SolidBrush(Color.Pink))
            {
                // 1. 断面切断線 (Red)
                g.DrawLine(cutLinePen, sp, ep);

                // 断面名 (例: B, B') ラベル (赤線の傾斜に合わせて表示)
                PointF spLabelPos = new PointF((float)(sp.X - uX * 14), (float)(sp.Y - uY * 14));
                PointF epLabelPos = new PointF((float)(ep.X + uX * 14), (float)(ep.Y + uY * 14));

                DrawRotatedString(g, secName, font, labelBrush, spLabelPos, lineAngleDeg, StringAlignment.Far, StringAlignment.Center);
                DrawRotatedString(g, $"{secName}'", font, labelBrush, epLabelPos, lineAngleDeg, StringAlignment.Near, StringAlignment.Center);

                // 2. 対辺基線 (Blue)
                if (_currentDanmen != null && _currentDanmen.danmen.Count > 1)
                {
                    PointF dpStart = _sectionPlacementPoint != null ? LocalTo2D(_sectionPlacementPoint.X, _sectionPlacementPoint.Y) : new PointF(sp.X, sp.Y + 60f);
                    PointF dpEnd = new PointF((float)(dpStart.X + lenScr * uX), (float)(dpStart.Y + lenScr * uY));

                    g.DrawLine(baseLinePen, dpStart, dpEnd);

                    // 基準標高計算 (断面の最高標高 + 0.1m(10cm) 以上でキリの良い標高)
                    double maxElev = _currentDanmen.danmen.Max(d => d.Elevation);
                    double rawBaseElev = maxElev + 0.10;
                    double baseElev = Math.Ceiling(rawBaseElev * 10.0) / 10.0;

                    // 青線に対しても 断面名 (例: B, B' 8.900m) 標高を表示
                    PointF dpStartLabelPos = new PointF((float)(dpStart.X - uX * 14), (float)(dpStart.Y - uY * 14));
                    PointF dpEndLabelPos = new PointF((float)(dpEnd.X + uX * 14), (float)(dpEnd.Y + uY * 14));

                    DrawRotatedString(g, secName, font, labelBrush, dpStartLabelPos, lineAngleDeg, StringAlignment.Far, StringAlignment.Center);
                    DrawRotatedString(g, $"{secName}'{baseElev:0.000}m", font, labelBrush, dpEndLabelPos, lineAngleDeg, StringAlignment.Near, StringAlignment.Center);

                    // 3. 青色基線の下側（切断線から離れる正地方向）へ赤色の断面曲線を描画
                    // 切断線 Sp から対辺基線 dpStart へ向かう正規化垂直ベクトル (nX, nY)
                    double pdx = dpStart.X - sp.X;
                    double pdy = dpStart.Y - sp.Y;
                    double pdLen = Math.Max(1e-6, Math.Sqrt(pdx * pdx + pdy * pdy));
                    double nX = pdx / pdLen;
                    double nY = pdy / pdLen;

                    var screenProfile = new List<PointF>();
                    for (int i = 0; i < _currentDanmen.danmen.Count; i++)
                    {
                        double t = (double)i / (_currentDanmen.danmen.Count - 1);
                        float bx = (float)(dpStart.X + t * (dpEnd.X - dpStart.X));
                        float by = (float)(dpStart.Y + t * (dpEnd.Y - dpStart.Y));

                        // 基準標高からの深さオフセット (1:1 正規実寸比率: depthM * scale)
                        double depthM = baseElev - _currentDanmen.danmen[i].Elevation;
                        double depthScr = depthM * scale;

                        float px = (float)(bx + nX * depthScr);
                        float py = (float)(by + nY * depthScr);

                        screenProfile.Add(new PointF(px, py));
                    }

                    if (screenProfile.Count > 1)
                    {
                        g.DrawLines(profilePen, screenProfile.ToArray());
                    }
                }
            }
        }

        private void Render3DView(Graphics g, int w, int h)
        {
            var (_, widthM, heightM, _, _, _, _, _) = GeometryMath.CalculateCropBox(_targetIkou.P1, _targetIkou.P2, _targetIkou.P3);

            double minZ = _allLocalPoints.Count > 0 ? _allLocalPoints.Min(p => p.Z) : 0;
            double maxZ = _allLocalPoints.Count > 0 ? _allLocalPoints.Max(p => p.Z) : 0;

            double cx = _pan3DX;
            double cy = _pan3DY;
            double cz = (minZ + maxZ) / 2.0;

            double rotH = tbRotateH.Value * Math.PI / 180.0;
            double rotV = tbRotateV.Value * Math.PI / 180.0;

            int margin = 80;
            double baseScale = Math.Min((w - margin * 2) / Math.Max(0.1, widthM * 1.4), (h - margin * 2) / Math.Max(0.1, heightM * 1.4));
            double scale = baseScale * _zoom3D;

            PointF Project3D(Point3D pt)
            {
                double dx = pt.X - cx;
                double dy = pt.Y - cy;
                double dz = (pt.Z - cz) * 2.0;

                double rx = dx * Math.Cos(rotH) - dy * Math.Sin(rotH);
                double ry = dx * Math.Sin(rotH) + dy * Math.Cos(rotH);
                double rz = dz;

                double px = (w / 2f) + (float)(rx * scale);
                double py = (h / 2f) - (float)((ry * Math.Sin(rotV) + rz * Math.Cos(rotV)) * scale);
                return new PointF((float)px, (float)py);
            }

            // Draw 3D Grid Surface Wireframe Mesh (クリッピング枠範囲)
            if (_currentMesh != null && _currentMesh.Positions.Count > 0)
            {
                using (var meshPen = new Pen(Color.FromArgb(200, 200, 205), 1f))
                {
                    int rx = _currentMesh.ResolutionX;
                    int ry = _currentMesh.ResolutionY;

                    for (int j = 0; j < ry; j++)
                    {
                        var pts = new List<PointF>();
                        for (int i = 0; i < rx; i++)
                        {
                            pts.Add(Project3D(_currentMesh.Positions[j * rx + i]));
                        }
                        if (pts.Count > 1) g.DrawLines(meshPen, pts.ToArray());
                    }

                    for (int i = 0; i < rx; i++)
                    {
                        var pts = new List<PointF>();
                        for (int j = 0; j < ry; j++)
                        {
                            pts.Add(Project3D(_currentMesh.Positions[j * rx + i]));
                        }
                        if (pts.Count > 1) g.DrawLines(meshPen, pts.ToArray());
                    }
                }
            }

            // Draw 3D Feature Boundary Lines & Points (遺構線 ＋ 補間ポイント ＋ 制御点)
            var spline = new Xross_Spline();
            int lineIdx = 0;
            using (var interpBrush = new SolidBrush(Color.FromArgb(0, 180, 255)))
            using (var ctrlBrush = new SolidBrush(Color.FromArgb(255, 215, 0)))
            using (var ctrlOutlinePen = new Pen(Color.FromArgb(220, 20, 20), 1.8f))
            {
                foreach (var line in _targetIkou.LList)
                {
                    if (line.Pnts.Count == 0) continue;
                    var localPnts = line.Pnts.Select(p =>
                    {
                        var (lx, ly) = GeometryMath.SurveyToFeatureLocalCenter(p.X, p.Y, _targetIkou.P1, _targetIkou.P2, _targetIkou.P3);
                        return new Point3D(lx, ly, p.Z);
                    }).ToList();

                    if (localPnts.Count == 0) continue;

                    bool isClosed = (line.Flag == 1);
                    if (!isClosed && localPnts.Count >= 3)
                    {
                        var pFirst = localPnts[0];
                        var pLast = localPnts[^1];
                        double dist = Math.Sqrt(Math.Pow(pFirst.X - pLast.X, 2) + Math.Pow(pFirst.Y - pLast.Y, 2));
                        if (dist < 0.05) isClosed = true;
                    }

                    List<Point3D> renderPnts = (localPnts.Count >= 3)
                        ? (isClosed ? spline.Calc3DCloseCurvePoints(localPnts, 5) : spline.Calc3DCurvePoints(localPnts, 5))
                        : localPnts;

                    var pts = renderPnts.Select(p => Project3D(p)).ToArray();
                    if (pts.Length > 1)
                    {
                        Color col = (lineIdx == 0) ? Color.FromArgb(220, 30, 60) : Color.FromArgb(0, 140, 240);
                        using (var pen = new Pen(col, 2.5f))
                        {
                            g.DrawLines(pen, pts);
                        }
                    }

                    // 1. 補間されたポイント (Interpolated Points) の表示
                    foreach (var p in pts)
                    {
                        g.FillEllipse(interpBrush, p.X - 3.5f, p.Y - 3.5f, 7f, 7f);
                    }

                    // 2. 元の制御点・測量点 (Original Control Points) の強調表示
                    foreach (var cp in localPnts)
                    {
                        PointF p = Project3D(cp);
                        g.FillEllipse(ctrlBrush, p.X - 5.5f, p.Y - 5.5f, 11f, 11f);
                        g.DrawEllipse(ctrlOutlinePen, p.X - 5.5f, p.Y - 5.5f, 11f, 11f);
                    }

                    lineIdx++;
                }
            }
        }

        private void picCanvas3D_MouseWheel(object? sender, MouseEventArgs e)
        {
            double factor = e.Delta > 0 ? 1.15 : (1.0 / 1.15);
            int w = picCanvas3D.Width;
            int h = picCanvas3D.Height;
            int margin = 80;

            if (_is3DViewMode)
            {
                var (_, widthM, heightM, _, _, _, _, _) = GeometryMath.CalculateCropBox(_targetIkou.P1, _targetIkou.P2, _targetIkou.P3);
                double baseScale = Math.Min((w - margin * 2) / Math.Max(0.1, widthM * 1.4), (h - margin * 2) / Math.Max(0.1, heightM * 1.4));

                double scaleOld = baseScale * _zoom3D;
                double zoom3DNew = Math.Clamp(_zoom3D * factor, 0.05, 50.0);
                double scaleNew = baseScale * zoom3DNew;

                double diff = (1.0 / scaleOld) - (1.0 / scaleNew);
                _pan3DX += (e.X - w / 2f) * diff;
                _pan3DY -= (e.Y - h / 2f) * diff;
                _zoom3D = zoom3DNew;
            }
            else
            {
                var (_, widthM, heightM, _, _, _, _, _) = GeometryMath.CalculateCropBox(_targetIkou.P1, _targetIkou.P2, _targetIkou.P3);
                double dX = widthM;
                double dY = heightM;
                double baseScale = Math.Min((w - margin * 2) / Math.Max(0.1, dX * 1.3), (h - margin * 2) / Math.Max(0.1, dY * 1.3));

                double scaleOld = baseScale * _zoom2D;
                double zoom2DNew = Math.Clamp(_zoom2D * factor, 0.05, 50.0);
                double scaleNew = baseScale * zoom2DNew;

                double diff = (1.0 / scaleOld) - (1.0 / scaleNew);
                _pan2DX += (e.X - w / 2f) * diff;
                _pan2DY -= (e.Y - h / 2f) * diff;
                _zoom2D = zoom2DNew;
            }
            picCanvas3D.Invalidate();
        }

        private void picCanvas3D_MouseDoubleClick(object? sender, MouseEventArgs e)
        {
            if (_is3DViewMode)
            {
                _zoom3D = 1.0;
                _pan3DX = 0.0;
                _pan3DY = 0.0;
                tbRotateH.Value = 0;
                tbRotateV.Value = 35;
            }
            else
            {
                _zoom2D = 1.0;
                _pan2DX = 0.0;
                _pan2DY = 0.0;
            }
            picCanvas3D.Invalidate();
        }

        private void picCanvas3D_MouseDown(object? sender, MouseEventArgs e)
        {
            _lastDragMousePos = e.Location;

            if (e.Button == MouseButtons.Middle)
            {
                _isMiddleMouseDown = true;
                return;
            }

            if (e.Button == MouseButtons.Right)
            {
                if (!_is3DViewMode && _danmenStep > 0)
                {
                    if (_danmenStep == 3)
                    {
                        _danmenStep = 2;
                        _tempDp = null;
                    }
                    else if (_danmenStep == 2)
                    {
                        _danmenStep = 1;
                        _tempEp = null;
                    }
                    else if (_danmenStep == 1)
                    {
                        _danmenStep = 0;
                        _tempSp = null;
                    }
                    picCanvas3D.Invalidate();
                    return;
                }

                _isRightMouseDown = true;
                return;
            }

            if (e.Button == MouseButtons.Left)
            {
                if (!_is3DViewMode && _danmenStep > 0)
                {
                    var localPt = CanvasToLocal(e.Location);
                    if (_danmenStep == 1)
                    {
                        _tempSp = localPt;
                        _danmenStep = 2;
                    }
                    else if (_danmenStep == 2)
                    {
                        _tempEp = localPt;
                        _danmenStep = 3;
                    }
                    else if (_danmenStep == 3)
                    {
                        if (_tempSp != null && _tempEp != null)
                        {
                            double dx = _tempEp.X - _tempSp.X;
                            double dy = _tempEp.Y - _tempSp.Y;
                            double len = Math.Max(1e-6, Math.Sqrt(dx * dx + dy * dy));
                            double ux = dx / len;
                            double uy = dy / len;
                            double vx = -uy;
                            double vy = ux;

                            double pdx = localPt.X - _tempSp.X;
                            double pdy = localPt.Y - _tempSp.Y;
                            double hOffset = pdx * vx + pdy * vy;

                            _tempDp = new Point3D(_tempSp.X + vx * hOffset, _tempSp.Y + vy * hOffset, 0);
                        }
                        else
                        {
                            _tempDp = localPt;
                        }

                        _danmenStep = 0;
                        _sectionStartPoint = _tempSp;
                        _sectionEndPoint = _tempEp;
                        _sectionPlacementPoint = _tempDp;
                        UpdateSectionProfile();
                    }
                    picCanvas3D.Invalidate();
                    return;
                }

                _isLeftMouseDownForPanOrRotate = true;
            }
        }

        private void picCanvas3D_MouseMove(object? sender, MouseEventArgs e)
        {
            _mouseCanvasPos = e.Location;
            int dx = e.X - _lastDragMousePos.X;
            int dy = e.Y - _lastDragMousePos.Y;
            _lastDragMousePos = e.Location;

            if (_danmenStep > 0 && !_is3DViewMode)
            {
                picCanvas3D.Invalidate();
                return;
            }

            bool isPanDrag = _isMiddleMouseDown ||
                             (!_is3DViewMode && _isLeftMouseDownForPanOrRotate) ||
                             (!_is3DViewMode && _isRightMouseDown && _danmenStep == 0) ||
                             (_is3DViewMode && ((Control.ModifierKeys & Keys.Shift) != 0) && (_isLeftMouseDownForPanOrRotate || _isRightMouseDown));

            if (isPanDrag)
            {
                if (_is3DViewMode)
                {
                    var (_, widthM, heightM, _, _, _, _, _) = GeometryMath.CalculateCropBox(_targetIkou.P1, _targetIkou.P2, _targetIkou.P3);
                    int margin = 80;
                    double baseScale = Math.Min((picCanvas3D.Width - margin * 2) / Math.Max(0.1, widthM * 1.4), (picCanvas3D.Height - margin * 2) / Math.Max(0.1, heightM * 1.4));
                    double scale = Math.Max(0.001, baseScale * _zoom3D);

                    _pan3DX -= dx / scale;
                    _pan3DY += dy / scale;
                }
                else
                {
                    var (_, widthM, heightM, _, _, _, _, _) = GeometryMath.CalculateCropBox(_targetIkou.P1, _targetIkou.P2, _targetIkou.P3);
                    int margin = 80;
                    double baseScale = Math.Min((picCanvas3D.Width - margin * 2) / Math.Max(0.1, widthM * 1.3), (picCanvas3D.Height - margin * 2) / Math.Max(0.1, heightM * 1.3));
                    double scale = Math.Max(0.001, baseScale * _zoom2D);

                    _pan2DX -= dx / scale;
                    _pan2DY += dy / scale;
                }
                picCanvas3D.Invalidate();
                return;
            }

            if (_is3DViewMode && (_isRightMouseDown || _isLeftMouseDownForPanOrRotate))
            {
                tbRotateH.Value = Math.Clamp(tbRotateH.Value + dx, tbRotateH.Minimum, tbRotateH.Maximum);
                tbRotateV.Value = Math.Clamp(tbRotateV.Value - dy, tbRotateV.Minimum, tbRotateV.Maximum);
                picCanvas3D.Invalidate();
            }
        }

        private void picCanvas3D_MouseUp(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle) _isMiddleMouseDown = false;
            if (e.Button == MouseButtons.Left) _isLeftMouseDownForPanOrRotate = false;
            if (e.Button == MouseButtons.Right) _isRightMouseDown = false;
        }

        private Point3D CanvasToLocal(Point p)
        {
            var (_, widthM, heightM, _, _, _, _, _) = GeometryMath.CalculateCropBox(_targetIkou.P1, _targetIkou.P2, _targetIkou.P3);

            double dX = widthM;
            double dY = heightM;
            double cx = _pan2DX;
            double cy = _pan2DY;

            int w = picCanvas3D.Width;
            int h = picCanvas3D.Height;
            int margin = 80;
            double baseScale = Math.Min((w - margin * 2) / Math.Max(0.1, dX * 1.3), (h - margin * 2) / Math.Max(0.1, dY * 1.3));
            double scale = baseScale * _zoom2D;

            double lx = cx + (p.X - w / 2f) / scale;
            double ly = cy - (p.Y - h / 2f) / scale;
            return new Point3D(lx, ly, 0);
        }

        private Point3D LocalToSurvey(Point3D localPt)
        {
            var (_, _, _, ux, uy, vx, vy, center) = GeometryMath.CalculateCropBox(_targetIkou.P1, _targetIkou.P2, _targetIkou.P3);
            double sx = center.X + localPt.X * ux + localPt.Y * vx;
            double sy = center.Y + localPt.X * uy + localPt.Y * vy;
            return new Point3D(sx, sy, localPt.Z);
        }

        private void btnDanmenSet_Click(object? sender, EventArgs e)
        {
            if (_currentDanmen != null && _sectionStartPoint != null && _sectionEndPoint != null)
            {
                int newDid = _targetDanmenRec?.DID ?? (_targetIkou.DmList.Count > 0 ? _targetIkou.DmList.Max(d => d.DID) + 1 : 1);
                string dmName = !string.IsNullOrWhiteSpace(_targetDanmenRec?.Name) ? _targetDanmenRec.Name : ((char)('A' + (newDid - 1))).ToString();

                // Convert Local Points (Sp, Ep, Dp) back to Survey space for global storage in DanmenRec
                Point3D surSp = LocalToSurvey(_sectionStartPoint);
                Point3D surEp = LocalToSurvey(_sectionEndPoint);
                Point3D surDp = _sectionPlacementPoint != null ? LocalToSurvey(_sectionPlacementPoint) : new Point3D(surSp.X, surSp.Y - 1.0, 0);

                var newDm = new DanmenRec(newDid, dmName,
                    new XYZ(surSp.X, surSp.Y),
                    new XYZ(surEp.X, surEp.Y),
                    new XYZ(surDp.X, surDp.Y));

                newDm.DmpList.Clear();
                foreach (var p in _currentDanmen.danmen)
                {
                    newDm.DmpList.Add(new DanmenPRec(p.Distance, p.Elevation));
                }

                ResultDanmenRec = newDm;
            }
        }
    }
}
