using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.Skins;
using DevExpress.LookAndFeel;
using DevExpress.UserSkins;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars.Helpers;
using DevExpress.XtraEditors;
using RibbonSimplePad.Properties;
using DevExpress.XtraBars.Docking2010.Views.WindowsUI;
using DevExpress.XtraSplashScreen;
using System.Threading;
using DevExpress.XtraBars.Alerter;
namespace RibbonSimplePad
{
    public partial class Form1 : RibbonForm
    {

        public static string idAlert, idButton;
       public static cde_recu2 cde2 = new cde_recu2();
        public static gestionFeur formFeur = new gestionFeur();
        public static compta cpt = new compta();
        public static stat2 stt = new stat2();
        public static gestionStock ges_stock = new gestionStock();
      
      
        public static Liste_cde list_cde = new Liste_cde();
        public static listefactures listef = new listefactures();
        public static statistique stat = new statistique();
        public static droits dd = new droits();
        public static listefacts listfacts = new listefacts();
        public static listebls listebls= new listebls();
        public static Notification formnotif = new Notification();
        public static ActionsSuivi formActions = new ActionsSuivi();
        public static Guide cc = new Guide();
        public static gestion_client gest_client = new gestion_client();
        public static liste_cde_client cde_client = new liste_cde_client();
        public static liste_devis list_dev = new liste_devis();
        public static passerCommande vv = new passerCommande();
        public static his_alimentations all= new his_alimentations();
        public static his_sortie_stock sto = new his_sortie_stock();
        public static retour_client rttt = new retour_client();
        public static user us = new user();
        public static about ab = new about();
        public static tableau_bord tableau = new tableau_bord();
        public static societe frm_societe = new societe();
        public static string etat_stat = "", deconn = "no";
        public static int wait = 0;
        public static WaitForm1 waitt = new WaitForm1();
        public static cde_recu cdeeee = new cde_recu();

        SplashScreenManager sp = new SplashScreenManager();
        sql_gmao fun = new sql_gmao();
        public static int load = 0;
        public static string des = "Déconnexion";

        public Form1()
        {
            InitializeComponent();
            InitSkinGallery();
        }
        void InitSkinGallery()
        {
            SkinHelper.InitSkinGallery(rgbiSkins, true);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
           



            //****************** gestion Stock****************************//

            if (login1.depart == "Utilisateur")
            {
               
               
                ////visualiser gestion stock
                if (login1.vis_stock == "OUI")
                {  Group_stock.Visible = true; nav_gest_stock.Visible = true; }
                else { Group_stock.Visible = false; nav_gest_stock.Visible = false; }
               
                if (login1.his_alim == "OUI")
                { Group_stock.Visible = true; nav_suivi_alime.Visible = true; }
                else { nav_suivi_alime.Visible = false; }

                if (login1.his_sort == "OUI")
                { nav_sort_stock.Visible = true; }
                else {  nav_sort_stock.Visible = false; }




                //clients
                if (login1.vis_clt == "OUI")
                { Group_cde_clt.Visible = true; nav_gest_clt.Visible = true; }
                else { Group_cde_clt.Visible = false; nav_gest_clt.Visible = false; }


                if (login1.aj_clt == "OUI")
                {  nav_liste_cde_clt.Visible = true; nav_ajou_cde_clt.Visible = true; }
                else {  nav_liste_cde_clt.Visible = false; nav_ajou_cde_clt.Visible = false; }

                //vfeur

                if (login1.vis_feur == "OUI")
                { group_feur.Visible = true; nav_gerer_feur.Visible = true; }
                else { group_feur.Visible = false; nav_gerer_feur.Visible = false; }

                if (login1.aj_feur == "OUI")
                {  nav_cde_feur.Visible = true; }
                else {  nav_cde_feur.Visible = false; }

                //devis

                if (login1.vis_devis == "OUI")
                { group_devis.Visible = true; nav_gest_devis.Visible = true; }
                else { group_devis.Visible = false; nav_gest_devis.Visible = false; }

                //stat

                if (login1.stat == "OUI")
                { navBarItem10.Visible = true; seuil_stock.Visible = true; }
                else { navBarItem10.Visible = false; seuil_stock.Visible = false; }

                //notification

                if (login1.not == "OUI")
                { Group_not.Visible = true; navBarItem14.Visible = true; }
                else { Group_not.Visible = false; navBarItem14.Visible = false; }

                if (login1.not == "OUI")
                { Group_not.Visible = true; navBarItem14.Visible = true; }
                else { Group_not.Visible = false; navBarItem14.Visible = false; }



                if (login1.conta == "OUI")
                {  navBarItem7.Visible = true; }
                else {navBarItem7.Visible = false; }

                if (login1.stat_conta == "OUI")
                {  navBarItem9.Visible = true; }
                else {  navBarItem9.Visible = false; }

                if (login1.stat_conta == "OUI" || login1.conta == "OUI")
                { navBarGroup1.Visible = true; }
                else { navBarGroup1.Visible = false; }


                Groupe_admin.Visible = false;

               


            }

          
          

            ////********************** Admin *****************************//
            if (login1.depart == "Administrateur")
            {
                Group_stock.Visible = true;
                nav_gest_stock.Visible = true;
                nav_histo_act.Visible = true;
                nav_sort_stock.Visible = true;

                Group_cde_clt.Visible = true; 
                nav_gest_clt.Visible = true;
                nav_liste_cde_clt.Visible = true;
                nav_ajou_cde_clt.Visible = true;

                group_feur.Visible = true; 
                nav_gerer_feur.Visible = true;
                nav_cde_feur.Visible = true;

                group_devis.Visible = true; 
                nav_gest_devis.Visible = true;

                navBarItem10.Visible = true;
                seuil_stock.Visible = true;

                Group_not.Visible = true; 
                navBarItem14.Visible = true;

                Groupe_admin.Visible = true;

                ribbonPageGroup4.Visible = true;
                timer2.Start();
                if (SplashScreenManager.Default != null && SplashScreenManager.Default.IsSplashFormVisible)
                {
                    SplashScreenManager.CloseForm();
                }
                SplashScreenManager.ShowForm(typeof(WaitForm1));
                if (tableau == null)
                { tableau = new tableau_bord(); }
                else { wait = 1; }

                tableau.MdiParent = this;
                tableau.Show();
                tableau.BringToFront();

            }
          
            barStaticItem1.Caption = login1.nom + " " + login1.prenom;


            notifi.Start();
            PrepareNotifcation();
            RunAlert();
            siStatus.Caption = System.DateTime.Now.ToLongDateString();
            siInfo.Caption = System.DateTime.Now.ToShortTimeString();
            deconn = "no";
            timer1.Start();
        }
        private void PrepareNotifcation()
        {
            DataTable seuilAlert = new DataTable();
            seuilAlert = fun.get_AlertSeuil();
            if (seuilAlert.Rows.Count != 0)
            {
                foreach (DataRow rowseuil in seuilAlert.Rows)
                {
                    DataTable NotifExist = new DataTable();
                    NotifExist = fun.Notif_exist("Rupture de stock", rowseuil["code_piece"].ToString());
                    if (NotifExist.Rows.Count == 0)
                    {
                        fun.set_notification("Rupture de stock", "Le produit " + rowseuil["libelle_piece"] + " est en rupture", rowseuil["code_piece"].ToString());
                    }
                }

            }
           
           
           
        }
        private void AlertDesign()
        {
            Image ig = Properties.Resources.supprimer_icone_6859_16;
            AlertButton bt1 = new AlertButton(ig);
            bt1.Name = "hideAlert";
            bt1.Hint = "Ne pas afficher";
            bt1.Style = AlertButtonStyle.Button;
            alertControl1.Buttons.Add(bt1);
        }
        private void RunAlert()
        {

            if (InvokeRequired)
            {
                BeginInvoke(new MethodInvoker(RunAlert));
            }
            else
            {
                DataTable AllAlert = new DataTable();
                AllAlert = fun.get_AllAlert();
                if (AllAlert.Rows.Count != 0)
                {
                    foreach (DataRow row in AllAlert.Rows)
                    {
                        string text = row["vu_notif"].ToString();
                        if (text.Contains(login1.pseudo) || text == "None")//test None pour éviter que les commande interne et/ou la discussion d'affichée par pseudo
                        {
                        }
                        else
                        {
                            Image anIm = Properties.Resources.info;
                            alertControl1.Show(this, new DevExpress.XtraBars.Alerter.AlertInfo("" + row["titre_notif"], "" + row["des_notif"], anIm));
                        }
                    }
                }
                DataTable AllcmdAlert = new DataTable();
                AllcmdAlert = fun.get_AllCMDAlert();
                if (AllcmdAlert.Rows.Count != 0)
                {
                    foreach (DataRow rowCMD in AllcmdAlert.Rows)
                    {
                        string departement = rowCMD["depar_notif"].ToString();
                        string logs = rowCMD["vu_notif"].ToString();
                        if (departement.Contains(login1.depart) || logs.Contains(login1.pseudo))
                        {
                        }
                        else
                        {
                            Image anIm = Properties.Resources.info;
                            alertControl1.Show(this, new DevExpress.XtraBars.Alerter.AlertInfo("" + rowCMD["titre_notif"], "" + rowCMD["des_notif"], anIm));
                        }
                    }

                }
            }

        }
      
       
        
        private void navBarGroup2_ItemChanged(object sender, EventArgs e)
        {
            timer2.Start();
            if (SplashScreenManager.Default != null && SplashScreenManager.Default.IsSplashFormVisible)
            {
                SplashScreenManager.CloseForm();
            }
            SplashScreenManager.ShowForm(typeof(WaitForm1));
            if (formFeur == null) { formFeur = new gestionFeur(); } else { wait = 1; }
            formFeur.MdiParent = this;
            formFeur.Show();
            formFeur.BringToFront();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            siStatus.Caption = System.DateTime.Now.ToLongDateString();
            siInfo.Caption = System.DateTime.Now.ToShortTimeString();
        }
        private void navBarItem9_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            
        }
      
        private void navBarItem11_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            timer2.Start();
            if (SplashScreenManager.Default != null && SplashScreenManager.Default.IsSplashFormVisible)
            {
                SplashScreenManager.CloseForm();
            }
            SplashScreenManager.ShowForm(typeof(WaitForm1));
            if (list_cde == null)
            { list_cde = new Liste_cde(); }
            else { wait = 1; }
            list_cde.MdiParent = Form1.ActiveForm;
            list_cde.Show();
            list_cde.BringToFront();
        }
      
        private void Stat1_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
           
        }
        private void Stat2_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            timer2.Start();
            if (SplashScreenManager.Default != null && SplashScreenManager.Default.IsSplashFormVisible)
            {
                SplashScreenManager.CloseForm();
            }
            SplashScreenManager.ShowForm(typeof(WaitForm1));
            if (stat == null)
            { stat = new statistique(); }
            else { wait = 1; }
            etat_stat = "intervention";
            stat.MdiParent = Form1.ActiveForm;
            stat.Show();
            stat.BringToFront();
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            string font = UserLookAndFeel.Default.SkinName.ToString();
            
            fun.update_skin(login1.id_user, font);
            if (deconn == "no")
            {
                XtraMessageBox.Show("Déconnectez vous", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
            }
            else
            {

               
                 DateTime histDate55 = DateTime.Now;

                fun.t_estt(des.ToString(), login1.pseudo, histDate55.ToString());
                foreach (XtraForm frm in this.MdiChildren)
                {
                    frm.Close();
                }
                foreach (AlertForm form in alertControl1.AlertFormList)
                    form.Close();
                notifi.Stop();
            }
        }
        private void iExit_ItemClick(object sender, ItemClickEventArgs e)
        {
            deconn = "yes";
            login1 log = new login1();
            log.Visible = true;
            this.Close();
        }
        private void navBarItem12_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            timer2.Start();
            if (SplashScreenManager.Default != null && SplashScreenManager.Default.IsSplashFormVisible)
            {
                SplashScreenManager.CloseForm();
            }
            SplashScreenManager.ShowForm(typeof(WaitForm1));
            if (dd == null)
            { dd = new droits(); }
            else { wait = 1; }

            dd.MdiParent = Form1.ActiveForm;
            dd.Show();
            dd.BringToFront();
        }
       
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (wait == 1)
            {
                if (SplashScreenManager.Default != null && SplashScreenManager.Default.IsSplashFormVisible)
                {
                    SplashScreenManager.CloseForm();
                }
                wait = 0;
                timer2.Stop();

            }
        }

        private void navBarItem16_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            timer2.Start();
            if (SplashScreenManager.Default != null && SplashScreenManager.Default.IsSplashFormVisible)
            {
                SplashScreenManager.CloseForm();
            }
            SplashScreenManager.ShowForm(typeof(WaitForm1));
            if (formActions == null)
            { formActions = new ActionsSuivi(); }
            else { wait = 1; }
            formActions.MdiParent = Form1.ActiveForm;
            formActions.Show();
            formActions.BringToFront();
        }
        private void navBarItem18_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            timer2.Start();
            if (SplashScreenManager.Default != null && SplashScreenManager.Default.IsSplashFormVisible)
            {
                SplashScreenManager.CloseForm();
            }
            SplashScreenManager.ShowForm(typeof(WaitForm1));
            if (formFeur == null)
            { formFeur = new gestionFeur(); }
            else { wait = 1; }
            formFeur.MdiParent = Form1.ActiveForm;
            formFeur.Show();
            formFeur.BringToFront();
        }

        private void navBarItem14_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            timer2.Start();
            if (SplashScreenManager.Default != null && SplashScreenManager.Default.IsSplashFormVisible)
            {
                SplashScreenManager.CloseForm();
            }
            SplashScreenManager.ShowForm(typeof(WaitForm1));
            if (formnotif == null)
            { formnotif = new Notification(); }
            else { wait = 1; }
            formnotif.MdiParent = Form1.ActiveForm;
            formnotif.Show();
            formnotif.BringToFront();
        }
        private void iAbout_ItemClick(object sender, ItemClickEventArgs e)
        {
            ab = new about();
            ab.Show();
            us.TopMost = true;
        }
        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            us = new user();
            us.Show();
            us.TopMost = true;
        }

        private void alertControl1_AlertClick(object sender, AlertClickEventArgs e)
        {
            timer2.Start();
            if (SplashScreenManager.Default != null && SplashScreenManager.Default.IsSplashFormVisible)
            {
                SplashScreenManager.CloseForm();
            }
            SplashScreenManager.ShowForm(typeof(WaitForm1));
            if (formnotif == null)
            { formnotif = new Notification(); }
            else { wait = 1; }
            formnotif.MdiParent = Form1.ActiveForm;
            formnotif.Show();
            formnotif.BringToFront();
        }

      

        private void navBarItem13_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            timer2.Start();
            if (SplashScreenManager.Default != null && SplashScreenManager.Default.IsSplashFormVisible)
            {
                SplashScreenManager.CloseForm();
            }
            SplashScreenManager.ShowForm(typeof(WaitForm1));
            if (frm_societe == null)
            { frm_societe = new societe(); }
            else { wait = 1; }
            frm_societe.MdiParent = Form1.ActiveForm;
            frm_societe.Show();
            frm_societe.BringToFront();
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            timer2.Start();
            if (SplashScreenManager.Default != null && SplashScreenManager.Default.IsSplashFormVisible)
            {
                SplashScreenManager.CloseForm();
            }
            SplashScreenManager.ShowForm(typeof(WaitForm1));
            if (tableau == null)
            { tableau = new tableau_bord(); }
            else { wait = 1; }
            tableau.MdiParent = this;
            tableau.Show();
            tableau.BringToFront();
        }

        private void notifi_Tick(object sender, EventArgs e)
        {
            notifi.Start();
            PrepareNotifcation();
            RunAlert();
        }

       

        private void navBarItem10_LinkClicked_1(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                timer2.Start();
                if (SplashScreenManager.Default != null && SplashScreenManager.Default.IsSplashFormVisible)
                {
                    SplashScreenManager.CloseForm();
                }
                SplashScreenManager.ShowForm(typeof(WaitForm1));
                if (list_cde == null)
                { list_cde = new Liste_cde(); }
                else { wait = 1; }
                gest_client.MdiParent = Form1.ActiveForm;
                gest_client.Show();
                gest_client.BringToFront();

            }
            catch (Exception except)
            { }
           
        }

        private void navBarItem19_LinkClicked_1(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            timer2.Start();
            if (SplashScreenManager.Default != null && SplashScreenManager.Default.IsSplashFormVisible)
            {
                SplashScreenManager.CloseForm();
            }
            SplashScreenManager.ShowForm(typeof(WaitForm1));
            if (cde_client == null)
            { cde_client = new liste_cde_client(); }
            else { wait = 1; }


            cde_client.MdiParent = Form1.ActiveForm;
            cde_client.Show();
            cde_client.BringToFront();
        }

        private void navBarItem20_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            timer2.Start();
            if (SplashScreenManager.Default != null && SplashScreenManager.Default.IsSplashFormVisible)
            {
                SplashScreenManager.CloseForm();
            }
            SplashScreenManager.ShowForm(typeof(WaitForm1));
            if (list_dev == null)
            { list_dev = new liste_devis(); }
            else { wait = 1; }


            list_dev.MdiParent = Form1.ActiveForm;
            list_dev.Show();
            list_dev.BringToFront();
        }

        private void navBarItem21_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            timer2.Start();
            
            passerCommande ft = new passerCommande(); 
            ft.MdiParent = Form1.ActiveForm;
            ft.Show();
            ft.BringToFront();
        }

        private void Group_stock_ItemChanged(object sender, EventArgs e)
        {
           
        }

        private void nav_cde_feur_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            timer2.Start();
            if (SplashScreenManager.Default != null && SplashScreenManager.Default.IsSplashFormVisible)
            {
                SplashScreenManager.CloseForm();
            }
            SplashScreenManager.ShowForm(typeof(WaitForm1));
            list_cde = new Liste_cde(); 
           wait = 1; 
            list_cde.MdiParent = Form1.ActiveForm;
            list_cde.Show();
            list_cde.BringToFront();
        }

        private void navBarItem7_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            timer2.Start();
            if (SplashScreenManager.Default != null && SplashScreenManager.Default.IsSplashFormVisible)
            {
                SplashScreenManager.CloseForm();
            }
            SplashScreenManager.ShowForm(typeof(WaitForm1));
            if (ges_stock == null)
            { ges_stock = new gestionStock(); }
            else { wait = 1; }
            ges_stock.MdiParent = Form1.ActiveForm;
            ges_stock.Show();
            ges_stock.BringToFront();
        }

        private void nav_gest_devis_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            
                 timer2.Start();
            if (SplashScreenManager.Default != null && SplashScreenManager.Default.IsSplashFormVisible)
            {
                SplashScreenManager.CloseForm();
            }
            SplashScreenManager.ShowForm(typeof(WaitForm1));
            if (list_dev == null)
            { list_dev = new liste_devis(); }
            else { wait = 1; }
            list_dev.MdiParent = Form1.ActiveForm;
            list_dev.Show();
            list_dev.BringToFront();
        }

        private void nav_suivi_alime_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            timer2.Start();
            if (SplashScreenManager.Default != null && SplashScreenManager.Default.IsSplashFormVisible)
            {
                SplashScreenManager.CloseForm();
            }
            SplashScreenManager.ShowForm(typeof(WaitForm1));
            if (all == null)
            { all = new his_alimentations(); }
            else { wait = 1; }
            all.MdiParent = Form1.ActiveForm;
            all.Show();
            all.BringToFront();
        }

        private void nav_sort_stock_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            timer2.Start();
            if (SplashScreenManager.Default != null && SplashScreenManager.Default.IsSplashFormVisible)
            {
                SplashScreenManager.CloseForm();
            }
            SplashScreenManager.ShowForm(typeof(WaitForm1));
            if (sto == null)
            { sto = new his_sortie_stock(); }
            else { wait = 1; }
            sto.MdiParent = Form1.ActiveForm;
            sto.Show();
            sto.BringToFront();
        }

        private void Form1_Activated(object sender, EventArgs e)
        {

        }

        private void nav_retour_clt_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            timer2.Start();
            if (SplashScreenManager.Default != null && SplashScreenManager.Default.IsSplashFormVisible)
            {
                SplashScreenManager.CloseForm();
            }
            SplashScreenManager.ShowForm(typeof(WaitForm1));
            if (rttt == null)
            { rttt = new retour_client(); }
            else { wait = 1; }


            rttt.MdiParent = Form1.ActiveForm;
            rttt.Show();
            rttt.BringToFront();
        }

        private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
        {
            timer2.Start();
            if (SplashScreenManager.Default != null && SplashScreenManager.Default.IsSplashFormVisible)
            {
                SplashScreenManager.CloseForm();
            }
            SplashScreenManager.ShowForm(typeof(WaitForm1));
            if (cc == null)
            { cc = new Guide(); }
            else { wait = 1; }
            cc.MdiParent = Form1.ActiveForm;
            cc.Show();
            cc.BringToFront();
        }

        private void navBarItem7_LinkClicked_1(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            timer2.Start();
            if (SplashScreenManager.Default != null && SplashScreenManager.Default.IsSplashFormVisible)
            {
                SplashScreenManager.CloseForm();
            }
            SplashScreenManager.ShowForm(typeof(WaitForm1));
            if (cpt == null)
            { cpt = new compta(); }
            else { wait = 1; }
            cpt.MdiParent = Form1.ActiveForm;
            cpt.Show();
            cpt.BringToFront();
        }

        private void navBarItem9_LinkClicked_1(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            timer2.Start();
            if (SplashScreenManager.Default != null && SplashScreenManager.Default.IsSplashFormVisible)
            {
                SplashScreenManager.CloseForm();
            }
            SplashScreenManager.ShowForm(typeof(WaitForm1));
            if (stt == null)
            { stt = new stat2(); }
            else { wait = 1; }
            stt.MdiParent = Form1.ActiveForm;
            stt.Show();
            stt.BringToFront();
        }

        private void navBarItem10_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            timer2.Start();
            if (SplashScreenManager.Default != null && SplashScreenManager.Default.IsSplashFormVisible)
            {
                SplashScreenManager.CloseForm();
            }
            SplashScreenManager.ShowForm(typeof(WaitForm1));
            if (stat == null)
            { stat = new statistique(); }
            else { wait = 1; }

            etat_stat = "stock";
            stat.MdiParent = Form1.ActiveForm;
            stat.Show();
            stat.BringToFront();
        }

        private void navBarItem12_LinkClicked_1(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            timer2.Start();
            if (SplashScreenManager.Default != null && SplashScreenManager.Default.IsSplashFormVisible)
            {
                SplashScreenManager.CloseForm();
            }
            SplashScreenManager.ShowForm(typeof(WaitForm1));
            if (cdeeee == null)
            { cdeeee = new cde_recu(); }
            else { wait = 1; }


            cdeeee.MdiParent = Form1.ActiveForm;
            cdeeee.Show();
            cdeeee.BringToFront();
        }

        private void navBarItem13_LinkClicked_1(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
        
            timer2.Start();
            if (SplashScreenManager.Default != null && SplashScreenManager.Default.IsSplashFormVisible)
            {
                SplashScreenManager.CloseForm();
            }
            SplashScreenManager.ShowForm(typeof(WaitForm1));
             list_cde = new Liste_cde("recu"); 
             wait = 1; 
            list_cde.MdiParent = Form1.ActiveForm;
            list_cde.Show();
            list_cde.BringToFront();
        }

       

        private void navBarItem16_LinkClicked_1(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            Suivip suip = new Suivip();
            suip.Show();
        }

        private void navBarItem18_LinkClicked_1(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            SuiviClt suiclt = new SuiviClt();
            suiclt.Show();
        }

        private void navBarItem21_LinkClicked_1(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            timer2.Start();
            if (SplashScreenManager.Default != null && SplashScreenManager.Default.IsSplashFormVisible)
            {
                SplashScreenManager.CloseForm();
            }
            SplashScreenManager.ShowForm(typeof(WaitForm1));
            if (listfacts == null)
            { listfacts = new listefacts(); }
            else { wait = 1; }


            listfacts.MdiParent = Form1.ActiveForm;
            listfacts.Show();
            listfacts.BringToFront();
            
            listfacts.Show();
        }

        private void navBarItem19_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            timer2.Start();
            if (SplashScreenManager.Default != null && SplashScreenManager.Default.IsSplashFormVisible)
            {
                SplashScreenManager.CloseForm();
            }
            SplashScreenManager.ShowForm(typeof(WaitForm1));
           
             listebls = new listebls(); 
             wait = 1; 


            listebls.MdiParent = Form1.ActiveForm;
            listebls.Show();
            listebls.BringToFront();
           
            
        }

        private void navBarItem21_LinkClicked_2(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            listefactvente LF = new listefactvente();
            LF.MdiParent = Form1.ActiveForm;

            LF.Show();
            LF.BringToFront();
           
        }

        private void navBarItem22_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            BLNOCommandeBL bl = new BLNOCommandeBL();
            bl.Show();
        }

        private void navBarControl_Click(object sender, EventArgs e)
        {

        }

        private void navBarItem12_LinkClicked_2(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            passerCommandeFr passercmdfr = new passerCommandeFr();

            passercmdfr.MdiParent = Form1.ActiveForm;
            passercmdfr.Show();
            passercmdfr.BringToFront();
        }

        private void navBarItem21_LinkClicked_3(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            facturepf factur = new facturepf();

            factur.MdiParent = Form1.ActiveForm;
            factur.Show();
            factur.BringToFront();
        }

        private void navBarItem22_LinkClicked_1(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            listepf factur = new listepf();

            factur.MdiParent = Form1.ActiveForm;
            factur.Show();
            factur.BringToFront();
        }

        private void navBarItem23_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {

            Factureachat factur = new Factureachat();

            factur.MdiParent = Form1.ActiveForm;
            factur.Show();
            factur.BringToFront();
            
        }

        private void navBarItem24_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            Avoircommande avoir = new Avoircommande();

            avoir.MdiParent = Form1.ActiveForm;
            avoir.Show();
            avoir.BringToFront();
        }

        private void navBarItem28_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            listeavoirs Listavoir = new listeavoirs();

            Listavoir.MdiParent = Form1.ActiveForm;
            Listavoir.Show();
            Listavoir.BringToFront();
        }

       
    }
}