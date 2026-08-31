using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Site7DrawingEditor.Services
{
    public class CanvasViewController
    {
        // Crop Canvas View Zoom & Pan (測量座標系)
        public float CropZoom { get; set; } = 1.0f;
        public PointF CropPan { get; set; } = PointF.Empty;
        public bool IsCropMouseDown { get; set; } = false;
        public Point CropLastMousePos { get; set; }

        // Paper Canvas View Zoom & Pan (数学座標系 - 用紙中心(0,0))
        public float PaperZoom { get; set; } = 1.0f;
        public PointF PaperPan { get; set; } = PointF.Empty;
        public bool IsPaperMouseDown { get; set; } = false;
        public Point PaperLastMousePos { get; set; }

        // Interactive Placement States
        public int CropStep { get; set; } = 0; // 0: Normal, 1: Pick P1 (左下), 2: Pick P2 (右下), 3: Pick P3 (高さ指示)
        public bool IsPickingPaperPosition { get; set; } = false;
        public bool IsPickingDirectionPosition { get; set; } = false;
        public bool IsPickingDanmenPosition { get; set; } = false;
        public Point DetailLastMousePos { get; set; }

        public void ResetCropZoom()
        {
            CropZoom = 1.0f;
            CropPan = PointF.Empty;
        }

        public void ResetPaperZoom()
        {
            PaperZoom = 1.0f;
            PaperPan = PointF.Empty;
        }

        /// <summary>
        /// 測量座標 (X: North, Y: East) ➔ Cropキャンバス画面ピクセル座標へ変換
        /// </summary>
        public PointF ToCropCanvasPoint(double surveyX, double surveyY, Size canvasSize,
            IEnumerable<MasterIkouModel> ikouList,
            IEnumerable<MasterIkouLModel> ikouLList,
            IEnumerable<MasterIbutuModel> ibutuList,
            IEnumerable<MasterKikaiModel> kikaiList)
        {
            int width = canvasSize.Width;
            int height = canvasSize.Height;

            var (posXMin, posXMax, posYMin, posYMax, scale, offsetX, offsetY) = GetSurveyBoundsAndScale(canvasSize, ikouList, ikouLList, ibutuList, kikaiList);

            float cx = width / 2f;
            float cy = height / 2f;

            double posX = surveyY;
            double posY = surveyX;
            float bx = (float)(offsetX + (posX - posXMin) * scale);
            float by = (float)(height - offsetY - (posY - posYMin) * scale);
            float px = cx + (bx - cx) * CropZoom + CropPan.X;
            float py = cy + (by - cy) * CropZoom + CropPan.Y;
            return new PointF(px, py);
        }

        /// <summary>
        /// Cropキャンバス画面ピクセル座標 ➔ 測量座標 (X: North, Y: East) へ逆変換
        /// </summary>
        public (double surveyX, double surveyY) CanvasToSurveyCrop(PointF canvasPt, Size canvasSize,
            IEnumerable<MasterIkouModel> ikouList,
            IEnumerable<MasterIkouLModel> ikouLList,
            IEnumerable<MasterIbutuModel> ibutuList,
            IEnumerable<MasterKikaiModel> kikaiList)
        {
            int width = canvasSize.Width;
            int height = canvasSize.Height;

            var (posXMin, posXMax, posYMin, posYMax, scale, offsetX, offsetY) = GetSurveyBoundsAndScale(canvasSize, ikouList, ikouLList, ibutuList, kikaiList);
            if (scale <= 0) return (0, 0);

            float cx = width / 2f;
            float cy = height / 2f;

            float bx = (canvasPt.X - CropPan.X - cx) / CropZoom + cx;
            float by = (canvasPt.Y - CropPan.Y - cy) / CropZoom + cy;
            double posX = (bx - offsetX) / scale + posXMin;
            double posY = (height - offsetY - by) / scale + posYMin;
            return (posY, posX);
        }

        private (double posXMin, double posXMax, double posYMin, double posYMax, double scale, float offsetX, float offsetY)
            GetSurveyBoundsAndScale(Size canvasSize,
                IEnumerable<MasterIkouModel> ikouList,
                IEnumerable<MasterIkouLModel> ikouLList,
                IEnumerable<MasterIbutuModel> ibutuList,
                IEnumerable<MasterKikaiModel> kikaiList)
        {
            int width = canvasSize.Width;
            int height = canvasSize.Height;

            double posXMin = double.MaxValue, posXMax = double.MinValue;
            double posYMin = double.MaxValue, posYMax = double.MinValue;

            if (ikouLList != null)
            {
                foreach (var line in ikouLList)
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

            if (ikouList != null)
            {
                foreach (var ik in ikouList)
                {
                    if (Math.Abs(ik.X) > 0.001 || Math.Abs(ik.Y) > 0.001)
                    {
                        if (ik.Y < posXMin) posXMin = ik.Y;
                        if (ik.Y > posXMax) posXMax = ik.Y;
                        if (ik.X < posYMin) posYMin = ik.X;
                        if (ik.X > posYMax) posYMax = ik.X;
                    }
                }
            }

            if (ibutuList != null)
            {
                foreach (var ib in ibutuList)
                {
                    if (Math.Abs(ib.X) > 0.001 || Math.Abs(ib.Y) > 0.001)
                    {
                        if (ib.Y < posXMin) posXMin = ib.Y;
                        if (ib.Y > posXMax) posXMax = ib.Y;
                        if (ib.X < posYMin) posYMin = ib.X;
                        if (ib.X > posYMax) posYMax = ib.X;
                    }
                }
            }

            if (kikaiList != null)
            {
                foreach (var k in kikaiList)
                {
                    if (Math.Abs(k.X) > 0.001 || Math.Abs(k.Y) > 0.001)
                    {
                        if (k.Y < posXMin) posXMin = k.Y;
                        if (k.Y > posXMax) posXMax = k.Y;
                        if (k.X < posYMin) posYMin = k.X;
                        if (k.X > posYMax) posYMax = k.X;
                    }
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

            return (posXMin, posXMax, posYMin, posYMax, scale, offsetX, offsetY);
        }

        /// <summary>
        /// 指定した遺構（または遺構名）を全体図の中央に適当な大きさでフォーカス表示する
        /// </summary>
        public void FocusFeatureByNameOnFullMap(string targetName, DrawingIkouModel? curIkou, Size canvasSize,
            IEnumerable<MasterIkouModel> ikouList,
            IEnumerable<MasterIkouLModel> ikouLList,
            IEnumerable<MasterIbutuModel> ibutuList,
            IEnumerable<MasterKikaiModel> kikaiList)
        {
            if (canvasSize.Width <= 0 || canvasSize.Height <= 0) return;
            targetName = targetName?.Trim() ?? "";

            int width = canvasSize.Width;
            int height = canvasSize.Height;

            var (posXMin, posXMax, posYMin, posYMax, baseScale, offsetX, offsetY) = GetSurveyBoundsAndScale(canvasSize, ikouList, ikouLList, ibutuList, kikaiList);

            double rangeX = posXMax - posXMin;
            double rangeY = posYMax - posYMin;

            var ikouPts = new List<(double surveyX, double surveyY)>();

            // 1. もし対象遺構モデル (curIkou) があり、名称が targetName と一致している場合、設定済みの遺構枠 (3点指示枠) を優先
            bool isCurIkouMatching = curIkou != null && !string.IsNullOrWhiteSpace(targetName) &&
                (curIkou.Name.Equals(targetName, StringComparison.OrdinalIgnoreCase) || string.IsNullOrWhiteSpace(curIkou.Name));

            if (isCurIkouMatching && curIkou != null &&
                (Math.Abs(curIkou.P1.X) > 0.001 || Math.Abs(curIkou.P1.Y) > 0.001 ||
                 Math.Abs(curIkou.P2.X) > 0.001 || Math.Abs(curIkou.P2.Y) > 0.001))
            {
                var (v1, v2, v3, v4) = GeometryMath.GetCropBoxVertices(curIkou.P1, curIkou.P2, curIkou.P3);
                ikouPts.Add((v1.X, v1.Y));
                ikouPts.Add((v2.X, v2.Y));
                ikouPts.Add((v3.X, v3.Y));
                ikouPts.Add((v4.X, v4.Y));
            }

            // 2. 枠がない、または ComboBox 等で直接遺構名を指定した場合は、マスター遺構実測線 (MasterIkouLList) から該当遺構の点群を取得
            if (ikouPts.Count == 0 && !string.IsNullOrWhiteSpace(targetName))
            {
                var matchedMasterIkou = ikouList.FirstOrDefault(ik =>
                    (!string.IsNullOrWhiteSpace(ik.Name) && ik.Name.Equals(targetName, StringComparison.OrdinalIgnoreCase)) ||
                    ($"遺構{ik.Id}".Equals(targetName, StringComparison.OrdinalIgnoreCase)) ||
                    (ik.Id.ToString().Equals(targetName, StringComparison.OrdinalIgnoreCase))
                );

                if (matchedMasterIkou != null)
                {
                    long featureId = matchedMasterIkou.Id;
                    foreach (var line in ikouLList)
                    {
                        if (line.Id == featureId)
                        {
                            var pts = SqliteDrawingManager.ParsePrecsText(line.Precs);
                            ikouPts.AddRange(pts.Select(p => (p.X, p.Y)));
                        }
                    }

                    if (ikouPts.Count == 0 && (Math.Abs(matchedMasterIkou.X) > 0.001 || Math.Abs(matchedMasterIkou.Y) > 0.001))
                    {
                        ikouPts.Add((matchedMasterIkou.X, matchedMasterIkou.Y));
                    }
                }
                else
                {
                    foreach (var line in ikouLList)
                    {
                        if (string.Equals(line.Name, targetName, StringComparison.OrdinalIgnoreCase))
                        {
                            var pts = SqliteDrawingManager.ParsePrecsText(line.Precs);
                            ikouPts.AddRange(pts.Select(p => (p.X, p.Y)));
                        }
                    }
                }
            }

            // 3. それでも未検出で curIkou に実測線リストがあればそれを使用
            if (ikouPts.Count == 0 && curIkou != null)
            {
                foreach (var line in curIkou.LList)
                {
                    ikouPts.AddRange(line.Pnts.Select(p => (p.X, p.Y)));
                }
            }

            if (ikouPts.Count == 0) return;

            double fMinX = ikouPts.Min(p => p.surveyX);
            double fMaxX = ikouPts.Max(p => p.surveyX);
            double fMinY = ikouPts.Min(p => p.surveyY);
            double fMaxY = ikouPts.Max(p => p.surveyY);

            double fCenterX = (fMinX + fMaxX) / 2.0; // Survey X (North)
            double fCenterY = (fMinY + fMaxY) / 2.0; // Survey Y (East)
            double fRangeX = fMaxX - fMinX;
            double fRangeY = fMaxY - fMinY;
            if (fRangeX < 0.5) fRangeX = 2.0;
            if (fRangeY < 0.5) fRangeY = 2.0;

            float cx = width / 2f;
            float cy = height / 2f;

            float featureBaseX = (float)(offsetX + (fCenterY - posXMin) * baseScale);
            float featureBaseY = (float)(height - offsetY - (fCenterX - posYMin) * baseScale);

            // 遺構枠（または遺構点群）がキャンバス領域（余白を確保した82%サイズ）内にすっぽり収まるズーム倍率を正確に算出
            double featureWidthPx = fRangeY * baseScale;
            double featureHeightPx = fRangeX * baseScale;
            if (featureWidthPx < 1.0) featureWidthPx = 1.0;
            if (featureHeightPx < 1.0) featureHeightPx = 1.0;

            double targetZoomX = (width * 0.82) / featureWidthPx;
            double targetZoomY = (height * 0.82) / featureHeightPx;
            float targetZoom = (float)Math.Clamp(Math.Min(targetZoomX, targetZoomY), 1.0f, 30.0f);

            CropZoom = targetZoom;
            CropPan = new PointF(-(featureBaseX - cx) * targetZoom, -(featureBaseY - cy) * targetZoom);
        }
    }
}
