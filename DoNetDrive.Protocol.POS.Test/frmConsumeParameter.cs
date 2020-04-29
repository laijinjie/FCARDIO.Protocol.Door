using DoNetDrive.Protocol.POS.ConsumeParameter.AdditionalCharges;
using DoNetDrive.Protocol.POS.ConsumeParameter.CancelConsume;
using DoNetDrive.Protocol.POS.ConsumeParameter.ConsumePassword;
using DoNetDrive.Protocol.POS.ConsumeParameter.ConsumptionLimits;
using DoNetDrive.Protocol.POS.ConsumeParameter.CountingCards;
using DoNetDrive.Protocol.POS.ConsumeParameter.Discount;
using DoNetDrive.Protocol.POS.ConsumeParameter.FixedFeeRule;
using DoNetDrive.Protocol.POS.ConsumeParameter.ICCardAccount;
using DoNetDrive.Protocol.POS.ConsumeParameter.Integral;
using DoNetDrive.Protocol.POS.ConsumeParameter.POSWorkMode;
using DoNetDrive.Protocol.POS.ConsumeParameter.TemporaryChangeFixedFee;
using DoNetDrive.Protocol.POS.Data;
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
    public partial class frmConsumeParameter : frmNodeForm
    {
        #region 单例模式
        private static object lockobj = new object();
        private static frmConsumeParameter onlyObj;
        public static frmConsumeParameter GetForm(INMain main)
        {
            if (onlyObj == null)
            {
                lock (lockobj)
                {
                    if (onlyObj == null)
                    {
                        onlyObj = new frmConsumeParameter(main);
                        FrmMain.AddNodeForms(onlyObj);
                    }
                }
            }
            return onlyObj;
        }

        private frmConsumeParameter(INMain main) : base(main)
        {
            InitializeComponent();
        }
        #endregion

        List<string> mPOSWorkMode = new List<string> { "标准收费", "定额收费", "菜单收费", "订餐机", "补贴机", "子账收费", "子账补贴" };

        private void frmConsumeParameter_Load(object sender, EventArgs e)
        {
            cmbPOSWorkMode.Items.AddRange(mPOSWorkMode.ToArray());
            cmbPOSWorkMode.SelectedIndex = 0;
        }

        public frmConsumeParameter()
        {
            InitializeComponent();
        }

        private void butReadPOSWorkMode_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadPOSWorkMode cmd = new ReadPOSWorkMode(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadPOSWorkMode_Result result = cmde.Command.getResult() as ReadPOSWorkMode_Result;

                string tip = $"消费模式:{mPOSWorkMode[result.Mode - 1]}";
                Invoke(() =>
                {
                    cmbPOSWorkMode.SelectedIndex = result.Mode - 1;
                });
                mMainForm.AddCmdLog(cmde, tip);
            };
        }

        private void butWritePOSWorkMode_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            WritePOSWorkMode_Parameter par = new WritePOSWorkMode_Parameter((byte)(cmbPOSWorkMode.SelectedIndex + 1));
            WritePOSWorkMode cmd = new WritePOSWorkMode(cmdDtl, par);
            mMainForm.AddCommand(cmd);
        }

        private void butReadConsumptionLimits_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadConsumptionLimits cmd = new ReadConsumptionLimits(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadConsumptionLimits_Result result = cmde.Command.getResult() as ReadConsumptionLimits_Result;

                string tip = "";
                Invoke(() =>
                {
                    txtLimitMoney.Value = result.LimitMoney;
                    txtDayLimitMoney.Value = result.DayLimitMoney;
                    txtDayLimit.Value = result.DayLimit;
                    txtMonthLimitMoney.Value = result.MonthLimitMoney;
                    txtMonthLimit.Value = result.MonthLimit;
                    txtMinimumReservedBalance.Value = result.MinimumReservedBalance;
                });
                mMainForm.AddCmdLog(cmde, tip);
            };
        }

        private void butWriteConsumptionLimits_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            WriteConsumptionLimits_Parameter par = new WriteConsumptionLimits_Parameter((int)(txtLimitMoney.Value), (int)(txtDayLimitMoney.Value),(byte)(txtDayLimit.Value)
                ,(int)(txtMonthLimitMoney.Value),(byte)(txtMonthLimit.Value),(int)(txtMinimumReservedBalance.Value));
            WriteConsumptionLimits cmd = new WriteConsumptionLimits(cmdDtl, par);
            mMainForm.AddCommand(cmd);
        }

        private void butReadConsumePassword_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadConsumePassword cmd = new ReadConsumePassword(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadConsumePassword_Result result = cmde.Command.getResult() as ReadConsumePassword_Result;
                bool bUseConsumePassword = result.Use == 1;
                string tip = $"消费时确认密码:【{(bUseConsumePassword? "启用":"不启用")}，免密码消费限额:{result.LimitMoney}】";
                Invoke(() =>
                {
                    cbUseConsumePassword.Checked = bUseConsumePassword;
                    txtPwdLimitMoney.Value = result.LimitMoney;
                });
                mMainForm.AddCmdLog(cmde, tip);
            };
        }

        private void butWriteConsumePassword_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            WriteConsumePassword_Parameter par = new WriteConsumePassword_Parameter((byte)(cbUseConsumePassword.Checked ? 1:0), (int)(txtPwdLimitMoney.Value));
            WriteConsumePassword cmd = new WriteConsumePassword(cmdDtl, par);
            mMainForm.AddCommand(cmd);
        }

        private void butReadTemporaryChange_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadTemporaryChangeFixedFee cmd = new ReadTemporaryChangeFixedFee(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadTemporaryChangeFixedFee_Result result = cmde.Command.getResult() as ReadTemporaryChangeFixedFee_Result;
                bool bUseConsumePassword = result.Use == 1;
                bool bReturnOriginal = result.ReturnOriginal == 1;
                string tip = $"临时变更定额、定次消费额度:【{(bUseConsumePassword ? "启用" : "不启用")}，消费后自动还原:{(bReturnOriginal ? "启用" : "不启用")}】";
                Invoke(() =>
                {
                    cbUseTemporaryChange.Checked = bUseConsumePassword;
                    cbReturnOriginal.Checked = bReturnOriginal;
                });
                mMainForm.AddCmdLog(cmde, tip);
            };
        }

        private void butWriteTemporaryChange_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            WriteTemporaryChangeFixedFee_Parameter par = new WriteTemporaryChangeFixedFee_Parameter((byte)(cbUseConsumePassword.Checked ? 1 : 0), (byte)(cbReturnOriginal.Checked ? 1 : 0));
            WriteTemporaryChangeFixedFee cmd = new WriteTemporaryChangeFixedFee(cmdDtl, par);
            mMainForm.AddCommand(cmd);
        }

        private void butReadCancelConsume_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadCancelConsume cmd = new ReadCancelConsume(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadCancelConsume_Result result = cmde.Command.getResult() as ReadCancelConsume_Result;
                bool bUseCancelConsume = result.Use == 1;
                string tip = $"撤销消费:【{(bUseCancelConsume ? "启用" : "不启用")}，最大撤销天数:{result.CancelDays}】";
                Invoke(() =>
                {
                    cbUseCancelConsume.Checked = bUseCancelConsume;
                    txtCancelDays.Value = result.CancelDays;
                });
                mMainForm.AddCmdLog(cmde, tip);
            };
        }

        private void butWriteCancelConsume_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            WriteCancelConsume_Parameter par = new WriteCancelConsume_Parameter((byte)(cbUseCancelConsume.Checked ? 1 : 0), (byte)txtCancelDays.Value);
            WriteCancelConsume cmd = new WriteCancelConsume(cmdDtl, par);
            mMainForm.AddCommand(cmd);
        }

        private void butReadICCardAccount_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadICCardAccount cmd = new ReadICCardAccount(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadICCardAccount_Result result = cmde.Command.getResult() as ReadICCardAccount_Result;
                bool bUseCashAccount = result.UseCashAccount == 1;
                bool bUseSubsidyAccount = result.UseSubsidyAccount == 1;
                string tip = $"IC卡账户:【{(bUseCashAccount ? "启用" : "不启用")}，补贴账户:{(bUseSubsidyAccount ? "启用" : "不启用")}】";
                Invoke(() =>
                {
                    cbUseCashAccount.Checked = bUseCashAccount;
                    cbUseSubsidyAccount.Checked = bUseSubsidyAccount;
                });
                mMainForm.AddCmdLog(cmde, tip);
            };
        }

        private void butWriteICCardAccount_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            WriteICCardAccount_Parameter par = new WriteICCardAccount_Parameter((byte)(cbUseCashAccount.Checked ? 1 : 0), (byte)(cbUseSubsidyAccount.Checked ? 1 : 0));
            WriteICCardAccount cmd = new WriteICCardAccount(cmdDtl, par);
            mMainForm.AddCommand(cmd);
        }

        private void butReadAdditionalCharges_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadAdditionalCharges cmd = new ReadAdditionalCharges(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadAdditionalCharges_Result result = cmde.Command.getResult() as ReadAdditionalCharges_Result;
                bool bUseAdditionalCharges = result.Use == 1;
                string tip = $"附加费用，时段多次消费收取附加费:【{(bUseAdditionalCharges ? "启用" : "不启用")}，】";
                Invoke(() =>
                {
                    cbUseAdditionalCharges.Checked = bUseAdditionalCharges;
                    txtAdditionalChargesFreeTimes.Value = result.FreeTimes;
                });
                mMainForm.AddCmdLog(cmde, tip);
            };
        }

        private void butWriteAdditionalCharges_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            WriteAdditionalCharges_Parameter par = new WriteAdditionalCharges_Parameter((byte)(cbUseAdditionalCharges.Checked ? 1 : 0), (byte)(txtAdditionalChargesFreeTimes.Value),0);
            WriteAdditionalCharges cmd = new WriteAdditionalCharges(cmdDtl, par);
            mMainForm.AddCommand(cmd);
        }

        private void butReadDiscount_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadDiscount cmd = new ReadDiscount(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadDiscount_Result result = cmde.Command.getResult() as ReadDiscount_Result;
                bool bUseICCardDiscount = result.UseICCardDiscount == 1;
                bool bUsePOSDiscount = result.UsePOSDiscount == 1;
                bool bUseCardTypeDiscount = result.UseCardTypeDiscount == 1;
                bool bUseDoubleDiscount = result.UseDoubleDiscount == 1;
                string tip = $"折扣:【{(bUseICCardDiscount ? "IC卡折扣启用" : "IC卡折扣不启用")}，】";
                Invoke(() =>
                {
                    cbUseICCardDiscount.Checked = bUseICCardDiscount;
                    cbUsePOSDiscount.Checked = bUsePOSDiscount;
                    cbUseCardTypeDiscount.Checked = bUseCardTypeDiscount;
                    cbDoubleDiscount.Checked = bUseDoubleDiscount;
                    txtPOSDiscount.Value = result.POSDiscount;

                });
                mMainForm.AddCmdLog(cmde, tip);
            };
        }

        private void butWriteDiscount_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            WriteDiscount_Parameter par = new WriteDiscount_Parameter((byte)(cbUseICCardDiscount.Checked ? 1 : 0), (byte)(cbUsePOSDiscount.Checked ? 1 : 0), (byte)(cbUseCardTypeDiscount.Checked ? 1 : 0), (byte)(txtPOSDiscount.Value), (byte)(cbDoubleDiscount.Checked ? 1 : 0));
            WriteDiscount cmd = new WriteDiscount(cmdDtl, par);
            mMainForm.AddCommand(cmd);
        }

        private void butReadIntegral_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadIntegral cmd = new ReadIntegral(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadIntegral_Result result = cmde.Command.getResult() as ReadIntegral_Result;
                bool bUse = result.Use == 1;
                string tip = $"积分:【{(bUse ? "积分启用" : "积分不启用")}，】";
                Invoke(() =>
                {
                    cbUseIntegral.Checked = bUse;
                    txtMaxCount.Value = result.MaxCount;
                    txtMaxIntegral.Value = result.MaxIntegral;
                    txtIntegralMoney.Value = result.Money;
                    txtIntegral.Value = result.Integral;

                });
                mMainForm.AddCmdLog(cmde, tip);
            };
        }

        private void butWriteIntegral_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            WriteIntegral_Parameter par = new WriteIntegral_Parameter((byte)(cbUseIntegral.Checked ? 1 : 0), (int)(txtIntegralMoney.Value), (int)(txtIntegral.Value), (int)(txtMaxIntegral.Value), (int)(txtMaxCount.Value));
            WriteIntegral cmd = new WriteIntegral(cmdDtl, par);
            mMainForm.AddCommand(cmd);
        }

        private void butReadCountingCards_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadCountingCards cmd = new ReadCountingCards(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadCountingCards_Result result = cmde.Command.getResult() as ReadCountingCards_Result;
                bool bUse = result.Use == 1;
                bool bUseResidueCount = result.UseResidueCount == 1;

                string tip = $"积分:【{(bUse ? "积分启用" : "积分不启用")}，】";
                Invoke(() =>
                {
                    cbUseCountingCards.Checked = bUse;
                    cbUseResidueCount.Checked = bUseResidueCount;
                    txtDeductionCount.Value = result.DeductionCount;

                });
                mMainForm.AddCmdLog(cmde, tip);
            };
        }

        private void butWriteCountingCards_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            WriteCountingCards_Parameter par = new WriteCountingCards_Parameter((byte)(cbUseCountingCards.Checked ? 1 : 0), (byte)(txtDeductionCount.Value), (byte)(cbUseResidueCount.Checked ? 1 : 0));
            WriteCountingCards cmd = new WriteCountingCards(cmdDtl, par);
            mMainForm.AddCommand(cmd);
        }

        private void butReadFixedFeeRule_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;
            ReadFixedFeeRule cmd = new ReadFixedFeeRule(cmdDtl);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                ReadFixedFeeRule_Result result = cmde.Command.getResult() as ReadFixedFeeRule_Result;

                Invoke(() =>
                {
                    dgvFixedFeeRule.AutoGenerateColumns = false;
                    dgvFixedFeeRule.DataSource = new BindingList<FixedFeeRuleDetail>(result.DataList);
                    //dgvFixedFeeRule.DataSource = result.PrintContents;
                });
                //mMainForm.AddCmdLog(cmde, result.Message);
            };
        }

        private void butWriteFixedFeeRule_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            if (cmdDtl == null) return;

            List<FixedFeeRuleDetail> fixedFeeRuleDetail = new List<FixedFeeRuleDetail>(8);
            for (int i = 0; i < dgvFixedFeeRule.Rows.Count; i++)
            {
                DataGridViewCheckBoxCell cellIsReservation = (DataGridViewCheckBoxCell)dgvFixedFeeRule.Rows[i].Cells[8];
                DataGridViewTextBoxCell cellBeginTime = (DataGridViewTextBoxCell)dgvFixedFeeRule.Rows[i].Cells[1];
                DataGridViewTextBoxCell cellEndTime = (DataGridViewTextBoxCell)dgvFixedFeeRule.Rows[i].Cells[2];
                DataGridViewTextBoxCell cellFixedFee = (DataGridViewTextBoxCell)dgvFixedFeeRule.Rows[i].Cells[3];
                DataGridViewTextBoxCell cellConsumptionLimits = (DataGridViewTextBoxCell)dgvFixedFeeRule.Rows[i].Cells[4];
                DataGridViewTextBoxCell cellLimite = (DataGridViewTextBoxCell)dgvFixedFeeRule.Rows[i].Cells[5];
                DataGridViewTextBoxCell cellCountingCardsDeductionCount = (DataGridViewTextBoxCell)dgvFixedFeeRule.Rows[i].Cells[6];
                DataGridViewTextBoxCell cellCountingCardsLimitsCount = (DataGridViewTextBoxCell)dgvFixedFeeRule.Rows[i].Cells[7];
                DataGridViewTextBoxCell cellMealTimeName = (DataGridViewTextBoxCell)dgvFixedFeeRule.Rows[i].Cells[9];

                FixedFeeRuleDetail model = new FixedFeeRuleDetail();
                model.SerialNumber = (byte)(i + 1);
                if ((bool)cellIsReservation.FormattedValue)
                    model.IsReservation = 1;
                else
                    model.IsReservation = 0;
                var beginhour = cellBeginTime.Value.ToString().Split(':')[0];
                var beginminute = cellBeginTime.Value.ToString().Split(':')[1];
                var endhour = cellEndTime.Value.ToString().Split(':')[0];
                var endminute = cellEndTime.Value.ToString().Split(':')[1];
                model.BeginTime = new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day,int.Parse(beginhour), int.Parse(beginminute), 0) ;
                model.EndTime = new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day,int.Parse(endhour), int.Parse(endminute), 0) ;
                model.ConsumptionLimits = (int)cellConsumptionLimits.Value;
                model.Limite = (byte)cellLimite.Value;
                model.FixedFee = (int)cellFixedFee.Value;
                model.CountingCardsDeductionCount = (byte)cellCountingCardsDeductionCount.Value;
                model.CountingCardsLimitsCount = (byte)cellCountingCardsLimitsCount.Value;
                model.MealTimeName = cellMealTimeName.Value.ToString();
                fixedFeeRuleDetail.Add(model);
            }
            WriteFixedFeeRule_Parameter par = new WriteFixedFeeRule_Parameter(fixedFeeRuleDetail);
            WriteFixedFeeRule cmd = new WriteFixedFeeRule(cmdDtl, par);
            mMainForm.AddCommand(cmd);

            //处理返回值
            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                mMainForm.AddCmdLog(cmde, "命令执行成功");
            };
        }

        private void dgvFixedFeeRule_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;
            if (e.ColumnIndex == 8 )
            {

                DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)dgvFixedFeeRule.Rows[e.RowIndex].Cells[e.ColumnIndex];
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
            if ((e.ColumnIndex >= 1 && e.ColumnIndex <= 7) || e.ColumnIndex == 9)
            {
                DataGridViewTextBoxColumn textbox = dgvFixedFeeRule.Columns[e.ColumnIndex] as DataGridViewTextBoxColumn;
                if (textbox != null) //如果该列是TextBox列
                {
                    dgvFixedFeeRule.BeginEdit(true); //开始编辑状态
                    dgvFixedFeeRule.ReadOnly = false;
                }

            }
            else
            {
                dgvFixedFeeRule.BeginEdit(false); //开始编辑状态
                dgvFixedFeeRule.ReadOnly = true;
            }
        }
    }
}
