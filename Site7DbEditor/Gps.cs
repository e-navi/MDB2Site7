using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Site7DbEditor.BLXY;
using static System.Net.Mime.MediaTypeNames;

namespace Site7DbEditor {
    public class Gps {
        string com;
        SerialPort serialPort = new SerialPort();

        public bool isOpen = false;
        public bool isChangePos = false;
        public XYZ curPos = new XYZ();
        public string receivedBuff = "";

        public string receiveStr;

        public GSIGEOME2011 gsigeome2011;
        public GSIGEOME2024 gsigeome2024;


        public BLXY blxy;

        public bool isEnableGPS;
        public bool isGetLocation;
        public bool isChange;
        public bool isGeoidH = true;
        public int gpsStatusMode = 2;   // 0:RTK-fix, 1:RTK-float, 2:DGPS-fix, 3: 単独測位

        public double kh = Def.GetIniDouble("TS", "GPS器械高", 1.5);

        public XYZ gpsP = new XYZ();
        public double gpsH;
        public int gpsStatus;
        public int gpsSatelite; // 衛星数
        public double gpsDOP;   // DOP
        public double gpsTime;  // 時間

        public int gpsCntMode = 0;  // 0:初期 1:カウント中 2:カウント終了
        public List<XYZ> gpsList = new List<XYZ>();
        public XYZ gpsPAve = null;

        public BL gpsBL;

        public string GetGpsStatusText()
        {
            return gpsStatus switch
            {
                4 => "RTK-Fix",
                5 => "RTK-Float",
                2 => "DGPS",
                1 => "単独測位",
                _ => "未受信"
            };
        }


        public Gps() {
            gsigeome2011 = new GSIGEOME2011();
            gsigeome2024 = new GSIGEOME2024();
            blxy = new BLXY(BLXY.SKEI_WLD, Env.KeiNum);
        }
        public void SetCom(string _com) {
            com = _com;
        }
        public void DisConnect() {
            if (serialPort != null) {
                serialPort.Close();
                serialPort = null;
            }
        }
        public void Connect() {

            if (serialPort.IsOpen) {
                serialPort.Close();
            }
            serialPort.BaudRate = 9600; //変調回数(1秒あたり)
            serialPort.DataBits = 8; // 1変調あたりのbit数
            serialPort.StopBits = StopBits.One;
            serialPort.Parity = Parity.None;
            serialPort.Handshake = Handshake.None;
            serialPort.DtrEnable = false;
            serialPort.RtsEnable = false;
            serialPort.PortName = com;
            serialPort.ReadTimeout = 10000;
            serialPort.WriteTimeout = 10000;


            try {
                //ポートの監視を開始する
                serialPort.Open();

                serialPort.DataReceived += SerialPort_DataReceived_Gps;

            } catch (Exception e) {
                MessageBox.Show(e.Message);
            }

        }
        public void startGpsCount() {
            gpsList.Clear();
            gpsCntMode = 1;
            gpsPAve = null;
        }
        public string getGpsStr1() {
            string str = "取得状況：";
            if (isGetLocation) {
                if (gpsStatus == 1) {
                    str += Env.GPSStatusStrs[Env.GPSStatus_Standalone];
                } else if (gpsStatus == 2) {
                    str += Env.GPSStatusStrs[Env.GPSStatus_GDPS];
                } else if (gpsStatus == 5) {
                    str += Env.GPSStatusStrs[Env.GPSStatus_Float];
                } else if (gpsStatus == 4) {
                    str += Env.GPSStatusStrs[Env.GPSStatus_Fix];
                }
            }

            return str;
        }
        public string getGpsStr2() {
            string str3 = "";
            str3 += String.Format("HDOP:{0:#.#}", gpsDOP);
            str3 += String.Format(" 衛星数:{0:#}", gpsSatelite);
            String strTime = String.Format("{0:######}", gpsTime);
            String JI = "00";
            String FUN = "00";
            String BYO = "00";
            if (strTime.Length > 4) {
                JI = strTime.Substring(0, strTime.Length - 4);
                FUN = strTime.Substring(strTime.Length - 4, 2);
                BYO = strTime.Substring(strTime.Length - 2, 2);
            }
            str3 += "　" + JI + "時" + FUN + "分" + BYO + "秒";

            return str3;
        }

        private void SerialPort_DataReceived_Gps(object sender, SerialDataReceivedEventArgs e) {
            SerialPort serialPort1 = (SerialPort)sender;
            string str = "";
            try {
                //receivedData = this.serialPort1.ReadLine();
                int rbyte = serialPort1.BytesToRead;

                for (int i = 0; i < rbyte; i++) {
                    int tmp = serialPort1.ReadChar();
                    char ctmp = (char)tmp;
                    receivedBuff += ctmp;

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

                    }
                    if (ctmp == 13) {
                        str = receivedBuff;
                        receivedBuff = "";
                        //textBox1.AppendText(str);

                        string[] strs = str.Split(',');

                        //2024/03/12 Shirai Add start--------------------
                        string NMEA = "$GNGGA";
                        string NMEA2 = "$GNGGA";
                        if (Env.isUsei93IMU()) {
                            NMEA = "$GNPOS";
                            NMEA2 = "$GPPOS";
                        }
                        //2024/03/12 Shirai Add end--------------------

                        //if (cols[0].equals("$GNGGA") && (10 <= cols.length) && (10 <= cols[2].length())) {//2024/03/18 Shirai コメント化
                        if (((strs[0].Equals(NMEA)) || (strs[0].Equals(NMEA2))) && (10 <= strs.Length) && (10 <= strs[2].Length)) {//2024/03/18 Shirai Add

                            //if ((strs[0].Equals("$GNGGA") || strs[0].Equals("$GNPOS") || strs[0].Equals("$GPPOS")) &&
                            // (10 <= strs.Length) && (10 <= strs[2].Length)) {

                            double lat = St7Lib.CheckAng(strs[2], 0.0);
                            double lng = St7Lib.CheckAng(strs[4], 0.0);
                            BL bl = new BL(lat, lng);
                            BLXY.P2 xy = blxy.BL2XY(bl);
                            gpsP.X = xy.X;
                            gpsP.Y = xy.Y;

                            gpsBL.Lat = lat;
                            gpsBL.Lng = lng;
                            /*
                            */
                            gpsH = St7Lib.CheckDouble(strs[9], 0.0) + St7Lib.CheckDouble(strs[11], 0.0) - kh;

                            if (Env.GPSHeight == Env.GPSHeight_2011) {
                                double gh = gsigeome2011.getGeoid(lat, lng);
                                if (gh == gsigeome2011.geoLim) {
                                    gpsH = 0.0;
                                } else {
                                    //gpsH = CheckDbl(cols[9], 0.0) - gbl.km.sh + gh;
                                    gpsH = gpsH - gh;
                                }
                            } else if (Env.GPSHeight == Env.GPSHeight_2024) {
                                double gh = gsigeome2024.getGeoidHeight(lat, lng);//2024⇒2011
                                double cr = gsigeome2024.getCorrection(lat, lng);//2024⇒2011
                                if (cr == gsigeome2024.NODATA) {
                                    cr = 0.0;
                                }
                                if (gh == gsigeome2024.NODATA) {
                                    gpsH = 0.0;
                                } else {
                                    //gpsH = CheckDbl(cols[9], 0.0) - gbl.km.sh + gh;
                                    gpsH = gpsH - gh - cr;
                                }
                            }
                            gpsP.Z = gpsH;

                            gpsStatus = St7Lib.CheckInt(strs[6], -1);
                            //20210513白井追加 Start-----------------------------
                            gpsSatelite = St7Lib.CheckInt(strs[7], 0);
                            gpsDOP = St7Lib.CheckDouble(strs[8], 5.0);
                            gpsTime = St7Lib.CheckDouble(strs[1], 010101.0) + 90000;
                            if (gpsTime > 240000) {
                                gpsTime -= 240000;
                            }
                            //20210513白井追加 End-----------------------------

                            //isGetLocation = Env.isGoodGPS(gpsStatus);
                            isGetLocation = true;
                            isChange = Env.isGetGPS(gpsStatus);

                            if (Env.isGoodGPS(gpsStatus) && gpsCntMode == 1) {
                                if (gpsList.Count < Env.GPSCount) {
                                    gpsList.Add(new XYZ(gpsP));
                                }
                                if (0 < gpsList.Count) {
                                    for (int j = 0; j < gpsList.Count; j++) {
                                        if (j == 0) {
                                            gpsPAve = new XYZ(gpsList[j]);
                                        } else {
                                            gpsPAve.X += gpsList[j].X;
                                            gpsPAve.Y += gpsList[j].Y;
                                            gpsPAve.Z += gpsList[j].Z;
                                        }
                                    }
                                    gpsPAve.X /= gpsList.Count;
                                    gpsPAve.Y /= gpsList.Count;
                                    gpsPAve.Z /= gpsList.Count;
                                }
                            }
                            /*
                            if (gbl.pm.isGPSRTKFix()) {
                                if (gbl.pm.isGPSRTKFix(gpsStatus) && (0 <= gpsTbl.gpsPcnt)) {
                                    gpsTbl.Add(gpsP);
                                    gpsTbl.Calc();
                                } else {
                                    gpsTbl.End();
                                }
                            }
                            */

                            isChangePos = true;
                        }
                        receiveStr = strs[0];
                    }
                }
            } catch (Exception ex) {


            }
        }

    }
    //ジオイド高を求める
    public class GSIGEOME2011 {
        public float[,] dat;
        double glamn, glomn, dgla, dglo;
        int nla, nlo, ikind;
        String verNo;
        int ys = 1802;
        int xs = 1202;
        public float geoLim = (float)999.0;

        String line;

        public GSIGEOME2011() {
            dat = new float[ys, xs];
            for (int i = 0; i < ys; i++) {
                for (int j = 0; j < xs; j++) {
                    dat[i, j] = geoLim;
                }
            }
            try {
                var assembly = Assembly.GetExecutingAssembly();
                var resourceName = "Site7DbEditor.Resources.gsigeome.bin";

                using (var stream = assembly.GetManifestResourceStream(resourceName)) {
                    if (stream != null) {
                        using (var reader = new BinaryReader(stream)) {
                            var rows = reader.ReadInt32();
                            var cols = reader.ReadInt32();
                            glamn = reader.ReadDouble();
                            glomn = reader.ReadDouble();
                            for (int i = 0; i < cols && i < ys; i++) {
                                for (int j = 0; j < rows && j < xs; j++) {
                                    dat[i, j] = reader.ReadSingle();
                                }
                            }
                        }
                    }
                }
            } catch { }
        }

        public void saveBin(string path) {
            string fname = Path.Combine(path, "gsigeome.bin");
            int rows = xs;
            int cols = ys;

            using (var stream = new FileStream(fname, FileMode.Create))
            using (var writer = new BinaryWriter(stream)) {
                // 配列の次元情報を先に保存
                writer.Write(rows);
                writer.Write(cols);
                writer.Write(glamn);
                writer.Write(glomn);

                // データ本体を保存
                for (int i = 0; i < cols; i++) {
                    for (int j = 0; j < rows; j++) {
                        writer.Write(dat[i, j]);
                    }
                }
            }
        }
        public void loadBin(string path) {
            string fname = Path.Combine(path, "gsigeome.bin");
            using (var stream = new FileStream(fname, FileMode.Open))
            using (var reader = new BinaryReader(stream)) {
                // 保存した次元情報を読み込む
                var rows = reader.ReadInt32();
                var cols = reader.ReadInt32();
                var glamn = reader.ReadDouble();
                var glomn = reader.ReadDouble();
                dat = new float[ys, xs];

                // データ本体を読み込む
                for (int i = 0; i < cols; i++) {
                    for (int j = 0; j < rows; j++) {
                        dat[i, j] = reader.ReadSingle();
                    }
                }
            }
        }

        //ジオイド高を返す lon,lat: "ddd.dddddd"（dddmm.mmmmではない）
        public float getGeoid(double lat, double lon) {
            //Log.d("GPSRTK-GSIGEOME_getGeo", "now");
            double dx, dy, x, y;
            float el2, xx, yy;
            int ix, iy, jx, jy, iadx, iady;

            el2 = 0.00001f;
            dx = 1.5 / 60.0;
            dy = 1.0 / 60.0;
            ix = (int)((lon - glomn) / dx) + 1;
            iy = (int)((lat - glamn) / dy) + 1;
            x = (lon - glomn) / dx - (ix - 1);
            y = (lat - glamn) / dy - (iy - 1);
            jx = ix + 1;
            jy = iy + 1;

            if ((0 < ix) && (ix < xs) && (0 < iy) && (iy < ys)) {
                yy = (float)Math.Abs(y);
                xx = (float)Math.Abs(x);
                iadx = 99;
                iady = 99;
                if (yy < el2) {
                    iady = 0;
                } else if ((1.0 - xx) < el2) {
                    iady = 1;
                }
                if (xx < el2) {
                    iadx = 0;
                } else if ((1.0 - yy) < el2) {
                    iadx = 1;
                }
                if (iady < 10) {
                    // the point is on the grid
                    if (iadx < 10) {
                        return (dat[iy + iady, ix + iadx]);
                    }
                    // the point is on the meridian cell line
                    if ((dat[iy + iady, ix] == geoLim) || (dat[iy + iady, jx] == geoLim)) {
                        // error : non significant data area
                        return geoLim;
                    } else {
                        return (float)((1.0 - x) * (dat[iy + iady, ix] + x * dat[iy + iady, jx]));
                    }
                } else if (iadx < 10) {
                    // the point is on the parallel cell line
                    if ((dat[iy, ix + iadx] == geoLim) || (dat[jy, ix + iadx] == geoLim)) {
                        // error: non significant data area
                        return geoLim;
                    } else {
                        return (float)((1.0 - y) * dat[iy, ix + iadx] + y * dat[jy, ix + iadx]);
                    }
                }
                // process for the point which is not on the grid lines
                if ((dat[jy, ix] == geoLim) || (dat[jy, jx] == geoLim) ||
                        (dat[iy, ix] == geoLim) || (dat[iy, jx] == geoLim)) {
                    // error: non significant data area
                    return geoLim;
                } else {
                    return (float)((1.0 - x) * (1.0 - y) * dat[iy, ix] + y * (1.0 - x) * dat[jy, ix] +
                            x * (1.0 - y) * dat[iy, jx] + dat[jy, jx] * x * y);
                }
            } else {
                // 範囲外
                return geoLim;
            }
        }
    }
    //2025/04/04 Shirai Add ジオイド2024用クラス End----------------------------------------------
    public class GSIGEOME2024 {
        private const double LAT_MIN = 15.0;
        private const double LAT_MAX = 50.0;
        private const double LON_MIN = 120.0;
        private const double LON_MAX = 160.0;
        private const double DELTA_LAT = 1.0 / 60.0; // 1 minute
        private const double DELTA_LON = 1.5 / 60.0; // 1.5 minutes
        private const int NROWS = 2101;
        private const int NCOLS = 1601;
        public double NODATA = -9999.0;

        private double[,] geoidGrid;
        private double[,] correctionGrid;
        private double[,] geoidGrid2;
        private double[,] correctionGrid2;

        public GSIGEOME2024() {
            //InputStream geoStream = gbl.curContext.getResources().openRawResource(R.raw.jpgeo2024);
            //InputStream corrStream = gbl.curContext.getResources().openRawResource(R.raw.hrefconv2024);

            //string geoidName = "Site7.Resources.jpgeo2024.isg";
            //string correctionName = "Site7.Resources.hrefconv2024.isg";
            //this.geoidGrid = loadISGGrid(geoidName);
            //this.correctionGrid = loadISGGrid(correctionName);


            string geoidName2 = "Site7.Resources.jpgeo2024.bin";
            string correctionName2 = "Site7.Resources.hrefconv2024.bin";
            this.geoidGrid = loadBin(geoidName2);
            this.correctionGrid = loadBin(correctionName2);


            /*
            this.geoidGrid = loadBin(Path.Combine(path, "jpgeo2024.bin"));
            this.correctionGrid = loadBin(Path.Combine(path, "hrefconv2024.bin"));
             * 
             */
        }
        public void saveBin(string path) {
            saveBin(Path.Combine(path, "jpgeo2024.bin"), geoidGrid);
            saveBin(Path.Combine(path, "hrefconv2024.bin"), correctionGrid);
        }
        public void saveBin(string fname, double[,] array) {
            int rows = array.GetLength(0);
            int cols = array.GetLength(1);

            using (var stream = new FileStream(fname, FileMode.Create))
            using (var writer = new BinaryWriter(stream)) {
                // 配列の次元情報を先に保存
                writer.Write(rows);
                writer.Write(cols);

                // データ本体を保存
                for (int i = 0; i < rows; i++) {
                    for (int j = 0; j < cols; j++) {
                        writer.Write(array[i, j]);
                    }
                }
            }
        }
        private double[,] loadBin(string name) {
            double[,] array = new double[NROWS, NCOLS];
            try {
                var assembly = Assembly.GetExecutingAssembly();
                using (var stream = assembly.GetManifestResourceStream(name)) {
                    if (stream != null) {
                        using (var reader = new BinaryReader(stream)) {
                            var rows = reader.ReadInt32();
                            var cols = reader.ReadInt32();
                            array = new double[rows, cols];
                            for (int i = 0; i < rows; i++) {
                                for (int j = 0; j < cols; j++) {
                                    array[i, j] = reader.ReadDouble();
                                }
                            }
                        }
                    }
                }
            } catch { }
            return array;
        }

        private double[,] loadISGGrid(string name) {
            double[,] grid = new double[NROWS, NCOLS];
            try {
                var assembly = Assembly.GetExecutingAssembly();
                using (var stream = assembly.GetManifestResourceStream(name)) {
                    if (stream != null) {
                        using (var sr = new StreamReader(stream)) {
                            string line, line2;
                            while ((line = sr.ReadLine()) != null) {
                                if (line.Contains("end_of_head"))
                                    break;
                            }
                            for (int i = 0; i < NROWS; i++) {
                                line = sr.ReadLine();
                                if (line == null) break;
                                line2 = line.Trim();
                                string[] tokens = Regex.Split(line2, @"\s+");
                                double d;
                                for (int j = 0; j < Math.Min(tokens.Length, NCOLS); j++) {
                                    d = NODATA;
                                    Double.TryParse(tokens[j], out d);
                                    grid[i, j] = d;
                                }
                            }
                        }
                    }
                }
            } catch { }
            return grid;
        }

        // 緯度経度からジオイド高を取得
        public double getGeoidHeight(double lat, double lon) {
            return interpolateGrid(geoidGrid, lat, lon);
        }

        // 緯度経度から補正量を取得
        public double getCorrection(double lat, double lon) {
            return interpolateGrid(correctionGrid, lat, lon);
        }

        // 双線形補間処理
        private double interpolateGrid(double[,] grid, double lat, double lon) {
            if (lat < LAT_MIN || lat > LAT_MAX || lon < LON_MIN || lon > LON_MAX) {
                return NODATA;
            }

            double rowF = (LAT_MAX - lat) / DELTA_LAT;
            double colF = (lon - LON_MIN) / DELTA_LON;

            int row = (int)Math.Floor(rowF);
            int col = (int)Math.Floor(colF);

            if (row < 0 || row >= NROWS - 1 || col < 0 || col >= NCOLS - 1) {
                return NODATA;
            }

            double t = rowF - row;
            double s = colF - col;

            double v00 = grid[row, col];
            double v10 = grid[row + 1, col];
            double v01 = grid[row, col + 1];
            double v11 = grid[row + 1, col + 1];

            if (v00 == NODATA || v10 == NODATA || v01 == NODATA || v11 == NODATA) {
                return NODATA;
            }

            return (1 - t) * (1 - s) * v00 +
                    t * (1 - s) * v10 +
                    (1 - t) * s * v01 +
                    t * s * v11;
        }
    }
    //2025/04/04 Shirai Add ジオイド2024用クラス End----------------------------------------------
}

