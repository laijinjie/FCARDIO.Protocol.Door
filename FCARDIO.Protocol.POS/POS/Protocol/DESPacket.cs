using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;
using FCARD.Common.Cryptography;
using FCARDIO.Protocol.OnlineAccess;

namespace FCARDIO.Protocol.POS.Protocol
{
    /// <summary>
    /// 适用于 消费机和其他加密通讯的数据包
    /// </summary>
    public class DESPacket : Packet.BasePacket
    {
        #region 常量定义

        /// <summary>
        /// 信息代码的最小值
        /// </summary>
        private static UInt32 CodeMin = 268435456;//0x10000000
        /// <summary>
        /// 信息代码最大值
        /// </summary>
        private static UInt32 CodeMax = 3758096384;//0xB0000000

        /// <summary>
        /// 广播时使用的网络标志 0xFF00FF00
        /// </summary>
        public const UInt32 BroadcastCode = 4278255360;//0xFF00FF00

        /// <summary>
        /// 当网络表示为0xFF00FF00时的密码 0xFCAD020061006535
        /// </summary>
        public static byte[] BroadcastPassword = new byte[] { 0xFC, 0xAD, 0x02, 0x00, 0x61, 0x00, 0x65, 0x35 };

        /// <summary>
        /// 监控消息的网络标志
        /// </summary>
        public const UInt32 WatchMessageCode = 4294967295;//0xFFFFFFFF

        /// <summary>
        /// 当网络表示为0xFFFFFFFF时的密码 0x12 34 56 78 9A BC DE F0
        /// </summary>
        public static byte[] WatchMessagePassword = new byte[] { 0x12, 0x34, 0x56, 0x78, 0x9A, 0xBC, 0xDE, 0xF0 };
        #endregion

        /// <summary>
        /// 信息代码
        /// </summary>
        public UInt32 Code;

        /// <summary>
        /// 设备SN
        /// </summary>
        public Byte[] SN;

        /// <summary>
        /// 通讯密码
        /// </summary>
        public Byte[] Password;

        /// <summary>
        /// 包含命令数据详情的子数据包。
        /// </summary>
        public DESCommandPacket CommandPacket;


        #region 类的初始化


        /// <summary>
        /// 创建一个空的DES数据包
        /// </summary>
        public DESPacket()
        {

        }

        /// <summary>
        /// 创建一个包含目标设备详情的数据包
        /// </summary>
        /// <param name="detail">目标设备详情</param>
        public DESPacket(DESDriveCommandDetail detail) : this(detail, 0, 0, 0)
        {

        }

        /// <summary>
        /// 创建一个包含目标设备详情、信息代码，命令类型，命令索引，命令参数的数据包
        /// </summary>
        /// <param name="detail">目标设备详情</param>
        /// <param name="cc">信息代码</param>
        /// <param name="ct">命令类型</param>
        /// <param name="ci">命令索引</param>
        /// <param name="cp">命令参数</param>
        public DESPacket(DESDriveCommandDetail detail, UInt32 cc,
                       byte ct, byte ci, byte cp) : this(detail, cc, ct, ci, cp, 0, null)
        {

        }

        /// <summary>
        /// 创建一个包含目标设备详情、信息代码，命令类型，命令索引，命令参数，数据长度，数据内容 的数据包
        /// </summary>
        /// <param name="detail">目标设备详情</param>
        /// <param name="cc">信息代码</param>
        /// <param name="ct">命令类型</param>
        /// <param name="ci">命令索引</param>
        /// <param name="cp">命令参数</param>
        /// <param name="dl">数据长度</param>
        /// <param name="cd">数据内容</param>
        public DESPacket(DESDriveCommandDetail detail, UInt32 cc,
                       byte ct, byte ci, byte cp,
                       int dl, IByteBuffer cd) : this(detail.SNByte, detail.PasswordByte, cc, ct, ci, cp, dl, cd)
        {

        }

        /// <summary>
        /// 创建一个包含目标设备详情、 命令类型，命令索引，命令参数 的数据包
        /// </summary>
        /// <param name="detail">目标设备详情</param>
        /// <param name="ct">命令类型</param>
        /// <param name="ci">命令索引</param>
        /// <param name="cp">命令参数</param>
        public DESPacket(DESDriveCommandDetail detail,
                       byte ct, byte ci, byte cp) : this(detail, ct, ci, cp, 0, null)
        {

        }

        /// <summary>
        /// 创建一个包含目标设备详情、命令类型，命令索引，命令参数，数据长度，数据内容 的数据包
        /// </summary>
        /// <param name="detail">目标设备详情</param>
        /// <param name="cc">信息代码</param>
        /// <param name="ct">命令类型</param>
        /// <param name="ci">命令索引</param>
        /// <param name="cp">命令参数</param>
        /// <param name="dl">数据长度</param>
        /// <param name="cd">数据内容</param>
        public DESPacket(DESDriveCommandDetail detail,
                       byte ct, byte ci, byte cp,
                       int dl, IByteBuffer cd) : this(detail.SNByte, detail.PasswordByte, ct, ci, cp, dl, cd)
        {

        }

        /// <summary>
        /// 创建一个包含目标设备SN和秘钥、命令类型，命令索引，命令参数 的数据包
        /// </summary>
        /// <param name="dstSN">目标设备SN</param>
        /// <param name="ps">秘钥</param>
        /// <param name="ct">命令类型</param>
        /// <param name="ci">命令索引</param>
        /// <param name="cp"></param>
        public DESPacket(byte[] dstSN, byte[] ps,
                       byte ct, byte ci, byte cp) : this(dstSN, ps, ct, ci, cp, 0, null)
        {

        }

        /// <summary>
        /// 创建一个包含目标设备SN和秘钥、命令类型，命令索引，命令参数，数据长度，数据内容 的数据包
        /// </summary>
        /// <param name="dstSN">目标设备SN</param>
        /// <param name="ps">秘钥</param>
        /// <param name="ct">命令类型</param>
        /// <param name="ci">命令索引</param>
        /// <param name="cp">命令参数</param>
        /// <param name="dl">数据长度</param>
        /// <param name="cd">数据内容</param>
        public DESPacket(byte[] dstSN, byte[] ps,
                       byte ct, byte ci, byte cp,
                       int dl, IByteBuffer cd) : this(dstSN, ps, GetRandomCode(ref CodeMin, ref CodeMax), ct, ci, cp, dl, cd)
        {

        }

        /// <summary>
        ///  创建一个包含目标设备SN和秘钥、信息代码，命令类型，命令索引，命令参数 的数据包
        /// </summary>
        /// <param name="dstSN">目标设备SN</param>
        /// <param name="ps">秘钥</param>
        /// <param name="cc">信息代码</param>
        /// <param name="ct">命令类型</param>
        /// <param name="ci">命令索引</param>
        /// <param name="cp">命令参数</param>
        public DESPacket(byte[] dstSN, byte[] ps, UInt32 cc,
                    byte ct, byte ci, byte cp) : this(dstSN, ps, cc, ct, ci, cp, 0, null)
        {


        }

        /// <summary>
        /// 创建一个包含目标设备SN和秘钥、信息代码，命令类型，命令索引，命令参数，数据长度，数据内容 的数据包
        /// </summary>
        /// <param name="dstSN">目标设备SN</param>
        /// <param name="ps">秘钥</param>
        /// <param name="cc">信息代码</param>
        /// <param name="ct">命令类型</param>
        /// <param name="ci">命令索引</param>
        /// <param name="cp">命令参数</param>
        /// <param name="dl">数据长度</param>
        /// <param name="cd">数据内容</param>
        public DESPacket(byte[] dstSN, byte[] ps, UInt32 cc,
               byte ct, byte ci, byte cp,
               int dl, IByteBuffer cd)
        {
            if (dstSN == null)
            {
                VerifyError("SN");
                return;
            }
            if (dstSN.Length != 16)
            {
                VerifyError("SN");
                return;
            }

            if (ps == null)
            {
                VerifyError("Password");
                return;
            }
            if (ps.Length != 8)
            {
                VerifyError("Password");
                return;
            }



            SN = dstSN;
            Password = ps;
            Code = cc;

            CommandPacket = new DESCommandPacket(cc, ct, ci, cp, dl, cd);
        }

        #endregion

        #region 改变数据包结构

        /// <summary>
        /// 修改命令包内容
        /// </summary>
        /// <param name="ct">命令类型</param>
        /// <param name="ci">命令索引</param>
        /// <param name="cp">命令参数</param>
        public void SetPacket(byte ct, byte ci, byte cp)
        {
            SetPacket(ct, ci, cp, 0, null);
        }

        /// <summary>
        /// 修改命令包内容
        /// </summary>
        /// <param name="ct">命令类型</param>
        /// <param name="ci">命令索引</param>
        /// <param name="cp">命令参数</param>
        /// <param name="dl">数据长度</param>
        /// <param name="cd">数据内容</param>
        public void SetPacket(byte ct, byte ci, byte cp,
                       int dl, IByteBuffer cd)
        {
            CommandPacket.SetPacket(ct, ci, cp, dl, cd);
        }
        #endregion

        #region 网络标志的修改

        /// <summary>
        /// 设置当前的数据包为UDP广播数据包,UDP广播发送和接收数据时使用
        /// </summary>
        public void SetUDPBroadcastPacket()
        {
            Code = BroadcastCode;
            CommandPacket.Code = BroadcastCode;
            Password = BroadcastPassword;
        }


        /// <summary>
        /// 设置当前的数据包为正常播数据包（默认状态就是正常，除非经过修改，否则不必调用此函数）
        /// </summary>
        public void SetNormalPacket()
        {
            SetNormalPacket(GetRandomCode(ref CodeMin, ref CodeMax));
        }

        /// <summary>
        /// 设置当前的数据包为正常播数据包（默认状态就是正常，除非经过修改，否则不必调用此函数）
        /// </summary>
        public void SetNormalPacket(UInt32 iCode)
        {
            Code = iCode;
            CommandPacket.Code = iCode;

            if (iCode == BroadcastCode)
            {
                Password = BroadcastPassword;
            }

            if (iCode == WatchMessageCode)
            {
                Password = WatchMessagePassword;
            }

        }

        #endregion

        /// <summary>
        /// 获取数据包的打包后的ByteBuf，用于发送数据
        /// </summary>
        /// <param name="Allocator">用于分配ByteBuf的分配器</param>
        /// <returns>组装后的命令包</returns>
        public override IByteBuffer GetPacketData(IByteBufferAllocator Allocator)
        {
            //设备SN   信息代码        长度       数据     检验值
            //16Byte   4byte          2Byte    可变长度   1Byte
            if (CmdData != null)
            {
                CmdData.Release();
            }
            CommandPacket.Code = Code;

            CmdData = CommandPacket.GetPacketData(Allocator);
            DataLen = CmdData.ReadableBytes;

            byte[] key;
            DES_C mDes;

            //计算秘钥
            switch (Code)
            {
                case BroadcastCode://广播
                    key = BroadcastPassword;
                    break;
                case WatchMessageCode://监控消息
                    key = WatchMessagePassword;
                    break;
                default:
                    var keyBuf = Allocator.Buffer(8);
                    keyBuf.WriteInt((int)Code);
                    keyBuf.WriteBytes(Password, 0, 4);
                    key = new byte[8];
                    keyBuf.ReadBytes(key, 0, 8);
                    keyBuf.Release(); keyBuf = null;

                    mDes = new DES_C(Password);
                    mDes.EncryptData(key, 0);
                    key = mDes.GetCiphertextInBytes();
                    mDes = null;
                    break;
            }
            //数据加密
            mDes = new DES_C(key);
            byte[] dataBuf = new byte[DataLen];
            CmdData.ReadBytes(dataBuf);
            mDes.EncryptAnyLength(dataBuf,dataBuf.Length,0);
            dataBuf = mDes.GetCiphertextAnyLength();
            DataLen = dataBuf.Length;
            CmdData.Release();
            CmdData = Allocator.Buffer(DataLen);
            CmdData.WriteBytes(dataBuf);

            //创建数据包
            var buf = Allocator.Buffer(23 + DataLen);
            buf.WriteBytes(SN, 0, 16);// SN
            buf.WriteInt((int)Code);// 信息代码

            if (DataLen > 0)
            {
                try
                {
                    if (CmdData.ReadableBytes >= DataLen)
                    {
                        buf.WriteShort(DataLen);// 长度
                        buf.WriteBytes(CmdData, CmdData.ReaderIndex, DataLen);// 数据
                    }
                    else
                    {
                        buf.WriteShort(0);//长度
                    }
                }
                catch (Exception)
                {

                    buf.SetShort(20, 0);
                }
            }
            else
                buf.WriteShort(0);//长度
            //创建数据包并进行转义和校验和计算
            var buf2 = CreatePacket(Allocator, buf);
            buf.Release();

            return buf2;
        }

        /// <summary>
        /// 释放使用的资源
        /// </summary>
        protected override void Release()
        {
            Password = null;
            SN = null;
        }
    }
}
