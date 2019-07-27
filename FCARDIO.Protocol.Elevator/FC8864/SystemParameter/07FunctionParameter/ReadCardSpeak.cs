using System;

namespace FCARDIO.Protocol.Elevator.FC8864.SystemParameter.FunctionParameter
{
    /// <summary>
    /// 定时读卡播报语音消息参数_模型
    /// </summary>
    public class ReadCardSpeak
    {
        /// <summary>
        /// 是否启用（0 - 不启用、1 - 启用）
        /// </summary>
        public bool Use;

        /// <summary>
        /// 消息编号（1 - 交房租、2 - 交管理费）
        /// </summary>
        public int MsgIndex;

        /// <summary>
        /// 起始时段 年月日时 BCD码，例：0x11120115，表示11年12月1日15点 最大不得超过2099年
        /// </summary>
        public DateTime BeginDate;

        /// <summary>
        /// 结束时段  最大不得超过2099年
        /// </summary>
        public DateTime EndDate;
    }
}