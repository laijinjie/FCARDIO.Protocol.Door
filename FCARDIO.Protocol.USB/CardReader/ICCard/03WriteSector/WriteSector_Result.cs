﻿using FCARDIO.Core.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.USB.CardReader.ICCard.Sector
{
    public class WriteSector_Result : WriteSector_Parameter, INCommandResult
    {
        public WriteSector_Result()
        {

        }
    }
}
