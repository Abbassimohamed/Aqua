using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.DXCore.Controls.XtraEditors;

namespace RibbonSimplePad
{
    class dialogresult
    {
        public void test()
        {
            DialogResult dialogResult = XtraMessageBox.Show("La pièce est déja ajoutée à la commande client!! 'Oui' pour additioner la quantité 'Non' pour annuler   ", "Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (dialogResult == DialogResult.Yes)
            {

             
            }
            else if (dialogResult == DialogResult.No)
            {

            }
        }
    }
}
