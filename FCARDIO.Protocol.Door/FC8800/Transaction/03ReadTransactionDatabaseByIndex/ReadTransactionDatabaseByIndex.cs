using DotNetty.Buffers;
using FCARDIO.Core.Command;
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
    public class ReadTransactionDatabaseByIndex : FC8800Command
    {
        private Queue<IByteBuffer> mBufs;
        private int mQuantity;

        /// <summary>
        /// 初始化命令结构
        /// </summary>
        /// <param name="cd"></param>
        /// <param name="parameter"></param>
        public ReadTransactionDatabaseByIndex(INCommandDetail cd, ReadTransactionDatabaseByIndex_Parameter parameter) : base(cd, parameter) { }

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
            Packet(0x08, 0x04, 0x00, 0x01 + 0x04 + 4,GetCmdDate());
        }

        /// <summary>
        /// 获取参数结构的字节编码
        /// </summary>
        /// <returns></returns>
        private IByteBuffer GetCmdDate()
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
            if (CheckResponse(oPck, 0x04))
            {
                var buf = oPck.CmdData;
                ReadTransactionDatabaseByIndex_Result rst = new ReadTransactionDatabaseByIndex_Result();
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
