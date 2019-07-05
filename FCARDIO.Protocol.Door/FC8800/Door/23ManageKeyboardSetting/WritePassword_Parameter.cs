using DotNetty.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCARDIO.Protocol.Util;

namespace FCARDIO.Protocol.Door.FC8800.Door.ManageKeyboardSetting
{
    /// <summary>
    /// 管理密码
    /// </summary>
    public class WritePassword_Parameter : AbstractParameter
    {
        /// <summary>
        /// 门号
        /// 门端口在控制板中的索引号，取值：1-4
        /// </summary>
        public int DoorNum { get; set; }

        /// <summary>
        /// 是否启用语音播报功能
        /// </summary>
        public string Password { get; set; }

        public WritePassword_Parameter()
        {

        }

        public WritePassword_Parameter(byte door, string password)
        {
            DoorNum = door;
            Password = password;
        }

        /// <summary>
        /// 检查参数
        /// </summary>
        /// <returns></returns>
        public override bool checkedParameter()
        {
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
            if (databuf.WritableBytes != 5)
            {
                throw new ArgumentException("door Error!");
            }
            databuf.WriteByte(DoorNum);

            Password = StringUtil.FillHexString(Password, 8, "F", true);
            StringUtil.HextoByteBuf(Password, databuf);
            return databuf;
        }

        /// <summary>
        /// 指定此类结构编码为字节缓冲后的长度
        /// </summary>
        /// <returns></returns>
        public override int GetDataLen()
        {
            return 5;
        }

        /// <summary>
        /// 将字节缓冲解码为类结构
        /// </summary>
        /// <param name="databuf"></param>
        public override void SetBytes(IByteBuffer databuf)
        {
            DoorNum = databuf.ReadByte();
            Password = StringUtil.ByteBufToHex(databuf, 4).TrimEnd('F');
        }
    }
}
