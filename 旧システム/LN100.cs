using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Site7 {
    public class LN100 {
        public FormMain formMain;
        string com;
        SerialPort serialPort = null;

        public void SetCom(string _com) {
            com = _com;
        }
        public void Connect() {
            if (serialPort == null) {
                serialPort = new SerialPort() {
                    BaudRate = 9600, //変調回数(1秒あたり)
                    DataBits = 8, // 1変調あたりのbit数
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
                //ポートの監視を開始する
                serialPort.Open();

                WriteData("@LGLON,1,2,");



            }
        }
        public void WriteData(String str) {
            if (serialPort == null) { 
                return; 
            }
            serialPort.Write(str+"\r");
        }
        public void WriteData2(String str) {
            if (serialPort == null) { 
                return; 
            }
            serialPort.Write(str);
        }
        /// シリアルポート・データ受信イベント
        /// ※データを受信する都度呼ばれるので、電文が全て送られてくるとは限らないので注意 <summary>
        /// シリアルポート・データ受信イベント

        public bool isChangePos = false;
        public  XYZ curPos = new XYZ();
        public string receivedBuff = "";

        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e) {
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
                            int p5 = St7Lib.CheckInt(strs[5], 0);
                            if (p5 == 2) {
                                curPos.X = St7Lib.CheckDouble(strs[1], 0.0);
                                curPos.Y = St7Lib.CheckDouble(strs[2], 0.0);
                                curPos.Z = St7Lib.CheckDouble(strs[3], 0.0);
                                isChangePos = true;
                            }
                        }
                    }
                }
            } catch (Exception ex) {


            }
        }

    }
}
