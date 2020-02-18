using DotNetty.Buffers;
using DoNetDrive.Core.Command;
using DoNetDrive.Protocol.POS.CardType.ReadDataBase;
using DoNetDrive.Protocol.POS.Data;
using System.Collections.Generic;

namespace DoNetDrive.Protocol.POS.CardType.AddCardTypeDetail
{
    public class AddCardTypeDetail : WriteCardTypeBase
    {

        /// <summary>
        /// 初始化参数
        /// </summary>
        /// <param name="cd"></param>
        /// <param name="par"></param>
        public AddCardTypeDetail(INCommandDetail cd, CardType_Parameter_Base par) : base(cd, par)
        {
            MaxBufSize = (mBatchCount * mParDataLen) + 4;
        }

        /// <summary>
        /// 将数据部分写入到缓冲区
        /// </summary>
        /// <param name="password">要写入的密码</param>
        /// <param name="databuf"></param>
        protected override void WriteCardTypeBodyToBuf(IByteBuffer databuf, CardTypeDetail CardType)
        {
            CardType.GetBytes(databuf);
        }

        /// <summary>
        /// 将命令打包成一个Packet，准备发送
        /// </summary>
        protected override void CreateCommandPacket0()
        {
            var buf = GetNewCmdDataBuf(MaxBufSize);
            WriteCardTypeToBuf(buf);
            Packet(0x8, 0x4, 0x00, (uint)buf.ReadableBytes, buf);
        }

        /// <summary>
        /// 创建返回值
        /// </summary>
        /// <param name="passwordList">无法写入的密码列表</param>
        /// <returns></returns>
        protected override ReadDataBase_Result CreateResult(List<CardTypeDetail> cardTypeDetail)
        {
            ReadDataBase_Result result = new ReadDataBase_Result(cardTypeDetail);
            return result;
        }
    }
}
