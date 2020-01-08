using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FCARDIO.Protocol.Door.FC8800.SystemParameter.SN;
using FCARDIO.Core.Extension;
using FCARDIO.Protocol.Door.FC8800.SystemParameter.ConnectPassword;
using FCARDIO.Protocol.Door.FC8800.SystemParameter.TCPSetting;
using System.Text.RegularExpressions;
using FCARDIO.Protocol.Door.FC8800.SystemParameter.Deadline;
using FCARDIO.Protocol.Door.FC8800.SystemParameter.Version;
using FCARDIO.Protocol.Door.FC8800.SystemParameter.SystemStatus;
using FCARDIO.Protocol.Door.FC8800.SystemParameter.FunctionParameter;
using System.Collections;
using FCARDIO.Protocol.Door.FC8800.SystemParameter;
using FCARDIO.Protocol.Door.FC8800.SystemParameter.Watch;
using FCARDIO.Protocol.Door.FC8800.SystemParameter.FireAlarm;
using FCARDIO.Protocol.Door.FC8800.SystemParameter.SmogAlarm;
using FCARDIO.Protocol.Door.FC8800.SystemParameter.Alarm;
using FCARDIO.Protocol.Door.FC8800.SystemParameter.WorkStatus;
using FCARDIO.Protocol.Door.FC8800.SystemParameter.Controller;
using FCARDIO.Protocol.Door.FC8800.SystemParameter.SearchControltor;
using FCARDIO.Protocol.OnlineAccess;
using FCARDIO.Protocol.Door.FC8800.SystemParameter.CacheContent;
using FCARDIO.Protocol.Door.FC8800.SystemParameter.KeepAliveInterval;
using FCARDIO.Protocol.Door.FC8800.SystemParameter.TheftFortify;
using FCARDIO.Protocol.Door.FC8800.SystemParameter.BalcklistAlarmOption;
using FCARDIO.Protocol.Door.FC8800.SystemParameter.ExploreLockMode;
using FCARDIO.Protocol.Door.FC8800.SystemParameter.Check485Line;
using FCARDIO.Protocol.Door.FC8800.SystemParameter.TCPClient;
using FCARDIO.Protocol.Door.FC8800.SystemParameter.CardDeadlineTipDay;
using FCARDIO.Protocol.Door.FC8800.SystemParameter.ControlPanelTamperAlarm;
using FCARDIO.Protocol.Door.FC8800.SystemParameter.HTTPPageLandingSwitch;
using FCARDIO.Protocol.Door.FC8800.SystemParameter.LawfulCardReleaseAlarmSwitch;

namespace FCARDIO.Protocol.Door.Test
{

    public partial class frmSystem : frmNodeForm
    {
        #region 单例模式
        private static object lockobj = new object();
        private static frmSystem onlyObj;
        public static frmSystem GetForm(INMain main)
        {
            if (onlyObj == null)
            {
                lock (lockobj)
                {
                    if (onlyObj == null)
                    {
                        onlyObj = new frmSystem(main);
                        frmMain.AddNodeForms(onlyObj);
                    }
                }
            }
            return onlyObj;
        }

        private frmSystem(INMain main) : base(main)
        {
            InitializeComponent();
        }
        #endregion

        private void frmSystem_Load(object sender, EventArgs e)
        {
            #region 解除报警
            DataGridViewCheckBoxColumn ck = new DataGridViewCheckBoxColumn();
            this.dgvAlarmType.Columns.Add(ck);
            this.dgvAlarmType.Columns[0].HeaderText = "选择";
            this.dgvAlarmType.Columns[0].Width = 38;
            this.dgvAlarmType.Rows.Add(13);
            this.dgvAlarmType.Columns.Add("", "报警类型");
            this.dgvAlarmType.Columns[1].Width = 130;
            this.dgvAlarmType.Rows[0].Cells[1].Value = "非法卡报警";
            this.dgvAlarmType.Rows[1].Cells[1].Value = "门磁报警";
            this.dgvAlarmType.Rows[2].Cells[1].Value = "胁迫报警";
            this.dgvAlarmType.Rows[3].Cells[1].Value = "开门超时报警";
            this.dgvAlarmType.Rows[4].Cells[1].Value = "黑名单报警";
            this.dgvAlarmType.Rows[5].Cells[1].Value = "匪警报警";
            this.dgvAlarmType.Rows[6].Cells[1].Value = "防盗主机报警";
            this.dgvAlarmType.Rows[7].Cells[1].Value = "消防报警";
            this.dgvAlarmType.Rows[8].Cells[1].Value = "烟雾报警";
            this.dgvAlarmType.Rows[9].Cells[1].Value = "关闭电锁出错报警";
            this.dgvAlarmType.Rows[10].Cells[1].Value = "防拆报警";
            this.dgvAlarmType.Rows[11].Cells[1].Value = "强制关锁报警";
            this.dgvAlarmType.Rows[12].Cells[1].Value = "强制开锁报警";

            this.dgvAlarmType.AllowUserToAddRows = false;
            for (int i = 0; i < this.dgvAlarmType.Rows.Count; i++)
            {
                this.dgvAlarmType.Rows[i].Cells[0].Value = true;
            }
            #endregion

            #region 获取设备状态信息
            this.dgvEquipmentStatusInfo.Rows.Add(9);
            this.dgvEquipmentStatusInfo.Rows[0].Cells[0].Value = "门1";
            this.dgvEquipmentStatusInfo.Rows[1].Cells[0].Value = "门2";
            this.dgvEquipmentStatusInfo.Rows[2].Cells[0].Value = "门3";
            this.dgvEquipmentStatusInfo.Rows[3].Cells[0].Value = "门4";
            this.dgvEquipmentStatusInfo.Rows[4].Cells[0].Value = "消防报警";
            this.dgvEquipmentStatusInfo.Rows[5].Cells[0].Value = "匪警报警";
            this.dgvEquipmentStatusInfo.Rows[6].Cells[0].Value = "烟雾报警";
            this.dgvEquipmentStatusInfo.Rows[7].Cells[0].Value = "防盗主机";
            this.dgvEquipmentStatusInfo.Rows[8].Cells[0].Value = "控制板防拆";
            this.dgvEquipmentStatusInfo.AllowUserToAddRows = false;
            #endregion
        }

        #region SN的读写操作

        private void butReadSN_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadSN cmd = new ReadSN(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                SN_Result result = cmde.Command.getResult() as SN_Result;
                string sn = result.SNBuf.GetString();
                Invoke(() =>
                {
                    txtSN.Text = sn;
                });
                mMainForm.AddCmdLog(cmde, sn);
            };
        }

        public void ReadSN()
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadSN cmd = new ReadSN(cmdDtl);
            mMainForm.AddCommand(cmd);

            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                SN_Result result = cmde.Command.getResult() as SN_Result;
                string sn = result.SNBuf.GetString();
               
                mMainForm.AddCmdLog(cmde, sn);
            };
        }

        private bool CheckSN()
        {
            string sn = txtSN.Text;
            if (sn.Length != 16)
            {
                MsgErr("SN 是16位字符，请重新输入！");
                return false;
            }
            int len = System.Text.Encoding.ASCII.GetByteCount(sn);
            if (len != 16)
            {
                MsgErr("SN 是16位字符，请重新输入！");
                return false;
            }
            return true;
        }

        private void butWriteSN_Click(object sender, EventArgs e)
        {
            if (!CheckSN()) return;
            string sn = txtSN.Text;
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            WriteSN cmd = new WriteSN(cmdDtl, new SN_Parameter(sn));
            mMainForm.AddCommand(cmd);


        }

        private void butWriteSN_Broadcast_Click(object sender, EventArgs e)
        {
            if (!CheckSN()) return;
            string sn = txtSN.Text;
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            WriteSN_Broadcast cmd = new WriteSN_Broadcast(cmdDtl, new SN_Parameter(sn));
            mMainForm.AddCommand(cmd);

        }
        #endregion

        #region 通讯密码
        private void butReadConnectPassword_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadConnectPassword cmd = new ReadConnectPassword(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                Password_Result result = cmde.Command.getResult() as Password_Result;
                string pwd = result.Password;
                Invoke(() =>
                {
                    txtConnectPassword.Text = pwd;
                });
                mMainForm.AddCmdLog(cmde, pwd);
            };
        }

        public void ReadPassword()
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadConnectPassword cmd = new ReadConnectPassword(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                Password_Result result = cmde.Command.getResult() as Password_Result;
                string pwd = result.Password;
                
                mMainForm.AddCmdLog(cmde, pwd);
            };
        }

        private void butWriteConnectPassword_Click(object sender, EventArgs e)
        {
            string pwd = txtConnectPassword.Text;
            if (pwd.Length != 8)
            {
                MsgErr("通讯密码 是8位十六进制字符，请重新输入！");
                return;
            }
            if (!pwd.IsHex())
            {
                MsgErr("通讯密码 是8位十六进制字符，请重新输入！");
                return;
            }


            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            WriteConnectPassword cmd = new WriteConnectPassword(cmdDtl, new Password_Parameter(pwd));
            mMainForm.AddCommand(cmd);


        }

        private void butResetConnectPassword_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ResetConnectPassword cmd = new ResetConnectPassword(cmdDtl);
            mMainForm.AddCommand(cmd);

        }
        #endregion

        #region TCP参数
        private void ButRendTCPSetting_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadTCPSetting cmd = new ReadTCPSetting(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadTCPSetting_Result result = cmde.Command.getResult() as ReadTCPSetting_Result;

                Invoke(() =>
                {
                    txtMAC.Text = result.TCP.mMAC;
                    txtIP.Text = result.TCP.mIP;
                    txtIPMask.Text = result.TCP.mIPMask;
                    txtIPGateway.Text = result.TCP.mIPGateway;
                    txtDNS.Text = result.TCP.mDNS;
                    txtDNSBackup.Text = result.TCP.mDNSBackup;
                    txtTCPPort.Text = result.TCP.mTCPPort.ToString();
                    txtUDPPort.Text = result.TCP.mUDPPort.ToString();
                    txtServerIP.Text = result.TCP.mServerIP;
                    txtServerAddr.Text = result.TCP.mServerAddr;
                    txtServerPort.Text = result.TCP.mServerPort.ToString();

                    cbxProtocolType.SelectedIndex = result.TCP.mProtocolType;
                    cbxAutoIP.SelectedIndex = result.TCP.mAutoIP == true ? 1 : 0;
                });
                string TCPInfo = DebugTCPDetail(result.TCP);
                mMainForm.AddCmdLog(cmde, TCPInfo);
            };
        }

        private string DebugTCPDetail(TCPDetail tcp)
        {
            string MAC = tcp.mMAC; //MAC地址
            string IP = tcp.mIP; //IP
            string IPMask = tcp.mIPMask; //子网掩码
            string IPGateway = tcp.mIPGateway; //网关地址
            string DNS = tcp.mDNS; //DNS
            string DNSBackup = tcp.mDNSBackup; //备用DNS
            string TCPPort = tcp.mTCPPort.ToString(); //本地TCP端口
            string UDPPort = tcp.mUDPPort.ToString(); //本地UDP端口
            string ServerIP = tcp.mServerIP; //服务器IP
            string ServerAddr = tcp.mServerAddr; //服务器域名
            string ServerPort = tcp.mServerPort.ToString(); //服务器端口

            int ProtocolType = tcp.mProtocolType; //TCP工作模式
            bool AutoIP = tcp.mAutoIP; //是否自动获得IP


            string TCPInfo = "MAC地址：" + MAC +
                             "  IP：" + IP +
                             "  子网掩码：" + IPMask +
                             "  网关地址：" + IPGateway +
                             "  DNS：" + DNS +
                             "  备用DNS：" + DNSBackup +
                             "  本地TCP端口：" + TCPPort +
                             "  本地UDP端口：" + UDPPort +
                             "  服务器IP：" + ServerIP +
                             "  服务器域名：" + ServerAddr +
                             "  服务器端口：" + ServerPort;
            return TCPInfo;
        }

        private void ButWriteTCPSetting_Click(object sender, EventArgs e)
        {
            string reg = @"([A-Fa-f0-9]{2}-){5}[A-Fa-f0-9]{2}";
            string reg2 = @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$";
            string reg3 = @"^\+?[1-9][0-9]*$";
            string reg4 = @"^(?=^.{3,255}$)[a-zA-Z0-9][-a-zA-Z0-9]{0,62}(\.[a-zA-Z0-9][-a-zA-Z0-9]{0,62})+$";

            if (!Regex.IsMatch(txtMAC.Text.Trim(), reg))
            {
                MsgErr("请输入正确MAC地址！");
                return;
            }

            if (!Regex.IsMatch(txtIP.Text.Trim(), reg2))
            {
                MsgErr("请输入正确IP地址！");
                return;
            }
            if (!Regex.IsMatch(txtIPMask.Text.Trim(), reg2))
            {
                MsgErr("请输入正确子网掩码！");
                return;
            }
            if (!Regex.IsMatch(txtIPGateway.Text.Trim(), reg2))
            {
                MsgErr("请输入正确网关IP！");
                return;
            }
            if (!Regex.IsMatch(txtDNS.Text.Trim(), reg2))
            {
                MsgErr("请输入正确DNS！");
                return;
            }
            if (!Regex.IsMatch(txtDNSBackup.Text.Trim(), reg2))
            {
                MsgErr("请输入正确备用DNS！");
                return;
            }
            if (!Regex.IsMatch(txtServerIP.Text.Trim(), reg2))
            {
                MsgErr("请输入正确服务器IP！");
                return;
            }
            if (!Regex.IsMatch(txtTCPPort.Text.Trim(), reg3))
            {
                MsgErr("请输入正确本地TCP端口！");
                return;
            }
            if (Convert.ToInt32(txtTCPPort.Text.Trim()) > 65535)
            {
                MsgErr("请输入正确本地TCP端口！");
                return;
            }
            if (!Regex.IsMatch(txtUDPPort.Text.Trim(), reg3))
            {
                MsgErr("请输入正确本地UDP端口！");
                return;
            }
            if (Convert.ToInt32(txtUDPPort.Text.Trim()) > 65535)
            {
                MsgErr("请输入正确本地TCP端口！");
                return;
            }
            if (!Regex.IsMatch(txtServerPort.Text.Trim(), reg3))
            {
                MsgErr("请输入正确服务器端口！");
                return;
            }
            if (Convert.ToInt32(txtServerPort.Text.Trim()) > 65535)
            {
                MsgErr("请输入正确服务器端口！");
                return;
            }
            //if (!Regex.IsMatch(txtServerAddr.Text.Trim(), reg4))
            //{
            //    MsgErr("请输入正确服务器域名！");
            //    return;
            //}
            if (Convert.ToInt16(cbxProtocolType.SelectedIndex) == 0)
            {
                MsgErr("请选择TCP工作模式！");
                return;
            }
            if (Convert.ToInt16(cbxAutoIP.SelectedIndex) == -1)
            {
                MsgErr("请选择是否自动获得IP！");
                return;
            }

            TCPDetail tcp = new TCPDetail();
            tcp.mIP = txtIP.Text.Trim();
            tcp.mMAC = txtMAC.Text.Trim();
            tcp.mIPMask = txtIPMask.Text.Trim();
            tcp.mIPGateway = txtIPGateway.Text.Trim();
            tcp.mDNS = txtDNS.Text.Trim();
            tcp.mDNSBackup = txtDNSBackup.Text.Trim();
            tcp.mTCPPort = Convert.ToInt32(txtTCPPort.Text.Trim());
            tcp.mUDPPort = Convert.ToInt32(txtUDPPort.Text.Trim());
            tcp.mServerIP = txtServerIP.Text.Trim();
            tcp.mServerAddr = txtServerAddr.Text.Trim();
            tcp.mServerPort = Convert.ToInt32(txtServerPort.Text.Trim());

            tcp.mProtocolType = Convert.ToUInt16(cbxProtocolType.SelectedIndex);

            if (cbxAutoIP.SelectedIndex == 1)
            {
                tcp.mAutoIP = true;
            }
            else
            {
                tcp.mAutoIP = false;
            }

            //var buf = DotNetty.Buffers.UnpooledByteBufferAllocator.Default.Buffer(0x89);
            //tcp.GetBytes(buf);

            //TCPDetail newtcp = new TCPDetail();
            //newtcp.SetBytes(buf);

            //string TCPInfo = "MAC地址:" + newtcp.mMAC +
            //                    "  IP：" + newtcp.mIP +
            //                    "  子网掩码：" + newtcp.mIPMask +
            //                    "  网关地址：" + newtcp.mIPGateway +
            //                    "  DNS：" + newtcp.mDNS +
            //                    "  备用DNS：" + newtcp.mDNSBackup +
            //                    "  本地TCP端口：" + newtcp.mTCPPort +
            //                    "  本地UDP端口：" + newtcp.mUDPPort +
            //                    "  服务器IP：" + newtcp.mServerIP +
            //                    "  服务器域名：" + newtcp.mServerAddr +
            //                    "  TCP工作模式：" + newtcp.mProtocolType +
            //                    "  自动获得IP：" + newtcp.mAutoIP +
            //                    "  服务器端口：" + newtcp.mServerPort;
            //mMainForm.AddCmdLog(null, TCPInfo);

            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            WriteTCPSetting cmd = new WriteTCPSetting(cmdDtl, new WriteTCPSetting_Parameter(tcp));
            mMainForm.AddCommand(cmd);
        }
        #endregion

        #region 设备有效期
        private void BtnReadDeadline_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadDeadline cmd = new ReadDeadline(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadDeadline_Result result = cmde.Command.getResult() as ReadDeadline_Result;

                ushort Deadline = result.Deadline; //有效期
                string DeadlineInfo = string.Empty;
                if (Deadline == 0)
                {
                    DeadlineInfo = "失效";
                }
                else if (Deadline == 65535)
                {
                    DeadlineInfo = "无限制(65535)";
                }
                else
                {
                    DeadlineInfo = Deadline.ToString() + "天";
                }

                Invoke(() =>
                {
                    cbxDeadline.Text = DeadlineInfo.Replace("天", "");
                });
                string DeadlineDay = "设备剩余有效天数:" + DeadlineInfo;
                mMainForm.AddCmdLog(cmde, DeadlineDay);
            };
        }

        private void BtnWriteDeadline_Click(object sender, EventArgs e)
        {
            string reg = @"^\+?[0-9]*$";
            if (!Regex.IsMatch(cbxDeadline.Text.Trim(), reg))
            {
                if (cbxDeadline.Text != "无限制(65535)" && cbxDeadline.Text != "失效")
                {
                    MsgErr("请输入正确有效天数！");
                    return;
                }
            }
            if (Regex.IsMatch(cbxDeadline.Text.Trim(), reg))
            {
                if (Convert.ToUInt32(cbxDeadline.Text) < 0 || Convert.ToUInt32(cbxDeadline.Text) > 65535)
                {
                    MsgErr("请输入正确有效天数！");
                    return;
                }
            }

            ushort deadlineDay = 0;
            string deadlineInfo = cbxDeadline.Text;
            if (deadlineInfo == "无限制(65535)")
            {
                deadlineDay = 65535;
            }
            else if (deadlineInfo == "失效")
            {
                deadlineDay = 0;
            }
            else
            {
                deadlineDay = Convert.ToUInt16(cbxDeadline.Text);
            }

            //WriteDeadline_Parameter wp = new WriteDeadline_Parameter();
            //wp.Deadline = deadlineDay;
            //var buf = DotNetty.Buffers.UnpooledByteBufferAllocator.Default.Buffer(0x02);
            //wp.GetBytes(buf);

            //WriteDeadline_Parameter wp2 = new WriteDeadline_Parameter();
            //wp2.SetBytes(buf);

            //string DeadlineDay = "设备剩余有效期天数:" + deadlineDay;
            //mMainForm.AddCmdLog(null, DeadlineDay);

            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            WriteDeadline cmd = new WriteDeadline(cmdDtl, new WriteDeadline_Parameter(deadlineDay));
            mMainForm.AddCommand(cmd);
        }
        #endregion

        #region 设备版本号
        private void BtnReadVersion_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadVersion cmd = new ReadVersion(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadVersion_Result result = cmde.Command.getResult() as ReadVersion_Result;
                string version = result.Version.ToString();
                Invoke(() =>
                {
                    txtVersion.Text = "Ver " + version;
                });
                version = "版本号：" + version;
                mMainForm.AddCmdLog(cmde, version);
            };
        }
        #endregion

        #region 设备运行信息
        private void BtnReadSystemStatus_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadSystemStatus cmd = new ReadSystemStatus(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadSystemStatus_Result result = cmde.Command.getResult() as ReadSystemStatus_Result;
                string RunDay = result.RunDay.ToString() + "天"; //设备已运行天数
                string FormatCount = result.FormatCount.ToString() + "次"; //格式化次数
                string RestartCount = result.RestartCount.ToString() + "次"; //看门狗复位次数
                string UPS = result.UPS == 0 ? "电源取电" : "UPS供电"; //UPS工作状态
                string Temperature = result.Temperature; //设备温度
                string Voltage = result.Voltage; //接入电压
                string StartTime = result.StartTime; //上电时间

                Invoke(() =>
                {
                    txtRunDay.Text = RunDay;
                    txtFormatCount.Text = FormatCount;
                    txtRestartCount.Text = RestartCount;
                    txtUPS.Text = UPS;
                    txtTemperature.Text = Temperature;
                    txtVoltage.Text = Voltage;
                    txtStartTime.Text = StartTime;

                });
                string TCPInfo = "设备已运行天数:" + RunDay +
                                 "  格式化次数：" + FormatCount +
                                 "  看门狗复位次数：" + RestartCount +
                                 "  UPS工作状态：" + UPS +
                                 "  设备温度：" + Temperature +
                                 "  接入电压：" + Voltage +
                                 "  上电时间：" + StartTime;
                mMainForm.AddCmdLog(cmde, TCPInfo);
            };
        }
        #endregion

        #region 记录存储方式
        private void BtnReadRecordMode_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadRecordMode cmd = new ReadRecordMode(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadRecordMode_Result result = cmde.Command.getResult() as ReadRecordMode_Result;
                string ModeStr = result.Mode == 0 ? "【0、记录存满后，循环覆盖存储】" : "【1、满后报警，不再保存新纪录】"; //记录存储方式
                Invoke(() =>
                {
                    if (result.Mode == 0)
                    {
                        rBtnCover.Checked = true;
                    }
                    else
                    {
                        rBtnNoCover.Checked = true;
                    }
                });
                ModeStr = "记录存储方式：" + ModeStr;
                mMainForm.AddCmdLog(cmde, ModeStr);
            };
        }

        private void BtnWriteRecordMode_Click(object sender, EventArgs e)
        {
            byte mode = 0;
            if (rBtnNoCover.Checked == true)
            {
                mode = 1;
            }

            //WriteRecordMode_Parameter wp = new WriteRecordMode_Parameter();
            //wp.Mode = mode;
            //var buf = DotNetty.Buffers.UnpooledByteBufferAllocator.Default.Buffer(0x02);
            //wp.GetBytes(buf);

            //WriteRecordMode_Parameter wp2 = new WriteRecordMode_Parameter();
            //wp2.SetBytes(buf);

            //string DeadlineDay = "记录存储方式:" + mode;
            //mMainForm.AddCmdLog(null, DeadlineDay);

            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            WriteRecordMode cmd = new WriteRecordMode(cmdDtl, new WriteRecordMode_Parameter(mode));
            mMainForm.AddCommand(cmd);
        }
        #endregion

        #region 读卡器密码键盘启用功能开关
        private void BtnReadKeyboard_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadKeyboard cmd = new ReadKeyboard(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadKeyboard_Result result = cmde.Command.getResult() as ReadKeyboard_Result;
                string KeyboardInfo = string.Empty;
                Invoke(() =>
                {
                    for (int i = 0; i < result.Keyboard.Count; i++)
                    {
                        if (result.Keyboard[i] == true)
                        {
                            if (i == 0)
                            {
                                cBox1.Checked = true;
                            }
                            else if (i == 1)
                            {
                                cBox2.Checked = true;
                            }
                            else if (i == 2)
                            {
                                cBox3.Checked = true;
                            }
                            else if (i == 3)
                            {
                                cBox4.Checked = true;
                            }
                            else if (i == 4)
                            {
                                cBox5.Checked = true;
                            }
                            else if (i == 5)
                            {
                                cBox6.Checked = true;
                            }
                            else if (i == 6)
                            {
                                cBox7.Checked = true;
                            }
                            else if (i == 7)
                            {
                                cBox8.Checked = true;
                            }

                            KeyboardInfo = KeyboardInfo + "  读卡器：" + (i + 1) + "，键盘开关：【1、接收键盘信号】";
                        }
                        else
                        {
                            KeyboardInfo = KeyboardInfo + "  读卡器：" + (i + 1) + "，键盘开关：【0、不接收键盘信号】";
                        }
                    }
                });
                mMainForm.AddCmdLog(cmde, KeyboardInfo);
            };
        }

        private void BtnWriteKeyboard_Click(object sender, EventArgs e)
        {
            BitArray bitSet = new BitArray(8);
            bitSet[0] = cBox1.Checked;
            bitSet[1] = cBox2.Checked;
            bitSet[2] = cBox3.Checked;
            bitSet[3] = cBox4.Checked;
            bitSet[4] = cBox5.Checked;
            bitSet[5] = cBox6.Checked;
            bitSet[6] = cBox7.Checked;
            bitSet[7] = cBox8.Checked;

            //WriteKeyboard_Parameter wp = new WriteKeyboard_Parameter();
            //wp.Keyboard = bitSet;
            //var buf = DotNetty.Buffers.UnpooledByteBufferAllocator.Default.Buffer(0x02);
            //wp.GetBytes(buf);

            //WriteKeyboard_Parameter wp2 = new WriteKeyboard_Parameter();
            //wp2.SetBytes(buf);

            //string DeadlineDay = "键盘开关1:" + bitSet[0] +
            //                     "  键盘开关2:" + bitSet[1] +
            //                     "  键盘开关3:" + bitSet[2] +
            //                     "  键盘开关4:" + bitSet[3] +
            //                     "  键盘开关5:" + bitSet[4] +
            //                     "  键盘开关6:" + bitSet[5] +
            //                     "  键盘开关7:" + bitSet[6] +
            //                     "  键盘开关8:" + bitSet[7];
            //mMainForm.AddCmdLog(null, DeadlineDay);

            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            WriteKeyboard cmd = new WriteKeyboard(cmdDtl, new WriteKeyboard_Parameter(bitSet));
            mMainForm.AddCommand(cmd);
        }
        #endregion

        #region 互锁参数
        private void BtnReadLockInteraction_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadLockInteraction cmd = new ReadLockInteraction(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadLockInteraction_Result result = cmde.Command.getResult() as ReadLockInteraction_Result;
                string DoorLockInfo = string.Empty;
                Invoke(() =>
                {
                    for (int i = 0; i < result.DoorPort.DoorMax; i++)
                    {
                        if (result.DoorPort.DoorPort[i] == 1)
                        {
                            if (i == 0)
                            {
                                cBoxDoor1.Checked = true;
                            }
                            else if (i == 1)
                            {
                                cBoxDoor2.Checked = true;
                            }
                            else if (i == 2)
                            {
                                cBoxDoor3.Checked = true;
                            }
                            else if (i == 3)
                            {
                                cBoxDoor4.Checked = true;
                            }

                            DoorLockInfo = DoorLockInfo + "  门" + (i + 1) + " 互锁";
                        }
                        if (string.IsNullOrWhiteSpace(DoorLockInfo))
                        {
                            DoorLockInfo = "不需要互锁";
                        }
                    }
                });
                mMainForm.AddCmdLog(cmde, DoorLockInfo);
            };
        }

        private void BtnWirteLockInteraction_Click(object sender, EventArgs e)
        {
            DoorPortDetail dpd = new DoorPortDetail(4);
            if (cBoxDoor1.Checked)
            {
                dpd.DoorPort[0] = 1;
            }
            else
            {
                dpd.DoorPort[0] = 0;
            }
            if (cBoxDoor2.Checked)
            {
                dpd.DoorPort[1] = 1;
            }
            else
            {
                dpd.DoorPort[1] = 0;
            }
            if (cBoxDoor3.Checked)
            {
                dpd.DoorPort[2] = 1;
            }
            else
            {
                dpd.DoorPort[2] = 0;
            }
            if (cBoxDoor4.Checked)
            {
                dpd.DoorPort[3] = 1;
            }
            else
            {
                dpd.DoorPort[3] = 0;
            }

            //WriteLockInteraction_Parameter wp = new WriteLockInteraction_Parameter();
            //wp.DoorPort = dpd;
            //var buf = DotNetty.Buffers.UnpooledByteBufferAllocator.Default.Buffer(0x02);
            //wp.GetBytes(buf);

            //WriteLockInteraction_Parameter wp2 = new WriteLockInteraction_Parameter();
            //wp2.SetBytes(buf);

            //string DeadlineDay = "门1:" + dpd.DoorPort[0] +
            //                     "  门2:" + dpd.DoorPort[1] +
            //                     "  门3:" + dpd.DoorPort[2] +
            //                     "  门4:" + dpd.DoorPort[3];
            //mMainForm.AddCmdLog(null, DeadlineDay);

            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            WriteLockInteraction cmd = new WriteLockInteraction(cmdDtl, new WriteLockInteraction_Parameter(dpd));
            mMainForm.AddCommand(cmd);
        }
        #endregion

        #region 消防报警
        private void BtnReadFireAlarmOption_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadFireAlarmOption cmd = new ReadFireAlarmOption(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadFireAlarmOption_Result result = cmde.Command.getResult() as ReadFireAlarmOption_Result;
                int OptionType = result.Option; //消防报警参数
                string OptionTypeStr = string.Empty;
                if (OptionType == 0)
                {
                    OptionTypeStr = "【0、不启用】";
                }
                else if (OptionType == 1)
                {
                    OptionTypeStr = "【1、报警输出，并开所有门，只能软件解除】";
                }
                else if (OptionType == 2)
                {
                    OptionTypeStr = "【2、报警输出，不开所有门，只能软件解除】";
                }
                else if (OptionType == 3)
                {
                    OptionTypeStr = "【3、有信号报警并开门，无信号解除报警并关门】";
                }
                else if (OptionType == 4)
                {
                    OptionTypeStr = "【4、有报警信号时开一次门，就像按钮开门一样】";
                }
                Invoke(() =>
                {
                    cbxOption.SelectedIndex = OptionType;
                });
                string Info = "消防报警参数：" + OptionTypeStr;
                mMainForm.AddCmdLog(cmde, Info);
            };
        }

        private void BtnWriteFireAlarmOption_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt16(cbxOption.SelectedIndex) == -1)
            {
                MsgErr("请选择消防报警模式！");
                return;
            }
            byte Option = Convert.ToByte(cbxOption.SelectedIndex);

            //WriteFireAlarmOption_Parameter wp = new WriteFireAlarmOption_Parameter();
            //wp.Option = Option;
            //var buf = DotNetty.Buffers.UnpooledByteBufferAllocator.Default.Buffer(0x01);
            //wp.GetBytes(buf);

            //WriteFireAlarmOption_Parameter wp2 = new WriteFireAlarmOption_Parameter();
            //wp2.SetBytes(buf);

            //string Info = "消防报警模式:" + Option;
            //mMainForm.AddCmdLog(null, Info);

            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            WriteFireAlarmOption cmd = new WriteFireAlarmOption(cmdDtl, new WriteFireAlarmOption_Parameter(Option));
            mMainForm.AddCommand(cmd);
        }
        private void BtnAlarm_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            SendFireAlarm cmd = new SendFireAlarm(cmdDtl);
            mMainForm.AddCommand(cmd);
            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                mMainForm.AddCmdLog(cmde, "开启消防报警");
            };
        }

        private void BtnCloseAlarm_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            CloseFireAlarm cmd = new CloseFireAlarm(cmdDtl);
            mMainForm.AddCommand(cmd);
            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                mMainForm.AddCmdLog(cmde, "解除消防报警");
            };
        }

        private void BtnAlarmState_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadFireAlarmState cmd = new ReadFireAlarmState(cmdDtl);
            mMainForm.AddCommand(cmd);
            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                string ModeStr = cmd.FireAlarmState == 0 ? "【0、未开启报警】" : "【1、已开启报警】"; //消防报警状态
                Invoke(() =>
                {
                    mMainForm.AddCmdLog(cmde, ModeStr);
                });
            };
        }
        #endregion

        #region 匪警报警
        private void BtnReadOpenAlarmOption_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadOpenAlarmOption cmd = new ReadOpenAlarmOption(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadOpenAlarmOption_Result result = cmde.Command.getResult() as ReadOpenAlarmOption_Result;
                int OptionType = result.Option; //匪警报警参数
                string OptionTypeStr = string.Empty;
                if (OptionType == 0)
                {
                    OptionTypeStr = "【0、禁用】";
                }
                else if (OptionType == 1)
                {
                    OptionTypeStr = "【1、报警并锁定所有门】";
                }
                else if (OptionType == 2)
                {
                    OptionTypeStr = "【2、报警，不锁定门】";
                }
                else if (OptionType == 3)
                {
                    OptionTypeStr = "【3、有信号报警，无信号解除】";
                }
                Invoke(() =>
                {
                    cbxPoliceType.SelectedIndex = OptionType;
                });
                string Info = "匪警报警参数：" + OptionTypeStr;
                mMainForm.AddCmdLog(cmde, Info);
            };
        }

        private void BtnWriteOpenAlarmOption_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt16(cbxPoliceType.SelectedIndex) == -1)
            {
                MsgErr("请选择匪警报警模式！");
                return;
            }
            byte Option = Convert.ToByte(cbxPoliceType.SelectedIndex);

            //WriteOpenAlarmOption_Parameter wp = new WriteOpenAlarmOption_Parameter();
            //wp.Option = Option;
            //var buf = DotNetty.Buffers.UnpooledByteBufferAllocator.Default.Buffer(0x01);
            //wp.GetBytes(buf);

            //WriteOpenAlarmOption_Parameter wp2 = new WriteOpenAlarmOption_Parameter();
            //wp2.SetBytes(buf);

            //string Info = "匪警报警模式:" + Option;
            //mMainForm.AddCmdLog(null, Info);

            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            WriteOpenAlarmOption cmd = new WriteOpenAlarmOption(cmdDtl, new WriteOpenAlarmOption_Parameter(Option));
            mMainForm.AddCommand(cmd);
        }
        #endregion

        #region 读卡间隔时间
        private void BtnReadIntervalTime_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadReaderIntervalTime cmd = new ReadReaderIntervalTime(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadReaderIntervalTime_Result result = cmde.Command.getResult() as ReadReaderIntervalTime_Result;

                ushort IntervalTime = result.IntervalTime; //读卡间隔时间
                string IntervalTimeInfo = string.Empty;
                if (IntervalTime == 0)
                {
                    IntervalTimeInfo = "无限制";
                }
                else
                {
                    IntervalTimeInfo = IntervalTime.ToString() + "秒";
                }

                Invoke(() =>
                {
                    cbxIntervalTime.Text = IntervalTimeInfo.Replace("秒", "");
                });
                string IntervalTimeStr = "读卡间隔时间：" + IntervalTimeInfo;
                mMainForm.AddCmdLog(cmde, IntervalTimeStr);
            };
        }

        private void BtnWriteIntervalTime_Click(object sender, EventArgs e)
        {
            string reg = @"^\+?[0-9]*$";
            if (!Regex.IsMatch(cbxIntervalTime.Text.Trim(), reg))
            {
                if (cbxIntervalTime.Text != "无限制")
                {
                    MsgErr("请输入正确读卡间隔时间！");
                    return;
                }
            }
            if (Regex.IsMatch(cbxIntervalTime.Text.Trim(), reg))
            {
                if (Convert.ToUInt32(cbxIntervalTime.Text) < 0 || Convert.ToUInt32(cbxIntervalTime.Text) > 65535)
                {
                    MsgErr("请输入正确读卡间隔时间！");
                    return;
                }
            }

            ushort IntervalTime = 0;
            string deadlineInfo = cbxIntervalTime.Text;
            if (deadlineInfo == "无限制")
            {
                IntervalTime = 0;
            }
            else
            {
                IntervalTime = Convert.ToUInt16(cbxIntervalTime.Text);
            }

            //WriteReaderIntervalTime_Parameter wp = new WriteReaderIntervalTime_Parameter();
            //wp.IntervalTime = IntervalTime;
            //var buf = DotNetty.Buffers.UnpooledByteBufferAllocator.Default.Buffer(0x02);
            //wp.GetBytes(buf);

            //WriteReaderIntervalTime_Parameter wp2 = new WriteReaderIntervalTime_Parameter();
            //wp2.SetBytes(buf);

            //string DeadlineDay = "读卡间隔时间:" + IntervalTime;
            //mMainForm.AddCmdLog(null, DeadlineDay);

            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            WriteReaderIntervalTime cmd = new WriteReaderIntervalTime(cmdDtl, new WriteReaderIntervalTime_Parameter(IntervalTime));
            mMainForm.AddCommand(cmd);
        }
        #endregion

        #region 语音播报语音段开关
        private void BtnReadBroadcast_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadBroadcast cmd = new ReadBroadcast(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadBroadcast_Result result = cmde.Command.getResult() as ReadBroadcast_Result;

                Invoke(() =>
                {
                    byte[] broadcast = new byte[4];
                    Array.Copy(result.Broadcast.Broadcast, 6, broadcast, 0, 4);
                    StringBuilder BroadcastInfo = new StringBuilder(64); //语音段开关

                    Array.Reverse(broadcast);
                    BitArray bit = new BitArray(broadcast);
                    for (int i = 30; i >= 0; i--)
                    {
                        BroadcastInfo.Append(bit[i] ? 1 : 0);
                    }

                    txtBroadcast.Text = BroadcastInfo.ToString();
                    string IntervalTimeStr = "语音段开关：" + txtBroadcast.Text + "  顺序 31←1";
                    mMainForm.AddCmdLog(cmde, IntervalTimeStr);
                });

            };
        }

        private void BtnWriteBroadcast_Click(object sender, EventArgs e)
        {
            string reg = @"^\+?[0-1]*$";
            if (!Regex.IsMatch(txtBroadcast.Text.Trim(), reg) || txtBroadcast.Text.Trim().Length != 31)
            {
                MsgErr("请输入正确格式语音开关段设置！");
                return;
            }
            byte[] bData = new byte[10];

            string strBit = txtBroadcast.Text.Trim();
            strBit = "0" + strBit;

            byte[] tmpData = new byte[4];
            BitArray bit = new BitArray(tmpData);
            int strIndex = 0;
            for (int i = 30; i >= 0; i--)
            {
                bit[i] = (strBit.Substring(++strIndex, 1) == "1");
            }

            bit.CopyTo(tmpData, 0);
            Array.Reverse(tmpData);
            Array.Copy(tmpData, 0, bData, 6, 4);

            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            WriteBroadcast cmd = new WriteBroadcast(cmdDtl, new WriteBroadcast_Parameter(bData));
            mMainForm.AddCommand(cmd);
        }
        #endregion

        #region 读卡器数据校验
        private void BtnReadReaderCheckMode_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadReaderCheckMode cmd = new ReadReaderCheckMode(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadReaderCheckMode_Result result = cmde.Command.getResult() as ReadReaderCheckMode_Result;
                string ModeStr = string.Empty; //读卡器数据校验
                Invoke(() =>
                {
                    if (result.ReaderCheckMode == 0)
                    {
                        rBtnNoEnable.Checked = true;
                        ModeStr = "0、不启用";
                    }
                    else if (result.ReaderCheckMode == 1)
                    {
                        rBtnEnable.Checked = true;
                        ModeStr = "1、启用";
                    }
                    else
                    {
                        rBtnEnableValidation.Checked = true;
                        ModeStr = "2、启用校验";
                    }
                });
                ModeStr = "读卡器校验：" + ModeStr;
                mMainForm.AddCmdLog(cmde, ModeStr);
            };
        }

        private void BtnWriteReaderCheckMode_Click(object sender, EventArgs e)
        {
            byte mode = 0;
            if (rBtnEnable.Checked == true)
            {
                mode = 1;
            }
            else if (rBtnEnableValidation.Checked == true)
            {
                mode = 2;
            }

            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            WriteReaderCheckMode cmd = new WriteReaderCheckMode(cmdDtl, new WriteReaderCheckMode_Parameter(mode));
            mMainForm.AddCommand(cmd);
        }
        #endregion

        #region 主板蜂鸣器
        private void BtnReadBuzzer_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadBuzzer cmd = new ReadBuzzer(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadBuzzer_Result result = cmde.Command.getResult() as ReadBuzzer_Result;
                string ModeStr = result.Buzzer == 0 ? "【0、不启用】" : "【1、启用】"; //记录存储方式
                Invoke(() =>
                {
                    if (result.Buzzer == 0)
                    {
                        rBtnNoBuzzer.Checked = true;
                    }
                    else
                    {
                        rBtnBuzzer.Checked = true;
                    }
                });
                ModeStr = "主板蜂鸣器：" + ModeStr;
                mMainForm.AddCmdLog(cmde, ModeStr);
            };
        }

        private void BtnWriteBuzzer_Click(object sender, EventArgs e)
        {
            byte buzzer = 0;
            if (rBtnBuzzer.Checked == true)
            {
                buzzer = 1;
            }

            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            WriteBuzzer cmd = new WriteBuzzer(cmdDtl, new WriteBuzzer_Parameter(buzzer));
            mMainForm.AddCommand(cmd);
        }
        #endregion

        #region 烟雾报警
        private void BtnReadSmogAlarmOption_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadSmogAlarmOption cmd = new ReadSmogAlarmOption(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadSmogAlarmOption_Result result = cmde.Command.getResult() as ReadSmogAlarmOption_Result;
                int OptionType = result.Option; //烟雾报警参数
                string OptionTypeStr = string.Empty;
                if (OptionType == 0)
                {
                    OptionTypeStr = "【0、禁用】";
                }
                else if (OptionType == 1)
                {
                    OptionTypeStr = "【1、仅报警（有信号报警，无信号解除）】";
                }
                else if (OptionType == 2)
                {
                    OptionTypeStr = "【2、报警并开所有门】";
                }
                else if (OptionType == 3)
                {
                    OptionTypeStr = "【3、报警并锁定所有门】";
                }
                Invoke(() =>
                {
                    cbxSmogAlarmOption.SelectedIndex = OptionType;
                });
                string Info = "烟雾报警参数：" + OptionTypeStr;
                mMainForm.AddCmdLog(cmde, Info);
            };
        }

        private void BtnWriteSmogAlarmOption_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt16(cbxSmogAlarmOption.SelectedIndex) == -1)
            {
                MsgErr("请选择烟雾报警模式！");
                return;
            }
            byte Option = Convert.ToByte(cbxSmogAlarmOption.SelectedIndex);

            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            WriteSmogAlarmOption cmd = new WriteSmogAlarmOption(cmdDtl, new WriteSmogAlarmOption_Parameter(Option));
            mMainForm.AddCommand(cmd);
        }
        private void BtnSmogAlarm_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            SendSmogAlarm cmd = new SendSmogAlarm(cmdDtl);
            mMainForm.AddCommand(cmd);
            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                mMainForm.AddCmdLog(cmde, "开启烟雾报警");
            };
        }

        private void BtnCloseSmogAlarm_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            CloseSmogAlarm cmd = new CloseSmogAlarm(cmdDtl);
            mMainForm.AddCommand(cmd);
            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                mMainForm.AddCmdLog(cmde, "解除烟雾报警");
            };
        }

        private void BtnSmogAlarmState_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadSmogAlarmState cmd = new ReadSmogAlarmState(cmdDtl);
            mMainForm.AddCommand(cmd);
            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                string ModeStr = cmd.SmogAlarmState == 0 ? "【0、未开启报警】" : "【1、已开启报警】"; //烟雾报警状态
                Invoke(() =>
                {
                    mMainForm.AddCmdLog(cmde, ModeStr);
                });
            };
        }
        #endregion

        #region 门内人数限制
        private void BtnReadEnterDoorLimit_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadEnterDoorLimit cmd = new ReadEnterDoorLimit(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadEnterDoorLimit_Result result = cmde.Command.getResult() as ReadEnterDoorLimit_Result;
                string GlobalLimit = result.Limit.GlobalLimit.ToString(); //全局上限
                string Door1Limit = result.Limit.DoorLimitArray[0].ToString(); //1号门上限
                string Door2Limit = result.Limit.DoorLimitArray[1].ToString(); //2号门上限
                string Door3Limit = result.Limit.DoorLimitArray[2].ToString(); //3号门上限
                string Door4Limit = result.Limit.DoorLimitArray[3].ToString(); //4号门上限

                string GlobalEnter = result.Limit.GlobalEnter.ToString(); //全局人数
                string Door1Enter = result.Limit.DoorEnterArray[0].ToString(); //1号门人数
                string Door2Enter = result.Limit.DoorEnterArray[1].ToString(); //2号门人数
                string Door3Enter = result.Limit.DoorEnterArray[2].ToString(); //3号门人数
                string Door4Enter = result.Limit.DoorEnterArray[3].ToString(); //4号门人数
                Invoke(() =>
                {
                    txtGlobalLimit.Text = GlobalLimit;
                    txtDoor1Limit.Text = Door1Limit;
                    txtDoor2Limit.Text = Door2Limit;
                    txtDoor3Limit.Text = Door3Limit;
                    txtDoor4Limit.Text = Door4Limit;

                    txtGlobalEnter.Text = GlobalEnter;
                    txtDoor1Enter.Text = Door1Enter;
                    txtDoor2Enter.Text = Door2Enter;
                    txtDoor3Enter.Text = Door3Enter;
                    txtDoor4Enter.Text = Door4Enter;

                });
                string EnterDoorLimitInfo = "全局上限:" + GlobalLimit +
                                 "  1号门上限：" + Door1Limit +
                                 "  2号门上限：" + Door2Limit +
                                 "  3号门上限：" + Door3Limit +
                                 "  4号门上限：" + Door4Limit +
                                 "  全局人数：" + GlobalEnter +
                                 "  1号门人数：" + Door1Enter +
                                 "  2号门人数：" + Door2Enter +
                                 "  3号门人数：" + Door3Enter +
                                 "  4号门人数：" + Door4Enter;
                mMainForm.AddCmdLog(cmde, EnterDoorLimitInfo);
            };
        }

        private void BtnWriteEnterDoorLimit_Click(object sender, EventArgs e)
        {
            string reg = @"^\+?[0-9]*$";
            UInt32 ui32 = 0;
            if (!Regex.IsMatch(txtGlobalLimit.Text.Trim(), reg))
            {
                MsgErr("请输入正确全局上限！");
                return;
            }
            if (!UInt32.TryParse(txtGlobalLimit.Text.Trim(), out ui32))
            {
                MsgErr("全局上限 太大或太小！");
                return;
            }

            if (!Regex.IsMatch(txtDoor1Limit.Text.Trim(), reg))
            {
                MsgErr("请输入正确1号门上限！");
                return;
            }
            if (!UInt32.TryParse(txtDoor1Limit.Text.Trim(), out ui32))
            {
                MsgErr("1号门上限 太大或太小！");
                return;
            }

            if (!Regex.IsMatch(txtDoor2Limit.Text.Trim(), reg))
            {
                MsgErr("请输入正确2号门上限！");
                return;
            }
            if (!UInt32.TryParse(txtDoor2Limit.Text.Trim(), out ui32))
            {
                MsgErr("2号门上限 太大或太小！");
                return;
            }

            if (!Regex.IsMatch(txtDoor3Limit.Text.Trim(), reg))
            {
                MsgErr("请输入正确3号门上限！");
                return;
            }
            if (!UInt32.TryParse(txtDoor3Limit.Text.Trim(), out ui32))
            {
                MsgErr("3号门上限 太大或太小！");
                return;
            }

            if (!Regex.IsMatch(txtDoor4Limit.Text.Trim(), reg))
            {
                MsgErr("请输入正确4号门上限！");
                return;
            }
            if (!UInt32.TryParse(txtDoor4Limit.Text.Trim(), out ui32))
            {
                MsgErr("4号门上限 太大或太小！");
                return;
            }

            if (!Regex.IsMatch(txtDoor1Enter.Text.Trim(), reg))
            {
                MsgErr("请输入正确1号门人数！");
                return;
            }
            if (!UInt32.TryParse(txtDoor1Enter.Text.Trim(), out ui32))
            {
                MsgErr("1号门人数 太大或太小！");
                return;
            }

            if (!Regex.IsMatch(txtDoor2Enter.Text.Trim(), reg))
            {
                MsgErr("请输入正确2号门人数！");
                return;
            }
            if (!UInt32.TryParse(txtDoor2Enter.Text.Trim(), out ui32))
            {
                MsgErr("2号门人数 太大或太小！");
                return;
            }

            if (!Regex.IsMatch(txtDoor3Enter.Text.Trim(), reg))
            {
                MsgErr("请输入正确3号门人数！");
                return;
            }
            if (!UInt32.TryParse(txtDoor3Enter.Text.Trim(), out ui32))
            {
                MsgErr("3号门人数 太大或太小！");
                return;
            }

            if (!Regex.IsMatch(txtDoor4Enter.Text.Trim(), reg))
            {
                MsgErr("请输入正确4号门人数！");
                return;
            }
            if (!UInt32.TryParse(txtDoor4Enter.Text.Trim(), out ui32))
            {
                MsgErr("4号门人数 太大或太小！");
                return;
            }
            
            
            DoorLimit dl = new DoorLimit();
            dl.GlobalLimit = Convert.ToUInt32(txtGlobalLimit.Text.Trim());
            dl.DoorLimitArray[0] = Convert.ToUInt32(txtDoor1Limit.Text.Trim());
            dl.DoorLimitArray[1] = Convert.ToUInt32(txtDoor2Limit.Text.Trim());
            dl.DoorLimitArray[2] = Convert.ToUInt32(txtDoor3Limit.Text.Trim());
            dl.DoorLimitArray[3] = Convert.ToUInt32(txtDoor4Limit.Text.Trim());

            dl.DoorEnterArray[0] = Convert.ToUInt32(txtDoor1Enter.Text.Trim());
            dl.DoorEnterArray[1] = Convert.ToUInt32(txtDoor2Enter.Text.Trim());
            dl.DoorEnterArray[2] = Convert.ToUInt32(txtDoor3Enter.Text.Trim());
            dl.DoorEnterArray[3] = Convert.ToUInt32(txtDoor4Enter.Text.Trim());

            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            WriteEnterDoorLimit cmd = new WriteEnterDoorLimit(cmdDtl, new WriteEnterDoorLimit_Parameter(dl));
            mMainForm.AddCommand(cmd);
        }
        #endregion

        #region 智能防盗主机参数
        private void BtnReadTheftAlarmSetting_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadTheftAlarmSetting cmd = new ReadTheftAlarmSetting(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadTheftAlarmSetting_Result result = cmde.Command.getResult() as ReadTheftAlarmSetting_Result;
                string UseStr = result.Setting.Use ? "【1、启用】" : "【0、禁用】"; //功能开关
                string InTime = result.Setting.InTime.ToString(); //进入延迟
                string OutTime = result.Setting.OutTime.ToString(); //退出延迟
                string AlarmTime = result.Setting.AlarmTime.ToString(); //报警时长
                string BeginPassword = result.Setting.BeginPassword.ToString(); //布防密码
                string ClosePassword = result.Setting.ClosePassword.ToString(); //撤防密码

                Invoke(() =>
                {
                    if (result.Setting.Use)
                    {
                        rBtnTheft.Checked = true;
                    }
                    else
                    {
                        rBtnNoTheft.Checked = true;
                    }
                    cbxInTime.Text = InTime;
                    cbxOutTime.Text = OutTime;
                    cbxAlarmTime.Text = AlarmTime;
                    txtBeginPassword.Text = BeginPassword;
                    txtClosePassword.Text = ClosePassword;
                });
                string TheftAlarmSettingInfo = "功能开关：" + UseStr +
                                               "  进入延迟：" + InTime + "秒" +
                                               "  退出延迟：" + OutTime + "秒" +
                                               "  报警时长：" + AlarmTime + "秒" +
                                               "  布防密码：" + BeginPassword +
                                               "  撤防密码：" + ClosePassword;
                mMainForm.AddCmdLog(cmde, TheftAlarmSettingInfo);
            };
        }

        private void BtnWriteTheftAlarmSetting_Click(object sender, EventArgs e)
        {
            string reg = @"^\+?[0-9]*$";
            if (!Regex.IsMatch(cbxInTime.Text.Trim(), reg) || cbxInTime.Text == "")
            {
                MsgErr("请输入正确进入延迟秒数！");
                return;
            }
            UInt16 ui32 = 0;
            if (Regex.IsMatch(cbxInTime.Text.Trim(), reg))
            {
                if (!UInt16.TryParse(cbxInTime.Text,out ui32))
                {
                    MsgErr("请输入正确进入延迟秒数！");
                    return;
                }
                if (Convert.ToUInt32(cbxInTime.Text) < 0 || Convert.ToUInt32(cbxInTime.Text) > 255)
                {
                    MsgErr("请输入正确进入延迟秒数！");
                    return;
                }
            }
            if (!Regex.IsMatch(cbxOutTime.Text.Trim(), reg) || cbxOutTime.Text == "")
            {
                MsgErr("请输入正确退出延迟秒数！");
                return;
            }
            if (Regex.IsMatch(cbxOutTime.Text.Trim(), reg))
            {
                if (!UInt16.TryParse(cbxOutTime.Text, out ui32))
                {
                    MsgErr("请输入正确进入延迟秒数！");
                    return;
                }
                if (Convert.ToUInt32(cbxOutTime.Text) < 0 || Convert.ToUInt32(cbxOutTime.Text) > 255)
                {
                    MsgErr("请输入正确退出延迟秒数！");
                    return;
                }
            }
            if (!Regex.IsMatch(cbxAlarmTime.Text.Trim(), reg) || cbxAlarmTime.Text == "")
            {
                MsgErr("请输入正确报警时长秒数！");
                return;
            }
            if (Regex.IsMatch(cbxAlarmTime.Text.Trim(), reg))
            {
                if (!UInt16.TryParse(cbxAlarmTime.Text, out ui32))
                {
                    MsgErr("请输入正确进入延迟秒数！");
                    return;
                }
                if (Convert.ToUInt32(cbxAlarmTime.Text) < 0 || Convert.ToUInt32(cbxAlarmTime.Text) > 65535)
                {
                    MsgErr("请输入正确报警时长秒数！");
                    return;
                }
            }

            if (!Regex.IsMatch(txtBeginPassword.Text.Trim(), reg))
            {
                MsgErr("布防密码必须为数字！");
                return;
            }
            if (txtBeginPassword.Text.Trim().Length > 8)
            {
                MsgErr("布防密码不能超过8位数字！");
                return;
            }
            if (!Regex.IsMatch(txtClosePassword.Text.Trim(), reg))
            {
                MsgErr("撤防密码必须为数字！");
                return;
            }
            if (txtClosePassword.Text.Trim().Length > 8)
            {
                MsgErr("撤防密码不能超过8位数字！");
                return;
            }

            TheftAlarmSetting ts = new TheftAlarmSetting();
            ts.Use = rBtnTheft.Checked;
            ts.InTime = Convert.ToUInt16(cbxInTime.Text);
            ts.OutTime = Convert.ToUInt16(cbxOutTime.Text);
            ts.AlarmTime = Convert.ToUInt16(cbxAlarmTime.Text);
            ts.BeginPassword = txtBeginPassword.Text;
            ts.ClosePassword = txtClosePassword.Text;

            //WriteTheftAlarmSetting_Parameter wp = new WriteTheftAlarmSetting_Parameter();
            //wp.Setting = ts;
            //var buf = DotNetty.Buffers.UnpooledByteBufferAllocator.Default.Buffer(0x02);
            //wp.GetBytes(buf);

            //WriteTheftAlarmSetting_Parameter wp2 = new WriteTheftAlarmSetting_Parameter();
            //wp2.SetBytes(buf);

            //string TheftAlarmSettingInfo = "功能开关：" + wp2.Setting.Use +
            //                                "  进入延迟：" + wp2.Setting.InTime + "秒" +
            //                                "  退出延迟：" + wp2.Setting.OutTime + "秒" +
            //                                "  报警时长：" + wp2.Setting.AlarmTime + "秒" +
            //                                "  布防密码：" + wp2.Setting.BeginPassword +
            //                                "  撤防密码：" + wp2.Setting.ClosePassword;
            //mMainForm.AddCmdLog(null, TheftAlarmSettingInfo);


            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            WriteTheftAlarmSetting cmd = new WriteTheftAlarmSetting(cmdDtl, new WriteTheftAlarmSetting_Parameter(ts));
            mMainForm.AddCommand(cmd);
        }
        private void BtnSetTheftFortify_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            SetTheftFortify cmd = new SetTheftFortify(cmdDtl);
            mMainForm.AddCommand(cmd);
            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                mMainForm.AddCmdLog(cmde, "防盗报警布防");
            };
        }

        private void BtnSetTheftDisarming_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            SetTheftDisarming cmd = new SetTheftDisarming(cmdDtl);
            mMainForm.AddCommand(cmd);
            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                mMainForm.AddCmdLog(cmde, "防盗报警撤防");
            };
        }
        #endregion

        #region 设置防潜回模式
        private void BtnReadCheckInOut_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadCheckInOut cmd = new ReadCheckInOut(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadCheckInOut_Result result = cmde.Command.getResult() as ReadCheckInOut_Result;
                string ModeStr = result.Mode == 1 ? "【1、单独每个门检测防潜回】" : "【2、整个控制器统一防潜回】"; //防潜回模式
                Invoke(() =>
                {
                    if (result.Mode == 1)
                    {
                        rBtnOneCheck.Checked = true;
                    }
                    else
                    {
                        rBtnAllCheck.Checked = true;
                    }
                });
                ModeStr = "防潜回检测模式：" + ModeStr;
                mMainForm.AddCmdLog(cmde, ModeStr);
            };
        }

        private void BtnWriteCheckInOut_Click(object sender, EventArgs e)
        {
            byte Mode = 1;
            if (rBtnAllCheck.Checked == true)
            {
                Mode = 2;
            }

            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            WriteCheckInOut cmd = new WriteCheckInOut(cmdDtl, new WriteCheckInOut_Parameter(Mode));
            mMainForm.AddCommand(cmd);
        }
        #endregion

        #region 卡片到期提示
        private void BtnReadCardPeriodSpeak_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadCardPeriodSpeak cmd = new ReadCardPeriodSpeak(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadCardPeriodSpeak_Result result = cmde.Command.getResult() as ReadCardPeriodSpeak_Result;
                string ModeStr = result.Use == 0 ? "【0、不启用】" : "【1、启用】"; //卡片到期提示是否启用
                Invoke(() =>
                {
                    if (result.Use == 0)
                    {
                        rBtnNoPeriodSpeak.Checked = true;
                    }
                    else
                    {
                        rBtnPeriodSpeak.Checked = true;
                    }
                });
                ModeStr = "卡片到期提示：" + ModeStr;
                mMainForm.AddCmdLog(cmde, ModeStr);
            };
        }

        private void BtnWriteCardPeriodSpeak_Click(object sender, EventArgs e)
        {
            byte buzzer = 0;
            if (rBtnPeriodSpeak.Checked == true)
            {
                buzzer = 1;
            }

            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            WriteCardPeriodSpeak cmd = new WriteCardPeriodSpeak(cmdDtl, new WriteCardPeriodSpeak_Parameter(buzzer));
            mMainForm.AddCommand(cmd);
        }
        #endregion

        #region 定时读卡播报语音消息参数
        private void BtnReadReadCardSpeak_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadReadCardSpeak cmd = new ReadReadCardSpeak(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadReadCardSpeak_Result result = cmde.Command.getResult() as ReadReadCardSpeak_Result;
                string UseStr = result.SpeakSetting.Use ? "【1、启用】" : "【0、不启用】"; //定时读卡播报语音消息功能是否启用
                string MsgIndexStr = result.SpeakSetting.MsgIndex == 1 ? "【1、交房租】" : "【2、交管理费】"; //消息编号类型
                string STime = result.SpeakSetting.BeginDate.ToString("yyyy-MM-dd HH时");
                string ETime = result.SpeakSetting.EndDate.ToString("yyyy-MM-dd HH时");
                Invoke(() =>
                {
                    if (result.SpeakSetting.Use)
                    {
                        rBtnEnableReadCardSpeak.Checked = true;
                    }
                    else
                    {
                        rBtnNoEnableReadCardSpeak.Checked = true;
                    }
                    if (result.SpeakSetting.MsgIndex == 1)
                    {
                        rBtnPayRent.Checked = true;
                    }
                    else
                    {
                        rBtnPayManagementFee.Checked = true;
                    }
                    txtSTime.Text = STime;
                    txtETime.Text = ETime;
                });
                UseStr = "功能开关：" + UseStr +
                         "  消息编号类型：" + MsgIndexStr +
                         "  起始时段：" + STime +
                         "  功能开关：" + ETime;
                mMainForm.AddCmdLog(cmde, UseStr);
            };
        }

        private void BtnWriteReadCardSpeak_Click(object sender, EventArgs e)
        {
            string reg = @"^\d{4}-\d{2}-\d{2} \d{2}时";
            if (!Regex.IsMatch(txtSTime.Text.Trim(), reg) || txtSTime.Text == "")
            {
                MsgErr("请输入正确起始时段！");
                return;
            }
            if (Convert.ToInt16(txtSTime.Text.Substring(0, 4)) > 2099
                || Convert.ToInt16(txtSTime.Text.Substring(5, 2)) > 12
                || Convert.ToInt16(txtSTime.Text.Substring(8, 2)) > 31
                || Convert.ToInt16(txtSTime.Text.Substring(11, 2)) > 23)
            {
                MsgErr("请输入正确起始时段！");
                return;
            }
            if (!Regex.IsMatch(txtETime.Text.Trim(), reg) || txtETime.Text == "")
            {
                MsgErr("请输入正确结束时段！");
                return;
            }
            if (Convert.ToInt16(txtETime.Text.Substring(0, 4)) > 2099
                || Convert.ToInt16(txtETime.Text.Substring(5, 2)) > 12
                || Convert.ToInt16(txtETime.Text.Substring(8, 2)) > 31
                || Convert.ToInt16(txtETime.Text.Substring(11, 2)) > 23)
            {
                MsgErr("请输入正确结束时段！");
                return;
            }
            if (Convert.ToInt16(txtSTime.Text.Substring(0, 4)) > Convert.ToInt16(txtETime.Text.Substring(0, 4)))
            {
                MsgErr("请输入正确时段范围！");
                return;
            }
            else if (Convert.ToInt16(txtSTime.Text.Substring(0, 4)) == Convert.ToInt16(txtETime.Text.Substring(0, 4)))
            {
                if (Convert.ToInt16(txtSTime.Text.Substring(5, 2)) > Convert.ToInt16(txtETime.Text.Substring(5, 2)))
                {
                    MsgErr("请输入正确时段范围！");
                    return;
                }
                else if (Convert.ToInt16(txtSTime.Text.Substring(5, 2)) == Convert.ToInt16(txtETime.Text.Substring(5, 2)))
                {
                    if (Convert.ToInt16(txtSTime.Text.Substring(8, 2)) > Convert.ToInt16(txtETime.Text.Substring(8, 2)))
                    {
                        MsgErr("请输入正确时段范围！");
                        return;
                    }
                    else if (Convert.ToInt16(txtSTime.Text.Substring(8, 2)) == Convert.ToInt16(txtETime.Text.Substring(8, 2)))
                    {
                        if (Convert.ToInt16(txtSTime.Text.Substring(11, 2)) > Convert.ToInt16(txtETime.Text.Substring(11, 2)))
                        {
                            MsgErr("请输入正确时段范围！");
                            return;
                        }
                    }
                }
            }

            int MsgIndex = 1;
            if (rBtnPayManagementFee.Checked == true)
            {
                MsgIndex = 2;
            }

            ReadCardSpeak rcs = new ReadCardSpeak();
            rcs.Use = rBtnEnableReadCardSpeak.Checked;
            rcs.MsgIndex = MsgIndex;
            rcs.BeginDate = Convert.ToDateTime(txtSTime.Text.Replace("时", "") + ":00:00");
            rcs.EndDate = Convert.ToDateTime(txtETime.Text.Replace("时", "") + ":00:00");

            //WriteReadCardSpeak_Parameter wp = new WriteReadCardSpeak_Parameter();
            //wp.SpeakSetting = rcs;
            //var buf = DotNetty.Buffers.UnpooledByteBufferAllocator.Default.Buffer(0x0A);
            //wp.GetBytes(buf);

            //WriteReadCardSpeak_Parameter wp2 = new WriteReadCardSpeak_Parameter();
            //wp2.SetBytes(buf);

            //string ReadCardSpeakInfo = "功能开关：" + wp2.SpeakSetting.Use +
            //                                 "  消息编号类型：" + wp2.SpeakSetting.MsgIndex +
            //                                 "  起始时段：" + wp2.SpeakSetting.BeginDate +
            //                                 "  功能开关：" + wp2.SpeakSetting.EndDate;
            //mMainForm.AddCmdLog(null, ReadCardSpeakInfo);

            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            WriteReadCardSpeak cmd = new WriteReadCardSpeak(cmdDtl, new WriteReadCardSpeak_Parameter(rcs));
            mMainForm.AddCommand(cmd);
        }
        #endregion

        #region 读取所有功能参数
        private void BtnAllRead_Click(object sender, EventArgs e)
        {
            BtnReadRecordMode_Click(sender, e); //记录存储方式
            BtnReadKeyboard_Click(sender, e); //读卡器密码键盘启用功能开关
            BtnReadLockInteraction_Click(sender, e); //互锁参数
            BtnReadFireAlarmOption_Click(sender, e); //消防报警
            BtnReadOpenAlarmOption_Click(sender, e); //匪警报警
            BtnReadIntervalTime_Click(sender, e); //读卡间隔时间
            BtnReadBroadcast_Click(sender, e); //语音播报语音段开关
            BtnReadReaderCheckMode_Click(sender, e); //读卡器数据校验
            BtnReadBuzzer_Click(sender, e); //主板蜂鸣器
            BtnReadSmogAlarmOption_Click(sender, e); //烟雾报警
            BtnReadEnterDoorLimit_Click(sender, e); //门内人数限制
            BtnReadTheftAlarmSetting_Click(sender, e); //智能防盗主机参数
            BtnReadCheckInOut_Click(sender, e); //设置防潜回模式
            BtnReadCardPeriodSpeak_Click(sender, e); //卡片到期提示
            BtnReadReadCardSpeak_Click(sender, e); //定时读卡播报语音消息参数
        }
        #endregion

        #region 实时监控
        private void BtnBeginWatch_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            BeginWatch cmd = new BeginWatch(cmdDtl);
            mMainForm.AddCommand(cmd);
            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                mMainForm.AddCmdLog(cmde, "已开启监控");
            };
        }

        private void BtnCloseWatch_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            CloseWatch cmd = new CloseWatch(cmdDtl);
            mMainForm.AddCommand(cmd);
            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                mMainForm.AddCmdLog(cmde, "未开启监控");
            };
        }

        private void BtnReadWatchState_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadWatchState cmd = new ReadWatchState(cmdDtl);
            mMainForm.AddCommand(cmd);
            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                string ModeStr = cmd.WatchState == 0 ? "【0、未开启监控】" : "【1、已开启监控】"; //监控状态
                Invoke(() =>
                {
                    if (cmd.WatchState == 1)
                    {
                        lbWatchState.Text = "开启";
                        lbWatchState.ForeColor = Color.Green;
                    }
                    else
                    {
                        lbWatchState.Text = "未开启";
                        lbWatchState.ForeColor = Color.Red;
                    }
                });
                mMainForm.AddCmdLog(cmde, ModeStr);
            };
        }

        private void BtnBeginWatchBroadcast_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            BeginWatch_Broadcast cmd = new BeginWatch_Broadcast(cmdDtl);
            mMainForm.AddCommand(cmd);
            ////处理返回值
            //cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            //{
            //    mMainForm.AddCmdLog(cmde, "已开启监控_广播");
            //};
        }

        private void BtnCloseWatchBroadcast_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            CloseWatch_Broadcast cmd = new CloseWatch_Broadcast(cmdDtl);
            mMainForm.AddCommand(cmd);
            ////处理返回值
            //cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            //{
            //    mMainForm.AddCmdLog(cmde, "已开启监控_广播");
            //};
        }


        #endregion

        #region 解除报警 
        private void BtnCloseTypeAlarm_Click(object sender, EventArgs e)
        {
            byte Door; //需要解除报警的门
            ushort AlarmType = 0; //需要解除的报警类型
            if (Convert.ToInt16(cbxCloseAlarmDoor.SelectedIndex) == -1)
            {
                MsgErr("请选择门号！");
                return;
            }
            if (Convert.ToInt16(cbxCloseAlarmDoor.SelectedIndex) == 0)
            {
                Door = 255;
            }
            else
            {
                Door = Convert.ToByte(cbxCloseAlarmDoor.SelectedIndex);
            }

            byte[] tmpData = new byte[2];
            BitArray bit = new BitArray(tmpData);
            for (int i = 0; i < this.dgvAlarmType.Rows.Count; i++)
            {
                bit[i] = Convert.ToBoolean(this.dgvAlarmType.Rows[i].Cells[0].EditedFormattedValue);
            }
            bit.CopyTo(tmpData, 0);
            AlarmType = BitConverter.ToUInt16(tmpData, 0);

            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            CloseAlarm cmd = new CloseAlarm(cmdDtl, new CloseAlarm_Parameter(Door, AlarmType));
            mMainForm.AddCommand(cmd);
        }
        #endregion

        #region 获取设备状态信息
        private void BtnWorkStatusInfo_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadWorkStatus cmd = new ReadWorkStatus(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadWorkStatus_Result result = cmde.Command.getResult() as ReadWorkStatus_Result;
                StringBuilder DoorInfo = new StringBuilder();
                string RelayState1Str = "【0、关闭】";
                string RelayState2Str = "【1、开启】";
                string RelayState3Str = "【2、继电器异常】";
                string DoorLongOpenState1Str = "【0、常闭】";
                string DoorLongOpenState2Str = "【1、常开】";
                string DoorAlarmState1Str = "【无报警】";
                string DoorAlarmState2Str = string.Empty;
                string DoorState1Str = "【0、关】";
                string DoorState2Str = "【1、开】";
                string AlarmState1Str = "无报警";
                string AlarmState2Str = "报警";
                string LockState1Str = "【0、关闭】";
                string LockState2Str = "【1、开启】";
                string LockState3Str = "【2、双稳态】";
                string PortLockState1Str = "【0、未锁定】";
                string PortLockState2Str = "【1、锁定】";

                Invoke(() =>
                {
                    //继电器物理状态
                    for (int i = 0; i < result.RelayState.DoorMax; i++)
                    {
                        if (result.RelayState.DoorPort[i] == 0)
                        {
                            this.dgvEquipmentStatusInfo.Rows[i].Cells[7].Value = RelayState1Str;
                            DoorInfo.Append("  门" + (i + 1) + " 物理状态：" + RelayState1Str + "");
                        }
                        else if (result.RelayState.DoorPort[i] == 1)
                        {
                            this.dgvEquipmentStatusInfo.Rows[i].Cells[7].Value = RelayState2Str;
                            DoorInfo.Append("  门" + (i + 1) + " 物理状态：" + RelayState1Str + "");
                        }
                        else if (result.RelayState.DoorPort[i] == 2)
                        {
                            this.dgvEquipmentStatusInfo.Rows[i].Cells[7].Value = RelayState3Str;
                            DoorInfo.Append("  门" + (i + 1) + " 物理状态：" + RelayState1Str + "");
                        }
                    }
                    mMainForm.AddCmdLog(cmde, DoorInfo.ToString());
                    DoorInfo.Clear();
                    //运行状态
                    for (int i = 0; i < result.DoorLongOpenState.DoorMax; i++)
                    {
                        if (result.DoorLongOpenState.DoorPort[i] == 0)
                        {
                            this.dgvEquipmentStatusInfo.Rows[i].Cells[1].Value = DoorLongOpenState1Str;
                            DoorInfo.Append("  门" + (i + 1) + " 开锁模式：" + DoorLongOpenState1Str + "");
                        }
                        else if (result.DoorLongOpenState.DoorPort[i] == 1)
                        {
                            this.dgvEquipmentStatusInfo.Rows[i].Cells[1].Value = DoorLongOpenState2Str;
                            DoorInfo.Append("  门" + (i + 1) + " 开锁模式：" + DoorLongOpenState2Str + "");
                        }
                    }
                    mMainForm.AddCmdLog(cmde, DoorInfo.ToString());
                    DoorInfo.Clear();
                    //门磁开关
                    for (int i = 0; i < result.DoorState.DoorMax; i++)
                    {
                        if (result.DoorState.DoorPort[i] == 0)
                        {
                            this.dgvEquipmentStatusInfo.Rows[i].Cells[2].Value = DoorState1Str;
                            DoorInfo.Append("  门" + (i + 1) + " 门磁：" + DoorState1Str + "");
                        }
                        else if (result.DoorState.DoorPort[i] == 1)
                        {
                            this.dgvEquipmentStatusInfo.Rows[i].Cells[2].Value = DoorState2Str;
                            DoorInfo.Append("  门" + (i + 1) + " 门磁：" + DoorState2Str + "");
                        }
                    }
                    mMainForm.AddCmdLog(cmde, DoorInfo.ToString());
                    DoorInfo.Clear();
                    //门报警状态
                    for (int i = 0; i < result.DoorAlarmState.DoorMax; i++)
                    {
                        if (result.DoorAlarmState.DoorPort[i] == 0)
                        {
                            this.dgvEquipmentStatusInfo.Rows[i].Cells[6].Value = DoorAlarmState1Str;
                            DoorInfo.Append("  门" + (i + 1) + " 报警状态：" + DoorAlarmState1Str + "");
                        }
                        else
                        {
                            byte[] ByteDoorAlarmStateSet = new byte[] { result.DoorAlarmState.DoorPort[i] };
                            BitArray bitSet = new BitArray(ByteDoorAlarmStateSet);
                            for (int j = 0; j < 6; j++)
                            {
                                if (bitSet[j])
                                {
                                    if (j == 0)
                                    {
                                        DoorAlarmState2Str = DoorAlarmState2Str + "  非法刷卡报警";
                                    }
                                    else if (j == 1)
                                    {
                                        DoorAlarmState2Str = DoorAlarmState2Str + "  门磁报警";
                                    }
                                    else if (j == 2)
                                    {
                                        DoorAlarmState2Str = DoorAlarmState2Str + "  胁迫报警";
                                    }
                                    else if (j == 3)
                                    {
                                        DoorAlarmState2Str = DoorAlarmState2Str + "  开门超时报警";
                                    }
                                    else if (j == 4)
                                    {
                                        DoorAlarmState2Str = DoorAlarmState2Str + "  黑名单报警";
                                    }
                                    else if (j == 5)
                                    {
                                        DoorAlarmState2Str = DoorAlarmState2Str + "  读卡器防拆报警";
                                    }
                                }
                            }
                            this.dgvEquipmentStatusInfo.Rows[i].Cells[6].Value = DoorAlarmState2Str;
                            DoorInfo.Append("  门" + (i + 1) + " 报警状态：" + DoorAlarmState2Str + "");
                        }
                    }
                    mMainForm.AddCmdLog(cmde, DoorInfo.ToString());
                    DoorInfo.Clear();
                    //设备报警状态
                    byte[] ByteSet = new byte[] { result.AlarmState };
                    BitArray bit = new BitArray(ByteSet);
                    for (int i = 0; i < 6; i++)
                    {
                        if (i == 0)
                        {
                            if (bit[i])
                            {
                                this.dgvEquipmentStatusInfo.Rows[5].Cells[6].Value = AlarmState2Str;
                                DoorInfo.Append("  匪警报警状态：" + AlarmState2Str + "");
                            }
                            else
                            {
                                this.dgvEquipmentStatusInfo.Rows[5].Cells[6].Value = AlarmState1Str;
                                DoorInfo.Append("  匪警报警状态：" + AlarmState1Str + "");
                            }
                        }
                        if (i == 1)
                        {
                            if (bit[i])
                            {
                                this.dgvEquipmentStatusInfo.Rows[7].Cells[6].Value = AlarmState2Str;
                                DoorInfo.Append("  防盗报警状态：" + AlarmState2Str + "");
                            }
                            else
                            {
                                this.dgvEquipmentStatusInfo.Rows[7].Cells[6].Value = AlarmState1Str;
                                DoorInfo.Append("  防盗报警状态：" + AlarmState1Str + "");
                            }
                        }
                        if (i == 2)
                        {
                            if (bit[i])
                            {
                                this.dgvEquipmentStatusInfo.Rows[4].Cells[6].Value = AlarmState2Str;
                                DoorInfo.Append("  消防报警状态：" + AlarmState2Str + "");
                            }
                            else
                            {
                                this.dgvEquipmentStatusInfo.Rows[4].Cells[6].Value = AlarmState1Str;
                                DoorInfo.Append("  消防报警状态：" + AlarmState1Str + "");
                            }
                        }
                        if (i == 3)
                        {
                            if (bit[i])
                            {
                                this.dgvEquipmentStatusInfo.Rows[6].Cells[6].Value = AlarmState2Str;
                                DoorInfo.Append("  烟雾报警状态：" + AlarmState2Str + "");
                            }
                            else
                            {
                                this.dgvEquipmentStatusInfo.Rows[6].Cells[6].Value = AlarmState1Str;
                                DoorInfo.Append("  烟雾报警状态：" + AlarmState1Str + "");
                            }
                        }
                        if (i == 5)
                        {
                            if (bit[i])
                            {
                                this.dgvEquipmentStatusInfo.Rows[8].Cells[6].Value = AlarmState2Str;
                                DoorInfo.Append("  控制板防拆报警状态：" + AlarmState2Str + "");
                            }
                            else
                            {
                                this.dgvEquipmentStatusInfo.Rows[8].Cells[6].Value = AlarmState1Str;
                                DoorInfo.Append("  控制板防拆报警状态：" + AlarmState1Str + "");
                            }
                        }
                    }
                    mMainForm.AddCmdLog(cmde, DoorInfo.ToString());
                    DoorInfo.Clear();
                    //继电器逻辑状态
                    for (int i = 0; i < 8; i++)
                    {
                        if (i < 4)
                        {
                            if (result.LockState.DoorPort[i] == 0)
                            {
                                this.dgvEquipmentStatusInfo.Rows[i].Cells[3].Value = LockState1Str;
                                DoorInfo.Append("  门" + (i + 1) + " 逻辑状态：" + LockState1Str + "");
                            }
                            else if (result.LockState.DoorPort[i] == 1)
                            {
                                this.dgvEquipmentStatusInfo.Rows[i].Cells[3].Value = LockState2Str;
                                DoorInfo.Append("  门" + (i + 1) + " 逻辑状态：" + LockState2Str + "");
                            }
                            else if (result.LockState.DoorPort[i] == 2)
                            {
                                this.dgvEquipmentStatusInfo.Rows[i].Cells[3].Value = LockState3Str;
                                DoorInfo.Append("  门" + (i + 1) + " 逻辑状态：" + LockState3Str + "");
                            }
                        }
                        else if (i == 4)
                        {
                            this.dgvEquipmentStatusInfo.Rows[i].Cells[3].Value = LockState1Str;
                            DoorInfo.Append("  消防报警继电器：" + LockState1Str + "");
                        }
                        else if (i == 5)
                        {
                            this.dgvEquipmentStatusInfo.Rows[i].Cells[3].Value = LockState1Str;
                            DoorInfo.Append("  匪警报警继电器：" + LockState1Str + "");
                        }
                        else if (i == 6)
                        {
                            this.dgvEquipmentStatusInfo.Rows[i].Cells[3].Value = LockState1Str;
                            DoorInfo.Append("  烟雾报警继电器：" + LockState1Str + "");
                        }
                        else if (i == 7)
                        {
                            this.dgvEquipmentStatusInfo.Rows[i].Cells[3].Value = LockState1Str;
                            DoorInfo.Append("  防盗主机报警继电器：" + LockState1Str + "");
                        }
                    }
                    mMainForm.AddCmdLog(cmde, DoorInfo.ToString());
                    DoorInfo.Clear();
                    //锁定状态
                    for (int i = 0; i < result.PortLockState.DoorMax; i++)
                    {
                        if (result.PortLockState.DoorPort[i] == 0)
                        {
                            this.dgvEquipmentStatusInfo.Rows[i].Cells[4].Value = PortLockState1Str;
                            DoorInfo.Append("  门" + (i + 1) + " 锁定状态：" + PortLockState1Str + "");
                        }
                        else if (result.PortLockState.DoorPort[i] == 1)
                        {
                            this.dgvEquipmentStatusInfo.Rows[i].Cells[4].Value = PortLockState2Str;
                            DoorInfo.Append("  门" + (i + 1) + " 锁定状态：" + PortLockState2Str + "");
                        }
                    }
                    mMainForm.AddCmdLog(cmde, DoorInfo.ToString());
                    DoorInfo.Clear();
                    Invoke(() =>
                    {
                        if (result.WatchState == 1)
                        {
                            lbSWatchState.Text = "已开启监控";
                            lbSWatchState.ForeColor = Color.Green;
                            DoorInfo.Append("  监控状态：已开启监控");
                        }
                        else
                        {
                            lbSWatchState.Text = "未开启监控";
                            lbSWatchState.ForeColor = Color.Red;
                            DoorInfo.Append("  监控状态：未开启监控");
                        }
                    });
                    mMainForm.AddCmdLog(cmde, DoorInfo.ToString());
                    DoorInfo.Clear();
                    //门内总人数
                    this.txtAllNum.Text = result.EnterTotal.GlobalEnter.ToString();
                    DoorInfo.Append("  门内总人数：" + result.EnterTotal.DoorEnterArray[0].ToString());
                    this.dgvEquipmentStatusInfo.Rows[0].Cells[5].Value = result.EnterTotal.DoorEnterArray[0].ToString(); //门1人数
                    DoorInfo.Append("  门1 门内人数：" + result.EnterTotal.DoorEnterArray[0].ToString());
                    this.dgvEquipmentStatusInfo.Rows[1].Cells[5].Value = result.EnterTotal.DoorEnterArray[1].ToString(); //门2人数
                    DoorInfo.Append("  门2 门内人数：" + result.EnterTotal.DoorEnterArray[1].ToString());
                    this.dgvEquipmentStatusInfo.Rows[2].Cells[5].Value = result.EnterTotal.DoorEnterArray[2].ToString(); //门3人数
                    DoorInfo.Append("  门3 门内人数：" + result.EnterTotal.DoorEnterArray[2].ToString());
                    this.dgvEquipmentStatusInfo.Rows[3].Cells[5].Value = result.EnterTotal.DoorEnterArray[3].ToString(); //门4人数
                    DoorInfo.Append("  门4 门内人数：" + result.EnterTotal.DoorEnterArray[3].ToString());
                    mMainForm.AddCmdLog(cmde, DoorInfo.ToString());
                    DoorInfo.Clear();
                    //防盗主机布防状态
                    string TheftStateStr = string.Empty;
                    if (result.TheftState == 1)
                    {
                        TheftStateStr = "延时布防";
                    }
                    else if (result.TheftState == 2)
                    {
                        TheftStateStr = "已布防";
                    }
                    else if (result.TheftState == 3)
                    {
                        TheftStateStr = "延时撤防";
                    }
                    else if (result.TheftState == 4)
                    {
                        TheftStateStr = "未布防";
                    }
                    else if (result.TheftState == 5)
                    {
                        TheftStateStr = "报警延时，准备启用报警";
                    }
                    else if (result.TheftState == 6)
                    {
                        TheftStateStr = "防盗报警已启动";
                    }
                    DoorInfo.Append("  防盗主机布防状态：" + TheftStateStr);
                    mMainForm.AddCmdLog(cmde, DoorInfo.ToString());
                    DoorInfo.Clear();
                });
            };
        }
        #endregion

        #region 防盗主机布防状态
        private void BtnTheftAlarmSettingState_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadTheftAlarmState cmd = new ReadTheftAlarmState(cmdDtl);
            mMainForm.AddCommand(cmd);
            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadTheftAlarmState_Result result = cmde.Command.getResult() as ReadTheftAlarmState_Result;
                StringBuilder TheftStateInfo = new StringBuilder();
                TheftStateInfo.Append("防盗主机布防状态：");
                string TheftStateStr = string.Empty;
                Invoke(() =>
                {
                    if (result.TheftState == 1)
                    {
                        TheftStateStr = "延时布防";
                    }
                    else if (result.TheftState == 2)
                    {
                        TheftStateStr = "已布防";
                    }
                    else if (result.TheftState == 3)
                    {
                        TheftStateStr = "延时撤防";
                    }
                    else if (result.TheftState == 4)
                    {
                        TheftStateStr = "未布防";
                    }
                    else if (result.TheftState == 5)
                    {
                        TheftStateStr = "报警延时，准备启用报警";
                    }
                    else if (result.TheftState == 6)
                    {
                        TheftStateStr = "防盗报警已启动";
                    }
                    txtBeginState.Text = TheftStateStr;
                    TheftStateInfo.Append(TheftStateStr);
                    mMainForm.AddCmdLog(cmde, TheftStateInfo.ToString());
                });
            };
        }
        #endregion

        #region 初始化设备
        private void BtnInitalData_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            FormatController cmd = new FormatController(cmdDtl);
            mMainForm.AddCommand(cmd);
            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                mMainForm.AddCmdLog(cmde, "初始化数据完成！");
            };
        }
        #endregion

        #region 搜索设备
        private Random mRandom = new Random();

        /// <summary>
        /// 获取一个随机数
        /// </summary>
        /// <param name="iMin">下限</param>
        /// <param name="iMax">上限</param>
        /// <returns></returns>
        private int GetRandomNum(int iMin, int iMax)
        {
            var rnd = mRandom.NextDouble();
            return iMin + (int)(rnd * (iMax - iMin + 1));
        }

        private void BtnSearchEquptOnNetNum_Click(object sender, EventArgs e)
        {

            ushort NetCode = (ushort)GetRandomNum(100, 60000);
            var cmdDtl = mMainForm.GetCommandDetail();
            cmdDtl.Timeout = 4000;
            cmdDtl.RestartCount = 0;
            int ReSend = 0;
            if (cmdDtl == null) return;

            SearchControltor_Parameter par = new SearchControltor_Parameter(NetCode);
            SearchControltor cmd = new SearchControltor(cmdDtl, par);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                //搜索控制器
                SearchControltor_Result result = cmde.Command.getResult() as SearchControltor_Result;
                string log = "SN:" + result.SN + "," + DebugTCPDetail(result.TCP);

                mMainForm.AddCmdLog(cmde, log);

                //修改网络标识
                Invoke(() =>
                {
                    var tmpCmdDtl = mMainForm.GetCommandDetail();
                    tmpCmdDtl.Timeout = 2000;
                    OnlineAccessCommandDetail detail = tmpCmdDtl as OnlineAccessCommandDetail;
                    detail.SN = result.SN;
                    WriteControltorNetCode writeCmd = new WriteControltorNetCode(tmpCmdDtl, par);
                    mMainForm.AddCommand(writeCmd);
                });

            };
            //超时
            cmdDtl.CommandTimeout += (sdr1, cmde1) =>
             {
                 ReSend += 1;
                 if(ReSend < 3)
                 {
                     SearchControltor recmd = new SearchControltor(cmde1.CommandDetail, par);
                     mMainForm.AddCommand(recmd);
                 }
                 
             };


            mMainForm.AddCommand(cmd);
        }
        #endregion

        #region 缓存区操作
        private void BtnReadCacheContent_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadCacheContent cmd = new ReadCacheContent(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                CacheContent_Result result = cmde.Command.getResult() as CacheContent_Result;
                string CacheContent = result.CacheContent;
                Invoke(() =>
                {
                    txtCacheContent.Text = CacheContent;
                });
                CacheContent = "缓存区内容：" + CacheContent;
                mMainForm.AddCmdLog(cmde, CacheContent);
            };
        }

        private void BtnWriteCacheContent_Click(object sender, EventArgs e)
        {
            string reg = @"^\+?[0-9]*$";
            if (!Regex.IsMatch(txtCacheContent.Text.Trim(), reg) || txtCacheContent.Text.Trim().Length > 30 || string.IsNullOrEmpty(txtCacheContent.Text.Trim()))
            {
                MsgErr("请输入正确缓存区内容！");
                return;
            }
            string cacheContent = txtCacheContent.Text.Trim();

            //CacheContent_Parameter wp = new CacheContent_Parameter();
            //wp.CacheContent = cacheContent;
            //var buf = DotNetty.Buffers.UnpooledByteBufferAllocator.Default.Buffer(0x1E);
            //wp.GetBytes(buf);

            //CacheContent_Parameter wp2 = new CacheContent_Parameter();
            //wp2.SetBytes(buf);

            //cacheContent = "缓存区内容：" + cacheContent;
            //mMainForm.AddCmdLog(null, cacheContent);

            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            WriteCacheContent cmd = new WriteCacheContent(cmdDtl, new CacheContent_Parameter(cacheContent));
            mMainForm.AddCommand(cmd);
        }
        #endregion

        #region 客户端控制器保活间隔
        private void BtnReadKeepAliveInterval_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadKeepAliveInterval cmd = new ReadKeepAliveInterval(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadKeepAliveInterval_Result result = cmde.Command.getResult() as ReadKeepAliveInterval_Result;

                ushort IntervalTime = result.IntervalTime; //保活间隔时间
                string IntervalTimeInfo = string.Empty;
                if (IntervalTime == 0)
                {
                    IntervalTimeInfo = "禁用";
                }
                else
                {
                    IntervalTimeInfo = IntervalTime.ToString() + "秒";
                }

                Invoke(() =>
                {
                    cbxKeepAliveInterval.Text = IntervalTimeInfo.Replace("秒", "");
                });
                string IntervalTimeStr = "与服务器建立连接后，每隔：" + IntervalTimeInfo + "，发送一次保活包";
                mMainForm.AddCmdLog(cmde, IntervalTimeStr);
            };
        }

        private void BtnWriteKeepAliveInterval_Click(object sender, EventArgs e)
        {
            string reg = @"^\+?[0-9]*$";
            if (!Regex.IsMatch(cbxKeepAliveInterval.Text.Trim(), reg))
            {
                if (cbxKeepAliveInterval.Text != "禁用")
                {
                    MsgErr("请输入正确保活间隔时间！");
                    return;
                }
            }
            if (Regex.IsMatch(cbxKeepAliveInterval.Text.Trim(), reg))
            {
                if (Convert.ToUInt32(cbxKeepAliveInterval.Text) < 0 || Convert.ToUInt32(cbxKeepAliveInterval.Text) > 65535)
                {
                    MsgErr("请输入正确保活间隔时间！");
                    return;
                }
            }

            ushort IntervalTime = 0;
            string deadlineInfo = cbxKeepAliveInterval.Text;
            if (deadlineInfo == "禁用")
            {
                IntervalTime = 0;
            }
            else
            {
                IntervalTime = Convert.ToUInt16(cbxKeepAliveInterval.Text);
            }

            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            WriteKeepAliveInterval cmd = new WriteKeepAliveInterval(cmdDtl, new WriteKeepAliveInterval_Parameter(IntervalTime));
            mMainForm.AddCommand(cmd);
        }
        #endregion

        #region 黑名单报警
        private void BtnReadBalcklistAlarmOption_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadBalcklistAlarmOption cmd = new ReadBalcklistAlarmOption(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadBalcklistAlarmOption_Result result = cmde.Command.getResult() as ReadBalcklistAlarmOption_Result;
                string ModeStr = result.Use == 0 ? "【0、不启用】" : "【1、启用】"; //黑名单报警功能是否启用
                Invoke(() =>
                {
                    if (result.Use == 0)
                    {
                        rBtnNoBalcklistAlarm.Checked = true;
                    }
                    else
                    {
                        rBtnBalcklistAlarm.Checked = true;
                    }
                });
                ModeStr = "黑名单报警：" + ModeStr;
                mMainForm.AddCmdLog(cmde, ModeStr);
            };
        }

        private void BtnWriteBalcklistAlarmOption_Click(object sender, EventArgs e)
        {
            byte use = 0;
            if (rBtnBalcklistAlarm.Checked == true)
            {
                use = 1;
            }

            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            WriteBalcklistAlarmOption cmd = new WriteBalcklistAlarmOption(cmdDtl, new WriteBalcklistAlarmOption_Parameter(use));
            mMainForm.AddCommand(cmd);
        }
        #endregion

        #region 防探测功能
        private void BtnReadExploreLockMode_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadExploreLockMode cmd = new ReadExploreLockMode(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadExploreLockMode_Result result = cmde.Command.getResult() as ReadExploreLockMode_Result;
                string ModeStr = result.Use == 0 ? "【0、不启用】" : "【1、启用】"; //防探测功能是否启用
                Invoke(() =>
                {
                    if (result.Use == 0)
                    {
                        rBtnNoExploreLockMode.Checked = true;
                    }
                    else
                    {
                        rBtnExploreLockMode.Checked = true;
                    }
                });
                ModeStr = "防探测功能：" + ModeStr;
                mMainForm.AddCmdLog(cmde, ModeStr);
            };
        }

        private void BtnWriteExploreLockMode_Click(object sender, EventArgs e)
        {
            byte use = 0;
            if (rBtnExploreLockMode.Checked == true)
            {
                use = 1;
            }

            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            WriteExploreLockMode cmd = new WriteExploreLockMode(cmdDtl, new WriteExploreLockMode_Parameter(use));
            mMainForm.AddCommand(cmd);
        }
        #endregion

        #region 485线路反接检测开关
        private void BtnReadCheck485Line_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadCheck485Line cmd = new ReadCheck485Line(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadCheck485Line_Result result = cmde.Command.getResult() as ReadCheck485Line_Result;
                string ModeStr = result.Use == 0 ? "【0、不启用】" : "【1、启用】"; //485线路反接检测开关是否启用
                Invoke(() =>
                {
                    if (result.Use == 0)
                    {
                        rBtnNoCheck485Line.Checked = true;
                    }
                    else
                    {
                        rBtnCheck485Line.Checked = true;
                    }
                });
                ModeStr = "485线路反接检测开关：" + ModeStr;
                mMainForm.AddCmdLog(cmde, ModeStr);
            };
        }

        private void BtnWriteCheck485Line_Click(object sender, EventArgs e)
        {
            byte use = 0;
            if (rBtnCheck485Line.Checked == true)
            {
                use = 1;
            }

            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            WriteCheck485Line cmd = new WriteCheck485Line(cmdDtl, new WriteCheck485Line_Parameter(use));
            mMainForm.AddCommand(cmd);
        }
        #endregion

        #region TCP客户端操作
        private void BtnReadTCPClientList_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadTCPClientList cmd = new ReadTCPClientList(cmdDtl);
            mMainForm.AddCommand(cmd);
            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                Invoke(() =>
                {
                    TCPClient_Result result = cmde.Command.getResult() as TCPClient_Result;
                    this.dgvTCPClientList.AllowUserToAddRows = false;
                    this.dgvTCPClientList.Rows.Clear();
                    if (result.tCPClientDetail.TCPClientNum > 0)
                    {
                        this.dgvTCPClientList.Rows.Add(result.tCPClientDetail.TCPClientNum);
                    }
                    for (int i = 0; i < result.tCPClientDetail.TCPClientNum; i++)
                    {
                        this.dgvTCPClientList.Rows[i].Cells[1].Value = i + 1;
                        this.dgvTCPClientList.Rows[i].Cells[2].Value = result.tCPClientDetail.IP[i];
                        this.dgvTCPClientList.Rows[i].Cells[3].Value = result.tCPClientDetail.TCPPort[i];
                        if (result.tCPClientDetail.ConnectTime[i].Year != 1)
                        {
                            this.dgvTCPClientList.Rows[i].Cells[4].Value = result.tCPClientDetail.ConnectTime[i];
                        }
                    }
                    mMainForm.AddCmdLog(cmde, "已连接客户端数量：" + result.tCPClientDetail.TCPClientNum);
                });
            };
        }

        private void BtnStopTCPClientConnection_Click(object sender, EventArgs e)
        {
            TCPClientDetail tCPClientDetail = new TCPClientDetail();
            if (this.dgvTCPClientList.Rows.Count > 0)
            {
                tCPClientDetail.IP = new string[this.dgvTCPClientList.Rows.Count];
                tCPClientDetail.TCPPort = new ushort[this.dgvTCPClientList.Rows.Count];
                for (int i = 0; i < this.dgvTCPClientList.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(this.dgvTCPClientList.Rows[i].Cells[0].EditedFormattedValue))
                    {
                        tCPClientDetail.IP[0] = this.dgvTCPClientList.Rows[i].Cells[2].Value.ToString();
                        tCPClientDetail.TCPPort[0] = Convert.ToUInt16(this.dgvTCPClientList.Rows[i].Cells[3].Value);
                        break;
                    }
                }
                if (string.IsNullOrEmpty(tCPClientDetail.IP[0])) //没有选择，默认选择列表第一条数据进行断开连接
                {
                    tCPClientDetail.IP[0] = this.dgvTCPClientList.Rows[0].Cells[2].Value.ToString();
                    tCPClientDetail.TCPPort[0] = Convert.ToUInt16(this.dgvTCPClientList.Rows[0].Cells[3].Value);
                }
            }
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            StopTCPClientConnection cmd = new StopTCPClientConnection(cmdDtl, new TCPClient_Parameter(tCPClientDetail));
            mMainForm.AddCommand(cmd);

            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                Invoke(() =>
                {
                    string TCPClientInfo = "IP：" + tCPClientDetail.IP[0] +
                                           " 端口：" + tCPClientDetail.TCPPort[0];
                    mMainForm.AddCmdLog(cmde, TCPClientInfo);
                });
            };
        }

        private void BtnStopAllTCPClientConnection_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            //cmdDtl.Timeout = 5000;
            if (cmdDtl == null) return;
            StopAllTCPClientConnection cmd = new StopAllTCPClientConnection(cmdDtl);
            mMainForm.AddCommand(cmd);
            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                mMainForm.AddCmdLog(cmde, "停止所有连接成功！");
            };
        }
        #endregion

        #region 有效期即将过期提醒时间
        private void BtnReadCardDeadlineTipDay_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadCardDeadlineTipDay cmd = new ReadCardDeadlineTipDay(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadCardDeadlineTipDay_Result result = cmde.Command.getResult() as ReadCardDeadlineTipDay_Result;
                string TipDayInfo = string.Empty;
                Invoke(() =>
                {
                    cbxCardDeadlineTipDay.Text = result.Day == 0 ? "禁用" : ""+ result.Day + "";
                });
                if (result.Day == 0)
                {
                    TipDayInfo = "禁用提前提醒卡片即将过期";
                }
                else
                {
                    TipDayInfo = "卡片有效期即将过期时，刷卡时提前：" + result.Day + "天提醒";
                }
                mMainForm.AddCmdLog(cmde, TipDayInfo);
            };
        }

        private void BtnWriteCardDeadlineTipDay_Click(object sender, EventArgs e)
        {
            string reg = @"^\+?[0-9]*$";
            if (!Regex.IsMatch(cbxCardDeadlineTipDay.Text.Trim(), reg) || string.IsNullOrEmpty(cbxCardDeadlineTipDay.Text.Trim()))
            {
                if (cbxCardDeadlineTipDay.Text != "禁用")
                {
                    MsgErr("请输入正确提前天数！");
                    return;
                }
            }
            if (Regex.IsMatch(cbxCardDeadlineTipDay.Text.Trim(), reg))
            {
                if (Convert.ToUInt32(cbxCardDeadlineTipDay.Text) < 0 || Convert.ToUInt32(cbxCardDeadlineTipDay.Text) > 255)
                {
                    MsgErr("请输入正确提前天数！");
                    return;
                }
            }

            byte tipDay = 0;
            string tipDayInfo = cbxCardDeadlineTipDay.Text;
            if (tipDayInfo == "禁用")
            {
                tipDay = 0;
            }
            else
            {
                tipDay = Convert.ToByte(tipDayInfo);
            }

            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            WriteCardDeadlineTipDay cmd = new WriteCardDeadlineTipDay(cmdDtl, new WriteCardDeadlineTipDay_Parameter(tipDay));
            mMainForm.AddCommand(cmd);
        }
        #endregion

        #region 控制板防拆报警功能
        private void BtnReadrBtnControlPanelTamperAlarm_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadControlPanelTamperAlarm cmd = new ReadControlPanelTamperAlarm(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadControlPanelTamperAlarm_Result result = cmde.Command.getResult() as ReadControlPanelTamperAlarm_Result;
                string ModeStr = result.Use == 0 ? "【0、不启用】" : "【1、启用】"; //控制板防拆报警开关是否启用
                Invoke(() =>
                {
                    if (result.Use == 0)
                    {
                        rBtnNoControlPanelTamperAlarm.Checked = true;
                    }
                    else
                    {
                        rBtnControlPanelTamperAlarm.Checked = true;
                    }
                });
                mMainForm.AddCmdLog(cmde, ModeStr);
            };
        }

        private void BtnWriterBtnControlPanelTamperAlarm_Click(object sender, EventArgs e)
        {
            byte use = 0;
            if (rBtnControlPanelTamperAlarm.Checked == true)
            {
                use = 1;
            }

            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            WriteControlPanelTamperAlarm cmd = new WriteControlPanelTamperAlarm(cmdDtl, new WriteControlPanelTamperAlarm_Parameter(use));
            mMainForm.AddCommand(cmd);
        }
        #endregion

        #region HTTP网页登陆开关
        private void BtnReadHTTPPageLandingSwitch_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadHTTPPageLandingSwitch cmd = new ReadHTTPPageLandingSwitch(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadHTTPPageLandingSwitch_Result result = cmde.Command.getResult() as ReadHTTPPageLandingSwitch_Result;
                string ModeStr = result.Use == 0 ? "【0、不启用】" : "【1、启用】"; //HTTP网页登陆开关是否启用
                Invoke(() =>
                {
                    if (result.Use == 0)
                    {
                        rBtnNoHTTPPageLandingSwitch.Checked = true;
                    }
                    else
                    {
                        rBtnHTTPPageLandingSwitch.Checked = true;
                    }
                });
                mMainForm.AddCmdLog(cmde, ModeStr);
            };
        }

        private void BtnWriteHTTPPageLandingSwitch_Click(object sender, EventArgs e)
        {
            byte use = 0;
            if (rBtnHTTPPageLandingSwitch.Checked == true)
            {
                use = 1;
            }

            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            WriteHTTPPageLandingSwitch cmd = new WriteHTTPPageLandingSwitch(cmdDtl, new WriteHTTPPageLandingSwitch_Parameter(use));
            mMainForm.AddCommand(cmd);
        }
        #endregion

        #region 开门超时报警时，合法卡解除报警开关
        private void BtnReadLawfulCardReleaseAlarmSwitch_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadLawfulCardReleaseAlarmSwitch cmd = new ReadLawfulCardReleaseAlarmSwitch(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadLawfulCardReleaseAlarmSwitch_Result result = cmde.Command.getResult() as ReadLawfulCardReleaseAlarmSwitch_Result;
                string ModeStr = result.Use == 0 ? "【0、不启用】" : "【1、启用】"; //合法卡解除报警开关是否启用
                Invoke(() =>
                {
                    if (result.Use == 0)
                    {
                        rBtnNoLawfulCardReleaseAlarmSwitch.Checked = true;
                    }
                    else
                    {
                        rBtnLawfulCardReleaseAlarmSwitch.Checked = true;
                    }
                });
                mMainForm.AddCmdLog(cmde, ModeStr);
            };
        }

        private void BtnWriteLawfulCardReleaseAlarmSwitch_Click(object sender, EventArgs e)
        {
            byte use = 0;
            if (rBtnLawfulCardReleaseAlarmSwitch.Checked == true)
            {
                use = 1;
            }

            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            WriteLawfulCardReleaseAlarmSwitch cmd = new WriteLawfulCardReleaseAlarmSwitch(cmdDtl, new WriteLawfulCardReleaseAlarmSwitch_Parameter(use));
            mMainForm.AddCommand(cmd);
        }
        #endregion
    }
}
