using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FCARDIO.Core;
using FCARDIO.Core.Command;
using FCARDIO.Core.Connector;
using FCARDIO.Core.Connector.TCPClient;
using FCARDIO.Core.Connector.TCPServer;
using FCARDIO.Core.Connector.TCPServer.Client;
using FCARDIO.Core.Connector.UDP;
using FCARDIO.Core.Extension;
using FCARDIO.Protocol.FC8800;

namespace FCARDIO.Protocol.Door.Test
{
    public partial class frmMain : Form, INMain
    {
        ConnectorAllocator mAllocator;
        ConnectorObserverHandler mObserver;
        private static HashSet<Form> NodeForms;
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

            mAllocator.ConnectorConnectedEvent += mAllocator_ConnectorConnectedEvent;
            mAllocator.ConnectorClosedEvent += mAllocator_ConnectorClosedEvent;
            mAllocator.ConnectorErrorEvent += mAllocator_ConnectorErrorEvent;

            mAllocator.ClientOnline += MAllocator_ClientOnline;
            mAllocator.ClientOffline += MAllocator_ClientOffline;

            mObserver.DisposeRequestEvent += MObserver_DisposeRequestEvent;
            mObserver.DisposeResponseEvent += MObserver_DisposeResponseEvent; ;

            IniConnTypeList();
            IniLstIO();
            InilstCommand();
            IniCommandClassNameList();
            Task.Run((Action)ShowCommandProcesslog);
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
            inc.AddRequestHandle(mObserver);
            switch (inc.GetConnectorType())
            {
                case ConnectorType.TCPServerClient://TCP客户端已连接
                    RemoveTCPServer_Client(inc.GetConnectorDetail());
                    break;
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
                case ConnectorType.TCPServerClient://TCP客户端已连接
                    AddTCPServer_Client(inc.GetConnectorDetail());
                    break;
                case ConnectorType.UDPClient://UDP客户端已连接
                    //AddUDPClient(inc.GetConnectorDetail());
                    break;
                default:
                    break;
            }
        }

        private void MObserver_DisposeResponseEvent(INConnector connector, string msg)
        {
            AddIOLog(connector.GetConnectorDetail(), "发送数据", msg);
        }


        private void MObserver_DisposeRequestEvent(INConnector connector, string msg)
        {
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
                case ConnectorType.TCPServer://TCP Server 服务器
                    Invoke(() => TCPServerBindOver(false));
                    AddIOLog(connector, "TCP服务", "TCP服务器开启失败");
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
                case ConnectorType.TCPServer://TCP Server 服务器
                    Invoke(() => TCPServerBindOver(false));
                    AddIOLog(connector, "TCP服务", "TCP服务已关闭");
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
                case ConnectorType.TCPServer://TCP Server 服务器
                    Invoke(() => TCPServerBindOver(true));
                    AddIOLog(connector, "TCP服务", "TCP服务已启动");
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
            if (e.Command.GetType().FullName == typeof(FC8800.SystemParameter.SearchControltor.SearchControltor).FullName)
            {
                AddCmdLog(e, "搜索完毕");
                return;
            }
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
            if (e.Command.GetType().FullName == typeof(FC8800.SystemParameter.SearchControltor.SearchControltor).FullName)
            {
                return;
            }
            mAllocator_CommandProcessEvent(sender, e);
            AddCmdLog(e, "命令完成");
            string cName = e.Command.GetType().FullName;

            switch (cName)
            {
                case Command_ReadSN://读SN
                    FC8800.SystemParameter.SN.SN_Result sn = e.Command.getResult() as FC8800.SystemParameter.SN.SN_Result;
                    Invoke(() => txtSN.Text = sn.SNBuf.GetString());
                    break;
                case Command_WriteSN://写SN
                case Command_WriteSN_Broadcast:
                    FC8800.SystemParameter.SN.SN_Parameter snPar = e.Command.Parameter as FC8800.SystemParameter.SN.SN_Parameter;
                    Invoke(() => txtSN.Text = snPar.SNBuf.GetString());
                    break;
                case Command_ReadConnectPassword://读通讯密码
                    FC8800.SystemParameter.ConnectPassword.Password_Result pwd = e.Command.getResult() as FC8800.SystemParameter.ConnectPassword.Password_Result;
                    Invoke(() => txtPassword.Text = pwd.Password);
                    break;
                case Command_WriteConnectPassword://写通讯密码
                    FC8800.SystemParameter.ConnectPassword.Password_Parameter pwdPar = e.Command.Parameter as FC8800.SystemParameter.ConnectPassword.Password_Parameter;
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
                    var tcpclientOnly = conn as TCPServerClientDetail_ReadOnly;
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
            if (lstCommand.InvokeRequired)
            {
                lstCommand.BeginInvoke((Action<string>)AddLog, s);
                return;
            }
            //string log = txtLog.Text;
            //if (log.Length > 20000)
            //{
            //    log = log.Substring(0, 10000);
            //}
            //txtLog.Text = s + "\r\n" + log;
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
                case 1://TCP 客户端方式通讯
                    connectType = CommandDetailFactory.ConnectType.TCPClient;
                    addr = txtTCPClientAddr.Text;
                    if (!int.TryParse(txtTCPClientPort.Text, out port))
                    {
                        port = 8000;
                    }
                    break;
                case 2://UDP 
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
                case 3://TCP服务器
                    if (!mTCPServerBind)
                    {
                        MsgErr("请先开启TCP服务");
                        return null;
                    }

                    connectType = CommandDetailFactory.ConnectType.TCPServerClient;
                    if (cmbTCPClient.SelectedItem == null)
                    {
                        MsgErr("请选择一个TCP客户端！");
                        return null;
                    }
                    TCPServerClientDetail_Item oItem = cmbTCPClient.SelectedItem as TCPServerClientDetail_Item;

                    addr = oItem.Key;
                    break;
                default:
                    break;
            }

            switch (cmdProtocolType.SelectedIndex)
            {
                case 0://FC8800系列协议 FC8800，MC5800
                    protocolType = CommandDetailFactory.ControllerType.FC88;

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

            return cmdDtl;

        }
        #endregion

        #region 通讯日志

        /// <summary>
        /// 初始化通讯日志列表
        /// </summary>
        private void IniLstIO()
        {
            lstIO.BeginUpdate();

            var cols = lstIO.Columns;
            cols.Clear();
            var sCaptions = "标签,内容,类型,远程信息,本地信息,时间".SplitTrim(",");
            var iWidths = new int[] { 60, 260, 90, 125, 125, 100 };
            for (int i = 0; i < sCaptions.Length; i++)
            {
                ColumnHeader col = new ColumnHeader();
                col.Text = sCaptions[i];
                col.TextAlign = HorizontalAlignment.Center;
                col.Width = iWidths[i];
                cols.Add(col);
            }
            lstIO.HideSelection = true;
            lstIO.LabelEdit = false;
            lstIO.MultiSelect = false;
            lstIO.FullRowSelect = true;
            lstIO.GridLines = true;
            lstIO.ShowItemToolTips = true;
            lstIO.EndUpdate();

            mIOItems = new ConcurrentQueue<ListViewItem>();
            Task.Run(() =>
            {
                do
                {
                    if (_IsClosed) break;
                    if (!mIOItems.IsEmpty)
                    {
                        Invoke(() =>
                        {
                            lstIO.BeginUpdate();

                            do
                            {
                                ListViewItem oItem;
                                if (mIOItems.TryDequeue(out oItem))
                                {
                                    lstIO.Items.Add(oItem);
                                }
                            } while (!mIOItems.IsEmpty);

                            lstIO.EndUpdate();
                        });

                    }
                    Sleep(500);
                    if (_IsClosed) break;
                } while (!_IsClosed);

                //Console.WriteLine("IniLstIO 刷新线程 已退出");
            });
        }

        private void Sleep(int time)
        {
            System.Threading.Thread.Sleep(time);
        }

        private ConcurrentQueue<ListViewItem> mIOItems;

        /// <summary>
        /// 添加一个通讯日志
        /// </summary>
        /// <param name="connDetail"></param>
        /// <param name="sTag">标签</param>
        /// <param name="txt">内容</param>
        public void AddIOLog(INConnectorDetail connDetail, string sTag, string txt)
        {
            string Local, Remote, cType;
            GetConnectorDetail(connDetail, out cType, out Local, out Remote);

            ListViewItem oItem = new ListViewItem();
            oItem.Text = sTag;
            oItem.SubItems.Add(new ListViewItem.ListViewSubItem(oItem, txt));
            oItem.SubItems.Add(new ListViewItem.ListViewSubItem(oItem, cType));
            oItem.SubItems.Add(new ListViewItem.ListViewSubItem(oItem, Remote));
            oItem.SubItems.Add(new ListViewItem.ListViewSubItem(oItem, Local));
            oItem.SubItems.Add(new ListViewItem.ListViewSubItem(oItem, DateTime.Now.ToTimeffff()));
            oItem.ToolTipText = txt;
            mIOItems.Enqueue(oItem);
        }


        /// <summary>
        /// 清空所有通讯日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butClear_Click(object sender, EventArgs e)
        {

            lstIO.Items.Clear();
        }
        #endregion

        #region 功能菜单

        private void butSystem_Click(object sender, EventArgs e)
        {
            frmSystem frm = frmSystem.GetForm(this);
            frm.Show();
        }

        private void butTime_Click(object sender, EventArgs e)
        {
            frmTime frm = frmTime.GetForm(this);
            frm.Show();
        }

        private void butDoor_Click(object sender, EventArgs e)
        {
            frmDoor frm = frmDoor.GetForm(this);
            frm.Show();

        }

        private void butHoliday_Click(object sender, EventArgs e)
        {

            frmHoliday frm = frmHoliday.GetForm(this);
            frm.Show();
        }

        private void ButPassword_Click(object sender, EventArgs e)
        {
            frmPassword frm = frmPassword.GetForm(this);
            frm.Show();
        }

        private void ButTimeGroup_Click(object sender, EventArgs e)
        {
            frmTimeGroup frm = frmTimeGroup.GetForm(this);
            frm.Show();
        }

        private void butCard_Click(object sender, EventArgs e)
        {
            frmCard frm = frmCard.GetForm(this);
            frm.Show();
        }

        private void butRecord_Click(object sender, EventArgs e)
        {
            frmRecord frm = frmRecord.GetForm(this);
            frm.Show();
        }

        private void butUploadSoftware_Click(object sender, EventArgs e)
        {
            frmUploadSoftware frm = frmUploadSoftware.GetForm(this);
            frm.Show();
        }
        #endregion

        /// <summary>
        /// 保存命令类型的功能名称
        /// </summary>
        private Dictionary<string, string> mCommandClasss;
        /// <summary>
        /// 初始化命令类型的功能名称
        /// </summary>
        private void IniCommandClassNameList()
        {
            mCommandClasss = new Dictionary<string, string>();

            mCommandClasss.Add(typeof(FC8800.SystemParameter.SN.ReadSN).FullName, "读取SN");
            mCommandClasss.Add(typeof(FC8800.SystemParameter.SN.WriteSN).FullName, "写SN");
            mCommandClasss.Add(typeof(FC8800.SystemParameter.SN.WriteSN_Broadcast).FullName, "广播写SN");

            mCommandClasss.Add(typeof(FC8800.SystemParameter.ConnectPassword.ReadConnectPassword).FullName, "获取通讯密码");
            mCommandClasss.Add(typeof(FC8800.SystemParameter.ConnectPassword.WriteConnectPassword).FullName, "设置通讯密码");
            mCommandClasss.Add(typeof(FC8800.SystemParameter.ConnectPassword.ResetConnectPassword).FullName, "重置通讯密码");

            mCommandClasss.Add(typeof(FC8800.SystemParameter.TCPSetting.ReadTCPSetting).FullName, "读取TCP参数");
            mCommandClasss.Add(typeof(FC8800.SystemParameter.TCPSetting.WriteTCPSetting).FullName, "写入TCP参数");

            mCommandClasss.Add(typeof(FC8800.SystemParameter.Deadline.ReadDeadline).FullName, "读取设备有效期");
            mCommandClasss.Add(typeof(FC8800.SystemParameter.Deadline.WriteDeadline).FullName, "写入设备有效期");

            mCommandClasss.Add(typeof(FC8800.SystemParameter.Version.ReadVersion).FullName, "读取设备版本号");

            mCommandClasss.Add(typeof(FC8800.SystemParameter.SystemStatus.ReadSystemStatus).FullName, "读取设备运行信息");

            mCommandClasss.Add(typeof(FC8800.SystemParameter.FunctionParameter.ReadRecordMode).FullName, "读取记录存储方式");
            mCommandClasss.Add(typeof(FC8800.SystemParameter.FunctionParameter.WriteRecordMode).FullName, "写入记录存储方式");

            mCommandClasss.Add(typeof(FC8800.SystemParameter.FunctionParameter.ReadKeyboard).FullName, "读取键盘开关");
            mCommandClasss.Add(typeof(FC8800.SystemParameter.FunctionParameter.WriteKeyboard).FullName, "设置键盘开关");

            mCommandClasss.Add(typeof(FC8800.SystemParameter.FunctionParameter.ReadLockInteraction).FullName, "读取互锁参数");
            mCommandClasss.Add(typeof(FC8800.SystemParameter.FunctionParameter.WriteLockInteraction).FullName, "设置互锁参数");

            mCommandClasss.Add(typeof(FC8800.SystemParameter.FunctionParameter.ReadFireAlarmOption).FullName, "读取消防报警参数");
            mCommandClasss.Add(typeof(FC8800.SystemParameter.FunctionParameter.WriteFireAlarmOption).FullName, "设置消防报警参数");

            mCommandClasss.Add(typeof(FC8800.SystemParameter.FunctionParameter.ReadOpenAlarmOption).FullName, "读取匪警报警参数");
            mCommandClasss.Add(typeof(FC8800.SystemParameter.FunctionParameter.WriteOpenAlarmOption).FullName, "设置匪警报警参数");

            mCommandClasss.Add(typeof(FC8800.SystemParameter.FunctionParameter.ReadReaderIntervalTime).FullName, "读取读卡间隔时间");
            mCommandClasss.Add(typeof(FC8800.SystemParameter.FunctionParameter.WriteReaderIntervalTime).FullName, "设置读卡间隔时间");

            mCommandClasss.Add(typeof(FC8800.SystemParameter.FunctionParameter.ReadBroadcast).FullName, "读取语音段开关");
            mCommandClasss.Add(typeof(FC8800.SystemParameter.FunctionParameter.WriteBroadcast).FullName, "设置语音段开关");

            mCommandClasss.Add(typeof(FC8800.SystemParameter.FunctionParameter.ReadReaderCheckMode).FullName, "读取读卡器校验");
            mCommandClasss.Add(typeof(FC8800.SystemParameter.FunctionParameter.WriteReaderCheckMode).FullName, "设置读卡器校验");

            mCommandClasss.Add(typeof(FC8800.SystemParameter.FunctionParameter.ReadBuzzer).FullName, "读取主板蜂鸣器");
            mCommandClasss.Add(typeof(FC8800.SystemParameter.FunctionParameter.WriteBuzzer).FullName, "设置主板蜂鸣器");

            mCommandClasss.Add(typeof(FC8800.SystemParameter.FunctionParameter.ReadSmogAlarmOption).FullName, "读取烟雾报警参数");
            mCommandClasss.Add(typeof(FC8800.SystemParameter.FunctionParameter.WriteSmogAlarmOption).FullName, "设置烟雾报警参数");

            mCommandClasss.Add(typeof(FC8800.SystemParameter.FunctionParameter.ReadEnterDoorLimit).FullName, "读取门内人数限制");
            mCommandClasss.Add(typeof(FC8800.SystemParameter.FunctionParameter.WriteEnterDoorLimit).FullName, "设置门内人数限制");

            mCommandClasss.Add(typeof(FC8800.SystemParameter.FunctionParameter.ReadTheftAlarmSetting).FullName, "读取智能防盗主机参数");
            mCommandClasss.Add(typeof(FC8800.SystemParameter.FunctionParameter.WriteTheftAlarmSetting).FullName, "设置智能防盗主机参数");

            mCommandClasss.Add(typeof(FC8800.SystemParameter.FunctionParameter.ReadCheckInOut).FullName, "读取防潜回模式");
            mCommandClasss.Add(typeof(FC8800.SystemParameter.FunctionParameter.WriteCheckInOut).FullName, "设置防潜回模式");

            mCommandClasss.Add(typeof(FC8800.SystemParameter.FunctionParameter.ReadCardPeriodSpeak).FullName, "读取卡片到期提示");
            mCommandClasss.Add(typeof(FC8800.SystemParameter.FunctionParameter.WriteCardPeriodSpeak).FullName, "设置卡片到期提示");

            mCommandClasss.Add(typeof(FC8800.SystemParameter.FunctionParameter.ReadReadCardSpeak).FullName, "读取定时读卡播报语音消息参数");
            mCommandClasss.Add(typeof(FC8800.SystemParameter.FunctionParameter.WriteReadCardSpeak).FullName, "设置定时读卡播报语音消息参数");

            mCommandClasss.Add(typeof(FC8800.SystemParameter.Watch.ReadWatchState).FullName, "读取实时监控状态");
            mCommandClasss.Add(typeof(FC8800.SystemParameter.Watch.BeginWatch).FullName, "开启实时监控");
            mCommandClasss.Add(typeof(FC8800.SystemParameter.Watch.CloseWatch).FullName, "关闭实时监控");
            mCommandClasss.Add(typeof(FC8800.SystemParameter.Watch.BeginWatch_Broadcast).FullName, "开启实时监控_广播");
            mCommandClasss.Add(typeof(FC8800.SystemParameter.Watch.CloseWatch_Broadcast).FullName, "关闭实时监控_广播");

            mCommandClasss.Add(typeof(FC8800.SystemParameter.FireAlarm.ReadFireAlarmState).FullName, "读取消防报警状态");
            mCommandClasss.Add(typeof(FC8800.SystemParameter.FireAlarm.SendFireAlarm).FullName, "消防报警通知");
            mCommandClasss.Add(typeof(FC8800.SystemParameter.FireAlarm.CloseFireAlarm).FullName, "解除消防报警");

            mCommandClasss.Add(typeof(FC8800.SystemParameter.SmogAlarm.ReadSmogAlarmState).FullName, "读取烟雾报警状态");
            mCommandClasss.Add(typeof(FC8800.SystemParameter.SmogAlarm.SendSmogAlarm).FullName, "烟雾报警通知");
            mCommandClasss.Add(typeof(FC8800.SystemParameter.SmogAlarm.CloseSmogAlarm).FullName, "解除烟雾报警");

            mCommandClasss.Add(typeof(FC8800.SystemParameter.Alarm.CloseAlarm).FullName, "解除报警");
            mCommandClasss.Add(typeof(FC8800.SystemParameter.WorkStatus.ReadWorkStatus).FullName, "获取设备状态信息");
            mCommandClasss.Add(typeof(FC8800.SystemParameter.WorkStatus.ReadTheftAlarmState).FullName, "获取防盗主机布防状态信息");

            mCommandClasss.Add(typeof(FC8800.SystemParameter.Controller.FormatController).FullName, "初始化数据");
            mCommandClasss.Add(typeof(FC8800.SystemParameter.SearchControltor.SearchControltor).FullName, "自动搜索设备");
            mCommandClasss.Add(typeof(FC8800.SystemParameter.SearchControltor.WriteControltorNetCode).FullName, "修改设备的网络代码");

            mCommandClasss.Add(typeof(FC8800.SystemParameter.CacheContent.ReadCacheContent).FullName, "读取缓存区内容");
            mCommandClasss.Add(typeof(FC8800.SystemParameter.CacheContent.WriteCacheContent).FullName, "设置缓存区内容");

            mCommandClasss.Add(typeof(FC8800.SystemParameter.KeepAliveInterval.ReadKeepAliveInterval).FullName, "读取保活间隔时间");
            mCommandClasss.Add(typeof(FC8800.SystemParameter.KeepAliveInterval.WriteKeepAliveInterval).FullName, "设置保活间隔时间");

            mCommandClasss.Add(typeof(FC8800.SystemParameter.TheftFortify.SetTheftFortify).FullName, "防盗报警布防");
            mCommandClasss.Add(typeof(FC8800.SystemParameter.TheftFortify.SetTheftDisarming).FullName, "防盗报警撤防");

            mCommandClasss.Add(typeof(FC8800.SystemParameter.BalcklistAlarmOption.ReadBalcklistAlarmOption).FullName, "读取黑名单报警参数");
            mCommandClasss.Add(typeof(FC8800.SystemParameter.BalcklistAlarmOption.WriteBalcklistAlarmOption).FullName, "设置黑名单报警功能");

            mCommandClasss.Add(typeof(FC8800.SystemParameter.ExploreLockMode.ReadExploreLockMode).FullName, "读取防探测功能开关参数");
            mCommandClasss.Add(typeof(FC8800.SystemParameter.ExploreLockMode.WriteExploreLockMode).FullName, "设置防探测功能开关");

            mCommandClasss.Add(typeof(FC8800.SystemParameter.Check485Line.ReadCheck485Line).FullName, "读取485线路反接检测开关");
            mCommandClasss.Add(typeof(FC8800.SystemParameter.Check485Line.WriteCheck485Line).FullName, "设置485线路反接检测开关");

            mCommandClasss.Add(typeof(FC8800.SystemParameter.TCPClient.ReadTCPClientList).FullName, "读取TCP客户端列表");
            mCommandClasss.Add(typeof(FC8800.SystemParameter.TCPClient.StopTCPClientConnection).FullName, "停止TCP客户端连接");
            mCommandClasss.Add(typeof(FC8800.SystemParameter.TCPClient.StopAllTCPClientConnection).FullName, "停止所有客户端连接");

            mCommandClasss.Add(typeof(FC8800.SystemParameter.CardDeadlineTipDay.ReadCardDeadlineTipDay).FullName, "读取有效期提醒阀值");
            mCommandClasss.Add(typeof(FC8800.SystemParameter.CardDeadlineTipDay.WriteCardDeadlineTipDay).FullName, "设置有效期提醒阀值");

            mCommandClasss.Add(typeof(FC8800.SystemParameter.ControlPanelTamperAlarm.ReadControlPanelTamperAlarm).FullName, "读取控制板防拆报警功能开关");
            mCommandClasss.Add(typeof(FC8800.SystemParameter.ControlPanelTamperAlarm.WriteControlPanelTamperAlarm).FullName, "设置控制板防拆报警功能开关");

            mCommandClasss.Add(typeof(FC8800.SystemParameter.HTTPPageLandingSwitch.ReadHTTPPageLandingSwitch).FullName, "读取HTTP网页登陆开关");
            mCommandClasss.Add(typeof(FC8800.SystemParameter.HTTPPageLandingSwitch.WriteHTTPPageLandingSwitch).FullName, "设置HTTP网页登陆开关");

            mCommandClasss.Add(typeof(FC8800.SystemParameter.LawfulCardReleaseAlarmSwitch.ReadLawfulCardReleaseAlarmSwitch).FullName, "读取合法卡解除报警开关");
            mCommandClasss.Add(typeof(FC8800.SystemParameter.LawfulCardReleaseAlarmSwitch.WriteLawfulCardReleaseAlarmSwitch).FullName, "设置合法卡解除报警开关");

            mCommandClasss.Add(typeof(FC8800.Time.ReadTime).FullName, "读系统时间");
            mCommandClasss.Add(typeof(FC8800.Time.WriteTime).FullName, "写系统时间");
            mCommandClasss.Add(typeof(FC8800.Time.WriteCustomTime).FullName, "写系统时间");
            mCommandClasss.Add(typeof(FC8800.Time.WriteTimeBroadcast).FullName, "写设备时间_广播命令");

            mCommandClasss.Add(typeof(FC8800.Time.TimeErrorCorrection.ReadTimeError).FullName, "读取误差自修正参数");
            mCommandClasss.Add(typeof(FC8800.Time.TimeErrorCorrection.WriteTimeError).FullName, "写入误差自修正参数");

            mCommandClasss.Add(typeof(FC8800.Door.ReaderOption.ReadReaderOption).FullName, "读取读卡器字节数");
            mCommandClasss.Add(typeof(FC8800.Door.ReaderOption.WriteReaderOption).FullName, "写入读卡器字节数");

            mCommandClasss.Add(typeof(FC8800.Door.RelayOption.ReadRelayOption).FullName, "读取继电器参数");
            mCommandClasss.Add(typeof(FC8800.Door.RelayOption.WriteRelayOption).FullName, "写入继电器参数");

            mCommandClasss.Add(typeof(FC8800.Door.Remote.OpenDoor).FullName, "远程开门");
            mCommandClasss.Add(typeof(FC8800.Door.Remote.CloseDoor).FullName, "远程关门");
            mCommandClasss.Add(typeof(FC8800.Door.Remote.HoldDoor).FullName, "设置门常开");
            mCommandClasss.Add(typeof(FC8800.Door.Remote.OpenDoor_CheckNum).FullName, "远程开门_验证");
            mCommandClasss.Add(typeof(FC8800.Door.Remote.LockDoor).FullName, "锁定门");
            mCommandClasss.Add(typeof(FC8800.Door.Remote.UnlockDoor).FullName, "解除锁定门");

            mCommandClasss.Add(typeof(FC8800.Door.ReaderWorkSetting.ReadReaderWorkSetting).FullName, "读取门认证方式");
            mCommandClasss.Add(typeof(FC8800.Door.ReaderWorkSetting.WriteReaderWorkSetting).FullName, "设置门认证方式");

            mCommandClasss.Add(typeof(FC8800.Holiday.ReadHolidayDetail).FullName, "读取节假日存储详情");
            mCommandClasss.Add(typeof(FC8800.Holiday.ClearHoliday).FullName, "清空节假日");
            mCommandClasss.Add(typeof(FC8800.Holiday.ReadAllHoliday).FullName, "读取所有节假日");

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
            cmdConnType.Items.AddRange("串口,TCP客户端,UDP,TCP服务器".SplitTrim(","));
            cmdConnType.SelectedIndex = 1;
            ShowConnTypePanel();

            cmdProtocolType.Items.Add("FC8800系列");
            cmdProtocolType.SelectedIndex = 0;
            _IsClosed = false;

            int iTop = gbTCPClient.Top, iLeft = gbTCPClient.Left;
            gbSerialPort.Top = iTop; gbSerialPort.Left = iLeft;
            gbServer.Top = iTop; gbServer.Left = iLeft;
            gbUDP.Top = iTop; gbUDP.Left = iLeft;

            IniSerialPortList();

            IniLoadLocalIP();

            TCPServerClients = new Dictionary<string, TCPServerClientDetail_Item>();
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
                cmbLocalIP.SelectedIndex = 0;
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
            bool[] pnlShow = new bool[4];
            pnlShow[cmdConnType.SelectedIndex] = true;

            GroupBox[] pnls = new GroupBox[] { gbSerialPort, gbTCPClient, gbUDP, gbServer };

            for (int i = 0; i < 4; i++)
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

        #region TCP 服务器
        /// <summary>
        /// TCPServer是否已绑定
        /// </summary>
        private bool mTCPServerBind;

        /// <summary>
        /// 包含所有客户端的项
        /// </summary>
        private Dictionary<string, TCPServerClientDetail_Item> TCPServerClients;

        /// <summary>
        /// 保存TCP客户端的详情
        /// </summary>
        private class TCPServerClientDetail_Item
        {
            /// <summary>
            /// 客户端的身份SN
            /// </summary>
            public string SN;
            /// <summary>
            /// 表示客户端的唯一Key
            /// </summary>
            public string Key;

            /// <summary>
            /// 客户端本地IP
            /// </summary>
            public IPDetail Local;

            /// <summary>
            /// 客户端的远程IP
            /// </summary>
            public IPDetail Remote;

            public TCPServerClientDetail_Item(TCPServerClientDetail_ReadOnly detail)
            {
                Key = detail.Key;
                Remote = new IPDetail(detail.Remote.Addr, detail.Remote.Port);
                Local = new IPDetail(detail.Local.Addr, detail.Local.Port);
            }

            public override string ToString()
            {
                if (string.IsNullOrEmpty(SN))
                {
                    return $"远程:{Remote.Addr}:{Remote.Port}";
                }
                else
                {
                    return $"{SN}({Remote.Addr}:{Remote.Port})";
                }

            }
        }

        private void butBeginTCPServer_Click(object sender, EventArgs e)
        {
            if (!txtServerPort.Text.IsNum())
            {
                MsgErr("端口号不正确！");
                return;
            }
            int port = txtServerPort.Text.ToInt32();
            string sLocalIP = cmbLocalIP.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(sLocalIP))
            {
                MsgErr("没有绑定本地IP！");
                return;
            }

            FCARDIO.Core.Connector.TCPServer.TCPServerDetail detail = new TCPServerDetail(sLocalIP, port);
            if (mTCPServerBind)
            {
                //关闭UDP服务器
                mAllocator.CloseConnector(detail);
                butBeginTCPServer.Text = "开启服务";
                mTCPServerBind = false;
                txtServerPort.Enabled = true;
                cmbLocalIP.Enabled = true;
            }
            else
            {
                butBeginTCPServer.Enabled = false;
                mTCPServerBind = true;
                txtServerPort.Enabled = false;
                cmbLocalIP.Enabled = false;

                //打开UDP服务器
                mAllocator.OpenConnector(detail);

                //等待后续事件，事件触发 mAllocator_ConnectorConnectedEvent 表示绑定成功
                //事件触发 mAllocator_ConnectorClosedEvent 表示绑定失败


            }

        }
        /// <summary>
        /// TCPServer绑定完毕
        /// </summary>
        /// <param name="bind">true 表示绑定成功</param>
        private void TCPServerBindOver(bool bind)
        {
            if (bind)
            {
                butBeginTCPServer.Text = "关闭服务";
            }
            else
            {
                mTCPServerBind = false;
                cmbLocalIP.Enabled = true;
                txtServerPort.Enabled = true;
            }

            butBeginTCPServer.Enabled = true;
        }

        /// <summary>
        /// 关闭一个已连接的TCP连接通道
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butCloseTCPClient_Click(object sender, EventArgs e)
        {
            TCPServerClientDetail_Item oItem = cmbTCPClient.SelectedItem as TCPServerClientDetail_Item;
            if (oItem == null)
            {
                MsgErr("请选择一个客户端！");
                return;
            }

            TCPServerClientDetail detail = new TCPServerClientDetail(oItem.Key);
            mAllocator.CloseConnector(detail);

        }

        /// <summary>
        /// 将客户端添加到列表中
        /// </summary>
        /// <param name="detail"></param>
        private void AddTCPServer_Client(INConnectorDetail detail)
        {
            if (cmbTCPClient.InvokeRequired)
            {
                Invoke(() => AddTCPServer_Client(detail));
                return;
            }
            TCPServerClientDetail_ReadOnly oClient = detail as TCPServerClientDetail_ReadOnly;
            var oItem = new TCPServerClientDetail_Item(oClient);

            cmbTCPClient.Items.Add(oItem);
            cmbTCPClient.SelectedIndex = cmbTCPClient.Items.Count - 1;
            TCPServerClients.Add(oItem.Key, oItem);

            AddIOLog(detail, "上线", "TCP 客户端已上线");
        }

        /// <summary>
        /// 从列表中删除TCP客户端
        /// </summary>
        /// <param name="detail"></param>
        private void RemoveTCPServer_Client(INConnectorDetail detail)
        {
            if (cmbTCPClient.InvokeRequired)
            {
                Invoke(() => RemoveTCPServer_Client(detail));
                return;
            }
            TCPServerClientDetail_ReadOnly oClient = detail as TCPServerClientDetail_ReadOnly;

            if (!TCPServerClients.ContainsKey(oClient.Key)) return;

            var oItem = TCPServerClients[oClient.Key];
            cmbTCPClient.Items.Remove(oItem);
            cmbTCPClient.SelectedIndex = cmbTCPClient.Items.Count - 1;
            TCPServerClients.Remove(oItem.Key);
            AddIOLog(detail, "离线", "TCP 客户端已离线");
        }

        #endregion


        #region 命令结果日志

        private void InilstCommand()
        {
            lstCommand.BeginUpdate();

            var cols = lstCommand.Columns;
            cols.Clear();
            var sCaptions = "类型,内容,身份信息,远程信息,时间,耗时".SplitTrim(",");
            var iWidths = new int[] { 100, 300, 120, 125, 100, 80 };
            for (int i = 0; i < sCaptions.Length; i++)
            {
                ColumnHeader col = new ColumnHeader();
                col.Text = sCaptions[i];
                col.TextAlign = HorizontalAlignment.Center;
                col.Width = iWidths[i];
                cols.Add(col);
            }
            lstCommand.HideSelection = true;
            lstCommand.LabelEdit = false;
            lstCommand.MultiSelect = false;
            lstCommand.FullRowSelect = true;
            lstCommand.GridLines = true;
            lstCommand.ShowItemToolTips = true;

            lstCommand.EndUpdate();
        }

        /// <summary>
        /// 添加命令日志
        /// </summary>
        /// <param name="e">命令描述符</param>
        /// <param name="txt">命令需要输出的内容</param>
        public void AddCmdLog(CommandEventArgs e, string txt)
        {
            ListViewItem oItem = new ListViewItem();

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
                    oItem.Text = mCommandClasss[sType];
                }
                else
                {
                    oItem.Text = sType;
                }

                oItem.SubItems.Add(new ListViewItem.ListViewSubItem(oItem, txt));
                string Local, Remote, cType;
                GetConnectorDetail(cmdDtl.Connector, out cType, out Local, out Remote);
                OnlineAccess.OnlineAccessCommandDetail fcDtl = cmdDtl as OnlineAccess.OnlineAccessCommandDetail;
                oItem.SubItems.Add(new ListViewItem.ListViewSubItem(oItem, fcDtl.SN));
                oItem.SubItems.Add(new ListViewItem.ListViewSubItem(oItem, Remote));
                oItem.SubItems.Add(new ListViewItem.ListViewSubItem(oItem, DateTime.Now.ToTimeffff()));
                oItem.SubItems.Add(new ListViewItem.ListViewSubItem(oItem, Timemill.ToString("0")));
                oItem.ToolTipText = txt;
            }
            else
            {
                oItem.Text = "-";
                oItem.SubItems.Add(new ListViewItem.ListViewSubItem(oItem, txt));
                oItem.SubItems.Add(new ListViewItem.ListViewSubItem(oItem, string.Empty));
                oItem.SubItems.Add(new ListViewItem.ListViewSubItem(oItem, string.Empty));
                oItem.SubItems.Add(new ListViewItem.ListViewSubItem(oItem, string.Empty));
                oItem.SubItems.Add(new ListViewItem.ListViewSubItem(oItem, string.Empty));
                oItem.ToolTipText = txt;
            }

            AddCmdItem(oItem);
        }

        private void AddCmdItem(ListViewItem oItem)
        {
            if (lstCommand.InvokeRequired)
            {
                Invoke(() => AddCmdItem(oItem));
                return;
            }
            lstCommand.BeginUpdate();
            lstCommand.Items.Insert(0, oItem);
            lstCommand.EndUpdate();
        }

        private void butClearCommand_Click(object sender, EventArgs e)
        {
            lstCommand.Items.Clear();
        }
        #endregion
    }
}
