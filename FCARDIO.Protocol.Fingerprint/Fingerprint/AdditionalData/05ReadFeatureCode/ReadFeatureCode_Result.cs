using DotNetty.Buffers;
using FCARDIO.Core.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Fingerprint.AdditionalData.ReadFeatureCode
{
    /// <summary>
    /// 读取人员照片/记录照片/指纹 返回结果
    /// </summary>
    public class ReadFeatureCode_Result : INCommandResult
    {
        /// <summary>
        /// 用户号
        /// </summary>
        public int UserCode;

        /// <summary>
        /// 文件类型
        /// 1 - 人员头像
        /// 2 - 指纹
        /// 3 - 记录照片
        /// 4 - 红外人脸特征码
        /// 5 - 动态人脸特征码
        /// </summary>
        public int Type;

        /// <summary>
        /// 文件句柄
        /// </summary>
        public int FileHandle;

        /// <summary>
        /// 文件大小
        /// </summary>
        public int FileSize;

        /// <summary>
        /// 起始位置
        /// </summary>
        public int StartIndex;

        /// <summary>
        /// 数据
        /// </summary>
        public byte[] Datas;

        /// <summary>
        /// CRC32校验数据
        /// </summary>
        public uint CRC;

        /// <summary>
        /// 指示命令执行结果
        /// </summary>
        public bool Result;


        public void Dispose()
        {

        }

        /// <summary>
        /// 读取ByteBuffer内容
        /// </summary>
        /// <param name="buf"></param>
        public void SetBytes(IByteBuffer buf)
        {
            Type = buf.ReadByte();
            UserCode = buf.ReadInt();
            FileHandle = buf.ReadInt();
            FileSize = buf.ReadMedium();


        }


    }
}
