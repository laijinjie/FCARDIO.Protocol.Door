using FCARDIO.Core.Command;
using System;

namespace FCARDIO.Protocol.Door.FC8800.SystemParameter.FunctionParameter
{
    /// <summary>
    /// 设置消防报警参数
    /// </summary>
    public class WriteFireAlarmOption : FC8800Command_WriteParameter
    {
        /// <summary>
        /// 设置消防报警参数 初始化命令
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        /// <param name="par">包含消防报警参数</param>
        public WriteFireAlarmOption(INCommandDetail cd, WriteFireAlarmOption_Parameter par) : base(cd, par) {
            CmdType = 0x01;
            CmdIndex = 0x0A;
            CmdPar = 0x05;
        }

        /// <summary>
        /// 检查命令参数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            WriteFireAlarmOption_Parameter model = value as WriteFireAlarmOption_Parameter;
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
            WriteFireAlarmOption_Parameter model = _Parameter as WriteFireAlarmOption_Parameter;


            Packet(CmdType, CmdIndex, CmdPar, Convert.ToUInt32(model.GetDataLen()), model.GetBytes(GetNewCmdDataBuf(model.GetDataLen())));
        }
    }
}