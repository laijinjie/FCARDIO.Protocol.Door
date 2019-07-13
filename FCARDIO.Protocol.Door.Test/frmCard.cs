using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FCARDIO.Protocol.Door.FC8800.Data;
using FCARDIO.Protocol.Door.FC8800.Utility;
using FCARDIO.Protocol.Door.Test.Model;

namespace FCARDIO.Protocol.Door.Test
{
    public partial class frmCard : frmNodeForm
    {
        private static object lockobj = new object();
        private static frmCard onlyObj;
        List<string> CardStatusList = new List<string>() { "正常", "挂失卡", "黑名单" };
        List<string> EnterStatusList = new List<string>() { "出入有效", "入有效", "出有效" };
        List<CardDetailDto> CardList = new List<CardDetailDto>();
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
            cmbcardType.Items.AddRange(new string[] {  "排序卡", "非排序卡", "所有卡" });
            cmbcardType.SelectedIndex = 0;
            dataGridView1.AutoGenerateColumns = false;
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
                mMainForm.AddCmdLog(cmde,$"命令成功：排序数据区容量上限:{result.SortDataBaseSize.ToString()},排序数据区已使用数量:{result.SortCardSize.ToString()}");
                mMainForm.AddCmdLog(cmde,$"非排序存储区容量上限:{result.SequenceDataBaseSize.ToString()},非排序存储区已使用数量:{result.SequenceCardSize.ToString()}");
            };
        }
        #endregion

        #region 读取所有授权卡
        private void button2_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            var par = new FC8800.Card.CardDataBase.ReadCardDataBase_Parameter(cmbcardType.SelectedIndex + 1);
            var cmd = new FC8800.Card.CardDataBase.ReadCardDataBase(cmdDtl, par);
            mMainForm.AddCommand(cmd);

            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                var result = cmd.getResult() as FC8800.Card.CardDataBase.ReadCardDataBase_Result;
                //var list = result.CardList;
                //List<FC8800.Data.CardDetail> CardList = result.CardList.Select(d => (FC8800.Data.CardDetail)d).ToList();

                mMainForm.AddCmdLog(cmde,$"命令成功：读取到的卡片列表:{result.CardList.ToString()},读取到的卡片数量:{result.DataBaseSize}，带读取的卡片数据类型:{result.CardType}");
                if (result.DataBaseSize > 0)
                {
                    List<FC8800.Data.CardDetail> mCardList = result.CardList.Select(d => (FC8800.Data.CardDetail)d).ToList(); ;

                    int id = 1;
                    foreach (var item in mCardList)
                    {
                        CardList.Add(ConvertDto(id, item));
                        id++;
                    }

                    Invoke(() =>
                    {
                        dataGridView1.DataSource = new BindingList<CardDetailDto>(CardList);

                    });
                }


            };
        }
        #endregion

        /// <summary>
        /// CardDetail 转换
        /// </summary>
        /// <param name="id"></param>
        /// <param name="card"></param>
        /// <returns></returns>
        private CardDetailDto ConvertDto(int id, FC8800.Data.CardDetail card)
        {
            CardDetailDto dto = new CardDetailDto();
            dto.ID = id;
            dto.CardData16 = GetCardDataHex(card.CardData);
            dto.CardData10 = card.CardData;

            dto.SetDoors(card);
            dto.SetTimeGroup(card.TimeGroup);
            dto.SetPrivilege(card.Privilege);
            dto.SetEnterStatus(GetEnterStatusList((byte)card.EnterStatus));
            dto.OpenTimes = card.OpenTimes;
            dto.CardStatus = CardStatusList[(int)card.CardStatus] + "卡";
            dto.Expiry = card.Expiry;
            dto.SetHoliday(card.Holiday);
            dto.HolidayUse = card.HolidayUse ? "有效" : "无效";
            dto.Password = card.Password;
            dto.RecordTime = card.RecordTime;
            return dto;
        }

        private FC8800.Data.CardDetail ConvertModel(CardDetailDto card)
        {
            FC8800.Data.CardDetail detail = new FC8800.Data.CardDetail();
            detail.CardData = Convert.ToUInt64(card.CardData10);
            var queryCardStatus = CardStatusList.Select((item, index) => new { index, item }).FirstOrDefault(t => card.CardStatus.Contains(t.item));

            var iEnterStatus1 = EnterStatusList.Select((item, index) => new { index, item }).FirstOrDefault(t => card.EnterStatus1.Contains(t.item)).index;
            var iEnterStatus2 = EnterStatusList.Select((item, index) => new { index, item }).FirstOrDefault(t => card.EnterStatus2.Contains(t.item)).index;
            var iEnterStatus3 = EnterStatusList.Select((item, index) => new { index, item }).FirstOrDefault(t => card.EnterStatus3.Contains(t.item)).index;
            var iEnterStatus4 = EnterStatusList.Select((item, index) => new { index, item }).FirstOrDefault(t => card.EnterStatus4.Contains(t.item)).index;
            string es1 = Convert.ToString(iEnterStatus1, 2);
            string es2 = Convert.ToString(iEnterStatus2, 2);
            string es3 = Convert.ToString(iEnterStatus3, 2);
            string es4 = Convert.ToString(iEnterStatus4, 2);
            string strEnterStatus = es1.PadLeft(2, '0') + es2.PadLeft(2, '0') + es3.PadLeft(2, '0') + es4.PadLeft(2, '0');
            detail.EnterStatus = Convert.ToInt32(strEnterStatus, 2);
            detail.TimeGroup = new byte[4] { card.TimeGroup1, card.TimeGroup2, card.TimeGroup3, card.TimeGroup4 };
            detail.CardStatus = Convert.ToByte(queryCardStatus.index);
            if (rbutPrivilege0.Checked)
            {
                detail.Privilege = 0;
            }
            if (rbutPrivilege1.Checked)
            {
                detail.Privilege = 1;
            }
            if (rbutPrivilege2.Checked)
            {
                detail.Privilege = 2;
            }
            if (rbutPrivilege3.Checked)
            {
                detail.Privilege = 3;
            }
            if (rbutPrivilege4.Checked)
            {
                detail.Privilege = 4;
            }

            detail.Expiry = card.Expiry;
            detail.HolidayUse = card.HolidayUse == "有效";
            detail.OpenTimes = card.OpenTimes;
            detail.Password = card.Password;
            detail.RecordTime = card.RecordTime;
            string strDoor1 = (card.door4 == "有权限" ? "1" : "0") + (card.door3 == "有权限" ? "1" : "0") + (card.door2 == "有权限" ? "1" : "0") + (card.door1 == "有权限" ? "1" : "0");
            detail.Door = Convert.ToInt32(strDoor1, 2);
            byte[] bHoliday = new byte[4];
            for (int i = 0; i < 4; i++)
            {
                detail.Holiday[0] = 255;// card.Holiday;
            }

            return detail;
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
            txtCardList.Text = "";
            List<FC8800.Data.CardDetail> _cardList = new List<FC8800.Data.CardDetail>();

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                DataGridViewTextBoxCell text = (DataGridViewTextBoxCell)dataGridView1.Rows[i].Cells[2];
                var dto = CardList.FirstOrDefault(t => t.CardData10 == ulong.Parse(text.Value.ToString()));
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
            detail.CardData = Convert.ToUInt64(CardData10);
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
            List<FC8800.Data.CardDetail> _cardList = new List<FC8800.Data.CardDetail>();

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                DataGridViewTextBoxCell text = (DataGridViewTextBoxCell)dataGridView1.Rows[i].Cells[2];
                var dto = CardList.FirstOrDefault(t => t.CardData10 == ulong.Parse(text.Value.ToString()));
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
            };
        }
        #endregion

        #region 删除授权卡
        private void butDeletecard_Click(object sender, EventArgs e)
        {
            //par 需要传输 需要删除的卡片列表 long[] CardList;、
            string CardData = dataGridView1.Rows[0].Cells["CardData10"].Value.ToString();
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if ((bool)cell.FormattedValue)
                {
                    cell.Value = false;
                    cell.EditingCellFormattedValue = false;
                }
                else
                {
                    cell.Value = true;
                    cell.EditingCellFormattedValue = true;
                }
                return;
            }

            DataGridViewTextBoxCell text = (DataGridViewTextBoxCell)dataGridView1.Rows[e.RowIndex].Cells[2];
            CardDetailDto dto = CardList.FirstOrDefault(t => t.CardData10 == Convert.ToUInt64(text.Value));
            //DataGridViewTextBoxCell textCardData16 = (DataGridViewTextBoxCell)dataGridView1.Rows[e.RowIndex].Cells[3];
            //DataGridViewTextBoxCell textPassword = (DataGridViewTextBoxCell)dataGridView1.Rows[e.RowIndex].Cells[4];
            //DataGridViewTextBoxCell textExpiry = (DataGridViewTextBoxCell)dataGridView1.Rows[e.RowIndex].Cells[5];
            //DataGridViewTextBoxCell textOpenTimes = (DataGridViewTextBoxCell)dataGridView1.Rows[e.RowIndex].Cells[6];
            //DataGridViewTextBoxCell textCardStatus = (DataGridViewTextBoxCell)dataGridView1.Rows[e.RowIndex].Cells[7];
            //DataGridViewTextBoxCell textCardStatus = (DataGridViewTextBoxCell)dataGridView1.Rows[e.RowIndex].Cells[7];
            txtCardData.Text = text.Value.ToString();
            txtCardData16.Text = dto.CardData16;
            txtPassword.Text = dto.Password;
            dtpDate.Value = dto.Expiry;
            dtpTime.Value = dto.Expiry;
            if (dto.OpenTimes == 65535)
            {
                cmbOpenTimes.SelectedIndex = cmbOpenTimes.Items.Count -1;
            }
            else
            {
                cmbOpenTimes.SelectedIndex = dto.OpenTimes;
            }
            
            for (int i = 0; i < CardStatusList.Count; i++)
            {
                if (dto.CardStatus.Contains(CardStatusList[i]))
                {
                    cmbCardStatus.SelectedIndex = i;
                }
            }
            cbbit0.Checked = dto.door1.Contains("有权限");
            cbbit1.Checked = dto.door2.Contains("有权限");
            cbbit2.Checked = dto.door3.Contains("有权限");
            cbbit3.Checked = dto.door4.Contains("有权限");

            cmbTimeGroup1.SelectedIndex = dto.TimeGroup1;
            cmbTimeGroup2.SelectedIndex = dto.TimeGroup2;
            cmbTimeGroup3.SelectedIndex = dto.TimeGroup3;
            cmbTimeGroup4.SelectedIndex = dto.TimeGroup4;

            //for (int i = 0; i < EnterStatusList.Count; i++)
            //{
            //    if (dto.EnterStatus1.Contains(EnterStatusList[i]))
            //    {
            //        cmbEnterStatus1.SelectedIndex = i;
            //    }
            //}
            cmbEnterStatus1.SelectedIndex = EnterStatusList.Select((item, index) => new { index, item }).FirstOrDefault(t => dto.EnterStatus1.Contains(t.item)).index;
            cmbEnterStatus2.SelectedIndex = EnterStatusList.Select((item, index) => new { index, item }).FirstOrDefault(t => dto.EnterStatus2.Contains(t.item)).index;
            cmbEnterStatus3.SelectedIndex = EnterStatusList.Select((item, index) => new { index, item }).FirstOrDefault(t => dto.EnterStatus3.Contains(t.item)).index;
            cmbEnterStatus4.SelectedIndex = EnterStatusList.Select((item, index) => new { index, item }).FirstOrDefault(t => dto.EnterStatus4.Contains(t.item)).index;
            //rbutPrivilege1.Checked = dto.Privilege1;
            GetCardDetail(Convert.ToUInt32(text.Value));
        }

        #region 读取单个卡片在控制器中的信息
        public void GetCardDetail(uint CardData)
        {
            //par 需要传输 需要上传的  授权卡 卡号

            var cmdDtl = mMainForm.GetCommandDetail();
            var par = new FC8800.Card.CardDetail.ReadCardDetail_Parameter(CardData);
            var cmd = new FC8800.Card.CardDetail.ReadCardDetail(cmdDtl, par);
            mMainForm.AddCommand(cmd);

            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                var result = cmd.getResult() as FC8800.Card.CardDetail.ReadCardDetail_Result;
                Invoke(() =>
                {
                    if (result.Card != null)
                    {
                        txtCardData.Text = result.Card.CardData.ToString();
                        txtPassword.Text = result.Card.Password;
                        dtpDate.Value = result.Card.Expiry;
                        dtpTime.Value = result.Card.Expiry;
                        if (result.Card.OpenTimes == 65535)
                        {
                            cmbOpenTimes.SelectedIndex = cmbOpenTimes.Items.Count - 1;
                        }
                        else
                        {
                            cmbOpenTimes.SelectedIndex = result.Card.OpenTimes;
                        }
                        cbbit0.Checked = result.Card.GetDoor(1);
                        cbbit1.Checked = result.Card.GetDoor(2);
                        cbbit2.Checked = result.Card.GetDoor(3);
                        cbbit3.Checked = result.Card.GetDoor(4);
                    }
                });
                
                mMainForm.AddCmdLog(cmde,$"命令成功：卡片是否存在:{result.IsReady},卡片的详情:{result.Card}");
            };
        }
        #endregion

        #region 出入标记
        public void EnterStatus()
        {
            cmbEnterStatus1.Items.Clear();
            cmbEnterStatus1.Items.AddRange(new string[] { "出入有效", "出有效", "入有效" });
            cmbEnterStatus1.SelectedIndex = 0;

            cmbEnterStatus2.Items.Clear();
            cmbEnterStatus2.Items.AddRange(new string[] { "出入有效", "出有效", "入有效" });
            cmbEnterStatus2.SelectedIndex = 0;

            cmbEnterStatus3.Items.Clear();
            cmbEnterStatus3.Items.AddRange(new string[] { "出入有效", "出有效", "入有效" });
            cmbEnterStatus3.SelectedIndex = 0;

            cmbEnterStatus4.Items.Clear();
            cmbEnterStatus4.Items.AddRange(new string[] { "出入有效", "出有效", "入有效" });
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
            cmbCardStatus.Items.AddRange(CardStatusList.ToArray());
            cmbCardStatus.SelectedIndex = 0;
        }
        #endregion

        #region 开门时段
        public void TimeGroup()
        {
            string[] time = new string[64];
            for (int i = 0; i < 64; i++)
            {
                time[i] = "卡门时段" + i + 1;
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
            ulong ulOut = 0;
            if (!ulong.TryParse(txtCardData.Text, out ulOut))
            {
                MessageBox.Show("error");
                return;
            }
            CardDetailDto dto = new CardDetailDto();
            BindDto(dto);
            CardList.Add(dto);
            dataGridView1.DataSource = new BindingList<CardDetailDto>(CardList);
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

        private void BindDto(CardDetailDto dto, ulong carddate = 0)
        {
            ulong ul = 0;
            dto.ID = CardList.Count + 1; //编号
            if (carddate == 0)
            {
                dto.CardData10 = ulong.Parse(txtCardData.Text);               //十进制卡号
                                                                              //十六进制卡号
            }
            else
            {
                dto.CardData10 = carddate;
            }
            dto.CardData16 = txtCardData16.Text;
            dto.Password = txtPassword.Text;                 //密码
            dto.Expiry = new DateTime(dtpDate.Value.Year, dtpDate.Value.Month, dtpDate.Value.Day,dtpTime.Value.Hour, dtpTime.Value.Minute,0) ;                     //有效期
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
        }
        #endregion

        /// <summary>
        /// 反选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)dataGridView1.Rows[i].Cells[0];
                if ((bool)cell.FormattedValue)
                {
                    cell.Value = false;
                    cell.EditingCellFormattedValue = false;
                }
                else
                {
                    cell.Value = true;
                    cell.EditingCellFormattedValue = true;
                }
            }
        }

        /// <summary>
        /// 清空列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button6_Click(object sender, EventArgs e)
        {
            int count = dataGridView1.Rows.Count;
            for (int i = count - 1; i >= 0; i--)
            {
                DataGridViewTextBoxCell text = (DataGridViewTextBoxCell)dataGridView1.Rows[i].Cells[2];
                CardList.RemoveAt(CardList.FindIndex(t => t.CardData10 == Convert.ToUInt32(text.Value)));
            }
            dataGridView1.DataSource = new BindingList<CardDetailDto>(CardList);
        }

        /// <summary>
        /// 生成随机卡 事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRandom_Click(object sender, EventArgs e)
        {
            int iOut = 0;
            if (!int.TryParse(txtCount.Text, out iOut))
            {
                MessageBox.Show("error");
                return;
            }
            int count = int.Parse(txtCount.Text);
            Random rnd = new Random();
            int max = 90000000;
            int min = 10000000;
            for (int i = 0; i < count; i++)
            {
                CardDetailDto dto = new CardDetailDto();

                long card = rnd.Next(max) % (max - min + 1) + min;
                dto.CardData10 = Convert.ToUInt64(card);
                dto.CardData16 = GetCardDataHex(dto.CardData10);

                BindDto(dto, dto.CardData10);
                CardList.Add(dto);
            }
            dataGridView1.DataSource = new BindingList<CardDetailDto>(CardList);
        }

        /// <summary>
        /// 从列表删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButDelList_Click(object sender, EventArgs e)
        {
            int count = dataGridView1.Rows.Count;
            for (int i = count - 1; i >= 0; i--)
            {
                DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)dataGridView1.Rows[i].Cells[0];
                bool bSelected = Convert.ToBoolean(checkCell.Value);
                if (bSelected)
                {
                    DataGridViewTextBoxCell text = (DataGridViewTextBoxCell)dataGridView1.Rows[i].Cells[2];
                    CardList.RemoveAt(CardList.FindIndex(t => t.CardData10 == Convert.ToUInt32(text.Value)));
                }
            }
            dataGridView1.DataSource = new BindingList<CardDetailDto>(CardList);
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
            List<FC8800.Data.CardDetail> _cardList = new List<FC8800.Data.CardDetail>();

            CardDetailDto dto = new CardDetailDto();

            dto.CardData10 = Convert.ToUInt64(txtCardData.Text);
            dto.CardData16 = GetCardDataHex(dto.CardData10);

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
        }

        private void TxtCardData_TextChanged(object sender, EventArgs e)
        {

        }

        private void TxtHoliday_TextChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 返回16进制卡号
        /// </summary>
        public string GetCardDataHex(ulong CardData)
        {
            byte[] tmpBuf = StringUtility.DecimalToBytes(CardData);
            return StringUtility.ByteToHex(tmpBuf);

        }

        /// <summary>
        /// 返回门权限数组
        /// </summary>
        public short[] GetDoorList(int Door)
        {

            return null;
            //var value = StringUtility.TenToBinary(Door);
            //return StringUtility.BinaryToByte(value);
        }

        /// <summary>
        /// 返回出入标记数组
        /// 144 -> 10010000
        /// </summary>
        /// <returns></returns>
        public short[] GetEnterStatusList(byte EnterStatus)
        {
            short[] list = new short[4];
            byte[] bitList = StringUtility.ByteToBit(EnterStatus);
            int index = 0;
            for (int i = bitList.Length - 1; i > 0; i = i - 2)
            {
                var value = bitList[i].ToString() + bitList[i - 1].ToString();
                list[index] = Convert.ToInt16(value, 2);
                index++;
            }
            //var binary = StringUtility.ByteToBit(EnterStatus).ToString();
            //int index = 0;
            //for (int i = 0; i < 8; i = i + 2)
            //{
            //    string value = binary.Substring(i, 2);
            //    list[index] = Convert.ToInt16(value, 2);
            //    index++;
            //}
            return list;
        }
    }
}
