using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using RibbonSimplePad.Report;
using DevExpress.XtraReports.UI;
namespace RibbonSimplePad
{
    public partial class FactureVente : DevExpress.XtraEditors.XtraForm
    {
        DataTable darttab;
        sql_gmao fun = new sql_gmao();
        double prixtotc = 0;
        int numligne=0;
        List<int> _LidBl;
        int Idfact = 0;
        public FactureVente(int idfact)
        {
            InitializeComponent();
            Idfact = idfact;
            DataTable dt_fact = fun.get_etat_factvente(idfact);
            if (dt_fact.Rows.Count > 0)
            {
                lookUpEdit1.EditValue = dt_fact.Rows[0]["id_clt"].ToString();
                Tnumfact.Text = dt_fact.Rows[0]["numero_fact"].ToString();
                dateEdit1.Text = dt_fact.Rows[0]["date_ajout"].ToString();
                textEdit1.Text = dt_fact.Rows[0]["L_num_bl"].ToString();

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
                DataTable dt_peic_fact = fun.get_piece44(idfact);
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
                updatesum();
            }
        }
        public FactureVente(List<int> LidBl)
        {
            InitializeComponent();
            Tnumfact.Text = NumFact().ToString();
            _LidBl = LidBl;
            dateEdit1.Text = DateTime.Now.ToString();
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
            string list_bl = "";
           
            foreach (int idbl in LidBl)
            {
                DataTable dt = new DataTable();
                dt = fun.getbl(Convert.ToInt32(idbl.ToString()));
                lookUpEdit1.EditValue = dt.Rows[0][2].ToString();
               list_bl= list_bl.Trim();
                list_bl += dt.Rows[0]["numero_bl"].ToString().Trim() + "/";
                DataTable dt_bl = fun.get_Allprodbybl(idbl.ToString());
                foreach (DataRow row1 in dt_bl.Rows)
                {
                    //DataRow dr = darttab.NewRow();

                    //dr["code_art"] = row1["code_art"].ToString();
                    //dr["libelle_piece"] = row1["libelle_piece"].ToString();
                    //dr["quantite_piece"] = row1["quantite_piece"].ToString();
                    //dr["id_clt"] = row1["id_clt"].ToString();
                    //dr["etat"] = row1["etat"].ToString();
                    //dr["puv"] = row1["puv"].ToString();
                    //dr["totvente"] = row1["totvente"].ToString();
                    //dr["id_commande"] = row1["id_commande"].ToString();
                    //dr["remise"] = row1["remise"].ToString();
                    //dr["ttva"] = row1["ttva"].ToString();
                    //dr["unit"] = row1["unit"].ToString();
                    //darttab.Rows.Add(dr);
                    //getalldatatable();
                    
                        Boolean trouve = false;
                        for (int i = 0; i < darttab.Rows.Count; i++)
                        {

                            if (darttab.Rows[i]["code_art"].ToString().ToLower().Trim() + darttab.Rows[i]["libelle_piece"].ToString().ToLower().Trim() == row1["code_art"].ToString().ToLower().Trim() + row1["libelle_piece"].ToString().ToLower().Trim())
                            {
                                trouve = true;
                                darttab.Rows[i]["quantite_piece"] = double.Parse(darttab.Rows[i]["quantite_piece"].ToString()) + double.Parse(row1["quantite_piece"].ToString());
                                darttab.Rows[i]["totvente"] = double.Parse(darttab.Rows[i]["totvente"].ToString()) + double.Parse(row1["totvente"].ToString());
                            }
                            
                        }
                        if (!trouve)
                        {
                            
                                DataRow dr = darttab.NewRow();

                                dr["code_art"] = row1["code_art"].ToString();
                                dr["libelle_piece"] = row1["libelle_piece"].ToString();
                                dr["quantite_piece"] = row1["quantite_piece"].ToString();
                                dr["id_clt"] = row1["id_clt"].ToString();
                                dr["etat"] = row1["etat"].ToString();
                                dr["puv"] = row1["puv"].ToString();
                                dr["totvente"] = row1["totvente"].ToString();
                                dr["id_commande"] = row1["id_commande"].ToString();
                                dr["remise"] = row1["remise"].ToString();
                                dr["ttva"] = row1["ttva"].ToString();
                                dr["unit"] = row1["unit"].ToString();
                                darttab.Rows.Add(dr);

                            
                        }
                    
                    
                   



                }
            }
            list_bl = list_bl.Substring(0, list_bl.Count() - 1);
            textEdit1.Text = list_bl;
            getalldatatable();
            updatesum();
        }
        public void updateFacture()
        {

            string dd = dateEdit1.Text.Substring(0, 10);//.ToString();
            dd = dd.Substring(0, 10);
            DataRowView rowView1 = (DataRowView)lookUpEdit1.GetSelectedDataRow();
            DataRow row = rowView1.Row;
            int id_clt = int.Parse(row[0].ToString());
            string client = row[1].ToString();
            string tva = "";
            double prix = 0, tt_ht = 0;
            fun.delete_piece_fromfactvente(Idfact);
            for (int i = 0; i < darttab.Rows.Count; i++)
            {
                DataRow row1 = gridView1.GetDataRow(i);
                fun.insert_piecee_fact(row1["libelle_piece"].ToString(), row1["code_art"].ToString(), Convert.ToDouble(row1["quantite_piece"].ToString().Replace('.', ',')), id_clt.ToString(), row1["etat"].ToString(), Idfact, row1["puv"].ToString(), row1["totvente"].ToString(), Idfact.ToString(), row1["remise"].ToString(), row1["ttva"].ToString(), row1["unit"].ToString());
               
                if (row1["ttva"].ToString() == "")
                {
                    tva = "0";
                }
                else
                {
                    tva = row1["ttva"].ToString();
                }
                
                prix = double.Parse(row1["quantite_piece"].ToString().Replace('.', ',')) * double.Parse(row1["puv"].ToString().Replace('.', ','));
                prix = prix - ((prix * double.Parse(row1["remise"].ToString().Replace('.', ',')) / 100));
                tt_ht += prix;
            }
            fun.updatefacturevente(id_clt.ToString(), dd, "en cours", client, "0", prixtotc.ToString("0.000"), "0", tt_ht.ToString("0.000"), textBox5.Text, tva, "0", "0", Tnumfact.Text,textEdit1.Text, Idfact);
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
            //this.gridView1.Columns[11].Caption = "Qte restante";
            gridView1.OptionsView.ShowAutoFilterRow = true;

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


            textBox1.Text = prixtotc.ToString("0.000");

        }
        public int NumFact()
        {

            int y = 0;
            int x = 0;
            DataTable dt = new DataTable();
            DataTable dr = new DataTable();
            dr = fun.ListFact();
            if (dr.Rows.Count == 0)
            {
                return 1;
            }

            dt = fun.max_num_Factvent();
            if (dt.Rows.Count == 0)
            {
                return 1;
            }
            // string max = dt.Rows[0]["max"].ToString();
            if (dt.Rows[0]["max"].ToString() == "")
            {

                y = 1;

            }
            else
            {
                y = int.Parse(dt.Rows[0]["max"].ToString()) + 1;

            }

            return y;
        }
        private void FactureVente_Load(object sender, EventArgs e)
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
            textEdit2.Text = row[0].ToString();
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            int count = gridView1.DataRowCount;

            if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
            {
                numligne = gridView1.FocusedRowHandle;
                DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                // idpiece = Convert.ToInt32(row[0].ToString());
              //  lookUpEdit2.EditValue = row[0];
                mdesign.Text = row[1].ToString();
                tquantit.Text = row[2].ToString();
                tpu.Text = row[5].ToString();
                tunit.Text = row[10].ToString();
                ttva.Text = row[9].ToString();
                tremise.Text = row[8].ToString();
                textEdit2.Text = row[0].ToString();
            }
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
                //MessageBox.Show("" + prixremis);
                Double pvtva = prixremis + ((prixremis * Convert.ToDouble(ttva.Text.Replace('.', ','))) / 100);
                //MessageBox.Show("" + pvtva);

                DataRowView rowView = (DataRowView)lookUpEdit2.GetSelectedDataRow();
                DataRow row = rowView.Row;
                DataRowView rowView1 = (DataRowView)lookUpEdit1.GetSelectedDataRow();
                DataRow row1 = rowView1.Row;
                string codeclt = row1[0].ToString();
                string codep = row[0].ToString();
                
                DataRow newpc = darttab.NewRow();
                newpc["code_art"] = textEdit2.Text;
                newpc["libelle_piece"] = mdesign.Text;
                newpc["quantite_piece"] = Convert.ToDouble(tquantit.Text.Replace('.', ','));
                newpc["id_clt"] = codeclt;
                newpc["etat"] = "validée";
                newpc["puv"] = tpu.Text;
                newpc["totvente"] = pvtva.ToString("0.000");
                newpc["id_commande"] = Tnumfact.Text;
                newpc["remise"] = tremise.Text;
                newpc["ttva"] = ttva.Text;
                newpc["unit"] = tunit.Text;
                
                darttab.Rows.Add(newpc);
                getalldatatable();
                updatesum();
            }
            catch (Exception exception)
            {
                MessageBox.Show("veuillez remplir les champs vides");
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            try
            {
                Double pvhorstva = Convert.ToDouble(tpu.Text.Replace('.', ',')) * Convert.ToDouble(tquantit.Text.Replace('.', ','));
                Double prixremis = pvhorstva - ((pvhorstva * Convert.ToDouble(tremise.Text.Replace('.', ','))) / 100);
                Double pvtva = prixremis + ((prixremis * Convert.ToDouble(ttva.Text.Replace('.', ','))) / 100);
                //DataRowView rowView = (DataRowView)lookUpEdit2.GetSelectedDataRow();
               // DataRow rowview = rowView.Row;
                DataRowView rowView1 = (DataRowView)lookUpEdit1.GetSelectedDataRow();
                DataRow row1 = rowView1.Row;
               // string codep = rowview[0].ToString();
                string codeclt = row1[0].ToString();
                DataRow row = darttab.NewRow();
                row[0] = textEdit2.Text;
                row[1] = mdesign.Text;
                row[2] = Convert.ToDouble(tquantit.Text.Replace('.', ','));
                row[3] = codeclt;
                row[4] = "validée";
                row[5] = tpu.Text;
                row[6] = pvtva.ToString("0.000");
                row[7] = Tnumfact.Text;
                row[8] = tremise.Text;
                row[9] = ttva.Text;
                row[10] = tunit.Text;
               
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
                getalldatatable();
                updatesum();
            }
            catch (Exception er)
            {
                MessageBox.Show("vérifier les données entrer");
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
                    darttab.Rows[numligne].Delete();
                   
                    getalldatatable();
                    updatesum();

                }
               
            }
            catch (Exception exc)
            { }
        }
        private int get_maxfact()
        {
            int y = 0;
            int x = 0;
            DataTable dt = new DataTable();
            DataTable dtbl = new DataTable();
            DataTable data = new DataTable();
            dt = fun.getcountcmd("facturevente");

            if (dt.Rows.Count == 0)
            {
                data = fun.getcurrentvalue("facturevente");

                if (Convert.ToInt32(data.Rows[0][0]) == 0)
                {
                    fun.resetautoincrement("facturevente", 0);
                    y = Convert.ToInt32(data.Rows[0][0]);
                }
                else
                {
                    fun.resetautoincrement("facturevente", 0);
                    data = fun.getcurrentvalue("facturevente");
                    y = Convert.ToInt32(data.Rows[0][0]);
                }
            }
            else
            {
                dtbl = fun.get_max_Factvente();
                x = Convert.ToInt32(dtbl.Rows[0][0]);
                fun.resetautoincrement("facturevente", x);
                data = fun.getcurrentvalue("facturevente");
                y = Convert.ToInt32(data.Rows[0][0]);

            }

            return y;

        }
        private void simpleButton4_Click(object sender, EventArgs e)
        {
            try
            {
                if (Idfact != 0)
                {
                    if (dateEdit1.Text == "")
                    {
                        MessageBox.Show("Entrer la date du facture");
                    }
                    else
                    {
                        updateFacture();
                        this.Close();
                        FactureReport report = new FactureReport(Idfact);
                        report.ShowPreview();
                    }
                }
                else
                {
                    int id_factt = get_maxfact() + 1;
                    int num_f = 0;
                    int.TryParse(Tnumfact.Text, out num_f);
                    DataTable dt_fact_n = fun.GetFactByNum(num_f);
                    if (dt_fact_n.Rows.Count > 0)
                    {
                        XtraMessageBox.Show("Il existe une facture avec ce numéro");
                    }
                    else
                    {
                        if (dateEdit1.Text == "")
                        {
                            MessageBox.Show("Entrer la date du facture");
                        }
                        else
                        {

                            string dd = dateEdit1.Text.Substring(0, 10);//.ToString();
                            dd = dd.Substring(0, 10);
                            DataRowView rowView1 = (DataRowView)lookUpEdit1.GetSelectedDataRow();
                            DataRow row = rowView1.Row;
                            int id_clt = int.Parse(row[0].ToString());
                            string client = row[1].ToString();
                            string tva = "";
                            double prix = 0, tt_ht = 0;
                            for (int i = 0; i < darttab.Rows.Count; i++)
                            {
                                DataRow row1 = gridView1.GetDataRow(i);
                                fun.insert_piecee_fact(row1["libelle_piece"].ToString(), row1["code_art"].ToString(), Convert.ToDouble(row1["quantite_piece"].ToString().Replace('.', ',')), id_clt.ToString(), row1["etat"].ToString(), id_factt, row1["puv"].ToString(), row1["totvente"].ToString(), id_factt.ToString(), row1["remise"].ToString(), row1["ttva"].ToString(), row1["unit"].ToString());
                                if (row1["ttva"].ToString() == "")
                                {
                                    tva = "0";
                                }
                                else
                                {
                                    tva = row1["ttva"].ToString();
                                }
                                prix = double.Parse(row1["quantite_piece"].ToString().Replace('.', ',')) * double.Parse(row1["puv"].ToString().Replace('.', ','));
                                prix = prix - ((prix * double.Parse(row1["remise"].ToString().Replace('.', ',')) / 100));
                                tt_ht += prix;
                            }
                            fun.insertintofacturevente(id_clt.ToString(), dd, "en cours", client, "0", prixtotc.ToString("0.000"), "0", tt_ht.ToString("0.000"), textBox5.Text, tva, "0", "0", Tnumfact.Text, _LidBl.First(), textEdit1.Text);

                            //
                        }
                    }
                    this.Close();
                    FactureReport report = new FactureReport(id_factt);
                    report.ShowPreview();
                }
            }
            catch (Exception ed)
            {
                MessageBox.Show("Vérifier les données entrer");
            }
        }
    }
}