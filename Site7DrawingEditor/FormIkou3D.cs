using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Site7DrawingEditor.Services;

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
            public static GridMesh CreateGridMesh(Point3D minp, Point3D maxp, List<Point3D> sourcePoints, int resolutionX = 50, int resolutionY = 50, double power = 5.0, int maxNeighbors = 4)
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
                        double z = CalculateIdw(new Point3D(x, y, 0), sourcePoints, power, smoothing, maxNeighbors);
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

            public static double CalculateIdw(Point3D target, List<Point3D> points, double power = 5.0, double smoothing = 0.0, int maxNeighbors = 4)
            {
                if (points.Count == 0) return 0;

                // k-NN: 距離順に近傍 maxNeighbors 点のみを対象にして上部点の影響を遮断 (Pit底の盛り上がり防止)
                IEnumerable<Point3D> targetPoints = points;
                if (points.Count > maxNeighbors)
                {
                    targetPoints = points
                        .Select(p => new { Pt = p, DistSq = (target.X - p.X) * (target.X - p.X) + (target.Y - p.Y) * (target.Y - p.Y) })
                        .OrderBy(p => p.DistSq)
                        .Take(maxNeighbors)
                        .Select(p => p.Pt);
                }

                double sumWeights = 0;
                double sumWeightedValues = 0;

                foreach (var p in targetPoints)
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

                return sumWeights > 0 ? sumWeightedValues / sumWeights : 0;
            }

            public static Danmen CalcDanmen(Point3D start, Point3D end, Point3D dp, List<Point3D> sourcePoints, int cnt = 100, double power = 5.0, int maxNeighbors = 4)
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
                    double z = CalculateIdw(new Point3D(x, y, 0), sourcePoints, power, smoothing, maxNeighbors);
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
        private readonly DrawingDbManager? _db;
        private readonly bool _chkColorByIkou;
        private readonly Func<int, bool>? _isLayerVisible;
        private readonly List<Point3D> _allLocalPoints = new List<Point3D>();
        private readonly List<Point3D> _virtualBottomPoints = new List<Point3D>();
        private readonly List<Point3D> _virtualBoundaryPoints = new List<Point3D>();
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

        private int SplineDivisions => int.TryParse(cmbSplineDiv.SelectedItem?.ToString(), out int div) ? div : 5;
        private double WeightPower => double.TryParse(txtWeightPower.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double w) ? Math.Max(0.1, w) : 5.0;
        private int NeighborCount => int.TryParse(txtNeighborCount.Text, out int cnt) ? Math.Max(1, cnt) : 4;

        public FormIkou3D(DrawingIkouModel ikou, DanmenRec? targetDanmen = null, DrawingDbManager? db = null, bool chkColorByIkou = true, Func<int, bool>? isLayerVisible = null)
        {
            InitializeComponent();
            _targetIkou = ikou;
            _targetDanmenRec = targetDanmen;
            _db = db;
            _chkColorByIkou = chkColorByIkou;
            _isLayerVisible = isLayerVisible;

            cmbSplineDiv.SelectedItem = "5";
            cmbGridResolution.SelectedIndex = 1; // 中 (50分割)
            txtWeightPower.Text = "5.0";
            txtNeighborCount.Text = "4";

            RebuildLocalPoints();

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

            WireEventHandlers();
            PopulateSummaryInfo();
            CalculateGridMesh();
        }

        private void RebuildLocalPoints()
        {
            _allLocalPoints.Clear();
            _virtualBottomPoints.Clear();
            _virtualBoundaryPoints.Clear();
            var spline = new Xross_Spline();
            int splineDiv = SplineDivisions;

            foreach (var line in _targetIkou.LList)
            {
                if (line.Pnts.Count == 0 || (_isLayerVisible != null && !_isLayerVisible(line.Layer))) continue;
                var localPnts = line.Pnts.Select(pt =>
                {
                    var (lx, ly) = GeometryMath.SurveyToFeatureLocalCenter(pt.X, pt.Y, _targetIkou.P1, _targetIkou.P2, _targetIkou.P3);
                    return new Point3D(lx, ly, pt.Z);
                }).ToList();

                bool isLayerCurve = (_db != null) ? LayerManager.IsLayerCurve(_db, line.Layer) : true;
                bool isClosed = (line.Flag == 1);
                if (!isClosed && localPnts.Count >= 3)
                {
                    var pFirst = localPnts[0];
                    var pLast = localPnts[^1];
                    double dist = Math.Sqrt(Math.Pow(pFirst.X - pLast.X, 2) + Math.Pow(pFirst.Y - pLast.Y, 2));
                    if (dist < 0.25) isClosed = true;
                }

                List<Point3D> effectiveLocalPnts = (isLayerCurve && localPnts.Count >= 3)
                    ? (isClosed ? spline.Calc3DCloseCurvePoints(localPnts, splineDiv) : spline.Calc3DCurvePoints(localPnts, splineDiv))
                    : localPnts;

                foreach (var pt in effectiveLocalPnts)
                {
                    _allLocalPoints.Add(pt);
                }

                // Pit底（レイヤ名に「下」または「底」が含まれる閉曲線）の場合、内部の重心に最深点（仮想中心点）と中点群を自動生成
                string layerName = "";
                if (_db?.MasterLayerList != null && _db.MasterLayerList.Count > 0)
                {
                    int normIdx = line.Layer;
                    if (normIdx >= 49 && normIdx <= 64) normIdx -= 48;
                    if (normIdx > 16) normIdx = ((normIdx - 1) % 16) + 1;

                    var ly = _db.MasterLayerList.FirstOrDefault(l =>
                        l.Id == line.Layer ||
                        l.Id == normIdx ||
                        l.Id == normIdx + 48 ||
                        l.Id == (line.Layer % 100));
                    if (ly != null && !string.IsNullOrEmpty(ly.Name)) layerName = ly.Name;
                }
                if (string.IsNullOrEmpty(layerName))
                {
                    var layerDef = LayerDefinitionService.Instance.GetLayer(LayerGroup.Ikou, line.Layer);
                    if (layerDef != null && !string.IsNullOrEmpty(layerDef.Name)) layerName = layerDef.Name;
                }

                bool isBottomLayer = layerName.Contains("下") || layerName.Contains("底");
                if (isClosed && isBottomLayer && localPnts.Count >= 3)
                {
                    double avgX = localPnts.Average(p => p.X);
                    double avgY = localPnts.Average(p => p.Y);
                    double avgZ = localPnts.Average(p => p.Z);
                    var centerPt = new Point3D(avgX, avgY, avgZ);
                    _allLocalPoints.Add(centerPt);
                    _virtualBottomPoints.Add(centerPt);

                    // 仮想中心点と底面曲線の各構成点との中点群を自動生成して底面全体を補強
                    foreach (var pt in effectiveLocalPnts)
                    {
                        double midX = (pt.X + avgX) / 2.0;
                        double midY = (pt.Y + avgY) / 2.0;
                        double midZ = (pt.Z + avgZ) / 2.0;
                        var midPt = new Point3D(midX, midY, midZ);
                        _allLocalPoints.Add(midPt);
                        _virtualBottomPoints.Add(midPt);
                    }
                }
            }

            // Pit上部（外側曲線）とGrid線の上下左右の交点標高をGrid枠端に設定
            GenerateOuterBoundaryGridPoints();
        }

        private void GenerateOuterBoundaryGridPoints()
        {
            var (_, widthM, heightM, _, _, _, _, _) = GeometryMath.CalculateCropBox(_targetIkou.P1, _targetIkou.P2, _targetIkou.P3);
            double minX = -widthM / 2.0;
            double maxX = +widthM / 2.0;
            double minY = -heightM / 2.0;
            double maxY = +heightM / 2.0;

            // Pit上部（レイヤ名に「上」が含まれる曲線、または外側曲線）のセグメントを収集
            var topSegments = new List<(Point3D P1, Point3D P2)>();
            var allSegments = new List<(Point3D P1, Point3D P2)>();

            var spline = new Xross_Spline();
            int splineDiv = SplineDivisions;

            foreach (var line in _targetIkou.LList)
            {
                if (line.Pnts.Count < 2 || (_isLayerVisible != null && !_isLayerVisible(line.Layer))) continue;
                var localPnts = line.Pnts.Select(pt =>
                {
                    var (lx, ly) = GeometryMath.SurveyToFeatureLocalCenter(pt.X, pt.Y, _targetIkou.P1, _targetIkou.P2, _targetIkou.P3);
                    return new Point3D(lx, ly, pt.Z);
                }).ToList();

                bool isLayerCurve = (_db != null) ? LayerManager.IsLayerCurve(_db, line.Layer) : true;
                bool isClosed = (line.Flag == 1);
                if (!isClosed && localPnts.Count >= 3)
                {
                    var pFirst = localPnts[0];
                    var pLast = localPnts[^1];
                    double dist = Math.Sqrt(Math.Pow(pFirst.X - pLast.X, 2) + Math.Pow(pFirst.Y - pLast.Y, 2));
                    if (dist < 0.25) isClosed = true;
                }

                List<Point3D> pnts = (isLayerCurve && localPnts.Count >= 3)
                    ? (isClosed ? spline.Calc3DCloseCurvePoints(localPnts, splineDiv) : spline.Calc3DCurvePoints(localPnts, splineDiv))
                    : localPnts;

                string layerName = "";
                if (_db?.MasterLayerList != null && _db.MasterLayerList.Count > 0)
                {
                    int normIdx = line.Layer;
                    if (normIdx >= 49 && normIdx <= 64) normIdx -= 48;
                    if (normIdx > 16) normIdx = ((normIdx - 1) % 16) + 1;
                    var ly = _db.MasterLayerList.FirstOrDefault(l =>
                        l.Id == line.Layer || l.Id == normIdx || l.Id == normIdx + 48 || l.Id == (line.Layer % 100));
                    if (ly != null && !string.IsNullOrEmpty(ly.Name)) layerName = ly.Name;
                }
                if (string.IsNullOrEmpty(layerName))
                {
                    var layerDef = LayerDefinitionService.Instance.GetLayer(LayerGroup.Ikou, line.Layer);
                    if (layerDef != null && !string.IsNullOrEmpty(layerDef.Name)) layerName = layerDef.Name;
                }

                bool isTopLayer = layerName.Contains("上") || (!layerName.Contains("下") && !layerName.Contains("底"));

                for (int i = 0; i < pnts.Count - 1; i++)
                {
                    var seg = (pnts[i], pnts[i + 1]);
                    allSegments.Add(seg);
                    if (isTopLayer) topSegments.Add(seg);
                }
                if (isClosed && pnts.Count >= 3)
                {
                    var seg = (pnts[^1], pnts[0]);
                    allSegments.Add(seg);
                    if (isTopLayer) topSegments.Add(seg);
                }
            }

            var targetSegments = topSegments.Count > 0 ? topSegments : allSegments;
            if (targetSegments.Count == 0) return;

            var targetPoints = targetSegments.SelectMany(s => new[] { s.P1, s.P2 }).Distinct().ToList();

            int resX = cmbGridResolution.SelectedIndex switch
            {
                0 => 25,
                2 => 100,
                _ => 50
            };
            int resY = resX;

            double maxDistY = (maxY - minY) / 3.0;
            double maxDistX = (maxX - minX) / 3.0;

            // 1. 縦線 X = xi との交点を求め、上下端 (xi, minY) / (xi, maxY) および中央1/2の中間点に交点標高を設定
            var verticalCrossData = new List<(double Xi, Point3D TopCross, Point3D BotCross, bool IsTopValid, bool IsBotValid)>();
            for (int i = 0; i < resX; i++)
            {
                double xi = minX + i * (maxX - minX) / Math.Max(1, resX - 1);
                var intersections = new List<Point3D>();
                foreach (var seg in targetSegments)
                {
                    if (TryIntersectVertical(seg.P1, seg.P2, xi, out Point3D cross))
                    {
                        intersections.Add(cross);
                    }
                }

                if (intersections.Count > 0)
                {
                    var topCross = intersections.OrderByDescending(p => p.Y).First();
                    var botCross = intersections.OrderBy(p => p.Y).First();

                    bool isTopValid = (maxY - topCross.Y) <= maxDistY + 1e-5;
                    bool isBotValid = (botCross.Y - minY) <= maxDistY + 1e-5;
                    verticalCrossData.Add((xi, topCross, botCross, isTopValid, isBotValid));

                    // 外周端点 (端点から交点までの距離が範囲の1/3以下の時のみ有効)
                    if (isTopValid)
                    {
                        var ptTop = new Point3D(xi, maxY, topCross.Z);
                        _allLocalPoints.Add(ptTop);
                        _virtualBoundaryPoints.Add(ptTop);
                    }
                    if (isBotValid)
                    {
                        var ptBot = new Point3D(xi, minY, botCross.Z);
                        _allLocalPoints.Add(ptBot);
                        _virtualBoundaryPoints.Add(ptBot);
                    }
                }
            }

            // 中間点は交差スパンの中央1/2（両端1/4を除外）かつ距離1/3以下の時のみ生成
            if (verticalCrossData.Count > 0)
            {
                int count = verticalCrossData.Count;
                int startIdx = (int)Math.Round(count * 0.25);
                int endIdx = (int)Math.Round(count * 0.75);
                for (int k = startIdx; k < endIdx && k < count; k++)
                {
                    var (xi, topCross, botCross, isTopValid, isBotValid) = verticalCrossData[k];
                    if (isTopValid)
                    {
                        var ptTopMid = new Point3D(xi, (topCross.Y + maxY) / 2.0, topCross.Z);
                        _allLocalPoints.Add(ptTopMid);
                        _virtualBoundaryPoints.Add(ptTopMid);
                    }
                    if (isBotValid)
                    {
                        var ptBotMid = new Point3D(xi, (botCross.Y + minY) / 2.0, botCross.Z);
                        _allLocalPoints.Add(ptBotMid);
                        _virtualBoundaryPoints.Add(ptBotMid);
                    }
                }
            }

            // 2. 横線 Y = yj との交点を求め、左右端 (minX, yj) / (maxX, yj) および中央1/2の中間点に交点標高を設定
            var horizontalCrossData = new List<(double Yj, Point3D RightCross, Point3D LeftCross, bool IsRightValid, bool IsLeftValid)>();
            for (int j = 0; j < resY; j++)
            {
                double yj = minY + j * (maxY - minY) / Math.Max(1, resY - 1);
                var intersections = new List<Point3D>();
                foreach (var seg in targetSegments)
                {
                    if (TryIntersectHorizontal(seg.P1, seg.P2, yj, out Point3D cross))
                    {
                        intersections.Add(cross);
                    }
                }

                if (intersections.Count > 0)
                {
                    var rightCross = intersections.OrderByDescending(p => p.X).First();
                    var leftCross = intersections.OrderBy(p => p.X).First();

                    bool isRightValid = (maxX - rightCross.X) <= maxDistX + 1e-5;
                    bool isLeftValid = (leftCross.X - minX) <= maxDistX + 1e-5;
                    horizontalCrossData.Add((yj, rightCross, leftCross, isRightValid, isLeftValid));

                    // 外周端点 (端点から交点までの距離が範囲の1/3以下の時のみ有効)
                    if (isRightValid)
                    {
                        var ptRight = new Point3D(maxX, yj, rightCross.Z);
                        _allLocalPoints.Add(ptRight);
                        _virtualBoundaryPoints.Add(ptRight);
                    }
                    if (isLeftValid)
                    {
                        var ptLeft = new Point3D(minX, yj, leftCross.Z);
                        _allLocalPoints.Add(ptLeft);
                        _virtualBoundaryPoints.Add(ptLeft);
                    }
                }
            }

            // 中間点は交差スパンの中央1/2（両端1/4を除外）かつ距離1/3以下の時のみ生成
            if (horizontalCrossData.Count > 0)
            {
                int count = horizontalCrossData.Count;
                int startIdx = (int)Math.Round(count * 0.25);
                int endIdx = (int)Math.Round(count * 0.75);
                for (int k = startIdx; k < endIdx && k < count; k++)
                {
                    var (yj, rightCross, leftCross, isRightValid, isLeftValid) = horizontalCrossData[k];
                    if (isRightValid)
                    {
                        var ptRightMid = new Point3D((rightCross.X + maxX) / 2.0, yj, rightCross.Z);
                        _allLocalPoints.Add(ptRightMid);
                        _virtualBoundaryPoints.Add(ptRightMid);
                    }
                    if (isLeftValid)
                    {
                        var ptLeftMid = new Point3D((leftCross.X + minX) / 2.0, yj, leftCross.Z);
                        _allLocalPoints.Add(ptLeftMid);
                        _virtualBoundaryPoints.Add(ptLeftMid);
                    }
                }
            }
        }

        private static bool TryIntersectVertical(Point3D p1, Point3D p2, double targetX, out Point3D intersection)
        {
            intersection = default;
            double minX = Math.Min(p1.X, p2.X);
            double maxX = Math.Max(p1.X, p2.X);
            if (targetX < minX - 1e-9 || targetX > maxX + 1e-9) return false;
            double dx = p2.X - p1.X;
            if (Math.Abs(dx) < 1e-9)
            {
                if (Math.Abs(p1.X - targetX) < 1e-5)
                {
                    intersection = new Point3D(targetX, (p1.Y + p2.Y) / 2.0, (p1.Z + p2.Z) / 2.0);
                    return true;
                }
                return false;
            }
            double t = (targetX - p1.X) / dx;
            if (t < -1e-5 || t > 1.0 + 1e-5) return false;
            t = Math.Clamp(t, 0.0, 1.0);
            double y = p1.Y + t * (p2.Y - p1.Y);
            double z = p1.Z + t * (p2.Z - p1.Z);
            intersection = new Point3D(targetX, y, z);
            return true;
        }

        private static bool TryIntersectHorizontal(Point3D p1, Point3D p2, double targetY, out Point3D intersection)
        {
            intersection = default;
            double minY = Math.Min(p1.Y, p2.Y);
            double maxY = Math.Max(p1.Y, p2.Y);
            if (targetY < minY - 1e-9 || targetY > maxY + 1e-9) return false;
            double dy = p2.Y - p1.Y;
            if (Math.Abs(dy) < 1e-9)
            {
                if (Math.Abs(p1.Y - targetY) < 1e-5)
                {
                    intersection = new Point3D((p1.X + p2.X) / 2.0, targetY, (p1.Z + p2.Z) / 2.0);
                    return true;
                }
                return false;
            }
            double t = (targetY - p1.Y) / dy;
            if (t < -1e-5 || t > 1.0 + 1e-5) return false;
            t = Math.Clamp(t, 0.0, 1.0);
            double x = p1.X + t * (p2.X - p1.X);
            double z = p1.Z + t * (p2.Z - p1.Z);
            intersection = new Point3D(x, targetY, z);
            return true;
        }

        private void WireEventHandlers()
        {
            cmbGridResolution.SelectedIndexChanged += (s, e) => CalculateGridMesh();
            cmbSplineDiv.SelectedIndexChanged += (s, e) =>
            {
                RebuildLocalPoints();
                PopulateSummaryInfo();
                CalculateGridMesh();
            };
            txtWeightPower.TextChanged += (s, e) => CalculateGridMesh();
            txtNeighborCount.TextChanged += (s, e) => CalculateGridMesh();
            btnGridCalc.Click += (s, e) =>
            {
                RebuildLocalPoints();
                PopulateSummaryInfo();
                CalculateGridMesh();
            };
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

            btnDanmenSet.Click += btnDanmenSet_Click;

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

            _currentMesh = GridAlgorithm.CreateGridMesh(minp, maxp, _allLocalPoints, res, res, WeightPower, NeighborCount);

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
            _currentDanmen = GridAlgorithm.CalcDanmen(_sectionStartPoint, _sectionEndPoint, dp, _allLocalPoints, 100, WeightPower, NeighborCount);
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
                    if (_isLayerVisible != null && !_isLayerVisible(line.Layer)) continue;
                    var localPnts = line.Pnts.Select(p =>
                    {
                        var (lx, ly) = GeometryMath.SurveyToFeatureLocalCenter(p.X, p.Y, _targetIkou.P1, _targetIkou.P2, _targetIkou.P3);
                        return new Point3D(lx, ly, p.Z);
                    }).ToList();

                    bool isLayerCurve = (_db != null) ? LayerManager.IsLayerCurve(_db, line.Layer) : true;
                    bool shouldDrawCurve = isLayerCurve && localPnts.Count >= 3;

                    List<Point3D> renderPnts = shouldDrawCurve
                        ? (line.Flag == 1 ? spline.Calc3DCloseCurvePoints(localPnts, SplineDivisions) : spline.Calc3DCurvePoints(localPnts, SplineDivisions))
                        : localPnts;

                    var pts = renderPnts.Select(p => LocalTo2D(p.X, p.Y)).ToArray();
                    if (pts.Length > 1)
                    {
                        Color col;
                        float penW = 1.8f;

                        if (_chkColorByIkou)
                        {
                            string ikouName = ResolveParentIkouName(line, _targetIkou.Name);
                            var layerDef = LayerDefinitionService.Instance.GetLayer(LayerGroup.Ikou, line.Layer);
                            int toneLevel = layerDef != null ? layerDef.Mark : 1;
                            float baseWidth = (layerDef != null && layerDef.Width > 0) ? (float)layerDef.Width : 1.8f;
                            (col, penW) = IkouNameColorService.Instance.GetIkouRenderStyle(ikouName, toneLevel, baseWidth);
                        }
                        else
                        {
                            col = LayerManager.GetLayerColor(line.Layer);
                        }

                        using (var pen = new Pen(col, penW))
                        {
                            g.DrawLines(pen, pts);
                        }
                    }
                }

                // 仮想中心点・中点群 (Virtual Bottom Points) の表示 (マゼンタ色 直径5px)
                if (_virtualBottomPoints.Count > 0)
                {
                    using (var vBrush = new SolidBrush(Color.Magenta))
                    using (var vPen = new Pen(Color.Purple, 1.0f))
                    {
                        foreach (var vp in _virtualBottomPoints)
                        {
                            PointF p = LocalTo2D(vp.X, vp.Y);
                            g.FillEllipse(vBrush, p.X - 2.5f, p.Y - 2.5f, 5f, 5f);
                            g.DrawEllipse(vPen, p.X - 2.5f, p.Y - 2.5f, 5f, 5f);
                        }
                    }
                }

                // Grid枠端の外周設定点 (Virtual Boundary Points) の表示 (シアン色 直径5px)
                if (_virtualBoundaryPoints.Count > 0)
                {
                    using (var bBrush = new SolidBrush(Color.FromArgb(0, 220, 255)))
                    using (var bPen = new Pen(Color.FromArgb(0, 100, 200), 1.0f))
                    {
                        foreach (var bp in _virtualBoundaryPoints)
                        {
                            PointF p = LocalTo2D(bp.X, bp.Y);
                            g.FillEllipse(bBrush, p.X - 2.5f, p.Y - 2.5f, 5f, 5f);
                            g.DrawEllipse(bPen, p.X - 2.5f, p.Y - 2.5f, 5f, 5f);
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
            using (var ctrlOutlinePen = new Pen(Color.FromArgb(200, 30, 30), 1.0f))
            {
                foreach (var line in _targetIkou.LList)
                {
                    if (line.Pnts.Count == 0) continue;
                    if (_isLayerVisible != null && !_isLayerVisible(line.Layer)) continue;
                    var localPnts = line.Pnts.Select(p =>
                    {
                        var (lx, ly) = GeometryMath.SurveyToFeatureLocalCenter(p.X, p.Y, _targetIkou.P1, _targetIkou.P2, _targetIkou.P3);
                        return new Point3D(lx, ly, p.Z);
                    }).ToList();

                    if (localPnts.Count == 0) continue;

                    bool isLayerCurve = (_db != null) ? LayerManager.IsLayerCurve(_db, line.Layer) : true;
                    bool isClosed = (line.Flag == 1);
                    if (!isClosed && localPnts.Count >= 3)
                    {
                        var pFirst = localPnts[0];
                        var pLast = localPnts[^1];
                        double dist = Math.Sqrt(Math.Pow(pFirst.X - pLast.X, 2) + Math.Pow(pFirst.Y - pLast.Y, 2));
                        if (dist < 0.05) isClosed = true;
                    }

                    bool shouldDrawCurve = isLayerCurve && localPnts.Count >= 3;
                    List<Point3D> renderPnts = shouldDrawCurve
                        ? (isClosed ? spline.Calc3DCloseCurvePoints(localPnts, SplineDivisions) : spline.Calc3DCurvePoints(localPnts, SplineDivisions))
                        : localPnts;

                    var pts = renderPnts.Select(p => Project3D(p)).ToArray();
                    if (pts.Length > 1)
                    {
                        Color col;
                        float penW = 1.8f;

                        if (_chkColorByIkou)
                        {
                            string ikouName = ResolveParentIkouName(line, _targetIkou.Name);
                            var layerDef = LayerDefinitionService.Instance.GetLayer(LayerGroup.Ikou, line.Layer);
                            int toneLevel = layerDef != null ? layerDef.Mark : 1;
                            float baseWidth = (layerDef != null && layerDef.Width > 0) ? (float)layerDef.Width : 1.8f;
                            (col, penW) = IkouNameColorService.Instance.GetIkouRenderStyle(ikouName, toneLevel, baseWidth);
                        }
                        else
                        {
                            col = LayerManager.GetLayerColor(line.Layer);
                        }

                        using (var pen = new Pen(col, penW))
                        {
                            g.DrawLines(pen, pts);
                        }
                    }

                    // 1. 補間されたポイント (曲線レイヤの場合のみ小さく表示)
                    if (shouldDrawCurve)
                    {
                        foreach (var p in pts)
                        {
                            g.FillEllipse(interpBrush, p.X - 1.25f, p.Y - 1.25f, 2.5f, 2.5f);
                        }
                    }

                    // 2. 元の制御点・測量点 (Original Control Points) の表示 (直径5px)
                    foreach (var cp in localPnts)
                    {
                        PointF p = Project3D(cp);
                        g.FillEllipse(ctrlBrush, p.X - 2.5f, p.Y - 2.5f, 5f, 5f);
                        g.DrawEllipse(ctrlOutlinePen, p.X - 2.5f, p.Y - 2.5f, 5f, 5f);
                    }
                }

                // 3. 仮想中心点・中点群 (Virtual Bottom Points) の表示 (マゼンタ色 直径5px)
                if (_virtualBottomPoints.Count > 0)
                {
                    using (var vBrush = new SolidBrush(Color.Magenta))
                    using (var vPen = new Pen(Color.Purple, 1.0f))
                    {
                        foreach (var vp in _virtualBottomPoints)
                        {
                            PointF p = Project3D(vp);
                            g.FillEllipse(vBrush, p.X - 2.5f, p.Y - 2.5f, 5f, 5f);
                            g.DrawEllipse(vPen, p.X - 2.5f, p.Y - 2.5f, 5f, 5f);
                        }
                    }
                }

                // 4. Grid枠端の外周設定点 (Virtual Boundary Points) の表示 (シアン色 直径5px)
                if (_virtualBoundaryPoints.Count > 0)
                {
                    using (var bBrush = new SolidBrush(Color.FromArgb(0, 220, 255)))
                    using (var bPen = new Pen(Color.FromArgb(0, 100, 200), 1.0f))
                    {
                        foreach (var bp in _virtualBoundaryPoints)
                        {
                            PointF p = Project3D(bp);
                            g.FillEllipse(bBrush, p.X - 2.5f, p.Y - 2.5f, 5f, 5f);
                            g.DrawEllipse(bPen, p.X - 2.5f, p.Y - 2.5f, 5f, 5f);
                        }
                    }
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
            if (_sectionStartPoint == null && _tempSp != null) _sectionStartPoint = _tempSp;
            if (_sectionEndPoint == null && _tempEp != null) _sectionEndPoint = _tempEp;
            if (_sectionPlacementPoint == null && _tempDp != null) _sectionPlacementPoint = _tempDp;

            if (_sectionStartPoint != null && _sectionEndPoint != null)
            {
                if (_sectionPlacementPoint == null)
                {
                    _sectionPlacementPoint = new Point3D(_sectionStartPoint.X, _sectionStartPoint.Y - 1.0, 0);
                }

                if (_currentDanmen == null || _currentDanmen.danmen.Count == 0)
                {
                    UpdateSectionProfile();
                }

                if (_currentDanmen != null)
                {
                    int newDid = _targetDanmenRec?.DID ?? (_targetIkou.DmList.Count > 0 ? _targetIkou.DmList.Max(d => d.DID) + 1 : 1);
                    string dmName = !string.IsNullOrWhiteSpace(_targetDanmenRec?.Name) ? _targetDanmenRec.Name : ((char)('A' + (newDid - 1))).ToString();

                    // Convert Local Points (Sp, Ep, Dp) back to Survey space for global storage in DanmenRec
                    Point3D surSp = LocalToSurvey(_sectionStartPoint);
                    Point3D surEp = LocalToSurvey(_sectionEndPoint);
                    Point3D surDp = LocalToSurvey(_sectionPlacementPoint);

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

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private string ResolveParentIkouName(ZIkouLRec line, string fallbackName)
        {
            if (_db == null || line == null) return fallbackName;

            if (line.Id > 0 && _db.MasterIkouList != null)
            {
                var ik = _db.MasterIkouList.FirstOrDefault(m => m.Id == line.Id);
                if (ik != null && !string.IsNullOrEmpty(ik.Name))
                {
                    return ik.Name;
                }
            }

            if (line.Pnts != null && line.Pnts.Count > 0 && _db.MasterIkouLList != null && _db.MasterIkouList != null)
            {
                var p0 = line.Pnts[0];
                foreach (var ml in _db.MasterIkouLList)
                {
                    if (ml.Layer != line.Layer) continue;
                    var pts = SqliteDrawingManager.ParsePrecsText(ml.Precs);
                    if (pts.Count > 0 && Math.Abs(pts[0].X - p0.X) < 0.005 && Math.Abs(pts[0].Y - p0.Y) < 0.005)
                    {
                        var parentIkou = _db.MasterIkouList.FirstOrDefault(ik => ik.Id == ml.Id);
                        if (parentIkou != null && !string.IsNullOrEmpty(parentIkou.Name))
                        {
                            line.Id = ml.Id; // キャッシュ
                            return parentIkou.Name;
                        }
                    }
                }
            }

            return fallbackName;
        }
    }
}
