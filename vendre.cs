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
    public partial class vendre : DevExpress.XtraEditors.XtraForm
    {
        public vendre()
        {
            InitializeComponent();
           
        }
        sql_gmao fun = new sql_gmao();
        Control animatedControl1;
        public static int  idd_fact, qte_piece;
        public static string lib_piece, test, id_piece, client, puv, puv_rev, id_client, nbcomande;


        private void vendre_Load(object sender, EventArgs e)
        {
          

            lookUpEdit1.Text = "";
            pictureEdit1.Visible = false;
            pictureEdit2.Visible = false;
            labelControl17.Visible = false;
            labelControl10.Visible = false;
            groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            groupControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            groupControl4.Dock = System.Windows.Forms.DockStyle.Fill;
            panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
           
        }
        private void clients()
        {
            //get All stock
            lookUpEdit1.Properties.DataSource = null;
            DataTable Allclients = new DataTable();
            Allclients = fun.get_clients();
            for (int i = 0; i < Allclients.Rows.Count; i++)
            {
                lookUpEdit1.Properties.ValueMember = "code_clt";
                lookUpEdit1.Properties.DisplayMember = "raison_soc";
                lookUpEdit1.Properties.DataSource = Allclients;
                lookUpEdit1.Properties.PopulateColumns();
                lookUpEdit1.Properties.Columns["code_clt"].Caption = "Code client";
                lookUpEdit1.Properties.Columns["raison_soc"].Caption = "Raison sociale";
                lookUpEdit1.Properties.Columns["responsbale"].Caption = "Responsable";
                lookUpEdit1.Properties.Columns["gsm_clt"].Visible = false;
                lookUpEdit1.Properties.Columns["gsm_clt"].Visible = false;
                lookUpEdit1.Properties.Columns["tel_clt"].Visible = false;
                lookUpEdit1.Properties.Columns["fax_clt"].Visible = false;
                lookUpEdit1.Properties.Columns["adresse_clt"].Visible = false;
                lookUpEdit1.Properties.Columns["cp_clt"].Visible = false;
                lookUpEdit1.Properties.Columns["ville_clt"].Visible = false;
                lookUpEdit1.Properties.Columns["email_clt"].Visible = false;
                lookUpEdit1.Properties.Columns["site_clt"].Visible = false;
                lookUpEdit1.Properties.Columns["tva_clt"].Visible = false;
                lookUpEdit1.Properties.Columns["forme_juriduque"].Visible = false;
                lookUpEdit1.Properties.Columns["mode_pay"].Visible = false;
            }
        }

        //effet de transition
        private void effect()
        {
            if (transitionManager1.Transitions[panelControl1] == null)
            {
                Transition transition1 = new Transition();
                transition1.Control = panelControl1;
                transitionManager1.Transitions.Add(transition1);
            }
            DevExpress.Utils.Animation.Transitions trType = (DevExpress.Utils.Animation.Transitions.Push);
            transitionManager1.Transitions[panelControl1].TransitionType = CreateTransitionInstance(trType);
            if (transitionManager1.IsTransaction)
            { transitionManager1.EndTransition(); }
            transitionManager1.StartTransition(panelControl1);
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
        private void ListeStock()
        {
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            gridControl1.DataSource = fun.get_stocknotprem();
            this.gridView1.Columns[0].Caption = "Code pièce";
            this.gridView1.Columns[1].Caption = "Désigniation";
            this.gridView1.Columns[2].Caption = "Unité";
            this.gridView1.Columns[3].Caption = "Quantité";
            this.gridView1.Columns[4].Caption = "Quantité réelle";
            this.gridView1.Columns[5].Caption = "Stock d'alerte";
            this.gridView1.Columns[6].Caption = "Nature";
            this.gridView1.Columns[7].Caption = "PUA";
            this.gridView1.Columns[8].Caption = "PUV";
            this.gridView1.Columns[9].Caption = "Emplacement";
            this.gridView1.Columns[10].Visible = false;
            this.gridView1.Columns[11].Visible = false;
            this.gridView1.Columns[12].Visible = false;
            this.gridView1.Columns[13].Visible = false;
            this.gridView1.Columns[14].Visible = false;

            gridView1.OptionsView.ShowAutoFilterRow = true;
        }

        private void vendre_Activated(object sender, EventArgs e)
        {
            ListeStock();
            clients();
            Form1.load = 1;

            Form1.wait = 1;
            gridView1.OptionsView.ShowAutoFilterRow = true;
            gridView1.BestFitColumns();
            gridView1.OptionsBehavior.Editable = false;
            gridView1.OptionsView.EnableAppearanceEvenRow = true;
            simpleButton2.Location = new Point(
   this.groupControl1.Width / 2 - simpleButton2.Size.Width / 2,
    this.groupControl1.Height / 2 - simpleButton2.Size.Height / 2
   );
        
        }

        private void simpleButton2_Click(object sender, System.EventArgs e)
        {
            effect();
            groupControl1.Visible = false;
            groupControl3.Visible = false;
            groupControl4.Visible = false;
            groupControl2.Visible = true;

            pictureEdit1.Visible = true;
            pictureEdit2.Visible = true;
            labelControl17.Visible = true;
            labelControl10.Visible = true;

        }
        private void pictureEdit1_Click(object sender, System.EventArgs e)
        {
            if (groupControl2.Visible == true)
            {

                effect();
                groupControl1.Visible = true;
                groupControl2.Visible = false;
                groupControl3.Visible = false;
                groupControl4.Visible = false;

                pictureEdit1.Visible = false;
                pictureEdit2.Visible = false;
                labelControl17.Visible = false;
                labelControl10.Visible = false;
            }

            if (groupControl3.Visible == true)
            {

                effect();
                groupControl1.Visible = false;
                groupControl2.Visible = true;
                groupControl3.Visible = false;
                groupControl4.Visible = false;

                pictureEdit1.Visible = true;
                pictureEdit2.Visible = true;
                labelControl17.Visible = true;
                labelControl10.Visible = true;
            }
        }

        private void pictureEdit2_Click(object sender, System.EventArgs e)
        {

            if (groupControl2.Visible == true)
            {
                if (lookUpEdit1.Text == lookUpEdit1.Properties.NullText)
                {
                    XtraMessageBox.Show("aucune commande n'a été passé");
                }
                else
                {
                    id_client = lookUpEdit1.EditValue.ToString(); ;
                    client = lookUpEdit1.Text.ToString();

                    string etat = "en cours";
                    DataTable dt = new DataTable();
                    dt = fun.get_max_fac(id_client, etat);


                    if (dt.Rows.Count != 0)
                    {
                        idd_fact = Convert.ToInt32(dt.Rows[0][0]);

                        if (Convert.ToInt32(dt.Rows[0][0]) != 0)
                        {

                            effect();
                            groupControl3.Visible = true;
                            groupControl2.Visible = false;
                            groupControl1.Visible = false;
                            groupControl4.Visible = false;

                            pictureEdit2.Visible = false;
                            pictureEdit1.Visible = true;
                            labelControl10.Visible = true;
                            labelControl17.Visible = false;

                            DataTable gg = new DataTable();
                            gg = fun.cheak_etat2(idd_fact);


                            if (gg.Rows.Count != 0)
                            {



                                if (gg.Rows[0][0].ToString() == "validée")
                                {


                                    simpleButton6.Enabled = false;
                                    simpleButton6.Visible = false;
                                    labelControl3.Visible = true;
                                    pictureEdit3.Visible = true;

                                }
                                else
                                {
                                    simpleButton6.Enabled = true;
                                    simpleButton6.Visible = true;
                                    labelControl3.Visible = false;
                                    pictureEdit3.Visible = false;
                                }


                            }
                        }



                    }

                    else
                    {


                        XtraMessageBox.Show("Pas de piéces ajoutées à la commande", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }

                    if (simpleButton6.Enabled == true)
                    { labelControl3.Visible = false; simpleButton6.Text = "Valider La commande client"; }
                    else
                    {
                        labelControl3.Visible = false; //simpleButton6.Text = "Commande client validée"; 
                        simpleButton6.Visible = false;
                    }


                }
            }
        }

        private void labelControl17_Click(object sender, System.EventArgs e)
        {
            if (groupControl2.Visible == true)
            {

                effect();
                groupControl3.Visible = true;
                groupControl2.Visible = false;
                groupControl1.Visible = false;
                groupControl4.Visible = false;

                pictureEdit2.Visible = false;
                pictureEdit1.Visible = true;
                labelControl10.Visible = true;
                labelControl17.Visible = false;
            }
        }

      
        private void labelControl10_Click(object sender, System.EventArgs e)
        {
            effect();
            groupControl3.Visible = false;
            groupControl2.Visible = false;
            groupControl1.Visible = true;
            groupControl4.Visible = false;

            pictureEdit1.Visible = false;
            pictureEdit2.Visible = false;
            labelControl10.Visible = false;
            labelControl17.Visible = false;
        }

        private void pictureEdit1_MouseHover(object sender, System.EventArgs e)
        {
            pictureEdit1.Image = Properties.Resources.precedant_h;
        }

        private void pictureEdit1_MouseLeave(object sender, System.EventArgs e)
        {
            pictureEdit1.Image = Properties.Resources.precedant;
        }

        private void pictureEdit2_MouseHover(object sender, System.EventArgs e)
        {
            pictureEdit2.Image = Properties.Resources.suivant_h;
        }

        private void pictureEdit2_MouseLeave(object sender, System.EventArgs e)
        {
            pictureEdit2.Image = Properties.Resources.suivant;
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            client = lookUpEdit1.Text.ToString();
            if (lookUpEdit1.EditValue.ToString() == "0")
            {
                XtraMessageBox.Show("Choisir ou ajouter un client ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                
                
                
                int count = gridView1.DataRowCount;
                if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
                {

                    System.Data.DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                    test = "vendre";
                    id_client = lookUpEdit1.EditValue.ToString();
                    nbcomande = textEdit1.Text;
                    id_piece = row[0].ToString();
                    lib_piece = Convert.ToString(row[1]);
                    qte_piece = Convert.ToInt32(row[3]);
                    puv = row[8].ToString();
                    puv_rev = row[11].ToString();
                    vendre2 qq = new vendre2();
                    qq.ShowDialog();
                }
            }
        }
        
       

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            string etat;

            etat = "en cours";
           
            id_client = lookUpEdit1.EditValue.ToString();

            DataTable dt = new DataTable();
            dt = fun.get_max_fac(id_client, etat);
           

            if (dt.Rows.Count != 0)
            {
                idd_fact = Convert.ToInt32(dt.Rows[0][0]);

              

                voir_pieces vp = new voir_pieces();
                vp.ShowDialog();

            }
            else
            {
                XtraMessageBox.Show("Pas de piéces ajoutées à la commande", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
          
            }
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
           
            simpleButton6.Enabled = false;
            simpleButton6.Visible = false;
            simpleButton6.Text = "commande validée!!";
            string libelle;
            libelle = "Commande client n°: " + idd_fact;
            id_client = lookUpEdit1.EditValue.ToString();
            string etat = "en cours";
            DataTable dt = new DataTable();
            dt = fun.get_max_fac(id_client, etat);
         

            if (dt.Rows.Count != 0)
            {
                idd_fact = Convert.ToInt32(dt.Rows[0][0]);

            }
          
            string etat2 = "validée";
            fun.set_bon_sortie(libelle,idd_fact,etat,id_client, client);
            fun.update_etat_cde_clt(etat2, idd_fact);
            labelControl3.Visible = true;
            pictureEdit3.Visible = true;
            labelControl3.Text = "La commande client n° " + idd_fact + " est validée";
        }

      
        private void vendre_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            this.Parent = null;
            e.Cancel = true;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            gestion_client gg = new gestion_client();
            gg.ShowDialog();
        }

        private void simpleButton3_Click_1(object sender, EventArgs e)
        {
            try
            {
                string etat;

                etat = "en cours";
                if (Convert.ToInt32(lookUpEdit1.EditValue) == 0)
                { return; }
                id_client = lookUpEdit1.EditValue.ToString();

                DataTable dt = new DataTable();
                dt = fun.get_max_fac(id_client, etat);


                if (dt.Rows.Count != 0)
                {
                    idd_fact = Convert.ToInt32(dt.Rows[0][0]);



                    voir_pieces vp = new voir_pieces();
                    vp.ShowDialog();

                }
                else
                {
                    XtraMessageBox.Show("Pas de piéces ajoutées à la commande", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("vérifier la commande");
            
            }
        
              }

       

      private void gridControl1_MouseClick_1(object sender, MouseEventArgs e)
        {
           int id_fact1;
             
                string etat_fact = "envoyée";
                string etat_liv = "envoyé";
                DataTable dt = new DataTable();
                if (lookUpEdit1.Text == lookUpEdit1.Properties.NullText)
                {
                    XtraMessageBox.Show("Veuillez choisir un client ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {

                    id_client = lookUpEdit1.EditValue.ToString();
                    nbcomande = textEdit1.Text;
                    dt = fun.test_fact2(id_client, etat_fact, etat_liv);
                    if (e.Button == System.Windows.Forms.MouseButtons.Right)
                    {

                        Point pt = this.Location;
                        pt.Offset(this.Left + e.X, this.Top + e.Y);
                        popupMenu1.ShowPopup(this.barManager1, Control.MousePosition);
                    }
                } 
                   
                }

      private void pictureEdit2_EditValueChanged(object sender, EventArgs e)
      {

      }

    
    }
}