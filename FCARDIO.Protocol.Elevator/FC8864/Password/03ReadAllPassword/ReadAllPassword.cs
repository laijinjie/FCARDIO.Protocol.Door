using DotNetty.Buffers;
using FCARDIO.Core.Command;
using FCARDIO.Protocol.Door.FC8800.Password;
using FCARDIO.Protocol.OnlineAccess;
using System.Collections.Generic;

namespace FCARDIO.Protocol.Elevator.FC8864.Password
{
    /// <summary>
    /// 从控制器读取所有密码
    /// </summary>
    public class ReadAllPassword : ReadAllPassword_Base<PasswordDetail>
    {
        /// <summary>
        /// 初始化参数
        /// </summary>
        /// <param name="cd"></param>
        public ReadAllPassword(INCommandDetail cd) : base(cd)
        {
            CmdType = 0x45;
            CheckResponseCmdType = 0x25;
        }

        /// <summary>
        /// 创建返回值
        /// </summary>
        /// <param name="passwordList">控制器返回的密码集合</param>
        protected override ReadAllPassword_Result_Base<PasswordDetail> CreateResult(List<PasswordDetail> passwordList)
        {
            ReadAllPassword_Result result = new ReadAllPassword_Result(passwordList);
            return result;
        }

        protected override void CommandNext1(OnlineAccessPacket oPck)
        {
            base.CommandNext1(oPck);
            /*
            if (CheckResponse(oPck))
            {
                var buf = oPck.CmdData;
                buf.Retain();
                mReadBuffers.Add(buf);
                CommandWaitResponse();
            }

            if (CheckResponse(oPck, CheckResponseCmdType, 3, 0xff, 4))
            {
                var buf = oPck.CmdData;
                int iTotal = buf.ReadInt();
                _ProcessMax = iTotal;
                List<PasswordDetail> PasswordList = new List<PasswordDetail>(iTotal);
                foreach (IByteBuffer tmpbuf in mReadBuffers)
                {
                    int iCount = tmpbuf.ReadInt();
                    for (int i = 0; i < iCount; i++)
                    {
                        PasswordDetail dtl = new PasswordDetail();
                        dtl.SetBytes(tmpbuf);
                        PasswordList.Add(dtl);
                    }
                    _ProcessStep += iCount;
                    fireCommandProcessEvent();
                }

                ReadAllPassword_Result_Base<PasswordDetail> rst = CreateResult(PasswordList);
                _Result = rst;

                ClearBuf();
                CommandCompleted();
                */
        }
       
    }
}
