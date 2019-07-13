﻿using FCARDIO.Core.Command;
using FCARDIO.Protocol.Door.FC8800.Door;
using FCARDIO.Protocol.FC8800;
using FCARDIO.Protocol.OnlineAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800
{
    /// <summary>
    /// 针对命令中的读参数命令进行抽象封装
    /// </summary>
    public abstract class FC8800Command_Read_DoorParameter : FC8800Command_ReadParameter
    {

        /// <summary>
        /// 初始化命令
        /// </summary>
        /// <param name="cd">包含命令所需的远程主机详情 （IP、端口、SN、密码、重发次数等）</param>
        /// <param name="par">包含门号的参数</param>
        public FC8800Command_Read_DoorParameter(INCommandDetail cd, DoorPort_Parameter par) : base(cd, par)
        {

        }

        /// <summary>
        /// 进行命令参数的检查<br/>
        /// 只有在有参数时才需要实现
        /// </summary>
        /// <param name="value">命令包含的参数</param>
        /// <returns>true 表示检查通过，false 表示检查不通过</returns>
        protected override bool CheckCommandParameter(INCommandParameter value)
        {
            DoorPort_Parameter model = value as DoorPort_Parameter;
            if (model == null) return false;
            return model.checkedParameter();
        }

    }
}
