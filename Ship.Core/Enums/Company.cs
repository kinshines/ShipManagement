using System;
using System.Collections.Generic;
using System.Text;

namespace Ship.Core.Enums
{
    /// <summary>
    /// 公司类型
    /// </summary>
    public enum CompanyType : byte
    {
        普通单位 = 0,
        中介公司 = 1
    }
    /// <summary>
    /// 公司性质
    /// </summary>
    public enum CompanyProperty : byte
    {
        民营企业 = 0,
        国有企业 = 1,
        外资企业 = 2,
        其他性质 = 3
    }
    /// <summary>
    /// 公司信誉等级
    /// </summary>
    public enum CompanyHonor : byte
    {
        优秀 = 0,
        良好 = 1,
        一般 = 2,
        初级 = 3,
        较差 = 4
    }
    /// <summary>
    /// 面试打分
    /// </summary>
    public enum InterviewScore : byte
    {
        优秀 = 0,
        良好 = 1,
        一般 = 2,
        差 = 3
    }
    /// <summary>
    /// 面试结论
    /// </summary>
    public enum InterviewConclusion : byte
    {
        通过 = 0,
        不通过 = 1
    }
    /// <summary>
    /// 劳保用品存入类型
    /// </summary>
    public enum LaborSupplyPutType : byte
    {
        购入 = 0,
        退回 = 1,
        原始库存 = 2
    }
    /// <summary>
    /// 劳保用品领取类型
    /// </summary>
    public enum LaborSupplyTakeType : byte
    {
        船员领取 = 0,
        公司领取 = 1
    }
}
