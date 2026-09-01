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
            Color.FromArgb(255, 0, 0),     // 2:  赤 (255, 0, 0)
            Color.FromArgb(0, 255, 0),     // 3:  緑 (0, 255, 0)
            Color.FromArgb(0, 0, 255),     // 4:  青 (0, 0, 255)
            Color.FromArgb(255, 255, 0),   // 5:  黄 (255, 255, 0)
            Color.FromArgb(255, 0, 255),   // 6:  マゼンタ (255, 0, 255)
            Color.FromArgb(0, 255, 255),   // 7:  シアン (0, 255, 255)
            Color.FromArgb(200, 200, 200), // 8:  白 (255, 255, 255 / キャンバス視認性確保)
            Color.FromArgb(192, 0, 128),   // 9:  牡丹 (192, 0, 128)
            Color.FromArgb(192, 128, 64),  // 10: 茶 (192, 128, 64)
            Color.FromArgb(255, 128, 0),   // 11: 橙 (255, 128, 0)
            Color.FromArgb(128, 192, 128), // 12: 薄緑 (128, 192, 128)
            Color.FromArgb(0, 128, 255),   // 13: 明青 (0, 128, 255)
            Color.FromArgb(128, 64, 255),  // 14: 青紫 (128, 64, 255)
            Color.FromArgb(192, 192, 192), // 15: 明灰 (192, 192, 192)
            Color.FromArgb(128, 128, 128)  // 16: 暗灰 (128, 128, 128)
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
