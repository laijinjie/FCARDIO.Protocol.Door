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

        public WriteMultiCard_Parameter(byte door, byte mode, byte antiPassback, byte verifytype, byte agroupcount, byte bgroupcount)
        {
            DoorNum = door;
            Mode = mode;
            AntiPassback = antiPassback;
            VerifyType = verifytype;
            AGroupCount = agroupcount;
            BGroupCount = bgroupcount;
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

            return databuf;
        }

        public override int GetDataLen()
        {
            int[] array = new int[2] { 3, 4 };
            return array[Step];

        }


    }
}
