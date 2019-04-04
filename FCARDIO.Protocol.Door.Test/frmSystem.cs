using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FCARDIO.Protocol.Door.FC8800.SystemParameter.SN;
using FCARDIO.Core.Extension;

namespace FCARDIO.Protocol.Door.Test
{

    public partial class frmSystem : Form
    {
        private static object lockobj = new object();
        private static frmSystem onlyObj;
        public static frmSystem GetForm(INMain main)
        {
            if(onlyObj == null)
            {
                lock (lockobj)
                {
                    if(onlyObj == null)
                    {
                        onlyObj = new frmSystem(main);
                        frmMain.AddNodeForms(onlyObj);
                    }
                }
            }
            return onlyObj;
        }

        INMain mMainForm;

        private frmSystem(INMain main)
        {
            InitializeComponent();
            mMainForm = main;
        }


        #region SN的读写操作
        
        private void butReadSN_Click(object sender, EventArgs e)
        {
            var cmdDtl = mMainForm.GetCommandDetail();
            ReadSN cmd = new ReadSN(cmdDtl);
            mMainForm.AddCommand(cmd);


            cmdDtl.CommandCompleteEvent += (sdr, cmde) =>
            {
                SN_Result result = cmd.getResult() as SN_Result;

                mMainForm.AddLog("命令成功：" + result.SNBuf.GetString());
            };
        }

        private void butWriteSN_Click(object sender, EventArgs e)
        {

        }

        private void butWriteSN_Broadcast_Click(object sender, EventArgs e)
        {

        }


        #endregion


    }
}
