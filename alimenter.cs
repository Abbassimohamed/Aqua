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
    public partial class alimenter : DevExpress.XtraEditors.XtraForm
    {
        public alimenter()
        {
            InitializeComponent();
        }
        sql_gmao fun = new sql_gmao();
        private void memoEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (dateEdit1.Text == "")
                {
                    dxErrorProvider1.Dispose();
                    dxErrorProvider1.SetError(dateEdit1, "veuillez choisir une date!!");
                }
                else if (timeEdit1.Text == "")
                {
                    dxErrorProvider1.Dispose();
                    dxErrorProvider1.SetError(timeEdit1, "veuillez choisir l'heure!!");
                }
                else if (textEdit1.Text == "")
                {
                    dxErrorProvider1.Dispose();
                    dxErrorProvider1.SetError(textEdit1, "Saisir la quantité!!");
                }
                else if (comboBoxEdit1.Text == "")
                {
                    dxErrorProvider1.Dispose();
                    dxErrorProvider1.SetError(comboBoxEdit1, "Choisir une nature!!");
                }
                else if (Convert.ToDateTime(dateEdit1.Text) > Convert.ToDateTime(System.DateTime.Now.ToShortDateString()))
                {
                    dxErrorProvider1.Dispose();
                    dxErrorProvider1.SetError(dateEdit1, "Date invalide !!");

                }

                else
                {
                    DataTable datatab = new DataTable();
                    datatab = fun.get_piecebycode(gestionStock.int_piece);
                    Double qte = Convert.ToDouble(datatab.Rows[0][3].ToString().Replace('.', ','));
                    Double newquantit = Convert.ToDouble(textEdit1.Text.Replace('.', ',')) + qte;
                    fun.update_stock_after_aliment(newquantit, gestionStock.int_piece);     //fun.alimenter(dateEdit1.DateTime, Convert.ToInt32(textEdit1.Text), memoEdit1.Text, timeEdit1.Text, gestionStock.lib, comboBoxEdit1.Text);
                    DataTable dt = new DataTable();

                    //dt = fun.selectMatPremByCodeP(gestionStock.int_piece);
                    //foreach (DataRow row in dt.Rows)
                    //{
                    //    int a = int.Parse(row[3].ToString());

                    //    string b = row[2].ToString();

                    //    fun.update_sousstock_after_accept(int.Parse(textEdit1.Text) * a, b);
                    //}
                    XtraMessageBox.Show("Piéces ajoutées en stock, Actualisez!!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    gestionStock.test = "";
                    this.Close();
                }
            }

            catch (Exception except)
            {
                MessageBox.Show("verifier les valeurs entrées");

            }
        }
        private void alimenter_Load(object sender, EventArgs e)
        {
            dateEdit1.DateTime = System.DateTime.Now;
            timeEdit1.Text = System.DateTime.Now.ToShortTimeString();
        }
    }
}