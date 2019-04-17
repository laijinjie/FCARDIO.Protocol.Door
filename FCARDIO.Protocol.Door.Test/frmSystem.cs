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
    }
}
