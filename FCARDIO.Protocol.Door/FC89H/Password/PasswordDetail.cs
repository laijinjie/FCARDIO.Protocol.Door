using DotNetty.Buffers;
using FCARDIO.Protocol.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC89H.Password
{
    /// <summary>
    /// FC89H 表示一个密码表
    /// </summary>
    public class PasswordDetail : FC8800.Password.PasswordDetail
    {
       
        /// <summary>
        /// 将密码序列化并写入buf中
        /// </summary>
        /// <param name="data"></param>
        protected override void WritePassword(IByteBuffer data)
        {
            data.WriteShort(OpenTimes);
            TimeUtil.DateToBCD_yyMMddhhmm(data, Expiry);
        }

        /// <summary>
        /// 从buf中读取密码
        /// </summary>
        /// <param name="data"></param>
        protected override void ReadPassword(IByteBuffer data)
        {
            OpenTimes = data.ReadUnsignedShort();
            Expiry = TimeUtil.BCDTimeToDate_yyMMddhhmm(data);
        }

        /// <summary>
        /// 写入 要删除的密码信息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override IByteBuffer GetDeleteBytes(IByteBuffer data)
        {
            Password = StringUtil.FillHexString(Password, 8, "F", true);
            StringUtil.HextoByteBuf(Password, data);
            return data;
        }
    }
}
