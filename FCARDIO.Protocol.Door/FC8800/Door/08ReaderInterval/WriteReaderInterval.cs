using DotNetty.Buffers;
using FCARDIO.Core.Command;
using FCARDIO.Protocol.FC8800;
using FCARDIO.Protocol.OnlineAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.Door.ReaderInterval
{
    /// <summary>
    /// 设置重复读卡间隔参数
    /// </summary>
    public class WriteReaderInterval : FC8800Command_WriteParameter
    {
        /// <summary>
        /// 设置开锁时输出时长
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        /// <param name="par">包含开锁时输出时长参数</param>
        public WriteReaderInterval(INCommandDetail cd, WriteReaderInterval_Parameter par) : base(cd, par) { }

        /// <summary>
        /// 检查命令参数
        /// </summary>
        /// <returns></returns>
        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            WriteReaderInterval_Parameter model = value as WriteReaderInterval_Parameter;
            if (model == null) return false;
            return model.checkedParameter();
        }

        /// <summary>
        /// 将命令打包成一个Packet，准备发送
        /// </summary>
        protected override void CreatePacket0()
        {
            Packet(0x03, 0x09, 0x01, 0x03, GetCmdData());
        }

        /// <summary>
        /// 创建命令所需的命令数据<br/>
        /// 将命令打包到ByteBuffer中
        /// </summary>
        /// <returns>包含命令数据的ByteBuffer</returns>
        private IByteBuffer GetCmdData()
        {
            WriteReaderInterval_Parameter model = _Parameter as WriteReaderInterval_Parameter;
            var acl = _Connector.GetByteBufAllocator();
            var buf = acl.Buffer(model.GetDataLen());
            model.GetBytes(buf);
            return buf;
        }
    }
}
