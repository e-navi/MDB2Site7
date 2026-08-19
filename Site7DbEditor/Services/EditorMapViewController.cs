using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Site7DbEditor.Services
{
    public class EditorMapViewController
    {
        public float ZoomFactorMap { get; set; } = 1.0f;
        public PointF PanOffsetMap { get; set; } = PointF.Empty;
        public bool IsMouseDownMap { get; set; } = false;
        public bool IsPanningMap { get; set; } = false;
        public Point MouseDownPosMap { get; set; }
        public Point LastMousePosMap { get; set; }

        public double PosXMin { get; private set; } = -50;
        public double PosXMax { get; private set; } = 50;
        public double PosYMin { get; private set; } = -50;
        public double PosYMax { get; private set; } = 50;
        public double MapScale { get; private set; } = 1.0;
        public float OffsetX { get; private set; } = 0;
        public float OffsetY { get; private set; } = 0;
        private bool _isBoundsCached = false;

        public void ResetZoom()
        {
            ZoomFactorMap = 1.0f;
            PanOffsetMap = PointF.Empty;
        }

        public void InvalidateBoundsCache()
        {
            _isBoundsCached = false;
        }

        public void UpdateMapBounds(Size canvasSize, IEnumerable<IkouLModel> ikouLList, IEnumerable<IbutuModel> ibutuList, bool forceRecalculate = false)
        {
            if (_isBoundsCached && !forceRecalculate) return;

            int width = canvasSize.Width;
            int height = canvasSize.Height;
            if (width <= 0 || height <= 0) return;

            double posXMin = double.MaxValue, posXMax = double.MinValue;
            double posYMin = double.MaxValue, posYMax = double.MinValue;

            foreach (var line in ikouLList)
            {
                var pts = SqliteManager.ParsePrecsText(line.Precs);
                foreach (var pt in pts)
                {
                    if (pt.Y < posXMin) posXMin = pt.Y;
                    if (pt.Y > posXMax) posXMax = pt.Y;
                    if (pt.X < posYMin) posYMin = pt.X;
                    if (pt.X > posYMax) posYMax = pt.X;
                }
            }

            foreach (var ibutu in ibutuList)
            {
                if (ibutu.Y < posXMin) posXMin = ibutu.Y;
                if (ibutu.Y > posXMax) posXMax = ibutu.Y;
                if (ibutu.X < posYMin) posYMin = ibutu.X;
                if (ibutu.X > posYMax) posYMax = ibutu.X;
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
            int drawWidth = width - (margin * 2);
            int drawHeight = height - (margin * 2);

            double scale = Math.Min(drawWidth / rangeX, drawHeight / rangeY);

            float offsetX = (float)((width - (rangeX * scale)) / 2.0);
            float offsetY = (float)((height - (rangeY * scale)) / 2.0);

            PosXMin = posXMin;
            PosXMax = posXMax;
            PosYMin = posYMin;
            PosYMax = posYMax;
            MapScale = scale;
            OffsetX = offsetX;
            OffsetY = offsetY;
            _isBoundsCached = true;
        }

        public PointF ToCanvasPoint(double surveyX, double surveyY, Size canvasSize)
        {
            int width = canvasSize.Width;
            int height = canvasSize.Height;

            float cx = width / 2f;
            float cy = height / 2f;

            double posX = surveyY;
            double posY = surveyX;

            float bx = (float)(OffsetX + (posX - PosXMin) * MapScale);
            float by = (float)(height - OffsetY - (posY - PosYMin) * MapScale);
            float px = cx + (bx - cx) * ZoomFactorMap + PanOffsetMap.X;
            float py = cy + (by - cy) * ZoomFactorMap + PanOffsetMap.Y;
            return new PointF(px, py);
        }

        public void CenterOnPoint(double surveyX, double surveyY, Size canvasSize)
        {
            int width = canvasSize.Width;
            int height = canvasSize.Height;
            if (width <= 0 || height <= 0 || MapScale <= 0) return;

            float cx = width / 2f;
            float cy = height / 2f;

            double posX = surveyY;
            double posY = surveyX;

            float bx = (float)(OffsetX + (posX - PosXMin) * MapScale);
            float by = (float)(height - OffsetY - (posY - PosYMin) * MapScale);

            PanOffsetMap = new PointF(-(bx - cx) * ZoomFactorMap, -(by - cy) * ZoomFactorMap);
        }

        public bool IsPointInView(double surveyX, double surveyY, Size canvasSize, float margin = 30f)
        {
            if (canvasSize.Width <= 0 || canvasSize.Height <= 0 || MapScale <= 0) return false;
            PointF pt = ToCanvasPoint(surveyX, surveyY, canvasSize);
            return pt.X >= margin && pt.X <= (canvasSize.Width - margin) &&
                   pt.Y >= margin && pt.Y <= (canvasSize.Height - margin);
        }

        public (double surveyX, double surveyY) CanvasToSurvey(PointF canvasPt, Size canvasSize)
        {
            int width = canvasSize.Width;
            int height = canvasSize.Height;

            if (MapScale <= 0) return (0, 0);

            float cx = width / 2f;
            float cy = height / 2f;

            float bx = cx + (canvasPt.X - cx - PanOffsetMap.X) / ZoomFactorMap;
            float by = cy + (canvasPt.Y - cy - PanOffsetMap.Y) / ZoomFactorMap;

            double clickY = PosXMin + (bx - OffsetX) / MapScale; // East
            double clickX = PosYMin + (height - OffsetY - by) / MapScale; // North

            return (clickX, clickY);
        }

        public static double DistanceToLineSegment(PointF pt, PointF p1, PointF p2)
        {
            float dx = p2.X - p1.X;
            float dy = p2.Y - p1.Y;
            if (dx == 0 && dy == 0)
            {
                return Math.Sqrt((pt.X - p1.X) * (pt.X - p1.X) + (pt.Y - p1.Y) * (pt.Y - p1.Y));
            }
            float t = ((pt.X - p1.X) * dx + (pt.Y - p1.Y) * dy) / (dx * dx + dy * dy);
            t = Math.Clamp(t, 0.0f, 1.0f);
            float projX = p1.X + t * dx;
            float projY = p1.Y + t * dy;
            return Math.Sqrt((pt.X - projX) * (pt.X - projX) + (pt.Y - projY) * (pt.Y - projY));
        }
    }
}
