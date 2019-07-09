using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;

namespace FCARDIO.Protocol.Door.FC8800.Door.MultiCard
{
    public class WriteMultiCard_Parameter : AbstractParameter
    {

        /// <summary>
        /// 门号
        /// 门端口在控制板中的索引号，取值：1-4
        /// </summary>
        public int DoorNum { get; set; }

        public int Step { get; set; }

        /// <summary>
        /// 刷卡模式 (1)
        /// </summary>
        public byte Mode { get; set; }

        /// <summary>
        /// 防潜回检测 (1)
        /// </summary>
        public byte AntiPassback { get; set; }

        /// <summary>
        /// 开门验证方式 (1)
        /// </summary>
        public byte VerifyType { get; set; }

        /// <summary>
        /// A组刷卡数量 (1)
        /// </summary>
        public byte AGroupCount { get; set; }

        /// <summary>
        /// B组刷卡数量 (1)
        /// </summary>
        public byte BGroupCount { get; set; }

        public byte GroupType { get; set; }
        public byte GroupNum { get; set; }


        public List<string> AListCardData { get; set; }
        public List<string> BListCardData { get; set; }

        public Dictionary<int, int> ADict { get; set; }
        public Dictionary<int, int> BDict { get; set; }

        public int mIndex = 0;

        public WriteMultiCard_Parameter(byte door, byte mode, byte antiPassback, byte verifytype, byte agroupcount, byte bgroupcount, List<string> aList, List<string> bList)
        {
            DoorNum = door;
            Mode = mode;
            AntiPassback = antiPassback;
            VerifyType = verifytype;
            AGroupCount = agroupcount;
            BGroupCount = bgroupcount;
            Step = 0;
            AListCardData = aList;
            BListCardData = bList;

            for (int i = 0; i < 5; i++)
            {
                var tempList = aList.Skip(0 * i).Take(50 * i + 50).ToArray();
                ADict.Add((i + 1), tempList.Count(t => t != null));
            }
            for (int i = 0; i < 20; i++)
            {
                var tempList = bList.Skip(0 * i).Take(50 * i + 50).ToArray();
                BDict.Add((i + 1), tempList.Count(t => t != null));
            }
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
            return true;
        }

        public override void Dispose()
        {
            

        }

        public override IByteBuffer GetBytes(IByteBuffer databuf)
        {
            int count = 0;
            switch (Step)
            {
                case 0:
                    if (databuf.WritableBytes != 3)
                    {
                        throw new ArgumentException("door Error!");
                    }
                    databuf.WriteByte(DoorNum);
                    databuf.WriteByte(Mode);
                    databuf.WriteByte(AntiPassback);
                    Step++;
                    break;
                case 1:
                    if (databuf.WritableBytes != 4)
                    {
                        throw new ArgumentException("door Error!");
                    }
                    databuf.WriteByte(DoorNum);
                    databuf.WriteByte(VerifyType);
                    databuf.WriteByte(AGroupCount);
                    databuf.WriteByte(BGroupCount);
                    Step++;
                    break;
                case 2://多卡开门A组设置 
                    if (databuf.WritableBytes != 3)
                    {
                        throw new ArgumentException("door Error!");
                    }
                    databuf.WriteByte(GroupType);
                    databuf.WriteByte(GroupNum);
                    count = ADict[GroupNum] - mIndex > 20 ? 20 : ADict[GroupNum] - mIndex;
                    //count = AListCardData.Skip(mIndex).Take(count).Count(t => t != null);
                    databuf.WriteByte(count);
                    Step = 3;
                    break;
                case 3://设置A组中的卡号
                    count = ADict[GroupNum] - mIndex > 20 ? 20 : ADict[GroupNum] - mIndex;
                    databuf.WriteByte(mIndex + 1);
                    var templist = AListCardData.Skip(mIndex).Take(count).Where(t => t != null).ToList();
                    for (int i = 0; i < templist.Count; i++)
                    {
                        int iCard = int.Parse(templist[i]);
                        string card = FCARDIO.Protocol.Util.StringUtil.FillString(i.ToString("X"), 16, "0", false);

                        //string card = Convert.ToInt32(AListCardData[mIndex], 16).ToString().PadLeft(16,'0');
                        byte[] b = FCARDIO.Protocol.Util.StringUtil.HexToByte(card);
                        databuf.WriteBytes(b);
                        ADict[GroupNum]--;
                        mIndex++;
                    }
                    if (ADict[GroupNum] == 0)
                    {
                        GroupNum++;
                        Step = 2;
                    }
                    if (GroupNum == 5)
                    {
                        Step = 4;
                    }
                    //
                    break;
                case 4://多卡开门B组设置 
                    if (databuf.WritableBytes != 3)
                    {
                        throw new ArgumentException("door Error!");
                    }
                    databuf.WriteByte(GroupType);
                    databuf.WriteByte(GroupNum);
                    count = 100 * GroupNum - mIndex > 20 ? 20 : 100 * GroupNum - mIndex;
                    databuf.WriteByte(count);
                    Step = 3;
                    break;
                default:
                    break;
            }
            
            return databuf;
        }

        public void SetWriteIndex(int writeIndex)
        {
            mIndex = writeIndex;
        }

        public override int GetDataLen()
        {
            switch (Step)
            {
                case 0:
                case 1:
                    int[] array = new int[2] { 3, 4 };
                    int len = array[Step];
                    return len;
                case 2:
                case 4:
                    return 3;
                case 3:
                case 5:
                    int count = AListCardData.Count - mIndex > 20 ? 20 : AListCardData.Count - mIndex;
                    return 1 + 9 * count;
                default:
                    break;
            }
            return 3;

        }

        public override void SetBytes(IByteBuffer databuf)
        {

        }
    }
}
