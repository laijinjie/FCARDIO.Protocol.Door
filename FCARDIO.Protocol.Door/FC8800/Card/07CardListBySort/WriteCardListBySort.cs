using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;
using FCARDIO.Core.Command;
using FCARDIO.Protocol.FC8800;
using FCARDIO.Protocol.OnlineAccess;

namespace FCARDIO.Protocol.Door.FC8800.Card.CardListBySort
{
    /// <summary>
    /// 
    /// </summary>
    public class WriteCardListBySort : FC8800Command_WriteParameter
    {
        /// <summary>
        /// 初始化命令结构
        /// </summary>
        /// <param name="cd"></param>
        /// <param name="perameter"></param>
        public WriteCardListBySort(INCommandDetail cd, WriteCardListBySort_Parameter par) : base(cd, par)
        {
        }

        /// <summary>
        /// 检查参数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            WriteCardListBySort_Parameter model = value as WriteCardListBySort_Parameter;
            if (model == null) return false;
            return model.checkedParameter();
        }

        /// <summary>
        /// 创建一个通讯指令
        /// </summary>
        protected override void CreatePacket0()
        {
            WriteCardListBySort_Parameter model = _Parameter as WriteCardListBySort_Parameter;
            var acl = _Connector.GetByteBufAllocator();
            var buf = acl.Buffer(model.GetDataLen());
            model.GetBytes(buf);
            Packet(0x07, 0x07, 0x01, Convert.ToUInt32(model.GetDataLen()), buf);
        }
        /// <summary>
        /// 获取参数结构的字节编码
        /// </summary>
        /// <returns></returns>
        //private IByteBuffer getCmdData()
        //{
        //    WriteCardListBySort_Parameter model = _Parameter as WriteCardListBySort_Parameter;
        //    var acl = _Connector.GetByteBufAllocator();
        //    var buf = acl.Buffer(model.GetDataLen());
        //    model.GetBytes(buf);
        //    return buf;
        //}

        

        /// <summary>
        /// 处理返回值
        /// </summary>
        /// <param name="oPck"></param>
        protected override void CommandNext1(OnlineAccessPacket oPck)
        {
            if (CheckResponse(oPck, 0x05))
            {
                var buf = oPck.CmdData;
                WriteCardListBySort_Result rst = new WriteCardListBySort_Result();
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
