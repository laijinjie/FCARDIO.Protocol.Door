using DotNetty.Buffers;
using FCARDIO.Protocol.Util;
using FCARDIO.Core.Data;
using System;
using FCARDIO.Core.Extension;

namespace FCARDIO.Protocol.Door.FC8800.Password
{
    /// <summary>
    /// 表示一个密码表
    /// </summary>
    public class PasswordDetail : AbstractData, IComparable<PasswordDetail>
    {
        /// <summary>
        /// 密码信息
        /// </summary>
        public string Password;

        /// <summary>
        /// 端口
        /// </summary>
        public int Door;

        /// <summary>
        /// 开门次数
        /// </summary>
        public int OpenTimes;

        /// <summary>
        /// 有效期
        /// </summary>
        public DateTime Expiry;


        /**
         * 获取指定门是否有权限
         *
         * @param iDoor 门号，取值范围：1-4
         * @return true 有权限，false 无权限。
         */
        public bool GetDoor(int iDoor)
        {
            if (iDoor < 0 || iDoor > 4)
            {

                throw new ArgumentException("Door 1-4");
            }
            iDoor -= 1;

            int iBitIndex = iDoor % 8;
            int iMaskValue = (int)Math.Pow(2, iBitIndex);
            int iByteValue = Door & iMaskValue;
            if (iBitIndex > 0)
            {
                iByteValue = iByteValue >> (iBitIndex);
            }
            return iByteValue == 1;
        }

        public int CompareTo(PasswordDetail other)
        {
            if (other.Password == Password)
            {
                return 0;
            }

            else
            {
                return -1;
            }
        }

        /// <summary>
        /// 写入 要上传的密码信息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override IByteBuffer GetBytes(IByteBuffer data)
        {
            data.WriteByte(Door);
            Password = StringUtil.FillHexString(Password, 8, "F", true);
            StringUtil.HextoByteBuf(Password, data);
            WritePassword(data);
            return data;
        }

        /// <summary>
        /// 写入 要删除的密码信息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public virtual IByteBuffer GetDeleteBytes(IByteBuffer data)
        {
            data.WriteByte(Door);
            Password = StringUtil.FillHexString(Password, 8, "F", true);
            StringUtil.HextoByteBuf(Password, data);
            return data;
        }

        public override int GetDataLen()
        {
            throw new NotImplementedException();
        }

        protected virtual void ReadPassword(IByteBuffer data)
        {

        }

        protected virtual void WritePassword(IByteBuffer data)
        {

        }

        /// <summary>
        /// 读取密码信息
        /// </summary>
        /// <param name="data"></param>
        public override void SetBytes(IByteBuffer data)
        {
            Door = data.ReadByte();

            byte[] btData = new byte[4];
            data.ReadBytes(btData, 0, 4);
            Password = btData.ToHex().TrimEnd('F');
            ReadPassword(data);
           
        }

    }
}
