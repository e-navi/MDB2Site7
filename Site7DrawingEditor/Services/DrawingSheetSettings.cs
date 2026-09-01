using System;
using System.IO;

namespace Site7DrawingEditor.Services
{
    public class DrawingSheetSettings
    {
        private static readonly Lazy<DrawingSheetSettings> _instance = new(() => new DrawingSheetSettings());
        public static DrawingSheetSettings Instance => _instance.Value;

        // 枠余白 (mm)
        public double MarginLeftMm { get; set; } = 10.0;
        public double MarginOtherMm { get; set; } = 6.0;

        // 方位記号 (北矢印)
        public bool ShowNorthArrow { get; set; } = true;
        public string NorthArrowType { get; set; } = "モダン";
        public double NorthArrowSizeMm { get; set; } = 11.0;

        // 縮尺スケールバー
        public bool ShowScaleBar { get; set; } = true;
        public string ScaleBarType { get; set; } = "精密線 (下縮尺)";
        public string ScaleBarPos { get; set; } = "中下";

        // 右下 表題欄・図面情報
        public bool ShowTitleBlock { get; set; } = true;
        public bool ShowDrawingName { get; set; } = true;
        public bool ShowScale { get; set; } = true;
        public bool ShowPaperSize { get; set; } = true;
        public bool ShowAuthor { get; set; } = false;
        public string AuthorText { get; set; } = "";
        public bool ShowDate { get; set; } = false;
        public string DateText { get; set; } = "";

        public DrawingSheetSettings()
        {
            LoadFromIni();
        }

        public void LoadFromIni()
        {
            try
            {
                MarginLeftMm = Def.GetIniDouble("DRAWING_SHEET_SETTINGS", "MarginLeftMm", 10.0);
                MarginOtherMm = Def.GetIniDouble("DRAWING_SHEET_SETTINGS", "MarginOtherMm", 6.0);

                ShowNorthArrow = Def.GetIniInt("DRAWING_SHEET_SETTINGS", "ShowNorthArrow", 1) == 1;
                string nat = Def.GetIniStr("DRAWING_SHEET_SETTINGS", "NorthArrowType");
                NorthArrowType = string.IsNullOrEmpty(nat) ? "モダン" : nat;
                NorthArrowSizeMm = Def.GetIniDouble("DRAWING_SHEET_SETTINGS", "NorthArrowSizeMm", 11.0);

                ShowScaleBar = Def.GetIniInt("DRAWING_SHEET_SETTINGS", "ShowScaleBar", 1) == 1;
                string sbt = Def.GetIniStr("DRAWING_SHEET_SETTINGS", "ScaleBarType");
                ScaleBarType = string.IsNullOrEmpty(sbt) ? "精密線 (下縮尺)" : sbt;
                string sbp = Def.GetIniStr("DRAWING_SHEET_SETTINGS", "ScaleBarPos");
                ScaleBarPos = string.IsNullOrEmpty(sbp) ? "中下" : sbp;

                ShowTitleBlock = Def.GetIniInt("DRAWING_SHEET_SETTINGS", "ShowTitleBlock", 1) == 1;
                ShowDrawingName = Def.GetIniInt("DRAWING_SHEET_SETTINGS", "ShowDrawingName", 1) == 1;
                ShowScale = Def.GetIniInt("DRAWING_SHEET_SETTINGS", "ShowScale", 1) == 1;
                ShowPaperSize = Def.GetIniInt("DRAWING_SHEET_SETTINGS", "ShowPaperSize", 1) == 1;
                ShowAuthor = Def.GetIniInt("DRAWING_SHEET_SETTINGS", "ShowAuthor", 0) == 1;
                AuthorText = Def.GetIniStr("DRAWING_SHEET_SETTINGS", "AuthorText");
                ShowDate = Def.GetIniInt("DRAWING_SHEET_SETTINGS", "ShowDate", 0) == 1;
                DateText = Def.GetIniStr("DRAWING_SHEET_SETTINGS", "DateText");
            }
            catch { }
        }

        public void SaveToIni()
        {
            try
            {
                Def.SetIniDouble("DRAWING_SHEET_SETTINGS", "MarginLeftMm", MarginLeftMm);
                Def.SetIniDouble("DRAWING_SHEET_SETTINGS", "MarginOtherMm", MarginOtherMm);

                Def.SetIniInt("DRAWING_SHEET_SETTINGS", "ShowNorthArrow", ShowNorthArrow ? 1 : 0);
                Def.SetIniStr("DRAWING_SHEET_SETTINGS", "NorthArrowType", NorthArrowType);
                Def.SetIniDouble("DRAWING_SHEET_SETTINGS", "NorthArrowSizeMm", NorthArrowSizeMm);

                Def.SetIniInt("DRAWING_SHEET_SETTINGS", "ShowScaleBar", ShowScaleBar ? 1 : 0);
                Def.SetIniStr("DRAWING_SHEET_SETTINGS", "ScaleBarType", ScaleBarType);
                Def.SetIniStr("DRAWING_SHEET_SETTINGS", "ScaleBarPos", ScaleBarPos);

                Def.SetIniInt("DRAWING_SHEET_SETTINGS", "ShowTitleBlock", ShowTitleBlock ? 1 : 0);
                Def.SetIniInt("DRAWING_SHEET_SETTINGS", "ShowDrawingName", ShowDrawingName ? 1 : 0);
                Def.SetIniInt("DRAWING_SHEET_SETTINGS", "ShowScale", ShowScale ? 1 : 0);
                Def.SetIniInt("DRAWING_SHEET_SETTINGS", "ShowPaperSize", ShowPaperSize ? 1 : 0);
                Def.SetIniInt("DRAWING_SHEET_SETTINGS", "ShowAuthor", ShowAuthor ? 1 : 0);
                Def.SetIniStr("DRAWING_SHEET_SETTINGS", "AuthorText", AuthorText);
                Def.SetIniInt("DRAWING_SHEET_SETTINGS", "ShowDate", ShowDate ? 1 : 0);
                Def.SetIniStr("DRAWING_SHEET_SETTINGS", "DateText", DateText);
            }
            catch { }
        }
    }
}
