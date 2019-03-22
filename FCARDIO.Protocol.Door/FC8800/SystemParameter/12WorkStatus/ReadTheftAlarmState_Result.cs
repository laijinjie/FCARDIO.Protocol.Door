using FCARDIO.Core.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.SystemParameter.WorkStatus
{
    /// <summary>
    /// 获取防盗主机布防状态_结果
    /// </summary>
    public class ReadTheftAlarmState_Result : INCommandResult
    {
        /// <summary>
        /// 布防状态<br/>
        /// <ul>
        /// <li>1 &emsp; 延时布防</li>
        /// <li>2 &emsp; 已布防</li>
        /// <li>3 &emsp; 延时撤防</li>
        /// <li>4 &emsp; 未布防</li>
        /// <li>5 &emsp; 报警延时，准备启用报警</li>
        /// <li>6 &emsp; 防盗报警已启动</li>
        /// </ul>
        /// </summary>
        public byte TheftState;

        /// <summary>
        /// 防盗主机报警状态（0未报警，1已报警）
        /// </summary>
        public byte TheftAlarm;

        public ReadTheftAlarmState_Result(byte _TheftState, byte _TheftAlarm)
        {
            TheftState = _TheftState;
            TheftAlarm = _TheftAlarm;
        }

        public void Dispose()
        {
            return;
        }
    }
}