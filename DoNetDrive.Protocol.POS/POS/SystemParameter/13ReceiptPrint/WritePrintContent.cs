using DoNetDrive.Core.Command;
using DoNetDrive.Protocol.POS.SystemParameter.ReceiptPrint;

namespace DoNetDrive.Protocol.POS.SystemParameter.ReceiptPrint
{
    public class WritePrintContent : Write_Command
    {
        /// <summary>
        /// 参数对象
        /// </summary>
        protected WriteReceiptPrint_Parameter mReceiptPrintPar;

        public WritePrintContent(Protocol.DESDriveCommandDetail cd, WriteReceiptPrint_Parameter par) : base(cd, par) { mReceiptPrintPar = par; }

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

            var buf = acl.Buffer(198);

            Packet(0x01, 0x0D, 0x02, 198, mReceiptPrintPar.Content_GetBytes(buf));
        }
    }
}
