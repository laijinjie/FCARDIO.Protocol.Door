using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;

namespace FCARDIO.Protocol.Door.FC8800.Door.AnyCardSetting
{
    /// <summary>
    /// 全卡开门功能
    /// 所有的卡都能开门，不需要权限首选注册，只要读卡器能识别就能开门。
    /// </summary>
    public class WriteAnyCardSetting_Parameter
        :AbstractParameter
    {
        /// <summary>
        /// 门号
        /// 门端口在控制板中的索引号，取值：1-4
        /// </summary>
        public int DoorNum;

        /// <summary>
        /// 是否启用全卡开门功能
        /// </summary>
        public bool Use;

        /// <summary>
        /// 是否启用在刷卡开门后保存卡片权限
        /// 保存后，以后关闭全卡功能，此卡也能开门。
        /// </summary>
        public bool AutoSave;

        public WriteAnyCardSetting_Parameter() { }

        /// <summary>
        /// 创建结构,并传入门号和是否开启此功能
        /// </summary>
        /// <param name="door">门号</param>
        /// <param name="use">是否启用全卡开门功能</param>
        /// <param name="auto">是否启用在刷卡开门后保存卡片权限</param>
        public WriteAnyCardSetting_Parameter(byte door,bool use,bool auto)
        {
            DoorNum = door;
            Use = use;
            AutoSave = auto;
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
            if (databuf.WritableBytes != 4)
            {
                throw new ArgumentException("door Error!");
            }
            databuf.WriteByte(DoorNum);
            databuf.WriteBoolean(Use);
            databuf.WriteBoolean(AutoSave);
            return databuf;
        }

        /// <summary>
        /// 指定此类结构编码为字节缓冲后的长度
        /// </summary>
        /// <returns></returns>
        public override int GetDataLen()
        {
            return 4;
        }

        /// <summary>
        /// 将字节缓冲解码为类结构
        /// </summary>
        /// <param name="databuf"></param>
        public override void SetBytes(IByteBuffer databuf)
        {
            DoorNum = databuf.ReadByte();
            Use = databuf.ReadBoolean();
            AutoSave = databuf.ReadBoolean();
        }
    }
}
