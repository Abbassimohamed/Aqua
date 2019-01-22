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
    public partial class Avoircommande : DevExpress.XtraEditors.XtraForm
    {
        public static Double prixtotc = 0.500, montantht = 0, montantttc = 0, montanttva=0,montantremis=0;
        public static DataTable darttab ;
        int numero_avoir = 0;
        int id = 0;
        public Avoircommande()
        {
            InitializeComponent();
            int a = get_maxcmd();
            tnumcommandebase.Text = a.ToString();
           
        }
        public Avoircommande(int numero,int id)
        {
            InitializeComponent();
            numero_avoir = numero;
            this.id = id;
           

        }
        public static int idpiece = 0, numligne=0;
        public static Double prixtot = 0;
       
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
           Double prixremis = pvhorstva - pvhorstva * Convert.ToDouble(tremise.Text.Replace('.', ',')) / 100;
           Double pvtva = prixremis + prixremis * Convert.ToDouble(ttva.Text.Replace('.', ',')) / 100;
            
            try
           {
               DataRowView rowView = (DataRowView)lookUpEdit2.GetSelectedDataRow();
               DataRow row = rowView.Row;
               DataRowView rowView1 = (DataRowView)lookUpEdit1.GetSelectedDataRow();
               DataRow row1 = rowView1.Row;
               
                   string codep = row[0].ToString();
                   string codeclt = row1[0].ToString();

                   DataRow newpc = darttab.NewRow();
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
                   newpc["id_produit"] = row[15].ToString();
                   
                   darttab.Rows.Add(newpc);
                   getalldatatable();
               
               
                 
                  

               
                  
             
              
                       
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
            DataTable dr = new DataTable();
            dr = fun.Listavoir();
            if (dr.Rows.Count == 0)
            {
                return 1;
            }

            dt = fun.max_num_avoir();
            if (dt.Rows.Count == 0)
            {
                return 1;
            }
            if (dt.Rows[0]["max"].ToString() == "NULL")
            {

                y = 1;
                
            }
            else
            {
                y = int.Parse(dt.Rows[0]["max"].ToString()) + 1;

            }

            return y;

        }           
        private void getalldatatable()
        {
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            gridControl1.DataSource = darttab;
            gridControl1.RefreshDataSource();
            this.gridView1.Columns[0].Caption = "Code article";
            this.gridView1.Columns[1].Caption = "Désignation";
            this.gridView1.Columns[2].Caption = "Quantité";
            this.gridView1.Columns[3].Visible = false;
            this.gridView1.Columns[4].Visible = false;
            this.gridView1.Columns[5].Caption = "PUV";
            this.gridView1.Columns[6].Caption = "Prix TTC";
            this.gridView1.Columns[7].Visible = false;
            this.gridView1.Columns[8].Caption = "remise";
            this.gridView1.Columns[9].Caption = "TVA";
            this.gridView1.Columns[10].Caption = "unité";
            this.gridView1.Columns[11].Visible = false;
           
            
            gridView1.OptionsView.ShowAutoFilterRow = true;
           
        }
        private void simpleButton4_Click(object sender, EventArgs e)
        {
            montantht = 0;
            montantttc = 0;
            string etat = "en cours";
            string etat_fac = "en cours";
            string timbre = textEdit1.Text;
            if (lookUpEdit1.Text.Trim() == "Choisir un client")
            {
                MessageBox.Show("Choisir un client svp !!!");
            }
            else
            {
                if (tnumcommandebase.Text == "")
                {
                    MessageBox.Show("Entrer le numéro d'avoir svp !!!");
                }
                else
                {
                    if (date.Text == "")
                    {
                        MessageBox.Show("Entrer la date svp !!!");
                    }
                    else
                    {
                        DataTable aux = fun.selectfromavoirsByNum(tnumcommandebase.Text);

                        if (CB_Fact.Text == "")
                        {
                            MessageBox.Show("Entrer le numero de la facture svp !!!");
                        }
                        else
                        {
                            DataTable dt = new DataTable();

                            dt = fun.get_cltByDesign(lookUpEdit1.Text);
                            string id_clt = dt.Rows[0][0].ToString();
                            if (numero_avoir != 0)
                            {
                                fun.delete_piece_avoir(numero_avoir);
                                if (gridView1.RowCount != 0)
                                {
                                    DataRowView rowView = (DataRowView)lookUpEdit1.GetSelectedDataRow();
                                    DataRow rowv = rowView.Row;

                                    for (int i = 0; i < gridView1.RowCount; i++)
                                    {

                                        DataRow row = gridView1.GetDataRow(i);


                                        // Double tva = Convert.ToDouble(row[6].ToString().Replace('.', ',')) * Convert.ToDouble(row[9].ToString()) / 100;
                                        //  montanttva += tva;  
                                        montantttc += Convert.ToDouble(row[6].ToString().Replace('.', ','));// +tva;
                                        fun.insert_piecee_avoir(row[0].ToString(), row[1].ToString(), row[2].ToString().Replace('.', ','), row[10].ToString(), row[5].ToString(), row[8].ToString(), row[9].ToString(), id_clt.ToString(), Convert.ToInt32(tnumcommandebase.Text), row[6].ToString(), row[11].ToString());
                                        //mse a jour stok
                                        //stck
                                        // fun.update_sousstock_avoir2(double.Parse(row[2].ToString().Replace('.', ',')), row[11].ToString());
                                        //string vv = row[12].ToString();

                                    }
                                    double tmbr = 0;
                                    double.TryParse(timbre.Replace('.', ','), out tmbr);
                                    montantttc += tmbr;
                                    fun.update_avoir(CB_Fact.Text, id, etat, date.Text.Substring(0, 10), rowv[0].ToString(), rowv[1].ToString(), "0", montantttc.ToString(), timbre, tnumcommandebase.Text, "0", "0");
                                    MessageBox.Show("le bon d'avoir est modifier avec succées veuillez consulter la liste des avoir");
                                    this.Close();
                                }
                            }
                            else
                            {
                                if (aux.Rows.Count > 0)
                                {
                                    MessageBox.Show("Il existe un bon de retour avec ce numéro");
                                }
                                else
                                {
                                    if (gridView1.RowCount != 0)
                                    {
                                        DataRowView rowView = (DataRowView)lookUpEdit1.GetSelectedDataRow();
                                        DataRow rowv = rowView.Row;

                                        for (int i = 0; i < gridView1.RowCount; i++)
                                        {

                                            DataRow row = gridView1.GetDataRow(i);


                                            // Double tva = Convert.ToDouble(row[6].ToString().Replace('.', ',')) * Convert.ToDouble(row[9].ToString()) / 100;
                                            //  montanttva += tva;  
                                            montantttc += Convert.ToDouble(row[6].ToString().Replace('.', ','));// +tva;
                                            fun.insert_piecee_avoir(row[0].ToString(), row[1].ToString(), row[2].ToString().Replace('.', ','), row[10].ToString(), row[5].ToString(), row[8].ToString(), row[9].ToString(), id_clt.ToString(), Convert.ToInt32(tnumcommandebase.Text), row[6].ToString(), row[11].ToString());
                                            //mse a jour stok
                                            //stck
                                            // fun.update_sousstock_avoir2(double.Parse(row[2].ToString().Replace('.', ',')), row[11].ToString());
                                            //string vv = row[12].ToString();

                                        }

                                        fun.insert_into_avoir(CB_Fact.Text, etat, date.Text.Substring(0, 10), rowv[0].ToString(), rowv[1].ToString(), "0", montantttc.ToString(), timbre, tnumcommandebase.Text, "0", "0");
                                        MessageBox.Show("le bon d'avoir est ajoutée avec succées veuillez consulter la liste des avoir");
                                        this.Close();
                                        Avoircommande lcmdclt = new Avoircommande();

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
                    }
                }
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
            double puv = 0;
            double.TryParse(row[8].ToString().Replace('.', ','),out puv);// -((double.Parse(row[8].ToString().Replace('.', ',')) * double.Parse(row[19].ToString().Replace('.', ','))) / 100);

           // prixtot = puv;
           tpu.Text = puv.ToString();
            //ttva.Text = row[19].ToString();
           
        }
       
        private void tpu_Validated(object sender, EventArgs e)
        {
            try
            {
                DataRowView rowView = (DataRowView)lookUpEdit2.GetSelectedDataRow();
                DataRow row = rowView.Row;
                Double prixv = Convert.ToDouble(tpu.Text.Replace('.', ','));
                Double prixachat = Convert.ToDouble(row[7].ToString().Replace('.', ','));
                if (prixv <= prixachat)
                {
                    XtraMessageBox.Show("le frais d'achat est de " + row[7].ToString());
                }
            }
            catch (Exception except)
            { }
        }
        private void gridControl1_Click(object sender, EventArgs e)
        {
           
            int count = gridView1.DataRowCount;

            if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
            {
                //simpleButton1.Enabled = false;
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
                Double prixremis = pvhorstva - pvhorstva * Convert.ToDouble(tremise.Text.Replace('.', ',')) / 100;
                Double pvtva = prixremis+prixremis * Convert.ToDouble(ttva.Text.Replace('.', ',')) / 100;
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
                row["id_produit"] = rowview[15].ToString();

                darttab.Rows[numligne].Delete();
                darttab.Rows.Add(row);
                getalldatatable();

                mdesign.Text = "";
                tpu.Text = "0";
                ttva.Text = "0";
                tquantit.Text = "0";
                tunit.Text = "";
                tremise.Text = "0";
                //lookUpEdit1.EditValue = "choisir un client";
            }
            catch (Exception rr)
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
                   
                   
                }
                get_maxcmd();
            }
            catch (Exception exc)
            { }
        }
      
        private void passerCommande_Load(object sender, EventArgs e)
        {
            lookUpEdit1.Text = "";
            lookUpEdit2.Text = "";
            

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
            darttab.Columns.Add("id_produit");
           
            DataTable dt_num_fact = fun.GetListNumFact();
            foreach (DataRow item in dt_num_fact.Rows)
            {
                CB_Fact.Properties.Items.Add(item[0].ToString());
            }
            if (numero_avoir != 0)
            {
                DataTable dt_avoir = fun.selectfromavoirsByNum(numero_avoir);
                if (dt_avoir.Rows.Count > 0)
                {
                    tnumcommandebase.Text = dt_avoir.Rows[0]["numero"].ToString();
                    date.Text = dt_avoir.Rows[0]["date_ajout"].ToString();
                    textEdit1.Text = dt_avoir.Rows[0]["timbre"].ToString();
                    lookUpEdit1.EditValue = dt_avoir.Rows[0]["id_clt"].ToString();
                    CB_Fact.Text = dt_avoir.Rows[0]["numero_fact"].ToString();
                
                }
                DataTable dt_piece_avoir = fun.selectPieceAvoirsByNum(numero_avoir);

                foreach (DataRow item in dt_piece_avoir.Rows)
                {
                    DataRow newpc = darttab.NewRow();
                    newpc["code_art"] = item["code_art"].ToString();
                    newpc["libelle_piece"] = item["libelle_piece"].ToString();
                    newpc["quantite_piece"] = item["quantite_piece"].ToString();
                    newpc["id_clt"] = item["id_clt"].ToString();
                    newpc["etat"] = item["etat"].ToString();
                    newpc["puv"] = item["puv"].ToString();
                    newpc["totvente"] = item["totvente"].ToString();
                    newpc["id_commande"] = item["id_commande"].ToString();
                    newpc["remise"] = item["remise"].ToString();
                    newpc["ttva"] = item["ttva"].ToString();
                    newpc["unit"] = item["unit"].ToString();
                    newpc["id_produit"] = item["id_prod"].ToString();

                    darttab.Rows.Add(newpc);
                    getalldatatable();
                }
            }
        }

       
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void lookUpEdit3_EditValueChanged(object sender, EventArgs e)
        {
           // DataRowView rowV = (DataRowView)lookUpEdit3.GetSelectedDataRow();
           // DataRow rowv = rowV.Row;
           //// codeverre = rowv[0].ToString();
           // Double price = 0;
           // double.TryParse(rowv[8].ToString().Replace('.', ',') ,out price);
           // tpu.Text = price.ToString();
        }

        private void Avoircommande_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            this.Parent = null;
            e.Cancel = true;
        }

        private void CB_Fact_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void CB_Fact_TextChanged(object sender, EventArgs e)
        {
            int numerofact = 0;
            int.TryParse(CB_Fact.Text, out numerofact);
            if (numerofact != 0)
            {
                DataTable dt_fact = fun.GetFactByNum(numerofact);
                if (dt_fact.Rows.Count > 0)
                {
                    lookUpEdit1.EditValue =int.Parse(dt_fact.Rows[0]["id_clt"].ToString());
                    if (id == 0)
                    {

                        DataTable dt_peic_fact = fun.get_piece44(int.Parse(dt_fact.Rows[0][0].ToString()));
                        darttab.Rows.Clear();
                        foreach (DataRow row1 in dt_peic_fact.Rows)
                        {

                            DataRow dr = darttab.NewRow();

                            dr["code_art"] = row1["code_piece_u"].ToString();
                            dr["libelle_piece"] = row1["libelle_piece_u"].ToString();
                            dr["quantite_piece"] = row1["quantite_piece_u"].ToString();
                            dr["id_clt"] = row1["id_clt"].ToString();
                            dr["etat"] = row1["etat"].ToString();
                            dr["puv"] = row1["puv"].ToString();
                            dr["totvente"] = row1["pv"].ToString();
                            dr["id_commande"] = row1["id_commande"].ToString();
                            dr["remise"] = row1["remise"].ToString();
                            dr["ttva"] = row1["tva"].ToString();
                            dr["unit"] = row1["etat2"].ToString();
                            darttab.Rows.Add(dr);
                        }



                        getalldatatable();

                    }
                    
                }
            }

        }
        

       
    }
}
    
