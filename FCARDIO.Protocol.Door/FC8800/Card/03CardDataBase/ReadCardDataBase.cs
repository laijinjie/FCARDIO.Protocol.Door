using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;
using FCARDIO.Core.Command;
using FCARDIO.Protocol.FC8800;
using FCARDIO.Protocol.OnlineAccess;

namespace FCARDIO.Protocol.Door.FC8800.Card.CardDataBase
{
    /// <summary>
    /// 从控制器中读取卡片数据<br/>
    /// 成功返回结果参考 @link ReadCardDataBase_Result 
    /// </summary>
    public class ReadCardDataBase
        :FC8800Command
    {
        private int mStep;//指示当前命令进行的步骤
        private ConcurrentLinkedQueue<ByteBuf> mBufs;
        private int mRecordCardSize;//记录的卡数量

        /// <summary>
        /// 初始化命令结构 
        /// </summary>
        /// <param name="cd"></param>
        /// <param name="parameter"></param>
        public ReadCardDataBase(INCommandDetail cd, ReadCardDataBase_Parameter parameter) : base(cd, parameter) { }
        
        /// <summary>
        /// 创建参数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected override bool CheckCommandParameter(INCommandParameter value)
         {
            ReadCardDataBase_Parameter model = value as ReadCardDataBase_Parameter;
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
            ReadCardDataBase_Parameter model = _Parameter as ReadCardDataBase_Parameter;
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
            if (CheckResponse(oPck, 0x04))
            {
                var buf = oPck.CmdData;
                ReadCardDataBase_Result rst = new ReadCardDataBase_Result();
                _Result = rst;
                rst.SetBytes(buf);
                CommandCompleted();
            }
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
