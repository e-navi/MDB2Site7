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
            bool isDarkBackground = false,
            bool chkShowDrawingFrame = true)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;

            int width = canvasSize.Width;
            int height = canvasSize.Height;
            if (width <= 0 || height <= 0) return;

            g.Clear(isDarkBackground ? Color.FromArgb(20, 20, 25) : Color.White);

            // Fast single-pass bounding box calculation without string allocations
            double posXMin = double.MaxValue, posXMax = double.MinValue;
            double posYMin = double.MaxValue, posYMax = double.MinValue;

            if (db.MasterIkouLList != null)
            {
                foreach (var line in db.MasterIkouLList)
                {
                    var pts = SqliteDrawingManager.ParsePrecsText(line.Precs);
                    foreach (var pt in pts)
                    {
                        if (pt.Y < posXMin) posXMin = pt.Y;
                        if (pt.Y > posXMax) posXMax = pt.Y;
                        if (pt.X < posYMin) posYMin = pt.X;
                        if (pt.X > posYMax) posYMax = pt.X;
                    }
                }
            }

            foreach (var ik in db.MasterIkouList)
            {
                if (Math.Abs(ik.X) > 0.001 || Math.Abs(ik.Y) > 0.001)
                {
                    if (ik.Y < posXMin) posXMin = ik.Y;
                    if (ik.Y > posXMax) posXMax = ik.Y;
                    if (ik.X < posYMin) posYMin = ik.X;
                    if (ik.X > posYMax) posYMax = ik.X;
                }
            }
            foreach (var ib in db.MasterIbutuList)
            {
                if (Math.Abs(ib.X) > 0.001 || Math.Abs(ib.Y) > 0.001)
                {
                    if (ib.Y < posXMin) posXMin = ib.Y;
                    if (ib.Y > posXMax) posXMax = ib.Y;
                    if (ib.X < posYMin) posYMin = ib.X;
                    if (ib.X > posYMax) posYMax = ib.X;
                }
            }
            foreach (var k in db.MasterKikaiList)
            {
                if (Math.Abs(k.X) > 0.001 || Math.Abs(k.Y) > 0.001)
                {
                    if (k.Y < posXMin) posXMin = k.Y;
                    if (k.Y > posXMax) posXMax = k.Y;
                    if (k.X < posYMin) posYMin = k.X;
                    if (k.X > posYMax) posYMax = k.X;
                }
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

            vc.CustomSurveyToScreen = (sx, sy) => ToCanvasPoint(sx, sy);

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

                if (db.MasterIkouLList != null)
                {
                    foreach (var line in db.MasterIkouLList)
                {
                    if (isLayerVisible != null && !isLayerVisible(line.Layer)) continue;

                    var pts = SqliteDrawingManager.ParsePrecsText(line.Precs);
                    if (pts.Count == 0) continue;

                    bool isLayerCurve = LayerManager.IsLayerCurve(db, line.Layer);

                    PointF[] screenPts;
                    if (isLayerCurve && pts.Count >= 3)
                    {
                        var curvePts = (line.Mode == 1)
                            ? spline.Calc3DCloseCurvePoints(pts, 5)
                            : spline.Calc3DCurvePoints(pts, 5);
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
                        var (msx, msy) = vc.CanvasToSurveyCrop(vc.CropLastMousePos, canvasSize, db.MasterIkouList, db.MasterIkouLList ?? Enumerable.Empty<MasterIkouLModel>(), db.MasterIbutuList, db.MasterKikaiList);
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

            // 図枠の描画
            if (chkShowDrawingFrame && DrawingFrameService.Instance.IsVisible)
            {
                DrawingFrameService.Instance.DrawFrame(g, vc, canvasSize, isDarkBackground);
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

            g.Clear(Color.FromArgb(228, 232, 240));

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

            using (var paperBrush = new SolidBrush(Color.White))
            using (var paperShadowBrush = new SolidBrush(Color.FromArgb(40, 0, 0, 0)))
            using (var paperPen = new Pen(Color.FromArgb(170, 180, 195), 1.0f))
            {
                g.FillRectangle(paperShadowBrush, paperLeft + 4f, paperTop + 4f, paperW, paperH);
                g.FillRectangle(paperBrush, paperLeft, paperTop, paperW, paperH);
                g.DrawRectangle(paperPen, paperLeft, paperTop, paperW, paperH);
            }

            PointF PaperMmToCanvas(double mmX, double mmY)
            {
                float px = paperCenterX + (float)(mmX / pInfo.WidthMm * paperW);
                float py = paperCenterY - (float)(mmY / pInfo.HeightMm * paperH);
                return new PointF(px, py);
            }

            var sheetSettings = DrawingSheetSettings.Instance;
            double halfW = pInfo.WidthMm / 2.0;
            double halfH = pInfo.HeightMm / 2.0;
            double marginLeftMm = sheetSettings.MarginLeftMm;
            double marginOtherMm = sheetSettings.MarginOtherMm;

            // 1. 外枠 (Outer Frame) の描画
            PointF pFrameBL = PaperMmToCanvas(-halfW + marginLeftMm, -halfH + marginOtherMm);
            PointF pFrameTR = PaperMmToCanvas(halfW - marginOtherMm, halfH - marginOtherMm);
            float frameX = Math.Min(pFrameBL.X, pFrameTR.X);
            float frameY = Math.Min(pFrameBL.Y, pFrameTR.Y);
            float frameW = Math.Abs(pFrameTR.X - pFrameBL.X);
            float frameH = Math.Abs(pFrameBL.Y - pFrameTR.Y);

            using (var framePen = new Pen(Color.Black, 1.5f))
            {
                g.DrawRectangle(framePen, frameX, frameY, frameW, frameH);
            }

            // 2. 表題欄 (Title Block) の描画 (外枠右下に配置)
            if (sheetSettings.ShowTitleBlock)
            {
                double tbWidthMm = 70.0;
                double tbHeightMm = (sheetSettings.ShowAuthor || sheetSettings.ShowDate) ? 24.0 : 18.0;
                PointF ptTbBL = PaperMmToCanvas(halfW - marginOtherMm - tbWidthMm, -halfH + marginOtherMm);
                PointF ptTbTR = PaperMmToCanvas(halfW - marginOtherMm, -halfH + marginOtherMm + tbHeightMm);

                float tbX = Math.Min(ptTbBL.X, ptTbTR.X);
                float tbY = Math.Min(ptTbBL.Y, ptTbTR.Y);
                float tbW = Math.Abs(ptTbTR.X - ptTbBL.X);
                float tbH = Math.Abs(ptTbBL.Y - ptTbTR.Y);

                using (var tbPen = new Pen(Color.Black, 1.2f))
                using (var tbBgBrush = new SolidBrush(Color.White))
                using (var headerBrush = new SolidBrush(Color.FromArgb(242, 244, 248)))
                using (var textBrush = new SolidBrush(Color.Black))
                using (var titleFont = new Font("Yu Gothic UI", Math.Max(7.0f, (float)(2.8 * (tbH / tbHeightMm))), FontStyle.Bold))
                using (var subFont = new Font("Yu Gothic UI", Math.Max(6.0f, (float)(2.0 * (tbH / tbHeightMm))), FontStyle.Regular))
                {
                    g.FillRectangle(tbBgBrush, tbX, tbY, tbW, tbH);
                    g.DrawRectangle(tbPen, tbX, tbY, tbW, tbH);

                    // 行数計算
                    int rowCount = (sheetSettings.ShowAuthor || sheetSettings.ShowDate) ? 3 : 2;
                    float rowH = tbH / (float)rowCount;

                    // 上段 (図面名ヘッダー)
                    g.FillRectangle(headerBrush, tbX + 1f, tbY + 1f, tbW - 2f, rowH);
                    g.DrawLine(tbPen, tbX, tbY + rowH, tbX + tbW, tbY + rowH);
                    string dName = sheetSettings.ShowDrawingName ? curDrawing.Name : "";
                    g.DrawString($"図面名: {dName}", titleFont, textBrush, tbX + 4f, tbY + 2f);

                    // 中段 (縮尺 / 用紙)
                    g.DrawLine(tbPen, tbX, tbY + rowH * 2f, tbX + tbW, tbY + rowH * 2f);
                    float colW = tbW / 2f;
                    g.DrawLine(tbPen, tbX + colW, tbY + rowH, tbX + colW, tbY + rowH * 2f);
                    string scaleStr = sheetSettings.ShowScale ? $"1/{curDrawing.Scale}" : "-";
                    string paperStr = sheetSettings.ShowPaperSize ? pInfo.Name : "-";
                    g.DrawString($"縮尺: {scaleStr}", subFont, textBrush, tbX + 4f, tbY + rowH + 2f);
                    g.DrawString($"用紙: {paperStr}", subFont, textBrush, tbX + colW + 4f, tbY + rowH + 2f);

                    // 下段 (作成者 / 日付)
                    if (rowCount == 3)
                    {
                        g.DrawLine(tbPen, tbX + colW, tbY + rowH * 2f, tbX + colW, tbY + tbH);
                        string authStr = sheetSettings.ShowAuthor && !string.IsNullOrEmpty(sheetSettings.AuthorText) ? sheetSettings.AuthorText : "";
                        string dateStr = sheetSettings.ShowDate && !string.IsNullOrEmpty(sheetSettings.DateText) ? sheetSettings.DateText : "";
                        g.DrawString($"作成: {authStr}", subFont, textBrush, tbX + 4f, tbY + rowH * 2f + 2f);
                        g.DrawString($"日付: {dateStr}", subFont, textBrush, tbX + colW + 4f, tbY + rowH * 2f + 2f);
                    }
                }
            }

            // 3. スケールバー (Scale Bar) の描画
            if (sheetSettings.ShowScaleBar)
            {
                double targetBarMm = 50.0;
                double idealMeters = (targetBarMm / 1000.0) * curDrawing.Scale;
                double barMeters;
                if (idealMeters <= 1.5) barMeters = 1.0;
                else if (idealMeters <= 3.0) barMeters = 2.0;
                else if (idealMeters <= 7.5) barMeters = 5.0;
                else if (idealMeters <= 15.0) barMeters = 10.0;
                else if (idealMeters <= 35.0) barMeters = 20.0;
                else if (idealMeters <= 75.0) barMeters = 50.0;
                else if (idealMeters <= 150.0) barMeters = 100.0;
                else barMeters = Math.Ceiling(idealMeters / 50.0) * 50.0;

                double barMm = (barMeters / curDrawing.Scale) * 1000.0;

                double sbCenterXMm = 0.0;
                double sbCenterYMm = -halfH + marginOtherMm + 6.5;

                string pos = sheetSettings.ScaleBarPos;
                if (pos == "左下")
                {
                    sbCenterXMm = -halfW + marginLeftMm + (barMm / 2.0) + 5.0;
                    sbCenterYMm = -halfH + marginOtherMm + 6.5;
                }
                else if (pos == "右下")
                {
                    double rightOffset = sheetSettings.ShowTitleBlock ? 75.0 : 5.0;
                    sbCenterXMm = halfW - marginOtherMm - rightOffset - (barMm / 2.0);
                    sbCenterYMm = -halfH + marginOtherMm + 6.5;
                }
                else if (pos == "左上")
                {
                    sbCenterXMm = -halfW + marginLeftMm + (barMm / 2.0) + 5.0;
                    sbCenterYMm = halfH - marginOtherMm - 10.0;
                }
                else if (pos == "右上")
                {
                    sbCenterXMm = halfW - marginOtherMm - (barMm / 2.0) - 5.0;
                    sbCenterYMm = halfH - marginOtherMm - 10.0;
                }

                PointF sbStart = PaperMmToCanvas(sbCenterXMm - barMm / 2.0, sbCenterYMm);
                PointF sbEnd = PaperMmToCanvas(sbCenterXMm + barMm / 2.0, sbCenterYMm);
                PointF sbMid = PaperMmToCanvas(sbCenterXMm, sbCenterYMm);

                float sbLeftX = sbStart.X;
                float sbRightX = sbEnd.X;
                float sbMidX = sbMid.X;
                float sbLineY = sbStart.Y;
                float tickH = Math.Max(3.0f, (float)(2.5 * (paperH / pInfo.HeightMm)));

                using (var sbPen = new Pen(Color.Black, 1.2f))
                using (var sbBrush = new SolidBrush(Color.Black))
                using (var sbFont = new Font("Yu Gothic UI", Math.Max(6.0f, (float)(2.2 * (paperH / pInfo.HeightMm))), FontStyle.Bold))
                {
                    // 基準水平線
                    g.DrawLine(sbPen, sbLeftX, sbLineY, sbRightX, sbLineY);

                    // 左端 (0)、中央、右端 (barMeters) 目盛
                    g.DrawLine(sbPen, sbLeftX, sbLineY, sbLeftX, sbLineY - tickH);
                    g.DrawLine(sbPen, sbMidX, sbLineY, sbMidX, sbLineY - tickH);
                    g.DrawLine(sbPen, sbRightX, sbLineY, sbRightX, sbLineY - tickH);

                    // 左半分の5分割中間目盛
                    for (int i = 1; i <= 4; i++)
                    {
                        float subX = sbLeftX + (sbMidX - sbLeftX) * (i / 5f);
                        g.DrawLine(sbPen, subX, sbLineY, subX, sbLineY - (tickH * 0.5f));
                    }

                    // 上部数値テキスト
                    string l0 = "0";
                    string lEnd = (barMeters % 1 == 0) ? $"{barMeters:0}m" : $"{barMeters:0.#}m";
                    var sz0 = g.MeasureString(l0, sbFont);
                    var szEnd = g.MeasureString(lEnd, sbFont);
                    g.DrawString(l0, sbFont, sbBrush, sbLeftX - (sz0.Width / 2f), sbLineY - tickH - sz0.Height);
                    g.DrawString(lEnd, sbFont, sbBrush, sbRightX - (szEnd.Width / 2f), sbLineY - tickH - szEnd.Height);

                    // 下部縮尺表記
                    string scaleText = $"(S=1:{curDrawing.Scale})";
                    var szScale = g.MeasureString(scaleText, sbFont);
                    g.DrawString(scaleText, sbFont, sbBrush, sbMidX - (szScale.Width / 2f), sbLineY + 2f);
                }
            }

            var currentIkous = db.DrawingIkousList.Where(di => di.ZID == curDrawing.ZID).ToList();
            var spline = new Xross_Spline();

            foreach (var ikou in currentIkous)
            {
                if (ikou.LList.Count == 0) continue;

                var (_, widthM, heightM, ux, uy, _, _, _) = GeometryMath.CalculateCropBox(ikou.P1, ikou.P2, ikou.P3);
                double scaleFactorMm = 1000.0 / curDrawing.Scale;

                Point3D effPP = GeometryMath.CalculateEffectivePaperPosition(ikou, currentIkous, curDrawing.Scale);

                PointF ptBL = PaperMmToCanvas(effPP.X - (widthM / 2.0) * scaleFactorMm, effPP.Y - (heightM / 2.0) * scaleFactorMm);
                PointF ptBR = PaperMmToCanvas(effPP.X + (widthM / 2.0) * scaleFactorMm, effPP.Y - (heightM / 2.0) * scaleFactorMm);
                PointF ptTR = PaperMmToCanvas(effPP.X + (widthM / 2.0) * scaleFactorMm, effPP.Y + (heightM / 2.0) * scaleFactorMm);
                PointF ptTL = PaperMmToCanvas(effPP.X - (widthM / 2.0) * scaleFactorMm, effPP.Y + (heightM / 2.0) * scaleFactorMm);

                using (var clipPath = new GraphicsPath())
                {
                    clipPath.AddPolygon(new[] { ptBL, ptBR, ptTR, ptTL });
                    Region oldClip = g.Clip;
                    g.SetClip(clipPath);

                    foreach (var line in ikou.LList)
                    {
                        if (line.Pnts.Count == 0) continue;

                        bool isLayerCurve = LayerManager.IsLayerCurve(db, line.Layer);
                        bool shouldDrawCurve = isLayerCurve && line.Pnts.Count >= 3;

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
                            PointF paperPt = GeometryMath.SurveyToPaperPoint(pt.X, pt.Y, ikou.P1, ikou.P2, ikou.P3, effPP, curDrawing.Scale);
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

                // 各遺構図の方位記号 (遺構図面設定の種類・寸法を適用)
                if (sheetSettings.ShowNorthArrow && chkShowDirectionPaper && ikou.IsShowDirection == 1)
                {
                    PointF compassCenter = PaperMmToCanvas(effPP.X + ikou.PDirection.X, effPP.Y + ikou.PDirection.Y);

                    double nSizeMm = sheetSettings.NorthArrowSizeMm;
                    float renderLen = (float)(nSizeMm / pInfo.HeightMm * paperH);
                    float renderWidth = renderLen * 0.45f;

                    // 画面上での北方向 (dx, dy) = (ux, -uy) に対する時計回り角度 (度)
                    double angleRad = Math.Atan2(ux, uy);
                    float angleDeg = (float)(angleRad * 180.0 / Math.PI);

                    DrawNorthArrowCore(g, compassCenter, angleDeg, renderLen, renderWidth, sheetSettings.NorthArrowType, false, 1.2f, Math.Max(7f, renderLen * 0.45f));
                }

                if (chkShowDanmenPaper)
                {
                    foreach (var dm in ikou.DmList)
                    {
                        PointF spPt = GeometryMath.SurveyToPaperPoint(dm.Sp.X, dm.Sp.Y, ikou.P1, ikou.P2, ikou.P3, effPP, curDrawing.Scale);
                        PointF epPt = GeometryMath.SurveyToPaperPoint(dm.Ep.X, dm.Ep.Y, ikou.P1, ikou.P2, ikou.P3, effPP, curDrawing.Scale);
                        PointF dpPt = GeometryMath.SurveyToPaperPoint(dm.Dp.X, dm.Dp.Y, ikou.P1, ikou.P2, ikou.P3, effPP, curDrawing.Scale);

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
                Point3D effSelPP = GeometryMath.CalculateEffectivePaperPosition(curSelectedIkou, currentIkous, curDrawing.Scale);
                PointF originPt = PaperMmToCanvas(effSelPP.X, effSelPP.Y);
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
            DrawingDbManager db,
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

                bool isLayerCurve = LayerManager.IsLayerCurve(db, line.Layer);
                List<Point3D> renderPnts;
                if (isLayerCurve && line.Pnts.Count >= 3)
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
                    using (var pen = new Pen(col, 1.5f))
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

            var sheetSettings = DrawingSheetSettings.Instance;
            if (sheetSettings.ShowNorthArrow && chkShowDirection && curIkou.IsShowDirection == 1 && curDrawing != null)
            {
                double compLocalMx = curIkou.PDirection.X * (curDrawing.Scale / 1000.0);
                double compLocalMy = curIkou.PDirection.Y * (curDrawing.Scale / 1000.0);

                float compassPx = (width / 2f) + (float)(compLocalMx * scale);
                float compassPy = (height / 2f) - (float)(compLocalMy * scale);
                PointF compassCenter = new PointF(compassPx, compassPy);

                double nSizeMm = sheetSettings.NorthArrowSizeMm;
                double nSizeM = nSizeMm * (curDrawing.Scale / 1000.0);
                float renderLen = Math.Max(10f, (float)(nSizeM * scale));
                float renderWidth = renderLen * 0.45f;

                double angleRad = Math.Atan2(ux, uy);
                float angleDeg = (float)(angleRad * 180.0 / Math.PI);

                DrawNorthArrowCore(g, compassCenter, angleDeg, renderLen, renderWidth, sheetSettings.NorthArrowType, false, 1.5f, Math.Max(7f, renderLen * 0.45f));
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

            using (var cutLinePen = new Pen(Color.Red, 1.5f))
            using (var baseLinePen = new Pen(Color.Blue, 1.5f))
            using (var profilePen = new Pen(Color.Red, 1.5f))
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

        public static void DrawNorthArrowCore(Graphics g, PointF anchor, float needleAngleDeg, float length, float width, string style, bool isDarkBackground, float penWidth, float fontPt)
        {
            Color fg = isDarkBackground ? Color.White : Color.Black;
            Color bg = isDarkBackground ? Color.FromArgb(120, 130, 150) : Color.White;

            var state = g.Save();
            g.TranslateTransform(anchor.X, anchor.Y);
            g.RotateTransform(needleAngleDeg);

            using (var blackBrush = new SolidBrush(fg))
            using (var whiteBrush = new SolidBrush(bg))
            using (var outlinePen = new Pen(fg, penWidth))
            using (var font = new Font("Yu Gothic UI", fontPt, FontStyle.Bold, GraphicsUnit.Pixel))
            {
                string nStr = "N";
                var nSz = g.MeasureString(nStr, font);

                if (style == "シンプル")
                {
                    PointF tip = new PointF(0, -length);
                    PointF tail = new PointF(0, length * 0.35f);
                    PointF headLeft = new PointF(-width * 0.7f, -length * 0.55f);
                    PointF headRight = new PointF(width * 0.7f, -length * 0.55f);
                    PointF headCenter = new PointF(0, -length * 0.65f);

                    g.DrawLine(outlinePen, tip, tail);
                    g.FillPolygon(blackBrush, new PointF[] { tip, headLeft, headCenter });
                    g.FillPolygon(whiteBrush, new PointF[] { tip, headRight, headCenter });
                    g.DrawPolygon(outlinePen, new PointF[] { tip, headLeft, headCenter, headRight });

                    g.DrawString(nStr, font, blackBrush, -nSz.Width / 2f, -length - nSz.Height - 1f);
                }
                else if (style == "円形コンパス")
                {
                    float radius = length * 0.45f;
                    g.DrawEllipse(outlinePen, -radius, -radius, radius * 2f, radius * 2f);

                    g.DrawLine(outlinePen, -radius, 0, radius, 0);
                    g.DrawLine(outlinePen, 0, 0, 0, radius);

                    PointF tip = new PointF(0, -length);
                    PointF leftWing = new PointF(-width * 0.6f, 0);
                    PointF rightWing = new PointF(width * 0.6f, 0);

                    g.FillPolygon(blackBrush, new PointF[] { tip, leftWing, new PointF(0, 0) });
                    g.FillPolygon(whiteBrush, new PointF[] { tip, rightWing, new PointF(0, 0) });
                    g.DrawPolygon(outlinePen, new PointF[] { tip, leftWing, new PointF(0, 0), rightWing });

                    g.DrawString(nStr, font, blackBrush, -nSz.Width / 2f, -length - nSz.Height - 1f);
                }
                else if (style == "モダン")
                {
                    PointF tip = new PointF(0, -length);
                    PointF tail = new PointF(0, length * 0.15f);
                    PointF leftWing = new PointF(-width * 0.9f, -length * 0.35f);
                    PointF rightWing = new PointF(width * 0.9f, -length * 0.35f);

                    g.FillPolygon(blackBrush, new PointF[] { tip, leftWing, tail });
                    g.FillPolygon(whiteBrush, new PointF[] { tip, rightWing, tail });
                    g.DrawPolygon(outlinePen, new PointF[] { tip, leftWing, tail, rightWing });

                    g.DrawString(nStr, font, blackBrush, -nSz.Width / 2f, -length - nSz.Height - 1f);
                }
                else // "標準矢印" (デフォルト)
                {
                    PointF tip = new PointF(0, -length);
                    PointF tail = new PointF(0, length * 0.35f);
                    PointF leftWing = new PointF(-width, -length * 0.1f);
                    PointF rightWing = new PointF(width, -length * 0.1f);

                    g.FillPolygon(blackBrush, new PointF[] { tip, leftWing, tail });
                    g.FillPolygon(whiteBrush, new PointF[] { tip, rightWing, tail });
                    g.DrawPolygon(outlinePen, new PointF[] { tip, leftWing, tail, rightWing });

                    g.DrawString(nStr, font, blackBrush, -nSz.Width / 2f, -length - nSz.Height - 1f);
                }
            }

            g.Restore(state);
        }

        #endregion
    }
}
