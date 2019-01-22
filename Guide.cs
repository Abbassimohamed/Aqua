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
    public partial class Guide : DevExpress.XtraEditors.XtraForm
    {
        public Guide()
        {
            InitializeComponent();
        }

        private void Guide_Load(object sender, EventArgs e)
        {
            this.pdfViewer1.LoadDocument(@"Guide.pdf");
          
            pdfViewer1.ZoomFactor = 80;
            pdfViewer1.Refresh();
            
        }
    }
}