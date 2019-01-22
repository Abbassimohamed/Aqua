using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;

namespace RibbonSimplePad
{
    public partial class reporting : DevExpress.XtraEditors.XtraForm
    {
       
       DataTable dt= new DataTable();

         public string format_devise(string mt)
        {
            string s;
            s = "3";

            if (mt != "")
            {
                try
                {
                    decimal val = decimal.Parse(mt);
                    mt = val.ToString("N" + s);
                    return mt;
                }
                catch
                {
                    // MessageBox.Show("Format de champ invalid ! Type numeric attendu ", "ERP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return "";
                }
            }
            else
            {
                return "";
            }
        }
        
        private void reporting_Load(object sender, EventArgs e)
        {
              dt.Columns.Add("utilisateur");
                    dt.Columns.Add("Charges");
                    dt.Columns.Add("Produits");
        
              ChartControl lineChart = new ChartControl();
            chartControl1.Visible = true;
            Series s1 = new Series("ecriture", ViewType.Bar);
            Series s2= new Series("ecriture", ViewType.Bar);
             Series s3= new Series("ecriture", ViewType.Bar);
          
            chartControl1.Series.Clear();
            foreach (DataRow rw in dt.Rows)
            {
                DevExpress.XtraCharts.SeriesPoint seriesPoint1 = new DevExpress.XtraCharts.SeriesPoint(rw[0].ToString(), new object[] {
               ((object)(format_devise(rw[1].ToString())))});
                MessageBox.Show(rw[1].ToString());
              s1.Points.Add(seriesPoint1);
                DevExpress.XtraCharts.SeriesPoint seriesPoint2 = new DevExpress.XtraCharts.SeriesPoint(rw[0].ToString(), new object[] {
               ((object)(format_devise(rw[2].ToString())))});
                MessageBox.Show(rw[2].ToString());
                s2.Points.Add(seriesPoint2);

                 DevExpress.XtraCharts.SeriesPoint seriesPoint3 = new DevExpress.XtraCharts.SeriesPoint(rw[0].ToString(), new object[] {
               ((object)(format_devise(rw[2].ToString())))});
                MessageBox.Show(rw[2].ToString());
                s3.Points.Add(seriesPoint3);
               
            }
            chartControl1.Series.Add(s1);
            chartControl1.Series.Add(s2);
             chartControl1.Series.Add(s3);
        }
 
        
       
        
        
        public reporting()
        {
            InitializeComponent();
        }

        //private void reporting_Load(object sender, EventArgs e)
        //{

        //}
    }
}