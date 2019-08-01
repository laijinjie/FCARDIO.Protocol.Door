using FCARDIO.Core.Command;
using FCARDIO.Protocol.Door.FC8800;
using System;

namespace FCARDIO.Protocol.Fingerprint.SystemParameter.RecordMode
{
    /// <summary>
    /// 设置记录存储方式
    /// </summary>
    public class WriteRecordMode : FC8800Command_WriteParameter
    {
        /// <summary>
        /// 初始化命令
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        /// <param name="par"></param>
        public WriteRecordMode(INCommandDetail cd, WriteRecordMode_Parameter par) : base(cd, par) { }

        /// <summary>
        /// 检查命令参数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            WriteRecordMode_Parameter model = value as WriteRecordMode_Parameter;
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
            WriteRecordMode_Parameter model = _Parameter as WriteRecordMode_Parameter;
            Packet(0x01, 0x0A, 0x01, Convert.ToUInt32(model.GetDataLen()), model.GetBytes(GetNewCmdDataBuf(model.GetDataLen())));
        }
    }
}
