using DotNetty.Buffers;
using FCARDIO.Core.Command;
using FCARDIO.Protocol.OnlineAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.Password
{
    /// <summary>
    /// 从控制器读取所有密码
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ReadAllPassword_Base<T> : FC8800Command_ReadParameter where T : PasswordDetail, new()
    {
        /// <summary>
        /// 读取到的密码缓冲
        /// </summary>
        protected List<IByteBuffer> mReadBuffers;

        /// <summary>
        /// 初始化命令结构
        /// </summary>
        /// <param name="cd"></param>
        public ReadAllPassword_Base(INCommandDetail cd) : base(cd)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="oPck"></param>
        protected override void CommandNext1(OnlineAccessPacket oPck)
        {
            if (CheckResponse(oPck))
            {
                var buf = oPck.CmdData;
                buf.Retain();
                mReadBuffers.Add(buf);
                CommandWaitResponse();
            }

            if (CheckResponse(oPck, 5, 3, 0xff, 4))
            {
                var buf = oPck.CmdData;
                int iTotal = buf.ReadInt();
                _ProcessMax = iTotal;
                List<T> PasswordList = new List<T>(iTotal);
                foreach (IByteBuffer tmpbuf in mReadBuffers)
                {
                    int iCount = tmpbuf.ReadInt();
                    for (int i = 0; i < iCount; i++)
                    {
                        T dtl = new T();
                        dtl.SetBytes(tmpbuf);
                        PasswordList.Add(dtl);
                    }
                    _ProcessStep += iCount;
                    fireCommandProcessEvent();
                }

                ReadAllPassword_Result_Base<T> rst = CreateResult(PasswordList);
                _Result = rst;

                ClearBuf();
                CommandCompleted();
            }
        }

        

        /// <summary>
        /// 创建返回值
        /// </summary>
        /// <param name="passwordList"></param>
        /// <param name="count"></param>
        protected abstract ReadAllPassword_Result_Base<T> CreateResult(List<T> passwordList);

        /// <summary>
        /// 创建一个通讯指令
        /// </summary>
        protected override void CreatePacket0()
        {
            Packet(5, 3);
            mReadBuffers = new List<IByteBuffer>();
            _ProcessMax = 1;
        }

        /// <summary>
        /// 清空缓冲区
        /// </summary>
        protected void ClearBuf()
        {
            foreach (IByteBuffer buf in mReadBuffers)
            {
                buf.Release();
            }
            mReadBuffers.Clear();
        }
    }
}
