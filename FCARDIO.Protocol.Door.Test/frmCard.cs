using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            cmbcardType.Items.AddRange(new string[] { "所有卡", "排序卡","非排序卡" });
            cmbcardType.SelectedIndex = 0;
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
                mMainForm.AddLog($"命令成功：读取到的卡片列表:……,读取到的卡片数量:{result.DataBaseSize}，带读取的卡片数据类型:{result.CardType}");
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
        }


        #endregion

        #region 上传至非排列区
        private void butCardListBySequence_Click(object sender, EventArgs e)
        { 
            //par 需要传输 失败卡数量 FailTotal 和失败的卡列表 List<FC8800.Data.CardDetail> CardList;
            var cmdDtl = mMainForm.GetCommandDetail();
            var par = new FC8800.Card.CardListBySequence.WriteCardListBySequence_Parameter();
            var cmd = new FC8800.Card.CardListBySequence.WriteCardListBySequence(cmdDtl, par);
            mMainForm.AddCommand(cmd);

            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                var result = cmd.getResult() as FC8800.Card.CardListBySequence.WriteCardListBySequence_Result;
                mMainForm.AddLog($"命令成功：失败卡数量:{result.FailTotal},失败的卡列表:……");
            };
        }
        #endregion

        #region 上传至排列区
        private void butCardListBySort_Click(object sender, EventArgs e)
        {
            //par 需要传输 需要上传的卡片列表  List<FC8800.Data.CardDetail> CardList;
            var cmdDtl = mMainForm.GetCommandDetail();
            var par = new FC8800.Card.CardListBySort.WriteCardListBySort_Parameter();
            var cmd = new FC8800.Card.CardListBySort.WriteCardListBySort(cmdDtl, par);
            mMainForm.AddCommand(cmd);

            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                var result = cmd.getResult() as FC8800.Card.CardListBySort.WriteCardListBySort_Result;
                mMainForm.AddLog($"命令成功：失败卡数量:{result.FailTotal},失败的卡列表:……");
            };
        }

        #endregion

        #region 删除授权卡
        private void butDeletecard_Click(object sender, EventArgs e)
        {
            //par 需要传输 需要删除的卡片列表 long[] CardList;
            var cmdDtl = mMainForm.GetCommandDetail();
            var par = new FC8800.Card.DeleteCard.DeleteCard_Parameter();
            var cmd = new FC8800.Card.DeleteCard.DeleteCard(cmdDtl, par);
            mMainForm.AddCommand(cmd);
        }
        #endregion

        #region 读取单个卡片在控制器中的信息
        public void GetCardDetail()
        {
            //par 需要传输 需要上传的  授权卡 卡号
            var cmdDtl = mMainForm.GetCommandDetail();
            var par = new FC8800.Card.CardDetail.ReadCardDetail_Parameter();
            var cmd = new FC8800.Card.CardDetail.ReadCardDetail(cmdDtl, par);
            mMainForm.AddCommand(cmd);

            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                var result = cmd.getResult() as FC8800.Card.CardDetail.ReadCardDetail_Result;
                mMainForm.AddLog($"命令成功：卡片是否存在:{result.IsReady},卡片的详情:{result.Card}");
            };
        } 
        #endregion

    }
}
