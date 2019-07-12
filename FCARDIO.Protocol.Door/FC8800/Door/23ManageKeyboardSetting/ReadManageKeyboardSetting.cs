using DotNetty.Buffers;
using FCARDIO.Core.Command;
using FCARDIO.Protocol.FC8800;
using FCARDIO.Protocol.OnlineAccess;
using FCARDIO.Protocol.Util;

namespace FCARDIO.Protocol.Door.FC8800.Door.ManageKeyboardSetting
{
    /// <summary>
    /// 读取键盘管理功能
    /// </summary>
    public class ReadManageKeyboardSetting : FC8800Command_ReadParameter
    {
        private WriteManageKeyboardSetting_Parameter mPar;
        public ReadManageKeyboardSetting(INCommandDetail cd, WriteManageKeyboardSetting_Parameter value) : base(cd, value) { mPar = value; }

        /// <summary>
        /// 检查参数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            WriteManageKeyboardSetting_Parameter model = value as WriteManageKeyboardSetting_Parameter;
            if (model == null) return false;
            return model.checkedParameter();
        }

        /// <summary>
        /// 创建一个通讯指令
        /// </summary>
        protected override void CreatePacket0()
        {

            Packet(0x03, 0x15, 0x01, 0x01, mPar.GetBytes(_Connector.GetByteBufAllocator().Buffer(1)));
        }

        /// <summary>
        /// 获取参数结构的字节编码
        /// </summary>
        /// <returns></returns>
        private IByteBuffer GetCmdData()
        {
            DoorPort_Parameter model = _Parameter as DoorPort_Parameter;
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
            if (CheckResponse(oPck, 2))
            {
                Setting_SetBytes(oPck.CmdData);
                _Result = mPar;
            }
            if (CheckResponse(oPck, 5))
            {
                Password_SetBytes(oPck.CmdData);
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

        /// <summary>
        /// 将字节缓冲解码为设置结构
        /// </summary>
        /// <param name="databuf"></param>
        public void Setting_SetBytes(IByteBuffer tmpBuf)
        {
            mPar.DoorNum = tmpBuf.ReadByte();
            mPar.Use = tmpBuf.ReadBoolean();
            _Result = mPar;
            if (mPar.Use)
            {
                tmpBuf = _Connector.GetByteBufAllocator().Buffer(1);
                tmpBuf.WriteByte(mPar.DoorNum);
                Packet(0x03, 0x15, 0x03, 0x01, tmpBuf);
                CommandReady();//设定命令当前状态为准备就绪，等待发送
            }
        }

        /// <summary>
        /// 将字节缓冲解码为密码结构
        /// </summary>
        /// <param name="databuf"></param>
        public void Password_SetBytes(IByteBuffer databuf)
        {
            mPar.DoorNum = databuf.ReadByte();
            mPar.Password = StringUtil.ByteBufToHex(databuf, 4).TrimEnd('F');
            CommandCompleted();
        }
    }
}
