using DoNetDrive.Core.Command;
using DoNetDrive.Protocol.Door.Door8800;
using DoNetDrive.Protocol.OnlineAccess;
using DotNetty.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoNetDrive.Protocol.Fingerprint.Person
{
    /// <summary>
    /// 添加人员并上传人员识别信息
    /// </summary>
    public class AddPeosonAndImage : Door8800Command_WriteParameter
    {
        /// <summary>
        /// 添加人员的返回值
        /// </summary>
        AddPersonAndImage_Result mResult;

        /// <summary>
        /// 添加人员的参数
        /// </summary>
        AddPersonAndImage_Parameter mPar;

        /// <summary>
        /// 用户号
        /// </summary>
        private uint UserCode;

        /// <summary>
        /// 写索引
        /// </summary>
        private int _WriteIndex = 0;

        /// <summary>
        /// 文件句柄
        /// </summary>
        private int _FileHandle = 0;
        /// <summary>
        /// 文件索引号
        /// </summary>
        private int _FileIndex = 0;

        /// <summary>
        /// 操作步骤
        /// </summary>
        private int _Step = 0;

        public AddPeosonAndImage(INCommandDetail cd, AddPersonAndImage_Parameter par) : base(cd, par) { mPar = par; }

        /// <summary>
        /// 将命令打包成一个Packet，准备发送
        /// </summary>
        protected override void CreatePacket0()
        {
            mResult = new AddPersonAndImage_Result(mPar.PersonDetail, mPar.IdentificationDatas);

            UserCode = mPar.PersonDetail.UserCode;
            var dataBuf = GetNewCmdDataBuf(0xA2);

            dataBuf.WriteByte(1);
            mPar.PersonDetail.GetBytes(dataBuf);
            Packet(0x07, 0x04, 0x00, 0xA2, dataBuf);
            _Step = 0;
            _Result = mResult;
        }

        /// <summary>
        /// 检查命令参数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            return (value as AddPersonAndImage_Parameter).checkedParameter();
        }


        /// <summary>
        /// 检查返回值
        /// </summary>
        /// <param name="oPck"></param>
        protected override void CommandNext0(OnlineAccessPacket oPck)
        {
            switch (_Step)
            {
                case 0://添加人员
                    if (CheckResponse_OK(oPck))
                    {
                        mResult.UserUploadStatus = true;
                        WriteImage();
                    }
                    else if (CheckResponse(oPck, 0x07, 0x04, 0xFF))
                    {//检查是否不是错误返回值
                        mResult.UserUploadStatus = false;
                        CommandCompleted();
                    }
                    break;
                case 1:
                    //返回文件句柄
                    CheckOpenFileResult(oPck);
                    break;
                case 2:
                    CheckWriteFileResult(oPck);
                    break;
                case 3://上传完毕
                    CheckWriteOver(oPck);
                    break;
                default:
                    break;
            }
        }


        /// <summary>
        /// 写文件
        /// </summary>
        private void WriteImage()
        {
            if ((_FileIndex + 1) > mPar.IdentificationDatas.Count)
            {
                //文件写完了
                CommandCompleted();
                return;
            }
            var id = mPar.IdentificationDatas[_FileIndex];

            var dataBuf = GetNewCmdDataBuf(6);
            dataBuf.WriteInt((int)UserCode);
            dataBuf.WriteByte(id.DataType); //数据类型, 1、人员照片;2、指纹特征码 ; 3、红外人脸特征码;4、动态人脸特征码
            dataBuf.WriteByte(id.DataNum);

            Packet(0x0B, 0x01, 0x00, 6, dataBuf);
            CommandReady();
            _Step = 1;
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
                    WriteImage();//没有获取到文件句柄，则重新获取
                }
                else
                {
                    var id = mPar.IdentificationDatas[_FileIndex];
                    var data = id.DataBuf;
                    var iLen = id.DataBufLen;
                    var iPackSize = 1024;
                    if (iPackSize > iLen) iPackSize = iLen;
                    _ProcessMax = iLen;
                    _ProcessStep = 0;
                    fireCommandProcessEvent();

                    int iBufSize = 7 + iPackSize;
                    var writeBuf = GetNewCmdDataBuf(iBufSize);
                    writeBuf.WriteInt(_FileHandle);
                    _WriteIndex = 0;
                    writeBuf.WriteMedium(_WriteIndex);
                    writeBuf.WriteBytes(data, id.DataBufOffset, iPackSize);

                    Packet(0x0B, 2, 0, (uint)writeBuf.ReadableBytes, writeBuf);
                    _Step = 2;
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
                var id = mPar.IdentificationDatas[_FileIndex];

                var data = id.DataBuf;
                var iPackSize = 1024;

                _WriteIndex += iPackSize;
                _ProcessStep += iPackSize;

                var iFileLen = id.DataBufLen;
                var iDataLen = iFileLen - _WriteIndex;
                var buf = GetCmdBuf();
                if (iDataLen > iPackSize) iDataLen = iPackSize;
                if (iDataLen <= 0)//文件上传完毕
                {
                    _ProcessStep = _ProcessMax;


                    CommandDetail.Timeout = mPar.WaitVerifyTime;

                    var crc32 = DoNetTool.Common.Cryptography.CRC32_C.CalculateDigest(data, 0, (uint)data.Length);

                    buf.WriteInt((int)crc32);
                    DoorPacket.CmdIndex = 0x03;
                    DoorPacket.DataLen = 4;
                    _Step = 3;
                }
                else
                {
                    buf.WriteInt(_FileHandle);
                    buf.WriteMedium(_WriteIndex);
                    buf.WriteBytes(data, id.DataBufOffset + _WriteIndex, iDataLen);
                    DoorPacket.DataLen = buf.ReadableBytes;
                }
                fireCommandProcessEvent();
                CommandReady();
            }
            else if (CheckResponse(oPck, 0x0B, 2, 2))
            {

                //文件意外关闭
                WriteImage();

            }
        }

        /// <summary>
        /// 检查文件写入是否完毕
        /// </summary>
        /// <param name="iStatus"></param>
        private void CheckWriteOver(OnlineAccessPacket oPck)
        {
            if (CheckResponse(oPck, 0x0B, 3, 0, 1))
            {
                int iStatus = oPck.CmdData.ReadByte();
                mResult.IdDataUploadStatus[_FileIndex] = iStatus;

                if (iStatus == 4)
                {
                    if (!mPar.WaitRepeatMessage)
                    {
                        WriteNextImage();
                        return;
                    }
                }
                else
                {
                    WriteNextImage();
                    return;
                }
            }

            if (CheckResponse(oPck, 0x0B, 0x03, 1, 4))
            {
                uint code = oPck.CmdData.ReadUnsignedInt();
                mResult.IdDataRepeatUser[_FileIndex] = code;
                WriteNextImage();
            }
        }

        /// <summary>
        /// 开始写下一个文件
        /// </summary>
        private void WriteNextImage()
        {
            _FileIndex += 1;//开始写下一个
            if ((_FileIndex + 1) > mPar.IdentificationDatas.Count)
            {
                //文件写完了
                CommandCompleted();
                return;
            }
            WriteImage();
        }

    }
}
