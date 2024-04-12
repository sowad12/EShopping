
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace EShopping.Utilities
{
    public static class StringUtilities
    {
        public static string RemoveSpecialChars(this string value)
        {
            string input = value;
            Regex regex = new Regex("[^a-zA-Z0-9_.]+");
            if (regex.IsMatch(value[value.Length - 1].ToString()))
            {
                input = value.Remove(value.Length - 1);
            }

            return Regex.Replace(input, "[^a-zA-Z0-9_]+", "_");
        }

        public static string ToUpperFirstLetter(this string str)
        {
            if (str == null)
            {
                return null;
            }

            if (str.Length > 1)
            {
                return char.ToUpper(str[0]) + str.Substring(1);
            }

            return str.ToUpper();
        }

        public static string Replace(this string str, char item, Func<char> character)
        {
            StringBuilder stringBuilder = new StringBuilder(str.Length);
            char[] array = str.ToCharArray();
            foreach (char c in array)
            {
                stringBuilder.Append((c == item) ? character() : c);
            }

            return stringBuilder.ToString();
        }

        public static string Numerify(this string numberString)
        {
            return numberString.Replace('#', () => new Random().Next(10).ToString().ToCharArray()[0]);
        }

        public static string Letterify(this string letterString)
        {
            return letterString.Replace('?', () => 'a'.To('z').Rand());
        }

        public static string Bothify(this string str)
        {
            return str.Numerify().Letterify();
        }

        public static IEnumerable<char> To(this char from, char to)
        {
            for (char i = from; i <= to; i = (char)(i + 1))
            {
                yield return i;
            }
        }

        public static bool HasValue(this string str)
        {
            str = ((str != null) ? str.Trim() : str);
            return !string.IsNullOrEmpty(str) && !string.IsNullOrWhiteSpace(str);
        }

        public static bool HasValidMaxLength(this string str, int constraintLength)
        {
            if (!str.HasValue())
            {
                return true;
            }

            return str.Length <= constraintLength;
        }

        public static bool IsEqual(string str1, string str2, bool ignoreCase = false)
        {
            str1 = (string.IsNullOrEmpty(str1) ? string.Empty : str1);
            str2 = (string.IsNullOrEmpty(str2) ? string.Empty : str2);
            return ignoreCase ? (str1.ToLower() == str2.ToLower()) : (str1 == str2);
        }

        public static bool IsStringIn(this string data, string[] arr, bool ignoreCase = false)
        {
            if (string.IsNullOrEmpty(data) || string.IsNullOrWhiteSpace(data))
            {
                return false;
            }

            return ignoreCase ? arr.Any((string x) => x.ToLower() == data.ToLower()) : arr.Any((string x) => x == data);
        }

        public static string SafeTrim(this string data)
        {
            return (data == null) ? data : data.Trim();
        }

        public static string ConcatWithSpace(params string[] items)
        {
            return items.Where((string x) => x.HasValue()).ToList().Join(" ");
        }

        public static bool CanBeSkipped(this string data)
        {
            string[] source = new string[1] { "+61111113333" };
            return data.HasValue() && source.Any((string x) => x.Equals(data));
        }

        public static bool IsAllNumeric(this string str)
        {
            if (str == null)
            {
                str = string.Empty;
            }

            return str.All((char c) => char.IsNumber(c) || c == '+');
        }

        public static string CapitalizeFirstLetter(this string str)
        {
            str = new CultureInfo("en-US").TextInfo.ToTitleCase(str);
            return str;
        }

        public static bool IsValidEmail(this string email)
        {
            if (!email.HasValue())
            {
                return true;
            }

            bool item = false;
            try
            {
                MailAddress mailAddress = new MailAddress(email);
                item = mailAddress.Address == email;
            }
            catch (Exception)
            {
            }

            List<bool> source = new List<bool>
            {
                item,
                Regex.IsMatch(email, "\\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\\Z", RegexOptions.IgnoreCase),
                Regex.IsMatch(email, "^([a-zA-Z0-9_\\-\\.]+)@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.)|(([a-zA-Z0-9\\-]+\\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\\]?)$", RegexOptions.IgnoreCase)
            };
            return source.All((bool x) => x);
        }

        public static bool IsValidLength(this string name, int limit)
        {
            return name.Length <= limit;
        }

        public static bool IsValidMinLength(this string name, int limit)
        {
            return name.Length >= limit;
        }

        public static bool IsValidClubName(this string name)
        {
            if (!name.HasValue())
            {
                return true;
            }

            Regex regex = new Regex("^[a-zA-Z0-9'_ -.,]*$");
            Match match = regex.Match(name);
            return match.Success;
        }

        public static bool IsValidName(this string name)
        {
            if (!name.HasValue())
            {
                return true;
            }

            return !new Regex("^\\d+$").Match(name).Success;
        }

        public static string GetActualStringFromBase64(this string base64)
        {
            if (!base64.HasValue())
            {
                return base64;
            }

            base64 = base64.Replace("data:image/png;base64,", "");
            base64 = base64.Replace("data:image/jpg;base64,", "");
            base64 = base64.Replace("data:image/jpeg;base64,", "");
            return base64;
        }

        public static List<string> SplitString(this string str, string seperator = ",")
        {
            if (!str.HasValue())
            {
                return new List<string>();
            }

            return (from x in str.Split(seperator, StringSplitOptions.TrimEntries)
                    where x.HasValue()
                    select x).ToList();
        }

        public static string GenerateFixedDigitString(this string input, string addChar, int digit, int position = 0)
        {
            if (!input.HasValue()) return string.Empty;
            var result = input.Trim();
            for (int i = result.Length; i < digit; i++)
            {
                if (position == 0)
                    result = result + addChar;
                else
                    result = addChar + result;
            }
            return result;
        }


        public static string StringMasking(this string input, char maskChar, int percentToApply, MaskOption maskOptions)
        {
            if (!input.HasValue()) return string.Empty;
            var result = input.Trim();

            if (result.Length == 0 || percentToApply < 1) return result;
            if (percentToApply >= 100) return new string(maskChar, result.Length);

            var maskLength = Math.Max((int)Math.Round(percentToApply * result.Length / 100m), 1);
            var mask = new string(maskChar, maskLength);

            switch (maskOptions)
            {
                case MaskOption.AtTheBeginingOfString:
                    result = mask + result.Substring(maskLength);
                    break;
                case MaskOption.AtTheEndOfString:
                    result = result.Substring(0, result.Length - maskLength) + mask;
                    break;
                case MaskOption.InTheMiddleOfString:
                    var maskStart = (result.Length - maskLength) / 2;
                    result = result.Substring(0, maskStart) + mask +
                        result.Substring(maskStart + maskLength);
                    break;
            }

            return result;
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
    public enum MaskOption : byte
    {
        AtTheBeginingOfString = 1,
        InTheMiddleOfString = 2,
        AtTheEndOfString = 3
    }
}
