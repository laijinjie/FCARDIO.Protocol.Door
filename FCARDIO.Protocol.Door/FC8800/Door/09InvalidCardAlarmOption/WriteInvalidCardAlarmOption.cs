﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;
using FCARDIO.Core.Command;
using FCARDIO.Protocol.FC8800;
using FCARDIO.Protocol.OnlineAccess;

namespace FCARDIO.Protocol.Door.FC8800.Door.InvalidCardAlarmOption
{
    /// <summary>
    /// 写入未注册卡读卡时报警功能
    /// </summary>
    public class WriteInvalidCardAlarmOption
        : FC8800Command
    {
        /// <summary>
        /// 初始化命令结构
        /// </summary>
        /// <param name="cd"></param>
        /// <param name="parameter">包含门号和报警功能开关的结构</param>
        public WriteInvalidCardAlarmOption(INCommandDetail cd, WriteInvalidCardAlarmOption_Parameter parameter) : base(cd, parameter) { }
        

        /// <summary>
        /// 检查参数
        /// </summary>
        /// <param name="value"></param>
        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            WriteInvalidCardAlarmOption_Parameter model = value as WriteInvalidCardAlarmOption_Parameter;
            if (model == null) return false;
            return model.checkedParameter();
        }
        
        /// <summary>
        /// 创建一个通讯指令
        /// </summary>
        protected override void CreatePacket0()
        {
            Packet(0x03, 0x0A, 0x00, 0x02, getCmdData());
        }

        /// <summary>
        /// 获取参数结构的字节编码
        /// </summary>
        private IByteBuffer getCmdData()
        {
            WriteInvalidCardAlarmOption_Parameter model = _Parameter as WriteInvalidCardAlarmOption_Parameter;
            var acl = _Connector.GetByteBufAllocator();
            var buf = acl.Buffer(model.GetDataLen());
            model.GetBytes(buf);
            return buf;
        }

        /// <summary>
        /// 处理返回值
        /// </summary>
        /// <param name="oPck"></param>
        protected override void CommandNext1(OnlineAccessPacket oPck)
        {
            return;
        }

        /// <summary>
        /// 命令重发时需要处理的函数
        /// </summary>
        protected override void CommandReSend()
        {
            return;
        }
        
        /// <summary>
        /// 命令释放时需要处理的函数
        /// </summary>
        protected override void Release1()
        {
            return;
        }
    }
}
