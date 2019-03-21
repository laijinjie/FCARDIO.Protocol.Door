using DotNetty.Buffers;
using FCARDIO.Core.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.SystemParameter.ConnectPassword
{
    /// <summary>
    /// 设置控制器通讯密码_参数
    /// </summary>
    public class WriteConnectPassword_Parameter : AbstractParameter
    {
        public byte[] PWDBuf;

        public WriteConnectPassword_Parameter() { }

        public WriteConnectPassword_Parameter(byte[] _PWD)
        {
            PWDBuf = _PWD;

            if (!checkedParameter())
            {
                throw new ArgumentException("PWD Error");
            }
        }

        public WriteConnectPassword_Parameter(string _PWD) : this(_PWD.GetBytes()) { }

        /// <summary>
        /// 检查参数
        /// </summary>
        /// <returns></returns>
        public override bool checkedParameter()
        {
            if (PWDBuf == null)
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
            if (PWDBuf != null)
            {
                PWDBuf = null;
            }

            return;
        }

        /// <summary>
        /// 编码参数
        /// </summary>
        /// <param name="databuf"></param>
        /// <returns></returns>
        public override IByteBuffer GetBytes(IByteBuffer databuf)
        {
            databuf.WriteBytes(PWDBuf);

            return databuf;
        }

        /// <summary>
        /// 获取数据长度
        /// </summary>
        /// <returns></returns>
        public override int GetDataLen()
        {
            return 0x04;
        }

        /// <summary>
        /// 解码参数
        /// </summary>
        /// <param name="databuf"></param>
        public override void SetBytes(IByteBuffer databuf)
        {
            if (PWDBuf == null)
            {
                PWDBuf = new byte[4];
            }

            if (databuf.ReadableBytes != 4)
            {
                throw new ArgumentException("databuf Error");
            }

            databuf.ReadBytes(PWDBuf);
        }
    }
}