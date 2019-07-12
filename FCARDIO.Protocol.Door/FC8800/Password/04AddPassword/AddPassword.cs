using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FCARDIO.Core.Command;
using FCARDIO.Core.Packet;
using FCARDIO.Protocol.OnlineAccess;

namespace FCARDIO.Protocol.Door.FC8800.Password
{
    /// <summary>
    /// 添加密码
    /// </summary>
    public class AddPassword : FC8800Command_WriteParameter 
    {
        protected int maxCount = 0;
        public AddPassword(INCommandDetail cd, AddPassword_Parameter par) : base(cd, par)
        {

        }

      

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            AddPassword_Parameter model = value as AddPassword_Parameter;
            if (model == null)
            {
                return false;
            }

            return model.checkedParameter();
        }

        /// <summary>
        /// 将命令打包成一个Packet，准备发送
        /// </summary>
        protected override void CreatePacket0()
        {
            AddPassword_Parameter model = _Parameter as AddPassword_Parameter;
            var acl = _Connector.GetByteBufAllocator();
            var buf = acl.Buffer(model.GetDataLen());
            maxCount = model.ListPassword.Count;
            Packet(0x5, 0x4, 0x00, Convert.ToUInt32(model.GetDataLen()), model.GetBytes(buf));
           
        }


        ///// <summary>
        ///// 生成命令的第一个数据包，后续的数据包应该在
        ///// </summary>
        //protected override void CreatePacket()
        //{
        //    AddPassword_Parameter model = _Parameter as AddPassword_Parameter;
        //    var acl = _Connector.GetByteBufAllocator();
        //    var buf = acl.Buffer(model.GetDataLen());
        //    maxCount = model.ListPassword.Count;
        //    Packet(0x5, 0x4, 0x00, Convert.ToUInt32(model.GetDataLen()), model.GetBytes(buf));
        //}


        

        /// <summary>
        /// 没有触发
        /// </summary>
        /// <param name="oPck"></param>
        protected override void CommandNext1(OnlineAccessPacket oPck)
        {
            AddPassword_Parameter model = _Parameter as AddPassword_Parameter;
            if (model.mIndex < maxCount)
            {
                var acl = _Connector.GetByteBufAllocator();
                var buf = acl.Buffer(model.GetDataLen());
                Packet(0x5, 0x4, 0x00, Convert.ToUInt32(model.GetDataLen()), model.GetBytes(buf));
                CommandReady();
            }
            else
            {
                CommandCompleted();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oPck"></param>
        protected override void CommandNext0(OnlineAccessPacket oPck)
        {
            if (CheckResponse_OK(oPck))
            {

                //继续发下一包
                CommandNext1(oPck);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        protected override void CommandReSend()
        {
            return;
            
        }

    }
}
