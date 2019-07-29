using DotNetty.Buffers;
using FCARDIO.Core.Command;
using FCARDIO.Protocol.Door.FC8800.Password;
using FCARDIO.Protocol.FC8800;
using FCARDIO.Protocol.OnlineAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Elevator.FC8864.Password
{
    /// <summary>
    /// FC88 将密码列表从控制器删除
    /// </summary>
    public class DeletePassword : WritePasswordBase<PasswordDetail,Password_Parameter>
    {
        /// <summary>
        /// 将命令打包成一个Packet，准备发送
        /// </summary>
        protected override void CreateCommandPacket0()
        {
            var buf = GetNewCmdDataBuf(MaxBufSize);
            WritePasswordToBuf(buf);
            Packet(CmdType, 0x5, 0x00, (uint)buf.ReadableBytes, buf);
        }

        /// <summary>
        /// 初始化参数
        /// </summary>
        /// <param name="cd"></param>
        /// <param name="par"></param>
        public DeletePassword(INCommandDetail cd, Password_Parameter par) : base(cd, par)
        {
            MaxBufSize = (mBatchCount * mDeleteDataLen) + 4;
            CmdType = 0x45;
            CheckResponseCmdType = 0x25;
        }

        /// <summary>
        /// 将数据部分写入到缓冲区
        /// </summary>
        /// <param name="databuf"></param>
        /// <param name="password">要写入到缓冲区的密码</param>
        protected override void WritePasswordBodyToBuf(IByteBuffer databuf, PasswordDetail password)
        {
            password.GetDeleteBytes(databuf);
        }

        /// <summary>
        /// 处理返回值
        /// </summary>
        /// <param name="oPck"></param>
        protected override void CommandNext1(OnlineAccessPacket oPck)
        {
            base.CommandNext1(oPck);
        }

        /// <summary>
        /// 没有实现
        /// </summary>
        /// <param name="passwordList"></param>
        /// <returns></returns>
        protected override ReadAllPassword_Result_Base<PasswordDetail> CreateResult(List<PasswordDetail> passwordList)
        {
            throw new NotImplementedException();
        }
    }
}
