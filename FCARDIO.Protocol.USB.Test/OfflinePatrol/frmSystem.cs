
using FCARDIO.Protocol.USB.OfflinePatrol.SystemParameter.SN;
using System;
using System.Windows.Forms;

namespace FCARDIO.Protocol.USB.OfflinePatrol.Test
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
                        FrmMain.AddNodeForms(onlyObj);
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


        #region 地址码
        private void BtnReadSN_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadSN cmd = new ReadSN(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                SN_Result result = cmde.Command.getResult() as SN_Result;
                string sn = result.SN.ToString();
                Invoke(() =>
                {
                    txtAddress.Text = sn;
                });
                mMainForm.AddCmdLog(cmde, sn);
            };
        }

        private void BtnWriteSN_Click(object sender, EventArgs e)
        {

        }

        #endregion
        /// <summary>
        /// 读取 版本号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnReadVersion_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 读取 截止日期
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnReadExpireTime_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 写入 截止日期
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnWriteExpireTime_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 读取运行信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnReadSystemStatus_Click(object sender, EventArgs e)
        {

        }

        #region 开机保持
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnReadStartupHoldTime_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnWriteStartupHoldTime_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region 出厂日期
        private void ReadCreateTime_Click(object sender, EventArgs e)
        {

        }

        private void WriteCreateTime_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region LED开灯保持
        private void ReadLEDOpenTime_Click(object sender, EventArgs e)
        {

        }

        private void WriteLEDOpenTime_Click(object sender, EventArgs e)
        {

        }
        #endregion
    }
}
