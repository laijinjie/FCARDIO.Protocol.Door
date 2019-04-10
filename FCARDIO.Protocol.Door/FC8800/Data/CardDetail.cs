using DotNetty.Buffers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using FCARDIO.Core.Extension;
namespace FCARDIO.Protocol.Door.FC8800.Data
{
    /// <summary>
    /// 卡片权限详情
    /// </summary>
    public class CardDetail
    {
        public int compareTo(CardDetail o)
        {
            if (o.CardData == CardData)
            {
                return 0;
            }
            else if (CardData < o.CardData)
            {
                return -1;
            }
            else if (CardData > o.CardData)
            {
                return 1;
            }
            else
            {
                return 0;
            }
            //return Long.compare(CardData, o.CardData);
        }


        public bool equals(Object o)
        {
            if (o is CardDetail)
            {
                return compareTo((CardDetail)o) == 0;
            }
            else
            {
                return false;
            }

        }


        public int GetDataLen()
        {
            return 0x21;//33字节
        }
        public void SetBytes(IByteBuffer data)
        {
            data.ReadByte();
            CardData = data.ReadUnsignedInt();

            byte[] btData = new byte[4];
            data.ReadBytes(btData, 0, 4);
            Password = btData.ToHex();

            byte[] btTime = new byte[6];
            data.ReadBytes(btTime, 0, 5);
            Expiry = TimeUtil.BCDTimeToDate_yyMMddhhmm(btTime);

            data.ReadBytes(btData, 0, 4);
            for (int i = 0; i < 4; i++)
            {
                TimeGroup[i] = btData[i];
            }

            OpenTimes = data.ReadUnsignedShort();

            int bData = data.ReadByte();//特权
            Door = bData >> 4;
            bData = bData & 15;
            Privilege = bData & 7;
            HolidayUse = (bData & 8) == 8;
            CardStatus = data.ReadByte();

            data.ReadBytes(btData, 0, 4);
            for (int i = 0; i < 4; i++)
            {
                Holiday[i] = btData[i];
            }

            EnterStatus = data.ReadByte();
            data.ReadBytes(btTime, 0, 6);
            RecordTime = TimeUtil.BCDTimeToDate_yyMMddhhmmss(btTime);
            return;
        }


        public IByteBuffer GetBytes()
        {
            return null;
        }

        public void GetBytes(IByteBuffer data)
        {
            data.WriteByte(0);
            data.WriteInt((int)CardData);
            //Password = btData.ToHex();
            Password = StringUtil.FillHexString(Password, 8, "F", true);
            long pwd = long.Parse(Password);
            data.WriteInt((int)pwd);

            byte[] btTime = new byte[6];
            TimeUtil.DateToBCD_yyMMddhhmm(btTime, Expiry);
            data.WriteBytes(btTime, 0, 5);

            data.WriteBytes(TimeGroup, 0, 4);

            data.WriteShort(OpenTimes);

            int bData = (Door << 4) + Privilege;//特权
            if (HolidayUse)
            {
                bData = bData | 8;
            }
            data.WriteByte(bData);
            data.WriteByte(CardStatus);

            data.WriteBytes(Holiday, 0, 4);

            data.WriteByte(EnterStatus);

            TimeUtil.DateToBCD_yyMMddhhmmss(btTime, RecordTime);
            data.WriteBytes(btTime, 0, 6);
        }

        /**
         * 卡号，取值范围 0x1-0xFFFFFFFF
         */
        public long CardData;
        /**
         * 卡密码,无密码不填。密码是4-8位的数字。
         */
        public String Password;
        /**
         * 截止日期，最大2089年12月31日
         */
        public DateTime Expiry;
        /**
         * 开门时段<br/>
         * 1-4门的开门时段；时段取值范围：1-64<br/>
         * TimeGroup[0] -- 1门的时段<br/>
         * TimeGroup[1] -- 2门的时段<br/>
         * TimeGroup[2] -- 3门的时段<br/>
         * TimeGroup[3] -- 4门的时段<br/>
         */
        private byte[] TimeGroup;
        /**
         * 有效次数,取值范围：0-65535;<br.>
         * 0表示次数用光了。65535表示不受限制
         */
        public int OpenTimes;

        /**
         * 开门权限<br/>
         * 1-4门的开门权限；false--无权，true--有权开门<br/>
         * bit0 -- 1门的权限<br/>
         * bit1 -- 2门的权限<br/>
         * bit2 -- 3门的权限<br/>
         * bit3 -- 4门的权限<br/>
         */
        private int Door;

        /**
         * 特权<br/>
         * <ul>
         * <li>0 &emsp; 普通卡      </li>
         * <li>1 &emsp; 首卡        </li>
         * <li>2 &emsp; 常开        </li>
         * <li>3 &emsp; 巡更        </li>
         * <li>4 &emsp; 防盗设置卡  </li>
         * </ul>
         */
        private int Privilege;

        /**
         * 卡片状态<br/>
         * 0：正常状态；1：挂失；2：黑名单
         */
        public byte CardStatus;

        /**
         * 节假日权限
         */
        private byte[] Holiday;

        /**
         * 使用节假日限制功能,节假日禁止开门
         */
        public bool HolidayUse;

        /**
         * 出入标记；
         */
        public int EnterStatus;

        /**
         * 最近一次读卡的记录时间
         */
        public DateTime RecordTime;

        public CardDetail()
        {
            CardData = 0;
            Password = null;
            Expiry = DateTime.Now;
            TimeGroup = new byte[4];
            Door = 0;
            Privilege = 0;
            CardStatus = 0;
            Holiday = new byte[] { (byte)255, (byte)255, (byte)255, (byte)255 };
            RecordTime = DateTime.Now;
            EnterStatus = 0;
            HolidayUse = false;
        }

        public CardDetail(long data)
        {
            CardData = data;
        }

        /**
         * 获取指定门的开门时段号
         *
         * @param iDoor 取值范围1-4
         * @return 开门时段号
         */
        public int GetTimeGroup(int iDoor)
        {
            if (iDoor < 0 || iDoor > 4)
            {

                throw new ArgumentException("Door 1-4");
            }
            return TimeGroup[iDoor - 1];
        }

        /**
         * 设置指定门的开门时段号
         *
         * @param iDoor 门号，取值范围：1-4
         * @param iNum 开门时段号，取值范围：1-64
         */
        public void SetTimeGroup(int iDoor, int iNum)
        {
            if (iDoor < 0 || iDoor > 4)
            {

                throw new ArgumentException("Door 1-4");
            }

            if (iNum < 0 || iNum > 64)
            {

                throw new ArgumentException("Num 1-64");
            }
            TimeGroup[iDoor - 1] = (byte)iNum;
        }

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

        /**
         * 设置指定门是否有权限
         *
         * @param iIndex 门号，取值范围：1-4
         * @param bUse true 有权限，false 无权限。
         */
        public void SetDoor(int iDoor, bool bUse)
        {
            if (iDoor < 0 || iDoor > 4)
            {

                throw new ArgumentException("Door 1-4");
            }

            if (bUse == GetDoor(iDoor))
            {
                return;
            }

            iDoor -= 1;
            int iBitIndex = iDoor % 8;
            int iMaskValue = (int)Math.Pow(2, iBitIndex);
            if (bUse)
            {
                Door = Door | iMaskValue;
            }
            else
            {
                Door = Door ^ iMaskValue;
            }
        }

        /**
         * 普通开门卡
         */
        public bool IsNormal()
        {
            return Privilege == 0;
        }

        /**
         * 普通开门卡--五特权开门卡
         */
        public void SetNormal()
        {
            Privilege = 0;
        }

        /**
         * 首卡特权卡
         */
        public bool IsPrivilege()
        {
            return Privilege == 1;
        }

        /**
         * 首卡特权卡
         */
        public void SetPrivilege()
        {
            Privilege = 1;
        }

        /**
         * 常开特权卡
         */
        public bool IsTiming()
        {
            return Privilege == 2;
        }

        /**
         * 常开特权卡
         */
        public void SetTiming()
        {
            Privilege = 2;
        }

        /**
         * 巡更卡
         */
        public bool IsGuardTour()
        {
            return Privilege == 3;
        }

        /**
         * 巡更卡
         */
        public void SetGuardTour()
        {
            Privilege = 3;
        }

        /**
         * 防盗设置卡
         */
        public bool IsAlarmSetting()
        {
            return Privilege == 4;
        }

        /**
         * 防盗设置卡
         */
        public void SetAlarmSetting()
        {
            Privilege = 4;
        }

        /**
         * 获取指定序号的节假日开关状态
         *
         * @param iIndex 取值范围 1-30
         * @return 开关状态 开关true 表示启用，false 表示禁用
         */
        public bool GetHolidayValue(int iIndex)
        {
            if (iIndex <= 0 || iIndex > 30)
            {
                throw new ArgumentException("iIndex= 1 -- 32");
            }
            iIndex -= 1;
            //计算索引所在的字节位置
            int iByteIndex = iIndex / 8;
            int iBitIndex = iIndex % 8;
            int iByteValue = Holiday[iByteIndex] & 0x000000ff;
            int iMaskValue = (int)Math.Pow(2, iBitIndex);
            iByteValue = iByteValue & iMaskValue;
            if (iBitIndex > 0)
            {
                iByteValue = iByteValue >> (iBitIndex);
            }
            return iByteValue == 1;

        }

        /**
         * 设置指定序号的节假日开关状态
         *
         * @param iIndex 取值范围 1-30
         * @param bUse 开关状态 开关true 表示启用，false 表示禁用
         */
        public void SetHolidayValue(int iIndex, bool bUse)
        {
            if (iIndex <= 0 || iIndex > 30)
            {
                throw new ArgumentException("iIndex= 1 -- 32");
            }
            if (bUse == GetHolidayValue(iIndex))
            {
                return;
            }
            iIndex -= 1;
            int iByteIndex = iIndex / 8;
            int iBitIndex = iIndex % 8;
            int iByteValue = Holiday[iByteIndex] & 0x000000ff;
            int iMaskValue = (int)Math.Pow(2, iBitIndex);
            if (bUse)
            {
                iByteValue = iByteValue | iMaskValue;
            }
            else
            {
                iByteValue = iByteValue ^ iMaskValue;
            }

            Holiday[iByteIndex] = (byte)iByteValue;

        }

        /**
         * 使用折半查询方式搜索卡片集合
         *
         * @param list 已经过排序的卡片集合
         * @param SearchCard 需要搜索的卡片卡号
         * @return 在集合中的索引号
         */
         
        public static int SearchCardDetail(List<CardDetail> list, long SearchCard)
        {
            int max, min, mid;
            CardDetail search = new CardDetail();
            search.CardData = SearchCard;

            return SearchCardDetail(list, search);

        }

        /**
         * 使用折半查询方式搜索卡片集合
         *
         * @param list 已经过排序的卡片集合
         * @param SearchCard 需要搜索的卡片卡号
         * @return 在集合中的索引号
         */

        public static int SearchCardDetail(List<CardDetail> list, CardDetail search)
        {
            int max, min, mid;
            max = list.Count() - 1;
            min = 0;
            while (min <= max)
            {
                mid = (max + min) >> 1;
                CardDetail cd = list[mid];
                int num = cd.compareTo(search);
                if (num > 0)
                {
                    max = mid - 1;
                }
                else if (num < 0)
                {
                    min = mid + 1;
                }
                else
                {
                    return mid;
                }
            }
            return -1;

        }
    }
}
