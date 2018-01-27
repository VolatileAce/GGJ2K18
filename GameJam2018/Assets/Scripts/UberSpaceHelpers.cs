using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

    public static class UberSpaceHelpers
    {
        //[Pure]
        /// <summary>
        /// formats seconds into XX:XX formatted string. Eg. 75 becomes 01:15
        /// </summary>
        public static string SecondsToString(int in_Seconds)
        {
            if (in_Seconds <= 0)
                return "--:--";

            string Result = string.Empty;

            int Minutes = in_Seconds / 60;

            if (Minutes < 10)
                Result += "0";
            else
                Result += Minutes;

            if (Minutes < 1)
                Result += "0";
            else
                Result += Minutes;
            Result += ":";

            while (in_Seconds >= 60)
                in_Seconds -= 60;

            if ((in_Seconds) < 10)
                Result += "0";

            Result += (in_Seconds);

            return Result;
        }
    }
