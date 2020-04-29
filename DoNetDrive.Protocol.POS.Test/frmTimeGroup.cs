using DoNetDrive.Protocol.Door.Door8800.Data.TimeGroup;
using DoNetDrive.Protocol.POS.TimeGroup;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DotNetDrive.Protocol.POS.Test
{
    public partial class frmTimeGroup : frmNodeForm
    {
        string[] WeekdayList = new string[] { "星期一", "星期二", "星期三", "星期四", "星期五", "星期六", "星期日" };
        private static object lockobj = new object();
        private static frmTimeGroup onlyObj;

        /// <summary>
        /// 64个
        /// </summary>
        WeekTimeGroup mWeekTimeGroup = new WeekTimeGroup(4);
        public static frmTimeGroup GetForm(INMain main)
        {
            if (onlyObj == null)
            {
                lock (lockobj)
                {
                    if (onlyObj == null)
                    {
                        onlyObj = new frmTimeGroup(main);
                        FrmMain.AddNodeForms(onlyObj);
                    }
                }
            }
            return onlyObj;
        }


        private frmTimeGroup(INMain main)
        {
            InitializeComponent();
            mMainForm = main;
        }

        private void frmTimeGroup_Load(object sender, EventArgs e)
        {
            InitTimeGroup();
            InitWeekday();
        }

        private void InitWeekday()
        {
            cbWeekday.Items.Clear();
            cbWeekday.Items.AddRange(WeekdayList);
            cbWeekday.SelectedIndex = 0;
        }

        #region 消费时段
        public void InitTimeGroup()
        {
            string[] time = new string[64];
            for (int i = 0; i < 64; i++)
            {
                time[i] = "消费时段" + (i + 1).ToString();
            }
            cbTimeGroup.Items.Clear();
            cbTimeGroup.Items.AddRange(time);
            cbTimeGroup.SelectedIndex = 0;

        }
        #endregion

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
            ReadTimeGroup_Parameter par = new ReadTimeGroup_Parameter((byte)(cbTimeGroup.SelectedIndex + 1));
            ReadTimeGroup cmd = new ReadTimeGroup(cmdDtl, par);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadTimeGroup_Result result = cmde.Command.getResult() as ReadTimeGroup_Result;
                mWeekTimeGroup = result.WeekTimeGroup;


                BindTimeSegment(result.WeekTimeGroup);
                //dataGridView1
                string log = $"已读取到数量：{result.Count} ";
                mMainForm.AddCmdLog(cmde, log);
            };
        }

        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="timeSegmentIndex"></param>
        /// <param name="type"></param>
        /// <param name="dateTime"></param>
        private void SetWeekTimeGroupValue(int timeSegmentIndex,int type, DateTime dateTime)
        {
            TimeSegment ts = mWeekTimeGroup.GetItem(cbWeekday.SelectedIndex).GetItem(timeSegmentIndex);
            if (type == 1)
            {
                ts.SetBeginTime(dateTime.Hour, dateTime.Minute);
            }
            else
            {
                ts.SetEndTime(dateTime.Hour, dateTime.Minute);
            }
        }


        private void BindTimeSegment(WeekTimeGroup wtg)
        {
            DateTime nw = DateTime.Now;
            var day = wtg.GetItem(0);
            //var tz = day.GetItem(0) as TimeSegment;
            SetAllTimePicker(groupBox1, "beginTimePicker", "endTimePicker", day);
            /*
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
            */
        }

        /// <summary>
        /// 上传所有开门时段
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAddTimeGroup_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;

            AddTimeGroup_Parameter par = new AddTimeGroup_Parameter(mWeekTimeGroup);
            AddTimeGroup cmd = new AddTimeGroup(cmdDtl, par);
            mMainForm.AddCommand(cmd);

            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                mMainForm.AddLog($"命令成功：");

            };
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

        /// <summary>
        /// 选择开门时段
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbTimeGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            var day = mWeekTimeGroup.GetItem(0);
            SetAllTimePicker(groupBox1, "beginTimePicker", "endTimePicker", day);
        }

        private void CbWeekday_SelectedIndexChanged(object sender, EventArgs e)
        {
            var day = mWeekTimeGroup.GetItem(cbWeekday.SelectedIndex);
            SetAllTimePicker(groupBox1, "beginTimePicker", "endTimePicker", day);
        }

        private void BeginTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            SetWeekTimeGroupValue(0, 1, beginTimePicker1.Value);
        }

        private void EndTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            SetWeekTimeGroupValue(0, 2, endTimePicker1.Value);
        }

        private void BeginTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            SetWeekTimeGroupValue(1, 1, beginTimePicker2.Value);
        }

        private void EndTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            SetWeekTimeGroupValue(1, 2, endTimePicker2.Value);
        }

        private void BeginTimePicker3_ValueChanged(object sender, EventArgs e)
        {
            SetWeekTimeGroupValue(2, 1, beginTimePicker3.Value);
        }

        private void EndTimePicker3_ValueChanged(object sender, EventArgs e)
        {
            SetWeekTimeGroupValue(2, 2, endTimePicker3.Value);
        }

        private void BeginTimePicker4_ValueChanged(object sender, EventArgs e)
        {
            SetWeekTimeGroupValue(3, 1, beginTimePicker4.Value);
        }

        private void EndTimePicker4_ValueChanged(object sender, EventArgs e)
        {
            SetWeekTimeGroupValue(3, 2, endTimePicker4.Value);
        }

     

        private void BtnFillNowTime_Click(object sender, EventArgs e)
        {

            mWeekTimeGroup = new WeekTimeGroup(4);
            //weekTimeGroup.mDay
            //星期一 至 星期日
            for (int y = 0; y < 7; y++)
            {
                DayTimeGroup dayTimeGroup = mWeekTimeGroup.GetItem(y);
                //每天时段
                for (int i = 0; i < 4; i++)
                {
                    DateTime dt = DateTime.Now;
                    //dt = dt.AddMinutes(-1);
                    TimeSegment segment = dayTimeGroup.GetItem(i);
                    dt = dt.AddMinutes(i + 1);
                    segment.SetBeginTime(dt.Hour, dt.Minute);
                    dt = dt.AddMinutes(i + 1);
                    segment.SetEndTime(dt.Hour, dt.Minute);
                    DateTimePicker beginTimePicker = FindControl(groupBox1, "beginTimePicker" + (i + 1).ToString()) as DateTimePicker;
                    DateTimePicker endTimePicker = FindControl(groupBox1, "endTimePicker" + (i + 1).ToString()) as DateTimePicker;
                    beginTimePicker.Value = segment.GetBeginTime();
                    endTimePicker.Value = segment.GetEndTime();
                }
            }

        }
    }
}
