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
    public class AddPeosonAndImage : Door8800Command_WriteParameter
    {
        /// <summary>
        /// 写入特征码返回结果
        /// </summary>
        AddPeosonAndImage_Result mResult;
        //WriteFeatureCode_Parameter mPar;
        AddPersonAndImage_Parameter mPar;
        /// <summary>
        /// 写索引
        /// </summary>
        private int _WriteIndex = 0;

        /// <summary>
        /// 文件句柄
        /// </summary>
        private int _FileHandle = 0;

        /// <summary>
        /// 保存写入失败的数据缓冲区
        /// </summary>
        protected Queue<IByteBuffer> mBufs = null;

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

            AddPersonAndImage_Parameter model = _Parameter as AddPersonAndImage_Parameter;
            var dataBuf = GetNewCmdDataBuf(0xA1 + 1);

            dataBuf.WriteByte(1);
            model.mPerson.GetBytes(dataBuf);
            Packet(0x07, 0x04, 0x00, (uint)dataBuf.ReadableBytes, dataBuf);
            _Step = 0;
            mResult = new AddPeosonAndImage_Result();
            _Result = mResult;
        }

        /// <summary>
        /// 检查命令参数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            AddPersonAndImage_Parameter model = value as AddPersonAndImage_Parameter;
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
                    if (CheckResponse_OK(oPck))
                    {
                        WriteImage();
                    }
                    else if (CheckResponse(oPck, 0x07, 0x04, 0xFF))
                    {//检查是否不是错误返回值

                        //缓存错误返回值
                        if (mBufs == null)
                        {
                            mBufs = new Queue<IByteBuffer>();
                        }
                        oPck.CmdData.Retain();
                        mBufs.Enqueue(oPck.CmdData);

                        Create_Result();
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
        /// 创建命令成功返回值
        /// </summary>
        protected virtual void Create_Result()
        {
            //无法写入的人员数量
            int FailTotal = 0;

            //无法写入的人员列表
            List<uint> list = new List<uint>();

            if (mBufs != null)
            {
                foreach (var buf in mBufs)
                {
                    int iCount = buf.ReadInt();
                    FailTotal += iCount;

                    for (int i = 0; i < iCount; i++)
                    {
                        ReadPasswordByFailBuf(list, buf);
                    }

                    buf.Release();
                }

            }
            _Result = new AddPeosonAndImage_Result(list);
        }

        /// <summary>
        /// 从错误密码列表中读取一个错误密码，加入到passwordList中
        /// </summary>
        /// <param name="personList">错误人员列表</param>
        /// <param name="buf"></param>
        private void ReadPasswordByFailBuf(List<uint> personList, IByteBuffer buf)
        {
            personList.Add(buf.ReadUnsignedInt());
        }

        private void WriteImage()
        {
            AddPersonAndImage_Parameter model = _Parameter as AddPersonAndImage_Parameter;
            var dataBuf = GetNewCmdDataBuf(6);
            Packet(0x0B, 0x01, 0x00, 6, model.GetWriteImageBytes(dataBuf));
            CommandReady();
            _Step = 1;
            //DoorPacket.CmdIndex = 0x01;
            //DoorPacket.CmdPar = 0x00;
            //DoorPacket.DataLen = 6;
            //DoorPacket.CmdType = 0x0B;
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
                    _Step = 3;
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
