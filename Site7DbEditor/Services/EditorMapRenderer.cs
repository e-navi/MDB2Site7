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
            bool chkShowBgPointCloud = true)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(isDarkBackground ? Color.FromArgb(16, 16, 20) : Color.FromArgb(248, 249, 250));

            int width = canvasSize.Width;
            int height = canvasSize.Height;
            if (width <= 0 || height <= 0) return;

            // 1. Update/Cache bounds ONCE per frame (O(N))
            vc.UpdateMapBounds(canvasSize, db.IkouLList, db.IbutuList, db.KikaiList, chkShowIkou, chkShowIbutu, chkShowKikai);

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

            // 0. Draw Mesh/Grid (メッシュ)
            if (chkShowGrid)
            {
                using (var gridPen = new Pen(isDarkBackground ? Color.FromArgb(35, 120, 120, 140) : Color.FromArgb(220, 222, 225), 1f) { DashStyle = DashStyle.Dot })
                {
                    int gridStep = 10;
                    double startGridX = Math.Floor(vc.PosXMin / gridStep) * gridStep;
                    double endGridX = Math.Ceiling(vc.PosXMax / gridStep) * gridStep;
                    double startGridY = Math.Floor(vc.PosYMin / gridStep) * gridStep;
                    double endGridY = Math.Ceiling(vc.PosYMax / gridStep) * gridStep;

                    for (double gx = startGridX; gx <= endGridX; gx += gridStep)
                    {
                        PointF p1 = ToCanvasPoint(vc.PosYMin, gx);
                        PointF p2 = ToCanvasPoint(vc.PosYMax, gx);
                        g.DrawLine(gridPen, p1, p2);
                    }
                    for (double gy = startGridY; gy <= endGridY; gy += gridStep)
                    {
                        PointF p1 = ToCanvasPoint(gy, vc.PosXMin);
                        PointF p2 = ToCanvasPoint(gy, vc.PosXMax);
                        g.DrawLine(gridPen, p1, p2);
                    }
                }
            }

            // 1. Draw Features (遺構L)
            if (chkShowIkou)
            {
                var spline = new Xross_Spline();

                foreach (var line in db.IkouLList)
                {
                    int layerIdx = line.Layer >= 49 ? (line.Layer - 48) : line.Layer;
                    if (isLayerVisible != null && !isLayerVisible(layerIdx)) continue;

                    var pts = SqliteManager.ParsePrecsText(line.Precs);
                    if (pts.Count == 0) continue;

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

                    bool isSelectedFeature = (activeTabIndex == 0 && line.Id == selectedIkouId);

                    int lineDbLayerId = line.Layer >= 49 ? line.Layer : (line.Layer + 48);
                    Color color = chkColorByIkou
                        ? EditorLayerService.PaletteColors[(int)(line.Id % EditorLayerService.PaletteColors.Length)]
                        : EditorLayerService.GetLayerColor(lineDbLayerId, db.LayerList);

                    if (screenPts.Length > 1)
                    {
                        if (isSelectedFeature)
                        {
                            using (var linePen = new Pen(color, 2.8f))
                                g.DrawLines(linePen, screenPts);
                        }
                        else
                        {
                            float penWidth = (layer != null && layer.Width > 0) ? (float)layer.Width : 1.5f;
                            using (var linePen = new Pen(color, penWidth))
                                g.DrawLines(linePen, screenPts);
                        }
                    }
                }
            }

            // 2. Draw Artifacts (遺物)
            if (chkShowIbutu)
            {
                using (var glowPen = new Pen(Color.FromArgb(180, 255, 0, 128), 3f))
                {
                    foreach (var ibutu in db.IbutuList)
                    {
                        PointF pt = ToCanvasPoint(ibutu.X, ibutu.Y);
                        Color ibutuColor = EditorLayerService.GetLayerColor(ibutu.Layer, db.LayerList);

                        using (var ibutuBrush = new SolidBrush(ibutuColor))
                        {
                            if (activeTabIndex == 1 && ibutu.Id == selectedIbutuId)
                            {
                                g.DrawEllipse(glowPen, pt.X - 7f, pt.Y - 7f, 14f, 14f);
                                g.FillEllipse(ibutuBrush, pt.X - 5f, pt.Y - 5f, 10f, 10f);
                                g.DrawEllipse(Pens.White, pt.X - 5f, pt.Y - 5f, 10f, 10f);
                            }
                            else
                            {
                                g.FillEllipse(ibutuBrush, pt.X - 3.5f, pt.Y - 3.5f, 7f, 7f);
                            }
                        }
                    }
                }
            }

            // 3. Draw Control Points (基準点)
            if (chkShowKikai)
            {
                string currentKpName = gbl.KikaiMan.kp?.Name ?? Env.KPName ?? Def.GetIniStr("TS", "器械点");
                string currentBpName = gbl.KikaiMan.bp?.Name ?? Env.BPName ?? Def.GetIniStr("TS", "後視点");

                Color kpTextColor = Color.FromArgb(239, 35, 60);
                Color bpTextColor = isDarkBackground ? Color.FromArgb(0, 200, 255) : Color.FromArgb(0, 102, 204);

                using (var kikaiBrush = new SolidBrush(Color.FromArgb(239, 35, 60)))
                using (var kikaiPen = new Pen(Color.Yellow, 1.5f))
                using (var selectPen = new Pen(Color.FromArgb(0, 225, 255), 3f))
                using (var markFont = new Font("Yu Gothic UI", 9.0F, FontStyle.Bold))
                using (var kpTextBrush = new SolidBrush(kpTextColor))
                using (var bpTextBrush = new SolidBrush(bpTextColor))
                using (var sfFar = new StringFormat { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center })
                {
                    foreach (var kikai in db.KikaiList)
                    {
                        PointF pt = ToCanvasPoint(kikai.X, kikai.Y);
                        g.FillEllipse(kikaiBrush, pt.X - 5f, pt.Y - 5f, 10f, 10f);
                        g.DrawEllipse((activeTabIndex == 2 && kikai.Id == selectedKikaiId) ? selectPen : kikaiPen, pt.X - 5f, pt.Y - 5f, 10f, 10f);

                        // 器械点 / 後視点 の文字描画 (基準点シンボルの左側)
                        string kikaiName = string.IsNullOrEmpty(kikai.Name) ? $"K{kikai.Id}" : kikai.Name;
                        bool isKp = !string.IsNullOrEmpty(currentKpName) && kikaiName.Equals(currentKpName, StringComparison.OrdinalIgnoreCase);
                        bool isBp = !string.IsNullOrEmpty(currentBpName) && kikaiName.Equals(currentBpName, StringComparison.OrdinalIgnoreCase);

                        if (isKp)
                        {
                            g.DrawString("器", markFont, kpTextBrush, pt.X - 6f, pt.Y, sfFar);
                        }
                        else if (isBp)
                        {
                            g.DrawString("後", markFont, bpTextBrush, pt.X - 6f, pt.Y, sfFar);
                        }
                    }
                }
            }

            // 4. Draw Map Labels
            using (var labelFont = new Font("Yu Gothic UI", 8.0F, FontStyle.Bold))
            using (var ikouLabelBrush = new SolidBrush(isDarkBackground ? Color.FromArgb(0, 225, 255) : Color.FromArgb(0, 120, 200)))
            using (var kikaiLabelBrush = new SolidBrush(isDarkBackground ? Color.White : Color.DarkBlue))
            {
                if (showIkouName || activeTabIndex == 0)
                {
                    if (chkShowIkou)
                    {
                        var ikouDict = db.IkouList.ToDictionary(k => k.Id, k => k.Name);

                        foreach (var line in db.IkouLList)
                        {
                            int layerIdx = line.Layer >= 49 ? (line.Layer - 48) : line.Layer;
                            if (isLayerVisible != null && !isLayerVisible(layerIdx)) continue;

                            var pts = SqliteManager.ParsePrecsText(line.Precs);
                            if (pts.Count == 0) continue;

                            ikouDict.TryGetValue(line.Id, out string? ikouName);
                            ikouName ??= "";

                            string lineName = line.Name ?? "";
                            string labelText = "";

                            if (!string.IsNullOrEmpty(ikouName) && !string.IsNullOrEmpty(lineName))
                                labelText = $"{ikouName}:{lineName}";
                            else if (!string.IsNullOrEmpty(ikouName))
                                labelText = ikouName;
                            else if (!string.IsNullOrEmpty(lineName))
                                labelText = lineName;

                            if (string.IsNullOrEmpty(labelText)) continue;

                            PointF midPt = ToCanvasPoint(pts[pts.Count / 2].X, pts[pts.Count / 2].Y);
                            SizeF textSize = g.MeasureString(labelText, labelFont);

                            g.DrawString(labelText, labelFont, ikouLabelBrush, midPt.X + 4, midPt.Y - textSize.Height / 2f);
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
                            Color ibutuColor = EditorLayerService.GetLayerColor(ibutu.Layer, db.LayerList);

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

            // 5. 選択中の遺構線のマーク(〇)・折れ線ガイド(薄いグレー)・点番号(PID)を最前面描画
            if (chkShowIkou && activeTabIndex == 0)
            {
                var selectedLine = db.IkouLList.FirstOrDefault(l => l.Id == selectedIkouId && l.Lid == selectedLid);
                if (selectedLine != null)
                {
                    var pts = SqliteManager.ParsePrecsText(selectedLine.Precs);
                    if (pts.Count > 0)
                    {
                        int dbLayerId = selectedLine.Layer >= 49 ? selectedLine.Layer : (selectedLine.Layer + 48);
                        var layer = db.LayerList.FirstOrDefault(l => l.Id == dbLayerId);
                        bool isLayerCurve = (layer != null) ? (layer.LType == 2) : true;
                        bool drawAsCurve = chkShowCurve && isLayerCurve && pts.Count >= 3;

                        // 曲線表示時に、ベースとなる折れ線を薄いグレー(破線)で表示し、中間点に□マークを表示
                        if (drawAsCurve && pts.Count > 1)
                        {
                            var polylinePts = pts.Select(p => ToCanvasPoint(p.X, p.Y)).ToArray();
                            Color grayColor = isDarkBackground
                                ? Color.FromArgb(160, 160, 170, 185)
                                : Color.FromArgb(170, 120, 130, 140);
                            using (var grayPen = new Pen(grayColor, 1.2f) { DashStyle = DashStyle.Dash })
                            {
                                g.DrawLines(grayPen, polylinePts);
                            }

                            // 中間点（□）の描画
                            using (var midPen = new Pen(Color.FromArgb(255, 220, 0), 1.5f))
                            using (var midBrush = new SolidBrush(Color.FromArgb(230, 255, 255, 255)))
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
                            }
                        }

                        using (var vertexPen = new Pen(Color.FromArgb(255, 220, 0), 1.5f))
                        using (var vertexBrush = new SolidBrush(Color.FromArgb(230, 255, 255, 255)))
                        using (var pidFont = new Font("Yu Gothic UI", 8.0F, FontStyle.Bold))
                        using (var pidBrush = new SolidBrush(Color.FromArgb(255, 220, 0)))
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
                            using (var targetPen = new Pen(Color.FromArgb(255, 214, 10), 2.5f))
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
        }
    }
}
