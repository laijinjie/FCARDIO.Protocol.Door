using DotNetty.Buffers;
using FCARDIO.Protocol.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC89H.Password
{
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
    }
}
