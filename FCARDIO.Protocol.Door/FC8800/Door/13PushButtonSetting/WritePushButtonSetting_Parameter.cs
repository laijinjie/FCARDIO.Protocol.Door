using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;
using FCARDIO.Core.Extension;

namespace FCARDIO.Protocol.Door.FC8800.Door.PushButtonSetting
{
    ///
    public class WritePushButtonSetting_Parameter
        : AbstractParameter
    {
        /// <summary>
        /// 门号
        /// 门端口在控制版中的索引号，取值：1-4
        /// </summary>
        public int DoorNum;

        /// <summary>
        /// 是否启用出门按钮功能
        /// </summary>
        public bool Use;

        /// <summary>
        /// 是否启用出门按钮常开功能
        /// 出门按钮按下5秒后门进入常开状态，再次按5秒退出常开
        /// </summary>
        public bool NormallyOpen;

        /// <summary>
        /// 出门按钮的使用时段
        /// </summary>
        public DateTime TimeGroup;

        public WritePushButtonSetting_Parameter()
        {
        }

        /// <summary>
        /// 创建结构，并传入门号和是否开启此功能
        /// </summary>
        /// <param name="door">门号</param>
        /// <param name="use">是否开启此功能</param>
        /// <param name="normallOpen">是否启用出门按钮常开功能</param>
        public WritePushButtonSetting_Parameter(byte door, bool use, bool normallyOpen)
        {
            DoorNum = door;
            Use = use;
            NormallyOpen = normallyOpen;
            //时间
            TimeGroup = new DateTime(8);
        }

        /// <summary>
        /// 检查参数
        /// </summary>
        /// <returns></returns>
        public override bool checkedParameter()
        {
            if (DoorNum > 4)
                throw new ArgumentException("door Is Max!");
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
        /// <returns></returns>
        public override IByteBuffer GetBytes(IByteBuffer databuf)
        {
            if (databuf.WritableBytes != 3)
            {
                throw new ArgumentException("databuf Error!");
            }
            databuf.WriteByte(DoorNum);
            databuf.WriteBoolean(Use);
            databuf.WriteBoolean(NormallyOpen);
            //时间？？？
            string time = TimeGroup.ToString();
            databuf.WriteBytes(time.HexToByte());

            return databuf;
        }

        /// <summary>
        /// 指定此类结构编码为字节缓冲后的长度
        /// </summary>
        /// <returns></returns>
        public override int GetDataLen()
        {
            return 3;
        }

        /// <summary>
        /// 将字节缓冲解码为类结构
        /// </summary>
        /// <param name="databuf"></param>
        public override void SetBytes(IByteBuffer databuf)
        {
            DoorNum = databuf.ReadByte();
            Use = databuf.ReadBoolean();
            NormallyOpen = databuf.ReadBoolean();

            //时间
            TimeGroup = DateTime.Parse(databuf.ReadByte().ToString());
        }
    }
}
