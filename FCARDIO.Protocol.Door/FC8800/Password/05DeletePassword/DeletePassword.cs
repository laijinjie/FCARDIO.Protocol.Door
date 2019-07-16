using DotNetty.Buffers;
using FCARDIO.Core.Command;
using FCARDIO.Protocol.FC8800;
using FCARDIO.Protocol.OnlineAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.Password
{
    /// <summary>
    /// 删除密码表
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DeletePassword<T> : FC8800Command_WriteParameter where T : PasswordDetail, new()
    {
        /// <summary>
        /// 1个参数长度
        /// </summary>
        protected int mParDataLen;
        /// <summary>
        /// 参数
        /// </summary>
        protected DeletePassword_Parameter<T> mPar;

        /// <summary>
        /// 每次上传数量
        /// </summary>

        protected const int batchCount = 20;

        /// <summary>
        /// 已上传数量
        /// </summary>
        protected int mIndex;

        /// <summary>
        /// 计算每次上传数量
        /// </summary>
        protected int BatchCount
        {
            get
            {
                if (mPar.ListPassword.Count > batchCount)
                {
                    return batchCount;
                }
                return mPar.ListPassword.Count - mIndex;
            }

        }

        /// <summary>
        /// 需要写入密码数
        /// </summary>
        protected int maxCount = 0;

        /// <summary>
        /// 初始化参数
        /// </summary>
        /// <param name="cd"></param>
        /// <param name="par"></param>
        public DeletePassword(INCommandDetail cd, DeletePassword_Parameter<T> par) : base(cd, par)
        {
            mPar = par;
            T model = new T();
            mParDataLen = model.GetDeleteDataLen();
        }
        /// <summary>
        /// 检查参数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            DeletePassword_Parameter<T> model = value as DeletePassword_Parameter<T>;
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
        protected override void CommandNext1(OnlineAccessPacket oPck)
        {
            if (CheckResponse_OK(oPck))
            {
                if (mIndex < maxCount)
                {

                    var buf = GetBytes(GetCmdBuf());
                    FCPacket.DataLen = (uint)buf.ReadableBytes;

                    _ProcessStep += BatchCount;
                    CommandReady();
                }
                else
                {
                    CommandCompleted();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="databuf"></param>
        /// <returns></returns>
        public IByteBuffer GetBytes(IByteBuffer databuf)
        {
            int iMaxSize = BatchCount; //每个数据包最大50个卡
            int iSize = 0;
            int iIndex = 0;

            databuf.Clear();
            int iLen = GetDataLen();
            if (databuf.WritableBytes != iLen)
            {
                throw new ArgumentException("Crad Error");
            }
            databuf.WriteInt(iMaxSize);
            for (int i = mIndex; i < mPar.ListPassword.Count; i++)
            {
                iIndex = i;
                iSize += 1;

                mPar.ListPassword[iIndex].GetDeleteBytes(databuf);
                if (iSize == iMaxSize)
                {
                    break;
                }
            }
            if (iSize != iMaxSize)
            {
                databuf.SetInt(0, iSize);
            }
            mIndex = iIndex + 1;
            return databuf;
        }

        /// <summary>
        /// 将命令打包成一个Packet，准备发送
        /// </summary>
        protected override void CreatePacket0()
        {
            maxCount = mPar.ListPassword.Count;
            Packet(0x5, 0x5, 0x00, Convert.ToUInt32(GetDataLen()), GetBytes(GetNewCmdDataBuf(GetDataLen())));
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        protected override void Release1()
        {
            mPar.ListPassword = null;
        }

        /// <summary>
        /// 获取长度
        /// </summary>
        /// <returns></returns>
        protected virtual int GetDataLen()
        {

            return 4 + (BatchCount * mParDataLen);
        }

        /// <summary>
        /// 处理返回通知
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
    }
}
