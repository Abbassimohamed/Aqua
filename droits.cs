using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.Utils.Animation;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System.Text.RegularExpressions;
namespace RibbonSimplePad
{
    public partial class droits : DevExpress.XtraEditors.XtraForm
    {
        public droits()
        {
            InitializeComponent();
        }
        sql_gmao fun = new sql_gmao();
        Control animatedControl;
        public static int id_user;
        public static string etat;
        // info generales
        public static string nom, prenom, fonction, depart, gsm, email, pseudo, passwd;
        // stock
        public static string vis_stock, ajou_stock, modif_stock,supp_stock, ger_uni, ger_mag, stock_doc, passer_cde, alimen, sort_prod, his_alim, his_sort;
        //info feur
        public static string vis_feur, aj_feur, mod_feur, supp_feur, feur_doc, supp_cde_feur;
        //info clt
        public static string vis_clt, aj_clt, mod_clt, supp_clt, clt_doc, supp_cde_clt, valid_cde_clt, fact, bon_liv, bon_sort;
        //gestion devis
        public static string vis_devis, ajout_devis, supp_devis, devis_doc;

        //Gestion comptabilité
        public static string conta, stat_conta;
        //autres
        public static string stat, not;

        private void droits_Load(object sender, EventArgs e)
        {
            labelControl16.Location = new Point(
      this.ClientSize.Width / 2 - labelControl16.Size.Width / 2,
       this.ClientSize.Height / 2 - labelControl16.Size.Height / 2
      );
            
            panelControl3.Dock = DockStyle.Fill;
            panelControl4.Dock = DockStyle.Fill;
            panelControl8.Dock = DockStyle.Fill;
            gridControl1.Dock = DockStyle.Fill;
            vid();
            labelControl17.Visible = false;
            
            gridView1.BestFitColumns();
            //options d'affichage
            animatedControl = panelControl2;
            panelControl2.Visible = true;
            panelControl3.Visible = false;
            panelControl4.Visible = false;
            panelControl5.Visible = false;

            panelControl8.Visible = false;
            labelControl17.Visible = false;
            panelControl3.Dock = DockStyle.Fill;
            panelControl4.Dock = DockStyle.Fill;
 
            panelControl8.Dock = DockStyle.Fill;
            gridControl1.Dock = DockStyle.Fill;
            act();
        }
        private void act()
        {
            //liste des utilisateurs
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            DataTable dt = new DataTable();
            dt = fun.grid_user();
            gridControl1.DataSource = dt;
            this.gridView1.Columns[0].Visible = false;
            this.gridView1.Columns[1].Caption = "Nom";
            this.gridView1.Columns[2].Caption = "Prénom";
            this.gridView1.Columns[3].Caption = "Fonction";
            this.gridView1.Columns[4].Caption = "Departement";
            this.gridView1.Columns[5].Caption = "GSM";
            this.gridView1.Columns[6].Caption = "Email";
            this.gridView1.Columns[7].Visible = false;
            this.gridView1.Columns[8].Visible = false;
            this.gridView1.Columns[9].Visible = false;
            this.gridView1.Columns[10].Visible = false;
            this.gridView1.Columns[11].Visible = false;
            this.gridView1.Columns[12].Visible = false;
            this.gridView1.Columns[13].Visible = false;
            this.gridView1.Columns[14].Visible = false;
            this.gridView1.Columns[15].Visible = false;
            this.gridView1.Columns[16].Visible = false;
            this.gridView1.Columns[17].Visible = false;
            this.gridView1.Columns[18].Visible = false;
            this.gridView1.Columns[19].Visible = false;
            this.gridView1.Columns[20].Visible = false;
            this.gridView1.Columns[21].Visible = false;
            this.gridView1.Columns[22].Visible = false;
            this.gridView1.Columns[23].Visible = false;
            this.gridView1.Columns[24].Visible = false;
            this.gridView1.Columns[25].Visible = false;
            this.gridView1.Columns[26].Visible = false;
            this.gridView1.Columns[27].Visible = false;
            this.gridView1.Columns[28].Visible = false;
            this.gridView1.Columns[29].Visible = false;
            this.gridView1.Columns[30].Visible = false;
            this.gridView1.Columns[31].Visible = false;
            this.gridView1.Columns[32].Visible = false;
            this.gridView1.Columns[33].Visible = false;
            this.gridView1.Columns[34].Visible = false;
            this.gridView1.Columns[35].Visible = false;
            this.gridView1.Columns[36].Visible = false;
            this.gridView1.Columns[37].Visible = false;
            this.gridView1.Columns[38].Visible = false;
            this.gridView1.Columns[39].Visible = false;
            this.gridView1.Columns[40].Visible = false;
            this.gridView1.Columns[41].Visible = false;
            this.gridView1.Columns[42].Visible = false;
            this.gridView1.Columns[43].Visible = false;
            this.gridView1.Columns[44].Visible = false;
            this.gridView1.Columns[45].Visible = false;
           
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //ajouter utilisateur interface
            textEdit1.Properties.ReadOnly = false;
            textEdit3.Properties.ReadOnly = false;
            textEdit6.Properties.ReadOnly = false;
            vid();
            effect();
            panelControl2.Visible = true;
            panelControl5.Visible = true;
            panelControl3.Visible = true;
            panelControl3.BringToFront();
            pictureEdit1.Visible = false;
            etat = "ajouter";
            gridControl1.Visible = false;
            labelControl17.Visible = false;
            textEdit5.Text = "Utilisateur";
        }
        //effet de transition
        private void effect()
        {
            if (transitionManager1.Transitions[animatedControl] == null)
            {
                Transition transition1 = new Transition();
                transition1.Control = animatedControl;
                transitionManager1.Transitions.Add(transition1);
            }
            DevExpress.Utils.Animation.Transitions trType = (DevExpress.Utils.Animation.Transitions.Push);
            transitionManager1.Transitions[animatedControl].TransitionType = CreateTransitionInstance(trType);
            if (transitionManager1.IsTransaction)
            { transitionManager1.EndTransition(); }
            transitionManager1.StartTransition(animatedControl);
            try
            {
            }
            finally
            { transitionManager1.EndTransition(); }
        }
        BaseTransition CreateTransitionInstance(Transitions transitionType)
        {
            switch (transitionType)
            {
                case Transitions.Dissolve: return new DissolveTransition();
                case Transitions.Fade: return new FadeTransition();
                case Transitions.Shape: return new ShapeTransition();
                case Transitions.Clock: return new ClockTransition();
                case Transitions.SlideFade: return new SlideFadeTransition();
                case Transitions.Cover: return new CoverTransition();
                case Transitions.Comb: return new CombTransition();
                default: return new PushTransition();
            }
        }
        //precedant button
        private void pictureEdit1_Click(object sender, EventArgs e)
        {
            labelControl17.Visible = false;
            checkEdit45.Visible = false;
            if (panelControl4.Visible == true  || panelControl8.Visible == true)
            {
                effect();
                panelControl8.Visible = false; panelControl4.Visible = false;  panelControl3.Visible = true;  pictureEdit1.Visible = false;
            }
        }
        //suivant button
        private void pictureEdit2_Click(object sender, EventArgs e)
        {
            if (panelControl3.Visible == true)
            {
                labelControl17.Visible = true;
                checkEdit45.Visible = true;

                if (textEdit1.Text == "")
                {
                    dxErrorProvider1.Dispose();
                    dxErrorProvider1.SetError(textEdit1, "saisir le Nom");
                }
                else if (textEdit6.Text == "")
                {
                    dxErrorProvider1.Dispose();
                    dxErrorProvider1.SetError(textEdit6, "saisir le Prénom");
                }
                else if (textEdit7.Text != "" && !Regex.IsMatch(textEdit7.Text, @"^[a-z,A-Z]{1,10}((-|.)\w+)*@\w+.\w{2,3}$"))
                {
                    dxErrorProvider1.Dispose();
                    dxErrorProvider1.SetError(textEdit7, "Email invalide!!!");
                }
                else if (textEdit2.Text == "")
                {
                    dxErrorProvider1.Dispose();
                    dxErrorProvider1.SetError(textEdit2, "saisir la fonction de l'utilisateur");
                }
                else if (textEdit5.Text == "")
                {
                    dxErrorProvider1.Dispose();
                    dxErrorProvider1.SetError(textEdit5, "Choisir un mode d'emploi pour l'utilisateur");
                }
                else if (textEdit3.Text == "")
                {
                    dxErrorProvider1.Dispose();
                    dxErrorProvider1.SetError(textEdit3, "saisir le Pseudo");
                }
                else if (textEdit4.Text == "")
                {
                    dxErrorProvider1.Dispose();
                    dxErrorProvider1.SetError(textEdit4, "saisir le mot de passe d'utilisateur");
                }

                else
                {
                    dxErrorProvider1.Dispose();

                    if (etat == "modifier")
                    {
                        //modifier utilisateur
                        if (textEdit5.Text == "Utilisateur")
                        {
                            effect();
                            panelControl4.Visible = true; panelControl3.Visible = false; labelControl17.Visible = true;
                            pictureEdit1.Visible = true;
                        }
                       
                        if (textEdit5.Text == "Administrateur")
                        {
                            effect();
                            panelControl8.Visible = true; panelControl3.Visible = false; 
                            pictureEdit1.Visible = true;
                        }
                    }
                    if (etat == "ajouter")
                    {
                        //ajouter utilisateur
                        DataTable dt = new DataTable();
                        dt = fun.test_user(textEdit3.Text);
                        if (dt.Rows.Count == 0)
                        {
                            if (textEdit5.Text == "Utilisateur")
                            {
                                effect();
                                panelControl4.Visible = true; panelControl3.Visible = false;
                                pictureEdit1.Visible = true;
                            }
                           
                            if (textEdit5.Text == "Administrateur")
                            {
                                effect();
                                panelControl8.Visible = true; panelControl3.Visible = false; 
                                pictureEdit1.Visible = true;
                            }
                        }
                        else { XtraMessageBox.Show("Pseudo existant dans la base", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    }
                }
            }
        }

        private void pictureEdit2_MouseHover(object sender, EventArgs e)
        {
            pictureEdit2.Image = Properties.Resources.suivant_h;
        }

        private void pictureEdit1_MouseHover(object sender, EventArgs e)
        {
            pictureEdit1.Image = Properties.Resources.precedant_h;
        }

        private void pictureEdit2_MouseLeave(object sender, EventArgs e)
        {
            pictureEdit2.Image = Properties.Resources.suivant;
        }

        private void pictureEdit1_MouseLeave(object sender, EventArgs e)
        {
            pictureEdit1.Image = Properties.Resources.precedant;
        }
        private void vid()
        {
            //vider les champs
            checkEdit1.Checked = false;
            checkEdit2.Checked = false;
            checkEdit3.Checked = false;
            checkEdit4.Checked = false;
            checkEdit5.Checked = false;
            checkEdit6.Checked = false;
            checkEdit7.Checked = false;
            checkEdit8.Checked = false;
            checkEdit9.Checked = false;
            checkEdit10.Checked = false;
            checkEdit11.Checked = false;
            checkEdit12.Checked = false;
            checkEdit13.Checked = false;
            checkEdit14.Checked = false;
            checkEdit15.Checked = false;
            checkEdit16.Checked = false;
            checkEdit17.Checked = false;
            checkEdit18.Checked = false;
            checkEdit19.Checked = false;
            checkEdit20.Checked = false;
            checkEdit21.Checked = false;
            checkEdit22.Checked = false;
            checkEdit23.Checked = false;
            checkEdit24.Checked = false;
            checkEdit25.Checked = false;
            checkEdit26.Checked = false;
            checkEdit27.Checked = false;
            checkEdit28.Checked = false;

            checkEdit30.Checked = false;
            checkEdit31.Checked = false;
            checkEdit32.Checked = false;
           
            checkEdit43.Checked = false;
           
            checkEdit33.Checked = false;
            checkEdit34.Checked = false;
            checkEdit35.Checked = false;
            checkEdit36.Checked = false;
            checkEdit29.Checked = false;
           
            textEdit1.Text = "";
            textEdit2.Text = "";
            textEdit3.Text = "";
            textEdit4.Text = "";
            textEdit5.Text = "";
            textEdit6.Text = "";
            textEdit7.Text = "";
            textEdit8.Text = "";
        }
     
        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            //recuperer les infos de l'user
            labelControl17.Visible = false;
            GridHitInfo celclick = gridView1.CalcHitInfo(gridControl1.PointToClient(Control.MousePosition));
            if (celclick.InRow)
            {
                int count = gridView1.DataRowCount;
                if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
                {
                    System.Data.DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                    id_user = Convert.ToInt32(row[0]);
                    etat = "modifier";
                    textEdit1.Text = row[1].ToString();
                    textEdit6.Text = row[2].ToString();
                    textEdit2.Text = row[3].ToString();
                    textEdit5.Text = row[4].ToString();
                    textEdit8.Text = row[5].ToString();
                    textEdit7.Text = row[6].ToString();
                    textEdit3.Text = row[7].ToString();
                    textEdit4.Text = row[8].ToString();
                    textEdit1.Properties.ReadOnly = true;
                    textEdit3.Properties.ReadOnly = true;
                    textEdit6.Properties.ReadOnly = true;

                    if (row[4].ToString() == "Utilisateur")
                    {
                        vis_stock = ""; ajou_stock = ""; modif_stock = ""; supp_stock = ""; ger_uni = ""; ger_mag = ""; stock_doc = ""; passer_cde = ""; alimen = ""; sort_prod = ""; his_alim = ""; his_sort = ""; vis_clt = ""; aj_clt = ""; mod_clt = ""; supp_clt = ""; clt_doc = ""; supp_cde_clt = ""; valid_cde_clt = ""; fact = ""; bon_liv = ""; bon_sort = ""; vis_feur = ""; aj_feur = ""; mod_feur = ""; supp_feur = "";
                        feur_doc = ""; supp_cde_feur = ""; vis_devis = ""; ajout_devis = ""; supp_devis = ""; devis_doc = ""; stat = ""; not = ""; conta = ""; stat_conta = "";

                        DataTable dd = new DataTable();
                        dd = fun.log_login(textEdit3.Text, textEdit4.Text);
                        if (dd.Rows.Count != 0)
                        {
                           vis_stock = dd.Rows[0]["visualiser_stock"].ToString();
                            ajou_stock = dd.Rows[0]["ajouter_stock"].ToString();
                            modif_stock = dd.Rows[0]["modifier_stock"].ToString();
                            supp_stock = dd.Rows[0]["supprimer_stock"].ToString();
                            ger_uni = dd.Rows[0]["ges_uni"].ToString();
                            ger_mag = dd.Rows[0]["ges_mag"].ToString();
                            stock_doc = dd.Rows[0]["doc_stock"].ToString();
                            passer_cde = dd.Rows[0]["passer_cde"].ToString();
                            alimen = dd.Rows[0]["aliment"].ToString();
                            sort_prod = dd.Rows[0]["sortie_prod"].ToString();
                            his_alim = dd.Rows[0]["alim"].ToString();
                            his_sort = dd.Rows[0]["sortie_sto"].ToString();
                            vis_clt = dd.Rows[0]["vis_clt"].ToString();
                            aj_clt = dd.Rows[0]["ajout_clt"].ToString();
                            mod_clt = dd.Rows[0]["mod_clt"].ToString();
                            supp_clt = dd.Rows[0]["supp_clt"].ToString();
                            clt_doc = dd.Rows[0]["doc_clt"].ToString();
                            supp_cde_clt = dd.Rows[0]["supp_cde_clt"].ToString();
                            valid_cde_clt = dd.Rows[0]["vali_cde_clt"].ToString();
                            fact = dd.Rows[0]["fact"].ToString();
                            bon_liv = dd.Rows[0]["bon_liv"].ToString();
                            bon_sort = dd.Rows[0]["bon_sortie"].ToString();
                            vis_feur = dd.Rows[0]["vis_feur"].ToString();
                            aj_feur = dd.Rows[0]["aj_feur"].ToString();
                            mod_feur = dd.Rows[0]["mod_feur"].ToString();
                            supp_feur = dd.Rows[0]["supp_feur"].ToString();
                            feur_doc = dd.Rows[0]["doc_feur"].ToString();
                            supp_cde_feur = dd.Rows[0]["supp_cde_feur"].ToString();
                            vis_devis = dd.Rows[0]["vis_dev"].ToString();
                            ajout_devis = dd.Rows[0]["aj_devis"].ToString();
                            supp_devis = dd.Rows[0]["supp_dev"].ToString();
                            devis_doc = dd.Rows[0]["doc_dev"].ToString();
                            stat = dd.Rows[0]["stat"].ToString();
                            not = dd.Rows[0]["noti"].ToString();
                            conta = dd.Rows[0]["conta"].ToString();
                            stat_conta = dd.Rows[0]["stat_conta"].ToString();
                        }
                        if (vis_stock == "OUI")
                        { checkEdit32.Checked = true; }
                        else { checkEdit32.Checked = false; }
                        if (ajou_stock == "OUI")
                        { checkEdit1.Checked = true; }
                        else { checkEdit1.Checked = false; }
                        if (modif_stock == "OUI")
                        { checkEdit2.Checked = true; }
                        else { checkEdit2.Checked = false; }
                        if (supp_stock== "OUI")
                        { checkEdit3.Checked = true; }
                        else { checkEdit3.Checked = false; }
                        if (ger_uni == "OUI")
                        { checkEdit4.Checked = true; }
                        else { checkEdit4.Checked = false; }
                        if (ger_mag == "OUI")
                        { checkEdit5.Checked = true; }
                        else { checkEdit5.Checked = false; }
                        if (stock_doc == "OUI")
                        { checkEdit6.Checked = true; }
                        else { checkEdit6.Checked = false; }
                        if (passer_cde == "OUI")
                        { checkEdit7.Checked = true; }
                        else { checkEdit7.Checked = false; }
                        if (alimen == "OUI")
                        { checkEdit8.Checked = true; }
                        else { checkEdit8.Checked = false; }
                        if (sort_prod == "OUI")
                        { checkEdit9.Checked = true; }
                        else { checkEdit9.Checked = false; }
                        if (his_alim == "OUI")
                        { checkEdit27.Checked = true; }
                        else { checkEdit27.Checked = false; }
                        if (his_sort == "OUI")
                        { checkEdit28.Checked = true; }
                        else { checkEdit28.Checked = false; }
                        if (vis_clt == "OUI")
                        { checkEdit34.Checked = true; }
                        else { checkEdit34.Checked = false; }
                        if (aj_clt == "OUI")
                        { checkEdit20.Checked = true; }
                        else { checkEdit20.Checked = false; }
                        if (mod_clt == "OUI")
                        { checkEdit17.Checked = true; }
                        else { checkEdit17.Checked = false; }
                        if (supp_clt == "OUI")
                        { checkEdit19.Checked = true; }
                        else { checkEdit19.Checked = false; }
                        if (clt_doc== "OUI")
                        { checkEdit18.Checked = true; }
                        else { checkEdit18.Checked = false; }
                        if (supp_cde_clt == "OUI")
                        { checkEdit22.Checked = true; }
                        else { checkEdit22.Checked = false; }
                        if (valid_cde_clt == "OUI")
                        { checkEdit23.Checked = true; }
                        else { checkEdit23.Checked = false; }
                        if (fact == "OUI")
                        { checkEdit24.Checked = true; }
                        else { checkEdit24.Checked = false; }
                        if (bon_liv == "OUI")
                        { checkEdit25.Checked = true; }
                        else { checkEdit25.Checked = false; }
                        if (bon_sort == "OUI")
                        { checkEdit26.Checked = true; }
                        else { checkEdit26.Checked = false; }
                        if (vis_feur == "OUI")
                        { checkEdit33.Checked = true; }
                        else { checkEdit33.Checked = false; }
                        if (aj_feur == "OUI")
                        { checkEdit15.Checked = true; }
                        else { checkEdit15.Checked = false; }
                        if (mod_feur == "OUI")
                        { checkEdit16.Checked = true; }
                        else { checkEdit16.Checked = false; }
                        if (supp_feur == "OUI")
                        { checkEdit14.Checked = true; }
                        else { checkEdit14.Checked = false; }
                        if (feur_doc == "OUI")
                        { checkEdit13.Checked = true; }
                        else { checkEdit13.Checked = false; }
                        if (supp_cde_feur == "OUI")
                        { checkEdit21.Checked = true; }
                        else { checkEdit21.Checked = false; }
                        if (vis_devis == "OUI")
                        { checkEdit35.Checked = true; }
                        else { checkEdit35.Checked = false; }
                        if (ajout_devis == "OUI")
                        { checkEdit10.Checked = true; }
                        else { checkEdit10.Checked = false; }
                        if (supp_devis == "OUI")
                        { checkEdit11.Checked = true; }
                        else { checkEdit11.Checked = false; }
                        if (devis_doc == "OUI")
                        { checkEdit12.Checked = true; }
                        else { checkEdit12.Checked = false; }
                        if (stat == "OUI")
                        { checkEdit31.Checked = true; }
                        else { checkEdit31.Checked = false; }
                        if (not == "OUI")
                        { checkEdit30.Checked = true; }
                        else { checkEdit30.Checked = false; }
                        if (conta == "OUI")
                        { checkEdit36.Checked = true; }
                        else { checkEdit36.Checked = false; }
                        if (stat_conta == "OUI")
                        { checkEdit29.Checked = true; }
                        else { checkEdit29.Checked = false; }
                    }
                 
                    effect();
                    gridControl1.Visible = false;
                    panelControl2.Visible = true;
                    panelControl3.Visible = true;
                    panelControl5.Visible = true;
                }
            }
        }
        private void simpleButton2_Click_1(object sender, EventArgs e)
        {
            vid();
        }
       
        private void checkEdit45_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit45.Checked == false)
            {
                checkEdit1.Checked = false;
                checkEdit2.Checked = false;
                checkEdit3.Checked = false;
                checkEdit4.Checked = false;
                checkEdit5.Checked = false;
                checkEdit6.Checked = false;
                checkEdit7.Checked = false;
                checkEdit8.Checked = false;
                checkEdit9.Checked = false;
                checkEdit10.Checked = false;
                checkEdit11.Checked = false;
                checkEdit12.Checked = false;
                checkEdit13.Checked = false;
                checkEdit14.Checked = false;
                checkEdit15.Checked = false;
                checkEdit16.Checked = false;
                checkEdit17.Checked = false;
                checkEdit18.Checked = false;
                checkEdit19.Checked = false;
                checkEdit20.Checked = false;
                checkEdit21.Checked = false;
                checkEdit22.Checked = false;
                checkEdit23.Checked = false;
                checkEdit24.Checked = false;
                checkEdit25.Checked = false;
                checkEdit26.Checked = false;
                checkEdit27.Checked = false;
                checkEdit28.Checked = false;
                
                checkEdit30.Checked = false;
                checkEdit31.Checked = false;
                checkEdit32.Checked = false;
               
                checkEdit43.Checked = false;
                
                checkEdit33.Checked = false;
                checkEdit34.Checked = false;
                checkEdit35.Checked = false;
               
                checkEdit43.Checked = false;
                checkEdit36.Checked = false;
                checkEdit29.Checked = false;
               
            }
            if (checkEdit45.Checked == true)
            {
                checkEdit1.Checked = true;
                checkEdit2.Checked = true;
                checkEdit3.Checked = true;
                checkEdit4.Checked = true;
                checkEdit5.Checked = true;
                checkEdit6.Checked = true;
                checkEdit7.Checked = true;
                checkEdit8.Checked = true;
                checkEdit9.Checked = true;
                checkEdit10.Checked = true;
                checkEdit11.Checked = true;
                checkEdit12.Checked = true;
                checkEdit13.Checked = true;
                checkEdit14.Checked = true;
                checkEdit15.Checked = true;
                checkEdit16.Checked = true;
                checkEdit17.Checked = true;
                checkEdit18.Checked = true;
                checkEdit19.Checked = true;
                checkEdit20.Checked = true;
                checkEdit21.Checked = true;
                checkEdit22.Checked = true;
                checkEdit23.Checked = true;
                checkEdit24.Checked = true;
                checkEdit25.Checked = true;
                checkEdit26.Checked = true;
                checkEdit27.Checked = true;
                checkEdit28.Checked = true;
                
                checkEdit30.Checked = true;
                checkEdit31.Checked = true;
                checkEdit32.Checked = true;
               
                checkEdit43.Checked = true;
                
                checkEdit33.Checked = true;
                checkEdit34.Checked = true;
                checkEdit35.Checked = true;
               
                checkEdit43.Checked = true;

                checkEdit36.Checked = true;

                checkEdit29.Checked = true;
               
            }
        }
        private void simpleButton2_Click_2(object sender, EventArgs e)
        {
            fun.delete_user(id_user);
            effect();
            act();
            labelControl11.Visible = true;
            timer1.Start();
        }
        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            //recuperer l'id de l'utilisateur
            GridHitInfo celclick = gridView1.CalcHitInfo(gridControl1.PointToClient(Control.MousePosition));
            if (celclick.InRow)
            {
                int count = gridView1.DataRowCount;
                if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
                {
                    System.Data.DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                    id_user = Convert.ToInt32(row[0]);
                }
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text += "a";
            if (label1.Text.ToString() == "10aaa")
            {
                labelControl11.Visible = false;
                labelControl12.Visible = false;
                labelControl13.Visible = false;
                label1.Text = "10";
                timer1.Stop();
            }
        }
        private void droits_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            this.Parent = null;
            e.Cancel = true;
        }

        private void labelControl10_Click(object sender, EventArgs e)
        {
            dxErrorProvider1.Dispose();
            effect();
            panelControl2.Visible = true;
            panelControl3.Visible = false;
            panelControl4.Visible = false;
            panelControl5.Visible = false;
            
            panelControl8.Visible = false;
            gridControl1.Visible = true;
            labelControl17.Visible = false;
            vid();
        }

        private void labelControl17_Click(object sender, EventArgs e)
        {
            //chargement de données personnels
            nom = textEdit1.Text;
            prenom = textEdit6.Text;
            gsm = textEdit8.Text;
            email = textEdit7.Text;
            fonction = textEdit2.Text;
            depart = textEdit5.Text;
            pseudo = textEdit3.Text;
            passwd = textEdit4.Text;
           
            //if (panelControl4.Visible == true)
            //{
                 //gestion de stock

                if (checkEdit32.Checked == true)
                { vis_stock = "OUI"; }
                else { vis_stock = "NON"; }
                if (checkEdit1.Checked == true)
                { ajou_stock = "OUI"; }
                else { ajou_stock = "NON"; }
                if (checkEdit2.Checked == true)
                { modif_stock = "OUI"; }
                else { modif_stock = "NON"; }
                if (checkEdit3.Checked == true)
                { supp_stock = "OUI"; }
                else { supp_stock = "NON"; }
                if (checkEdit4.Checked == true)
                { ger_uni = "OUI"; }
                else { ger_uni = "NON"; }
                if (checkEdit5.Checked == true)
                { ger_mag = "OUI"; }
                else { ger_mag = "NON"; }
                if (checkEdit6.Checked == true)
                { stock_doc = "OUI"; }
                else { stock_doc = "NON"; }
                if (checkEdit7.Checked == true)
                { passer_cde = "OUI"; }
                else { passer_cde = "NON"; }
                if (checkEdit8.Checked == true)
                { alimen = "OUI"; }
                else { alimen = "NON"; }
                if (checkEdit9.Checked == true)
                { sort_prod = "OUI"; }
                else { sort_prod = "NON"; }
                if (checkEdit27.Checked == true)
                { his_alim = "OUI"; }
                else { his_alim = "NON"; }
                if (checkEdit28.Checked == true)
                { his_sort = "OUI"; }
                else { his_sort = "NON"; }

               
                //gestion client

                if (checkEdit34.Checked == true)
                { vis_clt = "OUI"; }
                else { vis_clt = "NON"; }
                if (checkEdit20.Checked == true)
                { aj_clt = "OUI"; }
                else { aj_clt = "NON"; }
                if (checkEdit17.Checked == true)
                { mod_clt = "OUI"; }
                else { mod_clt = "NON"; }
                if (checkEdit19.Checked == true)
                { supp_clt = "OUI"; }
                else { supp_clt = "NON"; }
                if (checkEdit18.Checked == true)
                { clt_doc = "OUI"; }
                else { clt_doc = "NON"; }
                if (checkEdit22.Checked == true)
                { supp_cde_clt = "OUI"; }
                else { supp_cde_clt = "NON"; }
                if (checkEdit23.Checked == true)
                { valid_cde_clt = "OUI"; }
                else { valid_cde_clt = "NON"; }
                if (checkEdit24.Checked == true)
                { fact = "OUI"; }
                else { fact = "NON"; }
                if (checkEdit25.Checked == true)
                { bon_liv = "OUI"; }
                else { bon_liv = "NON"; }
                if (checkEdit26.Checked == true)
                { bon_sort = "OUI"; }
                else { bon_sort = "NON"; }


                //gestion feur
                if (checkEdit33.Checked == true)
                { vis_feur = "OUI"; }
                else { vis_feur = "NON"; }
                if (checkEdit15.Checked == true)
                { aj_feur = "OUI"; }
                else { aj_feur = "NON"; }
                if (checkEdit16.Checked == true)
                { mod_feur = "OUI"; }
                else { mod_feur = "NON"; }
                if (checkEdit14.Checked == true)
                { supp_feur = "OUI"; }
                else { supp_feur = "NON"; }
                if (checkEdit13.Checked == true)
                { feur_doc = "OUI"; }
                else { feur_doc = "NON"; }
                  if (checkEdit21.Checked == true)
                { supp_cde_feur = "OUI"; }
                else { supp_cde_feur = "NON"; }

                //devis

               if (checkEdit35.Checked == true)
                { vis_devis = "OUI"; }
                else { vis_devis = "NON"; }
                 if (checkEdit10.Checked == true)
                { ajout_devis = "OUI"; }
                else { ajout_devis = "NON"; }
                 if (checkEdit11.Checked == true)
                { supp_devis = "OUI"; }
                else { supp_devis = "NON"; }
                 if (checkEdit21.Checked == true)
                { devis_doc = "OUI"; }
                else { devis_doc = "NON"; }



            // comptabilité

                 if (checkEdit36.Checked == true)
                 { conta = "OUI"; }
                 else { conta = "NON"; }
                 if (checkEdit29.Checked == true)
                 { stat_conta = "OUI"; }
                 else { stat_conta = "NON"; }

                //autres
                
                if (checkEdit31.Checked == true)
                { stat = "OUI"; }
                else { stat = "NON"; }
                 if (checkEdit30.Checked == true)
                { not = "OUI"; }
                else { not = "NON"; }

                 if (textEdit5.Text == "Administrateur")
                 {
                     if (etat == "ajouter")
                     {
                         fun.insert_user_admin(nom, prenom, fonction, depart, gsm, email, pseudo, passwd);
                     }

                      if (etat == "modifier")
                     
                        { fun.update_user_admin(id_user, nom, prenom, fonction, depart, gsm, email, pseudo, passwd); }

                      gridControl1.Visible = true;
                      panelControl4.Visible = false;
                      panelControl3.Visible = false;
                      act();

                      panelControl8.Visible = false;
                      vid();
                      effect();

                 }

                 if (textEdit5.Text == "Utilisateur")
                 {

                     if (checkEdit1.Checked == false && checkEdit2.Checked == false && checkEdit3.Checked == false && checkEdit4.Checked == false && checkEdit5.Checked == false && checkEdit6.Checked == false && checkEdit7.Checked == false && checkEdit8.Checked == false && checkEdit9.Checked == false && checkEdit10.Checked == false && checkEdit11.Checked == false && checkEdit12.Checked == false && checkEdit13.Checked == false && checkEdit14.Checked == false && checkEdit15.Checked == false && checkEdit16.Checked == false && checkEdit17.Checked == false && checkEdit18.Checked == false && checkEdit19.Checked == false && checkEdit20.Checked == false && checkEdit21.Checked == false && checkEdit22.Checked == false && checkEdit23.Checked == false && checkEdit24.Checked == false && checkEdit25.Checked == false && checkEdit26.Checked == false && checkEdit27.Checked == false && checkEdit28.Checked == false && checkEdit30.Checked == false && checkEdit31.Checked == false && checkEdit32.Checked == false && checkEdit33.Checked == false && checkEdit34.Checked == false && checkEdit35.Checked == false && checkEdit43.Checked == false && checkEdit36.Checked == false && checkEdit29.Checked == false)
                     { XtraMessageBox.Show("Aucune option choisie!", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Information); }


                     else
                     {
                         if (etat == "ajouter")
                         {


                             //ajout droits d'accés
                             fun.insert_user_gestion_stock(nom, prenom, fonction, depart, gsm, email, pseudo, passwd, vis_stock, ajou_stock, modif_stock, supp_stock, ger_uni, ger_mag, stock_doc, passer_cde, alimen, sort_prod, his_alim, his_sort, vis_clt, aj_clt, mod_clt, supp_clt, clt_doc, supp_cde_clt, valid_cde_clt, fact, bon_liv, bon_sort, vis_feur, aj_feur, mod_feur, supp_feur, feur_doc, supp_cde_feur, vis_devis, ajout_devis, supp_devis, devis_doc, stat, not, conta, stat_conta);

                             labelControl13.Visible = true;
                             timer1.Start();
                         }
                         if (etat == "modifier")
                         {


                             //modifier droits d'accés
                             fun.update_user_gestion_stock(id_user, nom, prenom, fonction, depart, gsm, email, pseudo, passwd, vis_stock, ajou_stock, modif_stock, supp_stock, ger_uni, ger_mag, stock_doc, passer_cde, alimen, sort_prod, his_alim, his_sort, vis_clt, aj_clt, mod_clt, supp_clt, clt_doc, supp_cde_clt, valid_cde_clt, fact, bon_liv, bon_sort, vis_feur, aj_feur, mod_feur, supp_feur, feur_doc, supp_cde_feur, vis_devis, ajout_devis, supp_devis, devis_doc, stat, not, conta, stat_conta);
                             labelControl12.Visible = true;
                             timer1.Start();
                         }
                         gridControl1.Visible = true;
                         panelControl4.Visible = false;
                         panelControl3.Visible = false;
                         act();

                         panelControl8.Visible = false;
                         vid();
                         effect();

                     }
                 }
                     
        }

        private void droits_Activated(object sender, EventArgs e)
        {
            Form1.load = 1;
            Form1.wait = 1;
            gridView1.OptionsView.ShowAutoFilterRow = true;
            gridView1.BestFitColumns();
            gridView1.OptionsBehavior.Editable = false;
            gridView1.OptionsView.EnableAppearanceEvenRow = true;
        }

        private void pictureEdit2_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void checkEdit43_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit43.Checked == true)
            { textEdit4.Properties.UseSystemPasswordChar = true; }
            else { textEdit4.Properties.UseSystemPasswordChar = false; }
        }
    }
}