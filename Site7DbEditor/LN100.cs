using System;
using System.IO.Ports;

namespace Site7DbEditor
{
    public class LN100
    {
        public FormEditor? formMain;
        string com = "";
        SerialPort? serialPort = null;

        public bool isChangePos = false;
        public XYZ curPos = new XYZ();
        public string receivedBuff = "";

        public void SetCom(string _com)
        {
            com = _com;
        }

        public void Connect()
        {
            try
            {
                if (serialPort == null && !string.IsNullOrEmpty(com))
                {
                    serialPort = new SerialPort()
                    {
                        BaudRate = 9600,
                        DataBits = 8,
                        StopBits = StopBits.One,
                        Parity = Parity.None,
                        Handshake = Handshake.None,
                        DtrEnable = false,
                        RtsEnable = false,
                        PortName = com,
                        ReadTimeout = 10000,
                        WriteTimeout = 10000,
                    };
                    serialPort.DataReceived += SerialPort_DataReceived;
                    serialPort.Open();
                    WriteData("@LGLON,1,2,");
                }
            }
            catch { }
        }

        public void WriteData(string str)
        {
            if (serialPort == null || !serialPort.IsOpen) return;
            try { serialPort.Write(str + "\r"); } catch { }
        }

        public void WriteData2(string str)
        {
            if (serialPort == null || !serialPort.IsOpen) return;
            try { serialPort.Write(str); } catch { }
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (serialPort == null) return;
            try
            {
                int rbyte = serialPort.BytesToRead;
                for (int i = 0; i < rbyte; i++)
                {
                    int tmp = serialPort.ReadChar();
                    char ctmp = (char)tmp;
                    receivedBuff += ctmp;

                    if (ctmp == 02)
                    {
                        receivedBuff = "";
                    }
                    if (ctmp == 03 || ctmp == 13)
                    {
                        string str = receivedBuff;
                        receivedBuff = "";

                        string[] strs = str.Split(',');
                        if (strs.Length > 5 && strs[0].Equals("@MFILD"))
                        {
                            int p5 = St7Lib.CheckInt(strs[5], 0);
                            if (p5 == 2)
                            {
                                curPos.X = St7Lib.CheckDouble(strs[1], 0.0);
                                curPos.Y = St7Lib.CheckDouble(strs[2], 0.0);
                                curPos.Z = St7Lib.CheckDouble(strs[3], 0.0);
                                isChangePos = true;
                            }
                        }
                    }
                }
            }
            catch { }
        }
    }
}
