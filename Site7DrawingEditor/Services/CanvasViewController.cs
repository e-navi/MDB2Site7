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
            IEnumerable<MasterIkouLModel> ikouLList,
            IEnumerable<MasterIbutuModel> ibutuList,
            IEnumerable<MasterKikaiModel> kikaiList)
        {
            int width = canvasSize.Width;
            int height = canvasSize.Height;

            var (posXMin, posXMax, posYMin, posYMax, scale, offsetX, offsetY) = GetSurveyBoundsAndScale(canvasSize, ikouLList, ibutuList, kikaiList);

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
            IEnumerable<MasterIkouLModel> ikouLList,
            IEnumerable<MasterIbutuModel> ibutuList,
            IEnumerable<MasterKikaiModel> kikaiList)
        {
            int width = canvasSize.Width;
            int height = canvasSize.Height;

            var (posXMin, posXMax, posYMin, posYMax, scale, offsetX, offsetY) = GetSurveyBoundsAndScale(canvasSize, ikouLList, ibutuList, kikaiList);
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
                IEnumerable<MasterIkouLModel> ikouLList,
                IEnumerable<MasterIbutuModel> ibutuList,
                IEnumerable<MasterKikaiModel> kikaiList)
        {
            int width = canvasSize.Width;
            int height = canvasSize.Height;

            var rawPoints = new List<(double surveyX, double surveyY)>();

            foreach (var line in ikouLList)
            {
                var pts = SqliteDrawingManager.ParsePrecsText(line.Precs);
                rawPoints.AddRange(pts.Select(p => (p.X, p.Y)));
            }
            foreach (var ib in ibutuList) rawPoints.Add((ib.X, ib.Y));
            foreach (var k in kikaiList) rawPoints.Add((k.X, k.Y));

            var validPoints = rawPoints.Where(p => Math.Abs(p.surveyX) > 10.0 || Math.Abs(p.surveyY) > 10.0).ToList();
            if (validPoints.Count == 0) validPoints = rawPoints;

            if (validPoints.Count == 0) return (0, 1, 0, 1, 1, 0, 0);

            double posXMin = validPoints.Min(p => p.surveyY);
            double posXMax = validPoints.Max(p => p.surveyY);
            double posYMin = validPoints.Min(p => p.surveyX);
            double posYMax = validPoints.Max(p => p.surveyX);

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
        /// ダブルクリック時: 指定した遺構を全体図の中央に適当な大きさでフォーカス表示する
        /// </summary>
        public void FocusFeatureOnFullMap(DrawingIkouModel curIkou, Size canvasSize,
            IEnumerable<MasterIkouLModel> ikouLList,
            IEnumerable<MasterIbutuModel> ibutuList,
            IEnumerable<MasterKikaiModel> kikaiList)
        {
            if (curIkou == null || canvasSize.Width <= 0 || canvasSize.Height <= 0) return;

            int width = canvasSize.Width;
            int height = canvasSize.Height;

            var (posXMin, posXMax, posYMin, posYMax, baseScale, offsetX, offsetY) = GetSurveyBoundsAndScale(canvasSize, ikouLList, ibutuList, kikaiList);

            double rangeX = posXMax - posXMin;
            double rangeY = posYMax - posYMin;

            var ikouPts = new List<(double surveyX, double surveyY)>();
            foreach (var line in curIkou.LList)
            {
                ikouPts.AddRange(line.Pnts.Select(p => (p.X, p.Y)));
            }
            if (ikouPts.Count == 0)
            {
                var (v1, v2, v3, v4) = GeometryMath.GetCropBoxVertices(curIkou.P1, curIkou.P2, curIkou.P3);
                ikouPts.Add((v1.X, v1.Y));
                ikouPts.Add((v2.X, v2.Y));
                ikouPts.Add((v3.X, v3.Y));
                ikouPts.Add((v4.X, v4.Y));
            }

            double fMinX = ikouPts.Min(p => p.surveyX);
            double fMaxX = ikouPts.Max(p => p.surveyX);
            double fMinY = ikouPts.Min(p => p.surveyY);
            double fMaxY = ikouPts.Max(p => p.surveyY);

            double fCenterX = (fMinX + fMaxX) / 2.0; // Survey X (North)
            double fCenterY = (fMinY + fMaxY) / 2.0; // Survey Y (East)
            double fRangeX = fMaxX - fMinX;
            double fRangeY = fMaxY - fMinY;
            if (fRangeX < 0.001) fRangeX = 1.0;
            if (fRangeY < 0.001) fRangeY = 1.0;

            float cx = width / 2f;
            float cy = height / 2f;

            float featureBaseX = (float)(offsetX + (fCenterY - posXMin) * baseScale);
            float featureBaseY = (float)(height - offsetY - (fCenterX - posYMin) * baseScale);

            double maxFRange = Math.Max(fRangeX, fRangeY);
            double maxBaseRange = Math.Max(rangeX, rangeY);
            float targetZoom = (float)Math.Clamp((maxBaseRange / maxFRange) * 0.40, 1.2f, 18.0f);

            CropZoom = targetZoom;
            CropPan = new PointF(-(featureBaseX - cx) * targetZoom, -(featureBaseY - cy) * targetZoom);
        }
    }
}
