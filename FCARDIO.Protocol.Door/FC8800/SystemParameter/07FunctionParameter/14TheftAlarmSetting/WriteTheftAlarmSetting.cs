using FCARDIO.Core.Command;
using FCARDIO.Protocol.FC8800;
using FCARDIO.Protocol.OnlineAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.SystemParameter.FunctionParameter
{
    /// <summary>
    /// 设置智能防盗主机参数
    /// </summary>
    public class WriteTheftAlarmSetting : FC8800Command_WriteParameter
    {
        /// <summary>
        /// 设置智能防盗主机参数 初始化命令
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        /// <param name="par">包含智能防盗主机参数</param>
        public WriteTheftAlarmSetting(INCommandDetail cd, WriteTheftAlarmSetting_Parameter par) : base(cd, par) { }

        /// <summary>
        /// 检查命令参数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            WriteTheftAlarmSetting_Parameter model = value as WriteTheftAlarmSetting_Parameter;
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
            WriteTheftAlarmSetting_Parameter model = _Parameter as WriteTheftAlarmSetting_Parameter;
            Packet(0x01, 0x0A, 0x0E, Convert.ToUInt32(model.GetDataLen()), model.GetBytes(GetNewCmdDataBuf(model.GetDataLen())));
        }
    }
}