using System;
using System.Collections.Generic;
using System.Text;

namespace Ship.Core.Enums
{
    /// <summary>
    /// 性别
    /// </summary>
    public enum Gender : byte
    {
        男 = 0,
        女 = 1
    }
    /// <summary>
    /// 职务
    /// </summary>
    public enum Post : byte
    {
        船长 = 0,
        实习船长 = 1,
        大副 = 2,
        实习大副 = 3,
        二副 = 4,
        三副 = 5,
        轮机长 = 38,
        实习轮机长 = 39,
        大管轮 = 40,
        实习大管轮 = 41,
        二管轮 = 42,
        三管轮 = 43,
        电机员 = 44,
        木匠 = 45,
        水手长 = 46,
        一水 = 47,
        二水 = 48,
        实习二水 = 49,
        甲板实习生 = 51,
        甲板焊工 = 52,
        机工长 = 53,
        机舱焊工 = 54,
        一机 = 56,
        二机 = 57,
        实习二机 = 58,
        轮机实习生 = 61,
        电工 = 62,
        加油长 = 63,
        加油 = 64,
        抹油 = 65,
        大厨 = 67,
        二厨 = 68,
        服务生 = 71,
        其他 = 72
    }
    /// <summary>
    /// 英文水平
    /// </summary>
    public enum EnglishLevel : byte
    {
        四级 = 0,
        六级 = 1,
        八级 = 2,
        高中等级 = 3,
        初中等 = 4,
        初级水平 = 5
    }
    /// <summary>
    /// 婚姻状况
    /// </summary>
    public enum Marital : byte
    {
        未婚 = 0,
        已婚 = 1
    }
    /// <summary>
    /// 学历
    /// </summary>
    public enum EducationDegree : byte
    {
        本科 = 0,
        大专 = 1,
        中专 = 2,
        中技 = 3,
        高中 = 4,
        初中 = 5,
        硕士 = 6,
        博士 = 7,
        其他 = 8
    }
    /// <summary>
    /// 民族
    /// </summary>
    public enum Ethnic : byte
    {
        汉族 = 0,
        壮族 = 1,
        满族 = 2,
        回族 = 3,
        苗族 = 4,
        维吾尔族 = 5,
        土家族 = 6,
        彝族 = 7,
        蒙古族 = 8,
        藏族 = 9,
        布依族 = 10,
        侗族 = 11,
        瑶族 = 12,
        朝鲜族 = 13,
        白族 = 14,
        哈尼族 = 15,
        哈萨克族 = 16,
        黎族 = 17,
        傣族 = 18,
        畲族 = 19,
        傈僳族 = 20,
        仡佬族 = 21,
        东乡族 = 22,
        高山族 = 23,
        拉祜族 = 24,
        水族 = 25,
        佤族 = 26,
        纳西族 = 27,
        羌族 = 28,
        土族 = 29,
        仫佬族 = 30,
        锡伯族 = 31,
        柯尔克孜族 = 32,
        达斡尔族 = 33,
        景颇族 = 34,
        毛南族 = 35,
        撒拉族 = 36,
        布朗族 = 37,
        塔吉克族 = 38,
        阿昌族 = 39,
        普米族 = 40,
        鄂温克族 = 41,
        怒族 = 42,
        京族 = 43,
        基诺族 = 44,
        德昂族 = 45,
        保安族 = 46,
        俄罗斯族 = 47,
        裕固族 = 48,
        乌孜别克族 = 49,
        门巴族 = 50,
        鄂伦春族 = 51,
        独龙族 = 52,
        塔塔尔族 = 53,
        赫哲族 = 54,
        珞巴族 = 55
    }
    /// <summary>
    /// 船员来源
    /// </summary>
    public enum SailorSource : byte
    {
        自由船员 = 0,
        自有船员 = 1,
        借调船员 = 2,
        中介公司 = 3,
        特殊船员 = 4,
        其他来源 = 5
    }

    /// <summary>
    /// 船员状态
    /// </summary>
    public enum SailorStatus : byte
    {
        休假 = 0,
        在船 = 1,
        待派 = 2,
        培训 = 3,
        不跟踪 = 4
    }

    /// <summary>
    /// 血型
    /// </summary>
    public enum Blood : byte
    {
        A = 0,
        B = 1,
        AB = 2,
        O = 3,
        不详 = 4
    }
    /// <summary>
    /// 证书等级
    /// </summary>
    public enum CertificateDegree : byte
    {
        甲类,
        乙一,
        乙二,
        丙一,
        丙二,
        丁类,
        长江一,
        长江二,
        内河,
        其他
    }
}
