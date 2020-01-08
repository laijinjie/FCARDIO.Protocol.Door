using FCARDIO.Core.Command;
using FCARDIO.Protocol.Door.FC8800;
using FCARDIO.Protocol.Door.FC8800.Door.InvalidCardAlarmOption;
using FCARDIO.Protocol.OnlineAccess;
using System.Diagnostics;

namespace FCARDIO.Protocol.Door.FC89H.Door.InvalidCardAlarmOption
{
    /// <summary>
    /// 设置非法读卡报警参数
    /// </summary>
    public class WriteInvalidCardAlarmOption : FC8800Command_WriteParameter
    {
        /// <summary>
        /// 当前命令步骤
        /// </summary>
        protected int Step;

        /// <summary>
        /// 参数对象
        /// </summary>
        protected WriteInvalidCardAlarmOption_Parameter mPar;

        /// <summary>
        /// 设置非法读卡报警参数
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        /// <param name="par">包含非法读卡报警参数</param>
        public WriteInvalidCardAlarmOption(INCommandDetail cd, WriteInvalidCardAlarmOption_Parameter par) : base(cd, par) { mPar = par; }


        /// <summary>
        /// 检查命令参数
        /// </summary>
        /// <param name="value"></param>
        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            WriteInvalidCardAlarmOption_Parameter model = value as WriteInvalidCardAlarmOption_Parameter;
            if (model == null) return false;
            return model.checkedParameter();
        }

        /// <summary>
        /// 将命令打包成一个Packet，准备发送
        /// </summary>
        protected override void CreatePacket0()
        {
            WriteInvalidCardAlarmOption_Parameter model = _Parameter as WriteInvalidCardAlarmOption_Parameter;
            Packet(0x03, 0x0A, 0x00, 0x02, model.GetBytes(GetNewCmdDataBuf(model.GetDataLen())));
            IniPacketProcess();
        }

        /// <summary>
        /// 初始化指令的步骤数
        /// </summary>
        private void IniPacketProcess()
        {
            _ProcessMax = 2;
            Step = 2;

        }

        /// <summary>
        /// 接收到响应，开始处理下一步命令
        /// </summary>
        /// <param name="oPck"></param>
        protected override void CommandNext0(OnlineAccessPacket oPck)
        {

            switch (Step)
            {
                case 2:
                    if (CheckResponse_OK(oPck))
                    {
                        WriteReadInvalidCardTime();
                    }
                    break;
                case 3:
                    CommandCompleted();
                    break;
                default:
                    break;
            }
            return;
        }

        /// <summary>
        /// 写入 读未注册卡到达一定次数后报警
        /// </summary>
        private void WriteReadInvalidCardTime()
        {
            _ProcessStep = 2;
            var buf = GetCmdBuf();
            buf = mPar.ReadInvalidCardTime_GetBytes(buf);
            Packet(0x03, 0x0A, 0x02, 0x02, buf);
            Step = 3;
        }
    }
}
