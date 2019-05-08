using FCARDIO.Protocol.Door.FC8800.Holiday;
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
    public partial class frmHoliday : frmNodeForm
    {
        #region 单例模式

        private static object lockobj = new object();
        private static frmHoliday onlyObj;
        public static frmHoliday GetForm(INMain main)
        {
            if (onlyObj == null)
            {
                lock (lockobj)
                {
                    if (onlyObj == null)
                    {
                        onlyObj = new frmHoliday(main);
                        frmMain.AddNodeForms(onlyObj);
                    }
                }
            }
            return onlyObj;
        }
        #endregion

        private frmHoliday(INMain main)
        {
            InitializeComponent();
            mMainForm = main;
        }

        private void frmHoliday_Load(object sender, EventArgs e)
        {

        }

        #region 从控制板中读取节假日存储详情
        private void butReadHolidayDetail_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadHolidayDetail cmd = new ReadHolidayDetail(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadHolidayDetail_Result result = cmde.Command.getResult() as ReadHolidayDetail_Result;
                var dtl = result.Detail;
                string log =$"容量：{dtl.Capacity} ， 已用：{dtl.Count}" ;
                mMainForm.AddCmdLog(cmde, log);
            };
        }
        #endregion

        #region 清空节假日
        private void ClearHoliday_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ClearHoliday cmd = new ClearHoliday(cmdDtl);
            mMainForm.AddCommand(cmd);
            
        }
        #endregion

        #region 读取控制板中所有的节假日
        private void ReadAllHoliday_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadAllHoliday cmd = new ReadAllHoliday(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadAllHoliday_Result result = cmde.Command.getResult() as ReadAllHoliday_Result;

                string log = $"已读取到数量：{result.Count} ";
                mMainForm.AddCmdLog(cmde, log);
            };


        }
        #endregion
    }
}
