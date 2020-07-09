using DoNetDrive.Core.Command;
using DoNetDrive.Protocol.Fingerprint.Test.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DoNetDrive.Core.Extension;
using DoNetDrive.Protocol.Fingerprint.Person;

namespace DoNetDrive.Protocol.Fingerprint.Test
{
    public partial class frmPerson : frmNodeForm
    {
        #region 单例模式

        private static object lockobj = new object();
        private static frmPerson onlyObj;
        public static frmPerson GetForm(INMain main)
        {
            if (onlyObj == null)
            {
                lock (lockobj)
                {
                    if (onlyObj == null)
                    {
                        onlyObj = new frmPerson(main);
                        frmMain.AddNodeForms(onlyObj);
                    }
                }
            }
            return onlyObj;
        }
        #endregion

        BindingList<Person_UI> PersonList = null;

        /// <summary>
        /// 卡号字典
        /// </summary>
        HashSet<uint> CardHashTable = null;

        string mPersonImagePath;
        private frmPerson(INMain main)
        {
            InitializeComponent();
            mMainForm = main;
        }

        string[] mCardStatusList = new string[] { "正常状态", "挂失", "黑名单", "已删除" };

        string[] mEnterStatusList = new string[] { "出入有效", "入有效", "出有效" };

        string[] mIdentityList = new string[] { "普通用户", "管理员" };

        string[] mCardTypeList = new string[] { "普通卡", "常开" };

        private void InitControl()
        {
            TimeGroup();
            CardStatus();
            EnterStatus();
            Identity();
            OpenTimes();
        }

        public void OpenTimes()
        {
            cmbOpenTimes.Items.Clear();
            cmbOpenTimes.Items.Add("(0)已失效");

            string[] time = new string[300];
            for (int i = 1; i <= 300; i++)
            {
                time[i - 1] = i + "次";
            }
            cmbOpenTimes.Items.AddRange(time);
            cmbOpenTimes.Items.Add("无限制(65535)");
            cmbOpenTimes.SelectedIndex = cmbOpenTimes.Items.Count - 1;
        }

        private void Identity()
        {
            cmbIdentity.Items.Clear();
            cmbIdentity.Items.AddRange(mIdentityList);
            cmbIdentity.SelectedIndex = 0;
        }

        private void EnterStatus()
        {
            cmbEnterStatus.Items.Clear();
            cmbEnterStatus.Items.AddRange(mEnterStatusList);
            cmbEnterStatus.SelectedIndex = 0;

        }

        private void CardStatus()
        {
            cmbCardStatus.Items.Clear();
            cmbCardStatus.Items.AddRange(mCardStatusList);
            cmbCardStatus.SelectedIndex = 0;

            cmbCardType.Items.Clear();
            cmbCardType.Items.AddRange(mCardTypeList);
            cmbCardType.SelectedIndex = 0;
        }

        public void TimeGroup()
        {
            string[] time = new string[64];
            for (int i = 0; i < 64; i++)
            {
                time[i] = "卡门时段" + (i + 1);
            }
            cmbTimeGroup.Items.Clear();
            cmbTimeGroup.Items.AddRange(time);
            cmbTimeGroup.SelectedIndex = 0;
        }


        private void FrmPerson_Load(object sender, EventArgs e)
        {
            dgPersonList.AutoGenerateColumns = false;
            PersonList = new BindingList<Person_UI>();
            CardHashTable = new HashSet<uint>();
            dgPersonList.DataSource = PersonList;
            InitControl();
        }

        private void BtnReadDatabaseDetail_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            var cmd = new ReadPersonDatabaseDetail(cmdDtl);
            mMainForm.AddCommand(cmd);
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadPersonDatabaseDetail_Result result = cmde.Command.getResult() as ReadPersonDatabaseDetail_Result;

                mMainForm.AddCmdLog(cmde, $"人员档案最大容量：{result.SortDataBaseSize},已存储人员数量：{result.SortPersonSize},\r\n指纹特征码最大容量：{result.SortFingerprintDataBaseSize}" +
                    $",已存储指纹数量：{result.SortFingerprintSize},\r\n人脸特征码最大容量：{result.SortFaceDataBaseSize},已存储人脸数量：{result.SortFaceSize}");
            };
        }

        private void BtnReadAllPerson_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            var cmd = new ReadPersonDataBase(cmdDtl);
            cmdDtl.Timeout = 15000;
            mMainForm.AddCommand(cmd);
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadPersonDataBase_Result result = cmde.Command.getResult() as ReadPersonDataBase_Result;
                StringBuilder sLogs = new StringBuilder();
                Invoke(() =>
                {
                    if (result.DataBaseSize > 0)
                    {
                        foreach (Data.Person person in result.PersonList)
                        {
                            AddPersonToList(person);
                            DebugPersonDetail(person, sLogs);
                        }
                    }
                    PersonList.RaiseListChangedEvents = true;
                    PersonList.ResetBindings();
                });



                mMainForm.AddCmdLog(cmde, $"读取到的人员数:{result.DataBaseSize}");
                if (sLogs.Length > 0)
                {
                    string sFile = frmRecord.SaveFile(sLogs, $"读取所有卡片_{DateTime.Now:yyyyMMddHHmmss}.txt");
                    mMainForm.AddCmdLog(cmde, $"卡片日志保存路径:{sFile}");
                }
            };
        }

        private void BtnClearDataBase_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            var cmd = new ClearPersonDataBase(cmdDtl);
            mMainForm.AddCommand(cmd);
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                mMainForm.AddLog($"命令成功");
            };
        }

        private void BtnWriteAllPerson_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            cmdDtl.Timeout = 15000;
            cmdDtl.RestartCount = 0;
            INCommand cmd = null;
            List<Data.Person> persons = new List<Data.Person>();
            foreach (var item in PersonList)
            {
                if (item.Person.CardData < UInt32.MaxValue)
                {
                    persons.Add(item.Person);
                }
            }
            if (persons.Count > 0)
            {
                AddPerson_Parameter par = new AddPerson_Parameter(persons);
                cmd = new AddPerson(cmdDtl, par);
            }

            if (cmd == null) return;
            mMainForm.AddCommand(cmd);

            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                var result = cmd.getResult() as WritePerson_Result;
                WritePersonCallBlack(cmde, result);
            };
        }

        private void BtnClearList_Click(object sender, EventArgs e)
        {
            PersonList.Clear();
            CardHashTable.Clear();
        }

        private void DgPersonList_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int col = e.ColumnIndex, row = e.RowIndex;
            if (row < 0) return;
            var gdRow = dgPersonList.Rows[row];
            var PersonUI = gdRow.DataBoundItem as Person_UI;
            StringBuilder strBuf = new StringBuilder();

            DebugPerson(PersonUI.Person, strBuf);
            //txtDebug.Text = strBuf.ToString();
            PersonToControl(PersonUI.Person);
        }

        private void DebugPerson(Data.Person person, StringBuilder sLogs)
        {
            var ui = new Person_UI(person);
            DebugPersonDetail(person, sLogs);
            sLogs.AppendLine();
        }

        /// <summary>
        /// 将卡片输出到buf中
        /// </summary>
        /// <param name="card"></param>
        private StringBuilder DebugPersonDetail(Data.Person person, StringBuilder strBuf)
        {
            Person_UI ui = new Person_UI(person);
            strBuf.Append("用户号：").Append(ui.UserCode).Append("；姓名：").Append(ui.PName).Append("；部门：").Append(ui.Dept).Append("；职务：").Append(ui.Job).Append("；工号：").Append(ui.PCode);
            strBuf.Append("卡号：").Append(ui.CardData).Append("；密码：").Append(ui.Password);
            strBuf.Append("；有效期：").Append(ui.Expiry).Append("；有效次数：").Append(ui.OpenTimes).AppendLine("；");
            strBuf.Append("；开门时段：").Append(ui.TimeGroup);
            strBuf.Append("；状态：").Append(ui.CardStatus).Append("；用户身份：").AppendLine(ui.Identity);
            strBuf.Append("；节假日：").Append(ui.Holiday).Append("(1->32)");
            strBuf.Append("；出入标志：").Append(ui.EnterStatus);
            strBuf.Append("；最近读卡时间：").AppendLine(ui.ReadCardDate);
            strBuf.Append("；是否有人脸：").AppendLine(ui.IsFaceFeature);
            strBuf.Append("；指纹数：").AppendLine(ui.FingerprintCount.ToString());
            return strBuf;
        }


        /// <summary>
        /// 将卡片输出到控件中
        /// </summary>
        /// <param name="person"></param>
        private void PersonToControl(Data.Person person)
        {
            Person_UI ui = new Person_UI(person);
            txtCardDataHex.Text = person.CardData.ToString("X");
            txtCardData.Text = person.CardData.ToString();
            txtPassword.Text = ui.Password;
            dtpDate.Value = person.Expiry;
            dtpTime.Value = person.Expiry;
            cmbOpenTimes.Text = ui.OpenTimes;
            if (person.CardStatus < 4)
                cmbCardStatus.SelectedIndex = person.CardStatus;

            cmbTimeGroup.SelectedIndex = person.TimeGroup - 1;
            txtUserCode.Text = person.UserCode.ToString();
            txtPName.Text = person.PName;
            txtPCode.Text = person.PCode;
            txtJob.Text = person.Job;
            txtDept.Text = person.Dept;
            try
            {
                cmbEnterStatus.SelectedIndex = person.EnterStatus;
                cmbIdentity.SelectedIndex = person.Identity;
            }
            catch (Exception)
            {

            }

            //if (person.HolidayUse)
            txtHoliday.Text = ui.Holiday;
            //else
            //    txtHoliday.Text = new string('1', 30);

        }

        private void ButCreateCardNumByRandom_Click(object sender, EventArgs e)
        {
            int iCreateCount = CheckCreateCardCount();
            if (iCreateCount <= 0) return;

            Data.Person person;
            for (int i = 0; i < iCreateCount; i++)
            {
                person = CreateNewPerson(0);
                AddPersonToList(person);
            }
            PersonList.RaiseListChangedEvents = true;
            PersonList.ResetBindings();
        }

        /// <summary>
        /// 将一个人员详情添加到系统缓冲中
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        private bool AddPersonToList(Data.Person person)
        {

            if (!CardHashTable.Contains(person.UserCode))
            {
                var ui = new Person_UI(person);
                ui.CardIndex = (PersonList.Count + 1).ToString();
                PersonList.Add(ui);
                CardHashTable.Add(person.UserCode);
                return true;
            }
            return false;
        }

        private Data.Person AddPersonDetailToList()
        {
            Data.Person card = ContorlToPerson();
            if (card == null) return null;
            //检查卡片是否已存在
            if (CardHashTable.Contains(card.UserCode))
            {
                int iCount = PersonList.Count;
                for (int i = 0; i < iCount; i++)
                {
                    if (PersonList[i].Person.CardData == card.CardData)
                    {
                        PersonList[i].Person = card;
                        PersonList.ResetItem(i);
                        break;
                    }
                }

            }
            else
            {
                CardHashTable.Add(card.UserCode);
                PersonList.Add(new Person_UI(card));

            }
            return card;
        }

        private Random mCardRnd = new Random();
        private int mCardMax = 0x6FFFFFFF;
        private int mCardMin = 0x10000000;

        private Data.Person CreateNewPerson(uint iUserCode)
        {
            UInt64 cardNum = 0, cardNum2 = 0;
            uint userCode = 0;
            Data.Person person;
            if (iUserCode == 0)
            {
                cardNum = (UInt64)(mCardRnd.Next(mCardMax) % (mCardMax - mCardMin + 1) + mCardMin);

                cardNum2 = (UInt64)(mCardRnd.Next(mCardMax) % (mCardMax - mCardMin + 1) + mCardMin);
                cardNum = (cardNum << 32) + cardNum2;

                userCode = (uint)(mCardRnd.Next(mCardMax) % (mCardMax - mCardMin + 1) + mCardMin);
            }
            else
            {
                userCode = iUserCode;
            }
            if (CardHashTable.Contains(userCode))
            {
                if (iUserCode == 0)
                {
                    //有重复
                    return CreateNewPerson(0);
                }
                else
                {
                    return null;
                }

            }
            person = new Data.Person();
            return person;
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
                MessageBox.Show("输入的数字不正确，取值范围：1-0！");
                return 0;
            }
            if (iCreateCount > 0)
            {
                MessageBox.Show("输入的数字不正确，取值范围：1-0！");
                return 0;
            }
            if ((iCreateCount + PersonList.Count) > 0)
            {
                iCreateCount = 0 - PersonList.Count;

            }
            if (iCreateCount <= 0) return 0;

            return iCreateCount;
        }

        private void BtnDelList_Click(object sender, EventArgs e)
        {
            var lst = PersonList.Where(t => t.Selected == false).ToArray();

            if (lst.Length == 0)
                return;

            PersonList.RaiseListChangedEvents = false;
            PersonList.Clear();
            CardHashTable.Clear();
            foreach (var c in lst)
            {
                PersonList.Add(c);
                CardHashTable.Add(c.Person.UserCode);
            }
            PersonList.RaiseListChangedEvents = true;
            PersonList.ResetBindings();
        }

        private void BtnDelDevice_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            cmdDtl.Timeout = 15000;
            cmdDtl.RestartCount = 0;
            INCommand cmd = null;
            Data.Person person = ContorlToPerson();
            if (person == null) return;
            List<Data.Person> persons = new List<Data.Person>();
            persons.Add(person);
            var par = new DeletePerson_Parameter(persons);
            cmd = new DeletePerson(cmdDtl, par);

            mMainForm.AddCommand(cmd);
        }

        /// <summary>
        /// 将控件中的值转换为卡详情
        /// </summary>
        /// <returns></returns>
        private Data.Person ContorlToPerson()
        {
            Data.Person person = new Data.Person();
            string CardStr = txtCardData.Text;
            person.CardData = CardStr.ToUInt64();
            if (person.CardData < 0)
            {
                MsgErr("卡号输入不正确！");
                return null;
            }
            string sPwd = txtPassword.Text;
            if (!string.IsNullOrEmpty(sPwd))
            {
                if (!sPwd.IsNum())
                {
                    MsgErr("个人密码必须输入数字！");
                    return null;
                }
                if (sPwd.Length < 4)
                {
                    MsgErr("个人密码请输入 4-8个数字！");
                    return null;
                }
                person.Password = sPwd.FillString(8, "F");
            }
            //有效期
            var dtpD = dtpDate.Value; var dtpT = dtpTime.Value;
            person.Expiry = new DateTime(dtpD.Year, dtpD.Month, dtpD.Day, dtpT.Hour, dtpT.Minute, 59);
            person.UserCode = uint.Parse(txtUserCode.Text);
            person.PName = txtPName.Text;
            person.PCode = txtPCode.Text;
            person.Dept = txtDept.Text;
            person.Job = txtJob.Text;
            person.TimeGroup = cmbTimeGroup.SelectedIndex + 1;
            person.CardStatus = cmbCardStatus.SelectedIndex;
            person.CardType = cmbCardType.SelectedIndex;
            person.Identity = cmbIdentity.SelectedIndex;
            if (cmbOpenTimes.Text == Person_UI.OpenTimes_Invalid)
                person.OpenTimes = 0;
            else
            {
                if (cmbOpenTimes.Text == Person_UI.OpenTimes_Off)
                    person.OpenTimes = 65535;
                else
                {
                    string sTimes = cmbOpenTimes.Text.Replace("次", string.Empty).Trim();
                    if (sTimes.IsNum())
                    {
                        person.OpenTimes = Convert.ToUInt16(sTimes);
                    }
                }
            }

            string sHol = txtHoliday.Text.Trim();
            sHol.FillString(32, "0");
            var chars = sHol.ToCharArray();
            for (int i = 0; i < 32; i++)
            {
                person.SetHolidayValue(i + 1, chars[i] == '1');
            }
            return person;

        }

        private void BtnAddDevice_Click(object sender, EventArgs e)
        {
            Data.Person person = AddPersonDetailToList();
            if (person == null) return;

            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            //cmdDtl.Timeout = 10000;
            INCommand cmd;

            List<Data.Person> persons = new List<Data.Person>();
            persons.Add(person);
            var par = new AddPerson_Parameter(persons);
            cmd = new AddPerson(cmdDtl, par);


            mMainForm.AddCommand(cmd);

            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                var result = cmd.getResult() as WritePerson_Result;
                WritePersonCallBlack(cmde, result);
            };
        }

        private void WritePersonCallBlack(CommandEventArgs cmde, WritePerson_Result result)
        {
            if (result != null)
            {
                mMainForm.AddCmdLog(cmde, $"命令成功：写入失败的卡数量:{result.FailTotal}");
                if (result.FailTotal > 0)
                {
                    StringBuilder strBuf = new StringBuilder();
                    foreach (var item in result.PersonList)
                    {
                        strBuf.Append(item.ToString("00000000000000000000")).Append("(0x").Append(item.ToString("X18")).Append(")");
                    }
                    //txtDebug.Text = strBuf.ToString();
                    System.IO.File.WriteAllText(System.IO.Path.Combine(Application.StartupPath, "上传失败的卡号.txt"), strBuf.ToString(), Encoding.UTF8);
                }
            }
        }

        private void ChkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            bool bSelect = chkSelectAll.Checked;
            foreach (var item in PersonList)
            {
                item.Selected = bSelect;
            }
        }

        private void BtnAddList_Click(object sender, EventArgs e)
        {
            AddPersonDetailToList();
        }

        private void BtnCheckUserCode_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            uint usercode = 0;
            if (!uint.TryParse(txtUserCode.Text, out usercode))
            {
                MessageBox.Show("用户号格式不正确");
                return;
            }
            var par = new ReadPersonDetail_Parameter(usercode);
            var cmd = new ReadPersonDetail(cmdDtl, par);
            mMainForm.AddCommand(cmd);

            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                var result = cmd.getResult() as ReadPersonDetail_Result;

                if (!result.IsReady)
                {
                    mMainForm.AddCmdLog(cmde, $"人员不存在");
                }
                else
                {
                    PersonToControl(result.Person);
                    mMainForm.AddCmdLog(cmde, $"人员存在");
                }
            };
        }

        private void butSelectImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "图片文件|*.jpg;*.jpeg;*.bmp;*.png";
            ofd.Multiselect = false;
            if (ofd.ShowDialog() != DialogResult.OK) return;

            mPersonImagePath = ofd.FileName;
        }

        private void btnAddPesonAndImage_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(mPersonImagePath)) return;
            Data.Person person = AddPersonDetailToList();
            if (person == null) return;

            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            //cmdDtl.Timeout = 10000;
            INCommand cmd;

            byte[] datas = System.IO.File.ReadAllBytes(mPersonImagePath);
            var par = new AddPersonAndImage_Parameter(person, datas);
            cmd = new AddPeosonAndImage(cmdDtl, par);


            mMainForm.AddCommand(cmd);

            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                var result = cmd.getResult() as WritePerson_Result;
                WritePersonCallBlack(cmde, result);
            };
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            int sCode = int.Parse(txtRegUserCode.Text);
            string sName = txtRegUserName.Text;

            Data.Person person = new Data.Person((uint)sCode, sName);

            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            //cmdDtl.Timeout = 10000;
            INCommand cmd;

            var par = new RegisterIdentificationData_Parameter(person, 3);
            cmd = new RegisterIdentificationData(cmdDtl, par);


            mMainForm.AddCommand(cmd);

            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                var result = cmd.getResult() as RegisterIdentificationData_Result;
                if (result.Status == 101)
                {
                    if (result.ResultData != null)
                    {
                        string sFile = System.IO.Path.Combine(Application.StartupPath, $"Photo_{sCode}.jpg");
                        var buf = result.ResultData.DataBuf;
                        System.IO.File.WriteAllBytes(sFile, buf);
                        using (var ms = new System.IO.MemoryStream(buf))
                        {
                            Image img = Image.FromStream(ms);
                            picReg.Image = img;
                        }
                        
                        MsgTip("注册成功！");
                    }

                }
                else
                {
                    MessageBox.Show($"注册失败--{result.Status}");
                }
            };
        }
    }
}
