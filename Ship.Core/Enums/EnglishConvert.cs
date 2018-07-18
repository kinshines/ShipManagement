using System;
using System.Collections.Generic;
using System.Text;

namespace Ship.Core.Enums
{
    public class EnglishConvert
    {
        public static string EnglishPost(Post post)
        {
            switch (post)
            {
                case Post.船长:
                    return "CAPT";
                case Post.实习船长:
                    return "CAPT";
                case Post.大副:
                    return "C/O";
                case Post.实习大副:
                    return "C/O";
                case Post.二副:
                    return "2/O";
                case Post.三副:
                    return "3/O";
                case Post.轮机长:
                    return "C/E";
                case Post.实习轮机长:
                    return "C/E";
                case Post.大管轮:
                    return "2/E";
                case Post.实习大管轮:
                    return "2/E";
                case Post.二管轮:
                    return "3/E";
                case Post.三管轮:
                    return "4/E";
                case Post.水手长:
                    return "BSN";
                case Post.一水:
                    return "AB";
                case Post.二水:
                    return "OS";
                case Post.实习二水:
                    return "TRN.OS";
                case Post.甲板实习生:
                    return "D/C";
                case Post.机工长:
                    return "FTR";
                case Post.加油长:
                    return "NO.1";
                case Post.加油:
                    return "OIL";
                case Post.抹油:
                    return "WIP";
                case Post.轮机实习生:
                    return "E/C";
                case Post.大厨:
                    return "C/CK";
                case Post.一机:
                    return "OLR";
                case Post.二机:
                    return "WPR";
                case Post.实习二机:
                    return "TRN.WPR";
                case Post.服务生:
                    return "M/B";
                case Post.木匠:
                    return "CARP";
                case Post.电工:
                    return "E/E";
                case Post.电机员:
                    return "E/O";
                default:
                    return post.ToString();
            }
        }
        public static string EnglishMarital(Marital marital)
        {
            switch (marital)
            {
                case Marital.已婚:
                    return "Married";
                case Marital.未婚:
                    return "Single";
                default:
                    return marital.ToString();
            }
        }

        public static string EnglishVesselType(VesselType vesselType)
        {
            switch (vesselType)
            {
                case VesselType.散货船:
                    return "BULK";
                case VesselType.集装箱船:
                    return "CONT";
                case VesselType.杂货船:
                    return "G/C";
                default:
                    return vesselType.ToString();
            }
        }

        public static string EnglishDate(DateTime? datetime)
        {
            if (datetime.HasValue)
                return datetime.Value.ToString("MM-dd-yyyy");
            return "";
        }
    }
}
