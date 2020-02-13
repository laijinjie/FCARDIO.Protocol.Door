using FCARDIO.Core.Command;
using FCARDIO.Protocol.FC8800;
using FCARDIO.Protocol.OnlineAccess;

namespace FCARDIO.Protocol.Door.FC8800
{
    /// <summary>
    /// 针对命令中的写参数命令进行抽象封装
    /// </summary>
    public abstract class FC8800CommandEx : FC8800Command

    {
        /// <summary>
        /// 控制码分类
        /// </summary>
        public byte CmdType;

        /// <summary>
        /// 控制码命令
        /// </summary>
        public byte CmdIndex;

        /// <summary>
        /// 控制码参数
        /// </summary>
        public byte CmdPar;

        /// <summary>
        /// 数据码长度
        /// </summary>
        public byte DataLen;


        /// <summary>
        /// 返回指令分类
        /// </summary>
        protected byte CheckResponseCmdType;

        /// <summary>
        /// 初始化命令
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        /// <param name="par">包含命令所需要的其他参数</param>
        public FC8800CommandEx(INCommandDetail cd, INCommandParameter par) : base(cd, par)
        {

        }

        /// <summary>
        /// 命令重发时，对命令中一些缓冲做清空或参数重置<br/>
        /// 此命令一般情况下不需要实现！
        /// </summary>
        protected override void CommandReSend()
        {
            return;
        }

        /// <summary>
        /// 释放命令占用的内存<br/>
        /// 此命令一般情况下不需要实现！
        /// </summary>
        protected override void Release1()
        {
            return;
        }



        /// <summary>
        /// 检查指令返回值
        /// </summary>
        /// <param name="oPck"></param>
        /// <param name="CmdType">命令类型</param>
        /// <param name="CmdIndex">命令索引</param>
        /// <param name="CmdPar">命令参数</param>
        /// <returns></returns>
        protected virtual bool CheckResponse(OnlineAccessPacket oPck, byte CmdType, byte CmdIndex, byte CmdPar)
        {
            return (oPck.CmdType == CmdType + 0x30 &&
                oPck.CmdIndex == CmdIndex &&
                oPck.CmdPar == CmdPar);

        }

        /// <summary>
        /// 徐铭康增加，兼容其他类型(电梯)控制器
        /// 检查指令返回值
        /// </summary>
        /// <param name="oPck"></param>
        /// <param name="dl">命令类型</param>
        /// <returns></returns>
        protected override bool CheckResponse(OnlineAccessPacket oPck, int dl)
        {
            if (CheckResponseCmdType == 0)
            {
                CheckResponseCmdType = CmdType;
            }
            return (oPck.CmdType == CheckResponseCmdType + 0x30) && oPck.DataLen == dl;

        }


        /// <summary>
        /// 重置命令内容
        /// </summary>
        /// <param name="ct">命令类型</param>
        /// <param name="ci">命令索引</param>
        /// <param name="cp">命令参数</param>
        /// <param name="dl">数据长度</param>
        protected void RewritePacket(byte ct, byte ci, byte cp, int dl)
        {
            FCPacket.CmdType = ct;
            FCPacket.CmdIndex = ci;
            FCPacket.CmdPar = cp;
            FCPacket.DataLen = dl;

        }


        /// <summary>
        /// 重置命令内容
        /// </summary>
        /// <param name="ci">命令索引</param>
        /// <param name="cp">命令参数</param>
        /// <param name="dl">数据长度</param>
        protected void RewritePacket(byte ci, byte cp, int dl)
        {
            FCPacket.CmdIndex = ci;
            FCPacket.CmdPar = cp;
            FCPacket.DataLen = dl;

        }


    }
}
