using System;
using System.Globalization;
using System.Text;
using Abp.Timing;

namespace Akh.Breed
{
    public static class PersianDate
    {
        public static string GetShamsiStr(this DateTime? data)
        {
            string format = "yyyy/MM/dd hh:mm:ss";
            return GetShamsiStr(data, format);
        }
        
        public static string GetShamsiStr(this DateTime? data, string format)
        {
            if (data.HasValue)
            {
                return GetShamsiStr(data.Value, format);
            }
            return "";
        }
        
        public static DateTime? GetShamsi(this DateTime? data)
        {
            if (data.HasValue)
            {
                DateTime? dt = GetShamsi(data.Value);
                return dt;
            }
            return data;
        }
        
        public static DateTime? GetMiladi(this DateTime? data)
        {
            if (data.HasValue)
            {
                DateTime? dt = GetMiladi(data.Value);
                return dt;
            }
            return data;
        }

        public static string GetShamsiStr(this DateTime data)
        {
            string format = "yyyy/MM/dd hh:mm:ss";
            return GetShamsiStr(data, format);
        }

        public static string GetShamsiStr(this DateTime data, string format)
        {
            char[] delimiterChars = { ' ', '/', ':'};
            string[] words = format.Split(delimiterChars);
            string result = "";
            PersianCalendar persianCalendar = new PersianCalendar();
            int year = persianCalendar.GetYear(data);
            int month = persianCalendar.GetMonth(data);
            int day = persianCalendar.GetDayOfMonth(data);
            int hour = persianCalendar.GetHour(data);
            int minute = persianCalendar.GetMinute(data);
            int secound = persianCalendar.GetSecond(data);
            result = result + (words.Length > 0 ? year.ToString() : "");
            result = result + (words.Length > 1 ? "/" + month.ToString().PadLeft(words[1].Length, '0') : "");
            result = result + (words.Length > 2 ? "/" + day.ToString().PadLeft(words[2].Length, '0') : "");
            result = result + (words.Length > 3 ? " " + hour.ToString().PadLeft(words[3].Length, '0') : "");
            result = result + (words.Length > 4 ? ":" + minute.ToString().PadLeft(words[4].Length, '0') : "");
            result = result + (words.Length > 5 ? ":" + secound.ToString().PadLeft(words[5].Length, '0') : "");

            return result;
        }
        
        public static DateTime GetShamsi(this DateTime data)
        {
            PersianCalendar persianCalendar = new PersianCalendar();

            int year = persianCalendar.GetYear(data);
            int month = persianCalendar.GetMonth(data);
            int day = persianCalendar.GetDayOfMonth(data);
            int hour = persianCalendar.GetHour(data);
            int minute = persianCalendar.GetMinute(data);
            int secound = persianCalendar.GetSecond(data);
            DateTime persianDate = new DateTime(year,month,day,hour,minute,secound);
            return persianDate;
        }
        
        public static DateTime GetMiladi(this DateTime data)
        {
            DateTime georgianDateTime = new DateTime(data.Year, data.Month, data.Day,data.Hour, data.Minute,data.Second,new System.Globalization.PersianCalendar());
            return georgianDateTime;
        }
        
    }
}