using FCARDIO.Core.Command;
using FCARDIO.Protocol.FC8800;
using FCARDIO.Protocol.OnlineAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.SystemParameter.FunctionParameter
{
    /// <summary>
    /// 获取卡片到期提示
    /// </summary>
    public class ReadCardPeriodSpeak : FC8800Command_ReadParameter
    {
        /// <summary>
        /// 获取卡片到期提示 初始化命令
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        public ReadCardPeriodSpeak(INCommandDetail cd) : base(cd) {
            CmdType = 0x01;
            CmdIndex = 0x0A;
            CmdPar = 0x90;
        }

        /// <summary>
        /// 将命令打包成一个Packet，准备发送
        /// </summary>
        protected override void CreatePacket0()
        {
            Packet(CmdType, CmdIndex, CmdPar);
        }

        /// <summary>
        /// 命令返回值的判断
        /// </summary>
        /// <param name="oPck">包含返回指令的Packet</param>
        protected override void CommandNext1(OnlineAccessPacket oPck)
        {
            if (CheckResponse(oPck, 0x01))
            {
                var buf = oPck.CmdData;
                ReadCardPeriodSpeak_Result rst = new ReadCardPeriodSpeak_Result();
                _Result = rst;
                rst.SetBytes(buf);
                CommandCompleted();
            }
        }
    }
}