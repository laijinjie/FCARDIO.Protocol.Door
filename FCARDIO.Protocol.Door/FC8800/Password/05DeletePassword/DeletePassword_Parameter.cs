using DotNetty.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.Password
{
    /// <summary>
    /// 删除密码参数
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DeletePassword_Parameter<T> : AbstractParameter where T : PasswordDetail, new()
    {

        /// <summary>
        /// 要删除的密码集合
        /// </summary>
        public List<T> ListPassword;

        /// <summary>
        /// 初始化参数
        /// </summary>
        /// <param name="list">要删除的密码集合</param>
        public DeletePassword_Parameter(List<T> list)
        {
            ListPassword = list;
           
        }

        /// <summary>
        /// 检查参数
        /// </summary>
        /// <returns></returns>
        public override bool checkedParameter()
        {
            if (ListPassword == null || ListPassword.Count == 0)
            {
                return false;
            }
            int iOut = 0;
            foreach (var item in ListPassword)
            {
                if (item.Password.Length > 8 || item.Password.Length < 4)
                {
                    throw new ArgumentException("Password.Length Error!");
                }
                if (!int.TryParse(item.Password, out iOut) || iOut < 0)
                {
                    throw new ArgumentException("Password Error!");
                }

            }
            return true;
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public override void Dispose()
        {
            ListPassword = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="databuf"></param>
        /// <returns></returns>
        public override IByteBuffer GetBytes(IByteBuffer databuf)
        {
            
            return databuf;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetDataLen()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="databuf"></param>
        public override void SetBytes(IByteBuffer databuf)
        {
            throw new NotImplementedException();
        }
    }
}
