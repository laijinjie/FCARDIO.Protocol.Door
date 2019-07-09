using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;
using FCARDIO.Core.Command;
using FCARDIO.Core.Packet;
using FCARDIO.Protocol.OnlineAccess;

namespace FCARDIO.Protocol.Door.FC8800.Door.MultiCard
{
    /// <summary>
    /// 写多卡参数
    /// </summary>
    public class WriteMultiCard : FC8800Command_WriteParameter
    {
        private int writeIndex = 0;

        private int maxCount = 0;

        public int Step { get; set; }

        /// <summary>
        /// 初始化命令结构
        /// </summary>
        /// <param name="cd"></param>
        /// <param name="value"></param>
        public WriteMultiCard(INCommandDetail cd, WriteMultiCard_Parameter value) : base(cd, value) { }
        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            WriteMultiCard_Parameter model = value as WriteMultiCard_Parameter;
            if (model == null) return false;
            return model.checkedParameter();
        }

        protected override void CreatePacket0()
        {
            Packet(0x03, 0x17, 0x00, 3, GetCmdData());
            Step = 2;

        }

        /// <summary>
        /// 二十七 多卡开门A组设置
        /// </summary>
        protected virtual void WriteMultiCardData()
        {
            WriteMultiCard_Parameter model = _Parameter as WriteMultiCard_Parameter;
            model.GroupNum++;
            Packet(0x03, 0x18, 0x02, 3, GetCmdData());
            CommandReady();
        }

        /// <summary>
        /// 设置组中的卡号
        /// </summary>
        /// <param name="model"></param>
        protected virtual void WriteMultiCardDataNext(WriteMultiCard_Parameter model)
        {
            if (writeIndex < maxCount)
            {
                model.SetWriteIndex(writeIndex);
                var acl = _Connector.GetByteBufAllocator();
                var len = model.GetDataLen();
                var buf = acl.Buffer(len);
                Packet(0x03, 0x18, 0x52, (uint)len, model.GetBytes(buf));
                writeIndex += 20;
                if (writeIndex > 50)
                {
                    writeIndex = 0;
                    model.GroupNum++;
                    model.Step = 2;
                }

                CommandReady();
            }
            else
            {
                CommandCompleted();
            }
        }

        protected override void CommandNext(INPacket readPacket)
        {
            
            switch (Step)
            {
                case 2://二十六、多卡开门验证方式
                    Packet(0x03, 0x18, 0x00, 4, GetCmdData());
                    CommandReady();//设定命令当前状态为准备就绪，等待发送
                    Step = 3;
                    break;
                case 3://二十七 多卡开门A组设置

                    WriteMultiCard_Parameter model = _Parameter as WriteMultiCard_Parameter;
                    maxCount = model.AListCardData.Count;
                    Step = 4;
                    WriteMultiCardData();
                    //WriteMultiCardDataNext(model);
                    break;
                case 4:
                    WriteMultiCard_Parameter model4 = _Parameter as WriteMultiCard_Parameter;
                    WriteMultiCardDataNext(model4);
                    break;
                default:
                    break;
            }
            
           
        }

        /// <summary>
        /// 处理返回值
        /// </summary>
        /// <param name="oPck"></param>
        protected override void CommandNext1(OnlineAccessPacket oPck)
        {
            
        }
        private IByteBuffer GetCmdData()
        {
            WriteMultiCard_Parameter model = _Parameter as WriteMultiCard_Parameter;
            var acl = _Connector.GetByteBufAllocator();
            var buf = acl.Buffer(model.GetDataLen());
            model.GetBytes(buf);
            return buf;
        }
    }
}
