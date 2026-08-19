using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Site7DbEditor {

    public partial class UCCtrl : UserControl {
        class BluetoothComPortInfo {
            public string PortName { get; set; }
            public string Direction { get; set; }
            public string DisplayName { get; set; }


        }
        private System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
        private List<BluetoothComPortInfo> Ports = GetBluetoothComPorts();

        public UCCtrl() {
            InitializeComponent();

            tabControl4.SelectedIndex = Env.TSGPS;
            cBoxTS.SelectedIndex = Env.TS;
            cBoxGPS.SelectedIndex = Env.GPS;
            cBoxKei.SelectedIndex = Env.KeiNum - 1;
            cBoxGPSStatus.SelectedIndex = Env.GPSStatus;
            Kikaikou1.Text = gbl.KikaiMan.kh.ToString("0.000");
            Mirrorkou1.Text = gbl.KikaiMan.mh.ToString("0.000");

            gbl.TStation.btnAutoTsuibi = btnAutoTsuibi;
            gbl.TStation.searchAreaBtn = btnSearch;
            gbl.TStation.chbContMeasure = chbContMeasure;

            gbl.UCCtrl = this;

            SetBtns();
            setSerialPort();
        }
        public void setSerialPort() {
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();

            Ports = GetBluetoothComPorts();

            // 通常のCOMポート一覧も取得して、Bluetoothに含まれていないポートがあれば補完
            try {
                string[] sysPorts = System.IO.Ports.SerialPort.GetPortNames();
                foreach (string sp in sysPorts) {
                    if (!Ports.Any(p => p.PortName.Equals(sp, StringComparison.OrdinalIgnoreCase))) {
                        Ports.Add(new BluetoothComPortInfo {
                            PortName = sp,
                            Direction = "発信",
                            DisplayName = "シリアルポート"
                        });
                    }
                }
            } catch { }

            int i1 = -1;
            int i2 = -1;
            var validPorts = new List<BluetoothComPortInfo>();

            for (int i = 0; i < Ports.Count; i++) {
                var port = Ports[i];
                if (port.Direction == "発信" || string.IsNullOrEmpty(port.Direction) || port.Direction == "不明") {
                    validPorts.Add(port);
                    int itemIdx = comboBox1.Items.Add(port.PortName + " " + port.DisplayName);
                    if (!string.IsNullOrEmpty(Env.ComPortTS) && port.PortName.Equals(Env.ComPortTS, StringComparison.OrdinalIgnoreCase)) {
                        i1 = itemIdx;
                    }

                    int itemIdx2 = comboBox2.Items.Add(port.PortName + " " + port.DisplayName);
                    if (!string.IsNullOrEmpty(Env.ComPortGPS) && port.PortName.Equals(Env.ComPortGPS, StringComparison.OrdinalIgnoreCase)) {
                        i2 = itemIdx2;
                    }
                }
            }

            // 発信ポートが全く無かった場合は全ポートを表示
            if (comboBox1.Items.Count == 0 && Ports.Count > 0) {
                for (int i = 0; i < Ports.Count; i++) {
                    var port = Ports[i];
                    validPorts.Add(port);
                    int itemIdx = comboBox1.Items.Add(port.PortName + " " + port.DisplayName);
                    if (!string.IsNullOrEmpty(Env.ComPortTS) && port.PortName.Equals(Env.ComPortTS, StringComparison.OrdinalIgnoreCase)) {
                        i1 = itemIdx;
                    }
                    int itemIdx2 = comboBox2.Items.Add(port.PortName + " " + port.DisplayName);
                    if (!string.IsNullOrEmpty(Env.ComPortGPS) && port.PortName.Equals(Env.ComPortGPS, StringComparison.OrdinalIgnoreCase)) {
                        i2 = itemIdx2;
                    }
                }
            }

            if (i1 >= 0 && i1 < comboBox1.Items.Count) {
                comboBox1.SelectedIndex = i1;
            } else if (comboBox1.Items.Count > 0) {
                comboBox1.SelectedIndex = 0;
            }

            if (i2 >= 0 && i2 < comboBox2.Items.Count) {
                comboBox2.SelectedIndex = i2;
            } else if (comboBox2.Items.Count > 0) {
                comboBox2.SelectedIndex = 0;
            }
        }

        public int GetTSModel() {
            return cBoxTS.SelectedIndex;
        }

        public void SetTextBoxPos(double x, double y, double z) {
            if ((x == 0.0) && (y == 0.0)) {
                textBoxX.Text = "********";
                textBoxY.Text = "********";
                textBoxZ.Text = "********";
                btnUpdPos.Enabled = false;
                return;
            }

            textBoxX.Text = x.ToString("0.000");
            textBoxY.Text = y.ToString("0.000");
            textBoxZ.Text = z.ToString("0.000");

            if (!gbl.FormMain.isModeKijun()) {
                if (btnUpdPos.Enabled && chkAutoSet.Checked) {
                    gbl.FormMain.SetCXYZ(x, y, z);
                    btnUpdPos.Enabled = false;
                }
            }

        }
        public void SetTextBoxPos() {
            textBoxX.Text = "********";
            textBoxY.Text = "********";
            textBoxZ.Text = "********";
        }


        private void tabControl4_SelectedIndexChanged(object sender, EventArgs e) {
            Env.TSGPS = tabControl4.SelectedIndex;
        }
        public void SetBtns() {
            btnUp.Enabled = btnDown.Enabled = (cBoxTS.SelectedIndex == 1 || cBoxTS.SelectedIndex == 2);

            btnLeft.Enabled = btnRight.Enabled = btnLeft2.Enabled = btnRight2.Enabled = (cBoxTS.SelectedIndex != 3);
            btnLight.Enabled = btnSearch.Enabled = trackBar1.Enabled = (cBoxTS.SelectedIndex != 3);

            btnSijun.Enabled = (cBoxTS.SelectedIndex == 1 || cBoxTS.SelectedIndex == 2);

            //chkAutoSet.Visible = !gbl.FormMain.isModeKijun();

        }
        public void SetBtns2(bool isModeKijun = false) {
            chkAutoSet.Visible = !isModeKijun;

            //if (!Env.isUseLN100() && Env.isSupportTuibi()) {
            if (!Env.isUseLN100() && Env.getTSMode() == Env.TS_MODE_TUIBI) {
                gbl.TStation.AS_BtnClick_11();
                if (isModeKijun) {
                    Env.curTSMode0 = Env.TS_MODE_SHIJUNSOKUTEI;
                } else {
                    Env.curTSMode0 = Env.TS_MODE_TUIBI;
                }
                Env.curTSMode = Env.curTSMode0;
                btnAutoTsuibi.Text = Env.getTSModeStr(Env.curTSMode);
            }
        }
        public void SetBtns2(int mode) {
            // オーバーロード対応
        }
        private void cBoxTS_SelectedIndexChanged(object sender, EventArgs e) {
            Env.TS = cBoxTS.SelectedIndex;
            Env.SaveEnvTS();
            Env.initCurTSMode();

            btnConnect.Enabled = true;

            SetBtns();
            btnAutoTsuibi.Text = Env.getCurTSModeStr();
            /*
            if (cBoxTS.SelectedIndex == 0 || cBoxTS.SelectedIndex == 1) {
                btnAutoTsuibi.Text = "自動追尾";
                Env.TSMode = Env.TS_MODE_TUIBI;
            }
            if (cBoxTS.SelectedIndex == 2) {
                btnAutoTsuibi.Text = "視準測定";
                Env.TSMode = Env.TS_MODE_SHIJUNSOKUTEI;
            }
            if (cBoxTS.SelectedIndex == 3) {
                btnAutoTsuibi.Text = "測定";
                Env.TSMode = Env.TS_MODE_SOKUTEI;
            }
            */
        }
        private void cBoxGPS_SelectedIndexChanged(object sender, EventArgs e) {
            Env.GPS = cBoxGPS.SelectedIndex;
            Env.SaveEnvGPS();
        }

        private void cBoxKei_SelectedIndexChanged(object sender, EventArgs e) {
            Env.KeiNum = cBoxKei.SelectedIndex + 1;
            Env.SaveEnvGPS();
            gbl.Gps.blxy.SetKei(BLXY.SKEI_WLD, Env.KeiNum);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e) {
            if (comboBox2.SelectedItem != null) {
                string itemStr = comboBox2.SelectedItem.ToString();
                string portName = itemStr.Split(' ')[0];
                Env.ComPortGPS = portName;
                Env.SaveEnvGPS();
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) {
            if (comboBox1.SelectedItem != null) {
                string itemStr = comboBox1.SelectedItem.ToString();
                string portName = itemStr.Split(' ')[0];
                Env.ComPortTS = portName;
                Env.SaveEnvTS();
            }
        }

        private void cBoxGPSStatus_SelectedIndexChanged(object sender, EventArgs e) {
            Env.GPSStatus = cBoxGPSStatus.SelectedIndex;
            Env.SaveEnvGPS();
        }
        private void btnConnect_Click(object sender, EventArgs e) {
            Button btn = (Button)sender;
            if ((string)btn.Tag == "1") {
                if (btn.Text == "接続") {
                    gbl.TStation.SetCom(Env.ComPortTS);

                    gbl.TStation.Connect();
                    if (gbl.TStation.isConnect) {
                        timer1.Enabled = true;
                        SelKikaiTenBackTenBtn1.Enabled = true;
                        btn.Text = "切断";
                    }
                    SetBtns2(gbl.FormMain.isModeKijun());
                } else {
                    gbl.TStation.DisConnect();
                    timer1.Enabled = false;
                    SelKikaiTenBackTenBtn1.Enabled = false;
                    btn.Text = "接続";
                }
            } else {
                gbl.Gps.SetCom(Env.ComPortGPS);
                gbl.Gps.Connect();
                timer1.Enabled = true;
                //SelKikaiTenBackTenBtn1.Enabled = true;
                btn.Enabled = false;
            }
        }


        private void Kikaikou1_TextChanged(object sender, EventArgs e) {
            TextBox tb = (TextBox)sender;
            string text = tb.Text;
            double h;

            if (!double.TryParse(text, out h)) {
                return;
            }
            if ((string)tb.Tag == "1") {
                gbl.KikaiMan.kh = h;
                Def.SetIniDouble("TS", "器械高", h);
            }
            if ((string)tb.Tag == "2") {
                gbl.KikaiMan.mh = h;
                Def.SetIniDouble("TS", "ミラー高", h);
            }
            if ((string)tb.Tag == "3") {
                gbl.Gps.kh = h;
                Def.SetIniDouble("TS", "GPS器械高", h);
            }
        }
        private void btnDefKikaiBack_Click(object sender, EventArgs e) {
            if (gbl.st7Data.KijunP.KPList.Count < 2) {
                MessageBox.Show("基準点を２点以上登録してください！");
                return;
            }
            gbl.FormKikaiDef = new FormKikaiDef();
            gbl.FormKikaiDef.Show();
        }
        private void SelKikaiTenBackTenBtn1_Click(object sender, EventArgs e) {
            //gbl.KikaiMan.formMain = this;
            gbl.KikaiMan.showForm();
        }

        private void trackBar1_Scroll(object sender, EventArgs e) {
            gbl.TStation.rsspd = trackBar1.Value * 2;
        }
        private void btnAutoTsuibi_Click(object sender, EventArgs e) {
            Button btn = (Button)sender;
            int tag = int.Parse((String)btn.Tag);
            int tsModel = GetTSModel();

            if (tsModel == 0) {
                gbl.TStation.LN100_BtnClick(btn, tag);
            }
            if (tsModel == 1 || tsModel == 2 || tsModel == 3) {
                gbl.TStation.AS_BtnClick(btn, tag);
            }


        }
        private void btnUp_MouseDown(object sender, MouseEventArgs e) {
            Button btn = (Button)sender;
            int tag = int.Parse((String)btn.Tag);
            if (GetTSModel() == 0) {
                gbl.TStation.LN100_MouseDown(btn, tag);
            }
            if (GetTSModel() == 1 || GetTSModel() == 2) {
                gbl.TStation.AS_MouseDown(btn, tag);
            }
            stopwatch.Restart(); // タイマーを開始
        }
        private void btnUp_MouseUp(object sender, MouseEventArgs e) {
            Button btn = (Button)sender;
            int tag = int.Parse((String)btn.Tag);
            stopwatch.Stop();

            if (GetTSModel() == 0) {
                gbl.TStation.LN100_MouseUp(btn, tag, stopwatch.ElapsedMilliseconds);
            }
            if (GetTSModel() == 1 || GetTSModel() == 2) {
                gbl.TStation.AS_MouseUp(btn, tag, stopwatch.ElapsedMilliseconds);
            }
        }

        private void button8_Click(object sender, EventArgs e) {
            if (gbl.FormMain.isModeKijun() && Env.TSGPS == Env.TSGPS_GPS) {
                gbl.Gps.startGpsCount();
            } else {
                gbl.FormMain.SetCXYZ(double.Parse(textBoxX.Text), double.Parse(textBoxY.Text), double.Parse(textBoxZ.Text));
                SetTextBoxPos(0, 0, 0);
            }
        }

        private void timer1_Tick(object sender, EventArgs e) {
            if (tabControl4.SelectedIndex == 0) {
                if (50 < gbl.TStation.idleCnt++) {
                    if (gbl.TStation.CheckConnect()) {
                        gbl.TStation.idleCnt = 0;
                    }
                }
                chbContMeasure.Visible = (Env.curTSMode == Env.TS_MODE_TUIBI);  //自動追尾の時だけ連続測定のチェックを表示！

                if (gbl.TStation.isConnect == false) {
                    timer1.Enabled = false;
                    labelStatus.Text = "未接続";
                    btnConnect.Enabled = true;
                    btnConnect.Text = "接続";
                    SelKikaiTenBackTenBtn1.Enabled = false;
                    return;
                }
                timer1.Interval = 100;
                if (gbl.MField.isError) {
                    labelStatus.Text = "測定エラー";
                    btnUpdPos.Enabled = false;
                    SetTextBoxPos(0, 0, 0);
                    //textBoxX.Text = "********";
                    //textBoxY.Text = "********";
                    //textBoxZ.Text = "********";
                    gbl.FormMain.SetMsg("エラーコード:" + gbl.MField.errorMessage);
                    return;
                }
                if (gbl.MField.isTracking()) {
                    //2026.03.16 距離を測定しないときは座標を表示しないようにする
                    if (!gbl.MField.isLngOK()) {

                        //2026.03.23 by A.Iimuro 単独測定/連続測定処理
                        if (Env.curTSMode == Env.TS_MODE_TUIBI && chbContMeasure.Checked) {
                            //単独測定の時は追尾のみを継続する
                            labelStatus.Text = "追尾中(未測定)";
                            btnUpdPos.Enabled = false;
                            SetTextBoxPos(0, 0, 0);
                        } else {
                            labelStatus.Text = "追尾中(測定可)";
                            btnAutoTsuibi.Text = "測定";
                            // btnUpdPos.Enabled = false;
                            //SetTextBoxPos(0, 0, 0);
                        }
                        gbl.FormMain.ShowZumen0();
                        return;
                    }
                    labelStatus.Text = "追尾中";
                    btnAutoTsuibi.Text = "追尾中断";
                    XYZ p = gbl.KikaiMan.cnvP(gbl.MField.lng, gbl.MField.angH, gbl.MField.angV);
                    gbl.FormMain.SetMsg("lng;" + gbl.MField.lng.ToString("0.000") + " angH:" + gbl.MField.angH.ToString("0.000000") + " angV:" + gbl.MField.angV.ToString("0.000000"));
                    //if (!gbl.TStation.curPos.equal(p)) {
                    btnUpdPos.Enabled = true;
                    if (gbl.TStation.isChangePos) {
                        gbl.TStation.isChangePos = false;
                        if (gbl.MField.isLngOK()) {  //2026.03.23 by A.Iimuro 単独測定/連続測定処理

                            gbl.TStation.curPos.set(p);
                            if (gbl.TStation.isKikaiDefSet) {
                                gbl.TStation.isKikaiDefSet = false;
                            }
                            double x = gbl.TStation.curPos.X;
                            double y = gbl.TStation.curPos.Y;
                            double z = gbl.TStation.curPos.Z + St7Lib.CheckDouble(Kikaikou1.Text, 0.0) - St7Lib.CheckDouble(Mirrorkou1.Text, 0.0);
                            SetTextBoxPos(x, y, z);
                            //textBoxX.Text = x.ToString("0.000");
                            //textBoxY.Text = y.ToString("0.000");
                            //textBoxZ.Text = z.ToString("0.000");
                            if (gbl.FormMain.isModeKijun()) {
                                if (gbl.FormMain.IsYudoMode()) {
                                    gbl.FormMain.SetTSYudo();
                                }

                            } else {
                                //if (btnUpdPos.Enabled && chkAutoSet.Checked) {
                                //gbl.FormMain.SetCXYZ(x, y, z);
                                //}
                            }
                        }

                        gbl.FormMain.ShowZumen0();

                    }



                } else {
                    if (gbl.MField.isSearching() || gbl.MField.lng == 0.0 || gbl.MField.lng == -1.0) {
                        btnUpdPos.Enabled = false;
                        SetTextBoxPos(0, 0, 0);
                        //textBoxX.Text = "********";
                        //textBoxY.Text = "********";
                        //textBoxZ.Text = "********";
                    } else {
                        //if (!gbl.TStation.curPos.equal(p)) {
                        if (gbl.TStation.isChangePos) {
                            gbl.TStation.isChangePos = false;
                            btnUpdPos.Enabled = true;
                            XYZ p = gbl.KikaiMan.cnvP(gbl.MField.lng, gbl.MField.angH, gbl.MField.angV);
                            //msgBox2.Text = "lng;" + gbl.MField.lng.ToString("0.000") + " angH:" + gbl.MField.angH.ToString("0.000000") + " angV:" + gbl.MField.angV.ToString("0.000000");
                            gbl.FormMain.SetMsg("lng;" + gbl.MField.lng.ToString("0.000") + " angH:" + gbl.MField.angH.ToString("0.000000") + " angV:" + gbl.MField.angV.ToString("0.000000"));
                            gbl.TStation.curPos.set(p);
                            if (gbl.TStation.isKikaiDefSet) {
                                gbl.TStation.isKikaiDefSet = false;
                            }
                            double z = gbl.TStation.curPos.Z + St7Lib.CheckDouble(Kikaikou1.Text, 0.0) - St7Lib.CheckDouble(Mirrorkou1.Text, 0.0);
                            SetTextBoxPos(gbl.TStation.curPos.X, gbl.TStation.curPos.Y, z);
                            //textBoxX.Text = gbl.TStation.curPos.X.ToString("0.000");
                            //textBoxY.Text = gbl.TStation.curPos.Y.ToString("0.000");
                            //textBoxZ.Text = z.ToString("0.000");
                        }
                    }
                    if (gbl.MField.isSearching()) {
                        labelStatus.Text = "サーチ中";
                    } else {
                        if (Env.curTSMode == Env.TS_MODE_TUIBI) {
                            btnAutoTsuibi.Text = "自動追尾";
                            if (gbl.MField.curStatus == 3) {
                                labelStatus.Text = "追尾停止(接続中)";
                            } else if (gbl.MField.curStatus == 2) {
                                labelStatus.Text = "自動視準(接続中)";
                            } else
                                labelStatus.Text = "追尾なし";
                        } else {
                            btnAutoTsuibi.Text = Env.getTSModeStr(Env.curTSMode);
                            labelStatus.Text = "追尾なし";

                        }
                        btnAutoTsuibi.Refresh();        //2026.5.26 更新されないことがあるので追加
                    }

                }
                gbl.TStation.SetImgBtn(btnUpdPos);
            } else {
                // GPS
                //if (gbl.Gps.isGetLocation) {
                if (gbl.Gps.isChange) {
                    labelStatus2.Text = "GPS取得";
                    XYZ p = gbl.Gps.gpsP;
                    if (!gbl.Gps.curPos.equal(p)) {
                        gbl.Gps.curPos.set(p);
                        double z = gbl.Gps.curPos.Z;
                        SetTextBoxPos(gbl.Gps.curPos.X, gbl.Gps.curPos.Y, z);
                        //textBoxX.Text = gbl.Gps.curPos.X.ToString("0.000");
                        //textBoxY.Text = gbl.Gps.curPos.Y.ToString("0.000");
                        //textBoxZ.Text = z.ToString("0.000");
                        gbl.Gps.isChangePos = true;
                        gbl.FormMain.SetMsg(gbl.Gps.gpsBL.ToStr());
                        //msgBox2.Text = gbl.Gps.gpsBL.ToStr();
                        gbl.FormMain.ShowZumen0();

                    }
                    btnUpdPos.Enabled = Env.isGoodGPS(gbl.Gps.gpsStatus);
                    labelGPS1.Text = gbl.Gps.getGpsStr1();
                    labelGPS2.Text = gbl.Gps.getGpsStr2();

                    if (gbl.FormMain.isModeKijun()) {
                        if (gbl.FormMain.IsYudoMode()) {
                            gbl.FormMain.SetGPSYudo();
                        } else {
                            gbl.FormMain.SetGPSKijunP();
                        }

                    } else {
                        if (btnUpdPos.Enabled && chkAutoSet.Checked) {
                            gbl.FormMain.SetCXYZ(p.X, p.Y, p.Z);
                        }
                    }


                } else {
                    labelStatus2.Text = "GPS未取得";
                    btnUpdPos.Enabled = false;
                    SetTextBoxPos(0, 0, 0);
                    //textBoxX.Text = "********";
                    //textBoxY.Text = "********";
                    //textBoxZ.Text = "********";
                }
            }
        }
        static List<BluetoothComPortInfo> GetBluetoothComPorts() {
            var result = new List<BluetoothComPortInfo>();
            const string bthentumPath = @"SYSTEM\CurrentControlSet\Enum\BTHENUM";

            // Cache device names by MAC address from BTHPORT and BTHENUM
            var deviceNamesByMac = GetBluetoothDeviceNames();

            using (var bthKey = Registry.LocalMachine.OpenSubKey(bthentumPath)) {
                if (bthKey == null)
                    return result;

                foreach (var typeKeyName in bthKey.GetSubKeyNames()) {
                    using (var typeKey = bthKey.OpenSubKey(typeKeyName)) {
                        if (typeKey == null)
                            continue;

                        foreach (var instanceKeyName in typeKey.GetSubKeyNames()) {
                            using (var instanceKey = typeKey.OpenSubKey($@"{instanceKeyName}\Device Parameters")) {
                                if (instanceKey == null)
                                    continue;

                                var portNameObj = instanceKey.GetValue("PortName");
                                if (portNameObj == null)
                                    continue;

                                string portName = portNameObj.ToString();

                                // 1. Determine direction
                                // _LOCALMFG is definitely Incoming.
                                // _DEV_, _VID&, _PID& are definitely Outgoing.
                                string direction = "不明";
                                if (typeKeyName.IndexOf("_LOCALMFG", StringComparison.OrdinalIgnoreCase) >= 0)
                                    direction = "着信";
                                else if (typeKeyName.IndexOf("_DEV_", StringComparison.OrdinalIgnoreCase) >= 0 ||
                                         typeKeyName.IndexOf("_VID&", StringComparison.OrdinalIgnoreCase) >= 0 ||
                                         typeKeyName.IndexOf("_PID&", StringComparison.OrdinalIgnoreCase) >= 0)
                                    direction = "発信";

                                // 2. Determine Display Name
                                string displayName = "";
                                using (var parentInstanceKey = typeKey.OpenSubKey(instanceKeyName)) {
                                    // Try to get FriendlyName first (it might be the device name set by the user/driver)
                                    string friendlyName = parentInstanceKey?.GetValue("FriendlyName")?.ToString();
                                    string serviceDesc = parentInstanceKey?.GetValue("DeviceDesc")?.ToString();

                                    // Clean up serviceDesc (handle localized strings)
                                    if (serviceDesc != null && serviceDesc.StartsWith("@")) {
                                        var parts = serviceDesc.Split(';');
                                        if (parts.Length > 1)
                                            serviceDesc = parts[parts.Length - 1];
                                    }

                                    // If FriendlyName contains a COM port in parentheses, it's generic.
                                    bool isGenericFriendly = !string.IsNullOrEmpty(friendlyName) &&
                                                           (friendlyName.Contains("(COM") || friendlyName.Contains("Bluetooth リンク"));

                                    if (!string.IsNullOrEmpty(friendlyName) && !isGenericFriendly) {
                                        displayName = friendlyName;
                                    } else {
                                        // Try logic based on MAC address
                                        string mac = ExtractMac(typeKeyName);
                                        if (string.IsNullOrEmpty(mac))
                                            mac = ExtractMac(instanceKeyName);

                                        string deviceName = null;
                                        if (!string.IsNullOrEmpty(mac)) {
                                            deviceNamesByMac.TryGetValue(mac.ToLower(), out deviceName);
                                        }

                                        if (!string.IsNullOrEmpty(deviceName)) {
                                            displayName = deviceName;
                                        } else {
                                            displayName = serviceDesc ?? friendlyName ?? "Outgoing";
                                        }
                                    }
                                }
                                if (direction == "発信") {
                                    result.Add(new BluetoothComPortInfo {
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

            // Sort by COM port number
            result.Sort((a, b) => {
                int na = ExtractNumber(a.PortName);
                int nb = ExtractNumber(b.PortName);
                if (na != nb)
                    return na.CompareTo(nb);
                return string.Compare(a.PortName, b.PortName, StringComparison.Ordinal);
            });

            return result;
        }

        static Dictionary<string, string> GetBluetoothDeviceNames() {
            var results = new Dictionary<string, string>();

            // 1. From BTHPORT Parameters (Paired devices)
            try {
                using (var devicesKey = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\BTHPORT\Parameters\Devices")) {
                    if (devicesKey != null) {
                        foreach (var mac in devicesKey.GetSubKeyNames()) {
                            using (var devKey = devicesKey.OpenSubKey(mac)) {
                                var name = devKey.GetValue("Name");
                                string deviceName = null;
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
            } catch { }

            // 2. From BTHENUM (Enumerated device characteristics)
            // Sometimes the friendly name is stored in a key named Dev_MAC
            try {
                using (var bthKey = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Enum\BTHENUM")) {
                    if (bthKey != null) {
                        foreach (var typeKeyName in bthKey.GetSubKeyNames()) {
                            string mac = ExtractMac(typeKeyName);
                            if (string.IsNullOrEmpty(mac))
                                continue;

                            using (var typeKey = bthKey.OpenSubKey(typeKeyName)) {
                                foreach (var instKeyName in typeKey.GetSubKeyNames()) {
                                    using (var instKey = typeKey.OpenSubKey(instKeyName)) {
                                        var friendly = instKey?.GetValue("FriendlyName")?.ToString();
                                        if (!string.IsNullOrEmpty(friendly) && !friendly.Contains("(COM")) {
                                            results[mac.ToLower()] = friendly;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            } catch { }

            return results;
        }

        static string ExtractMac(string text) {
            if (string.IsNullOrEmpty(text))
                return null;
            // Match 12 hex digits, possibly prefixed by DEV_ or Dev_
            var match = Regex.Match(text, @"(?:DEV_|Dev_)([0-9A-F]{12})", RegexOptions.IgnoreCase);
            if (match.Success)
                return match.Groups[1].Value;

            // Also check for raw 12 hex digits in strings like &000780E25BAB_C00000000
            match = Regex.Match(text, @"(?:^|&|_)([0-9A-F]{12})(?:$|&|_)", RegexOptions.IgnoreCase);
            if (match.Success)
                return match.Groups[1].Value;

            return null;
        }

        static int ExtractNumber(string text) {
            var match = Regex.Match(text, @"\d+");
            return match.Success ? int.Parse(match.Value) : 0;
        }

        private void chbContMeasure_CheckedChanged(object sender, EventArgs e) {
            // 連続測定のON/OFFを切り替える
        }
    }


}
