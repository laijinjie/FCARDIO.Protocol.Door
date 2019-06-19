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
    public partial class frmPassword : Form
    {
        private static object lockobj = new object();
        private static frmPassword onlyObj;
        public static frmPassword GetForm(INMain main)
        {
            if (onlyObj == null)
            {
                lock (lockobj)
                {
                    if (onlyObj == null)
                    {
                        onlyObj = new frmPassword(main);
                        frmMain.AddNodeForms(onlyObj);
                    }
                }
            }
            return onlyObj;
        }

        INMain mMainForm;

        private frmPassword(INMain main)
        {
            InitializeComponent();
            mMainForm = main;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmPassword_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmPassword_FormClosed(object sender, FormClosedEventArgs e)
        {
            onlyObj = null;
        }

        /// <summary>
        /// 读取密码库存储详情 事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButReadPasswordDetail_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 从控制板读取所有密码表 事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButReadAllPassword_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 添加列表密码 事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButAddPassword_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 删除所有密码表 事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButClearPassword_Click(object sender, EventArgs e)
        {

        }
    }
}
