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
using RibbonSimplePad.Report;
using DevExpress.XtraReports.UI;

namespace RibbonSimplePad
{
    public partial class CommandeBL : DevExpress.XtraEditors.XtraForm
    {
        public static DataTable dartt;
        public static Double prixtotc;
        public static int numbl;
        public CommandeBL()
        {
            InitializeComponent();
            lookUpEdit1.Text = "";
            lookUpEdit2.Text = "";
            string x = liste_cde_client.id_commande.ToString();
            DataTable dattab = new DataTable();
            dattab = fun.get_cmdcltbyid(liste_cde_client.id_commande);

            tnumbl.Text =  NumeoBl().ToString();
            lookUpEdit1.EditValue = liste_cde_client.id_clt;
            tnbcmd.Text = liste_cde_client.id_cmd;
            getallcmd();
            
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
            dt = fun.getcountcmd("bon_livraison");

            if (dt.Rows.Count == 0)
            {
                data = fun.getcurrentvalue("bon_livraison");
               
                if (Convert.ToInt32(data.Rows[0][0]) == 0)
                {
                    fun.resetautoincrement("bon_livraison", 0);
                    y = Convert.ToInt32(data.Rows[0][0]);
                }
                else
                {
                    fun.resetautoincrement("bon_livraison", 0);
                    data = fun.getcurrentvalue("bon_livraison");
                    y = Convert.ToInt32(data.Rows[0][0]);
                }
            }
            else
            {
                dtbl = fun.get_max_BL();
                x = Convert.ToInt32(dtbl.Rows[0][0]);
                fun.resetautoincrement("bon_livraison", x);
                data = fun.getcurrentvalue("bon_livraison");
                y = Convert.ToInt32(data.Rows[0][0]);
               
            }
           
            return y;

        }
       

       
        private void getallcmd()
        {
            DataTable dt = new DataTable();
            dt = fun.get_Allprodbycmdnonull(liste_cde_client.id_commande.ToString());
           
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
            foreach (DataRow row in dt.Rows)
            {
                    DataRow newpc = dartt.NewRow();
                    newpc["id_piece"] = row[0].ToString();
                    newpc["code_art"] = row[1].ToString();
                    newpc["libelle_piece"] = row[2].ToString();
                    newpc["quantite_piece"] = Convert.ToDouble(row[12].ToString().Replace('.', ','));
                    newpc["id_clt"] = row[4].ToString();
                    newpc["etat"] = "validée";
                    newpc["puv"] = row[6].ToString();
                    double remise = 0, tva = 0, puv = 0,ttv=0,qt=0;
                    remise = double.Parse(row[9].ToString().Replace('.', ','));
                    tva = double.Parse(row[10].ToString().Replace('.', ','));
                    puv = double.Parse(row[6].ToString().Replace('.', ','));
                    qt = Convert.ToDouble(row[12].ToString().Replace('.', ','));
                    ttv = qt * puv;
                    ttv = ttv - ((ttv * remise) / 100);
                    ttv = ttv + ((ttv * tva) / 100);

                    newpc["totvente"] = ttv.ToString("0.000") ;
                    newpc["id_commande"] = row[8].ToString();
                    newpc["remise"] = row[9].ToString();
                    newpc["ttva"] = row[10].ToString();
                    newpc["unit"] = row[11].ToString();

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
            gridView1.OptionsView.ShowAutoFilterRow = true;
               
               
           
        }
        public Boolean verifierQuantite()
        {
            Boolean result = true;
            string msg = "Quantite demander supérieur au quantite du produit :\n";
            for (int i = 0; i < gridView1.DataRowCount; i++)
            {
                DataRow row = gridView1.GetDataRow(i);
                DataTable dt_prod = fun.GetProdByQtRest(row[1].ToString(), double.Parse(row[3].ToString()));
                if (dt_prod.Rows.Count > 0)
                {
                    result = false;
                    msg += row[0].ToString() + " Quantité disponible :" + dt_prod.Rows[0][0] + " -- Quantité demander : " + row[3].ToString() + "\n";
                }
            }
            if (result == false)
            {
                MessageBox.Show(msg);
            }
            return result;
        }
        private void simpleButton4_Click(object sender, EventArgs e)
        {
            if (verifierQuantite())
            {
                string numer_bl = tnumbl.Text;
                DataTable dt_blo = fun.GetBlByNum(int.Parse(numer_bl));
                if (dt_blo.Rows.Count > 0)
                {
                    XtraMessageBox.Show("Il existe un bon livraison avec ce numéro");
                }
                else
                {
                    if (dateEdit1.Text == "")
                    {
                        MessageBox.Show("Entrer la date SVP");
                    }
                    else
                    {
                        prixtotc = 0;
                        Double test = 0;
                        string etat = "en cours";
                        string etatcmd = "servi";
                        DataTable dt = new DataTable();
                        dt = fun.get_cltByDesign(lookUpEdit1.Text);
                        string id_clt = dt.Rows[0][0].ToString();

                        string timbre = liste_cde_client.timbre;
                        string id_bl = (get_maxbl() + 1).ToString();

                        for (int i = 0; i < gridView1.DataRowCount; i++)
                        {
                            DataRow row = gridView1.GetDataRow(i);
                            DataTable dat = new DataTable();
                            dat = fun.get_piececmdbynump(Convert.ToInt32(row[0]));
                            // Double quantiterestante = Convert.ToDouble(dat.Rows[0][12].ToString());
                            Double qterst = Convert.ToDouble(dat.Rows[0][12].ToString()) - Convert.ToDouble(row[3].ToString());
                            // Double prremis = Convert.ToDouble(dat.Rows[0][7].ToString()) - Convert.ToDouble(row[7].ToString());
                            double prix_net = 0;
                            prix_net = double.Parse(row[6].ToString().Replace('.', ',')) * double.Parse(row[3].ToString().Replace('.', ','));//qt*prix
                            prix_net = prix_net - ((prix_net * double.Parse(row[9].ToString().Replace('.', ','))) / 100);//-remise
                            prix_net = prix_net + ((prix_net * double.Parse(row[10].ToString().Replace('.', ','))) / 100);//+tva
                            prixtotc += prix_net;
                            fun.update_qterestcommande(qterst.ToString(), row[0].ToString());
                            fun.insert_piecee_bl(row[1].ToString(), row[2].ToString(), row[3].ToString(), row[4].ToString(), row[5].ToString(), row[6].ToString(), row[7].ToString(),int.Parse(id_bl), row[9].ToString(), row[10].ToString(), row[11].ToString());
                            //fun.update_sousstock_after_accept2(double.Parse(row[3].ToString().Replace('.', ',')), row[1].ToString());
                        }
                        fun.insert_into_bl2(id_clt, etat, lookUpEdit1.Text, tnbcmd.Text, liste_cde_client.id_commande.ToString(), prixtotc.ToString(), timbre, numer_bl, textEdit1.Text, textEdit2.Text, textEdit3.Text, dateEdit1.Text.Substring(0, 10));

                        DataTable dattt = fun.get_AllprodbyCMD(liste_cde_client.id_commande.ToString());

                        for (int i = 0; i < dattt.Rows.Count; i++)
                        {
                            DataRow row = dattt.Rows[i];

                            test += Convert.ToDouble(row[12]);

                        }
                        if (test == 0)
                        {
                            fun.update_etatcmd(etatcmd, liste_cde_client.id_commande.ToString());

                        }



                        fun.update_etat(etatcmd, liste_cde_client.id_commande.ToString());

                        numbl = Convert.ToInt32(id_bl);
                        MessageBox.Show("BL ajouté avec succées");
                        BLReport report = new BLReport(Convert.ToInt32(id_bl));
                        report.ShowPreview();
                        //Bon_livraison bl = new Bon_livraison();
                        //bl.ShowDialog();
                        this.Close();

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
                     idpiece = Convert.ToInt32(row[0].ToString());                   
                     lookUpEdit2.EditValue= row[1].ToString();                   
                     mdesign.Text = row[2].ToString();
                     tquantit.Text = row[3].ToString();
                     tpu.Text = row[6].ToString();
                     tunit.Text = row[11].ToString();
                     ttva.Text = row[10].ToString();
                     tremise.Text = row[9].ToString();
                 }
                 tunit.Enabled = false;
                 lookUpEdit2.Enabled = false;
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
                DataTable dt = fun.get_piececmdbynump(idpiece);
                Double qterst = Convert.ToDouble(dt.Rows[0][12].ToString().Replace('.', ',')) - Convert.ToDouble(tquantit.Text.Replace('.', ','));
             
                // fun.update_qterestcommande(qterst.ToString());
                DataRow row = dartt.Rows[idrow];
                DataRow newpc = dartt.NewRow();
                newpc["id_piece"] = idpiece.ToString();
                newpc["code_art"] = row[1];
                newpc["libelle_piece"] = mdesign.Text;
                newpc["quantite_piece"] = tquantit.Text;
                newpc["id_clt"] = row[4];
                newpc["etat"] = "validée";
                newpc["puv"] = tpu.Text ;
                newpc["totvente"] = pvtva.ToString();
                newpc["id_commande"] = row[8];
                newpc["remise"] = tremise.Text;
                newpc["ttva"] = ttva.Text;
                newpc["unit"] = row[11];
                dartt.Rows[idrow].Delete();
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
                gridView1.OptionsView.ShowAutoFilterRow = true;
            }


            catch (Exception except)
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
                    dartt.Rows[idrow].Delete();
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
                    gridView1.OptionsView.ShowAutoFilterRow = true;
                  
                }
            }
            catch(Exception exc)
            { }
        }

        private void CommandeBL_Load(object sender, EventArgs e)
        {

        }
        public int NumeoBl()
        {
            
            int y = 0;
            int x = 0;
            DataTable dt = new DataTable();
            DataTable dr = new DataTable();
            dr = fun.Listbl();
            if (dr.Rows.Count == 0)
            {
                return 1;
            }

            dt = fun.max_num_bl2();
            if (dt.Rows.Count == 0)
            {
                return 1;
            }
            string max = dt.Rows[0]["max"].ToString();
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

        
       
        

      
    }
}
