using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace Site7DbEditor.Services
{
    public static class EditorMapRenderer
    {
        public static void DrawMapCanvas(
            Graphics g,
            Size canvasSize,
            EditorMapViewController vc,
            EditorDbManager db,
            long selectedIkouId,
            long selectedLid,
            int selectedPointIndex,
            long selectedIbutuId,
            long selectedKikaiId,
            int activeTabIndex,
            bool chkShowIkou,
            bool chkShowIbutu,
            bool chkShowKikai,
            bool chkShowCurve,
            bool chkShowGrid,
            bool chkColorByIkou,
            Func<int, bool>? isLayerVisible = null,
            bool showIkouName = false,
            bool showIbutuName = false,
            bool showKikaiName = true,
            bool isDarkBackground = true,
            bool chkShowBgImage = true,
            bool chkShowBgPointCloud = true,
            bool chkShowHyoukou = false,
            bool chkShowScale = true)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(isDarkBackground ? Color.FromArgb(16, 16, 20) : Color.FromArgb(248, 249, 250));

            int width = canvasSize.Width;
            int height = canvasSize.Height;
            if (width <= 0 || height <= 0) return;

            // 1. Update/Cache bounds ONCE per frame (O(N))
            vc.UpdateMapBounds(canvasSize, db.IkouLList, db.IbutuList, db.KikaiList, chkShowIkou || chkShowHyoukou, chkShowIbutu, chkShowKikai);

            PointF ToCanvasPoint(double surveyX, double surveyY)
            {
                return vc.ToCanvasPoint(surveyX, surveyY, canvasSize);
            }

            // -1. Draw Background Image (背景画像)
            BackgroundImageService.Instance.DrawBackground(g, canvasSize, vc, chkShowBgImage);

            // -0.5. Draw Point Cloud 2D (背景点群)
            if (chkShowBgPointCloud && PointCloudService.Instance.HasPoints)
            {
                var pc = PointCloudService.Instance;
                int totalPts = pc.Points.Count;
                int step = Math.Max(1, totalPts / 60000); // 2D表示用に間引き
                double minZ = pc.MinZ;
                double rangeZ = Math.Max(0.1, pc.MaxZ - pc.MinZ);

                for (int i = 0; i < totalPts; i += step)
                {
                    var pt = pc.Points[i];
                    PointF p = ToCanvasPoint(pt.X, pt.Y);
                    if (p.X < -5 || p.X > width + 5 || p.Y < -5 || p.Y > height + 5) continue;

                    Color dotColor = pt.HasColor ? Color.FromArgb(255, pt.R, pt.G, pt.B) : Color.FromArgb(100, 200, 255);
                    using (var b = new SolidBrush(dotColor))
                    {
                        g.FillRectangle(b, p.X - 0.75f, p.Y - 0.75f, 1.5f, 1.5f);
                    }
                }
            }

            // 0. Calculate Grid Step (メッシュ間隔を自動計算)
            double gridStep = 10.0;
            var topLeftSurvey = vc.CanvasToSurvey(new PointF(0, 0), canvasSize);
            var bottomRightSurvey = vc.CanvasToSurvey(new PointF(width, height), canvasSize);

            double minSurveyX = Math.Min(topLeftSurvey.surveyX, bottomRightSurvey.surveyX);
            double maxSurveyX = Math.Max(topLeftSurvey.surveyX, bottomRightSurvey.surveyX);
            double minSurveyY = Math.Min(topLeftSurvey.surveyY, bottomRightSurvey.surveyY);
            double maxSurveyY = Math.Max(topLeftSurvey.surveyY, bottomRightSurvey.surveyY);

            double spanX = maxSurveyX - minSurveyX;
            double spanY = maxSurveyY - minSurveyY;
            double maxSpan = Math.Max(spanX, spanY);
            if (maxSpan <= 0.0001) maxSpan = 100.0;

            double rawStep = maxSpan / 7.0;
            double[] stepCandidates = new double[] {
                0.01, 0.02, 0.05, 0.1, 0.2, 0.3, 0.5,
                1.0, 2.0, 3.0, 5.0, 10.0, 20.0, 30.0, 50.0,
                100.0, 200.0, 300.0, 500.0, 1000.0, 2000.0, 3000.0, 5000.0, 10000.0
            };

            gridStep = stepCandidates[stepCandidates.Length - 1];
            for (int i = 0; i < stepCandidates.Length; i++)
            {
                if (stepCandidates[i] >= rawStep)
                {
                    gridStep = stepCandidates[i];
                    break;
                }
            }

            // 0. Draw Mesh/Grid (メッシュ & 座標値ラベル)
            if (chkShowGrid)
            {
                using (var gridPen = new Pen(isDarkBackground ? Color.FromArgb(40, 140, 140, 160) : Color.FromArgb(220, 222, 225), 1f) { DashStyle = DashStyle.Dot })
                using (var coordFont = new Font("Yu Gothic UI", 8.0F, FontStyle.Regular))
                using (var coordBrush = new SolidBrush(isDarkBackground ? Color.FromArgb(255, 110, 110) : Color.FromArgb(210, 0, 0)))
                {
                    double startGridY = Math.Floor(minSurveyY / gridStep) * gridStep;
                    double endGridY = Math.Ceiling(maxSurveyY / gridStep) * gridStep;
                    double startGridX = Math.Floor(minSurveyX / gridStep) * gridStep;
                    double endGridX = Math.Ceiling(maxSurveyX / gridStep) * gridStep;

                    // 縦メッシュ線（SurveyY = 一定）
                    for (double gy = startGridY; gy <= endGridY + (gridStep * 0.1); gy += gridStep)
                    {
                        PointF p = ToCanvasPoint((minSurveyX + maxSurveyX) / 2.0, gy);
                        if (p.X < -20 || p.X > width + 20) continue;

                        g.DrawLine(gridPen, p.X, 0, p.X, height);

                        string label = (gridStep < 1) ? gy.ToString("0.0") : gy.ToString("0");
                        var sz = g.MeasureString(label, coordFont);
                        g.DrawString(label, coordFont, coordBrush, p.X - sz.Width / 2f, 4f);
                    }

                    // 横メッシュ線（SurveyX = 一定）
                    for (double gx = startGridX; gx <= endGridX + (gridStep * 0.1); gx += gridStep)
                    {
                        PointF p = ToCanvasPoint(gx, (minSurveyY + maxSurveyY) / 2.0);
                        if (p.Y < -20 || p.Y > height + 20) continue;

                        g.DrawLine(gridPen, 0, p.Y, width, p.Y);

                        string label = (gridStep < 1) ? gx.ToString("0.0") : gx.ToString("0");
                        var sz = g.MeasureString(label, coordFont);
                        g.DrawString(label, coordFont, coordBrush, 4f, p.Y - sz.Height / 2f);
                    }
                }
            }

            // 1. Draw Features (遺構L)
            if (chkShowIkou || chkShowHyoukou)
            {
                var spline = new Xross_Spline();

                foreach (var line in db.IkouLList)
                {
                    int layerIdx = line.Layer >= 49 ? (line.Layer - 48) : line.Layer;
                    if (isLayerVisible != null && !isLayerVisible(layerIdx)) continue;

                    var pts = SqliteManager.ParsePrecsText(line.Precs);
                    if (pts.Count == 0) continue;

                    int lineDbLayerId = line.Layer >= 49 ? line.Layer : (line.Layer + 48);
                    Color color = chkColorByIkou
                        ? EditorLayerService.PaletteColors[(int)(line.Id % EditorLayerService.PaletteColors.Length)]
                        : EditorLayerService.GetLayerColor(lineDbLayerId, db.LayerList, isDarkBackground);

                    bool isSelectedFeature = (activeTabIndex == 0 && line.Id == selectedIkouId);

                    // ★ Mode == 2 (標高点): 線はつながず、遺構ONなら小さな点のみ、標高ONならZ値を小さく描画
                    if (line.Mode == 2)
                    {
                        if (chkShowIkou)
                        {
                            using (var ptBrush = new SolidBrush(isSelectedFeature ? Color.FromArgb(255, 230, 0) : color))
                            {
                                float r = isSelectedFeature ? 2.0f : 1.5f;
                                foreach (var p in pts)
                                {
                                    PointF sp = ToCanvasPoint(p.X, p.Y);
                                    g.FillEllipse(ptBrush, sp.X - r, sp.Y - r, r * 2f, r * 2f);
                                }
                            }
                        }

                        if (chkShowHyoukou)
                        {
                            using (var zFont = new Font("Yu Gothic UI", 6.5F, FontStyle.Regular))
                            using (var zBrush = new SolidBrush(isDarkBackground ? Color.FromArgb(140, 200, 140) : Color.FromArgb(30, 100, 30)))
                            {
                                foreach (var p in pts)
                                {
                                    PointF sp = ToCanvasPoint(p.X, p.Y);
                                    string zText = p.Z.ToString("0.000");
                                    g.DrawString(zText, zFont, zBrush, sp.X + 3f, sp.Y - 5f);
                                }
                            }
                        }

                        continue;
                    }

                    // ★ Mode != 2 (通常の折線・曲線): chkShowIkou が ON の時のみ描画
                    if (!chkShowIkou) continue;

                    int dbLayerId = line.Layer >= 49 ? line.Layer : (line.Layer + 48);
                    var layer = db.LayerList.FirstOrDefault(l => l.Id == dbLayerId);
                    bool isLayerCurve = (layer != null) ? (layer.LType == 2) : true;
                    bool drawAsCurve = chkShowCurve && isLayerCurve && pts.Count >= 3;

                    PointF[] screenPts;
                    if (drawAsCurve)
                    {
                        var curvePoints = (line.Mode == 1)
                            ? spline.Calc3DCloseCurvePoints(pts, 5)
                            : spline.Calc3DCurvePoints(pts, 5);
                        screenPts = curvePoints.Select(p => ToCanvasPoint(p.X, p.Y)).ToArray();
                    }
                    else
                    {
                        screenPts = pts.Select(p => ToCanvasPoint(p.X, p.Y)).ToArray();
                    }

                    if (screenPts.Length > 1)
                    {
                        bool isClosed = (line.Mode == 1 && screenPts.Length >= 3);
                        if (isSelectedFeature)
                        {
                            using (var linePen = new Pen(color, 2.8f))
                            {
                                g.DrawLines(linePen, screenPts);
                                if (isClosed)
                                    g.DrawLine(linePen, screenPts[screenPts.Length - 1], screenPts[0]);
                            }
                        }
                        else
                        {
                            float penWidth = (layer != null && layer.Width > 0) ? (float)layer.Width : 1.5f;
                            using (var linePen = new Pen(color, penWidth))
                            {
                                g.DrawLines(linePen, screenPts);
                                if (isClosed)
                                    g.DrawLine(linePen, screenPts[screenPts.Length - 1], screenPts[0]);
                            }
                        }
                    }
                }
            }

            // 2. Draw Artifacts (遺物)
            if (chkShowIbutu)
            {
                using (var glowPen = new Pen(Color.FromArgb(180, 255, 0, 128), 2f))
                {
                    foreach (var ibutu in db.IbutuList)
                    {
                        PointF pt = ToCanvasPoint(ibutu.X, ibutu.Y);
                        Color ibutuColor = EditorLayerService.GetLayerColor(ibutu.Layer, db.LayerList, isDarkBackground);

                        using (var ibutuBrush = new SolidBrush(ibutuColor))
                        {
                            if (activeTabIndex == 1 && ibutu.Id == selectedIbutuId)
                            {
                                g.DrawEllipse(glowPen, pt.X - 5.5f, pt.Y - 5.5f, 11f, 11f);
                                g.FillEllipse(ibutuBrush, pt.X - 3.5f, pt.Y - 3.5f, 7f, 7f);
                                g.DrawEllipse(Pens.White, pt.X - 3.5f, pt.Y - 3.5f, 7f, 7f);
                            }
                            else
                            {
                                g.FillEllipse(ibutuBrush, pt.X - 2.5f, pt.Y - 2.5f, 5f, 5f);
                                g.DrawEllipse(Pens.White, pt.X - 2.5f, pt.Y - 2.5f, 5f, 5f);
                            }
                        }
                    }
                }
            }

            // 3. Draw Stations / Control Points (基準点)
            if (chkShowKikai)
            {
                string currentKpName = gbl.KikaiMan.kp?.Name ?? Env.KPName ?? Def.GetIniStr("TS", "器械点");
                string currentBpName = gbl.KikaiMan.bp?.Name ?? Env.BPName ?? Def.GetIniStr("TS", "後視点");

                using (var kikaiBrush = new SolidBrush(isDarkBackground ? Color.FromArgb(0, 225, 255) : Color.FromArgb(0, 120, 200)))
                using (var kikaiPen = new Pen(isDarkBackground ? Color.White : Color.Black, 1.2f))
                using (var selectPen = new Pen(Color.FromArgb(255, 220, 0), 2f))
                using (var kpTextBrush = new SolidBrush(Color.FromArgb(255, 100, 100)))
                using (var bpTextBrush = new SolidBrush(Color.FromArgb(100, 200, 255)))
                using (var markFont = new Font("Yu Gothic UI", 9.0F, FontStyle.Bold))
                using (var sfFar = new StringFormat { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center })
                {
                    foreach (var kikai in db.KikaiList)
                    {
                        PointF pt = ToCanvasPoint(kikai.X, kikai.Y);
                        if (activeTabIndex == 2 && kikai.Id == selectedKikaiId)
                        {
                            g.FillEllipse(kikaiBrush, pt.X - 4.5f, pt.Y - 4.5f, 9f, 9f);
                            g.DrawEllipse(selectPen, pt.X - 4.5f, pt.Y - 4.5f, 9f, 9f);
                        }
                        else
                        {
                            g.FillEllipse(kikaiBrush, pt.X - 3f, pt.Y - 3f, 6f, 6f);
                            g.DrawEllipse(kikaiPen, pt.X - 3f, pt.Y - 3f, 6f, 6f);
                        }

                        // 器械点 / 後視点 の文字描画 (基準点シンボルの左側)
                        string kikaiName = string.IsNullOrEmpty(kikai.Name) ? $"K{kikai.Id}" : kikai.Name;
                        bool isKp = !string.IsNullOrEmpty(currentKpName) && kikaiName.Equals(currentKpName, StringComparison.OrdinalIgnoreCase);
                        bool isBp = !string.IsNullOrEmpty(currentBpName) && kikaiName.Equals(currentBpName, StringComparison.OrdinalIgnoreCase);

                        if (isKp)
                        {
                            g.DrawString("器", markFont, kpTextBrush, pt.X - 5f, pt.Y, sfFar);
                        }
                        else if (isBp)
                        {
                            g.DrawString("後", markFont, bpTextBrush, pt.X - 5f, pt.Y, sfFar);
                        }
                    }
                }
            }

            // 4. Draw Map Labels
            using (var ikouMasterFont = new Font("Yu Gothic UI", 9.5F, FontStyle.Bold))
            using (var lineLabelFont = new Font("Yu Gothic UI", 8.0F, FontStyle.Bold))
            using (var labelFont = new Font("Yu Gothic UI", 8.0F, FontStyle.Bold))
            using (var ikouMasterBrush = new SolidBrush(isDarkBackground ? Color.FromArgb(255, 230, 0) : Color.FromArgb(180, 80, 0)))
            using (var ikouLineLabelBrush = new SolidBrush(isDarkBackground ? Color.FromArgb(0, 225, 255) : Color.FromArgb(0, 120, 200)))
            using (var kikaiLabelBrush = new SolidBrush(isDarkBackground ? Color.White : Color.DarkBlue))
            {
                if (showIkouName || activeTabIndex == 0)
                {
                    if (chkShowIkou)
                    {
                        // 1. 遺構名（IkouModel.Name）の描画
                        foreach (var ikou in db.IkouList)
                        {
                            if (string.IsNullOrEmpty(ikou.Name)) continue;

                            PointF ikouLabelPt;
                            if (ikou.X != 0.0 || ikou.Y != 0.0)
                            {
                                ikouLabelPt = ToCanvasPoint(ikou.X, ikou.Y);
                            }
                            else
                            {
                                var childLines = db.IkouLList.Where(l => l.Id == ikou.Id).ToList();
                                var allPts = childLines.SelectMany(l => SqliteManager.ParsePrecsText(l.Precs)).ToList();
                                if (allPts.Count == 0) continue;
                                double avgX = allPts.Average(p => p.X);
                                double avgY = allPts.Average(p => p.Y);
                                ikouLabelPt = ToCanvasPoint(avgX, avgY);
                            }

                            SizeF ikouTextSize = g.MeasureString(ikou.Name, ikouMasterFont);
                            g.DrawString(ikou.Name, ikouMasterFont, ikouMasterBrush, ikouLabelPt.X - ikouTextSize.Width / 2f, ikouLabelPt.Y - ikouTextSize.Height / 2f);
                        }

                        // 2. 遺構線名（IkouLModel.Name）の描画
                        foreach (var line in db.IkouLList)
                        {
                            int layerIdx = line.Layer >= 49 ? (line.Layer - 48) : line.Layer;
                            if (isLayerVisible != null && !isLayerVisible(layerIdx)) continue;

                            var pts = SqliteManager.ParsePrecsText(line.Precs);
                            if (pts.Count == 0) continue;

                            string lineName = string.IsNullOrEmpty(line.Name) ? $"L{line.Lid}" : line.Name;

                            PointF labelPt = (line.X != 0.0 || line.Y != 0.0)
                                ? ToCanvasPoint(line.X, line.Y)
                                : ToCanvasPoint(pts[0].X, pts[0].Y);

                            SizeF textSize = g.MeasureString(lineName, lineLabelFont);
                            g.DrawString(lineName, lineLabelFont, ikouLineLabelBrush, labelPt.X - textSize.Width / 2f, labelPt.Y - textSize.Height / 2f);
                        }
                    }
                }

                if (showIbutuName || activeTabIndex == 1)
                {
                    if (chkShowIbutu)
                    {
                        foreach (var ibutu in db.IbutuList)
                        {
                            string nameText = "";
                            if (!string.IsNullOrEmpty(ibutu.Syubetu))
                            {
                                nameText = ibutu.No > 0 ? $"{ibutu.Syubetu}{ibutu.No}" : ibutu.Syubetu;
                            }
                            else if (ibutu.No > 0)
                            {
                                nameText = $"No.{ibutu.No}";
                            }
                            else
                            {
                                nameText = $"遺物{ibutu.Id}";
                            }

                            PointF pt = ToCanvasPoint(ibutu.X, ibutu.Y);
                            SizeF textSize = g.MeasureString(nameText, labelFont);
                            Color ibutuColor = EditorLayerService.GetLayerColor(ibutu.Layer, db.LayerList, isDarkBackground);

                            using (var ibutuLabelBrush = new SolidBrush(ibutuColor))
                            {
                                g.DrawString(nameText, labelFont, ibutuLabelBrush, pt.X + 6, pt.Y - textSize.Height / 2f);
                            }
                        }
                    }
                }

                if (showKikaiName || activeTabIndex == 2)
                {
                    if (chkShowKikai)
                    {
                        foreach (var kikai in db.KikaiList)
                        {
                            string nameText = string.IsNullOrEmpty(kikai.Name) ? $"K{kikai.Id}" : kikai.Name;
                            PointF pt = ToCanvasPoint(kikai.X, kikai.Y);
                            SizeF textSize = g.MeasureString(nameText, labelFont);
                            g.DrawString(nameText, labelFont, kikaiLabelBrush, pt.X + 6, pt.Y - textSize.Height / 2f);
                        }
                    }
                }
            }

            // 5. Draw Selected Line Vertices (頂点〇) and Midpoints (中間点□)
            if (activeTabIndex == 0 && selectedIkouId >= 0 && selectedLid >= 0)
            {
                var selectedLine = db.IkouLList.FirstOrDefault(l => l.Id == selectedIkouId && l.Lid == selectedLid);
                if (selectedLine != null)
                {
                    int selectedLineLayerIdx = selectedLine.Layer >= 49 ? (selectedLine.Layer - 48) : selectedLine.Layer;
                    if (isLayerVisible == null || isLayerVisible(selectedLineLayerIdx))
                    {
                        var pts = SqliteManager.ParsePrecsText(selectedLine.Precs);
                        int dbLayerId = selectedLine.Layer >= 49 ? selectedLine.Layer : (selectedLine.Layer + 48);
                        var layer = db.LayerList.FirstOrDefault(l => l.Id == dbLayerId);
                        bool isLayerCurve = (layer != null) ? (layer.LType == 2) : true;
                        bool drawAsCurve = chkShowCurve && isLayerCurve && pts.Count >= 3;

                        Color grayColor = isDarkBackground
                            ? Color.FromArgb(160, 160, 170, 185)
                            : Color.FromArgb(180, 100, 110, 125);
                        Color vertexPenColor = isDarkBackground
                            ? Color.FromArgb(255, 220, 0)
                            : Color.FromArgb(190, 85, 0);
                        Color vertexBrushColor = isDarkBackground
                            ? Color.FromArgb(230, 255, 255, 255)
                            : Color.FromArgb(255, 255, 255, 255);
                        Color pidTextColor = isDarkBackground
                            ? Color.FromArgb(255, 220, 0)
                            : Color.FromArgb(15, 23, 42);

                        // 頂点数が2点以上かつ標高点（Mode == 2）でない場合、中間点に□マークを表示（曲線表示時はベース折れ線を薄いグレー破線で表示）
                        if (selectedLine.Mode != 2 && pts.Count > 1)
                        {
                            var polylinePts = pts.Select(p => ToCanvasPoint(p.X, p.Y)).ToArray();
                            bool isClosed = (selectedLine.Mode == 1) && (pts.Count >= 3);

                            if (drawAsCurve)
                            {
                                using (var grayPen = new Pen(grayColor, 1.2f) { DashStyle = DashStyle.Dash })
                                {
                                    g.DrawLines(grayPen, polylinePts);
                                    if (isClosed)
                                    {
                                        g.DrawLine(grayPen, polylinePts[polylinePts.Length - 1], polylinePts[0]);
                                    }
                                }
                            }

                            // 中間点（□）の描画（曲線・折線問わず表示）
                            using (var midPen = new Pen(vertexPenColor, 1.5f))
                            using (var midBrush = new SolidBrush(vertexBrushColor))
                            {
                                for (int i = 0; i < polylinePts.Length - 1; i++)
                                {
                                    PointF p1 = polylinePts[i];
                                    PointF p2 = polylinePts[i + 1];
                                    float midX = (p1.X + p2.X) / 2f;
                                    float midY = (p1.Y + p2.Y) / 2f;

                                    g.FillRectangle(midBrush, midX - 3.5f, midY - 3.5f, 7f, 7f);
                                    g.DrawRectangle(midPen, midX - 3.5f, midY - 3.5f, 7f, 7f);
                                }

                                // 閉合の場合は終点〜始点間の中間点も描画
                                if (isClosed)
                                {
                                    PointF p1 = polylinePts[polylinePts.Length - 1];
                                    PointF p2 = polylinePts[0];
                                    float midX = (p1.X + p2.X) / 2f;
                                    float midY = (p1.Y + p2.Y) / 2f;

                                    g.FillRectangle(midBrush, midX - 3.5f, midY - 3.5f, 7f, 7f);
                                    g.DrawRectangle(midPen, midX - 3.5f, midY - 3.5f, 7f, 7f);
                                }
                            }
                        }

                        using (var vertexPen = new Pen(vertexPenColor, 1.5f))
                        using (var vertexBrush = new SolidBrush(vertexBrushColor))
                        using (var pidFont = new Font("Yu Gothic UI", 8.0F, FontStyle.Bold))
                        using (var pidBrush = new SolidBrush(pidTextColor))
                        {
                            for (int i = 0; i < pts.Count; i++)
                            {
                                var pt = pts[i];
                                PointF vp = ToCanvasPoint(pt.X, pt.Y);
                                string pidText = pt.Pid > 0 ? pt.Pid.ToString() : (i + 1).ToString();

                                g.FillEllipse(vertexBrush, vp.X - 3.5f, vp.Y - 3.5f, 7f, 7f);
                                g.DrawEllipse(vertexPen, vp.X - 3.5f, vp.Y - 3.5f, 7f, 7f);

                                g.DrawString(pidText, pidFont, pidBrush, vp.X + 5f, vp.Y - 12f);
                            }
                        }

                        if (selectedPointIndex >= 0 && selectedPointIndex < pts.Count)
                        {
                            PointF targetPt = ToCanvasPoint(pts[selectedPointIndex].X, pts[selectedPointIndex].Y);
                            Color targetPenColor = isDarkBackground ? Color.FromArgb(255, 214, 10) : Color.FromArgb(220, 38, 38);
                            using (var targetPen = new Pen(targetPenColor, 2.5f))
                            using (var dotBrush = new SolidBrush(Color.FromArgb(239, 35, 60)))
                            {
                                g.DrawEllipse(targetPen, targetPt.X - 9f, targetPt.Y - 9f, 18f, 18f);
                                g.DrawLine(targetPen, targetPt.X - 13f, targetPt.Y, targetPt.X + 13f, targetPt.Y);
                                g.DrawLine(targetPen, targetPt.X, targetPt.Y - 13f, targetPt.X, targetPt.Y + 13f);
                                g.FillEllipse(dotBrush, targetPt.X - 4f, targetPt.Y - 4f, 8f, 8f);
                            }
                        }
                    }
                }
            }

            // 6. 器械点から現在の測定点へのラバーバンド描画
            double kpX = 0.0, kpY = 0.0;
            bool hasKp = false;
            var km = gbl.KikaiMan;
            if (km.kp != null && (km.kp.X != 0.0 || km.kp.Y != 0.0))
            {
                kpX = km.kp.X;
                kpY = km.kp.Y;
                hasKp = true;
            }
            else
            {
                string kpName = km.kp?.Name ?? Env.KPName ?? Def.GetIniStr("TS", "器械点");
                if (!string.IsNullOrEmpty(kpName))
                {
                    var kikai = db.KikaiList.FirstOrDefault(k => (!string.IsNullOrEmpty(k.Name) && k.Name.Equals(kpName, StringComparison.OrdinalIgnoreCase)) || $"K{k.Id}".Equals(kpName, StringComparison.OrdinalIgnoreCase));
                    if (kikai != null)
                    {
                        kpX = kikai.X;
                        kpY = kikai.Y;
                        hasKp = true;
                    }
                }
            }

            XYZ? curMeasurePos = null;
            if (Env.TSGPS == Env.TSGPS_GPS)
            {
                if (gbl.Gps.curPos != null && (gbl.Gps.curPos.X != 0.0 || gbl.Gps.curPos.Y != 0.0))
                {
                    curMeasurePos = gbl.Gps.curPos;
                }
            }
            else
            {
                if (gbl.TStation.curPos != null && (gbl.TStation.curPos.X != 0.0 || gbl.TStation.curPos.Y != 0.0))
                {
                    curMeasurePos = gbl.TStation.curPos;
                }
            }

            if (hasKp && curMeasurePos != null)
            {
                PointF kpPt = ToCanvasPoint(kpX, kpY);
                PointF curPt = ToCanvasPoint(curMeasurePos.X, curMeasurePos.Y);

                using (var rubberPen = new Pen(Color.FromArgb(255, 230, 0), 2f) { DashStyle = DashStyle.Dash })
                using (var targetPen = new Pen(Color.FromArgb(0, 225, 255), 2f))
                using (var targetBrush = new SolidBrush(Color.FromArgb(180, 0, 225, 255)))
                {
                    g.DrawLine(rubberPen, kpPt, curPt);
                    g.FillEllipse(targetBrush, curPt.X - 4f, curPt.Y - 4f, 8f, 8f);
                    g.DrawEllipse(targetPen, curPt.X - 7f, curPt.Y - 7f, 14f, 14f);
                }
            }

            // 7. Draw Scale Bar (スケールバー)
            if (chkShowScale)
            {
                double scaleDist = gridStep * 2.0; // メッシュサイズ × 2
                PointF p0 = ToCanvasPoint(minSurveyX, minSurveyY);
                PointF p1 = ToCanvasPoint(minSurveyX, minSurveyY + scaleDist);
                float scalePixWidth = (float)Math.Abs(p1.X - p0.X);

                if (scalePixWidth >= 15f && scalePixWidth <= width * 0.95f)
                {
                    float cx = width / 2f;
                    float barY = height - 18f;
                    float startX = cx - scalePixWidth / 2f;
                    float endX = startX + scalePixWidth;

                    Color scaleColor = isDarkBackground ? Color.FromArgb(255, 100, 100) : Color.FromArgb(210, 0, 0);
                    using (var scalePen = new Pen(scaleColor, 1.5f))
                    using (var scaleFont = new Font("Yu Gothic UI", 8.5F, FontStyle.Bold))
                    using (var scaleBrush = new SolidBrush(scaleColor))
                    {
                        // 主横線
                        g.DrawLine(scalePen, startX, barY, endX, barY);

                        // 両端ヒゲ線 (高さ 10px)
                        g.DrawLine(scalePen, startX, barY - 5f, startX, barY + 5f);
                        g.DrawLine(scalePen, endX, barY - 5f, endX, barY + 5f);

                        // 5分割（2メッシュで合計10分割）目盛り
                        int totalDivs = 10;
                        float stepPix = scalePixWidth / totalDivs;
                        for (int i = 1; i < totalDivs; i++)
                        {
                            float tx = startX + i * stepPix;
                            float tickH = (i == 5) ? 4.5f : 2.5f; // 中央（1メッシュ区切り）は少し長め
                            g.DrawLine(scalePen, tx, barY - tickH, tx, barY + tickH);
                        }

                        // テキスト（例: "10M"）
                        string label = (scaleDist < 1) ? $"{scaleDist:0.0}M" : $"{scaleDist:0}M";
                        var sz = g.MeasureString(label, scaleFont);
                        g.DrawString(label, scaleFont, scaleBrush, cx - sz.Width / 2f, barY - sz.Height - 2f);
                    }
                }
            }
        }

        /// <summary>
        /// 印刷イメージの白背景で256x256の全図サムネイル(SITE7.png)をデータベースと同じフォルダに保存します。
        /// </summary>
        public static void SaveThumbnail(
            string dbPath,
            EditorDbManager db,
            bool showIkou = true,
            bool showIbutu = true,
            bool showKikai = true,
            bool drawCurve = true)
        {
            try
            {
                if (string.IsNullOrEmpty(dbPath)) return;
                string? dir = Path.GetDirectoryName(dbPath);
                if (string.IsNullOrEmpty(dir) || !Directory.Exists(dir)) return;

                string pngPath = Path.Combine(dir, "SITE7.png");

                var thumbVc = new EditorMapViewController();
                Size thumbSize = new Size(256, 256);
                thumbVc.UpdateMapBounds(thumbSize, db.IkouLList, db.IbutuList, db.KikaiList, showIkou, showIbutu, showKikai);
                thumbVc.ResetZoom();

                using (var bmp = new Bitmap(256, 256))
                {
                    using (var g = Graphics.FromImage(bmp))
                    {
                        DrawMapCanvas(
                            g,
                            thumbSize,
                            thumbVc,
                            db,
                            selectedIkouId: -1,
                            selectedLid: -1,
                            selectedPointIndex: -1,
                            selectedIbutuId: -1,
                            selectedKikaiId: -1,
                            activeTabIndex: -1,
                            chkShowIkou: showIkou,
                            chkShowIbutu: showIbutu,
                            chkShowKikai: showKikai,
                            chkShowCurve: drawCurve,
                            chkShowGrid: false,
                            chkColorByIkou: false,
                            isLayerVisible: null,
                            showIkouName: false,
                            showIbutuName: false,
                            showKikaiName: false,
                            isDarkBackground: false,
                            chkShowBgImage: false,
                            chkShowBgPointCloud: false
                        );
                    }

                    bmp.Save(pngPath, System.Drawing.Imaging.ImageFormat.Png);
                }
            }
            catch
            {
                // サムネイル保存失敗時はメイン保存処理をブロックしない
            }
        }
    }
}
