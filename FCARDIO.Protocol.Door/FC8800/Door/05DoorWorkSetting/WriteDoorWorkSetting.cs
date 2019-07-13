using DotNetty.Buffers;
using FCARDIO.Core.Command;
using FCARDIO.Protocol.FC8800;
using FCARDIO.Protocol.OnlineAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.Door.DoorWorkSetting
{
    /// <summary>
    /// 设置门工作方式
    /// </summary>
    public class WriteDoorWorkSetting : FC8800Command_WriteParameter
    {
        /// <summary>
        /// 设置门工作方式
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        /// <param name="par">包含门工作方式参数</param>
        public WriteDoorWorkSetting(INCommandDetail cd, ReadDoorWorkSetting_Parameter par) : base(cd, par) { }

        /// <summary>
        /// 检查命令参数
        /// </summary>
        /// <returns></returns>
        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            ReadDoorWorkSetting_Parameter model = value as ReadDoorWorkSetting_Parameter;
            if (model == null) return false;
            return model.checkedParameter();
        }

        /// <summary>
        /// 将命令打包成一个Packet，准备发送
        /// </summary>
        protected override void CreatePacket0()
        {
            ReadDoorWorkSetting_Parameter model = _Parameter as ReadDoorWorkSetting_Parameter;
            Packet(0x03, 0x06, 0x01, 0xE5, model.GetBytes(GetNewCmdDataBuf(model.GetDataLen())));
        }
    }
}
