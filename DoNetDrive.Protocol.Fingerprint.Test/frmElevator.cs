using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DoNetDrive.Protocol.Fingerprint.Elevator;

namespace DoNetDrive.Protocol.Fingerprint.Test
{
    public partial class frmElevator : frmNodeForm
    {
        #region 单例模式
        private static object lockobj = new object();
        private static frmElevator onlyObj;
        public static frmElevator GetForm(INMain main)
        {
            if (onlyObj == null)
            {
                lock (lockobj)
                {
                    if (onlyObj == null)
                    {
                        onlyObj = new frmElevator(main);
                        frmMain.AddNodeForms(onlyObj);
                    }
                }
            }
            return onlyObj;
        }

        private frmElevator(INMain main) : base(main)
        {
            InitializeComponent();

        }


        #endregion

        private void btnReadPersonElevatorAccess_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadPersonElevatorAccess_Parameter par = new ReadPersonElevatorAccess_Parameter(6);

            ReadPersonElevatorAccess cmd = new ReadPersonElevatorAccess(cmdDtl, par);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadPersonElevatorAccess_Result result = cmde.Command.getResult() as ReadPersonElevatorAccess_Result;

                sb.Clear();
                sb.Append("用户号：").Append(result.UserCode)
                .Append(",状态：").Append(result.Status)
                .Append(",继电器列表：").AppendLine()
                .Append(string.Join(",", result.RelayAccesss.ToArray()));

                mMainForm.AddCmdLog(cmde, sb.ToString());
            };
        }

        private void btnWritePersonElevatorAccess_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            WritePersonElevatorAccess_Parameter par = new WritePersonElevatorAccess_Parameter()
            {
                UserCode = 5
            };
            for (int i = 0; i < 64; i++)
            {
                par.RelayAccesss.Add(0);

            }
            par.RelayAccesss[0] = 0;
            par.RelayAccesss[1] = 1;
            par.RelayAccesss[2] = 0;
            par.RelayAccesss[3] = 1;
            par.RelayAccesss[4] = 1;
            par.RelayAccesss[5] = 0;
            par.RelayAccesss[6] = 0;
            par.RelayAccesss[7] = 1;
            WritePersonElevatorAccess cmd = new WritePersonElevatorAccess(cmdDtl, par);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                StringBuilder sb = new StringBuilder();
                WritePersonElevatorAccess_Result result = cmde.Command.getResult() as WritePersonElevatorAccess_Result;

                sb.Clear();
                sb.Append("用户号：").Append(result.UserCode)
                .Append(",状态：").Append(result.Status);
                mMainForm.AddCmdLog(cmde, sb.ToString());
            };
        }
    }
}
