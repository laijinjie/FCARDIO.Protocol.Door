using DotNetty.Buffers;
using FCARDIO.Core.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.Password
{
    /// <summary>
    /// 读取所有密码 结果
    /// </summary>
    public class ReadAllPassword_Result : INCommandResult 
    {
        /// <summary>
        /// 已读取到的节假日列表
        /// </summary>
        public List<PasswordDetail> Passowrds;

        /// <summary>
        /// 
        /// </summary>
        public ReadAllPassword_Result()
        {
            Passowrds = new List<PasswordDetail>();
        }

        /// <summary>
        /// 将字节缓冲区反序列化到实例
        /// </summary>
        /// <param name="iTotal">预计从缓冲区中解码出的最大数量</param>
        /// <param name="databufs">字节缓冲区列表</param>
        public void SetBytes(int iTotal, List<IByteBuffer> databufs)
        {
            Passowrds.Clear();
            Passowrds.Capacity = iTotal + 10;
            foreach (IByteBuffer buf in databufs)
            {
                int iCount = buf.ReadInt();
                for (int i = 0; i < iCount; i++)
                {
                    PasswordDetail dtl = new PasswordDetail();
                    dtl.SetBytes(buf);
                    Passowrds.Add(dtl);
                }
            }
            //Count = Holidays.Count;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            Passowrds.Clear();
            Passowrds = null;
        }
    }
}
