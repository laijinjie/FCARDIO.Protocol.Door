 using DoNetDrive.Core.Command;
using DoNetDrive.Protocol.Door.Door8800;
using DoNetDrive.Protocol.Fingerprint.AdditionalData;
using DoNetDrive.Protocol.Fingerprint.Data;
using DoNetDrive.Protocol.Fingerprint.Data.Transaction;
using DoNetDrive.Protocol.OnlineAccess;
using DotNetty.Buffers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoNetDrive.Protocol.Fingerprint.Transaction
{
    /// <summary>
    /// 读取认证记录及附加数据（体温，照片）
    /// </summary>
    public class ReadTransactionAndImageDatabase : Door8800Command_ReadParameter
    {
        /// <summary>
        /// 
        /// </summary>
        protected byte CmdType;

        /// <summary>
        /// 
        /// </summary>
        protected byte CheckResponseCmdType;

        /// <summary>
        /// 查询参数
        /// </summary>
        protected ReadTransactionAndImageDatabase_Parameter mParameter;

        /// <summary>
        /// 记录详情
        /// </summary>
        DoNetDrive.Protocol.Door.Door8800.Data.TransactionDetail[] mTransactionDetailList;

        /// <summary>
        /// 记录序号是否已读取的集合，
        /// </summary>
        Dictionary<int, bool> mTransactionSerialNumberList;

        /// <summary>
        /// 体温记录序号是否已读取的集合，
        /// </summary>
        Dictionary<int, bool> mBodyTemperatureTransactionSerialNumberList;

        /// <summary>
        /// 指示当前命令进行的步骤
        /// </summary>
        private int mStep;

        /// <summary>
        /// 指示当前下载照片的记录序号
        /// </summary>
        private int mSaveFileSerialNumber;

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
        protected Protocol.Door.Door8800.Data.TransactionDetail _TransactionDetail;

        /// <summary>
        /// 事务类型
        /// </summary>
        protected int mTransactionType;

        /// <summary>
        /// 下载照片返回值
        /// </summary>
        ReadFile_Result mResult;

        /// <summary>
        /// 读取到的文件块缓冲区
        /// </summary>
        private byte[] _FileDatas;

        /// <summary>
        /// 初始化命令结构
        /// </summary>
        /// <param name="cd"></param>
        /// <param name="parameter"></param>
        public ReadTransactionAndImageDatabase(INCommandDetail cd, ReadTransactionAndImageDatabase_Parameter parameter) : base(cd, parameter)
        {
            CmdType = 0x08;
            CheckResponseCmdType = 0x08;
        }

        /// <summary>
        /// 检查参数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            ReadTransactionAndImageDatabase_Parameter model = value as ReadTransactionAndImageDatabase_Parameter;
            if (model == null) return false;
            return model.checkedParameter();
        }

        /// <summary>
        /// 创建一个指令
        /// </summary>
        protected override void CreatePacket0()
        {
            mStep = 1;
            ReadTransactionAndImageDatabase_Result result = new ReadTransactionAndImageDatabase_Result();
            mParameter = (ReadTransactionAndImageDatabase_Parameter)_Parameter;
            //result.DatabaseType = mParameter.DatabaseType;
            result.TransactionList = new List<CardAndImageTransaction>();
            mTransactionType = 1;
            _Result = result;
            Packet(CmdType, 0x01);
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
                case 1://读取刷卡记录数据库空间信息的返回值
                    ReadCardDataBaseDetailCallBack(oPck);
                    break;
                case 11://读取体温记录数据库空间信息的返回值
                    ReadBodyTemperatureDataBaseDetailCallBack();
                    break;
                case 2://读刷卡记录数据库的返回值
                    ReadTransactionDatabaseByIndexCallBack(oPck);
                    break;
                case 12://读体温记录数据库的返回值
                    ReadBodyTemperatureTransactionDatabaseByIndexCallBack(oPck);
                    break;
                case 3://重复读取遗漏的记录的返回值
                    ReReadDatabaseCallBack(oPck);
                    break;
                case 4://写记录上传断点
                    if (CheckResponse_OK(oPck))
                    {
                        //_ProcessStep = _ProcessMax;
                        fireCommandProcessEvent();

                        //ReadTransactionAndImageDatabase_Result result = (ReadTransactionAndImageDatabase_Result)_Result;
                        //result.Quantity = mReadTotal;

                        ReadImageFile();
                    }
                    break;
                //
                case 10:

                //    break;
                //case 14:
                //    if (CheckResponse_OK(oPck))
                //    {
                //        //_ProcessStep = _ProcessMax;
                //        fireCommandProcessEvent();

                        
                //    }
                //    break;
                case 15://读取文件句柄
                    CheckOpenFileResule(oPck);
                    break;
                case 16://读文件块
                    CheckReadFileBlockResule(oPck);
                    break;
                case 17://关闭文件
                    CheckReadFile(oPck);
                    break;
             
                default:
                    break;
            }

        }

       

        /// <summary>
        /// 获取指定类型的数据库详情信息
        /// </summary>
        /// <returns></returns>
        protected virtual Protocol.Door.Door8800.Data.TransactionDetail GetTransactionDetail(OnlineAccessPacket oPck)
        {
            if (CheckResponse(oPck, CheckResponseCmdType, 0x01, 0x00))
            {
                var buf = oPck.CmdData;
                ReadTransactionDatabaseDetail_Result rst = new ReadTransactionDatabaseDetail_Result();
                rst.SetBytes(buf);

                mTransactionDetailList = rst.DatabaseDetail.ListTransaction;
                if (mStep < 10)
                {
                    return mTransactionDetailList[0];
                }
                else
                {
                    return mTransactionDetailList[3];
                }
            }

            return null;
        }

        /// <summary>
        /// 读取记录数据库空间信息的返回值 mStep=1
        /// </summary>
        /// <param name="oPck"></param>
        private void ReadCardDataBaseDetailCallBack(OnlineAccessPacket oPck)
        {
            _TransactionDetail = GetTransactionDetail(oPck);

            if (_TransactionDetail != null)
            {
                if (_TransactionDetail.readable() == 0)
                {
                    CommandCompleted();
                }
                else
                {
                    mStep = 2;
                    mBufs = new Queue<IByteBuffer>();
                    mTransactionSerialNumberList = new Dictionary<int, bool>();


                    var dataBuf = GetNewCmdDataBuf(9);
                    //dataBuf.WriteByte((int)mParameter.DatabaseType);
                    dataBuf.WriteByte(1);
                    dataBuf.WriteInt(0);
                    dataBuf.WriteInt(0);
                    Packet(CmdType, 0x04, 0x00, 0x09, dataBuf);

                    //计算最终需要读取的记录数
                    mReadable = (int)_TransactionDetail.readable();
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

                    CheckReadIndex();
                    ReadTransactionNext();
                }
            }
        }

        /// <summary>
        /// 读取体温记录数据库空间信息的返回值 mStep=11
        /// </summary>
        /// <param name="oPck"></param>
        private void ReadBodyTemperatureDataBaseDetailCallBack()
        {
            _TransactionDetail = mTransactionDetailList[3];

            if (_TransactionDetail != null)
            {
                if (_TransactionDetail.readable() == 0)
                {
                    CommandCompleted();
                }
                else
                {
                    mStep = 12;
                    mBufs = new Queue<IByteBuffer>();
                    mBodyTemperatureTransactionSerialNumberList = new Dictionary<int, bool>();


                    var dataBuf = GetNewCmdDataBuf(9);
                    //dataBuf.WriteByte((int)mParameter.DatabaseType);
                    dataBuf.WriteByte(4);
                    dataBuf.WriteInt(0);
                    dataBuf.WriteInt(0);
                    Packet(CmdType, 0x04, 0x00, 0x09, dataBuf);

                    //计算最终需要读取的记录数
                    mReadable = (int)_TransactionDetail.readable();
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

                    CheckReadIndex();
                    ReadTransactionNext();
                }
            }
        }
        /// <summary>
        /// 检查上传断点是否异常
        /// </summary>
        protected virtual void CheckReadIndex()
        {
            if (_TransactionDetail.IsCircle)
            {
                _TransactionDetail.ReadIndex = _TransactionDetail.WriteIndex;
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
            if (mReadQuantity > mReadable)
            {
                mReadQuantity = mReadable;
            }

            GetReadIndexRange(out int iBeginIndex, out int iEndIndex);
            //刷卡相关事件
            if (mStep < 10)
            {
                AddDictSerialNumberRange((int)_TransactionDetail.ReadIndex, mReadQuantity);
            }
            else
            {
                AddBodyTemperatureDictSerialNumberRange((int)_TransactionDetail.ReadIndex, mReadQuantity);
            }
            _TransactionDetail.ReadIndex = iEndIndex;//更新记录尾号

            var cmdBuf = DoorPacket.CmdData;
            cmdBuf.SetInt(1, iBeginIndex);
            cmdBuf.SetInt(5, mReadQuantity);

            CommandReady();
        }

        /// <summary>
        /// 检查上传断点是否异常
        /// </summary>
        protected void GetReadIndexRange(out int iBeginIndex, out int iEndIndex)
        {

            iBeginIndex = (int)_TransactionDetail.ReadIndex + 1;
            iEndIndex = iBeginIndex + mReadQuantity - 1;

            if (iEndIndex > _TransactionDetail.WriteIndex)
            {
                iEndIndex = (int)_TransactionDetail.WriteIndex;
                mReadQuantity = (int)(iEndIndex - iBeginIndex);
            }
        }


        /// <summary>
        /// 读记录数据库的返回值 mStep=2
        /// 读取下一包记录
        /// </summary>
        /// <param name="oPck"></param>
        private void ReadTransactionDatabaseByIndexCallBack(OnlineAccessPacket oPck)
        {
            if (CheckResponse(oPck, CheckResponseCmdType, 4, 0))
            {
                var buf = oPck.CmdData;
                SaveTransactionToBuf(buf);
            }
            else if (CheckResponse(oPck, CheckResponseCmdType, 0x04, 0xff, 4))
            {
                ReadTransactionNext();
            }
        }

        /// <summary>
        /// 读记录数据库的返回值 mStep=2
        /// 读取下一包记录
        /// </summary>
        /// <param name="oPck"></param>
        private void ReadBodyTemperatureTransactionDatabaseByIndexCallBack(OnlineAccessPacket oPck)
        {
            if (CheckResponse(oPck, CheckResponseCmdType, 4, 0))
            {
                var buf = oPck.CmdData;
                SaveTransactionToBuf(buf);
            }
            else if (CheckResponse(oPck, CheckResponseCmdType, 0x04, 0xff, 4))
            {
                ReadTransactionNext();
            }
        }
        /*
        private Random TestRnd = new Random();
        private int mLoseCount = 0, mReReadCount = 0;
        */

        /// <summary>
        /// 将返回的事务暂时保存在缓冲中
        /// </summary>
        /// <param name="bTransactionBuf"></param>
        private void SaveTransactionToBuf(IByteBuffer bTransactionBuf)
        {

            bTransactionBuf.MarkReaderIndex();
            int iSize = bTransactionBuf.ReadInt();//本数据包中包含的记录数
            int iBeginRecordNum = bTransactionBuf.ReadInt();//本数据包中第一个记录的序号（起始序号）
            bTransactionBuf.ResetReaderIndex();

            /*

            //随机不存，测试漏存现象
            int iValue = TestRnd.Next(1, 100);
            if (iValue > 45 && iValue < 60)
            {
                //mLoseCount += iSize;
                CommandWaitResponse();//不存 15%的概率
                return;
            }
            */
            if (mStep < 10)
            {
                //此序号未记录，返回，不保存
                if (!mTransactionSerialNumberList.ContainsKey(iBeginRecordNum))
                {
                    //让命令持续等待下去
                    CommandWaitResponse();
                    return;
                }

                //检查是否重复读取
                int iRecordNum = 0;
                int iSaveCount = 0;
                for (int i = 0; i < iSize; i++)
                {
                    iRecordNum = iBeginRecordNum + i;
                    if (mTransactionSerialNumberList.ContainsKey(iRecordNum))
                    {
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
            else
            {
                //此序号未记录，返回，不保存
                if (!mBodyTemperatureTransactionSerialNumberList.ContainsKey(iBeginRecordNum))
                {
                    //让命令持续等待下去
                    CommandWaitResponse();
                    return;
                }

                //检查是否重复读取
                int iRecordNum = 0;
                int iSaveCount = 0;
                for (int i = 0; i < iSize; i++)
                {
                    iRecordNum = iBeginRecordNum + i;
                    if (mBodyTemperatureTransactionSerialNumberList.ContainsKey(iRecordNum))
                    {
                        if (!mBodyTemperatureTransactionSerialNumberList[iRecordNum])
                        {
                            mBodyTemperatureTransactionSerialNumberList[iRecordNum] = true;
                            iSaveCount++;
                        }
                    }
                }
                if (iSaveCount > 0)
                {
                    //mReadTotal += iSaveCount;
                    //_ProcessStep = mReadTotal;
                    fireCommandProcessEvent();

                    bTransactionBuf.Retain();
                    mBufs.Enqueue(bTransactionBuf);

                }
                //让命令持续等待下去
                CommandWaitResponse();
            }
        }


        /// <summary>
        /// 检查修改记录读索引号的返回值  mStep=2
        /// </summary>
        private void ReadIndexComplete()
        {
            ReadTransactionAndImageDatabase_Result result = (ReadTransactionAndImageDatabase_Result)_Result;
            result.Quantity = mReadTotal;
            result.readable = (int)_TransactionDetail.readable();

            Analysis();

            CheckResultList();
        }

        /// <summary>
        /// 分析缓冲中的数据包
        /// </summary>
        private void Analysis()
        {
            ReadTransactionAndImageDatabase_Result result = (ReadTransactionAndImageDatabase_Result)_Result;
            if (result.TransactionList == null)
            {
                result.TransactionList = new List<CardAndImageTransaction>();
            }
            var lst = result.TransactionList;

            int iSize;
            //新建一个保存序列号字典，为了防止重复记录
            HashSet<int> hsSerialNumberList = new HashSet<int>();
            if (mStep < 10)
            {
                while (mBufs.Count > 0)
                {
                    IByteBuffer buf = mBufs.Dequeue();
                    iSize = buf.ReadInt();

                    for (int i = 0; i < iSize; i++)
                    {
                        int serialNumber = buf.ReadInt();

                        CardAndImageTransaction cd = new CardAndImageTransaction();
                        cd.SetSerialNumber(serialNumber);
                        cd.SetBytes(buf);

                        if (!hsSerialNumberList.Contains(serialNumber))
                        {
                            lst.Add(cd);
                            hsSerialNumberList.Add(serialNumber);
                        }

                    }
                    buf.Release();//要释放
                }
            }
            else
            {
                int index = 0;
                while (mBufs.Count > 0)
                {
                    IByteBuffer buf = mBufs.Dequeue();
                    iSize = buf.ReadInt();

                    for (int i = 0; i < iSize; i++)
                    {
                        int serialNumber = buf.ReadInt();
                        if (index < lst.Count)
                        {
                            CardAndImageTransaction cd = lst[index];
                            cd.SetBodyTemperatureBytes(buf);

                            if (!hsSerialNumberList.Contains(serialNumber))
                            {
                                hsSerialNumberList.Add(serialNumber);
                            }
                            index++;
                        }
                      

                    }
                    buf.Release();//要释放
                }
            }
            hsSerialNumberList.Clear();
            hsSerialNumberList = null;
        }


        /// <summary>
        /// 重复读取遗漏的记录 mStep=3
        /// </summary>
        /// <param name="oPck"></param>
        private void ReReadDatabaseCallBack(OnlineAccessPacket oPck)
        {
            if (CheckResponse(oPck, CheckResponseCmdType, 4, 0))
            {
                var buf = oPck.CmdData;
                SaveTransactionToBuf(buf);

            }
            else if (CheckResponse(oPck, CheckResponseCmdType, 4, 0xFF, 4))
            {
                //继续发送下一波
                CheckResultList();

            }
        }


        /// <summary>
        /// 检查是否有遗漏
        /// </summary>
        private void CheckResultList()
        {
            if (mStep < 10)
            {
                var tSerialNumber = mTransactionSerialNumberList.FirstOrDefault(t => t.Value == false);
                int iReadQuantity = mParameter.PacketSize;
                if (tSerialNumber.Key != 0)
                {
                    //检查丢失的记录是否连续
                    int iBeginNum = tSerialNumber.Key;
                    int iEndNum = iBeginNum + 1;
                    while (mTransactionSerialNumberList.ContainsKey(iEndNum) && mTransactionSerialNumberList[iEndNum] == false)
                    {
                        iEndNum++;
                        if ((iEndNum - iBeginNum) > iReadQuantity) break;
                    }

                    var buf = DoorPacket.CmdData;

                    buf.SetInt(1, iBeginNum);
                    buf.SetInt(5, (iEndNum - iBeginNum));
                    //mReReadCount++;
                    mStep = 3;
                    CommandReady();
                }
                else
                {
                    Analysis();//分析并保存记录

                    WriteTransactionReadIndex();
                }
            }
            else
            {
                var tSerialNumber = mBodyTemperatureTransactionSerialNumberList.FirstOrDefault(t => t.Value == false);
                int iReadQuantity = mParameter.PacketSize;
                if (tSerialNumber.Key != 0)
                {
                    //检查丢失的记录是否连续
                    int iBeginNum = tSerialNumber.Key;
                    int iEndNum = iBeginNum + 1;
                    while (mBodyTemperatureTransactionSerialNumberList.ContainsKey(iEndNum) && mBodyTemperatureTransactionSerialNumberList[iEndNum] == false)
                    {
                        iEndNum++;
                        if ((iEndNum - iBeginNum) > iReadQuantity) break;
                    }

                    var buf = DoorPacket.CmdData;

                    buf.SetInt(1, iBeginNum);
                    buf.SetInt(5, (iEndNum - iBeginNum));
                    //mReReadCount++;
                    mStep = 3;
                    CommandReady();
                }
                else
                {
                    Analysis();//分析并保存记录

                    WriteBodyTemperatureTransactionReadIndex();
                }
            }
        }
        /// <summary>
        /// 记录读取完毕，需要更新读索引（更新记录尾号）
        /// </summary>
        private void WriteTransactionReadIndex()
        {
            var buf = GetCmdBuf();
            buf.WriteByte(1);
            buf.WriteInt((int)_TransactionDetail.ReadIndex);
            buf.WriteBoolean(false);
            DoorPacket.CmdIndex = 0x03;
            DoorPacket.DataLen = buf.ReadableBytes;
            ReadTransactionAndImageDatabase_Result result = (ReadTransactionAndImageDatabase_Result)_Result;
            result.Quantity = mReadTotal;
            CommandReady();
            //mStep = 4;
            mStep = 11;
        }

        /// <summary>
        /// 记录体温读取完毕，需要更新读索引（更新记录尾号）
        /// </summary>
        private void WriteBodyTemperatureTransactionReadIndex()
        {
            var buf = GetCmdBuf();
            buf.WriteByte(4);
            buf.WriteInt((int)_TransactionDetail.ReadIndex);
            buf.WriteBoolean(false);
            DoorPacket.CmdIndex = 0x03;
            DoorPacket.DataLen = buf.ReadableBytes;
            CommandReady();
            mStep = 4;
        }

        /// <summary>
        /// 命令释放时需要的函数
        /// </summary>
        protected override void Release1()
        {
            mTransactionSerialNumberList = null;
            mBodyTemperatureTransactionSerialNumberList = null;
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
                mTransactionSerialNumberList.Add(i + startIndex, false);
            }
        }

        /// <summary>
        /// 提交序号到未读集合
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="len"></param>
        private void AddBodyTemperatureDictSerialNumberRange(int startIndex, int len)
        {
            for (int i = 1; i <= len; i++)
            {
                mBodyTemperatureTransactionSerialNumberList.Add(i + startIndex, false);
            }
        }

        #region 下载照片
        /// <summary>
        /// 下载记录照片
        /// </summary>
        private void ReadImageFile()
        {
            mResult = new ReadFile_Result();
            ReadTransactionAndImageDatabase_Result result = (ReadTransactionAndImageDatabase_Result)_Result;
            var record = result.TransactionList[mSaveFileSerialNumber];
            var buf = GetCmdBuf();
            buf.WriteByte(3);
            buf.WriteByte(1);
            buf.WriteInt(record.SerialNumber);
            DoorPacket.CmdIndex = 0x15;
            DoorPacket.CmdPar = 0x00;
            DoorPacket.DataLen = 6;
            DoorPacket.CmdType = 0x0B;
            mStep = 15;
            CommandReady();

        }

        /// <summary>
        /// 检查打开文件的返回值
        /// </summary>
        /// <param name="oPck"></param>
        protected virtual void CheckOpenFileResule(OnlineAccessPacket oPck)
        {
            //读取文件句柄
            if (CheckResponse(oPck, 0x0B, 0x15, 1, 17))
            {
                ReadTransactionAndImageDatabase_Result result = (ReadTransactionAndImageDatabase_Result)_Result;
                var record = result.TransactionList[mSaveFileSerialNumber];
                var buf = oPck.CmdData;

                mResult.SetBytes(buf);
                if (mResult.UserCode != record.SerialNumber || mResult.Type != 3) return;

                if (mResult.FileHandle == 0 || mResult.FileSize == 0)
                {
                    mSaveFileSerialNumber++;
                    if (mSaveFileSerialNumber == result.TransactionList.Count)
                    {
                        _ProcessStep = _ProcessMax;
                        CommandCompleted();
                    }
                    else
                    {
                        ReadImageFile();
                    }
                    return;
                }
                else
                {
                    if (mResult.FileHandle > 0 && mResult.FileSize > 0)
                    {
                        _FileDatas = new byte[mResult.FileSize];
                        _ProcessMax = mResult.FileSize;
                    }
                    else
                    {
                        CommandCompleted();
                        return;
                    }
                    //开始读文件块
                    mStep = 16;

                    _ProcessStep = 0;
                    var iPackSize = 1024;
                    if (iPackSize > mResult.FileSize) iPackSize = mResult.FileSize;
                    _ProcessMax = mResult.FileSize;
                    var readBuf = GetNewCmdDataBuf(10);
                    readBuf.WriteInt(mResult.FileHandle);
                    readBuf.WriteInt(0);
                    readBuf.WriteShort(iPackSize);

                    Packet(0x0B, 0x15, 2, (uint)readBuf.ReadableBytes, readBuf);

                    CommandReady();
                    return;
                }
            }
        }

        /// <summary>
        /// 接收读取到的文件块
        /// </summary>
        /// <param name="oPck"></param>
        protected virtual void CheckReadFileBlockResule(OnlineAccessPacket oPck)
        {
            //读取文件块返回值
            if (CheckResponse(oPck, 0x0B, 0x15, 2))
            {
                int FileHandle;
                int iDataIndex;
                int iSize = 0;


                var buf = oPck.CmdData;
                FileHandle = buf.ReadInt();
                iDataIndex = buf.ReadInt();
                iSize = buf.ReadUnsignedShort();
                uint crc = buf.ReadUnsignedInt();

                _ProcessStep = iDataIndex + iSize;


                buf.ReadBytes(_FileDatas, iDataIndex, iSize);

                var mycrc = DoNetTool.Common.Cryptography.CRC32_C.CalculateDigest(_FileDatas, (uint)iDataIndex, (uint)iSize);

                if (crc == mycrc)
                {
                    //校验通过，读取下一包
                    var iPackSize = 1024;

                    iDataIndex += iPackSize;

                    var iDataLen = mResult.FileSize - iDataIndex;
                    buf = GetCmdBuf();
                    if (iDataLen > iPackSize) iDataLen = iPackSize;
                    buf.WriteInt(mResult.FileHandle);

                    if (iDataLen <= 0)
                    {   //全部文件读取完毕
                        _ProcessStep = _ProcessMax;
                        DoorPacket.CmdPar = 3;
                        DoorPacket.DataLen = 4;
                        mStep = 17;
                    }
                    else
                    {
                        buf.WriteInt(iDataIndex);
                        buf.WriteShort(iDataLen);
                    }


                }
                else
                {
                    //校验错误，重新读取

                }

                CommandReady();
                return;
            }
        }

        /// <summary>
        /// 读取文件完毕，检验CRC32
        /// </summary>
        /// <param name="oPck"></param>
        protected virtual void CheckReadFile(OnlineAccessPacket oPck)
        {
            //读取文件块返回值
            if (CheckResponse(oPck, 0x0B, 0x15, 3))
            {

                var crc32 = DoNetTool.Common.Cryptography.CRC32_C.CalculateDigest(_FileDatas, 0, (uint)_FileDatas.Length);
                mResult.Result = (mResult.CRC == crc32);
                _ProcessStep = _ProcessMax;

                if (mResult.Result)
                {
                    mResult.Datas = _FileDatas;

                }
                _FileDatas = null;
                ReadTransactionAndImageDatabase_Result result = (ReadTransactionAndImageDatabase_Result)_Result;
                var record = result.TransactionList[mSaveFileSerialNumber];
                var sNewFile = System.IO.Path.Combine(mParameter.SaveImageDirectory, $"tmpPhoto_{record.SerialNumber}.jpg");
                record.Photo = 1;
                File.WriteAllBytes(sNewFile, mResult.Datas);

                mSaveFileSerialNumber++;
                if (mSaveFileSerialNumber == result.TransactionList.Count)
                {
                    _ProcessStep = _ProcessMax;
                    CommandCompleted();
                }
                else
                {
                    ReadImageFile();
                }
            }
        }

        #endregion
    }
}
