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
    public partial class passerCommande : DevExpress.XtraEditors.XtraForm
    {
        public static Double prixtotc=0.500;
        public static DataTable darttab ;
        public passerCommande()
        {
            InitializeComponent();
            lookUpEdit1.Text = "";
            lookUpEdit2.Text = "";
            textBox1.Text = prixtotc.ToString();
            int a = get_maxcmd()+1;
            tnumcommandebase.Text = a.ToString();
            darttab = new DataTable();
            darttab.Clear();
            darttab.Columns.Add("code_art");
            darttab.Columns.Add("libelle_piece");
            darttab.Columns.Add("quantite_piece");
            darttab.Columns.Add("id_clt");
            darttab.Columns.Add("etat");
            darttab.Columns.Add("puv");
            darttab.Columns.Add("totvente");
            darttab.Columns.Add("id_commande");
            darttab.Columns.Add("remise");
            darttab.Columns.Add("ttva");
            darttab.Columns.Add("unit");
            darttab.Columns.Add("qterest");
           
        }
        public static int idpiece = 0, numligne=0;
      
        sql_gmao fun = new sql_gmao();
        
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
            if(mdesign.Text=="")
            {
                mdesign.Text="";
            }
             if(tpu.Text=="")
            {
                tpu.Text="0";
            }
             if(ttva.Text=="")
            {
                ttva.Text="0";
            }
             if(tquantit.Text=="")
            {
                tquantit.Text="0";
            }
             if(tunit.Text=="")
            {
                tunit.Text="";

            }
            if(tremise.Text=="")
            {
                tremise.Text="0";

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
           //MessageBox.Show("" + prixremis);
            Double pvtva = prixremis + ((prixremis * Convert.ToDouble(ttva.Text.Replace('.', ','))) / 100);
            //MessageBox.Show("" + pvtva);
           
               DataRowView rowView = (DataRowView)lookUpEdit2.GetSelectedDataRow();
               DataRow row = rowView.Row;
               DataRowView rowView1 = (DataRowView)lookUpEdit1.GetSelectedDataRow();
               DataRow row1 = rowView1.Row;
               string codep = row[0].ToString();
               string codeclt = row1[0].ToString();
               DataRow newpc = darttab.NewRow();
               newpc["code_art"]=codep;
               newpc["libelle_piece"]=mdesign.Text;
               newpc["quantite_piece"]=Convert.ToDouble(tquantit.Text.Replace('.', ','));
               newpc["id_clt"]=codeclt;
               newpc["etat"]="validée";
               newpc["puv"]= tpu.Text;
               newpc["totvente"] = pvtva.ToString();
               newpc["id_commande"]=tnumcommandebase.Text;
               newpc["remise"]=tremise.Text;
               newpc["ttva"]=ttva.Text;
               newpc["unit"]=tunit.Text;
               newpc["qterest"] = Convert.ToDouble(tquantit.Text.Replace('.', ','));
              darttab.Rows.Add(newpc);
              getalldatatable();
              updatesum();          
           }
           catch(Exception exception)
           {
               MessageBox.Show("veuillez remplir les champs vides");
           }
        }
        private int get_maxcmd()
        {
            int y = 0;
            int x = 0;
            DataTable dt = new DataTable();
            DataTable dtbl = new DataTable();
            DataTable data = new DataTable();
            dt = fun.getcountcmd("CommandeClient");

            if (dt.Rows.Count == 0)
            {
                data = fun.getcurrentvalue("CommandeClient");

                if (Convert.ToInt32(data.Rows[0][0]) == 0)
                {
                    fun.resetautoincrement("CommandeClient", 0);
                    y = Convert.ToInt32(data.Rows[0][0]);
                }
                else
                {
                    fun.resetautoincrement("CommandeClient", 0);
                    data = fun.getcurrentvalue("CommandeClient");
                    y = Convert.ToInt32(data.Rows[0][0]);
                }
            }
            else
            {
                dtbl = fun.get_max_Commande();
                x = Convert.ToInt32(dtbl.Rows[0][0]);
                fun.resetautoincrement("CommandeClient", x);
                data = fun.getcurrentvalue("CommandeClient");
                y = Convert.ToInt32(data.Rows[0][0]);

            }

            return y;

        }
       
       
        private void getalldatatable()
        {
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            gridControl1.DataSource = darttab;
            
            this.gridView1.Columns[0].Caption = "Code article";
            this.gridView1.Columns[1].Caption = "Désignation";
            this.gridView1.Columns[2].Caption = "Quantité";
            this.gridView1.Columns[3].Visible = false;
            this.gridView1.Columns[4].Visible = false;
            this.gridView1.Columns[5].Caption = "PUV";
            this.gridView1.Columns[6].Caption = "Prix ttc";
            this.gridView1.Columns[7].Visible = false;
            this.gridView1.Columns[8].Caption = "remise";
            this.gridView1.Columns[9].Caption = "TVA";
            this.gridView1.Columns[10].Caption = "unité";
            this.gridView1.Columns[11].Caption = "Qte restante";
            gridView1.OptionsView.ShowAutoFilterRow = true;
           
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            string etat = "en cours";
            string etat_fac = "en cours";
            string timbre = textBox5.Text;
            prixtotc=0;
            double.TryParse(timbre.Replace('.', ','), out prixtotc);
           DataTable dt=new DataTable();
          
           dt = fun.get_cltByDesign(lookUpEdit1.Text);
          string  id_clt=dt.Rows[0][0].ToString();
          if (gridView1.RowCount != 0)
          {

            
              for (int i = 0; i < gridView1.RowCount; i++)
              {
                  DataRow row = gridView1.GetDataRow(i);
                  fun.insert_piecee_commande1(row[0].ToString(), row[1].ToString(), Convert.ToDouble(row[2].ToString().Replace('.', ',')), row[10].ToString(), row[5].ToString(), row[8].ToString(), row[9].ToString(), id_clt.ToString(), tnumcommandebase.Text, row[6].ToString(),row[11].ToString());
               
              }
              fun.insert_into_Commandepasse(tnbcmd.Text, id_clt, etat, lookUpEdit1.Text, etat_fac, textBox1.Text, textBox5.Text);
              MessageBox.Show("la commande est ajoutée avec succées veuillez consulter la liste des commandes crée");
              this.Close();
              passerCommande lcmdclt = new passerCommande();

              lcmdclt.MdiParent = Form1.ActiveForm;
              lcmdclt.Show();
              lcmdclt.BringToFront();
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



        public void fillgrid()
        { 
          
        }

        private void passerCommande_Activated(object sender, EventArgs e)
        {
            clients();
            articles();
        }

        private void lookUpEdit2_EditValueChanged(object sender, EventArgs e)
        {
            DataRowView rowView = (DataRowView)lookUpEdit2.GetSelectedDataRow();
            DataRow row =  rowView.Row; 
            mdesign.Text = row[1].ToString();
            tunit.Text   = row[2].ToString();        
            tpu.Text     = row[8].ToString();
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
            int count = gridView1.DataRowCount;

            if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
            {
                numligne = gridView1.FocusedRowHandle;
                DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                // idpiece = Convert.ToInt32(row[0].ToString());
                lookUpEdit2.EditValue = row[0];
                mdesign.Text = row[1].ToString();
                tquantit.Text = row[2].ToString();
                tpu.Text = row[5].ToString();
                tunit.Text = row[10].ToString();
                ttva.Text = row[9].ToString();
                tremise.Text = row[8].ToString();
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            try
            {
                Double pvhorstva = Convert.ToDouble(tpu.Text.Replace('.', ',')) * Convert.ToDouble(tquantit.Text.Replace('.', ','));
                Double prixremis = pvhorstva - ((pvhorstva * Convert.ToDouble(tremise.Text.Replace('.', ','))) / 100);
                Double pvtva = prixremis + ((prixremis * Convert.ToDouble(ttva.Text.Replace('.', ','))) / 100);
                DataRowView rowView = (DataRowView)lookUpEdit2.GetSelectedDataRow();
                DataRow rowview = rowView.Row;
                DataRowView rowView1 = (DataRowView)lookUpEdit1.GetSelectedDataRow();
                DataRow row1 = rowView1.Row;
                string codep = rowview[0].ToString();
                string codeclt = row1[0].ToString();
                DataRow row = darttab.NewRow();
                row[0] = codep;
                row[1] = mdesign.Text;
                row[2] = Convert.ToDouble(tquantit.Text.Replace('.', ','));
                row[3] = codeclt;
                row[4] = "validée";
                row[5] = tpu.Text;
                row[6] = pvtva.ToString();
                row[7] = tnumcommandebase.Text;
                row[8] = tremise.Text;
                row[9] = ttva.Text;
                row[10] = tunit.Text;
                row[11] = Convert.ToDouble(tquantit.Text.Replace('.', ','));
                darttab.Rows[numligne].Delete();
                darttab.Rows.Add(row);
                getalldatatable();
                updatesum();
                mdesign.Text = "";
                tpu.Text = "0";
                ttva.Text = "0";
                tquantit.Text = "0";
                tunit.Text = "";
                tremise.Text = "0";
                //lookUpEdit1.EditValue = "choisir un client";
            }
            catch (Exception er)
            { }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            try
            {
                int count = gridView1.DataRowCount;
                if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
                {
                    DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                    darttab.Rows[numligne].Delete();
                    MessageBox.Show("la commande a été mise à jour");
                    getalldatatable();
                    updatesum();
                   
                }
                get_maxcmd();
            }
            catch (Exception exc)
            { }
        }
        public void updatesum()
        {
            prixtotc = 0;
            double.TryParse(textBox5.Text.Replace('.', ','), out prixtotc);
         
                for (int i = 0; i < gridView1.RowCount; i++)
                {
                  
                    DataRow row1 = gridView1.GetDataRow(i);
                    prixtotc += Convert.ToDouble(row1[6].ToString().Replace('.', ','));
                   
                }
                

                textBox1.Text = prixtotc.ToString();
            
        }

        private void passerCommande_Load(object sender, EventArgs e)
        {

        }
       

        

       
    }
}
    
