using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Site7DbEditor.Services
{
    public static class EditorLayerService
    {
        public static readonly Color[] PaletteColors = new Color[]
        {
            Color.FromArgb(0, 225, 255),   // 1 Cyan
            Color.FromArgb(50, 205, 50),   // 2 Lime Green
            Color.FromArgb(255, 191, 0),   // 3 Amber
            Color.FromArgb(255, 0, 128),   // 4 Pink
            Color.FromArgb(153, 102, 255), // 5 Purple
            Color.FromArgb(255, 128, 0),   // 6 Orange
            Color.FromArgb(0, 204, 153),   // 7 Teal
            Color.FromArgb(239, 35, 60),   // 8 Red
            Color.FromArgb(72, 202, 228),  // 9 Light Blue
            Color.FromArgb(144, 224, 239), // 10 Sky Blue
            Color.FromArgb(128, 255, 0),   // 11 Bright Lime
            Color.FromArgb(255, 214, 10),  // 12 Yellow
            Color.FromArgb(255, 112, 166), // 13 Rose
            Color.FromArgb(181, 23, 158),  // 14 Magenta
            Color.FromArgb(114, 9, 183),   // 15 Deep Purple
            Color.FromArgb(67, 97, 238)    // 16 Royal Blue
        };

        public static readonly Color[] LayerTableColors = new Color[]
        {
            Color.FromArgb(200, 200, 200), // 0: デフォルト灰/白
            Color.FromArgb(240, 240, 240), // 1:  黒 (キャンバス上では視認性のため白/明灰 240, 240, 240)
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

        public static Color GetLayerColor(int layerId, IEnumerable<LayerModel> layerList)
        {
            int searchId = layerId;
            var matchedLayer = layerList.FirstOrDefault(l => l.Id == searchId);
            if (matchedLayer == null && layerId >= 1 && layerId <= 16)
            {
                matchedLayer = layerList.FirstOrDefault(l => l.Id == (layerId + 48));
            }

            if (matchedLayer != null)
            {
                int colorIdx = matchedLayer.Color;
                if (colorIdx >= 1 && colorIdx < LayerTableColors.Length)
                {
                    Color col = LayerTableColors[colorIdx];
                    if (col.R == 0 && col.G == 0 && col.B == 0) return Color.FromArgb(240, 240, 240);
                    return col;
                }
            }

            int fallbackIdx = ((layerId - 1) % 16) + 1;
            if (fallbackIdx < 1 || fallbackIdx >= LayerTableColors.Length) fallbackIdx = 1;
            return LayerTableColors[fallbackIdx];
        }

        public static Color GetControlColor(int layerId, IEnumerable<LayerModel> layerList)
        {
            var matchedLayer = layerList.FirstOrDefault(l => l.Id == layerId);
            if (matchedLayer != null)
            {
                int colorIdx = matchedLayer.Color;
                if (colorIdx >= 1 && colorIdx < LayerTableColors.Length)
                {
                    if (colorIdx == 1) return Color.FromArgb(20, 20, 20);
                    if (colorIdx == 8) return Color.FromArgb(140, 140, 140);
                    return LayerTableColors[colorIdx];
                }
            }

            int fallbackIdx = ((layerId - 1) % 16) + 1;
            if (fallbackIdx == 1) return Color.FromArgb(20, 20, 20);
            if (fallbackIdx == 8) return Color.FromArgb(140, 140, 140);
            if (fallbackIdx >= 1 && fallbackIdx < LayerTableColors.Length) return LayerTableColors[fallbackIdx];
            return Color.FromArgb(20, 20, 20);
        }
    }
}
