using DotNetty.Buffers;
using System;
using System.Text;

namespace DoNetDrive.Protocol.POS.Data
{
    /// <summary>
    /// 菜单
    /// </summary>
    public class MenuDetail : Door.FC8800.TemplateMethod.TemplateData_Base
    {
        /// <summary>
        /// 商品代码
        /// </summary>
        public int MenuNum;

        /// <summary>
        /// 商品条形码
        /// </summary>
        public string MenuBarCode;

        /// <summary>
        /// 商品名称
        /// </summary>
        public string MenuName;

        /// <summary>
        /// 商品价格
        /// </summary>
        public int MenuPrice;

        public override void SetBytes(IByteBuffer databuf)
        {
            MenuNum = databuf.ReadInt();
            MenuBarCode = Util.StringUtil.GetString(databuf, 40, Encoding.BigEndianUnicode);
            MenuName = Util.StringUtil.GetString(databuf, 16, Encoding.BigEndianUnicode);
            MenuPrice = databuf.ReadInt();
        }


        public override IByteBuffer GetBytes(IByteBuffer databuf)
        {
            databuf.WriteInt(MenuNum);
            Util.StringUtil.WriteString(databuf, MenuBarCode, 40, Encoding.BigEndianUnicode);
            Util.StringUtil.WriteString(databuf, MenuName, 16, Encoding.BigEndianUnicode);
            databuf.WriteInt(MenuPrice);
            return databuf;
        }

        /// <summary>
        /// 获取每个添加卡类长度
        /// </summary>
        /// <returns></returns>
        public override int GetDataLen()
        {
            return 64;
        }

        public override IByteBuffer GetDeleteBytes(IByteBuffer data)
        {
            throw new NotImplementedException();
        }

        public override int GetDeleteDataLen()
        {
            throw new NotImplementedException();
        }

        public override void SetFailBytes(IByteBuffer databuf)
        {
            MenuNum = databuf.ReadInt();
        }
    }
}
