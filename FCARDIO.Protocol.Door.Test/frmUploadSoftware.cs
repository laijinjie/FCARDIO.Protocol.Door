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
    public partial class frmUploadSoftware : Form
    {
        private static object lockobj = new object();
        private static frmUploadSoftware onlyObj;
        public static frmUploadSoftware GetForm(INMain main)
        {
            if (onlyObj == null)
            {
                lock (lockobj)
                {
                    if (onlyObj == null)
                    {
                        onlyObj = new frmUploadSoftware(main);
                        frmMain.AddNodeForms(onlyObj);
                    }
                }
            }
            return onlyObj;
        }

        INMain mMainForm;

        private frmUploadSoftware(INMain main)
        {
            InitializeComponent();
            mMainForm = main;
        }

        private void frmUploadSoftware_Load(object sender, EventArgs e)
        {

        }
    }
}
