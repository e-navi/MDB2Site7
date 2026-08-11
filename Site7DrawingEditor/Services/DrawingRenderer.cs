using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace Site7DrawingEditor.Services
{
    public static class DrawingRenderer
    {
        #region 1. Top-Left Crop Canvas (全体図: 測量座標系 X=北(上), Y=東(右))

        public static void DrawCropCanvas(
            Graphics g,
            Size canvasSize,
            CanvasViewController vc,
            DrawingDbManager db,
            DrawingIkouModel? curSelectedIkou,
            bool chkShowGridFull,
            bool chkShowIkouFull,
            bool chkShowCurveFull,
            bool chkColorByIkouFull,
            bool chkShowIbutuFull,
            bool chkShowKikaiFull,
            Func<int, bool>? isLayerVisible = null,
            bool showIkouName = false,
            bool showIbutuName = false,
            bool showKikaiName = true,
            bool isDarkBackground = false)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;

            int width = canvasSize.Width;
            int height = canvasSize.Height;
            if (width <= 0 || height <= 0) return;

            g.Clear(isDarkBackground ? Color.FromArgb(20, 20, 25) : Color.FromArgb(248, 249, 250));

            // Fast single-pass bounding box calculation without string allocations
            double posXMin = double.MaxValue, posXMax = double.MinValue;
            double posYMin = double.MaxValue, posYMax = double.MinValue;

            foreach (var ik in db.MasterIkouList)
            {
                if (ik.Y < posXMin) posXMin = ik.Y;
                if (ik.Y > posXMax) posXMax = ik.Y;
                if (ik.X < posYMin) posYMin = ik.X;
                if (ik.X > posYMax) posYMax = ik.X;
            }
            foreach (var ib in db.MasterIbutuList)
            {
                if (ib.Y < posXMin) posXMin = ib.Y;
                if (ib.Y > posXMax) posXMax = ib.Y;
                if (ib.X < posYMin) posYMin = ib.X;
                if (ib.X > posYMax) posYMax = ib.X;
            }
            foreach (var k in db.MasterKikaiList)
            {
                if (k.Y < posXMin) posXMin = k.Y;
                if (k.Y > posXMax) posXMax = k.Y;
                if (k.X < posYMin) posYMin = k.X;
                if (k.X > posYMax) posYMax = k.X;
            }

            if (posXMin == double.MaxValue)
            {
                posXMin = -50; posXMax = 50;
                posYMin = -50; posYMax = 50;
            }

            double rangeX = posXMax - posXMin;
            double rangeY = posYMax - posYMin;
            if (rangeX < 0.001) rangeX = 1.0;
            if (rangeY < 0.001) rangeY = 1.0;

            int margin = 35;
            double scale = Math.Min((width - margin * 2) / rangeX, (height - margin * 2) / rangeY);
            float offsetX = (float)((width - rangeX * scale) / 2.0);
            float offsetY = (float)((height - rangeY * scale) / 2.0);

            float cx = width / 2f;
            float cy = height / 2f;

            PointF ToCanvasPoint(double surveyX, double surveyY)
            {
                double posX = surveyY;
                double posY = surveyX;
                float bx = (float)(offsetX + (posX - posXMin) * scale);
                float by = (float)(height - offsetY - (posY - posYMin) * scale);
                float px = cx + (bx - cx) * vc.CropZoom + vc.CropPan.X;
                float py = cy + (by - cy) * vc.CropZoom + vc.CropPan.Y;
                return new PointF(px, py);
            }

            if (chkShowGridFull)
            {
                using (var gridPen = new Pen(Color.FromArgb(230, 232, 235), 1.0f) { DashStyle = DashStyle.Dot })
                {
                    for (int x = 0; x < width; x += 40) g.DrawLine(gridPen, x, 0, x, height);
                    for (int y = 0; y < height; y += 40) g.DrawLine(gridPen, 0, y, width, y);
                }
            }

            if (chkShowIkouFull)
            {
                var spline = new Xross_Spline();

                long selectedMasterId = -1;
                if (curSelectedIkou != null)
                {
                    var matched = db.MasterIkouList.FirstOrDefault(ik =>
                        (!string.IsNullOrWhiteSpace(ik.Name) && ik.Name.Equals(curSelectedIkou.Name, StringComparison.OrdinalIgnoreCase)) ||
                        ($"遺構{ik.Id}".Equals(curSelectedIkou.Name, StringComparison.OrdinalIgnoreCase))
                    );
                    if (matched != null) selectedMasterId = matched.Id;
                }

                foreach (var line in db.MasterIkouLList)
                {
                    if (isLayerVisible != null && !isLayerVisible(line.Layer)) continue;

                    var pts = SqliteDrawingManager.ParsePrecsText(line.Precs);
                    if (pts.Count == 0) continue;

                    PointF[] screenPts;
                    if (chkShowCurveFull && pts.Count >= 3)
                    {
                        var curvePts = line.Mode == 1 ? spline.Calc3DCloseCurvePoints(pts, 5) : spline.Calc3DCurvePoints(pts, 5);
                        screenPts = curvePts.Select(p => ToCanvasPoint(p.X, p.Y)).ToArray();
                    }
                    else
                    {
                        screenPts = pts.Select(p => ToCanvasPoint(p.X, p.Y)).ToArray();
                    }

                    Color color = chkColorByIkouFull
                        ? LayerManager.PaletteColors[(int)(line.Id % LayerManager.PaletteColors.Length)]
                        : LayerManager.GetLayerColor(line.Layer, isDarkBackground: isDarkBackground);
                    bool isSelectedFeature = (selectedMasterId > 0 && line.Id == selectedMasterId);

                    if (screenPts.Length > 1)
                    {
                        if (isSelectedFeature)
                        {
                            using (var linePen = new Pen(Color.FromArgb(255, color.R, color.G, color.B), 3.0f))
                                g.DrawLines(linePen, screenPts);
                        }
                        else
                        {
                            using (var linePen = new Pen(Color.FromArgb(180, color.R, color.G, color.B), 1.5f))
                                g.DrawLines(linePen, screenPts);
                        }
                    }
                }

                if (showIkouName)
                {
                    using (var font = new Font("Yu Gothic UI", 8.5F, FontStyle.Bold))
                    using (var brush = new SolidBrush(isDarkBackground ? Color.White : Color.FromArgb(60, 60, 70)))
                    {
                        foreach (var ik in db.MasterIkouList)
                        {
                            if (ik.X != 0 || ik.Y != 0)
                            {
                                PointF pt = ToCanvasPoint(ik.X, ik.Y);
                                string label = string.IsNullOrWhiteSpace(ik.Name) ? $"遺構{ik.Id}" : ik.Name;
                                g.DrawString(label, font, brush, pt.X + 4f, pt.Y - 12f);
                            }
                        }
                    }
                }
            }

            if (chkShowIbutuFull)
            {
                using (var ibutuBrush = new SolidBrush(Color.FromArgb(255, 191, 0)))
                using (var font = new Font("Yu Gothic UI", 8F, FontStyle.Bold))
                using (var textBrush = new SolidBrush(isDarkBackground ? Color.Yellow : Color.FromArgb(180, 100, 0)))
                {
                    foreach (var ib in db.MasterIbutuList)
                    {
                        PointF pt = ToCanvasPoint(ib.X, ib.Y);
                        g.FillEllipse(ibutuBrush, pt.X - 3.5f, pt.Y - 3.5f, 7f, 7f);
                        if (showIbutuName)
                        {
                            string label = string.IsNullOrWhiteSpace(ib.Syubetu) ? $"遺物{ib.Id}" : ib.Syubetu;
                            g.DrawString(label, font, textBrush, pt.X + 5f, pt.Y - 5f);
                        }
                    }
                }
            }

            if (chkShowKikaiFull)
            {
                using (var kikaiBrush = new SolidBrush(Color.FromArgb(239, 35, 60)))
                using (var kikaiPen = new Pen(Color.Yellow, 1.5f))
                using (var font = new Font("Yu Gothic UI", 8F, FontStyle.Bold))
                using (var textBrush = new SolidBrush(isDarkBackground ? Color.White : Color.DarkBlue))
                {
                    foreach (var k in db.MasterKikaiList)
                    {
                        PointF pt = ToCanvasPoint(k.X, k.Y);
                        g.FillEllipse(kikaiBrush, pt.X - 5f, pt.Y - 5f, 10f, 10f);
                        g.DrawEllipse(kikaiPen, pt.X - 5f, pt.Y - 5f, 10f, 10f);
                        if (showKikaiName)
                        {
                            g.DrawString(k.Name, font, textBrush, pt.X + 6f, pt.Y - 6f);
                        }
                    }
                }
            }

            // 3点指示（p1:左下, p2:右下, p3:高さ指示）による長方形枠の描画
            foreach (var ikou in db.DrawingIkousList)
            {
                bool isSelected = (curSelectedIkou == ikou);

                var (v1, v2, v3, v4) = GeometryMath.GetCropBoxVertices(ikou.P1, ikou.P2, ikou.P3);

                PointF pt1 = ToCanvasPoint(v1.X, v1.Y);
                PointF pt2 = ToCanvasPoint(v2.X, v2.Y);
                PointF pt3 = ToCanvasPoint(v3.X, v3.Y);
                PointF pt4 = ToCanvasPoint(v4.X, v4.Y);

                Color boxCol = isSelected ? Color.FromArgb(255, 255, 191, 0) : Color.FromArgb(140, 180, 180, 190);
                float boxWidth = isSelected ? 2.5f : 1.2f;

                using (var boxPen = new Pen(boxCol, boxWidth) { DashStyle = DashStyle.Dash })
                using (var font = new Font("Yu Gothic UI", 8.5F, FontStyle.Bold))
                using (var brush = new SolidBrush(boxCol))
                {
                    g.DrawPolygon(boxPen, new[] { pt1, pt2, pt3, pt4 });
                    g.DrawString(ikou.Name, font, brush, pt1.X, pt1.Y - 15);
                }

                if (isSelected)
                {
                    void DrawHandle(PointF pt, string label, Color col)
                    {
                        using (var b = new SolidBrush(col))
                        using (var p = new Pen(Color.White, 2f))
                        using (var f = new Font("Yu Gothic UI", 9F, FontStyle.Bold))
                        using (var tb = new SolidBrush(Color.FromArgb(40, 40, 40)))
                        {
                            g.FillEllipse(b, pt.X - 6f, pt.Y - 6f, 12f, 12f);
                            g.DrawEllipse(p, pt.X - 6f, pt.Y - 6f, 12f, 12f);
                            g.DrawString(label, f, tb, pt.X + 8f, pt.Y - 8f);
                        }
                    }

                    PointF p3Handle = ToCanvasPoint(ikou.P3.X, ikou.P3.Y);

                    DrawHandle(pt1, "p1 (左下)", Color.FromArgb(239, 35, 60));
                    DrawHandle(pt2, "p2 (右下)", Color.FromArgb(56, 176, 0));
                    DrawHandle(p3Handle, "p3 (高さ指示)", Color.FromArgb(0, 180, 216));
                }
            }

            // 3点枠指示インタラクティブ・ラバーバンド描画
            if (vc.CropStep > 0 && curSelectedIkou != null)
            {
                using (var rubberPen = new Pen(Color.FromArgb(255, 220, 0), 2.2f) { DashStyle = DashStyle.Dash })
                using (var font = new Font("Yu Gothic UI", 9F, FontStyle.Bold))
                using (var textBrush = new SolidBrush(Color.FromArgb(255, 220, 0)))
                {
                    if (vc.CropStep == 2)
                    {
                        PointF pt1 = ToCanvasPoint(curSelectedIkou.P1.X, curSelectedIkou.P1.Y);
                        g.DrawLine(rubberPen, pt1, vc.CropLastMousePos);
                        g.FillEllipse(textBrush, pt1.X - 5f, pt1.Y - 5f, 10f, 10f);
                        g.DrawString("p1 (確定)", font, textBrush, pt1.X + 8f, pt1.Y - 8f);
                        g.DrawString("p2 (マウス位置)", font, textBrush, vc.CropLastMousePos.X + 8f, vc.CropLastMousePos.Y - 8f);
                    }
                    else if (vc.CropStep == 3)
                    {
                        var (msx, msy) = vc.CanvasToSurveyCrop(vc.CropLastMousePos, canvasSize, db.MasterIkouLList, db.MasterIbutuList, db.MasterKikaiList);
                        XYZ p3Temp = GeometryMath.ProjectToPerpendicular(curSelectedIkou.P1, curSelectedIkou.P2, msx, msy);
                        var (v1, v2, v3, v4) = GeometryMath.GetCropBoxVertices(curSelectedIkou.P1, curSelectedIkou.P2, p3Temp);

                        PointF pt1 = ToCanvasPoint(v1.X, v1.Y);
                        PointF pt2 = ToCanvasPoint(v2.X, v2.Y);
                        PointF pt3 = ToCanvasPoint(v3.X, v3.Y);
                        PointF pt4 = ToCanvasPoint(v4.X, v4.Y);

                        g.DrawPolygon(rubberPen, new[] { pt1, pt2, pt3, pt4 });
                        g.DrawString("p3 (高さプレビュー)", font, textBrush, vc.CropLastMousePos.X + 8f, vc.CropLastMousePos.Y - 8f);
                    }
                }
            }
        }

        #endregion

        #region 2. Top-Right Paper Layout Canvas Painting (数学座標系: 原点(0,0)=用紙中心)

        public static void DrawPaperCanvas(
            Graphics g,
            Size canvasSize,
            CanvasViewController vc,
            DrawingDbManager db,
            DrawingModel? curDrawing,
            DrawingIkouModel? curSelectedIkou,
            bool chkShowCurvePaper,
            bool chkColorByIkouPaper,
            bool chkShowDirectionPaper,
            bool chkShowDanmenPaper)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;

            int canvasWidth = canvasSize.Width;
            int canvasHeight = canvasSize.Height;

            g.Clear(Color.FromArgb(20, 20, 25));

            if (curDrawing == null) return;

            var pInfo = curDrawing.PaperInfo;

            int margin = 25;
            double paperAspect = pInfo.WidthMm / pInfo.HeightMm;
            double screenAspect = (double)(canvasWidth - margin * 2) / (canvasHeight - margin * 2);

            double renderPaperWidth, renderPaperHeight;
            if (screenAspect > paperAspect)
            {
                renderPaperHeight = canvasHeight - margin * 2;
                renderPaperWidth = renderPaperHeight * paperAspect;
            }
            else
            {
                renderPaperWidth = canvasWidth - margin * 2;
                renderPaperHeight = renderPaperWidth / paperAspect;
            }

            float cx = canvasWidth / 2f;
            float cy = canvasHeight / 2f;

            float paperCenterX = cx + vc.PaperPan.X;
            float paperCenterY = cy + vc.PaperPan.Y;

            float paperW = (float)(renderPaperWidth * vc.PaperZoom);
            float paperH = (float)(renderPaperHeight * vc.PaperZoom);
            float paperLeft = paperCenterX - paperW / 2f;
            float paperTop = paperCenterY - paperH / 2f;

            using (var paperBrush = new SolidBrush(Color.FromArgb(250, 250, 250)))
            using (var paperPen = new Pen(Color.FromArgb(0, 180, 216), 2f))
            {
                g.FillRectangle(paperBrush, paperLeft, paperTop, paperW, paperH);
                g.DrawRectangle(paperPen, paperLeft, paperTop, paperW, paperH);
            }

            using (var centerPen = new Pen(Color.FromArgb(120, 255, 0, 128), 1f) { DashStyle = DashStyle.Dash })
            {
                g.DrawLine(centerPen, paperCenterX - 15f, paperCenterY, paperCenterX + 15f, paperCenterY);
                g.DrawLine(centerPen, paperCenterX, paperCenterY - 15f, paperCenterX, paperCenterY + 15f);
            }

            using (var titleFont = new Font("Yu Gothic UI", 8.5F, FontStyle.Bold))
            using (var titleBrush = new SolidBrush(Color.Black))
            using (var titlePen = new Pen(Color.Black, 1.2f))
            {
                float tbW = paperW * 0.30f;
                float tbH = paperH * 0.12f;
                float tbX = paperLeft + paperW - tbW - 8f;
                float tbY = paperTop + paperH - tbH - 8f;
                g.DrawRectangle(titlePen, tbX, tbY, tbW, tbH);
                g.DrawString($"図面名: {curDrawing.Name}\n用紙: {pInfo.Name} | 縮尺: 1/{curDrawing.Scale}", titleFont, titleBrush, tbX + 4f, tbY + 4f);
            }

            PointF PaperMmToCanvas(double mmX, double mmY)
            {
                float px = paperCenterX + (float)(mmX / pInfo.WidthMm * paperW);
                float py = paperCenterY - (float)(mmY / pInfo.HeightMm * paperH);
                return new PointF(px, py);
            }

            var currentIkous = db.DrawingIkousList.Where(di => di.ZID == curDrawing.ZID).ToList();
            var baseIkou = currentIkous.FirstOrDefault();
            var spline = new Xross_Spline();

            // 全体遺構図 (Type == 0) の場合、配置された「最初の遺構 (baseIkou)」を基準に傾き考慮のトンボ(+)・斜め外周座標を描画
            if (curDrawing.Type == 0 && baseIkou != null)
            {
                double paperStepMm = (pInfo.WidthMm >= 400 || pInfo.HeightMm >= 400 || curDrawing.PaperSize < 3) ? 100.0 : 50.0;
                double scaleRatio = curDrawing.Scale / 1000.0;
                double deltaSurvey = paperStepMm * scaleRatio;

                double halfW = pInfo.WidthMm / 2.0;
                double halfH = pInfo.HeightMm / 2.0;

                var (_, _, _, ux, uy, vx, vy, center) = GeometryMath.CalculateCropBox(baseIkou.P1, baseIkou.P2, baseIkou.P3);

                float angleSurX = (float)(Math.Atan2(-vx, ux) * 180.0 / Math.PI);
                float angleSurY = (float)(Math.Atan2(-vy, uy) * 180.0 / Math.PI);

                Point3D pBL = GeometryMath.PaperPointToSurvey(new PointF((float)-halfW, (float)-halfH), baseIkou.P1, baseIkou.P2, baseIkou.P3, baseIkou.PP, curDrawing.Scale);
                Point3D pBR = GeometryMath.PaperPointToSurvey(new PointF((float)halfW, (float)-halfH), baseIkou.P1, baseIkou.P2, baseIkou.P3, baseIkou.PP, curDrawing.Scale);
                Point3D pTR = GeometryMath.PaperPointToSurvey(new PointF((float)halfW, (float)halfH), baseIkou.P1, baseIkou.P2, baseIkou.P3, baseIkou.PP, curDrawing.Scale);
                Point3D pTL = GeometryMath.PaperPointToSurvey(new PointF((float)-halfW, (float)halfH), baseIkou.P1, baseIkou.P2, baseIkou.P3, baseIkou.PP, curDrawing.Scale);

                double minSurX = Math.Min(Math.Min(pBL.X, pBR.X), Math.Min(pTR.X, pTL.X));
                double maxSurX = Math.Max(Math.Max(pBL.X, pBR.X), Math.Max(pTR.X, pTL.X));
                double minSurY = Math.Min(Math.Min(pBL.Y, pBR.Y), Math.Min(pTR.Y, pTL.Y));
                double maxSurY = Math.Max(Math.Max(pBL.Y, pBR.Y), Math.Max(pTR.Y, pTL.Y));

                double startSurX = Math.Floor(minSurX / deltaSurvey) * deltaSurvey;
                double endSurX = Math.Ceiling(maxSurX / deltaSurvey) * deltaSurvey;
                double startSurY = Math.Floor(minSurY / deltaSurvey) * deltaSurvey;
                double endSurY = Math.Ceiling(maxSurY / deltaSurvey) * deltaSurvey;

                using (var tomboPen = new Pen(Color.FromArgb(160, 200, 30, 30), 1.2f))
                using (var borderPen = new Pen(Color.FromArgb(180, 200, 30, 30), 1.2f))
                using (var coordFont = new Font("Yu Gothic UI", 7.5F, FontStyle.Bold))
                using (var coordBrush = new SolidBrush(Color.FromArgb(200, 30, 30)))
                {
                    // A. 傾き追従型格子交差トンボ (+) の描画
                    for (double sx = startSurX; sx <= endSurX + 0.001; sx += deltaSurvey)
                    {
                        for (double sy = startSurY; sy <= endSurY + 0.001; sy += deltaSurvey)
                        {
                            var (xLocalM, yLocalM) = GeometryMath.SurveyToFeatureLocalCenter(sx, sy, baseIkou.P1, baseIkou.P2, baseIkou.P3);
                            PointF paperMmPt = GeometryMath.LocalCenterToPaperPoint(xLocalM, yLocalM, baseIkou.PP, curDrawing.Scale);

                            if (paperMmPt.X >= -halfW + 0.1 && paperMmPt.X <= halfW - 0.1 &&
                                paperMmPt.Y >= -halfH + 0.1 && paperMmPt.Y <= halfH - 0.1)
                            {
                                PointF cvPt = PaperMmToCanvas(paperMmPt.X, paperMmPt.Y);

                                float arm = 6f;
                                float radX = angleSurX * (float)Math.PI / 180f;
                                float radY = angleSurY * (float)Math.PI / 180f;

                                g.DrawLine(tomboPen,
                                    cvPt.X - arm * (float)Math.Cos(radX), cvPt.Y - arm * (float)Math.Sin(radX),
                                    cvPt.X + arm * (float)Math.Cos(radX), cvPt.Y + arm * (float)Math.Sin(radX));

                                g.DrawLine(tomboPen,
                                    cvPt.X - arm * (float)Math.Cos(radY), cvPt.Y - arm * (float)Math.Sin(radY),
                                    cvPt.X + arm * (float)Math.Cos(radY), cvPt.Y + arm * (float)Math.Sin(radY));
                            }
                        }
                    }

                    // B. 真世界測量 X 直線 (S_X = sx) と 用紙外枠の斜め交差 ＆ 回転座標表記
                    for (double sx = startSurX; sx <= endSurX + 0.001; sx += deltaSurvey)
                    {
                        var (locM1, locM2) = GeometryMath.SurveyToFeatureLocalCenter(sx, minSurY, baseIkou.P1, baseIkou.P2, baseIkou.P3);
                        var (locM3, locM4) = GeometryMath.SurveyToFeatureLocalCenter(sx, maxSurY, baseIkou.P1, baseIkou.P2, baseIkou.P3);
                        PointF pt1 = GeometryMath.LocalCenterToPaperPoint(locM1, locM2, baseIkou.PP, curDrawing.Scale);
                        PointF pt2 = GeometryMath.LocalCenterToPaperPoint(locM3, locM4, baseIkou.PP, curDrawing.Scale);

                        var intersections = GeometryMath.FindGridLinePaperIntersections(pt1, pt2, halfW, halfH);
                        foreach (var (paperMmPt, borderSide) in intersections)
                        {
                            PointF cvPt = PaperMmToCanvas(paperMmPt.X, paperMmPt.Y);

                            float lineLen = 14f;
                            float radY = angleSurY * (float)Math.PI / 180f;
                            g.DrawLine(borderPen,
                                cvPt.X - (lineLen / 2f) * (float)Math.Cos(radY), cvPt.Y - (lineLen / 2f) * (float)Math.Sin(radY),
                                cvPt.X + (lineLen / 2f) * (float)Math.Cos(radY), cvPt.Y + (lineLen / 2f) * (float)Math.Sin(radY));

                            float rotText = angleSurY;
                            while (rotText > 90f) rotText -= 180f;
                            while (rotText < -90f) rotText += 180f;

                            string labelStr = $"X={sx:0}";
                            DrawRotatedString(g, labelStr, coordFont, coordBrush, cvPt, rotText, StringAlignment.Center, StringAlignment.Center);
                        }
                    }

                    // C. 真世界測量 Y 直線 (S_Y = sy) と 用紙外枠の斜め交差 ＆ 回転座標表記
                    for (double sy = startSurY; sy <= endSurY + 0.001; sy += deltaSurvey)
                    {
                        var (locM1, locM2) = GeometryMath.SurveyToFeatureLocalCenter(minSurX, sy, baseIkou.P1, baseIkou.P2, baseIkou.P3);
                        var (locM3, locM4) = GeometryMath.SurveyToFeatureLocalCenter(maxSurX, sy, baseIkou.P1, baseIkou.P2, baseIkou.P3);
                        PointF pt1 = GeometryMath.LocalCenterToPaperPoint(locM1, locM2, baseIkou.PP, curDrawing.Scale);
                        PointF pt2 = GeometryMath.LocalCenterToPaperPoint(locM3, locM4, baseIkou.PP, curDrawing.Scale);

                        var intersections = GeometryMath.FindGridLinePaperIntersections(pt1, pt2, halfW, halfH);
                        foreach (var (paperMmPt, borderSide) in intersections)
                        {
                            PointF cvPt = PaperMmToCanvas(paperMmPt.X, paperMmPt.Y);

                            float lineLen = 14f;
                            float radX = angleSurX * (float)Math.PI / 180f;
                            g.DrawLine(borderPen,
                                cvPt.X - (lineLen / 2f) * (float)Math.Cos(radX), cvPt.Y - (lineLen / 2f) * (float)Math.Sin(radX),
                                cvPt.X + (lineLen / 2f) * (float)Math.Cos(radX), cvPt.Y + (lineLen / 2f) * (float)Math.Sin(radX));

                            float rotText = angleSurX;
                            while (rotText > 90f) rotText -= 180f;
                            while (rotText < -90f) rotText += 180f;

                            string labelStr = $"Y={sy:0}";
                            DrawRotatedString(g, labelStr, coordFont, coordBrush, cvPt, rotText, StringAlignment.Center, StringAlignment.Center);
                        }
                    }
                }
            }

            foreach (var ikou in currentIkous)
            {
                if (ikou.LList.Count == 0) continue;

                var (_, widthM, heightM, ux, uy, _, _, _) = GeometryMath.CalculateCropBox(ikou.P1, ikou.P2, ikou.P3);
                double scaleFactorMm = 1000.0 / curDrawing.Scale;

                PointF ptBL = PaperMmToCanvas(ikou.PP.X - (widthM / 2.0) * scaleFactorMm, ikou.PP.Y - (heightM / 2.0) * scaleFactorMm);
                PointF ptBR = PaperMmToCanvas(ikou.PP.X + (widthM / 2.0) * scaleFactorMm, ikou.PP.Y - (heightM / 2.0) * scaleFactorMm);
                PointF ptTR = PaperMmToCanvas(ikou.PP.X + (widthM / 2.0) * scaleFactorMm, ikou.PP.Y + (heightM / 2.0) * scaleFactorMm);
                PointF ptTL = PaperMmToCanvas(ikou.PP.X - (widthM / 2.0) * scaleFactorMm, ikou.PP.Y + (heightM / 2.0) * scaleFactorMm);

                using (var clipPath = new GraphicsPath())
                {
                    clipPath.AddPolygon(new[] { ptBL, ptBR, ptTR, ptTL });
                    Region oldClip = g.Clip;
                    g.SetClip(clipPath);

                    foreach (var line in ikou.LList)
                    {
                        if (line.Pnts.Count == 0) continue;

                        var layer = db.MasterLayerList.FirstOrDefault(l => l.Id == line.Layer || l.Id == (line.Layer >= 49 ? line.Layer : line.Layer + 48));
                        bool isLayerCurve = (layer != null) ? (layer.LType == 2) : true;
                        bool shouldDrawCurve = chkShowCurvePaper && isLayerCurve && line.Pnts.Count >= 3;

                        List<Point3D> renderPnts;
                        if (shouldDrawCurve)
                        {
                            renderPnts = line.Flag == 1 ? spline.Calc3DCloseCurvePoints(line.Pnts, 5) : spline.Calc3DCurvePoints(line.Pnts, 5);
                        }
                        else
                        {
                            renderPnts = line.Pnts;
                        }

                        var paperScreenPts = new List<PointF>();
                        foreach (var pt in renderPnts)
                        {
                            PointF paperPt = GeometryMath.SurveyToPaperPoint(pt.X, pt.Y, ikou.P1, ikou.P2, ikou.P3, ikou.PP, curDrawing.Scale);
                            paperScreenPts.Add(PaperMmToCanvas(paperPt.X, paperPt.Y));
                        }

                        if (paperScreenPts.Count > 1)
                        {
                            Color col = chkColorByIkouPaper
                                ? LayerManager.PaletteColors[(int)(ikou.IID % LayerManager.PaletteColors.Length)]
                                : LayerManager.GetLayerColor(line.Layer);
                            using (var linePen = new Pen(col, 1.8f))
                            {
                                g.DrawLines(linePen, paperScreenPts.ToArray());
                            }
                        }
                    }

                    g.Clip = oldClip;
                }

                using (var framePen = new Pen(Color.FromArgb(170, 180, 185), 1.2f) { DashStyle = DashStyle.Dash })
                using (var nameFont = new Font("Yu Gothic UI", 8.5F, FontStyle.Bold))
                using (var nameBrush = new SolidBrush(Color.FromArgb(120, 120, 130)))
                {
                    g.DrawPolygon(framePen, new[] { ptBL, ptBR, ptTR, ptTL });
                    g.DrawString(ikou.Name, nameFont, nameBrush, ptBL.X + 2f, ptBL.Y + 2f);
                }

                // 正確な方位マーク (対角線長さの1/10のサイズで描画)
                if (chkShowDirectionPaper && ikou.IsShowDirection == 1)
                {
                    PointF compassCenter = PaperMmToCanvas(ikou.PP.X + ikou.PDirection.X, ikou.PP.Y + ikou.PDirection.Y);

                    double diagM = Math.Sqrt(widthM * widthM + heightM * heightM);
                    double diagMm = diagM * scaleFactorMm;
                    double diagPx = diagMm * (paperW / pInfo.WidthMm);
                    float arrowLen = Math.Max(6f, (float)(diagPx / 10.0));

                    float nX = compassCenter.X + arrowLen * (float)ux;
                    float nY = compassCenter.Y - arrowLen * (float)uy;

                    using (var arrowPen = new Pen(Color.FromArgb(56, 176, 0), 2.2f))
                    using (var font = new Font("Yu Gothic UI", 9.5F, FontStyle.Bold))
                    using (var brush = new SolidBrush(Color.FromArgb(56, 176, 0)))
                    {
                        g.DrawLine(arrowPen, compassCenter.X, compassCenter.Y, nX, nY);
                        g.DrawString("N", font, brush, nX - 5f, nY - 16f);
                        g.FillEllipse(brush, compassCenter.X - 3f, compassCenter.Y - 3f, 6f, 6f);
                    }
                }

                if (chkShowDanmenPaper)
                {
                    foreach (var dm in ikou.DmList)
                    {
                        PointF spPt = GeometryMath.SurveyToPaperPoint(dm.Sp.X, dm.Sp.Y, ikou.P1, ikou.P2, ikou.P3, ikou.PP, curDrawing.Scale);
                        PointF epPt = GeometryMath.SurveyToPaperPoint(dm.Ep.X, dm.Ep.Y, ikou.P1, ikou.P2, ikou.P3, ikou.PP, curDrawing.Scale);
                        PointF dpPt = GeometryMath.SurveyToPaperPoint(dm.Dp.X, dm.Dp.Y, ikou.P1, ikou.P2, ikou.P3, ikou.PP, curDrawing.Scale);

                        PointF spCanvas = PaperMmToCanvas(spPt.X, spPt.Y);
                        PointF epCanvas = PaperMmToCanvas(epPt.X, epPt.Y);
                        PointF dpCanvas = PaperMmToCanvas(dpPt.X, dpPt.Y);

                        double totalLenM = dm.DmpList.Count > 1 ? Math.Max(1e-6, dm.DmpList.Last().Len) : 1.0;
                        double lenPx = Math.Sqrt(Math.Pow(epCanvas.X - spCanvas.X, 2) + Math.Pow(epCanvas.Y - spCanvas.Y, 2));
                        double pixelsPerMeter = lenPx / totalLenM;

                        RenderUnifiedSection(g, dm, spCanvas, epCanvas, dpCanvas, pixelsPerMeter);
                    }
                }
            }

            // 遺構枠配置位置指定（Paper Position Pick）ラバーバンド描画
            if (vc.IsPickingPaperPosition && curSelectedIkou != null)
            {
                PointF originPt = PaperMmToCanvas(curSelectedIkou.PP.X, curSelectedIkou.PP.Y);
                using (var rubberPen = new Pen(Color.FromArgb(0, 225, 255), 2f) { DashStyle = DashStyle.Dash })
                using (var font = new Font("Yu Gothic UI", 9F, FontStyle.Bold))
                using (var brush = new SolidBrush(Color.FromArgb(0, 225, 255)))
                {
                    g.DrawLine(rubberPen, originPt, vc.PaperLastMousePos);
                    g.FillEllipse(brush, originPt.X - 4f, originPt.Y - 4f, 8f, 8f);
                    g.DrawEllipse(rubberPen, vc.PaperLastMousePos.X - 8f, vc.PaperLastMousePos.Y - 8f, 16f, 16f);
                    g.DrawString("配置目標位置 (右クリックでキャンセル)", font, brush, vc.PaperLastMousePos.X + 10f, vc.PaperLastMousePos.Y - 8f);
                }
            }
        }

        #endregion

        #region 3. Bottom-Right Feature Detail Preview Canvas Painting

        public static void DrawDetailCanvas(
            Graphics g,
            Size canvasSize,
            CanvasViewController vc,
            DrawingModel? curDrawing,
            DrawingIkouModel? curIkou,
            bool chkColorByIkouFull,
            bool chkShowDirection)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;

            int width = canvasSize.Width;
            int height = canvasSize.Height;

            g.Clear(Color.FromArgb(250, 250, 250));

            if (curIkou == null || curIkou.LList.Count == 0)
            {
                using (var font = new Font("Yu Gothic UI", 9F))
                using (var brush = new SolidBrush(Color.Gray))
                {
                    g.DrawString("選択された遺構の座標データがありません", font, brush, 15, 15);
                }
                return;
            }

            var (_, widthM, heightM, ux, uy, vx, vy, _) = GeometryMath.CalculateCropBox(curIkou.P1, curIkou.P2, curIkou.P3);

            int margin = 35;
            double scale = Math.Min((width - margin * 2) / widthM, (height - margin * 2) / heightM) * 0.70;

            PointF ToDetailPoint(double surveyX, double surveyY)
            {
                var (xLocalM, yLocalM) = GeometryMath.SurveyToFeatureLocalCenter(surveyX, surveyY, curIkou.P1, curIkou.P2, curIkou.P3);
                float px = (width / 2f) + (float)(xLocalM * scale);
                float py = (height / 2f) - (float)(yLocalM * scale);
                return new PointF(px, py);
            }

            float boxHalfW = (float)(widthM / 2.0 * scale);
            float boxHalfH = (float)(heightM / 2.0 * scale);
            RectangleF cropRect = new RectangleF((width / 2f) - boxHalfW, (height / 2f) - boxHalfH, boxHalfW * 2f, boxHalfH * 2f);

            Region oldDetailClip = g.Clip;
            g.SetClip(cropRect);

            var spline = new Xross_Spline();
            foreach (var line in curIkou.LList)
            {
                if (line.Pnts.Count == 0) continue;

                List<Point3D> renderPnts;
                if (line.Pnts.Count >= 3)
                {
                    renderPnts = (line.Flag == 1)
                        ? spline.Calc3DCloseCurvePoints(line.Pnts, 5)
                        : spline.Calc3DCurvePoints(line.Pnts, 5);
                }
                else
                {
                    renderPnts = line.Pnts;
                }

                var pts = renderPnts.Select(p => ToDetailPoint(p.X, p.Y)).ToArray();

                if (pts.Length > 1)
                {
                    Color col = chkColorByIkouFull
                        ? LayerManager.PaletteColors[(int)(curIkou.IID % LayerManager.PaletteColors.Length)]
                        : LayerManager.GetLayerColor(line.Layer);
                    using (var pen = new Pen(col, 2f))
                    {
                        g.DrawLines(pen, pts);
                    }
                }
            }

            g.Clip = oldDetailClip;

            using (var framePen = new Pen(Color.FromArgb(160, 180, 180, 190), 1.5f) { DashStyle = DashStyle.Dash })
            {
                g.DrawRectangle(framePen, cropRect.X, cropRect.Y, cropRect.Width, cropRect.Height);
            }

            foreach (var dm in curIkou.DmList)
            {
                PointF sp = ToDetailPoint(dm.Sp.X, dm.Sp.Y);
                PointF ep = ToDetailPoint(dm.Ep.X, dm.Ep.Y);
                PointF dp = ToDetailPoint(dm.Dp.X, dm.Dp.Y);

                double totalLenM = dm.DmpList.Count > 1 ? Math.Max(1e-6, dm.DmpList.Last().Len) : 1.0;
                double lenPx = Math.Sqrt(Math.Pow(ep.X - sp.X, 2) + Math.Pow(ep.Y - sp.Y, 2));
                double pixelsPerMeter = lenPx / totalLenM;

                RenderUnifiedSection(g, dm, sp, ep, dp, pixelsPerMeter);
            }

            if (chkShowDirection && curIkou.IsShowDirection == 1 && curDrawing != null)
            {
                double compLocalMx = curIkou.PDirection.X * (curDrawing.Scale / 1000.0);
                double compLocalMy = curIkou.PDirection.Y * (curDrawing.Scale / 1000.0);

                float compassPx = (width / 2f) + (float)(compLocalMx * scale);
                float compassPy = (height / 2f) - (float)(compLocalMy * scale);
                PointF compassCenter = new PointF(compassPx, compassPy);

                double diagM = Math.Sqrt(widthM * widthM + heightM * heightM);
                double diagPx = diagM * scale;
                float arrowLen = Math.Max(8f, (float)(diagPx / 10.0));

                float nX = compassCenter.X + arrowLen * (float)ux;
                float nY = compassCenter.Y - arrowLen * (float)uy;

                using (var arrowPen = new Pen(Color.FromArgb(56, 176, 0), 2.2f))
                using (var font = new Font("Yu Gothic UI", 9.5F, FontStyle.Bold))
                using (var brush = new SolidBrush(Color.FromArgb(56, 176, 0)))
                {
                    g.DrawLine(arrowPen, compassCenter.X, compassCenter.Y, nX, nY);
                    g.DrawString("N", font, brush, nX - 5f, nY - 16f);
                    g.FillEllipse(brush, compassCenter.X - 3f, compassCenter.Y - 3f, 6f, 6f);
                }
            }

            if (vc.IsPickingDirectionPosition)
            {
                PointF compassOriginPt = new PointF(width / 2f, height / 2f);
                if (curIkou != null && curDrawing != null)
                {
                    double compLocalMx = curIkou.PDirection.X * (curDrawing.Scale / 1000.0);
                    double compLocalMy = curIkou.PDirection.Y * (curDrawing.Scale / 1000.0);
                    float compassPx = (width / 2f) + (float)(compLocalMx * scale);
                    float compassPy = (height / 2f) - (float)(compLocalMy * scale);
                    compassOriginPt = new PointF(compassPx, compassPy);
                }

                using (var rubberPen = new Pen(Color.FromArgb(56, 176, 0), 2f) { DashStyle = DashStyle.Dash })
                using (var font = new Font("Yu Gothic UI", 9F, FontStyle.Bold))
                using (var brush = new SolidBrush(Color.FromArgb(56, 176, 0)))
                {
                    g.DrawLine(rubberPen, compassOriginPt, vc.DetailLastMousePos);
                    g.FillEllipse(brush, compassOriginPt.X - 4f, compassOriginPt.Y - 4f, 8f, 8f);
                    g.DrawEllipse(rubberPen, vc.DetailLastMousePos.X - 8f, vc.DetailLastMousePos.Y - 8f, 16f, 16f);
                    g.DrawString("方位配置目標 (右クリックでキャンセル)", font, brush, vc.DetailLastMousePos.X + 10f, vc.DetailLastMousePos.Y - 8f);
                }
            }
        }

        #endregion

        #region Helper Drawing Utilities

        public static void DrawRotatedString(Graphics g, string text, Font font, Brush brush, PointF pos, double angleDegrees, StringAlignment alignH = StringAlignment.Center, StringAlignment alignV = StringAlignment.Center)
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

        public static void RenderUnifiedSection(Graphics g, DanmenRec dm, PointF sp, PointF ep, PointF dp, double pixelsPerMeter)
        {
            double dx = ep.X - sp.X;
            double dy = ep.Y - sp.Y;
            double lenPx = Math.Max(1e-6, Math.Sqrt(dx * dx + dy * dy));

            double uX = dx / lenPx;
            double uY = dy / lenPx;

            double pdx = dp.X - sp.X;
            double pdy = dp.Y - sp.Y;
            double pdLen = Math.Max(1e-6, Math.Sqrt(pdx * pdx + pdy * pdy));
            double nX = pdx / pdLen;
            double nY = pdy / pdLen;

            double lineAngleDeg = Math.Atan2(dy, dx) * 180.0 / Math.PI;

            PointF dpEnd = new PointF((float)(dp.X + lenPx * uX), (float)(dp.Y + lenPx * uY));

            using (var cutLinePen = new Pen(Color.Red, 2f))
            using (var baseLinePen = new Pen(Color.Blue, 2f))
            using (var profilePen = new Pen(Color.Red, 2.2f))
            using (var font = new Font("Yu Gothic UI", 9.5F, FontStyle.Bold))
            using (var labelBrush = new SolidBrush(Color.FromArgb(255, 120, 160)))
            {
                // 1. Cut Line (Red)
                g.DrawLine(cutLinePen, sp, ep);

                string name = string.IsNullOrWhiteSpace(dm.Name) ? "A" : dm.Name.Trim();
                PointF spLabelPos = new PointF((float)(sp.X - uX * 14), (float)(sp.Y - uY * 14));
                PointF epLabelPos = new PointF((float)(ep.X + uX * 14), (float)(ep.Y + uY * 14));

                DrawRotatedString(g, name, font, labelBrush, spLabelPos, lineAngleDeg, StringAlignment.Far, StringAlignment.Center);
                DrawRotatedString(g, $"{name}'", font, labelBrush, epLabelPos, lineAngleDeg, StringAlignment.Near, StringAlignment.Center);

                // 2. Baseline (Blue)
                g.DrawLine(baseLinePen, dp, dpEnd);

                double baseH = dm.GetBaseH();

                PointF dpLabelPos = new PointF((float)(dp.X - uX * 14), (float)(dp.Y - uY * 14));
                PointF dpEndLabelPos = new PointF((float)(dpEnd.X + uX * 14), (float)(dpEnd.Y + uY * 14));

                DrawRotatedString(g, name, font, labelBrush, dpLabelPos, lineAngleDeg, StringAlignment.Far, StringAlignment.Center);
                DrawRotatedString(g, $"{name}'{baseH:0.000}m", font, labelBrush, dpEndLabelPos, lineAngleDeg, StringAlignment.Near, StringAlignment.Center);

                // 3. Profile Curve (Red)
                if (dm.DmpList.Count > 1)
                {
                    var profilePts = new List<PointF>();
                    foreach (var dmp in dm.DmpList)
                    {
                        double distPx = dmp.Len * pixelsPerMeter;
                        double depthM = baseH - dmp.H;
                        double depthPx = depthM * pixelsPerMeter;

                        float bx = (float)(dp.X + distPx * uX);
                        float by = (float)(dp.Y + distPx * uY);

                        float px = (float)(bx + nX * depthPx);
                        float py = (float)(by + nY * depthPx);

                        profilePts.Add(new PointF(px, py));
                    }

                    if (profilePts.Count > 1)
                    {
                        g.DrawLines(profilePen, profilePts.ToArray());
                    }
                }
            }
        }

        #endregion
    }
}
