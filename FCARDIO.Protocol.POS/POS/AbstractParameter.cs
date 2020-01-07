﻿using FCARDIO.Core.Command;
using FCARDIO.Core.Data;

namespace FCARDIO.Protocol.POS
{
    /// <summary>
    /// 所有参数的基类，规定了一个基本数据检查流程
    /// </summary>
    public abstract class AbstractParameter : AbstractData, INCommandParameter
    {
        /// <summary>
        /// 检查参数的统一接口
        /// </summary>
        /// <returns></returns>
        public abstract bool checkedParameter();
        /// <summary>
        /// 释放资源
        /// </summary>
        public abstract void Dispose();
    }
}
