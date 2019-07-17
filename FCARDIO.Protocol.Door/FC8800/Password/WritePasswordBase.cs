﻿using DotNetty.Buffers;
using FCARDIO.Core.Command;
using System;
using System.Linq;
using FCARDIO.Protocol.OnlineAccess;
using System.Collections.Generic;

namespace FCARDIO.Protocol.Door.FC8800.Password
{
    /// <summary>
    /// 将密码列表写入到控制器
    /// </summary>
    public abstract class WritePasswordBase<T,P> : FC8800Command_WriteParameter where T : PasswordDetail,new () where P : Password_Parameter_Base<T>
    {
        /// <summary>
        /// 1个写入参数长度
        /// </summary>
        protected int mParDataLen;

        /// <summary>
        /// 1个删除参数长度
        /// </summary>
        protected int mDeleteDataLen;
        /// <summary>
        /// 参数
        /// </summary>
        Password_Parameter_Base<T> mPar;

        /// <summary>
        /// 每次上传数量
        /// </summary>
        protected const int mBatchCount = 20;

        /// <summary>
        /// 已上传数量
        /// </summary>
        protected int mIndex;

        /// <summary>
        /// 默认的缓冲区大小
        /// </summary>
        protected int MaxBufSize = 350;

        /// <summary>
        /// 需要写入密码数
        /// </summary>
        protected int maxCount = 0;

        /// <summary>
        /// 保存写入失败的数据缓冲区
        /// </summary>
        protected Queue<IByteBuffer> mBufs = null;

        /// <summary>
        /// 初始化命令结构 
        /// </summary>
        /// <param name="cd"></param>
        /// <param name="parameter">包含需要上传的密码列表参数</param>
        public WritePasswordBase(INCommandDetail cd, Password_Parameter_Base<T> parameter) : base(cd, parameter)
        {
            mPar = parameter;
            T model = new T();
            mParDataLen = model.GetDataLen();
            mDeleteDataLen = model.GetDeleteDataLen();
        }

        /// <summary>
        /// 将命令打包成一个Packet，准备发送
        /// </summary>
        protected override void CreatePacket0()
        {
            maxCount = mPar.PasswordList.Count;
            CreateCommandPacket0();
            _ProcessMax = maxCount;
        }

        /// <summary>
        /// 继承类具体实现
        /// </summary>
        protected abstract void CreateCommandPacket0();

        /// <summary>
        /// 检查参数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            P model = value as P;
            if (model == null)
            {
                return false;
            }
            return model.checkedParameter();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="databuf"></param>
        /// <returns></returns>
        public IByteBuffer WritePasswordToBuf(IByteBuffer databuf)
        {

            var lst = mPar.PasswordList;
            int iCount = lst.Count;//获取列表总长度
            iCount = iCount - mIndex;//计算未上传总数

            int iLen = iCount;
            if (iLen > mBatchCount)
            {
                iLen = mBatchCount;
            }

            databuf.Clear();
           
            databuf.WriteInt(iLen);
            for (int i = 0; i < iLen; i++)
            {
                WritePasswordBodyToBuf(databuf, lst[mIndex + i]);
            }

            mIndex += iLen;
            _ProcessStep += iLen;
            return databuf;
        }

        /// <summary>
        /// 将数据部分写入到缓冲区
        /// </summary>
        /// <param name="databuf"></param>
        /// <param name="password">要写入到缓冲区的密码</param>
        protected abstract void WritePasswordBodyToBuf(IByteBuffer databuf, T password);
        

        /// <summary>
        /// 处理返回值
        /// </summary>
        /// <param name="oPck"></param>
        protected override void CommandNext1(OnlineAccessPacket oPck)
        {
            if (IsWriteOver())
            {
                Create_Result();
                CommandCompleted();
            }
            else
            {
                //未发送完毕，继续发送
                var buf = GetCmdBuf();
                WritePasswordToBuf(buf);
                FCPacket.DataLen = (UInt32)buf.ReadableBytes;
                CommandReady();//设定命令当前状态为准备就绪，等待发送
            }
        }

        /// <summary>
        /// 重写父类对处理返回值的定义
        /// </summary>
        /// <param name="oPck"></param>
        protected override void CommandNext0(OnlineAccessPacket oPck)
        {
            if (CheckResponse_OK(oPck))
            {

                //继续发下一包
                CommandNext1(oPck);
            }
            else if (CheckResponse(oPck, 0x05, 0x04, 0xFF, oPck.DataLen))
            {//检查是否不是错误返回值

                //缓存错误返回值
                if (mBufs == null)
                {
                    mBufs = new Queue<IByteBuffer>();
                }
                oPck.CmdData.Retain();
                mBufs.Enqueue(oPck.CmdData);

                //继续发下一包
                CommandNext1(oPck);
            }
        }

        /// <summary>
        /// 创建命令成功返回值
        /// </summary>
        protected virtual void Create_Result()
        {
            //无法写入的卡数量
            int FailTotal = 0;

            //无法写入的卡列表
            List<string> PasswordList = new List<string>();


            if (mBufs != null)
            {
                foreach (var buf in mBufs)
                {
                    int iCount = buf.ReadInt();
                    FailTotal += iCount;

                    for (int i = 0; i < iCount; i++)
                    {
                        ReadPasswordByFailBuf(PasswordList, buf);
                    }

                    buf.Release();
                }
            }


            AddPassword_Result result = new AddPassword_Result(FailTotal, PasswordList);
            _Result = result;
        }

        /// <summary>
        /// 用来解析返回的错误密码数据
        /// </summary>
        T _Password;

        /// <summary>
        /// 从错误密码列表中读取一个错误密码，加入到passwordList中
        /// </summary>
        /// <param name="passwordList">错误密码列表</param>
        /// <param name="buf"></param>
        private void ReadPasswordByFailBuf(List<string> passwordList, IByteBuffer buf)
        {
            if (_Password == null) _Password = new T();
            _Password.SetBytes(buf);
            passwordList.Add(_Password.Password);
        }

        /// <summary>
        /// 检查是否已写完所有卡
        /// </summary>
        /// <returns></returns>
        protected bool IsWriteOver()
        {
            int iCount = mPar.PasswordList.Count;//获取列表总长度

            return (iCount - mIndex) == 0;
        }
    }
}
