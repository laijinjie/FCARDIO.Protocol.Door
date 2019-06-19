using FCARDIO.Protocol.Door.FC8800.Data.TimeGroup;
using FCARDIO.Protocol.Door.FC8800.TimeGroup;
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
    public partial class frmTimeGroup : Form
    {
        private static object lockobj = new object();
        private static frmTimeGroup onlyObj;

        /// <summary>
        /// 64个
        /// </summary>
        List<WeekTimeGroup> ListWeekTimeGroup = new List<WeekTimeGroup>();
        public static frmTimeGroup GetForm(INMain main)
        {
            if (onlyObj == null)
            {
                lock (lockobj)
                {
                    if (onlyObj == null)
                    {
                        onlyObj = new frmTimeGroup(main);
                        frmMain.AddNodeForms(onlyObj);
                    }
                }
            }
            return onlyObj;
        }

        INMain mMainForm;

        private frmTimeGroup(INMain main)
        {
            InitializeComponent();
            mMainForm = main;
        }

        private void frmTimeGroup_Load(object sender, EventArgs e)
        {

        }

        private void FrmTimeGroup_FormClosed(object sender, FormClosedEventArgs e)
        {
            onlyObj = null;
        }

        /// <summary>
        /// 采集开门时段 事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnReadTimeGroup_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadTimeGroup cmd = new ReadTimeGroup(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadTimeGroup_Result result = cmde.Command.getResult() as ReadTimeGroup_Result;
                ListWeekTimeGroup = result.ListWeekTimeGroup;


                BindTimeSegment();
                //dataGridView1
                string log = $"已读取到数量：{result.Count} ";
                mMainForm.AddCmdLog(cmde, log);
            };
        }

        private void BindTimeSegment()
        {
            DateTime nw = DateTime.Now;
            WeekTimeGroup tg = ListWeekTimeGroup[0];
            var day = tg.GetItem(0);
            for (int j = 0; j < 8; j++)
            {
                var tz = day.GetItem(j) as TimeSegment;
                if (j == 0)
                {
                    tz.SetBeginTime(beginTimePicker1.Text == "" ? 0 : DateTime.Parse(beginTimePicker1.Text).Hour, beginTimePicker1.Text == "" ? 0 : DateTime.Parse(beginTimePicker1.Text).Minute);
                    tz.SetEndTime(endTimePicker1.Text == "" ? 0 : DateTime.Parse(endTimePicker1.Text).Hour, endTimePicker1.Text == "" ? 0 : DateTime.Parse(endTimePicker1.Text).Minute);
                }
                else if (j == 1)
                {
                    tz.SetBeginTime(beginTimePicker2.Text == "" ? 0 : DateTime.Parse(beginTimePicker2.Text).Hour, beginTimePicker1.Text == "" ? 0 : DateTime.Parse(beginTimePicker2.Text).Minute);
                    tz.SetEndTime(endTimePicker2.Text == "" ? 0 : DateTime.Parse(endTimePicker2.Text).Hour, endTimePicker1.Text == "" ? 0 : DateTime.Parse(endTimePicker2.Text).Minute);
                }
                else if (j == 2)
                {
                    tz.SetBeginTime(beginTimePicker3.Text == "" ? 0 : DateTime.Parse(beginTimePicker3.Text).Hour, beginTimePicker1.Text == "" ? 0 : DateTime.Parse(beginTimePicker3.Text).Minute);
                    tz.SetEndTime(endTimePicker3.Text == "" ? 0 : DateTime.Parse(endTimePicker3.Text).Hour, endTimePicker1.Text == "" ? 0 : DateTime.Parse(endTimePicker3.Text).Minute);
                }
                else if (j == 3)
                {
                    tz.SetBeginTime(beginTimePicker4.Text == "" ? 0 : DateTime.Parse(beginTimePicker4.Text).Hour, beginTimePicker1.Text == "" ? 0 : DateTime.Parse(beginTimePicker4.Text).Minute);
                    tz.SetEndTime(endTimePicker4.Text == "" ? 0 : DateTime.Parse(endTimePicker4.Text).Hour, endTimePicker1.Text == "" ? 0 : DateTime.Parse(endTimePicker4.Text).Minute);
                }
                else if (j == 4)
                {
                    tz.SetBeginTime(beginTimePicker5.Text == "" ? 0 : DateTime.Parse(beginTimePicker5.Text).Hour, beginTimePicker1.Text == "" ? 0 : DateTime.Parse(beginTimePicker5.Text).Minute);
                    tz.SetEndTime(endTimePicker5.Text == "" ? 0 : DateTime.Parse(endTimePicker5.Text).Hour, endTimePicker1.Text == "" ? 0 : DateTime.Parse(endTimePicker5.Text).Minute);
                }
                else if (j == 5)
                {
                    tz.SetBeginTime(beginTimePicker6.Text == "" ? 0 : DateTime.Parse(beginTimePicker6.Text).Hour, beginTimePicker1.Text == "" ? 0 : DateTime.Parse(beginTimePicker6.Text).Minute);
                    tz.SetEndTime(endTimePicker6.Text == "" ? 0 : DateTime.Parse(endTimePicker6.Text).Hour, endTimePicker1.Text == "" ? 0 : DateTime.Parse(endTimePicker6.Text).Minute);
                }
                else if (j == 6)
                {
                    tz.SetBeginTime(beginTimePicker7.Text == "" ? 0 : DateTime.Parse(beginTimePicker7.Text).Hour, beginTimePicker1.Text == "" ? 0 : DateTime.Parse(beginTimePicker7.Text).Minute);
                    tz.SetEndTime(endTimePicker7.Text == "" ? 0 : DateTime.Parse(endTimePicker7.Text).Hour, endTimePicker1.Text == "" ? 0 : DateTime.Parse(endTimePicker7.Text).Minute);
                }
                else if (j == 7)
                {
                    tz.SetBeginTime(beginTimePicker8.Text == "" ? 0 : DateTime.Parse(beginTimePicker8.Text).Hour, beginTimePicker1.Text == "" ? 0 : DateTime.Parse(beginTimePicker8.Text).Minute);
                    tz.SetEndTime(endTimePicker8.Text == "" ? 0 : DateTime.Parse(endTimePicker8.Text).Hour, endTimePicker1.Text == "" ? 0 : DateTime.Parse(endTimePicker8.Text).Minute);
                }
            }
        }

        /// <summary>
        /// 上传所有开门时段
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAddTimeGroup_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 清空所有开门时段
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClearTimeGroup_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ClearTimeGroup cmd = new ClearTimeGroup(cmdDtl);
            mMainForm.AddCommand(cmd);

            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                mMainForm.AddLog($"命令成功：");

            };
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void CbTimeGroup_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
