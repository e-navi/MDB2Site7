using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Button = System.Windows.Forms.Button;

namespace Site7DbEditor {
    public class TStation {
        //public FormMain formMain;
        //public FormLN100 formLN100 = new FormLN100();

        private const int LONG_CLICK_THRESHOLD_MS = 500; // 500ミリ秒


        string com;
        SerialPort serialPort = new SerialPort();




        //public int prism = 1;
        //public int prismValue = -7;

        public bool isOpen = false;
        public bool isChangePos = false;
        public bool isLN100 = true;
        public XYZ curPos = new XYZ();
        public double curLng;
        public double curAngV;
        public double curAngH;
        public string receivedBuff = "";

        public string sendStr;
        public string receiveStr;

        const int CMD_IWCCS = 0;
        const int CMD_IWCCE = 1;
        const int CMD_MFILD = 2;
        const int CMD_MTILT = 3;
        const int CMD_MBATT = 4;
        const int CMD_ONUMB = 5;
        const int CMD_OCOND = 6;
        const int CMD_OSTNG = 7;
        const int CMD_OWSET = 8;
        const int CMD_ISTNG = 9;
        const int CMD_IHANG = 10;
        const int CMD_ITRGT = 11;
        const int CMD_IATMS = 12;
        const int CMD_ITLCR = 13;
        const int CMD_IWSET = 14;
        const int CMD_LPWOF = 15;
        const int CMD_LSLON = 16;
        const int CMD_LSLOF = 17;
        const int CMD_LGLON = 18;
        const int CMD_LGLOF = 19;
        const int CMD_LLPON = 20;
        const int CMD_LLPOF = 21;
        const int CMD_RSPOS = 22;
        const int CMD_RSSPD = 23;
        const int CMD_RTRCK = 24;
        const int CMD_RLAYO = 25;
        const int CMD_SFILD = 26;
        const int CMD_STILT = 27;
        const int CMD_SBATT = 28;
        const int CMD_SMOTR = 29;
        const int CMD_MWEBS = 30;
        const int CMD_OTSST = 31;
        const int CMD_OTSID = 32;
        const int CMD_IKYCD = 33;
        const int CMD_SWEB = 34;

        public int curCmd = -1;

        //public int autoTrackMode = 0;  // 0:グローバルサーチ 1:鉛直方向サーチ
        //public bool isSearching = false;

        public Button btnAutoTsuibi;
        public Button searchAreaBtn;
        public CheckBox chbContMeasure;

        bool isChangeTuibiStatus = false;
        int curTuibiStatus = 0; //追尾設定 0:なし,2:自動視準,3:自動追尾 追尾停止,4:プリズム待ち,5:ロック

        bool searchMode = false;
        bool connectMode = false;
        bool SITEconnectMode = false;

        public bool isConnect = false;
        bool isConnect0 = false;
        bool isTracking0 = false;
        bool isSearching = false;
        bool isLighting = false;//2021/09/27 shirai true⇒falseに。これでConnect時に初めからライト付かない
        bool isEnableTSShield = false;
        bool isClosing = false;
        int curStatus0 = -1;
        int curStatus = 0;
        public int idleCnt = 0;    // CheckConnect() で接続チェック

        int btnType = -1;//2022/02/14 Iimuro Add

        int batteryLevel = 100;
        int autoTrackMode = 0;
        public int rsspd = 10;
        int lightType = 2;    // 2:ガイドライト 1:レーザ照射

        public bool isKikaiDefSet = false; // 2026.05.29 by A.Iimuro 器械点測定完了フラグ。

        public TStation() {

        }
        public void LN100_BtnClick(Button btn, int tag) {
            switch (tag) {
                case 0:
                    if (btn.Text == "消灯") {
                        gbl.TStation.SetLight(false);
                        btn.Text = "点灯";
                    } else {
                        gbl.TStation.SetLight(true);
                        btn.Text = "消灯";
                    }
                    break;
                case 1: //Speed

                    break;
                case 2: //G or V
                    if (btn.Text == "G") {
                        gbl.TStation.SetAutoTrackMode(1);
                        btn.Text = "V";
                    } else {
                        gbl.TStation.SetAutoTrackMode(0);
                        btn.Text = "G";
                    }
                    break;
                case 3: //自動追尾
                    if (btn.Text == "自動追尾") {
                        gbl.TStation.SetAutoTrack(true);
                        gbl.TStation.SetFILD(true);
                        btn.Text = "中断";
                    } else {
                        gbl.TStation.SetAutoTrack(false);
                        gbl.TStation.SetFILD(false);
                        btn.Text = "自動追尾";
                    }
                    break;
                case 4:
                    //Hide();
                    break;
                case 11:
                    gbl.TStation.StopRotate();
                    break;
            }
        }
        public void LN100_MouseDown(Button btn, int tag) {
            int val = rsspd;
            switch (tag) {
                case 5: // <<
                    val += 2;
                    break;
                case 6: // <
                    break;
                case 7: // >
                    val *= -1;
                    break;
                case 8: // >>
                    val += 2;
                    val *= -1;
                    break;
            }
            gbl.TStation.RotateSPD(1, val);

        }
        public void LN100_MouseUp(Button btn, int tag, long ms) {

            switch (tag) {
                case 5: // <<
                case 6: // <
                case 7: // >
                case 8: // >>
                    if (ms < LONG_CLICK_THRESHOLD_MS) {
                        Thread.Sleep(LONG_CLICK_THRESHOLD_MS);
                    }
                    gbl.TStation.StopRotate();
                    break;
            }

        }

        public void AS_BtnClick(Button btn, int tag) {
            switch (tag) {
                case 0:
                    if (btn.Text == "消灯") {
                        gbl.TStation.SetLight(false);
                        btn.Text = "点灯";
                    } else {
                        gbl.TStation.SetLight(true);
                        btn.Text = "消灯";
                    }
                    break;
                /*
                                    if (btn.Text == "消灯") {
                                        gbl.TStation.WriteData("*GLOFF");
                                        btn.Text = "点灯";
                                    } else {
                                        gbl.TStation.WriteData("*GLON");
                                        btn.Text = "消灯";
                                    }
                                    break;
                */
                case 1: //Speed

                    break;
                case 2: //G or V
                    /*
                    if (btn.Text == "G") {
                        gbl.TStation.WriteData("@RTRCK,0,");
                        btn.Text = "V";
                    } else {
                        gbl.TStation.WriteData("@RTRCK,1,");
                        btn.Text = "G";
                    }
                    */
                    SetPA(Env.SearchH, Env.SearchV);
                    break;
                case 3: //自動追尾
                    AS_BtnClick_3();
                    break;
                case 4:
                    //Hide();
                    break;
                case 11:
                    AS_BtnClick_11();
                    break;
                case 12:    //TS mode 選択
                    selTSMode(btn);
                    break;
            }
        }
        public void AS_BtnClick_3() {
            gbl.MField.isError = false;
            if (Env.curTSMode == Env.TS_MODE_TUIBI) {
                if (gbl.MField.isTracking()) {  //  追尾中
                    if (chbContMeasure.Checked) {
                        SetSearchMode(false);
                        StopRotate();
                        StopSokutei();
                        return;
                    } else {
                        //追尾のみの時
                        SetFILD(true);
                        return;
                    }
                }

                SetMode(Env.SokkyoMode == Env.SokkyoMode_Seimitu, true);
                SetAutoTrack(true);
                SetSearchMode(true);
            } else if (Env.curTSMode == Env.TS_MODE_SHIJUNSOKUTEI) {
                SetMode(Env.SokkyoMode == Env.SokkyoMode_Seimitu, false);
                SetLSL(true);
                //SetFILD(true);
                SetSearchMode(true);
                //2022/03/31 shirai add start----------------------------------
                //SetLSL(true);で自動視準した後、それが完了するまでを待つ動作（完了時に返り値「OK+CRLF」）。10秒経つか、自動視準の返り値がOKなら先に進むことにする。
                //BluetoothTSクラスのsendBT関数内でReceiveStrが「OK」から始まってればcurCmd=1となるとされているのでこれをOKの返り値を得た基準とする。
                int i = 0;
                while ((i < 10) && (curCmd != 1)) {
                    Thread.Sleep(1000);
                    i++;
                }
                if (curCmd != 1) {
                    StopSokutei();//タイムアウトに合わせて自動視準コマンドを取り止め。
                                  //gbl.ShowToast("視準失敗のため測距できませんでした。");
                } else {
                    SetFILD(true);//問題なく自動視準できれば測定。
                }
                //2022/03/31 shirai add end----------------------------------
            } else if (Env.curTSMode == Env.TS_MODE_SHIJUN) {
                SetMode(Env.SokkyoMode == Env.SokkyoMode_Seimitu, false);

                // 2022.01.31 by A.Iimuro. 視準のみで視準後に測定のみに変える！
                if (gbl.MField.isAngOK()) {
                    gbl.MField.ClearLng();
                    SetFILD(true);
                    SetSearchMode(true);
                } else {
                    gbl.MField.ClearLng();
                    SetLSL(true);
                    //SetFILD0(true);
                }
            } else if (Env.curTSMode == Env.TS_MODE_SOKUTEI) {
                //SetMode(Env.SokkyoMode == Env.SokkyoMode_Seimitu, false);
                SetFILD(true);
            }
        }
        public void AS_BtnClick_11() {
            if (gbl.MField.isTracking()) {
                SetFILD(false);
                btnAutoTsuibi.Text = "自動追尾";
            }
            //gbl.TStation.StopRotate();
            StopSokutei();
        }

        private void selTSMode(Button btn) {
            // ContextMenuStrip とメニューアイテムを作成
            ContextMenuStrip contextMenuStrip = new ContextMenuStrip();

            /*
            if (Env.isSupportTuibi()) {
                ToolStripMenuItem tuibiLabel = new ToolStripMenuItem(Env.getTSModeStr(Env.TS_MODE_TUIBI));
                tuibiLabel.Click += (_, __) => {
                    Env.curTSMode = Env.TS_MODE_TUIBI;
                    btnAutoTsuibi.Text = Env.getTSModeStr(Env.curTSMode);
                };
                contextMenuStrip.Items.Add(tuibiLabel);
            }
            for (int i = 0; i <= Env.TS_MODE_SHIJUNSOKUTEI; i++) {
                int mode = Env.TS_MODE_SHIJUNSOKUTEI - i; // ローカル変数にキャプチャ
                ToolStripMenuItem modeLabel = new ToolStripMenuItem(Env.getTSModeStr(mode));
                modeLabel.Click += (_, __) => {
                    Env.curTSMode = mode;
                    btnAutoTsuibi.Text = Env.getTSModeStr(Env.curTSMode);
                };
                contextMenuStrip.Items.Add(modeLabel);
            }
            */
            for (int i = Env.getCurTSMode(); 0 <= i; i--) {
                int mode = i; // ローカル変数にキャプチャ
                ToolStripMenuItem modeLabel = new ToolStripMenuItem(Env.getTSModeStr(mode));
                modeLabel.Click += (_, __) => {
                    Env.curTSMode0 = mode;
                    Env.curTSMode = mode;
                    btnAutoTsuibi.Text = Env.getTSModeStr(Env.curTSMode0);
                };
                contextMenuStrip.Items.Add(modeLabel);
            }
            contextMenuStrip.Show(btn.PointToScreen(new System.Drawing.Point(btn.Width, 0)));

        }

        public void AS_MouseDown(Button btn, int tag) {
            //int val = rsspd + 4;
            int val = rsspd + 6;
            int max = 16;

            int type = 1;

            switch (tag) {
                case 5: // <<
                    //val += 2;
                    break;
                case 6: // <
                    val = max;
                    break;
                case 7: // >
                    val = max * -1;
                    break;
                case 8: // >>
                    //val += 2;
                    val *= -1;
                    break;
                case 9: // ↑
                    type = 2;
                    val *= -1;
                    break;
                case 10: // ↓
                    type = 2;
                    break;
            }
            if (Env.UseRC == Env.UseRC_Yes)
                type = 2;
            if (btn.Enabled)
                gbl.TStation.RotateSPD(type, val);
        }
        public async void AS_MouseUp(Button btn, int tag, long ms) {

            if (Env.UseRC == Env.UseRC_Yes)
                return;

            switch (tag) {
                //case 5: // <<
                case 6: // <
                case 7: // >
                //case 8: // >>
                case 9: // ↑
                case 10: // ↓
                    btn.Enabled = false;
                    if (ms < LONG_CLICK_THRESHOLD_MS * rsspd / 2) {
                        await Task.Delay(LONG_CLICK_THRESHOLD_MS * rsspd / 2);
                    }
                    gbl.TStation.StopRotate();
                    break;
            }
            btn.Enabled = true;

        }



        public void SetCom(string _com) {
            com = _com;
        }
        public void SetCom(bool _isLN100, string _com) {
            isLN100 = _isLN100;
            com = _com;
        }
        public void ShowForm() {
            //formLN100 = new FormLN100();
            //formLN100.Show();
        }
        public void DisConnect() {
            if (isConnect) {
                SetLight(false);
                StopSokutei();
            }
            if (serialPort != null) {
                serialPort.Close();
                serialPort = null;
            }
            isConnect = false;
        }
        public void Connect() {
            isLN100 = (gbl.FormMain.GetTSModel() == 0);
            if (serialPort == null) {
                serialPort = new SerialPort();
            }
            if (serialPort.IsOpen) {
                serialPort.Close();
            }
            serialPort.BaudRate = 115200; //変調回数(1秒あたり)
            serialPort.DataBits = 8; // 1変調あたりの bit数
            serialPort.StopBits = StopBits.One;
            serialPort.Parity = Parity.None;
            serialPort.Handshake = Handshake.None;
            serialPort.DtrEnable = false;
            serialPort.RtsEnable = false;
            serialPort.PortName = com;
            //2026.05.12 Timeoutの時間を2秒に変更
            //serialPort.ReadTimeout = 10000;
            //serialPort.WriteTimeout = 10000;
            serialPort.ReadTimeout = 2000;
            serialPort.WriteTimeout = 2000;


            try {
                //ポートの監視を開始する
                serialPort.Open();

                if (isLN100)
                    serialPort.DataReceived += SerialPort_DataReceived_LN100;
                else
                    serialPort.DataReceived += SerialPort_DataReceived_TSA;

            } catch (Exception e) {
                //MessageBox.Show(e.Message);
                MessageBox.Show("接続できません");

                return;
            }
            isConnect = true;
            Init2();
            //LN100.WriteData("@LGLON,1,2,");

        }
        public bool CheckConnect() {
            if (!isConnect) {
                return false;
            }
            if (!serialPort.IsOpen) {
                return false;
            }
            try {
                serialPort.Write("\r");
            } catch (Exception e) {
                //MessageBox.Show(e.ToString());
                isConnect = false;
                MessageBox.Show("再接続してください");
            }
            //Console.WriteLine("SerialPort is Connecting.");
            return isConnect;
        }
        public string GetTCPRec(int cmd, string strCmd, int type) {
            sendStr = strCmd;
            receiveStr = "";

            Thread.Sleep(10);
            WriteData(strCmd);

            int n = 10;
            if (type == 0)
                n = 500;

            int cnt = 0;
            while ((receiveStr == "") && (cnt++ < n)) {
                if (!isConnect)
                    break;
                Thread.Sleep(10);
            }
            if (receiveStr == "") {
                cnt++;
            }

            return receiveStr;
        }
        public void WriteData(String str) {
            if (!isConnect) {
                return;
            }
            if (!serialPort.IsOpen) {
                return;
            }
            try {
                serialPort.Write(str + "\r");
            } catch (Exception e) {
                //MessageBox.Show(e.ToString());
                MessageBox.Show("再接続してください");
                isConnect = false;
            }
            Console.WriteLine(str);
        }
        public void WriteData2(String str) {
            if (!serialPort.IsOpen) {
                return;
            }
            serialPort.Write(str);
        }


        /// シリアルポート・データ受信イベント
        /// ※データを受信する都度呼ばれるので、電文が全て送られてくるとは限らないので注意 <summary>
        /// シリアルポート・データ受信イベント

        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SerialPort_DataReceived_LN100(object sender, SerialDataReceivedEventArgs e) {
            SerialPort serialPort1 = (SerialPort)sender;
            string str = "";
            try {
                //receivedData = this.serialPort1.ReadLine();
                int rbyte = serialPort1.BytesToRead;

                for (int i = 0; i < rbyte; i++) {
                    int tmp = serialPort1.ReadChar();
                    char ctmp = (char)tmp;
                    receivedBuff += ctmp;

                    if (ctmp == 02) {
                        receivedBuff = "";
                    }
                    if (ctmp == 03) {
                        str = receivedBuff;
                        receivedBuff = "";

                        // ここで受信データの処理

                    }
                    if (ctmp == 13) {
                        str = receivedBuff;
                        receivedBuff = "";
                        //textBox1.AppendText(str);

                        string[] strs = str.Split(',');

                        if (strs[0].Equals("@MFILD")) {
                            gbl.MField.SetRecLN100(str);

                            curLng = St7Lib.CheckDouble(strs[1], -1.0);
                            curAngV = St7Lib.CheckDouble(strs[2], -1.0);
                            curAngH = St7Lib.CheckDouble(strs[3], -1.0);
                            isChangePos = true;
                        }
                        receiveStr = strs[0];
                    }
                }
            } catch (Exception ex) {


            }
        }
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SerialPort_DataReceived_TSA(object sender, SerialDataReceivedEventArgs e) {
            SerialPort serialPort1 = (SerialPort)sender;
            string str = "";
            try {
                //receivedData = this.serialPort1.ReadLine();
                string sendStr0 = sendStr;
                int rbyte = serialPort1.BytesToRead;

                for (int i = 0; i < rbyte; i++) {
                    idleCnt = 0;
                    int tmp = serialPort1.ReadChar();
                    char ctmp = (char)tmp;
                    if (ctmp != 13) {
                        receivedBuff += ctmp;
                    }

                    if (ctmp == '\n') {
                        receivedBuff = "";
                    }
                    if (ctmp == 02) {
                        receivedBuff = "";
                    }
                    if (ctmp == 03) {
                        str = receivedBuff;
                        receivedBuff = "";

                        // ここで受信データの処理
                        //receiveStr = str;

                    }
                    if (ctmp == 06) {
                        receiveStr = "receive ACK " + sendStr;
                        receivedBuff = "";


                        if (sendStr.StartsWith("*TBON")) {
                            sendStr = "";//2022/02/25 Iimuro add
                            //continue;
                        }
                        if (sendStr.StartsWith("*SJ000000")) {
                            sendStr = "";//2022/02/25 Iimuro add
                            //continue;
                        }
                        if (sendStr.StartsWith("*R")) {
                            gbl.MField.ClearLng();
                            gbl.MField.curStatus = 0;
                            sendStr = "";//2022/02/25 Iimuro add
                            //continue;
                        }
                        if (sendStr.StartsWith("*Q")) {
                            gbl.MField.ClearLng();
                            gbl.MField.curStatus = 0;
                            sendStr = "";//2022/02/25 Iimuro add
                            //continue;
                        }
                        if (sendStr.StartsWith("*ST0")) {
                            gbl.MField.curStatus = 0;
                            sendStr = "";//2022/02/25 Iimuro add
                            //continue;
                        }
                        if (sendStr.StartsWith("*J")) {
                            sendStr = "";//2022/02/25 Iimuro add
                            //continue;
                        }
                        sendStr = "";


                    }
                    if (ctmp == 0x15) {
                        receiveStr = "receive NAK " + sendStr;
                        receivedBuff = "";
                    }
                    if (ctmp == 13) {   //改行コード
                        receiveStr = receivedBuff;
                        receivedBuff = "";
                        //textBox1.AppendText(str);

                        string[] str0 = receiveStr.Split(',');

                        if (sendStr.StartsWith("\u0014")) {

                            if (string.IsNullOrWhiteSpace(receiveStr) || receiveStr == "OK" || receiveStr.StartsWith("OK")) {
                                continue;
                            }
                            str = receiveStr;
                            receivedBuff = "";

                            if (!gbl.MField.SetRecAS2(str)) {
                                isChangePos = true;
                            }
                            sendStr = "\u0012";     // \u0014 の時は \u0012でないとSTOPしない
                            curCmd = 1;
                        }
                        if (str0[0].StartsWith("*ST3") || str0[0].StartsWith("*ST2")) {
                            gbl.MField.SetRecAS(receiveStr);

                            curLng = St7Lib.CheckDouble(str0[4], -1.0);
                            curAngV = St7Lib.CheckDouble(str0[3], -1.0);
                            curAngH = St7Lib.CheckDouble(str0[2], -1.0);

                            //gbl.MField.SetRec(curLng, curAngV / 360.0, curAngH / 360.0);

                            isChangePos = true;
                            str = "";
                            int curStatus = (str0[0].Length >= 6) ? St7Lib.CheckInt(str0[0].Substring(5, 1), 0) : 0;
                            gbl.MField.curStatus = curStatus;

                            if (curStatus != curTuibiStatus) {

                                curTuibiStatus = curStatus;
                                isChangeTuibiStatus = true;
                            }
                            /*
                            if (str0[2].StartsWith("E")) {
                                gbl.ts.curTuibiStatus = 0;
                                gbl.ts.isChangeTuibiStatus = true;
                                ERR_CODE = str0[2];
                                Log.v("ERR receive", ReceiveStr);
                                //continue;
                            }
                            if (str0[3].startsWith("E")) {
                                gbl.ts.curTuibiStatus = 0;
                                gbl.ts.isChangeTuibiStatus = true;
                                ERR_CODE = str0[3];
                                Log.v("ERR receive", ReceiveStr);
                                //continue;
                            }
                            if (str0[4].startsWith("E")) {
                                gbl.ts.curTuibiStatus = 0;
                                gbl.ts.curTuibiStatus = 0;
                                gbl.ts.isChangeTuibiStatus = true;
                                ERR_CODE = str0[4];
                                Log.v("ERR receive", ReceiveStr);
                                //continue;
                            }
                            */
                            int bl = St7Lib.CheckInt(str0[1], -1);
                            if (0 <= bl) {
                                batteryLevel = bl;
                            }
                            //2026.03.23 by A.Iimuro 単独測定/連続測定処理
                            if (Env.curTSMode == Env.TS_MODE_TUIBI && !chbContMeasure.Checked) {
                                sendStr = "*ST1";
                                curCmd = 1;

                            }
                        }
                        if (str0[0].StartsWith("*ST1")) {
                            gbl.MField.SetRecAS(receiveStr);
                            // curLng を -1 にするため、SetRec()を直接呼び出す
                            //gbl.MField.SetRecAS(receiveStr);

                            curLng = -1.0;
                            curAngV = St7Lib.CheckDouble(str0[3], -1.0);
                            curAngH = St7Lib.CheckDouble(str0[2], -1.0);

                            gbl.MField.SetRec(curLng, curAngV / 360.0, curAngH / 360.0);

                            if (Env.curTSMode == Env.TS_MODE_SHIJUN) {
                                Env.curTSMode = Env.TS_MODE_SOKUTEI;
                                btnAutoTsuibi.Text = (Env.getCurTSModeStr());
                            }
                            sendStr = "";
                            //2026.03.23 by A.Iimuro 単独測定/連続測定処理
                            if (Env.curTSMode == Env.TS_MODE_TUIBI) {
                                if (chbContMeasure.Checked) {
                                    //連続測定に切り替わった時は *ST3を送信する
                                    sendStr = "*ST3";
                                    curCmd = 1;
                                } else {
                                    //単独測定の時は追尾のみを継続する

                                }
                            } else {
                                //視準測定の時は追尾を中断する
                                sendStr = "*ST0";
                                curCmd = 1;
                            }
                            isChangePos = true;
                        }
                        if (str0[0].StartsWith("OK")) {  // *TBON の時
                            //if (sendStr0 == "*TBON" || sendStr0.StartsWith("*SJ3")) {

                                if ((Env.curTSMode == Env.TS_MODE_TUIBI)) {
                                    if (chbContMeasure.Checked) {
                                        sendStr = "*ST3";
                                    } else {
                                        sendStr = "*ST1";
                                    }
                                    curCmd = 1;
                                } else {
                                    if (Env.curTSMode == Env.TS_MODE_SHIJUNSOKUTEI) {
                                        // && SendStr.startsWith("*SJ3 0")) {
                                        sendStr = "*ST3";    // 2022.09.19 by A.Iimuro なぜか消されていた！
                                        curCmd = 1;
                                    } else if (Env.curTSMode == Env.TS_MODE_SHIJUN) {
                                        sendStr = "*ST1";
                                        curCmd = 1;
                                    }
                                    receiveStr = "OK " + sendStr;
                                }
                            //}
                        }
                        // 2022/02/16 Add RC ボタン RC Start------------------------------
                        if (str0[0].StartsWith("*RCA")) {
                            /*
							if (gbl.curTSMode == gbl.TS_MODE_TUIBI)
								SendStr = calcChkEOR("*SJ3 1,0,,0,,,,");	//測距のみ
							else
								SendStr = calcChkEOR("*SJ3 1,0,,0,,,,");
							curCmd = 1;
							SendStr0 = SendStr;
							*/
                        }
                        //長押し
                        if (str0[0].StartsWith("*RCB")) {
                            if (Env.curTSMode == Env.TS_MODE_TUIBI)
                                sendStr = "*SJ3 1,1,,0,,,,";    //追尾
                            else
                                sendStr = "*SJ3 1,0,,0,,,,";
                            curCmd = 1;
                        }
                    }

                }
                //Console.WriteLine(receiveStr);

                if (sendStr != "" && !sendStr.StartsWith("\u0014")) {
                    WriteData(sendStr);
                    sendStr = "";
                }


            } catch (Exception ex) {


            }
        }
        public void resetConnect() {
            if (isConnect)
                DisConnect();
            isConnect = false;
            isConnect0 = false;
            curStatus0 = -1;
            curStatus = 0;//2022/02/14 Iimuro Add
            btnType = -1;//2022/02/14 Iimuro Add
        }

        public bool isChangeTracking() {

            if (Env.isSupportTuibi()) {
                if (gbl.MField.isTracking() != isTracking0) {
                    return true;
                } else {
                    return false;
                }
            } else {
                return false;
            }
        }

        public void SetImgBtn(Button btn, int _btnType) {
            if (btnType != _btnType) {
                if (_btnType == 1) {
                    btn.Enabled = true;
                    //if (btnType != -1) gbl.sm.playSound(gbl.sm.mp3_1);
                }
                if (_btnType == 0) {
                    btn.Enabled = false;
                    //if (gbl.curTSMode == gbl.TS_MODE_TUIBI)
                    //if (btnType != -1) gbl.sm.playSound(gbl.sm.mp3_3);
                }
                btnType = _btnType;
            }
        }
        public bool SetImgBtn(Button btn) {
            if (Env.isUseGPS()) {
                if (Env.isUseGPSRTK()) {
                    if (isConnect) {
                        curStatus = 1;
                    } else {
                        curStatus = 0;
                    }
                }
                if (gbl.Gps.isGetLocation)
                    SetImgBtn(btn, 1);
                else
                    SetImgBtn(btn, 0);
                return true;
            }
            if (isConnect) {
                // 2022.01.31 by A.Iimuro. 視準のみで視準後に測定のみに変える！　Start--------
                if (Env.curTSMode == Env.TS_MODE_SHIJUN) {
                    if (gbl.MField.isAngOK())
                        btnAutoTsuibi.Text = (Env.getTSModeStr(Env.TS_MODE_SOKUTEI));
                    else
                        btnAutoTsuibi.Text = (Env.getTSModeStr(Env.TS_MODE_SHIJUN));
                }
                // 2022.01.31 by A.Iimuro. 視準のみで視準後に測定のみに変える！　End----------
                if (gbl.MField.isTracking()) {
                    //追尾中（記録可能）
                    if (curStatus != 2) {
                        //                        curStatus = 2;
                        //btn.setImageResource(R.drawable.ic_kiroku);
                        //btn.setEnabled(true);
                        SetImgBtn(btn, 1);
                        //追加 2019.11.15 by muro.
                        isSearching = true;
                        /*
                        if (chbContMeasure.Checked)
                            btnAutoTsuibi.Text = ("追尾中断");
                        else
                            btnAutoTsuibi.Text = ("測定");
                        //btnAutoTsuibi.Text = ("追尾中断");
                        */
                        //if (gbl.isSupportTuibi())
                        //SetSearchMode(true);
                    }
                    //} else if (gbl.mfield.isLngOK()) {
                    //} else if (gbl.mfield.isLngOK() && !gbl.isSupportTuibi()) {	// 		測距　→　測距＋追尾でない！ 2020.10.22 by A.Iimuro
                } else if (gbl.MField.isLngOK()) {  // 		測距　→　測距＋追尾でない！ 2022.01.31 by A.Iimuro
                                                    //btn.setImageResource(R.drawable.ic_kiroku);
                                                    //btn.setEnabled(true);
                    curStatus = 1;  //2022/09/23 A.Iimuro Add
                    SetImgBtn(btn, 1);
                } else {
                    //追尾なし（記録不可）
                    SetImgBtn(btn, 0);
                    if (curStatus != 1) {
                        curStatus = 1;
                        //btn.setImageResource(R.drawable.ic_kiroku_off);
                        //btn.setEnabled(false);
                        //setImgBtn(btn, 0);
                        //SetSearchMode(false);
                    }
                }
                if (isChangeTuibiStatus) {
                    isChangeTuibiStatus = false;
                    SetSearchMode(true);
                }
            } else {
                curStatus = 0;//2022/04/07 shirai check　ここが原因でたまに繋がっているにも関わらず「接続」のアイコンが出てた。
                SetImgBtn(btn, 0);
                if (isConnect0 != isConnect) {
                    isConnect0 = isConnect;
                    //btn.setImageResource(R.drawable.ic_kiroku_off);
                    //btn.setEnabled(false);
                    //setImgBtn(btn, 0);
                    return false;
                }
            }
            if (curStatus != curStatus0) {
                if (curStatus == 0) {
                    //btn.setImageResource(R.drawable.ic_kiroku_off);
                    //btn.setEnabled(false);
                    SetImgBtn(btn, 0);
                }
                if (curStatus == 1) {
                    if (curStatus0 == 2) {
                        // 2022.01.31 by A.Iimuro. 追尾可能TSでも測定できる様にした Start-----------
                        //if (gbl.isSupportTuibi()) {
                        if (Env.curTSMode == Env.TS_MODE_TUIBI) {//2022/02/14 Iimuro Add 「if (gbl.curTSMode == gbl.TS_MODE_TUIBI)の条件」
                            //gbl.sm.playSound(gbl.sm.mp3_3);
                        }
                        SetImgBtn(btn, 0);
                        // 2022.01.31 by A.Iimuro. 追尾可能TSでも測定できる様にした End-----------
                    }
                }
                if (curStatus == 2) {
                    // 2022.01.31 by A.Iimuro. 追尾可能TSでも測定できる様にした Start-----------
                    //if (gbl.isSupportTuibi()) {
                    if (Env.curTSMode == Env.TS_MODE_TUIBI) {
                        //gbl.sm.playSound(gbl.sm.mp3_1);
                    }
                    // 2022.01.31 by A.Iimuro. 追尾可能TSでも測定できる様にした End-------------
                    SetImgBtn(btn, 1);
                }
                curStatus0 = curStatus;
                return true;
            }
            return false;
        }
        public void SetSearchMode(bool OnOff) {
            //Log.d("TS_SetSearchMode","now");
            if (OnOff) {
                if (gbl.MField.isTracking()) {
                    isSearching = true;
                    if (chbContMeasure.Checked)
                        btnAutoTsuibi.Text = ("追尾中断");
                    else
                        btnAutoTsuibi.Text = ("測定");

                    curStatus = 2;
                    return;
                }
                if (gbl.MField.isSearching()) {
                    isSearching = true;
                    btnAutoTsuibi.Text = ("サーチ中断");
                    curStatus = 1;
                    return;
                }
                if (!Env.isSupportTuibi()) {
                    if (gbl.MField.isLngOK()) {
                        isSearching = true;
                        //btnAutoTsuibi.Text = ("測定中断");
                        btnAutoTsuibi.Text = Env.getCurTSModeStr();
                        curStatus = 1;
                        return;
                    }
                }
                /*
                if (curTuibiStatus == 4) {
                    isSearching = true;
                    searchBtn.setText("サーチ中断");
                    curStatus = 1;
                    return;
                }
                if (curTuibiStatus == 5) {
                    isSearching = true;
                    searchBtn.setText("追尾中断");
                    curStatus = 2;
                    return;
                }
                 */
            }
            //if (!gbl.isUseLN100()) {
            //			curTuibiStatus = 0;
            //			curStatus = 1;
            //}

            /*	2022/01/25 Cut by Iimuro
                    if (gbl.isSupportTuibi()) {
                        searchBtn.setText("自動追尾");
                        //curStatus = 1;
                    } else {
                        if (gbl.isSupportAutoShijun()) {
                            searchBtn.setText("自動視準");
                        } else {
                            searchBtn.setText("測定");
                        }
                    }
            */
            // 2022/01/25 Change by Iimuro.
            btnAutoTsuibi.Text = (Env.getCurTSModeStr());

            isSearching = false;
            /*
            if (OnOff) {
                searchBtn.setText("サーチ中断");
                gbl.ts.isSearching = true;
            } else {
                if (gbl.isSupportTuibi()) {
                    searchBtn.setText("自動追尾");
                } else {
                    searchBtn.setText("自動視準");
                }
                gbl.ts.isSearching = false;
            }
            */
        }


        public bool Init2() {
            string str;

            if (isLN100) {
                //プリズム
                SetITRGT();
                SetLight(true);
                searchAreaBtn.Font = new Font(searchAreaBtn.Font.FontFamily, 10.0F, searchAreaBtn.Font.Style);
            } else {
                Env.initCurTSMode();
                //                btnAutoTsuibi.Text = Env.getCurTSModeStr();

                //チルト補正
                str = "/B 0,0,0,,0,0,0,0," + Env.Tilt.ToString() + ",0,0,0";
                GetTCPRec(1, str, 0);

                //距離分解能・角度分解能
                if (Env.getTSMode() != Env.TS_MODE_SOKUTEI) {
                    GetTCPRec(1, "/Dk 0,0", 0);     //NG
                } else {
                    // 角度分解能は、'/B'で設定可能
                }

                //測距モード
                if (Env.SokkyoMode == Env.SokkyoMode_Seimitu)
                    GetTCPRec(1, "Xb", 0);        //精密
                else
                    GetTCPRec(1, "Xd", 0);        //高速

                //プリズム
                SetITRGT();     // NG

                //ライト                
                SetLight(true);


                //機械点
                SetKikai();


            }
            SetSearchAreaBtn();

            return true;
        }
        private void SetSearchAreaBtn() {
            if (isLN100) {
                searchAreaBtn.Font = new Font(searchAreaBtn.Font.FontFamily, 10.0F, searchAreaBtn.Font.Style);
            } else {
                searchAreaBtn.Font = new Font(searchAreaBtn.Font.FontFamily, 7.0F, searchAreaBtn.Font.Style);
                searchAreaBtn.Text = String.Format("サーチ\n範囲\n{0:#}/{1:#}", Env.SearchH, Env.SearchV);
            }
        }
        //器械点指定
        public bool SetKikai() {
            if (isLN100) {

            } else {
                SetKP(gbl.KikaiMan.kp.X, gbl.KikaiMan.kp.Y, gbl.KikaiMan.kp.Z);
                SetHAng(gbl.KikaiMan.angK);
            }
            return true;
        }
        //器械点
        public bool SetKP(double x, double y, double z) {
            if (isLN100) {

            } else {
                string rec = "/Da " + x.ToString("F3") + "," + y.ToString("F3") + "," + z.ToString("F3");
                GetTCPRec(1, rec, 0);
            }
            return true;
        }
        //後視点
        public bool SetHAng(double ang) {
            if (isLN100) {

            } else {
                string rec = "/Dc " + ang2dms(ang, 0);
                GetTCPRec(1, rec, 0);
            }
            return true;
        }
        //測定モード　高速・連続
        public bool SetMode(bool isPrecision, bool isContinuous) {
            if (isLN100) {

            } else {
                string rec;
                if (isPrecision) {
                    //精密
                    if (isContinuous) {
                        //連続
                        rec = "Xb";
                    } else {
                        //単回
                        rec = "Xa";
                    }
                } else {
                    //高速
                    if (isContinuous) {
                        //連続
                        rec = "Xd";
                    } else {
                        //単回
                        rec = "Xc";
                    }
                }
                GetTCPRec(1, rec, 0);
            }
            return true;
        }
        //ターゲットタイプ設定（プリズム）
        public bool SetITRGT() {

            String ret;
            if (isLN100) {

                String rec = String.Format("@ITRGT,1,34,{0:#},", Env.PrismVal);

                ret = GetTCPRec(CMD_ITRGT, rec, 1);
                if (ret.Equals(""))
                    return false;
            } else {
                if (Env.getTSMode() != Env.TS_MODE_SOKUTEI) {
                    string str = "*/PG 3,";    //360°プリズム

                    if (Env.Prism == 0)
                        str = "*/PG 0,";
                    if (Env.Prism == 3)
                        str = "*/PG 2,";

                    str = str + Env.PrismVal.ToString() + ",";
                    GetTCPRec(CMD_ITRGT, str, 0);
                }
            }
            return true;
        }
        public bool SetLightType(int type, int val) {
            if (isLN100) {

            } else {
                if (Env.getTSMode() != Env.TS_MODE_SOKUTEI) {
                    string str;
                    if (type == Env.LightPat_LED) {
                        str = "*/PF 1," + (val + 1).ToString();
                    } else {
                        str = "*/PF 2,1";
                    }
                    GetTCPRec(1, str, 0);
                }
            }

            return true;
        }
        public bool SetLight(bool OnOff) {

            String ret;
            if (isLN100) {

                double GuideLightPat = Env.GuideLightPat;
                double GuideLightVal = Env.GuideLightVal;

                if (OnOff) {
                    String rec = "@LGLON," + GuideLightPat + "," + GuideLightVal + ",";

                    ret = GetTCPRec(CMD_LGLON, rec, 1);
                } else {
                    //ret = wifiTCP.GetTCPRec(wifiUDP.serverIP, "@LGLOF,", 2, 3, 1);
                    ret = GetTCPRec(CMD_LGLOF, "@LGLOF,", 1);
                }
                if (ret.Equals(""))
                    return false;
            } else {
                if (Env.getTSMode() != Env.TS_MODE_SOKUTEI) {
                    if (OnOff) {
                        SetLightType(Env.LightPat, Env.LightVal);
                        ret = GetTCPRec(CMD_LGLON, "*GLON", 0);
                    } else {
                        ret = GetTCPRec(CMD_LGLOF, "*GLOFF", 0);
                    }
                }
            }
            return true;
        }
        public bool SetLP(bool OnOff) {
            String ret;
            if (isLN100) {
                if (OnOff) {
                    //ret = wifiTCP.GetTCPRec(wifiUDP.serverIP, "@LLPON,1,2,", 2, 3, 1);
                    ret = GetTCPRec(CMD_LLPON, "@LLPON,1,2,", 1);
                } else {
                    //ret = wifiTCP.GetTCPRec(wifiUDP.serverIP, "@LLPOF,", 2, 3, 1);
                    ret = GetTCPRec(CMD_LLPOF, "@LLPOF,", 1);
                }
                if (ret.Equals(""))
                    return false;
            } else {

            }
            return true;
        }
        //回転動作停止+測定中止
        public bool StopSokutei() {
            if (isLN100) {

            } else {
                if (Env.curTSMode == Env.TS_MODE_SOKUTEI) {
                    GetTCPRec(CMD_MFILD, "\u0012", 1);
                } else {
                    GetTCPRec(3, "*Q", 0);

                }
                //gbl.MField.ClearLng();
                //gbl.MField.curStatus = 0;           //2026.5.25 added 動きがおかしいため追加
            }
            return true;
        }
        //回転動作停止
        public bool StopRotate() {
            if (isLN100) {
                SetAutoTrack(false);
            } else {
                if (Env.curTSMode == Env.TS_MODE_SOKUTEI) {
                    GetTCPRec(CMD_MFILD, "\u0012", 1);
                } else {
                    GetTCPRec(3, "*R", 0);

                }
            }
            return true;
        }
        //水平角０°設定
        public bool SetZero() {
            if (isLN100) {

            } else {
                GetTCPRec(3, "Xh", 0);
            }
            return true;
        }
        //自動視準
        public bool SetLSL(bool OnOff) {
            String ret;
            if (isLN100) {
                if (OnOff) {
                    //ret = wifiTCP.GetTCPRec(wifiUDP.serverIP, "@LLPON,1,2,", 2, 3, 1);
                    ret = GetTCPRec(CMD_LSLON, "@LSLON,", 1);
                } else {
                    //ret = wifiTCP.GetTCPRec(wifiUDP.serverIP, "@LLPOF,", 2, 3, 1);
                    ret = GetTCPRec(CMD_LSLOF, "@LSLOF,", 1);
                }
                if (ret.Equals(""))
                    return false;
            } else {
                curTuibiStatus = 4;	//プリズム待ち（サーチ中）
                if (OnOff) {
                    StopRotate();
                    SetFILD(false);
                    if (Env.UseRC == Env.UseRC_Yes)
                        GetTCPRec(1, "*SJ3 1,0,,0,,,,", 0);
                    else
                        GetTCPRec(1, "*SJ3 0,0,,0,,,,", 0);
                } else {
                    StopRotate();
                }
            }
            return true;
        }
        public bool SetAutoTrackMode(int mode) {
            autoTrackMode = mode;
            return (SetAutoTrack(true));
        }
        public bool SetAutoTrack(bool OnOff) {
            String ret;
            if (isLN100) {
                if (OnOff) {
                    if (autoTrackMode == 0) {
                        //ret = wifiTCP.GetTCPRec(wifiUDP.serverIP, "@RTRCK,0,", 2, 0, 0);
                        ret = GetTCPRec(CMD_RTRCK, "@RTRCK,0,", 0);
                    } else {
                        //ret = wifiTCP.GetTCPRec(wifiUDP.serverIP, "@RTRCK,1,", 2, 0, 0);
                        ret = GetTCPRec(CMD_RTRCK, "@RTRCK,1,", 0);
                    }
                } else {
                    //ret = wifiTCP.GetTCPRec(wifiUDP.serverIP, "@SMOTR,", 2, 0, 0);
                    ret = GetTCPRec(CMD_SMOTR, "@SMOTR,", 0);
                }
                if (ret.Equals(""))
                    return false;
            } else {
                curTuibiStatus = 4;	//プリズム待ち（サーチ中）
                if (OnOff) {
                    //if (btTS.curStatus2 == btTS.STATUS_BUSY)
                    //    StopRotate();
                    //else {
                    //StopRotate();
                    StopSokutei();

                    //ret = btTS.getTCPRec(1,  gbl.ts.ssid, "*TBON", 0);//2022/02/25 Iimuro コメント化

                    //2022/02/25 Iimuro Add Start-----------------------------------
                    if (Env.UseRC == Env.UseRC_Yes) {
                        ret = GetTCPRec(1, "*SJ3 1,1,,0,,,,", 0);
                    } else {
                        // 2022/02/23 by A.Iimuro. *TBONは使わないようにする！

                        ret = GetTCPRec(1, "*TBON", 0);// 2022/09/19 by A.Iimuro. *TBONに戻す
                                                       //ret = btTS.getTCPRec(1, gbl.ts.ssid, "*SJ3 0,1,,0,,,,", 0);//2022/09/19 Iimuro コメント化
                    }
                    //2022/02/25 Iimuro Add End-----------------------------------
                    //btTS.getTCPRec(1,  gbl.ts.ssid, "*TBON", 1);
                    //}
                } else {
                    StopRotate();
                }
            }
            return true;
        }
        //測角のみ計測
        public bool SetFILD0(bool OnOff) {
            //Log.d("TS_AS_SetFILD0", "now");
            String ret;
            if (OnOff) {
                //btTS.getTCPRec(1,  gbl.ts.ssid, "Ei", 1);
                GetTCPRec(CMD_MFILD, "*ST1", 0);
            } else {
                //StopRotate();
                GetTCPRec(CMD_MFILD, "*ST0", 0);
            }
            return true;
        }
        public bool SetFILD(bool OnOff) {
            String ret;
            if (isLN100) {
                if (OnOff) {
                    //ret = wifiTCP.GetTCPRec(wifiUDP.serverIP, "@MFILD,0,2,", 2, 10, 1);
                    //ret = wifiTCP.GetTCPRec(wifiUDP.serverIP, "@MFILD,1,1,", 2, 10, 1);
                    ret = GetTCPRec(CMD_MFILD, "@MFILD,1,1,", 0);
                } else {
                    ret = GetTCPRec(CMD_SFILD, "@SFILD,", 0);
                }
                if (ret.Equals(""))
                    return false;
            } else {
                if (OnOff) {
                    //btTS.getTCPRec(1,  gbl.ts.ssid, "Ei", 1);
                    if (Env.curTSMode == Env.TS_MODE_SOKUTEI) {
                        ret = GetTCPRec(CMD_MFILD, "\u0014", 1);
                    } else {
                        ret = GetTCPRec(CMD_MFILD, "*ST3", 1);
                        //ret = GetTCPRec(CMD_MFILD, "*ST2", 1);

                    }
                } else {
                    //btTS.getTCPRec(1,  gbl.ts.ssid, "Ei", 1);
                    if (Env.curTSMode == Env.TS_MODE_SOKUTEI) {
                        ret = GetTCPRec(CMD_MFILD, "\u0012", 1);
                    } else {
                        //StopRotate();
                        ret = GetTCPRec(CMD_SFILD, "*ST0", 1);
                    }
                }
            }
            return true;
        }
        public bool SetBATT(bool OnOff) {
            String ret;
            if (isLN100) {
                if (OnOff) {
                    //ret = wifiTCP.GetTCPRec(wifiUDP.serverIP, "@MBATT,", 2, 0, 0);
                    ret = GetTCPRec(CMD_MBATT, "@MBATT,", 0);
                } else {
                    //ret = wifiTCP.GetTCPRec(wifiUDP.serverIP, "@SBATT,", 2, 0, 0);
                    ret = GetTCPRec(CMD_SBATT, "@SBATT,", 0);
                }
                if (ret.Equals(""))
                    return false;
            } else {
            }
            return true;
        }
        public bool SetTILT(bool OnOff) {
            String ret;
            if (isLN100) {
                if (OnOff) {
                    //ret = wifiTCP.GetTCPRec(wifiUDP.serverIP, "@MTILT,0,", 2, 3, 1);
                    ret = GetTCPRec(CMD_MTILT, "@MTILT,0,", 1);
                } else {
                    //ret = wifiTCP.GetTCPRec(wifiUDP.serverIP, "@STILT,", 2, 0, 0);
                    ret = GetTCPRec(CMD_STILT, "@STILT,", 0);
                }
                if (ret.Equals(""))
                    return false;
            } else {

            }
            return true;
        }
        public bool SetPA(double H, double V) {
            SetSearchMode(false);
            StopRotate();

            string str;
            string ret;

            if (Env.UseRC == Env.UseRC_Yes) {
                if (Env.isSupportTuibi()) {
                    str = String.Format("*/PA 2,1,{0:F4},{1:F4}", H, V);
                } else {
                    str = String.Format("*/PA 2,0,{0:F4},{1:F4}", H, V);
                }
            } else {
                if (Env.isSupportTuibi()) {
                    str = String.Format("*/PA 1,1,{0:F4},{1:F4}", H, V);
                } else {
                    str = String.Format("*/PA 1,0,{0:F4},{1:F4}", H, V);
                }
            }
            ret = GetTCPRec(CMD_RTRCK, str, 1);

            return true;
        }
        public bool RotateV(double ang) {
            String ret;
            String rec;
            if (isLN100) {
                ang = gbl.MField.angV + ang / 360.0;
                if (ang < 0.18055556)
                    ang = 0.18055556;
                if (0.31944444 <= ang)
                    ang = 0.31944444;

                rec = String.Format("@RSPOS,0,1,{0:#.########},0,0.0,", ang);
                //ret = wifiTCP.GetTCPRec(wifiUDP.serverIP, rec, 2, 30, 1);
                ret = GetTCPRec(CMD_RSPOS, rec, 1);
                if (ret.Equals(""))
                    return false;
            } else {

            }
            return true;
        }
        public bool RotateH(double ang) {
            String ret;
            String rec;

            if (isLN100) {
                if (ang < 0)
                    ang += 1.0;
                if (1.0 <= ang)
                    ang -= 1.0;

                rec = String.Format("@RSPOS,0,0,0.0,1,{0:#.########},", ang);
                //ret = wifiTCP.GetTCPRec(wifiUDP.serverIP, rec, 2, 30, 1);
                ret = GetTCPRec(CMD_RSPOS, rec, 1);
                if (ret.Equals(""))
                    return false;
            } else {

            }
            return true;
        }
        public bool RotateSPD(int type, int n) {
            String rec;
            String ret;
            if (isLN100) {
                rec = String.Format("@RSSPD,0,{0:#},", n);
                //ret = wifiTCP.GetTCPRec(wifiUDP.serverIP, rec, 2, 0, 0);
                ret = GetTCPRec(CMD_RSSPD, rec, 0);
                if (ret.Equals(""))
                    return false;
            } else {
                if (n == 0) {
                    rec = "*R";
                    GetTCPRec(CMD_RSSPD, rec, 0);
                    SetFILD0(true);
                } else {
                    SetFILD0(false);
                    StopRotate();
                    if (type == 1) {
                        //rec = String.format("*JG %+d,0,,,,", n*2000);
                        if (n < 0)
                            rec = String.Format("*JH-{0:##}V+00", n * -1);
                        else
                            rec = String.Format("*JH+{0:##}V+00", n);
                    } else if (type == 2) {//2022/02/25 Iimuro 条件else⇒else if (type == 2)に変更
                        if (n < 0)
                            rec = String.Format("*JH+00V-{0:##}", n * -1);
                        else
                            rec = String.Format("*JH+00V+{0:##}", n);
                    } else {//2022/02/25 Iimuro 条件elseとその中身追加。
                            // type:3 RCリモートコントロール使用
                        if (n < 0)
                            rec = "*SJ101000";
                        else
                            rec = "*SJ3 2,0,,0,,,,";
                    }
                    GetTCPRec(CMD_RSSPD, rec, 0);
                }
            }
            return true;
        }

        private string ang2dms(double ang, int type) {
            if (type == 0) {
                if (ang < 0)
                    ang += 1.0;
            } else {
                if (ang < -0.5)
                    ang += 1.0;
                if (0.5 < ang)
                    ang -= 1.0;
            }
            ang *= 360.0;

            return convertToSexagesimal(ang, type);
        }
        //緯度経度の度分秒->少数変換
        public double convertToDecimal(double du, double fen, double miao) {
            if (du < 0)
                return -(Math.Abs(du) + (Math.Abs(fen) + (Math.Abs(miao) / 60)) / 60);

            return Math.Abs(du) + (Math.Abs(fen) + (Math.Abs(miao) / 60)) / 60;

        }
        //少数->度分秒変換
        public String convertToSexagesimal(double num, int type) {
            int du = (int)Math.Floor(Math.Abs(num));    //整数部分
            double temp = getdPoint(Math.Abs(num)) * 60;
            int fen = (int)Math.Floor(temp); //整数部分
            double miao = getdPoint(temp) * 60;
            int miao0 = (int)miao;

            if (type == 0)
                return String.Format("{0:000}{1:00}{2:00}", du, fen, miao0);
            //return String.Format("%03d%02d%02d", du, fen, miao0);

            if (num < 0)
                return String.Format("-{0:000}{1:00}{2:00}", du, fen, miao0);
            //return String.format("-%03d%02d%02d", du, fen, miao0);

            return String.Format("+{0:000}{1:00}{2:00}", du, fen, miao0);
            //return String.format("+%03d%02d%02d", du, fen, miao0);

        }
        //小数部分を取り出す
        public double getdPoint(double num) {
            /*
            double d = num;
            int fInt = (int)d;
            BigDecimal b1 = new BigDecimal(Double.toString(d));
            BigDecimal b2 = new BigDecimal(Integer.toString(fInt));
            double dPoint = b1.subtract(b2).floatValue();
            */
            double dPoint = num - (int)num;
            return dPoint;
        }
    }
}
