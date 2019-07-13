using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.Data
{
    /// <summary>
    /// 门磁记录
    /// TransactionCode 事件代码含义表：
    /// 1 &emsp; 开门
    /// 2 &emsp; 关门 
    /// 3 &emsp; 进入门磁报警状态
    /// 4 &emsp; 退出门磁报警状态
    /// 5 &emsp; 门未关好
    /// </summary>
    public class DoorSensorTransaction : AbstractDoorTransaction
    {
        /// <summary>
        /// 
        /// </summary>
        public DoorSensorTransaction():base(3)
        {

        }
    }
}
