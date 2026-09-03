using System.Drawing;

namespace Site7DrawingEditor.Services
{
    public static class LayerManager
    {
        public static readonly Color[] PaletteColors = new Color[]
        {
            Color.LimeGreen,
            Color.Orange,
            Color.Magenta,
            Color.DeepSkyBlue,
            Color.Yellow,
            Color.Crimson,
            Color.Cyan,
            Color.SpringGreen,
            Color.Violet,
            Color.Gold
        };

        public static readonly Color[] LayerTableColors = new Color[]
        {
            Color.FromArgb(0, 0, 0),       // 1:  黒 (0, 0, 0)
            Color.FromArgb(255, 60, 60),   // 2:  赤 (255, 60, 60)
            Color.FromArgb(60, 220, 60),   // 3:  緑 (60, 220, 60)
            Color.FromArgb(60, 160, 255),  // 4:  青 (60, 160, 255)
            Color.FromArgb(255, 235, 50),  // 5:  黄 (255, 235, 50)
            Color.FromArgb(255, 80, 255),  // 6:  マゼンタ (255, 80, 255)
            Color.FromArgb(0, 230, 255),   // 7:  シアン (0, 230, 255)
            Color.FromArgb(255, 255, 255), // 8:  白 (255, 255, 255)
            Color.FromArgb(235, 50, 160),  // 9:  牡丹 (235, 50, 160)
            Color.FromArgb(210, 140, 70),  // 10: 茶 (210, 140, 70)
            Color.FromArgb(255, 140, 30),  // 11: 橙 (255, 140, 30)
            Color.FromArgb(140, 210, 140), // 12: 薄緑 (140, 210, 140)
            Color.FromArgb(30, 160, 255),  // 13: 明青 (30, 160, 255)
            Color.FromArgb(160, 90, 255),  // 14: 青紫 (160, 90, 255)
            Color.FromArgb(200, 200, 200), // 15: 明灰 (200, 200, 200)
            Color.FromArgb(140, 140, 140)  // 16: 暗灰 (140, 140, 140)
        };

        public static Color GetLayerColor(int layer, bool isDarkBackground = false)
        {
            if (layer <= 0) return isDarkBackground ? Color.White : Color.Black;
            return LayerDefinitionService.Instance.GetColor(LayerGroup.Ikou, layer, isDarkBackground);
        }

        public static bool IsLayerCurve(DrawingDbManager db, int layer)
        {
            if (db?.MasterLayerList == null || db.MasterLayerList.Count == 0) return true;

            int normIdx = layer;
            if (normIdx >= 49 && normIdx <= 64) normIdx -= 48;
            if (normIdx > 16) normIdx = ((normIdx - 1) % 16) + 1;

            var layerInfo = db.MasterLayerList.FirstOrDefault(ly =>
                ly.Id == layer ||
                ly.Id == normIdx ||
                ly.Id == normIdx + 48 ||
                ly.Id == (layer % 100));

            if (layerInfo != null)
            {
                return layerInfo.LType == 2;
            }
            return true; // デフォルトは曲線
        }
    }
}
