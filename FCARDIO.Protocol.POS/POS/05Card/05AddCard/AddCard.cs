using DotNetty.Buffers;
using DoNetDrive.Core.Command;
using DoNetDrive.Protocol.Door.FC8800.TemplateMethod;
using System.Collections.Generic;

namespace DoNetDrive.Protocol.POS.Card
{
    /// <summary>
    /// 添加名单
    /// </summary>
    public class AddCard : Door.FC8800.TemplateMethod.TemplateWriteData_Base<Data.MenuDetail>
    {
        /// <summary>
        /// 当前命令进度
        /// </summary>
        protected int mStep = 0;

        /// <summary>
        /// 初始化参数
        /// </summary>
        /// <param name="cd"></param>
        /// <param name="par"></param>
        public AddCard(INCommandDetail cd, AddCard_Parameter par) : base(cd, par)
        {
            MaxBufSize = (mBatchCount * mParDataLen) + 4;
            CheckResponseCmdType = 0x05;
            CmdIndex = 0x04;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="DataList"></param>
        /// <returns></returns>
        protected override TemplateResult_Base CreateResult(List<TemplateData_Base> DataList)
        {
            ReadAllCard_Result result = new ReadAllCard_Result(DataList);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="databuf"></param>
        /// <param name="data"></param>
        protected override void WriteDataBodyToBuf(IByteBuffer databuf, TemplateData_Base data)
        {
            Data.CardDetail menuDetail = data as Data.CardDetail;
            menuDetail.GetBytes(databuf);
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void CreateCommandPacket0()
        {
            var buf = GetNewCmdDataBuf(MaxBufSize);
            WriteDataToBuf(buf);
            Packet(0x05, 0x4, 0x00, (uint)buf.ReadableBytes, buf);
        }
    }
}
