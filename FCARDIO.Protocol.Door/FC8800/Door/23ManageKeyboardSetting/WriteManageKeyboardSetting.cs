﻿using DotNetty.Buffers;
using FCARDIO.Core.Command;
using FCARDIO.Protocol.FC8800;
using FCARDIO.Protocol.OnlineAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.Door.ManageKeyboardSetting
{
    /// <summary>
    /// 键盘管理功能
    /// </summary>
    public class WriteManageKeyboardSetting : FC8800Command_WriteParameter
    {
        protected WriteManageKeyboardSetting_Parameter mManageKeyboardPar;
        /// <summary>
        /// 当前命令步骤
        /// </summary>
        protected int Step;

        /// <summary>
        /// 初始化命令结构
        /// </summary>
        /// <param name="cd"></param>
        /// <param name="value"></param>
        public WriteManageKeyboardSetting(INCommandDetail cd, WriteManageKeyboardSetting_Parameter value) : base(cd, value) { mManageKeyboardPar = value; }

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
            var buf = GetNewCmdDataBuf(5);
            Packet(0x03, 0x15, 0x00, 2, mManageKeyboardPar.Setting_GetBytes(buf)) ;
            Step = 1;
            IniPacketProcess();
        }

        /// <summary>
        /// 初始化指令的步骤数
        /// </summary>
        private void IniPacketProcess()
        {
            if (mManageKeyboardPar.Use)
            {
                _ProcessMax = 2;
                Step = 2;
            }
            else
            {
                _ProcessMax = 1;
            }
        }

        /// <summary>
        /// 获取参数结构的字节编码
        /// </summary>
        /// <returns></returns>
        private IByteBuffer GetCmdData()
        {
            WriteManageKeyboardSetting_Parameter model = _Parameter as WriteManageKeyboardSetting_Parameter;
            var acl = _Connector.GetByteBufAllocator();
            var buf = acl.Buffer(model.GetDataLen());
            model.GetBytes(buf);
            return buf;
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
                        WritePassword();
                    }
                    break;
                default:
                    break;
            }
            return;
        }

        /// <summary>
        /// 写密码
        /// </summary>
        private void WritePassword()
        {
            _ProcessStep = 2;
            var buf = GetCmdBuf();
            mManageKeyboardPar.Password_GetBytes(buf);
            Packet(0x15, 0x02, 5);
            Step = 0;
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

        /// <summary>
        /// 处理返回值
        /// </summary>
        /// <param name="oPck"></param>
        protected override void CommandNext1(OnlineAccessPacket oPck)
        {
        }
    }
}
