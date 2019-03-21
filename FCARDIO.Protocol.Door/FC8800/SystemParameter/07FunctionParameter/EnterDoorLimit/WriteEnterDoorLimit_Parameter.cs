using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;

namespace FCARDIO.Protocol.Door.FC8800.SystemParameter.FunctionParameter
{
    /// <summary>
    /// 设置门内人数限制_参数
    /// </summary>
    public class WriteEnterDoorLimit_Parameter : AbstractParameter
    {
        public DoorLimit Limit;

        public WriteEnterDoorLimit_Parameter(DoorLimit _Limit)
        {
            Limit = _Limit;
        }

        /// <summary>
        /// 检查参数
        /// </summary>
        /// <returns></returns>
        public override bool checkedParameter()
        {
            if (Limit == null)
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
            Limit = null;

            return;
        }

        /// <summary>
        /// 编码参数
        /// </summary>
        /// <param name="databuf"></param>
        /// <returns></returns>
        public override IByteBuffer GetBytes(IByteBuffer databuf)
        {
            databuf.WriteInt(Convert.ToInt32(Limit.GlobalLimit));

            for (int i = 0; i < 4; i++)
            {
                databuf.WriteInt(Convert.ToInt32(Limit.DoorLimitArray[i]));
            }

            for (int i = 0; i < 4; i++)
            {
                databuf.WriteInt(Convert.ToInt32(Limit.DoorEnterArray[i]));
            }

            return databuf;
        }

        /// <summary>
        /// 获取数据长度
        /// </summary>
        /// <returns></returns>
        public override int GetDataLen()
        {
            return 0x24;
        }

        /// <summary>
        /// 解码参数
        /// </summary>
        /// <param name="databuf"></param>
        public override void SetBytes(IByteBuffer databuf)
        {
            throw new NotImplementedException();
        }
    }
}