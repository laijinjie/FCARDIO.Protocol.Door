using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;

namespace FCARDIO.Protocol.Door.FC8800.Password
{
    /// <summary>
    /// 添加密码 参数
    /// </summary>
    public class AddPassword_Parameter<T> : AbstractParameter where T : PasswordDetail,new ()
    {
        
        /// <summary>
        /// 要添加的密码集合
        /// </summary>
        public List<T> ListPassword { get; set; }

        /// <summary>
        /// 初始化参数
        /// </summary>
        /// <param name="list">要添加的密码集合</param>
        public AddPassword_Parameter(List<T> list)
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
                if (!int.TryParse(item.Password,out iOut) || iOut < 0)
                {
                    throw new ArgumentException("Password Error!");
                }
                if (!checkedParameterItem(item))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 检查每个密码
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        protected virtual bool checkedParameterItem(PasswordDetail password)
        {
            return true;
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public override void Dispose()
        {
            ListPassword = null;
        }
        /*
        /// <summary>
        /// 
        /// </summary>
        /// <param name="databuf"></param>
        /// <returns></returns>
        public override IByteBuffer GetBytes(IByteBuffer databuf)
        {
            int iMaxSize = BatchCount; //每个数据包最大50个卡
            int iSize = 0;
            int iIndex = 0;

            databuf.Clear();
            int iLen = GetDataLen();
            if (databuf.WritableBytes != iLen)
            {
                throw new ArgumentException("Crad Error");
            }
            databuf.WriteInt(iMaxSize);
            for (int i = mIndex; i < ListPassword.Count; i++)
            {
                iIndex = i;
                iSize += 1;

                ListPassword[iIndex].GetBytes(databuf);
                if (iSize == iMaxSize)
                {
                    break;
                }
                //card.WriteCardData(databuf);
            }
            if (iSize != iMaxSize)
            {
                databuf.SetInt(0, iSize);
            }
            mIndex = iIndex + 1;
            return databuf;
        }

        public override int GetDataLen()
        {
            return 4 + (BatchCount * 5);
        }
        */
        public override void SetBytes(IByteBuffer databuf)
        {
            throw new NotImplementedException();
        }

        public override int GetDataLen()
        {
            throw new NotImplementedException();
        }

        public override IByteBuffer GetBytes(IByteBuffer databuf)
        {
            throw new NotImplementedException();
        }
    }
}
