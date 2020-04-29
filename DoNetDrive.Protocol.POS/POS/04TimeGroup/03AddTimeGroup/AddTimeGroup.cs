using DoNetDrive.Core.Command;
using DoNetDrive.Protocol.Door.Door8800.TimeGroup;
using DoNetDrive.Protocol.POS.Protocol;
using DotNetty.Buffers;

namespace DoNetDrive.Protocol.POS.TimeGroup
{
    public class AddTimeGroup : Write_Command
    {
        /// <summary>
        /// 参数
        /// </summary>
        protected AddTimeGroup_Parameter mPar;
        /// <summary>
        /// 写入索引
        /// </summary>
        protected int writeIndex = 0;

        /// <summary>
        /// 总开门时段数
        /// </summary>
        protected int maxCount = 0;

        /// <summary>
        /// 初始化参数
        /// </summary>
        /// <param name="cd">命令详情</param>
        /// <param name="par">命令逻辑所需要的命令参数 </param>
        public AddTimeGroup(DESDriveCommandDetail cd, AddTimeGroup_Parameter par) : base(cd, par)
        {
            mPar = par;
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

            Packet(0x04, 0x03, 0x00, 0x71, model.GetBytes(buf));

        }

        /// <summary>
        /// 将 参数 编码到字节流
        /// </summary>
        /// <param name="databuf"></param>
        /// <returns></returns>
        protected IByteBuffer GetBytes(IByteBuffer databuf)
        {
            databuf.WriteByte(mPar.Index);

            mPar.WeekTimeGroup.GetBytes(databuf);
            return databuf;
        }

       

    }
}
