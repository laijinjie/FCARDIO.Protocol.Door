using FCARDIO.Core.Command;
using FCARDIO.Protocol.Door.FC8800;
using FCARDIO.Protocol.OnlineAccess;
using System;

namespace FCARDIO.Protocol.Fingerprint.AdditionalData.ReadFeatureCode
{
    /// <summary>
    /// 读取人员照片/记录照片/指纹
    /// </summary>
    public class ReadFeatureCode : FC8800Command_WriteParameter
    {
        /// <summary>
        /// 
        /// </summary>
        ReadFeatureCode_Result mResult;
        /// <summary>
        /// 初始化命令
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        /// <param name="par"></param>
        public ReadFeatureCode(INCommandDetail cd, ReadFeatureCode_Parameter par) : base(cd, par) { }

        /// <summary>
        /// 将命令打包成一个Packet，准备发送
        /// </summary>
        protected override void CreatePacket0()
        {
            ReadFeatureCode_Parameter model = _Parameter as ReadFeatureCode_Parameter;
            Packet(0x0B, 0x05, 0x00, Convert.ToUInt32(model.GetDataLen()), model.GetBytes(GetNewCmdDataBuf(model.GetDataLen())));
        }

        /// <summary>
        /// 检查命令参数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            ReadFeatureCode_Parameter model = value as ReadFeatureCode_Parameter;
            if (model == null)
            {
                return false;
            }
            return model.checkedParameter();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="oPck"></param>
        protected override void CommandNext0(OnlineAccessPacket oPck)
        {
            if (mResult == null)
            {
                mResult = new ReadFeatureCode_Result();
                _Result = mResult;
            }
           
            if (CheckResponse(oPck, 0x0b, 5, 1, 12))
            {
                var buf = oPck.CmdData;

                mResult.SetBytes(buf);
                if (mResult.FileHandle == 0)
                {
                    CommandCompleted();
                }
                else
                {
                    CommandWaitResponse();
                }
            }
            if (CheckResponse(oPck, 0x0b, 5, 2))
            {
                var buf = oPck.CmdData;
                mResult.SetFileHandleBytes(buf);
                CommandWaitResponse();
            }
            if (CheckResponse(oPck, 0x0b, 5, 3, 4))
            {
                var buf = oPck.CmdData;
                mResult.SetCRCBytes(buf);
                CommandCompleted();
            }
        }
    }
}
