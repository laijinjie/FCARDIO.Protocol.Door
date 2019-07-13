using FCARDIO.Core.Command;
using FCARDIO.Protocol.FC8800;
using FCARDIO.Protocol.OnlineAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.Password
{
    public class DeletePassword<T> : FC8800Command where T : PasswordDetail, new()
    {
        int maxCount = 0;
        public DeletePassword(INCommandDetail cd, DeletePassword_Parameter<T> par) : base(cd, par)
        {

        }
        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            DeletePassword_Parameter<T> model = value as DeletePassword_Parameter<T>;
            if (model == null)
            {
                return false;
            }

            return model.checkedParameter();
        }

        protected override void CommandNext1(OnlineAccessPacket oPck)
        {
            DeletePassword_Parameter<T> model = _Parameter as DeletePassword_Parameter<T>;
            if (model.mIndex < maxCount)
            {
                var acl = _Connector.GetByteBufAllocator();
                var buf = acl.Buffer(model.GetDataLen());
                Packet(0x5, 0x5, 0x00, Convert.ToUInt32(model.GetDataLen()), model.GetBytes(buf));
                CommandReady();
            }
            else
            {
                CommandCompleted();
            }
        }

        protected override void CommandReSend()
        {
            
        }

        protected override void CreatePacket0()
        {
            DeletePassword_Parameter<T> model = _Parameter as DeletePassword_Parameter<T>;
            var acl = _Connector.GetByteBufAllocator();
            var buf = acl.Buffer(model.GetDataLen());
            maxCount = model.ListPassword.Count;
            Packet(0x5, 0x5, 0x00, Convert.ToUInt32(model.GetDataLen()), model.GetBytes(buf));
        }

        protected override void Release1()
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oPck"></param>
        protected override void CommandNext0(OnlineAccessPacket oPck)
        {
            if (CheckResponse_OK(oPck))
            {

                //继续发下一包
                CommandNext1(oPck);
            }

        }
    }
}
