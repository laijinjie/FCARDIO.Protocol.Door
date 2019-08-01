﻿using DotNetty.Buffers;
using FCARDIO.Protocol.Door.FC8800;
using FCARDIO.Core.Extension;

namespace FCARDIO.Protocol.Fingerprint.SystemParameter.IP
{
    public class WriteIP_Parameter : AbstractParameter
    {
        /// <summary>
        /// 保存通讯密码的数组
        /// </summary>
        public string Password;

        /// <summary>
        /// 构建一个空的实例
        /// </summary>
        public WriteIP_Parameter() { }

        /// <summary>
        /// 使用通讯密码初始化实例
        /// </summary>
        /// <param name="_PWD">通讯密码：十六进制字符串</param>
        public WriteIP_Parameter(string _PWD)
        {
            Password = _PWD;

        }
        /// <summary>
        /// 使用字节数组初始化实例
        /// </summary>
        /// <param name="_PWD">表示通讯密码的字节数组</param>
        public WriteIP_Parameter(byte[] _PWD) : this(_PWD.ToHex()) { }

        /// <summary>
        /// 检查参数
        /// </summary>
        /// <returns></returns>
        public override bool checkedParameter()
        {
           
            return true;
        }

        /// <summary>
        /// 获取数据长度
        /// </summary>
        /// <returns></returns>
        public override int GetDataLen()
        {
            return 4;
        }


        /// <summary>
        /// 释放资源
        /// </summary>
        public override void Dispose()
        {
            return;
        }

        /// <summary>
        /// 编码参数
        /// </summary>
        /// <param name="databuf"></param>
        /// <returns></returns>
        public override IByteBuffer GetBytes(IByteBuffer databuf)
        {
           
            return databuf;
        }



        /// <summary>
        /// 解码参数
        /// </summary>
        /// <param name="databuf"></param>
        public override void SetBytes(IByteBuffer databuf)
        {
        }
    }
}
