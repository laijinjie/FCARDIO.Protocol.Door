﻿using System;
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

        private void frmPassword_Load(object sender, EventArgs e)
        {

        }

        private void FrmPassword_FormClosed(object sender, FormClosedEventArgs e)
        {
            onlyObj = null;
        }
    }
}
