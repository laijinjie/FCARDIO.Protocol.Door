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
    public class AddHoliday : FC8800Command_WriteParameter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cd"></param>
        public AddHoliday(INCommandDetail cd, AddHoliday_Parameter par) : base(cd, par)
        {

        }

        /// <summary>
        /// 检查命令参数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            AddHoliday_Parameter model = value as AddHoliday_Parameter;
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
            AddHoliday_Parameter model = _Parameter as AddHoliday_Parameter;
            var acl = _Connector.GetByteBufAllocator();
            var buf = acl.Buffer(model.GetDataLen());
            Packet(0x4, 0x4, 0x00, Convert.ToUInt32(model.GetDataLen()), model.GetBytes(buf));
            // Packet(0x02, 0x02, 0x00, 0x07, buf);
        }

        protected override void CommandNext1(OnlineAccessPacket oPck)
        {
            throw new NotImplementedException();
        }

    }
}
