using FCARDIO.Core.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.SystemParameter.SystemStatus
{
    /// <summary>
    /// 获取设备运行信息_结果
    /// </summary>
    public class ReadSystemStatus_Result : INCommandResult
    {
        /// <summary>
        /// 系统运行天数
        /// </summary>
        public int RunDay;

        /// <summary>
        /// 格式化次数
        /// </summary>
        public int FormatCount;

        /// <summary>
        /// 看门狗复位次数
        /// </summary>
        public int RestartCount;

        /// <summary>
        /// UPS供电状态（0 - 表示电源取电；1 - 表示UPS供电）
        /// </summary>
        public int UPS;

        /// <summary>
        /// 系统温度
        /// </summary>
        public float Temperature;

        /// <summary>
        /// 上电时间
        /// </summary>
        public DateTime StartTime;

        /// <summary>
        /// 电压
        /// </summary>
        public float Voltage;

        public ReadSystemStatus_Result(int _RunDay, int _FormatCount, int _RestartCount, int _UPS, float _Temperature, DateTime _StartTime, float _Voltage)
        {
            RunDay = _RunDay;
            FormatCount = _FormatCount;
            RestartCount = _RestartCount;
            UPS = _UPS;
            Temperature = _Temperature;
            StartTime = _StartTime;
            Voltage = _Voltage;
        }

        public void Dispose()
        {
            return;
        }
    }
}