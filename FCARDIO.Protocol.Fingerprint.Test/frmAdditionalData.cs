using FCARDIO.Protocol.Fingerprint.AdditionalData.DeleteFeatureCode;
using FCARDIO.Protocol.Fingerprint.AdditionalData.PersonDetail;
using FCARDIO.Protocol.Fingerprint.AdditionalData.ReadFeatureCode;
using FCARDIO.Protocol.Fingerprint.AdditionalData.WriteFeatureCode;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FCARDIO.Protocol.Fingerprint.Test
{
    public partial class frmAdditionalData : frmNodeForm
    {
        #region 单例模式

        private static object lockobj = new object();
        private static frmAdditionalData onlyObj;

        public static frmAdditionalData GetForm(INMain main)
        {
            if (onlyObj == null)
            {
                lock (lockobj)
                {
                    if (onlyObj == null)
                    {
                        onlyObj = new frmAdditionalData(main);
                        frmMain.AddNodeForms(onlyObj);
                    }
                }
            }
            return onlyObj;
        }
        #endregion


        private frmAdditionalData(INMain main)
        {
            InitializeComponent();
            mMainForm = main;
            InitControl();
        }

        string[] mUploadTypeList = new string[] { "人员头像照片", "指纹特征码", "人脸特征码" };
        string[] mDownloadTypeList = new string[] { "人员头像", "指纹特征码", "记录照片", "人脸特征码" };
        /// <summary>
        /// 
        /// </summary>
        private void InitControl()
        {
            cmbUploadType.Items.Clear();
            cmbUploadType.Items.AddRange(mUploadTypeList);
            cmbUploadType.SelectedIndex = 0;

            cmbDownloadType.Items.Clear();
            cmbDownloadType.Items.AddRange(mDownloadTypeList);
            cmbDownloadType.SelectedIndex = 0;

        }

        private void BtnGetPerson_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            int iUsercode = 0;
            if (!int.TryParse(txtDownloadUserCode.Text, out iUsercode) || iUsercode < 0)
            {
                return;
            }
            ReadPersonDetail_Parameter par = new ReadPersonDetail_Parameter(iUsercode);
            ReadPersonDetail cmd = new ReadPersonDetail(cmdDtl, par);

            mMainForm.AddCommand(cmd);

            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                var result = cmd.getResult() as ReadPersonDetail_Result;
                StringBuilder strBuf = new StringBuilder();
                strBuf.Append($"人员ID：{result.UserCode}，照片存储情况：\r\n");
                for (int i = 0; i < result.PhotoList.Length; i++)
                {
                    strBuf.Append($"照片{i + 1}:{(result.PhotoList[i] == 1 ? "有，" : "无，")}");
                }
                strBuf.Append("\r\n");
                for (int i = 0; i < result.FingerprintList.Length; i++)
                {
                    strBuf.Append($"指纹{i + 1}:{(result.FingerprintList[i] == 1 ? "有，" : "无，")}");
                }
                strBuf.Append("\r\n");
                strBuf.Append($"人脸特征码存储情况：{(result.HasFace ? "有" : "无")}");
                mMainForm.AddCmdLog(cmde, strBuf.ToString());
            };
        }

        private void BtnDownload_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            int iUsercode = 0;
            if (!int.TryParse(txtDownloadUserCode.Text, out iUsercode) || iUsercode < 0)
            {
                return;
            }
            if (cmbDownloadSerialNumber.SelectedIndex == -1)
            {
                return;
            }
            int serialNumber = Convert.ToInt32(cmbDownloadSerialNumber.SelectedItem);
            ReadFeatureCode_Parameter par = new ReadFeatureCode_Parameter(iUsercode, cmbDownloadType.SelectedIndex + 1, serialNumber);
            ReadFeatureCode cmd = new ReadFeatureCode(cmdDtl, par);

            mMainForm.AddCommand(cmd);

            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                var result = cmd.getResult() as ReadFeatureCode_Result;
                if (result.FileHandle == 0)
                {
                    mMainForm.AddCmdLog(cmde, "待下载文件不存在");
                    return;
                }
                Invoke(() =>
                {
                    if (result.Type == 1)
                    {
                        cmbUploadType.SelectedIndex = 0;
                    }
                    if (result.Type == 2 && result.FileHandle != 0)
                    {
                        gbData.Visible = true;
                        cmbUploadType.SelectedIndex = 1;
                        txtCodeData.Text = Convert.ToBase64String(result.Datas);
                    }
                    if (result.Type == 4 && result.FileHandle != 0)
                    {
                        gbData.Visible = true;
                        cmbUploadType.SelectedIndex = 2;
                        txtCodeData.Text = Convert.ToBase64String(result.Datas);
                    }
                });

            };
        }

        private void CmbDownloadType_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbDownloadSerialNumber.Items.Clear();
            if (cmbDownloadType.SelectedIndex == 0)
            {
                string[] serialNumberList = new string[5];
                for (int i = 1; i <= 5; i++)
                {
                    serialNumberList[i - 1] = i.ToString();
                }
                cmbDownloadSerialNumber.Items.AddRange(serialNumberList);
                cmbDownloadSerialNumber.SelectedIndex = 0;
            }
            if (cmbDownloadType.SelectedIndex == 1)
            {
                string[] serialNumberList = new string[10];
                for (int i = 0; i <= 9; i++)
                {
                    serialNumberList[i] = i.ToString();
                }
                cmbDownloadSerialNumber.Items.AddRange(serialNumberList);
                cmbDownloadSerialNumber.SelectedIndex = 0;
            }
            if (cmbDownloadType.SelectedIndex == 3 || cmbDownloadType.SelectedIndex == 2)
            {
                cmbDownloadSerialNumber.Items.Add("1");
                cmbDownloadSerialNumber.SelectedIndex = 0;
            }
        }

        private void CmbUploadType_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbUploadSerialNumber.Items.Clear();
            if (cmbUploadType.SelectedIndex == 2)
            {
                cmbUploadSerialNumber.Items.Add("1");
                cmbUploadSerialNumber.SelectedIndex = 0;
            }
            if (cmbUploadType.SelectedIndex == 0)
            {
                string[] serialNumberList = new string[5];
                for (int i = 1; i <= 5; i++)
                {
                    serialNumberList[i - 1] = i.ToString();
                }
                cmbUploadSerialNumber.Items.AddRange(serialNumberList);
                cmbUploadSerialNumber.SelectedIndex = 0;
            }
            if (cmbUploadType.SelectedIndex == 1)
            {
                string[] serialNumberList = new string[10];
                for (int i = 0; i <= 9; i++)
                {
                    serialNumberList[i] = i.ToString();
                }
                cmbUploadSerialNumber.Items.AddRange(serialNumberList);
                cmbUploadSerialNumber.SelectedIndex = 0;
            }
        }

        private void BtnDeleteCode_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            int iUsercode = 0;
            if (!int.TryParse(txtUploadUserCode.Text, out iUsercode) || iUsercode < 0)
            {
                return;
            }
            byte[] fingerprintList = new byte[10];
            for (int i = 0; i < 10; i++)
            {
                fingerprintList[i] = 0;
            }
            byte[] photoList = new byte[5];
            for (int i = 0; i < 5; i++)
            {
                photoList[i] = 0;
            }
            bool delFace = false;
            if (cmbUploadType.SelectedIndex == 0)
            {
                photoList[cmbUploadSerialNumber.SelectedIndex] = 1;
            }
            if (cmbUploadType.SelectedIndex == 1)
            {
                fingerprintList[cmbUploadSerialNumber.SelectedIndex] = 1;
            }
            if (cmbUploadType.SelectedIndex == 2)
            {
                delFace = true;
            }

            DeleteFeatureCode_Parameter par = new DeleteFeatureCode_Parameter(iUsercode, photoList, fingerprintList, delFace);
            DeleteFeatureCode cmd = new DeleteFeatureCode(cmdDtl, par);

            mMainForm.AddCommand(cmd);
        }

        private void BtnUploadCode_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            int iUsercode = 0;
            if (!int.TryParse(txtUploadUserCode.Text, out iUsercode) || iUsercode < 0)
            {
                return;
            }
            if (string.IsNullOrWhiteSpace(txtCodeData.Text))
            {
                return;
            }
            byte[] datas = Convert.FromBase64String(txtCodeData.Text);
            WriteFeatureCode_Parameter par = new WriteFeatureCode_Parameter(iUsercode, cmbUploadType.SelectedIndex + 1, Convert.ToInt32(cmbUploadSerialNumber.SelectedItem), datas);
            WriteFeatureCode cmd = new WriteFeatureCode(cmdDtl, par);

            mMainForm.AddCommand(cmd);
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                var result = cmde.Command.getResult() as WriteFeatureCode_Result;
                if (result.Success)
                {
                    mMainForm.AddCmdLog(cmde, "写入成功");
                }
                else
                {
                    mMainForm.AddCmdLog(cmde, "写入失败");
                }
            };
        }

        private void BtnCompute_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCodeData.Text))
            {
                return;
            }
            byte[] datas = Convert.FromBase64String(txtCodeData.Text);
            uint CRC32 = FCARD.Common.Cryptography.CRC32_C.CalculateDigest(datas, 0, (uint)datas.Length);
            MessageBox.Show("特征码的 CRC32：" + CRC32.ToString("x"));
        }
    }
}
