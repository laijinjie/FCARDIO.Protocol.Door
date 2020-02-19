using DoNetDrive.Core.Command;
using DoNetDrive.Protocol.Door.Door8800.TemplateMethod;
using DoNetDrive.Protocol.OnlineAccess;
using DotNetty.Buffers;
using System;
using System.Collections.Generic;

namespace DoNetDrive.Protocol.POS.Subsidy
{
    public class AddSussidy : TemplateWriteData_Base<Data.SubsidyDetail>
    {
        /// <summary>
        /// 当前命令进度
        /// </summary>
        protected int mStep = 0;

        /// <summary>
        /// 初始化参数
        /// </summary>
        /// <param name="cd"></param>
        /// <param name="par"></param>
        public AddSussidy(INCommandDetail cd, AddSussidy_Parameter par) : base(cd, par)
        {
            MaxBufSize = (mBatchCount * mParDataLen) + 4;
        }

        protected override void CreatePacket0()
        {
            mStep = 1;
            Packet(0x07, 0x04, 0x00);
        }

        /// <summary>
        /// 检测结束指令返回值
        /// </summary>
        /// <param name="oPck"></param>
        /// <returns></returns>
        protected override bool CheckResponseCompleted(OnlineAccessPacket oPck)
        {
            return false;
        }

        /// <summary>
        /// 重写父类对处理返回值的定义
        /// </summary>
        /// <param name="oPck"></param>
        protected override void CommandNext0(OnlineAccessPacket oPck)
        {
            switch (mStep)
            {
                case 1://处理开始写入指令返回
                    if (CheckResponse_OK(oPck))
                    {
                        _ProcessStep++;
                        //硬件已准备就绪，开始写入卡

                        //创建一个通讯缓冲区
                        var buf = GetNewCmdDataBuf(MaxBufSize);
                        WriteDataToBuf(buf);
                        Packet(0x07, 0x04, 0x01, (uint)buf.ReadableBytes, buf);
                        CommandReady();//设定命令当前状态为准备就绪，等待发送
                        mStep = 2;//使命令进入下一个阶段
                        return;
                    }
                    break;
                case 2:
                    if (CheckResponse_OK(oPck))
                    {
                        //继续发下一包
                        CommandNext1(oPck);
                    }
                    break;

                default:
                    break;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="DataList"></param>
        /// <returns></returns>
        protected override TemplateResult_Base CreateResult(List<TemplateData_Base> DataList)
        {
            ReadAllSubsidy_Result result = new ReadAllSubsidy_Result(DataList);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="databuf"></param>
        /// <param name="data"></param>
        protected override void WriteDataBodyToBuf(IByteBuffer databuf, TemplateData_Base data)
        {
            Data.SubsidyDetail subsidy = data as Data.SubsidyDetail;
            subsidy.GetBytes(databuf);
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void CreateCommandPacket0()
        {
            throw new NotImplementedException();
        }
    }
}
