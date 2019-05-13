using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;
using FCARDIO.Core.Command;
using FCARDIO.Protocol.FC8800;
using FCARDIO.Protocol.OnlineAccess;

namespace FCARDIO.Protocol.Door.FC8800.Door.InvalidCardAlarmOption
{
    /// <summary>
    /// 设置非法读卡报警参数
    /// </summary>
    public class WriteInvalidCardAlarmOption : FC8800Command_WriteParameter
    {
        /// <summary>
        /// 设置非法读卡报警参数
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        /// <param name="par">包含非法读卡报警参数</param>
        public WriteInvalidCardAlarmOption(INCommandDetail cd, WriteInvalidCardAlarmOption_Parameter par) : base(cd, par) { }


        /// <summary>
        /// 检查命令参数
        /// </summary>
        /// <param name="value"></param>
        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            WriteInvalidCardAlarmOption_Parameter model = value as WriteInvalidCardAlarmOption_Parameter;
            if (model == null) return false;
            return model.checkedParameter();
        }

        /// <summary>
        /// 将命令打包成一个Packet，准备发送
        /// </summary>
        protected override void CreatePacket0()
        {
            Packet(0x03, 0x0A, 0x00, 0x02, getCmdData());
        }

        /// <summary>
        /// 创建命令所需的命令数据<br/>
        /// 将命令打包到ByteBuffer中
        /// </summary>
        /// <returns>包含命令数据的ByteBuffer</returns>
        private IByteBuffer getCmdData()
        {
            WriteInvalidCardAlarmOption_Parameter model = _Parameter as WriteInvalidCardAlarmOption_Parameter;
            var acl = _Connector.GetByteBufAllocator();
            var buf = acl.Buffer(model.GetDataLen());
            model.GetBytes(buf);
            return buf;
        }
    }
}
