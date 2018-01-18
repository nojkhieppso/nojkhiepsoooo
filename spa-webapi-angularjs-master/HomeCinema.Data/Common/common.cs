using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HomeCinema.Data.Common
{
    public static class common
    {
        public static int searchrBool(string StartAt, string EndAt)
        {
            int result = 0;
            if (string.IsNullOrEmpty(StartAt) && string.IsNullOrEmpty(EndAt))
            {
                result = 0;
            }
            else
            {
                if (string.IsNullOrEmpty(StartAt) && !string.IsNullOrEmpty(EndAt))
                {
                    result = 1;
                }
                else
                {
                    if (!string.IsNullOrEmpty(StartAt) && string.IsNullOrEmpty(EndAt))
                    {
                        result = 2;
                    }
                    else
                    {
                        result = 3;
                    }

                }
            }
            return result;
        }

        public static string Generate(string s1)
        {
            string result = string.Empty;
            using (MD5 md5Hash = MD5.Create())
            {
                result = GetMd5Hash(md5Hash, s1);
            }
            return result;
        }
        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
        public static string DayOfWeekvi(string DayOfWeek)
        {
            string resultDayOfWeek = string.Empty;
            switch (DayOfWeek.Trim())
            {
                case "Sunday":
                    {
                        resultDayOfWeek="Chủ nhật";
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

        public static double Hours(this string str)
        {
            double hours = 0;
            string[] arr = str.Split(':');
            string[] arrr = arr[1].Split(' ');
            if(arrr[1].ToLower()=="am")
            {
                hours = double.Parse(arr[0]);
            }
            else
            {
                hours = double.Parse(arr[0])+12;
            }
            return hours;
        }
        public static double Hours24(this string str)
        {
            double hours = 0;
            if (str.Trim().Length>0)
            {
                string[] arr = str.Split(':');
                hours = double.Parse(arr[0]);
            }
            
            return hours;
        }
        public static double Minutes(this string str)
        {
            string[] arr = str.Split(':');

            string[] minutes = arr[1].Split(' ');

            return double.Parse(minutes[0].ToString());
        }
        public static double Minutes24(this string str)
        {
            double minutes = 0;
            if (str.Trim().Length > 0)
            {
                string[] arr = str.Split(':');
                minutes = double.Parse(arr[1].ToString());
            }
            return minutes;
        }
        public static double formatcurency(this double str)
        {
            CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");
            return double.Parse(str.ToString("#.###",cul.NumberFormat));
        }
        public static DateTime ChangeTime(this DateTime dateTime, int hours, int minutes, int seconds, int milliseconds)
        {
            return new DateTime(
                dateTime.Year,
                dateTime.Month,
                dateTime.Day,
                hours,
                minutes,
                seconds,
                milliseconds,
                dateTime.Kind);
        }
        public static string Resetdate(this string date )
        {
            string result = string.Empty;
            string[] arr = date.Split('T');
            result = arr[0] + "T00:00:00";
            return result;
        }
        public static double tinhtiengiang(this double sotientiet,int sochau,int sotiet)
        {
            double total = 0;
            total = sotientiet * sochau * sotiet;
            return total;
        }
    }
}
