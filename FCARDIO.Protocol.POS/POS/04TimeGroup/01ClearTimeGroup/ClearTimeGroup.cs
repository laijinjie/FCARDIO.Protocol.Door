using FCARDIO.Core.Command;
using FCARDIO.Protocol.OnlineAccess;

namespace FCARDIO.Protocol.POS.TimeGroup
{
    public class ClearTimeGroup : Door.FC8800.TimeGroup.ClearTimeGroup
    {
        /// <summary>
        /// 初始化参数
        /// </summary>
        /// <param name="cd"></param>
        public ClearTimeGroup(INCommandDetail cd) : base(cd)
        {
            CmdType = 0x04;
            CmdIndex = 0x01;
        }

    }
}
