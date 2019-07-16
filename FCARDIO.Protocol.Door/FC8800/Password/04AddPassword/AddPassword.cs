using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;
using FCARDIO.Core.Command;
using FCARDIO.Core.Packet;
using FCARDIO.Protocol.OnlineAccess;

namespace FCARDIO.Protocol.Door.FC8800.Password
{
    /// <summary>
    /// 添加密码
    /// </summary>
    public class AddPassword<T> : FC8800Command_WriteParameter where T : PasswordDetail,new ()
    {
        /// <summary>
        /// 1个参数长度
        /// </summary>
        protected int mParDataLen;
        /// <summary>
        /// 参数
        /// </summary>
        AddPassword_Parameter<T> mPar;

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
        public AddPassword(INCommandDetail cd, AddPassword_Parameter<T> par) : base(cd, par)
        {
            mPar = par;
            T model = new T();
            mParDataLen = model.GetDataLen();
        }

        /// <summary>
        /// 检查参数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            AddPassword_Parameter<T> model = value as AddPassword_Parameter<T>;
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
            maxCount = mPar.ListPassword.Count;
            Packet(0x5, 0x4, 0x00, Convert.ToUInt32(GetDataLen()), GetBytes(GetNewCmdDataBuf(GetDataLen())));
            _ProcessMax = maxCount;
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

                mPar.ListPassword[iIndex].GetBytes(databuf);
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
        /// 接收到响应，开始处理下一步命令
        /// </summary>
        /// <param name="oPck"></param>
        protected override void CommandNext0(OnlineAccessPacket oPck)
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
        /// 获取长度
        /// </summary>
        /// <returns></returns>
        protected virtual int GetDataLen()
        {
            
            return 4 + (BatchCount * mParDataLen);
        }
    }
}
