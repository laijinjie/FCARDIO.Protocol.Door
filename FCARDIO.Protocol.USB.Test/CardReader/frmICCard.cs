using FCARDIO.Protocol.USB.CardReader.ICCard.SearchCard;
using FCARDIO.Protocol.USB.CardReader.ICCard.Sector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FCARDIO.Protocol.USB.CardReader.Test
{
    public partial class frmICCard : frmNodeForm
    {

        public static string[] mCardTypeList;

        public byte Type;

        #region 单例模式
        private static object lockobj = new object();
        private static frmICCard onlyObj;
        public static frmICCard GetForm(INMain main)
        {
            if (onlyObj == null)
            {
                lock (lockobj)
                {
                    if (onlyObj == null)
                    {
                        onlyObj = new frmICCard(main);
                        FrmMain.AddNodeForms(onlyObj);
                    }
                }
            }
            return onlyObj;
        }

        private frmICCard(INMain main) : base(main)
        {
            InitializeComponent();
            InitControl(0);
        }
        #endregion

        static frmICCard()
        {
            mCardTypeList = new string[] { "", "MF1 IC卡 S50", "NFC标签卡", "NFC手机", "身份证", "CPU IC卡 S50", "CPU卡", "MF1 IC卡 S70", "CPU IC卡 S70", "ID卡" };
        }

        private void BtnSearchCard_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            SearchCard cmd = new SearchCard(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                SearchCard_Result result = cmde.Command.getResult() as SearchCard_Result;

                Invoke(() =>
                {
                    plSector.Visible = result.IsSuccess;
                    Type = result.Type;
                    if (result.IsSuccess)
                    {
                        txtCardData.Text = result.CardData;
                        txtCardType.Text = mCardTypeList[Type];


                    }
                    else
                    {
                        txtCardData.Text = "";
                        txtCardType.Text = "";
                    }
                    if (true)
                    {

                    }
                });
                //mMainForm.AddCmdLog(cmde, $"{txtVersion.Text}");
            };
        }

        private void InitControl(byte type)
        {
            cmbNumber.Items.Clear();
            cmbStartBlock.Items.Clear();
            cmbReadCount.Items.Clear();

            cmbNumber.Items.Add("0");
            cmbNumber.Items.Add("1");
            cmbNumber.SelectedIndex = 1;
            cmbStartBlock.Items.Add("0");
            cmbStartBlock.SelectedIndex = 0;

            string[] listCount = new string[48];
            for (int i = 0; i < 48; i++)
            {
                listCount[i] = (i + 1).ToString();
            }
            cmbReadCount.Items.AddRange(listCount);
            cmbReadCount.SelectedIndex = 0;

            cmbVerifyMode.Items.Clear();
            cmbVerifyMode.Items.AddRange(new string[] { "A密钥", "B密钥" });
            cmbVerifyMode.SelectedIndex = 0;
            //if (type != 0)
            //{
            //    int number = 0;
            //    int block = 0;
            //    if (type == 1 || type == 5)
            //    {
            //        number = 15;
            //        block = 3;
            //    }
            //    else if (type == 7 || type == 8)
            //    {

            //    }
            //}
        }

        private void BtnReadSector_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;

            ReadSector_Parameter par = new ReadSector_Parameter(Type,(byte)cmbNumber.SelectedIndex, (byte)cmbStartBlock.SelectedIndex
                ,(byte)(cmbReadCount.SelectedIndex + 1),(byte)(cmbVerifyMode.SelectedIndex+1),txtPassword.Text);
            ReadSector cmd = new ReadSector(cmdDtl,par);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadSector_Result result = cmde.Command.getResult() as ReadSector_Result;

                Invoke(() =>
                {

                    txtContent.Text = result.Content;

                });
                //mMainForm.AddCmdLog(cmde, $"{txtVersion.Text}");
            };
        }

        private void BtnWriteSector_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;

            WriteSector_Parameter par = new WriteSector_Parameter(Type, (byte)cmbNumber.SelectedIndex, (byte)cmbStartBlock.SelectedIndex
                ,  (byte)(cmbVerifyMode.SelectedIndex + 1), txtPassword.Text,txtContent.Text);
            WriteSector cmd = new WriteSector(cmdDtl, par);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                //mMainForm.AddCmdLog(cmde, $"{txtVersion.Text}");
            };
        }
    }
}
