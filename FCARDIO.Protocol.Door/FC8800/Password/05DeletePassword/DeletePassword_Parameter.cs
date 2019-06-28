using DotNetty.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.Password
{
    public class DeletePassword_Parameter : AbstractParameter
    {
        private int writeIndex = 0;

        private const int batchCount = 50;
        public int mIndex
        {
            get; private set;
        }
        private int BatchCount
        {
            get
            {
                if (ListPassword.Count > batchCount)
                {
                    return batchCount;
                }
                return ListPassword.Count - mIndex;
            }

        }
        /// <summary>
        /// 
        /// </summary>
        public List<PasswordDetail> ListPassword { get; private set; }

        public DeletePassword_Parameter(List<PasswordDetail> list)
        {
            ListPassword = list;
           
        }
        public override bool checkedParameter()
        {
            if (ListPassword == null || ListPassword.Count == 0)
            {
                return false;
            }
            int iOut = 0;
            foreach (var item in ListPassword)
            {
                if (item.Password.Length > 8)
                {
                    return false;
                }
                if (!int.TryParse(item.Password, out iOut))
                {
                    return false;
                }
            }
            return true;
        }

        public override void Dispose()
        {
            ListPassword = null;
        }

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

        public override void SetBytes(IByteBuffer databuf)
        {
            throw new NotImplementedException();
        }
    }
}
