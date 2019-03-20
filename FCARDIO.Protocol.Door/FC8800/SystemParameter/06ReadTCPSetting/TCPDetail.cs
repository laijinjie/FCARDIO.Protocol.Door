using DotNetty.Buffers;
using FCARDIO.Core.Data;
using FCARDIO.Core.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.SystemParameter.ReadTCPSetting
{
    /// <summary>
    /// 获取控制器通讯密码_模型
    /// </summary>
    public class TCPDetail : AbstractData

    {
        /// <summary>
        /// MAC地址
        /// </summary>
        public string mMAC { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        public string mIP { get; set; }

        /// <summary>
        /// IP掩码
        /// </summary>
        public string mIPMask { get; set; }

        /// <summary>
        /// 网关
        /// </summary>
        public string mIPGateway { get; set; }

        /// <summary>
        /// DNS
        /// </summary>
        public string mDNS { get; set; }

        /// <summary>
        /// 备用DNS
        /// </summary>
        public string mDNSBackup { get; set; }

        /// <summary>
        /// 控制器网络协议类型：1--TCP  client （控制器就是 Client）;2--TCP Server（控制器就是 Server）
        /// </summary>
        public short mProtocolType { get; set; }

        /// <summary>
        /// 控制器使用的TCP端口
        /// </summary>
        public ushort mTCPPort { get; set; }

        /// <summary>
        /// 控制器使用的UDP端口
        /// </summary>
        public int mUDPPort { get; set; }

        /// <summary>
        /// 控制器作为客户端时，目标服务器的端口
        /// </summary>
        public int mServerPort { get; set; }

        /// <summary>
        /// 控制器作为客户端时，目标服务器的IP
        /// </summary>
        public string mServerIP { get; set; }

        /// <summary>
        /// 自动获得IP
        /// </summary>
        public bool mAutoIP { get; set; }

        /// <summary>
        /// 控制器作为客户端时，目标服务器的域名
        /// </summary>
        public string mServerAddr { get; set; }



        private void SaveMACToByteBuf(String mac, IByteBuffer buf)
        {

            if (!CheckMAC(mac))
            {
                for (int i = 0; i < 6; i++)
                {
                    buf.WriteByte(0);
                }
                return;
            }
            String[] macList = mac.Split('-');
            int iLen = macList.Length;
            for (int i = 0; i < iLen; i++)
            {
                buf.WriteByte(Convert.ToInt32(macList[i]));
            }

        }


        public static bool CheckMAC(String mac)
        {
            if (string.IsNullOrEmpty(mac))
            {
                return false;
            }
            String[] macList = mac.Split('-');
            int iLen = macList.Length;
            if (iLen != 6)
            {
                return false;
            }
            try
            {
                int iValue;
                for (int i = 0; i < iLen; i++)
                {
                    iValue = Convert.ToInt32(macList[i]);
                    if (iValue < 0 || iValue > 255)
                    {
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        private void Save0toByteBuf(IByteBuffer buf, int iCount)
        {
            for (int i = 0; i < iCount; i++)
            {
                buf.WriteByte(0);
            }
        }

        private void SaveIPToByteBuf(String IP, IByteBuffer buf)
        {
            if (!CheckIP(IP))
            {
                for (int i = 0; i < 4; i++)
                {
                    buf.WriteByte(0);
                }
                return;
            }
            String[] ipList = IP.Split('.');
            int iLen = ipList.Length;
            for (int i = 0; i < iLen; i++)
            {
                buf.WriteByte(Convert.ToInt32(ipList[i]));
            }

        }

        public static bool CheckIP(String ip)
        {
            if (string.IsNullOrEmpty(ip))
            {
                return false;
            }
            String[] ipList = ip.Split('.');
            int iLen = ipList.Length;
            if (iLen != 4)
            {
                return false;
            }
            try
            {
                int iValue;
                for (int i = 0; i < iLen; i++)
                {
                    iValue = Convert.ToInt32(ipList[i]);
                    if (iValue < 0 || iValue > 255)
                    {
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public override  IByteBuffer GetBytes(IByteBuffer databuf)
        {
            SaveMACToByteBuf(mMAC, databuf);
            SaveIPToByteBuf(mIP, databuf);
            SaveIPToByteBuf(mIPMask, databuf);
            SaveIPToByteBuf(mIPGateway, databuf);
            SaveIPToByteBuf(mDNS, databuf);
            SaveIPToByteBuf(mDNSBackup, databuf);
            databuf.WriteByte(mProtocolType);
            databuf.WriteUnsignedShort(mTCPPort);
            databuf.WriteUnsignedShort(Convert.ToUInt16(mUDPPort));
            databuf.WriteUnsignedShort(Convert.ToUInt16(mServerPort));
            SaveIPToByteBuf(mServerIP, databuf);
            databuf.WriteByte(mAutoIP ? 1 : 0);

            if (string.IsNullOrEmpty(mServerAddr))
            {
                Save0toByteBuf(databuf, 99);
            }
            else
            {
                byte[] tmp = mServerAddr.GetBytes();
                databuf.WriteBytes(tmp);
                int iCount = 99 - tmp.Length;
                if (iCount > 0)
                {
                    Save0toByteBuf(databuf, iCount);
                }
            }
            return databuf;
        }

        public override int GetDataLen()
        {
            return 0x89;
        }

        public override void SetBytes(IByteBuffer databuf)
        {
            throw new NotImplementedException();
        }
    }
}