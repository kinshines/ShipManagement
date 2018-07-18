using System;
using System.Collections.Generic;
using System.Text;

namespace Ship.Core.Enums
{
    /// <summary>
    /// 通知来源
    /// </summary>
    public enum NoticeSource : byte
    {
        Contract = 0,
        Certificate = 1,
        VesselCertificate = 2
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
    public enum NoticeHandleType : byte
    {
        关闭提醒 = 0,
        推迟提醒 = 1
    }
}
