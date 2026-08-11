using System;
using System.IO.Ports;
using System.Windows.Forms;

namespace Site7DbEditor
{
    public class TStation
    {
        public FormEditor? formMain;
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

        public Button? btnAutoTsuibi;
        public Button? searchAreaBtn;
        public CheckBox? chbContMeasure;
        public int rsspd = 2;
        public int idleCnt = 0;

        public void SetCom(string _com)
        {
            com = _com;
        }

        public void Connect()
        {
            try
            {
                DisConnect();
                if (string.IsNullOrEmpty(com)) return;

                serialPort = new SerialPort()
                {
                    PortName = com,
                    BaudRate = 9600,
                    DataBits = 8,
                    StopBits = StopBits.One,
                    Parity = Parity.None,
                    Handshake = Handshake.None,
                    ReadTimeout = 5000,
                    WriteTimeout = 5000
                };
                serialPort.DataReceived += SerialPort_DataReceived;
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
            return isConnect && (serialPort != null && serialPort.IsOpen);
        }

        public void WriteData(string str)
        {
            if (serialPort == null || !serialPort.IsOpen) return;
            try { serialPort.Write(str + "\r"); } catch { }
        }

        public void LN100_BtnClick(Button btn, int tag)
        {
            if (tag == 1) WriteData("@LGLON,1,2,");
            else if (tag == 2) WriteData("@LGLON,0,2,");
        }

        public void LN100_MouseDown(Button btn, int tag) { }
        public void LN100_MouseUp(Button btn, int tag, long elapsedMs) { }

        public void AS_BtnClick(Button btn, int tag) { }
        public void AS_MouseDown(Button btn, int tag) { }
        public void AS_MouseUp(Button btn, int tag, long elapsedMs) { }
        public void AS_BtnClick_11() { }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (serialPort == null) return;
            try
            {
                string line = serialPort.ReadLine();
                if (string.IsNullOrEmpty(line)) return;

                string[] strs = line.Split(',');
                if (strs.Length > 3)
                {
                    curLng = St7Lib.CheckDouble(strs[1], 0.0);
                    curAngV = St7Lib.CheckDouble(strs[2], 0.0);
                    curAngH = St7Lib.CheckDouble(strs[3], 0.0);

                    curPos = gbl.KikaiMan.cnvP(curLng, curAngH, curAngV);
                    isChangePos = true;
                }
            }
            catch { }
        }
    }
}
