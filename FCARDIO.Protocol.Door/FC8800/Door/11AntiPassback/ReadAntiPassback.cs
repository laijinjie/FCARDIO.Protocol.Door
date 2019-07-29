﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;
using FCARDIO.Core.Command;
using FCARDIO.Protocol.FC8800;
using FCARDIO.Protocol.OnlineAccess;

namespace FCARDIO.Protocol.Door.FC8800.Door.AntiPassback
{
    /// <summary>
    /// 读取防潜返
    /// 刷卡进门后，必须刷卡出门才能再次刷卡进门。
    /// 成功返回结果参考 ReadAntiPassback_Result
    /// </summary>
    public class ReadAntiPassback
        : FC8800Command_Read_DoorParameter
    {
        /// <summary>
        /// 初始化命令结构
        /// </summary>
        /// <param name="cd"></param>
        /// <param name="value">需要读取的门号结构</param>
        public ReadAntiPassback(INCommandDetail cd, DoorPort_Parameter value) : base(cd, value)
        {
        }
        
        /// <summary>
        /// 创建一个通讯指令
        /// </summary>
        protected override void CreatePacket0()
        {
            DoorPort_Parameter model = _Parameter as DoorPort_Parameter;
            Packet(0x03, 0xC, 0x00, 0x01, model.GetBytes(GetNewCmdDataBuf(model.GetDataLen())));
        }

        /// <summary>
        /// 处理返回值
        /// </summary>
        /// <param name="oPck"></param>
        protected override void CommandNext1(OnlineAccessPacket oPck)
        {
            if (CheckResponse(oPck, 0x02))
            {
                var buf = oPck.CmdData;
                AntiPassback_Result rst = new AntiPassback_Result();
                _Result = rst;
                rst.SetBytes(buf);
                CommandCompleted();
            }
        }

    }
}
