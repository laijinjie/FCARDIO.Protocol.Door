using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;
using FCARDIO.Core.Command;
using FCARDIO.Protocol.Door.FC8800.Data;
using FCARDIO.Protocol.OnlineAccess;

namespace FCARDIO.Protocol.Door.FC8800.Card.CardDataBase
{
    /// <summary>
    /// 从控制器中读取卡片数据<br/>
    /// 成功返回结果参考 @link ReadCardDataBase_Result 
    /// </summary>
    public class ReadCardDataBase : FC8800Command_ReadParameter
    {
        private int mStep;//指示当前命令进行的步骤
        /// <summary>
        /// 读取到的节假日缓冲
        /// </summary>
        private List<IByteBuffer> mReadBuffers;

        private int mRecordCardSize;//记录的卡数量

        public ReadCardDataBase(INCommandDetail cd) : base(cd, null)
        {
        }
        /// <summary>
        /// 初始化命令结构 
        /// </summary>
        /// <param name="cd"></param>
        /// <param name="parameter"></param>
        public ReadCardDataBase(INCommandDetail cd, ReadCardDataBase_Parameter parameter) : base(cd, parameter) { }

        /// <summary>
        /// 创建参数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            ReadCardDataBase_Parameter model = value as ReadCardDataBase_Parameter;
            if (model == null) return false;
            return model.checkedParameter();
        }

        /// <summary>
        /// 创建一个通讯指令
        /// </summary>
        protected override void CreatePacket0()
        {
            //Packet(0x07, 0x02, 0x00, 0x01, GetCmdDate());
            Packet(0x07, 0x01, 0x00);
            mReadBuffers = new List<IByteBuffer>();
        }

        /// <summary>
        /// 获取参数结构的字节编码
        /// </summary>
        /// <returns></returns>
        private IByteBuffer GetCmdDate()
        {
            ReadCardDataBase_Parameter model = _Parameter as ReadCardDataBase_Parameter;
            var acl = _Connector.GetByteBufAllocator();
            var buf = acl.Buffer(model.GetDataLen());
            model.GetBytes(buf);
            return buf;
        }


        /// <summary>
        /// 处理返回值
        /// </summary>
        /// <param name="oPck"></param>
        protected override void CommandNext1(OnlineAccessPacket oPck)
        {
            //if (CheckResponse(oPck, 0x04))
            //{
            //    var buf = oPck.CmdData;
            //    ReadCardDataBase_Result rst = new ReadCardDataBase_Result();
            //    _Result = rst;
            //    rst.SetBytes(buf);
            //    CommandCompleted();
            //}


            //应答：密码
            if (CheckResponse(oPck))
            {
                var buf = oPck.CmdData;
                buf.Retain();
                mReadBuffers.Add(buf);
                CommandWaitResponse();
            }
            //应答：传输结束
            if (CheckResponse(oPck, 7, 3, 0xFF, 4))
            {

                //var buf = oPck.CmdData;
                int iTotal = oPck.CmdData.ReadInt();

                ReadCardDataBase_Result rst = new ReadCardDataBase_Result();
                rst.DataBaseSize = iTotal;
                List<Data.CardDetailBase> _list = new List<Data.CardDetailBase>(iTotal + 10);
                foreach (IByteBuffer buf in mReadBuffers)
                {
                    int iCardSize = buf.ReadInt();
                    for (int i = 0; i < iCardSize; i++)
                    {
                        Data.CardDetail card = new Data.CardDetail();
                        card.SetBytes(buf);
                        _list.Add(card);
                    }
                }
                rst.CardList = _list;
                _Result = rst;


                //rst.SetBytes(mReadBuffers);
                ClearBuf();
                CommandCompleted();
            }
        }

        /// <summary>
        /// 命令重发时需要的函数
        /// </summary>
        protected override void CommandReSend()
        {
            return;
        }

        /// <summary>
        /// 命令释放时需要的参数
        /// </summary>
        protected override void Release1()
        {
            return;
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
