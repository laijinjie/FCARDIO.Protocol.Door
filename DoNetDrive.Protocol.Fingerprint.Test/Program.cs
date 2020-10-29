using AutoUpdaterDotNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoNetDrive.Protocol.Fingerprint.Test
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            #region 自动更新
            AutoUpdater.UpdateMode = Mode.ForcedDownload;
            AutoUpdater.ShowSkipButton = false;
            AutoUpdater.ShowRemindLaterButton = false;
            AutoUpdater.RunUpdateAsAdmin = true;
            AutoUpdater.Mandatory = true;
            AutoUpdater.Start("http://fcardsoftware.oss-cn-zhangjiakou.aliyuncs.com/ToolDownload/Update/FaceDebugToolForNet/update.xml"); 
            #endregion
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
        }
    }
}
