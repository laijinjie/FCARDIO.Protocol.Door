using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCARDIO.Core.Command;
using FCARDIO.Protocol.OnlineAccess;

namespace FCARDIO.Protocol.Door.FC8800.Watch
{
    /// <summary>
    /// 监控读卡信息
    /// </summary>
    public class ReadCardWatch : FC8800CommandEx
    {
        /// <summary>
        /// 初始化参数
        /// </summary>
        /// <param name="detail"></param>
        public ReadCardWatch(INCommandDetail detail) : base(detail, null) { }

        /// <summary>
        /// 没有实现
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 处理返回值
        /// </summary>
        /// <param name="oPck"></param>
        protected override void CommandNext1(OnlineAccessPacket oPck)
        {
            if (CheckResponse(oPck, 0x11))
            {
                var buf = oPck.CmdData;
                
                CommandCompleted();
            }
        }

        /// <summary>
        /// 创建一个通讯指令
        /// </summary>
        protected override void CreatePacket0()
        {
            Packet(0x19, 0x01, 0x00, 0x00, null);
        }
    }
}
