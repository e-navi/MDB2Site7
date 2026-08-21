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

        // 余白（用紙上のミリメートル単位）
        public double MarginLeftMm { get; set; } = 20.0;
        public double MarginRightMm { get; set; } = 10.0;
        public double MarginTopMm { get; set; } = 10.0;
        public double MarginBottomMm { get; set; } = 10.0;

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

            // 外枠ペン（黒または白）
            Color outerColor = isDarkBackground ? Color.FromArgb(240, 240, 245) : Color.FromArgb(15, 15, 20);
            Color innerColor = isDarkBackground ? Color.FromArgb(0, 210, 255) : Color.FromArgb(0, 140, 220);
            Color centerColor = isDarkBackground ? Color.FromArgb(255, 180, 0) : Color.FromArgb(220, 100, 0);

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

            // 3. 中心マーク（十字線）
            PointF centerScreen = vc.ToCanvasPoint(CenterX, CenterY, canvasSize);
            using (var centerPen = new Pen(centerColor, 1.5f))
            {
                g.DrawLine(centerPen, centerScreen.X - 8f, centerScreen.Y, centerScreen.X + 8f, centerScreen.Y);
                g.DrawLine(centerPen, centerScreen.X, centerScreen.Y - 8f, centerScreen.X, centerScreen.Y + 8f);
                g.DrawEllipse(centerPen, centerScreen.X - 4f, centerScreen.Y - 4f, 8f, 8f);
            }

            // 4. 図枠情報ラベル（外枠の左上外側に表示）
            PointF infoPos = outerScreen[3]; // 左上
            string infoText = $"全図枠 [{PaperSizeName} {(IsLandscape ? "横" : "縦")} 1/{Scale:0} ({RotationAngleDeg:0.0}°)]";
            using (var infoFont = new Font("Yu Gothic UI", 9.0F, FontStyle.Bold))
            using (var infoBgBrush = new SolidBrush(Color.FromArgb(180, 20, 20, 25)))
            using (var infoTextBrush = new SolidBrush(outerColor))
            {
                var sz = g.MeasureString(infoText, infoFont);
                g.FillRectangle(infoBgBrush, infoPos.X, infoPos.Y - sz.Height - 4f, sz.Width + 8f, sz.Height + 2f);
                g.DrawString(infoText, infoFont, infoTextBrush, infoPos.X + 4f, infoPos.Y - sz.Height - 3f);
            }
        }
    }
}
