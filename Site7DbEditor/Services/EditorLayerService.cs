using System;
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

        public static Color GetIkouLineColor(int layerNo, bool isDarkBackground = true)
        {
            int idx = Math.Clamp(layerNo, 1, 16);
            return LayerDefinitionService.Instance.GetColor(LayerGroup.Ikou, idx, isDarkBackground);
        }

        public static Color GetIbutuColor(int layerNo, bool isDarkBackground = true)
        {
            int idx = Math.Clamp(layerNo, 1, 16);
            return LayerDefinitionService.Instance.GetColor(LayerGroup.Ibutu, idx, isDarkBackground);
        }

        public static Color GetKikaiColor(int layerNo, bool isDarkBackground = true)
        {
            int idx = Math.Clamp(layerNo, 1, 16);
            return LayerDefinitionService.Instance.GetColor(LayerGroup.Kikai, idx, isDarkBackground);
        }

        public static Color GetSakuzuColor(int layerNo, bool isDarkBackground = true)
        {
            int idx = Math.Clamp(layerNo, 1, 16);
            return LayerDefinitionService.Instance.GetColor(LayerGroup.Sakuzu, idx, isDarkBackground);
        }

        public static Color GetLayerColor(int layerId, IEnumerable<LayerModel>? layerList = null, bool isDarkBackground = true)
        {
            // 遺構 (1..16 または 49..64)
            if (layerId >= 49 && layerId <= 64)
            {
                return GetIkouLineColor(layerId - 48, isDarkBackground);
            }
            if (layerId >= 33 && layerId <= 48)
            {
                return GetSakuzuColor(layerId - 32, isDarkBackground);
            }
            if (layerId >= 17 && layerId <= 32)
            {
                return GetKikaiColor(layerId - 16, isDarkBackground);
            }

            // 1..16 (遺物または遺構)
            return GetIkouLineColor(layerId, isDarkBackground);
        }

        public static Color GetControlColor(int layerId, IEnumerable<LayerModel>? layerList = null, bool isDarkBackground = true)
        {
            return GetLayerColor(layerId, layerList, isDarkBackground);
        }
    }
}
