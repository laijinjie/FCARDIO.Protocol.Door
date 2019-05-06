using DotNetty.Buffers;
using FCARDIO.Core.Command;
using FCARDIO.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.SystemParameter.SearchControltor
{
    public class SearchControltor_Result : AbstractData, INCommandResult
    {
        private TCPSetting.TCPDetail _TCP;

        public string SN { get; set; }

        public TCPSetting.TCPDetail TCP => _TCP;

        public SearchControltor_Result()
        {
            _TCP = new TCPSetting.TCPDetail();
        }

        public void Dispose()
        {
            _TCP = null;
            return;
        }

        public override IByteBuffer GetBytes(IByteBuffer databuf)
        {
            return databuf;
        }

        public override int GetDataLen()
        {
            return 0x89;
        }

        public override void SetBytes(IByteBuffer databuf)
        {
            _TCP.SetBytes(databuf);
        }
    }
}
