using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;
using FCARDIO.Core.Command;
using FCARDIO.Protocol.FC8800;
using FCARDIO.Protocol.OnlineAccess;

namespace FCARDIO.Protocol.Door.FC8800.Door.SensorAlarmSetting
{
    /// <summary>
    /// 写入门磁报警功能<br/>
    /// 当无有效开门验证时（远程开门、刷卡、密码、出门按钮），检测到门磁打开时就会报警。<br/>
    /// 成功返回结果参考  ReadSensorAlarmSetting_Result 
    /// </summary>
    public class WriteSensorAlarmSetting
        : FC8800Command
    {
        /// <summary>
        /// 初始化命令结构
        /// </summary>
        /// <param name="cd"></param>
        /// <param name="value">包括门号和出门按钮功能</param>
        public WriteSensorAlarmSetting(INCommandDetail cd, WriteSensorAlarmSetting_Parameter value) : base(cd, value) { }

        /// <summary>
        /// 检查参数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            WriteSensorAlarmSetting_Parameter model = value as WriteSensorAlarmSetting_Parameter;
            if (model == null) return false;
            return model.checkedParameter();
        }
        
        /// <summary>
        /// 创建一个通讯指令
        /// </summary>
        protected override void CreatePacket0()
        {
            Packet(0x03, 0x10, 0x01, 0xE2, GetCmdData());
        }

        /// <summary>
        /// 获取参数结构的字节编码
        /// </summary>
        /// <returns></returns>
        private IByteBuffer GetCmdData()
        {
            WriteSensorAlarmSetting_Parameter model = _Parameter as WriteSensorAlarmSetting_Parameter;
            var acl = _Connector.GetByteBufAllocator();
            var buf = acl.Buffer(model.GetDataLen());
            model.GetBytes(buf);
            return buf;
        }

        /// <summary>
        /// 处理返回值
        /// </summary>
        /// <param name="oPck"></param>
        protected override void CommandNext1(OnlineAccessPacket oPck)
        {
            return;
        }
        
        /// <summary>
        /// 命令重发时需要处理的函数
        /// </summary>
        protected override void CommandReSend()
        {
            return;
        }

       /// <summary>
       /// 命令释放时需要处理的函数
       /// </summary>
        protected override void Release1()
        {
            return;
        }
    }
}
