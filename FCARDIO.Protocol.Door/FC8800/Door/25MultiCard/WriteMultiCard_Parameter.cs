using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;

namespace FCARDIO.Protocol.Door.FC8800.Door.MultiCard
{
    /// <summary>
    /// 固定多卡组，单组结构
    /// </summary>
    public class MultiCard_GroupFix
    {
        /// <summary>
        /// 1--入门多卡组，2--出门多卡组，3--出入门通用组。
        /// </summary>
        public byte GroupType;

        /// <summary>
        /// 固定多卡组中的卡列表。
        /// </summary>
        public List<UInt64> CardList;


    }


    public class WriteMultiCard_Parameter : AbstractParameter
    {

        /// <summary>
        /// 门号
        /// 门端口在控制板中的索引号，取值：1-4
        /// </summary>
        public int DoorNum { get; set; }

        /// <summary>
        /// 刷卡模式 0--表示多卡时当遇到非组合内的刷卡时继续等待下一张正确的卡(默认参数)。          1--表示当遇到非组合内刷卡时直接退出。
        /// </summary>
        public byte Mode { get; set; }

        /// <summary>
        /// 防潜回检测 0--多卡时当开启防潜回功能时要进行防潜回检测。            1--多卡时当开启防潜回功能时不进行防潜回检测。
        /// </summary>
        public byte AntiPassback { get; set; }

        /// <summary>
        /// 开门验证方式0--禁用多卡验证。1--A组和B组组合验证（A组任意数量，B组任意数量）。          2--固定组合验证（原FC8800验证方式）          3--数量验证（此方式不需要特定组，只要是合法卡刷卡一次数量即可）
        /// 当验证模式为3时，【A组刷卡数量】字段规定的就是合法卡刷卡数量
        /// </summary>
        public byte VerifyType { get; set; }

        /// <summary>
        /// A组刷卡数量 取值范围：0-20
        /// </summary>
        public byte AGroupCount { get; set; }

        /// <summary>
        /// B组刷卡数量 取值范围：0-100
        /// </summary>
        public byte BGroupCount { get; set; }

        //public bool IsUseFixMode { get; set; }

        /// <summary>
        /// 多卡组A组
        /// </summary>
        public List<List<UInt64>> GroupA { get; set; }

        /// <summary>
        /// 多卡组B组
        /// </summary>
        public List<List<UInt64>> GroupB { get; set; }


        /// <summary>
        /// 多卡固定组
        /// </summary>
        public List<MultiCard_GroupFix> GroupFix { get; set; }

        public string mProtocolType { get;private set; }


    public WriteMultiCard_Parameter() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="door"></param>
        /// <param name="mode"></param>
        /// <param name="antiPassback"></param>
        /// <param name="verifytype"></param>
        /// <param name="agroupcount"></param>
        /// <param name="bgroupcount"></param>
        /// <param name="group_a"></param>
        /// <param name="group_b"></param>
        /// <param name="group_fix"></param>
        public WriteMultiCard_Parameter(byte door,
            byte mode, byte antiPassback,
            byte verifytype, byte agroupcount, byte bgroupcount,
            string protocolType,
            List<List<UInt64>> group_a, List<List<UInt64>> group_b,
            
            List<MultiCard_GroupFix> group_fix)
        {
            DoorNum = door;
            Mode = mode;
            AntiPassback = antiPassback;

            VerifyType = verifytype;
            AGroupCount = agroupcount;
            BGroupCount = bgroupcount;

            GroupA = group_a;
            GroupB = group_b;
            mProtocolType = protocolType;
            GroupFix = group_fix;

            checkedParameter();

        }

        public override bool checkedParameter()
        {
            if (DoorNum < 1 || DoorNum > 4)
                throw new ArgumentException("Door Error!");

            if (Mode < 0 || Mode > 1)
                throw new ArgumentException("Mode Error!");
            if (AntiPassback < 0 || AntiPassback > 1)
                throw new ArgumentException("AntiPassback Error!");

            if (VerifyType < 0 || VerifyType > 3)
                throw new ArgumentException("VerifyType Error!");

            if (AGroupCount < 0 || AGroupCount > 50)
                throw new ArgumentException("AGroupCount Error!");

            if (BGroupCount < 0 || BGroupCount > 100)
                throw new ArgumentException("BGroupCount Error!");

            if (!mProtocolType.Contains("MC58"))
            {
                switch (VerifyType)
                {
                    case 1:
                        if (GroupA == null || GroupB == null)
                            throw new ArgumentException("GroupA or GroupB Error!");

                        if (GroupA.Count < 5 || GroupB.Count < 20)
                            throw new ArgumentException("GroupA or GroupB Error!");

                        CheckGroup(GroupA);
                        CheckGroup(GroupB);

                        break;
                    case 2:
                        if (GroupFix == null)

                            if (GroupFix.Count < 10)
                                throw new ArgumentException("GroupA or GroupB Error!");

                        foreach (var fix in GroupFix)
                        {
                            CheckGroup(fix.CardList);
                        }

                        break;
                }
            }
           
            return true;
        }

        private void CheckGroup(List<List<UInt64>>  checkGroup)
        {
            foreach (var group in checkGroup)
            {
                CheckGroup(group);
            }
        }

        private void CheckGroup(List<UInt64> group)
        {
            if(group == null)
            {
                throw new ArgumentException("Card Group Error!");
            }

            foreach (var c in group)
            {
                if (c > UInt32.MaxValue)
                {
                    throw new ArgumentException("Card Error!");
                }

                if (c == 0)
                {
                    throw new ArgumentException("Card Error!");
                }
            }
        }

        public override void Dispose()
        {
            return;

        }
        
        /// <summary>
        /// 将 多卡开门检测模式参数 编码到字节流
        /// </summary>
        /// <param name="databuf"></param>
        /// <returns></returns>
        internal IByteBuffer CheckMode_GetBytes(IByteBuffer databuf)
        {
            if (databuf.WritableBytes != 3)
            {
                throw new ArgumentException("buf Error!");
            }
            databuf.WriteByte(DoorNum);
            databuf.WriteByte(Mode);
            databuf.WriteByte(AntiPassback);
            return databuf;
        }

        /// <summary>
        /// 将 多卡开门验证方式 编码到字节流
        /// </summary>
        /// <param name="databuf"></param>
        /// <returns></returns>
        internal IByteBuffer VerifyType_GetBytes(IByteBuffer databuf)
        {
            if (databuf.WritableBytes != 4)
            {
                throw new ArgumentException("buf Error!");
            }
            databuf.WriteByte(DoorNum);
            databuf.WriteByte(VerifyType);
            databuf.WriteByte(AGroupCount);
            databuf.WriteByte(BGroupCount);
            return databuf;
        }
        

        public override int GetDataLen()
        {
           
            return 0;

        }

        public override void SetBytes(IByteBuffer databuf)
        {
            return;
        }

        public override IByteBuffer GetBytes(IByteBuffer databuf)
        {
            return databuf;
        }
    }
}
