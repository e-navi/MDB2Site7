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
        public string NorthArrowType { get; set; } = "標準矢印"; // 方位記号の種類 (標準矢印, シンプル, 円形コンパス, モダン)
        public double NorthArrowSizeMm { get; set; } = 15.0; // 方位記号サイズ（mm）
        public string NorthArrowPosition { get; set; } = "右上"; // 右上, 左上, 右下, 左下, カスタム
        public double NorthArrowCustomSurveyX { get; set; } = 0.0; // カスタム指定時の測量X
        public double NorthArrowCustomSurveyY { get; set; } = 0.0; // カスタム指定時の測量Y
        public bool HasCustomNorthArrowPos { get; set; } = false;

        public bool ShowScaleBar { get; set; } = true;      // スケールバー表示
        public string ScaleBarType { get; set; } = "精密線 (下縮尺)"; // スケールバー種類 (精密線 (下縮尺), ブロック, シンプル線, 二重枠, 目盛付き)
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
        /// 方位記号（北矢印）の描画
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

            // CADマップ上では常に北が画面上向き（真上）
            DrawNorthArrowCore(g, anchor, 0f, length, width, NorthArrowType, isDarkBackground, 1.2f, 8.5f);
        }

        /// <summary>
        /// 多様なデザインの方位記号（北矢印）を描画する共通メソッド
        /// </summary>
        private void DrawNorthArrowCore(Graphics g, PointF anchor, float needleRad, float length, float width, string style, bool isDarkBackground, float penWidth, float fontPt)
        {
            float cos = (float)Math.Cos(needleRad);
            float sin = (float)Math.Sin(needleRad);

            Color fg = isDarkBackground ? Color.White : Color.Black;
            Color bg = isDarkBackground ? Color.FromArgb(120, 130, 150) : Color.White;

            using (var blackBrush = new SolidBrush(fg))
            using (var whiteBrush = new SolidBrush(bg))
            using (var outlinePen = new Pen(fg, penWidth))
            using (var font = new Font("Arial", fontPt, FontStyle.Bold, GraphicsUnit.Pixel))
            {
                string nStr = "N";
                var nSz = g.MeasureString(nStr, font);

                if (style == "シンプル")
                {
                    // 細身の直線針＋鋭角矢じり
                    PointF tip = new PointF(anchor.X - sin * length, anchor.Y - cos * length);
                    PointF tail = new PointF(anchor.X + sin * (length * 0.35f), anchor.Y + cos * (length * 0.35f));
                    PointF headLeft = new PointF(tip.X + sin * (length * 0.45f) - cos * (width * 0.7f), tip.Y + cos * (length * 0.45f) + sin * (width * 0.7f));
                    PointF headRight = new PointF(tip.X + sin * (length * 0.45f) + cos * (width * 0.7f), tip.Y + cos * (length * 0.45f) - sin * (width * 0.7f));
                    PointF headCenter = new PointF(tip.X + sin * (length * 0.35f), tip.Y + cos * (length * 0.35f));

                    g.DrawLine(outlinePen, tip, tail);
                    g.FillPolygon(blackBrush, new PointF[] { tip, headLeft, headCenter });
                    g.FillPolygon(whiteBrush, new PointF[] { tip, headRight, headCenter });
                    g.DrawPolygon(outlinePen, new PointF[] { tip, headLeft, headCenter, headRight });

                    PointF nPos = new PointF(tip.X - sin * (fontPt * 1.2f) - (nSz.Width / 2f), tip.Y - cos * (fontPt * 1.2f) - (nSz.Height / 2f));
                    g.DrawString(nStr, font, blackBrush, nPos);
                }
                else if (style == "円形コンパス")
                {
                    // 円形枠＋十字＋北針
                    float radius = length * 0.45f;
                    g.DrawEllipse(outlinePen, anchor.X - radius, anchor.Y - radius, radius * 2f, radius * 2f);

                    PointF east = new PointF(anchor.X + cos * radius, anchor.Y - sin * radius);
                    PointF west = new PointF(anchor.X - cos * radius, anchor.Y + sin * radius);
                    PointF south = new PointF(anchor.X + sin * radius, anchor.Y + cos * radius);
                    g.DrawLine(outlinePen, east, west);
                    g.DrawLine(outlinePen, anchor, south);

                    PointF tip = new PointF(anchor.X - sin * length, anchor.Y - cos * length);
                    PointF leftWing = new PointF(anchor.X - cos * (width * 0.6f), anchor.Y + sin * (width * 0.6f));
                    PointF rightWing = new PointF(anchor.X + cos * (width * 0.6f), anchor.Y - sin * (width * 0.6f));

                    g.FillPolygon(blackBrush, new PointF[] { tip, leftWing, anchor });
                    g.FillPolygon(whiteBrush, new PointF[] { tip, rightWing, anchor });
                    g.DrawPolygon(outlinePen, new PointF[] { tip, leftWing, anchor, rightWing });

                    PointF nPos = new PointF(tip.X - sin * (fontPt * 1.2f) - (nSz.Width / 2f), tip.Y - cos * (fontPt * 1.2f) - (nSz.Height / 2f));
                    g.DrawString(nStr, font, blackBrush, nPos);
                }
                else if (style == "モダン")
                {
                    // シャープなモダン矢印（抜きのあるスマートデザイン）
                    PointF tip = new PointF(anchor.X - sin * length, anchor.Y - cos * length);
                    PointF tail = new PointF(anchor.X + sin * (length * 0.15f), anchor.Y + cos * (length * 0.15f));
                    PointF leftWing = new PointF(anchor.X - cos * (width * 0.9f) + sin * (length * 0.35f), anchor.Y + sin * (width * 0.9f) + cos * (length * 0.35f));
                    PointF rightWing = new PointF(anchor.X + cos * (width * 0.9f) + sin * (length * 0.35f), anchor.Y - sin * (width * 0.9f) + cos * (length * 0.35f));

                    g.FillPolygon(blackBrush, new PointF[] { tip, leftWing, tail });
                    g.FillPolygon(whiteBrush, new PointF[] { tip, rightWing, tail });
                    g.DrawPolygon(outlinePen, new PointF[] { tip, leftWing, tail, rightWing });

                    PointF nPos = new PointF(tip.X - sin * (fontPt * 1.2f) - (nSz.Width / 2f), tip.Y - cos * (fontPt * 1.2f) - (nSz.Height / 2f));
                    g.DrawString(nStr, font, blackBrush, nPos);
                }
                else // "標準矢印" (デフォルト)
                {
                    PointF tip = new PointF(anchor.X - sin * length, anchor.Y - cos * length);
                    PointF tail = new PointF(anchor.X + sin * (length * 0.35f), anchor.Y + cos * (length * 0.35f));
                    PointF leftWing = new PointF(anchor.X - cos * width + sin * (length * 0.1f), anchor.Y + sin * width + cos * (length * 0.1f));
                    PointF rightWing = new PointF(anchor.X + cos * width + sin * (length * 0.1f), anchor.Y - sin * width + cos * (length * 0.1f));

                    g.FillPolygon(blackBrush, new PointF[] { tip, leftWing, tail });
                    g.FillPolygon(whiteBrush, new PointF[] { tip, rightWing, tail });
                    g.DrawPolygon(outlinePen, new PointF[] { tip, leftWing, tail, rightWing });

                    PointF nPos = new PointF(tip.X - sin * (fontPt * 1.2f) - (nSz.Width / 2f), tip.Y - cos * (fontPt * 1.2f) - (nSz.Height / 2f));
                    g.DrawString(nStr, font, blackBrush, nPos);
                }
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
                // 右下 (内枠に近づける)
                anchor = GetCornerPoint(innerScreen, "右下", 14f);
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
            g.TranslateTransform(0, -14f); // 枠の内側へオフセット (下部縮尺文字が入る適度な余白)

            using (var font = new Font("Yu Gothic UI", 7.5F, FontStyle.Bold))
            {
                float leftX = -barPixelWidth / 2f;
                float topY = 0f;
                DrawScaleBarCore(g, leftX, topY, barPixelWidth, barHeight, barMeters, halfBarMeters, ScaleBarType, primaryColor, secondaryColor, 1.2f, font);
            }
            g.Restore(state);
        }

        /// <summary>
        /// 多様なデザインのスケールバーを描画する共通メソッド
        /// </summary>
        private void DrawScaleBarCore(Graphics g, float leftX, float topY, float barWidth, float barHeight, double barMeters, double halfBarMeters, string style, Color primaryColor, Color secondaryColor, float penWidth, Font font)
        {
            using (var primaryBrush = new SolidBrush(primaryColor))
            using (var secondaryBrush = new SolidBrush(secondaryColor))
            using (var pen = new Pen(primaryColor, penWidth))
            {
                float midX = leftX + (barWidth / 2f);
                float endX = leftX + barWidth;

                string scaleStr = (Scale % 1 == 0) ? Scale.ToString("0") : Scale.ToString("0.#");
                string bottomScaleText = $"(S=1:{scaleStr})";

                if (style == "精密線 (下縮尺)" || style == "精密線")
                {
                    // ユーザー指定デザイン: 基準線＋左半分5分割目盛（中間4本は半分の長さ）＋右半分単一区間＋上部0/全長m＋下部中央(S=1:xxx)
                    float lineY = topY + barHeight;
                    float tickH = Math.Max(3.0f, barHeight);

                    // 基準水平線
                    g.DrawLine(pen, leftX, lineY, endX, lineY);

                    // 0位置目盛 (左端) - フル長
                    g.DrawLine(pen, leftX, lineY, leftX, lineY - tickH);

                    // 左半分の5分割中間目盛 (1, 2, 3, 4) - 半分の長さ
                    for (int i = 1; i <= 4; i++)
                    {
                        float subX = leftX + (midX - leftX) * (i / 5f);
                        g.DrawLine(pen, subX, lineY, subX, lineY - (tickH * 0.5f));
                    }

                    // 中間目盛 (Mid) - フル長
                    g.DrawLine(pen, midX, lineY, midX, lineY - tickH);

                    // 終端目盛 (End) - フル長
                    g.DrawLine(pen, endX, lineY, endX, lineY - tickH);

                    // 上部数値テキスト
                    string l0 = "0";
                    string lEnd = $"{barMeters:0.#}m";
                    var sz0 = g.MeasureString(l0, font);
                    var szEnd = g.MeasureString(lEnd, font);

                    float textY = lineY - tickH - sz0.Height - 1f;
                    g.DrawString(l0, font, primaryBrush, leftX - (sz0.Width / 2f), textY);
                    g.DrawString(lEnd, font, primaryBrush, endX - (szEnd.Width / 2f), textY);

                    // 下部縮尺テキスト (中央揃え)
                    var szS = g.MeasureString(bottomScaleText, font);
                    g.DrawString(bottomScaleText, font, primaryBrush, (leftX + endX) / 2f - (szS.Width / 2f), lineY + 2f);
                }
                else if (style == "シンプル線")
                {
                    // 単線＋3点垂直目盛線＋下部縮尺
                    float lineY = topY + barHeight;
                    float tickH = Math.Max(3.0f, barHeight);
                    g.DrawLine(pen, leftX, lineY, endX, lineY);
                    g.DrawLine(pen, leftX, lineY - tickH, leftX, lineY);
                    g.DrawLine(pen, midX, lineY - tickH, midX, lineY);
                    g.DrawLine(pen, endX, lineY - tickH, endX, lineY);

                    string l0 = "0";
                    string lMid = $"{halfBarMeters:0.#}";
                    string lEnd = $"{barMeters:0.#}m";
                    var sz0 = g.MeasureString(l0, font);
                    var szMid = g.MeasureString(lMid, font);
                    var szEnd = g.MeasureString(lEnd, font);

                    float textY = lineY - tickH - sz0.Height - 1f;
                    g.DrawString(l0, font, primaryBrush, leftX - (sz0.Width / 2f), textY);
                    g.DrawString(lMid, font, primaryBrush, midX - (szMid.Width / 2f), textY);
                    g.DrawString(lEnd, font, primaryBrush, endX - (szEnd.Width / 2f), textY);

                    var szS = g.MeasureString(bottomScaleText, font);
                    g.DrawString(bottomScaleText, font, primaryBrush, (leftX + endX) / 2f - (szS.Width / 2f), lineY + 2f);
                }
                else if (style == "二重枠")
                {
                    // 白抜き枠＋中央分割線
                    g.FillRectangle(secondaryBrush, leftX, topY, barWidth, barHeight);
                    g.DrawRectangle(pen, leftX, topY, barWidth, barHeight);
                    g.DrawLine(pen, midX, topY, midX, topY + barHeight);

                    string l0 = "0";
                    string lMid = $"{halfBarMeters:0.#}";
                    string lEnd = $"{barMeters:0.#}m (1/{scaleStr})";
                    var sz0 = g.MeasureString(l0, font);
                    var szMid = g.MeasureString(lMid, font);
                    var szEnd = g.MeasureString(lEnd, font);

                    float textY = topY - sz0.Height - 1f;
                    g.DrawString(l0, font, primaryBrush, leftX - (sz0.Width / 2f), textY);
                    g.DrawString(lMid, font, primaryBrush, midX - (szMid.Width / 2f), textY);
                    g.DrawString(lEnd, font, primaryBrush, endX - (szEnd.Width / 2f), textY);
                }
                else if (style == "目盛付き")
                {
                    // 左半分をさらに細分化した測量精密スタイル
                    float qX = leftX + (barWidth / 4f);
                    g.FillRectangle(primaryBrush, leftX, topY, barWidth / 4f, barHeight);
                    g.FillRectangle(secondaryBrush, qX, topY, barWidth / 4f, barHeight);
                    g.FillRectangle(primaryBrush, midX, topY, barWidth / 2f, barHeight);
                    g.DrawRectangle(pen, leftX, topY, barWidth, barHeight);
                    g.DrawLine(pen, qX, topY, qX, topY + barHeight);
                    g.DrawLine(pen, midX, topY, midX, topY + barHeight);

                    string l0 = "0";
                    string lMid = $"{halfBarMeters:0.#}";
                    string lEnd = $"{barMeters:0.#}m (1/{scaleStr})";
                    var sz0 = g.MeasureString(l0, font);
                    var szMid = g.MeasureString(lMid, font);
                    var szEnd = g.MeasureString(lEnd, font);

                    float textY = topY - sz0.Height - 1f;
                    g.DrawString(l0, font, primaryBrush, leftX - (sz0.Width / 2f), textY);
                    g.DrawString(lMid, font, primaryBrush, midX - (szMid.Width / 2f), textY);
                    g.DrawString(lEnd, font, primaryBrush, endX - (szEnd.Width / 2f), textY);
                }
                else // "ブロック" (デフォルト)
                {
                    g.FillRectangle(primaryBrush, leftX, topY, barWidth / 2f, barHeight);
                    g.FillRectangle(secondaryBrush, midX, topY, barWidth / 2f, barHeight);
                    g.DrawRectangle(pen, leftX, topY, barWidth, barHeight);
                    g.DrawLine(pen, midX, topY, midX, topY + barHeight);

                    string l0 = "0";
                    string lMid = $"{halfBarMeters:0.#}";
                    string lEnd = $"{barMeters:0.#}m (1/{scaleStr})";
                    var sz0 = g.MeasureString(l0, font);
                    var szMid = g.MeasureString(lMid, font);
                    var szEnd = g.MeasureString(lEnd, font);

                    float textY = topY - sz0.Height - 1f;
                    g.DrawString(l0, font, primaryBrush, leftX - (sz0.Width / 2f), textY);
                    g.DrawString(lMid, font, primaryBrush, midX - (szMid.Width / 2f), textY);
                    g.DrawString(lEnd, font, primaryBrush, endX - (szEnd.Width / 2f), textY);
                }
            }
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
        /// <summary>
        /// 図面プレビューキャンバス（用紙イメージ）へのリアルタイムレンダリング
        /// </summary>
        public void DrawPaperPreview(
            Graphics g,
            Size canvasSize,
            EditorDbManager? db,
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

            // 1. 用紙のドロップシャドウ
            using (var shadowBrush = new SolidBrush(Color.FromArgb(80, 0, 0, 0)))
            {
                g.FillRectangle(shadowBrush, paperLeft + 5f, paperTop + 5f, paperW, paperH);
            }

            DrawPaperDrawingCore(
                g,
                paperLeft,
                paperTop,
                zoom,
                (float)wMm,
                (float)hMm,
                db,
                isMapLayerVisible,
                showCurve,
                colorByIkou,
                showIkou,
                showIkouName,
                showIbutu,
                showIbutuName,
                showKikai,
                showKikaiName,
                showHyoukou,
                isPrinting: false
            );

            // 用紙下部のインフォメーションラベル
            string docInfo = $"【図面出力イメージ】{PaperSizeName} {(IsLandscape ? "横" : "縦")} 1/{Scale:0} (回転 {RotationAngleDeg:0.0}°)   ピッチ: {GetEffectivePitchMeters():0.#}m";
            using (var docFont = new Font("Yu Gothic UI", 8F, FontStyle.Bold))
            using (var docBrush = new SolidBrush(Color.FromArgb(180, 200, 220)))
            {
                g.DrawString(docInfo, docFont, docBrush, paperLeft + 4f, paperTop + paperH + 4f);
            }
        }

        /// <summary>
        /// 用紙図面要素（外枠・内枠・トンボ・格子線・遺構線・座標値・方位記号・スケールバー）のコア描画
        /// 画面プレビュー・高解像度プリンタ印刷の両方で用紙実ミリ寸法の比率を完全に一致させて描画
        /// </summary>
        public void DrawPaperDrawingCore(
            Graphics g,
            float paperLeft,
            float paperTop,
            float zoom,
            float wMm,
            float hMm,
            EditorDbManager? dbManager,
            Func<int, bool>? isMapLayerVisible,
            bool showCurve,
            bool colorByIkou,
            bool showIkou,
            bool showIkouName,
            bool showIbutu,
            bool showIbutuName,
            bool showKikai,
            bool showKikaiName,
            bool showHyoukou,
            bool isPrinting = false,
            bool isMonochrome = false)
        {
            float paperW = wMm * zoom;
            float paperH = hMm * zoom;

            Color AdaptColor(Color c)
            {
                if (!isMonochrome) return c;
                int gray = (int)(0.299 * c.R + 0.587 * c.G + 0.114 * c.B);
                if (gray > 160) gray = 100; // 白飛び防止
                return Color.FromArgb(c.A, gray, gray, gray);
            }

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

            // 1. 白地用紙の描画
            RectangleF paperRect = new RectangleF(paperLeft, paperTop, paperW, paperH);
            g.FillRectangle(Brushes.White, paperRect);
            if (!isPrinting)
            {
                using (var paperBorderPen = new Pen(Color.FromArgb(200, 200, 210), 1f))
                {
                    g.DrawRectangle(paperBorderPen, paperRect.X, paperRect.Y, paperRect.Width, paperRect.Height);
                }
            }

            // 2. 外枠の描画 (余白: 左 MarginLeftMm, 他 MarginOtherMm)
            float outerLeft = paperLeft + (float)(MarginLeftMm * zoom);
            float outerTop = paperTop + (float)(MarginOtherMm * zoom);
            float outerW = paperW - (float)((MarginLeftMm + MarginOtherMm) * zoom);
            float outerH = paperH - (float)((MarginOtherMm * 2.0) * zoom);
            RectangleF outerRect = new RectangleF(outerLeft, outerTop, outerW, outerH);

            float outerPenWidth = Math.Max(0.4f, (float)(0.35 * zoom));
            float thickPenWidth = Math.Max(0.8f, (float)(0.8 * zoom));
            using (var outerPen = new Pen(Color.FromArgb(20, 20, 20), outerPenWidth))
            using (var thickPen = new Pen(Color.FromArgb(20, 20, 20), thickPenWidth))
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

            Color innerColor = isMonochrome ? Color.FromArgb(20, 20, 20) : Color.FromArgb(0, 100, 180);
            using (var innerPen = new Pen(innerColor, outerPenWidth))
            {
                g.DrawRectangle(innerPen, innerRect.X, innerRect.Y, innerRect.Width, innerRect.Height);
            }

            // 4. トンボ (+) & 外枠・内枠間の座標値の計算・描画
            double pitch = GetEffectivePitchMeters();
            var innerCorners = GetInnerCornersSurvey();

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
            float tomboPenWidth = Math.Max(0.3f, (float)(0.25 * zoom));
            float gridPenWidth = Math.Max(0.2f, (float)(0.2 * zoom));
            using (var tomboPen = new Pen(Color.FromArgb(160, 160, 160), tomboPenWidth))
            using (var gridPen = new Pen(Color.FromArgb(190, 190, 190), gridPenWidth) { DashStyle = DashStyle.Dash })
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

                // トンボ (+) の描画 (実寸 3.5mm の腕長)
                if (ShowTombo)
                {
                    float arm = Math.Max(2.5f, (float)(3.5 * zoom));
                    for (double sx = startSurX; sx <= endSurX + 0.001; sx += pitch)
                    {
                        for (double sy = startSurY; sy <= endSurY + 0.001; sy += pitch)
                        {
                            PointF pt = SurveyToPaperScreen(sx, sy);
                            if (innerRect.Contains(pt))
                            {
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

            var db = dbManager;
            if (db != null)
            {
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

                        Color rawColor = colorByIkou
                            ? EditorLayerService.PaletteColors[(int)(line.Id % EditorLayerService.PaletteColors.Length)]
                            : EditorLayerService.GetIkouLineColor(line.Layer, false);
                        Color color = AdaptColor(rawColor);

                        if (line.Mode == 2)
                        {
                            float ptRadius = Math.Max(1.0f, (float)(0.75 * zoom));
                            if (showIkou)
                            {
                                using (var ptBrush = new SolidBrush(color))
                                {
                                    foreach (var p in pts)
                                    {
                                        PointF sp = SurveyToPaperScreen(p.X, p.Y);
                                        g.FillEllipse(ptBrush, sp.X - ptRadius, sp.Y - ptRadius, ptRadius * 2f, ptRadius * 2f);
                                    }
                                }
                            }
                            if (showHyoukou)
                            {
                                float zFontPx = Math.Max(2.5f, (float)(2.2 * zoom));
                                Color zColor = isMonochrome ? Color.FromArgb(40, 40, 40) : Color.FromArgb(30, 100, 30);
                                using (var zFont = new Font("Yu Gothic UI", zFontPx, FontStyle.Regular, GraphicsUnit.Pixel))
                                using (var zBrush = new SolidBrush(zColor))
                                {
                                    foreach (var p in pts)
                                    {
                                        PointF sp = SurveyToPaperScreen(p.X, p.Y);
                                        g.DrawString(p.Z.ToString("0.000"), zFont, zBrush, sp.X + (float)(1.0 * zoom), sp.Y - (float)(2.5 * zoom));
                                    }
                                }
                            }
                            continue;
                        }

                        if (!showIkou) continue;

                        var layerItem = LayerDefinitionService.Instance.GetLayer(LayerGroup.Ikou, line.Layer);
                        bool isLayerCurve = (layerItem != null) ? (layerItem.LType == 2) : true;
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
                            float lWidth = (layerItem != null && layerItem.Width > 0) ? (float)(layerItem.Width * 0.3 * zoom) : Math.Max(0.4f, (float)(0.35 * zoom));
                            using (var linePen = new Pen(color, lWidth))
                            {
                                g.DrawLines(linePen, screenPts);
                                if (line.Mode == 1 && screenPts.Length >= 3)
                                {
                                    g.DrawLine(linePen, screenPts[screenPts.Length - 1], screenPts[0]);
                                }
                            }
                        }

                        // 遺構線名ラベル (実寸 3.0mm 高さ)
                        if (showIkouName && !string.IsNullOrEmpty(line.Name))
                        {
                            PointF labelPt = (line.X != 0 || line.Y != 0)
                                ? SurveyToPaperScreen(line.X, line.Y)
                                : SurveyToPaperScreen(pts[0].X, pts[0].Y);

                            float lFontPx = Math.Max(3.0f, (float)(3.0 * zoom));
                            using (var lFont = new Font("Yu Gothic UI", lFontPx, FontStyle.Bold, GraphicsUnit.Pixel))
                            using (var lBrush = new SolidBrush(Color.Black))
                            using (var bgBrush = new SolidBrush(Color.FromArgb(210, 255, 255, 255)))
                            {
                                var sz = g.MeasureString(line.Name, lFont);
                                g.FillRectangle(bgBrush, labelPt.X - 1f, labelPt.Y - 1f, sz.Width + 2f, sz.Height + 2f);
                                g.DrawString(line.Name, lFont, lBrush, labelPt.X, labelPt.Y);
                            }
                        }
                    }
                }

                // C. 遺物 (Artifact Points) の描画
                // C. 遺物 (Artifact Points) の描画
                if (showIbutu)
                {
                    float ibFontPx = Math.Max(3.0f, (float)(2.5 * zoom));
                    using (var ibutuFont = new Font("Yu Gothic UI", ibFontPx, FontStyle.Bold, GraphicsUnit.Pixel))
                    {
                        foreach (var ibutu in db.IbutuList)
                        {
                            PointF sp = SurveyToPaperScreen(ibutu.X, ibutu.Y);
                            var layerItem = LayerDefinitionService.Instance.GetLayer(LayerGroup.Ibutu, ibutu.Layer);
                            Color ibColor = isMonochrome ? Color.FromArgb(20, 20, 20) : EditorLayerService.GetIbutuColor(ibutu.Layer, false);
                            float ibRadius = Math.Max(1.5f, (float)(layerItem.Size * 1.5 * zoom));

                            using (var ibBrush = new SolidBrush(ibColor))
                            using (var ibPen = new Pen(ibColor, Math.Max(0.5f, (float)(0.4 * zoom))))
                            using (var borderPen = new Pen(Color.Black, Math.Max(0.5f, (float)(0.3 * zoom))))
                            using (var textBrush = new SolidBrush(ibColor))
                            {
                                var penToUse = (layerItem.Mark == 5 || layerItem.Mark == 6) ? ibPen : borderPen;
                                EditorLayerService.DrawPointMark(g, sp, layerItem.Mark, ibRadius, ibBrush, penToUse);

                                if (showIbutuName)
                                {
                                    string ibName = !string.IsNullOrEmpty(ibutu.Syubetu)
                                        ? (ibutu.No > 0 ? $"{ibutu.Syubetu}{ibutu.No}" : ibutu.Syubetu)
                                        : (ibutu.No > 0 ? $"No.{ibutu.No}" : $"遺物{ibutu.Id}");

                                    g.DrawString(ibName, ibutuFont, textBrush, sp.X + ibRadius + (float)(1.0 * zoom), sp.Y - ibRadius);
                                }
                            }
                        }
                    }
                }

                // D. 基準点・機械点 (Kikai Points) の描画
                if (showKikai)
                {
                    float kFontPx = Math.Max(3.0f, (float)(3.0 * zoom));
                    using (var kikaiFont = new Font("Yu Gothic UI", kFontPx, FontStyle.Bold, GraphicsUnit.Pixel))
                    {
                        foreach (var k in db.KikaiList)
                        {
                            PointF sp = SurveyToPaperScreen(k.X, k.Y);
                            var layerItem = LayerDefinitionService.Instance.GetLayer(LayerGroup.Kikai, k.Layer);
                            Color kikaiColor = isMonochrome ? Color.FromArgb(30, 30, 30) : EditorLayerService.GetKikaiColor(k.Layer, false);
                            float kikaiRadius = Math.Max(2.0f, (float)(layerItem.Size * 2.0 * zoom));

                            using (var kikaiBrush = new SolidBrush(kikaiColor))
                            using (var kikaiPen = new Pen(kikaiColor, Math.Max(0.6f, (float)(0.5 * zoom))))
                            using (var borderPen = new Pen(Color.Black, Math.Max(0.5f, (float)(0.35 * zoom))))
                            using (var textBrush = new SolidBrush(kikaiColor))
                            {
                                var penToUse = (layerItem.Mark == 5 || layerItem.Mark == 6) ? kikaiPen : borderPen;
                                EditorLayerService.DrawPointMark(g, sp, layerItem.Mark, kikaiRadius, kikaiBrush, penToUse);

                                if (showKikaiName && !string.IsNullOrEmpty(k.Name))
                                {
                                    g.DrawString(k.Name, kikaiFont, textBrush, sp.X + kikaiRadius + (float)(1.0 * zoom), sp.Y - kikaiRadius * 0.8f);
                                }
                            }
                        }
                    }
                }
            }

            // クリッピング解除
            g.Clip = oldClip;

            // 6. 外枠・内枠間の座標表記（X=...m, Y=...m）（実寸 3.0mm 高さ）
            if (ShowBorderCoords)
            {
                float coordFontPx = Math.Max(3.0f, (float)(3.0 * zoom));
                using (var coordFont = new Font("Yu Gothic UI", coordFontPx, FontStyle.Bold, GraphicsUnit.Pixel))
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
                    anchor = GetCornerPoint(innerCornersScreen, NorthArrowPosition, (float)(14.0 * zoom));
                }

                float length = (float)Math.Max(12.0f, (float)(NorthArrowSizeMm * zoom));
                float width = length * 0.28f;
                // 印刷用紙上での北方向（図枠の回転角 RotationAngleDeg に連動）
                float needleRad = (float)(RotationAngleDeg * Math.PI / 180.0);
                float nArrowPenWidth = Math.Max(0.35f, (float)(0.35 * zoom));
                float nFontPx = Math.Max(7.0f, (float)(7.0 * zoom));

                DrawNorthArrowCore(g, anchor, needleRad, length, width, NorthArrowType, false, nArrowPenWidth, nFontPx);
            }

            // 8. スケールバー (Scale Bar) の描画（用紙上で実寸約50mmの長さ）
            if (ShowScaleBar)
            {
                // 目標バー長: 用紙上で約50mm前後
                double targetBarMm = 50.0;
                double idealMeters = (targetBarMm / 1000.0) * Scale;

                double barMeters;
                if (idealMeters <= 3.0) barMeters = 2.0;
                else if (idealMeters <= 7.5) barMeters = 5.0;
                else if (idealMeters <= 15.0) barMeters = 10.0;
                else if (idealMeters <= 35.0) barMeters = 20.0;
                else if (idealMeters <= 75.0) barMeters = 50.0;
                else if (idealMeters <= 150.0) barMeters = 100.0;
                else barMeters = Math.Ceiling(idealMeters / 50.0) * 50.0;

                double halfBarMeters = barMeters / 2.0;
                double barMm = (barMeters / Scale) * 1000.0; // 用紙上の正確なミリ長
                float barWidth = (float)(barMm * zoom);
                float barHeight = Math.Max(2.0f, (float)(2.5 * zoom)); // 2.5mm高さ

                float scaleBarOffset = (float)(9.5 * zoom);
                PointF anchor;
                if (ScaleBarPosition == "中下")
                {
                    anchor = new PointF((innerRect.Left + innerRect.Right) / 2f, innerRect.Bottom - scaleBarOffset);
                }
                else
                {
                    anchor = new PointF(innerRect.Right - (barWidth / 2f) - scaleBarOffset, innerRect.Bottom - scaleBarOffset);
                }

                float leftX = anchor.X - (barWidth / 2f);
                float topY = anchor.Y;

                float scaleBarPenWidth = Math.Max(0.35f, (float)(0.35 * zoom));
                float scaleFontPx = Math.Max(2.8f, (float)(2.6 * zoom));
                using (var font = new Font("Yu Gothic UI", scaleFontPx, FontStyle.Bold, GraphicsUnit.Pixel))
                {
                    DrawScaleBarCore(g, leftX, topY, barWidth, barHeight, barMeters, halfBarMeters, ScaleBarType, Color.Black, Color.White, scaleBarPenWidth, font);
                }
            }
        }

        /// <summary>
        /// Windows標準印刷ダイアログ経由で図面を高解像度印刷（縮小印刷・モノクロ印刷にも自動対応）
        /// </summary>
        public void Print(
            IWin32Window owner,
            EditorDbManager? db,
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
            try
            {
                using (var pd = new System.Drawing.Printing.PrintDocument())
                {
                    pd.DocumentName = $"SITE7_図面_{PaperSizeName}_{(IsLandscape ? "横" : "縦")}";
                    pd.DefaultPageSettings.Landscape = IsLandscape;

                    // プリンタでサポートされている用紙サイズがあれば図枠のサイズ(A3, A4等)をデフォルトに設定
                    foreach (System.Drawing.Printing.PaperSize ps in pd.PrinterSettings.PaperSizes)
                    {
                        if (string.Equals(ps.PaperName, PaperSizeName, StringComparison.OrdinalIgnoreCase) ||
                            ps.PaperName.StartsWith(PaperSizeName, StringComparison.OrdinalIgnoreCase))
                        {
                            pd.DefaultPageSettings.PaperSize = ps;
                            break;
                        }
                    }

                    using (var dlg = new PrintDialog())
                    {
                        dlg.Document = pd;
                        dlg.UseEXDialog = false; // クラシックWin32ダイアログを使用してWin11のプレビュー非対応枠を解消
                        dlg.AllowSomePages = false;
                        dlg.AllowSelection = false;

                        if (dlg.ShowDialog(owner) == DialogResult.OK)
                        {
                            pd.PrintPage += (s, e) =>
                            {
                                if (e.Graphics == null) return;

                                var (logicalWMm, logicalHMm) = GetPaperDimensionsMm();

                                // 実際のプリンタ用紙サイズ (1/100インチ単位)
                                float pageW100Inch = e.PageBounds.Width;
                                float pageH100Inch = e.PageBounds.Height;

                                // 1/100インチからmmへの変換 (1/100 inch = 0.254 mm)
                                float physicalPageWMm = pageW100Inch * 0.254f;
                                float physicalPageHMm = pageH100Inch * 0.254f;

                                // 印刷可能マージン（左右上下各5mm程度を確保）
                                float marginMm = 5.0f;
                                float availWMm = Math.Max(10f, physicalPageWMm - (marginMm * 2f));
                                float availHMm = Math.Max(10f, physicalPageHMm - (marginMm * 2f));

                                // 縮小印刷比率（等倍で収まれば1.0、用紙が小さければ縮小）
                                float scaleRatio = Math.Min(availWMm / (float)logicalWMm, availHMm / (float)logicalHMm);
                                if (scaleRatio > 1.0f) scaleRatio = 1.0f; // 拡大はせず最大100%等倍

                                // 100分の1インチ単位でのzoom（1mmあたり何1/100インチか）
                                float zoom100InchPerMm = (1.0f / 0.254f) * scaleRatio;

                                float printedW100Inch = (float)logicalWMm * zoom100InchPerMm;
                                float printedH100Inch = (float)logicalHMm * zoom100InchPerMm;

                                float originX = (pageW100Inch - printedW100Inch) / 2f;
                                float originY = (pageH100Inch - printedH100Inch) / 2f;

                                bool isMonochrome = !e.PageSettings.Color || !pd.PrinterSettings.DefaultPageSettings.Color;

                                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                                DrawPaperDrawingCore(
                                    e.Graphics,
                                    originX,
                                    originY,
                                    zoom100InchPerMm,
                                    (float)logicalWMm,
                                    (float)logicalHMm,
                                    db,
                                    isMapLayerVisible,
                                    showCurve,
                                    colorByIkou,
                                    showIkou,
                                    showIkouName,
                                    showIbutu,
                                    showIbutuName,
                                    showKikai,
                                    showKikaiName,
                                    showHyoukou,
                                    isPrinting: true,
                                    isMonochrome: isMonochrome
                                );

                                e.HasMorePages = false;
                            };

                            using (var ppd = new PrintPreviewDialog())
                            {
                                ppd.Document = pd;
                                ppd.Text = $"印刷プレビュー - SITE7 図面 ({PaperSizeName} {(IsLandscape ? "横" : "縦")})";
                                ppd.Width = 1050;
                                ppd.Height = 750;
                                ppd.StartPosition = FormStartPosition.CenterParent;
                                ppd.ShowDialog(owner);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(owner, $"印刷の開始中にエラーが発生しました:\n{ex.Message}", "印刷エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                NorthArrowType = Def.GetIniStr("DRAWING_FRAME", "NorthArrowType");
                if (string.IsNullOrEmpty(NorthArrowType)) NorthArrowType = "標準矢印";
                NorthArrowSizeMm = Def.GetIniDouble("DRAWING_FRAME", "NorthArrowSizeMm", 15.0);
                NorthArrowPosition = Def.GetIniStr("DRAWING_FRAME", "NorthArrowPosition");
                if (string.IsNullOrEmpty(NorthArrowPosition)) NorthArrowPosition = "右上";
                NorthArrowCustomSurveyX = Def.GetIniDouble("DRAWING_FRAME", "NorthArrowCustomSurveyX", 0.0);
                NorthArrowCustomSurveyY = Def.GetIniDouble("DRAWING_FRAME", "NorthArrowCustomSurveyY", 0.0);
                HasCustomNorthArrowPos = Def.GetIniInt("DRAWING_FRAME", "HasCustomNorthArrowPos", 0) == 1;

                ShowScaleBar = Def.GetIniInt("DRAWING_FRAME", "ShowScaleBar", 1) == 1;
                ScaleBarType = Def.GetIniStr("DRAWING_FRAME", "ScaleBarType");
                if (string.IsNullOrEmpty(ScaleBarType)) ScaleBarType = "精密線 (下縮尺)";
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
                Def.SetIniStr("DRAWING_FRAME", "NorthArrowType", NorthArrowType);
                Def.SetIniDouble("DRAWING_FRAME", "NorthArrowSizeMm", NorthArrowSizeMm);
                Def.SetIniStr("DRAWING_FRAME", "NorthArrowPosition", NorthArrowPosition);
                Def.SetIniDouble("DRAWING_FRAME", "NorthArrowCustomSurveyX", NorthArrowCustomSurveyX);
                Def.SetIniDouble("DRAWING_FRAME", "NorthArrowCustomSurveyY", NorthArrowCustomSurveyY);
                Def.SetIniInt("DRAWING_FRAME", "HasCustomNorthArrowPos", HasCustomNorthArrowPos ? 1 : 0);

                Def.SetIniInt("DRAWING_FRAME", "ShowScaleBar", ShowScaleBar ? 1 : 0);
                Def.SetIniStr("DRAWING_FRAME", "ScaleBarType", ScaleBarType);
                Def.SetIniStr("DRAWING_FRAME", "ScaleBarPosition", ScaleBarPosition);

                Def.SetIniDouble("DRAWING_FRAME", "MarginLeftMm", MarginLeftMm);
                Def.SetIniDouble("DRAWING_FRAME", "MarginOtherMm", MarginOtherMm);
                Def.SetIniDouble("DRAWING_FRAME", "OuterInnerSpacingMm", OuterInnerSpacingMm);
            }
            catch { }
        }
    }
}
