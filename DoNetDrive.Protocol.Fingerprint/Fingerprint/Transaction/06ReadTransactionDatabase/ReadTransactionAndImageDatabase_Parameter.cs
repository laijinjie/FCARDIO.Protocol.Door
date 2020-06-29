using DoNetDrive.Protocol.Door.Door8800;
using DotNetty.Buffers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoNetDrive.Protocol.Fingerprint.Transaction
{
    public class ReadTransactionAndImageDatabase_Parameter : AbstractParameter
    {

        /// <summary>
        /// 读取数量 0-160000,0表示都取所有新记录
        /// </summary>
        public int Quantity;

        /// <summary>
        ///  每次读取数量 1-150
        /// </summary>
        public int PacketSize = 200;

        public string SaveImageDirectory { get; set; }

        /// <summary>
        /// 初始化参数
        /// </summary>
        /// <param name="type">取值范围 1-6</param>
        /// <param name="_Quantity">读取数量</param>
        public ReadTransactionAndImageDatabase_Parameter(int _Quantity,string _SaveImageDirectory)
        {
            SaveImageDirectory = _SaveImageDirectory;
            Quantity = _Quantity;
        }

        /// <summary>
        /// 检查参数
        /// </summary>
        /// <returns></returns>
        public override bool checkedParameter()
        {
            if (string.IsNullOrEmpty(SaveImageDirectory))
            {
                throw new ArgumentException("SaveImagePath Error!");
            }
            if (!Directory.Exists(SaveImageDirectory))
            {
                throw new ArgumentException("SaveImagePath Error!");
            }
            if (PacketSize < 1 || PacketSize > 200)
            {
                throw new ArgumentException("PacketSize Error!");
            }
            if (Quantity < 0 || Quantity > 300000)
            {
                throw new ArgumentException("Quantity Error!");
            }
            return true;
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public override void Dispose()
        {
            return;
        }

        /// <summary>
        /// 将结构编码为字节缓冲
        /// </summary>
        /// <param name="databuf"></param>
        /// <returns></returns>
        public override IByteBuffer GetBytes(IByteBuffer databuf)
        {
            if (databuf.WritableBytes != 0)
            {
                throw new ArgumentException("Crad Error");
            }
            databuf.WriteByte(PacketSize);
            databuf.WriteByte(Quantity);
            return databuf;
        }

        /// <summary>
        /// 指定此类结构编码为字节缓冲后的长度
        /// </summary>
        /// <returns></returns>
        public override int GetDataLen()
        {
            return 0;
        }

        /// <summary>
        /// 将字节缓冲解码为类结构
        /// </summary>
        /// <param name="databuf"></param>
        public override void SetBytes(IByteBuffer databuf)
        {
            PacketSize = databuf.ReadByte();
            Quantity = databuf.ReadByte();
        }
    }
}
