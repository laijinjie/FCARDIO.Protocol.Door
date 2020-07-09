using DoNetDrive.Core.Command;
using DoNetDrive.Protocol.Door.Door8800;
using DoNetDrive.Protocol.OnlineAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoNetDrive.Protocol.Fingerprint.AdditionalData
{
    /// <summary>
    /// 读取大型文件 可读取超过16M的文件
    /// </summary>
    public class ReadFile : Door8800Command_WriteParameter
    {
        /// <summary>
        /// 返回值
        /// </summary>
        ReadFile_Result mResult;

        /// <summary>
        /// 参数
        /// </summary>
        ReadFile_Parameter mPar;

        /// <summary>
        /// 读取到的文件块缓冲区
        /// </summary>
        private byte[] _FileDatas;

        /// <summary>
        /// 执行步骤
        /// </summary>
        private int _Step = 0;

        /// <summary>
        /// 初始化命令
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        /// <param name="par"></param>
        public ReadFile(INCommandDetail cd, ReadFile_Parameter par) : base(cd, par) { mPar = par; _Step = 0; }

        /// <summary>
        /// 将命令打包成一个Packet，准备发送
        /// </summary>
        protected override void CreatePacket0()
        {
            mResult = new ReadFile_Result();
            _Result = mResult;
            ReadFile_Parameter model = _Parameter as ReadFile_Parameter;
            Packet(0x0B, 0x15, 0x00, (uint)model.GetDataLen(), model.GetBytes(GetNewCmdDataBuf(model.GetDataLen())));
        }

        /// <summary>
        /// 检查命令参数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            ReadFile_Parameter model = value as ReadFile_Parameter;
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
            switch (_Step)
            {
                case 0://读取文件句柄
                    CheckOpenFileResule(oPck);
                    break;
                case 1://读文件块
                    CheckReadFileBlockResule(oPck);
                    break;
                case 2://关闭文件
                    CheckReadFile(oPck);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 检查打开文件的返回值
        /// </summary>
        /// <param name="oPck"></param>
        protected virtual void CheckOpenFileResule(OnlineAccessPacket oPck)
        {
            //读取文件句柄
            if (CheckResponse(oPck, 0x0B, 0x15, 1, 17))
            {
                var buf = oPck.CmdData;

                mResult.SetBytes(buf);
                if (mResult.UserCode != mPar.UserCode || mResult.Type != mPar.Type) return;

                if (mResult.FileHandle == 0 || mResult.FileSize == 0)
                {
                    CommandCompleted();
                    return;
                }
                else
                {
                    if (mResult.FileHandle > 0 && mResult.FileSize > 0)
                    {
                        _FileDatas = new byte[mResult.FileSize];
                        _ProcessMax = mResult.FileSize;
                    }
                    else
                    {
                        CommandCompleted();
                        return;
                    }
                    //开始读文件块
                    _Step = 1;

                    _ProcessStep = 0;
                    var iPackSize = 1024;
                    if (iPackSize > mResult.FileSize) iPackSize = mResult.FileSize;
                    _ProcessMax = mResult.FileSize;
                    var readBuf = GetNewCmdDataBuf(10);
                    readBuf.WriteInt(mResult.FileHandle);
                    readBuf.WriteInt(0);
                    readBuf.WriteShort(iPackSize);
                    fireCommandProcessEvent();
                    Packet(0x0B, 0x15, 2, (uint)readBuf.ReadableBytes, readBuf);

                    CommandReady();
                    return;
                }
            }
        }

        /// <summary>
        /// 接收读取到的文件块
        /// </summary>
        /// <param name="oPck"></param>
        protected virtual void CheckReadFileBlockResule(OnlineAccessPacket oPck)
        {
            //读取文件块返回值
            if (CheckResponse(oPck, 0x0B, 0x15, 2))
            {
                int FileHandle;
                int iDataIndex;
                int iSize = 0;


                var buf = oPck.CmdData;
                FileHandle = buf.ReadInt();
                iDataIndex = buf.ReadInt();
                iSize = buf.ReadUnsignedShort();
                uint crc = buf.ReadUnsignedInt();

                _ProcessStep = iDataIndex + iSize;
                fireCommandProcessEvent();

                buf.ReadBytes(_FileDatas, iDataIndex, iSize);

                var mycrc = DoNetTool.Common.Cryptography.CRC32_C.CalculateDigest(_FileDatas, (uint)iDataIndex, (uint)iSize);

                if (crc == mycrc)
                {
                    //校验通过，读取下一包
                    var iPackSize = 1024;

                    iDataIndex += iPackSize;

                    var iDataLen = mResult.FileSize - iDataIndex;
                    buf = GetCmdBuf();
                    if (iDataLen > iPackSize) iDataLen = iPackSize;
                    buf.WriteInt(mResult.FileHandle);

                    if (iDataLen <= 0)
                    {   //全部文件读取完毕
                        _ProcessStep = _ProcessMax;
                        DoorPacket.CmdPar = 3;
                        DoorPacket.DataLen = 4;
                        _Step = 2;
                    }
                    else
                    {
                        buf.WriteInt(iDataIndex);
                        buf.WriteShort(iDataLen);
                    }


                }
                else
                {
                    //校验错误，重新读取

                }

                CommandReady();
                return;
            }
        }

        /// <summary>
        /// 读取文件完毕，检验CRC32
        /// </summary>
        /// <param name="oPck"></param>
        protected virtual void CheckReadFile(OnlineAccessPacket oPck)
        {
            //读取文件块返回值
            if (CheckResponse(oPck, 0x0B, 0x15, 3))
            {

                var crc32 = DoNetTool.Common.Cryptography.CRC32_C.CalculateDigest(_FileDatas, 0, (uint)_FileDatas.Length);
                mResult.Result = (mResult.CRC == crc32);
                _ProcessStep = _ProcessMax;

                if (mResult.Result)
                {
                    mResult.Datas = _FileDatas;

                }
                _FileDatas = null;
                CommandCompleted();
            }
        }


        /// <summary>
        /// 释放资源
        /// </summary>
        protected override void Release1()
        {
            mResult = null;
            mPar = null;
            _FileDatas = null;

        }
    }
}
