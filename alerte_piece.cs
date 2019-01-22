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
    public partial class alerte_piece : DevExpress.XtraEditors.XtraForm
    {
        public alerte_piece()
        {
            InitializeComponent();
        }
        private void alerte_piece_Load(object sender, EventArgs e)
        {
            //afficher la quantité en stock et le seuil
          
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}