using DotNetty.Buffers;
using DoNetDrive.Core.Command;
using DoNetDrive.Protocol.POS.CardType.ReadDataBase;
using DoNetDrive.Protocol.POS.Data;
using System.Collections.Generic;
using DoNetDrive.Protocol.POS.TemplateMethod;
using DoNetDrive.Protocol.POS.Protocol;

namespace DoNetDrive.Protocol.POS.CardType.AddCardTypeDetail
{
    public class AddCardTypeDetail : TemplateWriteData_Base<AddCardTypeDetail_Parameter, CardTypeDetail>
    {

        /// <summary>
        /// 初始化参数
        /// </summary>
        /// <param name="cd"></param>
        /// <param name="par"></param>
        public AddCardTypeDetail(Protocol.DESDriveCommandDetail cd, AddCardTypeDetail_Parameter par) : base(cd, par)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="DataList"></param>
        /// <returns></returns>
        protected override TemplateResult_Base CreateResult(List<CardTypeDetail> DataList)
        {
            ReadDataBase_Result result = new ReadDataBase_Result(DataList);
            return result;
        }


        /// <summary>
        /// 检测结束指令返回值
        /// </summary>
        /// <param name="oPck"></param>
        /// <returns></returns>
        protected override bool CheckResponseCompleted(DESPacket oPck)
        {
            var subPck = oPck.CommandPacket;
            return (subPck.CmdType == 0x38 &&
                subPck.CmdIndex == 4 &&
                subPck.CmdPar == 0xff);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="databuf"></param>
        /// <param name="data"></param>
        protected override void WriteDataBodyToBuf(IByteBuffer databuf, TemplateData_Base data)
        {
            CardTypeDetail cardTypeDetail = data as CardTypeDetail;
            cardTypeDetail.GetDeleteBytes(databuf);
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void CreateCommandPacket0()
        {
            var buf = GetNewCmdDataBuf(MaxBufSize);
            WriteDataToBuf(buf);
            Packet(0x08, 0x4, 0x00, (uint)buf.ReadableBytes, buf);
        }
    }
}
