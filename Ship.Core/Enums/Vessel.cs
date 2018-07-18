using System;
using System.Collections.Generic;
using System.Text;

namespace Ship.Core.Enums
{
    /// <summary>
    /// 船舶收费项
    /// </summary>
    public enum VesselFeeItem : byte
    {
        船员花销 = 0,
        物料 = 1,
        备件 = 2,
        维修保养 = 3,
        滑油 = 4,
        日常花销 = 5,
        通讯费 = 6,
        保险费 = 7,
        检验发证 = 9,
        其他费用 = 8,
    }
    /// <summary>
    /// 船舶类型
    /// </summary>
    public enum VesselType : byte
    {
        散货船 = 9,
        集装箱船 = 4,
        杂货船 = 14,
        驳船 = 0,
        工程船 = 1,
        滚装船 = 2,
        化学品船 = 3,
        舰艇 = 5,
        客船 = 6,
        冷藏船 = 7,
        木材船 = 8,
        拖船 = 10,
        液化气船 = 11,
        油轮 = 12,
        运沙船 = 13,
        其他 = 15
    }

    /// <summary>
    /// 船舶等级
    /// </summary>
    public enum VesselDegree : byte
    {
        一等 = 1,
        二等 = 2,
        三等 = 3,
        其他 = 0
    }
    /// <summary>
    /// 船舶部门
    /// </summary>
    public enum VesselDepartment : byte
    {
        甲板部门 = 1,
        轮机部门 = 2,
        海上无线电部门 = 3,
        其他 = 0
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
    public enum VesselManageType : byte
    {
        派员船舶 = 0,
        管理船舶 = 1
    }
}
