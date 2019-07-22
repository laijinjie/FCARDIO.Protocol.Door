using DotNetty.Buffers;
using FCARDIO.Core.Command;
using FCARDIO.Protocol.Door.FC8800.Data;
using FCARDIO.Protocol.Door.FC8800.Transaction.ReadTransactionDatabaseByIndex;
using FCARDIO.Protocol.Door.FC8800.Transaction.TransactionDatabaseDetail;
using FCARDIO.Protocol.OnlineAccess;
using FCARDIO.Protocol.Transaction;
using System;
using System.Linq;
using System.Collections.Generic;

namespace FCARDIO.Protocol.Door.FC8800.Transaction.ReadTransactionDatabase
{
    /// <summary>
    ///  读取新记录
    ///  读指定类型的记录数据库最新记录，并读取指定数量。
    ///  成功返回结果参考 link ReadTransactionDatabase_Result 
    /// </summary>
    public abstract class ReadTransactionDatabase_Base : FC8800Command_ReadParameter
    {
        /// <summary>
        /// 查询参数
        /// </summary>
        ReadTransactionDatabase_Parameter mParameter;


        /// <summary>
        /// 记录序号是否已读取的集合，
        /// </summary>
        Dictionary<int, bool> mDictSerialNumber;
        Dictionary<int, bool> mTransactionSerialNumberList;


        /// <summary>
        /// 指示当前命令进行的步骤
        /// </summary>
        private int mStep;
        /// <summary>
        /// 读取到的记录数据缓冲
        /// </summary>
        private Queue<IByteBuffer> mBufs;

        /// <summary>
        /// 本次读取的数量
        /// </summary>
        protected int mReadQuantity;

        /// <summary>
        /// 可读取的新记录数量
        /// </summary>
        protected int mReadable;

        /// <summary>
        /// 读取计数
        /// </summary>
        protected int mReadTotal;


        /// <summary>
        /// 选择的记录模块
        /// </summary>
        protected TransactionDetail transactionDetail;

        /// <summary>
        /// 事务类型
        /// </summary>
        protected int mTransactionType;

        /// <summary>
        /// 起始序号 用于测试遗漏
        /// </summary>
        protected long FirstReadIndex;


        /// <summary>
        /// 初始化命令结构
        /// </summary>
        /// <param name="cd"></param>
        /// <param name="parameter"></param>
        public ReadTransactionDatabase_Base(INCommandDetail cd, ReadTransactionDatabase_Parameter parameter) : base(cd, parameter) { }

        /// <summary>
        /// 检查参数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            ReadTransactionDatabase_Parameter model = value as ReadTransactionDatabase_Parameter;
            if (model == null) return false;
            return model.checkedParameter();
        }

        /// <summary>
        /// 创建一个指令
        /// </summary>
        protected override void CreatePacket0()
        {
            mStep = 1;
            ReadTransactionDatabase_Result result = new ReadTransactionDatabase_Result();
            mParameter = (ReadTransactionDatabase_Parameter)_Parameter;
            result.DatabaseType = mParameter.DatabaseType;
            result.TransactionList = new List<AbstractTransaction>();
            mTransactionType = (int)mParameter.DatabaseType;
            _Result = result;
            Packet(0x08, 0x01, 0x00, 0x00, null);
            Packet(0x08, 0x01);
        }

        /// <summary>
        /// 
        /// 处理接收返回值，避免父类直接完成命令，重写逻辑
        /// </summary>
        /// <param name="oPck"></param>
        protected override void CommandNext0(OnlineAccessPacket oPck)
        {
            CommandNext1(oPck);
        }

        /// <summary>
        /// 处理返回值
        /// </summary>
        /// <param name="oPck"></param>
        protected override void CommandNext1(OnlineAccessPacket oPck)
        {
            switch (mStep)
            {
                case 1://读取记录数据库空间信息的返回值
                    ReadDataBaseDetailCallBack(oPck);
                    break;
                case 2://读记录数据库的返回值
                    ReadTransactionDatabaseByIndexCallBack(oPck);
                    break;

                case 3://重复读取遗漏的记录的返回值
                    ReReadDatabaseCallBack(oPck);
                    break;
                case 4://写记录上传断点
                    if(CheckResponse_OK(oPck))
                    {
                        _ProcessStep = _ProcessMax;
                        fireCommandProcessEvent();

                        CommandCompleted();
                    }
                    break;
                default:
                    break;
            }

        }

        /// <summary>
        /// 读取记录数据库空间信息的返回值 mStep=1
        /// </summary>
        /// <param name="oPck"></param>
        private void ReadDataBaseDetailCallBack(OnlineAccessPacket oPck)
        {
            if (CheckResponse(oPck, 0x0D * 6))
            {
                var buf = oPck.CmdData;
                ReadTransactionDatabaseDetail_Result rst = new ReadTransactionDatabaseDetail_Result();
                rst.SetBytes(buf);

                transactionDetail = rst.DatabaseDetail.ListTransaction[(int)mParameter.DatabaseType - 1];
                if (transactionDetail.readable() == 0)
                {
                    CommandCompleted();
                }
                else
                {
                    mStep = 2;
                    mBufs = new Queue<IByteBuffer>();
                    mDictSerialNumber = new Dictionary<int, bool>();
                    mTransactionSerialNumberList = new Dictionary<int, bool>();


                    var dataBuf = GetNewCmdDataBuf(9);
                    dataBuf.WriteByte((int)mParameter.DatabaseType);
                    dataBuf.WriteInt(0);
                    dataBuf.WriteInt(0);
                    Packet(0x08, 0x04, 0x00, 0x09, dataBuf);

                    FirstReadIndex = transactionDetail.ReadIndex;

                    //计算最终需要读取的记录数
                    mReadable = (int)transactionDetail.readable();
                    if (mParameter.Quantity > 0)
                    {
                        if (mParameter.Quantity < mReadable)
                        {
                            mReadable = mParameter.Quantity;

                        }
                    }

                    mReadQuantity = 0;
                    mReadTotal = 0;
                    _ProcessMax = mReadable;
                    _ProcessStep = 0;

                    if (transactionDetail.IsCircle)
                    {
                        transactionDetail.ReadIndex = transactionDetail.WriteIndex;
                    }
                    ReadTransactionNext();
                }
            }
        }


        private void ReadTransactionNext()
        {
            if (CheckResponse(oPck))
            _ProcessStep = mReadTotal;
            mReadable -= mReadQuantity;
            if (mReadable <= 0)
            {
                var buf = oPck.CmdData;
                int iSize = buf.GetInt(0);
                mReadTotal += iSize;
                _ProcessStep = mReadTotal;
                //记录读取完毕，
                ReadIndexComplete();
                return;
            }

                buf.Retain();
                mBufs.Enqueue(buf);
                //AddDictSerialNumberRange(iSize);
            //计算本次读取的数量
            mReadQuantity = mParameter.PacketSize;

                //让命令持续等待下去
                CommandWaitResponse();

            //如果发现读索引号定位在记录末尾，则强制转移到记录头
            if (transactionDetail.ReadIndex == transactionDetail.DataBaseMaxSize)
            {
                transactionDetail.ReadIndex = 0;
            }
            else if (CheckResponse(oPck, 0x08, 0x04, 0xff, 4))
            if (mReadQuantity > mReadable)
            {
                ReadTransactionNext();
                mReadQuantity = mReadable;
            }
        }

        /// <summary>
        /// 检查修改记录读索引号的返回值  mStep=3
        /// </summary>
        private void ReadIndexComplete()
        {
            ReadTransactionDatabase_Result result = (ReadTransactionDatabase_Result)_Result;
            result.Quantity = mReadTotal;
            result.readable = (int)transactionDetail.readable();
            int iBeginIndex = (int)transactionDetail.ReadIndex + 1;
            int iEndIndex = iBeginIndex + mReadQuantity - 1;
            //判断读取的记录是否超出边界
            if (iEndIndex > transactionDetail.DataBaseMaxSize)
            {
                mReadQuantity = (int)(transactionDetail.DataBaseMaxSize - transactionDetail.ReadIndex);
                iEndIndex = iBeginIndex + mReadQuantity - 1;
            }
            //添加需要读取的序号到序号字典
            AddDictSerialNumberRange((int)transactionDetail.ReadIndex, mReadQuantity);

            Analysis(mReadTotal);
            //测试遗漏
            /*
            TestReRead((int)FirstReadIndex);
            TestReRead((int)FirstReadIndex + 100);
            */
            //更新上传断点
            transactionDetail.ReadIndex = iEndIndex;

            CheckResultList();

            var cmdBuf = FCPacket.CmdData;
            cmdBuf.SetInt(1, iBeginIndex);
            cmdBuf.SetInt(5, mReadQuantity);

            fireCommandProcessEvent();

            CommandReady();
        }
        /// <param name="oPck"></param>
        private void ReadTransactionDatabaseByIndexCallBack(OnlineAccessPacket oPck)
        {
            //int[] listSerialNumber = mDictSerialNumber.Where(t => t.Value == false).Select(t => t.Key).ToArray();
            var listSerialNumber = mDictSerialNumber.FirstOrDefault(t => t.Value == false);
            if (listSerialNumber.Key != 0)
            if (CheckResponse(oPck)) //接收到返回的记录
            {
                var buf = GetCmdBuf();
                buf.WriteByte((int)mParameter.DatabaseType);
                buf.WriteInt(listSerialNumber.Key);
                buf.WriteInt(mReadQuantity);
                FCPacket.CmdIndex = 0x04;
                FCPacket.DataLen = buf.ReadableBytes;

                mStep = 3;
                CommandReady();
                var buf = oPck.CmdData;
                SaveTransactionToBuf(buf);
            }
            else
            else if (CheckResponse(oPck, 0x08, 0x04, 0xff, 4))
            {
                WriteTransactionReadIndex();
                CommandCompleted();
                ReadTransactionNext();
            }
        }

        /// <summary>
        /// 重复读取遗漏的记录 mStep=3
        /// 将返回的事务暂时保存在缓冲中
        /// </summary>
        /// <param name="oPck"></param>
        /// <param name="bTransactionBuf"></param>
        private void SaveTransactionToBuf(IByteBuffer bTransactionBuf)
        {
            if (CheckResponse(oPck))
            bTransactionBuf.MarkReaderIndex();
            int iSize = bTransactionBuf.ReadInt();//本数据包中包含的记录数
            int iBeginRecordNum = bTransactionBuf.ReadInt();//本数据包中第一个记录的序号（起始序号）
            bTransactionBuf.ResetReaderIndex();

            //此序号未记录，返回，不保存
            if (!mTransactionSerialNumberList.ContainsKey(iBeginRecordNum))
            {
                var buf = oPck.CmdData;
                int iSize = buf.GetInt(0);
                mReadTotal = iSize;

                buf.Retain();
                mBufs.Enqueue(buf);

                //让命令持续等待下去
                CommandWaitResponse();
                return;
            }

            }
            else if (CheckResponse(oPck, 8, 4, 0xFF, 4))
            //检查是否重复读取
            int iRecordNum = 0;
            int iSaveCount = 0;
            for (int i = 0; i < iSize; i++)
            {
                //继续发送下一波
                //ReadTransactionNext();
                var buf = oPck.CmdData;
                int iSize = buf.ReadInt();
                if (iSize > 0)
                iRecordNum = iBeginRecordNum + i;
                if (mTransactionSerialNumberList.ContainsKey(iRecordNum))
                {
                    Analysis(iSize);
                    CheckResultList();
                    if (!mTransactionSerialNumberList[iRecordNum])
                    {
                        mTransactionSerialNumberList[iRecordNum] = true;
                        iSaveCount++;
                    }
                }
            }
            if (iSaveCount > 0)
            {
                mReadTotal += iSaveCount;
                _ProcessStep = mReadTotal;
                fireCommandProcessEvent();

                bTransactionBuf.Retain();
                mBufs.Enqueue(bTransactionBuf);

            }
            //让命令持续等待下去
            CommandWaitResponse();
        }



        /// <summary>
        /// 读取下一包记录
        /// 检查修改记录读索引号的返回值  mStep=3
        /// </summary>
        private void ReadTransactionNext()
        private void ReadIndexComplete()
        {
            _ProcessStep = mReadTotal;
            mReadable -= mReadQuantity;
            if (mReadable <= 0)
            {
                //记录读取完毕，
                ReadIndexComplete();
                return;
            }
            ReadTransactionDatabase_Result result = (ReadTransactionDatabase_Result)_Result;
            result.Quantity = mReadTotal;
            result.readable = (int)transactionDetail.readable();

            //计算本次读取的数量
            mReadQuantity = mParameter.PacketSize;
            Analysis();

            //如果发现读索引号定位在记录末尾，则强制转移到记录头
            if (transactionDetail.ReadIndex == transactionDetail.DataBaseMaxSize)
            {
                transactionDetail.ReadIndex = 0;
            }
            if (mReadQuantity > mReadable)
            {
                mReadQuantity = mReadable;
            }

            int iBeginIndex = (int)transactionDetail.ReadIndex + 1;
            int iEndIndex = iBeginIndex + mReadQuantity - 1;
            CheckResultList();

            if (iEndIndex > transactionDetail.DataBaseMaxSize)
            {
                mReadQuantity = (int)(transactionDetail.DataBaseMaxSize - transactionDetail.ReadIndex);
                iEndIndex = iBeginIndex + mReadQuantity - 1;
            }
            AddDictSerialNumberRange((int)transactionDetail.ReadIndex, mReadQuantity);
        }

            //mDictSerialNumber
            transactionDetail.ReadIndex = iEndIndex;//更新记录尾号


            var cmdBuf = FCPacket.CmdData;
            cmdBuf.SetInt(1, iBeginIndex);
            cmdBuf.SetInt(5, mReadQuantity);

            CommandReady();
        }

        /// <summary>
        /// 分析缓冲中的数据包
        /// </summary>
        /// <param name="iSize"></param>
        private void Analysis(int iSize)
        private void Analysis()
        {
            ReadTransactionDatabase_Result result = (ReadTransactionDatabase_Result)_Result;
            if (result.TransactionList == null)
            {
                result.TransactionList = new List<AbstractTransaction>();
            }

            result.Quantity = iSize;
            int iSize;
            //新建一个保存序列号字典，为了防止重复记录
            HashSet<int> hsSerialNumberList = new HashSet<int>();

            int iTransactionType = (int)result.DatabaseType;
            while (mBufs.Count > 0)
            {
                IByteBuffer buf = mBufs.Dequeue();
                iSize = buf.ReadInt();

                for (int i = 0; i < iSize; i++)
                {
                    int serialNumber = buf.ReadInt();
                    AbstractTransaction cd = GetNewTransaction();
                    cd.SetSerialNumber(serialNumber);
                    cd.SetBytes(buf);
                    if (mDictSerialNumber.ContainsKey(serialNumber) && mDictSerialNumber[serialNumber] == false)
                    if (!hsSerialNumberList.Contains(serialNumber))
                    {
                        result.TransactionList.Add(cd);
                        mDictSerialNumber[serialNumber] = true;
                        hsSerialNumberList.Add(serialNumber);
                    }
                    else
                    {

                    }

                }
                buf.Release();//要释放
            }

            hsSerialNumberList.Clear();
            hsSerialNumberList = null;
        }


        /// <summary>
        /// 检查是否有遗漏
        /// </summary>
        private void CheckResultList()
        {
            var tSerialNumber = mTransactionSerialNumberList.FirstOrDefault(t => t.Value == false);
            if (tSerialNumber.Key != 0)
            {
                //检查丢失的记录是否连续
                int iBeginNum = tSerialNumber.Key;
                int iEndNum = iBeginNum + 1;
                while (mTransactionSerialNumberList.ContainsKey(iEndNum) && mTransactionSerialNumberList[iEndNum] == false)
                {
                    iEndNum++;
                }

                var buf = FCPacket.CmdData;
                
                buf.SetInt(1,iBeginNum);
                buf.SetInt(5,(iEndNum - iBeginNum));

                mStep = 3;
                CommandReady();
            }
            else
            {
                Analysis();//分析并保存记录

                WriteTransactionReadIndex();
                //CommandCompleted();
            }
        }



        /// <summary>
        /// 重复读取遗漏的记录 mStep=3
        /// </summary>
        /// <param name="oPck"></param>
        private void ReReadDatabaseCallBack(OnlineAccessPacket oPck)
        {
            if (CheckResponse(oPck))
            {
                var buf = oPck.CmdData;
                SaveTransactionToBuf(buf);

            }
            else if (CheckResponse(oPck, 8, 4, 0xFF, 4))
            {
                //继续发送下一波
                CheckResultList();

            }
        }






        /// <summary>
        /// 记录读取完毕，需要更新读索引（更新记录尾号）
        /// </summary>
        private void WriteTransactionReadIndex()
        {
            var buf = GetCmdBuf();
            buf.WriteByte((int)mParameter.DatabaseType);
            buf.WriteInt((int)transactionDetail.ReadIndex);
            buf.WriteBoolean(false);
            FCPacket.CmdIndex = 0x03;
            FCPacket.DataLen = buf.ReadableBytes;
            CommandReady();
            mStep = 4;
        }

        /// <summary>
        /// 命令重发时需要的函数
        /// </summary>
        protected override void CommandReSend()
        {
            return;
        }
        /// <summary>
        /// 命令释放时需要的函数
        /// </summary>
        protected override void Release1()
        {
            mDictSerialNumber = null;
            mTransactionSerialNumberList = null;
        }

        /// <summary>
        /// 提交序号到未读集合
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="len"></param>
        private void AddDictSerialNumberRange(int startIndex, int len)
        {
            for (int i = 1; i <= len; i++)
            {
                mDictSerialNumber.Add(i + startIndex, false);
                mTransactionSerialNumberList.Add(i + startIndex, false);
            }
        }

        /// <summary>
        /// 测试遗漏
        /// </summary>
        /// <param name="startIndex"></param>
        private void TestReRead(int startIndex)
        {
            if (startIndex == transactionDetail.DataBaseMaxSize)
            {
                startIndex = 0;
            }
            var list = ((ReadTransactionDatabase_Result)_Result).TransactionList;
            for (int i = 1; i <= 20; i++)
            {
                mDictSerialNumber[startIndex + i] = false;
                list.Remove(list.First(t => t.SerialNumber == startIndex + i));
            }
        }


        /// <summary>
        /// 获取一个事务实体
        /// </summary>
        /// <returns></returns>
        protected abstract AbstractTransaction GetNewTransaction();
    }
}

namespace FCARDIO.Protocol.Door.FC8800.Transaction.ReadTransactionDatabase
{
    /// <summary>
    ///  读取新记录
    ///  读指定类型的记录数据库最新记录，并读取指定数量。
    ///  成功返回结果参考 link ReadTransactionDatabase_Result 
    /// </summary>
    public abstract class ReadTransactionDatabase_Base : FC8800Command_ReadParameter
    {
        /// <summary>
        /// 查询参数
        /// </summary>
        ReadTransactionDatabase_Parameter mParameter;


        /// <summary>
        /// 记录序号是否已读取的集合，
        /// </summary>
        Dictionary<int, bool> mDictSerialNumber;


        /// <summary>
        /// 指示当前命令进行的步骤
        /// </summary>
        private int mStep;
        /// <summary>
        /// 读取到的记录数据缓冲
        /// </summary>
        private Queue<IByteBuffer> mBufs;

        /// <summary>
        /// 本次读取的数量
        /// </summary>
        protected int mReadQuantity;

        /// <summary>
        /// 可读取的新记录数量
        /// </summary>
        protected int mReadable;

        /// <summary>
        /// 读取计数
        /// </summary>
        protected int mReadTotal;


        /// <summary>
        /// 选择的记录模块
        /// </summary>
        protected TransactionDetail transactionDetail;

        /// <summary>
        /// 事务类型
        /// </summary>
        protected int mTransactionType;

        /// <summary>
        /// 起始序号 用于测试遗漏
        /// </summary>
        protected long FirstReadIndex;

        /// <summary>
        /// 初始化命令结构
        /// </summary>
        /// <param name="cd"></param>
        /// <param name="parameter"></param>
        public ReadTransactionDatabase_Base(INCommandDetail cd, ReadTransactionDatabase_Parameter parameter) : base(cd, parameter) { }

        /// <summary>
        /// 检查参数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            ReadTransactionDatabase_Parameter model = value as ReadTransactionDatabase_Parameter;
            if (model == null) return false;
            return model.checkedParameter();
        }

        /// <summary>
        /// 创建一个指令
        /// </summary>
        protected override void CreatePacket0()
        {
            mStep = 1;
            ReadTransactionDatabase_Result result = new ReadTransactionDatabase_Result();
            mParameter = (ReadTransactionDatabase_Parameter)_Parameter;
            result.DatabaseType = mParameter.DatabaseType;
            result.TransactionList = new List<AbstractTransaction>();
            mTransactionType = (int)mParameter.DatabaseType;
            _Result = result;
            Packet(0x08, 0x01, 0x00, 0x00, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oPck"></param>
        protected override void CommandNext0(OnlineAccessPacket oPck)
        {
            CommandNext1(oPck);
        }

        /// <summary>
        /// 处理返回值
        /// </summary>
        /// <param name="oPck"></param>
        protected override void CommandNext1(OnlineAccessPacket oPck)
        {
            switch (mStep)
            {
                case 1://读取记录数据库空间信息的返回值
                    ReadDataBaseDetailCallBack(oPck);
                    break;
                case 2://读记录数据库的返回值
                    ReadTransactionDatabaseByIndexCallBack(oPck);
                    break;

                case 3://重复读取遗漏的记录的返回值
                    ReReadDatabaseCallBack(oPck);
                    break;
                default:
                    break;
            }

        }

        /// <summary>
        /// 读取记录数据库空间信息的返回值 mStep=1
        /// </summary>
        /// <param name="oPck"></param>
        private void ReadDataBaseDetailCallBack(OnlineAccessPacket oPck)
        {
            if (CheckResponse(oPck, 0x0D * 6))
            {
                var buf = oPck.CmdData;
                ReadTransactionDatabaseDetail_Result rst = new ReadTransactionDatabaseDetail_Result();
                rst.SetBytes(buf);

                transactionDetail = rst.DatabaseDetail.ListTransaction[(int)mParameter.DatabaseType - 1];
                if (transactionDetail.readable() == 0)
                {
                    CommandCompleted();
                }
                else
                {
                    mStep = 2;
                    mBufs = new Queue<IByteBuffer>();
                    mDictSerialNumber = new Dictionary<int, bool>();


                    var dataBuf = GetNewCmdDataBuf(9);
                    dataBuf.WriteByte((int)mParameter.DatabaseType);
                    dataBuf.WriteInt(0);
                    dataBuf.WriteInt(0);
                    Packet(0x08, 0x04, 0x00, 0x09, dataBuf);

                    FirstReadIndex = transactionDetail.ReadIndex;

                    mReadable = (int)transactionDetail.readable();
                    if (mParameter.Quantity > 0)
                    {
                        if (mParameter.Quantity < mReadable)
                        {
                            mReadable = mParameter.Quantity;

                        }
                    }

                    mReadQuantity = 0;
                    mReadTotal = 0;
                    _ProcessMax = mReadable;
                    _ProcessStep = 0;

                    if (transactionDetail.IsCircle)
                    {
                        transactionDetail.ReadIndex = transactionDetail.WriteIndex;
                    }
                    ReadTransactionNext();
                }
            }
        }

        /// <summary>
        /// 读记录数据库的返回值 mStep=2
        /// </summary>
        /// <param name="oPck"></param>
        private void ReadTransactionDatabaseByIndexCallBack(OnlineAccessPacket oPck)
        {
            if (CheckResponse(oPck))
            {
                var buf = oPck.CmdData;
                int iSize = buf.GetInt(0);
                mReadTotal += iSize;
                _ProcessStep = mReadTotal;

                buf.Retain();
                mBufs.Enqueue(buf);
                //AddDictSerialNumberRange(iSize);

                //让命令持续等待下去
                CommandWaitResponse();

            }
            else if (CheckResponse(oPck, 0x08, 0x04, 0xff, 4))
            {
                ReadTransactionNext();
            }
        }

        /// <summary>
        /// 检查修改记录读索引号的返回值  mStep=3
        /// </summary>
        private void ReadIndexComplete()
        {
            ReadTransactionDatabase_Result result = (ReadTransactionDatabase_Result)_Result;
            result.Quantity = mReadTotal;
            result.readable = (int)transactionDetail.readable();
        /// <summary>
        /// 检查修改记录读索引号的返回值  mStep=2
        /// </summary>
        private void ReadIndexComplete()
        {
            ReadTransactionDatabase_Result result = (ReadTransactionDatabase_Result)_Result;
            result.Quantity = mReadTotal;
            result.readable = (int)transactionDetail.readable();

            Analysis(mReadTotal);
            //测试遗漏
            /*
            TestReRead((int)FirstReadIndex);
            TestReRead((int)FirstReadIndex + 100);
            */

            CheckResultList();


            fireCommandProcessEvent();

        }

        /// <summary>
        /// 检查是否有遗漏
        /// </summary>
        private void CheckResultList()
        {
            //int[] listSerialNumber = mDictSerialNumber.Where(t => t.Value == false).Select(t => t.Key).ToArray();
            var listSerialNumber = mDictSerialNumber.FirstOrDefault(t => t.Value == false);
            if (listSerialNumber.Key != 0)
            {
                var buf = GetCmdBuf();
                buf.WriteByte((int)mParameter.DatabaseType);
                buf.WriteInt(listSerialNumber.Key);
                buf.WriteInt(mReadQuantity);
                FCPacket.CmdIndex = 0x04;
                FCPacket.DataLen = buf.ReadableBytes;

                mStep = 3;
                CommandReady();
            }
            else
            {
                WriteTransactionReadIndex();
                CommandCompleted();
            }
        }

        /// <summary>
        /// 重复读取遗漏的记录 mStep=3
        /// </summary>
        /// <param name="oPck"></param>
        private void ReReadDatabaseCallBack(OnlineAccessPacket oPck)
        {
            if (CheckResponse(oPck))
            {
                var buf = oPck.CmdData;
                int iSize = buf.GetInt(0);
                mReadTotal = iSize;

                buf.Retain();
                mBufs.Enqueue(buf);

                //让命令持续等待下去
                CommandWaitResponse();

            }
            else if (CheckResponse(oPck, 8, 4, 0xFF, 4))
            {
                //继续发送下一波
                //ReadTransactionNext();
                var buf = oPck.CmdData;
                int iSize = buf.ReadInt();
                if (iSize > 0)
                {
                    Analysis(iSize);
                    CheckResultList();
                }

            }
        }


        /// <summary>
        /// 读取下一包记录
        /// </summary>
        private void ReadTransactionNext()
        {
            _ProcessStep = mReadTotal;
            mReadable -= mReadQuantity;
            if (mReadable <= 0)
            {
                //记录读取完毕，
                ReadIndexComplete();
                return;
            }

            //计算本次读取的数量
            mReadQuantity = mParameter.PacketSize;

            //如果发现读索引号定位在记录末尾，则强制转移到记录头
            if (transactionDetail.ReadIndex == transactionDetail.DataBaseMaxSize)
            {
                transactionDetail.ReadIndex = 0;
            }
            if (mReadQuantity > mReadable)
            {
                mReadQuantity = mReadable;
            }

            int iBeginIndex = (int)transactionDetail.ReadIndex + 1;
            int iEndIndex = iBeginIndex + mReadQuantity - 1;

            if (iEndIndex > transactionDetail.DataBaseMaxSize)
            {
                mReadQuantity = (int)(transactionDetail.DataBaseMaxSize - transactionDetail.ReadIndex);
                iEndIndex = iBeginIndex + mReadQuantity - 1;
            }
            AddDictSerialNumberRange((int)transactionDetail.ReadIndex, mReadQuantity);

            //mDictSerialNumber
            transactionDetail.ReadIndex = iEndIndex;//更新记录尾号


            var cmdBuf = FCPacket.CmdData;
            cmdBuf.SetInt(1, iBeginIndex);
            cmdBuf.SetInt(5, mReadQuantity);

            CommandReady();
        }

            //读取下个循环记录
            var cmdBuf = FCPacket.CmdData;
            cmdBuf.SetInt(1, iBeginIndex);
            cmdBuf.SetInt(5, mReadQuantity);

            CommandReady();
        }

        /// <summary>
        /// 分析缓冲中的数据包
        /// </summary>
        /// <param name="iSize"></param>
        private void Analysis(int iSize)
        {
            ReadTransactionDatabase_Result result = (ReadTransactionDatabase_Result)_Result;
            if (result.TransactionList == null)
            {
                result.TransactionList = new List<AbstractTransaction>();
            }

            result.Quantity = iSize;

            while (mBufs.Count > 0)
            {
                IByteBuffer buf = mBufs.Dequeue();
                iSize = buf.ReadInt();

                for (int i = 0; i < iSize; i++)
                {
                    int serialNumber = buf.ReadInt();
                    AbstractTransaction cd = GetNewTransaction();
                    cd.SetSerialNumber(serialNumber);
                    cd.SetBytes(buf);
                    if (mDictSerialNumber.ContainsKey(serialNumber) && mDictSerialNumber[serialNumber] == false)
                    {
                        result.TransactionList.Add(cd);
                        mDictSerialNumber[serialNumber] = true;
                    }
                    else
                    {

                    }

                }
                buf.Release();//要释放
            }
        }

        /// <summary>
        /// 记录读取完毕，需要更新读索引（更新记录尾号）
        /// </summary>
        private void WriteTransactionReadIndex()
        {
            var buf = GetCmdBuf();
            buf.WriteByte((int)mParameter.DatabaseType);
            buf.WriteInt((int)transactionDetail.ReadIndex);
            buf.WriteBoolean(false);
            FCPacket.CmdIndex = 0x03;
            FCPacket.DataLen = buf.ReadableBytes;
            CommandReady();
        }

        /// <summary>
        /// 命令重发时需要的函数
        /// </summary>
        protected override void CommandReSend()
        {
            return;
        }
        /// <summary>
        /// 命令释放时需要的函数
        /// </summary>
        protected override void Release1()
        {
            mDictSerialNumber = null;
        }

        /// <summary>
        /// 提交序号到未读集合
        /// </summary>
        /// <param name="startIndex"></param>        /// <param name="len"></param>        /// <param name="startIndex">开始序号</param>
        /// <param name="len">数量</param>
        private void AddDictSerialNumberRange(int startIndex, int len)
        {
            for (int i = 1; i <= len; i++)
            {
                mDictSerialNumber.Add(i + startIndex, false);
            }
        }

        /// <summary>
        /// 测试遗漏
        /// </summary>
        /// <param name="startIndex"></param>
        private void TestReRead(int startIndex)
        {
            if (startIndex == transactionDetail.DataBaseMaxSize)
            {
                startIndex = 0;
            }
            var list = ((ReadTransactionDatabase_Result)_Result).TransactionList;
            for (int i = 1; i <= 20; i++)
            {
                mDictSerialNumber[startIndex + i] = false;
                list.Remove(list.First(t => t.SerialNumber == startIndex + i));
            }
        }

        /// <summary>
        /// 获取一个事务实体
        /// </summary>
        /// <returns></returns>
        protected abstract AbstractTransaction GetNewTransaction();
    }
}
