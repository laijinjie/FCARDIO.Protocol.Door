﻿using DoNetDrive.Core.Command;
using DoNetTool.Common.Extensions;
using DoNetTool.Common;
using DoNetDrive.Protocol.Fingerprint.AdditionalData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoNetDrive.Protocol.Fingerprint.Test
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

        string[] mUploadTypeList = new string[] { "人员头像照片", "指纹特征码", "红外人脸特征码", "动态人脸特征码" };
        string[] mDownloadTypeList = new string[] { "人员头像", "指纹特征码", "记录照片", "红外人脸特征码", "动态人脸特征码" };
        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControl()
        {
            cmbUploadType.Items.Clear();
            cmbUploadType.Items.AddRange(mUploadTypeList);
            cmbUploadType.SelectedIndex = 0;

            cmbDownloadType.Items.Clear();
            cmbDownloadType.Items.AddRange(mDownloadTypeList);
            cmbDownloadType.SelectedIndex = 0;

            IniEquptType();
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
            INCommand cmd;


            if (chkByBlock.Checked)
            {
                ReadFile_Parameter par = new ReadFile_Parameter(iUsercode, cmbDownloadType.SelectedIndex + 1, serialNumber);
                cmd = new ReadFile(cmdDtl, par);

            }
            else
            {
                ReadFeatureCode_Parameter par = new ReadFeatureCode_Parameter(iUsercode, cmbDownloadType.SelectedIndex + 1, serialNumber);
                cmd = new ReadFeatureCode(cmdDtl, par);
            }

            mMainForm.AddCommand(cmd);

            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                var result = cmd.getResult() as ReadFeatureCode_Result;
                if (result.FileHandle == 0)
                {
                    mMainForm.AddCmdLog(cmde, "待下载文件不存在");
                    return;
                }
                if (!result.Result)
                {
                    mMainForm.AddCmdLog(cmde, "文件CRC32校验失败！");
                    return;
                }
                Invoke(() =>
                {
                    if (result.FileType == 1 || result.FileType == 3)
                    {
                        cmbUploadType.SelectedIndex = 0;
                        string sNewFile = System.IO.Path.Combine(Application.StartupPath, "Photo");
                        Directory.CreateDirectory(sNewFile);
                        sNewFile = System.IO.Path.Combine(sNewFile, $"tmpPhoto_{result.UserCode}.jpg");
                        File.WriteAllBytes(sNewFile, result.FileDatas);
                        pictureBox1.Image = Image.FromStream(new System.IO.MemoryStream(result.FileDatas));
                    }
                    else
                    {
                        txtCodeData.Text = Convert.ToBase64String(result.FileDatas);
                    }
                    if (result.FileType == 2) cmbUploadType.SelectedIndex = 1;
                    if (result.FileType == 4) cmbUploadType.SelectedIndex = 2;
                    if (result.FileType == 5) cmbUploadType.SelectedIndex = 3;

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
            if (cmbUploadType.SelectedIndex == 0) return;
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
                if (result.Result == 1)
                {
                    mMainForm.AddCmdLog(cmde, "写入成功");
                }
                else
                {
                    mMainForm.AddCmdLog(cmde, $"写入失败：code={result.Result}");
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
            uint CRC32 = DoNetTool.Common.Cryptography.CRC32_C.CalculateDigest(datas, 0, (uint)datas.Length);
            MessageBox.Show("特征码的 CRC32：" + CRC32.ToString("x"));
        }



        /// <summary>
        /// 文件最大尺寸
        /// </summary>
        private const int ImageSizeMax = 100 * 1024;
        /// <summary>
        /// 进行图片转换，图片像素不能超过 480*640，大小尺寸不能超过50K
        /// </summary>
        /// <param name="strFile"></param>
        /// <returns></returns>
        private byte[] ConvertImage(byte[] bImage, out Bitmap newImage)
        {
            Image img = Image.FromStream(new System.IO.MemoryStream(bImage));
            float rate = 1;
            if ((img.Width != 480 && img.Height != 640) || bImage.Length > ImageSizeMax)
            {
                float rate1, rate2;

                rate1 = (float)480 / (float)img.Width;
                rate2 = (float)640 / (float)img.Height;
                rate = rate1 > rate2 ? rate2 : rate1;
                //if (rate > 1) rate = 1;

            }
            int iWidth = img.Width, iHeight = img.Height;
            iWidth = (int)(iWidth * rate);
            iHeight = (int)(iHeight * rate);
            byte[] newFile = null;
            ImageCodecInfo jgpEncoder = GetEncoder(ImageFormat.Jpeg);

            System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;
            // 创建一个EncoderParameters对象.
            // 一个EncoderParameters对象有一个EncoderParameter数组对象
            EncoderParameters myEncoderParameters = new EncoderParameters(1);



            using (Bitmap bimg = new Bitmap(480, 640, PixelFormat.Format32bppArgb))
            {
                using (Graphics graphics = Graphics.FromImage(bimg))
                {
                    graphics.PageUnit = GraphicsUnit.Pixel;
                    graphics.CompositingQuality = CompositingQuality.HighQuality;
                    graphics.SmoothingMode = SmoothingMode.HighQuality;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.Clear(Color.White);
                    graphics.DrawImage(img, new Rectangle((480 - iWidth) / 2, (640 - iHeight) / 2, iWidth, iHeight));
                    graphics.Dispose();
                }
                newImage = new Bitmap(bimg);

                //进行图片大小的测算
                long iQuality = 80;
                bool bSave = false;
                do
                {
                    EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, iQuality);//这里用来设置保存时的图片质量
                    myEncoderParameters.Param[0] = myEncoderParameter;
                    using (MemoryStream ms = new MemoryStream())
                    {
                        bimg.Save(ms, jgpEncoder, myEncoderParameters);
                        myEncoderParameter.Dispose();
                        if (ms.Length <= ImageSizeMax)
                        {
                            newFile = ms.GetBuffer();
                            bSave = true;
                        }
                        ms.Close();
                        ms.Dispose();

                        iQuality -= 5;
                    }
                } while (!bSave);

            }

            return newFile;

        }

        private ImageCodecInfo GetEncoder(ImageFormat format)
        {

            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }



        private void ButUploadImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "图片文件|*.jpg;*.jpeg;*.bmp;*.png";
            ofd.Multiselect = false;
            if (ofd.ShowDialog() != DialogResult.OK) return;



            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            int iUsercode = 0;
            if (!int.TryParse(txtUploadUserCode.Text, out iUsercode) || iUsercode < 0)
            {
                return;
            }

            byte[] datas = System.IO.File.ReadAllBytes(ofd.FileName);
            Bitmap newImg;
            datas = ConvertImage(datas, out newImg);
            pictureBox1.Image = newImg;
            string sNewFile = System.IO.Path.Combine(Application.StartupPath, "tmpImage.jpg");
            File.WriteAllBytes(sNewFile, datas);

            WriteFeatureCode_Parameter par = new WriteFeatureCode_Parameter(iUsercode, 1, 1, datas);
            par.WaitRepeatMessage = true;
            WriteFeatureCode cmd = new WriteFeatureCode(cmdDtl, par);
            cmdDtl.Timeout = 5000;
            mMainForm.AddCommand(cmd);

            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                var result = cmde.Command.getResult() as WriteFeatureCode_Result;
                if (result.Result == 1)
                {
                    mMainForm.AddCmdLog(cmde, "写入成功");
                }
                else
                {
                    if (result.Result == 4)
                    {
                        mMainForm.AddCmdLog(cmde, $"写入失败：照片重复，重复的人员编号：{result.RepeatUser}");
                    }
                    else
                        mMainForm.AddCmdLog(cmde, $"写入失败：code={result.Result}");
                }
            };
        }
        #region 上传固件

        private class EquptAESKey
        {
            public string Name;
            /// <summary>
            /// AES 密码
            /// </summary>
            public string Key;
            /// <summary>
            /// 
            /// </summary>
            public int PacketByteLen;

            public EquptAESKey(string sName, string sKey, int iPLen)
            {
                Name = sName;
                Key = sKey;
                PacketByteLen = iPLen;
            }
        }
        private Dictionary<String, EquptAESKey> mAesKey;

        private void IniEquptType()
        {
            mAesKey = new Dictionary<string, EquptAESKey>();
            var oKey = new EquptAESKey("A102 指纹机", "70c5287e4572bdd9", 1024);
            mAesKey.Add(oKey.Name, oKey);

            oKey = new EquptAESKey("A103 指纹机", "0097d54f8d755a9b", 1024);
            mAesKey.Add(oKey.Name, oKey);

            oKey = new EquptAESKey("A108 指纹红外人脸机", "f89cabe34e5b9965", 1024);
            mAesKey.Add(oKey.Name, oKey);

            oKey = new EquptAESKey("FC8300 动态人脸机", "286eb84a8342627c", 1024);
            mAesKey.Add(oKey.Name, oKey);

            cmbEquptType.Items.Clear();
            cmbEquptType.Items.AddRange(mAesKey.Keys.ToArray());
            cmbEquptType.SelectedIndex = 0;

        }

        private void ButUploadSoftware_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "固件文件|*.RCBin";
            ofd.Multiselect = false;
            if (ofd.ShowDialog() != DialogResult.OK) return;

            string sFile = ofd.FileName;
            if (!File.Exists(sFile)) return;

            var oItem = mAesKey[cmbEquptType.Text];
            int iRCPacketByteLen = oItem.PacketByteLen;
            var bSurFile = File.ReadAllBytes(sFile);
            int iFileLen = bSurFile.Length;

            var sKey = Encoding.ASCII.GetString(bSurFile, 0, 16);
            if (!sKey.Equals(oItem.Key))
            {
                MsgErr("固件类型不正确！");
                return;
            }
            string sVer = $"{bSurFile[16]}.{bSurFile[17]}";

            byte[] iCRCBuf = bSurFile.Copy(18, 4);
            uint iSoftwareCRC32 = iCRCBuf.ToInt32();
            int iSoftwareSize = iFileLen - 26;
            uint iFileCRC32 = bSurFile.Copy(iFileLen - 4, 4).ToInt32();
            byte[] bSoftWareData = bSurFile.Copy(22, iSoftwareSize);
            uint itmpCRC32 = DoNetTool.Common.Cryptography.CRC32_C.CalculateDigest(bSoftWareData, 0, (uint)iSoftwareSize);
            if (itmpCRC32 != iFileCRC32)
            {
                MsgErr("固件CRC32不正确！");
                return;
            }


            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            var par = new Software.UpdateSoftware_Parameter(bSoftWareData, iSoftwareCRC32);
            INCommand cmd = null;
            if (oItem.Name.Equals("FC8300 动态人脸机"))
            {
                cmd = new Software.UpdateSoftware(cmdDtl, par);
            }
            else
            {
                cmd = new Software.UpdateSoftware_FP(cmdDtl, par);
            }

            cmdDtl.Timeout = 500;
            cmdDtl.RestartCount = 5;
            mMainForm.AddCommand(cmd);

            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                var result = cmde.Command.getResult() as Software.UpdateSoftware_Result;
                if (result.Success == 1)
                {
                    mMainForm.AddCmdLog(cmde, "固件上传完毕");
                }
                else
                {
                    mMainForm.AddCmdLog(cmde, $"固件上传失败：code={result.Success}");
                }
            };

        }

        private void CreateCRC32()
        {

        }

        #endregion


    }
}
