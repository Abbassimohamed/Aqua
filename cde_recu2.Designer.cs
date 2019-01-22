namespace RibbonSimplePad
{
    partial class cde_recu2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(cde_recu2));
            this.gridControl5 = new DevExpress.XtraGrid.GridControl();
            this.gridView5 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.simpleButton26 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton25 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton22 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton24 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton23 = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridControl5
            // 
            this.gridControl5.Cursor = System.Windows.Forms.Cursors.Default;
            this.gridControl5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl5.Location = new System.Drawing.Point(2, 2);
            this.gridControl5.MainView = this.gridView5;
            this.gridControl5.Name = "gridControl5";
            this.gridControl5.Size = new System.Drawing.Size(899, 481);
            this.gridControl5.TabIndex = 10;
            this.gridControl5.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView5});
            // 
            // gridView5
            // 
            this.gridView5.GridControl = this.gridControl5;
            this.gridView5.Name = "gridView5";
            this.gridView5.OptionsBehavior.Editable = false;
            this.gridView5.OptionsView.AllowCellMerge = true;
            this.gridView5.RowHeight = 50;
            this.gridView5.DoubleClick += new System.EventHandler(this.gridView5_DoubleClick);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.simpleButton26);
            this.panelControl1.Controls.Add(this.simpleButton25);
            this.panelControl1.Controls.Add(this.simpleButton22);
            this.panelControl1.Controls.Add(this.simpleButton24);
            this.panelControl1.Controls.Add(this.simpleButton23);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(903, 44);
            this.panelControl1.TabIndex = 18;
            // 
            // simpleButton26
            // 
            this.simpleButton26.Image = global::RibbonSimplePad.Properties.Resources.ajouter_icone_5107_16;
            this.simpleButton26.Location = new System.Drawing.Point(29, 12);
            this.simpleButton26.Name = "simpleButton26";
            this.simpleButton26.Size = new System.Drawing.Size(75, 23);
            this.simpleButton26.TabIndex = 11;
            this.simpleButton26.Text = "Ajouter";
            this.simpleButton26.Click += new System.EventHandler(this.simpleButton26_Click);
            // 
            // simpleButton25
            // 
            this.simpleButton25.Image = global::RibbonSimplePad.Properties.Resources.pie_chart_modifier_icone_8023_16;
            this.simpleButton25.Location = new System.Drawing.Point(129, 12);
            this.simpleButton25.Name = "simpleButton25";
            this.simpleButton25.Size = new System.Drawing.Size(75, 23);
            this.simpleButton25.TabIndex = 12;
            this.simpleButton25.Text = "Modifier";
            this.simpleButton25.Click += new System.EventHandler(this.simpleButton25_Click);
            // 
            // simpleButton22
            // 
            this.simpleButton22.Image = global::RibbonSimplePad.Properties.Resources.devis_16;
            this.simpleButton22.Location = new System.Drawing.Point(429, 12);
            this.simpleButton22.Name = "simpleButton22";
            this.simpleButton22.Size = new System.Drawing.Size(75, 23);
            this.simpleButton22.TabIndex = 15;
            this.simpleButton22.Text = "Exporter";
            this.simpleButton22.Click += new System.EventHandler(this.simpleButton22_Click);
            // 
            // simpleButton24
            // 
            this.simpleButton24.Image = global::RibbonSimplePad.Properties.Resources.supprimer_icone_6859_16;
            this.simpleButton24.Location = new System.Drawing.Point(229, 12);
            this.simpleButton24.Name = "simpleButton24";
            this.simpleButton24.Size = new System.Drawing.Size(75, 23);
            this.simpleButton24.TabIndex = 13;
            this.simpleButton24.Text = "Supprimer";
            this.simpleButton24.Click += new System.EventHandler(this.simpleButton24_Click);
            // 
            // simpleButton23
            // 
            this.simpleButton23.Image = global::RibbonSimplePad.Properties.Resources.actualiser_16;
            this.simpleButton23.Location = new System.Drawing.Point(329, 12);
            this.simpleButton23.Name = "simpleButton23";
            this.simpleButton23.Size = new System.Drawing.Size(75, 23);
            this.simpleButton23.TabIndex = 14;
            this.simpleButton23.Text = "Actualiser";
            this.simpleButton23.Click += new System.EventHandler(this.simpleButton23_Click);
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.gridControl5);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 44);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(903, 485);
            this.panelControl2.TabIndex = 19;
            // 
            // cde_recu2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(903, 529);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "cde_recu2";
            this.Text = "Liste de commandes Fournisseur reçus";
            this.Activated += new System.EventHandler(this.cde_recu2_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.cde_recu2_FormClosing);
            this.Load += new System.EventHandler(this.cde_recu2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl5;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView5;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton simpleButton26;
        private DevExpress.XtraEditors.SimpleButton simpleButton25;
        private DevExpress.XtraEditors.SimpleButton simpleButton22;
        private DevExpress.XtraEditors.SimpleButton simpleButton24;
        private DevExpress.XtraEditors.SimpleButton simpleButton23;
        private DevExpress.XtraEditors.PanelControl panelControl2;
    }
}