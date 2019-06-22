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
    /// FC88\MC58 将卡片列表写入到控制器排序区 
    /// </summary>
    public class WriteCardListBySort : FC8800Command
    {
        private int mStep;//当前命令进度
        private int mWriteCardIndex;//指示当前命令进行的步骤
        private Queue<IByteBuffer> mBufs;

        /// <summary>
        /// 初始化命令结构
        /// </summary>
        /// <param name="cd"></param>
        /// <param name="perameter"></param>
        public WriteCardListBySort(INCommandDetail cd, WriteCardListBySort_Parameter perameter) : base(cd, perameter) { }

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
        /// 创建一个通讯指令,准备开始写排序区
        /// </summary>
        protected override void CreatePacket0()
        {
            mStep = 1;
            Packet(0x07, 0x07, 0x00);
        }

        /// <summary>
        /// 重写父类对处理返回值的定义
        /// </summary>
        /// <param name="oPck"></param>
        protected override void CommandNext0(OnlineAccessPacket oPck)
        {
            switch (mStep)
            {
                case 1://处理开始写入指令返回
                    if (CheckResponse_OK(oPck))
                    {
                        //硬件已准备就绪，开始写入卡

                        //创建一个通讯缓冲区
                        var acl = _Connector.GetByteBufAllocator();
                        var buf = acl.Buffer((10 * 0x21) + 8);

                        Packet(0x07, 0x07, 0x01, (uint)buf.ReadableBytes, buf);
                        CommandReady();//设定命令当前状态为准备就绪，等待发送
                        mStep = 2;//使命令进入下一个阶段
                        return;
                    }
                    break;
                case 2:
                    if (CheckResponse_OK(oPck))
                    {
                        //继续发下一包
                        CommandNext1(oPck);
                    }
                    else if (CheckResponse(oPck, 0x07, 0x07, 0xFF, oPck.DataLen))
                    {//检查是否不是错误返回值

                        //缓存错误返回值
                        if (mBufs == null)
                        {
                            mBufs = new Queue<IByteBuffer>();
                        }

                        //mBufs.Enqueue(oPck.CmdData);

                        //继续发下一包
                        CommandNext1(oPck);
                    }
                    break;
                case 3:
                    if (CheckResponse_OK(oPck))
                    {
                        //命令全部发送完毕
                        CommandCompleted();
                    }
                    break;
                default:
                    break;
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
                //使命令进入下一个阶段
                Packet(0x07, 0x07, 0x2);
                CommandReady();//设定命令当前状态为准备就绪，等待发送
                mStep = 3;//使命令进入下一个阶段
            }
            else
            {
                //未发送完毕，继续发送
                var buf = FCPacket.CmdData;
                buf.Clear();
                WriteCardDetailToBuf(buf);
                FCPacket.DataLen = (UInt32)buf.ReadableBytes;
                CommandReady();//设定命令当前状态为准备就绪，等待发送
            }
        }



        /// <summary>
        /// 将卡详情写入到ByteBuf中
        /// </summary>
        private void WriteCardDetailToBuf(IByteBuffer buf)
        {
            WriteCardListBySort_Parameter model = _Parameter as WriteCardListBySort_Parameter;
            var lst = model.CardList;
            int iCount = lst.Count;//获取列表总长度
            iCount = iCount - mWriteCardIndex;//计算未上传总数

            int iLen = iCount;
            if (iLen > 10)
            {
                iLen = 10;
            }

            buf.WriteInt(mWriteCardIndex + 1);//指示此次写入的卡起始序号
            buf.WriteInt(iLen);//指示此包包含的卡数量
            for (int i = 0; i < iLen; i++)
            {
                var card = lst[mWriteCardIndex + i];
                card.GetBytes(buf);
            }

            mWriteCardIndex += iLen;
        }



        /// <summary>
        /// 检查是否已写完所有卡
        /// </summary>
        /// <returns></returns>
        private bool IsWriteOver()
        {
            WriteCardListBySort_Parameter model = _Parameter as WriteCardListBySort_Parameter;
            int iCount = model.CardList.Count;//获取列表总长度

            return (iCount - mWriteCardIndex) == 0;
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
