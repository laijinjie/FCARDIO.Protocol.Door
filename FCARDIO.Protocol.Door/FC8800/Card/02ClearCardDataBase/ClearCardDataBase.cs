using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;
using FCARDIO.Core.Command;
using FCARDIO.Protocol.FC8800;
using FCARDIO.Protocol.OnlineAccess;

namespace FCARDIO.Protocol.Door.FC8800.Card.ClearCardDataBase
{
     public class ClearCardDataBase
        :FC8800Command
    {
        /// <summary>
        /// 初始化命令结构 
        /// </summary>
        /// <param name="cd"></param>
        /// <param name="parameter"></param>
        public ClearCardDataBase(INCommandDetail cd, ClearCardDataBase_Parameter parameter) : base(cd, parameter) { }

        /// <summary>
        /// 创建参数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            ClearCardDataBase_Parameter model = value as ClearCardDataBase_Parameter;
            if (model == null) return false;
            return model.checkedParameter();
        }

        /// <summary>
        /// 创建一个通讯指令
        /// </summary>
        protected override void CreatePacket0()
        {
            Packet(0x07, 0x02, 0x00, 0x01, GetCmdDate());
        }

        /// <summary>
        /// 获取参数结构的字节编码
        /// </summary>
        /// <returns></returns>
        private IByteBuffer GetCmdDate()
        {
            ClearCardDataBase_Parameter model = _Parameter as ClearCardDataBase_Parameter;
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
        /// 命令重发时需要的函数
        /// </summary>
        protected override void CommandReSend()
        {
            return;
        }

        /// <summary>
        /// 命令释放时需要的参数
        /// </summary>
        protected override void Release1()
        {
            return;
        }
    }
}
