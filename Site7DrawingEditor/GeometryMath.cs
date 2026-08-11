using System;
using System.Drawing;

namespace Site7DrawingEditor
{
    public static class GeometryMath
    {
        /// <summary>
        /// 3点指示（p1:左下, p2:右下, p3:高さ指示点）から長方形枠のパラメータを計算する
        /// P3は枠の高さ(H)を決定する指示点であり、頂点ではない
        /// 測量座標系: X=北(上), Y=東(右)
        /// </summary>
        public static (double angleRad, double widthM, double heightM, double ux, double uy, double vx, double vy, XYZ center) CalculateCropBox(XYZ p1, XYZ p2, XYZ p3)
        {
            double dx = p2.X - p1.X; // Survey X (North)
            double dy = p2.Y - p1.Y; // Survey Y (East)
            double widthM = Math.Sqrt(dx * dx + dy * dy);
            if (widthM < 0.0001) widthM = 1.0;

            double ux = dx / widthM; // 底辺 X (North) 方向単位ベクトル
            double uy = dy / widthM; // 底辺 Y (East) 方向単位ベクトル

            // 測量空間(Y=東/右, X=北/上)における+90度反時計回り (左/上方向) 垂直単位ベクトル
            double vx = uy;   // North component (P1->P2が東(uy=1)の時, vx=1で北向き)
            double vy = -ux;  // East component

            // p1 から p3 へのベクトルを垂直(v)上に投影して高さ(H)を算出
            double p3dx = p3.X - p1.X;
            double p3dy = p3.Y - p1.Y;
            double heightM = p3dx * vx + p3dy * vy;

            // クロップ長方形枠の中心座標 (測量座標系)
            double cx = p1.X + (widthM / 2.0) * ux + (heightM / 2.0) * vx;
            double cy = p1.Y + (widthM / 2.0) * uy + (heightM / 2.0) * vy;
            XYZ center = new XYZ(cx, cy, 0);

            // 底辺回転角 (ラジアン)
            double angleRad = Math.Atan2(dy, dx);

            return (angleRad, widthM, heightM, ux, uy, vx, vy, center);
        }

        /// <summary>
        /// 高さ指示点 p3 を、p1からの垂線上の点に投影変換する
        /// </summary>
        public static XYZ ProjectToPerpendicular(XYZ p1, XYZ p2, double rawX, double rawY)
        {
            double dx = p2.X - p1.X;
            double dy = p2.Y - p1.Y;
            double widthM = Math.Sqrt(dx * dx + dy * dy);
            if (widthM < 0.0001) return new XYZ(rawX, rawY);

            double ux = dx / widthM;
            double uy = dy / widthM;
            double vx = uy;
            double vy = -ux;

            double p3dx = rawX - p1.X;
            double p3dy = rawY - p1.Y;
            double h = p3dx * vx + p3dy * vy;

            return new XYZ(p1.X + vx * h, p1.Y + vy * h);
        }

        /// <summary>
        /// 長方形枠の4頂点 (V1:左下, V2:右下, V3:右上, V4:左上) を算出する
        /// </summary>
        public static (XYZ v1, XYZ v2, XYZ v3, XYZ v4) GetCropBoxVertices(XYZ p1, XYZ p2, XYZ p3)
        {
            var (_, widthM, heightM, ux, uy, vx, vy, _) = CalculateCropBox(p1, p2, p3);

            XYZ v1 = new XYZ(p1); // 左下
            XYZ v2 = new XYZ(p2); // 右下
            XYZ v3 = new XYZ(p2.X + vx * heightM, p2.Y + vy * heightM); // 右上
            XYZ v4 = new XYZ(p1.X + vx * heightM, p1.Y + vy * heightM); // 左上

            return (v1, v2, v3, v4);
        }

        /// <summary>
        /// 測量座標 (surveyX, surveyY) を p1 基準の 3点クロップローカル座標 (u, v) [単位: m] へ変換する
        /// u: 底辺 p1->p2 方向の距離(m)
        /// v: 垂直方向の距離(m)
        /// </summary>
        public static (double u, double v) SurveyToCropLocal(double surveyX, double surveyY, XYZ p1, XYZ p2)
        {
            double dx = p2.X - p1.X;
            double dy = p2.Y - p1.Y;
            double len = Math.Sqrt(dx * dx + dy * dy);
            if (len < 0.0001) len = 1.0;

            double ux = dx / len;
            double uy = dy / len;
            double vx = uy;
            double vy = -ux;

            double px = surveyX - p1.X;
            double py = surveyY - p1.Y;

            double u = px * ux + py * uy;
            double v = px * vx + py * vy;

            return (u, v);
        }

        /// <summary>
        /// 測量座標 (surveyX, surveyY) を 遺構枠中心 (0,0) を基準としたローカル数学座標 (xLocalM, yLocalM) [単位: m] へ変換する
        /// </summary>
        public static (double xLocalM, double yLocalM) SurveyToFeatureLocalCenter(double surveyX, double surveyY, XYZ p1, XYZ p2, XYZ p3)
        {
            var (_, _, _, ux, uy, vx, vy, center) = CalculateCropBox(p1, p2, p3);

            double px = surveyX - center.X;
            double py = surveyY - center.Y;

            double xLocalM = px * ux + py * uy; // 底辺方向 (横)
            double yLocalM = px * vx + py * vy; // 垂直方向 (縦)

            return (xLocalM, yLocalM);
        }

        /// <summary>
        /// 遺構枠中心 (0,0) 基準のローカル数学座標 (xLocalM, yLocalM) [単位: m] を
        /// 用紙中心 (0,0) 基準の用紙座標 (paperX, paperY) [単位: mm] へ変換する
        /// pp: 用紙中心 (0,0) からの遺構枠中心の配置オフセット (PX, PY) [単位: mm]
        /// </summary>
        public static PointF LocalCenterToPaperPoint(double xLocalM, double yLocalM, Point3D pp, int scale)
        {
            if (scale <= 0) scale = 20;
            double mmPerMeter = 1000.0 / scale; // 例: 1/20 -> 50mm / meter

            float paperX = (float)(pp.X + xLocalM * mmPerMeter);
            float paperY = (float)(pp.Y + yLocalM * mmPerMeter);

            return new PointF(paperX, paperY);
        }

        /// <summary>
        /// 測量座標 (surveyX, surveyY) を直接 用紙中心原点 (0,0) 基準の用紙座標 (paperX, paperY) [単位: mm] へ変換する
        /// </summary>
        public static PointF SurveyToPaperPoint(double surveyX, double surveyY, XYZ p1, XYZ p2, XYZ p3, Point3D pp, int scale)
        {
            var (xLocalM, yLocalM) = SurveyToFeatureLocalCenter(surveyX, surveyY, p1, p2, p3);
            return LocalCenterToPaperPoint(xLocalM, yLocalM, pp, scale);
        }

        /// <summary>
        /// 用紙中心原点 (0,0) 基準の用紙座標 (paperPt) [単位: mm] から実世界測量座標 (surveyX, surveyY) を逆算する
        /// </summary>
        public static Point3D PaperPointToSurvey(PointF paperPt, XYZ p1, XYZ p2, XYZ p3, Point3D pp, int scale)
        {
            if (scale <= 0) scale = 20;
            double metersPerMm = scale / 1000.0;
            double xLocalM = (paperPt.X - pp.X) * metersPerMm;
            double yLocalM = (paperPt.Y - pp.Y) * metersPerMm;

            var (_, _, _, ux, uy, vx, vy, center) = CalculateCropBox(p1, p2, p3);
            double surveyX = center.X + xLocalM * ux + yLocalM * vx;
            double surveyY = center.Y + xLocalM * uy + yLocalM * vy;

            return new Point3D(surveyX, surveyY, 0);
        }

        /// <summary>
        /// 測量座標直線 (2点指定) と用紙外枠 (長方形 [-halfW, +halfW] x [-halfH, +halfH]) の交点を算出
        /// </summary>
        public static List<(PointF paperMmPt, string borderSide)> FindGridLinePaperIntersections(
            PointF linePt1, PointF linePt2, double halfW, double halfH)
        {
            var results = new List<(PointF paperMmPt, string borderSide)>();

            double x1 = linePt1.X, y1 = linePt1.Y;
            double x2 = linePt2.X, y2 = linePt2.Y;
            double dx = x2 - x1;
            double dy = y2 - y1;

            if (Math.Abs(dx) < 1e-9 && Math.Abs(dy) < 1e-9) return results;

            // 1. 下枠: y = -halfH
            if (Math.Abs(dy) > 1e-9)
            {
                double t = (-halfH - y1) / dy;
                double ix = x1 + t * dx;
                if (ix >= -halfW - 0.01 && ix <= halfW + 0.01)
                    results.Add((new PointF((float)ix, (float)-halfH), "Bottom"));
            }

            // 2. 上枠: y = +halfH
            if (Math.Abs(dy) > 1e-9)
            {
                double t = (halfH - y1) / dy;
                double ix = x1 + t * dx;
                if (ix >= -halfW - 0.01 && ix <= halfW + 0.01)
                    results.Add((new PointF((float)ix, (float)halfH), "Top"));
            }

            // 3. 左枠: x = -halfW
            if (Math.Abs(dx) > 1e-9)
            {
                double t = (-halfW - x1) / dx;
                double iy = y1 + t * dy;
                if (iy >= -halfH - 0.01 && iy <= halfH + 0.01)
                    results.Add((new PointF((float)-halfW, (float)iy), "Left"));
            }

            // 4. 右枠: x = +halfW
            if (Math.Abs(dx) > 1e-9)
            {
                double t = (+halfW - x1) / dx;
                double iy = y1 + t * dy;
                if (iy >= -halfH - 0.01 && iy <= halfH + 0.01)
                    results.Add((new PointF((float)+halfW, (float)iy), "Right"));
            }

            return results;
        }
    }
}
