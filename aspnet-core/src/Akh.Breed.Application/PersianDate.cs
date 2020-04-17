using System;
using System.Globalization;
using Abp.Timing;

namespace Akh.Breed
{
    public static class PersianDate
    {
        public static DateTime? GetShamsi(this DateTime? data)
        {
            DateTime? dt = GetShamsi(data.Value); 
            return dt;
        }
        
        public static DateTime? GetMiladi(this DateTime? data)
        {
            DateTime? dt = GetMiladi(data.Value); 
            return dt;
        }
        public static DateTime GetShamsi(this DateTime data)
        {
            if (CultureInfo.CurrentUICulture.Name == "fa")
            {
                return new DateTime(1000,10,10);
            }
            return new DateTime(3000,10,10);
            PersianCalendar persianCalendar = new PersianCalendar();

            int year = persianCalendar.GetYear(data);
            int month = persianCalendar.GetMonth(data);
            int day = persianCalendar.GetDayOfMonth(data);
            DateTime persianDate = new DateTime(year,month,day,12,0,0);
            return persianDate;
        }
        
        public static DateTime GetMiladi(this DateTime data)
        {
            DateTime georgianDateTime = new DateTime(data.Year, data.Month, data.Day,12,0,0,new System.Globalization.PersianCalendar());
            return georgianDateTime;
        }
        
    }
}