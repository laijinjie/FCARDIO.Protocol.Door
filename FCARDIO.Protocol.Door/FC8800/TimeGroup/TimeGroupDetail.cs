using DotNetty.Buffers;
using FCARDIO.Core.Data;
using FCARDIO.Protocol.Door.FC8800.Data.TimeGroup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.TimeGroup
{
    /// <summary>
    /// 表示一个开门时段
    /// </summary>
    public class TimeGroupDetail : AbstractData
    {
        /// <summary>
        /// 一组开门时段
        /// </summary>
        public WeekTimeGroup WeekTimeGroup { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="databuf"></param>
        /// <returns></returns>
        public override IByteBuffer GetBytes(IByteBuffer databuf)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetDataLen()
        {
            return 0xE0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="databuf"></param>
        public override void SetBytes(IByteBuffer databuf)
        {
            throw new NotImplementedException();
        }
    }
}
