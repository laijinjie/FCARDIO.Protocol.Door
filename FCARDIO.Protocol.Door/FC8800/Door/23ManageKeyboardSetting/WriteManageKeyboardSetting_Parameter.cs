using DotNetty.Buffers;
using FCARDIO.Core.Command;
using FCARDIO.Protocol.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.Door.ManageKeyboardSetting
{
    /// <summary>
    /// 键盘管理功能
    /// </summary>
    public class WriteManageKeyboardSetting_Parameter : DoorPort_Parameter, INCommandResult
    {
        /// <summary>
        /// 门号
        /// 门端口在控制板中的索引号，取值：1-4
        /// </summary>
        public int DoorNum;

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Use;

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 读取功能 初始化
        /// </summary>
        /// <param name="door"></param>
        public WriteManageKeyboardSetting_Parameter(byte door):base(door)
        {
            DoorNum = door;
        }
        /// <summary>
        /// 设置功能 初始化参数
        /// </summary>
        /// <param name="door">门号</param>
        /// <param name="use">是否启用</param>
        /// <param name="password">密码</param>
        public WriteManageKeyboardSetting_Parameter(byte door, bool use,string password):base(door)
        {
            DoorNum = door;
            Use = use;
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
        public IByteBuffer Setting_GetBytes(IByteBuffer databuf)
        {
            if (databuf.WritableBytes < 2)
            {
                throw new ArgumentException("databuf Error!");
            }
            databuf.WriteByte(DoorNum);
            databuf.WriteBoolean(Use);
            return databuf;
        }

        public IByteBuffer Password_GetBytes(IByteBuffer databuf)
        {
            if (databuf.WritableBytes < 5)
            {
                throw new ArgumentException("databuf Error!");
            }
            databuf.WriteByte(DoorNum);

            Password = StringUtil.FillHexString(Password, 8, "F", true);
            StringUtil.HextoByteBuf(Password, databuf);
            return databuf;
        }

        /// <summary>
        /// 将结构编码为字节缓冲
        /// </summary>
        /// <param name="databuf"></param>
        /// <returns></returns>
        public override IByteBuffer GetBytes(IByteBuffer databuf)
        {
            if (databuf.WritableBytes != 1)
            {
                return null;
            }
            databuf.WriteByte(Door);
            return databuf;
        }

        /// <summary>
        /// 指定此类结构编码为字节缓冲后的长度
        /// </summary>
        /// <returns></returns>
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
        }

        
    }
}
