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
    public partial class delimenter : DevExpress.XtraEditors.XtraForm
    {
        public delimenter()
        {
            InitializeComponent();
        }
        sql_gmao fun = new sql_gmao();
        private void memoEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
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
                if (Convert.ToInt32(textEdit1.Text) > gestionStock.qte)
                {
                    XtraMessageBox.Show("Qantité actuellement indisponible en stock (quantité disponible:" + gestionStock.qte + ")", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    fun.update_stock_after_aliment2(Convert.ToInt32(textEdit1.Text), gestionStock.int_piece);
                    fun.delimenter(dateEdit1.DateTime, Convert.ToInt32(textEdit1.Text), memoEdit1.Text, timeEdit1.Text, gestionStock.lib, comboBoxEdit1.Text);
                    XtraMessageBox.Show("Piéces sorties de stock, Actualisez!!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    gestionStock.test = "";
                    this.Close();
                }
            }
        }
        private void alimenter_Load(object sender, EventArgs e)
        {
            dateEdit1.DateTime = System.DateTime.Now;
            timeEdit1.Text = System.DateTime.Now.ToShortTimeString();
        }
    }
}