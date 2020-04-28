using DoNetDrive.Protocol.POS.SystemParameter.Buzzer;
using DoNetDrive.Protocol.POS.SystemParameter.ConnectPassword;
using DoNetDrive.Protocol.POS.SystemParameter.Deadline;
using DoNetDrive.Protocol.POS.SystemParameter.ReceiptPrint;
using DoNetDrive.Protocol.POS.SystemParameter.RecordStorageMode;
using DoNetDrive.Protocol.POS.SystemParameter.Relay;
using DoNetDrive.Protocol.POS.SystemParameter.ScreenDisplay.DisplayContent;
using DoNetDrive.Protocol.POS.SystemParameter.ScreenDisplay.Logo;
using DoNetDrive.Protocol.POS.SystemParameter.ScreenDisplay.Message;
using DoNetDrive.Protocol.POS.SystemParameter.ScreenDisplay.Name;
using DoNetDrive.Protocol.POS.SystemParameter.ScreenDisplay.Title;
using DoNetDrive.Protocol.POS.SystemParameter.SN;
using DoNetDrive.Protocol.POS.SystemParameter.USBDisk;
using DoNetDrive.Protocol.POS.SystemParameter.Version;
using DoNetDrive.Protocol.POS.SystemParameter.Voice;
using DoNetDrive.Protocol.POS.SystemParameter.WIFIAccount;
using DoNetTool.Common.Extensions;
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

        public frmSystem()
        {
            InitializeComponent();
        }

        string[] mRecordStorage = { "满循环", "满报警" };
        List<string> mPrintLocation = new List<string> { "","页头", "页尾" };
        List<string> mRelayMode = new List<string> { "", "COM-NO", "COM-NC", "双稳态" };

        #region SN
        private void butReadSN_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadSN cmd = new ReadSN(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                SN_Result result = cmde.Command.getResult() as SN_Result;
                string sn = Encoding.ASCII.GetString(result.SNBuf);
                Invoke(() =>
                {
                    txtSN.Text = sn;
                });
                mMainForm.AddCmdLog(cmde, sn);
            };
        }

        private void butWriteSN_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region 密码
        private void butReadConnectPassword_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadConnectPassword cmd = new ReadConnectPassword(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                Password_Result result = cmde.Command.getResult() as Password_Result;
                string pwd = result.Password;
                Invoke(() =>
                {
                    txtConnectPassword.Text = pwd;
                });
                mMainForm.AddCmdLog(cmde, pwd);
            };
        }

        private void butWriteConnectPassword_Click(object sender, EventArgs e)
        {

        }

        #endregion
        private void btnReadVersion_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadVersion cmd = new ReadVersion(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadVersion_Result result = cmde.Command.getResult() as ReadVersion_Result;
                string version = result.Version.ToString();
                Invoke(() =>
                {
                    txtVersion.Text = "Ver " + version;
                });
                version = "版本号：" + version;
                mMainForm.AddCmdLog(cmde, version);
            };
        }

        #region TCP
        private void butRendTCPSetting_Click(object sender, EventArgs e)
        {

        }

        private void butWriteTCPSetting_Click(object sender, EventArgs e)
        {

        }

        #endregion

        private void butReadDeadline_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadDeadline cmd = new ReadDeadline(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadDeadline_Result result = cmde.Command.getResult() as ReadDeadline_Result;
                Invoke(() =>
                {
                    dtDeadline.Value = result.Deadline;
                });
                mMainForm.AddCmdLog(cmde, result.Deadline.ToString("yyyy-MM-dd"));
            };
        }

        private void butWriteDeadline_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            WriteDeadline_Parameter par = new WriteDeadline_Parameter(dtDeadline.Value);
            WriteDeadline cmd = new WriteDeadline(cmdDtl,par);
            mMainForm.AddCommand(cmd);

            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                mMainForm.AddCmdLog(cmde, "命令执行成功");
            };

        }

        private void butReadRecordStorageMode_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadRecordStorageMode cmd = new ReadRecordStorageMode(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadRecordStorageMode_Result result = cmde.Command.getResult() as ReadRecordStorageMode_Result;
                Invoke(() =>
                {
                    cmbRecordStorageMode.SelectedIndex = result.Mode;
                });
                mMainForm.AddCmdLog(cmde, mRecordStorage[result.Mode]);
            };
        }

        private void butWriteRecordStorageMode_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            WriteRecordStorageMode_Parameter par = new WriteRecordStorageMode_Parameter(cmbRecordStorageMode.SelectedIndex);
            WriteRecordStorageMode cmd = new WriteRecordStorageMode(cmdDtl, par);
            mMainForm.AddCommand(cmd);

            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                mMainForm.AddCmdLog(cmde, "命令执行成功");
            };

        }

        private void frmSystem_Load(object sender, EventArgs e)
        {
            cmbRecordStorageMode.Items.AddRange(mRecordStorage);
            cmbRecordStorageMode.SelectedIndex = 0;

            cmbRelayMode.Items.AddRange(mRelayMode.ToArray());
            cmbRelayMode.SelectedIndex = 0;
        }

        private void butReadName_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadName cmd = new ReadName(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadName_Result result = cmde.Command.getResult() as ReadName_Result;
                Invoke(() =>
                {
                    txtName.Text = result.Name;
                });
                mMainForm.AddCmdLog(cmde, result.Name);
            };
        }

        private void butWriteName_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            WriteName_Parameter par = new WriteName_Parameter(txtName.Text.Trim());
            WriteName cmd = new WriteName(cmdDtl, par);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
               
                mMainForm.AddCmdLog(cmde, "命令执行成功");
            };
        }

        private void butReadTitle_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadTitle cmd = new ReadTitle(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadTitle_Result result = cmde.Command.getResult() as ReadTitle_Result;
                Invoke(() =>
                {
                    txtTitle.Text = result.Title;
                });
                mMainForm.AddCmdLog(cmde, result.Title);
            };
        }

        private void butWriteTitle_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            WriteTitle_Parameter par = new WriteTitle_Parameter(txtTitle.Text.Trim());
            WriteTitle cmd = new WriteTitle(cmdDtl, par);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                mMainForm.AddCmdLog(cmde, "命令执行成功");
            };
        }

        private void butReadMessage_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadMessage cmd = new ReadMessage(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadMessage_Result result = cmde.Command.getResult() as ReadMessage_Result;
                Invoke(() =>
                {
                    txtMessage.Text = result.Message;
                });
                mMainForm.AddCmdLog(cmde, result.Message);
            };

        }

        private void butWriteMessage_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            WriteMessage_Parameter par = new WriteMessage_Parameter(txtMessage.Text.Trim());
            WriteMessage cmd = new WriteMessage(cmdDtl, par);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                mMainForm.AddCmdLog(cmde, "命令执行成功");
            };
        }

        private void butReadShow_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadDisplayContent cmd = new ReadDisplayContent(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadDisplayContent_Result result = cmde.Command.getResult() as ReadDisplayContent_Result;
                bool showName = result.Name == 1;
                bool showPCode = result.PCode == 1;
                bool showDept = result.Dept == 1;
                bool showBalance = result.Balance == 1;
                Invoke(() =>
                {
                    cbShowName.Checked = showName;
                    cbShowPcode.Checked = showPCode;
                    cbShowDept.Checked = showDept;
                    cbShowMoney.Checked = showBalance;
                });
                //mMainForm.AddCmdLog(cmde, result.Message);
            };

        }

        private void butWriteShow_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            byte bShowName = (byte)(cbShowName.Checked ? 1 : 0);
            byte bShowPcode = (byte)(cbShowPcode.Checked ? 1 : 0);
            byte bShowDept = (byte)(cbShowDept.Checked ? 1 : 0);
            byte bShowMoney = (byte)(cbShowMoney.Checked ? 1 : 0);
            WriteDisplayContent_Parameter par = new WriteDisplayContent_Parameter(bShowName,bShowPcode,bShowDept,0,bShowMoney);
            WriteDisplayContent cmd = new WriteDisplayContent(cmdDtl, par);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                mMainForm.AddCmdLog(cmde, "命令执行成功");
            };
        }

        private void butReadLogo_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadLogo cmd = new ReadLogo(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadLogo_Result result = cmde.Command.getResult() as ReadLogo_Result;
                Invoke(() =>
                {
                    txtLogo.Text = result.Logo;
                    txtPhone.Text = result.Phone;
                });
                //mMainForm.AddCmdLog(cmde, result.Message);
            };
        }

        private void butWriteLogo_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            byte bShowName = (byte)(cbShowName.Checked ? 1 : 0);
            byte bShowPcode = (byte)(cbShowPcode.Checked ? 1 : 0);
            byte bShowDept = (byte)(cbShowDept.Checked ? 1 : 0);
            byte bShowMoney = (byte)(cbShowMoney.Checked ? 1 : 0);
            WriteLogo_Parameter par = new WriteLogo_Parameter(txtLogo.Text,txtPhone.Text);
            WriteLogo cmd = new WriteLogo(cmdDtl, par);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                mMainForm.AddCmdLog(cmde, "命令执行成功");
            };
        }

        private void btnReadUSBDisk_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadUSBDisk cmd = new ReadUSBDisk(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadUSBDisk_Result result = cmde.Command.getResult() as ReadUSBDisk_Result;
                bool bUploadCardList = result.UploadCardList == 1;
                bool bUploadMenu = result.UploadMenu == 1;
                bool bUploadTimeGroup = result.UploadTimeGroup == 1;
                bool bUploadCardTypeList = result.UploadCardTypeList == 1;
                bool bUploadConsumeParameter = result.UploadConsumeParameter == 1;
                bool bUploadUpgrade = result.UploadUpgrade == 1;

                bool bDownloadCardList = result.DownloadCardList == 1;
                bool bDownloadMenu = result.DownloadMenu == 1;
                bool bDownloadTimeGroup = result.DownloadTimeGroup == 1;
                bool bDownloadCardTypeList = result.DownloadCardTypeList == 1;
                bool bDownloadConsumeParameter = result.DownloadConsumeParameter == 1;
                bool bDownloadTransaction = result.DownloadTransaction == 1;
                bool bDownloadSystemTransaction = result.DownloadSystemTransaction == 1;

                Invoke(() =>
                {
                    cbUploadCardList.Checked = bUploadCardList;
                    cbUploadMenu.Checked = bUploadMenu;
                    cbUploadTimeGroup.Checked = bUploadTimeGroup;
                    cbUploadCardTypeList.Checked = bUploadCardTypeList;
                    cbUploadConsumeParameter.Checked = bUploadConsumeParameter;
                    cbUpgrade.Checked = bUploadUpgrade;

                    cbDownloadCardList.Checked = bDownloadCardList;
                    cbDownloadMenu.Checked = bDownloadMenu;
                    cbDownloadTimeGroup.Checked = bDownloadTimeGroup;
                    cbDownloadCardTypeList.Checked = bDownloadCardTypeList;
                    cbDownloadConsumeParameter.Checked = bDownloadConsumeParameter;
                    cbDownloadTransaction.Checked = bDownloadTransaction;
                    cbDownloadSystemTransaction.Checked = bDownloadSystemTransaction;
                });
                //mMainForm.AddCmdLog(cmde, result.Message);
            };
        }

        private void btnWriteUSBDisk_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            byte bUploadCardList = (byte)(cbUploadCardList.Checked ? 1 : 0);
            byte bUploadMenu = (byte)(cbUploadMenu.Checked ? 1 : 0);
            byte bUploadTimeGroup = (byte)(cbUploadTimeGroup.Checked ? 1 : 0);
            byte bUploadCardTypeList = (byte)(cbUploadCardTypeList.Checked ? 1 : 0);

            byte bUploadConsumeParameter = (byte)(cbUploadConsumeParameter.Checked ? 1 : 0);
            byte bUpgrade = (byte)(cbUpgrade.Checked ? 1 : 0);
            byte bDownloadCardList = (byte)(cbDownloadCardList.Checked ? 1 : 0);
            byte bDownloadMenu = (byte)(cbDownloadMenu.Checked ? 1 : 0);
            byte bDownloadTimeGroup = (byte)(cbDownloadTimeGroup.Checked ? 1 : 0);
            byte bDownloadCardTypeList = (byte)(cbDownloadCardTypeList.Checked ? 1 : 0);
            byte bDownloadConsumeParameter = (byte)(cbDownloadConsumeParameter.Checked ? 1 : 0);
            byte bDownloadTransaction = (byte)(cbDownloadTransaction.Checked ? 1 : 0);
            byte bDownloadSystemTransaction = (byte)(cbDownloadSystemTransaction.Checked ? 1 : 0);
            WriteUSBDisk_Parameter par = new WriteUSBDisk_Parameter(bUploadCardList,bUploadMenu,bUploadTimeGroup,bUploadCardTypeList,bUploadConsumeParameter,bUpgrade,bDownloadCardList,bDownloadMenu
                ,bDownloadTimeGroup,bDownloadCardTypeList,bDownloadConsumeParameter,bDownloadTransaction,bDownloadSystemTransaction);
            WriteUSBDisk cmd = new WriteUSBDisk(cmdDtl, par);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                mMainForm.AddCmdLog(cmde, "命令执行成功");
            };
        }

        private void butReadPrintCount_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadReceiptPrint cmd = new ReadReceiptPrint(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadReceiptPrint_Result result = cmde.Command.getResult() as ReadReceiptPrint_Result;
                bool bIsOpen = result.IsOpen == 1;
                Invoke(() =>
                {
                    cbIsOpenPrint.Checked = bIsOpen;
                    cmbPrintCount.SelectedIndex = result.PrintCount - 1;
                });
                //mMainForm.AddCmdLog(cmde, result.Message);
            };
        }

        private void butWritePrintCount_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            byte bIsOpenPrint = (byte)(cbIsOpenPrint.Checked ? 1 : 0);
            byte bPrintCount = (byte)(cmbPrintCount.SelectedIndex + 1);
            WriteReceiptPrint_Parameter par = new WriteReceiptPrint_Parameter(bIsOpenPrint, bPrintCount);
            WriteReceiptPrint cmd = new WriteReceiptPrint(cmdDtl, par);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                mMainForm.AddCmdLog(cmde, "命令执行成功");
            };
        }

        private void butReadPrintContent_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadPrintContent cmd = new ReadPrintContent(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadReceiptPrint_Result result = cmde.Command.getResult() as ReadReceiptPrint_Result;
             
                Invoke(() =>
                {
                    dgvPrintContent.AutoGenerateColumns = false;
                    dgvPrintContent.DataSource = new BindingList<PrintContent>(result.PrintContents);
                    //dgvPrintContent.DataSource = result.PrintContents;
                });
                //mMainForm.AddCmdLog(cmde, result.Message);
            };
        }

        private void butWritePrintContent_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;

            List<PrintContent> printContents = new List<PrintContent>(6);
            for (int i = 0; i < dgvPrintContent.Rows.Count; i++)
            {
                DataGridViewCheckBoxCell cellIsOpen = (DataGridViewCheckBoxCell)dgvPrintContent.Rows[i].Cells[1];
                DataGridViewTextBoxCell cellContent = (DataGridViewTextBoxCell)dgvPrintContent.Rows[i].Cells[2];
                DataGridViewComboBoxCell cellLocation = (DataGridViewComboBoxCell)dgvPrintContent.Rows[i].Cells[3];

                PrintContent model = new PrintContent();
                model.Index = (byte)(i + 1);
                if ((bool)cellIsOpen.FormattedValue)
                    model.IsOpen =  1;
                else
                    model.IsOpen = 0;
                model.Content = cellContent.Value.ToString();
                model.Location = (byte)mPrintLocation.FindIndex(t => t == cellLocation.Value.ToString());
                printContents.Add(model);
            }
            WriteReceiptPrint_Parameter par = new WriteReceiptPrint_Parameter(printContents);
            WritePrintContent cmd = new WritePrintContent(cmdDtl, par);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                mMainForm.AddCmdLog(cmde, "命令执行成功");
            };
        }

        private void dgvPrintContent_CellMouseClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                DataGridViewTextBoxColumn textbox = dgvPrintContent.Columns[e.ColumnIndex] as DataGridViewTextBoxColumn;
                if (textbox != null) //如果该列是TextBox列
                {
                    dgvPrintContent.BeginEdit(true); //开始编辑状态
                    dgvPrintContent.ReadOnly = false;
                }

            }
            if (e.ColumnIndex == 3)
            {
                DataGridViewComboBoxColumn combobox = dgvPrintContent.Columns[e.ColumnIndex] as DataGridViewComboBoxColumn;
                if (combobox != null) //如果该列是TextBox列
                {
                    dgvPrintContent.BeginEdit(true); //开始编辑状态
                    dgvPrintContent.ReadOnly = false;
                }
            }
            if (e.ColumnIndex == 1)
            {
                DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)dgvPrintContent.Rows[e.RowIndex].Cells[e.ColumnIndex];
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

        private void butReadVoice_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadVoice cmd = new ReadVoice(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadVoice_Result result = cmde.Command.getResult() as ReadVoice_Result;
                bool bIsOpen = result.IsOpen == 1;
                bool bCardMoney = result.CardMoney == 1;
                bool bPayMoney = result.PayMoney == 1;
                bool bPasswordTip = result.PasswordTip == 1;
                bool bBlackList = result.BlackList == 1;
                bool bErrorTip = result.ErrorTip == 1;
                Invoke(() =>
                {
                    cbVoiceIsOpen.Checked = bIsOpen;
                    cbVoiceMoney.Checked = bCardMoney;
                    cbVoicePayMoney.Checked = bPayMoney;
                    cbVoicePasswordTip.Checked = bPasswordTip;
                    cbVoiceBlacklist.Checked = bBlackList;
                    cbVoiceErrorTip.Checked = bErrorTip;
                });
                //mMainForm.AddCmdLog(cmde, result.Message);
            };

            var cmdDtl2 = mMainForm.GetCommandDetail();
            if (cmdDtl2 == null) return;
            ReadVoiceStart cmd2 = new ReadVoiceStart(cmdDtl2);
            mMainForm.AddCommand(cmd2);

            //处理返回值
            cmdDtl2.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadVoiceStart_Result result = cmde.Command.getResult() as ReadVoiceStart_Result;
                bool bStart = result.Start == 1;
                bool bAdvertisement = result.Advertisement == 1;
              
                Invoke(() =>
                {
                    cbVoiceStart.Checked = bStart;
                    cbVoiceAdvertisement.Checked = bAdvertisement;
                    
                });
                //mMainForm.AddCmdLog(cmde, result.Message);
            };
        }

        private void butWriteVoice_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            byte bIsOpen = (byte)(cbVoiceIsOpen.Checked ? 1 : 0);
            byte bPayMoney = (byte)(cbVoicePayMoney.Checked ? 1 : 0);
            byte bMoney = (byte)(cbVoiceMoney.Checked ? 1 : 0);
            byte bBlacklist = (byte)(cbVoiceBlacklist.Checked ? 1 : 0);
            byte bErrorTip = (byte)(cbVoiceErrorTip.Checked ? 1 : 0);
            byte bPasswordTip = (byte)(cbVoicePasswordTip.Checked ? 1 : 0);
            WriteVoice_Parameter par = new WriteVoice_Parameter(bIsOpen,bMoney,bPayMoney,bBlacklist,bErrorTip,bPasswordTip);
            WriteVoice cmd = new WriteVoice(cmdDtl, par);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                mMainForm.AddCmdLog(cmde, "命令执行成功");
            };

            var cmdDtl2 = mMainForm.GetCommandDetail();
            if (cmdDtl2 == null) return;
            byte bVoiceStart = (byte)(cbVoiceStart.Checked ? 1 : 0);
            byte bAdvertisement = (byte)(cbVoiceAdvertisement.Checked ? 1 : 0);
            WriteVoiceStart_Parameter par2 = new WriteVoiceStart_Parameter(bVoiceStart, bAdvertisement);
            WriteVoiceStart cmd2 = new WriteVoiceStart(cmdDtl2, par2);
            mMainForm.AddCommand(cmd2);

            //处理返回值
            cmdDtl2.CommandCompleteEvent += (sdr, cmde) =>
            {
                mMainForm.AddCmdLog(cmde, "命令执行成功");
            };
        }

        private void butReadRelay_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadRelay cmd = new ReadRelay(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadRelay_Result result = cmde.Command.getResult() as ReadRelay_Result;
                bool bIsOpen = result.Use == 1;
                string tip = "";
                Invoke(() =>
                {
                    cbRelayIsOpen.Checked = bIsOpen;
                    txtOutputRetention.Value = decimal.Parse(result.OutputRetention.ToString());
                    cmbRelayMode.SelectedIndex = result.Mode - 1;
                });
                mMainForm.AddCmdLog(cmde, tip);
            };
        }

        private void butWriteRelay_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            byte bIsOpen = (byte)(cbRelayIsOpen.Checked ? 1 : 0);
            byte RelayMode = (byte)(cmbRelayMode.SelectedIndex + 1);
            byte OutputRetention = (byte)(txtOutputRetention.Value);
            WriteRelay_Parameter par = new WriteRelay_Parameter(bIsOpen, RelayMode, OutputRetention);
            WriteRelay cmd = new WriteRelay(cmdDtl, par);
            mMainForm.AddCommand(cmd);
        }

        private void butReadBuzzer_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadBuzzer cmd = new ReadBuzzer(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadBuzzer_Result result = cmde.Command.getResult() as ReadBuzzer_Result;
                bool bIsOpen = result.Buzzer == 1;
                string tip = "";
                Invoke(() =>
                {
                    cbBuzzerUse.Checked = bIsOpen;
                });
                mMainForm.AddCmdLog(cmde, tip);
            };
        }

        private void butWriteBuzzer_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            byte bIsOpen = (byte)(cbBuzzerUse.Checked ? 1 : 0);
            WriteBuzzer_Parameter par = new WriteBuzzer_Parameter(bIsOpen);
            WriteBuzzer cmd = new WriteBuzzer(cmdDtl, par);
            mMainForm.AddCommand(cmd);
        }

        private void butReadWIFIAccount_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadWIFIAccount cmd = new ReadWIFIAccount(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadWIFIAccount_Result result = cmde.Command.getResult() as ReadWIFIAccount_Result;
                
                string tip = "";
                Invoke(() =>
                {
                    txtAccount.Text = result.Account;
                    txtPassword.Text = result.Password;
                });
                mMainForm.AddCmdLog(cmde, tip);
            };
        }

        private void butWriteWIFIAccount_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            
            WriteWIFIAccount_Parameter par = new WriteWIFIAccount_Parameter(txtAccount.Text, txtPassword.Text);
            WriteWIFIAccount cmd = new WriteWIFIAccount(cmdDtl, par);
            mMainForm.AddCommand(cmd);
        }
    }
}
