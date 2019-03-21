using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;

namespace FCARDIO.Protocol.Door.FC8800.SystemParameter.FunctionParameter
{
    /// <summary>
    /// 设置消防报警参数_参数
    /// </summary>
    public class WriteFireAlarmOption_Parameter : AbstractParameter
    {
        /// <summary>
        /// 消防报警参数（0 - 不启用、1 - 报警输出，并开所有门，只能软件解除、2 - 报警输出，不开所有门，只能软件解除、3 - 有信号报警并开门，无信号解除报警并关门、4 - 有报警信号时开一次门，就像按钮开门一样）
        /// </summary>
        public byte Option;

        public WriteFireAlarmOption_Parameter(byte _Option)
        {
            Option = _Option;
        }

        /// <summary>
        /// 检查参数
        /// </summary>
        /// <returns></returns>
        public override bool checkedParameter()
        {
            if (Option != 0 && Option != 1 && Option != 2 && Option != 3 && Option != 4)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public override void Dispose()
        {
            return;
        }

        /// <summary>
        /// 编码参数
        /// </summary>
        /// <param name="databuf"></param>
        /// <returns></returns>
        public override IByteBuffer GetBytes(IByteBuffer databuf)
        {
            return databuf.WriteByte(Option);
        }

        /// <summary>
        /// 获取数据长度
        /// </summary>
        /// <returns></returns>
        public override int GetDataLen()
        {
            return 0x01;
        }

        /// <summary>
        /// 解码参数
        /// </summary>
        /// <param name="databuf"></param>
        public override void SetBytes(IByteBuffer databuf)
        {
            Option = databuf.ReadByte();
        }
    }
}