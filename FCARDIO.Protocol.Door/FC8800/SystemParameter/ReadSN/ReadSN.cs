using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCARDIO.Protocol.OnlineAccess;
using FCARDIO.Protocol.FC8800;
using FCARDIO.Core.Extension;
using FCARDIO.Core.Command;


namespace FCARDIO.Protocol.Door.FC8800.SystemParameter.ReadSN
{
    public class ReadSN : FC8800Command
    {
        public ReadSN(INCommandDetail cd) : base(cd, null)
        {
        }

        /// <summary>
        /// 命令再次进行拼装
        /// </summary>
        protected override void CreatePacket0()
        {
            Packet(1, 2);
        }


        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            throw new NotImplementedException();
        }

        protected override void CommandNext1(OnlineAccessPacket oPck)
        {
            if (CheckResponse(oPck, 16))
            {
                var buf = oPck.CmdData;
                byte[] snBuf = new byte[16];
                buf.ReadBytes(snBuf);
                var SN = snBuf.GetString();
                _Result = new ReadSN_Result(SN, snBuf);
                CommandCompleted();
            }
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
