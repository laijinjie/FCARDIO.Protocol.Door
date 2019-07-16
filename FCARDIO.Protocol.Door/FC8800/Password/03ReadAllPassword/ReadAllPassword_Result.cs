﻿using DotNetty.Buffers;
using FCARDIO.Core.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.Password
{
    /// <summary>
    /// 读取所有密码 结果
    /// </summary>
    public class ReadAllPassword_Result : ReadAllPassword_Result_Base<PasswordDetail>
    {
        /// <summary>
        /// 初始化参数
        /// </summary>
        /// <param name="passwordList"></param>
        public ReadAllPassword_Result(List<PasswordDetail> passwordList) :base(passwordList)
        {

        }
    }
}
