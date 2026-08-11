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

            int idx = (layer - 1) % LayerTableColors.Length;
            Color col = LayerTableColors[idx];

            // CAD標準動作: 黒背景の時は「黒 (Layer 1: 0,0,0)」を「白 (255,255,255)」に自働反転表示
            if (isDarkBackground && col.R == 0 && col.G == 0 && col.B == 0)
            {
                return Color.White;
            }

            // 白背景の時に「白 (Layer 8: 255,255,255)」なら視認性のため濃い灰色で表示
            if (!isDarkBackground && col.R == 255 && col.G == 255 && col.B == 255)
            {
                return Color.FromArgb(40, 40, 40);
            }

            return col;
        }
    }
}
