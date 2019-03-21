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

        }
    }
}
