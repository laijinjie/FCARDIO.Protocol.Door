using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.USB.OfflinePatrol.Data
{
    /// <summary>
    /// 巡更人员信息
    /// </summary>
    public class PatrolEmpl
    {
        /// <summary>
        /// 工号
        /// </summary>
        public ushort PCode;

        /// <summary>
        /// 卡号
        /// </summary>
        public uint CardData;

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name;
    }
}
