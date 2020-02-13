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
    /// 设置读卡间隔时间
    /// </summary>
    public class WriteReaderIntervalTime : FC8800Command_WriteParameter
    {
        /// <summary>
        /// 设置读卡间隔时间 初始化命令
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        /// <param name="par">包含读卡间隔时间</param>
        public WriteReaderIntervalTime(INCommandDetail cd, WriteReaderIntervalTime_Parameter par) : base(cd, par) {
            CmdType = 0x01;
            CmdIndex = 0x0A;
            CmdPar = 0x07;
        }

        /// <summary>
        /// 检查命令参数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            WriteReaderIntervalTime_Parameter model = value as WriteReaderIntervalTime_Parameter;
            if (model == null)
            {
                return false;
            }
            return model.checkedParameter();
        }

        /// <summary>
        /// 将命令打包成一个Packet，准备发送
        /// </summary>
        protected override void CreatePacket0()
        {
            WriteReaderIntervalTime_Parameter model = _Parameter as WriteReaderIntervalTime_Parameter;

            var acl = _Connector.GetByteBufAllocator();

            var buf = acl.Buffer(model.GetDataLen());

            Packet(CmdType, CmdIndex, CmdPar, Convert.ToUInt32(model.GetDataLen()), model.GetBytes(buf));
        }
    }
}