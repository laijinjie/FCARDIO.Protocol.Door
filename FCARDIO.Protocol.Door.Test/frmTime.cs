using FCARDIO.Protocol.Door.FC8800.Time;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FCARDIO.Protocol.Door.Test
{
    public partial class frmTime : frmNodeForm
    {
        #region 单例模式
        private static object lockobj = new object();
        private static frmTime onlyObj;
        public static frmTime GetForm(INMain main)
        {
            if (onlyObj == null)
            {
                lock (lockobj)
                {
                    if (onlyObj == null)
                    {
                        onlyObj = new frmTime(main);
                        frmMain.AddNodeForms(onlyObj);
                    }
                }
            }
            return onlyObj;
        }

        private frmTime(INMain main) : base(main)
        {
            InitializeComponent();
        }
        #endregion

        private void frmTime_Load(object sender, EventArgs e)
        {

        }

        #region 设备时间读写
        private void BtnReadSystemTime_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadTime cmd = new ReadTime(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadTime_Result result = cmde.Command.getResult() as ReadTime_Result;
                string ControllerDate = result.ControllerDate.ToString("yyyy-MM-dd HH:mm:ss"); //设备时间
                int Seconds = 0; //误差秒数
                string tip = string.Empty;
                Invoke(() =>
                {
                    txtSystemTime.Text = ControllerDate;
                    txtComputerTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    Seconds = Convert.ToDateTime(txtComputerTime.Text).Subtract(result.ControllerDate).Seconds;
                    if (Seconds < 4)
                    {
                        tip = "无误差";
                    }
                    else
                    {
                        tip = "误差" + Seconds + "秒";
                    }
                    txtErrorTime.Text = tip;
                });
                string ControllerDateInfo = "电脑时间：" + txtComputerTime.Text +
                                            "，控制板时间：" + ControllerDate + "，" + tip;
                mMainForm.AddCmdLog(cmde, ControllerDateInfo);
            };
        }

        private void BtnWriteSystemTime_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            WriteTime cmd = new WriteTime(cmdDtl);
            mMainForm.AddCommand(cmd);
        }

        private void BtnWriteBroadcastTime_Click(object sender, EventArgs e)
        {

        }
        #endregion


    }
}
