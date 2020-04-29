using DotNetty.Buffers;
using DoNetDrive.Core.Command;
using DoNetDrive.Protocol.POS.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoNetDrive.Protocol.Door.Door8800.Data.TimeGroup;

namespace DoNetDrive.Protocol.POS.TimeGroup
{
    /// <summary>
    /// 读取所有时段
    /// </summary>
    public class ReadTimeGroup : Read_Command
    {

        /// <summary>
        /// 初始化参数
        /// </summary>
        /// <param name="cd"></param>
        public ReadTimeGroup(DESDriveCommandDetail cd, ReadTimeGroup_Parameter par) : base(cd, par)
        {
        }


        /// <summary>
        /// 将命令打包成一个Packet，准备发送
        /// </summary>
        protected override void CreatePacket0()
        {
            ReadTimeGroup_Parameter model = _Parameter as ReadTimeGroup_Parameter;

            var acl = _Connector.GetByteBufAllocator();

            var buf = acl.Buffer(model.GetDataLen());

            Packet(0x04, 0x02, 0x00, Convert.ToUInt32(model.GetDataLen()), model.GetBytes(buf));
        }

        /// <summary>
        /// 处理返回值
        /// </summary>
        /// <param name="oPck"></param>
        protected override void CommandNext1(DESPacket oPck)
        {
            if (CheckResponse(oPck, 0x71))
            {
                var buf = oPck.CommandPacket.CmdData;

                ReadTimeGroup_Result rtgr = new ReadTimeGroup_Result();
                _Result = rtgr;

                SetBytes(rtgr, buf);
                CommandCompleted();
            }

        }

        /// <summary>
        ///  将 字节流  转换为 开门时段
        /// </summary>
        /// <param name="result">读取所有开门时段结果</param>
        /// <param name="databufs"></param>
        public void SetBytes(ReadTimeGroup_Result result, IByteBuffer databufs)
        {
            result.Index = databufs.ReadByte();
            WeekTimeGroup wtg = new WeekTimeGroup(4);
            
            wtg.SetBytes(databufs);
            result.WeekTimeGroup = wtg;
        }


    }
}
