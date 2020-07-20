using System;
using System.Globalization;
using System.Threading;

namespace DigoErp.Service.Extentions
{
    public static class DateExtentions
    {
        private static readonly CultureInfo cultureInfo;
        private static readonly string DateFormat = "dd/MM/yyyy";
        static DateExtentions()
        {
            cultureInfo = new CultureInfo("en");
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
        }
        public static DateTime StartOfMonth(this DateTime dateTime)
        {
            try
            {
                var startDate = new DateTime(dateTime.Year, dateTime.Month, 1);
                return startDate;
            }
            catch (Exception)
            {
                return dateTime;
            }
        }
        public static DateTime EndOfMonth(this DateTime dateTime)
        {
            try
            {
                var startDate = dateTime.StartOfMonth();
                var endDate = startDate.AddMonths(1).AddDays(-1);
                return endDate;
            }
            catch (Exception ex)
            {
                return dateTime;
            }
        }

        public static DateTime ParseStringDate(this string date)
        {
            DateTime dateTime = new DateTime();
            try
            {
                dateTime = DateTime.ParseExact(date, "dd-MM-yyyy", cultureInfo);
                return dateTime;
            }
            catch (Exception ex)
            {
                return dateTime;
            }
        }
    }
}
