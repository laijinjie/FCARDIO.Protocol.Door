using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FCARDIO.Protocol.FC8800;

namespace FCARDIO.Protocol.Door.Test
{
    public partial class frmCard : Form
    {
        private static object lockobj = new object();
        private static frmCard onlyObj;
        public static frmCard GetForm(INMain main)
        {
            if (onlyObj == null)
            {
                lock (lockobj)
                {
                    if (onlyObj == null)
                    {
                        onlyObj = new frmCard(main);
                        frmMain.AddNodeForms(onlyObj);
                    }
                }
            }
            return onlyObj;
        }

        INMain mMainForm;

        private frmCard(INMain main)
        {
            InitializeComponent();
            mMainForm = main;
        }

        private void frmCard_Load(object sender, EventArgs e)
        {
            //操作类型
            cmbcardType.Items.Clear();
            cmbcardType.Items.AddRange(new string[] { "所有卡", "排序卡", "非排序卡" });
            cmbcardType.SelectedIndex = 0;

            TimeGroup();
            EnterStatus();
            OpenTimes();
            CardStatus();
        }

        #region 读取授权卡的信息
        private void button1_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            var cmd = new FC8800.Card.CardDatabaseDetail.ReadCardDatabaseDetail(cmdDtl);
            mMainForm.AddCommand(cmd);

            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                var result = cmd.getResult() as FC8800.Card.CardDatabaseDetail.ReadCardDatabaseDetail_Result;
                mMainForm.AddLog($"命令成功：排序数据区容量上限:{result.SortDataBaseSize},排序数据区已使用数量:{result.SortCardSize}，顺序存储区容量上限:{result.SequenceDataBaseSize},顺序存储区已使用数量:{result.SequenceCardSize}");
            };
        }
        #endregion

        #region 读取所有授权卡
        private void button2_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            var par = new FC8800.Card.CardDataBase.ReadCardDataBase_Parameter(cmbcardType.SelectedIndex);
            var cmd = new FC8800.Card.CardDataBase.ReadCardDataBase(cmdDtl, par);
            mMainForm.AddCommand(cmd);

            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                var result = cmd.getResult() as FC8800.Card.CardDataBase.ReadCardDataBase_Result;

                List<FC8800.Data.CardDetail> CardList = result.CardList;
                txtCardList.Text = CardList.ToString();

                mMainForm.AddLog($"命令成功：读取到的卡片列表:{result.CardList.ToString()},读取到的卡片数量:{result.DataBaseSize}，带读取的卡片数据类型:{result.CardType}");
            };
        }
        #endregion

        #region 清空授权卡
        private void butClearCardDataBase_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            var par = new FC8800.Card.ClearCardDataBase.ClearCardDataBase_Parameter(cmbcardType.SelectedIndex);
            var cmd = new FC8800.Card.ClearCardDataBase.ClearCardDataBase(cmdDtl, par);
            mMainForm.AddCommand(cmd);
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                mMainForm.AddLog($"命令成功");
            };
        }
        #endregion

        #region 上传至非排列区
        private void butCardListBySequence_Click(object sender, EventArgs e)
        {
            txtCardList.Text = "";
            string CardList1 = txtCardList.Text.ToString();

            List<FC8800.Data.CardDetail> CardList = new List<FC8800.Data.CardDetail>();
          
            CardList.Add(GetList());

            int FailTotal = CardList.Count;

            //par 需要传输 失败卡数量 FailTotal 和失败的卡列表 List<FC8800.Data.CardDetail> CardList;
            var cmdDtl = mMainForm.GetCommandDetail();
            var par = new FC8800.Card.CardListBySequence.WriteCardListBySequence_Parameter(FailTotal, CardList);
            var cmd = new FC8800.Card.CardListBySequence.WriteCardListBySequence(cmdDtl, par);
            mMainForm.AddCommand(cmd);

            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                var result = cmd.getResult() as FC8800.Card.CardListBySequence.WriteCardListBySequence_Result;
                txtCardList.Text = result.CardList.ToString();
                mMainForm.AddLog($"命令成功：失败卡数量:{result.FailTotal},失败的卡列表:{result.CardList.ToString()}");
            };
        }
        #endregion

        #region 获取表格中的 一条数据
        public FC8800.Data.CardDetail GetList()
        {
            string ID = dataGridView1.Rows[0].Cells["ID"].Value.ToString();
            string CardData10 = dataGridView1.Rows[0].Cells["CardData10"].Value.ToString();
            string CardData16 = dataGridView1.Rows[0].Cells["CardData16"].Value.ToString();
            string Password = dataGridView1.Rows[0].Cells["Password"].Value.ToString();
            string Expiry = dataGridView1.Rows[0].Cells["Expiry"].Value.ToString();
            string CardStatus1 = dataGridView1.Rows[0].Cells["CardStatus1"].Value.ToString();
            string OpenTime = dataGridView1.Rows[0].Cells["OpenTime"].Value.ToString();
            string door1 = dataGridView1.Rows[0].Cells["door1"].Value.ToString();
            string door2 = dataGridView1.Rows[0].Cells["door2"].Value.ToString();
            string door3 = dataGridView1.Rows[0].Cells["door3"].Value.ToString();
            string door4 = dataGridView1.Rows[0].Cells["door4"].Value.ToString();
            string TimeGroup1 = dataGridView1.Rows[0].Cells["TimeGroup1"].Value.ToString();
            string TimeGroup2 = dataGridView1.Rows[0].Cells["TimeGroup2"].Value.ToString();
            string TimeGroup3 = dataGridView1.Rows[0].Cells["TimeGroup3"].Value.ToString();
            string TimeGroup4 = dataGridView1.Rows[0].Cells["TimeGroup4"].Value.ToString();
            string Privilege1 = dataGridView1.Rows[0].Cells["Privilege1"].Value.ToString();
            string Privilege2 = dataGridView1.Rows[0].Cells["Privilege2"].Value.ToString();
            string Privilege3 = dataGridView1.Rows[0].Cells["Privilege3"].Value.ToString();
            string Privilege4 = dataGridView1.Rows[0].Cells["Privilege4"].Value.ToString();
            string Holiday = dataGridView1.Rows[0].Cells["Holiday"].Value.ToString();
            string EnterStatus1 = dataGridView1.Rows[0].Cells["EnterStatus1"].Value.ToString();
            string EnterStatus2 = dataGridView1.Rows[0].Cells["EnterStatus2"].Value.ToString();
            string EnterStatus3 = dataGridView1.Rows[0].Cells["EnterStatus3"].Value.ToString();
            string EnterStatus4 = dataGridView1.Rows[0].Cells["EnterStatus4"].Value.ToString();
            string HolidayUse = dataGridView1.Rows[0].Cells["HolidayUse"].Value.ToString();

            FC8800.Data.CardDetail detail = new FC8800.Data.CardDetail();
            detail.CardData = long.Parse(CardData10);
            detail.CardStatus = byte.Parse(CardStatus1);
            detail.EnterStatus = int.Parse(EnterStatus1);
            detail.Expiry = DateTime.Parse(Expiry);
            detail.HolidayUse = bool.Parse(HolidayUse);
            detail.OpenTimes = int.Parse(OpenTime);
            detail.Password = Password;
            return detail;
        } 
        #endregion

        #region 上传至排列区
        private void butCardListBySort_Click(object sender, EventArgs e)
        {
            //par 需要传输 需要上传的卡片列表  List<FC8800.Data.CardDetail> CardList;
            List<FC8800.Data.CardDetail> CardList = new List<FC8800.Data.CardDetail>();
            
            CardList.Add(GetList());
            

            var cmdDtl = mMainForm.GetCommandDetail();
            var par = new FC8800.Card.CardListBySort.WriteCardListBySort_Parameter(CardList);
            var cmd = new FC8800.Card.CardListBySort.WriteCardListBySort(cmdDtl, par);
            mMainForm.AddCommand(cmd);
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                var result = cmd.getResult() as FC8800.Card.CardListBySort.WriteCardListBySort_Result;
                mMainForm.AddLog($"命令成功：失败卡数量:{result.FailTotal},失败的卡列表:{result.CardList.ToString()}");
            };
        }
        #endregion

        #region 删除授权卡
        private void butDeletecard_Click(object sender, EventArgs e)
        {
            //par 需要传输 需要删除的卡片列表 long[] CardList;、
            string CardData = dataGridView1.Rows[0].Cells["CardData10"].Value.ToString();
            long[] CardList = new long[] { };
            CardList[0] = long.Parse(CardData);

            var cmdDtl = mMainForm.GetCommandDetail();
            var par = new FC8800.Card.DeleteCard.DeleteCard_Parameter(CardList);
            var cmd = new FC8800.Card.DeleteCard.DeleteCard(cmdDtl, par);
            mMainForm.AddCommand(cmd);
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                mMainForm.AddLog($"命令成功");
            };
        }
        #endregion

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            GetCardDetail();
        }

        #region 读取单个卡片在控制器中的信息
        public void GetCardDetail()
        {
            //par 需要传输 需要上传的  授权卡 卡号
            int CardData = int.Parse(txtCardData.Text);

            var cmdDtl = mMainForm.GetCommandDetail();
            var par = new FC8800.Card.CardDetail.ReadCardDetail_Parameter(CardData);
            var cmd = new FC8800.Card.CardDetail.ReadCardDetail(cmdDtl, par);
            mMainForm.AddCommand(cmd);

            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                var result = cmd.getResult() as FC8800.Card.CardDetail.ReadCardDetail_Result;
                mMainForm.AddLog($"命令成功：卡片是否存在:{result.IsReady},卡片的详情:{result.Card}");
            };
        }
        #endregion

        #region 出入标记
        public void EnterStatus()
        {
            cmbEnterStatus1.Items.Clear();
            cmbEnterStatus1.Items.AddRange(new string[] { "出入有效", "出有效", "有效" });
            cmbEnterStatus1.SelectedIndex = 0;

            cmbEnterStatus2.Items.Clear();
            cmbEnterStatus2.Items.AddRange(new string[] { "出入有效", "出有效", "有效" });
            cmbEnterStatus2.SelectedIndex = 0;

            cmbEnterStatus3.Items.Clear();
            cmbEnterStatus3.Items.AddRange(new string[] { "出入有效", "出有效", "有效" });
            cmbEnterStatus3.SelectedIndex = 0;

            cmbEnterStatus4.Items.Clear();
            cmbEnterStatus4.Items.AddRange(new string[] { "出入有效", "出有效", "有效" });
            cmbEnterStatus4.SelectedIndex = 0;
        }
        #endregion

        #region 有效次数
        public void OpenTimes()
        {
            cmbOpenTimes.Items.Clear();
            string[] time = new string[100];
            for (int i = 0; i < 100; i++)
            {
                time[i] = i + "";
                if (time[0] == "0")
                    time[0] = "无效";
                if (i == 100)
                    time[i] = "无限制";
            }
            cmbOpenTimes.Items.AddRange(time);
            cmbOpenTimes.SelectedIndex = 0;
        }
        #endregion

        #region 卡片状态
        public void CardStatus()
        {
            cmbCardStatus.Items.Clear();
            cmbCardStatus.Items.AddRange(new string[] { "正常", "挂失", "黑名单" });
            cmbCardStatus.SelectedIndex = 0;
        }
        #endregion

        #region 开门时段
        public void TimeGroup()
        {
            string[] time = new string[64];
            for (int i = 0; i < 64; i++)
            {
                time[i] = "卡门时段" + i;
            }

            cmbTimeGroup1.Items.Clear();
            cmbTimeGroup1.Items.AddRange(time);
            cmbTimeGroup1.SelectedIndex = 0;

            cmbTimeGroup2.Items.Clear();
            cmbTimeGroup2.Items.AddRange(time);
            cmbTimeGroup2.SelectedIndex = 0;

            cmbTimeGroup3.Items.Clear();
            cmbTimeGroup3.Items.AddRange(time);
            cmbTimeGroup3.SelectedIndex = 0;

            cmbTimeGroup4.Items.Clear();
            cmbTimeGroup4.Items.AddRange(time);
            cmbTimeGroup4.SelectedIndex = 0;
        }
        #endregion

        #region 添加卡信息
        private void butInsertList_Click(object sender, EventArgs e)
        {
            string ID = txtCardDataID.Text;                     //编号
            string CardData10 = txtCardData.Text;               //十进制卡号
            string CardData16 = txtCardData16.Text;             //十六进制卡号
            string Password = txtPassword.Text;                 //密码
            string Expiry = txtExpiry.Text;                     //有效期
            int CardStatus1 = cmbCardStatus.SelectedIndex;      //卡片状态
            int OpenTime = cmbOpenTimes.SelectedIndex;                //有效次数
            bool door1 = cbbit0.Checked ? true : false;         //门1权限
            int TimeGroup1 = 0;
            int EnterStatus1 = 0;
            if (door1)
            {
                TimeGroup1 = cmbTimeGroup1.SelectedIndex;       //卡门时段
                EnterStatus1 = cmbEnterStatus1.SelectedIndex;   //出入标志
            }
            bool door2 = cbbit1.Checked ? true : false;         //门2权限
            int TimeGroup2 = 0;
            int EnterStatus2 = 0;
            if (door1)
            {
                TimeGroup2 = cmbTimeGroup1.SelectedIndex;       //卡门时段
                EnterStatus2 = cmbEnterStatus1.SelectedIndex;   //出入标志
            }
            bool door3 = cbbit2.Checked ? true : false;         //门3权限
            int TimeGroup3 = 0;
            int EnterStatus3 = 0;
            if (door1)
            {
                TimeGroup3 = cmbTimeGroup1.SelectedIndex;       //卡门时段
                EnterStatus3 = cmbEnterStatus1.SelectedIndex;   //出入标志
            }
            bool door4 = cbbit3.Checked ? true : false;
            int TimeGroup4 = 0;
            int EnterStatus4 = 0;
            if (door1)
            {
                TimeGroup4 = cmbTimeGroup1.SelectedIndex;       //卡门时段
                EnterStatus4 = cmbEnterStatus1.SelectedIndex;   //出入标志
            }
            bool Privilege1 = rbutPrivilege1.Checked ? true : false;  //首卡
            bool Privilege2 = rbutPrivilege2.Checked ? true : false;    //常开
            bool Privilege3 = rbutPrivilege3.Checked ? true : false;    //巡更
            bool Privilege4 = rbutPrivilege4.Checked ? true : false;    //防盗设置卡
            string Holiday = "1";  //节假日
            bool HolidayUse = cbHolidayUse.Checked ? true : false;  //使用节假日


            for (int i = 0; i < 1; i++)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells["ID"].Value = ID;
                dataGridView1.Rows[i].Cells["CardData10"].Value = CardData10;
                dataGridView1.Rows[i].Cells["CardData16"].Value = CardData16;
                dataGridView1.Rows[i].Cells["Password"].Value = Password;
                dataGridView1.Rows[i].Cells["Expiry"].Value = Expiry;
                dataGridView1.Rows[i].Cells["CardStatus1"].Value = CardStatus1;
                dataGridView1.Rows[i].Cells["OpenTime"].Value = OpenTime;
                dataGridView1.Rows[i].Cells["door1"].Value = door1;
                dataGridView1.Rows[i].Cells["door2"].Value = door2;
                dataGridView1.Rows[i].Cells["door3"].Value = door3;
                dataGridView1.Rows[i].Cells["door4"].Value = door4;
                dataGridView1.Rows[i].Cells["TimeGroup1"].Value = TimeGroup1;
                dataGridView1.Rows[i].Cells["TimeGroup2"].Value = TimeGroup2;
                dataGridView1.Rows[i].Cells["TimeGroup3"].Value = TimeGroup3;
                dataGridView1.Rows[i].Cells["TimeGroup4"].Value = TimeGroup4;
                dataGridView1.Rows[i].Cells["Privilege1"].Value = Privilege1;
                dataGridView1.Rows[i].Cells["Privilege2"].Value = Privilege2;
                dataGridView1.Rows[i].Cells["Privilege3"].Value = Privilege3;
                dataGridView1.Rows[i].Cells["Privilege4"].Value = Privilege4;
                dataGridView1.Rows[i].Cells["Holiday"].Value = Holiday;
                dataGridView1.Rows[i].Cells["EnterStatus1"].Value = EnterStatus1;
                dataGridView1.Rows[i].Cells["EnterStatus2"].Value = EnterStatus2;
                dataGridView1.Rows[i].Cells["EnterStatus3"].Value = EnterStatus3;
                dataGridView1.Rows[i].Cells["EnterStatus4"].Value = EnterStatus4;
                dataGridView1.Rows[i].Cells["HolidayUse"].Value = HolidayUse;
            }
        }
        #endregion
        
    }
}
