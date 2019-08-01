using FCARDIO.Core.Command;
using FCARDIO.Protocol.Door.FC8800;
using System;

namespace FCARDIO.Protocol.Fingerprint.SystemParameter.IP
{
    /// <summary>
    /// 设置IP参数
    /// </summary>
    public class WriteIP : FC8800Command_WriteParameter
    {
        /// <summary>
        /// 设置控制器TCP参数 初始化命令
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        /// <param name="par">包含TCP参数信息</param>
        public WriteIP(INCommandDetail cd, WriteIP_Parameter par) : base(cd, par) { }

        /// <summary>
        /// 将命令打包成一个Packet，准备发送
        /// </summary>
        protected override void CreatePacket0()
        {
            WriteIP_Parameter model = _Parameter as WriteIP_Parameter;

            var acl = _Connector.GetByteBufAllocator();

            var buf = acl.Buffer(model.GetDataLen());

            Packet(0x01, 0x06, 0x01, Convert.ToUInt32(model.GetDataLen()), model.GetBytes(buf));
        }

        /// <summary>
        /// 检查命令参数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            WriteIP_Parameter model = value as WriteIP_Parameter;
            if (model == null)
            {
                return false;
            }

            return model.checkedParameter();
        }
    }
}
