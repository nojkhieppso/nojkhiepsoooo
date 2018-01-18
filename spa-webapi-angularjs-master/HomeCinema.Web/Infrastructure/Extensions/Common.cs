using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeCinema.Web.Infrastructure.Extensions
{
    public class Common
    {
        internal static string DayOfWeekvi(DayOfWeek DayOfWeek)
        {
            string resultDayOfWeek = string.Empty;
            switch (DayOfWeek.ToString().Trim())
            {
                case "Sunday":
                    {
                        resultDayOfWeek = "Chủ nhật";
                        break;
                    }
                case "Monday":
                    {
                        resultDayOfWeek = "Thứ 2";
                        break;
                    }

                case "Tuesday":
                    {
                        resultDayOfWeek = "Thứ 3";
                        break;
                    }
                case "Wednesday":
                    {
                        resultDayOfWeek = "Thứ 4";
                        break;
                    }
                case "Thursday":
                    {
                        resultDayOfWeek = "Thứ 5";
                        break;
                    }

                case "Friday":
                    {
                        resultDayOfWeek = "Thứ 6";
                        break;
                    }
                case "Saturday":
                    {
                        resultDayOfWeek = "Thứ 7";
                        break;
                    }
            }
            return resultDayOfWeek;
        }

        public static bool truefalse(string input)
        {
            bool output = false;
            if(string.Equals(input.ToLower(),"true"))
            {
                output = true;
            }
            return output;
        }
    }
}