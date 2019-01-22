using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace RibbonSimplePad
{
    public partial class ajout_devis : DevExpress.XtraEditors.XtraForm
    {
        public ajout_devis()
        {
            InitializeComponent();
        }
        public static int id_devis;
        sql_gmao fun =new sql_gmao();
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string client = lookupedit1.Text.ToString();

            if (lookupedit1.EditValue.ToString() == "")
            {
                XtraMessageBox.Show("Choisir ou ajouter un client ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                if (liste_devis.etat == "modif")
                {
                    id_devis = liste_devis.id_devis;

                }
                 if (liste_devis.etat == "ajout")
                {
                   fun. update__devis(lookupedit1.Text,lookupedit1.EditValue.ToString(),liste_devis.id_devis);
                  
                }
                 plus_devis pp = new plus_devis();
                 pp.ShowDialog();
              
            }
            
        }

        private void piece() 
        {
          
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            gridControl1.DataSource = fun.get_piece_from_devis(liste_devis.id_devis);
            this.gridView1.Columns[0].Visible = false;
            this.gridView1.Columns[1].Visible = false;
            this.gridView1.Columns[2].Caption = "Libellé";
            this.gridView1.Columns[3].Caption = "Quantité";
            this.gridView1.Columns[4].Visible = false;
            this.gridView1.Columns[5].Visible = false;
            this.gridView1.Columns[6].Visible = false;
            this.gridView1.Columns[7].Visible = false;
            this.gridView1.Columns[8].Visible = false;
            this.gridView1.Columns[9].Caption = "Prix Unitaire HT";
            this.gridView1.Columns[10].Caption = "Montant HT";
            

            gridView1.OptionsView.ShowAutoFilterRow = true;
        }

        private void clients()
        {
            //get All stock
            lookupedit1.Properties.DataSource = null;
            DataTable Allclients = new DataTable();
            Allclients = fun.get_clients();
            for (int i = 0; i < Allclients.Rows.Count; i++)
            {
                lookupedit1.Properties.ValueMember = "code_clt";
                lookupedit1.Properties.DisplayMember = "raison_soc";
                lookupedit1.Properties.DataSource = Allclients;
                lookupedit1.Properties.PopulateColumns();
                lookupedit1.Properties.Columns["code_clt"].Caption = "Code client";
                lookupedit1.Properties.Columns["raison_soc"].Caption = "Raison sociale";
                lookupedit1.Properties.Columns["responsbale"].Caption = "Responsable";
                lookupedit1.Properties.Columns["gsm_clt"].Visible = false;
                lookupedit1.Properties.Columns["gsm_clt"].Visible = false;
                lookupedit1.Properties.Columns["tel_clt"].Visible = false;
                lookupedit1.Properties.Columns["fax_clt"].Visible = false;
                lookupedit1.Properties.Columns["adresse_clt"].Visible = false;
                lookupedit1.Properties.Columns["cp_clt"].Visible = false;
                lookupedit1.Properties.Columns["ville_clt"].Visible = false;
                lookupedit1.Properties.Columns["email_clt"].Visible = false;
                lookupedit1.Properties.Columns["site_clt"].Visible = false;
                lookupedit1.Properties.Columns["tva_clt"].Visible = false;
                lookupedit1.Properties.Columns["forme_juriduque"].Visible = false;
                lookupedit1.Properties.Columns["mode_pay"].Visible = false;
            }
        }

        private void ajout_devis_Load(object sender, EventArgs e)
        {

            if (liste_devis.etat_envoie == "envoyé")
            { simpleButton1.Enabled = false; }
            else { simpleButton1.Enabled = true; }
            clients();
            if (liste_devis.etat == "modif")
            {
                lookupedit1.Enabled = false;


                DataTable nn = new DataTable();
                nn = fun.get_cltByCode(liste_devis.id_clt);
                lookupedit1.Text = nn.Rows[0][1].ToString();

            }
            if (liste_devis.etat == "ajout")
            {

                lookupedit1.Enabled = true;
                simpleButton1.Enabled = true;
                DataTable aa = new DataTable();

                string w = "en cours";
                //ajouter une commande
                fun.insert_into_devis(w);
                DataTable tt = new DataTable();
                tt = fun.get_max_devis(w);
                if (tt.Rows.Count != 0)
                {
                    id_devis = Convert.ToInt32(tt.Rows[0][0]);


                }
            }
            
        }

        private void ajout_devis_Activated(object sender, EventArgs e)
        {

            if (liste_devis.etat == "ajout")
            {
                liste_devis.id_devis = id_devis;
                if (lookupedit1.Text != "")
                { lookupedit1.Enabled = false; }
                else { lookupedit1.Enabled = true; gridView1.Columns.Clear(); }
               
            }

            piece();
            gridView1.OptionsView.ShowAutoFilterRow = true;
            gridView1.BestFitColumns();
            gridView1.OptionsBehavior.Editable = false;
            gridView1.OptionsView.EnableAppearanceEvenRow = true;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Liste_devis();
        }

        private void Liste_devis()
        {
            liste_devis listdevis=new liste_devis();
           listdevis.gridControl1.DataSource = null;
           listdevis.gridView1.Columns.Clear();
           listdevis.gridControl1.DataSource = fun.get_devis();
           listdevis.gridView1.Columns[0].Caption = "Code Devis";
           listdevis.gridView1.Columns[1].Caption = "Date d'ajout de Devis";
           listdevis.gridView1.Columns[2].Visible = false;
           listdevis.gridView1.Columns[3].Visible = false;
           listdevis.gridView1.Columns[4].Caption = "Etat de Devis ";
           listdevis.gridView1.Columns[5].Caption = "Client";

           listdevis.gridView1.Columns[6].Visible = false;
           listdevis.gridView1.Columns[7].Visible = false;
           listdevis.gridView1.Columns[8].Visible = false;
           listdevis.gridView1.Columns[9].Visible = false;
           listdevis.gridView1.Columns[10].Visible = false;
           listdevis.gridView1.Columns[11].Visible = false;
           listdevis.gridView1.Columns[12].Visible = false;
           listdevis.gridView1.OptionsView.ShowAutoFilterRow = true;
           listdevis.Show();
        }

       
    }
}