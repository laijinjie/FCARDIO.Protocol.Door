using DoNetDrive.Core.Command;

namespace DoNetDrive.Protocol.POS.SystemParameter.ReceiptPrint
{
    public class WriteReceiptPrint : Write_Command
    {
        /// <summary>
        /// 参数对象
        /// </summary>
        protected WriteReceiptPrint_Parameter mReceiptPrintPar;

        public WriteReceiptPrint(Protocol.DESDriveCommandDetail cd, WriteReceiptPrint_Parameter par) : base(cd, par) { mReceiptPrintPar = par; }

        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            WriteReceiptPrint_Parameter model = value as WriteReceiptPrint_Parameter;
            if (model == null) return false;
            return model.checkedParameter();
        }

        protected override void CreatePacket0()
        {
            WriteReceiptPrint_Parameter model = _Parameter as WriteReceiptPrint_Parameter;

            var acl = _Connector.GetByteBufAllocator();

            var buf = acl.Buffer(model.GetDataLen());

            Packet(0x01, 0x0D, 0x00, 2, model.GetBytes(buf));
        }

        /// <summary>
        /// 处理返回值
        /// </summary>
        /// <param name="oPck">包含返回指令的Packet</param>
        protected override void CommandNext0(Protocol.DESPacket oPck)
        {
            if (CheckResponse_OK(oPck))
            {
                CommandCompleted();
            }

        }
    }
}
