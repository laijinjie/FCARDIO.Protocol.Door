using FCARDIO.Core;
using FCARDIO.Core.Command;
using FCARDIO.Core.Connector;
using FCARDIO.Core.Connector.TCPClient;
using FCARDIO.Core.Connector.TCPServer.Client;
using FCARDIO.Core.Connector.UDP;
using FCARDIO.Core.Extension;
using FCARDIO.Protocol.Door.FC8800.Data;
using FCARDIO.Protocol.Door.FC8800.SystemParameter.ConnectPassword;
using FCARDIO.Protocol.FC8800;
using FCARDIO.Protocol.Fingerprint.SystemParameter.Watch;
using FCARDIO.Protocol.Fingerprint.Test.Model;
using FCARDIO.Protocol.Transaction;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FCARDIO.Protocol.Door.FC8800.SystemParameter.SN;

namespace FCARDIO.Protocol.Fingerprint.Test
{
    public partial class frmMain : Form, INMain
    {
        ConnectorAllocator mAllocator;
        ConnectorObserverHandler mObserver;
        private static HashSet<Form> NodeForms;
        private static string[] TransactionTypeName;


        private void Invoke(Action p)
        {
            try
            {
                Invoke((Delegate)p);
            }
            catch (Exception)
            {

                return;
            }

        }

        static frmMain()
        {
            NodeForms = new HashSet<Form>();
            IniCommandClassNameList();

            TransactionTypeName = new string[255];
            TransactionTypeName[1] = "读卡记录";
            TransactionTypeName[2] = "出门开关记录";
            TransactionTypeName[3] = "门磁记录";
            TransactionTypeName[4] = "软件操作记录";
            TransactionTypeName[5] = "报警记录";
            TransactionTypeName[6] = "系统记录";
            TransactionTypeName[0x22] = "保活包";
        }

        public static void AddNodeForms(Form frm)
        {
            if (!NodeForms.Contains(frm))
            {
                NodeForms.Add(frm);
            }
        }


        bool _IsClosed;

        public frmMain()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗口初始化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmMain_Load(object sender, EventArgs e)
        {
            _IsClosed = false;
            Task.Run(() =>
            {
                System.Threading.Thread.Sleep(100);
                Invoke(new Action(IniForm));

            });
        }


        /// <summary>
        /// 窗体初始化
        /// </summary>
        public void IniForm()
        {
            if (_IsClosed) return;

            mAllocator = ConnectorAllocator.GetAllocator();
            mObserver = new ConnectorObserverHandler();

            mAllocator.CommandCompleteEvent += mAllocator_CommandCompleteEvent;
            mAllocator.CommandErrorEvent += mAllocator_CommandErrorEvent;
            mAllocator.CommandProcessEvent += mAllocator_CommandProcessEvent;
            mAllocator.CommandTimeout += mAllocator_CommandTimeout;
            mAllocator.AuthenticationErrorEvent += MAllocator_AuthenticationErrorEvent;

            mAllocator.TransactionMessage += MAllocator_TransactionMessage;

            mAllocator.ConnectorConnectedEvent += mAllocator_ConnectorConnectedEvent;
            mAllocator.ConnectorClosedEvent += mAllocator_ConnectorClosedEvent;
            mAllocator.ConnectorErrorEvent += mAllocator_ConnectorErrorEvent;

            mAllocator.ClientOnline += MAllocator_ClientOnline;
            mAllocator.ClientOffline += MAllocator_ClientOffline;

            mObserver.DisposeRequestEvent += MObserver_DisposeRequestEvent;
            mObserver.DisposeResponseEvent += MObserver_DisposeResponseEvent; 

            IniConnTypeList();
            IniLstIO();
            InilstCommand();

            Task.Run((Action)ShowCommandProcesslog);

            butUDPBind_Click(null, null);
        }




        #region 通道事件
        /// <summary>
        /// 客户端离线
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MAllocator_ClientOffline(object sender, ServerEventArgs e)
        {
            INConnector inc = sender as INConnector;
            inc.RemoveRequestHandle(typeof(ConnectorObserverHandler));
            inc.RemoveRequestHandle(typeof(FC8800RequestHandle));
            switch (inc.GetConnectorType())
            {
                case ConnectorType.UDPClient://UDP客户端已连接
                    //RemoveUDPClient(inc.GetConnectorDetail());
                    break;
                default:
                    break;
            }
        }



        /// <summary>
        /// 客户端上线
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MAllocator_ClientOnline(object sender, ServerEventArgs e)
        {
            INConnector inc = sender as INConnector;
            inc.AddRequestHandle(mObserver);
            switch (inc.GetConnectorType())
            {
                case ConnectorType.UDPClient://UDP客户端已连接

                    //inc.OpenForciblyConnect();
                    FC8800RequestHandle fC8800Request =
                        new FC8800RequestHandle(DotNetty.Buffers.UnpooledByteBufferAllocator.Default, RequestHandleFactory);
                    inc.RemoveRequestHandle(typeof(FC8800RequestHandle));//先删除，防止已存在就无法添加。
                    inc.AddRequestHandle(fC8800Request);

                    //AddUDPClient(inc.GetConnectorDetail());
                    break;
                default:
                    break;
            }
        }

        private void MObserver_DisposeResponseEvent(INConnector connector, string msg)
        {
            if (!mShowIOEvent) return;
            AddIOLog(connector.GetConnectorDetail(), "发送数据", msg);
        }


        private void MObserver_DisposeRequestEvent(INConnector connector, string msg)
        {
            if (!mShowIOEvent) return;
            AddIOLog(connector.GetConnectorDetail(), "接收数据", msg);
        }

        private void mAllocator_ConnectorErrorEvent(object sender, Core.Connector.INConnectorDetail connector)
        {
            switch (connector.GetTypeName())
            {
                case ConnectorType.UDPServer://UDP服务器
                    Invoke(() => UDPBindOver(false));
                    AddIOLog(connector, "UDP绑定", "UDP绑定失败");
                    break;
                default:
                    AddIOLog(connector, "错误", "连接失败");
                    break;
            }
        }

        private void mAllocator_ConnectorClosedEvent(object sender, Core.Connector.INConnectorDetail connector)
        {
            switch (connector.GetTypeName())
            {
                case ConnectorType.UDPServer://UDP服务器
                    Invoke(() => UDPBindOver(false));
                    AddIOLog(connector, "UDP绑定", "UDP绑定已关闭");
                    break;
                default:
                    AddIOLog(connector, "关闭", "连接通道已关闭");
                    break;
            }
        }

        private void mAllocator_ConnectorConnectedEvent(object sender, Core.Connector.INConnectorDetail connector)
        {
            switch (connector.GetTypeName())
            {
                case ConnectorType.UDPServer://UDP服务器
                    Invoke(() => UDPBindOver(true));
                    AddIOLog(connector, "UDP绑定", "UDP绑定成功");
                    break;
                default:
                    mAllocator.GetConnector(connector).AddRequestHandle(mObserver);

                    AddIOLog(connector, "成功", "通道连接成功");
                    break;
            }
        }

        #endregion

        #region 命令事件
        private void MAllocator_AuthenticationErrorEvent(object sender, CommandEventArgs e)
        {
            AddCmdLog(e, "通讯密码错误");
        }

        private void mAllocator_CommandTimeout(object sender, CommandEventArgs e)
        {
            //if (e.Command.GetType().FullName == typeof(FC8800.SystemParameter.SearchControltor.SearchControltor).FullName)
            //{
            //    AddCmdLog(e, "搜索完毕");
            //    return;
            //}
            AddCmdLog(e, "命令超时");
        }


        private void mAllocator_CommandErrorEvent(object sender, CommandEventArgs e)
        {
            AddCmdLog(e, "命令错误");
        }


        private const string Command_ReadSN = "FCARDIO.Protocol.Door.FC8800.SystemParameter.SN.ReadSN";
        private const string Command_WriteSN = "FCARDIO.Protocol.Door.FC8800.SystemParameter.SN.WriteSN";
        private const string Command_WriteSN_Broadcast = "FCARDIO.Protocol.Door.FC8800.SystemParameter.SN.WriteSN_Broadcast";
        private const string Command_ReadConnectPassword = "FCARDIO.Protocol.Door.FC8800.SystemParameter.ConnectPassword.ReadConnectPassword";
        private const string Command_WriteConnectPassword = "FCARDIO.Protocol.Door.FC8800.SystemParameter.ConnectPassword.WriteConnectPassword";
        private const string Command_ResetConnectPassword = "FCARDIO.Protocol.Door.FC8800.SystemParameter.ConnectPassword.ResetConnectPassword";

        private void mAllocator_CommandCompleteEvent(object sender, CommandEventArgs e)
        {
            /*
            if (e.Command.GetType().FullName == typeof(FC8800.SystemParameter.SearchControltor.SearchControltor).FullName)
            {
                return;
            }
            */
            mAllocator_CommandProcessEvent(sender, e);
            AddCmdLog(e, "命令完成");
            string cName = e.Command.GetType().FullName;
            /*   */
            var cmddtl = e.CommandDetail;
            switch (cName)
            {
                case Command_ReadSN://读SN
                    Protocol.Door.FC8800.SystemParameter.SN.SN_Result sn = e.Command.getResult() as Protocol.Door.FC8800.SystemParameter.SN.SN_Result;
                    Invoke(() => txtSN.Text = sn.SNBuf.GetString());
                    if (cmddtl.UserData != null)
                    {
                        string tmpStr = cmddtl.UserData as string;
                        if (tmpStr != null && tmpStr == "AutoReadSN")
                        {
                            FCARDIO.Protocol.OnlineAccess.OnlineAccessCommandDetail ocd = cmddtl as FCARDIO.Protocol.OnlineAccess.OnlineAccessCommandDetail;
                            ocd.SN = sn.SNBuf.GetString();
                            ReadConnectPassword cmd = new ReadConnectPassword(cmddtl);
                            AddCommand(cmd);
                        }
                        
                    }
                    break;

                case Command_ReadConnectPassword://读通讯密码
                    Protocol.Door.FC8800.SystemParameter.ConnectPassword.Password_Result pwd = e.Command.getResult() as Protocol.Door.FC8800.SystemParameter.ConnectPassword.Password_Result;
                    Invoke(() => txtPassword.Text = pwd.Password);
                    break;
                case Command_WriteConnectPassword://写通讯密码
                    Protocol.Door.FC8800.SystemParameter.ConnectPassword.Password_Parameter pwdPar = e.Command.Parameter as Protocol.Door.FC8800.SystemParameter.ConnectPassword.Password_Parameter;
                    Invoke(() => txtPassword.Text = pwdPar.Password);
                    break;
                case Command_ResetConnectPassword://复位通讯密码
                    Invoke(() => txtPassword.Text = "FFFFFFFF");
                    break;
                default:
                    break;
            }




        }
        #endregion

        #region 命令进度事件
        /// <summary>
        /// 命令日志
        /// </summary>
        private string mCommandProcessLog;
        /// <summary>
        /// 显示命令进度日志
        /// </summary>
        private void ShowCommandProcesslog()
        {
            do
            {
                if (_IsClosed) break;

                Invoke(() =>
                {
                    if (_IsClosed) return;
                    txtProcess.Text = mCommandProcessLog;

                });
                Sleep(300);
                if (_IsClosed) break;
            } while (!_IsClosed);
            // Console.WriteLine("ShowCommandProcesslog 已退出");
        }


        private void mAllocator_CommandProcessEvent(object sender, CommandEventArgs e)
        {
            var cmd = e.Command;
            string sName = cmd.GetType().FullName;
            if (mCommandClasss.ContainsKey(sName)) sName = mCommandClasss[sName];

            double time = 0;
            var dtl = e.CommandDetail;
            if (dtl.BeginTime != DateTime.MinValue && dtl.EndTime != DateTime.MinValue)
            {
                time = (dtl.EndTime - dtl.BeginTime).TotalMilliseconds;
            }
            else
            {
                if (dtl.BeginTime != DateTime.MinValue)
                    time = (DateTime.Now - e.CommandDetail.BeginTime).TotalMilliseconds;
            }


            string sLog = $"{sName} 已耗时：{time:0},进度：{cmd.getProcessStep()} / {cmd.getProcessMax()}";
            mCommandProcessLog = sLog;
        }



        #endregion

        #region 获取通道描述信息
        private string GetConnectorDetail(INConnector conn)
        {
            return GetConnectorDetail(conn.GetConnectorDetail());
        }
        private string GetConnectorDetail(INConnectorDetail conn)
        {
            string Local, Remote, cType;
            GetConnectorDetail(conn, out cType, out Local, out Remote);
            string ret = $"通道类型：{cType} 本地IP：{Local} ,远端IP：{Remote}";

            switch (conn.GetTypeName())
            {
                case ConnectorType.UDPServer:
                    ret = $"通道类型：{cType}  本地绑定IP：{Local}";
                    break;
                case ConnectorType.TCPServer:
                    ret = $"通道类型：{cType} 本地绑定IP：{Local}";
                    break;
                case ConnectorType.SerialPort:
                    ret = $"通道类型：{cType} {Local}";
                    break;
                default:
                    ret = $"通道类型：{cType} {Local}";
                    break;
            }

            return $"{ret}:{DateTime.Now.ToTimeffff()}";

        }

        /// <summary>
        /// 获取连接通道详情
        /// </summary>
        /// <param name="conn">连接通道描述符</param>
        /// <param name="Local">返回描述本地信息</param>
        /// <param name="Remote">返回描述远程信息</param>
        /// <returns></returns>
        private void GetConnectorDetail(INConnectorDetail conn, out string cType, out string Local, out string Remote)
        {
            Local = string.Empty;
            Remote = string.Empty;
            cType = string.Empty;

            var oConn = mAllocator.GetConnector(conn);
            if (oConn == null) return;

            IPDetail local = oConn.LocalAddress();
            conn = oConn.GetConnectorDetail();

            switch (conn.GetTypeName())
            {
                case ConnectorType.TCPClient:
                    var tcpclient = conn as TCPClientDetail;
                    cType = "TCP客户端";
                    Local = $"{local.ToString()}";
                    Remote = $"{tcpclient.Addr}:{tcpclient.Port}";
                    break;
                case ConnectorType.TCPServerClient:
                    cType = "TCP客户端节点";
                    var tcpclientOnly = conn as TCPServerClientDetail;
                    Local = $"{local.ToString()}";
                    Remote = $"{tcpclientOnly.Remote.ToString()}";
                    break;
                case ConnectorType.UDPClient:
                    cType = "UDP客户端";
                    var udpOnly = conn as TCPClientDetail;
                    Local = $"{local.ToString()}";
                    Remote = $"{udpOnly.Addr}:{udpOnly.Port}";
                    break;
                case ConnectorType.UDPServer:
                    cType = "UDP服务器";
                    Local = $"{local.ToString()}";
                    break;
                case ConnectorType.TCPServer:
                    cType = "TCP服务器";
                    Local = $"{local.ToString()}";
                    break;
                case ConnectorType.SerialPort:
                    cType = "串口";
                    var com = conn as Core.Connector.SerialPort.SerialPortDetail;
                    Local = $"COM{local.Port}:{com.Baudrate}";
                    break;
                default:
                    cType = conn.GetTypeName();
                    Local = $"{conn.GetKey()}";
                    break;
            }
        }
        #endregion

        #region 窗体关闭
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            _IsClosed = true;
            Sleep(500);
            foreach (var frm in NodeForms)
            {
                frm.Dispose();
            }
            NodeForms.Clear();
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            //释放资源
            mAllocator.Dispose();
            Sleep(500);
            //Sleep(50000);
        }
        #endregion


        #region 公共函数
        /// <summary>
        /// 显示日志
        /// </summary>
        /// <param name="s">需要显示的日志</param>
        public void AddLog(string s)
        {
            if (_IsClosed) return;

        }



        /// <summary>
        /// 显示日志
        /// </summary>
        /// <param name="s">需要显示的日志</param>
        public void AddLog(StringBuilder s)
        {
            if (_IsClosed) return;

            AddLog(s.ToString());
        }

        /// <summary>
        /// 将命令加入到分配器开始执行
        /// </summary>
        /// <param name="cmd"></param>
        public void AddCommand(INCommand cmd)
        {
            if (cmd.CommandDetail == null) return;
            mAllocator.AddCommand(cmd);
        }


        /// <summary>
        /// 获取一个命令详情，已经装配好通讯目标的所有信息
        /// </summary>
        /// <returns>命令详情</returns>
        public INCommandDetail GetCommandDetail()
        {
            if (_IsClosed) return null;
            CommandDetailFactory.ConnectType connectType = CommandDetailFactory.ConnectType.TCPClient;
            CommandDetailFactory.ControllerType protocolType = CommandDetailFactory.ControllerType.FC88;
            string addr = string.Empty, sn, password;
            int port = 0;
            switch (cmdConnType.SelectedIndex)//串口,TCP客户端,UDP,TCP服务器
            {
                case 0://串口
                    if (cmbSerialPort.SelectedIndex == -1)
                    {
                        MsgTip("请先选择一个串口号！");
                        return null;
                    }
                    connectType = CommandDetailFactory.ConnectType.SerialPort;
                    addr = string.Empty;
                    port = cmbSerialPort.Text.Substring(3).ToInt32();
                    break;

                case 1://UDP 
                    if (!mUDPIsBind)
                    {
                        MsgErr("请先绑定UDP端口");
                        return null;
                    }
                    connectType = CommandDetailFactory.ConnectType.UDPClient;
                    addr = txtUDPAddr.Text;
                    if (!int.TryParse(txtUDPPort.Text, out port))
                    {
                        port = 8000;
                    }
                    break;

                default:
                    break;
            }


            if (port > 65535) port = 8000;

            sn = txtSN.Text;
            if (string.IsNullOrEmpty(sn))
            {
                sn = "0000000000000000";
            }
            if (sn.GetBytes().Length != 16)
            {
                sn = "0000000000000000";
            }

            password = txtPassword.Text;
            if (!password.IsHex())
            {
                password = FC8800Command.NULLPassword;
            }
            if (password.Length != 8)
            {
                password = FC8800Command.NULLPassword;
            }



            var cmdDtl = CommandDetailFactory.CreateDetail(connectType, addr, port,
                protocolType, sn, password);

            if (connectType == CommandDetailFactory.ConnectType.UDPClient)
            {
                UDPClientDetail dtl = cmdDtl.Connector as UDPClientDetail;
                dtl.LocalAddr = cmbLocalIP.Text;
                dtl.LocalPort = txtUDPLocalPort.Text.ToInt32();
            }
            cmdDtl.Timeout = 600;
            cmdDtl.RestartCount = 3;
            return cmdDtl;

        }
        #endregion


        #region 通讯日志
        private bool mShowIOEvent = false;
        private void chkShowIO_CheckedChanged(object sender, EventArgs e)
        {
            mShowIOEvent = chkShowIO.Checked;
        }


        /// <summary>
        /// 初始化通讯日志列表
        /// </summary>
        private void IniLstIO()
        {
            mIOMessageList = new ConcurrentQueue<IOMessage>();
            Task.Run(() =>
            {
                do
                {
                    if (_IsClosed) break;
                    if (!mIOMessageList.IsEmpty)
                    {
                        Invoke(() =>
                        {
                            //lstIO.BeginUpdate();

                            do
                            {
                                IOMessage oItem;
                                if (mIOMessageList.TryDequeue(out oItem))
                                {
                                    dgvIO.Rows.Insert(0, oItem.Title, oItem.Content, oItem.Type, oItem.Time, oItem.Remote, oItem.Local);
                                    //int index = this.dgvIO.Rows.Add();
                                    //dgvIO.Rows[index].Cells[0].Value = oItem.Title.Title;
                                    //dgvIO.Rows[index].Cells[1].Value = oItem.Content;
                                    //dgvIO.Rows[index].Cells[2].Value = oItem.Type;
                                    //dgvIO.Rows[index].Cells[3].Value = oItem.Time;
                                    //dgvIO.Rows[index].Cells[4].Value = oItem.Remote;
                                    //dgvIO.Rows[index].Cells[5].Value = oItem.Local;
                                }
                            } while (!mIOMessageList.IsEmpty);

                            //lstIO.EndUpdate();
                        });

                    }
                    Sleep(1000);
                    if (_IsClosed) break;
                } while (!_IsClosed);

                //Console.WriteLine("IniLstIO 刷新线程 已退出");
            });
        }

        private void Sleep(int time)
        {
            System.Threading.Thread.Sleep(time);
        }

        private ConcurrentQueue<IOMessage> mIOMessageList;

        /// <summary>
        /// 添加一个通讯日志
        /// </summary>
        /// <param name="connDetail"></param>
        /// <param name="sTag">标签</param>
        /// <param name="txt">内容</param>
        public void AddIOLog(INConnectorDetail connDetail, string sTag, string txt)
        {
            if (!mShowIOEvent) return;

            string Local, Remote, cType;
            GetConnectorDetail(connDetail, out cType, out Local, out Remote);
            IOMessage iOMessage = new IOMessage();
            iOMessage.Title = sTag;
            if (txt.Length > 50)
            {
                txt = txt.Substring(0, 50) + "\r\n" + txt.Substring(50);
            }

            iOMessage.Content = txt;
            iOMessage.Type = cType;
            iOMessage.Remote = Remote;
            iOMessage.Local = Local;
            iOMessage.Time = DateTime.Now.ToTimeffff();

            mIOMessageList.Enqueue(iOMessage);
        }


        /// <summary>
        /// 清空所有通讯日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butClear_Click(object sender, EventArgs e)
        {
            dgvIO.Rows.Clear();
            //lstIO.Items.Clear();
        }
        #endregion

        #region 功能菜单

        private void butSystem_Click(object sender, EventArgs e)
        {
            frmSystem frm = frmSystem.GetForm(this);
            frm.Show();
            if (frm.WindowState == FormWindowState.Minimized)
                frm.WindowState = FormWindowState.Normal;
            frm.Activate();
            ShowFrm(frm);
        }

        private void butTime_Click(object sender, EventArgs e)
        {
            frmTime frm = frmTime.GetForm(this);
            frm.Show();
            if (frm.WindowState == FormWindowState.Minimized)
                frm.WindowState = FormWindowState.Normal;
            frm.Activate();
            ShowFrm(frm);
        }

        private void butDoor_Click(object sender, EventArgs e)
        {
            frmDoor frm = frmDoor.GetForm(this);
            frm.Show();
            if (frm.WindowState == FormWindowState.Minimized)
                frm.WindowState = FormWindowState.Normal;
            frm.Activate();
            ShowFrm(frm);
        }

        private void butHoliday_Click(object sender, EventArgs e)
        {

            frmHoliday frm = frmHoliday.GetForm(this);
            frm.Show();
            if (frm.WindowState == FormWindowState.Minimized)
                frm.WindowState = FormWindowState.Normal;
            frm.Activate();
            ShowFrm(frm);
        }

        private void ButPassword_Click(object sender, EventArgs e)
        {
            //frmPassword frm = frmPassword.GetForm(this);
            //frm.Show();
            //if (frm.WindowState == FormWindowState.Minimized)
            //    frm.WindowState = FormWindowState.Normal;
            //frm.Activate();
            //ShowFrm(frm);
        }

        private void ButTimeGroup_Click(object sender, EventArgs e)
        {
            frmTimeGroup frm = frmTimeGroup.GetForm(this);
            frm.Show();
            if (frm.WindowState == FormWindowState.Minimized)
                frm.WindowState = FormWindowState.Normal;
            frm.Activate();
            ShowFrm(frm);
        }

        private void butCard_Click(object sender, EventArgs e)
        {
            frmPerson frm = frmPerson.GetForm(this);
            frm.Show();
            if (frm.WindowState == FormWindowState.Minimized)
                frm.WindowState = FormWindowState.Normal;
            frm.Activate();
            ShowFrm(frm);

        }


        private void ButAlarm_Click(object sender, EventArgs e)
        {
            frmAlarm frm = frmAlarm.GetForm(this);
            frm.Show();
            if (frm.WindowState == FormWindowState.Minimized)
                frm.WindowState = FormWindowState.Normal;
            frm.Activate();
            ShowFrm(frm);
        }
        /// <summary>
        /// 显示窗口在侧边栏
        /// </summary>
        /// <param name="chi"></param>
        private void ShowFrm(Form chi)
        {
            Screen scr = Screen.PrimaryScreen;

            foreach (Screen ss in Screen.AllScreens)
            {
                var rc = ss.Bounds;
                if (rc.Left < this.Left && (rc.Left + rc.Width) > this.Left)
                {
                    if (rc.Top < this.Top && (rc.Top + rc.Bottom) > this.Top)
                    {
                        scr = ss;
                        break;
                    }
                }

            }

            var scrRc = scr.Bounds;
            int iLeft = (scrRc.Width - (Width + chi.Width)) / 2;
            int iTop = (scrRc.Height - (Height > chi.Height ? Height : chi.Height)) / 2;

            this.Left = scrRc.Left + iLeft;
            this.Top = scrRc.Top + iTop;

            chi.Left = scrRc.Left + iLeft + this.Width;
            chi.Top = scrRc.Top + iTop;


        }

        private void butRecord_Click(object sender, EventArgs e)
        {
            frmRecord frm = frmRecord.GetForm(this);
            frm.Show();
            if (frm.WindowState == FormWindowState.Minimized)
                frm.WindowState = FormWindowState.Normal;
            frm.Activate();
            ShowFrm(frm);
        }

        private void butUploadSoftware_Click(object sender, EventArgs e)
        {

        }

        private void ButAdditionalData_Click(object sender, EventArgs e)
        {
            frmAdditionalData frm = frmAdditionalData.GetForm(this);
            frm.Show();
            if (frm.WindowState == FormWindowState.Minimized)
                frm.WindowState = FormWindowState.Normal;
            frm.Activate();
            ShowFrm(frm);
        }
        #endregion

        /// <summary>
        /// 保存命令类型的功能名称
        /// </summary>
        private static Dictionary<string, string> mCommandClasss;

        /// <summary>
        /// 协议类型
        /// </summary>
        CommandDetailFactory.ControllerType[] mProtocolTypeTable = new CommandDetailFactory.ControllerType[3];
        /// <summary>
        /// 初始化命令类型的功能名称
        /// </summary>
        private static void IniCommandClassNameList()
        {
            mCommandClasss = new Dictionary<string, string>();

            mCommandClasss.Add(typeof(ReadSN).FullName, "读取SN");
            mCommandClasss.Add(typeof(WriteSN).FullName, "写SN");
            mCommandClasss.Add(typeof(WriteSN_Broadcast).FullName, "广播写SN");

            mCommandClasss.Add(typeof(ReadConnectPassword).FullName, "获取通讯密码");
            mCommandClasss.Add(typeof(WriteConnectPassword).FullName, "设置通讯密码");
            mCommandClasss.Add(typeof(ResetConnectPassword).FullName, "重置通讯密码");

            mCommandClasss.Add(typeof(Protocol.Door.FC8800.SystemParameter.TCPSetting.ReadTCPSetting).FullName, "读取TCP参数");
            mCommandClasss.Add(typeof(Protocol.Door.FC8800.SystemParameter.TCPSetting.WriteTCPSetting).FullName, "写入TCP参数");
            mCommandClasss.Add(typeof(Protocol.Door.FC8800.SystemParameter.KeepAliveInterval.ReadKeepAliveInterval).FullName, "读取保活间隔时间");
            mCommandClasss.Add(typeof(Protocol.Door.FC8800.SystemParameter.KeepAliveInterval.WriteKeepAliveInterval).FullName, "写入保活间隔时间");

            mCommandClasss.Add(typeof(SystemParameter.Version.ReadVersion).FullName, "读取设备版本号");
            mCommandClasss.Add(typeof(SystemParameter.SystemStatus.ReadSystemRunStatus).FullName, "获取设备运行信息");
            mCommandClasss.Add(typeof(SystemParameter.RecordMode.ReadRecordMode).FullName, "获取记录存储方式");
            mCommandClasss.Add(typeof(SystemParameter.RecordMode.WriteRecordMode).FullName, "设置记录存储方式");
            mCommandClasss.Add(typeof(SystemParameter.Watch.BeginWatch).FullName, "开启数据监控");
            mCommandClasss.Add(typeof(SystemParameter.Watch.CloseWatch).FullName, "关闭数据监控");
            mCommandClasss.Add(typeof(SystemParameter.Watch.ReadWatchState).FullName, "读取实时监控状态");
            mCommandClasss.Add(typeof(SystemParameter.SystemStatus.ReadSystemStatus).FullName, "读取设备状态信息");
            mCommandClasss.Add(typeof(FCARDIO.Protocol.Door.FC8800.SystemParameter.Controller.FormatController).FullName, "初始化设备");
            mCommandClasss.Add(typeof(Protocol.Door.FC8800.SystemParameter.SearchControltor.SearchControltor).FullName, "搜索控制器");
            mCommandClasss.Add(typeof(Protocol.Door.FC8800.SystemParameter.SearchControltor.WriteControltorNetCode).FullName, "根据SN设置网络标识");
            mCommandClasss.Add(typeof(SystemParameter.DataEncryptionSwitch.ReadDataEncryptionSwitch).FullName, "读取数据包加密开关");
            mCommandClasss.Add(typeof(SystemParameter.DataEncryptionSwitch.WriteDataEncryptionSwitch).FullName, "设置数据包加密开关");
            mCommandClasss.Add(typeof(SystemParameter.LocalIdentity.ReadLocalIdentity).FullName, "读取本机身份");
            mCommandClasss.Add(typeof(SystemParameter.LocalIdentity.WriteLocalIdentity).FullName, "设置本机身份");
            mCommandClasss.Add(typeof(SystemParameter.WiegandOutput.ReadWiegandOutput).FullName, "读取韦根输出");
            mCommandClasss.Add(typeof(SystemParameter.WiegandOutput.WriteWiegandOutput).FullName, "设置韦根输出");
            mCommandClasss.Add(typeof(SystemParameter.ComparisonThreshold.ReadComparisonThreshold).FullName, "读取人脸、指纹比对阈值");
            mCommandClasss.Add(typeof(SystemParameter.ComparisonThreshold.WriteComparisonThreshold).FullName, "设置人脸、指纹比对阈值");
            mCommandClasss.Add(typeof(SystemParameter.ScreenDisplayContent.ReadScreenDisplayContent).FullName, "读取屏幕显示内容");
            mCommandClasss.Add(typeof(SystemParameter.ScreenDisplayContent.WriteScreenDisplayContent).FullName, "设置屏幕显示内容");
            mCommandClasss.Add(typeof(SystemParameter.ManageMenuPassword.ReadManageMenuPassword).FullName, "读取管理菜单密码");
            mCommandClasss.Add(typeof(SystemParameter.ManageMenuPassword.WriteManageMenuPassword).FullName, "设置管理菜单密码");
            mCommandClasss.Add(typeof(SystemParameter.OEM.ReadOEM).FullName, "读取OEM信息");
            mCommandClasss.Add(typeof(SystemParameter.OEM.WriteOEM).FullName, "设置OEM信息");


            mCommandClasss.Add(typeof(Protocol.Door.FC8800.Time.ReadTime).FullName, "读系统时间");
            mCommandClasss.Add(typeof(Protocol.Door.FC8800.Time.WriteTime).FullName, "写系统时间");
            mCommandClasss.Add(typeof(Protocol.Door.FC8800.Time.WriteCustomTime).FullName, "写系统时间");
            mCommandClasss.Add(typeof(Protocol.Door.FC8800.Time.WriteTimeBroadcast).FullName, "写设备时间_广播命令");

            mCommandClasss.Add(typeof(Protocol.Door.FC8800.Holiday.ReadHolidayDetail).FullName, "从控制板中读取节假日存储详情");
            mCommandClasss.Add(typeof(Protocol.Door.FC8800.Holiday.ClearHoliday).FullName, "清空控制器中的所有节假日");
            mCommandClasss.Add(typeof(Protocol.Door.FC8800.Holiday.ReadAllHoliday).FullName, "读取控制板中已存储的所有节假日");
            mCommandClasss.Add(typeof(Protocol.Door.FC8800.Holiday.AddHoliday).FullName, "添加节假日到控制版");
            mCommandClasss.Add(typeof(Protocol.Door.FC8800.Holiday.DeleteHoliday).FullName, "从控制器删除节假日");


            mCommandClasss.Add(typeof(Protocol.Door.FC8800.TimeGroup.ClearTimeGroup).FullName, "清空所有开门时段");
            mCommandClasss.Add(typeof(Protocol.Door.FC8800.TimeGroup.ReadTimeGroup).FullName, "读取所有开门时段");
            mCommandClasss.Add(typeof(Protocol.Door.FC8800.TimeGroup.AddTimeGroup).FullName, "添加开门时段");

            mCommandClasss.Add(typeof(Transaction.TransactionDatabaseDetail.ReadTransactionDatabaseDetail).FullName, "读取控制器中的卡片数据库信息");
            mCommandClasss.Add(typeof(Transaction.ClearTransactionDatabase.ClearTransactionDatabase).FullName, "清空指定类型的记录数据库");
            mCommandClasss.Add(typeof(Transaction.ReadTransactionDatabaseByIndex.ReadTransactionDatabaseByIndex).FullName, "按指定序号读记录");
            mCommandClasss.Add(typeof(Transaction.ReadTransactionDatabase.ReadTransactionDatabase).FullName, "读取新记录");
            mCommandClasss.Add(typeof(Transaction.TransactionDatabaseReadIndex.WriteTransactionDatabaseReadIndex).FullName, "更新记录指针");
            mCommandClasss.Add(typeof(Transaction.WriteTransactionDatabaseWriteIndex.WriteTransactionDatabaseWriteIndex).FullName, "修改指定记录数据库的写索引");

            mCommandClasss.Add(typeof(Alarm.SendFireAlarm.WriteSendFireAlarm).FullName, "通知设备触发消防报警");
            mCommandClasss.Add(typeof(Alarm.BlacklistAlarm.ReadBlacklistAlarm).FullName, "读取黑名单报警");
            mCommandClasss.Add(typeof(Alarm.BlacklistAlarm.WriteBlacklistAlarm).FullName, "设置黑名单报警");
            mCommandClasss.Add(typeof(Alarm.AntiDisassemblyAlarm.ReadAntiDisassemblyAlarm).FullName, "读取防拆报警功能");
            mCommandClasss.Add(typeof(Alarm.AntiDisassemblyAlarm.WriteAntiDisassemblyAlarm).FullName, "设置防拆报警功能");
            mCommandClasss.Add(typeof(Alarm.IllegalVerificationAlarm.ReadIllegalVerificationAlarm).FullName, "读取非法验证报警");
            mCommandClasss.Add(typeof(Alarm.IllegalVerificationAlarm.WriteIllegalVerificationAlarm).FullName, "设置非法验证报警");
            mCommandClasss.Add(typeof(Alarm.AlarmPassword.ReadAlarmPassword).FullName, "读取胁迫报警密码");
            mCommandClasss.Add(typeof(Alarm.AlarmPassword.WriteAlarmPassword).FullName, "设置胁迫报警密码");
            mCommandClasss.Add(typeof(Alarm.OpenDoorTimeoutAlarm.ReadOpenDoorTimeoutAlarm).FullName, "读取开门超时报警参数");
            mCommandClasss.Add(typeof(Alarm.OpenDoorTimeoutAlarm.WriteOpenDoorTimeoutAlarm).FullName, "设置开门超时报警参数");
            mCommandClasss.Add(typeof(Alarm.GateMagneticAlarm.ReadGateMagneticAlarm).FullName, "读取门磁报警参数");
            mCommandClasss.Add(typeof(Alarm.GateMagneticAlarm.WriteGateMagneticAlarm).FullName, "设置门磁报警参数");
            mCommandClasss.Add(typeof(Alarm.LegalVerificationCloseAlarm.ReadLegalVerificationCloseAlarm).FullName, "读取合法验证解除报警开关");
            mCommandClasss.Add(typeof(Alarm.LegalVerificationCloseAlarm.WriteLegalVerificationCloseAlarm).FullName, "设置合法验证解除报警开关");
            mCommandClasss.Add(typeof(Alarm.CloseAlarm).FullName, "解除报警");

            mCommandClasss.Add(typeof(Door.ReaderOption.ReadReaderOption).FullName, "读取读卡器字节数");
            mCommandClasss.Add(typeof(Door.ReaderOption.WriteReaderOption).FullName, "设置读卡器字节数");
            mCommandClasss.Add(typeof(Door.RelayOption.ReadRelayOption).FullName, "读取继电器参数");
            mCommandClasss.Add(typeof(Door.RelayOption.WriteRelayOption).FullName, "设置继电器参数");
            mCommandClasss.Add(typeof(Door.Remote.CloseDoor).FullName, "远程关门");
            mCommandClasss.Add(typeof(Door.Remote.HoldDoor).FullName, "设置门常开");
            mCommandClasss.Add(typeof(Door.Remote.LockDoor).FullName, "锁定门");
            mCommandClasss.Add(typeof(Door.Remote.OpenDoor).FullName, "远程开门");
            mCommandClasss.Add(typeof(Door.Remote.UnlockDoor).FullName, "解除锁定门");
            mCommandClasss.Add(typeof(Door.DoorWorkSetting.ReadDoorWorkSetting).FullName, "读取门定时常开");
            mCommandClasss.Add(typeof(Door.DoorWorkSetting.WriteDoorWorkSetting).FullName, "设置门定时常开");
            mCommandClasss.Add(typeof(Door.RelayReleaseTime.ReadUnlockingTime).FullName, "获取开锁时输出时长");
            mCommandClasss.Add(typeof(Door.RelayReleaseTime.WriteUnlockingTime).FullName, "设置开锁时输出时长");
            mCommandClasss.Add(typeof(Door.ExemptionVerificationOpen.ReadExemptionVerificationOpen).FullName, "读取免验证开门");
            mCommandClasss.Add(typeof(Door.ExemptionVerificationOpen.WriteExemptionVerificationOpen).FullName, "设置免验证开门");
            mCommandClasss.Add(typeof(Door.VoiceBroadcastSetting.ReadVoiceBroadcastSetting).FullName, "读取语音播报功能");
            mCommandClasss.Add(typeof(Door.VoiceBroadcastSetting.WriteVoiceBroadcastSetting).FullName, "设置语音播报功能");
            mCommandClasss.Add(typeof(Door.ReaderIntervalTime.ReadReaderIntervalTime).FullName, "获取重复验证权限间隔");
            mCommandClasss.Add(typeof(Door.ReaderIntervalTime.WriteReaderIntervalTime).FullName, "设置重复验证权限间隔");
            mCommandClasss.Add(typeof(Door.ExpirationPrompt.ReadExpirationPrompt).FullName, "读取权限到期提示参数");
            mCommandClasss.Add(typeof(Door.ExpirationPrompt.WriteExpirationPrompt).FullName, "设置权限到期提示参数");

            mCommandClasss.Add(typeof(Person.PersonDatabaseDetail.ReadPersonDatabaseDetail).FullName, "读取人员存储详情");
            mCommandClasss.Add(typeof(Person.ClearPersonDataBase.ClearPersonDataBase).FullName, "清空所有人员");
            mCommandClasss.Add(typeof(Person.PersonDetail.ReadPersonDetail).FullName, "读取单个人员");
            mCommandClasss.Add(typeof(Person.DeletePerson.DeletePerson).FullName, "删除人员");
            mCommandClasss.Add(typeof(Person.AddPerson.AddPerson).FullName, "添加人员");

            mCommandClasss.Add(typeof(AdditionalData.WriteFeatureCode).FullName, "写入特征码");
            mCommandClasss.Add(typeof(AdditionalData.ReadFeatureCode).FullName, "读取特征码");
            mCommandClasss.Add(typeof(AdditionalData.ReadPersonDetail).FullName, "查询人员附加数据详情");
            mCommandClasss.Add(typeof(AdditionalData.DeleteFeatureCode).FullName, "删除特征码");
            mCommandClasss.Add(typeof(AdditionalData.ReadFile).FullName, "读文件");


            mCommandClasss.Add(typeof(Software.UpdateSoftware).FullName, "上传固件");
            mCommandClasss.Add(typeof(Software.UpdateSoftware_FP).FullName, "上传固件");
        }



        #region 串口管理


        /// <summary>
        /// 重新加载串口列表
        /// </summary>
        private void IniSerialPortList()
        {
            cmbSerialPort.Items.Clear();
            var Ports = System.IO.Ports.SerialPort.GetPortNames();
            if (Ports.Length > 0)
            {
                cmbSerialPort.Items.AddRange(Ports);
                cmbSerialPort.SelectedIndex = 0;
            }



        }

        private void butReloadSerialPort_Click(object sender, EventArgs e)
        {
            IniSerialPortList();
        }

        #endregion

        #region UDP
        /// <summary>
        /// UDP是否已绑定
        /// </summary>
        private bool mUDPIsBind = false;

        private void butUDPBind_Click(object sender, EventArgs e)
        {
            if (!txtUDPLocalPort.Text.IsNum())
            {
                MsgErr("端口号不正确！");
                return;
            }
            int port = txtUDPLocalPort.Text.ToInt32();
            string sLocalIP = cmbLocalIP.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(sLocalIP))
            {
                MsgErr("没有绑定本地IP！");
                return;
            }


            FCARDIO.Core.Connector.UDP.UDPServerDetail detail = new UDPServerDetail(sLocalIP, port);
            if (mUDPIsBind)
            {
                //关闭UDP服务器
                mAllocator.CloseConnector(detail);
                butUDPBind.Text = "开启服务";
                mUDPIsBind = false;
                txtUDPLocalPort.Enabled = true;
                cmbLocalIP.Enabled = true;
            }
            else
            {
                butUDPBind.Enabled = false;
                mUDPIsBind = true;
                txtUDPLocalPort.Enabled = false;
                cmbLocalIP.Enabled = false;
                //打开UDP服务器
                mAllocator.OpenConnector(detail);

                //等待后续事件，事件触发 mAllocator_ConnectorConnectedEvent 表示绑定成功
                //事件触发 mAllocator_ConnectorClosedEvent 表示绑定失败


            }
        }
        /// <summary>
        /// UDP绑定完毕
        /// </summary>
        /// <param name="bind">true 表示绑定成功</param>
        private void UDPBindOver(bool bind)
        {
            if (bind)
            {
                butUDPBind.Text = "关闭服务";
            }
            else
            {
                mUDPIsBind = false;
                cmbLocalIP.Enabled = true;
                txtUDPLocalPort.Enabled = true;
            }

            butUDPBind.Enabled = true;
        }
        #endregion

        #region 初始化通讯类型
        /// <summary>
        /// 初始化通讯类型列表
        /// </summary>
        private void IniConnTypeList()
        {
            cmdConnType.Items.AddRange("串口,UDP".SplitTrim(","));
            cmdConnType.SelectedIndex = 1;
            ShowConnTypePanel();


            _IsClosed = false;

            int iTop = gbSerialPort.Top, iLeft = gbSerialPort.Left;
            gbUDP.Top = iTop; gbUDP.Left = iLeft;

            IniSerialPortList();

            IniLoadLocalIP();

        }

        /// <summary>
        /// 初始化本机IP
        /// </summary>
        private void IniLoadLocalIP()
        {
            cmbLocalIP.Items.Clear();

            IPHostEntry localentry = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress oItem in localentry.AddressList)
            {
                IPAddress ip = oItem;

                if (ip.IsIPv4MappedToIPv6)
                {
                    ip = ip.MapToIPv4();
                }
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    cmbLocalIP.Items.Add(ip.ToString());
                }
            }

            if (cmbLocalIP.Items.Count > 0)
            {
                cmbLocalIP.SelectedIndex = cmbLocalIP.Items.Count - 1;
            }
        }

        private void cmdConnType_SelectedIndexChanged(object sender, EventArgs e)
        {

            ShowConnTypePanel();
        }
        /// <summary>
        /// 显示通讯方式面板
        /// </summary>
        private void ShowConnTypePanel()
        {
            bool[] pnlShow = new bool[2];
            pnlShow[cmdConnType.SelectedIndex] = true;

            GroupBox[] pnls = new GroupBox[] { gbSerialPort, gbUDP };

            for (int i = 0; i < 2; i++)
            {
                pnls[i].Visible = pnlShow[i];
            }
        }

        #endregion

        #region 提示框
        public void MsgTip(string sText)
        {
            MessageBox.Show(sText, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void MsgErr(string sText)
        {
            MessageBox.Show(sText, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        #endregion


        #region 命令结果日志

        private void InilstCommand()
        {

        }

        /// <summary>
        /// 添加命令日志
        /// </summary>
        /// <param name="e">命令描述符</param>
        /// <param name="txt">命令需要输出的内容</param>
        public void AddCmdLog(CommandEventArgs e, string txt)
        {
            CommandResult commandResult = new CommandResult();
            INCommandDetail cmdDtl = e?.CommandDetail; string sType = e?.Command.GetType().FullName;
            if (_IsClosed) return;
            if (e != null)
            {
                double Timemill = 0;
                if (cmdDtl.EndTime == DateTime.MinValue || cmdDtl.BeginTime == DateTime.MinValue)
                {
                    Timemill = 0;
                }
                else
                {
                    Timemill = (cmdDtl.EndTime - cmdDtl.BeginTime).TotalMilliseconds;//命令耗时毫秒数
                }

                if (mCommandClasss.ContainsKey(sType))
                {
                    commandResult.Title = mCommandClasss[sType];
                }
                else
                {
                    commandResult.Title = sType;
                }
                commandResult.Content = txt;
                string Local, Remote, cType;
                GetConnectorDetail(cmdDtl.Connector, out cType, out Local, out Remote);
                OnlineAccess.OnlineAccessCommandDetail fcDtl = cmdDtl as OnlineAccess.OnlineAccessCommandDetail;
                commandResult.SN = fcDtl.SN;
                commandResult.Remote = Remote;
                commandResult.Time = DateTime.Now.ToTimeffff();
                commandResult.Timemill = Timemill.ToString("0");
            }
            else
            {
                commandResult.Title = "-";
                commandResult.Content = txt;
            }
            AddCmdItem(commandResult);
        }

        private void AddCmdItem(CommandResult commandResult)
        {
            if (dgvResult.InvokeRequired)
            {
                Invoke(() => AddCmdItem(commandResult));
                return;
            }
            dgvResult.Rows.Insert(0, commandResult.Title, commandResult.Content, commandResult.SN, commandResult.Remote, commandResult.Time, commandResult.Timemill);
            //int index = this.dgvResult.Rows.Add();
            //dgvResult.Rows[index].Cells[0].Value = commandResult.Title;
            //dgvResult.Rows[index].Cells[1].Value = commandResult.Content;
            //dgvResult.Rows[index].Cells[2].Value = commandResult.SN;
            //dgvResult.Rows[index].Cells[3].Value = commandResult.Remote;
            //dgvResult.Rows[index].Cells[4].Value = commandResult.Time;
            //dgvResult.Rows[index].Cells[5].Value = commandResult.Timemill;
        }

        private void butClearCommand_Click(object sender, EventArgs e)
        {
            dgvResult.Rows.Clear();
        }
        #endregion



        #region 数据监控


        private void buWatch_Click(object sender, EventArgs e)
        {
            var cmdDtl = GetCommandDetail();
            if (cmdDtl == null) return;

            INConnector cnt = mAllocator.GetConnector(cmdDtl.Connector);
            if (cnt == null)
            {
                //未开启监控
                mAllocator.OpenConnector(cmdDtl.Connector);
                cnt = mAllocator.GetConnector(cmdDtl.Connector);

            }
            BeginWatch cmd = new BeginWatch(cmdDtl);
            AddCommand(cmd);
            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                AddCmdLog(cmde, "已开启监控");
            };

            //使通道保持连接不关闭
            cnt.OpenForciblyConnect();
            FC8800RequestHandle fC8800Request =
                new FC8800RequestHandle(DotNetty.Buffers.UnpooledByteBufferAllocator.Default, RequestHandleFactory);
            cnt.RemoveRequestHandle(typeof(FC8800RequestHandle));//先删除，防止已存在就无法添加。
            cnt.AddRequestHandle(fC8800Request);



        }

        /// <summary>
        /// 用于根据SN，命令参数、命令索引生产用于处理对应消息的处理类工厂函数
        /// </summary>
        /// <param name="sn"></param>
        /// <param name="cmdIndex"></param>
        /// <param name="cmdPar"></param>
        /// <returns></returns>
        private AbstractTransaction RequestHandleFactory(string sn, byte cmdIndex, byte cmdPar)
        {

            //在这里需要根据SN进行类型判定，也可以根据SN来进行查表
            if (cmdIndex >= 1 && cmdIndex <= 3)
            {
                return Transaction.ReadTransactionDatabaseByIndex.ReadTransactionDatabaseByIndex.NewTransactionTable[cmdIndex]();
            }
            if (cmdIndex == 0x22)
            {
                return new FCARDIO.Protocol.Door.FC8800.Data.Transaction.ConnectMessageTransaction();
            }
            return null;
        }

        /// <summary>
        /// 监控消息
        /// </summary>
        /// <param name="connector"></param>
        /// <param name="EventData"></param>
        private void MAllocator_TransactionMessage(INConnectorDetail connector, Core.Data.INData EventData)
        {
            CommandResult commandResult = new CommandResult();

            if (_IsClosed) return;

            FC8800Transaction fcTrn = EventData as FC8800Transaction;
            StringBuilder strbuf = new StringBuilder();
            var evn = fcTrn.EventData;


            //消息类型
            commandResult.Title = TransactionTypeName[fcTrn.CmdIndex];
            commandResult.SN = fcTrn.SN;

            //客户端信息
            string Local, Remote, cType;
            GetConnectorDetail(connector, out cType, out Local, out Remote);

            commandResult.Remote = Remote;

            commandResult.Time = fcTrn.EventData.TransactionDate.ToDateTimeStr();
            commandResult.Timemill = "-";


            if (fcTrn.CmdIndex <= 3)
            {
                strbuf.Append("事件：").Append(evn.TransactionCode);

                string[] codeNameList = frmRecord.mTransactionCodeNameList[evn.TransactionType];
                strbuf.Append("(").Append(codeNameList[evn.TransactionCode]).Append(")");
                if (fcTrn.CmdIndex == 1)
                {
                    Data.Transaction.CardTransaction cardtrn = evn as Data.Transaction.CardTransaction;
                    strbuf.Append("；用户号：").Append(cardtrn.UserCode.ToString()).Append("；出/入：").Append(cardtrn.Accesstype.ToString());
                    strbuf.Append("，照片：").AppendLine(cardtrn.Photo == 1 ? "有" : "无");
                }
                if (fcTrn.CmdIndex == 2)
                {
                    AbstractDoorTransaction cardtrn = evn as AbstractDoorTransaction;
                    strbuf.Append("；门号：").Append(cardtrn.Door);
                }

            }
            commandResult.Content = strbuf.ToString();

            AddCmdItem(commandResult);
        }



        #endregion

        private void DgvIO_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewTextBoxColumn textbox = dgvIO.Columns[e.ColumnIndex] as DataGridViewTextBoxColumn;
            if (textbox != null) //如果该列是TextBox列
            {
                dgvIO.BeginEdit(true); //开始编辑状态
                dgvIO.ReadOnly = false;
            }
        }

        private void ButReadSN_Click(object sender, EventArgs e)
        {
            var cmdDtl = GetCommandDetail();
            if (cmdDtl == null) return;

            cmdDtl.UserData = "AutoReadSN";
            ReadSN cmd = new ReadSN(cmdDtl);
            AddCommand(cmd);


        }
    }
}
