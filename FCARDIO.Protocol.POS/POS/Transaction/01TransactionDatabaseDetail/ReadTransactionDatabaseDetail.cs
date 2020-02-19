using DoNetDrive.Core.Command;
using DoNetDrive.Protocol.OnlineAccess;

namespace DoNetDrive.Protocol.POS.Transaction
{
    public class ReadTransactionDatabaseDetail : Read_Command
    {
        /// <summary>
        /// 初始化参数
        /// </summary>
        /// <param name="detail"></param>
        public ReadTransactionDatabaseDetail(INCommandDetail detail) : base(detail, null) { }

        /// <summary>
        /// 创建一个通讯指令
        /// </summary>
        protected override void CreatePacket0()
        {
            Packet(0x48, 0x01, 0x00, 0x00, null);
        }

        /// <summary>
        /// 处理返回值
        /// </summary>
        /// <param name="oPck"></param>
        protected override void CommandNext1(OnlineAccessPacket oPck)
        {
            if (CheckResponse(oPck, 0x0D * 6))
            {
                var buf = oPck.CmdData;
                ReadTransactionDatabaseDetail_Result rst = new ReadTransactionDatabaseDetail_Result();
                _Result = rst;
                rst.SetBytes(buf);
                CommandCompleted();
            }
        }
    }
}
