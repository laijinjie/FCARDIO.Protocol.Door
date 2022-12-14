using DotNetty.Buffers;
using DoNetDrive.Core.Command;
using DoNetDrive.Protocol.Door8800;
using DoNetDrive.Protocol.OnlineAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoNetDrive.Protocol.Door.Door8800.Door.RelayReleaseTime
{
    /// <summary>
    /// 获取开锁时输出时长
    /// </summary>
    public class ReadRelayReleaseTime : Door8800Command_Read_DoorParameter
    {
        /// <summary>
        /// 读取门工作方式
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        /// <param name="par">包含门端口</param>
        public ReadRelayReleaseTime(INCommandDetail cd, DoorPort_Parameter par) : base(cd, par) { }

       

        /// <summary>
        /// 命令返回值的判断
        /// </summary>
        /// <param name="oPck">包含返回指令的Packet</param>
        protected override void CommandNext1(OnlineAccessPacket oPck)
        {
            if (CheckResponse(oPck, 0x03))
            {
                var buf = oPck.CmdData;
                RelayReleaseTime_Result rst = new RelayReleaseTime_Result();
                _Result = rst;
                rst.SetBytes(buf);
                CommandCompleted();
            }
        }

        /// <summary>
        /// 将命令打包成一个Packet，准备发送
        /// </summary>
        protected override void CreatePacket0()
        {
            DoorPort_Parameter model = _Parameter as DoorPort_Parameter;
            Packet(0x03, 0x08, 0x00, 0x01, model.GetBytes(GetNewCmdDataBuf(model.GetDataLen())));
        }

       
    }
}
