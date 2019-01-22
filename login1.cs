using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.OleDb;
using DevExpress.LookAndFeel;
using System.Diagnostics;
using System.IO;
using System.Configuration;
using SKGL;
namespace RibbonSimplePad
{
    public partial class login1 : Form
    {
        public login1()
        {
            InitializeComponent();
        }
        sql_gmao fun = new sql_gmao();
        public static int id_user;
        public static string raison_sociale, adresse, fax, tel, email_sos, test_image;
        public static byte[] IMG, IMG2;
        public static string des = "Authentification";
        public static string etat, skinn;
       
        public static string  export_stock, pass_en_cde, vis_cde_inter, filtrer_cde_inter, exporter_cde_inter, passer_cde_feur, vis_cde_feur, supp_pdt_cde, modif_qté_cde, expor_cde_feur, stat1, ajout_feur, modif_feur, supprimer_feur, exporter_feur;
        public static string vis_tech, ajout_tech, modif_tech, supp_tech, expor_tech, vis_equip, ajout_equi, modif_equi, supp_equip, gerer_fiche_tech, imprimer_equi, vis_famille, ajout_famille, modif_famille, export_famille, vis_plan_p, ajout_plan_p, modif_plan_p, supp_plan_p, copier_plan_p, export_plan_p, deplacer_plan_p, deplacer_entre_reces, multiselection, redimention, stat2;

        // info generales
        public static string nom, prenom, fonction, depart, gsm, email, pseudo, passwd;
        // stock
        public static string vis_stock, ajou_stock, modif_stock, supp_stock, ger_uni, ger_mag, stock_doc, passer_cde, alimen, sort_prod, his_alim, his_sort;
        //info feur
        public static string vis_feur, aj_feur, mod_feur, supp_feur, feur_doc, supp_cde_feur;
        //info clt
        public static string vis_clt, aj_clt, mod_clt, supp_clt, clt_doc, supp_cde_clt, valid_cde_clt, fact, bon_liv, bon_sort;
        //gestion devis
        public static string vis_devis, ajout_devis, supp_devis, devis_doc;
        //autres
        public static string stat, not;

        public static string conta, stat_conta;
        
        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
            }
        }
        private void login_Load(object sender, EventArgs e)
        {
            string key = "";// System.Configuration.ConfigurationManager.AppSettings["serial"].Trim();
            
                DataTable dt_key = fun.getKey();
           
            DateTime last_date=new DateTime();
           
            string password = "saber2webpass";
            //prog cc = new prog();
            if (dt_key.Rows.Count > 0)
            {
                key = dt_key.Rows[0]["keys"].ToString().Trim();
            }
            if (key == "")
            {
                labelControl3.Visible = true;
                simpleButton1.Visible = true;
                textEdit2.Visible = true;
                comboBox1.Visible = false;
                textEdit1.Visible = false;
                pictureBox1.Visible = false;
            }
            else
            {
                try
                {
                    SKGL.Validate ValidateAKey = new SKGL.Validate();
                    ValidateAKey.secretPhase = password;
                    ValidateAKey.Key = key;

                    DateTime d = ValidateAKey.ExpireDate;
                    if (ValidateAKey.IsValid && !ValidateAKey.IsExpired && ValidateAKey.SetTime >= ValidateAKey.DaysLeft && d > last_date)
                    {

                        labelControl3.Visible = false;
                        simpleButton1.Visible = false;
                        textEdit2.Visible = false;
                        comboBox1.Visible = true;
                        textEdit1.Visible = true;
                        pictureBox1.Visible = true;

                        if (dt_key.Rows.Count > 0)
                        {

                            last_date = DateTime.Parse(dt_key.Rows[0]["last_date"].ToString());

                            if (last_date < DateTime.Now)
                            {
                                fun.Update_last_day_Key(DateTime.Now);
                            }

                        }
                    }
                    else
                    {
                        labelControl3.Visible = true;
                        simpleButton1.Visible = true;
                        textEdit2.Visible = true;
                        comboBox1.Visible = false;
                        textEdit1.Visible = false;
                        pictureBox1.Visible = false;
                    }
                }
                catch (Exception bb)
                {
                    labelControl3.Visible = true;
                    simpleButton1.Visible = true;
                    textEdit2.Visible = true;
                    comboBox1.Visible = false;
                    textEdit1.Visible = false;
                    pictureBox1.Visible = false;
                }
            }
            // recuperer les données de société
            DataTable dd = new DataTable();
            dd = fun.select_from_société();
            if (dd.Rows.Count != 0)
            {
                raison_sociale = dd.Rows[0]["nom_societe"].ToString();
                adresse = dd.Rows[0]["adresse_societe"].ToString();
                fax = dd.Rows[0]["fax_societe"].ToString();
                tel = dd.Rows[0]["tel_societe"].ToString();
                email_sos = dd.Rows[0]["email_societe"].ToString();
                test_image = dd.Rows[0]["pic_societe"].ToString();
                if (test_image != "")
                {
                    IMG = (Byte[])(dd.Rows[0]["logo_societe"]);
                  
                    MemoryStream mem = new MemoryStream(login1.IMG);
                    pictureEdit1.Image = Image.FromStream(mem);
                }

                else { pictureEdit1.Image = null; }
                labelControl2.Text = raison_sociale;

            }
            this.TransparencyKey = BackColor;
            // requperer les pseudos
            DataTable zz = new DataTable();
            zz = fun.combologin();
            foreach (DataRow row in zz.Rows)
            {
                comboBox1.Properties.Items.Add(row["login"]);
            }
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pic_log();
           

        }
        private void pic_log()
        {
            // test connexion
            
           
              
                    vis_stock = ""; ajou_stock = ""; modif_stock = ""; supp_stock = ""; ger_uni = ""; ger_mag = ""; stock_doc = ""; passer_cde = ""; alimen = ""; sort_prod = ""; his_alim = ""; his_sort = ""; vis_clt = ""; aj_clt = ""; mod_clt = ""; supp_clt = ""; clt_doc = ""; supp_cde_clt = ""; valid_cde_clt = ""; fact = ""; bon_liv = ""; bon_sort = ""; vis_feur = ""; aj_feur = ""; mod_feur = ""; supp_feur = "";
                    feur_doc = ""; supp_cde_feur = ""; vis_devis = ""; ajout_devis = ""; supp_devis = ""; devis_doc = ""; stat = ""; not = ""; conta = ""; stat_conta = "";


                    pseudo = comboBox1.Text;
                    DataTable dd = new DataTable();
                    dd = fun.log_login(comboBox1.Text, textEdit1.Text);
             if (dd.Rows.Count != 0)
            {
               pseudo = comboBox1.Text;
                depart = dd.Rows[0]["depart"].ToString();
                nom = dd.Rows[0]["nom"].ToString();
                prenom = dd.Rows[0]["prenom"].ToString();
                vis_stock = dd.Rows[0]["visualiser_stock"].ToString();
                passwd = dd.Rows[0]["passwd"].ToString();


                id_user =Convert.ToInt32( dd.Rows[0]["id_user"]);

                ajou_stock = dd.Rows[0]["ajouter_stock"].ToString();



                modif_stock = dd.Rows[0]["modifier_stock"].ToString();



                supp_stock = dd.Rows[0]["supprimer_stock"].ToString();

                 

                    ger_uni = dd.Rows[0]["ges_uni"].ToString();

                   

                    ger_mag = dd.Rows[0]["ges_mag"].ToString();

                   

                    stock_doc = dd.Rows[0]["doc_stock"].ToString();

                   

                    passer_cde = dd.Rows[0]["passer_cde"].ToString();

                   

                    alimen = dd.Rows[0]["aliment"].ToString();
                   

                   
                    sort_prod = dd.Rows[0]["sortie_prod"].ToString();

                   

                    his_alim = dd.Rows[0]["alim"].ToString();

                 

                    his_sort = dd.Rows[0]["sortie_sto"].ToString();

                  

                    vis_clt = dd.Rows[0]["vis_clt"].ToString();

                  

                    aj_clt = dd.Rows[0]["ajout_clt"].ToString();

                  

                    mod_clt = dd.Rows[0]["mod_clt"].ToString();

                    

                    supp_clt = dd.Rows[0]["supp_clt"].ToString();

                   

                    clt_doc = dd.Rows[0]["doc_clt"].ToString();


                   

                    supp_cde_clt = dd.Rows[0]["supp_cde_clt"].ToString();

                  

                    valid_cde_clt = dd.Rows[0]["vali_cde_clt"].ToString();

                   

                    fact = dd.Rows[0]["fact"].ToString();

                   

                    bon_liv = dd.Rows[0]["bon_liv"].ToString();

                   

                    bon_sort = dd.Rows[0]["bon_sortie"].ToString();

                    

                    vis_feur = dd.Rows[0]["vis_feur"].ToString();

                  

                    aj_feur = dd.Rows[0]["aj_feur"].ToString();

                   

                    mod_feur = dd.Rows[0]["mod_feur"].ToString();

                    

                    supp_feur = dd.Rows[0]["supp_feur"].ToString();

                  

                    feur_doc = dd.Rows[0]["doc_feur"].ToString();
                    supp_cde_feur = dd.Rows[0]["supp_cde_feur"].ToString();
                    vis_devis = dd.Rows[0]["vis_dev"].ToString();
                    ajout_devis = dd.Rows[0]["aj_devis"].ToString();
                    supp_devis = dd.Rows[0]["supp_dev"].ToString();
                    devis_doc = dd.Rows[0]["doc_dev"].ToString();
                    stat = dd.Rows[0]["stat"].ToString();
                    skinn = dd.Rows[0]["theme"].ToString();
                    not = dd.Rows[0]["noti"].ToString();
                    conta = dd.Rows[0]["conta"].ToString();
                    stat_conta = dd.Rows[0]["stat_conta"].ToString();
                   
                   
                   
                 
                 DateTime histDate55 = DateTime.Now;

                 fun.t_estt(des.ToString(), pseudo, histDate55.ToString());

                    pictureBox1.Enabled = false;
                    label2.Visible = false;
                    UserLookAndFeel.Default.SkinName = skinn;
                    this.Hide();
                    Form1 fn = new Form1();
                    fn.ShowDialog();
                
            }
            else
            { label2.Visible = true; }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }

        private void textEdit1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                pic_log();
            }
        }

        private void pictureEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            fun.UpdatetKey(textEdit2.Text,DateTime.Now);

            login_Load(new object(),new EventArgs());
        }
    }
}