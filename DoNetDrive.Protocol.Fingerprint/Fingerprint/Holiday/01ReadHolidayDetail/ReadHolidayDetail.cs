using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoNetDrive.Core.Command;
using DoNetDrive.Protocol.OnlineAccess;
using DoNetDrive.Protocol.Door.Door8800;

namespace DoNetDrive.Protocol.Fingerprint.Holiday
{
    /// <summary>
    /// 从人脸机中读取节假日存储详情，命令成功后返回 ReadHolidayDetail_Result
    /// </summary> 
    public class ReadHolidayDetail : Door8800Command_ReadParameter
    {
        /// <summary>
        /// 构造从人脸机中读取节假日存储详情命令，无需其他参数
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        public ReadHolidayDetail(INCommandDetail cd) : base(cd, null)
        {
        }

        /// <summary>
        /// 将命令打包成一个Packet，准备发送
        /// </summary>
        protected override void CreatePacket0()
        {
            Packet(5, 1);
        }

        /// <summary>
        /// 命令返回值的判断
        /// </summary>
        /// <param name="oPck">包含返回指令的Packet</param>
        protected override void CommandNext1(OnlineAccessPacket oPck)
        {
            if (CheckResponse(oPck, 0x04))
            {
                var buf = oPck.CmdData;
                ReadHolidayDetail_Result rst = new ReadHolidayDetail_Result();
                _Result = rst;
                rst.SetBytes(buf);
                CommandCompleted();
            }
        }

        
    }
}
