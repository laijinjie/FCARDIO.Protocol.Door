using DotNetty.Buffers;
using FCARDIO.Core.Command;
using FCARDIO.Protocol.FC8800;
using FCARDIO.Protocol.OnlineAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.Door.DoorWorkSetting
{
    /// <summary>
    /// 读取门工作方式
    /// </summary>
    public class ReadDoorWorkSetting : FC8800Command_ReadParameter
    {
        /// <summary>
        /// 读取门工作方式
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        /// <param name="par">包含门端口</param>
        public ReadDoorWorkSetting(INCommandDetail cd, DoorPort_Parameter par) : base(cd, par) { }

        /// <summary>
        /// 检查命令参数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            DoorPort_Parameter model = value as DoorPort_Parameter;
            if (model == null) return false;
            return model.checkedParameter();
        }

        /// <summary>
        /// 命令返回值的判断
        /// </summary>
        /// <param name="oPck">包含返回指令的Packet</param>
        protected override void CommandNext1(OnlineAccessPacket oPck)
        {
            if (CheckResponse(oPck, 0xE5))
            {
                var buf = oPck.CmdData;
                DoorWorkSetting_Result rst = new DoorWorkSetting_Result();
                _Result = rst;
                rst.SetBytes(buf);
                CommandCompleted();
            }
        }

        /// <summary>
        /// 创建命令所需的命令数据<br/>
        /// 将命令打包到ByteBuffer中
        /// </summary>
        /// <returns>包含命令数据的ByteBuffer</returns>
        protected IByteBuffer GetCmdData()
        {
            DoorPort_Parameter model = _Parameter as DoorPort_Parameter;
            var acl = _Connector.GetByteBufAllocator();
            var buf = acl.Buffer(model.GetDataLen());
            model.GetBytes(buf);
            return buf;
        }

        /// <summary>
        /// 将命令打包成一个Packet，准备发送
        /// </summary>
        protected override void CreatePacket0()
        {
            Packet(0x03, 0x06, 0x00, 0x01, GetCmdData());
        }
    }
}
