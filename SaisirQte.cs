using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;
using System.IO.IsolatedStorage;
namespace RibbonSimplePad
{
    public partial class SaisirQte : DevExpress.XtraEditors.XtraForm
    {
        public SaisirQte()
        {
            InitializeComponent();
        }
        public static Double prixunit;
        public static Double prixattt;
        public static Double ptot;
          gestionStock gs = new gestionStock();
       public static Double previent=0;
        private void button1_Click(object sender, EventArgs e)
        {
            sql_gmao fun = new sql_gmao();
            Stockart stockart = new Stockart();
           
            IsolatedStorageFile isoStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null);
            try
            {
                if (isoStore.FileExists("TestStore.txt"))
                {
                    using (IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream("TestStore.txt", FileMode.Open, isoStore))
                    {
                        using (StreamReader reader = new StreamReader(isoStream))
                        {
                            if (textBox1.Text == "")
                            {
                                XtraMessageBox.Show("insérer la quantité demandée");

                            }
                            else
                            {

                                string a = reader.ReadLine();
                                
                                string b = reader.ReadLine();
                                
                                prixunit = Double.Parse(reader.ReadLine());
                              
                                ptot = ptot + prixunit * Double.Parse(textBox1.Text);
                                Double c = Convert.ToDouble(textBox1.Text);
                               
                                fun.setRelArt_SousArt(a, b, c, ptot);
                                previent = previent + ptot;
                                XtraMessageBox.Show("" + previent);
                                this.Dispose();

                            }
                        }
                    }
                }
              
            }
            catch (Exception except)
            {
                MessageBox.Show("miss by"+except);
            }
        }

      
    }
}