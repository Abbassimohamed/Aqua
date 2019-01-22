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
    public partial class voir_pieces : DevExpress.XtraEditors.XtraForm
    {
        public voir_pieces()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        sql_gmao fun = new sql_gmao();
    
    
    private void Listepiece()
        {
            string etat; 
            etat = "envoye";
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            gridControl1.DataSource = fun.get_piece_fact(etat,vendre.id_client,vendre.idd_fact);
            this.gridView1.Columns[0].Visible = false;
            this.gridView1.Columns[1].Visible = false;
            this.gridView1.Columns[2].Caption = "Piéce";
            this.gridView1.Columns[3].Caption = "Quantité";
            this.gridView1.Columns[4].Visible = false;
            this.gridView1.Columns[5].Visible = false;
            this.gridView1.Columns[6].Visible = false; 
            this.gridView1.Columns[7].Visible = false;
            this.gridView1.Columns[8].Visible = false;
            this.gridView1.Columns[9].Caption = "Prix Unitaire HT";
            this.gridView1.Columns[10].Caption = "Montant HT";
            gridView1.OptionsView.ShowAutoFilterRow = true;
        
        }

    private void voir_pieces_Load(object sender, EventArgs e)
    {
        Listepiece();
    }

    private void voir_pieces_Activated(object sender, EventArgs e)
    {
        gridView1.OptionsView.ShowAutoFilterRow = true;
        gridView1.BestFitColumns();
        gridView1.OptionsBehavior.Editable = false;
        gridView1.OptionsView.EnableAppearanceEvenRow = true;
    }
    }
}