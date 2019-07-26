﻿using FCARDIO.Core.Command;
using FCARDIO.Protocol.USB.CardReader.ICCard.Sector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.USB.CardReader.ICCard.Sector
{
    public class ReadAllSector_Result : INCommandResult
    {
        public List<ReadSector_Result> DataList;

        public ReadAllSector_Result(List<ReadSector_Result> list)
        {
            DataList = list;
        }

        public void Dispose()
        {

        }
    }
}
