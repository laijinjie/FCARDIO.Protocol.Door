using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCARDIO.Core.Command;
using FCARDIO.Core.Packet;
using FCARDIO.Protocol.OnlineAccess;

namespace FCARDIO.Protocol.Door.FC8800.TimeGroup
{
    /// <summary>
    /// 添加节假日
    /// </summary>
    public class AddTimeGroup : FC8800Command_WriteParameter
    {
        private int writeIndex = 0;

        private int maxCount = 0;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cd"></param>
        public AddTimeGroup(INCommandDetail cd, AddTimeGroup_Parameter par) : base(cd, par)
        {

        }

        /// <summary>
        /// 检查命令参数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            AddTimeGroup_Parameter model = value as AddTimeGroup_Parameter;
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
            AddTimeGroup_Parameter model = _Parameter as AddTimeGroup_Parameter;
            var acl = _Connector.GetByteBufAllocator();
            var buf = acl.Buffer(model.GetDataLen());
            maxCount = model.ListWeekTimeGroup.Count;
            Packet(0x6, 0x3, 0x00, Convert.ToUInt32(model.GetDataLen()), model.GetBytes(buf));
            writeIndex++;
           

        }

        /// <summary>
        /// 没有触发
        /// </summary>
        /// <param name="oPck"></param>
        protected override void CommandNext1(OnlineAccessPacket oPck)
        {
            

        }

        /// <summary>
        /// 检查并进行命令的下一部分，分次上传数据
        /// </summary>
        /// <param name="readPacket">收到的数据包</param>
        protected override void CommandNext(INPacket readPacket)
        {
            //应答：OK
            AddTimeGroup_Parameter model = _Parameter as AddTimeGroup_Parameter;
            if (writeIndex < maxCount)
            {
                model.SetWriteIndex(writeIndex);
                var acl = _Connector.GetByteBufAllocator();
                var buf = acl.Buffer(model.GetDataLen());
                Packet(0x6, 0x3, 0x00, Convert.ToUInt32(model.GetDataLen()), model.GetBytes(buf));
                writeIndex++;
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
