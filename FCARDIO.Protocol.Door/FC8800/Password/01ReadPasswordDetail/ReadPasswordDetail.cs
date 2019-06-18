using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCARDIO.Core.Command;
using FCARDIO.Protocol.OnlineAccess;

namespace FCARDIO.Protocol.Door.FC8800.Password
{
    /// <summary>
    /// 读取密码容量信息
    /// </summary>
    public class ReadPasswordDetail : FC8800Command_ReadParameter
    {
        public ReadPasswordDetail(INCommandDetail cd) : base(cd, null)
        {

        }
        protected override void CommandNext1(OnlineAccessPacket oPck)
        {
            if (CheckResponse(oPck, 0x04))
            {
                var buf = oPck.CmdData;
                ReadPasswordDetail_Result rst = new ReadPasswordDetail_Result();
                _Result = rst;
                rst.SetBytes(buf);
                CommandCompleted();
            }
        }

        protected override void CreatePacket0()
        {
            Packet(5, 1);
        }
    }
}
