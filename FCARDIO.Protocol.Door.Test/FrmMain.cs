using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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

            mAllocator = ConnectorAllocator.GetAllocator();
            mObserver = new ConnectorObserverHandler();

            mAllocator.CommandCompleteEvent += mAllocator_CommandCompleteEvent;
            mAllocator.CommandErrorEvent += mAllocator_CommandErrorEvent;
            mAllocator.CommandProcessEvent += mAllocator_CommandProcessEvent;
            mAllocator.CommandTimeout += mAllocator_CommandTimeout;
            mAllocator.ConnectorConnectedEvent += mAllocator_ConnectorConnectedEvent;
            mAllocator.ConnectorClosedEvent += mAllocator_ConnectorClosedEvent;
            mAllocator.ConnectorErrorEvent += mAllocator_ConnectorErrorEvent;

            mObserver.DisposeRequestEvent += MObserver_DisposeRequestEvent;
            mObserver.DisposeResponseEvent += MObserver_DisposeResponseEvent; ;

            cmdConnType.Items.Add("TCP客户端");
            cmdConnType.SelectedIndex = 0;
            cmdProtocolType.Items.Add("FC8800系列");
            cmdProtocolType.SelectedIndex = 0;
            _IsClosed = false;
        }

        private void MObserver_DisposeResponseEvent(INConnector connector, string msg)
        {
            
            AddLog($"发送数据：{GetConnectorDetail(connector)} \r\n{msg}");

        }


        private void MObserver_DisposeRequestEvent(INConnector connector, string msg)
        {
            AddLog($"接收数据：{GetConnectorDetail(connector)} \r\n{msg}");
        }

        private void mAllocator_ConnectorErrorEvent(object sender, Core.Connector.INConnectorDetail connector)
        {
            AddLog($"连接超时：{GetConnectorDetail(connector)}");
        }

        private void mAllocator_ConnectorClosedEvent(object sender, Core.Connector.INConnectorDetail connector)
        {
            AddLog($"连接关闭：{GetConnectorDetail(connector)}");
        }

        private void mAllocator_ConnectorConnectedEvent(object sender, Core.Connector.INConnectorDetail connector)
        {
            mAllocator.GetConnector(connector).AddRequestHandle(mObserver);
            AddLog($"连接成功：{GetConnectorDetail(connector)}");
        }

        private void mAllocator_CommandTimeout(object sender, CommandEventArgs e)
        {
            AddLog($"命令超时：{GetConnectorDetail(e.ConnectorDetail)} {e.Command.ToString()}");
        }

        private void mAllocator_CommandProcessEvent(object sender, CommandEventArgs e)
        {
            AddLog($"命令进度：{GetConnectorDetail(e.ConnectorDetail)} {e.Command.ToString()} \r\n {e.Command.getProcessStep()} / {e.Command.getProcessMax()}");
        }

        private void mAllocator_CommandErrorEvent(object sender, CommandEventArgs e)
        {
            AddLog($"命令错误：{GetConnectorDetail(e.ConnectorDetail)} {e.Command.ToString()}");
        }

        private void mAllocator_CommandCompleteEvent(object sender, CommandEventArgs e)
        {
            AddLog($"命令完成：{GetConnectorDetail(e.ConnectorDetail)} {e.Command.ToString()}");
        }

        private string GetConnectorDetail(INConnector conn)
        {
            return GetConnectorDetail(conn.GetConnectorDetail());
        }
        private string GetConnectorDetail(INConnectorDetail conn)
        {
            string ret = string.Empty;

            switch (conn.GetTypeName())
            {
                case ConnectorType.TCPClient:
                    var tcpclient = conn as TCPClientDetail;
                    ret = $"通道类型：TCP客户端 本地IP：{tcpclient.LocalAddr}:{tcpclient.LocalPort} ,远端IP：{tcpclient.Addr}:{tcpclient.Port}";
                    break;
                case ConnectorType.TCPServerClient:
                    var tcpclientOnly = conn as TCPServerClientDetail_ReadOnly;
                    if (tcpclientOnly == null)
                    {
                        var tcd = conn as TCPServerClientDetail;

                        ret = $"通道类型：TCPServer_Client key：{tcd.Key}";
                    }
                    else
                        ret = $"通道类型：TCPServer_Client key：{tcpclientOnly.Key},远端IP：{tcpclientOnly.Remote.ToString()}";


                    break;
                case ConnectorType.UDPClient:
                    var udpOnly = conn as TCPClientDetail;
                    ret = $"类型：UDP 本地IP：{udpOnly.LocalAddr}:{udpOnly.LocalPort} ,远端IP：{udpOnly.Addr}:{udpOnly.Port}";
                    break;
                case ConnectorType.UDPServer:
                    var udpserver = conn as UDPServerDetail;
                    ret = $"类型：UDP 服务器  本地绑定IP：{udpserver.LocalAddr}:{udpserver.LocalPort}";
                    break;
                case ConnectorType.TCPServer:
                    var tcpserver = conn as TCPServerDetail;
                    ret = $"类型：TCP服务器  本地绑定IP：{tcpserver.LocalAddr}:{tcpserver.LocalPort}";
                    break;
                default:
                    ret = $"通道：{conn.GetKey()}";
                    break;
            }

            return $"{ret}:{DateTime.Now.ToTimeffff()}";

        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            //释放资源
            mAllocator.Dispose();
        }
        /// <summary>
        /// 显示日志
        /// </summary>
        /// <param name="s">需要显示的日志</param>
        public void AddLog(string s)
        {
            if (_IsClosed) return;
            if (txtLog.InvokeRequired)
            {
                txtLog.BeginInvoke((Action<string>)AddLog, s);
                return;
            }
            string log = txtLog.Text;
            if (log.Length > 20000)
            {
                log = log.Substring(0, 10000);
            }
            txtLog.Text = s + "\r\n" + log;

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
            switch (cmdConnType.SelectedIndex)
            {
                //TCP 客户端方式通讯
                case 0:
                    connectType = CommandDetailFactory.ConnectType.TCPClient;
                    addr = txtTCPClientAddr.Text;
                    if (!int.TryParse(txtTCPClientPort.Text, out port))
                    {
                        port = 8000;
                    }
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
            return CommandDetailFactory.CreateDetail(connectType, addr, port,
                protocolType, sn, password);

        }

        private void butClear_Click(object sender, EventArgs e)
        {

            txtLog.Text = string.Empty;
        }

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

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            _IsClosed = true;
            foreach (var frm in NodeForms)
            {
                frm.Dispose();
            }
            NodeForms.Clear();

        }
    }
}
