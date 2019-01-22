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
    public partial class ModifierBL2 : DevExpress.XtraEditors.XtraForm
    {
        public static DataTable dartt = new DataTable();
        public  static Double prixtotc,prix_ht;
        public static Double test;

             public static int numbl,id_clt=0 ;
             int id_bl = 0, id_cmd = 0;
             DataTable dt_detail_bl, dt_detail_cmd;
        public ModifierBL2(int id_bl)
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
            dartt.Columns.Add("id_commande");
            dartt.Columns.Add("remise");
            dartt.Columns.Add("ttva");
            dartt.Columns.Add("unit");

            this.id_bl = id_bl;
            dt_detail_bl = fun.getbl(id_bl);
            if (dt_detail_bl.Rows.Count > 0)
            {
                lookUpEdit1.EditValue = dt_detail_bl.Rows[0]["id_clt"].ToString();
                textEdit1.Text = dt_detail_bl.Rows[0]["mode_livraison"].ToString();
                textEdit2.Text = dt_detail_bl.Rows[0]["moyen_livraison"].ToString();
                textEdit3.Text = dt_detail_bl.Rows[0]["lieu_livraison"].ToString();
                tnumcommandebase.Text = dt_detail_bl.Rows[0]["numero_bl"].ToString();
                textBox5.Text = dt_detail_bl.Rows[0]["timbre"].ToString();
                dateEdit1.Text = dt_detail_bl.Rows[0]["date_ajout"].ToString();
                if (dt_detail_bl.Rows[0]["id_commande"].ToString() != "" && dt_detail_bl.Rows[0]["id_commande"].ToString().Trim() != "0")
                {
                    //simpleButton1.Enabled = false;
                   // lookUpEdit2.Enabled = false;
                   // lookUpEdit1.Enabled = false;
                    tnbcmd.Enabled = false;
                    id_cmd = int.Parse(dt_detail_bl.Rows[0]["id_commande"].ToString());
                    dt_detail_cmd = fun.affiche_cmdclt_infos(id_cmd);
                    if (dt_detail_cmd.Rows.Count > 0)
                    {
                        tnbcmd.Text = dt_detail_cmd.Rows[0]["n_boncmd"].ToString();


                    }
                   
                }
                else
                {
                    tnbcmd.Text = dt_detail_bl.Rows[0]["nbcmd"].ToString();
                }
                
            }

            DataTable d_piec_bl = fun.get_Allprodbybl(id_bl.ToString());
            foreach (DataRow dr in d_piec_bl.Rows)
            {
                DataRow drw = dartt.NewRow();

                drw["code_art"] = dr["code_art"];
                drw["libelle_piece"] = dr["libelle_piece"];
                drw["quantite_piece"] = dr["quantite_piece"];
                drw["id_clt"] = dr["id_clt"];
                drw["etat"] = dr["etat"];
                drw["puv"] = dr["puv"];
                drw["totvente"] = dr["totvente"];
                drw["id_commande"] = dr["id_commande"];
                drw["remise"] = dr["remise"];
                drw["ttva"] = dr["ttva"];
                drw["unit"] = dr["unit"];
                dartt.Rows.Add(drw);
            }
            
            getalldatatable();
           // tnumcommandebase.Text =  NumeoBl().ToString();
                    
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




        void update_bl_sans_cmd()
        {
            fun.delete_piece_bl(id_bl);
            prix_ht = 0; prixtotc = 0;
            for (int i = 0; i < gridView1.DataRowCount; i++)
            {
                DataRow row = gridView1.GetDataRow(i);
               
                Double pnetvente = Convert.ToDouble(row[2].ToString().Replace('.', ',')) * Convert.ToDouble(row[5].ToString().Replace('.', ','));
               double prix_rem = pnetvente - ((pnetvente * Convert.ToDouble(row[8].ToString().Replace('.', ','))) / 100);
                prix_ht += prix_rem;
                Double pnetventetc = prix_rem + ((prix_rem * Convert.ToDouble(row[9].ToString().Replace('.', ','))) / 100);
                prixtotc += pnetventetc;
                fun.insert_piecee_bl(row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(), row[4].ToString(), row[5].ToString(), row[6].ToString(),id_bl, row[8].ToString(), row[9].ToString(), row[10].ToString());
                //fun.update_sousstock_after_accept2(double.Parse(row[2].ToString().Replace('.', ',')), row[0].ToString());
            }
            string id_clt,nom_clt;
            id_clt=lookUpEdit1.EditValue.ToString();
            nom_clt=lookUpEdit1.Text;
            fun.update_bonlivraison2(id_bl, prix_ht.ToString(), prixtotc.ToString(), textBox5.Text, textEdit1.Text, textEdit2.Text, textEdit3.Text, tnbcmd.Text,id_clt,nom_clt,dateEdit1.DateTime.ToString().Substring(0,(10)));
        }
        void update_bl_avec_cmd()
        {
            //for (int i = 0; i < gridView1.DataRowCount; i++)
            //{
            //    DataRow row = gridView1.GetDataRow(i);
            //    DataTable dat = new DataTable();
            //    dat = fun.get_piececmdbycode2(row[0].ToString(),id_cmd);
            //    Double quantiterestante = Convert.ToDouble(dat.Rows[0][12].ToString());
            //    Double qterst = Convert.ToDouble(dat.Rows[0][12].ToString()) + Convert.ToDouble(row[2].ToString());
            //    fun.update_qterestcommande2(qterst.ToString(), row[0].ToString(), id_cmd);
            //}

            DataTable dtt = fun.get_Allprodbybl(id_bl.ToString());
            foreach (DataRow dx in dtt.Rows)
            {
                DataTable dat = new DataTable();
                dat = fun.get_piececmdbycode2(dx[1].ToString(),id_cmd);
                if (dat.Rows.Count > 0)
                {
                    Double quantiterestante = Convert.ToDouble(dat.Rows[0][12].ToString());
                    Double qterst = Convert.ToDouble(dat.Rows[0][12].ToString()) + Convert.ToDouble(dx[3].ToString());
                    fun.update_qterestcommande2(qterst.ToString(), dx[1].ToString(), id_cmd);
                }
            }
            fun.delete_piece_bl(id_bl);
            prix_ht = 0; prixtotc = 0;
            for (int i = 0; i < gridView1.DataRowCount; i++)
            {
                DataRow row = gridView1.GetDataRow(i);
                DataTable dat = new DataTable();
                dat = fun.get_piececmdbycode2(row[0].ToString(),id_cmd);
                if (dat.Rows.Count > 0)
                {
                    Double quantiterestante = Convert.ToDouble(dat.Rows[0][12].ToString());
                    Double qterst = Convert.ToDouble(dat.Rows[0][12].ToString()) - Convert.ToDouble(row[2].ToString());
                    fun.update_qterestcommande2(qterst.ToString(), row[0].ToString(), id_cmd);
                }
               // Double prremis = Convert.ToDouble(dat.Rows[0][7].ToString()) - Convert.ToDouble(row[7].ToString());
                double prix_net = double.Parse(row[5].ToString().Replace('.', ',')) * double.Parse(row[2].ToString().Replace('.', ','));//qt*prix
                prix_net = prix_net - ((prix_net * double.Parse(row[8].ToString().Replace('.', ','))) / 100);//-remise
                prix_net = prix_net + ((prix_net * double.Parse(row[9].ToString().Replace('.', ','))) / 100);//+tva
                prixtotc += prix_net;
                
              //  fun.insert_piecee_bl(row[1].ToString(), row[2].ToString(), row[3].ToString(), row[4].ToString(), row[5].ToString(), row[6].ToString(), row[7].ToString(), id_bl, row[9].ToString(), row[10].ToString(), row[11].ToString());
               // fun.update_sousstock_after_accept2(double.Parse(row[3].ToString().Replace('.', ',')), row[0].ToString());
               // fun.update_piece_bl2(row[1].ToString(), row[2].ToString(), row[0].ToString(), row[8].ToString(), row[6].ToString(), row[9].ToString(), row[5].ToString(),int.Parse(tnumcommandebase.Text));
                fun.insert_piecee_bl(row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(), row[4].ToString(), row[5].ToString(), row[6].ToString(), id_bl, row[8].ToString(), row[9].ToString(), row[10].ToString());

            }
            fun.update_bonlivraison3(id_bl, prix_ht.ToString(), prixtotc.ToString(), textBox5.Text, textEdit1.Text, textEdit2.Text, textEdit3.Text,dateEdit1.DateTime.ToString().Substring(0,10));

        }
        public Boolean VerfierQtBlCmd(int id_cmd)
        {
            Boolean result= true;
            string msg = "Quantité du produit est supérieur au qantité restante du commande \n Code Produit : \n";
            DataTable dtt = fun.get_Allprodbybl(id_bl.ToString());
            for (int i = 0; i < gridView1.DataRowCount; i++)
            {
                DataRow row = gridView1.GetDataRow(i);
                DataTable dat = new DataTable();
                dat = fun.get_piececmdbycode2(row[0].ToString(), id_cmd);
                if (dat.Rows.Count > 0)
                {
                    Double quantiterestante = Convert.ToDouble(dat.Rows[0][12].ToString());
                    Double qterst =  Convert.ToDouble(row[2].ToString());
                    if (qterst > quantiterestante)
                    {
                        result = false;
                        msg += row[0].ToString() + "\n";
                    }
                   
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
            if (dateEdit1.Text =="")
            {
                MessageBox.Show("Entrer la date");
            }
            else
            {
                if (id_cmd == 0)
                {
                    update_bl_sans_cmd();
                    MessageBox.Show("BL modifié avec succès");
                    numbl = id_bl;
                    BLReport report = new BLReport(Convert.ToInt32(id_bl));
                    report.ShowPreview();
                    this.Close();
                }
                else
                {

                    update_bl_avec_cmd();
                    MessageBox.Show("BL modifié avec succès");
                    numbl = id_bl;
                    BLReport report = new BLReport(Convert.ToInt32(id_bl));
                    report.ShowPreview();
                    this.Close();

                }
            }
           
            
                
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
            Double prixremis = pvhorstva - ((pvhorstva * Convert.ToDouble(tremise.Text.Replace('.', ','))) / 100);
            Double pvtva = prixremis +( (prixremis * Convert.ToDouble(ttva.Text.Replace('.', ','))) / 100);
           


            DataRowView rowView1 = (DataRowView)lookUpEdit1.GetSelectedDataRow();
            DataRow row1 = rowView1.Row;
            DataRowView rowView2 = (DataRowView)lookUpEdit2.GetSelectedDataRow();
            DataRow row2 = rowView2.Row;
            string codeclt = row1[0].ToString();
            DataRow row = dartt.NewRow();
            row[0] = row2[0].ToString();
            row[1] = mdesign.Text;
            row[2] = Convert.ToDouble(tquantit.Text.Replace('.', ','));
            row[3] = codeclt;
            row[4] = "validée";
            row[5] = tpu.Text;
            row[6] = pvtva.ToString();
            row[7] = NumeoBl().ToString();
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

                Double prixremis = pvhorstva -(( pvhorstva * Convert.ToDouble(tremise.Text.Replace('.', ','))) / 100);

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
                newpc["id_commande"] = (get_maxbl() + 1).ToString();
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
                for (int i = 0; i < gridView1.RowCount; i++)
                {

                    DataRow row1 = gridView1.GetDataRow(i);
                    prixtotc += Convert.ToDouble(row1[6].ToString().Replace('.', ','));
                    prix_ht += Convert.ToDouble(row1[2].ToString().Replace('.', ',')) * Convert.ToDouble(row1[5].ToString().Replace('.', ','));
                }

              //  MessageBox.Show(prix_ht.ToString());
               
            }
            catch (Exception xce)
            { 
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void BLNOCommandeBL_Load(object sender, EventArgs e)
        {

        }
       
      
    }
}
