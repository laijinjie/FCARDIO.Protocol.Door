using DotNetty.Buffers;
using FCARDIO.Core.Command;
using FCARDIO.Protocol.FC8800;
using FCARDIO.Protocol.OnlineAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.Door.ManyCardOpenGroup
{
    public class ReadManyCardOpenGroup : FC8800Command
    {
        private int Total;

        private List<IByteBuffer> mReadBuffers;

        public ReadManyCardOpenGroup(INCommandDetail cd, WriteManyCardOpenGroup_Parameter value) : base(cd, value) { }

        /// <summary>
        /// 检查参数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            WriteManyCardOpenGroup_Parameter model = value as WriteManyCardOpenGroup_Parameter;
            if (model == null) return false;

            return model.checkedParameter();
        }

        /// <summary>
        /// 创建一个通讯指令
        /// </summary>
        protected override void CreatePacket0()
        {
            Packet(0x03, 0x18, 0x03, 0x02, GetCmdDate());
        }

        /// <summary>
        /// 获取参数结构的字节编码
        /// </summary>
        /// <returns></returns>
        private IByteBuffer GetCmdDate()
        {
            WriteManyCardOpenGroup_Parameter model = _Parameter as WriteManyCardOpenGroup_Parameter;
            var acl = _Connector.GetByteBufAllocator();
            var buf = acl.Buffer(model.GetDataLen());
            model.GetBytes(buf);
            return buf;
        }

        private IByteBuffer GetCmdDateNext()
        {
            WriteManyCardOpenGroup_Parameter model = _Parameter as WriteManyCardOpenGroup_Parameter;
            var acl = _Connector.GetByteBufAllocator();
            var buf = acl.Buffer(3);
            model.GetBytes(buf);
            return buf;
        }

        /// <summary>
        /// 处理返回值
        /// </summary>
        /// <param name="oPck"></param>
        protected override void CommandNext1(OnlineAccessPacket oPck)
        {
            if (CheckResponse(oPck,0x03))
            {
                var buf = oPck.CmdData;
                ManyCardOpenGroup_Result rst = new ManyCardOpenGroup_Result();
                _Result = rst;
                rst.SetBytes(buf);
                buf.Retain();
                //mReadBuffers.Add(buf);
                CommandWaitResponse();
            }
            else if (CheckResponse(oPck))
            {
                var buf = oPck.CmdData;
                
                //CommandCompleted();
                //Packet(0x03, 0x18, 0x03, 0x03, GetCmdDateNext());
            }
            if (CheckResponse(oPck, 0x33, 0x18, 0x53, (uint)(1 + 9 * 5)))
            {
                var buf = oPck.CmdData;
                ManyCardOpenGroup_Result rst = new ManyCardOpenGroup_Result();
                _Result = rst;
                //rst.SetBytesNext(buf);
                CommandCompleted();
            }
        }

        /// <summary>
        /// 命令重发时需要处理的函数
        /// </summary>
        protected override void CommandReSend()
        {
            return;
        }

        /// <summary>
        /// 命令释放时选哟处理的函数
        /// </summary>
        protected override void Release1()
        {
            return;
        }
    }
}
