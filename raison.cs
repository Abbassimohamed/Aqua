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
    public partial class raison : DevExpress.XtraEditors.XtraForm
    {
        public raison()
        {
            InitializeComponent();
        }
        sql_gmao fun = new sql_gmao();
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            fun.update_commentaire_fact(liste_cde_client.id_fact,memoEdit1.Text);
            this.Close();
        }
    }
}