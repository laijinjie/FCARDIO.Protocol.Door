using FCARDIO.Core.Command;
using System;

namespace FCARDIO.Protocol.Elevator.FC8864.Holiday
{
    /// <summary>
    /// 从控制器删除节假日
    /// </summary>
    public class DeleteHoliday : Write_Command
    {
        /// <summary>
        /// 初始化参数
        /// </summary>
        /// <param name="cd"></param>
        /// <param name="par"></param>
        public DeleteHoliday(INCommandDetail cd, DeleteHoliday_Parameter par) : base(cd, par)
        {

        }

        /// <summary>
        /// 检查参数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            DeleteHoliday_Parameter model = value as DeleteHoliday_Parameter;
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
            DeleteHoliday_Parameter model = _Parameter as DeleteHoliday_Parameter;
            var acl = _Connector.GetByteBufAllocator();
            var buf = acl.Buffer(model.GetDataLen());
            Packet(0x44, 0x4, 0x01, Convert.ToUInt32(model.GetDataLen()), model.GetBytes(buf));
        }
    }
}
