using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace MdbFdbExporter
{
    public partial class FormIkouViewer : Form
    {
        private List<GroupPointData> _allPoints = new List<GroupPointData>();
        private List<string> _allGroups = new List<string>();

        private SplitRule _rule1, _rule2, _rule3;
        private string _pattern1 = "", _pattern2 = "", _pattern3 = "";

        private List<GroupPointData> _currentIkouPoints = new List<GroupPointData>();

        // Vibrant colors for different IKOULINEs
        private static readonly Color[] LineColors = new Color[]
        {
            Color.FromArgb(0, 180, 216),   // Cyan
            Color.FromArgb(56, 176, 0),    // Lime Green
            Color.FromArgb(255, 183, 3),   // Amber
            Color.FromArgb(247, 37, 133),  // Pink/Magenta
            Color.FromArgb(114, 9, 183),   // Purple
            Color.FromArgb(76, 201, 240)   // Sky Blue
        };

        private float _zoomFactorAll = 1.0f;
        private PointF _panOffsetAll = new PointF(0, 0);
        private bool _isPanningAll = false;
        private Point _lastMousePosAll;
        private Point _mouseDownPosAll;

        private float _zoomFactorSelected = 1.0f;
        private PointF _panOffsetSelected = new PointF(0, 0);
        private bool _isPanningSelected = false;
        private Point _lastMousePosSelected;

        // Stores label hitboxes and screen points for click-to-select in bottom canvas
        private Dictionary<string, RectangleF> _ikouLabelRectsAll = new Dictionary<string, RectangleF>(StringComparer.OrdinalIgnoreCase);
        private Dictionary<string, List<PointF>> _ikouScreenPointsAll = new Dictionary<string, List<PointF>>(StringComparer.OrdinalIgnoreCase);

        public FormIkouViewer()
        {
            InitializeComponent();
            SetupGridStyle();

            // Setup events for picCanvas (top selected canvas)
            this.picCanvas.Paint += picCanvas_Paint;
            this.picCanvas.MouseWheel += picCanvasSelected_MouseWheel;
            this.picCanvas.MouseDown += picCanvasSelected_MouseDown;
            this.picCanvas.MouseMove += picCanvasSelected_MouseMove;
            this.picCanvas.MouseUp += picCanvasSelected_MouseUp;
            this.picCanvas.MouseEnter += (s, e) => picCanvas.Focus();
            this.picCanvas.DoubleClick += (s, e) => { _zoomFactorSelected = 1.0f; _panOffsetSelected = PointF.Empty; picCanvas.Invalidate(); };

            // Setup events for picCanvasAll (bottom all features canvas)
            this.picCanvasAll.Paint += picCanvasAll_Paint;
            this.picCanvasAll.MouseWheel += picCanvasAll_MouseWheel;
            this.picCanvasAll.MouseDown += picCanvasAll_MouseDown;
            this.picCanvasAll.MouseMove += picCanvasAll_MouseMove;
            this.picCanvasAll.MouseUp += picCanvasAll_MouseUp;
            this.picCanvasAll.MouseEnter += (s, e) => picCanvasAll.Focus();
            this.picCanvasAll.DoubleClick += (s, e) => { _zoomFactorAll = 1.0f; _panOffsetAll = PointF.Empty; picCanvasAll.Invalidate(); };

            this.cmbIkouSelect.SelectedIndexChanged += cmbIkouSelect_SelectedIndexChanged;
            this.picCanvas.Resize += (s, e) => picCanvas.Invalidate();
            this.picCanvasAll.Resize += (s, e) => picCanvasAll.Invalidate();
        }

        #region Mouse Zoom, Pan & Click Handlers

        private void picCanvasAll_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Middle)
            {
                _isPanningAll = true;
                _lastMousePosAll = e.Location;
                _mouseDownPosAll = e.Location;
            }
        }

        private void picCanvasAll_MouseMove(object? sender, MouseEventArgs e)
        {
            if (_isPanningAll)
            {
                int dx = e.X - _lastMousePosAll.X;
                int dy = e.Y - _lastMousePosAll.Y;
                _panOffsetAll = new PointF(_panOffsetAll.X + dx, _panOffsetAll.Y + dy);
                _lastMousePosAll = e.Location;
                picCanvasAll.Cursor = Cursors.SizeAll;
                picCanvasAll.Invalidate();
            }
            else
            {
                // Hover check for clickable IKOU labels or points
                bool hoverHit = false;
                foreach (var kvp in _ikouLabelRectsAll)
                {
                    var rect = kvp.Value;
                    rect.Inflate(6f, 6f);
                    if (rect.Contains(e.Location))
                    {
                        hoverHit = true;
                        break;
                    }
                }
                if (!hoverHit)
                {
                    foreach (var kvp in _ikouScreenPointsAll)
                    {
                        foreach (var pt in kvp.Value)
                        {
                            double dist = Math.Sqrt(Math.Pow(pt.X - e.X, 2) + Math.Pow(pt.Y - e.Y, 2));
                            if (dist < 10)
                            {
                                hoverHit = true;
                                break;
                            }
                        }
                        if (hoverHit) break;
                    }
                }
                picCanvasAll.Cursor = hoverHit ? Cursors.Hand : Cursors.Default;
            }
        }

        private void picCanvasAll_MouseUp(object? sender, MouseEventArgs e)
        {
            if (_isPanningAll)
            {
                _isPanningAll = false;
                
                // Calculate drag distance to distinguish click from pan
                int dist = Math.Abs(e.X - _mouseDownPosAll.X) + Math.Abs(e.Y - _mouseDownPosAll.Y);
                if (dist < 5 && e.Button == MouseButtons.Left)
                {
                    string? foundIkou = null;
                    
                    // 1. Check if clicked on an IKOU label box
                    foreach (var kvp in _ikouLabelRectsAll)
                    {
                        var rect = kvp.Value;
                        rect.Inflate(8f, 8f);
                        if (rect.Contains(e.Location))
                        {
                            foundIkou = kvp.Key;
                            break;
                        }
                    }

                    // 2. If not label, check if clicked near any IKOU vertex point
                    if (foundIkou == null)
                    {
                        foreach (var kvp in _ikouScreenPointsAll)
                        {
                            foreach (var pt in kvp.Value)
                            {
                                double d = Math.Sqrt(Math.Pow(pt.X - e.X, 2) + Math.Pow(pt.Y - e.Y, 2));
                                if (d < 12)
                                {
                                    foundIkou = kvp.Key;
                                    break;
                                }
                            }
                            if (foundIkou != null) break;
                        }
                    }

                    // Select clicked IKOU in combo box
                    if (!string.IsNullOrEmpty(foundIkou) && cmbIkouSelect.Items.Contains(foundIkou))
                    {
                        cmbIkouSelect.SelectedItem = foundIkou;
                    }
                }
            }
        }

        private void picCanvasAll_MouseWheel(object? sender, MouseEventArgs e)
        {
            float oldZoom = _zoomFactorAll;
            float scaleFactor = e.Delta > 0 ? 1.15f : (1.0f / 1.15f);
            float newZoom = Math.Clamp(oldZoom * scaleFactor, 0.1f, 30.0f);

            if (Math.Abs(newZoom - oldZoom) > 0.0001f)
            {
                float ratio = newZoom / oldZoom;
                float cx = picCanvasAll.Width / 2f;
                float cy = picCanvasAll.Height / 2f;

                _panOffsetAll.X = e.X - cx - (e.X - cx - _panOffsetAll.X) * ratio;
                _panOffsetAll.Y = e.Y - cy - (e.Y - cy - _panOffsetAll.Y) * ratio;
                _zoomFactorAll = newZoom;

                picCanvasAll.Invalidate();
            }
        }

        private void picCanvasSelected_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Middle)
            {
                _isPanningSelected = true;
                _lastMousePosSelected = e.Location;
                picCanvas.Cursor = Cursors.SizeAll;
            }
        }

        private void picCanvasSelected_MouseMove(object? sender, MouseEventArgs e)
        {
            if (_isPanningSelected)
            {
                int dx = e.X - _lastMousePosSelected.X;
                int dy = e.Y - _lastMousePosSelected.Y;
                _panOffsetSelected = new PointF(_panOffsetSelected.X + dx, _panOffsetSelected.Y + dy);
                _lastMousePosSelected = e.Location;
                picCanvas.Invalidate();
            }
        }

        private void picCanvasSelected_MouseUp(object? sender, MouseEventArgs e)
        {
            if (_isPanningSelected)
            {
                _isPanningSelected = false;
                picCanvas.Cursor = Cursors.Default;
            }
        }

        private void picCanvasSelected_MouseWheel(object? sender, MouseEventArgs e)
        {
            float oldZoom = _zoomFactorSelected;
            float scaleFactor = e.Delta > 0 ? 1.15f : (1.0f / 1.15f);
            float newZoom = Math.Clamp(oldZoom * scaleFactor, 0.1f, 30.0f);

            if (Math.Abs(newZoom - oldZoom) > 0.0001f)
            {
                float ratio = newZoom / oldZoom;
                float cx = picCanvas.Width / 2f;
                float cy = picCanvas.Height / 2f;

                _panOffsetSelected.X = e.X - cx - (e.X - cx - _panOffsetSelected.X) * ratio;
                _panOffsetSelected.Y = e.Y - cy - (e.Y - cy - _panOffsetSelected.Y) * ratio;
                _zoomFactorSelected = newZoom;

                picCanvas.Invalidate();
            }
        }

        #endregion

        public void InitializeViewer(
            string targetIkou,
            List<GroupPointData> pointData,
            List<string> groupNames,
            SplitRule rule1, string pattern1,
            SplitRule rule2, string pattern2,
            SplitRule rule3, string pattern3)
        {
            _allPoints = pointData ?? new List<GroupPointData>();
            _allGroups = groupNames ?? new List<string>();
            _rule1 = rule1; _pattern1 = pattern1;
            _rule2 = rule2; _pattern2 = pattern2;
            _rule3 = rule3; _pattern3 = pattern3;

            // Extract all unique IKOU names
            var ikouSet = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);

            if (_allPoints.Count > 0)
            {
                foreach (var pt in _allPoints)
                {
                    var split = DbHelper.SplitGroupNameChain(pt.GroupName, _rule1, _pattern1, _rule2, _pattern2, _rule3, _pattern3);
                    if (!string.IsNullOrEmpty(split.ikou))
                        ikouSet.Add(split.ikou);
                }
            }
            else
            {
                foreach (var grp in _allGroups)
                {
                    var split = DbHelper.SplitGroupNameChain(grp, _rule1, _pattern1, _rule2, _pattern2, _rule3, _pattern3);
                    if (!string.IsNullOrEmpty(split.ikou))
                        ikouSet.Add(split.ikou);
                }
            }

            cmbIkouSelect.BeginUpdate();
            cmbIkouSelect.Items.Clear();
            foreach (var ikou in ikouSet)
            {
                cmbIkouSelect.Items.Add(ikou);
            }
            cmbIkouSelect.EndUpdate();

            if (!string.IsNullOrEmpty(targetIkou) && cmbIkouSelect.Items.Contains(targetIkou))
            {
                cmbIkouSelect.SelectedItem = targetIkou;
            }
            else if (cmbIkouSelect.Items.Count > 0)
            {
                cmbIkouSelect.SelectedIndex = 0;
            }
        }

        private void SetupGridStyle()
        {
            dgvIkouPoints.EnableHeadersVisualStyles = false;
            dgvIkouPoints.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(43, 45, 66);
            dgvIkouPoints.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvIkouPoints.ColumnHeadersDefaultCellStyle.Font = new Font("Yu Gothic UI", 8.5F, FontStyle.Bold);
            dgvIkouPoints.DefaultCellStyle.BackColor = Color.FromArgb(30, 30, 35);
            dgvIkouPoints.DefaultCellStyle.ForeColor = Color.White;
            dgvIkouPoints.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 180, 216);
            dgvIkouPoints.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvIkouPoints.GridColor = Color.FromArgb(60, 60, 70);
            dgvIkouPoints.BorderStyle = BorderStyle.None;
        }

        private void cmbIkouSelect_SelectedIndexChanged(object? sender, EventArgs e)
        {
            string selectedIkou = cmbIkouSelect.SelectedItem?.ToString() ?? "";
            if (string.IsNullOrEmpty(selectedIkou))
                return;

            // Filter points for selected IKOU
            _currentIkouPoints = _allPoints
                .Where(pt => string.Equals(
                    DbHelper.SplitGroupNameChain(pt.GroupName, _rule1, _pattern1, _rule2, _pattern2, _rule3, _pattern3).ikou,
                    selectedIkou, StringComparison.OrdinalIgnoreCase))
                .ToList();

            // Build Data Table for DataGridView
            var dt = new DataTable();
            dt.Columns.Add("No.", typeof(string));
            dt.Columns.Add("元のグループ名", typeof(string));
            dt.Columns.Add("遺構線名 (IKOULINE)", typeof(string));
            dt.Columns.Add("X", typeof(string));
            dt.Columns.Add("Y", typeof(string));
            dt.Columns.Add("Z", typeof(string));

            var lineSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            double zMin = double.MaxValue, zMax = double.MinValue;

            if (_currentIkouPoints.Count > 0)
            {
                foreach (var pt in _currentIkouPoints)
                {
                    var split = DbHelper.SplitGroupNameChain(pt.GroupName, _rule1, _pattern1, _rule2, _pattern2, _rule3, _pattern3);
                    string lineName = string.IsNullOrEmpty(split.ikouLine) ? "(なし)" : split.ikouLine;
                    lineSet.Add(lineName);

                    if (pt.Z < zMin) zMin = pt.Z;
                    if (pt.Z > zMax) zMax = pt.Z;

                    dt.Rows.Add(pt.PointNo, pt.GroupName, lineName, pt.X.ToString("F3"), pt.Y.ToString("F3"), pt.Z.ToString("F3"));
                }
            }
            else
            {
                // Fallback using group names
                var matchingGroups = _allGroups
                    .Where(g => string.Equals(
                        DbHelper.SplitGroupNameChain(g, _rule1, _pattern1, _rule2, _pattern2, _rule3, _pattern3).ikou,
                        selectedIkou, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                foreach (var g in matchingGroups)
                {
                    var split = DbHelper.SplitGroupNameChain(g, _rule1, _pattern1, _rule2, _pattern2, _rule3, _pattern3);
                    string lineName = string.IsNullOrEmpty(split.ikouLine) ? "(なし)" : split.ikouLine;
                    lineSet.Add(lineName);

                    dt.Rows.Add("-", g, lineName, "-", "-", "-");
                }
            }

            dgvIkouPoints.DataSource = dt;
            if (dgvIkouPoints.Columns.Count >= 6)
            {
                dgvIkouPoints.Columns[0].Width = 50;
                dgvIkouPoints.Columns[1].Width = 120;
                dgvIkouPoints.Columns[2].Width = 100;
                dgvIkouPoints.Columns[3].Width = 80;
                dgvIkouPoints.Columns[4].Width = 80;
                dgvIkouPoints.Columns[5].Width = 70;
            }

            // Update Summary
            string zRangeStr = (zMin != double.MaxValue && zMax != double.MinValue) ? $"{zMin:F2}m ～ {zMax:F2}m" : "-";
            lblSummary.Text = $"全 {_currentIkouPoints.Count:N0} 点 | {lineSet.Count:N0} 線 | 標高 (Z): {zRangeStr}";

            // Trigger 2D Canvases redraw
            picCanvas.Invalidate();
            picCanvasAll.Invalidate();
        }

        private void picCanvas_Paint(object? sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            int width = picCanvas.Width;
            int height = picCanvas.Height;

            // Draw Background Grid
            using (var gridPen = new Pen(Color.FromArgb(40, 40, 50), 1))
            {
                for (int x = 0; x < width; x += 40)
                    g.DrawLine(gridPen, x, 0, x, height);
                for (int y = 0; y < height; y += 40)
                    g.DrawLine(gridPen, 0, y, width, y);
            }

            if (_currentIkouPoints.Count == 0)
            {
                using (var font = new Font("Yu Gothic UI", 10F, FontStyle.Regular))
                using (var brush = new SolidBrush(Color.Gray))
                {
                    g.DrawString("描画する座標点データがありません", font, brush, 20, 20);
                }
                return;
            }

            // Calculate Bounding Box in Japanese Survey Coordinates (X=North, Y=East)
            // Screen Horizontal (X) = Survey Y (East)
            // Screen Vertical (Y) = Survey X (North)
            double posXMin = _currentIkouPoints.Min(p => p.Y);
            double posXMax = _currentIkouPoints.Max(p => p.Y);
            double posYMin = _currentIkouPoints.Min(p => p.X);
            double posYMax = _currentIkouPoints.Max(p => p.X);

            double rangeX = posXMax - posXMin;
            double rangeY = posYMax - posYMin;

            if (rangeX < 0.001) rangeX = 1.0;
            if (rangeY < 0.001) rangeY = 1.0;

            int margin = 35;
            int drawWidth = width - (margin * 2);
            int drawHeight = height - (margin * 2);

            double scaleX = drawWidth / rangeX;
            double scaleY = drawHeight / rangeY;
            double scale = Math.Min(scaleX, scaleY); // Preserve aspect ratio

            // Calculate centering offsets so bounding box is perfectly centered in viewport
            float offsetX = (float)((width - (rangeX * scale)) / 2.0);
            float offsetY = (float)((height - (rangeY * scale)) / 2.0);

            // Transform Survey (X_north, Y_east) to Screen Viewport (px, py)
            float cx = width / 2f;
            float cy = height / 2f;
            PointF ToCanvasPoint(double surveyX, double surveyY)
            {
                double posX = surveyY; // Survey Y (East) -> Screen Horizontal
                double posY = surveyX; // Survey X (North) -> Screen Vertical

                float bx = (float)(offsetX + (posX - posXMin) * scale);
                float by = (float)(height - offsetY - (posY - posYMin) * scale); // Invert Y for screen coordinates
                float px = cx + (bx - cx) * _zoomFactorSelected + _panOffsetSelected.X;
                float py = cy + (by - cy) * _zoomFactorSelected + _panOffsetSelected.Y;
                return new PointF(px, py);
            }

            // Group Points by IKOULINE
            var lineGroups = _currentIkouPoints
                .GroupBy(p => DbHelper.SplitGroupNameChain(p.GroupName, _rule1, _pattern1, _rule2, _pattern2, _rule3, _pattern3).ikouLine)
                .ToList();

            int colorIdx = 0;
            using (var ptFont = new Font("Yu Gothic UI", 7.5F, FontStyle.Regular))
            using (var textBrush = new SolidBrush(Color.FromArgb(200, 200, 200)))
            {
                foreach (var grp in lineGroups)
                {
                    Color color = LineColors[colorIdx % LineColors.Length];
                    colorIdx++;

                    var pointsList = grp.ToList();
                    var screenPts = pointsList.Select(p => ToCanvasPoint(p.X, p.Y)).ToArray();

                    // Draw connecting lines
                    if (screenPts.Length > 1)
                    {
                        using (var linePen = new Pen(color, 2f))
                        {
                            g.DrawLines(linePen, screenPts);
                        }
                    }

                    // Draw point markers and numbers (UNSCALED fixed size: 6px dot)
                    using (var ptBrush = new SolidBrush(color))
                    {
                        for (int i = 0; i < screenPts.Length; i++)
                        {
                            var pt = screenPts[i];
                            g.FillEllipse(ptBrush, pt.X - 3, pt.Y - 3, 6, 6);

                            string label = string.IsNullOrEmpty(pointsList[i].PointNo) ? $"{i + 1}" : pointsList[i].PointNo;
                            g.DrawString(label, ptFont, textBrush, pt.X + 4, pt.Y - 8);
                        }
                    }
                }
            }

            // Draw Legend / List of Feature Lines (IKOULINE List)
            if (lineGroups.Count > 0)
            {
                using (var legendFont = new Font("Yu Gothic UI", 8.5F, FontStyle.Regular))
                using (var headerFont = new Font("Yu Gothic UI", 8.5F, FontStyle.Bold))
                using (var bgBrush = new SolidBrush(Color.FromArgb(200, 20, 20, 28)))
                using (var borderPen = new Pen(Color.FromArgb(60, 60, 75), 1))
                {
                    int itemHeight = 18;
                    int legendHeight = 24 + (lineGroups.Count * itemHeight);
                    int legW = 150;
                    Rectangle legendRect = new Rectangle(width - legW - 10, 10, legW, legendHeight);
                    g.FillRectangle(bgBrush, legendRect);
                    g.DrawRectangle(borderPen, legendRect);

                    using (var titleBrush = new SolidBrush(Color.FromArgb(0, 180, 216)))
                    {
                        g.DrawString("📍 遺構線 (IKOULINE)", headerFont, titleBrush, width - legW + 6, 14);
                    }

                    int legY = 34;
                    int cIdx = 0;
                    foreach (var grp in lineGroups)
                    {
                        string lineName = string.IsNullOrEmpty(grp.Key) ? "(なし)" : grp.Key;
                        Color color = LineColors[cIdx % LineColors.Length];
                        cIdx++;

                        using (var colorBrush = new SolidBrush(color))
                        using (var itemTextBrush = new SolidBrush(Color.FromArgb(220, 220, 225)))
                        {
                            g.FillRectangle(colorBrush, width - legW + 8, legY + 3, 10, 10);
                            string itemText = $"{lineName} ({grp.Count()}点)";
                            g.DrawString(itemText, legendFont, itemTextBrush, width - legW + 22, legY);
                        }
                        legY += itemHeight;
                    }
                }
            }

            // Draw Zoom hint
            if (_zoomFactorSelected != 1.0f || _panOffsetSelected != PointF.Empty)
            {
                using (var hintFont = new Font("Yu Gothic UI", 7.5F))
                using (var hintBrush = new SolidBrush(Color.FromArgb(160, 0, 220, 255)))
                {
                    g.DrawString($"Zoom: {_zoomFactorSelected:F2}x (Wクリックでリセット)", hintFont, hintBrush, 10, height - 20);
                }
            }
        }

        private void picCanvasAll_Paint(object? sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            int width = picCanvasAll.Width;
            int height = picCanvasAll.Height;

            // Clear hit test collections
            _ikouLabelRectsAll.Clear();
            _ikouScreenPointsAll.Clear();

            // Draw Background Grid
            using (var gridPen = new Pen(Color.FromArgb(35, 35, 45), 1))
            {
                for (int x = 0; x < width; x += 40)
                    g.DrawLine(gridPen, x, 0, x, height);
                for (int y = 0; y < height; y += 40)
                    g.DrawLine(gridPen, 0, y, width, y);
            }

            if (_allPoints.Count == 0)
            {
                using (var font = new Font("Yu Gothic UI", 10F, FontStyle.Regular))
                using (var brush = new SolidBrush(Color.Gray))
                {
                    g.DrawString("描画する全遺構データがありません", font, brush, 20, 20);
                }
                return;
            }

            // Calculate Bounding Box across ALL points in Japanese Survey Coordinates (X=North, Y=East)
            // Screen Horizontal (X) = Survey Y (East)
            // Screen Vertical (Y) = Survey X (North)
            double posXMin = _allPoints.Min(p => p.Y);
            double posXMax = _allPoints.Max(p => p.Y);
            double posYMin = _allPoints.Min(p => p.X);
            double posYMax = _allPoints.Max(p => p.X);

            double rangeX = posXMax - posXMin;
            double rangeY = posYMax - posYMin;

            if (rangeX < 0.001) rangeX = 1.0;
            if (rangeY < 0.001) rangeY = 1.0;

            int margin = 35;
            int drawWidth = width - (margin * 2);
            int drawHeight = height - (margin * 2);

            double scaleX = drawWidth / rangeX;
            double scaleY = drawHeight / rangeY;
            double scale = Math.Min(scaleX, scaleY); // Preserve aspect ratio

            // Calculate centering offsets so bounding box is perfectly centered in viewport
            float offsetX = (float)((width - (rangeX * scale)) / 2.0);
            float offsetY = (float)((height - (rangeY * scale)) / 2.0);

            // Transform Survey (X_north, Y_east) to Screen Viewport (px, py)
            float cx = width / 2f;
            float cy = height / 2f;
            PointF ToCanvasPoint(double surveyX, double surveyY)
            {
                double posX = surveyY; // Survey Y (East) -> Screen Horizontal
                double posY = surveyX; // Survey X (North) -> Screen Vertical

                float bx = (float)(offsetX + (posX - posXMin) * scale);
                float by = (float)(height - offsetY - (posY - posYMin) * scale); // Invert Y
                float px = cx + (bx - cx) * _zoomFactorAll + _panOffsetAll.X;
                float py = cy + (by - cy) * _zoomFactorAll + _panOffsetAll.Y;
                return new PointF(px, py);
            }

            string selectedIkou = cmbIkouSelect.SelectedItem?.ToString() ?? "";

            // Group Points by IKOU
            var ikouGroups = _allPoints
                .GroupBy(p => DbHelper.SplitGroupNameChain(p.GroupName, _rule1, _pattern1, _rule2, _pattern2, _rule3, _pattern3).ikou)
                .ToList();

            int colorIdx = 0;
            var ikouColorMap = new Dictionary<string, Color>(StringComparer.OrdinalIgnoreCase);

            foreach (var ikouGroup in ikouGroups)
            {
                string ikouName = string.IsNullOrEmpty(ikouGroup.Key) ? "(未分類)" : ikouGroup.Key;
                Color baseColor = LineColors[colorIdx % LineColors.Length];
                colorIdx++;
                ikouColorMap[ikouName] = baseColor;
            }

            using (var ikouFontNorm = new Font("Yu Gothic UI", 8F, FontStyle.Regular))
            using (var ikouFontSel = new Font("Yu Gothic UI", 9F, FontStyle.Bold))
            {
                // First Pass: Draw Non-Selected IKOUs in Light Gray
                foreach (var ikouGroup in ikouGroups)
                {
                    string ikouName = string.IsNullOrEmpty(ikouGroup.Key) ? "(未分類)" : ikouGroup.Key;
                    bool isSelected = string.Equals(ikouName, selectedIkou, StringComparison.OrdinalIgnoreCase);
                    if (isSelected) continue; // Draw selected IKOU on top in second pass

                    Color lineClr = Color.FromArgb(80, 90, 105);   // Light Gray / Subtle Dim
                    Color dotClr = Color.FromArgb(95, 105, 120);

                    var allScreenPts = new List<PointF>();
                    var lineGroups = ikouGroup
                        .GroupBy(p => DbHelper.SplitGroupNameChain(p.GroupName, _rule1, _pattern1, _rule2, _pattern2, _rule3, _pattern3).ikouLine);

                    foreach (var lineGrp in lineGroups)
                    {
                        var pointsList = lineGrp.ToList();
                        var screenPts = pointsList.Select(p => ToCanvasPoint(p.X, p.Y)).ToArray();
                        allScreenPts.AddRange(screenPts);

                        if (screenPts.Length > 1)
                        {
                            using (var linePen = new Pen(lineClr, 1.2f))
                            {
                                g.DrawLines(linePen, screenPts);
                            }
                        }

                        // Draw UNSCALED fixed-size dots (4px)
                        using (var ptBrush = new SolidBrush(dotClr))
                        {
                            foreach (var pt in screenPts)
                            {
                                g.FillEllipse(ptBrush, pt.X - 2f, pt.Y - 2f, 4f, 4f);
                            }
                        }
                    }

                    _ikouScreenPointsAll[ikouName] = allScreenPts;

                    // Label for non-selected IKOU
                    double avgX = ikouGroup.Average(p => p.X);
                    double avgY = ikouGroup.Average(p => p.Y);
                    PointF centerPt = ToCanvasPoint(avgX, avgY);

                    using (var labelBrush = new SolidBrush(Color.FromArgb(140, 150, 165)))
                    using (var bgBrush = new SolidBrush(Color.FromArgb(140, 20, 20, 25)))
                    {
                        var size = g.MeasureString(ikouName, ikouFontNorm);
                        RectangleF rect = new RectangleF(centerPt.X - 2, centerPt.Y - 14, size.Width + 4, size.Height);
                        g.FillRectangle(bgBrush, rect);
                        g.DrawString(ikouName, ikouFontNorm, labelBrush, centerPt.X, centerPt.Y - 14);

                        _ikouLabelRectsAll[ikouName] = rect;
                    }
                }

                // Second Pass: Draw Selected IKOU on top (Vivid & Highlighted with matching IKOULINE colors)
                foreach (var ikouGroup in ikouGroups)
                {
                    string ikouName = string.IsNullOrEmpty(ikouGroup.Key) ? "(未分類)" : ikouGroup.Key;
                    bool isSelected = string.Equals(ikouName, selectedIkou, StringComparison.OrdinalIgnoreCase);
                    if (!isSelected) continue;

                    var allScreenPts = new List<PointF>();
                    var lineGroups = ikouGroup
                        .GroupBy(p => DbHelper.SplitGroupNameChain(p.GroupName, _rule1, _pattern1, _rule2, _pattern2, _rule3, _pattern3).ikouLine)
                        .ToList();

                    int lineColorIdx = 0;
                    foreach (var lineGrp in lineGroups)
                    {
                        Color lineClr = LineColors[lineColorIdx % LineColors.Length];
                        lineColorIdx++;

                        var pointsList = lineGrp.ToList();
                        var screenPts = pointsList.Select(p => ToCanvasPoint(p.X, p.Y)).ToArray();
                        allScreenPts.AddRange(screenPts);

                        if (screenPts.Length > 1)
                        {
                            using (var linePen = new Pen(lineClr, 2.5f))
                            {
                                g.DrawLines(linePen, screenPts);
                            }
                        }

                        // Draw UNSCALED fixed-size dots (7px)
                        using (var ptBrush = new SolidBrush(lineClr))
                        {
                            foreach (var pt in screenPts)
                            {
                                g.FillEllipse(ptBrush, pt.X - 3.5f, pt.Y - 3.5f, 7f, 7f);
                            }
                        }
                    }

                    _ikouScreenPointsAll[ikouName] = allScreenPts;

                    // Label for selected IKOU
                    double avgX = ikouGroup.Average(p => p.X);
                    double avgY = ikouGroup.Average(p => p.Y);
                    PointF centerPt = ToCanvasPoint(avgX, avgY);

                    using (var labelBrush = new SolidBrush(Color.Yellow))
                    using (var bgBrush = new SolidBrush(Color.FromArgb(220, 10, 10, 15)))
                    {
                        string selText = $"★ {ikouName}";
                        var size = g.MeasureString(selText, ikouFontSel);
                        RectangleF rect = new RectangleF(centerPt.X - 4, centerPt.Y - 16, size.Width + 8, size.Height + 2);
                        g.FillRectangle(bgBrush, rect);
                        using (var borderPen = new Pen(Color.Yellow, 1.5f))
                        {
                            g.DrawRectangle(borderPen, rect.X, rect.Y, rect.Width, rect.Height);
                        }
                        g.DrawString(selText, ikouFontSel, labelBrush, centerPt.X, centerPt.Y - 15);

                        _ikouLabelRectsAll[ikouName] = rect;
                    }
                }
            }

            // Draw Zoom hint on bottom left
            if (_zoomFactorAll != 1.0f || _panOffsetAll != PointF.Empty)
            {
                using (var hintFont = new Font("Yu Gothic UI", 7.5F))
                using (var hintBrush = new SolidBrush(Color.FromArgb(160, 0, 255, 200)))
                {
                    g.DrawString($"Zoom: {_zoomFactorAll:F2}x | ドラッグでパン | クリックで遺構選択 | Wクリックでリセット", hintFont, hintBrush, 10, height - 20);
                }
            }
        }
    }
}
