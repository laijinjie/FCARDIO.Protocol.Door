using FCARDIO.Core.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.USB.CardReader.ICCard.Sector
{
    /// <summary>
    /// 写扇区内容
    /// </summary>
    public class WriteSector : Write_Command
    {
        /// <summary>
        /// 初始化命令
        /// </summary>
        /// <param name="cd"></param>
        /// <param name="par"></param>
        public WriteSector(INCommandDetail cd, WriteSector_Parameter par) : base(cd, par)
        {

        }

        /// <summary>
        /// 将命令打包成一个Packet，准备发送
        /// </summary>
        protected override void CreatePacket0()
        {
            WriteSector_Parameter model = _Parameter as WriteSector_Parameter;
            Packet(0x02, 0x02, (uint)model.GetDataLen(), model.GetBytes(GetNewCmdDataBuf(model.GetDataLen())));
        }

        /// <summary>
        /// 检查参数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            WriteSector_Parameter model = value as WriteSector_Parameter;
            if (model == null) return false;
            return model.checkedParameter();
        }
    }
}
