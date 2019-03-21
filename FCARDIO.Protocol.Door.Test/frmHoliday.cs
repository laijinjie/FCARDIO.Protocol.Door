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
    public partial class frmHoliday : Form
    {
        private static object lockobj = new object();
        private static frmHoliday onlyObj;
        public static frmHoliday GetForm(INMain main)
        {
            if (onlyObj == null)
            {
                lock (lockobj)
                {
                    if (onlyObj == null)
                    {
                        onlyObj = new frmHoliday(main);
                        frmMain.AddNodeForms(onlyObj);
                    }
                }
            }
            return onlyObj;
        }

        INMain mMainForm;

        private frmHoliday(INMain main)
        {
            InitializeComponent();
            mMainForm = main;
        }

        private void frmHoliday_Load(object sender, EventArgs e)
        {

        }
    }
}
