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
using DevExpress.Utils;
using System.Data.SqlClient;
using DevExpress.Utils.Animation;
namespace RibbonSimplePad
{
    public partial class statistique : DevExpress.XtraEditors.XtraForm
    {
        public statistique()
        {
            InitializeComponent();
        }
  
        private void statistique_Load(object sender, EventArgs e)
        {

        }
        private void effect()
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
        private void statistique_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            this.Parent = null;
            e.Cancel = true;
        }

        private void statistique_Activated(object sender, EventArgs e)
        {
            try
            {
                chartControl1.DataSource = null;
                chartControl1.Series.Clear();
                chartControl1.Titles.Clear();
                ChartTitle chartTitle1 = new ChartTitle();

                //*********************************** stat seuil stock ***********************************

                SqlCommand selectCommand = new SqlCommand("SELECT * FROM stock");
                SqlDataAdapter da = new SqlDataAdapter(selectCommand);
                DataSet ds = new DataSet();
                selectCommand.Connection = sql_gmao.conn;
                da.Fill(ds, "stock");
                Series series1 = new Series("Quantité enstock", ViewType.Bar);
                chartControl1.Series.Add(series1);
                Series series2 = new Series("Seuil", ViewType.Line);
                chartControl1.Series.Add(series2);
                Series series3 = new Series("Quantité Réelle", ViewType.Area);
                chartControl1.Series.Add(series3);
                chartControl1.Series[0].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl1.DataSource = ds.Tables[0];
                chartControl1.Series[0].ArgumentDataMember = "libelle_piece";
                chartControl1.Series[0].ValueDataMembers[0] = "quantite_piece";
                chartControl1.Series[1].ArgumentScaleType = DevExpress.XtraCharts.ScaleType.Qualitative;
                chartControl1.DataSource = ds.Tables[0];
                chartControl1.Series[1].ArgumentDataMember = "libelle_piece";
                chartControl1.Series[1].ValueDataMembers[0] = "seuil_piece";
                chartControl1.Series[1].ChangeView(ViewType.Line);
                chartControl1.Series[2].ArgumentDataMember = "libelle_piece";
                chartControl1.Series[2].ValueDataMembers[0] = "quantite_reelle";
                chartControl1.Series[2].ChangeView(ViewType.Area);
                chartTitle1.Text = "Quantité disponible/ Seuil/ Quantité réelle";


                chartControl1.Titles.Add(chartTitle1);

                rangeControl2.Dock = DockStyle.Top;
                chartControl1.Dock = DockStyle.Fill;
            }
            catch (Exception excepti)
            { }
        }
        }
}