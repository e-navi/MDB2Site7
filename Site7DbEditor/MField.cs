using System;

namespace Site7DbEditor
{
    public class MField
    {
        public bool isChange;
        public double lng = 0.0;
        public double angV = 0.0;
        public double angV0 = -1.0;
        public double angH = 0.0;
        public double angH0 = -1.0;
        public double curPAngH;
        public int curStatus;

        public bool isError = false;
        public string errorMessage = "";

        public bool isTracking()
        {
            return curStatus == 5;
        }

        public bool isSearching()
        {
            return curStatus == 4;
        }

        public bool isLngOK()
        {
            if (Env.isUseLN100()) return isTracking();
            return lng > 0.0;
        }
    }
}
