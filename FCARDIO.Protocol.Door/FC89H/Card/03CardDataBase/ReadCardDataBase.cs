using FCARDIO.Core.Command;
using FCARDIO.Protocol.Door.FC8800.Card.CardDataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC89H.Card.CardDataBase
{
    /// <summary>
    /// FC89H 读所有卡
    /// </summary>
    public class ReadCardDataBase :FCARDIO.Protocol.Door.FC8800.Card.CardDataBase.ReadCardDataBase
    {
        /// <summary>
        /// 初始化命令结构 
        /// </summary>
        /// <param name="cd"></param>
        /// <param name="parameter"></param>
        public ReadCardDataBase(INCommandDetail cd, ReadCardDataBase_Parameter parameter) : base(cd, parameter) { }
    }
}
