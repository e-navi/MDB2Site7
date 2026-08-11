using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace Site7DrawingEditor.Services
{
    public class DrawingDbManager
    {
        public string CurrentDbPath { get; set; } = "";

        public BindingList<DrawingModel> DrawingsList { get; } = new BindingList<DrawingModel>();
        public BindingList<DrawingIkouModel> DrawingIkousList { get; } = new BindingList<DrawingIkouModel>();
        public BindingList<MasterIkouModel> MasterIkouList { get; } = new BindingList<MasterIkouModel>();
        public BindingList<MasterIkouLModel> MasterIkouLList { get; } = new BindingList<MasterIkouLModel>();
        public BindingList<MasterIbutuModel> MasterIbutuList { get; } = new BindingList<MasterIbutuModel>();
        public BindingList<MasterKikaiModel> MasterKikaiList { get; } = new BindingList<MasterKikaiModel>();
        public BindingList<MasterLayerModel> MasterLayerList { get; } = new BindingList<MasterLayerModel>();
        public BindingList<DanmenRec> DanmenList { get; } = new BindingList<DanmenRec>();

        public void LoadDatabase(string dbPath)
        {
            if (!File.Exists(dbPath)) return;
            CurrentDbPath = dbPath;

            DrawingsList.Clear();
            DrawingIkousList.Clear();
            MasterIkouList.Clear();
            MasterIkouLList.Clear();
            MasterIbutuList.Clear();
            MasterKikaiList.Clear();

            var (drawings, drawingIkous) = SqliteDrawingManager.LoadDrawings(dbPath);
            foreach (var d in drawings) DrawingsList.Add(d);
            foreach (var di in drawingIkous) DrawingIkousList.Add(di);

            var (ikouList, ikouLList, ibutuList, kikaiList, layerList) = SqliteDrawingManager.LoadMasterSurveyData(dbPath);
            foreach (var ik in ikouList) MasterIkouList.Add(ik);
            foreach (var ikl in ikouLList) MasterIkouLList.Add(ikl);
            foreach (var ib in ibutuList) MasterIbutuList.Add(ib);
            foreach (var k in kikaiList) MasterKikaiList.Add(k);
            MasterLayerList.Clear();
            foreach (var ly in layerList) MasterLayerList.Add(ly);

            if (DrawingsList.Count == 0 && MasterIkouList.Count > 0)
            {
                var defaultDrawing = new DrawingModel { ZID = 1, Name = "図面1", PaperSize = 3, Scale = 20, Type = 1 };
                DrawingsList.Add(defaultDrawing);

                var firstIkou = MasterIkouList[0];
                long selId = firstIkou.Id;
                string featureName = string.IsNullOrWhiteSpace(firstIkou.Name) ? $"遺構{selId}" : firstIkou.Name;

                var defaultIkouModel = new DrawingIkouModel
                {
                    ZID = 1,
                    IID = 1,
                    Name = featureName,
                    PP = new Point3D(0, 0, 0)
                };

                var lines = MasterIkouLList.Where(l => l.Id == selId).ToList();
                var pts = new List<Point3D>();
                foreach (var line in lines)
                {
                    pts.AddRange(SqliteDrawingManager.ParsePrecsText(line.Precs));
                }

                if (pts.Count > 0)
                {
                    double minX = pts.Min(p => p.X);
                    double maxX = pts.Max(p => p.X);
                    double minY = pts.Min(p => p.Y);
                    double maxY = pts.Max(p => p.Y);

                    defaultIkouModel.P1 = new XYZ(minX - 0.5, minY - 0.5);
                    defaultIkouModel.P2 = new XYZ(maxX + 0.5, minY - 0.5);
                    defaultIkouModel.P3 = new XYZ(maxX + 0.5, maxY + 0.5);
                }

                AutoExtractFeatureLines(defaultIkouModel, featureName);
                DrawingIkousList.Add(defaultIkouModel);
            }
        }

        public void SaveDatabase(string dbPath)
        {
            if (string.IsNullOrEmpty(dbPath) || !File.Exists(dbPath)) return;
            SqliteDrawingManager.SaveDrawings(dbPath, DrawingsList.ToList(), DrawingIkousList.ToList());
        }

        public (string message, bool isSuccess) AutoExtractFeatureLines(DrawingIkouModel item, string customFeatureName = "")
        {
            item.LList.Clear();
            string targetName = string.IsNullOrWhiteSpace(customFeatureName) ? item.Name : customFeatureName;

            var matchedMasterIkou = MasterIkouList.FirstOrDefault(ik =>
                (!string.IsNullOrWhiteSpace(ik.Name) && ik.Name.Equals(targetName, StringComparison.OrdinalIgnoreCase)) ||
                ($"遺構{ik.Id}".Equals(targetName, StringComparison.OrdinalIgnoreCase))
            );

            if (matchedMasterIkou != null)
            {
                long singleFeatureId = matchedMasterIkou.Id;
                var targetLines = MasterIkouLList.Where(l => l.Id == singleFeatureId).ToList();
                foreach (var line in targetLines)
                {
                    var pts = SqliteDrawingManager.ParsePrecsText(line.Precs);
                    if (pts.Count == 0) continue;
                    int flag = line.Mode == 1 ? 1 : 0;
                    item.LList.Add(new ZIkouLRec((int)line.Lid, line.Layer, flag, pts));
                }

                item.LListStr = item.LList2Str();
                return ($"✔ 単一遺構 [{targetName}] (ID: {singleFeatureId}) を更新・抽出しました", true);
            }
            else
            {
                var (_, widthM, heightM, _, _, _, _, _) = GeometryMath.CalculateCropBox(item.P1, item.P2, item.P3);

                int extractedCount = 0;
                foreach (var line in MasterIkouLList)
                {
                    var pts = SqliteDrawingManager.ParsePrecsText(line.Precs);
                    if (pts.Count == 0) continue;

                    bool isInsideCropBox = pts.Any(p =>
                    {
                        var (u, v) = GeometryMath.SurveyToCropLocal(p.X, p.Y, item.P1, item.P2);
                        return (u >= -0.5 && u <= widthM + 0.5 && v >= -0.5 && v <= heightM + 0.5);
                    });

                    if (isInsideCropBox)
                    {
                        int flag = line.Mode == 1 ? 1 : 0;
                        item.LList.Add(new ZIkouLRec((int)line.Lid, line.Layer, flag, pts));
                        extractedCount++;
                    }
                }

                item.LListStr = item.LList2Str();
                return ($"✔ 遺構範囲 [{targetName}] (枠内 {extractedCount} 遺構線) を更新・抽出しました", true);
            }
        }
    }
}
