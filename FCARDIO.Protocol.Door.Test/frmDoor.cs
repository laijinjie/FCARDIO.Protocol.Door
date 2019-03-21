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
    public partial class frmDoor : Form
    {
        private static object lockobj = new object();
        private static frmDoor onlyObj;
        public static frmDoor GetForm(INMain main)
        {
            if (onlyObj == null)
            {
                lock (lockobj)
                {
                    if (onlyObj == null)
                    {
                        onlyObj = new frmDoor(main);
                        frmMain.AddNodeForms(onlyObj);
                    }
                }
            }
            return onlyObj;
        }

        INMain mMainForm;

        private frmDoor(INMain main)
        {
            InitializeComponent();
            mMainForm = main;
        }

        private void frmDoor_Load(object sender, EventArgs e)
        {

        }
    }
}
