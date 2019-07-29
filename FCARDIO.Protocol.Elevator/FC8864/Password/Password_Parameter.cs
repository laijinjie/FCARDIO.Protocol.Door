﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetty.Buffers;

namespace FCARDIO.Protocol.Elevator.FC8864.Password
{
    /// <summary>
    /// 写入密码 参数
    /// </summary>
    public class Password_Parameter : FCARDIO.Protocol.Door.FC8800.Password.Password_Parameter_Base<PasswordDetail>
    {
      
        /// <summary>
        /// 初始化参数
        /// </summary>
        /// <param name="list">要写入的密码集合</param>
        public Password_Parameter(List<PasswordDetail> list):base (list){ }

        
        /// <summary>
        /// 检查每个密码
        /// </summary>
        /// <param name="password">密码信息</param>
        /// <returns></returns>
        protected override bool checkedParameterItem(PasswordDetail password)
        {
            return true;
        }

        
    }
}
