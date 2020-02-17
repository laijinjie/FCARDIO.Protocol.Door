﻿using FCARDIO.Core.Command;

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
        public WriteDoorWorkSetting(INCommandDetail cd, WriteDoorWorkSetting_Parameter par) : base(cd, par) {

        }

        /// <summary>
        /// 检查命令参数
        /// </summary>
        /// <returns></returns>
        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            WriteDoorWorkSetting_Parameter model = value as WriteDoorWorkSetting_Parameter;
            if (model == null) return false;
            return model.checkedParameter();
        }

        /// <summary>
        /// 将命令打包成一个Packet，准备发送
        /// </summary>
        protected override void CreatePacket0()
        {
            WriteDoorWorkSetting_Parameter model = _Parameter as WriteDoorWorkSetting_Parameter;
            Packet(0x03, 0x06, 0x01, 0xE5, model.GetBytes(GetNewCmdDataBuf(model.GetDataLen())));
        }
    }
}
