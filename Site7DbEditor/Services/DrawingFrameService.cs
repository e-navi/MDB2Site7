using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using Site7DbEditor;

namespace Site7DbEditor.Services
{
    public class DrawingFrameService
    {
        private static DrawingFrameService? _instance;
        public static DrawingFrameService Instance => _instance ??= new DrawingFrameService();

        public DrawingFrameService()
        {
            LoadFromIni();
        }

        // 状態・パラメータ
        public bool IsVisible { get; set; } = true;
        public bool IsDrawingPreviewEnabled { get; set; } = false; // 図面表示確認（2分割プレビュー）
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
            string infoText = $"図枠 [{PaperSizeName} {(IsLandscape ? "横" : "縦")} 1/{Scale:0} ({RotationAngleDeg:0.0}°)] ピッチ:{effectivePitch:0.#}m";
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

            // 内枠の測量座標範囲（内枠に格子線が表示される範囲を基準とする）
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

            PointF[] innerScreen = ToScreenPoints(vc, innerCorners, canvasSize);

            using (var tomboPen = new Pen(tomboColor, 1.2f))
            using (var gridPen = new Pen(Color.FromArgb(60, tomboColor), 1f) { DashStyle = DashStyle.Dash })
            using (var coordFont = new Font("Yu Gothic UI", 7.5F, FontStyle.Bold))
            using (var coordBrush = new SolidBrush(coordColor))
            using (var coordBgBrush = new SolidBrush(isDarkBackground ? Color.FromArgb(160, 25, 25, 35) : Color.FromArgb(180, 255, 255, 255)))
            {
                // A. 格子線 (Grid Lines) の描画（内枠ポリゴンでクリッピングして枠端まで完全に通過）
                if (ShowGridLines)
                {
                    var oldClip = g.Clip;
                    using (var innerPath = new GraphicsPath())
                    {
                        innerPath.AddPolygon(innerScreen);
                        g.SetClip(innerPath);

                        // X一定線（東西方向）
                        for (double sx = startSurX; sx <= endSurX + 0.001; sx += pitch)
                        {
                            PointF pStart = vc.ToCanvasPoint(sx, startSurY, canvasSize);
                            PointF pEnd = vc.ToCanvasPoint(sx, endSurY, canvasSize);
                            g.DrawLine(gridPen, pStart, pEnd);
                        }

                        // Y一定線（南北方向）
                        for (double sy = startSurY; sy <= endSurY + 0.001; sy += pitch)
                        {
                            PointF pStart = vc.ToCanvasPoint(startSurX, sy, canvasSize);
                            PointF pEnd = vc.ToCanvasPoint(endSurX, sy, canvasSize);
                            g.DrawLine(gridPen, pStart, pEnd);
                        }
                    }
                    g.Clip = oldClip;
                }

                // B. トンボ (+) の描画
                if (ShowTombo)
                {
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
                    var (outerWM, outerHM, outerOffEastM, outerOffNorthM) = GetOuterFrameDimensionsMeters();
                    double uMinOuter = outerOffEastM - outerWM / 2.0;
                    double uMaxOuter = outerOffEastM + outerWM / 2.0;
                    double vMinOuter = outerOffNorthM - outerHM / 2.0;
                    double vMaxOuter = outerOffNorthM + outerHM / 2.0;

                    double uMidLeft = (uMinOuter + uMin) / 2.0;
                    double uMidRight = (uMaxOuter + uMax) / 2.0;
                    double vMidBottom = (vMinOuter + vMin) / 2.0;
                    double vMidTop = (vMaxOuter + vMax) / 2.0;

                    for (double sx = startSurX; sx <= endSurX + 0.001; sx += pitch)
                    {
                        DrawSingleCoordinateLabel(g, vc, canvasSize, sx, true, uMidLeft, uMidRight, vMidBottom, vMidTop, uMin, uMax, vMin, vMax, coordFont, coordBrush, coordBgBrush);
                    }

                    for (double sy = startSurY; sy <= endSurY + 0.001; sy += pitch)
                    {
                        DrawSingleCoordinateLabel(g, vc, canvasSize, sy, false, uMidLeft, uMidRight, vMidBottom, vMidTop, uMin, uMax, vMin, vMax, coordFont, coordBrush, coordBgBrush);
                    }
                }
            }
        }

        private static float NormalizeTextAngle(float deg)
        {
            while (deg > 90f) deg -= 180f;
            while (deg < -90f) deg += 180f;
            return deg;
        }

        /// <summary>
        /// 1本のグリッド線（X=定数 または Y=定数）が内枠と交差する位置の外側中間線に、格子線と平行に回転して座標ラベルを描画（内枠基準）
        /// </summary>
        private void DrawSingleCoordinateLabel(Graphics g, EditorMapViewController vc, Size canvasSize, double val, bool isXAxis, double uMidLeft, double uMidRight, double vMidBottom, double vMidTop, double uMinInner, double uMaxInner, double vMinInner, double vMaxInner, Font font, Brush textBrush, Brush bgBrush)
        {
            double rad = RotationAngleDeg * Math.PI / 180.0;
            double cos = Math.Cos(rad);
            double sin = Math.Sin(rad);

            string numStr = (Math.Abs(val % 1.0) < 1e-4) ? $"{val:0}m" : $"{val:0.00}m";
            string labelText = isXAxis ? $"X={numStr}" : $"Y={numStr}";
            var intersections = new System.Collections.Generic.List<(double surX, double surY)>();

            if (isXAxis)
            {
                double dNorth = val - CenterX;
                // 1. 内枠下辺 (v = vMinInner) と交差するか判定 -> 中間線 (v = vMidBottom) 上の点
                if (Math.Abs(sin) > 1e-6)
                {
                    double uEdge = (vMinInner * cos - dNorth) / sin;
                    if (uEdge >= uMinInner - 0.01 && uEdge <= uMaxInner + 0.01)
                    {
                        double uMid = (vMidBottom * cos - dNorth) / sin;
                        double dEastMid = vMidBottom * sin + uMid * cos;
                        intersections.Add((val, CenterY + dEastMid));
                    }
                }
                // 2. 内枠上辺 (v = vMaxInner) と交差するか判定 -> 中間線 (v = vMidTop) 上の点
                if (Math.Abs(sin) > 1e-6)
                {
                    double uEdge = (vMaxInner * cos - dNorth) / sin;
                    if (uEdge >= uMinInner - 0.01 && uEdge <= uMaxInner + 0.01)
                    {
                        double uMid = (vMidTop * cos - dNorth) / sin;
                        double dEastMid = vMidTop * sin + uMid * cos;
                        intersections.Add((val, CenterY + dEastMid));
                    }
                }
                // 3. 内枠左辺 (u = uMinInner) と交差するか判定 -> 中間線 (u = uMidLeft) 上の点
                if (Math.Abs(cos) > 1e-6)
                {
                    double vEdge = (dNorth + uMinInner * sin) / cos;
                    if (vEdge >= vMinInner - 0.01 && vEdge <= vMaxInner + 0.01)
                    {
                        double vMid = (dNorth + uMidLeft * sin) / cos;
                        double dEastMid = vMid * sin + uMidLeft * cos;
                        intersections.Add((val, CenterY + dEastMid));
                    }
                }
                // 4. 内枠右辺 (u = uMaxInner) と交差するか判定 -> 中間線 (u = uMidRight) 上の点
                if (Math.Abs(cos) > 1e-6)
                {
                    double vEdge = (dNorth + uMaxInner * sin) / cos;
                    if (vEdge >= vMinInner - 0.01 && vEdge <= vMaxInner + 0.01)
                    {
                        double vMid = (dNorth + uMidRight * sin) / cos;
                        double dEastMid = vMid * sin + uMidRight * cos;
                        intersections.Add((val, CenterY + dEastMid));
                    }
                }
            }
            else
            {
                double dEast = val - CenterY;
                // 1. 内枠下辺 (v = vMinInner) と交差するか判定 -> 中間線 (v = vMidBottom) 上の点
                if (Math.Abs(cos) > 1e-6)
                {
                    double uEdge = (dEast - vMinInner * sin) / cos;
                    if (uEdge >= uMinInner - 0.01 && uEdge <= uMaxInner + 0.01)
                    {
                        double uMid = (dEast - vMidBottom * sin) / cos;
                        double dNorthMid = vMidBottom * cos - uMid * sin;
                        intersections.Add((CenterX + dNorthMid, val));
                    }
                }
                // 2. 内枠上辺 (v = vMaxInner) と交差するか判定 -> 中間線 (v = vMidTop) 上の点
                if (Math.Abs(cos) > 1e-6)
                {
                    double uEdge = (dEast - vMaxInner * sin) / cos;
                    if (uEdge >= uMinInner - 0.01 && uEdge <= uMaxInner + 0.01)
                    {
                        double uMid = (dEast - vMidTop * sin) / cos;
                        double dNorthMid = vMidTop * cos - uMid * sin;
                        intersections.Add((CenterX + dNorthMid, val));
                    }
                }
                // 3. 内枠左辺 (u = uMinInner) と交差するか判定 -> 中間線 (u = uMidLeft) 上の点
                if (Math.Abs(sin) > 1e-6)
                {
                    double vEdge = (dEast - uMinInner * cos) / sin;
                    if (vEdge >= vMinInner - 0.01 && vEdge <= vMaxInner + 0.01)
                    {
                        double vMid = (dEast - uMidLeft * cos) / sin;
                        double dNorthMid = vMid * cos - uMidLeft * sin;
                        intersections.Add((CenterX + dNorthMid, val));
                    }
                }
                // 4. 内枠右辺 (u = uMaxInner) と交差するか判定 -> 中間線 (u = uMidRight) 上の点
                if (Math.Abs(sin) > 1e-6)
                {
                    double vEdge = (dEast - uMaxInner * cos) / sin;
                    if (vEdge >= vMinInner - 0.01 && vEdge <= vMaxInner + 0.01)
                    {
                        double vMid = (dEast - uMidRight * cos) / sin;
                        double dNorthMid = vMid * cos - uMidRight * sin;
                        intersections.Add((CenterX + dNorthMid, val));
                    }
                }
            }

            // 測量座標系での格子線の傾きベクトルから画面上の回転角度を計算 (可読性を保つため -90°〜+90° に正規化)
            float rotDeg;
            if (isXAxis)
            {
                // X一定線: 測量東(Y)方向に伸びる
                PointF p0 = vc.ToCanvasPoint(val, CenterY, canvasSize);
                PointF p1 = vc.ToCanvasPoint(val, CenterY + 10.0, canvasSize);
                float dx = p1.X - p0.X;
                float dy = p1.Y - p0.Y;
                rotDeg = NormalizeTextAngle((float)(Math.Atan2(dy, dx) * 180.0 / Math.PI));
            }
            else
            {
                // Y一定線: 測量北(X)方向に伸びる
                PointF p0 = vc.ToCanvasPoint(CenterX, val, canvasSize);
                PointF p1 = vc.ToCanvasPoint(CenterX + 10.0, val, canvasSize);
                float dx = p1.X - p0.X;
                float dy = p1.Y - p0.Y;
                rotDeg = NormalizeTextAngle((float)(Math.Atan2(dy, dx) * 180.0 / Math.PI));
            }

            foreach (var (surX, surY) in intersections)
            {
                PointF pt = vc.ToCanvasPoint(surX, surY, canvasSize);
                var sz = g.MeasureString(labelText, font);

                var state = g.Save();
                g.TranslateTransform(pt.X, pt.Y);
                g.RotateTransform(rotDeg);

                // 背景は透明（文字のみ描画）
                g.DrawString(labelText, font, textBrush, -sz.Width / 2f, -sz.Height / 2f);
                g.Restore(state);
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
                anchor = new PointF((innerScreen[0].X + innerScreen[1].X) / 2f, (innerScreen[0].Y + innerScreen[1].Y) / 2f);
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
            Color primaryColor = isDarkBackground ? Color.White : Color.Black;
            Color secondaryColor = isDarkBackground ? Color.FromArgb(80, 85, 100) : Color.White;

            float dx = innerScreen[1].X - innerScreen[0].X;
            float dy = innerScreen[1].Y - innerScreen[0].Y;
            float edgeAngle = (float)(Math.Atan2(dy, dx) * 180.0 / Math.PI);
            float rotDeg = NormalizeTextAngle(edgeAngle);

            var state = g.Save();
            g.TranslateTransform(anchor.X, anchor.Y);
            g.RotateTransform(rotDeg);
            g.TranslateTransform(0, -18f); // 枠の内側へオフセット

            using (var primaryBrush = new SolidBrush(primaryColor))
            using (var secondaryBrush = new SolidBrush(secondaryColor))
            using (var pen = new Pen(primaryColor, 1.2f))
            using (var font = new Font("Yu Gothic UI", 7.5F, FontStyle.Bold))
            {
                float leftX = -barPixelWidth / 2f;
                float topY = 0f;
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
            g.Restore(state);
        }

        private PointF GetCornerPoint(PointF[] corners, string pos, float inset)
        {
            // 0:左下, 1:右下, 2:右上, 3:左上
            if (corners.Length < 4) return PointF.Empty;

            float duX = corners[1].X - corners[0].X;
            float duY = corners[1].Y - corners[0].Y;
            float lenU = (float)Math.Sqrt(duX * duX + duY * duY);
            float uX = (lenU > 0.001f) ? (duX / lenU) : 1f;
            float uY = (lenU > 0.001f) ? (duY / lenU) : 0f;

            float dvX = corners[3].X - corners[0].X;
            float dvY = corners[3].Y - corners[0].Y;
            float lenV = (float)Math.Sqrt(dvX * dvX + dvY * dvY);
            float vX = (lenV > 0.001f) ? (dvX / lenV) : 0f;
            float vY = (lenV > 0.001f) ? (dvY / lenV) : -1f;

            switch (pos)
            {
                case "左上":
                    return new PointF(corners[3].X + uX * inset - vX * inset, corners[3].Y + uY * inset - vY * inset);
                case "左下":
                    return new PointF(corners[0].X + uX * inset + vX * inset, corners[0].Y + uY * inset + vY * inset);
                case "右下":
                    return new PointF(corners[1].X - uX * inset + vX * inset, corners[1].Y - uY * inset + vY * inset);
                case "右上":
                default:
                    return new PointF(corners[2].X - uX * inset - vX * inset, corners[2].Y - uY * inset - vY * inset);
            }
        }

        /// <summary>
        /// 図面プレビューキャンバス上のスクリーン座標を測量座標 (北X, 東Y) に変換
        /// </summary>
        public (double surveyX, double surveyY) PaperScreenToSurvey(PointF screenPt, Size canvasSize)
        {
            var (wMm, hMm) = GetPaperDimensionsMm();
            float availW = Math.Max(10f, canvasSize.Width - 36f);
            float availH = Math.Max(10f, canvasSize.Height - 36f);
            float zoom = Math.Min(availW / (float)wMm, availH / (float)hMm);

            float paperW = (float)wMm * zoom;
            float paperH = (float)hMm * zoom;
            float paperLeft = (canvasSize.Width - paperW) / 2f;
            float paperTop = (canvasSize.Height - paperH) / 2f;

            float mmX = (screenPt.X - (paperLeft + paperW / 2f)) / zoom;
            float mmY = ((paperTop + paperH / 2f) - screenPt.Y) / zoom;

            double u = (mmX / 1000.0) * Scale;
            double v = (mmY / 1000.0) * Scale;

            double rad = RotationAngleDeg * Math.PI / 180.0;
            double cos = Math.Cos(rad);
            double sin = Math.Sin(rad);

            double dNorth = v * cos - u * sin;
            double dEast = v * sin + u * cos;

            return (CenterX + dNorth, CenterY + dEast);
        }

        /// <summary>
        /// 図面プレビューキャンバス（用紙イメージ）へのリアルタイムレンダリング
        /// </summary>
        public void DrawPaperPreview(
            Graphics g,
            Size canvasSize,
            EditorDbManager db,
            Func<int, bool>? isMapLayerVisible,
            bool showCurve,
            bool colorByIkou,
            bool showIkou,
            bool showIkouName,
            bool showIbutu,
            bool showIbutuName,
            bool showKikai,
            bool showKikaiName,
            bool showHyoukou)
        {
            if (canvasSize.Width <= 0 || canvasSize.Height <= 0) return;

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Color.FromArgb(32, 34, 44)); // スタジオ暗色背景

            var (wMm, hMm) = GetPaperDimensionsMm();
            float availW = Math.Max(10f, canvasSize.Width - 36f);
            float availH = Math.Max(10f, canvasSize.Height - 36f);
            float zoom = Math.Min(availW / (float)wMm, availH / (float)hMm);

            float paperW = (float)wMm * zoom;
            float paperH = (float)hMm * zoom;
            float paperLeft = (canvasSize.Width - paperW) / 2f;
            float paperTop = (canvasSize.Height - paperH) / 2f;

            PointF SurveyToPaperScreen(double sx, double sy)
            {
                double dNorth = sx - CenterX;
                double dEast = sy - CenterY;
                double rad = RotationAngleDeg * Math.PI / 180.0;
                double cos = Math.Cos(rad);
                double sin = Math.Sin(rad);

                double u = dNorth * (-sin) + dEast * cos;
                double v = dNorth * cos + dEast * sin;

                double mmX = (u / Scale) * 1000.0;
                double mmY = (v / Scale) * 1000.0;

                float px = paperLeft + (paperW / 2f) + (float)(mmX * zoom);
                float py = paperTop + (paperH / 2f) - (float)(mmY * zoom);
                return new PointF(px, py);
            }

            // 1. 用紙のドロップシャドウ & 白地用紙の描画
            RectangleF paperRect = new RectangleF(paperLeft, paperTop, paperW, paperH);
            using (var shadowBrush = new SolidBrush(Color.FromArgb(80, 0, 0, 0)))
            {
                g.FillRectangle(shadowBrush, paperLeft + 5f, paperTop + 5f, paperW, paperH);
            }
            g.FillRectangle(Brushes.White, paperRect);
            using (var paperBorderPen = new Pen(Color.FromArgb(200, 200, 210), 1f))
            {
                g.DrawRectangle(paperBorderPen, paperRect.X, paperRect.Y, paperRect.Width, paperRect.Height);
            }

            // 2. 外枠の描画 (余白: 左 MarginLeftMm, 他 MarginOtherMm)
            float outerLeft = paperLeft + (float)(MarginLeftMm * zoom);
            float outerTop = paperTop + (float)(MarginOtherMm * zoom);
            float outerW = paperW - (float)((MarginLeftMm + MarginOtherMm) * zoom);
            float outerH = paperH - (float)((MarginOtherMm * 2.0) * zoom);
            RectangleF outerRect = new RectangleF(outerLeft, outerTop, outerW, outerH);

            using (var outerPen = new Pen(Color.FromArgb(20, 20, 20), 1.2f))
            using (var thickPen = new Pen(Color.FromArgb(20, 20, 20), 2.8f))
            {
                g.DrawRectangle(outerPen, outerRect.X, outerRect.Y, outerRect.Width, outerRect.Height);
                // 下辺と右辺を太線で描画
                g.DrawLine(thickPen, outerRect.Left, outerRect.Bottom, outerRect.Right, outerRect.Bottom);
                g.DrawLine(thickPen, outerRect.Right, outerRect.Top, outerRect.Right, outerRect.Bottom);
            }

            // 3. 内枠の描画 (外枠から OuterInnerSpacingMm 内側)
            float spacingPx = (float)(OuterInnerSpacingMm * zoom);
            float innerLeft = outerLeft + spacingPx;
            float innerTop = outerTop + spacingPx;
            float innerW = Math.Max(10f, outerW - spacingPx * 2f);
            float innerH = Math.Max(10f, outerH - spacingPx * 2f);
            RectangleF innerRect = new RectangleF(innerLeft, innerTop, innerW, innerH);

            using (var innerPen = new Pen(Color.FromArgb(0, 100, 180), 1.2f))
            {
                g.DrawRectangle(innerPen, innerRect.X, innerRect.Y, innerRect.Width, innerRect.Height);
            }

            // 4. トンボ (+) & 外枠・内枠間の座標値の計算・描画
            double pitch = GetEffectivePitchMeters();
            var innerCorners = GetInnerCornersSurvey();

            // 内枠の測量座標範囲（内枠に格子線が表示される範囲を基準とする）
            double minSurX = double.MaxValue, maxSurX = double.MinValue;
            double minSurY = double.MaxValue, maxSurY = double.MinValue;
            foreach (var corner in innerCorners)
            {
                minSurX = Math.Min(minSurX, corner.surveyX);
                maxSurX = Math.Max(maxSurX, corner.surveyX);
                minSurY = Math.Min(minSurY, corner.surveyY);
                maxSurY = Math.Max(maxSurY, corner.surveyY);
            }

            double startSurX = Math.Floor(minSurX / pitch) * pitch;
            double endSurX = Math.Ceiling(maxSurX / pitch) * pitch;
            double startSurY = Math.Floor(minSurY / pitch) * pitch;
            double endSurY = Math.Ceiling(maxSurY / pitch) * pitch;

            // 5. 内枠内クリッピングで図面要素を描画
            var oldClip = g.Clip;
            g.SetClip(innerRect);

            // A. 格子線 (Grid Lines) の描画（内枠クリッピング内で端から端まで完全に通過）
            using (var tomboPen = new Pen(Color.FromArgb(160, 160, 160), 0.9f))
            using (var gridPen = new Pen(Color.FromArgb(190, 190, 190), 0.8f) { DashStyle = DashStyle.Dash })
            {
                if (ShowGridLines)
                {
                    // X一定線（東西方向）
                    for (double sx = startSurX; sx <= endSurX + 0.001; sx += pitch)
                    {
                        PointF pStart = SurveyToPaperScreen(sx, startSurY);
                        PointF pEnd = SurveyToPaperScreen(sx, endSurY);
                        g.DrawLine(gridPen, pStart, pEnd);
                    }

                    // Y一定線（南北方向）
                    for (double sy = startSurY; sy <= endSurY + 0.001; sy += pitch)
                    {
                        PointF pStart = SurveyToPaperScreen(startSurX, sy);
                        PointF pEnd = SurveyToPaperScreen(endSurX, sy);
                        g.DrawLine(gridPen, pStart, pEnd);
                    }
                }

                // トンボ (+) の描画
                if (ShowTombo)
                {
                    for (double sx = startSurX; sx <= endSurX + 0.001; sx += pitch)
                    {
                        for (double sy = startSurY; sy <= endSurY + 0.001; sy += pitch)
                        {
                            PointF pt = SurveyToPaperScreen(sx, sy);
                            if (innerRect.Contains(pt))
                            {
                                float arm = 4.5f;
                                PointF ptN = SurveyToPaperScreen(sx + (pitch * 0.05), sy);
                                PointF ptE = SurveyToPaperScreen(sx, sy + (pitch * 0.05));

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
            }

            // B. 遺構線 (Ikou Lines) の描画
            if (showIkou || showHyoukou)
            {
                var spline = new Xross_Spline();
                foreach (var line in db.IkouLList)
                {
                    int layerIdx = line.Layer >= 49 ? (line.Layer - 48) : line.Layer;
                    if (isMapLayerVisible != null && !isMapLayerVisible(layerIdx)) continue;

                    var pts = SqliteManager.ParsePrecsText(line.Precs);
                    if (pts.Count == 0) continue;

                    int lineDbLayerId = line.Layer >= 49 ? line.Layer : (line.Layer + 48);
                    Color color = colorByIkou
                        ? EditorLayerService.PaletteColors[(int)(line.Id % EditorLayerService.PaletteColors.Length)]
                        : EditorLayerService.GetLayerColor(lineDbLayerId, db.LayerList, false);

                    if (line.Mode == 2)
                    {
                        if (showIkou)
                        {
                            using (var ptBrush = new SolidBrush(color))
                            {
                                foreach (var p in pts)
                                {
                                    PointF sp = SurveyToPaperScreen(p.X, p.Y);
                                    g.FillEllipse(ptBrush, sp.X - 1.5f, sp.Y - 1.5f, 3f, 3f);
                                }
                            }
                        }
                        if (showHyoukou)
                        {
                            using (var zFont = new Font("Yu Gothic UI", 6F, FontStyle.Regular))
                            using (var zBrush = new SolidBrush(Color.FromArgb(30, 100, 30)))
                            {
                                foreach (var p in pts)
                                {
                                    PointF sp = SurveyToPaperScreen(p.X, p.Y);
                                    g.DrawString(p.Z.ToString("0.000"), zFont, zBrush, sp.X + 2f, sp.Y - 4f);
                                }
                            }
                        }
                        continue;
                    }

                    if (!showIkou) continue;

                    var layer = db.LayerList.FirstOrDefault(l => l.Id == lineDbLayerId);
                    bool isLayerCurve = (layer != null) ? (layer.LType == 2) : true;
                    bool drawAsCurve = showCurve && isLayerCurve && pts.Count >= 3;

                    PointF[] screenPts;
                    if (drawAsCurve)
                    {
                        var curvePoints = (line.Mode == 1)
                            ? spline.Calc3DCloseCurvePoints(pts, 5)
                            : spline.Calc3DCurvePoints(pts, 5);
                        screenPts = curvePoints.Select(p => SurveyToPaperScreen(p.X, p.Y)).ToArray();
                    }
                    else
                    {
                        screenPts = pts.Select(p => SurveyToPaperScreen(p.X, p.Y)).ToArray();
                    }

                    if (screenPts.Length > 1)
                    {
                        float penWidth = (layer != null && layer.Width > 0) ? (float)layer.Width : 1.2f;
                        using (var linePen = new Pen(color, penWidth))
                        {
                            g.DrawLines(linePen, screenPts);
                            if (line.Mode == 1 && screenPts.Length >= 3)
                            {
                                g.DrawLine(linePen, screenPts[screenPts.Length - 1], screenPts[0]);
                            }
                        }
                    }

                    // 頂点マーク
                    using (var vBrush = new SolidBrush(color))
                    {
                        foreach (var p in pts)
                        {
                            PointF sp = SurveyToPaperScreen(p.X, p.Y);
                            g.FillRectangle(vBrush, sp.X - 1f, sp.Y - 1f, 2f, 2f);
                        }
                    }

                    // 遺構線名ラベル
                    if (showIkouName && !string.IsNullOrEmpty(line.Name))
                    {
                        PointF labelPt = (line.X != 0 || line.Y != 0)
                            ? SurveyToPaperScreen(line.X, line.Y)
                            : SurveyToPaperScreen(pts[0].X, pts[0].Y);

                        using (var lFont = new Font("Yu Gothic UI", 7F, FontStyle.Bold))
                        using (var lBrush = new SolidBrush(Color.Black))
                        using (var bgBrush = new SolidBrush(Color.FromArgb(200, 255, 255, 255)))
                        {
                            var sz = g.MeasureString(line.Name, lFont);
                            g.FillRectangle(bgBrush, labelPt.X - 1f, labelPt.Y - 1f, sz.Width + 2f, sz.Height + 2f);
                            g.DrawString(line.Name, lFont, lBrush, labelPt.X, labelPt.Y);
                        }
                    }
                }
            }

            // C. 遺物 (Ibutu Points) の描画
            if (showIbutu)
            {
                using (var ibutuPen = new Pen(Color.Red, 1.2f))
                using (var ibutuFont = new Font("Yu Gothic UI", 6.5F, FontStyle.Bold))
                using (var textBrush = new SolidBrush(Color.DarkRed))
                {
                    foreach (var ibutu in db.IbutuList)
                    {
                        PointF sp = SurveyToPaperScreen(ibutu.X, ibutu.Y);
                        g.DrawEllipse(ibutuPen, sp.X - 2.5f, sp.Y - 2.5f, 5f, 5f);
                        g.DrawLine(ibutuPen, sp.X - 4f, sp.Y, sp.X + 4f, sp.Y);
                        g.DrawLine(ibutuPen, sp.X, sp.Y - 4f, sp.X, sp.Y + 4f);

                        if (showIbutuName)
                        {
                            string ibName = !string.IsNullOrEmpty(ibutu.Syubetu)
                                ? (ibutu.No > 0 ? $"{ibutu.Syubetu}{ibutu.No}" : ibutu.Syubetu)
                                : (ibutu.No > 0 ? $"No.{ibutu.No}" : $"遺物{ibutu.Id}");

                            g.DrawString(ibName, ibutuFont, textBrush, sp.X + 4f, sp.Y - 4f);
                        }
                    }
                }
            }

            // D. 基準点・機械点 (Kikai Points) の描画
            if (showKikai)
            {
                using (var kikaiBrush = new SolidBrush(Color.FromArgb(0, 120, 215)))
                using (var kikaiFont = new Font("Yu Gothic UI", 7F, FontStyle.Bold))
                using (var textBrush = new SolidBrush(Color.FromArgb(0, 70, 140)))
                {
                    foreach (var k in db.KikaiList)
                    {
                        PointF sp = SurveyToPaperScreen(k.X, k.Y);
                        PointF[] tri = new PointF[] {
                            new PointF(sp.X, sp.Y - 4f),
                            new PointF(sp.X - 3.5f, sp.Y + 3f),
                            new PointF(sp.X + 3.5f, sp.Y + 3f)
                        };
                        g.FillPolygon(kikaiBrush, tri);

                        if (showKikaiName && !string.IsNullOrEmpty(k.Name))
                        {
                            g.DrawString(k.Name, kikaiFont, textBrush, sp.X + 4f, sp.Y - 4f);
                        }
                    }
                }
            }

            // クリッピング解除
            g.Clip = oldClip;

            // 6. 外枠・内枠間の座標表記（X=...m, Y=...m）
            if (ShowBorderCoords)
            {
                using (var coordFont = new Font("Yu Gothic UI", 7F, FontStyle.Bold))
                using (var coordBrush = new SolidBrush(Color.FromArgb(40, 40, 50)))
                using (var coordBgBrush = new SolidBrush(Color.FromArgb(230, 255, 255, 255)))
                {
                    var (outerWM, outerHM, offsetEastM, offsetNorthM) = GetOuterFrameDimensionsMeters();
                    var (innerWM, innerHM, _, _) = GetInnerFrameDimensionsMeters();

                    double uMinOuter = offsetEastM - outerWM / 2.0;
                    double uMaxOuter = offsetEastM + outerWM / 2.0;
                    double vMinOuter = offsetNorthM - outerHM / 2.0;
                    double vMaxOuter = offsetNorthM + outerHM / 2.0;

                    double uMinInner = offsetEastM - innerWM / 2.0;
                    double uMaxInner = offsetEastM + innerWM / 2.0;
                    double vMinInner = offsetNorthM - innerHM / 2.0;
                    double vMaxInner = offsetNorthM + innerHM / 2.0;

                    double uMidLeft = (uMinOuter + uMinInner) / 2.0;
                    double uMidRight = (uMaxOuter + uMaxInner) / 2.0;
                    double vMidBottom = (vMinOuter + vMinInner) / 2.0;
                    double vMidTop = (vMaxOuter + vMaxInner) / 2.0;

                    for (double sx = startSurX; sx <= endSurX + 0.001; sx += pitch)
                    {
                        DrawSingleCoordinateLabelPaper(g, sx, true, uMidLeft, uMidRight, vMidBottom, vMidTop, uMinInner, uMaxInner, vMinInner, vMaxInner, coordFont, coordBrush, coordBgBrush, SurveyToPaperScreen);
                    }
                    for (double sy = startSurY; sy <= endSurY + 0.001; sy += pitch)
                    {
                        DrawSingleCoordinateLabelPaper(g, sy, false, uMidLeft, uMidRight, vMidBottom, vMidTop, uMinInner, uMaxInner, vMinInner, vMaxInner, coordFont, coordBrush, coordBgBrush, SurveyToPaperScreen);
                    }
                }
            }

            // 7. 方位記号 (North Arrow) の描画
            if (ShowNorthArrow)
            {
                PointF anchor;
                if (NorthArrowPosition == "カスタム" && HasCustomNorthArrowPos)
                {
                    anchor = SurveyToPaperScreen(NorthArrowCustomSurveyX, NorthArrowCustomSurveyY);
                }
                else
                {
                    PointF[] innerCornersScreen = new PointF[] {
                        new PointF(innerRect.Left, innerRect.Bottom),
                        new PointF(innerRect.Right, innerRect.Bottom),
                        new PointF(innerRect.Right, innerRect.Top),
                        new PointF(innerRect.Left, innerRect.Top)
                    };
                    anchor = GetCornerPoint(innerCornersScreen, NorthArrowPosition, 26f);
                }

                float length = (float)Math.Max(14.0, NorthArrowSizeMm * zoom * 1.3);
                float width = length * 0.28f;
                float needleRad = (float)(-RotationAngleDeg * Math.PI / 180.0);
                float cos = (float)Math.Cos(needleRad);
                float sin = (float)Math.Sin(needleRad);

                PointF tip = new PointF(anchor.X - sin * length, anchor.Y - cos * length);
                PointF tail = new PointF(anchor.X + sin * (length * 0.35f), anchor.Y + cos * (length * 0.35f));
                PointF leftWing = new PointF(anchor.X - cos * width + sin * (length * 0.1f), anchor.Y + sin * width + cos * (length * 0.1f));
                PointF rightWing = new PointF(anchor.X + cos * width + sin * (length * 0.1f), anchor.Y - sin * width + cos * (length * 0.1f));

                using (var blackBrush = new SolidBrush(Color.Black))
                using (var whiteBrush = new SolidBrush(Color.White))
                using (var outlinePen = new Pen(Color.Black, 1.2f))
                using (var font = new Font("Arial", 8F, FontStyle.Bold))
                {
                    g.FillPolygon(blackBrush, new PointF[] { tip, leftWing, tail });
                    g.FillPolygon(whiteBrush, new PointF[] { tip, rightWing, tail });
                    g.DrawPolygon(outlinePen, new PointF[] { tip, leftWing, tail, rightWing });

                    string nStr = "N";
                    var nSz = g.MeasureString(nStr, font);
                    PointF nPos = new PointF(tip.X - sin * 9f - (nSz.Width / 2f), tip.Y - cos * 9f - (nSz.Height / 2f));
                    g.DrawString(nStr, font, blackBrush, nPos);
                }
            }

            // 8. スケールバー (Scale Bar) の描画
            if (ShowScaleBar)
            {
                PointF anchor;
                if (ScaleBarPosition == "中下")
                {
                    anchor = new PointF((innerRect.Left + innerRect.Right) / 2f, innerRect.Bottom - 18f);
                }
                else
                {
                    anchor = new PointF(innerRect.Right - 55f, innerRect.Bottom - 18f);
                }

                double barMeters = (Scale >= 500) ? 50.0 : (Scale >= 200) ? 20.0 : 10.0;
                double halfBarMeters = barMeters / 2.0;
                double barMm = (barMeters / Scale) * 1000.0;
                float barPixelWidth = (float)(barMm * zoom);
                if (barPixelWidth < 30f || barPixelWidth > 300f) barPixelWidth = 80f;

                float barHeight = 4.5f;
                float leftX = anchor.X - (barPixelWidth / 2f);
                float topY = anchor.Y;

                using (var primaryBrush = new SolidBrush(Color.Black))
                using (var secondaryBrush = new SolidBrush(Color.White))
                using (var pen = new Pen(Color.Black, 1.1f))
                using (var font = new Font("Yu Gothic UI", 7F, FontStyle.Bold))
                {
                    float midX = leftX + (barPixelWidth / 2f);
                    g.FillRectangle(primaryBrush, leftX, topY, barPixelWidth / 2f, barHeight);
                    g.FillRectangle(secondaryBrush, midX, topY, barPixelWidth / 2f, barHeight);
                    g.DrawRectangle(pen, leftX, topY, barPixelWidth, barHeight);
                    g.DrawLine(pen, midX, topY, midX, topY + barHeight);

                    g.DrawString("0", font, primaryBrush, leftX - 3f, topY - 12f);
                    g.DrawString($"{halfBarMeters:0}", font, primaryBrush, midX - 5f, topY - 12f);
                    g.DrawString($"{barMeters:0}m (1/{Scale:0})", font, primaryBrush, leftX + barPixelWidth - 10f, topY - 12f);
                }
            }

            // 9. 用紙下部のインフォメーションラベル
            string docInfo = $"【図面出力イメージ】{PaperSizeName} {(IsLandscape ? "横" : "縦")} 1/{Scale:0} (回転 {RotationAngleDeg:0.0}°)   ピッチ: {pitch:0.#}m";
            using (var docFont = new Font("Yu Gothic UI", 8F, FontStyle.Bold))
            using (var docBrush = new SolidBrush(Color.FromArgb(180, 200, 220)))
            {
                g.DrawString(docInfo, docFont, docBrush, paperLeft + 4f, paperTop + paperH + 4f);
            }
        }

        private void DrawSingleCoordinateLabelPaper(Graphics g, double val, bool isXAxis, double uMidLeft, double uMidRight, double vMidBottom, double vMidTop, double uMinInner, double uMaxInner, double vMinInner, double vMaxInner, Font font, Brush textBrush, Brush bgBrush, Func<double, double, PointF> surveyToScreen)
        {
            double rad = RotationAngleDeg * Math.PI / 180.0;
            double cos = Math.Cos(rad);
            double sin = Math.Sin(rad);

            string numStr = (Math.Abs(val % 1.0) < 1e-4) ? $"{val:0}m" : $"{val:0.00}m";
            string labelText = isXAxis ? $"X={numStr}" : $"Y={numStr}";
            var intersections = new System.Collections.Generic.List<(double surX, double surY)>();

            if (isXAxis)
            {
                double dNorth = val - CenterX;
                // 1. 内枠下辺 (v = vMinInner) と交差するか判定 -> 中間線 (v = vMidBottom) 上の点
                if (Math.Abs(sin) > 1e-6)
                {
                    double uEdge = (vMinInner * cos - dNorth) / sin;
                    if (uEdge >= uMinInner - 0.01 && uEdge <= uMaxInner + 0.01)
                    {
                        double uMid = (vMidBottom * cos - dNorth) / sin;
                        double dEastMid = vMidBottom * sin + uMid * cos;
                        intersections.Add((val, CenterY + dEastMid));
                    }
                }
                // 2. 内枠上辺 (v = vMaxInner) と交差するか判定 -> 中間線 (v = vMidTop) 上の点
                if (Math.Abs(sin) > 1e-6)
                {
                    double uEdge = (vMaxInner * cos - dNorth) / sin;
                    if (uEdge >= uMinInner - 0.01 && uEdge <= uMaxInner + 0.01)
                    {
                        double uMid = (vMidTop * cos - dNorth) / sin;
                        double dEastMid = vMidTop * sin + uMid * cos;
                        intersections.Add((val, CenterY + dEastMid));
                    }
                }
                // 3. 内枠左辺 (u = uMinInner) と交差するか判定 -> 中間線 (u = uMidLeft) 上の点
                if (Math.Abs(cos) > 1e-6)
                {
                    double vEdge = (dNorth + uMinInner * sin) / cos;
                    if (vEdge >= vMinInner - 0.01 && vEdge <= vMaxInner + 0.01)
                    {
                        double vMid = (dNorth + uMidLeft * sin) / cos;
                        double dEastMid = vMid * sin + uMidLeft * cos;
                        intersections.Add((val, CenterY + dEastMid));
                    }
                }
                // 4. 内枠右辺 (u = uMaxInner) と交差するか判定 -> 中間線 (u = uMidRight) 上の点
                if (Math.Abs(cos) > 1e-6)
                {
                    double vEdge = (dNorth + uMaxInner * sin) / cos;
                    if (vEdge >= vMinInner - 0.01 && vEdge <= vMaxInner + 0.01)
                    {
                        double vMid = (dNorth + uMidRight * sin) / cos;
                        double dEastMid = vMid * sin + uMidRight * cos;
                        intersections.Add((val, CenterY + dEastMid));
                    }
                }
            }
            else
            {
                double dEast = val - CenterY;
                // 1. 内枠下辺 (v = vMinInner) と交差するか判定 -> 中間線 (v = vMidBottom) 上の点
                if (Math.Abs(cos) > 1e-6)
                {
                    double uEdge = (dEast - vMinInner * sin) / cos;
                    if (uEdge >= uMinInner - 0.01 && uEdge <= uMaxInner + 0.01)
                    {
                        double uMid = (dEast - vMidBottom * sin) / cos;
                        double dNorthMid = vMidBottom * cos - uMid * sin;
                        intersections.Add((CenterX + dNorthMid, val));
                    }
                }
                // 2. 内枠上辺 (v = vMaxInner) と交差するか判定 -> 中間線 (v = vMidTop) 上の点
                if (Math.Abs(cos) > 1e-6)
                {
                    double uEdge = (dEast - vMaxInner * sin) / cos;
                    if (uEdge >= uMinInner - 0.01 && uEdge <= uMaxInner + 0.01)
                    {
                        double uMid = (dEast - vMidTop * sin) / cos;
                        double dNorthMid = vMidTop * cos - uMid * sin;
                        intersections.Add((CenterX + dNorthMid, val));
                    }
                }
                // 3. 内枠左辺 (u = uMinInner) と交差するか判定 -> 中間線 (u = uMidLeft) 上の点
                if (Math.Abs(sin) > 1e-6)
                {
                    double vEdge = (dEast - uMinInner * cos) / sin;
                    if (vEdge >= vMinInner - 0.01 && vEdge <= vMaxInner + 0.01)
                    {
                        double vMid = (dEast - uMidLeft * cos) / sin;
                        double dNorthMid = vMid * cos - uMidLeft * sin;
                        intersections.Add((CenterX + dNorthMid, val));
                    }
                }
                // 4. 内枠右辺 (u = uMaxInner) と交差するか判定 -> 中間線 (u = uMidRight) 上の点
                if (Math.Abs(sin) > 1e-6)
                {
                    double vEdge = (dEast - uMaxInner * cos) / sin;
                    if (vEdge >= vMinInner - 0.01 && vEdge <= vMaxInner + 0.01)
                    {
                        double vMid = (dEast - uMidRight * cos) / sin;
                        double dNorthMid = vMid * cos - uMidRight * sin;
                        intersections.Add((CenterX + dNorthMid, val));
                    }
                }
            }

            // 用紙プレビュー上での格子線の傾きに合わせて文字を回転 (-90°〜+90°に正規化)
            float rotDeg;
            if (isXAxis)
            {
                // X一定線: 測量東(Y)方向に伸びる
                PointF p0 = surveyToScreen(val, CenterY);
                PointF p1 = surveyToScreen(val, CenterY + 10.0);
                float dx = p1.X - p0.X;
                float dy = p1.Y - p0.Y;
                rotDeg = NormalizeTextAngle((float)(Math.Atan2(dy, dx) * 180.0 / Math.PI));
            }
            else
            {
                // Y一定線: 測量北(X)方向に伸びる
                PointF p0 = surveyToScreen(CenterX, val);
                PointF p1 = surveyToScreen(CenterX + 10.0, val);
                float dx = p1.X - p0.X;
                float dy = p1.Y - p0.Y;
                rotDeg = NormalizeTextAngle((float)(Math.Atan2(dy, dx) * 180.0 / Math.PI));
            }

            foreach (var (surX, surY) in intersections)
            {
                PointF pt = surveyToScreen(surX, surY);
                var sz = g.MeasureString(labelText, font);

                var state = g.Save();
                g.TranslateTransform(pt.X, pt.Y);
                g.RotateTransform(rotDeg);

                // 背景は透明（文字のみ描画）
                g.DrawString(labelText, font, textBrush, -sz.Width / 2f, -sz.Height / 2f);
                g.Restore(state);
            }
        }

        /// <summary>
        /// INIファイルから図枠設定を読み込み
        /// </summary>
        public void LoadFromIni()
        {
            try
            {
                IsVisible = Def.GetIniInt("DRAWING_FRAME", "Visible", 1) == 1;
                IsDrawingPreviewEnabled = Def.GetIniInt("DRAWING_FRAME", "IsDrawingPreviewEnabled", 0) == 1;
                PaperSizeName = Def.GetIniStr("DRAWING_FRAME", "PaperSizeName");
                if (string.IsNullOrEmpty(PaperSizeName)) PaperSizeName = "A3";
                IsLandscape = Def.GetIniInt("DRAWING_FRAME", "IsLandscape", 1) == 1;
                Scale = Def.GetIniDouble("DRAWING_FRAME", "Scale", 200.0);
                CenterX = Def.GetIniDouble("DRAWING_FRAME", "CenterX", 0.0);
                CenterY = Def.GetIniDouble("DRAWING_FRAME", "CenterY", 0.0);
                RotationAngleDeg = Def.GetIniDouble("DRAWING_FRAME", "RotationAngleDeg", 0.0);

                ShowTombo = Def.GetIniInt("DRAWING_FRAME", "ShowTombo", 1) == 1;
                ShowGridLines = Def.GetIniInt("DRAWING_FRAME", "ShowGridLines", 0) == 1;
                IsPitchAuto = Def.GetIniInt("DRAWING_FRAME", "IsPitchAuto", 1) == 1;
                PitchMeters = Def.GetIniDouble("DRAWING_FRAME", "PitchMeters", 20.0);
                ShowBorderCoords = Def.GetIniInt("DRAWING_FRAME", "ShowBorderCoords", 1) == 1;

                ShowNorthArrow = Def.GetIniInt("DRAWING_FRAME", "ShowNorthArrow", 1) == 1;
                NorthArrowSizeMm = Def.GetIniDouble("DRAWING_FRAME", "NorthArrowSizeMm", 15.0);
                NorthArrowPosition = Def.GetIniStr("DRAWING_FRAME", "NorthArrowPosition");
                if (string.IsNullOrEmpty(NorthArrowPosition)) NorthArrowPosition = "右上";
                NorthArrowCustomSurveyX = Def.GetIniDouble("DRAWING_FRAME", "NorthArrowCustomSurveyX", 0.0);
                NorthArrowCustomSurveyY = Def.GetIniDouble("DRAWING_FRAME", "NorthArrowCustomSurveyY", 0.0);
                HasCustomNorthArrowPos = Def.GetIniInt("DRAWING_FRAME", "HasCustomNorthArrowPos", 0) == 1;

                ShowScaleBar = Def.GetIniInt("DRAWING_FRAME", "ShowScaleBar", 1) == 1;
                ScaleBarPosition = Def.GetIniStr("DRAWING_FRAME", "ScaleBarPosition");
                if (string.IsNullOrEmpty(ScaleBarPosition)) ScaleBarPosition = "中下";

                MarginLeftMm = Def.GetIniDouble("DRAWING_FRAME", "MarginLeftMm", 20.0);
                MarginOtherMm = Def.GetIniDouble("DRAWING_FRAME", "MarginOtherMm", 10.0);
                OuterInnerSpacingMm = Def.GetIniDouble("DRAWING_FRAME", "OuterInnerSpacingMm", 10.0);
            }
            catch { }
        }

        /// <summary>
        /// INIファイルへ図枠設定を保存
        /// </summary>
        public void SaveToIni()
        {
            try
            {
                Def.SetIniInt("DRAWING_FRAME", "Visible", IsVisible ? 1 : 0);
                Def.SetIniInt("DRAWING_FRAME", "IsDrawingPreviewEnabled", IsDrawingPreviewEnabled ? 1 : 0);
                Def.SetIniStr("DRAWING_FRAME", "PaperSizeName", PaperSizeName);
                Def.SetIniInt("DRAWING_FRAME", "IsLandscape", IsLandscape ? 1 : 0);
                Def.SetIniDouble("DRAWING_FRAME", "Scale", Scale);
                Def.SetIniDouble("DRAWING_FRAME", "CenterX", CenterX);
                Def.SetIniDouble("DRAWING_FRAME", "CenterY", CenterY);
                Def.SetIniDouble("DRAWING_FRAME", "RotationAngleDeg", RotationAngleDeg);

                Def.SetIniInt("DRAWING_FRAME", "ShowTombo", ShowTombo ? 1 : 0);
                Def.SetIniInt("DRAWING_FRAME", "ShowGridLines", ShowGridLines ? 1 : 0);
                Def.SetIniInt("DRAWING_FRAME", "IsPitchAuto", IsPitchAuto ? 1 : 0);
                Def.SetIniDouble("DRAWING_FRAME", "PitchMeters", PitchMeters);
                Def.SetIniInt("DRAWING_FRAME", "ShowBorderCoords", ShowBorderCoords ? 1 : 0);

                Def.SetIniInt("DRAWING_FRAME", "ShowNorthArrow", ShowNorthArrow ? 1 : 0);
                Def.SetIniDouble("DRAWING_FRAME", "NorthArrowSizeMm", NorthArrowSizeMm);
                Def.SetIniStr("DRAWING_FRAME", "NorthArrowPosition", NorthArrowPosition);
                Def.SetIniDouble("DRAWING_FRAME", "NorthArrowCustomSurveyX", NorthArrowCustomSurveyX);
                Def.SetIniDouble("DRAWING_FRAME", "NorthArrowCustomSurveyY", NorthArrowCustomSurveyY);
                Def.SetIniInt("DRAWING_FRAME", "HasCustomNorthArrowPos", HasCustomNorthArrowPos ? 1 : 0);

                Def.SetIniInt("DRAWING_FRAME", "ShowScaleBar", ShowScaleBar ? 1 : 0);
                Def.SetIniStr("DRAWING_FRAME", "ScaleBarPosition", ScaleBarPosition);

                Def.SetIniDouble("DRAWING_FRAME", "MarginLeftMm", MarginLeftMm);
                Def.SetIniDouble("DRAWING_FRAME", "MarginOtherMm", MarginOtherMm);
                Def.SetIniDouble("DRAWING_FRAME", "OuterInnerSpacingMm", OuterInnerSpacingMm);
            }
            catch { }
        }
    }
}
