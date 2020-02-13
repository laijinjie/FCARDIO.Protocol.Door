﻿using DotNetty.Buffers;
using FCARDIO.Protocol.Util;
using FCARDIO.Core.Data;
using System;
using FCARDIO.Core.Extension;

namespace FCARDIO.Protocol.Door.FC8800.TemplateMethod
{
    /// <summary>
    /// 模板抽象元素基类
    /// </summary>
    public abstract class TemplateData_Base : AbstractData
    {

        /// <summary>
        /// 写入 要删除的元素信息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public abstract IByteBuffer GetDeleteBytes(IByteBuffer data);

        /// <summary>
        /// 获取每个要删除元素的长度
        /// </summary>
        /// <returns></returns>
        public abstract int GetDeleteDataLen();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buf"></param>
        public abstract void SetFailBytes(IByteBuffer buf);

    }
}
