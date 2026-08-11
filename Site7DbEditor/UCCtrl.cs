using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Site7DbEditor
{
    public partial class UCCtrl : UserControl
    {
        public event Action<double, double, double>? CoordinateReceived;

        class BluetoothComPortInfo
        {
            public string PortName { get; set; } = "";
            public string Direction { get; set; } = "";
            public string DisplayName { get; set; } = "";
        }

        private System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
        private List<BluetoothComPortInfo> Ports = new List<BluetoothComPortInfo>();

        public UCCtrl()
        {
            InitializeComponent();

            Ports = GetBluetoothComPorts();

            tabControl4.SelectedTab = tabTS;
            if (Env.TS >= 0 && Env.TS < cBoxTS.Items.Count) cBoxTS.SelectedIndex = Env.TS;
            if (Env.GPS >= 0 && Env.GPS < cBoxGPS.Items.Count) cBoxGPS.SelectedIndex = Env.GPS;
            if (Env.KeiNum - 1 >= 0 && Env.KeiNum - 1 < cBoxKei.Items.Count) cBoxKei.SelectedIndex = Env.KeiNum - 1;
            if (Env.GPSStatus >= 0 && Env.GPSStatus < cBoxGPSStatus.Items.Count) cBoxGPSStatus.SelectedIndex = Env.GPSStatus;

            Kikaikou1.Text = gbl.KikaiMan.kh.ToString("0.000");
            Mirrorkou1.Text = gbl.KikaiMan.mh.ToString("0.000");

            gbl.TStation.btnAutoTsuibi = btnAutoTsuibi;
            gbl.TStation.searchAreaBtn = btnSearch;
            gbl.TStation.chbContMeasure = chbContMeasure;

            setSerialPort();
            SetBtns();
        }

        public void setSerialPort()
        {
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();

            int i1 = -1, i2 = -1;
            for (int i = 0; i < Ports.Count; i++)
            {
                var port = Ports[i];
                comboBox1.Items.Add(port.PortName + " " + port.DisplayName);
                if (Env.ComPortTS == port.PortName) i1 = i;

                comboBox2.Items.Add(port.PortName + " " + port.DisplayName);
                if (Env.ComPortGPS == port.PortName) i2 = i;
            }

            if (comboBox1.Items.Count == 0)
            {
                string[] sysPorts = SerialPort.GetPortNames();
                foreach (string p in sysPorts)
                {
                    comboBox1.Items.Add(p);
                    comboBox2.Items.Add(p);
                }
            }

            if (i1 >= 0 && i1 < comboBox1.Items.Count) comboBox1.SelectedIndex = i1;
            else if (comboBox1.Items.Count > 0) comboBox1.SelectedIndex = 0;

            if (i2 >= 0 && i2 < comboBox2.Items.Count) comboBox2.SelectedIndex = i2;
            else if (comboBox2.Items.Count > 0) comboBox2.SelectedIndex = 0;
        }

        public int GetTSModel()
        {
            return cBoxTS.SelectedIndex;
        }

        public void SetTextBoxPos(double x, double y, double z)
        {
            if (x == 0.0 && y == 0.0)
            {
                textBoxX.Text = "********";
                textBoxY.Text = "********";
                textBoxZ.Text = "********";
                btnUpdPos.Enabled = false;
                return;
            }

            textBoxX.Text = x.ToString("0.000");
            textBoxY.Text = y.ToString("0.000");
            textBoxZ.Text = z.ToString("0.000");
            btnUpdPos.Enabled = true;

            CoordinateReceived?.Invoke(x, y, z);

            if (btnUpdPos.Enabled && chkAutoSet.Checked)
            {
                if (gbl.FormMain != null)
                {
                    gbl.FormMain.SetCXYZ(x, y, z);
                }
            }
        }

        public void SetTextBoxPos()
        {
            textBoxX.Text = "********";
            textBoxY.Text = "********";
            textBoxZ.Text = "********";
        }

        private void tabControl4_SelectedIndexChanged(object? sender, EventArgs e)
        {
            Env.TSGPS = tabControl4.SelectedIndex;
        }

        public void SetBtns()
        {
            btnUp.Enabled = btnDown.Enabled = (cBoxTS.SelectedIndex == 1 || cBoxTS.SelectedIndex == 2);
            btnLeft.Enabled = btnRight.Enabled = btnLeft2.Enabled = btnRight2.Enabled = (cBoxTS.SelectedIndex != 3);
            btnLight.Enabled = btnSearch.Enabled = trackBar1.Enabled = (cBoxTS.SelectedIndex != 3);
            btnSijun.Enabled = (cBoxTS.SelectedIndex == 1 || cBoxTS.SelectedIndex == 2);
        }

        private void cBoxTS_SelectedIndexChanged(object? sender, EventArgs e)
        {
            Env.TS = cBoxTS.SelectedIndex;
            Env.SaveEnvTS();
            SetBtns();
        }

        private void cBoxGPS_SelectedIndexChanged(object? sender, EventArgs e)
        {
            Env.GPS = cBoxGPS.SelectedIndex;
            Env.SaveEnvGPS();
        }

        private void cBoxKei_SelectedIndexChanged(object? sender, EventArgs e)
        {
            Env.KeiNum = cBoxKei.SelectedIndex + 1;
            Env.SaveEnvGPS();
            gbl.Gps.blxy.SetKei(BLXY.SKEI_WLD, Env.KeiNum);
        }

        private void comboBox1_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex >= 0 && comboBox1.SelectedIndex < Ports.Count)
            {
                Env.ComPortTS = Ports[comboBox1.SelectedIndex].PortName;
                Env.SaveEnvTS();
            }
        }

        private void comboBox2_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex >= 0 && comboBox2.SelectedIndex < Ports.Count)
            {
                Env.ComPortGPS = Ports[comboBox2.SelectedIndex].PortName;
                Env.SaveEnvGPS();
            }
        }

        private void cBoxGPSStatus_SelectedIndexChanged(object? sender, EventArgs e)
        {
            Env.GPSStatus = cBoxGPSStatus.SelectedIndex;
            Env.SaveEnvGPS();
        }

        private void btnConnect_Click(object? sender, EventArgs e)
        {
            Button? btn = sender as Button;
            if (btn == null) return;

            if ((string)btn.Tag == "1")
            {
                if (btn.Text == "接続")
                {
                    gbl.TStation.SetCom(Env.ComPortTS);
                    gbl.TStation.Connect();
                    if (gbl.TStation.isConnect)
                    {
                        timer1.Enabled = true;
                        btn.Text = "切断";
                    }
                }
                else
                {
                    gbl.TStation.DisConnect();
                    timer1.Enabled = false;
                    btn.Text = "接続";
                }
            }
            else
            {
                gbl.Gps.SetCom(Env.ComPortGPS);
                gbl.Gps.Connect();
                timer1.Enabled = true;
            }
        }

        private void Kikaikou1_TextChanged(object? sender, EventArgs e)
        {
            TextBox? tb = sender as TextBox;
            if (tb == null) return;
            if (double.TryParse(tb.Text, out double h))
            {
                if ((string)tb.Tag == "1") gbl.KikaiMan.kh = h;
                if ((string)tb.Tag == "2") gbl.KikaiMan.mh = h;
                if ((string)tb.Tag == "3") gbl.Gps.kh = h;
            }
        }

        private void btnDefKikaiBack_Click(object? sender, EventArgs e)
        {
            MessageBox.Show("器械点設定ダイアログ", "器械点設定", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void SelKikaiTenBackTenBtn1_Click(object? sender, EventArgs e)
        {
            gbl.KikaiMan.showForm();
        }

        private void trackBar1_Scroll(object? sender, EventArgs e)
        {
            gbl.TStation.rsspd = trackBar1.Value * 2;
        }

        private void btnAutoTsuibi_Click(object? sender, EventArgs e)
        {
            Button? btn = sender as Button;
            if (btn == null) return;
            int tag = St7Lib.CheckInt((string)btn.Tag, 0);
            int tsModel = GetTSModel();

            if (tsModel == 0) gbl.TStation.LN100_BtnClick(btn, tag);
            else gbl.TStation.AS_BtnClick(btn, tag);
        }

        private void btnUp_MouseDown(object? sender, MouseEventArgs e)
        {
            Button? btn = sender as Button;
            if (btn == null) return;
            int tag = St7Lib.CheckInt((string)btn.Tag, 0);
            if (GetTSModel() == 0) gbl.TStation.LN100_MouseDown(btn, tag);
            else gbl.TStation.AS_MouseDown(btn, tag);
            stopwatch.Restart();
        }

        private void btnUp_MouseUp(object? sender, MouseEventArgs e)
        {
            Button? btn = sender as Button;
            if (btn == null) return;
            int tag = St7Lib.CheckInt((string)btn.Tag, 0);
            stopwatch.Stop();

            if (GetTSModel() == 0) gbl.TStation.LN100_MouseUp(btn, tag, stopwatch.ElapsedMilliseconds);
            else gbl.TStation.AS_MouseUp(btn, tag, stopwatch.ElapsedMilliseconds);
        }

        private void button8_Click(object? sender, EventArgs e)
        {
            if (double.TryParse(textBoxX.Text, out double x) &&
                double.TryParse(textBoxY.Text, out double y) &&
                double.TryParse(textBoxZ.Text, out double z))
            {
                if (gbl.FormMain != null)
                {
                    gbl.FormMain.SetCXYZ(x, y, z);
                }
                CoordinateReceived?.Invoke(x, y, z);
            }
        }

        private void timer1_Tick(object? sender, EventArgs e)
        {
            if (gbl.TStation.isChangePos)
            {
                gbl.TStation.isChangePos = false;
                SetTextBoxPos(gbl.TStation.curPos.X, gbl.TStation.curPos.Y, gbl.TStation.curPos.Z);
            }
            if (gbl.Gps.isChangePos)
            {
                gbl.Gps.isChangePos = false;
                SetTextBoxPos(gbl.Gps.curPos.X, gbl.Gps.curPos.Y, gbl.Gps.curPos.Z);
            }
        }

        private void chbContMeasure_CheckedChanged(object? sender, EventArgs e)
        {
        }

        private static List<BluetoothComPortInfo> GetBluetoothComPorts()
        {
            var result = new List<BluetoothComPortInfo>();
            const string bthentumPath = @"SYSTEM\CurrentControlSet\Enum\BTHENUM";

            var deviceNamesByMac = GetBluetoothDeviceNames();

            try
            {
                using (var bthKey = Registry.LocalMachine.OpenSubKey(bthentumPath))
                {
                    if (bthKey == null)
                        return result;

                    foreach (var typeKeyName in bthKey.GetSubKeyNames())
                    {
                        using (var typeKey = bthKey.OpenSubKey(typeKeyName))
                        {
                            if (typeKey == null)
                                continue;

                            foreach (var instanceKeyName in typeKey.GetSubKeyNames())
                            {
                                using (var instanceKey = typeKey.OpenSubKey($@"{instanceKeyName}\Device Parameters"))
                                {
                                    if (instanceKey == null)
                                        continue;

                                    var portNameObj = instanceKey.GetValue("PortName");
                                    if (portNameObj == null)
                                        continue;

                                    string portName = portNameObj.ToString()!;

                                    string direction = "不明";
                                    if (typeKeyName.IndexOf("_LOCALMFG", StringComparison.OrdinalIgnoreCase) >= 0)
                                        direction = "着信";
                                    else if (typeKeyName.IndexOf("_DEV_", StringComparison.OrdinalIgnoreCase) >= 0 ||
                                             typeKeyName.IndexOf("_VID&", StringComparison.OrdinalIgnoreCase) >= 0 ||
                                             typeKeyName.IndexOf("_PID&", StringComparison.OrdinalIgnoreCase) >= 0)
                                        direction = "発信";

                                    string displayName = "";
                                    using (var parentInstanceKey = typeKey.OpenSubKey(instanceKeyName))
                                    {
                                        string? friendlyName = parentInstanceKey?.GetValue("FriendlyName")?.ToString();
                                        string? serviceDesc = parentInstanceKey?.GetValue("DeviceDesc")?.ToString();

                                        if (serviceDesc != null && serviceDesc.StartsWith("@"))
                                        {
                                            var parts = serviceDesc.Split(';');
                                            if (parts.Length > 1)
                                                serviceDesc = parts[parts.Length - 1];
                                        }

                                        bool isGenericFriendly = !string.IsNullOrEmpty(friendlyName) &&
                                                               (friendlyName.Contains("(COM") || friendlyName.Contains("Bluetooth リンク"));

                                        if (!string.IsNullOrEmpty(friendlyName) && !isGenericFriendly)
                                        {
                                            displayName = friendlyName;
                                        }
                                        else
                                        {
                                            string? mac = ExtractMac(typeKeyName);
                                            if (string.IsNullOrEmpty(mac))
                                                mac = ExtractMac(instanceKeyName);

                                            string? deviceName = null;
                                            if (!string.IsNullOrEmpty(mac))
                                            {
                                                deviceNamesByMac.TryGetValue(mac.ToLower(), out deviceName);
                                            }

                                            if (!string.IsNullOrEmpty(deviceName))
                                            {
                                                displayName = deviceName;
                                            }
                                            else
                                            {
                                                displayName = serviceDesc ?? friendlyName ?? "Outgoing";
                                            }
                                        }
                                    }
                                    if (direction == "発信")
                                    {
                                        result.Add(new BluetoothComPortInfo
                                        {
                                            PortName = portName,
                                            Direction = direction,
                                            DisplayName = displayName
                                        });
                                    }
                                }
                            }
                        }
                    }
                }

                result.Sort((a, b) =>
                {
                    int na = ExtractNumber(a.PortName);
                    int nb = ExtractNumber(b.PortName);
                    if (na != nb)
                        return na.CompareTo(nb);
                    return string.Compare(a.PortName, b.PortName, StringComparison.Ordinal);
                });
            }
            catch { }

            return result;
        }

        private static Dictionary<string, string> GetBluetoothDeviceNames()
        {
            var results = new Dictionary<string, string>();

            try
            {
                using (var devicesKey = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\BTHPORT\Parameters\Devices"))
                {
                    if (devicesKey != null)
                    {
                        foreach (var mac in devicesKey.GetSubKeyNames())
                        {
                            using (var devKey = devicesKey.OpenSubKey(mac))
                            {
                                var name = devKey?.GetValue("Name");
                                string? deviceName = null;
                                if (name is byte[] bytes)
                                    deviceName = System.Text.Encoding.UTF8.GetString(bytes).TrimEnd('\0');
                                else
                                    deviceName = name?.ToString();

                                if (!string.IsNullOrEmpty(deviceName))
                                    results[mac.ToLower()] = deviceName;
                            }
                        }
                    }
                }
            }
            catch { }

            try
            {
                using (var bthKey = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Enum\BTHENUM"))
                {
                    if (bthKey != null)
                    {
                        foreach (var typeKeyName in bthKey.GetSubKeyNames())
                        {
                            string? mac = ExtractMac(typeKeyName);
                            if (string.IsNullOrEmpty(mac))
                                continue;

                            using (var typeKey = bthKey.OpenSubKey(typeKeyName))
                            {
                                if (typeKey == null) continue;
                                foreach (var instKeyName in typeKey.GetSubKeyNames())
                                {
                                    using (var instKey = typeKey.OpenSubKey(instKeyName))
                                    {
                                        var friendly = instKey?.GetValue("FriendlyName")?.ToString();
                                        if (!string.IsNullOrEmpty(friendly) && !friendly.Contains("(COM"))
                                        {
                                            results[mac.ToLower()] = friendly;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch { }

            return results;
        }

        private static string? ExtractMac(string text)
        {
            if (string.IsNullOrEmpty(text))
                return null;
            var match = Regex.Match(text, @"(?:DEV_|Dev_)([0-9A-F]{12})", RegexOptions.IgnoreCase);
            if (match.Success)
                return match.Groups[1].Value;

            match = Regex.Match(text, @"(?:^|&|_)([0-9A-F]{12})(?:$|&|_)", RegexOptions.IgnoreCase);
            if (match.Success)
                return match.Groups[1].Value;

            return null;
        }

        private static int ExtractNumber(string text)
        {
            var match = Regex.Match(text, @"\d+");
            return match.Success ? int.Parse(match.Value) : 0;
        }
    }
}
