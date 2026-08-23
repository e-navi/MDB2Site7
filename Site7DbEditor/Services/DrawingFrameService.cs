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
        public string NorthArrowPosition { get; set; } = "右上"; // 右上, 左上, 右下, 左下
        public bool ShowScaleBar { get; set; } = true;      // スケールバー表示
        public string ScaleBarPosition { get; set; } = "右下";   // 右下, 左下, 右上, 左上

        // 余白（用紙上のミリメートル単位）
        public double MarginLeftMm { get; set; } = 20.0;
        public double MarginRightMm { get; set; } = 10.0;
        public double MarginTopMm { get; set; } = 10.0;
        public double MarginBottomMm { get; set; } = 10.0;

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
                // 縦向きの場合は入れ替え
                var temp = w;
                w = h;
                h = temp;
            }
            return (w, h);
        }

        /// <summary>
        /// 実空間（メートル）での外枠の幅・高さを取得
        /// </summary>
        public (double widthM, double heightM) GetFrameDimensionsMeters()
        {
            var (wMm, hMm) = GetPaperDimensionsMm();
            double wM = (wMm / 1000.0) * Scale;
            double hM = (hMm / 1000.0) * Scale;
            return (wM, hM);
        }

        /// <summary>
        /// 実空間（メートル）での内枠の幅・高さを取得
        /// </summary>
        public (double widthM, double heightM, double offsetEastM, double offsetNorthM) GetInnerFrameDimensionsMeters()
        {
            var (wMm, hMm) = GetPaperDimensionsMm();
            double innerWMm = Math.Max(10.0, wMm - (MarginLeftMm + MarginRightMm));
            double innerHMm = Math.Max(10.0, hMm - (MarginTopMm + MarginBottomMm));

            double innerWM = (innerWMm / 1000.0) * Scale;
            double innerHM = (innerHMm / 1000.0) * Scale;

            // マージン非対称による中心からのズレ（ローカル座標）
            double offsetEastMm = (MarginLeftMm - MarginRightMm) / 2.0;
            double offsetNorthMm = (MarginBottomMm - MarginTopMm) / 2.0;

            double offsetEastM = (offsetEastMm / 1000.0) * Scale;
            double offsetNorthM = (offsetNorthMm / 1000.0) * Scale;

            return (innerWM, innerHM, offsetEastM, offsetNorthM);
        }

        /// <summary>
        /// 外枠の4頂点（測量座標: 北X, 東Y）を取得（左下、右下、右上、左上）
        /// </summary>
        public (double surveyX, double surveyY)[] GetOuterCornersSurvey()
        {
            return GetOuterCornersSurvey(CenterX, CenterY, RotationAngleDeg);
        }

        public (double surveyX, double surveyY)[] GetOuterCornersSurvey(double cx, double cy, double angleDeg)
        {
            var (wM, hM) = GetFrameDimensionsMeters();
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

        /// <summary>
        /// 内枠の4頂点（測量座標: 北X, 東Y）を取得
        /// </summary>
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

            var outerCorners = GetOuterCornersSurvey(cx, cy, angleDeg);
            var innerCorners = GetInnerCornersSurvey(cx, cy, angleDeg);

            PointF[] outerScreen = new PointF[outerCorners.Length];
            for (int i = 0; i < outerCorners.Length; i++)
            {
                outerScreen[i] = vc.ToCanvasPoint(outerCorners[i].surveyX, outerCorners[i].surveyY, canvasSize);
            }

            PointF[] innerScreen = new PointF[innerCorners.Length];
            for (int i = 0; i < innerCorners.Length; i++)
            {
                innerScreen[i] = vc.ToCanvasPoint(innerCorners[i].surveyX, innerCorners[i].surveyY, canvasSize);
            }

            // ラバーバンドペン（黄色点線）
            using (var outerPen = new Pen(Color.FromArgb(255, 230, 0), 1.6f) { DashStyle = DashStyle.Dash })
            using (var thickPen = new Pen(Color.FromArgb(255, 230, 0), 3.6f) { DashStyle = DashStyle.Dash })
            using (var innerPen = new Pen(Color.FromArgb(0, 225, 255), 1.4f) { DashStyle = DashStyle.Dot })
            using (var centerPen = new Pen(Color.FromArgb(255, 100, 100), 1.8f))
            {
                g.DrawPolygon(outerPen, outerScreen);
                // 下辺 (0:左下 -> 1:右下) と 右辺 (1:右下 -> 2:右上) を太線で強調
                g.DrawLine(thickPen, outerScreen[0], outerScreen[1]);
                g.DrawLine(thickPen, outerScreen[1], outerScreen[2]);

                g.DrawPolygon(innerPen, innerScreen);

                // 中心点
                PointF centerScreen = vc.ToCanvasPoint(cx, cy, canvasSize);
                g.DrawLine(centerPen, centerScreen.X - 10f, centerScreen.Y, centerScreen.X + 10f, centerScreen.Y);
                g.DrawLine(centerPen, centerScreen.X, centerScreen.Y - 10f, centerScreen.X, centerScreen.Y + 10f);
                g.DrawEllipse(centerPen, centerScreen.X - 5f, centerScreen.Y - 5f, 10f, 10f);

                // 回転モードの場合、中心からマウス位置へのラバーバンド線
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

            var outerCorners = GetOuterCornersSurvey();
            var innerCorners = GetInnerCornersSurvey();

            PointF[] outerScreen = new PointF[outerCorners.Length];
            for (int i = 0; i < outerCorners.Length; i++)
            {
                outerScreen[i] = vc.ToCanvasPoint(outerCorners[i].surveyX, outerCorners[i].surveyY, canvasSize);
            }

            PointF[] innerScreen = new PointF[innerCorners.Length];
            for (int i = 0; i < innerCorners.Length; i++)
            {
                innerScreen[i] = vc.ToCanvasPoint(innerCorners[i].surveyX, innerCorners[i].surveyY, canvasSize);
            }

            // 配色ペン・ブラシ
            Color outerColor = isDarkBackground ? Color.FromArgb(240, 240, 245) : Color.FromArgb(20, 20, 25);
            Color innerColor = isDarkBackground ? Color.FromArgb(0, 210, 255) : Color.FromArgb(0, 130, 210);
            Color centerColor = isDarkBackground ? Color.FromArgb(255, 180, 0) : Color.FromArgb(220, 100, 0);
            Color tomboColor = isDarkBackground ? Color.FromArgb(200, 255, 100, 100) : Color.FromArgb(220, 180, 20, 20);
            Color coordColor = isDarkBackground ? Color.FromArgb(220, 220, 230) : Color.FromArgb(40, 40, 50);

            // 1. 外枠の描画（全体は通常実線、下辺と右辺は太線で用紙の向きを明示）
            using (var outerPen = new Pen(outerColor, 1.5f))
            using (var thickPen = new Pen(outerColor, 3.8f))
            {
                g.DrawPolygon(outerPen, outerScreen);
                // 下辺 (0:左下 -> 1:右下) と 右辺 (1:右下 -> 2:右上) を太線で強調
                g.DrawLine(thickPen, outerScreen[0], outerScreen[1]);
                g.DrawLine(thickPen, outerScreen[1], outerScreen[2]);
            }

            // 2. 内枠の描画 (作図範囲: シアン実線)
            using (var innerPen = new Pen(innerColor, 1.4f))
            {
                g.DrawPolygon(innerPen, innerScreen);
            }

            // 3. トンボ (+) & 格子線 & 外枠・内枠間座標値の描画
            DrawTomboAndCoordinates(g, vc, canvasSize, innerCorners, isDarkBackground, tomboColor, coordColor);

            // 4. 中心マーク（十字線）
            PointF centerScreen = vc.ToCanvasPoint(CenterX, CenterY, canvasSize);
            using (var centerPen = new Pen(centerColor, 1.5f))
            {
                g.DrawLine(centerPen, centerScreen.X - 8f, centerScreen.Y, centerScreen.X + 8f, centerScreen.Y);
                g.DrawLine(centerPen, centerScreen.X, centerScreen.Y - 8f, centerScreen.X, centerScreen.Y + 8f);
                g.DrawEllipse(centerPen, centerScreen.X - 4f, centerScreen.Y - 4f, 8f, 8f);
            }

            // 5. 方位記号 (North Arrow) の描画
            if (ShowNorthArrow)
            {
                DrawNorthArrow(g, innerScreen, isDarkBackground);
            }

            // 6. スケールバー (Scale Bar) の描画
            if (ShowScaleBar)
            {
                DrawScaleBar(g, innerScreen, isDarkBackground);
            }

            // 7. 図枠情報ラベル（外枠の左上外側に表示）
            PointF infoPos = outerScreen[3]; // 左上
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
                                // 測量X方向（北軸）と 測量Y方向（東軸）に沿った十字アーム
                                float armNorthDx = (float)(cosRot * -arm * 0 + -sinRot * 0); // 画面上での北方向
                                // キャンバス座標系ではYが下向きなので vc.ToCanvasPointの向きを反映
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
                    // 測量X（北）定数線と内枠境界の交差点に "X=..." を描画
                    for (double sx = startSurX; sx <= endSurX + 0.001; sx += pitch)
                    {
                        DrawSingleCoordinateLabel(g, vc, canvasSize, sx, true, uMin, uMax, vMin, vMax, coordFont, coordBrush, coordBgBrush);
                    }

                    // 測量Y（東）定数線と内枠境界の交差点に "Y=..." を描画
                    for (double sy = startSurY; sy <= endSurY + 0.001; sy += pitch)
                    {
                        DrawSingleCoordinateLabel(g, vc, canvasSize, sy, false, uMin, uMax, vMin, vMax, coordFont, coordBrush, coordBgBrush);
                    }
                }
            }
        }

        /// <summary>
        /// 1本のグリッド線（X=定数 または Y=定数）が内枠と交差する外枠余白部に座標ラベルを描画
        /// </summary>
        private void DrawSingleCoordinateLabel(Graphics g, EditorMapViewController vc, Size canvasSize, double val, bool isXAxis, double uMin, double uMax, double vMin, double vMax, Font font, Brush textBrush, Brush bgBrush)
        {
            double rad = RotationAngleDeg * Math.PI / 180.0;
            double cos = Math.Cos(rad);
            double sin = Math.Sin(rad);

            string labelText = isXAxis ? $"X={val:0.000}m" : $"Y={val:0.000}m";

            // 測量線 val と内枠4辺の交差を調べる
            // isXAxis: 測量X = val (北=val), 測量Yを変化させる -> (dNorth = val - CenterX, dEast = t)
            //   u = dNorth * (-sin) + dEast * cos
            //   v = dNorth * cos + dEast * sin
            // 交差判定: u = uMin/uMax または v = vMin/vMax

            var intersections = new System.Collections.Generic.List<(double surX, double surY, double outOffsetU, double outOffsetV)>();

            if (isXAxis)
            {
                double dNorth = val - CenterX;
                // 1) v = vMin (下辺) -> dEast = (vMin - dNorth * cos) / sin (sin != 0)
                if (Math.Abs(sin) > 1e-6)
                {
                    double dEast = (vMin - dNorth * cos) / sin;
                    double u = dNorth * (-sin) + dEast * cos;
                    if (u >= uMin - 0.01 && u <= uMax + 0.01)
                        intersections.Add((val, CenterY + dEast, 0, -1.2));
                }
                // 2) v = vMax (上辺)
                if (Math.Abs(sin) > 1e-6)
                {
                    double dEast = (vMax - dNorth * cos) / sin;
                    double u = dNorth * (-sin) + dEast * cos;
                    if (u >= uMin - 0.01 && u <= uMax + 0.01)
                        intersections.Add((val, CenterY + dEast, 0, 1.2));
                }
                // 3) u = uMin (左辺) -> dEast = (uMin + dNorth * sin) / cos
                if (Math.Abs(cos) > 1e-6)
                {
                    double dEast = (uMin + dNorth * sin) / cos;
                    double v = dNorth * cos + dEast * sin;
                    if (v >= vMin - 0.01 && v <= vMax + 0.01)
                        intersections.Add((val, CenterY + dEast, -1.2, 0));
                }
                // 4) u = uMax (右辺)
                if (Math.Abs(cos) > 1e-6)
                {
                    double dEast = (uMax + dNorth * sin) / cos;
                    double v = dNorth * cos + dEast * sin;
                    if (v >= vMin - 0.01 && v <= vMax + 0.01)
                        intersections.Add((val, CenterY + dEast, 1.2, 0));
                }
            }
            else
            {
                double dEast = val - CenterY;
                // 1) u = uMin (左辺) -> dNorth = (dEast * cos - uMin) / sin
                if (Math.Abs(sin) > 1e-6)
                {
                    double dNorth = (dEast * cos - uMin) / sin;
                    double v = dNorth * cos + dEast * sin;
                    if (v >= vMin - 0.01 && v <= vMax + 0.01)
                        intersections.Add((CenterX + dNorth, val, -1.2, 0));
                }
                // 2) u = uMax (右辺)
                if (Math.Abs(sin) > 1e-6)
                {
                    double dNorth = (dEast * cos - uMax) / sin;
                    double v = dNorth * cos + dEast * sin;
                    if (v >= vMin - 0.01 && v <= vMax + 0.01)
                        intersections.Add((CenterX + dNorth, val, 1.2, 0));
                }
                // 3) v = vMin (下辺) -> dNorth = (vMin - dEast * sin) / cos
                if (Math.Abs(cos) > 1e-6)
                {
                    double dNorth = (vMin - dEast * sin) / cos;
                    double u = dNorth * (-sin) + dEast * cos;
                    if (u >= uMin - 0.01 && u <= uMax + 0.01)
                        intersections.Add((CenterX + dNorth, val, 0, -1.2));
                }
                // 4) v = vMax (上辺)
                if (Math.Abs(cos) > 1e-6)
                {
                    double dNorth = (vMax - dEast * sin) / cos;
                    double u = dNorth * (-sin) + dEast * cos;
                    if (u >= uMin - 0.01 && u <= uMax + 0.01)
                        intersections.Add((CenterX + dNorth, val, 0, 1.2));
                }
            }

            foreach (var (surX, surY, offU, offV) in intersections)
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
        private void DrawNorthArrow(Graphics g, PointF[] innerScreen, bool isDarkBackground)
        {
            PointF anchor = GetCornerPoint(innerScreen, NorthArrowPosition, 28f);

            // 北方向の角度（画面上ではRotationAngleDegに応じて回転）
            float needleRad = (float)(-RotationAngleDeg * Math.PI / 180.0);
            float length = 24f;
            float width = 7f;

            float cos = (float)Math.Cos(needleRad);
            float sin = (float)Math.Sin(needleRad);

            // 北の先端
            PointF tip = new PointF(anchor.X - sin * length, anchor.Y - cos * length);
            // 尾部中心
            PointF tail = new PointF(anchor.X + sin * (length * 0.4f), anchor.Y + cos * (length * 0.4f));
            // 左右の翼
            PointF leftWing = new PointF(anchor.X - cos * width + sin * (length * 0.1f), anchor.Y + sin * width + cos * (length * 0.1f));
            PointF rightWing = new PointF(anchor.X + cos * width + sin * (length * 0.1f), anchor.Y - sin * width + cos * (length * 0.1f));

            using (var blackBrush = new SolidBrush(isDarkBackground ? Color.White : Color.Black))
            using (var whiteBrush = new SolidBrush(isDarkBackground ? Color.FromArgb(120, 130, 150) : Color.White))
            using (var outlinePen = new Pen(isDarkBackground ? Color.White : Color.Black, 1.2f))
            using (var font = new Font("Arial", 8.5F, FontStyle.Bold))
            {
                // 左半分（黒/塗りつぶし）
                g.FillPolygon(blackBrush, new PointF[] { tip, leftWing, tail });
                // 右半分（白/薄色）
                g.FillPolygon(whiteBrush, new PointF[] { tip, rightWing, tail });
                // 輪郭
                g.DrawPolygon(outlinePen, new PointF[] { tip, leftWing, tail, rightWing });

                // "N" 文字の描画
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
            PointF anchor = GetCornerPoint(innerScreen, ScaleBarPosition, 20f);

            // 縮尺に応じた適切な実距離バー長さ (例: 1/200 -> 10m/20m, 1/500 -> 20m/50m, 1/100 -> 5m/10m)
            double barMeters = (Scale >= 500) ? 50.0 : (Scale >= 200) ? 20.0 : 10.0;
            double halfBarMeters = barMeters / 2.0;

            // 用紙上のミリメートル -> 実空間 -> キャンバス上のピクセル長さを算出
            // Scale 1/200 で 20m = 用紙上 100mm
            double barMm = (barMeters / Scale) * 1000.0;

            // キャンバス上での長さを内枠の角の画面ピクセル比率から算出
            // innerScreen[0] -> innerScreen[1] (内枠下辺の幅)
            float innerPixelWidth = (float)Math.Sqrt(Math.Pow(innerScreen[1].X - innerScreen[0].X, 2) + Math.Pow(innerScreen[1].Y - innerScreen[0].Y, 2));
            var (innerWMm, _) = GetPaperDimensionsMm();
            innerWMm = Math.Max(10.0, innerWMm - (MarginLeftMm + MarginRightMm));

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
                // 左半分・右半分のブロック
                float midX = leftX + (barPixelWidth / 2f);
                g.FillRectangle(primaryBrush, leftX, topY, barPixelWidth / 2f, barHeight);
                g.FillRectangle(secondaryBrush, midX, topY, barPixelWidth / 2f, barHeight);
                g.DrawRectangle(pen, leftX, topY, barPixelWidth, barHeight);
                g.DrawLine(pen, midX, topY, midX, topY + barHeight);

                // 目盛ラベル: "0", "halfM", "barMeters m"
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

