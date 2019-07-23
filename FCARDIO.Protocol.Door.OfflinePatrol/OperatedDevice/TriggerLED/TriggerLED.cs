﻿using FCARDIO.Core.Command;

namespace FCARDIO.Protocol.Door.OfflinePatrol.OperatedDevice.TriggerLED
{
    /// <summary>
    /// 触发LED手电筒
    /// </summary>
    public class TriggerLED : Write_Command
    {
        /// <summary>
        /// 初始化命令
        /// </summary>
        /// <param name="cd"></param>
        /// <param name="par"></param>
        public TriggerLED(INCommandDetail cd, INCommandParameter par) : base(cd, par)
        {

        }

        /// <summary>
        /// 检查参数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            TriggerLED_Parameter model = value as TriggerLED_Parameter;
            if (model == null) return false;
            return model.checkedParameter();
        }

        /// <summary>
        /// 将命令打包成一个Packet，准备发送
        /// </summary>
        protected override void CreatePacket0()
        {
            TriggerLED_Parameter model = _Parameter as TriggerLED_Parameter;
            Packet(0x05, 0x02, 1, model.GetBytes(GetNewCmdDataBuf(model.GetDataLen())));
        }
    }
}
