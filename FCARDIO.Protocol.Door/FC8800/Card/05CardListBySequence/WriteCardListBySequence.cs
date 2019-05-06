﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;
using FCARDIO.Core.Command;
using FCARDIO.Protocol.FC8800;
using FCARDIO.Protocol.OnlineAccess;

namespace FCARDIO.Protocol.Door.FC8800.Card.CardListBySequence
{
    /// <summary>
    /// 将卡片列表写入到控制器非排序区
    /// </summary>
    public class WriteCardListBySequence
        : FC8800Command
    {
        private int mIndex;//指示当前命令进行的步骤
        private List<FC8800.Data.CardDetail> _List;
        private Queue<IByteBuffer> mBufs;

        /// <summary>
        /// 初始化命令结构 
        /// </summary>
        /// <param name="cd"></param>
        /// <param name="parameter"></param>
        public WriteCardListBySequence(INCommandDetail cd, WriteCardListBySequence_Parameter perameter) : base(cd, perameter) { }

        /// <summary>
        /// 检查参数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            WriteCardListBySequence_Parameter model = new WriteCardListBySequence_Parameter();
            if (model == null) return false;
            return model.checkedParameter();
        }

        /// <summary>
        /// 创建一个通讯指令
        /// </summary>
        protected override void CreatePacket0()
        {
            uint iLen = (5 * 0x21) + 4;
            Packet(0x07, 0x04, 0x00, iLen, getCmdData());
        }

        /// <summary>
        /// 获取参数结构的字节编码
        /// </summary>
        /// <returns></returns>
        private IByteBuffer getCmdData()
        {
            WriteCardListBySequence_Parameter model = _Parameter as WriteCardListBySequence_Parameter;
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
            if (CheckResponse(oPck, 0x05))
            {
                var buf = oPck.CmdData;
                WriteCardListBySequence_Result rst = new WriteCardListBySequence_Result();
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
        /// 命令释放时需要处理的函数
        /// </summary>
        protected override void Release1()
        {
            return;
        }
    }
}