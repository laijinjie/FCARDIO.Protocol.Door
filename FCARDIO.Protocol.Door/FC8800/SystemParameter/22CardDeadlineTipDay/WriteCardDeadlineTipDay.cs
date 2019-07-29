﻿using FCARDIO.Core.Command;
using FCARDIO.Protocol.FC8800;
using FCARDIO.Protocol.OnlineAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.SystemParameter.CardDeadlineTipDay
{
    /// <summary>
    /// 设置有效期即将过期提醒时间
    /// </summary>
    public class WriteCardDeadlineTipDay : FC8800Command_WriteParameter
    {
        /// <summary>
        /// 设置有效期即将过期提醒时间
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        /// <param name="par">包含有效期即将过期提醒时间</param>
        public WriteCardDeadlineTipDay(INCommandDetail cd, INCommandParameter par) : base(cd, par) { }

        /// <summary>
        /// 检查命令参数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            WriteCardDeadlineTipDay_Parameter model = value as WriteCardDeadlineTipDay_Parameter;
            if (model == null)
            {
                return false;
            }

            return model.checkedParameter();
        }

        /// <summary>
        /// 拼装命令
        /// </summary>
        protected override void CreatePacket0()
        {
            WriteCardDeadlineTipDay_Parameter model = _Parameter as WriteCardDeadlineTipDay_Parameter;

            var acl = _Connector.GetByteBufAllocator();

            var buf = acl.Buffer(model.GetDataLen());

            Packet(0x01, 0x15, 0x00, Convert.ToUInt32(model.GetDataLen()), model.GetBytes(buf));
        }
    }
}