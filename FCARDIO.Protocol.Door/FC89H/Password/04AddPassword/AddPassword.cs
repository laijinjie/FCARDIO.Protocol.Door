using FCARDIO.Core.Command;
using FCARDIO.Protocol.OnlineAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC89H.Password.AddPassword
{
    public class AddPassword : FC8800.Password.AddPassword
    {
        public AddPassword(INCommandDetail cd, AddPassword_Parameter par) : base(cd, par)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            AddPassword_Parameter model = value as AddPassword_Parameter;
            if (model == null)
            {
                return false;
            }

            return model.checkedParameter();
        }


        protected override void CreatePacket0()
        {
            AddPassword_Parameter model = _Parameter as AddPassword_Parameter;
            var acl = _Connector.GetByteBufAllocator();
            var buf = acl.Buffer(model.GetDataLen());
            maxCount = model.ListPassword.Count;
            Packet(0x5, 0x4, 0x00, Convert.ToUInt32(model.GetDataLen()), model.GetBytes(buf));

        }

        protected override void CommandNext1(OnlineAccessPacket oPck)
        {
            AddPassword_Parameter model = _Parameter as AddPassword_Parameter;
            if (model.mIndex < maxCount)
            {
                var acl = _Connector.GetByteBufAllocator();
                var buf = acl.Buffer(model.GetDataLen());
                Packet(0x5, 0x4, 0x00, Convert.ToUInt32(model.GetDataLen()), model.GetBytes(buf));
                CommandReady();
            }
            else
            {
                CommandCompleted();
            }
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
