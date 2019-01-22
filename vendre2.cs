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
    public partial class vendre2 : DevExpress.XtraEditors.XtraForm
    {
        public vendre2()
        {
            InitializeComponent();
        }
        sql_gmao fun = new sql_gmao();
        public static Double pv, prix_final;
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (vendre.test == "vendre")
                {

                    if (checkEdit2.Checked == true)
                    {
                        pv = Double.Parse(vendre.puv_rev) * Double.Parse(textEdit1.Text);
                    }

                    else
                    { pv = Double.Parse(vendre.puv) * Double.Parse(textEdit1.Text); }

                    if (Convert.ToDouble(textEdit1.Text) > vendre.qte_piece)
                    {

                        XtraMessageBox.Show(String.Format("La quantité demandée  de {0} est indisponible en stock!!", vendre.lib_piece), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }

                    else
                    {

                        string en_cours = "en cours";
                        string etat = "validée";
                        DataTable dt = new DataTable();
                        dt = fun.test_fact(vendre.id_client, etat);

                        if (dt.Rows.Count != 0)
                        {
                            int id_fact;
                            id_fact = Convert.ToInt32(dt.Rows[0][0]);
                            DataTable gf = new DataTable();
                            gf = fun.select_etoile_from_piece_fact(vendre.id_client, vendre.id_piece, en_cours, id_fact);
                            if (gf.Rows.Count != 0)
                            {

                                Double qte = Convert.ToDouble(gf.Rows[0][3]);


                                Double nouv_qte = 0;
                                nouv_qte = qte + Convert.ToDouble(textEdit1.Text);

                                if (checkEdit2.Checked == true)
                                { prix_final = nouv_qte * Convert.ToDouble(vendre.puv_rev); }

                                else

                                { prix_final = nouv_qte * Convert.ToDouble(vendre.puv); }

                                // si piece existante dans la commande additionner la quantité
                                DialogResult dialogResult = XtraMessageBox.Show("La pièce est déja ajoutée à la commande client!! 'Oui' pour additioner la quantité 'Non' pour annuler   ", "Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                                if (dialogResult == DialogResult.Yes)
                                {

                                    fun.update_qte_piece_fact(Convert.ToDouble(textEdit1.Text), vendre.id_client, vendre.lib_piece, prix_final);

                                }
                                else if (dialogResult == DialogResult.No)
                                {

                                }
                            }
                            else
                            {

                                string d = "en cours";
                                if (checkEdit2.Checked == true)
                                {
                                    fun.insert_piecee_fact(vendre.lib_piece, vendre.id_piece, Convert.ToDouble(textEdit1.Text), vendre.id_client, d, id_fact, vendre.puv_rev.ToString(), pv.ToString(), vendre.nbcomande);

                                }

                                else
                                {
                                    fun.insert_piecee_fact(vendre.lib_piece, vendre.id_piece, Convert.ToDouble(textEdit1.Text), vendre.id_client, d, id_fact, vendre.puv.ToString(), pv.ToString(), vendre.nbcomande);

                                }

                            }

                        }

                        else
                        {
                            int fact;
                            DataTable aa = new DataTable();

                            string w = "en cours";
                            //ajouter une commande
                            //fun.insert_into_fact(vendre.id_client, w, vendre.client, w, vendre.nbcomande);
                            DataTable tt = new DataTable();
                            tt = fun.get_max_fac(vendre.id_client, w);


                            if (tt.Rows.Count != 0)
                            {
                                fact = Convert.ToInt32(tt.Rows[0][0]);

                                if (checkEdit2.Checked == true)
                                {
                                    fun.insert_piecee_fact(vendre.lib_piece, vendre.id_piece, Convert.ToDouble(textEdit1.Text), vendre.id_client, w, fact, vendre.puv_rev.ToString(), pv.ToString(), vendre.nbcomande);
                                }

                                else
                                {
                                    fun.insert_piecee_fact(vendre.lib_piece, vendre.id_piece, Convert.ToDouble(textEdit1.Text), vendre.id_client, w, fact, vendre.puv.ToString(), pv.ToString(), vendre.nbcomande);

                                }

                            }
                        }

                    }


                    this.Close();

                }
                else { }

            }
            catch (Exception exce)
            {
                MessageBox.Show("" + exce);
            }
        }
    }
}