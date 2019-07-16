using DotNetty.Buffers;
using FCARDIO.Core.Command;
using FCARDIO.Protocol.Door.FC8800.Password;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC89H.Password
{
    /// <summary>
    /// FC89H 将密码列表从控制器删除
    /// </summary>
    public class DeletePassword : WritePasswordBase<PasswordDetail, DeletePassword_Parameter>
    {

        /// <summary>
        /// 将命令打包成一个Packet，准备发送
        /// </summary>
        protected override void CreateCommandPacket0()
        {
            var buf = GetNewCmdDataBuf(MaxBufSize);
            WritePasswordToBuf(buf);
            Packet(0x5, 0x5, 0x00, (uint)buf.ReadableBytes, buf);
        }

        /// <summary>
        /// 初始化参数
        /// </summary>
        /// <param name="cd"></param>
        /// <param name="par"></param>
        public DeletePassword(INCommandDetail cd, DeletePassword_Parameter par) : base(cd, par)
        {
            MaxBufSize = (mBatchCount * mDeleteDataLen) + 4;
        }

        /// <summary>
        /// 将数据部分写入到缓冲区
        /// </summary>
        /// <param name="i"></param>
        /// <param name="databuf"></param>
        /// <param name="lst"></param>
        protected override void WritePasswordBodyToBuf(int i, IByteBuffer databuf, List<PasswordDetail> lst)
        {
            lst[mIndex + i].GetDeleteBytes(databuf);
        }
    }
}