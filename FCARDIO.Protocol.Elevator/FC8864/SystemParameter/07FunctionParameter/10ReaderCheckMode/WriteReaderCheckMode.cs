using FCARDIO.Core.Command;
using FCARDIO.Protocol.FC8800;
using FCARDIO.Protocol.OnlineAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Elevator.FC8864.SystemParameter.FunctionParameter
{
    /// <summary>
    /// 设置读卡器数据校验
    /// </summary>
    public class WriteReaderCheckMode : Write_Command
    {
        /// <summary>
        /// 设置读卡器数据校验 初始化命令
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        /// <param name="par">包含读卡器数据校验</param>
        public WriteReaderCheckMode(INCommandDetail cd, WriteReaderCheckMode_Parameter par) : base(cd, par) { }

        /// <summary>
        /// 检查命令参数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            WriteReaderCheckMode_Parameter model = value as WriteReaderCheckMode_Parameter;
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
            WriteReaderCheckMode_Parameter model = _Parameter as WriteReaderCheckMode_Parameter;
            Packet(0x41, 0x0A, 0x09, Convert.ToUInt32(model.GetDataLen()), model.GetBytes(GetNewCmdDataBuf(model.GetDataLen())));
        }
    }
}