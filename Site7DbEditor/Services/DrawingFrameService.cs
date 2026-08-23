using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Site7DbEditor.Services
{
    public class DrawingFrameService
    {
        private static DrawingFrameService? _instance;
        public static DrawingFrameService Instance => _instance ??= new DrawingFrameService();

        // 状態・パラメータ
        public bool IsVisible { get; set; } = true;
        public string PaperSizeName { get; set; } = "A3"; // A4, A3, A2, A1, A0
        public bool IsLandscape { get; set; } = true;     // 横向き
        public double Scale { get; set; } = 200.0;        // 縮尺 (例: 200 = 1/200)
        public double CenterX { get; set; } = 0.0;        // 測量X (北方向)
        public double CenterY { get; set; } = 0.0;        // 測量Y (東方向)
        public double RotationAngleDeg { get; set; } = 0.0; // 回転角度（度単位、反時計回り）

        // トンボ・格子設定
        public bool ShowTombo { get; set; } = true;         // トンボ(+)表示
        public bool ShowGridLines { get; set; } = false;    // 格子線表示
        public bool IsPitchAuto { get; set; } = true;       // ピッチ自動（用紙100mm基準）
        public double PitchMeters { get; set; } = 20.0;     // 手動ピッチ（実寸m）
        public bool ShowBorderCoords { get; set; } = true;  // 外枠・内枠間の座標値表示

        // 付加要素設定
        public bool ShowNorthArrow { get; set; } = true;    // 方位記号表示
        public double NorthArrowSizeMm { get; set; } = 15.0; // 方位記号サイズ（mm）
        public string NorthArrowPosition { get; set; } = "右上"; // 右上, 左上, 右下, 左下, カスタム
        public double NorthArrowCustomSurveyX { get; set; } = 0.0; // カスタム指定時の測量X
        public double NorthArrowCustomSurveyY { get; set; } = 0.0; // カスタム指定時の測量Y
        public bool HasCustomNorthArrowPos { get; set; } = false;

        public bool ShowScaleBar { get; set; } = true;      // スケールバー表示
        public string ScaleBarPosition { get; set; } = "中下";   // 中下, 右下

        // 枠余白・間隔（用紙上のミリメートル単位）
        public double MarginLeftMm { get; set; } = 20.0;       // 外枠余白: 左 (mm)
        public double MarginOtherMm { get; set; } = 10.0;      // 外枠余白: 左以外 [上・右・下] (mm)
        public double OuterInnerSpacingMm { get; set; } = 10.0; // 外枠と内枠の間隔 (mm)

        /// <summary>
        /// 現在の有効なピッチ（実空間メートル）を取得
        /// </summary>
        public double GetEffectivePitchMeters()
        {
            if (IsPitchAuto)
            {
                // 用紙100mmピッチ基準 (1/200 -> 20m, 1/500 -> 50m, 1/100 -> 10m, 1/50 -> 5m)
                double autoPitch = (100.0 / 1000.0) * Scale;
                return Math.Max(1.0, autoPitch);
            }
            return Math.Max(0.1, PitchMeters);
        }

        /// <summary>
        /// 用紙サイズ（mm）を取得 (幅, 高さ) ※Landscape/Portrait考慮
        /// </summary>
        public (double widthMm, double heightMm) GetPaperDimensionsMm()
        {
            double w = 420.0, h = 297.0; // Default A3
            switch (PaperSizeName.ToUpperInvariant())
            {
                case "A4": w = 297.0; h = 210.0; break;
                case "A3": w = 420.0; h = 297.0; break;
                case "A2": w = 594.0; h = 420.0; break;
                case "A1": w = 841.0; h = 594.0; break;
                case "A0": w = 1189.0; h = 841.0; break;
            }

            if (!IsLandscape)
            {
                var temp = w;
                w = h;
                h = temp;
            }
            return (w, h);
        }

        /// <summary>
        /// 実空間（メートル）での図枠（用紙外周）の幅・高さを取得
        /// </summary>
        public (double widthM, double heightM) GetPaperDimensionsMeters()
        {
            var (wMm, hMm) = GetPaperDimensionsMm();
            double wM = (wMm / 1000.0) * Scale;
            double hM = (hMm / 1000.0) * Scale;
            return (wM, hM);
        }

        /// <summary>
        /// 実空間（メートル）での外枠の幅・高さを取得
        /// </summary>
        public (double widthM, double heightM, double offsetEastM, double offsetNorthM) GetOuterFrameDimensionsMeters()
        {
            var (wMm, hMm) = GetPaperDimensionsMm();
            double outerWMm = Math.Max(10.0, wMm - (MarginLeftMm + MarginOtherMm));
            double outerHMm = Math.Max(10.0, hMm - (MarginOtherMm * 2.0));

            double outerWM = (outerWMm / 1000.0) * Scale;
            double outerHM = (outerHMm / 1000.0) * Scale;

            // マージン非対称による中心からのズレ（ローカル座標）
            double offsetEastMm = (MarginLeftMm - MarginOtherMm) / 2.0;
            double offsetNorthMm = 0.0;

            double offsetEastM = (offsetEastMm / 1000.0) * Scale;
            double offsetNorthM = (offsetNorthMm / 1000.0) * Scale;

            return (outerWM, outerHM, offsetEastM, offsetNorthM);
        }

        /// <summary>
        /// 実空間（メートル）での内枠の幅・高さを取得
        /// </summary>
        public (double widthM, double heightM, double offsetEastM, double offsetNorthM) GetInnerFrameDimensionsMeters()
        {
            var (outerWM, outerHM, offsetEastM, offsetNorthM) = GetOuterFrameDimensionsMeters();
            double spacingM = (OuterInnerSpacingMm / 1000.0) * Scale;

            double innerWM = Math.Max(1.0, outerWM - spacingM * 2.0);
            double innerHM = Math.Max(1.0, outerHM - spacingM * 2.0);

            return (innerWM, innerHM, offsetEastM, offsetNorthM);
        }

        /// <summary>
        /// 図枠（用紙外形）の4頂点（測量座標: 北X, 東Y）を取得
        /// </summary>
        public (double surveyX, double surveyY)[] GetPaperCornersSurvey(double cx, double cy, double angleDeg)
        {
            var (wM, hM) = GetPaperDimensionsMeters();
            double halfW = wM / 2.0;
            double halfH = hM / 2.0;

            (double u, double v)[] localCorners = new (double, double)[] {
                (-halfW, -halfH),
                ( halfW, -halfH),
                ( halfW,  halfH),
                (-halfW,  halfH)
            };

            return TransformLocalToSurvey(localCorners, cx, cy, angleDeg);
        }

        public (double surveyX, double surveyY)[] GetOuterCornersSurvey()
        {
            return GetOuterCornersSurvey(CenterX, CenterY, RotationAngleDeg);
        }

        public (double surveyX, double surveyY)[] GetOuterCornersSurvey(double cx, double cy, double angleDeg)
        {
            var (outerWM, outerHM, offsetEastM, offsetNorthM) = GetOuterFrameDimensionsMeters();
            double halfW = outerWM / 2.0;
            double halfH = outerHM / 2.0;

            (double u, double v)[] localCorners = new (double, double)[] {
                (offsetEastM - halfW, offsetNorthM - halfH),
                (offsetEastM + halfW, offsetNorthM - halfH),
                (offsetEastM + halfW, offsetNorthM + halfH),
                (offsetEastM - halfW, offsetNorthM + halfH)
            };

            return TransformLocalToSurvey(localCorners, cx, cy, angleDeg);
        }

        public (double surveyX, double surveyY)[] GetInnerCornersSurvey()
        {
            return GetInnerCornersSurvey(CenterX, CenterY, RotationAngleDeg);
        }

        public (double surveyX, double surveyY)[] GetInnerCornersSurvey(double cx, double cy, double angleDeg)
        {
            var (innerWM, innerHM, offsetEastM, offsetNorthM) = GetInnerFrameDimensionsMeters();
            double halfW = innerWM / 2.0;
            double halfH = innerHM / 2.0;

            (double u, double v)[] localCorners = new (double, double)[] {
                (offsetEastM - halfW, offsetNorthM - halfH),
                (offsetEastM + halfW, offsetNorthM - halfH),
                (offsetEastM + halfW, offsetNorthM + halfH),
                (offsetEastM - halfW, offsetNorthM + halfH)
            };

            return TransformLocalToSurvey(localCorners, cx, cy, angleDeg);
        }

        /// <summary>
        /// ローカル座標系(u: 東西, v: 南北)から測量座標系(北X, 東Y)への回転・平行移動
        /// </summary>
        private (double surveyX, double surveyY)[] TransformLocalToSurvey((double u, double v)[] locals, double cx, double cy, double angleDeg)
        {
            double rad = angleDeg * Math.PI / 180.0;
            double cos = Math.Cos(rad);
            double sin = Math.Sin(rad);

            var result = new (double, double)[locals.Length];
            for (int i = 0; i < locals.Length; i++)
            {
                double u = locals[i].u;
                double v = locals[i].v;

                // u = 東方向(Y軸), v = 北方向(X軸)
                double rotatedNorth = v * cos - u * sin;
                double rotatedEast  = v * sin + u * cos;

                result[i] = (cx + rotatedNorth, cy + rotatedEast);
            }
            return result;
        }

        /// <summary>
        /// ラバーバンド用の図枠プレビュー描画（中心移動・回転指定時）
        /// </summary>
        public void DrawRubberBandFrame(Graphics g, EditorMapViewController vc, Size canvasSize, double cx, double cy, double angleDeg, Point mouseScreenPos, bool isRotating)
        {
            if (canvasSize.Width <= 0 || canvasSize.Height <= 0) return;

            var paperCorners = GetPaperCornersSurvey(cx, cy, angleDeg);
            var outerCorners = GetOuterCornersSurvey(cx, cy, angleDeg);
            var innerCorners = GetInnerCornersSurvey(cx, cy, angleDeg);

            PointF[] paperScreen = ToScreenPoints(vc, paperCorners, canvasSize);
            PointF[] outerScreen = ToScreenPoints(vc, outerCorners, canvasSize);
            PointF[] innerScreen = ToScreenPoints(vc, innerCorners, canvasSize);

            using (var paperPen = new Pen(Color.FromArgb(180, 200, 200, 200), 1.2f) { DashStyle = DashStyle.Dot })
            using (var outerPen = new Pen(Color.FromArgb(255, 230, 0), 1.6f) { DashStyle = DashStyle.Dash })
            using (var thickPen = new Pen(Color.FromArgb(255, 230, 0), 3.6f) { DashStyle = DashStyle.Dash })
            using (var innerPen = new Pen(Color.FromArgb(0, 225, 255), 1.4f) { DashStyle = DashStyle.Dot })
            using (var centerPen = new Pen(Color.FromArgb(255, 100, 100), 1.8f))
            {
                // 1. 図枠（用紙外周）
                g.DrawPolygon(paperPen, paperScreen);

                // 2. 外枠
                g.DrawPolygon(outerPen, outerScreen);
                g.DrawLine(thickPen, outerScreen[0], outerScreen[1]); // 下辺
                g.DrawLine(thickPen, outerScreen[1], outerScreen[2]); // 右辺

                // 3. 内枠
                g.DrawPolygon(innerPen, innerScreen);

                // 中心点
                PointF centerScreen = vc.ToCanvasPoint(cx, cy, canvasSize);
                g.DrawLine(centerPen, centerScreen.X - 10f, centerScreen.Y, centerScreen.X + 10f, centerScreen.Y);
                g.DrawLine(centerPen, centerScreen.X, centerScreen.Y - 10f, centerScreen.X, centerScreen.Y + 10f);
                g.DrawEllipse(centerPen, centerScreen.X - 5f, centerScreen.Y - 5f, 10f, 10f);

                if (isRotating)
                {
                    using (var rayPen = new Pen(Color.FromArgb(255, 80, 80), 2f) { DashStyle = DashStyle.Dash })
                    {
                        g.DrawLine(rayPen, centerScreen, mouseScreenPos);
                        g.FillEllipse(Brushes.Red, mouseScreenPos.X - 4f, mouseScreenPos.Y - 4f, 8f, 8f);
                    }
                }
            }
        }

        /// <summary>
        /// マップキャンバスに図枠を描画
        /// </summary>
        public void DrawFrame(Graphics g, EditorMapViewController vc, Size canvasSize, bool isDarkBackground)
        {
            if (!IsVisible) return;
            if (canvasSize.Width <= 0 || canvasSize.Height <= 0) return;

            var paperCorners = GetPaperCornersSurvey(CenterX, CenterY, RotationAngleDeg);
            var outerCorners = GetOuterCornersSurvey();
            var innerCorners = GetInnerCornersSurvey();

            PointF[] paperScreen = ToScreenPoints(vc, paperCorners, canvasSize);
            PointF[] outerScreen = ToScreenPoints(vc, outerCorners, canvasSize);
            PointF[] innerScreen = ToScreenPoints(vc, innerCorners, canvasSize);

            // 配色ペン・ブラシ
            Color paperColor = isDarkBackground ? Color.FromArgb(120, 130, 150) : Color.FromArgb(160, 160, 170);
            Color outerColor = isDarkBackground ? Color.FromArgb(240, 240, 245) : Color.FromArgb(20, 20, 25);
            Color innerColor = isDarkBackground ? Color.FromArgb(0, 210, 255) : Color.FromArgb(0, 130, 210);
            Color centerColor = isDarkBackground ? Color.FromArgb(255, 180, 0) : Color.FromArgb(220, 100, 0);
            Color tomboColor = isDarkBackground ? Color.FromArgb(200, 255, 100, 100) : Color.FromArgb(220, 180, 20, 20);
            Color coordColor = isDarkBackground ? Color.FromArgb(220, 220, 230) : Color.FromArgb(40, 40, 50);

            // 1. 図枠（用紙外形）の描画（細線）
            using (var paperPen = new Pen(paperColor, 1.0f) { DashStyle = DashStyle.Dash })
            {
                g.DrawPolygon(paperPen, paperScreen);
            }

            // 2. 外枠の描画（通常実線、下辺と右辺は太線で用紙の向きを明示）
            using (var outerPen = new Pen(outerColor, 1.5f))
            using (var thickPen = new Pen(outerColor, 3.8f))
            {
                g.DrawPolygon(outerPen, outerScreen);
                // 下辺 (0:左下 -> 1:右下) と 右辺 (1:右下 -> 2:右上) を太線で強調
                g.DrawLine(thickPen, outerScreen[0], outerScreen[1]);
                g.DrawLine(thickPen, outerScreen[1], outerScreen[2]);
            }

            // 3. 内枠の描画 (作図範囲: シアン実線)
            using (var innerPen = new Pen(innerColor, 1.4f))
            {
                g.DrawPolygon(innerPen, innerScreen);
            }

            // 4. トンボ (+) & 格子線 & 外枠・内枠間座標値の描画
            DrawTomboAndCoordinates(g, vc, canvasSize, innerCorners, isDarkBackground, tomboColor, coordColor);

            // 5. 中心マーク（十字線）
            PointF centerScreen = vc.ToCanvasPoint(CenterX, CenterY, canvasSize);
            using (var centerPen = new Pen(centerColor, 1.5f))
            {
                g.DrawLine(centerPen, centerScreen.X - 8f, centerScreen.Y, centerScreen.X + 8f, centerScreen.Y);
                g.DrawLine(centerPen, centerScreen.X, centerScreen.Y - 8f, centerScreen.X, centerScreen.Y + 8f);
                g.DrawEllipse(centerPen, centerScreen.X - 4f, centerScreen.Y - 4f, 8f, 8f);
            }

            // 6. 方位記号 (North Arrow) の描画
            if (ShowNorthArrow)
            {
                DrawNorthArrow(g, vc, canvasSize, innerScreen, isDarkBackground);
            }

            // 7. スケールバー (Scale Bar) の描画
            if (ShowScaleBar)
            {
                DrawScaleBar(g, innerScreen, isDarkBackground);
            }

            // 8. 図枠情報ラベル（図枠の左上外側に表示）
            PointF infoPos = paperScreen[3]; // 左上
            double effectivePitch = GetEffectivePitchMeters();
            string infoText = $"全図枠 [{PaperSizeName} {(IsLandscape ? "横" : "縦")} 1/{Scale:0} ({RotationAngleDeg:0.0}°)] ピッチ:{effectivePitch:0.#}m";
            using (var infoFont = new Font("Yu Gothic UI", 8.5F, FontStyle.Bold))
            using (var infoBgBrush = new SolidBrush(Color.FromArgb(180, 20, 20, 25)))
            using (var infoTextBrush = new SolidBrush(outerColor))
            {
                var sz = g.MeasureString(infoText, infoFont);
                g.FillRectangle(infoBgBrush, infoPos.X, infoPos.Y - sz.Height - 4f, sz.Width + 8f, sz.Height + 2f);
                g.DrawString(infoText, infoFont, infoTextBrush, infoPos.X + 4f, infoPos.Y - sz.Height - 3f);
            }
        }

        private PointF[] ToScreenPoints(EditorMapViewController vc, (double surveyX, double surveyY)[] pts, Size canvasSize)
        {
            var result = new PointF[pts.Length];
            for (int i = 0; i < pts.Length; i++)
            {
                result[i] = vc.ToCanvasPoint(pts[i].surveyX, pts[i].surveyY, canvasSize);
            }
            return result;
        }

        /// <summary>
        /// トンボ(+)および外枠・内枠間の座標値（X=..., Y=...）を描画
        /// </summary>
        private void DrawTomboAndCoordinates(Graphics g, EditorMapViewController vc, Size canvasSize, (double surveyX, double surveyY)[] innerCorners, bool isDarkBackground, Color tomboColor, Color coordColor)
        {
            if (!ShowTombo && !ShowGridLines && !ShowBorderCoords) return;

            double pitch = GetEffectivePitchMeters();
            if (pitch <= 0.01) return;

            // 内枠の測量座標範囲
            double minSurX = double.MaxValue, maxSurX = double.MinValue;
            double minSurY = double.MaxValue, maxSurY = double.MinValue;
            for (int i = 0; i < innerCorners.Length; i++)
            {
                minSurX = Math.Min(minSurX, innerCorners[i].surveyX);
                maxSurX = Math.Max(maxSurX, innerCorners[i].surveyX);
                minSurY = Math.Min(minSurY, innerCorners[i].surveyY);
                maxSurY = Math.Max(maxSurY, innerCorners[i].surveyY);
            }

            double startSurX = Math.Floor(minSurX / pitch) * pitch;
            double endSurX = Math.Ceiling(maxSurX / pitch) * pitch;
            double startSurY = Math.Floor(minSurY / pitch) * pitch;
            double endSurY = Math.Ceiling(maxSurY / pitch) * pitch;

            // 内枠のローカル座標境界
            var (innerWM, innerHM, offsetEastM, offsetNorthM) = GetInnerFrameDimensionsMeters();
            double halfW = innerWM / 2.0;
            double halfH = innerHM / 2.0;
            double uMin = offsetEastM - halfW;
            double uMax = offsetEastM + halfW;
            double vMin = offsetNorthM - halfH;
            double vMax = offsetNorthM + halfH;

            double radSurX = RotationAngleDeg * Math.PI / 180.0;
            double cosRot = Math.Cos(radSurX);
            double sinRot = Math.Sin(radSurX);

            using (var tomboPen = new Pen(tomboColor, 1.2f))
            using (var gridPen = new Pen(Color.FromArgb(60, tomboColor), 1f) { DashStyle = DashStyle.Dash })
            using (var coordFont = new Font("Yu Gothic UI", 7.5F, FontStyle.Bold))
            using (var coordBrush = new SolidBrush(coordColor))
            using (var coordBgBrush = new SolidBrush(isDarkBackground ? Color.FromArgb(160, 25, 25, 35) : Color.FromArgb(180, 255, 255, 255)))
            {
                // A. トンボ (+) と 格子線の描画
                for (double sx = startSurX; sx <= endSurX + 0.001; sx += pitch)
                {
                    for (double sy = startSurY; sy <= endSurY + 0.001; sy += pitch)
                    {
                        // 測量座標 -> ローカル(u, v)
                        double dNorth = sx - CenterX;
                        double dEast = sy - CenterY;
                        double u = dNorth * (-sinRot) + dEast * cosRot;
                        double v = dNorth * cosRot + dEast * sinRot;

                        if (u >= uMin - 0.05 && u <= uMax + 0.05 && v >= vMin - 0.05 && v <= vMax + 0.05)
                        {
                            PointF pt = vc.ToCanvasPoint(sx, sy, canvasSize);

                            if (ShowTombo)
                            {
                                float arm = 5.5f;
                                PointF ptN = vc.ToCanvasPoint(sx + (pitch * 0.05), sy, canvasSize);
                                PointF ptE = vc.ToCanvasPoint(sx, sy + (pitch * 0.05), canvasSize);

                                float dirNx = ptN.X - pt.X;
                                float dirNy = ptN.Y - pt.Y;
                                float lenN = (float)Math.Sqrt(dirNx * dirNx + dirNy * dirNy);
                                if (lenN > 0.001f) { dirNx = dirNx / lenN * arm; dirNy = dirNy / lenN * arm; }

                                float dirEx = ptE.X - pt.X;
                                float dirEy = ptE.Y - pt.Y;
                                float lenE = (float)Math.Sqrt(dirEx * dirEx + dirEy * dirEy);
                                if (lenE > 0.001f) { dirEx = dirEx / lenE * arm; dirEy = dirEy / lenE * arm; }

                                g.DrawLine(tomboPen, pt.X - dirNx, pt.Y - dirNy, pt.X + dirNx, pt.Y + dirNy);
                                g.DrawLine(tomboPen, pt.X - dirEx, pt.Y - dirEy, pt.X + dirEx, pt.Y + dirEy);
                            }
                        }
                    }
                }

                // B. 外枠・内枠間の座標値表示 (X=000.000m, Y=000.000m)
                if (ShowBorderCoords)
                {
                    for (double sx = startSurX; sx <= endSurX + 0.001; sx += pitch)
                    {
                        DrawSingleCoordinateLabel(g, vc, canvasSize, sx, true, uMin, uMax, vMin, vMax, coordFont, coordBrush, coordBgBrush);
                    }

                    for (double sy = startSurY; sy <= endSurY + 0.001; sy += pitch)
                    {
                        DrawSingleCoordinateLabel(g, vc, canvasSize, sy, false, uMin, uMax, vMin, vMax, coordFont, coordBrush, coordBgBrush);
                    }
                }
            }
        }

        /// <summary>
        /// 1本のグリッド線（X=定数 または Y=定数）が内枠と交差する外枠と内枠の間に座標ラベルを描画
        /// </summary>
        private void DrawSingleCoordinateLabel(Graphics g, EditorMapViewController vc, Size canvasSize, double val, bool isXAxis, double uMin, double uMax, double vMin, double vMax, Font font, Brush textBrush, Brush bgBrush)
        {
            double rad = RotationAngleDeg * Math.PI / 180.0;
            double cos = Math.Cos(rad);
            double sin = Math.Sin(rad);

            string labelText = isXAxis ? $"X={val:0.00}m" : $"Y={val:0.00}m";

            var intersections = new System.Collections.Generic.List<(double surX, double surY)>();

            if (isXAxis)
            {
                double dNorth = val - CenterX;
                if (Math.Abs(sin) > 1e-6)
                {
                    double dEast1 = (vMin - dNorth * cos) / sin;
                    double u1 = dNorth * (-sin) + dEast1 * cos;
                    if (u1 >= uMin - 0.01 && u1 <= uMax + 0.01) intersections.Add((val, CenterY + dEast1));

                    double dEast2 = (vMax - dNorth * cos) / sin;
                    double u2 = dNorth * (-sin) + dEast2 * cos;
                    if (u2 >= uMin - 0.01 && u2 <= uMax + 0.01) intersections.Add((val, CenterY + dEast2));
                }
                if (Math.Abs(cos) > 1e-6)
                {
                    double dEast3 = (uMin + dNorth * sin) / cos;
                    double v3 = dNorth * cos + dEast3 * sin;
                    if (v3 >= vMin - 0.01 && v3 <= vMax + 0.01) intersections.Add((val, CenterY + dEast3));

                    double dEast4 = (uMax + dNorth * sin) / cos;
                    double v4 = dNorth * cos + dEast4 * sin;
                    if (v4 >= vMin - 0.01 && v4 <= vMax + 0.01) intersections.Add((val, CenterY + dEast4));
                }
            }
            else
            {
                double dEast = val - CenterY;
                if (Math.Abs(sin) > 1e-6)
                {
                    double dNorth1 = (dEast * cos - uMin) / sin;
                    double v1 = dNorth1 * cos + dEast * sin;
                    if (v1 >= vMin - 0.01 && v1 <= vMax + 0.01) intersections.Add((CenterX + dNorth1, val));

                    double dNorth2 = (dEast * cos - uMax) / sin;
                    double v2 = dNorth2 * cos + dEast * sin;
                    if (v2 >= vMin - 0.01 && v2 <= vMax + 0.01) intersections.Add((CenterX + dNorth2, val));
                }
                if (Math.Abs(cos) > 1e-6)
                {
                    double dNorth3 = (vMin - dEast * sin) / cos;
                    double u3 = dNorth3 * (-sin) + dEast * cos;
                    if (u3 >= uMin - 0.01 && u3 <= uMax + 0.01) intersections.Add((CenterX + dNorth3, val));

                    double dNorth4 = (vMax - dEast * sin) / cos;
                    double u4 = dNorth4 * (-sin) + dEast * cos;
                    if (u4 >= uMin - 0.01 && u4 <= uMax + 0.01) intersections.Add((CenterX + dNorth4, val));
                }
            }

            foreach (var (surX, surY) in intersections)
            {
                PointF pt = vc.ToCanvasPoint(surX, surY, canvasSize);
                var sz = g.MeasureString(labelText, font);
                float drawX = pt.X - (sz.Width / 2f);
                float drawY = pt.Y - (sz.Height / 2f);

                g.FillRectangle(bgBrush, drawX - 1f, drawY - 1f, sz.Width + 2f, sz.Height + 2f);
                g.DrawString(labelText, font, textBrush, drawX, drawY);
            }
        }

        /// <summary>
        /// 方位記号（北矢印マーク）の描画
        /// </summary>
        private void DrawNorthArrow(Graphics g, EditorMapViewController vc, Size canvasSize, PointF[] innerScreen, bool isDarkBackground)
        {
            PointF anchor;
            if (NorthArrowPosition == "カスタム" && HasCustomNorthArrowPos)
            {
                anchor = vc.ToCanvasPoint(NorthArrowCustomSurveyX, NorthArrowCustomSurveyY, canvasSize);
            }
            else
            {
                anchor = GetCornerPoint(innerScreen, NorthArrowPosition, 28f);
            }

            // 画面上の長さ (用紙mm -> 画面ピクセル換算)
            float innerPixelWidth = (float)Math.Sqrt(Math.Pow(innerScreen[1].X - innerScreen[0].X, 2) + Math.Pow(innerScreen[1].Y - innerScreen[0].Y, 2));
            var (innerWMm, _) = GetPaperDimensionsMm();
            innerWMm = Math.Max(10.0, innerWMm - (MarginLeftMm + MarginOtherMm + OuterInnerSpacingMm * 2.0));
            float pixelPerMm = innerPixelWidth / (float)innerWMm;

            float length = (float)Math.Max(16.0, NorthArrowSizeMm * pixelPerMm * 1.5);
            float width = length * 0.28f;

            // 北方向の角度（画面上ではRotationAngleDegに応じて回転）
            float needleRad = (float)(-RotationAngleDeg * Math.PI / 180.0);
            float cos = (float)Math.Cos(needleRad);
            float sin = (float)Math.Sin(needleRad);

            // 北の先端
            PointF tip = new PointF(anchor.X - sin * length, anchor.Y - cos * length);
            // 尾部中心
            PointF tail = new PointF(anchor.X + sin * (length * 0.35f), anchor.Y + cos * (length * 0.35f));
            // 左右の翼
            PointF leftWing = new PointF(anchor.X - cos * width + sin * (length * 0.1f), anchor.Y + sin * width + cos * (length * 0.1f));
            PointF rightWing = new PointF(anchor.X + cos * width + sin * (length * 0.1f), anchor.Y - sin * width + cos * (length * 0.1f));

            using (var blackBrush = new SolidBrush(isDarkBackground ? Color.White : Color.Black))
            using (var whiteBrush = new SolidBrush(isDarkBackground ? Color.FromArgb(120, 130, 150) : Color.White))
            using (var outlinePen = new Pen(isDarkBackground ? Color.White : Color.Black, 1.2f))
            using (var font = new Font("Arial", 8.5F, FontStyle.Bold))
            {
                g.FillPolygon(blackBrush, new PointF[] { tip, leftWing, tail });
                g.FillPolygon(whiteBrush, new PointF[] { tip, rightWing, tail });
                g.DrawPolygon(outlinePen, new PointF[] { tip, leftWing, tail, rightWing });

                string nStr = "N";
                var nSz = g.MeasureString(nStr, font);
                PointF nPos = new PointF(tip.X - sin * 10f - (nSz.Width / 2f), tip.Y - cos * 10f - (nSz.Height / 2f));
                g.DrawString(nStr, font, blackBrush, nPos);
            }
        }

        /// <summary>
        /// スケールバー（縮尺バー）の描画
        /// </summary>
        private void DrawScaleBar(Graphics g, PointF[] innerScreen, bool isDarkBackground)
        {
            PointF anchor;
            if (ScaleBarPosition == "中下")
            {
                // 内枠下辺の中央
                anchor = new PointF((innerScreen[0].X + innerScreen[1].X) / 2f, (innerScreen[0].Y + innerScreen[1].Y) / 2f - 20f);
            }
            else
            {
                // 右下
                anchor = GetCornerPoint(innerScreen, "右下", 20f);
            }

            double barMeters = (Scale >= 500) ? 50.0 : (Scale >= 200) ? 20.0 : 10.0;
            double halfBarMeters = barMeters / 2.0;
            double barMm = (barMeters / Scale) * 1000.0;

            float innerPixelWidth = (float)Math.Sqrt(Math.Pow(innerScreen[1].X - innerScreen[0].X, 2) + Math.Pow(innerScreen[1].Y - innerScreen[0].Y, 2));
            var (innerWMm, _) = GetPaperDimensionsMm();
            innerWMm = Math.Max(10.0, innerWMm - (MarginLeftMm + MarginOtherMm + OuterInnerSpacingMm * 2.0));

            float barPixelWidth = (float)(innerPixelWidth * (barMm / innerWMm));
            if (barPixelWidth < 30f || barPixelWidth > 400f) barPixelWidth = 100f;

            float barHeight = 5f;
            float leftX = anchor.X - (barPixelWidth / 2f);
            float topY = anchor.Y;

            Color primaryColor = isDarkBackground ? Color.White : Color.Black;
            Color secondaryColor = isDarkBackground ? Color.FromArgb(80, 85, 100) : Color.White;

            using (var primaryBrush = new SolidBrush(primaryColor))
            using (var secondaryBrush = new SolidBrush(secondaryColor))
            using (var pen = new Pen(primaryColor, 1.2f))
            using (var font = new Font("Yu Gothic UI", 7.5F, FontStyle.Bold))
            {
                float midX = leftX + (barPixelWidth / 2f);
                g.FillRectangle(primaryBrush, leftX, topY, barPixelWidth / 2f, barHeight);
                g.FillRectangle(secondaryBrush, midX, topY, barPixelWidth / 2f, barHeight);
                g.DrawRectangle(pen, leftX, topY, barPixelWidth, barHeight);
                g.DrawLine(pen, midX, topY, midX, topY + barHeight);

                string l0 = "0";
                string lMid = $"{halfBarMeters:0}";
                string lEnd = $"{barMeters:0}m (1/{Scale:0})";

                g.DrawString(l0, font, primaryBrush, leftX - 4f, topY - 14f);
                g.DrawString(lMid, font, primaryBrush, midX - 6f, topY - 14f);
                g.DrawString(lEnd, font, primaryBrush, leftX + barPixelWidth - 10f, topY - 14f);
            }
        }

        private PointF GetCornerPoint(PointF[] corners, string pos, float inset)
        {
            // 0:左下, 1:右下, 2:右上, 3:左上
            switch (pos)
            {
                case "左上":
                    return new PointF(corners[3].X + inset, corners[3].Y + inset);
                case "左下":
                    return new PointF(corners[0].X + inset, corners[0].Y - inset);
                case "右下":
                    return new PointF(corners[1].X - inset, corners[1].Y - inset);
                case "右上":
                default:
                    return new PointF(corners[2].X - inset, corners[2].Y + inset);
            }
        }
    }
}
