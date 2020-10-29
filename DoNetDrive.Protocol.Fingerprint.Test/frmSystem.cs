﻿using DoNetDrive.Protocol.Door.Door8800.SystemParameter.SN;
using System;
using DoNetDrive.Core.Extension;
using System.Windows.Forms;
using DoNetDrive.Protocol.Door.Door8800.SystemParameter.ConnectPassword;
using DoNetDrive.Protocol.Door.Door8800.SystemParameter.TCPSetting;
using System.Text.RegularExpressions;
using DoNetDrive.Protocol.Fingerprint.SystemParameter.Version;
using DoNetDrive.Protocol.Fingerprint.SystemParameter.SystemStatus;
using DoNetDrive.Protocol.Door.Door8800.SystemParameter.FunctionParameter;
using DoNetDrive.Protocol.Fingerprint.SystemParameter.Watch;
using DoNetDrive.Protocol.Fingerprint.Alarm;
using System.Drawing;
using DoNetDrive.Protocol.Door.Door8800.SystemParameter.CacheContent;
using DoNetDrive.Protocol.Door.Door8800.SystemParameter.KeepAliveInterval;
using DoNetDrive.Protocol.Fingerprint.SystemParameter.DataEncryptionSwitch;
//using DoNetDrive.Protocol.Fingerprint.SystemParameter.LocalIdentity;
using DoNetDrive.Protocol.Fingerprint.SystemParameter.WiegandOutput;
//using DoNetDrive.Protocol.Fingerprint.SystemParameter.ComparisonThreshold;
using DoNetDrive.Protocol.Fingerprint.SystemParameter.ScreenDisplayContent;
using DoNetDrive.Protocol.Fingerprint.SystemParameter.ManageMenuPassword;
using DoNetDrive.Protocol.Fingerprint.SystemParameter.OEM;
using DoNetDrive.Protocol.Fingerprint.SystemParameter;
using System.Text;

namespace DoNetDrive.Protocol.Fingerprint.Test
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


        private void FrmSystem_Load(object sender, EventArgs e)
        {
            LoadUILanguage();

        }

        #region 多语言
        public override void LoadUILanguage()
        {
            base.LoadUILanguage();
            GetLanguage(tpPar1);//  设备参数设置
            GetLanguage(gpSN);//  SN
            GetLanguage(LblDriveSN);//  SN:
            GetLanguage(butReadSN);//  读取
            GetLanguage(butWriteSN);//  写入
            GetLanguage(butWriteSN_Broadcast);//  广播写
            GetLanguage(gbPassword);//  通讯密码
            GetLanguage(LblConnectPassword);//  密码:
            GetLanguage(butReadConnectPassword);//  读取
            GetLanguage(butWriteConnectPassword);//  写入
            GetLanguage(butResetConnectPassword);//  重置
            GetLanguage(btnReadSystemStatus);//  读取
            GetLanguage(gbVersion);//  版本号
            GetLanguage(LblVersion);//  硬件版本号:
            GetLanguage(btnReadVersion);//  读取
            GetLanguage(gbTCP);//  TCP/IP 连接参数
            GetLanguage(lblMAC);//  MAC地址:
            GetLanguage(LblIP);//  IP地址:
            GetLanguage(LblIPMask);//  子网掩码:
            GetLanguage(LblIPGateway);//  网关IP:
            GetLanguage(LblDNS);//  DNS:
            GetLanguage(LblDNSBackup);//  备用DNS:
            GetLanguage(lblAutoIP);//  自动获得IP:
            GetLanguage(LblUDPPort);//  本地UDP端口:
            GetLanguage(LblServerPort);//  服务器端口:
            GetLanguage(LblServerIP);//  服务器IP:
            GetLanguage(LblServerAddr);//  服务器域名:
            GetLanguage(butRendTCPSetting);//  读取
            GetLanguage(butWriteTCPSetting);//  写入
            GetLanguage(gbRunStatus);//  设备运行信息
            GetLanguage(lblRunDay);//  设备已运行天数:
            GetLanguage(LblRestartCount);//  看门狗复位次数:
            GetLanguage(lblFormatCount);//  格式化次数:
            GetLanguage(LblStartTime);//  上电时间:
            GetLanguage(gbRecordMode);//  记录存储方式
            GetLanguage(lblRecordMode);//  记录满盘后:
            GetLanguage(rBtnCover);//  循环覆盖存储
            GetLanguage(rBtnNoCover);//  不再保存新纪录
            GetLanguage(btnReadRecordMode);//  读取
            GetLanguage(btnWriteRecordMode);//  写入
            GetLanguage(gbWatch);//  数据监控
            GetLanguage(lbWatchStateTag);//  监控状态:
            GetLanguage(lbWatchState);//  未开启
            GetLanguage(btnBeginWatch);//  实时监控开
            GetLanguage(btnCloseWatch);//  实时监控关
            GetLanguage(btnReadWatchState);//  状态
            GetLanguage(btnBeginWatch_Broadcast);//  开启广播
            GetLanguage(btnCloseWatch_Broadcast);//  关闭广播

            GetLanguage(tabPage2);//参数2
            GetLanguage(gpLocalIdentity);//本机身份
            GetLanguage(Lbl_p2_DoorNum);//门号
            GetLanguage(Lbl_p2_InOut);//进出
            GetLanguage(btnReadLocalIdentity);//读取本机身份
            GetLanguage(btnWriteLocalIdentity);//写入本机身份
            GetLanguage(Lbl_p2_LocalName);//本机名称
            GetLanguage(gpWgOut);//韦根输出
            GetLanguage(Lbl_p2_ReadCardByte);//读卡字节
            GetLanguage(Lbl_p2_WgOut);//韦根输出
            GetLanguage(Lbl_p2_WgOrder);//WG字节顺序
            GetLanguage(Lbl_p2_OutputDataType);//输出数据类型
            GetLanguage(btnReadWiegandOutput);//读取韦根输出
            GetLanguage(btnWriteWiegandOutput);//写入韦根输出
            GetLanguage(gpFace_fingerprint_threshold);//人脸、指纹对比阈值
            GetLanguage(Lbl_p2_FoFReadCardByte);//读卡字节
            GetLanguage(Lbl_p2_FoFWgOut);//韦根输出
            GetLanguage(btnReadComparisonThreshold);//读取人脸、指纹对比阈值
            GetLanguage(btnWriteComparisonThreshold);//写入人脸、指纹对比阈值
            GetLanguage(gpScreenContent);//屏幕显示内容
            GetLanguage(cbDisplay1);//人名
            GetLanguage(cbDisplay2);//人员编号
            GetLanguage(cbDisplay3);//部门
            GetLanguage(cbDisplay4);//职务
            GetLanguage(cbDisplay5);//人员照片
            GetLanguage(cbDisplay6);//卡号
            GetLanguage(cbDisplay7);//记录照片
            GetLanguage(cbDisplay8);//记录时间
            GetLanguage(cbDisplay9);//用户号
            GetLanguage(gpMenuManagementPassword);//菜单管理密码
            GetLanguage(Lbl_p2_pwd);//密码
            GetLanguage(btnReadManageMenuPassword);//读取菜单管理密码
            GetLanguage(btnWriteManageMenuPassword);//写入菜单管理密码
            GetLanguage(gpOemInfo);//OEM信息
            GetLanguage(Lbl_p2_Manufacturers);//制造商
            GetLanguage(Lbl_p2_Url);//网 址
            GetLanguage(Lbl_p2_MakeDate);//生产日期
            GetLanguage(btnReadOEM);//制造商读取
            GetLanguage(btnWriteOEM);//制造商写入


            GetLanguage(tabPage3);//                                  参数3
            GetLanguage(gpDeviceLanguage);//                          设备语言
            GetLanguage(Lbl_p3_Language);//                           语言
            GetLanguage(butReadLanguage);//                           读取
            GetLanguage(butWriteLanguage);//                          写入
            GetLanguage(gpDeviceVolume);//                            设备音量
            GetLanguage(Lbl_Volume);//                                音量
            GetLanguage(butReadVolume);//                             读取
            GetLanguage(butWriteVolume);//                            写入
            GetLanguage(Lbl_Filllightmode);//                         补光灯模式
            GetLanguage(But_WriteFaceLEDMode);//                      写入
            GetLanguage(But_ReadFaceLEDMode);//                       读取
            GetLanguage(Lbl_MaskRecognitionSwitch);//                 口罩识别开关
            GetLanguage(But_WriteFaceMouthmufflePar);//               写入
            GetLanguage(But_ReadFaceMouthmufflePar);//                读取
            GetLanguage(Lbl_TemperatureExaminationAndFormat);//       体温检测及格式
            GetLanguage(But_WriteFaceBodyTemperature);//              写入
            GetLanguage(But_ReadFaceBodyTemperature);//               读取
            GetLanguage(Lbl_TemperatureAlarmThreshold);//             体温报警阈值
            GetLanguage(But_WriteFaceBodyTemperatureAlarm);//         写入
            GetLanguage(But_ReadFaceBodyTemperatureAlarm);//          读取
            GetLanguage(Lbl_TemperatureIndicatorSwitch);//            体温数值显示开关
            GetLanguage(But_WriteFaceBodyTemperatureShow);//          写入
            GetLanguage(But_ReadFaceBodyTemperatureShow);//           读取
            GetLanguage(Lbl_ShortMessage);//                          短消息
            GetLanguage(But_WriteShortMessage);//                     写入
            GetLanguage(But_ReadShortMessage);//                      读取
            GetLanguage(LblDoorAccessMode);//开门验证方式
            GetLanguage(butReadDoorOpenCheckMode);//读取
            GetLanguage(butWriteDoorOpenCheckMode);// 写入 
            GetLanguage(LabFaceBioassay);//活体检测
            GetLanguage(Btn_ReadFaceBioassay);//读取
            GetLanguage(Btn_WriteFaceBioassay);// 写入 


            GetLanguage(tpNetwork);//客户端网络参数
            GetLanguage(gbServerDetail);//服务器参数
            GetLanguage(lblServerPort_1);//服务器端口号:
            GetLanguage(lblServerIP_1);//服务器IP:
            GetLanguage(LblServerDomain);//服务器域名:
            GetLanguage(butReadNetworkServerDetail);//读取
            GetLanguage(butWriteNetworkServerDetail);//写入
            GetLanguage(gbClientDetail);//客户端参数
            GetLanguage(LblKeepAliveInterval);//与服务器建立连接后，每隔
            GetLanguage(LblKeepAliveInterval1);//秒，发送一次保活包
            GetLanguage(btnReadKeepAliveInterval);//读取
            GetLanguage(btnWriteKeepAliveInterval);//写入
            GetLanguage(butRequireSendKeepalivePacket);//立即发送保活包
            GetLanguage(butReadClientWorkMode);//读取
            GetLanguage(butWriteClientWorkMode);//写入
            GetLanguage(butRequireConnectServer);//立即重连服务器
            GetLanguage(butReadClientStatus_Result);//获取状态
            GetLanguage(LblClientNetWorkMode);//客户端网络模式:
            GetLanguage(LblServerStatus);//客户端网络状态:
            LoadComboxItemsLanguage(cmbInOut, "cmbInOut");//;
            cmbDoor.Items.Clear();
            cmbDoor.Items.AddRange(DoorList);
            cmbDoor.SelectedIndex = 0;
            cmbInOut.SelectedIndex = 0;

            Cmb_FaceMouthmuffle.Items.Clear();
            cmb_FaceBodyTemperatureShow.Items.Clear();
            cmbReadCardByte.Items.Clear();
            cmbWGOutput.Items.Clear();
            cmbWGByteSort.Items.Clear();
            cmbOutputType.Items.Clear();
            cmbDoorAccessMode.Items.Clear();

            var ReadCardByteList = GetLanguage("ReadCardByteList").Split(',');
            cmbReadCardByte.Items.AddRange(ReadCardByteList);
            cmbReadCardByte.SelectedIndex = 0;


            var IsUseList = GetLanguage("IsUseList").Split(',');
            cmbWGOutput.Items.AddRange(IsUseList);
            cmbWGOutput.SelectedIndex = 0;

            cbxAutoIP.Items.AddRange(IsUseList);
            cbxAutoIP.SelectedIndex = 0;

            var faceIsUseList = GetLanguage("Cmb_FaceMouthmuffle").Split(',');
            Cmb_FaceMouthmuffle.Items.AddRange(faceIsUseList);
            Cmb_FaceMouthmuffle.SelectedIndex = 0;

            cmb_FaceBodyTemperatureShow.Items.AddRange(faceIsUseList);
            cmb_FaceBodyTemperatureShow.SelectedIndex = 0;

            var WGByteSortList = GetLanguage("WGByteSortList").Split(',');
            cmbWGByteSort.Items.AddRange(WGByteSortList);
            cmbWGByteSort.SelectedIndex = 0;

            var OutputTypeList = GetLanguage("OutputTypeList").Split(',');
            cmbOutputType.Items.AddRange(OutputTypeList);
            cmbOutputType.SelectedIndex = 0;

            var sDoorAccessModeList = GetLanguage("DoorAccessModeList").Split(',');
            cmbDoorAccessMode.Items.AddRange(sDoorAccessModeList);
            cmbDoorAccessMode.SelectedIndex = 0;

            string[] ComparisonThresholdList = new string[100];
            for (int i = 1; i <= 100; i++)
            {
                ComparisonThresholdList[i - 1] = i.ToString();
            }
            cmbFace.Items.AddRange(ComparisonThresholdList);
            cmbFingerprint.Items.AddRange(ComparisonThresholdList);
            cmbFace.SelectedIndex = 0;
            cmbFingerprint.SelectedIndex = 0;
            Cmb_FaceLEDMode.Items.Clear();
            var FaceLEDModeList = GetLanguage("FaceLEDModeList").Split(',');
            Cmb_FaceLEDMode.Items.AddRange(FaceLEDModeList);
            Cmb_FaceLEDMode.SelectedIndex = 0;

            Cmb_FaceMouthmuffle.SelectedIndex = 0;

            cmb_FaceBodyTemperatureShow.SelectedIndex = 0;

            Cmb_FaceBodyTemperature.Items.Clear();
            var FaceBodyTemperatureList = GetLanguage("FaceBodyTemperatureList").Split(',');
            Cmb_FaceBodyTemperature.Items.AddRange(FaceBodyTemperatureList);
            Cmb_FaceBodyTemperature.SelectedIndex = 0;

            cbxKeepAliveInterval.Text = "10";
            cmbClientNetWorkMode.Items.Clear();
            ClientNetWorkMode = GetLanguage("ClientNetWorkMode").Split(',');
            cmbClientNetWorkMode.Items.AddRange(ClientNetWorkMode);
            cmbClientNetWorkMode.SelectedIndex = 1;
            var sFaceBioassay = GetLanguage("FaceBioassayCmb").Split(',');
            CmbFaceBioassay.Items.Clear();
            CmbFaceBioassay.Items.AddRange(sFaceBioassay);
            CmbFaceBioassay.SelectedIndex = 0;
            IniDriveLanguage();
            IniDriveVolume();
        }
        #endregion


        string[] ClientNetWorkMode;
        // string[] FaceBodyTemperatureList = { "禁止", "摄氏度（默认值）", "华氏度" };
        //string[] FaceLEDModeList = { "一直关", "一直亮", "检测到人员时开" };
        string[] DoorList = new string[] { "1", "2", "3", "4" };
        // string[] ReadCardByteList = new string[] { "韦根26", "韦根34", "韦根26", "韦根66", "禁用" };
        // string[] IsUseList = new string[] { "启用", "禁用" };
        //string[] WGByteSortList = new string[] { "高位在前低位在后", "低位在前高位在后" };
        //string[] OutputTypeList = new string[] { "输出用户号", "输出人员卡号" };

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

        private void FrmSystem_FormClosed(object sender, FormClosedEventArgs e)
        {
            onlyObj = null;
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
                MsgErr(GetLanguage("Msg_1"));
                return;
            }
            if (!pwd.IsHex())
            {
                MsgErr(GetLanguage("Msg_1"));
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
            ResetConnectPassword cmd = new ResetConnectPassword(cmdDtl);
            mMainForm.AddCommand(cmd);
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
                    txtUDPPort.Text = result.TCP.mUDPPort.ToString();
                    txtServerIP.Text = result.TCP.mServerIP;
                    txtServerAddr.Text = result.TCP.mServerAddr;
                    txtServerPort.Text = result.TCP.mServerPort.ToString();

                    cbxAutoIP.SelectedIndex = result.TCP.mAutoIP == true ? 1 : 0;
                });
                string TCPInfo = DebugTCPDetail(result.TCP);
                mMainForm.AddCmdLog(cmde, TCPInfo);
            };
        }

        private void ButWriteTCPSetting_Click(object sender, EventArgs e)
        {
            string reg = @"([A-Fa-f0-9]{2}-){5}[A-Fa-f0-9]{2}";
            string reg2 = @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$";
            string reg3 = @"^\+?[1-9][0-9]*$";
            string reg4 = @"^(?=^.{3,255}$)[a-zA-Z0-9][-a-zA-Z0-9]{0,62}(\.[a-zA-Z0-9][-a-zA-Z0-9]{0,62})+$";

            if (!Regex.IsMatch(txtMAC.Text.Trim(), reg))
            {
                MsgErr(GetLanguage("Msg_2"));
                return;
            }

            if (!Regex.IsMatch(txtIP.Text.Trim(), reg2))
            {
                MsgErr(GetLanguage("Msg_3"));
                return;
            }
            if (!Regex.IsMatch(txtIPMask.Text.Trim(), reg2))
            {
                MsgErr(GetLanguage("Msg_4"));
                return;
            }
            if (!Regex.IsMatch(txtIPGateway.Text.Trim(), reg2))
            {
                MsgErr(GetLanguage("Msg_5"));
                return;
            }
            if (!Regex.IsMatch(txtDNS.Text.Trim(), reg2))
            {
                MsgErr(GetLanguage("Msg_6"));
                return;
            }
            if (!Regex.IsMatch(txtDNSBackup.Text.Trim(), reg2))
            {
                MsgErr(GetLanguage("Msg_7"));
                return;
            }
            if (!Regex.IsMatch(txtServerIP.Text.Trim(), reg2))
            {
                MsgErr(GetLanguage("Msg_8"));
                return;
            }
            if (!Regex.IsMatch(txtUDPPort.Text.Trim(), reg3))
            {
                MsgErr(GetLanguage("Msg_9"));
                return;
            }
            if (Convert.ToInt32(txtUDPPort.Text.Trim()) > 65535)
            {
                MsgErr(GetLanguage("Msg_10"));
                return;
            }
            if (!Regex.IsMatch(txtServerPort.Text.Trim(), reg3))
            {
                MsgErr(GetLanguage("Msg_11"));
                return;
            }
            if (Convert.ToInt32(txtServerPort.Text.Trim()) > 65535)
            {
                MsgErr(GetLanguage("Msg_11"));
                return;
            }
            txtServerAddr.Text = "www.pc15.net";



            if (Convert.ToInt16(cbxAutoIP.SelectedIndex) == -1)
            {
                MsgErr(GetLanguage("Msg_12"));
                return;
            }

            TCPDetail tcp = new TCPDetail();
            tcp.mIP = txtIP.Text.Trim();
            tcp.mMAC = txtMAC.Text.Trim();
            tcp.mIPMask = txtIPMask.Text.Trim();
            tcp.mIPGateway = txtIPGateway.Text.Trim();
            tcp.mDNS = txtDNS.Text.Trim();
            tcp.mDNSBackup = txtDNSBackup.Text.Trim();
            tcp.mTCPPort = 8000;
            tcp.mUDPPort = Convert.ToInt32(txtUDPPort.Text.Trim());
            tcp.mServerIP = txtServerIP.Text.Trim();
            tcp.mServerAddr = txtServerAddr.Text.Trim();
            tcp.mServerPort = Convert.ToInt32(txtServerPort.Text.Trim());

            tcp.mProtocolType = 1;

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
            //                    "  IP:" + newtcp.mIP +
            //                    "  子网掩码:" + newtcp.mIPMask +
            //                    "  网关地址:" + newtcp.mIPGateway +
            //                    "  DNS:" + newtcp.mDNS +
            //                    "  备用DNS:" + newtcp.mDNSBackup +
            //                    "  本地TCP端口:" + newtcp.mTCPPort +
            //                    "  本地UDP端口:" + newtcp.mUDPPort +
            //                    "  服务器IP:" + newtcp.mServerIP +
            //                    "  服务器域名:" + newtcp.mServerAddr +
            //                    "  TCP工作模式:" + newtcp.mProtocolType +
            //                    "  自动获得IP:" + newtcp.mAutoIP +
            //                    "  服务器端口:" + newtcp.mServerPort;
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
            ReadSystemRunStatus cmd = new ReadSystemRunStatus(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadSystemRunStatus_Result result = cmde.Command.getResult() as ReadSystemRunStatus_Result;
                string RunDay = result.RunDay.ToString() + GetLanguage("Msg_13"); //设备已运行天数
                string FormatCount = result.FormatCount.ToString() + GetLanguage("Msg_14"); //格式化次数
                string RestartCount = result.RestartCount.ToString() + GetLanguage("Msg_14"); //看门狗复位次数
                string StartTime = result.StartTime; //上电时间

                Invoke(() =>
                {
                    txtRunDay.Text = RunDay;
                    txtFormatCount.Text = FormatCount;
                    txtRestartCount.Text = RestartCount;
                    txtStartTime.Text = StartTime;

                });
                var line = System.Environment.NewLine;
                string TCPInfo = GetLanguage("Msg_15") + ":" + RunDay + line +
                                 GetLanguage("Msg_16") + ":" + FormatCount + line +
                                 GetLanguage("Msg_17") + ":" + RestartCount + line +
                                 GetLanguage("Msg_18") + ":" + StartTime;
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
            var line = System.Environment.NewLine;
            //GetLanguage("Msg_18")
            string TCPInfo = GetLanguage("Msg_19") + ":" + MAC + line +
                             "IP:" + IP + line +
                             GetLanguage("Msg_20") + ":" + IPMask + line +
                             GetLanguage("Msg_21") + ":" + IPGateway + line +
                             "DNS:" + DNS + line +
                             GetLanguage("Msg_22") + ":" + DNSBackup + line +
                             GetLanguage("Msg_23") + ":" + TCPPort + line +
                             GetLanguage("Msg_24") + ":" + UDPPort + line +
                             GetLanguage("Msg_25") + ":" + ServerIP + line +
                             GetLanguage("Msg_26") + ":" + ServerAddr + line +
                             GetLanguage("Msg_27") + ":" + ServerPort;
            return TCPInfo;
        }

        private void ButWriteSN_Click(object sender, EventArgs e)
        {
            if (!CheckSN()) return;
            string sn = txtSN.Text;
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            WriteSN cmd = new WriteSN(cmdDtl, new SN_Parameter(sn));
            mMainForm.AddCommand(cmd);
        }

        private void ButWriteSN_Broadcast_Click(object sender, EventArgs e)
        {
            if (!CheckSN()) return;
            string sn = txtSN.Text;
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            WriteSN_Broadcast cmd = new WriteSN_Broadcast(cmdDtl, new SN_Parameter(sn));
            mMainForm.AddCommand(cmd);
        }

        /// <summary>
        /// 检测SN格式
        /// </summary>
        /// <returns></returns>
        private bool CheckSN()
        {
            //GetLanguage("Msg_27")
            string sn = txtSN.Text;
            if (sn.Length != 16)
            {
                MsgErr(GetLanguage("Msg_28"));
                return false;
            }
            int len = System.Text.Encoding.ASCII.GetByteCount(sn);
            if (len != 16)
            {
                MsgErr(GetLanguage("Msg_28"));
                return false;
            }
            return true;
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
                version = GetLanguage("Msg_29") + ":" + version;
                mMainForm.AddCmdLog(cmde, version);
            };
        }

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
                string ModeStr = result.Mode == 0 ? GetLanguage("Msg_30") : GetLanguage("Msg_31"); //记录存储方式
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
                ModeStr = GetLanguage("Msg_32") + ":" + ModeStr;
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

            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            WriteRecordMode cmd = new WriteRecordMode(cmdDtl, new WriteRecordMode_Parameter(mode));
            mMainForm.AddCommand(cmd);
        }

        private void BtnBeginWatch_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            BeginWatch cmd = new BeginWatch(cmdDtl);
            mMainForm.AddCommand(cmd);

            lbWatchState.Text = GetLanguage("Msg_33");
            lbWatchState.ForeColor = Color.Green;
        }

        private void BtnCloseWatch_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            CloseWatch cmd = new CloseWatch(cmdDtl);
            mMainForm.AddCommand(cmd);

            lbWatchState.Text = GetLanguage("Msg_34");
            lbWatchState.ForeColor = Color.Red;
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
                string WatchStateStr = cmd.WatchState == 0 ? GetLanguage("Msg_35") : GetLanguage("Msg_36");
                Invoke(() =>
                {
                    if (cmd.WatchState == 1)
                    {
                        lbWatchState.Text = GetLanguage("Msg_33");
                        lbWatchState.ForeColor = Color.Green;
                    }
                    else
                    {
                        lbWatchState.Text = GetLanguage("Msg_34");
                        lbWatchState.ForeColor = Color.Red;
                    }
                });
                WatchStateStr = GetLanguage("Msg_37") + ":" + WatchStateStr;
                mMainForm.AddCmdLog(cmde, WatchStateStr);
            };
        }

        private void BtnBeginWatch_Broadcast_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            BeginWatch_Broadcast cmd = new BeginWatch_Broadcast(cmdDtl);
            mMainForm.AddCommand(cmd);
        }

        private void BtnCloseWatch_Broadcast_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            CloseWatch_Broadcast cmd = new CloseWatch_Broadcast(cmdDtl);
            mMainForm.AddCommand(cmd);
        }


        #region 保活包间隔
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
                    IntervalTimeInfo = GetLanguage("Msg_38");
                }
                else
                {
                    IntervalTimeInfo = IntervalTime.ToString() + GetLanguage("Msg_39");
                }

                Invoke(() =>
                {
                    cbxKeepAliveInterval.Text = IntervalTime.ToString();
                });
                string IntervalTimeStr = string.Format(GetLanguage("Msg_40"), IntervalTimeInfo);
                mMainForm.AddCmdLog(cmde, IntervalTimeStr);
            };
        }

        private void BtnWriteKeepAliveInterval_Click(object sender, EventArgs e)
        {
            ushort IntervalTime = 0;
            string sIntervalTime = cbxKeepAliveInterval.Text.Trim();
            if (sIntervalTime == GetLanguage("Msg_38"))
            {
                IntervalTime = 0;
            }
            else
            {
                if (!ushort.TryParse(sIntervalTime, out IntervalTime))
                {
                    MsgErr(GetLanguage("Msg_41"));
                    return;
                }
            }
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            WriteKeepAliveInterval cmd = new WriteKeepAliveInterval(cmdDtl, new WriteKeepAliveInterval_Parameter(IntervalTime));
            mMainForm.AddCommand(cmd);
        }
        #endregion

        private void BtnReadLocalIdentity_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadLocalIdentity cmd = new ReadLocalIdentity(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadLocalIdentity_Result result = cmde.Command.getResult() as ReadLocalIdentity_Result;

                string Info = string.Format(GetLanguage("Msg_42"), DoorList[result.Door - 1]);

                Info += result.InOut == 0 ? GetLanguage("Msg_44") + "，" : GetLanguage("Msg_45") + "，" + GetLanguage("Msg_43") + ":" + result.LocalName;

                Invoke(() =>
                {
                    txtLocalName.Text = result.LocalName;
                    cmbDoor.SelectedItem = result.Door - 1;
                    cmbInOut.SelectedIndex = result.InOut;
                });
                mMainForm.AddCmdLog(cmde, Info);
            };
        }

        private void BtnWriteLocalIdentity_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            WriteLocalIdentity_Parameter par = new WriteLocalIdentity_Parameter(Convert.ToByte(cmbDoor.SelectedItem), txtLocalName.Text, (byte)(cmbInOut.SelectedIndex));
            WriteLocalIdentity cmd = new WriteLocalIdentity(cmdDtl, par);
            mMainForm.AddCommand(cmd);
        }

        private void BtnWriteWiegandOutput_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            WriteWiegandOutput_Parameter par = new WriteWiegandOutput_Parameter(Convert.ToByte(cmbReadCardByte.SelectedIndex + 1), Convert.ToByte(cmbWGOutput.SelectedIndex + 1)
                , Convert.ToByte(cmbWGByteSort.SelectedIndex), Convert.ToByte(cmbOutputType.SelectedIndex + 1));
            WriteWiegandOutput cmd = new WriteWiegandOutput(cmdDtl, par);
            mMainForm.AddCommand(cmd);
        }

        private void BtnReadWiegandOutput_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadWiegandOutput cmd = new ReadWiegandOutput(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadWiegandOutput_Result result = cmde.Command.getResult() as ReadWiegandOutput_Result;

                //string Info = $"本机身份:门号【{DoorList[result.Door]}】，";

                //Info += result.InOut == 0 ? "进，" : "出，" + "本机身份:" + result.LocalName;

                Invoke(() =>
                {
                    cmbReadCardByte.SelectedIndex = result.ReadCardByte - 1;
                    cmbWGOutput.SelectedIndex = result.WGOutputSwitch - 1;
                    cmbWGByteSort.SelectedIndex = result.WGByteSort - 1;
                    cmbOutputType.SelectedIndex = result.OutputType - 1;
                });
                //  mMainForm.AddCmdLog(cmde, "");
            };

        }

        private void BtnReadComparisonThreshold_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadComparisonThreshold cmd = new ReadComparisonThreshold(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadComparisonThreshold_Result result = cmde.Command.getResult() as ReadComparisonThreshold_Result;

                //string Info = $"本机身份:门号【{DoorList[result.Door]}】，";

                //Info += result.InOut == 0 ? "进，" : "出，" + "本机身份:" + result.LocalName;

                Invoke(() =>
                {
                    cmbFace.SelectedItem = result.FaceComparisonThreshold.ToString();
                    cmbFingerprint.SelectedItem = result.FingerprintComparisonThreshold.ToString();

                });
                // mMainForm.AddCmdLog(cmde, "");
            };
        }

        private void BtnWriteComparisonThreshold_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            WriteComparisonThreshold_Parameter par = new WriteComparisonThreshold_Parameter(Convert.ToByte(cmbFace.SelectedItem), Convert.ToByte(cmbFingerprint.SelectedItem));
            WriteComparisonThreshold cmd = new WriteComparisonThreshold(cmdDtl, par);
            mMainForm.AddCommand(cmd);
        }

        private void BtnReadScreenDisplayContent_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadScreenDisplayContent cmd = new ReadScreenDisplayContent(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadScreenDisplayContent_Result result = cmde.Command.getResult() as ReadScreenDisplayContent_Result;

                //string Info = $"本机身份:门号【{DoorList[result.Door]}】，";

                //Info += result.InOut == 0 ? "进，" : "出，" + "本机身份:" + result.LocalName;

                Invoke(() =>
                {
                    cbDisplay1.Checked = result.DisplayList[0] == 1;
                    cbDisplay2.Checked = result.DisplayList[1] == 1;
                    cbDisplay3.Checked = result.DisplayList[2] == 1;
                    cbDisplay4.Checked = result.DisplayList[3] == 1;
                    cbDisplay5.Checked = result.DisplayList[4] == 1;
                    cbDisplay6.Checked = result.DisplayList[5] == 1;
                    cbDisplay7.Checked = result.DisplayList[6] == 1;
                    cbDisplay8.Checked = result.DisplayList[7] == 1;
                    cbDisplay9.Checked = result.DisplayList[8] == 1;
                });
                // mMainForm.AddCmdLog(cmde, "");
            };
        }

        private void BtnWriteScreenDisplayContent_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            byte[] list = new byte[9];
            list[0] = Convert.ToByte(cbDisplay1.Checked ? 1 : 0);
            list[1] = Convert.ToByte(cbDisplay2.Checked ? 1 : 0);
            list[2] = Convert.ToByte(cbDisplay3.Checked ? 1 : 0);
            list[3] = Convert.ToByte(cbDisplay4.Checked ? 1 : 0);
            list[4] = Convert.ToByte(cbDisplay5.Checked ? 1 : 0);
            list[5] = Convert.ToByte(cbDisplay6.Checked ? 1 : 0);
            list[6] = Convert.ToByte(cbDisplay7.Checked ? 1 : 0);
            list[7] = Convert.ToByte(cbDisplay8.Checked ? 1 : 0);
            list[8] = Convert.ToByte(cbDisplay9.Checked ? 1 : 0);

            WriteScreenDisplayContent_Parameter par = new WriteScreenDisplayContent_Parameter(list);
            WriteScreenDisplayContent cmd = new WriteScreenDisplayContent(cmdDtl, par);
            mMainForm.AddCommand(cmd);
        }

        #region 菜单管理密码
        private void BtnReadManageMenuPassword_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadManageMenuPassword cmd = new ReadManageMenuPassword(cmdDtl);
            mMainForm.AddCommand(cmd);

            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadManageMenuPassword_Result result = cmde.Command.getResult() as ReadManageMenuPassword_Result;

                Invoke(() =>
                {
                    txtPassword.Text = result.Password;
                });
                // mMainForm.AddCmdLog(cmde, "");
            };
        }

        private void BtnWriteManageMenuPassword_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            string password = txtPassword.Text;
            if (string.IsNullOrWhiteSpace(password))
            {
                password = "FFFFFFFF";
            }
            WriteManageMenuPassword_Parameter par = new WriteManageMenuPassword_Parameter(password);
            WriteManageMenuPassword cmd = new WriteManageMenuPassword(cmdDtl, par);
            mMainForm.AddCommand(cmd);
        }
        #endregion

        #region OEM
        private void BtnReadOEM_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadOEM cmd = new ReadOEM(cmdDtl);
            mMainForm.AddCommand(cmd);

            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                OEM_Result result = cmde.Command.getResult() as OEM_Result;

                Invoke(() =>
                {
                    txtManufacturer.Text = result.Detail.Manufacturer;
                    txtWebAddr.Text = result.Detail.WebAddr;
                    dtpDate.Value = result.Detail.DeliveryDate;
                    dtpTime.Value = result.Detail.DeliveryDate;
                });
                //  mMainForm.AddCmdLog(cmde, "");
            };
        }

        private void BtnWriteOEM_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            OEMDetail oEM = new OEMDetail()
            {
                Manufacturer = txtManufacturer.Text,
                WebAddr = txtWebAddr.Text
                ,
                DeliveryDate = new DateTime(dtpDate.Value.Year, dtpDate.Value.Month, dtpDate.Value.Day, dtpTime.Value.Hour, dtpTime.Value.Minute, dtpTime.Value.Second)
            };
            OEM_Parameter par = new OEM_Parameter(oEM);
            WriteOEM cmd = new WriteOEM(cmdDtl, par);
            mMainForm.AddCommand(cmd);
        }
        #endregion

        #region 设备语言
        private void IniDriveLanguage()
        {
            string[] DriveLanguageList = GetLanguage("DriveLanguageList").SplitTrim(",");
            cmbLanguage.Items.Clear();
            cmbLanguage.Items.AddRange(DriveLanguageList);
            cmbLanguage.SelectedIndex = 0;
        }

        private void ButReadLanguage_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;

            ReadDriveLanguage cmd = new ReadDriveLanguage(cmdDtl);
            mMainForm.AddCommand(cmd);

            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadDriveLanguage_Result result = cmde.Result as ReadDriveLanguage_Result;

                Invoke(() =>
                {
                    if (result.Language < 13)
                    {
                        cmbLanguage.SelectedIndex = result.Language - 1;
                        mMainForm.AddCmdLog(cmde, GetLanguage("Msg_46") + $":{cmbLanguage.Text}");
                    }
                    else
                    {
                        mMainForm.AddCmdLog(cmde, GetLanguage("Msg_46") + $":{result.Language}");
                    }

                });

            };

        }


        private void ButWriteLanguage_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            int Lang = cmbLanguage.SelectedIndex + 1;
            WriteDriveLanguage_Parameter par = new WriteDriveLanguage_Parameter(Lang);
            WriteDriveLanguage cmd = new WriteDriveLanguage(cmdDtl, par);
            mMainForm.AddCommand(cmd);
        }
        #endregion

        #region 设备音量
        private void IniDriveVolume()
        {
            cmbDriveVolume.Items.Clear();
            cmbDriveVolume.Items.Add(GetLanguage("Msg_47"));
            for (int i = 1; i <= 10; i++)
            {
                cmbDriveVolume.Items.Add(i.ToString());
            }
            cmbDriveVolume.SelectedIndex = 10;
        }

        private void ButReadVolume_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;

            ReadDriveVolume cmd = new ReadDriveVolume(cmdDtl);
            mMainForm.AddCommand(cmd);

            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadDriveVolume_Result result = cmde.Result as ReadDriveVolume_Result;

                Invoke(() =>
                {
                    if (result.Volume <= 10)
                    {
                        cmbDriveVolume.SelectedIndex = result.Volume;
                        mMainForm.AddCmdLog(cmde, GetLanguage("Msg_48") + $":{cmbDriveVolume.Text}");
                    }
                    else
                    {
                        mMainForm.AddCmdLog(cmde, GetLanguage("Msg_46") + $":{result.Volume}");
                    }

                });

            };
        }

        private void ButWriteVolume_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            int iVolume = cmbDriveVolume.SelectedIndex;
            WriteDriveVolume_Parameter par = new WriteDriveVolume_Parameter(iVolume);
            WriteDriveVolume cmd = new WriteDriveVolume(cmdDtl, par);
            mMainForm.AddCommand(cmd);
        }
        #endregion

        #region 补光灯模式读取
        /// <summary>
        /// 补光灯模式写入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void But_WriteFaceLEDMode_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            var cmdPar = new Fingerprint.SystemParameter.WriteFaceLEDMode_Parameter(Cmb_FaceLEDMode.SelectedIndex);
            var cmd = new Fingerprint.SystemParameter.WriteFaceLEDMode(cmdDtl, cmdPar);
            mMainForm.AddCommand(cmd);
        }
        /// <summary>
        /// 补光灯模式读取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void But_ReadFaceLEDMode_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;

            var cmd = new Fingerprint.SystemParameter.ReadFaceLEDMode(cmdDtl);
            mMainForm.AddCommand(cmd);
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
             {
                 var result = cmde.Command.getResult() as ReadFaceLEDMode_Result;
                 Invoke(() =>
                 {
                     Cmb_FaceLEDMode.SelectedIndex = result.LEDMode;
                 });

             };
        }
        #endregion

        #region 口罩识别开关
        /// <summary>
        /// 写入口罩识别开关
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void But_WriteFaceMouthmufflePar_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            var cmdPar = new Fingerprint.SystemParameter.WriteFaceMouthmufflePar_Parameter(Cmb_FaceMouthmuffle.SelectedIndex);
            var cmd = new WriteFaceMouthmufflePar(cmdDtl, cmdPar);
            mMainForm.AddCommand(cmd);
        }
        /// <summary>
        /// 读取口罩识别开关
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void But_ReadFaceMouthmufflePar_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            var cmd = new ReadFaceMouthmufflePar(cmdDtl);
            mMainForm.AddCommand(cmd);
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                var result = cmde.Command.getResult() as ReadFaceMouthmufflePar_Result;
                Invoke(() =>
                {
                    Cmb_FaceMouthmuffle.SelectedIndex = result.Mouthmuffle;
                });

            };
        }
        #endregion

        #region 体温检测及格式
        /// <summary>
        /// 写入体温检测及格式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void But_WriteFaceBodyTemperature_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;

            var cmdPar = new Fingerprint.SystemParameter.WriteFaceBodyTemperaturePar_Parameter(Cmb_FaceBodyTemperature.SelectedIndex);
            var cmd = new WriteFaceBodyTemperaturePar(cmdDtl, cmdPar);
            mMainForm.AddCommand(cmd);
        }
        /// <summary>
        /// 读取体温检测及格式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void But_ReadFaceBodyTemperature_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            var cmd = new ReadFaceBodyTemperaturePar(cmdDtl);
            mMainForm.AddCommand(cmd);
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                var result = cmde.Command.getResult() as ReadFaceBodyTemperaturePar_Result;
                Invoke(() =>
                {
                    Cmb_FaceBodyTemperature.SelectedIndex = result.BodyTemperaturePar;
                });

            };
        }
        #endregion

        #region 体温报警阈值
        /// <summary>
        /// 写入体温报警阈值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void But_WriteFaceBodyTemperatureAlarm_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            if (!double.TryParse(Txt_BodyTemperatureAlarm.Text, out double alarmPar))
            {
                MessageBox.Show(GetLanguage("Msg_49"));
                return;
            }
            var cmdPar = new Fingerprint.SystemParameter.WriteFaceBodyTemperatureAlarmPar_Parameter(((int)alarmPar * 10));
            var cmd = new Fingerprint.SystemParameter.WriteFaceBodyTemperatureAlarmPar(cmdDtl, cmdPar);
            mMainForm.AddCommand(cmd);
        }
        /// <summary>
        /// 读取体温报警阈值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void But_ReadFaceBodyTemperatureAlarm_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            var cmd = new ReadFaceBodyTemperatureAlarmPar(cmdDtl);
            mMainForm.AddCommand(cmd);
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                var result = cmde.Command.getResult() as ReadFaceBodyTemperatureAlarmPar_Result;
                Invoke(() =>
                {
                    Txt_BodyTemperatureAlarm.Text = ((double)result.AlarmPar / (double)10).ToString("0.0");
                });

            };
        }
        #endregion

        #region 体温数值显示开关
        /// <summary>
        /// 写入体温数值显示开关
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void But_WriteFaceBodyTemperatureShow_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            var cmdPar = new Fingerprint.SystemParameter.WriteFaceBodyTemperatureShowPar_Parameter(cmb_FaceBodyTemperatureShow.SelectedIndex);
            var cmd = new Fingerprint.SystemParameter.WriteFaceBodyTemperatureShowPar(cmdDtl, cmdPar);
            mMainForm.AddCommand(cmd);
        }
        /// <summary>
        /// 读取体温数值显示开关
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void But_ReadFaceBodyTemperatureShow_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            var cmd = new ReadFaceBodyTemperatureShowPar(cmdDtl);
            mMainForm.AddCommand(cmd);
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                var result = cmde.Command.getResult() as ReadFaceBodyTemperatureShowPar_Result;
                Invoke(() =>
                {
                    cmb_FaceBodyTemperatureShow.SelectedIndex = result.IsShow;
                });

            };
        }
        #endregion

        #region 短消息

        /// <summary>
        /// 写入短消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void But_WriteShortMessage_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            if (Txt_ShortMessage.TextLength > 30)
            {
                MessageBox.Show(GetLanguage("Msg_50"));
                return;
            }
            var cmdPar = new Fingerprint.SystemParameter.WriteShortMessage_Parameter(Txt_ShortMessage.Text);
            var cmd = new Fingerprint.SystemParameter.WriteShortMessage(cmdDtl, cmdPar);
            mMainForm.AddCommand(cmd);
        }
        /// <summary>
        /// 读取短消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void But_ReadShortMessage_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            var cmd = new ReadShortMessage(cmdDtl);
            mMainForm.AddCommand(cmd);
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {

                var result = cmde.Command.getResult() as ReadShortMessage_Result;
                mMainForm.AddCmdLog(cmde, GetLanguage("Msg_51") + $":{ result.Message}");
                Invoke(() =>
                {
                    Txt_ShortMessage.Text = result.Message;
                });


            };
        }
        #endregion

        #region 开门验证方式
        /// <summary>
        /// 写入开门验证方式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butWriteDoorOpenCheckMode_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            var cmdPar = new Door.WriteDoorOpenCheckMode_Parameter((byte)(cmbDoorAccessMode.SelectedIndex + 1));
            var cmd = new Door.WriteDoorOpenCheckMode(cmdDtl, cmdPar);
            mMainForm.AddCommand(cmd);
        }
        /// <summary>
        /// 读取开门验证方式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butReadDoorOpenCheckMode_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            var cmd = new Door.ReadDoorOpenCheckMode(cmdDtl);
            mMainForm.AddCommand(cmd);
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                var result = cmde.Command.getResult() as Door.ReadDoorOpenCheckMode_Result;
                Invoke(() =>
                {
                    if (result.CheckMode < 3)
                    {
                        cmbDoorAccessMode.SelectedIndex = result.CheckMode - 1;
                        mMainForm.AddCmdLog(cmde, GetLanguage("DoorAccessModeResult") + cmbDoorAccessMode.Text);
                    }
                    else
                    {
                        mMainForm.AddCmdLog(cmde, GetLanguage("DoorAccessModeResult") + $"{ result.CheckMode}");
                    }

                });

            };
        }
        #endregion


        #region 服务器网络参数

        private void butReadNetworkServerDetail_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            var cmd = new ReadNetworkServerDetail(cmdDtl);
            mMainForm.AddCommand(cmd);
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                var result = cmde.Command.getResult() as ReadNetworkServerDetail_Result;
                mMainForm.AddCmdLog(cmde, string.Format(GetLanguage("Msg_52"), result.ServerPort, result.ServerIP, result.ServerDomain));
                Invoke(() =>
                {
                    txtServerIP_1.Text = result.ServerIP;
                    txtServerPort_1.Text = result.ServerPort.ToString();
                    txtServerDomain.Text = result.ServerDomain;

                });

            };
        }

        private void butWriteNetworkServerDetail_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            string sIP = txtServerIP_1.Text;
            int iPort = 0;
            if (!int.TryParse(txtServerPort_1.Text, out iPort))
            {
                MsgErr(GetLanguage("Msg_11"));
                return;
            }
            string sDomain = txtServerDomain.Text;

            var par = new WriteNetworkServerDetail_Parameter(iPort, sIP);
            par.ServerDomain = sDomain;
            if (!par.checkedParameter())
            {
                MsgErr(GetLanguage("Msg_53"));
                return;
            }

            var cmd = new WriteNetworkServerDetail(cmdDtl, par);
            mMainForm.AddCommand(cmd);

            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                mMainForm.AddCmdLog(cmde, string.Format(GetLanguage("Msg_52"), iPort, sIP, sDomain));
            };
        }

        #endregion

        #region 立即发送一次保活包

        private void butRequireSendKeepalivePacket_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            var cmd = new RequireSendKeepalivePacket(cmdDtl);
            mMainForm.AddCommand(cmd);

            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                var result = cmde.Command.getResult() as RequireSendKeepalivePacket_Result;
                int iCode = result.ResultStatus;
                string[] sCodeName = new string[10];
                sCodeName[1] = GetLanguage("Msg_54");
                sCodeName[2] = GetLanguage("Msg_55");
                sCodeName[3] = GetLanguage("Msg_56");
                sCodeName[4] = GetLanguage("Msg_57");
                sCodeName[5] = GetLanguage("Msg_58");
                sCodeName[6] = GetLanguage("Msg_59");
                sCodeName[7] = GetLanguage("Msg_60");
                sCodeName[8] = GetLanguage("Msg_61");
                mMainForm.AddCmdLog(cmde, GetLanguage("Msg_62") + $":{sCodeName[iCode]} ");

            };
        }
        #endregion

        #region 使设备重新连接服务器
        private void butRequireConnectServer_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            var cmd = new RequireConnectServer(cmdDtl);
            mMainForm.AddCommand(cmd);

            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                var result = cmde.Command.getResult() as RequireConnectServer_Result;
                int iCode = result.ResultStatus;
                string[] sCodeName = new string[10];
                sCodeName[1] = GetLanguage("Msg_63");
                sCodeName[2] = GetLanguage("Msg_55");
                sCodeName[3] = GetLanguage("Msg_56");
                sCodeName[4] = GetLanguage("Msg_57");
                sCodeName[5] = GetLanguage("Msg_58");
                sCodeName[6] = GetLanguage("Msg_59");
                sCodeName[7] = GetLanguage("Msg_60");
                sCodeName[8] = GetLanguage("Msg_61");
                mMainForm.AddCmdLog(cmde, $"Msg_62:{sCodeName[iCode]} ");

            };
        }
        #endregion

        #region 获取客户端连接状态
        private void butReadClientStatus_Result_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            var cmd = new ReadClientStatus(cmdDtl);
            mMainForm.AddCommand(cmd);

            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                var result = cmde.Command.getResult() as ReadClientStatus_Result;
                int iModel = result.ClientModel;

                var strbuf = new StringBuilder();

                strbuf.Append(GetLanguage("Msg_64") + ":").Append(ClientNetWorkMode[iModel]);
                strbuf.Append("，" + GetLanguage("Msg_65") + ":").Append(result.ServerIP);
                string[] sConnectStatus = new string[256];
                sConnectStatus[0] = GetLanguage("Msg_66");
                sConnectStatus[1] = GetLanguage("Msg_67");
                sConnectStatus[2] = GetLanguage("Msg_68");
                sConnectStatus[255] = GetLanguage("Msg_69");

                strbuf.Append("," + GetLanguage("Msg_70") + ":").Append(sConnectStatus[result.ConnectStatus]);

                strbuf.AppendLine().Append(GetLanguage("Msg_71") + ":");
                if (result.LastKeepaliveTime != DateTime.MinValue)
                {
                    strbuf.Append(result.LastKeepaliveTime);
                }
                else
                {
                    strbuf.Append("-");
                }


                mMainForm.AddCmdLog(cmde, strbuf.ToString());
                Invoke(() =>
                {
                    cmbClientNetWorkMode.SelectedIndex = iModel;
                    txtServerStatus.Text = strbuf.ToString();
                });

            };
        }
        #endregion

        #region 客户端网络模式
        private void butReadClientWorkMode_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            var cmd = new ReadClientWorkMode(cmdDtl);
            mMainForm.AddCommand(cmd);

            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                var result = cmde.Command.getResult() as ReadClientWorkMode_Result;
                int iModel = result.ClientModel;
                Invoke(() =>
                {
                    cmbClientNetWorkMode.SelectedIndex = result.ClientModel;
                });
                var strbuf = new StringBuilder();

                strbuf.Append(GetLanguage("Msg_64") + ":").Append(ClientNetWorkMode[iModel]);

                mMainForm.AddCmdLog(cmde, strbuf.ToString());
                Invoke(() =>
                {
                    cmbClientNetWorkMode.SelectedIndex = iModel;
                });
            };
        }

        private void butWriteClientWorkMode_Click(object sender, EventArgs e)
        {
            int iModel = cmbClientNetWorkMode.SelectedIndex;
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            var par = new WriteClientWorkMode_Parameter(iModel);
            var cmd = new WriteClientWorkMode(cmdDtl, par);
            mMainForm.AddCommand(cmd);

            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                var strbuf = new StringBuilder();
                strbuf.Append(GetLanguage("Msg_64") + ":").Append(ClientNetWorkMode[iModel]);

                mMainForm.AddCmdLog(cmde, strbuf.ToString());
            };
        }

        #endregion

        #region 活体检测
        private void Btn_ReadFaceBioassay_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadFaceBioassay cmd = new ReadFaceBioassay(cmdDtl);
            mMainForm.AddCommand(cmd);
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadFaceBioassay_Result result = cmde.Command.getResult() as ReadFaceBioassay_Result;

                Invoke(() =>
                {
                    CmbFaceBioassay.SelectedIndex = result.BioassayType;
                    mMainForm.AddCmdLog(cmde, GetLanguage("LabFaceBioassay") + CmbFaceBioassay.Text);
                });
            };
        }

        private void Btn_WriteFaceBioassay_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            WriteFaceBioassay_Parameter parameter = new WriteFaceBioassay_Parameter(CmbFaceBioassay.SelectedIndex);
            WriteFaceBioassay cmd = new WriteFaceBioassay(cmdDtl, parameter);
            mMainForm.AddCommand(cmd);

        } 
        #endregion
    }
}
