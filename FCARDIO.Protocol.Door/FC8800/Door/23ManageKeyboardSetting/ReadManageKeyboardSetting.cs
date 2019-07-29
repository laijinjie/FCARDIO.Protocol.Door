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
    public class ReadManageKeyboardSetting : FC8800Command_Read_DoorParameter
    {
        

        /// <summary>
        /// 初始化参数
        /// </summary>
        /// <param name="cd"></param>
        /// <param name="value"></param>
        public ReadManageKeyboardSetting(INCommandDetail cd, DoorPort_Parameter value) : base(cd, value) {
             }

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
            DoorPort_Parameter model = _Parameter as DoorPort_Parameter;
            Packet(0x03, 0x15, 0x01, 0x01, model.GetBytes(GetNewCmdDataBuf(model.GetDataLen())));
            WriteManageKeyboardSetting_Parameter result = new WriteManageKeyboardSetting_Parameter((byte)model.Door);
            _Result = result;
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

            }
            if (CheckResponse(oPck, 5))
            {
                Password_SetBytes(oPck.CmdData);
            }
        }

        /// <summary>
        /// 将字节缓冲解码为设置结构
        /// </summary>
        /// <param name="tmpBuf"></param>
        public void Setting_SetBytes(IByteBuffer tmpBuf)
        {
            ((WriteManageKeyboardSetting_Parameter)_Result).DoorNum = tmpBuf.ReadByte();
            ((WriteManageKeyboardSetting_Parameter)_Result).Use = tmpBuf.ReadBoolean();
            tmpBuf = _Connector.GetByteBufAllocator().Buffer(1);
            tmpBuf.WriteByte(((WriteManageKeyboardSetting_Parameter)_Result).DoorNum);
            Packet(0x03, 0x15, 0x03, 0x01, tmpBuf);
            CommandReady();//设定命令当前状态为准备就绪，等待发送
        }

        /// <summary>
        /// 将字节缓冲解码为密码结构
        /// </summary>
        /// <param name="databuf"></param>
        public void Password_SetBytes(IByteBuffer databuf)
        {
            ((WriteManageKeyboardSetting_Parameter)_Result).DoorNum = databuf.ReadByte();
            ((WriteManageKeyboardSetting_Parameter)_Result).Password = StringUtil.ByteBufToHex(databuf, 4).TrimEnd('F');
            CommandCompleted();
        }
    }
}
