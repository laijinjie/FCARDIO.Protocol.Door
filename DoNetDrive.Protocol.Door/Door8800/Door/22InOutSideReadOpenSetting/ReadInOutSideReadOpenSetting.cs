using DotNetty.Buffers;
using DoNetDrive.Core.Command;
using DoNetDrive.Protocol.Door8800;
using DoNetDrive.Protocol.OnlineAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoNetDrive.Protocol.Door.Door8800.Door.InOutSideReadOpenSetting
{
    /// <summary>
    /// 读取门内外同时读卡开门
    /// </summary>
    public class ReadInOutSideReadOpenSetting : Door8800Command_Read_DoorParameter
    {
        /// <summary>
        /// 初始化参数
        /// </summary>
        /// <param name="cd"></param>
        /// <param name="value"></param>
        public ReadInOutSideReadOpenSetting(INCommandDetail cd, DoorPort_Parameter value) : base(cd, value) { }


        /// <summary>
        /// 创建一个通讯指令
        /// </summary>
        protected override void CreatePacket0()
        {
            DoorPort_Parameter model = _Parameter as DoorPort_Parameter;
            Packet(0x03, 0x14, 0x01, 0x01, model.GetBytes(_Connector.GetByteBufAllocator().Buffer(1)));
        }


        /// <summary>
        /// 处理返回值
        /// </summary>
        /// <param name="oPck"></param>
        protected override void CommandNext1(OnlineAccessPacket oPck)
        {
            if (CheckResponse(oPck, 2))
            {
                var buf = oPck.CmdData;
                InOutSideReadOpenSetting_Result rst = new InOutSideReadOpenSetting_Result();
                _Result = rst;
                rst.SetBytes(buf);
                CommandCompleted();
            }
        }

    }
}
