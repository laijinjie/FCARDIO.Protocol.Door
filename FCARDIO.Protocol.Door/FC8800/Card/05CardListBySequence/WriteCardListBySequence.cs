using System;
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
    /// FC88\MC58 将卡片列表写入到控制器非排序区 
    /// </summary>
    public class WriteCardListBySequence
        : FC8800Command
    {
        private int mIndex;//指示当前命令进行的步骤
        private Queue<IByteBuffer> mBufs;

        /// <summary>
        /// 初始化命令结构 
        /// </summary>
        /// <param name="cd"></param>
        /// <param name="perameter">包含需要上传的卡列表参数</param>
        public WriteCardListBySequence(INCommandDetail cd, WriteCardListBySequence_Parameter perameter) : base(cd, perameter) { }

        /// <summary>
        /// 检查参数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            WriteCardListBySequence_Parameter model = value as WriteCardListBySequence_Parameter;
            if (model == null) return false;
            return model.checkedParameter();
        }

        /// <summary>
        /// 创建一个通讯指令
        /// </summary>
        protected override void CreatePacket0()
        {

            var acl = _Connector.GetByteBufAllocator();
            //创建一个通讯缓冲区
            var buf = acl.Buffer((10 * 0x21) + 4);


            WriteCardListBySequence_Parameter model = _Parameter as WriteCardListBySequence_Parameter;


            mIndex = 0;
            WriteCardDetailToBuf(buf);
            _ProcessMax = model.CardList.Count;
            if (model.CardList.Count % 10 > 0) _ProcessMax++;

            Packet(0x07, 0x04, 0x00, (uint)buf.ReadableBytes, buf);
        }

        /// <summary>
        /// 将卡详情写入到ByteBuf中
        /// </summary>
        private void WriteCardDetailToBuf(IByteBuffer buf)
        {
            WriteCardListBySequence_Parameter model = _Parameter as WriteCardListBySequence_Parameter;
            var lst = model.CardList;
            int iCount = lst.Count;//获取列表总长度
            iCount = iCount - mIndex;//计算未上传总数

            int iLen = iCount;
            if (iLen > 10)
            {
                iLen = 10;
            }
            

            buf.WriteInt(iLen);//指示此包包含的卡数量
            for (int i = 0; i < iLen; i++)
            {
                var card = lst[mIndex + i];
                card.GetBytes(buf);
            }

            _ProcessStep+= iLen ;
            mIndex += iLen;
        }

        /// <summary>
        /// 检查是否已写完所有卡
        /// </summary>
        /// <returns></returns>
        private bool IsWriteOver()
        {
            WriteCardListBySequence_Parameter model = _Parameter as WriteCardListBySequence_Parameter;
            int iCount = model.CardList.Count;//获取列表总长度

            return (iCount - mIndex) == 0;
        }

        /// <summary>
        /// 重写父类对处理返回值的定义
        /// </summary>
        /// <param name="oPck"></param>
        protected override void CommandNext0(OnlineAccessPacket oPck)
        {
            if (CheckResponse_OK(oPck))
            {

                //继续发下一包
                CommandNext1(oPck);
            }
            else if (CheckResponse(oPck, 0x07, 0x04, 0xFF, oPck.DataLen))
            {//检查是否不是错误返回值

                //缓存错误返回值
                if(mBufs==null)
                {
                    mBufs = new Queue<IByteBuffer>();
                }

                //mBufs.Enqueue(oPck.CmdData);

                //继续发下一包
                CommandNext1(oPck);
            }
        }


        /// <summary>
        /// 处理返回值
        /// </summary>
        /// <param name="oPck"></param>
        protected override void CommandNext1(OnlineAccessPacket oPck)
        {
            if (IsWriteOver())
            {
                //全部上传完毕
                CommandCompleted();
            }
            else
            {
                //未发送完毕，继续发送
                var buf = FCPacket.CmdData;
                buf.Clear();
                WriteCardDetailToBuf(buf);
                FCPacket.DataLen =(UInt32) buf.ReadableBytes;
                CommandReady();//设定命令当前状态为准备就绪，等待发送
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
