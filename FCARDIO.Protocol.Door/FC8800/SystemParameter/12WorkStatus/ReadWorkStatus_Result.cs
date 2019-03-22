﻿using FCARDIO.Core.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCARDIO.Protocol.Door.FC8800.SystemParameter.WorkStatus
{
    /// <summary>
    /// 控制器各端口工作状态信息_结果
    /// </summary>
    public class ReadWorkStatus_Result : INCommandResult
    {
        /// <summary>
        /// 门继电器物理状态（0 - 表示COM和NC常闭、1 - 表示COM和NO常闭、2 - 表示继电器无法检测，继电器异常）
        /// </summary>
        public DoorPortDetail RelayState;

        /// <summary>
        /// 运行状态（常开还是常闭，0表示常闭，1常开）
        /// </summary>
        public DoorPortDetail DoorLongOpenState;

        /// <summary>
        /// 门磁开关（0表关，1表示开）
        /// </summary>
        public DoorPortDetail DoorState;

        /// <summary>
        /// 门报警状态（0 - 非法刷卡报警、1 - 门磁报警、2 - 胁迫报警、3 - 开门超时报警、4 - 黑名单报警）
        /// </summary>
        public DoorPortDetail DoorAlarmState;

        /// <summary>
        /// 设备报警状态（0 - 匪警报警、1 - 防盗报警、2 - 消防报警、3 - 烟雾报警、4 - 消防报警(命令通知)）
        /// </summary>
        public byte AlarmState;

        /// <summary>
        /// 继电器逻辑状态.<br/>
        /// <h2>继电器的逻辑开关状态</h2>
        /// <ul>
        /// <li>0--继电器关；</li>
        /// <li>1--继电器开；</li>
        /// <li>2--双稳态；  </li>
        /// </ul>
        /// <h2>门序号值说明</h2>
        /// 1-4是表示门的继电器，这个继电器状态需要根据门的继电器类型判断真实物理状况或者根据第一组状态值【门继电器物理状态】判断。<br/>
        /// 5-8是报警继电器，目前定义只有0或1两个状态。<br>
        /// 状态0表示：COM和NC导通<br>
        /// 状态1表示：COM和NO导通<br>
        /// <ul>
        /// <li>1-4 &emsp; 4个门的继电器</li>
        /// <li>5 &emsp; 消防继电器</li>
        /// <li>6 &emsp; 匪警继电器</li>
        /// <li>7 &emsp; 烟雾报警继电器</li>
        /// <li>8 &emsp; 防盗主机报警继电器</li>
        /// </ul>
        /// </summary>
        public DoorPortDetail LockState;

        /// <summary>
        /// 锁定状态.<br/>
        /// 4个门，0--未锁定，1--锁定
        /// </summary>
        public DoorPortDetail PortLockState;

        /// <summary>
        /// 监控状态.<br/>
        /// 0--未开启监控；1--开启监控
        /// </summary>
        public byte WatchState;

        /// <summary>
        /// 门内人数
        /// </summary>
        public DoorLimit EnterTotal;

        /// <summary>
        /// 防盗主机布防状态<br/>
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

        public ReadWorkStatus_Result(DoorPortDetail _RelayState, DoorPortDetail _DoorLongOpenState, DoorPortDetail _DoorState, DoorPortDetail _DoorAlarmState, byte _AlarmState, DoorPortDetail _LockState, DoorPortDetail _PortLockState, byte _WatchState, DoorLimit _EnterTotal, byte _TheftState)
        {
            RelayState = _RelayState;
            DoorLongOpenState = _DoorLongOpenState;
            DoorState = _DoorState;
            DoorAlarmState = _DoorAlarmState;
            AlarmState = _AlarmState;
            LockState = _LockState;
            PortLockState = _PortLockState;
            WatchState = _WatchState;
            EnterTotal = _EnterTotal;
            TheftState = _TheftState;
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            RelayState = null;
            DoorLongOpenState = null;
            DoorState = null;
            DoorAlarmState = null;
            LockState = null;
            PortLockState = null;
            EnterTotal = null;

            return;
        }
    }
}