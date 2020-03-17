using DotNetty.Buffers;
using DoNetDrive.Protocol.POS.Data;
using System;
using System.Collections.Generic;

namespace DoNetDrive.Protocol.POS.CardType
{
    public class CardType_Parameter_Base : AbstractParameter
    {
        /// <summary>
        /// 要添加的密码集合
        /// </summary>
        public List<CardTypeDetail> CardTypeList { get; set; }

        /// <summary>
        /// 创建 将密码列表写入到控制器或从控制器删除 指令的参数
        /// </summary>
        public CardType_Parameter_Base(List<CardTypeDetail> cardTypeList)
        {
            CardTypeList = cardTypeList;
        }

        /// <summary>
        /// 提供给继承类使用
        /// </summary>
        public CardType_Parameter_Base()
        {

        }

        /// <summary>
        /// 检查参数
        /// </summary>
        /// <returns></returns>
        public override bool checkedParameter()
        {
            if (CardTypeList == null || CardTypeList.Count == 0)
            {
                return false;
            }
            foreach (var item in CardTypeList)
            {
                if (item.SubsidyMoney < 0)
                {
                    throw new ArgumentException("SubsidyMoney Error!");
                }
            }

            return true;
        }
        /// <summary>
        /// 释放资源
        /// </summary>
        public override void Dispose()
        {
            CardTypeList = null;
        }

        /// <summary>
        /// 不实现此功能
        /// </summary>
        /// <param name="databuf"></param>
        public override void SetBytes(IByteBuffer databuf)
        {
            return;
        }

        /// <summary>
        /// 不实现此功能
        /// </summary>
        /// <returns></returns>
        public override int GetDataLen()
        {
            return 0;
        }

        /// <summary>
        /// 不实现此功能
        /// </summary>
        /// <param name="databuf"></param>
        /// <returns></returns>
        public override IByteBuffer GetBytes(IByteBuffer databuf)
        {
            return databuf;
        }
    }
}
