using DotNetty.Buffers;
using FCARDIO.Core.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.Door.MultiCard
{
    /// <summary>
    /// 
    /// </summary>
    public class MultiCard_Result : WriteMultiCard_Parameter, INCommandResult
    {
        /// <summary>
        /// 将 字节流  转换为 多卡开门检测模式参数
        /// </summary>
        /// <param name="databuf"></param>
        /// <returns></returns>
        internal IByteBuffer CheckMode_SetBytes(IByteBuffer databuf)
        {
            if (databuf.ReadableBytes != 2)
            {
                throw new ArgumentException("buf Error!");
            }
            //DoorNum = databuf.ReadByte();
            Mode = databuf.ReadByte();
            AntiPassback = databuf.ReadByte();
            return databuf;
        }

        /// <summary>
        /// 将 字节流  转换为 多卡开门验证方式
        /// </summary>
        /// <param name="databuf"></param>
        /// <returns></returns>
        internal IByteBuffer VerifyType_SetBytes(IByteBuffer databuf)
        {
            if (databuf.ReadableBytes != 3)
            {
                throw new ArgumentException("buf Error!");
            }
            //DoorNum = databuf.ReadByte();
            VerifyType = databuf.ReadByte();
            AGroupCount = databuf.ReadByte();
            BGroupCount = databuf.ReadByte();
            return databuf;
        }

    }
}
