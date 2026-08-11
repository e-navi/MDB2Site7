using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site7 {
    public class MField {
        public bool isChange;
        public double lng = 0.0;
        public double angV = 0.0;
        public double angV0 = -1.0;
        public double angH = 0.0;
        public double angH0 = -1.0;
        public double curPAngH; //編集中の座標の方向角
        public int curStatus;   //0: 追尾なし、2:自動視準（接続中）、3:追尾停止（接続中）、4:プリズム待ち(サーチ)、5:ロック（追尾中）
        
        public bool isError = false;
        public string errorMessage = "";

        public bool isTracking() {
            //Log.d("MField_isTracking","now");
            //if (Env.isUseLN100()) {
            if (curStatus == 5)
                return true;
            else
                return false;
            //} else {
            //    if (curStatus == 5)
            //        return true;
            //    else
            //        return false;
            //}
        }
        public bool isSearching() {
            //Log.d("MField_isSearching","now");
            //if (gbl.isUseLN100()) {
            if (curStatus == 4)
                return true;
            else
                return false;
            //} else {
            //    if (curStatus == 4)
            //        return true;
            //    else
            //        return false;
            //}
        }
        public bool isLngOK() {
            //Log.d("MField_isLngOK","now");
            if (Env.isUseLN100()) {
                return isTracking();
            }

            //if (lng == 0.0)//2022.01.31 by A.Iimuro. 視準のみの時に lng に -1.0 をセット(clearと区別する)
            if (lng <= 0.0)
                return false;
            else
                return true;
        }
        //2022.01.31 by A.Iimuro.視準のみの場合のチェック用に追加 Start---------
        public bool isAngOK() {
            //Log.d("MField_isLngOK","now");
            //if (gbl.isUseLN100()) {
            return isTracking();
            //}
            //if (lng == 0.0)
            //    return false;
            //else
            //    return true;
        }
        //2022.01.31 by A.Iimuro.視準のみの場合のチェック用に追加 End-----------
        public double CheckDbl(String str, double defaultValue) {
            //Log.d("MField_CheckDbl", "now");
            double d;
            if (!double.TryParse(str, out d)) {
                d = defaultValue;
            }
            return d;
        }
        public int CheckInt(String str, int defaultValue) {
            //Log.d("MField_CheckInt", "now");
            int i;
            if (!int.TryParse(str, out i)) {
                i = defaultValue;
            }
            return i;
        }
        /*
        double calcHoseiLng(double lng) {
            //Log.d("MField_calcHoseiLng", "now");
            double val = 1.0;
            //lng += (double)gbl.pm.PrismValue*0.001;
            if (gbl.gm.cur.isUseHosei) {
                double R = 6370000;
                double Ng = 0.0;    //ジオイド高   とりあえず 0をセット
                val = R / (R + gbl.gm.cur.hoseiMeanElv + Ng);

                val = val * gbl.gm.cur.hoseiScale;
            }
            lng *= val;
            return lng;
        }
        double calcRevHoseiLng(double lng) {
            //Log.d("MField_calcRevHoseiLng", "now");
            double val = 1.0;
            if (gbl.gm.cur.isUseHosei) {
                double R = 6370000;
                double Ng = 0.0;    //ジオイド高   とりあえず 0をセット
                val = R / (R + gbl.gm.cur.hoseiMeanElv + Ng);

                val = val * gbl.gm.cur.hoseiScale;
            }
            lng /= val;
            //lng -= (double)gbl.pm.PrismValue*0.001;
            return lng;
        }
        */
        public void ClearLng() {
            //Log.d("MField_ClearLng","now");
            lng = 0.0;
            isChange = true;
        }
        public bool SetRec(double curLng, double curAngV, double curAngH) {
            //Log.d("MField_SetRec!","now");
            if (0 < curLng) {
                //    curLng = calcHoseiLng(curLng);
            }
            if (lng != curLng || angV != curAngV || angH != curAngH) {
                if (angH0 == -1.0) {
                    angH0 = curAngH;
                }
                if (angV0 == -1.0) {
                    angV0 = curAngV;
                }
                lng = curLng;
                angV = curAngV;
                angH = curAngH;
                isChange = true;
                //if (gbl.curKikaiMode == gbl.km.KMODE_B) {
                //gbl.ts.curStatus = 2;
                //}

                //if (curStatus == 0) {
                //    curStatus = 1;
                //}
            }
            isError = false;
            return isError;
        }
        public bool SetRecAS(String rec) {
            if (rec == null) {
                isError = true;
                errorMessage = "受信エラー";
                return isError;
            }
            String[] cols = rec.Split(',');
            double curLng = St7Lib.CheckDouble(cols[4], 0.0);
            double curAngV = St7Lib.CheckDouble(cols[3], 0.0);
            double curAngH = St7Lib.CheckDouble(cols[2], 0.0);
            curStatus = St7Lib.CheckInt(cols[0].Substring(5, 1), 0);

            //修正！2026.03.12 by A.Iimuro 視準測定の時に curStatus に3が返る！
            //if (curStatus == 5 || curStatus == 2) {
            //if (curStatus == 5 || curStatus == 2 || curStatus == 3) {
            if (curStatus == 5 || curStatus == 2 || curStatus == 3 || curStatus == 0) {
                    SetRec(curLng, curAngV / 360.0, curAngH / 360.0);
                isError = false;
            } else {
                isError = true;
                errorMessage = "測定できません";   
            }
            return isError;
        }
        public bool SetRecAS2(String rec) {
            if (rec == null) {
                isError = true;
                errorMessage = "受信エラー";
                return isError;
            }
            if (rec.StartsWith("E200")) {
                isError = true;
                errorMessage = "E200:測定できません";
                return isError;
            }
            String[] cols = rec.Split(' ');

            if (cols.Length == 4) {
                double curLng = St7Lib.CheckDouble2(cols[0], 0.0, -4);
                double curAngV = St7Lib.CheckDouble2(cols[1], 0.0, -5);
                double curAngH = St7Lib.CheckDouble2(cols[2], 0.0, -5);

                SetRec(curLng, curAngV / 360.0, curAngH / 360.0);

                if (Env.curTSMode0 == Env.TS_MODE_SHIJUN) {
                    Env.curTSMode = Env.curTSMode0;
                }
                isError = false;

            } else {
                isError = true;
                errorMessage = "測定できません";
            }
            return isError;
        }
        public void SetRecLN100(String rec) {
            //Log.d("MField_SetRec", "now");
            if (rec == null)
                return;
            String[] cols = rec.Split(',');
            double curLng = St7Lib.CheckDouble(cols[1], 0.0);
            double curAngV = St7Lib.CheckDouble(cols[2], 0.0);
            double curAngH = St7Lib.CheckDouble(cols[3], 0.0);
            int ln100p5 = St7Lib.CheckInt(cols[5], 0);
            curStatus = 0;
            if (ln100p5 == 2) { //追尾中
                curStatus = 5;
                SetRec(curLng, curAngV, curAngH);
                //if (gbl.ts.curStatus != 2) {
                //    gbl.ts.isChangeTuibiStatus = true;
                //    gbl.ts.curTuibiStatus = 5;
                //}
            }
            if (ln100p5 == 3) { //プリズムサーチ
                curStatus = 4;
                //if (gbl.ts.curStatus != 1) {
                //if (gbl.ts.curTuibiStatus != 4) {
                //gbl.ts.curStatus = -1;
                //}
                //gbl.ts.isChangeTuibiStatus = true;
                //gbl.ts.curTuibiStatus = 4;
                //}
            }
            if (ln100p5 == 0) { //待機中
                curStatus = 3;
                //if (gbl.ts.curStatus != 0) {
                //    gbl.ts.isChangeTuibiStatus = true;
                //    gbl.ts.curTuibiStatus = 0;
                //}
            }
            //if (!gbl.ts.isConnect) {
            //    gbl.ts.isConnect = true;
            //}
        }
        public double ToRadian(double angle) {
            return (double)(angle * Math.PI / 180);
        }
        public double getLngH() {
            //Log.d("MField_getLngH", "now");
            double lh = lng * Math.Sin(ToRadian(angV * 360.0));
            return (Math.Abs(lh));
        }
        public double getLngV() {
            //Log.d("MField_getLngV", "now");
            return (lng * Math.Cos(ToRadian(angV * 360.0)));
        }
    }
}
