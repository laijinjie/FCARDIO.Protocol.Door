using DotNetty.Buffers;
using FCARDIO.Core.Command;
using FCARDIO.Protocol.FC8800;
using FCARDIO.Protocol.OnlineAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.Door.InterLockSetting
{
    /// <summary>
    /// 读取区域互锁
    /// </summary>
    public class ReadInterLockSetting : FC8800Command_Read_DoorParameter
    {
        /// <summary>
        /// 初始化参数
        /// </summary>
        /// <param name="cd"></param>
        /// <param name="value"></param>
        public ReadInterLockSetting(INCommandDetail cd, DoorPort_Parameter value) : base(cd, value) { }


        /// <summary>
        /// 创建一个通讯指令
        /// </summary>
        protected override void CreatePacket0()
        {
            DoorPort_Parameter model = _Parameter as DoorPort_Parameter;
            Packet(0x03, 0x19, 0x03, 0x01, model.GetBytes(GetNewCmdDataBuf(model.GetDataLen())));
        }


        /// <summary>
        /// 处理返回值
        /// </summary>
        /// <param name="oPck"></param>
        protected override void CommandNext1(OnlineAccessPacket oPck)
        {
            if (CheckResponse(oPck, 25))
            {
                var buf = oPck.CmdData;
                InterLockSetting_Result rst = new InterLockSetting_Result();
                _Result = rst;
                rst.SetBytes(buf);
                CommandCompleted();
            }
        }

    }
}
