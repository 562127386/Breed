using System;
using System.Globalization;
using Abp.Timing;

namespace Akh.Breed
{
    public static class PersianDate
    {
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