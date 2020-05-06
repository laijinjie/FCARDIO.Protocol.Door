﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using DoNetDrive.Core.Extension;

namespace DoNetDrive.Protocol.Fingerprint.Test.Model
{
    public class Person_UI : INotifyPropertyChanged
    {
        public Data.Person Person;
        public Person_UI(Data.Person person)
        {
            Person = person;
        }

        private static StringBuilder mStrBuf = new StringBuilder(1024);

        /// <summary>
        /// 选择
        /// </summary>
        private bool _Selected;
        /// <summary>
        /// 选择
        /// </summary>
        public bool Selected
        {
            get { return _Selected; }
            set
            {
                _Selected = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// 序号
        /// </summary>
        public string CardIndex { get; set; }

        public uint UserCode
        {
            get
            {
                return Person.UserCode;
            }
        }

        public string PName{get{return Person.PName; } }
        public string PCode { get{return Person.PCode; } }
        public string Job { get{return Person.Job; } }
        public string Dept { get{return Person.Dept; } }

        /// <summary>
        /// 指纹数
        /// </summary>
        public int FingerprintCount
        {
            get { return Person.FingerprintFeatureCodeCout; }
        }

        /// <summary>
        /// 是否有人脸
        /// </summary>
        public string IsFaceFeature
        {
            get { return Person.IsFaceFeatureCode ? "有" : "无"; }
        }

        /// <summary>
        /// 卡号
        /// </summary>
        public string CardData
        {
            get
            {
                mStrBuf.Clear();
                mStrBuf.Append(Person.CardData.ToString("d20")).Append("(0x").Append(Person.CardData.ToString("X16")).Append(")");
                return mStrBuf.ToString();
            }
        }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password
        {
            get
            {
                if (string.IsNullOrEmpty(Person.Password))
                {
                    return string.Empty;
                }
                return Person.Password.ToUpper().Replace("F", string.Empty);
            }
        }

        /// <summary>
        /// 有效期
        /// </summary>
        public string Expiry { get { return Person.Expiry.ToDateTimeStr(); } }

        /// <summary>
        /// 表示卡状态的列表
        /// </summary>
        public static string[] CardStatusList = new string[] { "正常", "挂失卡", "黑名单" };
        public static string[] CardTypeList = new string[] { "普通卡", "常开" };

        /// <summary>
        /// 卡片状态
        /// </summary>
        public string CardStatus
        {
            get
            {
                return Person.CardStatus < 3 ? CardStatusList[Person.CardStatus] : Person.CardStatus.ToString();
            }
        }

        /// <summary>
        /// 特权
        /// </summary>
        public string CardType
        {
            get
            {
                return CardTypeList[Person.CardType];
            }
        }

        /// <summary>
        /// 开门次数 (0)已失效
        /// </summary>
        public const string OpenTimes_Invalid = "(0)已失效";

        /// <summary>
        /// 开门次数 无限制(65535)
        /// </summary>
        public const string OpenTimes_Off = "无限制(65535)";

        /// <summary>
        /// 有效次数
        /// </summary>
        public string OpenTimes
        {
            get
            {
                return Person.OpenTimes == 0 ?
                OpenTimes_Invalid :
                Person.OpenTimes == 65535 ?
                OpenTimes_Off : Person.OpenTimes.ToString() + "次";
            }
        }

        /// <summary>
        /// 节假日
        /// </summary>
        public string Holiday
        {
            get
            {
                mStrBuf.Clear();
                for (int i = 1; i <= 32; i++)
                {
                    mStrBuf.Append(Person.GetHolidayValue(i) ? "1" : "0");
                }
                //if (Person.HolidayUse)
                //{

                //}
                //else
                //{
                //    mStrBuf.Append("节假日不受限制！");
                //}


                return mStrBuf.ToString();
            }
        }

        /// <summary>
        /// 开门时段
        /// </summary>
        public string TimeGroup
        {
            get
            {
                return Person.TimeGroup.ToString();
            }
        }

        /// <summary>
        /// 出入状态
        /// </summary>
        public string EnterStatus
        {
            get
            {
                return GetEnterStatus();
            }
        }

        /// <summary>
        /// 用户身份
        /// </summary>
        public string Identity
        {
            get
            {
                switch (Person.Identity)
                {
                    case 1:
                        return "管理员";
                    default:
                        return "普通用户";
                }
            }
        }

        /// <summary>
        /// 获取出入标志
        /// </summary>
        public string GetEnterStatus()
        {
            switch (Person.EnterStatus)
            {
                case 1:
                    return "入有效";
                case 2:
                    return "出有效";
                default:
                    return "出入有效";
            }
        }

        /// <summary>
        /// 最近读卡时间
        /// </summary>
        public string ReadCardDate { get { return Person.RecordTime.ToDateTimeStr(); } }

        /// <summary>
        ///  在属性值更改时发生。
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}