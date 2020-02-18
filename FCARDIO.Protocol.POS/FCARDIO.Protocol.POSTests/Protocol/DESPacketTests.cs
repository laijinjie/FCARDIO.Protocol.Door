using Microsoft.VisualStudio.TestTools.UnitTesting;
using DoNetDrive.Protocol.POS.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;

namespace DoNetDrive.Protocol.POS.Protocol.Tests
{
    [TestClass()]
    public class DESPacketTests
    {
        public static System.Text.Encoding Ascii = System.Text.Encoding.ASCII;
        public static IByteBufferAllocator BufferAllocator = UnpooledByteBufferAllocator.Default;

        public static string sn = "FC-S500018070038";
        public static string Password = "12345678";
        public static uint CommandCode = 0xAF99BBD;
        public static string ContrastHex = "7E46432D533530303031383037303033380AF99BBD0010C222A7198ACB100D646B42DFEC150781637E";

        public static DESDriveCommandDetail GetCommandDetail()
        {
            DESDriveCommandDetail dtl = new DESDriveCommandDetail(
                new DoNetDrive.Core.Connector.TCPClient.TCPClientDetail("192.168.1.15", 8000),
                Ascii.GetBytes(sn), Ascii.GetBytes(Password));
            return dtl;
        }

        [TestMethod()]
        public void DESPacketTest()
        {
            try
            {
                DESPacket pck = new DESPacket();

            }
            catch (Exception)
            {
                Assert.Fail();

            }

        }

        [TestMethod()]
        public void DESPacketTest1()
        {
            var dtl = GetCommandDetail();
            DESPacket pck = new DESPacket(dtl, CommandCode, 1, 4, 0);
            var pckData = pck.GetPacketData(BufferAllocator);
            string ResultBuf = ByteBufferUtil.HexDump(pckData).ToUpper();
            pckData.Release();
            Assert.AreEqual<string>(ContrastHex, ResultBuf);

            var buf = BufferAllocator.Buffer(1);
            buf.WriteByte(0);
            pck.SetPacket(1, 0xA, 1, 1, buf);
            pck.SetNormalPacket(0x4A563DFE);
            pckData = pck.GetPacketData(BufferAllocator);
            ResultBuf = ByteBufferUtil.HexDump(pckData).ToUpper();
            pckData.Release();
            string sPacketHex2 = "7E46432D533530303031383037303033384A563DFE001067ACF9AFD25EAF2A071457E989592D3DBF7E";
            Assert.AreEqual<string>(sPacketHex2, ResultBuf);
        }
    }
}