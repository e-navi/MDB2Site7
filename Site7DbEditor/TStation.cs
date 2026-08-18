using System;
using System.IO.Ports;
using System.Windows.Forms;

namespace Site7DbEditor
{
    public class TStation
    {
        //public FormEditor? formMain;

        private const int LONG_CLICK_THRESHOLD_MS = 500; // 500É~Éäïb

        string com = "";
        SerialPort? serialPort;

        public bool isConnect = false;
        public bool isChangePos = false;
        public bool isLN100 = true;
        public XYZ curPos = new XYZ();
        public double curLng;
        public double curAngV;
        public double curAngH;
        public string receivedBuff = "";


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


        public Button? btnAutoTsuibi;
        public Button? searchAreaBtn;
        public CheckBox? chbContMeasure;
        public int rsspd = 2;
        public int idleCnt = 0;

        public void SetCom(bool _isLN100, string _com)
        {
            isLN100 = _isLN100;
            com = _com;
        }

        public void Connect()
        {
            try {
                DisConnect();
                if (string.IsNullOrEmpty(com)) return;

                serialPort = new SerialPort()
                {
                    PortName = com,
                    BaudRate = 115200,
                    DataBits = 8,
                    StopBits = StopBits.One,
                    Parity = Parity.None,
                    Handshake = Handshake.None,
                    ReadTimeout = 2000,
                    WriteTimeout = 2000
                };
                if (isLN100)
                    serialPort.DataReceived += SerialPort_DataReceived_LN100;
                else
                    serialPort.DataReceived += SerialPort_DataReceived_TSA;
                serialPort.Open();
                isConnect = true;
            }
            catch
            {
                isConnect = false;
            }
        }

        public void DisConnect()
        {
            try
            {
                if (serialPort != null)
                {
                    if (serialPort.IsOpen) serialPort.Close();
                    serialPort.Dispose();
                    serialPort = null;
                }
            }
            catch { }
            isConnect = false;
        }

        public bool CheckConnect()
        {
            //return isConnect && (serialPort != null && serialPort.IsOpen);
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
                MessageBox.Show("çƒê⁄ë±ÇµÇƒÇ≠ÇæÇ≥Ç¢");
            }
            //Console.WriteLine("SerialPort is Connecting.");
            return isConnect;
        }

        public void WriteData(string str)
        {
            if (serialPort == null || !serialPort.IsOpen) return;
            try { serialPort.Write(str + "\r"); } catch { }
        }

        public void LN100_BtnClick(Button btn, int tag)
        {
        }

        public void LN100_MouseDown(Button btn, int tag) {
        }
        public void LN100_MouseUp(Button btn, int tag, long elapsedMs) {
        }

        public void AS_BtnClick(Button btn, int tag) {
        }
        public void AS_BtnClick_3() {
        }
        public void AS_BtnClick_11() {
        }
        private void selTSMode(Button btn) {
        }
        public void AS_MouseDown(Button btn, int tag) {
        }
        public void AS_MouseUp(Button btn, int tag, long elapsedMs) {
        }

        private void SerialPort_DataReceived_LN100(object sender, SerialDataReceivedEventArgs e) {
        }
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SerialPort_DataReceived_TSA(object sender, SerialDataReceivedEventArgs e) {
        }
    }
}
