using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using RibbonSimplePad;
using System.Threading;
using System.IO;
using System.Net;
using DevExpress.Skins;
namespace RibbonSimplePad
{
    static class Program
    {
        
        public static bool g_bOk;
       
        public static string g_sDbLocation;
        [STAThread]
        public static void Main ()
        {
            Application.EnableVisualStyles();
            DevExpress.Skins.SkinManager.EnableFormSkins();
            Application.SetCompatibleTextRenderingDefault(false);
            DevExpress.UserSkins.BonusSkins.Register();
            SkinManager.EnableFormSkins();
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("fr-FR");
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("fr-FR");
            Application.Run(new  login1());

            int iRet = 0;
            string sPath = null;
            g_sDbLocation = sPath;
            try
            {
                FileInfo fInfo = new FileInfo(g_sDbLocation);
                if (fInfo.Exists)
                {
                    g_bOk = true;
                }
            }
            catch (Exception esd) { }
           
          
        }
        public static string GetAppPath()
        {
            string sPath = null;
            try
            {
                sPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
                sPath = sPath.Substring(0, sPath.LastIndexOf("\\", System.StringComparison.OrdinalIgnoreCase) + 1);
                return sPath;
            }
            catch (Exception)
            {
            }   return null;
        }
        public static string GetLikelyDbPath()
        {
            int iLoc = 0;
            string sPath = null;
            sPath = GetAppPath();
            iLoc = GetAppPath().ToUpper().IndexOf("\\BIN") + 1;
            if (iLoc > 0)
            {
                sPath = sPath.Substring(0, iLoc - 1) + "\\";
            }
            return sPath;
        }
    }
}