using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SailorDomain.Entities
{
    /// <summary>
    /// 性别
    /// </summary>
    public enum Gender : byte
    {
        男=0,
        女=1
    }
    /// <summary>
    /// 职务
    /// </summary>
    public enum Post : byte
    {
        船长=0,
        实习船长=1,
        大副=2,
        实习大副=3,
        二副=4,
        三副=5,
        轮机长 = 38,
        实习轮机长 = 39,
        大管轮 = 40,
        实习大管轮 = 41,
        二管轮 = 42,
        三管轮 = 43,
        电机员 = 44,
        木匠 = 45,
        水手长=46,
        一水 = 47,
        二水 = 48,
        实习二水 = 49,
        甲板实习生 = 51,
        甲板焊工=52,
        机工长 = 53,
        机舱焊工 = 54,
        一机 = 56,
        二机 = 57,
        实习二机=58,
        轮机实习生 = 61,
        电工=62,
        加油长=63,
        加油=64,
        抹油=65,
        大厨=67,
        二厨=68,
        服务生=71,
        其他=72
    }
    /// <summary>
    /// 英文水平
    /// </summary>
    public enum EnglishLevel : byte
    {
        四级=0,
        六级=1,
        八级=2,
        高中等级=3,
        初中等=4,
        初级水平=5
    }
    /// <summary>
    /// 婚姻状况
    /// </summary>
    public enum Marital : byte
    {
        未婚=0,
        已婚=1
    }
    /// <summary>
    /// 学历
    /// </summary>
    public enum EducationDegree : byte
    {
        本科=0,
        大专=1,
        中专=2,
        中技=3,
        高中=4,
        初中=5,
        硕士=6,
        博士=7,
        其他=8
    }
    /// <summary>
    /// 民族
    /// </summary>
    public enum Ethnic : byte
    {
        汉族=0,
        壮族=1,
        满族=2,
        回族=3,
        苗族=4,
        维吾尔族=5,
        土家族=6,
        彝族=7,
        蒙古族=8,
        藏族=9,
        布依族=10,
        侗族=11,
        瑶族=12,
        朝鲜族=13,
        白族=14,
        哈尼族=15,
        哈萨克族=16,
        黎族=17,
        傣族=18,
        畲族=19,
        傈僳族=20,
        仡佬族=21,
        东乡族=22,
        高山族=23,
        拉祜族=24,
        水族=25,
        佤族=26,
        纳西族=27,
        羌族=28,
        土族=29,
        仫佬族=30,
        锡伯族=31,
        柯尔克孜族=32,
        达斡尔族=33,
        景颇族=34,
        毛南族=35,
        撒拉族=36,
        布朗族=37,
        塔吉克族=38,
        阿昌族=39,
        普米族=40,
        鄂温克族=41,
        怒族=42,
        京族=43,
        基诺族=44,
        德昂族=45,
        保安族=46,
        俄罗斯族=47,
        裕固族=48,
        乌孜别克族=49,
        门巴族=50,
        鄂伦春族=51,
        独龙族=52,
        塔塔尔族=53,
        赫哲族=54,
        珞巴族=55
    }
    /// <summary>
    /// 船员来源
    /// </summary>
    public enum SailorSource : byte
    {
        自由船员=0,
        自有船员=1,
        借调船员=2,
        中介公司=3,
        特殊船员=4,
        其他来源=5
    }
    /// <summary>
    /// 公司类型
    /// </summary>
    public enum CompanyType : byte
    {
        普通单位=0,
        中介公司=1
    }
    /// <summary>
    /// 公司性质
    /// </summary>
    public enum CompanyProperty : byte
    {
        民营企业=0,
        国有企业=1,
        外资企业=2,
        其他性质=3
    }
    /// <summary>
    /// 公司信誉等级
    /// </summary>
    public enum CompanyHonor : byte
    {
        优秀=0,
        良好=1,
        一般=2,
        初级=3,
        较差=4
    }
    /// <summary>
    /// 面试打分
    /// </summary>
    public enum InterviewScore : byte
    {
        优秀=0,
        良好=1,
        一般=2,
        差=3
    }
    /// <summary>
    /// 面试结论
    /// </summary>
    public enum InterviewConclusion : byte
    {
        通过=0,
        不通过=1
    }
    /// <summary>
    /// 劳保用品存入类型
    /// </summary>
    public enum LaborSupplyPutType:byte 
    {
        购入=0,
        退回=1,
        原始库存=2
    }
    /// <summary>
    /// 劳保用品领取类型
    /// </summary>
    public enum LaborSupplyTakeType : byte
    {
        船员领取=0,
        公司领取=1
    }
    /// <summary>
    /// 船员状态
    /// </summary>
    public enum SailorStatus : byte
    {
        休假=0,
        在船=1,
        待派=2,
        培训=3,
        不跟踪=4
    }
    /// <summary>
    /// 通知来源
    /// </summary>
    public enum NoticeSource : byte
    {
        Contract=0,
        Certificate=1,
        VesselCertificate=2
    }
    /// <summary>
    /// 通知来源中文
    /// </summary>
    public enum NoticeSourceChinese : byte
    {
        合同 = 0,
        船员证书 = 1,
        船舶证书 = 2
    }
    /// <summary>
    /// 消息处理方式
    /// </summary>
    public enum NoticeHandleType :byte
    {
        关闭提醒=0,
        推迟提醒=1
    }
    /// <summary>
    /// 船舶收费项
    /// </summary>
    public enum VesselFeeItem : byte
    {
        船员花销=0,
        物料=1,
        备件=2,
        维修保养=3,
        滑油=4,
        日常花销=5,
        通讯费=6,
        保险费=7,
        检验发证=9,
        其他费用=8,
    }
    /// <summary>
    /// 血型
    /// </summary>
    public enum Blood : byte
    {
        A=0,
        B=1,
        AB=2,
        O=3,
        不详=4
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
    /// <summary>
    /// 船舶类型
    /// </summary>
    public enum VesselType : byte 
    {
        散货船 = 9,
        集装箱船 = 4,
        杂货船 = 14,
        驳船=0,
        工程船=1,
        滚装船=2,
        化学品船=3,
        舰艇=5,
        客船=6,
        冷藏船=7,
        木材船=8,
        拖船=10,
        液化气船=11,
        油轮=12,
        运沙船=13,
        其他=15
    }
    /// <summary>
    /// 船舶等级
    /// </summary>
    public enum VesselDegree : byte
    {
        一等=1,
        二等=2,
        三等=3,
        其他=0
    }
    /// <summary>
    /// 船舶部门
    /// </summary>
    public enum VesselDepartment : byte
    {
        甲板部门=1,
        轮机部门=2,
        海上无线电部门=3,
        其他=0
    }

    public enum VesselAccountSide : byte
    {
        Cost,
        Deposit
    }
    /// <summary>
    /// 证书类别
    /// </summary>
    public enum CertificateCategory : byte
    {
        船员证书,
        船舶证书
    }
    /// <summary>
    /// 船舶管理类型
    /// </summary>
    public enum VesselManageType:byte
    { 
        派员船舶=0,
        管理船舶=1
    }
}