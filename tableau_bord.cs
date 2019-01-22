using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraCharts;
using System.Data.SqlClient;
using DevExpress.Utils.Animation;

namespace RibbonSimplePad
{
    public partial class tableau_bord : DevExpress.XtraEditors.XtraForm
    {
        public tableau_bord()
        {
            InitializeComponent();
            grid_repture();
        }
        sql_gmao fun = new sql_gmao();
        private void tableau_bord_Load(object sender, EventArgs e)
        {
            //grid_repture();
            gridView1.BestFitColumns();
           
           
        }

        private void grid_repture()
        {
            // remplir datagrid
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            DataTable dt = new DataTable();
            dt = fun.get_AlertSeuil();
            gridControl1.DataSource = dt;
            this.gridView1.Columns[0].Caption = "Code pièce";
            this.gridView1.Columns[1].Caption = "Désigniation";
            this.gridView1.Columns[2].Caption = "Unité";
            this.gridView1.Columns[3].Caption = "Quantité";
            this.gridView1.Columns[4].Visible = false;
            this.gridView1.Columns[5].Caption = "Stock d'alerte"; 
            this.gridView1.Columns[6].Visible = false;
            this.gridView1.Columns[7].Visible = false;
            this.gridView1.Columns[8].Visible = false;
            this.gridView1.Columns[9].Visible = false;
            this.gridView1.Columns[10].Visible = false;
            this.gridView1.Columns[11].Visible = false;
            this.gridView1.Columns[12].Visible = false;
            this.gridView1.Columns[13].Visible = false;
            this.gridView1.Columns[14].Visible = false;
            gridView1.OptionsView.ShowAutoFilterRow = true;
            gridView1.OptionsView.EnableAppearanceEvenRow = true;
        }
      
       
       

        private void stat_stock()
        {

            chartControl1.DataSource = null;
            chartControl1.Series.Clear();
            chartControl1.Titles.Clear();
            ChartTitle chartTitle1 = new ChartTitle();

            //*********************************** stat seuil stock ***********************************
            /*
            SqlCommand selectCommand = new SqlCommand("SELECT * FROM stock");
            SqlDataAdapter da = new SqlDataAdapter(selectCommand);
            DataSet ds = new DataSet();
            selectCommand.Connection = sql_gmao.conn;
            da.Fill(ds, "stock");
            Series series1 = new Series("Quantité en stock", ViewType.Bar);
            chartControl1.Series.Add(series1);
            Series series2 = new Series("Seuil", ViewType.Line);
            chartControl1.Series.Add(series2);
            Series series3 = new Series("Quantité Réelle", ViewType.Area);
            chartControl1.Series.Add(series3);
            chartControl1.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
            chartControl1.DataSource = ds.Tables[0];
            chartControl1.Series[0].ArgumentDataMember = "code_piece";
            chartControl1.Series[0].ValueDataMembers[0] = "quantite_piece";
            chartControl1.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
            chartControl1.DataSource = ds.Tables[0];
            chartControl1.Series[1].ArgumentDataMember = "code_piece";
            chartControl1.Series[1].ValueDataMembers[0] = "seuil_piece";
            chartControl1.Series[1].ChangeView(ViewType.Line);

            chartControl1.Series[2].ArgumentDataMember = "code_piece";
            chartControl1.Series[2].ValueDataMembers[0] = "quantite_reelle";
            chartControl1.Series[2].ChangeView(ViewType.Area);
            chartTitle1.Text = "Quantité disponible/ Seuil/ Quantité réelle";
         
            chartControl1.Titles.Add(chartTitle1);
            */
        }

        private void stat_intervention()
        {
            //************************** nbr intervention pour chaque equipement ************************** 

            chartControl1.DataSource = null;
            chartControl1.Series.Clear();
            chartControl1.Titles.Clear();
            ChartTitle chartTitle2 = new ChartTitle();
            int c = 4;
            SqlCommand selectCommand2 = new SqlCommand("SELECT * FROM equipement WHERE iImageIndex= '" + c + "'");
            SqlDataAdapter da2 = new SqlDataAdapter(selectCommand2);
            DataSet ds2 = new DataSet();
            selectCommand2.Connection = sql_gmao.conn;
            da2.Fill(ds2, "equipement");
            Series series11 = new Series("Nombre d'Interventions", ViewType.Bar);
            chartControl1.Series.Add(series11);
            chartControl1.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
            chartControl1.DataSource = ds2.Tables[0];
            chartControl1.Series[0].ArgumentDataMember = "sName";
            chartControl1.Series[0].ValueDataMembers[0] = "nbr";
            chartTitle2.Text = "Nombre d'interventions pour chaque équipement";
            chartControl1.Titles.Add(chartTitle2);

        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
           
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
           
        }
      

        private void effect_chart()
        {
            if (transitionManager1.Transitions[chartControl1] == null)
            {
                Transition transition1 = new Transition();
                transition1.Control = chartControl1;
                transitionManager1.Transitions.Add(transition1);
            }
            DevExpress.Utils.Animation.Transitions trType = (DevExpress.Utils.Animation.Transitions.Push);
            transitionManager1.Transitions[chartControl1].TransitionType = CreateTransitionInstance(trType);
            if (transitionManager1.IsTransaction)
            {
                transitionManager1.EndTransition();
            }
            transitionManager1.StartTransition(chartControl1);
            try
            {

            }
            finally
            {
                transitionManager1.EndTransition();
            }
        }

        BaseTransition CreateTransitionInstance(Transitions transitionType)
        {
            switch (transitionType)
            {
                case Transitions.Dissolve: return new DissolveTransition();
                case Transitions.Fade: return new FadeTransition();
                case Transitions.Shape: return new ShapeTransition();
                case Transitions.Clock: return new ClockTransition();
                case Transitions.SlideFade: return new SlideFadeTransition();
                case Transitions.Cover: return new CoverTransition();
                case Transitions.Comb: return new CombTransition();
                default: return new PushTransition();
            }
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            effect_chart();
            stat_stock();
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            effect_chart();
            stat_intervention();
        }

        private void gridControl2_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                Point pt = this.Location;
                pt.Offset(this.Left + e.X, this.Top + e.Y);
                popupMenu1.ShowPopup(this.barManager1, Control.MousePosition);
            }
        }

        private void chartControl1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                Point pt = this.Location;
                pt.Offset(this.Left + e.X, this.Top + e.Y);
                popupMenu2.ShowPopup(this.barManager1, Control.MousePosition);
            }
        }

        private void tableau_bord_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            this.Parent = null;
            e.Cancel = true;
        }

        private void tableau_bord_Activated(object sender, EventArgs e)
        {
            splitContainerControl1.SplitterPosition = this.ClientSize.Height / 2;
           
            Form1.wait = 1;
            Form1.load = 1;

            stat_stock();
            grid_repture();
            gridView1.BestFitColumns();
        }

        private void chartControl1_Click(object sender, EventArgs e)
        {

        }

    }
}