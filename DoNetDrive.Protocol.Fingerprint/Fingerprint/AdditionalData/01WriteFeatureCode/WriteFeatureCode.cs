﻿using DoNetDrive.Core.Command;
using DoNetDrive.Protocol.Door.Door8800;
using DoNetDrive.Protocol.OnlineAccess;
using System;

namespace DoNetDrive.Protocol.Fingerprint.AdditionalData
{
    /// <summary>
    /// 写入头像照片\指纹\人脸特征码
    /// </summary>
    public class WriteFeatureCode : Door8800Command_WriteParameter
    {
        /// <summary>
        /// 写入特征码返回结果
        /// </summary>
        WriteFeatureCode_Result mResult;
        WriteFeatureCode_Parameter mPar;

        /// <summary>
        /// 写索引
        /// </summary>
        private int _WriteIndex = 0;

        /// <summary>
        /// 文件句柄
        /// </summary>
        private int _FileHandle = 0;

        /// <summary>
        /// 操作步骤
        /// </summary>
        private int _Step = 0;


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
            var dataBuf = GetNewCmdDataBuf(model.GetDataLen());
            Packet(0x0B, 0x01, 0x00, 6, model.GetBytes(dataBuf));

            mResult = new WriteFeatureCode_Result();
            _Result = mResult;
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
        /// 检查返回值
        /// </summary>
        /// <param name="oPck"></param>
        protected override void CommandNext0(OnlineAccessPacket oPck)
        {
            switch (_Step)
            {
                case 0:
                    //返回文件句柄
                    CheckOpenFileResult(oPck);
                    break;
                case 1:
                    CheckWriteFileResult(oPck);
                    break;
                case 2://上传完毕
                    if (CheckResponse(oPck, 0x0B, 3, 0, 1))
                    {
                        mResult.Success = oPck.CmdData.ReadByte();
                        CommandCompleted();
                    }
                    break;
                default:
                    break;
            }




        }
        /// <summary>
        /// 检查打开文件返回值
        /// </summary>
        private void CheckOpenFileResult(OnlineAccessPacket oPck)
        {
            if (CheckResponse(oPck, 4))
            {
                var buf = oPck.CmdData;
                _FileHandle = buf.ReadInt();
                if (_FileHandle == 0)
                {
                    mResult.Success = 0;
                    CommandCompleted();
                }
                else
                {
                    mResult.FileHandle = _FileHandle;
                    var data = mPar.Datas;
                    var iPackSize = 1024;
                    if (iPackSize > data.Length) iPackSize = data.Length;
                    _ProcessMax = data.Length;
                    _ProcessStep = 0;
                    int iBufSize = 7 + iPackSize;
                    var writeBuf = GetNewCmdDataBuf(iBufSize);
                    writeBuf.WriteInt(_FileHandle);
                    _WriteIndex = 0;
                    writeBuf.WriteMedium(_WriteIndex);
                    writeBuf.WriteBytes(data, 0, iPackSize);

                    Packet(0x0B, 2, 0, (uint)writeBuf.ReadableBytes, writeBuf);
                    _Step = 1;
                    CommandReady();

                }
            }
        }

        /// <summary>
        /// 检查写文件返回值
        /// </summary>
        private void CheckWriteFileResult(OnlineAccessPacket oPck)
        {
            if (CheckResponse(oPck, 0x0B, 2, 0))
            {
                var data = mPar.Datas;
                var iPackSize = 1024;

                _WriteIndex += iPackSize;
                _ProcessStep += iPackSize;

                var iFileLen = data.Length;
                var iDataLen = iFileLen - _WriteIndex;
                var buf = GetCmdBuf();
                if (iDataLen > iPackSize) iDataLen = iPackSize;
                if (iDataLen <= 0)
                {
                    _ProcessStep = _ProcessMax;
                    CommandDetail.Timeout = mPar.WaitVerifyTime;

                    var crc32 = DoNetTool.Common.Cryptography.CRC32_C.CalculateDigest(data, 0, (uint)data.Length);

                    buf.WriteInt((int)crc32);
                    DoorPacket.CmdIndex = 0x03;
                    DoorPacket.DataLen = 4;
                    _Step = 2;
                }
                else
                {
                    buf.WriteInt(_FileHandle);
                    buf.WriteMedium(_WriteIndex);
                    buf.WriteBytes(data, _WriteIndex, iDataLen);
                    DoorPacket.DataLen = buf.ReadableBytes;
                }
                CommandReady();
            }
            else if (CheckResponse(oPck, 0x0B, 2, 2))
            {

                //mResult.Success = 255;
                var dataBuf = GetNewCmdDataBuf(mPar.GetDataLen());
                Packet(0x0B, 0x01, 0x00, 6, mPar.GetBytes(dataBuf));
                CommandReady();

            }
        }

    }
}
