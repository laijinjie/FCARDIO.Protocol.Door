using DoNetDrive.Core.Command;
using DoNetDrive.Protocol.Door.Door8800;
using DoNetDrive.Protocol.Fingerprint.AdditionalData;
using DoNetDrive.Protocol.Fingerprint.Data;
using DoNetDrive.Protocol.Fingerprint.Data.Transaction;
using DoNetDrive.Protocol.OnlineAccess;
using DoNetDrive.Protocol.Transaction;
using DotNetty.Buffers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoNetDrive.Protocol.Fingerprint.Transaction
{
    /// <summary>
    /// 读取认证记录及附加数据（体温，照片），仅适用于人脸机
    /// </summary>
    public class ReadTransactionAndImageDatabase : BaseCombinedCommand
    {
        /// <summary>
        /// 是否保存照片到文件
        /// </summary>
        private readonly bool mSaveImageToFile;
        /// <summary>
        /// 图片保存文件夹
        /// </summary>
        private readonly string mImageDir;

        /// <summary>
        /// 读取到的记录
        /// </summary>
        Dictionary<int, CardAndImageTransaction> _TransactionList;

        /// <summary>
        /// 记录详情
        /// </summary>
        DoNetDrive.Protocol.Door.Door8800.Data.TransactionDetail[] mTransactionDetailList;

        /// <summary>
        /// 记录的上传断点备份
        /// </summary>
        private int mReadIndex_Blackup;

        /// <summary>
        /// 指示当前命令进行的步骤
        /// </summary>
        private int mStep;

        /// <summary>
        /// 指示当前下载照片的记录序号
        /// </summary>
        private int mSaveFileSerialNumber;

        /// <summary>
        /// 最大的记录序号
        /// </summary>
        private int mMaxSerialNum;

        /// <summary>
        /// 读取计数
        /// </summary>
        protected int mReadTotal;

        /// <summary>
        /// 读取记录的子函数
        /// </summary>
        BaseReadTransactionDatabaseSubCommand _ReadTransactionCommand;
        /// <summary>
        /// 读取文件的子函数
        /// </summary>
        ReadFileSubCommand _ReadFileCommand;

        /// <summary>
        /// 初始化命令结构
        /// </summary>
        /// <param name="cd"></param>
        /// <param name="parameter"></param>
        public ReadTransactionAndImageDatabase(INCommandDetail cd,
            ReadTransactionAndImageDatabase_Parameter parameter) : base(cd, parameter)
        {
            mSaveImageToFile = parameter.PhotoSaveToFile;
            mImageDir = parameter.SaveImageDirectory;
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
            Packet(0x08, 0x01);
        }

        /// <summary>
        /// 
        /// 处理接收返回值，避免父类直接完成命令，重写逻辑
        /// </summary>
        /// <param name="oPck"></param>
        protected override void CommandNext0(OnlineAccessPacket oPck)
        {
            try
            {
                switch (mStep)
                {
                    case 1://读取记录详情返回值
                        CheckDataDetail(oPck);
                        break;
                    case 2://读取认证记录
                        _ReadTransactionCommand?.CommandNext(oPck);
                        break;
                    case 3://更新认证记录上传断点为备份断点
                        if (CheckResponse_OK(oPck)) BeginReadBodyTemperature();
                        break;
                    case 4://读取体温记录
                        _ReadTransactionCommand?.CommandNext(oPck);
                        break;
                    case 5://修改体温记录上传断点为备份断点
                        if (CheckResponse_OK(oPck)) BeginReadImageFile();
                        break;
                    case 6://开始读取记录照片
                        _ReadFileCommand?.CommandNext(oPck);
                        break;
                    case 7://记录和照片都读取完毕,更新认证记录上传断点完毕，开始更新体温上传断点
                        if (CheckResponse_OK(oPck)) WriteTransactionReadIndex_BodyTemperature();
                        break;
                    case 8://更新体温上传断点完毕，创建返回值
                        if (CheckResponse_OK(oPck)) CreateResult();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {

                Trace.WriteLine($"{_Connector.GetKey()} ReadTransactionAndImageDatabase_CommandNext0 {mStep}:{_ProcessStep} {ex.Message}");
            }
        }

        /// <summary>
        /// 检查记录详情返回值
        /// </summary>
        /// <param name="oPck"></param>
        private void CheckDataDetail(OnlineAccessPacket oPck)
        {
            TransactionDetail transactionDetail;
            var model = _Parameter as ReadTransactionAndImageDatabase_Parameter;
            if (CheckResponse(oPck, 0x08, 0x01, 0x00))
            {
                var buf = oPck.CmdData;
                var rst = new ReadTransactionDatabaseDetail_Result();
                rst.SetBytes(buf);
                mTransactionDetailList = rst.DatabaseDetail.ListTransaction;

                transactionDetail = mTransactionDetailList[0] as TransactionDetail;
            }
            else
            {
                return;
            }

            //读卡记录
            _ReadTransactionCommand = new ReadTransactionDatabaseSubCommand<CardTransaction>(this);

            mReadIndex_Blackup = (int)transactionDetail.ReadIndex;

            _ReadTransactionCommand.PacketSize = model.PacketSize;
            if (transactionDetail.readable() > 0)
            {
                _ReadTransactionCommand.BeginRead(1, transactionDetail, model.Quantity);
                mStep = 2;
                CommandReady();
            }
            else
            {
                CreateResult();
            }

        }

        /// <summary>
        /// 命令执行完毕
        /// </summary>
        /// <param name="subCmd"></param>
        public override void SubCommandOver(ISubCommand subCmd)
        {
            switch (mStep)
            {
                case 2://读取认证记录
                    ReadCardTransactionOver();
                    break;
                case 4://读取体温记录
                    ReadBodyTemperatureTransactionOver();
                    break;
                case 6://读取文件
                    ReadImageCallblack();
                    break;
                default:
                    break;
            }

        }

        /// <summary>
        /// 读取认证记录完毕，开始读取体温记录
        /// </summary>
        private void ReadCardTransactionOver()
        {
            var lst = _ReadTransactionCommand.GetTransactions();
            _TransactionList = new Dictionary<int, CardAndImageTransaction>(lst.Count);
            foreach (var kv in lst)
            {
                _TransactionList.Add(kv.Key, new CardAndImageTransaction((CardTransaction)kv.Value));
            }
            lst.Clear();
            mStep = 3;
            _ReadTransactionCommand.Release();
            _ReadTransactionCommand = null;
            mMaxSerialNum = _TransactionList.Keys.Max();
            WriteTransactionReadIndex(1, mReadIndex_Blackup);//还原上传断点为备份断点
        }

        #region 读取体温记录

        /// <summary>
        /// 开始读取体温记录
        /// </summary>
        private void BeginReadBodyTemperature()
        {
            mStep = 4;
            var tr = mTransactionDetailList[3];

            mReadIndex_Blackup = (int)tr.ReadIndex;

            if (tr.readable() > 0)
            {
                _ReadTransactionCommand = new ReadTransactionDatabaseSubCommand<BodyTemperatureTransaction>(this);
                //开始读取体温记录
                _ReadTransactionCommand.BeginRead(4,
                    tr as TransactionDetail, _TransactionList.Count);
                CommandReady();
            }
            else
            {
                BeginReadImageFile();
            }

        }

        /// <summary>
        /// 读取体温记录完毕
        /// </summary>
        private void ReadBodyTemperatureTransactionOver()
        {
            var lst = _ReadTransactionCommand.GetTransactions();
            var iBTMaxNum = 0;
            int trNum = 0;
            if (lst.Count == 0)
            {
                //所有记录已读取完毕，没有新记录
                ReadBodyTemperatureTransaction_UpdateReadIndexBlackup();
                return;
            }
            foreach (var k in lst.Keys)
            {
                var bt = lst[k] as BodyTemperatureTransaction;
                trNum = bt.SerialNumber;
                if (trNum > iBTMaxNum) iBTMaxNum = trNum;
                if (_TransactionList.ContainsKey(trNum))
                {
                    var ct = _TransactionList[trNum];
                    ct.BodyTemperature = bt.BodyTemperature;
                }
            }
            var trLst = lst.Values.ToArray();
            Array.Sort(trLst, (x, y) => (x.SerialNumber.CompareTo(y.SerialNumber)));

            var BT_Dtl = mTransactionDetailList[3] as TransactionDetail;

            if (iBTMaxNum > mMaxSerialNum)
            {
                //序号已经超过了认证记录，不能在读了，需要回滚
                var bt = trLst.First((x) => x.SerialNumber >= mMaxSerialNum) as BodyTemperatureTransaction;
                if (bt.SerialNumber > mMaxSerialNum)
                {

                    BT_Dtl.ReadIndex = bt.RecordSerialNumber - 1;
                }
                else
                {
                    BT_Dtl.ReadIndex = bt.RecordSerialNumber;
                }

                ReadBodyTemperatureTransaction_UpdateReadIndexBlackup();

            }
            else
            {
                if (iBTMaxNum != mMaxSerialNum)
                {
                    //体温记录中的序号，没有超过或等于记录序号，需要继续读取
                    _ReadTransactionCommand.BeginRead(4, BT_Dtl, _TransactionList.Count);
                    CommandReady();
                }
                else
                {
                    ReadBodyTemperatureTransaction_UpdateReadIndexBlackup();
                }
            }
        }

        /// <summary>
        /// 体温记录读取完毕，更新上传断点为备份断点
        /// </summary>
        private void ReadBodyTemperatureTransaction_UpdateReadIndexBlackup()
        {
            _ReadTransactionCommand?.Release();
            _ReadTransactionCommand = null;

            mStep = 5;
            WriteTransactionReadIndex(4, mReadIndex_Blackup);
            CommandReady();
        }
        #endregion


        #region 修改上传断点
        private void WriteTransactionReadIndex_Card()
        {
            mStep = 7;
            WriteTransactionReadIndex(1, (int)mTransactionDetailList[0].ReadIndex);
        }

        private void WriteTransactionReadIndex_BodyTemperature()
        {
            mStep = 8;
            WriteTransactionReadIndex(4, (int)mTransactionDetailList[3].ReadIndex);
        }

        /// <summary>
        /// 记录读取完毕，需要更新读索引（更新记录尾号）
        /// </summary>
        private void WriteTransactionReadIndex(int iType, int ReadIndex)
        {

            var buf = GetCmdBuf();
            if (buf.WritableBytes < 5)
                buf = GetNewCmdDataBuf(5);
            buf.WriteByte(iType);
            buf.WriteInt(ReadIndex);
            Packet(0x08, 0x03, 0x00, 5, buf);
            CommandReady();
        }
        #endregion

        #region 产生返回值
        /// <summary>
        /// 产生返回值，并使命令完结
        /// </summary>
        private void CreateResult()
        {
            var rst = new ReadTransactionAndImageDatabase_Result();
            _Result = rst;
            rst.readable = (int)mTransactionDetailList[0].readable();
            if (_TransactionList != null)
            {
                rst.Quantity = _TransactionList.Count;

                rst.TransactionList = new List<CardAndImageTransaction>(_TransactionList.Values);
            }
            else
            {
                rst.Quantity = 0;

                rst.TransactionList = new List<CardAndImageTransaction>();
            }


            CommandCompleted();
        }
        #endregion


        #region 读取照片
        private void BeginReadImageFile()
        {
            mStep = 6;

            _ProcessMax = _TransactionList.Count;
            _ProcessStep = 0;
            fireCommandProcessEvent();

            mSaveFileSerialNumber = _TransactionList.Keys.Min();

            _ReadFileCommand = new ReadFileSubCommand(this);
            //Trace.WriteLine($"{_Connector.GetKey()} 开始下载记录照片:{mSaveFileSerialNumber}/{mMaxSerialNum} ");
            _ReadFileCommand.BeginRead(mSaveFileSerialNumber, 3, 1);
            CommandReady();
        }
        /// <summary>
        /// 读取照片完毕
        /// </summary>
        private void ReadImageCallblack()
        {
            if (_ReadFileCommand.IsCommandOver())
            {
                var CardTr = _TransactionList[mSaveFileSerialNumber] as CardAndImageTransaction;
                if (_ReadFileCommand.FileResult)
                {

                    CardTr.SetPhoto(1);
                    CardTr.PhotoSize = _ReadFileCommand.FileSize;

                    if (mSaveImageToFile)
                    {
                        try
                        {
                            string sFile = System.IO.Path.Combine(mImageDir, $"{mSaveFileSerialNumber}.jpg");
                            System.IO.File.WriteAllBytes(sFile, _ReadFileCommand.FileDatas);
                            CardTr.PhotoFile = sFile;
                        }
                        catch (Exception)
                        {
                            CardTr.PhotoFile = string.Empty;
                            CardTr.PhotoDataBuf = _ReadFileCommand.FileDatas;
                        }
                    }
                    else
                    {
                        CardTr.PhotoDataBuf = _ReadFileCommand.FileDatas;
                    }
                }
                else
                {
                    CardTr.SetPhoto(0);
                }

                ReadImageNext();
            }
            else
            {
                ReadImageNext();
            }
        }
        private void ReadImageNext()
        {
            mSaveFileSerialNumber++;
            if (mSaveFileSerialNumber > mMaxSerialNum)
            {
                _ReadFileCommand.Release();
                //读取完毕
                WriteTransactionReadIndex_Card();
                return;
            }
            if (_TransactionList.ContainsKey(mSaveFileSerialNumber))
            {
                _ProcessStep++;
                fireCommandProcessEvent();

                _ReadFileCommand.BeginRead(mSaveFileSerialNumber, 3, 1);
                CommandReady();
            }
            else
            {
                ReadImageNext();
            }
        }

        #endregion

        /// <summary>
        /// 命令准备就绪
        /// </summary>
        /// <param name="subCmd"></param>
        public override void SubCommandReady(ISubCommand subCmd)
        {
            if (mStep != 6)
            {
                base.SubCommandReady(subCmd);
            }
            else
            {
                CommandReady();
            }

        }

    }
}
