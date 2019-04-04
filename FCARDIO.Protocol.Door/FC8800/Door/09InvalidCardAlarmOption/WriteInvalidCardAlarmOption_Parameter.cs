using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;

namespace FCARDIO.Protocol.Door.FC8800.Door.InvalidCardAlarmOption
{
    /// <summary>
    /// 未注册卡的功能参数结构
    /// </summary>
    public class WriteInvalidCardAlarmOption_Parameter
        : AbstractParameter
    {
        /// <summary>
        /// 门号
        /// 门端口在控制板中的索引号，取值：1-4
        /// </summary>
        public int DoorNum;

        /// <summary>
        /// 是否报警功能
        /// </summary>
        public bool Use;

        public WriteInvalidCardAlarmOption_Parameter()
        {
        }
        /// <summary>
        /// 创建结构,并传入门号和是否开启此功能
        /// </summary>
        /// <param name="door">门号</param>
        /// <param name="use">是否开启此功能</param>
        public WriteInvalidCardAlarmOption_Parameter(byte door,bool use)
        {
            DoorNum = door;
            Use = use;
        }

        /// <summary>
        /// 检查参数
        /// </summary>
        public override bool checkedParameter()
        {
            if (DoorNum  > 4)
                throw new ArgumentException("invalidCardAlarmOption Is Max!");
            return true;
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public override void Dispose()
        {
            return;
        }

        /// <summary>
        /// 将结构编码为字节缓冲
        /// </summary>
        /// <param name="databuf"></param>
        public override IByteBuffer GetBytes(IByteBuffer databuf)
        {
            if (databuf.WritableBytes != 2)
            {
                throw new ArgumentException("databuf Error!");
            }
            databuf.WriteByte(DoorNum);
            databuf.WriteBoolean(Use);
            return databuf;
        }

        /// <summary>
        /// 指示此类结构编码为字节缓冲后的长度
        /// </summary>
        public override int GetDataLen()
        {
            return 2;
        }

        /// <summary>
        /// 将字节缓冲解码为类结构
        /// </summary>
        /// <param name="databuf"></param>
        public override void SetBytes(IByteBuffer databuf)
        {
            DoorNum = databuf.ReadByte();
            Use = databuf.ReadBoolean();
        }
    }
}
