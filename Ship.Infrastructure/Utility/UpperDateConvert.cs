using System;
using System.Collections.Generic;
using System.Text;

namespace Ship.Infrastructure.Utility
{
    /// <summary>
    /// 日期转换为中文大写
    /// </summary>
    public class UpperDateConvert
    {
        //把数字转换为大写
        private static string numtoUpper(int num)
        {
            String str = num.ToString();
            string rstr = "";
            int n;
            for (int i = 0; i < str.Length; i++)
            {
                n = Convert.ToInt16(str[i].ToString());//char转数字,转换为字符串，再转数字
                switch (n)
                {
                    case 0: rstr = rstr + "〇"; break;
                    case 1: rstr = rstr + "一"; break;
                    case 2: rstr = rstr + "二"; break;
                    case 3: rstr = rstr + "三"; break;
                    case 4: rstr = rstr + "四"; break;
                    case 5: rstr = rstr + "五"; break;
                    case 6: rstr = rstr + "六"; break;
                    case 7: rstr = rstr + "七"; break;
                    case 8: rstr = rstr + "八"; break;
                    default: rstr = rstr + "九"; break;


                }

            }
            return rstr;
        }
        //月转化为大写
        private static string monthtoUpper(int month)
        {
            if (month < 10)
            {
                return numtoUpper(month);
            }
            else
                if (month == 10) { return "十"; }

            else
            {
                return "十" + numtoUpper(month - 10);
            }
        }
        //日转化为大写
        private static string daytoUpper(int day)
        {
            if (day < 20)
            {
                return monthtoUpper(day);
            }
            else
            {
                String str = day.ToString();
                if (str[1] == '0')
                {
                    return numtoUpper(Convert.ToInt16(str[0].ToString())) + "十";

                }


                else
                {
                    return numtoUpper(Convert.ToInt16(str[0].ToString())) + "十"
                        + numtoUpper(Convert.ToInt16(str[1].ToString()));
                }
            }
        }
        //日期转换为大写
        public static string dateToUpper(System.DateTime date)
        {
            int year = date.Year;
            int month = date.Month;
            int day = date.Day;
            return numtoUpper(year) + "年" + monthtoUpper(month) + "月" + daytoUpper(day) + "日";

        }
    }
}
