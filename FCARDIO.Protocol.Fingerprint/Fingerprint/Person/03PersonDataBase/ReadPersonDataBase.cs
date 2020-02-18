using DotNetty.Buffers;
using DoNetDrive.Core.Command;
using DoNetDrive.Protocol.Door.Door8800;
using DoNetDrive.Protocol.OnlineAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoNetDrive.Protocol.Fingerprint.Person
{
    /// <summary>
    /// 
    /// </summary>
    public class ReadPersonDataBase : Door8800Command_ReadParameter
    {
        /// <summary>
        /// 读取到的人员数据缓冲
        /// </summary>
        private Queue<IByteBuffer> mBufs;

        /// <summary>
        /// 指示当前命令进行的步骤
        /// </summary>
        private int mStep;

        /// <summary>
        /// 初始化命令结构
        /// </summary>
        /// <param name="detail"></param>
        public ReadPersonDataBase(INCommandDetail detail) : base(detail, null) { }

        /// <summary>
        /// 创建一个通讯指令
        /// </summary>
        protected override void CreatePacket0()
        {
            Packet(0x07, 0x01, 0x00);
            mStep = 1;
        }

        /// <summary>
        /// 处理返回值
        /// </summary>
        /// <param name="oPck"></param>
        protected override void CommandNext1(OnlineAccessPacket oPck)
        {
            switch (mStep)
            {
                case 1://读取人员数据库详情回调
                    if (CheckResponse(oPck))
                    {
                        ReadDetailCallBlack(oPck);
                    }

                    break;
                case 2://读取人员数据库内容
                    if (CheckResponse(oPck))//检查返回的是否为卡数据
                    {
                        ReadPersonDatabaseCallBlack(oPck.CmdData);
                    }

                    else if (CheckResponse(oPck,0x07,0x03,0xff, 4)) //检查数据是否返回完毕
                    {
                        ReadPersonDatabaseOverCallBlack(oPck.CmdData);
                    }

                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 读取人员数据库内容完毕
        /// </summary>
        /// <param name="cmdData"></param>
        private void ReadPersonDatabaseOverCallBlack(IByteBuffer buf)
        {
            int iCount = buf.ReadInt();//获取本次总传输的人员数量
            if (iCount > 0)
                _ProcessStep = iCount;
            fireCommandProcessEvent();
            //开始解析卡数据
            List<Data.Person> personList = new List<Data.Person>();
            while (mBufs.Count > 0)
            {
                buf = mBufs.Dequeue();
                iCount = buf.ReadByte();
                for (int i = 0; i < iCount; i++)
                {
                    Data.Person person = new Data.Person();
                    person.SetBytes(buf);
                    personList.Add(person);
                }
                buf.Release();
            }
            ReadPersonDataBase_Result result = new ReadPersonDataBase_Result(personList);
            _Result = result;

            CommandCompleted();
        }

        /// <summary>
        /// 读取人员数据库内容
        /// </summary>
        /// <param name="cmdData"></param>
        private void ReadPersonDatabaseCallBlack(IByteBuffer buf)
        {
            int iCount = buf.GetByte(0);//获取本次传输的人员数量
            _ProcessStep += iCount;
            buf.Retain();
            mBufs.Enqueue(buf);
            fireCommandProcessEvent();
            CommandWaitResponse();
        }

        /// <summary>
        /// 读取人员数据库信息回调
        /// </summary>
        /// <param name="cmdData"></param>
        private void ReadDetailCallBlack(OnlineAccessPacket oPck)
        {
            _ProcessMax = oPck.CmdData.GetInt(4);
            mBufs = new Queue<IByteBuffer>();
            mStep = 2;
            Packet(0x07, 0x03, 0x00);
            CommandReady();
        }

        /// <summary>
        /// 命令重发时需要处理的函数
        /// </summary>
        protected override void CommandReSend()
        {
            return;
        }

        /// <summary>
        /// 命令释放时需要处理的函数
        /// </summary>
        protected override void Release1()
        {
            return;
        }
    }
}
