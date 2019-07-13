using DotNetty.Buffers;
using FCARDIO.Core.Command;
using FCARDIO.Protocol.Door.FC8800.Data;
using FCARDIO.Protocol.FC8800;
using FCARDIO.Protocol.OnlineAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.Transaction.ReadTransactionDatabaseByIndex
{
    /// <summary>
    /// 读记录数据库
    /// 按指定索引号开始读指定类型的记录数据库，并读取指定数量。
    /// 成功返回结果参考 ReadTransactionDatabaseByIndex_Result 
    /// </summary>
    public class ReadTransactionDatabaseByIndex : FC8800Command_ReadParameter
    {
        private Queue<IByteBuffer> mBufs;
        private int mQuantity;

        /// <summary>
        /// 输入参数
        /// </summary>
        ReadTransactionDatabaseByIndex_Parameter mPar;
        /// <summary>
        /// 初始化命令结构
        /// </summary>
        /// <param name="cd"></param>
        /// <param name="parameter"></param>
        public ReadTransactionDatabaseByIndex(INCommandDetail cd, ReadTransactionDatabaseByIndex_Parameter parameter) : base(cd, parameter) { mPar = parameter; mBufs = new Queue<IByteBuffer>(); }

        /// <summary>
        /// 检查参数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            ReadTransactionDatabaseByIndex_Parameter model = value as ReadTransactionDatabaseByIndex_Parameter;
            if (model == null) return false;
            return model.checkedParameter();
        }

        /// <summary>
        /// 创建一个指令
        /// </summary>
        protected override void CreatePacket0()
        {
            Packet(0x08, 0x04, 0x00, 0x01 + 0x04 + 4,GetCmdData());
           

        }

        /// <summary>
        /// 获取参数结构的字节编码
        /// </summary>
        /// <returns></returns>
        private IByteBuffer GetCmdData()
        {
            ReadTransactionDatabaseByIndex_Parameter model = _Parameter as ReadTransactionDatabaseByIndex_Parameter;
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
            if (CheckResponse(oPck))
            {
                var buf = oPck.CmdData;
                int iSize = buf.GetInt(0);
                _ProcessStep += iSize;
                buf.Retain();
                mBufs.Enqueue(buf);

                /*
                if (_Result == null)
                {
                    _Result = new ReadTransactionDatabaseByIndex_Result();
                    ((ReadTransactionDatabaseByIndex_Result)_Result).TransactionList = new List<AbstractTransaction>(iCount);
                }
                
                for (int i = 0; i < iCount; i++)
                {
                    switch (mPar.DatabaseType)
                    {
                        case 1:
                            CardTransaction cardTransaction = new CardTransaction();
                            cardTransaction.SetBytes(buf);
                            ((ReadTransactionDatabaseByIndex_Result)_Result).TransactionList.Add(cardTransaction);
                            break;
                        case 2:
                            ButtonTransaction buttonTransaction = new ButtonTransaction();
                            buttonTransaction.SetBytes(buf);
                            ((ReadTransactionDatabaseByIndex_Result)_Result).TransactionList.Add(buttonTransaction);
                            break;
                        case 3:
                            DoorSensorTransaction doorSensorTransaction = new DoorSensorTransaction();
                            doorSensorTransaction.SetBytes(buf);
                            ((ReadTransactionDatabaseByIndex_Result)_Result).TransactionList.Add(doorSensorTransaction);
                            break;
                        case 4:
                            SoftwareTransaction softwareTransaction = new SoftwareTransaction();
                            softwareTransaction.SetBytes(buf);
                            ((ReadTransactionDatabaseByIndex_Result)_Result).TransactionList.Add(softwareTransaction);
                            break;
                        case 5:
                            AlarmTransaction alarmTransaction = new AlarmTransaction();
                            alarmTransaction.SetBytes(buf);
                            ((ReadTransactionDatabaseByIndex_Result)_Result).TransactionList.Add(alarmTransaction);
                            break;
                        case 6:
                            SystemTransaction systemTransaction = new SystemTransaction();
                            systemTransaction.SetBytes(buf);
                            ((ReadTransactionDatabaseByIndex_Result)_Result).TransactionList.Add(systemTransaction);
                            break;
                        default:
                            break;
                    }
                }
                */
                CommandWaitResponse();
            }
            if (CheckResponse(oPck,0x08, 0x04, 0xff, 4))
            {
                var buf = oPck.CmdData;
                int iSize = buf.ReadInt();
                ReadTransactionDatabaseByIndex_Result result = new ReadTransactionDatabaseByIndex_Result();
                result.DatabaseType = (e_TransactionDatabaseType)mPar.DatabaseType;
                result.ReadIndex = mPar.ReadIndex;
                result.Quantity = iSize;
                _Result = result;
                if (iSize > 0)
                {
                    Analysis(iSize);
                }

                //ReadTransactionDatabaseByIndex_Result rst = new ReadTransactionDatabaseByIndex_Result();
                //_Result = rst;
                //rst.SetBytes(buf);

                CommandCompleted();

            }
            /*
            if (CheckResponse(oPck))
            {
                var buf = oPck.CmdData;
                mBufs.Enqueue(buf);
                

                CommandWaitResponse();
            }
            
            */
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
            return;
        }

        /// <summary>
        /// 分析缓冲中的数据包
        /// </summary>
        /// <param name="iSize">总传输量</param>
        protected void Analysis(int iSize)
        {
            ReadTransactionDatabaseByIndex_Result result = (ReadTransactionDatabaseByIndex_Result)_Result;
            result.TransactionList = new List<AbstractTransaction>();
            Type TransactionType;
            switch (result.DatabaseType)
            {
                case e_TransactionDatabaseType.OnCardTransaction:
                    TransactionType = typeof(CardTransaction);
                    break;
                case e_TransactionDatabaseType.OnButtonTransaction:
                    TransactionType = typeof(ButtonTransaction);
                    break;
                case e_TransactionDatabaseType.OnDoorSensorTransaction:
                    TransactionType = typeof(DoorSensorTransaction);
                    break;
                case e_TransactionDatabaseType.OnSoftwareTransaction:
                    TransactionType = typeof(SoftwareTransaction);
                    break;
                case e_TransactionDatabaseType.OnAlarmTransaction:
                    TransactionType = typeof(AlarmTransaction);
                    break;
                case e_TransactionDatabaseType.OnSystemTransaction:
                    TransactionType = typeof(SystemTransaction);
                    break;
                default:
                    result.Quantity = 0;
                    return;

            }
            while (mBufs.Count > 0 && mBufs.Peek() != null)
            {
                IByteBuffer buf = mBufs.Dequeue();
                iSize = buf.ReadInt();

                for (int i = 0; i < iSize; i++)
                {
                    try
                    {
                        AbstractTransaction cd = Activator.CreateInstance(TransactionType) as AbstractTransaction;
                        cd.SerialNumber = buf.ReadInt();
                        cd.SetBytes(buf);
                        result.TransactionList.Add(cd);
                    }
                    catch (Exception e)
                    {
                        result.Quantity = 0;
                        return;
                    }

                }
                //buf = null;
            }
        }
    }
}
