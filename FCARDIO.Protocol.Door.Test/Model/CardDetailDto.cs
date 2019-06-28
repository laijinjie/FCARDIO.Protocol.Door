using FCARDIO.Protocol.Door.FC8800.Data;
using FCARDIO.Protocol.Door.FC8800.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.Test.Model
{
    public class CardDetailDto
    {
        public void SetDoors(CardDetail card)
        {
            door1 = card.GetDoor(1) ? "有权限" : "无权限";
            door2 = card.GetDoor(2) ? "有权限" : "无权限";
            door3 = card.GetDoor(3) ? "有权限" : "无权限";
            door4 = card.GetDoor(4) ? "有权限" : "无权限";
        }

        public void SetTimeGroup(byte[] list)
        {
            TimeGroup1 = list[0];
            TimeGroup2 = list[1];
            TimeGroup3 = list[2];
            TimeGroup4 = list[3];
        }

        public void SetPrivilege(int list)
        {
            Privilege0 = list == 0 ? "有效" : "无效";
            Privilege1 = list == 1 ? "有效" : "无效";
            Privilege2 = list == 2 ? "有效" : "无效";
            Privilege3 = list == 3 ? "有效" : "无效";
            Privilege4 = list == 4 ? "有效" : "无效";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        public void SetEnterStatus(short[] list)
        {
            EnterStatus1 = ConvertEnterStatus(list[0]);
            EnterStatus2 = ConvertEnterStatus(list[1]);
            EnterStatus3 = ConvertEnterStatus(list[2]);
            EnterStatus4 = ConvertEnterStatus(list[3]);
        }

        /// <summary>
        /// 设置节假日
        /// </summary>
        /// <param name="holiday"></param>
        public void SetHoliday(byte[] holiday)
        {
            for (int i = 0; i < holiday.Length; i++)
            {
                //Holiday += StringUtility.TenToBinary(holiday[i]).ToString();
            }
        }

        private string ConvertEnterStatus(short b)
        {
            switch (b)
            {
                case 0:
                case 3:
                    return "出入有效";
                case 1:
                    return "入有效";
                case 2:
                    return "出有效";
                default:
                    return "";
            }
        }


        public int ID { get; set; }

        /**
        * 卡号，取值范围 0x1-0xFFFFFFFF
        */
        public UInt64 CardData10 { get; set; }
        public string CardData16 { get; set; }
        /**
         * 卡密码,无密码不填。密码是4-8位的数字。
         */
        public String Password { get; set; }

        /**
         * 截止日期，最大2089年12月31日
         */
        public DateTime Expiry { get; set; }
        /**
          * 开门时段<br/>
          * 1-4门的开门时段；时段取值范围：1-64<br/>
          * TimeGroup[0] -- 1门的时段<br/>
          * TimeGroup[1] -- 2门的时段<br/>
          * TimeGroup[2] -- 3门的时段<br/>
          * TimeGroup[3] -- 4门的时段<br/>
          */
        public byte TimeGroup1 { get; set; }
        public byte TimeGroup2 { get; set; }
        public byte TimeGroup3 { get; set; }
        public byte TimeGroup4 { get; set; }

        public string door1 { get; set; }
        public string door2 { get; set; }
        public string door3 { get; set; }
        public string door4 { get; set; }

        public string Privilege1 { get; set; }
        public string Privilege2 { get; set; }
        public string Privilege3 { get; set; }
        public string Privilege4 { get; set; }
        public string Privilege0 { get; set; }

        public string EnterStatus1 { get; set; }
        public string EnterStatus2 { get; set; }
        public string EnterStatus3 { get; set; }
        public string EnterStatus4 { get; set; }

        /**
         * 有效次数,取值范围：0-65535;<br.>
         * 0表示次数用光了。65535表示不受限制
         */
        public int OpenTimes { get; set; }

        public bool Selected { get; set; }
        /**
         * 卡片状态<br/>
         * 0：正常状态；1：挂失；2：黑名单
         */
        public string CardStatus { get; set; }

        /**
         * 使用节假日限制功能,节假日禁止开门
         */
        public string Holiday { get; set; }

        /**
         * 节假日权限
         */
        public string HolidayUse { get; set; }


        /**
         * 最近一次读卡的记录时间
         */
        public DateTime RecordTime { get; set; }
    }
}
