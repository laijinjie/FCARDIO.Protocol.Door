using FCARDIO.Core.Command;
using FCARDIO.Protocol.Door.FC8800;
using FCARDIO.Protocol.OnlineAccess;
using System;

namespace FCARDIO.Protocol.Fingerprint.AdditionalData.WriteFeatureCode
{
    /// <summary>
    /// 写入特征码
    /// </summary>
    public class WriteFeatureCode : FC8800Command_WriteParameter
    {
        /// <summary>
        /// 写入特征码返回结果
        /// </summary>
        WriteFeatureCode_Result mResult;
        WriteFeatureCode_Parameter mPar;
        /// <summary>
        /// 初始化命令
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        /// <param name="par"></param>
        public WriteFeatureCode(INCommandDetail cd, WriteFeatureCode_Parameter par) : base(cd, par) { mPar = par; }

        /// <summary>
        /// 将命令打包成一个Packet，准备发送
        /// </summary>
        protected override void CreatePacket0()
        {

            WriteFeatureCode_Parameter model = _Parameter as WriteFeatureCode_Parameter;
            var dataBuf = GetNewCmdDataBuf(model.GetWriteFileDataLen());
            Packet(0x0B, 0x01, 0x00, 6, model.GetBytes(dataBuf));
        }

        /// <summary>
        /// 检查命令参数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            WriteFeatureCode_Parameter model = value as WriteFeatureCode_Parameter;
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
                mResult = new WriteFeatureCode_Result();
                _Result = mResult;
            }
            //返回文件句柄
            if (CheckResponse(oPck,4))
            {
                var buf = oPck.CmdData;
                mResult.SetBytes(buf);
                if (mResult.FileHandle == 0)
                {
                    CommandCompleted();
                }
                else
                {
                    mPar.FileHandle = mResult.FileHandle;
                    var writeBuf = GetCmdBuf();
                    writeBuf = mPar.GetWriteFileBytes(writeBuf);
                    FCPacket.CmdIndex = 0x02;
                    FCPacket.DataLen = writeBuf.ReadableBytes; //mPar.GetWriteFileDataLen();
                    //Packet(0x0b, 0x02, 0x00, (uint)dataLen, mPar.GetWriteFileBytes(writeBuf));
                    CommandReady();
                }
            }
            //成功存储 ,写入文件完毕,进行CRC32校验
            if (CheckResponse(oPck, 0x0b, 2, 0))
            {
                var crc32 = FCARD.Common.Cryptography.CRC32_C.CalculateDigest(mPar.Datas,0, (uint)mPar.Datas.Length);
                var writeBuf = GetCmdBuf();
                writeBuf.WriteInt((int)crc32);
                FCPacket.CmdIndex = 0x03;
                FCPacket.DataLen = 4; 
                //Packet(0x0b, 0x03, 0x00, 4, mPar.GetWriteFileBytes(GetNewCmdDataBuf(4)));
                CommandReady();
            }//未启动准备状态
            if (CheckResponse(oPck, 0x0b, 2, 2))
            {
                mResult.Success = false;
                CommandCompleted();
            }//返回CRC32校验结果
            if (CheckResponse(oPck, 0x0b, 3, 0, 1))
            {
                mResult.Success = oPck.CmdData.ReadBoolean();
                CommandCompleted();
            }
        }
    }
}
