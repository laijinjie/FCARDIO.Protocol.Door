using DoNetDrive.Core.Command;
using DotNetty.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoNetDrive.Protocol.Fingerprint.Person
{
    public class AddPeosonAndImage_Result : INCommandResult
    {
        /// <summary>
        /// 文件句柄
        /// </summary>
        public int FileHandle;

        /// <summary>
        /// 写入结果
        /// 1--校验成功
        //0--校验失败
        //2--特征码无法识别
        //3--人员照片不可识别
        //255-文件未准备就绪
        /// </summary>
        public byte Success { get; set; }

        /// <summary>
        /// 无法写入的人员数量
        /// </summary>
        public readonly int FailTotal;

        /// <summary>
        /// 无法写入的人员列表
        /// </summary>
        public List<uint> PersonList;

        public AddPeosonAndImage_Result()
        {

        }
        public AddPeosonAndImage_Result(List<uint> personList)
        {
            FailTotal = personList.Count;
            PersonList = personList;
        }

        public void Dispose()
        {

        }

        /// <summary>
        /// 读取ByteBuffer内容
        /// </summary>
        /// <param name="buf"></param>
        public void SetBytes(IByteBuffer buf)
        {
            FileHandle = buf.ReadInt();
        }
    }
}
