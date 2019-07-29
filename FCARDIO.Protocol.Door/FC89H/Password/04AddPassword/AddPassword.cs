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
    /// FC89H 将密码列表写入到控制器
    /// </summary>
    public class AddPassword : WritePasswordBase<PasswordDetail, AddPassword_Parameter>
    {
        /// <summary>
        /// 将命令打包成一个Packet，准备发送
        /// </summary>
        protected override void CreateCommandPacket0()
        {
            var buf = GetNewCmdDataBuf(MaxBufSize);
            WritePasswordToBuf(buf);
            Packet(0x5, 0x4, 0x00, (uint)buf.ReadableBytes, buf);
        }

        /// <summary>
        /// 初始化参数
        /// </summary>
        /// <param name="cd"></param>
        /// <param name="par"></param>
        public AddPassword(INCommandDetail cd, AddPassword_Parameter par) : base(cd, par)
        {
            MaxBufSize = (mBatchCount * mParDataLen) + 4;
        }

        /// <summary>
        /// 将数据部分写入到缓冲区
        /// </summary>
        /// <param name="databuf"></param>
        /// <param name="password">要写入到缓冲区密码</param>
        protected override void WritePasswordBodyToBuf(IByteBuffer databuf, PasswordDetail password)
        {
            password.GetBytes(databuf);
        }

        /// <summary>
        /// 创建返回值
        /// </summary>
        /// <param name="passwordList">无法写入的密码列表</param>
        /// <returns></returns>
        protected override ReadAllPassword_Result_Base<PasswordDetail> CreateResult(List<PasswordDetail> passwordList)
        {
            ReadAllPassword_Result result = new ReadAllPassword_Result(passwordList);
            return result;
        }
    }
}

