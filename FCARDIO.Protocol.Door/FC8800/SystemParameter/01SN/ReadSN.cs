using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCARDIO.Protocol.OnlineAccess;
using FCARDIO.Protocol.FC8800;
using FCARDIO.Core.Extension;
using FCARDIO.Core.Command;


namespace FCARDIO.Protocol.Door.FC8800.SystemParameter.SN
{
    /// <summary>
    /// 获取控制器SN
    /// </summary>
    public class ReadSN : FC8800Command
    {
        public ReadSN(INCommandDetail cd) : base(cd, null)
        {
        }

        /// <summary>
        /// 命令在此进行拼装
        /// </summary>
        protected override void CreatePacket0()
        {
            Packet(1, 2);
        }


        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            return true;
        }

        protected override void CommandNext1(OnlineAccessPacket oPck)
        {
            if (CheckResponse(oPck, 16))
            {
                var buf = oPck.CmdData;
                SN_Result rst = new SN_Result();
                _Result = rst;
                rst.SetBytes(buf);
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
