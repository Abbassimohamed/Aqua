using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.SqlClient;
using DevExpress.XtraCharts;
using DevExpress.Utils.Animation;

namespace RibbonSimplePad
{
    public partial class stat2 : DevExpress.XtraEditors.XtraForm
    {
        SeriesPoint m_HotTrackedPoint;
        public stat2()
        {
            InitializeComponent();
        }


        public  string etat,typp;
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
        private void stat2_Load(object sender, EventArgs e)
        {
            etat = "encaiss";
            typp = "encaissement";
            repositoryItemComboBox1.Items.Clear();
            
            for (int i = 2000; i <= 2100; i++)
            {
                repositoryItemComboBox1.Items.Add(i.ToString());
            }
            DateTime zz = System.DateTime.Now;
            int yearz = zz.Year;
            barEditItem1.EditValue = yearz;


            chartControl1.DataSource = null;
            chartControl1.Series.Clear();
            chartControl1.Titles.Clear();
            ChartTitle chartTitle1 = new ChartTitle();

           

            //*********************************** stat seuil stock ***********************************
           
            DateTime dd = System.DateTime.Now;
            int year = dd.Year;

            SqlCommand selectCommand = new SqlCommand("SELECT * FROM conta where type1='" + typp + "' and YEAR(datee)= '" + year + "'");
            SqlDataAdapter da = new SqlDataAdapter(selectCommand);
            DataSet ds = new DataSet();


            selectCommand.Connection = sql_gmao.conn;
            da.Fill(ds, "conta");
            Series series1 = new Series("Date", ViewType.Bar);
            chartControl1.Series.Add(series1);


            chartControl1.Series[0].ArgumentScaleType = ScaleType.Qualitative;
            chartControl1.DataSource = ds.Tables[0];
            chartControl1.Series[0].ArgumentDataMember = "datee";

            chartControl1.Series[0].ValueDataMembers[0] = "montant";
            //chartControl1.Series[1].ArgumentScaleType = ScaleType.Qualitative;
            //chartControl1.DataSource = ds.Tables[0];
            //chartControl1.Series[1].ArgumentDataMember = "datee";
            //chartControl1.Series[1].ValueDataMembers[0] = "montant";
            // chartControl1.Series[1].ChangeView(ViewType.Line);






            // Set the scale type for the series' arguments and values.
            series1.ArgumentScaleType = ScaleType.DateTime;
            series1.ValueScaleType = ScaleType.Numerical;

            // Cast the chart's diagram to the XYDiagram type, to access its axes.
            XYDiagram diagram = chartControl1.Diagram as XYDiagram;

            // Define the date-time measurement unit, to which the beginning of 
            // a diagram's gridlines and labels should be aligned. 
            //****************** diagram.AxisX.DateTimeScaleOptions.GridAlignment = DateTimeGridAlignment.Day;

            // Define the detail level for date-time values.
            diagram.AxisX.DateTimeScaleOptions.MeasureUnit = DateTimeMeasureUnit.Month;

            // Define the custom date-time format (name of a month) for the axis labels.
            diagram.AxisX.Label.TextPattern = "{A:MMMM-yyyy}";
            diagram.AxisX.DateTimeScaleOptions.AggregateFunction = AggregateFunction.Sum;


            // Since the ValueScaleType of the chart's series is Numerical,
            // it is possible to customize the NumericOptions of Y-axis.
            // diagram.AxisY.Label.TextPattern = "{A:C1}";
            ((BarSeriesView)series1.View).ColorEach = true;



            // Access the type-specific options of the diagram.
            ((XYDiagram)chartControl1.Diagram).EnableAxisXZooming = true;

            // Hide the legend (if necessary).
            //chartControl1.Legend.Visible = false;


            chartTitle1.Text = "Liste des Encaissements";

            chartControl1.Titles.Clear();
            chartControl1.Titles.Add(chartTitle1);

            rangeControl2.Dock = DockStyle.Top;
            chartControl1.Dock = DockStyle.Fill;

           
          
        }

        private void stat2_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            this.Parent = null;
            e.Cancel = true;
        }

        private void stat2_Activated(object sender, EventArgs e)
        {
            Form1.load = 1;

            Form1.wait = 1;

           
          
        }

        private void chartControl1_CustomDrawSeriesPoint(object sender, CustomDrawSeriesPointEventArgs e)
        {

        }

        private void chartControl1_ObjectHotTracked(object sender, HotTrackEventArgs e)
        {

        }

        private void repositoryItemComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }

        private void barEditItem1_EditValueChanged(object sender, EventArgs e)
        {
            chartControl1.DataSource = null;
            chartControl1.Series.Clear();
            chartControl1.Titles.Clear();
            ChartTitle chartTitle1 = new ChartTitle();

            //*********************************** stat seuil stock ***********************************
          

            int year;
            year = Convert.ToInt32(barEditItem1.EditValue);

            SqlCommand selectCommand = new SqlCommand("SELECT * FROM conta where type1='" + typp + "' and YEAR(datee)= '" + year + "'");
            SqlDataAdapter da = new SqlDataAdapter(selectCommand);
            DataSet ds = new DataSet();


            selectCommand.Connection = sql_gmao.conn;
            da.Fill(ds, "conta");
            Series series1 = new Series("Date", ViewType.Bar);
            chartControl1.Series.Add(series1);


            chartControl1.Series[0].ArgumentScaleType = ScaleType.Qualitative;
            chartControl1.DataSource = ds.Tables[0];
            chartControl1.Series[0].ArgumentDataMember = "datee";

            chartControl1.Series[0].ValueDataMembers[0] = "montant";
            //chartControl1.Series[1].ArgumentScaleType = ScaleType.Qualitative;
            //chartControl1.DataSource = ds.Tables[0];
            //chartControl1.Series[1].ArgumentDataMember = "datee";
            //chartControl1.Series[1].ValueDataMembers[0] = "montant";
            // chartControl1.Series[1].ChangeView(ViewType.Line);

            if (ds.Tables[0].Rows.Count == 0)
            {
                XtraMessageBox.Show("Aucun enregistrement pour cette année!!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }


            else
            {

                // Set the scale type for the series' arguments and values.
                series1.ArgumentScaleType = ScaleType.DateTime;
                series1.ValueScaleType = ScaleType.Numerical;

                // Cast the chart's diagram to the XYDiagram type, to access its axes.
                XYDiagram diagram = chartControl1.Diagram as XYDiagram;

                // Define the date-time measurement unit, to which the beginning of 
                // a diagram's gridlines and labels should be aligned. 



                // Define the detail level for date-time values.
                //diagram.AxisX.DateTimeScaleOptions.MeasureUnit = DateTimeMeasureUnit.Month;
                //diagram.AxisX.DateTimeScaleOptions.GridAlignment = DateTimeGridAlignment.Day;

                // Define the custom date-time format (name of a month) for the axis labels.
                diagram.AxisX.Label.TextPattern = "{A:MMMM-yyyy}";
                diagram.AxisX.DateTimeScaleOptions.AggregateFunction = AggregateFunction.Sum;


                // Since the ValueScaleType of the chart's series is Numerical,
                // it is possible to customize the NumericOptions of Y-axis.
                // diagram.AxisY.Label.TextPattern = "{A:C1}";
                ((BarSeriesView)series1.View).ColorEach = true;



                // Access the type-specific options of the diagram.
                ((XYDiagram)chartControl1.Diagram).EnableAxisXZooming = true;

                // Hide the legend (if necessary).
                //chartControl1.Legend.Visible = false;
                if (etat == "encaiss")

                { chartTitle1.Text = "Liste des Encaissements"; }

                if (etat == "decaiss")

                { chartTitle1.Text = "Liste des Décaissements"; }


                chartControl1.Titles.Add(chartTitle1);

                rangeControl2.Dock = DockStyle.Top;
                chartControl1.Dock = DockStyle.Fill;

                //  chartControl1.CrosshairOptions.CrosshairLabelMode = CrosshairLabelMode.ShowForNearestSeries;

                // this.chartControl1.Series[0].LabelsVisibility = Visible;

                ((XYDiagram)chartControl1.Diagram).AxisX.GridLines.Visible = true;

                chartControl1.SeriesTemplate.Label.Visible = false;



            }


            chartControl1.Refresh();
            chartControl1.RefreshData();
            

        }

        private void chartControl1_CustomDrawCrosshair(object sender, CustomDrawCrosshairEventArgs e)
        {

        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
            chartControl1.DataSource = null;
            chartControl1.Series.Clear();
            chartControl1.Titles.Clear();
            ChartTitle chartTitle1 = new ChartTitle();

            //*********************************** stat seuil stock ***********************************
            if (etat == "decaiss")
            {
                
               
                
                typp = "encaissement";
                
              
            }
            else 
            {
                
                
                
                typp = "decaissement";

                
            }

           

            int year;
            year = Convert.ToInt32(barEditItem1.EditValue);

            SqlCommand selectCommand = new SqlCommand("SELECT * FROM conta where type1='" + typp + "' and YEAR(datee)= '" + year + "'");
            SqlDataAdapter da = new SqlDataAdapter(selectCommand);
            DataSet ds = new DataSet();


            selectCommand.Connection = sql_gmao.conn;
            da.Fill(ds, "conta");
            Series series1 = new Series("Date", ViewType.Bar);
            chartControl1.Series.Add(series1);


            chartControl1.Series[0].ArgumentScaleType = ScaleType.Qualitative;
            chartControl1.DataSource = ds.Tables[0];
            chartControl1.Series[0].ArgumentDataMember = "datee";

            chartControl1.Series[0].ValueDataMembers[0] = "montant";
            //chartControl1.Series[1].ArgumentScaleType = ScaleType.Qualitative;
            //chartControl1.DataSource = ds.Tables[0];
            //chartControl1.Series[1].ArgumentDataMember = "datee";
            //chartControl1.Series[1].ValueDataMembers[0] = "montant";
            // chartControl1.Series[1].ChangeView(ViewType.Line);

            if (ds.Tables[0].Rows.Count == 0)
            {
                XtraMessageBox.Show("Aucun enregistrement pour cette année!!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }


            else
            {

                // Set the scale type for the series' arguments and values.
                series1.ArgumentScaleType = ScaleType.DateTime;
                series1.ValueScaleType = ScaleType.Numerical;

                // Cast the chart's diagram to the XYDiagram type, to access its axes.
                XYDiagram diagram = chartControl1.Diagram as XYDiagram;

                // Define the date-time measurement unit, to which the beginning of 
                // a diagram's gridlines and labels should be aligned. 



                // Define the detail level for date-time values.
                //diagram.AxisX.DateTimeScaleOptions.MeasureUnit = DateTimeMeasureUnit.Month;
                //diagram.AxisX.DateTimeScaleOptions.GridAlignment = DateTimeGridAlignment.Day;

                // Define the custom date-time format (name of a month) for the axis labels.
                diagram.AxisX.Label.TextPattern = "{A:MMMM-yyyy}";
                diagram.AxisX.DateTimeScaleOptions.AggregateFunction = AggregateFunction.Sum;


                // Since the ValueScaleType of the chart's series is Numerical,
                // it is possible to customize the NumericOptions of Y-axis.
                // diagram.AxisY.Label.TextPattern = "{A:C1}";
                ((BarSeriesView)series1.View).ColorEach = true;



                // Access the type-specific options of the diagram.
                ((XYDiagram)chartControl1.Diagram).EnableAxisXZooming = true;

                // Hide the legend (if necessary).
                //chartControl1.Legend.Visible = false;


              


                chartControl1.Titles.Add(chartTitle1);

                rangeControl2.Dock = DockStyle.Top;
                chartControl1.Dock = DockStyle.Fill;

                //  chartControl1.CrosshairOptions.CrosshairLabelMode = CrosshairLabelMode.ShowForNearestSeries;

                // this.chartControl1.Series[0].LabelsVisibility = Visible;

                //((XYDiagram)chartControl1.Diagram).AxisX.GridLines.Visible = true;

                //chartControl1.SeriesTemplate.Label.Visible = false;



               
            
            }
             if (etat == "decaiss")
                {





                    etat = "encaiss";
                    chartTitle1.Text = "Liste des Encaissements";
                    effect();
                }
                else
                {





                    etat = "decaiss";
                    chartTitle1.Text = "Liste des Décaissements";
                    effect();
                }
             chartControl1.Refresh();
             chartControl1.RefreshData();

        }

        private void chartControl1_MouseClick(object sender, MouseEventArgs e)
        {
            if (etat == "encaiss")
            { barButtonItem1.Caption = "Voir décaissements"; }
            if (etat == "decaiss")
            { barButtonItem1.Caption = "Voir Encaissements"; }
            
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {

                
                Point pt = this.Location;
                pt.Offset(this.Left + e.X, this.Top + e.Y);
                popupMenu1.ShowPopup(this.barManager1, Control.MousePosition);

            }
        }
    }
}