using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Linq;

namespace Sanita.Utility
{
    public class UtilityConvertMoney
    {
        private static string[] strSo = { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
        private static string[] strDonViNho = { "linh", "lăm", "mười", "mươi", "mốt", "trăm" };
        private static string[] strDonViLon = { "", "nghìn", "triệu", "tỷ" };
        private static string[] strMainGroup;
        private static string[] strSubGroup;
        private static string Len1(string strA)
        {
            return strSo[int.Parse(strA)];
        }
        private static string Len2(string strA)
        {
            if (strA.Substring(0, 1) == "0")
            {
                return strDonViNho[0] + " " + Len1(strA.Substring(1, 1));
            }
            else if (strA.Substring(0, 1) == "1")
            {
                if (strA.Substring(1, 1) == "5")
                {
                    return strDonViNho[2] + " " + strDonViNho[1];
                }
                else if (strA.Substring(1, 1) == "0")
                {
                    return strDonViNho[2];
                }
                else
                {
                    return strDonViNho[2] + " " + Len1(strA.Substring(1, 1));
                }
            }
            else
            {
                if (strA.Substring(1, 1) == "5")
                {
                    return Len1(strA.Substring(0, 1)) + " " + strDonViNho[3] + " " + strDonViNho[1];
                }
                else if (strA.Substring(1, 1) == "0")
                {
                    return Len1(strA.Substring(0, 1)) + " " + strDonViNho[3];
                }
                else if (strA.Substring(1, 1) == "1")
                {
                    return Len1(strA.Substring(0, 1)) + " " + strDonViNho[3] + " " + strDonViNho[4];
                }
                else
                {
                    return Len1(strA.Substring(0, 1)) + " " + strDonViNho[3] + " " + Len1(strA.Substring(1, 1));
                }
            }
        }
        private static string Len3(string strA)
        {
            if ((strA.Substring(0, 3) == "000"))
            {
                return null;
            }
            else if ((strA.Substring(1, 2) == "00"))
            {
                return Len1(strA.Substring(0, 1)) + " " + strDonViNho[5];
            }
            else
            {
                return Len1(strA.Substring(0, 1)) + " " + strDonViNho[5] + " " + Len2(strA.Substring(1, strA.Length - 1));
            }
        }
        /////////////////////
        private static string FullLen(string strSend)
        {
            bool boKTNull = false;
            string strKQ = "";
            string strA = strSend.Trim();
            int iIndex = strA.Length - 9;
            int iPreIndex = 0;

            if (strSend.Trim() == "")
            {
                return Len1("0");
            }
            //tra ve khong neu la khong
            for (int i = 0; i < strA.Length; i++)
            {
                if (strA.Substring(i, 1) != "0")
                {
                    break;
                }
                else if (i == strA.Length - 1)
                {
                    return strSo[0];
                }
            }
            int k = 0;
            while (strSend.Trim().Substring(k++, 1) == "0")
            {
                strA = strA.Remove(0, 1);
            }
            //
            if (strA.Length < 9)
            {
                iPreIndex = strA.Length;
            }
            //
            if ((strA.Length % 9) != 0)
            {
                strMainGroup = new string[strA.Length / 9 + 1];
            }
            else
            {
                strMainGroup = new string[strA.Length / 9];
            }
            //nguoc
            for (int i = strMainGroup.Length - 1; i >= 0; i--)
            {
                if (iIndex >= 0)
                {
                    iPreIndex = iIndex;
                    strMainGroup[i] = strA.Substring(iIndex, 9);
                    iIndex -= 9;
                }
                else
                {
                    strMainGroup[i] = strA.Substring(0, iPreIndex);
                }

            }
            /////////////////////////////////
            //tach moi maingroup thanh nhieu subgroup
            //xuoi
            for (int j = 0; j < strMainGroup.Length; j++)
            {
                //gan lai gia tri
                iIndex = strMainGroup[j].Length - 3;
                if (strMainGroup[j].Length < 3)
                {
                    iPreIndex = strMainGroup[j].Length;
                }
                ///
                if ((strMainGroup[j].Length % 3) != 0)
                {
                    strSubGroup = new string[strMainGroup[j].Length / 3 + 1];
                }
                else
                {
                    strSubGroup = new string[strMainGroup[j].Length / 3];
                }
                for (int i = strSubGroup.Length - 1; i >= 0; i--)
                {
                    if (iIndex >= 0)
                    {
                        iPreIndex = iIndex;
                        strSubGroup[i] = strMainGroup[j].Substring(iIndex, 3);
                        iIndex -= 3;
                    }
                    else
                    {
                        strSubGroup[i] = strMainGroup[j].Substring(0, iPreIndex);
                    }
                }
                //duyet subgroup de lay string
                for (int i = 0; i < strSubGroup.Length; i++)
                {
                    boKTNull = false;//phai de o day
                    if ((j == strMainGroup.Length - 1) && (i == strSubGroup.Length - 1))
                    {
                        if (strSubGroup[i].Length < 3)
                        {
                            if (strSubGroup[i].Length == 1)
                            {
                                strKQ += Len1(strSubGroup[i]);
                            }
                            else
                            {
                                strKQ += Len2(strSubGroup[i]);
                            }
                        }
                        else
                        {
                            strKQ += Len3(strSubGroup[i]);
                        }
                    }
                    else
                    {
                        if (strSubGroup[i].Length < 3)
                        {
                            if (strSubGroup[i].Length == 1)
                            {
                                strKQ += Len1(strSubGroup[i]) + " ";
                            }
                            else
                            {
                                strKQ += Len2(strSubGroup[i]) + " ";
                            }
                        }
                        else
                        {
                            if (Len3(strSubGroup[i]) == null)
                            {
                                boKTNull = true;
                            }
                            else
                            {
                                strKQ += Len3(strSubGroup[i]) + " ";
                            }
                        }
                    }
                    //dung
                    if (!boKTNull)
                    {
                        if (strSubGroup.Length - 1 - i != 0)
                        {
                            strKQ += strDonViLon[strSubGroup.Length - 1 - i] + " ";
                        }
                        else
                        {
                            strKQ += strDonViLon[strSubGroup.Length - 1 - i] + " ";
                        }

                    }
                }
                //dung
                if (j != strMainGroup.Length - 1)
                {
                    if (!boKTNull)
                    {
                        strKQ = strKQ.Substring(0, strKQ.Length - 1) + strDonViLon[3] + " ";
                    }
                    else
                    {
                        strKQ = strKQ.Substring(0, strKQ.Length - 1) + " " + strDonViLon[3] + " ";
                    }
                }
            }
            //xoa ky tu trang
            strKQ = strKQ.Trim();
            //xoa dau , neu co
            if (strKQ.Substring(strKQ.Length - 1, 1) == ".")
            {
                strKQ = strKQ.Remove(strKQ.Length - 1, 1);
            }
            return strKQ;

            ////////////////////////////////////
        }

        public static string Convert(double db)
        {
            return Convert(db, "VN");
        }

        public static string Convert(double db, String language)
        {
#if false
            if (SystemInfo.CAM)
            {
                if (SystemInfo.MONEY_CODE == "VND")
                {
                    if (language == "EN")
                    {
                        return Convert_EN2(db);
                    }
                    if (db >= 0)
                    {
                        string ret = Convert(db.ToString(SystemInfo.MONEY_FORMAT, CultureInfo.InvariantCulture), ' ', "*phẩy");
                        if (ret.Length > 0)
                        {
                            ret = ret[0].ToString(CultureInfo.InvariantCulture).ToUpper() + ret.Remove(0, 1);
                        }

                        return ret;
                    }
                    else
                    {
                        return "";
                    }
                }
                else
                {
                    return "";
                }
            }
            else
#endif
            {
                if (language == "EN")
                {
                    return Convert_EN2(db);
                }
                if (db >= 0)
                {
                    string ret = Convert(db.ToString(SystemInfo.MONEY_FORMAT, CultureInfo.InvariantCulture), ' ', "*phẩy");
                    if (ret.Length > 0)
                    {
                        ret = ret[0].ToString(CultureInfo.InvariantCulture).ToUpper() + ret.Remove(0, 1);
                    }

                    return ret;
                }
                else
                {
                    return "";
                }
            }
        }

        #region ENGLISH
        static string[] ones = new string[] { "", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
        static string[] teens = new string[] { "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
        static string[] tens = new string[] { "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };
        static string[] thousandsGroups = { "", " thousand", " million", " billion" };

        private static string FriendlyInteger(int n, string leftDigits, int thousands)
        {
            if (n == 0)
            {
                return leftDigits;
            }
            string friendlyInt = leftDigits;
            if (friendlyInt.Length > 0)
            {
                if (tens.Contains(friendlyInt) && n < 10)
                {
                    friendlyInt += "-";
                }
                else
                {
                    friendlyInt += " ";
                }
            }

            if (n < 10)
            {
                friendlyInt += ones[n];
            }
            else if (n < 20)
            {
                friendlyInt += teens[n - 10];
            }
            else if (n < 100)
            {
                friendlyInt += FriendlyInteger(n % 10, tens[n / 10 - 2], 0);
            }
            else if (n < 1000)
            {
                friendlyInt += FriendlyInteger(n % 100, (ones[n / 100] + " hundred"), 0);
            }
            else
            {
                friendlyInt += FriendlyInteger(n % 1000, FriendlyInteger(n / 1000, "", thousands + 1), 0);
            }

            if (thousandsGroups[thousands] != "")
            {
                return friendlyInt + thousandsGroups[thousands] + ",";
            }
            else
            {
                return friendlyInt + thousandsGroups[thousands];
            }
        }

        public static string Convert_EN(double n)
        {
            if (n == 0)
            {
                return "Zero";
            }
            else if (n < 0)
            {
                return "Negative " + Convert_EN(-n);
            }

            double data = double.Parse(n.ToString(SystemInfo.MONEY_FORMAT, CultureInfo.InvariantCulture));
            String text = FriendlyInteger((int)(data), "", 0);
            String[] list_text = text.Split(' ');
            text = "";
            for (int i = 0; i < list_text.Length; i++)
            {
                if (i == list_text.Length - 1)
                {
                    if (text == "")
                    {
                        text = list_text[i];
                    }
                    else
                    {
                        text += " and " + list_text[i];
                    }
                }
                else
                {
                    if (text == "")
                    {
                        text = list_text[i];
                    }
                    else
                    {
                        text += " " + list_text[i];
                    }
                }
            }
            return text;
        }

        #endregion

        #region ENGLISH-2

        public static string Convert_EN2(double n)
        {
            double data = double.Parse(n.ToString(SystemInfo.MONEY_FORMAT, CultureInfo.InvariantCulture));
            return ConvertNumberToWord((long)(data));
        }

        public static string LevelNumberToWords(long number, string level = "")
        {
            string words = "";

            if ((number / 1000) > 0)
            {
                words += LevelNumberToWords(number / 1000, "") + " thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += LevelNumberToWords(number / 100, "") + " hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
                var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }


            if (level.Length > 0)
            {
                words += " " + level + ", ";
            }

            return words;
        }


        public static string ConvertNumberToWord(long number)
        {
            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + ConvertNumberToWord(Math.Abs(number));

            string words = "";

            if ((number / 1000000000) > 0)
            {
                words += LevelNumberToWords(number / 1000000000, "billion");
                number %= 1000000000;
            }

            if ((number / 1000000) > 0)
            {
                words += LevelNumberToWords(number / 1000000, "million");
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += LevelNumberToWords(number / 1000, "thousand");
                number %= 1000;
            }


            if ((number / 100) > 0)
            {
                words += ConvertNumberToWord(number / 100) + " hundred, ";
                number %= 100;
            }

            if (number > 0)
            {
                var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
                var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }

            ///////////////////            
            int index1 = words.LastIndexOf("  ");
            while (index1 > 0)
            {
                words = words.Remove(index1, 1);
                index1 = words.LastIndexOf("  ");
            }


            if (words.EndsWith(", "))
            {
                words = words.Remove(words.Length - 2, 2);
            }


            index1 = words.LastIndexOf(',');
            if (index1 > 0)
            {
                words = words.Remove(index1, 1);
                words = words.Insert(index1, " and");
            }


            return words;
        }

        #endregion

        public static string ConvertSound(double db)
        {
            if (db >= 0)
            {
                String ssss = db.ToString("0,0", CultureInfo.InvariantCulture);
                string ret = Convert(ssss, ' ', "*phẩy");

                if (ret.Length > 0)
                {
                    ret = ret[0].ToString(CultureInfo.InvariantCulture).ToUpper() + ret.Remove(0, 1);
                }

                return ret;
            }
            else
            {
                return "";
            }
        }

        public static string Convert(string strSend, char charInSeparator, string strOutSeparator)
        {
            if (strOutSeparator == "")
            {
                return "Lỗi dấu phân cách đầu ra rỗng";
            }
            if (strSend == "")
            {
                return Len1("0");
            }

            //Bo dau ','
            strSend = strSend.Replace(",", "");

            string[] strTmp = new string[2];
            try
            {

                strTmp = strSend.Split(charInSeparator);
                if (strTmp.Length == 2)
                {
                    string strTmpRight = strTmp[1];
                    for (int i = strTmpRight.Length - 1; i >= 0; i--)
                    {
                        if (strTmpRight.Substring(i, 1) == "0")
                        {
                            strTmpRight = strTmpRight.Remove(i, 1);
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (strTmpRight != "")
                    {
                        string strRight = "";
                        for (int i = 0; i < strTmpRight.Length; i++)
                        {
                            strRight += Len1(strTmpRight.Substring(i, 1)) + " ";
                        }


                        return FullLen(strTmp[0]) + " " + strOutSeparator + " " + strRight.TrimEnd();
                    }
                    else
                    {
                        return FullLen(strTmp[0]);
                    }
                }
                else
                {
                    return FullLen(strTmp[0]);
                }
            }
            catch
            {
                return FullLen(strTmp[0]);
            }

        }

    }
}
