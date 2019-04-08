using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;
using FCARDIO.Core.Command;
using FCARDIO.Protocol.FC8800;
using FCARDIO.Protocol.OnlineAccess;

namespace FCARDIO.Protocol.Door.FC8800.Door.AnyCardSetting
{
    /// <summary>
    /// 读取全卡开门功能
    /// 所有的卡都能开门，不需要权限首选注册，只要读卡器能识别就能开门。
    /// 成功返回结果参考 {@link ReadAnyCardSetting_Result}
    /// </summary>
    public class ReadAnyCardSetting
        : FC8800Command
    {
        /// <summary>
        /// 初始化命令结构
        /// </summary>
        /// <param name="cd"></param>
        /// <param name="value">需要读取的门号结构</param>
        public ReadAnyCardSetting(INCommandDetail cd, DoorPort_Parameter value) : base(cd, value) { }

        /// <summary>
        /// 检查参数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            DoorPort_Parameter model = value as DoorPort_Parameter;
            if (model == null) return false;
            return model.checkedParameter();
        }

        /// <summary>
        /// 创建一个通讯指令
        /// </summary>
        protected override void CreatePacket0()
        {
            Packet(0x03, 0x11, 0x01, 0x01, GetCmdDate());
        }

        /// <summary>
        /// 获取参数结构的字节编码
        /// </summary>
        /// <returns></returns>
        private IByteBuffer GetCmdDate()
        {
            DoorPort_Parameter model = _Parameter as DoorPort_Parameter;
            var acl = _Connector.GetByteBufAllocator();
            var buf = acl.Buffer(model.GetDataLen());
            return buf;
        }

        /// <summary>
        /// 处理返回值
        /// </summary>
        /// <param name="oPck"></param>
        protected override void CommandNext1(OnlineAccessPacket oPck)
        {
            if (CheckResponse(oPck, 4))
            {
                var buf = oPck.CmdData;
                AnyCardSetting_Result rst = new AnyCardSetting_Result();
                _Result = rst;
                rst.SetBytes(buf);
                CommandCompleted();
            }
        }

        /// <summary>
        /// 命令重发时需要处理的函数
        /// </summary>
        protected override void CommandReSend()
        {
            return;
        }
      
        /// <summary>
        /// 命令释放时选哟处理的函数
        /// </summary>
        protected override void Release1()
        {
            return;
        }
    }
}
