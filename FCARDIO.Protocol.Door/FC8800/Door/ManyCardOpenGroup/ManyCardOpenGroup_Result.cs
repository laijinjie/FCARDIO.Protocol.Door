using DotNetty.Buffers;
using FCARDIO.Core.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.Door.ManyCardOpenGroup
{
    public class ManyCardOpenGroup_Result : WriteManyCardOpenGroup_Parameter, INCommandResult
    {
        /// <summary>
        /// 卡集合 (9*N)
        /// </summary>
        public Dictionary<byte,string> ListCardData { get; set; }

        public void SetBytesNext(IByteBuffer databuf)
        {

        }
    }
}
