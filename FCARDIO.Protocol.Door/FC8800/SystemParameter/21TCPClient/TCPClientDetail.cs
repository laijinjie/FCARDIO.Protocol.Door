﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.SystemParameter.TCPClient
{
    /// <summary>
    /// TCP客户端信息_模型
    /// </summary>
    public class TCPClientDetail
    {
        /// <summary>
        /// 客户端数量
        /// </summary>
        public byte TCPClientNum { get; set; }

        /// <summary>
        /// IP
        /// </summary>
        public string[] IP { get; set; }

        /// <summary>
        /// 端口
        /// </summary>
        public ushort[] TCPPort { get; set; }

        /// <summary>
        /// 接入时间
        /// </summary>
        public DateTime[] ConnectTime { get; set; }
    }
}
