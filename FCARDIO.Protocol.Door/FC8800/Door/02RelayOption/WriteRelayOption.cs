using DotNetty.Buffers;
using FCARDIO.Core.Command;
using FCARDIO.Protocol.FC8800;
using FCARDIO.Protocol.OnlineAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.Door.RelayOption
{
    public class WriteRelayOption : FC8800Command
    {
        public WriteRelayOption(INCommandDetail cd, RelayOption_Parameter value) : base(cd, value)
        {
        }
        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            RelayOption_Parameter model = value as RelayOption_Parameter;
            if (model == null) return false;
            return model.checkedParameter();
        }
        /// <summary>
        /// 命令在此进行拼装
        /// </summary>
        protected override void CreatePacket0()
        {
            Packet(0x03, 0x01, 0x01, 0x4, GetCmdData());
        }

        protected IByteBuffer GetCmdData()
        {
            RelayOption_Parameter model = _Parameter as RelayOption_Parameter;
            var acl = _Connector.GetByteBufAllocator();
            var buf = acl.Buffer(model.GetDataLen());
            model.GetBytes(buf);
            return buf;
        }

        /// <summary>
        /// 【应答：OK】 => 父类已处理
        /// </summary>
        /// <param name="oPck"></param>
        protected override void CommandNext1(OnlineAccessPacket oPck)
        {
            return;
        }

        protected override void CommandReSend()
        {
            return;
        }

        protected override void Release1()
        {
            return;
        }
    }
}
