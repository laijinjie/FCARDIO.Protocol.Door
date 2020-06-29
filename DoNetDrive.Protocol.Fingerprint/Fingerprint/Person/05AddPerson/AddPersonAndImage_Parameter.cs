using DoNetDrive.Protocol.Door.Door8800;
using DotNetty.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoNetDrive.Protocol.Fingerprint
{
    public class AddPersonAndImage_Parameter : AbstractParameter
    {
        public Data.Person mPerson { get; private set; }

        /// <summary>
        /// 等待校验的时间，单位毫秒
        /// </summary>
        public int WaitVerifyTime;

        public int Type;

        public byte[] Datas { get; private set; }
        public AddPersonAndImage_Parameter(Data.Person person, byte[] datas)
        {
            mPerson = person;
            Datas = datas;
            WaitVerifyTime = 6000;
            Type = 1;
        }
        public override bool checkedParameter()
        {
            if (mPerson == null) return false;
            if (mPerson.UserCode == 0 || mPerson.UserCode > int.MaxValue) return false;
            //if (mPerson.CardData == 0 || mPerson.CardData > int.MaxValue) return false;
            if (mPerson.TimeGroup > 64 || mPerson.TimeGroup < 1) return false;
            if (mPerson.EnterStatus > 3 || mPerson.EnterStatus < 0) return false;
            if (mPerson.Expiry.Year > 2099 || mPerson.Expiry.Year < 2000) return false;

            if (mPerson.FingerprintFeatureCodeCout > 10) return false;

            if (mPerson.Holiday == null || mPerson.Holiday.Length > 4) return false;
            if (mPerson.Identity > 1 || mPerson.Identity < 0) return false;
            //if (string.IsNullOrEmpty(mPerson.PName)) return false;
            //if (string.IsNullOrEmpty(mPerson.PCode)) return false;
            //if (string.IsNullOrEmpty(mPerson.))
            if (mPerson.CardType > 1 || mPerson.CardType < 0) return false;
            if (mPerson.CardStatus > 3 || mPerson.CardStatus < 0) return false;

            if (Datas == null)
            {
                return false;
            }
            return true;
        }

        public override void Dispose()
        {

        }

        public override IByteBuffer GetBytes(IByteBuffer databuf)
        {
            mPerson.GetBytes(databuf);
            return databuf;
        }

        public IByteBuffer GetWriteImageBytes(IByteBuffer databuf)
        {
            databuf.WriteInt((int)mPerson.UserCode);
            databuf.WriteByte(1);
            databuf.WriteByte(1);
            return databuf;
        }

        public override int GetDataLen()
        {
            return mPerson.GetDataLen();
        }

        public override void SetBytes(IByteBuffer databuf)
        {
            mPerson.SetBytes(databuf);
        }
    }
}
