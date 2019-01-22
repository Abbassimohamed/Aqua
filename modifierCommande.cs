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
    public partial class modifierCommande : DevExpress.XtraEditors.XtraForm
    {
        public static Double prixtotc=0;
        public static DataTable dartt ;
        public static string codepiece = "";
        double timbre = 0;
        public modifierCommande()
        {
            InitializeComponent();
            lookUpEdit1.Text = "";
            lookUpEdit2.Text = "";
                  
            tnumcommandebase.Text = liste_cde_client.id_commande.ToString();
            lookUpEdit1.EditValue = liste_cde_client.id_clt;
            tnbcmd.Text = liste_cde_client.id_cmd;
            DataTable dt_cmd = fun.affiche_cmdclt_infos(liste_cde_client.id_commande);
            if (dt_cmd.Rows.Count > 0)
            {
                double.TryParse(dt_cmd.Rows[0]["timbre"].ToString().Replace('.', ','), out timbre);
                string etatcmd = dt_cmd.Rows[0]["etatcmd"].ToString();
                if (etatcmd == "validée")
                {
                    simpleButton4.Visible = false;
                    simpleButton3.Visible = false;
                    simpleButton1.Visible = false;
                    simpleButton2.Visible = false;
                }
                prixtotc = timbre;
            }
            try
            {
                getalldatatable();
            }
            catch (Exception exception)
            { }
           
        }
        public static int idpiece = 0, numligne=0;
      
        sql_gmao fun = new sql_gmao();
        
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (mdesign.Text == "")
                {
                    mdesign.Text = "";
                }
                if (tpu.Text == "")
                {
                    tpu.Text = "0";
                }
                if (ttva.Text == "")
                {
                    ttva.Text = "0";
                }
                if (tquantit.Text == "")
                {
                    tquantit.Text = "0";
                }
                if (tunit.Text == "")
                {
                    tunit.Text = "";

                }
                if (tremise.Text == "")
                {
                    tremise.Text = "0";

                }
                if (lookUpEdit1.EditValue == null)
                {
                    lookUpEdit1.EditValue = "";

                }
                if (lookUpEdit2.EditValue == null)
                {
                    lookUpEdit2.EditValue = "";

                }

                Double pvhorstva = Convert.ToDouble(tpu.Text.Replace('.', ',')) * Convert.ToDouble(tquantit.Text.Replace('.', ','));
                Double prixremis = pvhorstva - ((pvhorstva * Convert.ToDouble(tremise.Text.Replace('.', ','))) / 100);
                Double pvtva = prixremis + ((prixremis * Convert.ToDouble(ttva.Text.Replace('.', ','))) / 100);

                DataRowView rowView = (DataRowView)lookUpEdit2.GetSelectedDataRow();
                DataRow row = rowView.Row;
                DataRowView rowView1 = (DataRowView)lookUpEdit1.GetSelectedDataRow();
                DataRow row1 = rowView1.Row;
                string codep = row[0].ToString();
                string codeclt = row1[0].ToString();
                DataRow newpc = dartt.NewRow();

                newpc["code_art"] = codep;
                newpc["libelle_piece"] = mdesign.Text;
                newpc["quantite_piece"] = Convert.ToDouble(tquantit.Text.Replace('.', ','));
                newpc["id_clt"] = codeclt;
                newpc["etat"] = "validée";
                newpc["puv"] = tpu.Text;
                newpc["totvente"] = pvtva.ToString();
                newpc["id_commande"] = tnumcommandebase.Text;
                newpc["remise"] = tremise.Text;
                newpc["ttva"] = ttva.Text;
                newpc["unit"] = tunit.Text;
                newpc["Qte rest"] = Convert.ToDouble(tquantit.Text.Replace('.', ','));
                dartt.Rows.Add(newpc);
                gridControl1.DataSource = null;
                gridView1.Columns.Clear();
                gridControl1.DataSource = dartt;
                this.gridView1.Columns[0].Visible = false;
                this.gridView1.Columns[1].Caption = "Code article";
                this.gridView1.Columns[2].Caption = "Désignation";
                this.gridView1.Columns[3].Caption = "Quantité";
                this.gridView1.Columns[4].Visible = false;
                this.gridView1.Columns[5].Visible = false;
                this.gridView1.Columns[6].Caption = "PUV";
                this.gridView1.Columns[7].Caption = "Prix ttc";
                this.gridView1.Columns[8].Visible = false;
                this.gridView1.Columns[9].Caption = "remise";
                this.gridView1.Columns[10].Caption = "TVA";
                this.gridView1.Columns[11].Caption = "unité";
                this.gridView1.Columns[12].Caption = "QteRest";
                gridView1.OptionsView.ShowAutoFilterRow = true;
            }
            catch (Exception er)
            {
                MessageBox.Show("veuillez remplir les champs vides");
            }
        }
           
    
 
        
     

        private void getalldatatable()
        {
            DataTable dt = new DataTable();
            dt = fun.get_AllprodbyCMD(liste_cde_client.id_commande.ToString());

            dartt = new DataTable();
            dartt.Clear();
            dartt.Columns.Add("id_piece");
            dartt.Columns.Add("code_art");
            dartt.Columns.Add("libelle_piece");
            dartt.Columns.Add("quantite_piece");
            dartt.Columns.Add("id_clt");
            dartt.Columns.Add("etat");
            dartt.Columns.Add("puv");
            dartt.Columns.Add("totvente");
            dartt.Columns.Add("id_commande");
            dartt.Columns.Add("remise");
            dartt.Columns.Add("ttva");
            dartt.Columns.Add("unit");
            dartt.Columns.Add("Qte rest");
            foreach (DataRow row in dt.Rows)
            {
                DataRow newpc = dartt.NewRow();
                newpc["id_piece"] = row[0];
                newpc["code_art"] = row[1];
                newpc["libelle_piece"] = row[2];
                newpc["quantite_piece"] = Convert.ToDouble(row[3].ToString().Replace('.', ','));
                newpc["id_clt"] = row[4];
                newpc["etat"] = "validée";
                newpc["puv"] = row[6];
                newpc["totvente"] = row[7];
                newpc["id_commande"] = row[8];
                newpc["remise"] = row[9];
                newpc["ttva"] = row[10];
                newpc["unit"] = row[11];
                newpc["Qte rest"] = row[12];
                dartt.Rows.Add(newpc);
            }
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            gridControl1.DataSource = dartt;
            this.gridView1.Columns[0].Visible = false;
            this.gridView1.Columns[1].Caption = "Code article";
            this.gridView1.Columns[2].Caption = "Désignation";
            this.gridView1.Columns[3].Caption = "Quantité";
            this.gridView1.Columns[4].Visible = false;
            this.gridView1.Columns[5].Visible = false;
            this.gridView1.Columns[6].Caption = "PUV";
            this.gridView1.Columns[7].Caption = "Prix ttc";
            this.gridView1.Columns[8].Visible = false;
            this.gridView1.Columns[9].Caption = "remise";
            this.gridView1.Columns[10].Caption = "TVA";
            this.gridView1.Columns[11].Caption = "unité";
            this.gridView1.Columns[12].Caption = "QteRest";
            gridView1.OptionsView.ShowAutoFilterRow = true;
          
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            string etat = "en cours";
            string etat_fac = "en cours";
            string timbre = this.timbre.ToString();
           DataTable dt=new DataTable();
          
           dt = fun.get_cltByDesign(lookUpEdit1.Text);
          string  id_clt=dt.Rows[0][0].ToString();
         
           
          if (gridView1.RowCount != 0)
          {

              fun.delete_piece_from_cde_clt(liste_cde_client.id_commande);
              for (int i = 0; i < gridView1.RowCount; i++)
              {
                  DataRow row = gridView1.GetDataRow(i);
                  fun.insert_piecee_commande1(row[1].ToString(), row[2].ToString(), Convert.ToDouble(row[3].ToString().Replace('.', ',')), row[11].ToString(), row[6].ToString(), row[9].ToString(), row[10].ToString(), id_clt.ToString(), tnumcommandebase.Text, row[7].ToString(),row[12].ToString());
                                }
              fun.update_comandeclient(prixtotc.ToString(), tnumcommandebase.Text);
             
              MessageBox.Show("la commande est modifiée avec succées veuillez Actualiser la liste des commandes clients");
              this.Close();
          }
          else
          {
              MessageBox.Show("veuillez insérer les lignes de commande");
          }
          
        }


        private void clients()
        {
            //get All stock
            lookUpEdit1.Properties.DataSource = null;
            DataTable Allclients = new DataTable();
            Allclients = fun.get_Allclt();
            
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
        private void articles()
        {
            //get All client
            lookUpEdit2.Properties.DataSource = null;
            DataTable Allarticle = new DataTable();
            Allarticle = fun.get_allstock();
            
                lookUpEdit2.Properties.ValueMember = "code_piece";
                lookUpEdit2.Properties.DisplayMember = "libelle_piece";
                lookUpEdit2.Properties.DataSource = Allarticle;
                lookUpEdit2.Properties.PopulateColumns();
                lookUpEdit2.Properties.Columns["code_piece"].Caption = "Code article";
                lookUpEdit2.Properties.Columns["libelle_piece"].Caption = "Désignation";
                lookUpEdit2.Properties.Columns["unite_piece"].Visible = false;
                lookUpEdit2.Properties.Columns["quantite_piece"].Visible = false;
                lookUpEdit2.Properties.Columns["quantite_reelle"].Visible = false;
                lookUpEdit2.Properties.Columns["seuil_piece"].Visible = false;
                lookUpEdit2.Properties.Columns["nature"].Visible = false;
                lookUpEdit2.Properties.Columns["pua"].Visible = false;
                lookUpEdit2.Properties.Columns["puv"].Visible = false;
                lookUpEdit2.Properties.Columns["empalcement_piece"].Visible = false;
                lookUpEdit2.Properties.Columns["code_feur"].Visible = false;
                lookUpEdit2.Properties.Columns["puv_rev"].Visible = false;
                lookUpEdit2.Properties.Columns["couleur"].Visible = false;
                lookUpEdit2.Properties.Columns["taille"].Visible = false;
                lookUpEdit2.Properties.Columns["sous_categorie"].Visible = false;
               
            
        }


        private void passerCommande_Activated(object sender, EventArgs e)
        {
            clients();
            articles();
        }

        private void lookUpEdit2_EditValueChanged(object sender, EventArgs e)
        {
            DataRowView rowView = (DataRowView)lookUpEdit2.GetSelectedDataRow();
            DataRow row = rowView.Row;
          
            mdesign.Text =row[1].ToString();
            tunit.Text = row[2].ToString();
            tpu.Text = row[8].ToString();
            
        }

        private void tpu_Validated(object sender, EventArgs e)
        {
            DataRowView rowView = (DataRowView)lookUpEdit2.GetSelectedDataRow();
            DataRow row = rowView.Row;
            Double prixv = Convert.ToDouble(tpu.Text.Replace('.', ','));
            Double prixachat = Convert.ToDouble(row[7].ToString().Replace('.', ','));
            if (prixv <= prixachat)
            {
                XtraMessageBox.Show("le frais d'achat est de "+row[7].ToString());
            }
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            try
            {
                int count = gridView1.DataRowCount;

                if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
                {
                    numligne = gridView1.FocusedRowHandle;
                    DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                    // idpiece = Convert.ToInt32(row[0].ToString());
                    codepiece = row[0].ToString();
                    lookUpEdit2.EditValue = row[1].ToString();
                    mdesign.Text = row[2].ToString();
                    tquantit.Text = row[3].ToString();
                    tpu.Text = row[6].ToString();
                    tunit.Text = row[11].ToString();
                    ttva.Text = row[10].ToString();
                    tremise.Text = row[9].ToString();

                }
            }
            catch (Exception except)
            { }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            try
            {
                Double pvhorstva = Convert.ToDouble(tpu.Text.Replace('.', ',')) * Convert.ToDouble(tquantit.Text.Replace('.', ','));
                Double prixremis = pvhorstva - ((pvhorstva * Convert.ToDouble(tremise.Text.Replace('.', ','))) / 100);
                Double pvtva = prixremis + ((prixremis * Convert.ToDouble(ttva.Text.Replace('.', ','))) / 100);

                DataRowView rowView1 = (DataRowView)lookUpEdit1.GetSelectedDataRow();
                DataRow row1 = rowView1.Row;

                string codeclt = row1[0].ToString();
                DataRow row = dartt.NewRow();
                row[1] = codepiece;
                row[2] = mdesign.Text;
                row[3] = Convert.ToDouble(tquantit.Text.Replace('.', ','));
                row[4] = codeclt;
                row[5] = "validée";
                row[6] = tpu.Text;
                row[7] = pvtva.ToString();
                row[8] = tnumcommandebase.Text;
                row[9] = tremise.Text;
                row[10] = ttva.Text;
                row[11] = tunit.Text;
                row[12] = Convert.ToDouble(tquantit.Text.Replace('.', ','));
                prixtotc -= Convert.ToDouble(dartt.Rows[numligne][7].ToString());
                dartt.Rows[numligne].Delete();

                dartt.Rows.Add(row);
                gridControl1.DataSource = null;
                gridView1.Columns.Clear();
                gridControl1.DataSource = dartt;
                this.gridView1.Columns[0].Visible = false;
                this.gridView1.Columns[1].Caption = "Code article";
                this.gridView1.Columns[2].Caption = "Désignation";
                this.gridView1.Columns[3].Caption = "Quantité";
                this.gridView1.Columns[4].Visible = false;
                this.gridView1.Columns[5].Visible = false;
                this.gridView1.Columns[6].Caption = "PUV";
                this.gridView1.Columns[7].Caption = "Prix ttc";
                this.gridView1.Columns[8].Visible = false;
                this.gridView1.Columns[9].Caption = "remise";
                this.gridView1.Columns[10].Caption = "TVA";
                this.gridView1.Columns[11].Caption = "unité";
                this.gridView1.Columns[12].Caption = "QteRest";
                gridView1.OptionsView.ShowAutoFilterRow = true;

                mdesign.Text = "";
                tpu.Text = "0";
                ttva.Text = "0";
                tquantit.Text = "0";
                tunit.Text = "";
                tremise.Text = "0";
                //lookUpEdit1.EditValue = "choisir un client";



                // fun.update_qte_piece_commande(mdesign.Text,tquantit.Text,idpiece,tremise.Text,prixremis.ToString(),ttva.Text,tpu.Text);
                // MessageBox.Show("la commande a été mise à jour");
                //getallcmdbypro(tnumcommandebase.Text);
            }
            catch (Exception et)
            {
            }
        }
     
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            try
            {
                int count = gridView1.DataRowCount;
                if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
                {
                    DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                    dartt.Rows[numligne].Delete();
                    MessageBox.Show("la commande a été mise à jour");
                    gridControl1.DataSource = null;
                    gridView1.Columns.Clear();
                    gridControl1.DataSource = dartt;
                    this.gridView1.Columns[0].Visible = false;
                    this.gridView1.Columns[1].Caption = "Code article";
                    this.gridView1.Columns[2].Caption = "Désignation";
                    this.gridView1.Columns[3].Caption = "Quantité";
                    this.gridView1.Columns[4].Visible = false;
                    this.gridView1.Columns[5].Visible = false;
                    this.gridView1.Columns[6].Caption = "PUV";
                    this.gridView1.Columns[7].Caption = "Prix ttc";
                    this.gridView1.Columns[8].Visible = false;
                    this.gridView1.Columns[9].Caption = "remise";
                    this.gridView1.Columns[10].Caption = "TVA";
                    this.gridView1.Columns[11].Caption = "unité";
                    this.gridView1.Columns[12].Caption = "QteRest";
                    gridView1.OptionsView.ShowAutoFilterRow = true;
                  
                }
            }
            catch (Exception exc)
            { }
        }

        private void modifierCommande_Load(object sender, EventArgs e)
        {

        }
       
       
    }
}
    
