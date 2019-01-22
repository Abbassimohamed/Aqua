using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO.IsolatedStorage;
using System.IO;
namespace RibbonSimplePad
{
    public partial class qteservie : DevExpress.XtraEditors.XtraForm
    {

        public qteservie()
        {
            InitializeComponent();
        }
        sql_gmao fun = new sql_gmao();
        public static int qteres;
        public static int qteservi;
       
        private void simpleButton1_Click(object sender, EventArgs e)
        {


            IsolatedStorageFile isoStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null);

            if (isoStore.FileExists("TestStore1.txt"))
            {

                using (IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream("TestStore1.txt", FileMode.Open, isoStore))
                {
                    using (StreamReader reader = new StreamReader(isoStream))
                    {
                        string a= reader.ReadLine();
                        int puv = int.Parse(reader.ReadLine());
                                            
                        int b = liste_cde_client.id_fact;
                       int  quantite = int.Parse(textEdit1.Text);
                       double montant = puv * quantite;
                        fun.update_piece_qte(a,b,quantite,montant);
                        this.Dispose();
                        
                    }
                }
            
            }
        }
           
           
        }
           
        }


        

