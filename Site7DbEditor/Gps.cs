using System;
using System.Collections.Generic;
using System.IO.Ports;
using static Site7DbEditor.BLXY;

namespace Site7DbEditor
{
    public class Gps
    {
        string com = "";
        SerialPort? serialPort;

        public bool isOpen = false;
        public bool isChangePos = false;
        public XYZ curPos = new XYZ();
        public string receivedBuff = "";
        public string receiveStr = "";

        public BLXY blxy;

        public bool isEnableGPS;
        public bool isGetLocation;
        public bool isChange;
        public bool isGeoidH = true;
        public int gpsStatusMode = 2;

        public double kh = Def.GetIniDouble("TS", "GPS器械高", 1.5);

        public XYZ gpsP = new XYZ();
        public double gpsH;
        public int gpsStatus;
        public int gpsSatelite;
        public double gpsDOP;
        public double gpsTime;

        public int gpsCntMode = 0;
        public List<XYZ> gpsList = new List<XYZ>();
        public XYZ? gpsPAve = null;

        public BL gpsBL = new BL();

        public Gps()
        {
            blxy = new BLXY(BLXY.SKEI_WLD, Env.KeiNum);
        }

        public void SetCom(string _com)
        {
            com = _com;
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
            isOpen = false;
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
                    DtrEnable = false,
                    RtsEnable = false,
                    ReadTimeout = 5000,
                    WriteTimeout = 5000
                };
                serialPort.DataReceived += SerialPort_DataReceived;
                serialPort.Open();
                isOpen = true;
            }
            catch
            {
                isOpen = false;
            }
        }

        public void startGpsCount()
        {
            gpsCntMode = 1;
            gpsList.Clear();
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (serialPort == null) return;
            try
            {
                string line = serialPort.ReadLine();
                if (string.IsNullOrEmpty(line)) return;

                if (line.StartsWith("$GPGGA") || line.StartsWith("$GNGGA") || line.StartsWith("$GAGGA"))
                {
                    string[] strs = line.Split(',');
                    if (strs.Length > 9)
                    {
                        gpsStatus = St7Lib.CheckInt(strs[6], 0);
                        gpsSatelite = St7Lib.CheckInt(strs[7], 0);
                        gpsDOP = St7Lib.CheckDouble(strs[8], 0.0);

                        double lat = St7Lib.CheckAng(strs[2], 0.0);
                        double lng = St7Lib.CheckAng(strs[4], 0.0);
                        gpsBL = new BL(lat, lng);
                        var p2 = blxy.BL2XY(gpsBL);

                        gpsH = St7Lib.CheckDouble(strs[9], 0.0) - kh;
                        gpsP = new XYZ(p2.Y, p2.X, gpsH);
                        curPos = gpsP;
                        isChangePos = true;
                    }
                }
            }
            catch { }
        }

        public string GetGpsStatusText()
        {
            return gpsStatus switch
            {
                1 => "単独測位",
                2 => "DGPS",
                4 => "RTK-Fix",
                5 => "RTK-Float",
                6 => "推測航法",
                0 => "未取得",
                _ => $"Status={gpsStatus}"
            };
        }
    }
}
