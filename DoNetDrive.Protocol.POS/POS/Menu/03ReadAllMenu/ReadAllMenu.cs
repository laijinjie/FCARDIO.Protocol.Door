using DoNetDrive.Core.Command;
using DoNetDrive.Protocol.Door.Door8800.TemplateMethod;
using DoNetDrive.Protocol.OnlineAccess;
using DoNetDrive.Protocol.POS.Data;
using DotNetty.Buffers;
using System.Collections.Generic;

namespace DoNetDrive.Protocol.POS.Menu
{
    public class ReadAllMenu : TemplateReadData_Base<MenuDetail>
    {
        public ReadAllMenu(INCommandDetail cd) : base(cd)
        {
           
        }

        /// <summary>
        /// 创建一个通讯指令
        /// </summary>
        protected override void CreatePacket0()
        {
            Packet(0x06, 3);
            mReadBuffers = new List<IByteBuffer>();
            _ProcessMax = 1;
        }

        /// <summary>
        /// 检测下一包指令返回值
        /// </summary>
        /// <param name="oPck"></param>
        /// <returns></returns>
        protected override bool CheckResponseNext(OnlineAccessPacket oPck)
        {
            return (oPck.CmdType == 0x36 &&
                oPck.CmdIndex == 3 &&
                oPck.CmdPar == 0);
        }

        /// <summary>
        /// 检测结束指令返回值
        /// </summary>
        /// <param name="oPck"></param>
        /// <returns></returns>
        protected override bool CheckResponseCompleted(OnlineAccessPacket oPck)
        {
            return (oPck.CmdType == 0x36 &&
                oPck.CmdIndex == 3 &&
                oPck.CmdPar == 0xff && oPck.DataLen == 2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataList"></param>
        /// <returns></returns>
        protected override TemplateResult_Base CreateResult(List<TemplateData_Base> dataList)
        {
            ReadAllMenu_Result result = new ReadAllMenu_Result(dataList);
            return result;
        }
    }
}
