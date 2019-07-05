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
    /// 设置 区域互锁
    /// </summary>
    public class WriteInterLockSetting : FC8800Command
    {
        /// <summary>
        /// 初始化命令结构
        /// </summary>
        /// <param name="cd"></param>
        /// <param name="value">包括门号和出门按钮功能</param>
        public WriteInterLockSetting(INCommandDetail cd, WriteInterLockSetting_Parameter value) : base(cd, value) { }

        /// <summary>
        /// 检查参数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            WriteInterLockSetting_Parameter model = value as WriteInterLockSetting_Parameter;
            if (model == null) return false;
            for (int i = 0; i < model.IP.Length; i++)
            {
                if (model.IP[i] > 255 || model.IP[i] < 0)
                {
                    return false;
                }
            }
            if (model.Num < 1 || model.Num > 63)
            {
                return false;
            }
            return model.checkedParameter();
        }

        /// <summary>
        /// 创建一个通讯指令
        /// </summary>
        protected override void CreatePacket0()
        {
            Packet(0x03, 0x19, 0x02, 25, GetCmdData());
        }

        /// <summary>
        /// 获取参数结构的字节编码
        /// </summary>
        /// <returns></returns>
        private IByteBuffer GetCmdData()
        {
            WriteInterLockSetting_Parameter model = _Parameter as WriteInterLockSetting_Parameter;
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
