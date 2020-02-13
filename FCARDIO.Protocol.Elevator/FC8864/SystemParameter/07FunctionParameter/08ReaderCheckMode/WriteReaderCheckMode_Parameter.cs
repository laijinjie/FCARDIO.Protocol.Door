using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;

namespace FCARDIO.Protocol.Elevator.FC8864.SystemParameter.FunctionParameter
{
    /// <summary>
    /// 设置读卡器数据校验_参数
    /// </summary>
    public class WriteReaderCheckMode_Parameter : Protocol.Door.FC8800.SystemParameter.FunctionParameter.WriteReaderCheckMode_Parameter
    {
        public WriteReaderCheckMode_Parameter()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_ReaderCheckMode"></param>
        public WriteReaderCheckMode_Parameter(byte _ReaderCheckMode)
        {
            ReaderCheckMode = _ReaderCheckMode;
        }

    }
}