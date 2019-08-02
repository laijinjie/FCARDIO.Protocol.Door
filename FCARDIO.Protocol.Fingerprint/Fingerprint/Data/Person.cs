using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Fingerprint.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class Person : IComparable<Person>
    {
        /// <summary>
        /// 用户号
        /// </summary>
        public uint UserCode;
        /// <summary>
        /// 卡号，取值范围 0x1-0xFFFFFFFF
        /// </summary>
        public UInt64 CardData;
        /// <summary>
        /// 卡密码,无密码不填。密码是4-8位的数字。
        /// </summary>
        public string Password;

        /// <summary>
        /// 截止日期，最大2089年12月31日
        /// </summary>
        public DateTime Expiry;

        /// <summary>
        /// 开门时段 取值范围：1-64；
        /// </summary>
        public int TimeGroup;


        /// <summary>
        /// 有效次数,取值范围：0-65535;<para/>
        /// 0表示次数用光了。65535表示不受限制
        /// </summary>
        public int OpenTimes;

        /// <summary>
        /// 用户身份
        /// 0 -- 普通用户
        /// 1 -- 管理员  
        /// </summary>
        public int Identity;

        /// <summary>
        ///卡片状态
        ///0 -- 普通卡
        ///1 -- 常开
        /// </summary>
        public int CardType;

        /// <summary>
        ///卡片状态
        ///0：正常状态；1：挂失；2：黑名单；3：已删除
        /// </summary>
        public int CardStatus;

        /// <summary>
        ///出入标记
        ///3、0  出入有效
        ///1  入有效
        ///2  出有效
        /// </summary>
        public int EnterStatus;

        /// <summary>
        /// 人员姓名
        /// </summary>
        public string PName;

        /// <summary>
        /// 人员编号
        /// </summary>
        public string PCode;

        /// <summary>
        /// 人员部门
        /// </summary>
        public string Dept;

        /// <summary>
        /// 人员职务
        /// </summary>
        public string Post;

        /// <summary>
        ///最近验证时间
        /// </summary>
        public DateTime RecordTime;

        /// <summary>
        /// 是否有人脸特征码
        /// </summary>
        public bool IsFaceFeatureCode;

        /// <summary>
        ///节假日权限
        /// </summary>
        public byte[] Holiday;

        /// <summary>
        /// 是否有指纹特征码
        /// 每个位表示一个指纹，一个人有10个指纹
        /// Bit0--指纹1，0-没有；1--有
        /// ...........
        /// bit9--指纹10
        /// </summary>
        public ushort IsFingerprintFeatureCode;

        public int CompareTo(Person other)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取一个人员详情实例，序列化到buf中的字节占比
        /// </summary>
        /// <returns></returns>
        public int GetDataLen()
        {
            return 0xA1;//161字节
        }
    }
}
