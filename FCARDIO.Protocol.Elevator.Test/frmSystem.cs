using FCARDIO.Protocol.Elevator.FC8864.SystemParameter.SN;
using FCARDIO.Core.Extension;
using System;
using System.Windows.Forms;
using FCARDIO.Protocol.Elevator.FC8864.SystemParameter.ConnectPassword;
using FCARDIO.Protocol.Elevator.FC8864.SystemParameter.Deadline;
using System.Text.RegularExpressions;
using FCARDIO.Protocol.Elevator.FC8864.SystemParameter.Version;
using FCARDIO.Protocol.Elevator.FC8864.SystemParameter.SystemStatus;
using FCARDIO.Protocol.Elevator.FC8864.SystemParameter.TCPSetting;

namespace FCARDIO.Protocol.Elevator.Test
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

        public frmSystem()
        {
            InitializeComponent();
        }

        private void FrmSystem_Load(object sender, EventArgs e)
        {

        }

        private void FrmSystem_FormClosed(object sender, FormClosedEventArgs e)
        {
            onlyObj = null;
        }

        private void ButReadSN_Click(object sender, EventArgs e)
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

        private void ButReadConnectPassword_Click(object sender, EventArgs e)
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

        private void ButWriteConnectPassword_Click(object sender, EventArgs e)
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

        private void ButResetConnectPassword_Click(object sender, EventArgs e)
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
            if (!Regex.IsMatch(txtServerAddr.Text.Trim(), reg4))
            {
                MsgErr("请输入正确服务器域名！");
                return;
            }
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
    }
}
