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

namespace FCARDIO.Protocol.Door.FC8800.Transaction.ReadTransactionDatabase
{
    /// <summary>
    ///  读取新记录
    ///  读指定类型的记录数据库最新记录，并读取指定数量。
    ///  成功返回结果参考 link ReadTransactionDatabase_Result 
    /// </summary>
    public class ReadTransactionDatabase : FC8800Command
    {
        private Queue<IByteBuffer> mBufs;
        /// <summary>
        /// 本次读取的数量
        /// </summary>
        private int mReadQuantity;
        private int mStep;
        private ReadTransactionDatabase_Parameter thisParameter;
        private TransactionDetail transactionDetail;
        /// <summary>
        /// 可读取的新记录数量
        /// </summary>
        private int mReadable;
        /// <summary>
        /// 读取计数
        /// </summary>
        private int mReadTotal;

        /// <summary>
        /// 初始化命令结构
        /// </summary>
        /// <param name="cd"></param>
        /// <param name="parameter"></param>
        public ReadTransactionDatabase(INCommandDetail cd, ReadTransactionDatabase_Parameter parameter) : base(cd, parameter) { }

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
            Packet(0x08,0x01,0x00,0x00, GetCmdDate());
        }

        /// <summary>
        /// 获取参数结构的字节编码
        /// </summary>
        /// <returns></returns>
        private IByteBuffer GetCmdDate()
        {
            ReadTransactionDatabase_Parameter model = _Parameter as ReadTransactionDatabase_Parameter;
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
            if (CheckResponse(oPck, 8, 1, 0, 0xD * 6))
            {
                var buf = oPck.CmdData;
                ReadTransactionDatabase_Result rst = new ReadTransactionDatabase_Result();
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
        /// 命令释放时需要的函数
        /// </summary>
        protected override void Release1()
        {
            return;
        }
    }
}
