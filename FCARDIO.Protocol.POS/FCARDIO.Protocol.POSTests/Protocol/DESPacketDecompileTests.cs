using Microsoft.VisualStudio.TestTools.UnitTesting;
using FCARDIO.Protocol.POS.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;

namespace FCARDIO.Protocol.POS.Protocol.Tests
{
    [TestClass()]
    public class DESPacketDecompileTests
    {
        public static System.Text.Encoding Ascii = System.Text.Encoding.ASCII;

        [TestMethod()]
        public void DESPacketDecompileTest()
        {
            var dtl = DESPacketTests.GetCommandDetail();

            var buf = DESPacketTests.BufferAllocator.Buffer(16);
            buf.WriteBytes(dtl.SNByte);
            string sn = Ascii.GetString(dtl.SNByte);

            DESPacket pck = new DESPacket(dtl, 1, 5, 0, 16, buf);
            var pckData = pck.GetPacketData(DESPacketTests.BufferAllocator);
            byte[] bCodebuf = new byte[4];
            pckData.GetBytes(17, bCodebuf, 0, 4);
            pckData.SetBytes(1, bCodebuf);
            pckData.SetBytes(5, pck.SN, 0, 16);

            DESPacketDecompile dec = new DESPacketDecompile(DESPacketTests.BufferAllocator);
            List<Core.Packet.INPacket> pcks = new List<Core.Packet.INPacket>();
            bool bRet = dec.Decompile(pckData, pcks);
            pckData.Release();
            if (bRet)
            {
                DESPacket p = pcks.First() as DESPacket;
                if (p != null)
                {
                    Assert.AreEqual<uint>(p.Code, pck.Code);
                    Assert.AreEqual<string>(Ascii.GetString(p.SN), sn);

                    //开始解密
                    p.Password = pck.Password;
                    bRet = p.DecodeSubPacket(DESPacketTests.BufferAllocator);
                    Assert.IsTrue(bRet);

                    var subPck = p.CommandPacket;
                    Assert.AreEqual<uint>(subPck.Code, pck.Code);
                    Assert.AreEqual<int>(subPck.CmdType, 1);
                    Assert.AreEqual<int>(subPck.CmdIndex, 5);
                    Assert.AreEqual<int>(subPck.CmdPar, 0);
                    Assert.AreEqual<int>(subPck.DataLen, 16);

                    byte[] snBytes = new byte[16];
                    subPck.CmdData.GetBytes(0, snBytes, 0, 16);
                    Assert.AreEqual<string>(Ascii.GetString(snBytes), sn);
                }
                else
                {
                    Assert.Fail();
                }

            }
            else
            {
                Assert.Fail();
            }


        }
    }
}