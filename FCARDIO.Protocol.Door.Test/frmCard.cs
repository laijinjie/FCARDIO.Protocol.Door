using FCARDIO.Core.Command;
using FCARDIO.Protocol.Door.FC8800.Data;
using FCARDIO.Protocol.Door.Test.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FCARDIO.Protocol.Door.Test
{
    public partial class frmCard : frmNodeForm
    {
        private static object lockobj = new object();
        private static frmCard onlyObj;


        BindingList<CardDetail_UI> CardList = null;
        /// <summary>
        /// 卡号字典
        /// </summary>
        Dictionary<UInt64, CardDetailBase> CardDic = null;

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
            cmbcardType.Items.AddRange(new string[] { "排序卡", "非排序卡", "所有卡" });
            cmbcardType.SelectedIndex = 0;
            dgCardList.AutoGenerateColumns = false;
            CardList = new BindingList<CardDetail_UI>();
            CardDic = new Dictionary<ulong, CardDetailBase>();
            dgCardList.DataSource = CardList;

            IniControl();

        }

        #region 读取授权卡存储信息
        private void button1_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            INCommand cmd;

            cmd = new FC8800.Card.CardDatabaseDetail.ReadCardDatabaseDetail(cmdDtl);

            mMainForm.AddCommand(cmd);

            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                var result = cmd.getResult() as FC8800.Card.CardDatabaseDetail.ReadCardDatabaseDetail_Result;
                mMainForm.AddCmdLog(cmde, $"命令成功：排序数据区：容量:{result.SortDataBaseSize.ToString()},已使用数量:{result.SortCardSize.ToString()}");
                mMainForm.AddCmdLog(cmde, $"非排序存储区：容量:{result.SequenceDataBaseSize.ToString()},已使用数量:{result.SequenceCardSize.ToString()}");
            };
        }
        #endregion

        #region 读取所有授权卡
        private void button2_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            INCommand cmd;
            var par = new FC8800.Card.CardDataBase.ReadCardDataBase_Parameter(cmbcardType.SelectedIndex + 1);

            if (mMainForm.GetProtocolType() == CommandDetailFactory.ControllerType.FC89H)
            {
                cmd = new FC89H.Card.CardDataBase.ReadCardDataBase(cmdDtl, par);
            }
            else
            {
                cmd = new FC8800.Card.CardDataBase.ReadCardDataBase(cmdDtl, par);
            }
            CardList.Clear();
            CardDic.Clear();
            mMainForm.AddCommand(cmd);

            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {

                Invoke(() => ReadCardDataBaseCallBlack(cmde));
            };
        }

        /// <summary>
        /// 读取卡数据库命令执行完毕，回调
        /// </summary>
        /// <param name="cmde"></param>
        private void ReadCardDataBaseCallBlack(CommandEventArgs cmde)
        {
            int iReadCount = 0;
            int iType = 0;
            CardList.RaiseListChangedEvents = false;
            CardList.Clear();
            CardDic.Clear();
            var result = cmde.Result as FC8800.Card.CardDataBase.ReadCardDataBase_Result;
            if (result != null)
            {
                iType = result.CardType;
                iReadCount = result.DataBaseSize;
                if (iReadCount > 0)
                {
                    //FC8800的卡号
                    foreach (var c in result.CardList)
                    {
                        AddCardDataBaseToList(c);

                    }
                }
            }
            else
            {
                var fc89Result = cmde.Result as FC89H.Card.CardDataBase.ReadCardDataBase_Result;
                iType = fc89Result.CardType;
                iReadCount = fc89Result.DataBaseSize;
                if (iReadCount > 0)
                {
                    //FC89H的卡号
                    foreach (var c in fc89Result.CardList)
                    {
                        AddCardDataBaseToList(c);
                    }
                }
            }

            CardList.RaiseListChangedEvents = true;
            CardList.ResetBindings();

            string[] DBTypes = new string[] { "", "排序区", "非排序区", "所有区域" };
            mMainForm.AddCmdLog(cmde, $"读取到的卡片数:{iReadCount},带读取的卡片数据类型:{DBTypes[iType]}");
        }

        /// <summary>
        /// 将一个卡详情添加到系统缓冲中
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        private bool AddCardDataBaseToList(CardDetailBase card)
        {

            if (!CardDic.ContainsKey(card.CardData))
            {
                var ui = new CardDetail_UI(card);
                ui.CardIndex = (CardList.Count + 1).ToString();
                CardList.Add(ui);
                CardDic.Add(card.CardData, card);
                return true;
            }
            return false;
        }
        #endregion

        /// <summary>
        /// 表格的行单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgCardList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int col = e.ColumnIndex, row = e.RowIndex;
            if (row < 0) return;
            var gdRow = dgCardList.Rows[row];
            var CardUI = gdRow.DataBoundItem as CardDetail_UI;
            StringBuilder strBuf = new StringBuilder();

            DebugCardDetail(CardUI.CardDetail, strBuf);
            txtDebug.Text = strBuf.ToString();
            CardDetailToControl(CardUI.CardDetail);
        }

        #region 清空授权卡
        private void butClearCardDataBase_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            var par = new FC8800.Card.ClearCardDataBase.ClearCardDataBase_Parameter(cmbcardType.SelectedIndex + 1);
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

            var cmdDtl = mMainForm.GetCommandDetail();
            INCommand cmd;
            
            
            if (mMainForm.GetProtocolType() == CommandDetailFactory.ControllerType.FC89H)
            {
                //暂时注释
                //cmd = new FC89H.Card.CardDataBase.ReadCardDataBase(cmdDtl, par);
            }
            else
            {
                //暂时注释
                //var par = new FC8800.Card.CardListBySequence.WriteCardListBySequence_Parameter(cmbcardType.SelectedIndex + 1);
                //cmd = new FC8800.Card.CardDataBase.ReadCardDataBase(cmdDtl, par);
            }

            /*
            for (int i = 0; i < dgCardList.Rows.Count; i++)
            {
                DataGridViewTextBoxCell text = (DataGridViewTextBoxCell)dgCardList.Rows[i].Cells[2];
                var dto = CardList.FirstOrDefault(t => t.CardData == ulong.Parse(text.Value.ToString()));
                _cardList.Add(ConvertModel(dto));
            }

            //par 需要传输 失败卡数量 FailTotal 和失败的卡列表 List<FC8800.Data.CardDetail> CardList;
            var cmdDtl = mMainForm.GetCommandDetail();
            var par = new FC8800.Card.CardListBySequence.WriteCardListBySequence_Parameter(_cardList);
            var cmd = new FC8800.Card.CardListBySequence.WriteCardListBySequence(cmdDtl, par);
            mMainForm.AddCommand(cmd);

            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                //var result = cmd.getResult() as FC8800.Card.CardListBySequence.WriteCardListBySequence_Result;
                //if (result != null)
                //{
                //    mMainForm.AddCmdLog(cmde, $"命令成功：失败卡数量:{result.FailTotal},失败的卡列表:{result.CardList.ToString()}");
                //}
            };*/
        }
        #endregion

        #region 上传至排列区
        private void butCardListBySort_Click(object sender, EventArgs e)
        {
            /*
            //par 需要传输 需要上传的卡片列表  List<FC8800.Data.CardDetail> CardList;
            List<FC8800.Data.CardDetail> _cardList = new List<FC8800.Data.CardDetail>();

            for (int i = 0; i < dgCardList.Rows.Count; i++)
            {
                DataGridViewTextBoxCell text = (DataGridViewTextBoxCell)dgCardList.Rows[i].Cells[2];
                var dto = CardList.FirstOrDefault(t => t.CardData == ulong.Parse(text.Value.ToString()));
                _cardList.Add(ConvertModel(dto));
            }
            //

            var cmdDtl = mMainForm.GetCommandDetail();
            var par = new FC8800.Card.CardListBySort.WriteCardListBySort_Parameter(_cardList);
            var cmd = new FC8800.Card.CardListBySort.WriteCardListBySort(cmdDtl, par);
            mMainForm.AddCommand(cmd);
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                //mMainForm.AddLog($"命令成功：失败卡数量:{result.FailTotal},失败的卡列表:{result.CardList.ToString()}");
            };*/
        }
        #endregion

        #region 删除授权卡
        private void butDeletecard_Click(object sender, EventArgs e)
        {
            //par 需要传输 需要删除的卡片列表 long[] CardList;、
            string CardData = dgCardList.Rows[0].Cells["CardData10"].Value.ToString();
            List<CardDetail> CardList = new List<CardDetail>();
            CardList.Add(new CardDetail() { CardData = ulong.Parse(CardData) });

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




        #region 读取单个卡片在控制器中的信息

        private void butReadCardDetail_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            INCommand cmd;
            UInt64 card = 0;
            if (!UInt64.TryParse(txtCardData.Text, out card))
            {
                MsgErr("请输入正确的卡号");
                return;
            }


            try
            {
                if (mMainForm.GetProtocolType() == CommandDetailFactory.ControllerType.FC89H)
                {
                    var par = new FC89H.Card.CardDetail.ReadCardDetail_Parameter(card);
                    cmd = new FC89H.Card.CardDetail.ReadCardDetail(cmdDtl, par);
                }
                else
                {
                    var par = new FC8800.Card.CardDetail.ReadCardDetail_Parameter(card);
                    cmd = new FC8800.Card.CardDetail.ReadCardDetail(cmdDtl, par);
                }
            }
            catch (Exception createcarderr)
            {
                MsgErr(createcarderr.Message);
                return;
            }
            
            mMainForm.AddCommand(cmd);

            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                Invoke(() => ReadCardDetailCallBlack(cmde));
            };
        }

        /// <summary>
        /// 读取卡片详情回调
        /// </summary>
        /// <param name="cmde"></param>
        private void ReadCardDetailCallBlack(CommandEventArgs cmde)
        {
            StringBuilder strBuf = new StringBuilder();
            var result = cmde.Result as FC8800.Card.CardDetail.ReadCardDetail_Result;
            if (result != null)
            {
                if (!result.IsReady)
                {
                    mMainForm.AddCmdLog(cmde, $"此卡未注册！");
                    return;
                }
                else
                {
                    DebugCardDetail(result.Card, strBuf);
                    CardDetailToControl(result.Card);
                }
            }
            else
            {
                var fc89Result = cmde.Result as FC89H.Card.CardDetail.ReadCardDetail_Result;
                if (!fc89Result.IsReady)
                {
                    mMainForm.AddCmdLog(cmde, $"此卡未注册！");
                    return;
                }
                else
                {
                    DebugCardDetail(fc89Result.Card, strBuf);
                    CardDetailToControl(fc89Result.Card);
                }

            }
            txtDebug.Text = strBuf.ToString();

            mMainForm.AddCmdLog(cmde, "卡片已注册，详情查看【卡详情描述】");
        }
        /// <summary>
        /// 将卡片输出到buf中
        /// </summary>
        /// <param name="card"></param>
        private StringBuilder DebugCardDetail(CardDetailBase card, StringBuilder strBuf)
        {
            CardDetail_UI ui = new CardDetail_UI(card);
            strBuf.Append("卡号：").Append(ui.CardData).Append("；密码：").AppendLine(ui.Password);
            strBuf.Append("有效期：").Append(ui.Expiry).Append("；有效次数：").Append(ui.OpenTimes).AppendLine("；");
            strBuf.Append("权限：").AppendLine(ui.doorAccess);
            strBuf.Append("开门时段：").AppendLine(ui.TimeGroup);
            strBuf.Append("状态：").Append(ui.CardStatus).Append("；特权：").Append(ui.Privilege).Append("；节假日：").AppendLine(ui.Holiday);
            strBuf.Append("出入标志：").AppendLine(ui.EnterStatus);
            strBuf.Append("最近读卡时间：").AppendLine(ui.ReadCardDate);
            return strBuf;
        }

        /// <summary>
        /// 将卡片输出到控件中
        /// </summary>
        /// <param name="card"></param>
        private void CardDetailToControl(CardDetailBase card)
        {
            CardDetail_UI ui = new CardDetail_UI(card);
            txtCardData16.Text = card.CardData.ToString("X");
            txtCardData.Text = card.CardData.ToString();
            txtPassword.Text = ui.Password;
            dtpDate.Value = card.Expiry;
            dtpTime.Value = card.Expiry;
            cmbOpenTimes.Text = ui.OpenTimes;
            cmbCardStatus.SelectedIndex = card.CardStatus;
            CheckBox[] DoorList = { chkDoor1, chkDoor2, chkDoor3, chkDoor4 };
            ComboBox[] TGList = { cmbTimeGroup1, cmbTimeGroup2, cmbTimeGroup3, cmbTimeGroup4 };
            ComboBox[] EnterStatusList = { cmbEnterStatus1, cmbEnterStatus1, cmbEnterStatus1, cmbEnterStatus1 };
            int i;
            for (i = 1; i <= 4; i++)
            {
                //门权限
                DoorList[i - 1].Checked = card.GetDoor(i);
                //开门时段
                TGList[i - 1].SelectedIndex = card.GetTimeGroup(i);
                //出入状态
                int value = ui.GetEnterStatusValue(i);
                if (value == 0 || value == 3)
                    EnterStatusList[i - 1].SelectedIndex = 0;
                if (value == 1 || value == 2)
                    EnterStatusList[i - 1].SelectedIndex = value;
            }

            cmbPrivilege.SelectedIndex = card.Privilege;
            cbHolidayUse.Checked = card.HolidayUse;
            if (card.HolidayUse)
                txtHoliday.Text = ui.Holiday;
            else
                txtHoliday.Text = new string('1', 30);

        }
        #endregion

        #region 初始化控件列表
        private void IniControl()
        {
            TimeGroup();
            EnterStatus();
            OpenTimes();
            CardStatus();

        }
        #endregion

        #region 出入标记
        public void EnterStatus()
        {
            ComboBox[] EnterStatusList = { cmbEnterStatus1, cmbEnterStatus1, cmbEnterStatus1, cmbEnterStatus1 };
            string[] arr = new string[] { "出入有效", "出有效", "入有效" };
            foreach (var cmb in EnterStatusList)
            {
                cmb.Items.Clear();
                cmb.Items.AddRange(arr);
                cmb.SelectedIndex = 0;
            }
        }
        #endregion

        #region 有效次数
        public void OpenTimes()
        {
            cmbOpenTimes.Items.Clear();
            cmbOpenTimes.Items.Add("(0)已失效");

            string[] time = new string[100];
            for (int i = 1; i <= 100; i++)
            {
                time[i - 1] = i + "次";
            }
            cmbOpenTimes.Items.AddRange(time);
            cmbOpenTimes.Items.Add("无限制(65535)");
            cmbOpenTimes.SelectedIndex = cmbOpenTimes.Items.Count - 1;
        }
        #endregion

        #region 卡片状态
        public void CardStatus()
        {
            cmbCardStatus.Items.Clear();
            cmbCardStatus.Items.AddRange(CardDetail_UI.CardStatusList);
            cmbCardStatus.SelectedIndex = 0;

            cmbPrivilege.Items.Clear();
            cmbPrivilege.Items.AddRange(CardDetail_UI.CardPrivilegeList);
            cmbPrivilege.SelectedIndex = 0;
        }
        #endregion

        #region 开门时段
        public void TimeGroup()
        {
            string[] time = new string[64];
            ComboBox[] TGList = { cmbTimeGroup1, cmbTimeGroup2, cmbTimeGroup3, cmbTimeGroup4 };

            for (int i = 0; i < 64; i++)
            {
                time[i] = "卡门时段" + (i + 1);
            }

            foreach (var tgb in TGList)
            {
                tgb.Items.Clear();
                tgb.Items.AddRange(time);
                tgb.SelectedIndex = 0;
            }
        }
        #endregion

        #region 添加卡信息



        private void butInsertList_Click(object sender, EventArgs e)
        {
            
            /*
            ulong ulOut = 0;
            if (!ulong.TryParse(txtCardData.Text, out ulOut))
            {
                MessageBox.Show("error");
                return;
            }
            CardDetail_UI dto = new CardDetail_UI();
            BindDto(dto);
            CardList.Add(dto);
            dgCardList.DataSource = new BindingList<CardDetail_UI>(CardList);*/

            /*
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
            */
        }

        private void BindDto(CardDetail_UI dto, ulong carddate = 0)
        {
            /*
            ulong ul = 0;
            dto.ID = CardList.Count + 1; //编号
            if (carddate == 0)
            {
                dto.CardData = ulong.Parse(txtCardData.Text);               //十进制卡号
                                                                            //十六进制卡号
            }
            else
            {
                dto.CardData = carddate;
            }
            dto.CardData16 = txtCardData16.Text;
            dto.Password = txtPassword.Text;                 //密码
            dto.Expiry = new DateTime(dtpDate.Value.Year, dtpDate.Value.Month, dtpDate.Value.Day, dtpTime.Value.Hour, dtpTime.Value.Minute, 0);                     //有效期
            dto.CardStatus = CardStatusList[cmbCardStatus.SelectedIndex] + "卡";      //卡片状态
            dto.OpenTimes = cmbOpenTimes.SelectedIndex;                //有效次数
            if (cmbOpenTimes.SelectedIndex == cmbOpenTimes.Items.Count - 1)
            {
                dto.OpenTimes = 65535;
            }
            dto.door1 = cbbit0.Checked ? "有权限" : "无权限";
            dto.TimeGroup1 = Convert.ToByte(cmbTimeGroup1.SelectedIndex + 1);       //卡门时段
            dto.EnterStatus1 = EnterStatusList[cmbEnterStatus1.SelectedIndex];   //出入标志

            dto.door2 = cbbit1.Checked ? "有权限" : "无权限";
            dto.TimeGroup2 = Convert.ToByte(cmbTimeGroup2.SelectedIndex + 1);       //卡门时段
            dto.EnterStatus2 = EnterStatusList[cmbEnterStatus2.SelectedIndex];   //出入标志

            dto.door3 = cbbit2.Checked ? "有权限" : "无权限";
            dto.TimeGroup3 = Convert.ToByte(cmbTimeGroup3.SelectedIndex + 1);       //卡门时段
            dto.EnterStatus3 = EnterStatusList[cmbEnterStatus3.SelectedIndex];   //出入标志

            dto.door4 = cbbit3.Checked ? "有权限" : "无权限";
            dto.TimeGroup4 = Convert.ToByte(cmbTimeGroup4.SelectedIndex + 1);       //卡门时段
            dto.EnterStatus4 = EnterStatusList[cmbEnterStatus4.SelectedIndex];   //出入标志

            dto.Privilege1 = rbutPrivilege1.Checked ? "有效" : "无效";  //首卡
            dto.Privilege2 = rbutPrivilege2.Checked ? "有效" : "无效";    //常开
            dto.Privilege3 = rbutPrivilege3.Checked ? "有效" : "无效";    //巡更
            dto.Privilege4 = rbutPrivilege4.Checked ? "有效" : "无效";    //防盗设置卡
            dto.Holiday = txtHoliday.Text;  //节假日
            dto.HolidayUse = cbHolidayUse.Checked ? "有效" : "无效";  //使用节假日
            */
        }
        #endregion

        /// <summary>
        /// 反选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            bool bSelect = chkSelectAll.Checked;
            foreach (var item in CardList)
            {
                item.Selected = bSelect;
            }
        }

        /// <summary>
        /// 清空列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butClearGrid_Click(object sender, EventArgs e)
        {
            CardList.Clear();
            CardDic.Clear();
        }

        /// <summary>
        /// 检查待创建的卡号数量
        /// </summary>
        /// <returns></returns>
        private int CheckCreateCardCount()
        {
            int iCreateCount = 0;
            if (!int.TryParse(txtCount.Text, out iCreateCount))
            {
                MessageBox.Show("输入的数字不正确，取值范围：1-32000！");
                return 0;
            }
            if (iCreateCount > 32000)
            {
                MessageBox.Show("输入的数字不正确，取值范围：1-32000！");
                return 0;
            }
            if ((iCreateCount + CardList.Count) > 32000)
            {
                iCreateCount = 32000 - CardList.Count;

            }
            if (iCreateCount <= 0) return 0;

            return iCreateCount;
        }

        /// <summary>
        /// 生成随机卡 事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butCreateCardNumByRandom_Click(object sender, EventArgs e)
        {
            int iCreateCount = CheckCreateCardCount();
            if (iCreateCount <= 0) return;

            CommandDetailFactory.ControllerType iType = mMainForm.GetProtocolType();
            CardList.RaiseListChangedEvents = false;
            CardDetailBase card;
            for (int i = 0; i < iCreateCount; i++)
            {

                card = CreateNewCardDetail(iType, 0);
                AddCardDataBaseToList(card);

            }
            CardList.RaiseListChangedEvents = true;
            CardList.ResetBindings();
        }

        /// <summary>
        /// 生成顺序卡
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butCreateCardNumByOrder_Click(object sender, EventArgs e)
        {
            int iCreateCount = CheckCreateCardCount();
            if (iCreateCount <= 0) return;

            string sBeginNum = frmInputBox.ShowBox("起始序号", "请输入卡号的起始序号，取值范围：1-4000000000", "1", 10);
            UInt64 iBeginNum = 0;
            if (!UInt64.TryParse(sBeginNum, out iBeginNum))
            {
                return;
            }
            if (iBeginNum <= 0) iBeginNum = 1;

            CardDetailBase card;
            CommandDetailFactory.ControllerType iType = mMainForm.GetProtocolType();

            CardList.RaiseListChangedEvents = false;
            while (iCreateCount > 0)
            {
                card = CreateNewCardDetail(iType, ++iBeginNum);
                if (card != null)
                {
                    AddCardDataBaseToList(card);

                    iCreateCount--;
                }

            }
            CardList.RaiseListChangedEvents = true;
            CardList.ResetBindings();

        }


        private Random mCardRnd = new Random();
        private int mCardMax = 0x6FFFFFFF;
        private int mCardMin = 0x10000000;
        /// <summary>
        /// 创建一个不重复的卡
        /// </summary>
        /// <param name="iType"></param>
        /// <param name="iCardNum"></param>
        /// <returns></returns>
        private CardDetailBase CreateNewCardDetail(CommandDetailFactory.ControllerType iType, UInt64 iCardNum)
        {
            UInt64 cardNum = 0, cardNum2 = 0;
            CardDetailBase card;
            if (iCardNum == 0)
            {
                cardNum = (UInt64)(mCardRnd.Next(mCardMax) % (mCardMax - mCardMin + 1) + mCardMin);
                if (iType == CommandDetailFactory.ControllerType.FC89H)
                {
                    cardNum2 = (UInt64)(mCardRnd.Next(mCardMax) % (mCardMax - mCardMin + 1) + mCardMin);
                    cardNum = (cardNum << 32) + cardNum2;
                }


            }
            else
            {
                cardNum = iCardNum;
            }


            //检查卡片是否重复
            if (CardDic.ContainsKey(cardNum))
            {
                if (iCardNum == 0)
                {
                    //有重复
                    return CreateNewCardDetail(iType, 0);
                }
                else
                {
                    return null;
                }

            }


            if (iType == CommandDetailFactory.ControllerType.FC89H)
            {
                card = new FC89H.Data.CardDetail();
            }
            else
            {
                card = new FC8800.Data.CardDetail();
            }
            card.CardData = cardNum;
            return card;
        }

        /// <summary>
        /// 从列表删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButDelList_Click(object sender, EventArgs e)
        {
            var lst = CardList.Where(t => t.Selected = true).ToArray();


            CardList.RaiseListChangedEvents = false;
            CardList.Clear();
            CardDic.Clear();
            foreach (var c in lst)
            {
                CardList.Add(c);
                CardDic.Add(c.CardDetail.CardData, c.CardDetail);
            }
            CardList.RaiseListChangedEvents = true;
            CardList.ResetBindings();
        }

        private void BtnDelDevice_Click(object sender, EventArgs e)
        {
            List<CardDetail> _cardList = new List<CardDetail>();
            _cardList.Add(new CardDetail() { CardData = ulong.Parse(txtCardData.Text) });
            var cmdDtl = mMainForm.GetCommandDetail();
            var par = new FC8800.Card.DeleteCard.DeleteCard_Parameter(_cardList);
            var cmd = new FC8800.Card.DeleteCard.DeleteCard(cmdDtl, par);
            mMainForm.AddCommand(cmd);

            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                mMainForm.AddCmdLog(cmde, $"命令成功");
            };
        }

        private void BtnDelSelect_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 增加至设备 写入非排序区
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAddDevice_Click(object sender, EventArgs e)
        {
            /*
            List<FC8800.Data.CardDetail> _cardList = new List<FC8800.Data.CardDetail>();

            CardDetail_UI dto = new CardDetail_UI();

            dto.CardData = Convert.ToUInt64(txtCardData.Text);
            dto.CardData16 = GetCardDataHex(dto.CardData);

            BindDto(dto);
            _cardList.Add(ConvertModel(dto));

            //par 需要传输 失败卡数量 FailTotal 和失败的卡列表 List<FC8800.Data.CardDetail> CardList;
            var cmdDtl = mMainForm.GetCommandDetail();
            var par = new FC8800.Card.CardListBySequence.WriteCardListBySequence_Parameter(_cardList);
            var cmd = new FC8800.Card.CardListBySequence.WriteCardListBySequence(cmdDtl, par);
            mMainForm.AddCommand(cmd);

            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                //var result = cmd.getResult() as FC8800.Card.CardListBySequence.WriteCardListBySequence_Result;
                //if (result != null)
                //{
                //    mMainForm.AddCmdLog(cmde, $"命令成功：失败卡数量:{result.FailTotal},失败的卡列表:{result.CardList.ToString()}");
                //}
            };
            */
        }

        private void TxtCardData_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
