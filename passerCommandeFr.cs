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
    public partial class passerCommandeFr : DevExpress.XtraEditors.XtraForm
    {
        public static Double prixtotc=0.500;
        public static DataTable darttab ;
        int id_cmd = 0;
        public passerCommandeFr(int IdCmd)
        {
            InitializeComponent();
            id_cmd=IdCmd;
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
            lookUpEdit1.EditValue = "";
            DataTable dt_cmdfr = fun.affiche_cmdFR_infos(id_cmd);
            if (dt_cmdfr.Rows.Count > 0)
            {
                textEdit1.Text = dt_cmdfr.Rows[0]["ref_cmd"].ToString();
                textEdit2.Text = dt_cmdfr.Rows[0]["ref_pf"].ToString();
                textEdit3.Text = dt_cmdfr.Rows[0]["mode_payement"].ToString();
                textEdit4.Text = dt_cmdfr.Rows[0]["contact"].ToString();
                textEdit5.Text = dt_cmdfr.Rows[0]["mode_liv"].ToString();
                textEdit6.Text = dt_cmdfr.Rows[0]["delai_livraison"].ToString();
                textEdit7.Text = dt_cmdfr.Rows[0]["delai_payement"].ToString();
            }
        }
        public passerCommandeFr()
        {
            InitializeComponent();
           
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
            Double pvtva = prixremis + ((prixremis * Convert.ToDouble(ttva.Text.Replace('.', ','))) / 100);
            try
           {
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
            dt = fun.getcountcmd("Commandefr");

            if (dt.Rows.Count == 0)
            {
                data = fun.getcurrentvalue("Commandefr");

                if (Convert.ToInt32(data.Rows[0][0]) == 0)
                {
                    fun.resetautoincrement("Commandefr", 0);
                    y = Convert.ToInt32(data.Rows[0][0]);
                }
                else
                {
                    fun.resetautoincrement("Commandefr", 0);
                    data = fun.getcurrentvalue("Commandefr");
                    y = Convert.ToInt32(data.Rows[0][0]);
                }
            }
            else
            {
                dtbl = fun.get_max_Commandefr();
                x = Convert.ToInt32(dtbl.Rows[0][0]);
                fun.resetautoincrement("Commandefr", x);
                data = fun.getcurrentvalue("Commandefr");
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
            DataRowView rowView1 = (DataRowView)lookUpEdit1.GetSelectedDataRow();
            if (rowView1 == null)
            {
                XtraMessageBox.Show("Choisir le fournisseur");
            }
            else
            {
                if (id_cmd != 0)
                {
                    UpdateCmdFournisseur();
                }
                else
                {
                    string etat = "en cours";
                    string etat_fac = "en cours";
                    string timbre = textBox5.Text;
                    prixtotc = 0;
                    double.TryParse(textBox5.Text.Replace('.', ','), out prixtotc);
                    DataTable dt = new DataTable();

                    dt = fun.get_FeurByCode(lookUpEdit1.EditValue.ToString());
                    string id_clt = dt.Rows[0][0].ToString();
                    if (gridView1.RowCount != 0)
                    {


                        for (int i = 0; i < gridView1.RowCount; i++)
                        {
                            DataRow row = gridView1.GetDataRow(i);
                            fun.insert_piecee_commandefr(row[0].ToString(), row[1].ToString(), Convert.ToDouble(row[2].ToString().Replace('.', ',')), row[10].ToString(), row[5].ToString(), row[8].ToString(), row[9].ToString(), id_clt.ToString(), tnumcommandebase.Text, row[6].ToString(), row[11].ToString());

                        }
                        fun.insert_into_Commandefr(id_clt, etat, lookUpEdit1.Text, etat_fac, textBox1.Text, timbre, textEdit1.Text, textEdit2.Text, textEdit3.Text, textEdit4.Text, textEdit5.Text, textEdit6.Text, textEdit7.Text);
                        MessageBox.Show("la commande est ajoutée avec succées veuillez consulter la liste des commandes crée");
                        this.Close();
                        passerCommandeFr lcmdclt = new passerCommandeFr();

                        lcmdclt.MdiParent = Form1.ActiveForm;
                        lcmdclt.Show();
                        lcmdclt.BringToFront();
                    }
                    else
                    {
                        MessageBox.Show("veuillez insérer les lignes de commande");
                    }
                }
            }
        }


        private void clients()
        {
            //get All stock
            lookUpEdit1.Properties.DataSource = null;
            DataTable Allclients = new DataTable();
            Allclients = fun.get_frs();
            for (int i = 0; i < Allclients.Rows.Count; i++)
            {
                lookUpEdit1.Properties.ValueMember = "code_feur";
                lookUpEdit1.Properties.DisplayMember = "raison_soc";
                lookUpEdit1.Properties.DataSource = Allclients;
                lookUpEdit1.Properties.PopulateColumns();
                lookUpEdit1.Properties.Columns["code_feur"].Caption = "Code fr";
                lookUpEdit1.Properties.Columns["raison_soc"].Caption = "Raison sociale";
                lookUpEdit1.Properties.Columns["responsbale"].Caption = "Responsable";
                lookUpEdit1.Properties.Columns["gsm_feur"].Visible = false;
                lookUpEdit1.Properties.Columns["tel_feur"].Visible = false;
                lookUpEdit1.Properties.Columns["fax_feur"].Visible = false;
                lookUpEdit1.Properties.Columns["adresse_feur"].Visible = false;
                lookUpEdit1.Properties.Columns["cp_feur"].Visible = false;
                lookUpEdit1.Properties.Columns["ville_feur"].Visible = false;
                lookUpEdit1.Properties.Columns["email_feur"].Visible = false;
                lookUpEdit1.Properties.Columns["site_feur"].Visible = false;
                lookUpEdit1.Properties.Columns["tva_feur"].Visible = false;
                lookUpEdit1.Properties.Columns["forme_juriduque"].Visible = false;
                lookUpEdit1.Properties.Columns["mode_pay"].Visible = false;
            }
        }
        private void articles()
        {
            //get All articles
            lookUpEdit2.Properties.DataSource = null;
            DataTable Allarticle = new DataTable();
            Allarticle = fun.get_allstock();
            for (int i = 0; i < Allarticle.Rows.Count; i++)
            {
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
        }



        public void fillgrid()
        { 
          
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
                //lookUpEdit1.EditValue = "choisir un fournisseur";
            }
            catch (Exception er) { }
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
            prixtotc = double.Parse(textBox5.Text.Replace('.',','));
         
                for (int i = 0; i < gridView1.RowCount; i++)
                {
                  
                    DataRow row1 = gridView1.GetDataRow(i);
                    prixtotc += Convert.ToDouble(row1[6].ToString().Replace('.', ','));
                   
                }
                

                textBox1.Text = prixtotc.ToString();
            
        }

        private void passerCommandeFr_Activated_1(object sender, EventArgs e)
        {
             clients();
            articles();
        }

        private void passerCommandeFr_Load(object sender, EventArgs e)
        {
            if (id_cmd != 0)
            {
                ListProduitCmd();
            }
        }

        public void UpdateCmdFournisseur()
        {
            DataRowView rowView1 = (DataRowView)lookUpEdit1.GetSelectedDataRow();
            if(rowView1==null)
            {
                XtraMessageBox.Show("Choisir le fournisseur SVP");
            }
            else
            {
                DataRow row1 = rowView1.Row;
                string fr=row1[0].ToString();
                fun.update_clt_cmd_fr(id_cmd, fr, textEdit1.Text, textEdit2.Text, textEdit3.Text, textEdit4.Text, textEdit5.Text, textEdit6.Text, textEdit7.Text);

                fun.delete_piece_from_cde_fr(id_cmd);

                if (gridView1.RowCount != 0)
                {


                    for (int i = 0; i < gridView1.RowCount; i++)
                    {
                        DataRow row = gridView1.GetDataRow(i);
                        fun.insert_piecee_commandefr(row[0].ToString(), row[1].ToString(), Convert.ToDouble(row[2].ToString().Replace('.', ',')), row[10].ToString(), row[5].ToString(), row[8].ToString(), row[9].ToString(), fr.ToString(), id_cmd.ToString(), row[6].ToString(), row[11].ToString());

                    }
                    
                    MessageBox.Show("la commande est ajoutée avec succées veuillez consulter la liste des commandes crée");
                    this.Close();
                }

            }
        }
        public void ListProduitCmd()
        {
            DataTable bb = new DataTable();
            bb = fun.get_etat_cmdfr(id_cmd);
            if (bb.Rows.Count > 0)
            {
                lookUpEdit1.EditValue = bb.Rows[0]["id_clt"].ToString();
            }
            
            tnumcommandebase.Text = id_cmd.ToString();

            DataTable dt = new DataTable();
            dt = fun.get_list_piece_from_cdefr(id_cmd);


            foreach (DataRow drw in dt.Rows)
            {
                DataRow newpc = darttab.NewRow();
                newpc["code_art"] = drw[1].ToString();
                newpc["libelle_piece"] =  drw[2].ToString();
                newpc["quantite_piece"] =  drw[3].ToString();
                newpc["id_clt"] =  drw[4].ToString();
                newpc["etat"] =  drw[5].ToString();
                newpc["puv"] =  drw[6].ToString();
                newpc["totvente"] =  drw[7].ToString();
                newpc["id_commande"] =  drw[8].ToString();
                newpc["remise"] =  drw[9].ToString();
                newpc["ttva"] =  drw[10].ToString();
                newpc["unit"] = drw[11].ToString();
                newpc["qterest"] =  drw[12].ToString();
                darttab.Rows.Add(newpc);
            }
                
                getalldatatable();
                updatesum();
            
        }
        

       
    }
}
    
