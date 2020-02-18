﻿using DotNetty.Buffers;
using DoNetDrive.Protocol.Door.Door8800;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoNetDrive.Protocol.Fingerprint.SystemParameter.LocalIdentity
{
    /// <summary>
    /// 设置本机身份参数
    /// </summary>
    public class WriteLocalIdentity_Parameter : AbstractParameter
    {
        /// <summary>
        /// 本机所属门号 1-4
        /// </summary>
        public byte Door;

        /// <summary>
        /// 本机名称 (60字节)
        /// </summary>
        public string LocalName;

        /// <summary>
        /// 进出类别 
        /// 0 - 进门
        /// 1 - 出门
        /// </summary>
        public byte InOut;

        /// <summary>
        /// 构建一个空的实例
        /// </summary>
        public WriteLocalIdentity_Parameter() { }

        /// <summary>
        /// 初始化实例
        /// </summary>
        /// <param name="door">本机所属门号</param>
        /// <param name="localName">本机名称</param>
        /// <param name="inOut">进出类别</param>
        public WriteLocalIdentity_Parameter(byte door,string localName, byte inOut)
        {
            Door = door;
            LocalName = localName;
            InOut = inOut;
        }

        /// <summary>
        /// 检查参数
        /// </summary>
        /// <returns></returns>
        public override bool checkedParameter()
        {
            if (InOut > 1)
            {
                return false;
            }
            if (Door < 1 || Door > 4)
            {
                return false;
            }
            if (string.IsNullOrEmpty(LocalName) || Encoding.GetEncoding("GBK").GetBytes(LocalName).Length > 60)
            {
                return false;
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
        /// 对参数进行编码
        /// </summary>
        /// <param name="databuf"></param>
        /// <returns></returns>
        public override IByteBuffer GetBytes(IByteBuffer databuf)
        {
            databuf.WriteByte(Door);

            byte[] bName = new byte[60];

            int strLen = LocalName.Length;
            //int gbkLen = Encoding.GetEncoding("GBK").GetBytes(LocalName).Length;
            //int gbkCount = gbkLen - strLen;

            bName = Encoding.BigEndianUnicode.GetBytes(LocalName.PadRight(60  - strLen, '\0'));
            databuf.WriteBytes(bName);

            databuf.WriteByte(InOut);
          
            return databuf;
        }

        /// <summary>
        /// 获取数据长度
        /// </summary>
        /// <returns></returns>
        public override int GetDataLen()
        {
            return 62;
        }

        /// <summary>
        /// 对参数进行解码
        /// </summary>
        /// <param name="databuf"></param>
        public override void SetBytes(IByteBuffer databuf)
        {
            Door = databuf.ReadByte();

            //LocalName = databuf.ReadString(60, System.Text.Encoding.GetEncoding("gb2312"));
            byte[] bName = new byte[60];
            databuf.ReadBytes(bName);
            //LocalName = Encoding.GetEncoding("GB2312").GetString(bName).Replace("\0", "");
            //LocalName = Encoding.ASCII.GetString(bName).Replace("\0", "");
            LocalName = Encoding.BigEndianUnicode.GetString(bName).Replace("\0", "");
            InOut = databuf.ReadByte();
        }
    }
}
