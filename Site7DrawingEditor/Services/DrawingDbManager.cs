using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

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
            LoadLayerSettings(dbPath);

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
                var allExtractedPts = new List<Point3D>();

                foreach (var line in targetLines)
                {
                    var pts = SqliteDrawingManager.ParsePrecsText(line.Precs);
                    if (pts.Count == 0) continue;
                    int flag = line.Mode == 1 ? 1 : 0;
                    item.LList.Add(new ZIkouLRec((int)line.Lid, line.Layer, flag, pts));
                    allExtractedPts.AddRange(pts);
                }

                if (allExtractedPts.Count == 0 && (Math.Abs(matchedMasterIkou.X) > 0.001 || Math.Abs(matchedMasterIkou.Y) > 0.001))
                {
                    allExtractedPts.Add(new Point3D(matchedMasterIkou.X, matchedMasterIkou.Y, matchedMasterIkou.Z));
                }

                // 遺構枠が未設定 (0,0) の場合、自動的に遺構範囲から枠を設定
                bool isCropUnset = (Math.Abs(item.P1.X) < 0.001 && Math.Abs(item.P1.Y) < 0.001 &&
                                    Math.Abs(item.P2.X) < 0.001 && Math.Abs(item.P2.Y) < 0.001);

                if (isCropUnset && allExtractedPts.Count > 0)
                {
                    var (p1, p2, p3) = GeometryMath.ComputeDefaultCropBox(allExtractedPts);
                    item.P1 = p1;
                    item.P2 = p2;
                    item.P3 = p3;
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

        private void LoadLayerSettings(string dbPath)
        {
            string? genbaDir = Path.GetDirectoryName(dbPath);
            var candidatePaths = new List<string>();
            if (!string.IsNullOrEmpty(genbaDir))
            {
                candidatePaths.Add(Path.Combine(genbaDir, "Def", "Layer遺構.txt"));
                candidatePaths.Add(Path.Combine(genbaDir, "Layer遺構.txt"));
            }
            candidatePaths.Add(@"C:\SITE7\GENBA\NEW\Def\Layer遺構.txt");
            candidatePaths.Add(@"C:\SITE7\DEF\Layer遺構.txt");

            string? foundFile = candidatePaths.FirstOrDefault(File.Exists);
            if (foundFile != null)
            {
                try
                {
                    Encoding enc;
                    try { enc = Encoding.GetEncoding(932); } catch { enc = Encoding.UTF8; }
                    string[] lines = File.ReadAllLines(foundFile, enc);
                    int lineIdx = 1;
                    foreach (var line in lines)
                    {
                        if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#")) continue;
                        string[] parts = line.Split('\t');
                        if (parts.Length >= 5)
                        {
                            string code = parts[0].Trim();
                            string name = parts[1].Trim();
                            int ltype = int.TryParse(parts[4].Trim(), out int lt) ? lt : 2;

                            int layerNum = lineIdx;
                            if (code.StartsWith("L", StringComparison.OrdinalIgnoreCase) && int.TryParse(code.Substring(1), out int num))
                            {
                                layerNum = num;
                            }

                            var existing1 = MasterLayerList.FirstOrDefault(l => l.Id == layerNum);
                            if (existing1 != null)
                            {
                                existing1.LType = ltype;
                                if (!string.IsNullOrEmpty(name)) existing1.Name = name;
                            }
                            else
                            {
                                MasterLayerList.Add(new MasterLayerModel { Id = layerNum, Name = name, LType = ltype });
                            }

                            var existing2 = MasterLayerList.FirstOrDefault(l => l.Id == layerNum + 48);
                            if (existing2 != null)
                            {
                                existing2.LType = ltype;
                                if (!string.IsNullOrEmpty(name)) existing2.Name = name;
                            }
                            else
                            {
                                MasterLayerList.Add(new MasterLayerModel { Id = layerNum + 48, Name = name, LType = ltype });
                            }

                            lineIdx++;
                        }
                    }
                }
                catch { }
            }
        }
    }
}
