using DotNetty.Buffers;
using DoNetDrive.Core.Command;
using DoNetDrive.Protocol.OnlineAccess;
using DoNetDrive.Protocol.POS.CardType.ReadDataBase;
using DoNetDrive.Protocol.POS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoNetDrive.Protocol.POS.CardType
{
    public abstract class WriteCardTypeBase : Write_Command 
    {/// <summary>
     /// 指令分类
     /// </summary>
        protected byte CmdType;

        /// <summary>
        /// 返回指令分类
        /// </summary>
        protected byte CheckResponseCmdType;

        /// <summary>
        /// 1个写入参数长度
        /// </summary>
        protected int mParDataLen;

        /// <summary>
        /// 1个删除参数长度
        /// </summary>
        protected int mDeleteDataLen;
        /// <summary>
        /// 参数
        /// </summary>
        CardType_Parameter_Base mPar;

        /// <summary>
        /// 每次上传数量
        /// </summary>
        protected int mBatchCount = 5;

        /// <summary>
        /// 已上传数量
        /// </summary>
        protected int mIndex;

        /// <summary>
        /// 默认的缓冲区大小
        /// </summary>
        protected int MaxBufSize = 350;

        /// <summary>
        /// 需要写入密码数
        /// </summary>
        protected int maxCount = 0;

        /// <summary>
        /// 保存写入失败的数据缓冲区
        /// </summary>
        protected Queue<IByteBuffer> mBufs = null;

        /// <summary>
        /// 初始化命令结构 
        /// </summary>
        /// <param name="cd"></param>
        /// <param name="parameter">包含需要上传的密码列表参数</param>
        public WriteCardTypeBase(INCommandDetail cd, CardType_Parameter_Base parameter) : base(cd, parameter)
        {
            mPar = parameter;
            CardTypeDetail model = new CardTypeDetail();
            mParDataLen = model.GetDataLen();
            mDeleteDataLen = model.GetDeleteDataLen();
            CmdType = 0x08;
            CheckResponseCmdType = 0x08;
        }

        /// <summary>
        /// 将命令打包成一个Packet，准备发送
        /// </summary>
        protected override void CreatePacket0()
        {
            maxCount = mPar.CardTypeList.Count;
            CreateCommandPacket0();
            _ProcessMax = maxCount;
        }

        /// <summary>
        /// 继承类具体实现
        /// </summary>
        protected abstract void CreateCommandPacket0();

        /// <summary>
        /// 检查参数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            CardTypeDetail model = value as CardTypeDetail;
            
            return model != null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="databuf"></param>
        /// <returns></returns>
        public IByteBuffer WriteCardTypeToBuf(IByteBuffer databuf)
        {

            var lst = mPar.CardTypeList;
            int iCount = lst.Count;//获取列表总长度
            iCount = iCount - mIndex;//计算未上传总数

            int iLen = iCount;
            if (iLen > mBatchCount)
            {
                iLen = mBatchCount;
            }

            databuf.Clear();

            databuf.WriteInt(iLen);
            for (int i = 0; i < iLen; i++)
            {
                WriteCardTypeBodyToBuf(databuf, lst[mIndex + i]);
            }

            mIndex += iLen;
            _ProcessStep += iLen;
            return databuf;
        }

        /// <summary>
        /// 将数据部分写入到缓冲区
        /// </summary>
        /// <param name="databuf"></param>
        /// <param name="password">要写入到缓冲区的密码</param>
        protected abstract void WriteCardTypeBodyToBuf(IByteBuffer databuf, CardTypeDetail cardType);

        /**/
        /// <summary>
        /// 命令超时
        /// </summary>
        protected override void CommandReSend()
        {
            //mIndex -= mBatchCount;
            //var buf = GetCmdBuf();
            //WritePasswordToBuf(buf);
            //FCPacket.DataLen = (UInt32)buf.ReadableBytes;
            //CommandReady();//设定命令当前状态为准备就绪，等待发送
        }


        /// <summary>
        /// 处理返回值
        /// </summary>
        /// <param name="oPck"></param>
        protected override void CommandNext1(OnlineAccessPacket oPck)
        {
            if (IsWriteOver())
            {
                Create_Result();
                CommandCompleted();
            }
            else
            {
                //未发送完毕，继续发送
                var buf = GetCmdBuf();
                WriteCardTypeToBuf(buf);
                FCPacket.DataLen = buf.ReadableBytes;
                CommandReady();//设定命令当前状态为准备就绪，等待发送
            }
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
            else if (CheckResponse(oPck, CheckResponseCmdType, 0x04, 0xFF, oPck.DataLen))
            {//检查是否不是错误返回值

                //缓存错误返回值
                if (mBufs == null)
                {
                    mBufs = new Queue<IByteBuffer>();
                }
                oPck.CmdData.Retain();
                mBufs.Enqueue(oPck.CmdData);

                //继续发下一包
                CommandNext1(oPck);
            }
        }

        /// <summary>
        /// 创建命令成功返回值
        /// </summary>
        protected virtual void Create_Result()
        {
            //无法写入的密码数量
            int FailTotal = 0;

            //无法写入的密码列表
            List<CardTypeDetail> CardTypeDetailList = new List<CardTypeDetail>();


            if (mBufs != null)
            {
                foreach (var buf in mBufs)
                {
                    int iCount = buf.ReadInt();
                    FailTotal += iCount;

                    for (int i = 0; i < iCount; i++)
                    {
                        ReadCardTypeByFailBuf(CardTypeDetailList, buf);
                    }

                    buf.Release();
                }
                ReadDataBase_Result result = CreateResult(CardTypeDetailList);
                _Result = result;
            }



        }

        /// <summary>
        /// 创建返回值
        /// </summary>
        /// <param name="passwordList">控制器返回的密码集合</param>
        protected abstract ReadDataBase_Result CreateResult(List<CardTypeDetail> cardTypeDetailList);

        /// <summary>
        /// 用来解析返回的错误密码数据
        /// </summary>
        CardTypeDetail _CardTypeDetail;

        /// <summary>
        /// 从错误密码列表中读取一个错误密码，加入到passwordList中
        /// </summary>
        /// <param name="passwordList">错误密码列表</param>
        /// <param name="buf"></param>
        private void ReadCardTypeByFailBuf(List<CardTypeDetail> cardTypeDetailList, IByteBuffer buf)
        {
            if (_CardTypeDetail == null) _CardTypeDetail = new CardTypeDetail();
            _CardTypeDetail.SetBytes(buf);
            cardTypeDetailList.Add(_CardTypeDetail);
        }

        /// <summary>
        /// 检查是否已写完所有卡
        /// </summary>
        /// <returns></returns>
        protected bool IsWriteOver()
        {
            int iCount = mPar.CardTypeList.Count;//获取列表总长度

            return (iCount - mIndex) == 0;
        }
    }
}
