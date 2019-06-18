using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCARDIO.Core.Command;
using FCARDIO.Protocol.OnlineAccess;

namespace FCARDIO.Protocol.Door.FC8800.Holiday
{
    /// <summary>
    /// 添加节假日
    /// </summary>
    public class DeleteHoliday : FC8800Command_WriteParameter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cd"></param>
        public DeleteHoliday(INCommandDetail cd, DeleteHoliday_Parameter par) : base(cd, par)
        {

        }

        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            DeleteHoliday_Parameter model = value as DeleteHoliday_Parameter;
            if (model == null)
            {
                return false;
            }

            return model.checkedParameter();
        }

        protected override void CreatePacket0()
        {
            DeleteHoliday_Parameter model = _Parameter as DeleteHoliday_Parameter;
            var acl = _Connector.GetByteBufAllocator();
            var buf = acl.Buffer(model.GetDataLen());
            Packet(0x4, 0x4, 0x01, Convert.ToUInt32(model.GetDataLen()), model.GetBytes(buf));
        }
    }
}
