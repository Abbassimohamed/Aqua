using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraBars;
namespace RibbonSimplePad
{
    public partial class Factureachat : DevExpress.XtraEditors.XtraForm
    {
        public static DataTable dartt;
        public  static Double prixtotc,prix_ht,tva=0;
        public static Double test;

             public static int numbl,id_clt=0 ;
             public Factureachat()
        {
            InitializeComponent();
            dartt = new DataTable();
            dartt.Clear();
           
            dartt.Columns.Add("code_art");
            dartt.Columns.Add("libelle_piece");
            dartt.Columns.Add("quantite_piece");
            dartt.Columns.Add("id_clt");
            dartt.Columns.Add("etat");
            dartt.Columns.Add("puv");
            dartt.Columns.Add("totvente");
            dartt.Columns.Add("id_factpf");
            dartt.Columns.Add("remise");
            dartt.Columns.Add("ttva");
            dartt.Columns.Add("unit");
            
            textBox1.Text="0,500";
            tnumcommandebase.Text = (get_maxbl()+1).ToString();
                    
        }
        public static int idpiece = 0, idrow=0;
        sql_gmao fun = new sql_gmao();


        private int get_maxbl()
        {
            int y = 0;
            int x = 0;
            DataTable dt = new DataTable();
            DataTable dtbl = new DataTable();
            DataTable data = new DataTable();
            dt = fun.getcountcmd("factureachat");

            if (dt.Rows.Count == 0)
            {
                data = fun.getcurrentvalue("factureachat");
               
                if (Convert.ToInt32(data.Rows[0][0]) == 0)
                {
                    fun.resetautoincrement("factureachat", 0);
                    y = Convert.ToInt32(data.Rows[0][0]);
                }
                else
                {
                    fun.resetautoincrement("factureachat", 0);
                    data = fun.getcurrentvalue("factureachat");
                    y = Convert.ToInt32(data.Rows[0][0]);
                }
            }
            else
            {
                dtbl = fun.get_max_FactPf();
                x = Convert.ToInt32(dtbl.Rows[0][0]);
                fun.resetautoincrement("factureachat", x);
                data = fun.getcurrentvalue("factureachat");
                y = Convert.ToInt32(data.Rows[0][0]);
               
            }
           
            return y;

        }
       
        private void simpleButton4_Click(object sender, EventArgs e)
        {
            
                string etat ="validée";
                string etatcmd = "";
                DataTable dt = new DataTable();
                dt = fun.get_cltByDesign(lookUpEdit1.Text);
                id_clt = Convert.ToInt32(dt.Rows[0][0]);
                test = 0;
                 string  timbre="0.500";
                for (int i = 0; i < gridView1.DataRowCount;i++)
                {
                    DataRow row = gridView1.GetDataRow(i);
                    /*
                    DataTable dat = new DataTable();
                    dat = fun.get_piececmdbynump(Convert.ToInt32(row[0]));                  
                    Double quantiterestante = Convert.ToDouble(dat.Rows[0][12].ToString());
                    Double qterst = Convert.ToDouble(dat.Rows[0][12].ToString()) - Convert.ToDouble(row[3].ToString());
                    Double prremis = Convert.ToDouble(dat.Rows[0][7].ToString()) - Convert.ToDouble(row[7].ToString());
                    prixtotc+=prremis;
                    fun.update_qterestcommande(qterst.ToString(),row[0].ToString());
                    
                    */
                    tva=Convert.ToDouble( row[9].ToString().Replace('.',','));
                    fun.insert_piecee_factachat(row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(), row[4].ToString(), row[5].ToString(), row[6].ToString(), tnumcommandebase.Text, row[8].ToString(), row[9].ToString(), row[10].ToString());
                
                }

                    fun.insert_into_factachat(id_clt.ToString(), etat, lookUpEdit1.Text, tnbcmd.Text, prixtotc.ToString(), timbre, tva.ToString());   
                    numbl =Convert.ToInt32( tnumcommandebase.Text);
                    MessageBox.Show("Facture proformat ajoutée avec succées");
                    this.Close();        
                    facturepf factpf = new facturepf();
                    factpf.MdiParent = Form1.ActiveForm;
                    factpf.Show();
                    factpf.BringToFront();
                    this.Close();
                
        }


        private void clients()
        { //get All stock
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
            //get All client
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

        private void passerCommande_Activated(object sender, EventArgs e)
        {
            clients();
            articles();
        }

        private void lookUpEdit2_EditValueChanged(object sender, EventArgs e)
        {
            DataRowView rowView = (DataRowView)lookUpEdit2.GetSelectedDataRow();
            DataRow row = rowView.Row;

            mdesign.Text = row[1].ToString();
            tunit.Text = row[2].ToString();
            tpu.Text = row[8].ToString();
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
            {

            }
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            try
            {
                 int count = gridView1.DataRowCount;
                 if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
                 {
                     idrow = gridView1.FocusedRowHandle;
                    
                     DataRow row = gridView1.GetDataRow(idrow);
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
            catch (Exception except)
            { }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {

            Double pvhorstva = Convert.ToDouble(tpu.Text.Replace('.', ',')) * Convert.ToDouble(tquantit.Text.Replace('.', ','));
            Double pvtva = pvhorstva + pvhorstva * Convert.ToDouble(ttva.Text.Replace('.', ',')) / 100;
            Double prixremis = pvtva - pvtva * Convert.ToDouble(tremise.Text.Replace('.', ',')) / 100;


            DataRowView rowView1 = (DataRowView)lookUpEdit1.GetSelectedDataRow();
            DataRow row1 = rowView1.Row;
            DataRowView rowView2 = (DataRowView)lookUpEdit2.GetSelectedDataRow();
            DataRow row2 = rowView2.Row;
            string codeclt = row1[0].ToString();
            DataRow row = dartt.NewRow();
            row[0] = row1[0].ToString();
            row[1] = mdesign.Text;
            row[2] = Convert.ToDouble(tquantit.Text.Replace('.', ','));
            row[3] = codeclt;
            row[4] = "validée";
            row[5] = tpu.Text;
            row[6] = prixremis.ToString();
            row[7] = tnumcommandebase.Text;
            row[8] = tremise.Text;
            row[9] = ttva.Text;
            row[10] = tunit.Text;
           
           
            dartt.Rows[idrow].Delete();

            dartt.Rows.Add(row);
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            gridControl1.DataSource = dartt;
           
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
           
            gridView1.OptionsView.ShowAutoFilterRow = true;
            mdesign.Text = "";
            tpu.Text = "0";
            ttva.Text = "0";
            tquantit.Text = "0";
            tunit.Text = "";
            tremise.Text = "0";
            lookUpEdit1.EditValue = "choisir un client";
            updatesum();
         
            }
        
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            try
            {
                int count = gridView1.DataRowCount;
                if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
                {
                    DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                    dartt.Rows[idrow].Delete();
                    MessageBox.Show("la commande a été mise à jour");
                    gridControl1.DataSource = null;
                    gridView1.Columns.Clear();
                    gridControl1.DataSource = dartt;

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

                    gridView1.OptionsView.ShowAutoFilterRow = true;
                    updatesum();
                }
            }
            catch (Exception exc)
            { }
        }

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
                newpc["id_factpf"] = tnumcommandebase.Text;
                newpc["remise"] = tremise.Text;
                newpc["ttva"] = ttva.Text;
                newpc["unit"] = tunit.Text;
                dartt.Rows.Add(newpc);
                getalldatatable();
                updatesum();
            }
            catch (Exception except)
            {
                MessageBox.Show("vérifier les valeurs entrées");
            }
        }

        private void getalldatatable()
        {
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            gridControl1.DataSource = dartt;

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
            gridView1.OptionsView.ShowAutoFilterRow = true; 
        }

        public void updatesum()
        {
            try
            {
                prixtotc = double.Parse(textBox5.Text.Replace('.',','));
                prix_ht = 0;
                prixtotc = 0;
                for (int i = 0; i < gridView1.RowCount; i++)
                {

                    DataRow row1 = gridView1.GetDataRow(i);
                    prixtotc += Convert.ToDouble(row1[6].ToString().Replace('.', ','));
                    prix_ht += Convert.ToDouble(row1[2].ToString().Replace('.', ',')) * Convert.ToDouble(row1[5].ToString().Replace('.', ','));
                }

                //MessageBox.Show(prix_ht.ToString());
                textBox1.Text = prixtotc.ToString();
            }
            catch (Exception xce)
            { 
            }
        }

        private void Factureachat_Load(object sender, EventArgs e)
        {

        }
       
      
    }
}
