using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopping.Utilities
{
    public static class DateTimeUtilities
    {
        private const string _isoFormatter = "yyyy-MM-ddTHH:mm:ss.FFFFFFFZ";

        public static string ToIsoFormat(this DateTime dateTime)
        {
            return dateTime.ToString(_isoFormatter);
        }


        public static string _convertString(this DateTime datetime)
        {
            return datetime.ToString("dd/MM/yyyy");
        }
        public static string ConvertDateToString(this DateTime? datetime)
        {
            return datetime.HasValue ? _convertString(datetime.Value) : string.Empty;
        }
        public static string ConvertDateToString(this DateTime datetime)
        {
            var dt = _convertString(datetime);
            return dt;
        }

        public static string TryParseDateAndFormat(this string dateStr)
        {
            if (DateTime.TryParse(dateStr, out var dateValue))
            {
                return dateValue.ConvertDateToString();
            }

            return dateStr;
        }

        public static string ToLocalYearMonthDay(this DateTime? dateTime)
        {
            if (dateTime is null) return string.Empty;
            return $"{dateTime.Value.ToLocalTime().Year}-{(dateTime.Value.ToLocalTime().Month < 10 ? "0" : string.Empty)}{dateTime.Value.ToLocalTime().Month}-{(dateTime.Value.ToLocalTime().Day < 10 ? "0" : string.Empty)}{dateTime.Value.ToLocalTime().Day}"; ;
        }

        public static string ToLocalYearMonthDay(this DateTime dateTime)
        {
            return $"{dateTime.ToLocalTime().Year}-{(dateTime.ToLocalTime().Month < 10 ? "0" : string.Empty)}{dateTime.ToLocalTime().Month}-{(dateTime.ToLocalTime().Day < 10 ? "0" : string.Empty)}{dateTime.ToLocalTime().Day}"; ;
        }


        public static int ToUtcHour(this int localHour, int timeZoneOffset)
        {
            var today = DateTime.UtcNow.Date;
            var runHourDate = new DateTime(today.Year, today.Month, today.Day, localHour, 0, 0);
            var offsetDay = runHourDate.AddHours(timeZoneOffset);
            return offsetDay.Hour;
        }

        public static bool IsValidDate(this string dateString)
        {
            if (!string.IsNullOrEmpty(dateString))
            {
                return dateString.ConvertDate() != null;
            }
            return false;
        }

        public static bool IsFutureDate(this string dateString, bool allowToday = false)
        {
            var dt = dateString.ConvertDate();
            if (dt != null)
            {
                if (allowToday) return dt.Value.Date <= DateTime.Now.Date;
                else return dt.Value.Date >= DateTime.Now.Date;
            }
            return false;
        }

        public static DateTime? ConvertDate(this string dateString)
        {
            if (!string.IsNullOrEmpty(dateString))
            {

                /* TRY TO PARSE */
                if (DateTime.TryParse(dateString, out DateTime parsedDateTime))
                {
                    return parsedDateTime.ToUniversalTime();

                    /*                    new DateTime(
                                          year: parsedDateTime.Year,
                                          month: parsedDateTime.Month,
                                          day: parsedDateTime.Day,
                                          hour: parsedDateTime.Hour,
                                          minute: parsedDateTime.Minute,
                                          second: parsedDateTime.Second,
                                          millisecond: parsedDateTime.Millisecond,
                                          kind: DateTimeKind.Unspecified); */
                }
                else
                {
                    char splitChar = '/';
                    if (dateString.Contains('-')) splitChar = '-';
                    if (dateString.Contains('.')) splitChar = '.';

                    string[] arr = dateString.Split(splitChar);

                    if (arr.Length == 3)
                    {
                        string datePart = arr[0], monthPart = arr[1], yearPart = arr[2];
                        if (datePart.Length > 2 || monthPart.Length > 2 || yearPart.Length != 4) return null;

                        try
                        {
                            return new DateTime(
                                year: Convert.ToInt16(yearPart),
                                month: Convert.ToInt16(monthPart),
                                day: Convert.ToInt16(datePart)).ToUniversalTime();

                            // return new DateTime(Convert.ToInt16(yearPart), Convert.ToInt16(monthPart), Convert.ToInt16(datePart));
                        }
                        catch
                        {
                            return null;
                        }
                    }
                }
            }
            return null;
        }

        public static bool IsValidTime(this string timeString)
        {
            if (!string.IsNullOrEmpty(timeString))
            {
                return timeString.ConvertTime() != null;
            }
            return false;
        }

        public static DateTime? ConvertTime(this string dateString)
        {
            var arr = dateString.Split(':');
            if (arr.Length == 2)
            {
                try
                {
                    return DateTime.Parse(dateString, CultureInfo.CurrentCulture);
                }
                catch
                {
                    return null;
                }
            }
            return null;
        }

        public static string UTCToString(this DateTime dateTime)
        {
            //return dateTime.ToString("s") + $".{dateTime.Millisecond}Z";
            return dateTime.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'");
        }

        public static string ToUTCString(this DateTime? dateTime)
        {
            if (dateTime.HasValue is false) return string.Empty;
            //return dateTime.Value.ToString("s") + "Z";
            return dateTime?.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'");
        }

        public static int YearDifference(DateTime a, DateTime b)
        {
            DateTime zeroTime = new DateTime(1, 1, 1);
            TimeSpan span = b - a;
            return (zeroTime + span).Year - 1;
        }

        public static int MonthDifference(this DateTime lValue, DateTime rValue)
        {
            return Math.Abs((lValue.Month - rValue.Month) + 12 * (lValue.Year - rValue.Year));
        }

        public static DateTime GetNthWeekofMonth(DateTime date, int nthWeek, DayOfWeek dayOfWeek)
        {
            return date.Next(dayOfWeek).AddDays((nthWeek - 1) * 7);
        }

        public static DateTime Next(this DateTime date, DayOfWeek dayOfWeek)
        {
            return date.AddDays((dayOfWeek < date.DayOfWeek ? 7 : 0) + dayOfWeek - date.DayOfWeek);
        }

        public static string GetReadableDate(DateTime date = default, string format = "dd MMM, yyyy")
        {
            date = date == default ? DateTime.UtcNow.Date : date;
            return date.ToString(format);
        }
    }
}
