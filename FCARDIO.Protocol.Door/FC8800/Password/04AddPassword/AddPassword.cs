using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;
using FCARDIO.Core.Command;
using FCARDIO.Core.Packet;
using FCARDIO.Protocol.OnlineAccess;

namespace FCARDIO.Protocol.Door.FC8800.Password
{
    /// <summary>
    /// FC88 将密码列表写入到控制器
    /// </summary>
    public class AddPassword : WritePasswordBase<PasswordDetail,Password_Parameter>
    {
        
        /// <summary>
        /// 初始化参数
        /// </summary>
        /// <param name="cd"></param>
        /// <param name="par"></param>
        public AddPassword(INCommandDetail cd, Password_Parameter par) : base(cd, par)
        {
            MaxBufSize = (mBatchCount * mParDataLen) + 4;
        }

        /// <summary>
        /// 将数据部分写入到缓冲区
        /// </summary>
        /// <param name="password">要写入的密码</param>
        /// <param name="databuf"></param>
        protected override void WritePasswordBodyToBuf(IByteBuffer databuf, PasswordDetail password)
        {
            password.GetBytes(databuf);
        }

        /// <summary>
        /// 将命令打包成一个Packet，准备发送
        /// </summary>
        protected override void CreateCommandPacket0()
        {
            var buf = GetNewCmdDataBuf(MaxBufSize);
            WritePasswordToBuf(buf);
            Packet(0x5, 0x4, 0x00, (uint)buf.ReadableBytes, buf);
        }
    }
}
