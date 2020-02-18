using DotNetty.Buffers;
using FCARDIO.Core.Command;
using FCARDIO.Core.Packet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.POS.Protocol
{
    /// <summary>
    /// 使用DES加密的命令包
    /// 适用于：FC5000 系列消费机
    /// </summary>
    public abstract class DESCommand : AbstractCommand
    {
        /// <summary>
        ///  处理当前命令逻辑的数据包和 _Packet 是同一个对象
        /// </summary>
        protected DESPacket DPacket;

        /// <summary>
        /// 表示校验和错误的状态
        /// </summary>
        public static CommandStatus_Faulted CheckSumErrorStatus = (CommandStatus_Faulted)FC8800.FC8800Command.CheckSumErrorStatus;

        /// <summary>
        /// 表示通讯密码错误的状态
        /// </summary>
        public static CommandStatus_Faulted PasswordErrorStatus = (CommandStatus_Faulted)FC8800.FC8800Command.PasswordErrorStatus;

        /// <summary>
        /// 创建一个DES命令类，包含命令目标和连接通道信息，还有命令参数
        /// </summary>
        /// <param name="cd">命令目标和连接通道信息</param>
        /// <param name="par">命令参数</param>
        public DESCommand(DESDriveCommandDetail cd, INCommandParameter par) : base(cd, par)
        {
            DPacket = new DESPacket(cd);
            _Packet = DPacket;
            _IsWaitResponse = true;
        }

        /// <summary>
        /// 指令开始执行时，用于让命令组装第一个用于发送的数据包 CommandNext0 中组装（如果有的话），并创建一个用于解析返回值的对象_Decompile
        /// </summary>
        protected override void CreatePacket()
        {
            _Decompile = new DESPacketDecompile(_Connector.GetByteBufAllocator());
            CreatePacket0();
        }

        /// <summary>
        /// 生成命令的第一个数据包，后续的数据包应该在接收到返回值在 CommandNext 函数中陆续组装
        /// </summary>
        protected abstract void CreatePacket0();

        /// <summary>
        /// 获取当前命令所使用的缓冲区
        /// </summary>
        /// <returns></returns>
        protected virtual IByteBuffer GetCmdBuf()
        {
            if (DPacket == null) return null;
            if (DPacket.CommandPacket == null) return null;

            var buf = DPacket.CommandPacket.CmdData;
            buf.Clear();
            return buf;
        }

        /// <summary>
        /// 修改数据包的内容 命令类型,命令索引,强制 命令参数 为0
        /// </summary>
        /// <param name="ct">命令类型</param>
        /// <param name="ci">命令索引</param>
        protected virtual void Packet(byte ct, byte ci)
        {
            Packet(ct, ci, 0);
        }

        /// <summary>
        /// 修改数据包的内容 命令类型,命令索引,命令参数 
        /// </summary>
        /// <param name="ct">命令类型</param>
        /// <param name="ci">命令索引</param>
        /// <param name="cp">命令参数</param>
        protected virtual void Packet(byte ct, byte ci, byte cp)
        {
            Packet(ct, ci, cp, 0, null);
        }

        /// <summary>
        /// 修改数据包的内容 命令类型,命令索引,命令参数,数据长度,数据内容
        /// </summary>
        /// <param name="ct">命令类型</param>
        /// <param name="ci">命令索引</param>
        /// <param name="cp">命令参数</param>
        /// <param name="dl">数据长度</param>
        /// <param name="cd">数据内容</param>
        protected virtual void Packet(byte ct, byte ci, byte cp,
               int dl, IByteBuffer cd)
        {

            DPacket.SetPacket(ct, ci, cp, dl, cd);
        }

        /// <summary>
        /// 产生一个错误
        /// </summary>
        /// <param name="sText">错误描述</param>
        protected virtual void VerifyError(string sText)
        {
            throw new ArgumentException($"{sText} is Error");
        }

        /// <summary>
        /// 释放使用的资源
        /// </summary>
        protected override void Release0()
        {
            DPacket = null;
            Release1();
        }

        /// <summary>
        /// 让派生类主动释放资源
        /// </summary>
        protected abstract void Release1();


        /// <summary>
        /// 检查并进行命令的下一部分
        /// </summary>
        /// <param name="readPacket"></param>
        protected override void CommandNext(INPacket readPacket)
        {
            DESPacket oPck = readPacket as DESPacket;
            if (oPck == null) return;
            if (oPck.Code != DPacket.Code) return; //信息代码不一致，不是此命令的后续

            throw new NotImplementedException();
        }

        protected override void CommandReSend()
        {
            throw new NotImplementedException();
        }




    }
}
