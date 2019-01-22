using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Diagnostics;
using DevExpress.XtraEditors;
using DevExpress.Skins;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars.Ribbon.Gallery;
using DevExpress.Utils.Drawing;
using DevExpress.Utils;
using DevExpress.Tutorials.Controls;
using DevExpress.XtraEditors.Controls;
using DevExpress.LookAndFeel;

namespace DevExpress.XtraBars.Demos.RibbonSimplePad {
    public partial class frmMain : DevExpress.XtraBars.Ribbon.RibbonForm {
        public frmMain() {
            InitializeComponent();
            CreateColorPopup(popupControlContainer1);
            InitSkinGallery();
            InitFontGallery();
            InitColorGallery();
            InitEditors();
            UserLookAndFeel.Default.SetSkinStyle("Office 2010 Blue");
        }

        int documentIndex = 0;
        ColorPopup cp;
        frmFind dlgFind = null;
        frmReplace dlgReplace = null;
        GalleryItem fCurrentFontItem, fCurrentColorItem;
        string DocumentName { get { return string.Format("New Document {0}", documentIndex); } }

        void CreateNewDocument() {
            CreateNewDocument(null);
        }
        void InitEditors() {
            riicStyle.Items.Add(new ImageComboBoxItem("Office 2007", RibbonControlStyle.Office2007, -1));
            riicStyle.Items.Add(new ImageComboBoxItem("Office 2010", RibbonControlStyle.Office2010, -1));
            biStyle.EditValue = ribbonControl1.RibbonStyle;
        }
        public void ShowHideFormatCategory() {
            RibbonPageCategory selectionCategory = Ribbon.PageCategories[0] as RibbonPageCategory;
            if(selectionCategory == null) return;
            if(CurrentRichTextBox == null)
                selectionCategory.Visible = false;
            else
                selectionCategory.Visible = CurrentRichTextBox.SelectionLength != 0;
            if(selectionCategory.Visible) Ribbon.SelectedPage = selectionCategory.Pages[0];
        }
        void CreateNewDocument(string fileName) {
            documentIndex++;
            frmPad pad = new frmPad();
            if(fileName != null)
                pad.LoadDocument(fileName);
            else
                pad.DocName = DocumentName;
            pad.MdiParent = this;
            pad.Closed += new EventHandler(Pad_Closed);
            pad.ShowPopupMenu += new EventHandler(Pad_ShowPopupMenu);
            pad.Show();
            InitNewDocument(pad.RTBMain);
        }

        void Pad_Closed(object sender, EventArgs e) {
            CloseFind();
        }
        void Pad_ShowPopupMenu(object sender, EventArgs e) {
            pmMain.ShowPopup(Control.MousePosition);
        }
        void CloseFind() {
            if(dlgFind != null && dlgFind.RichText != CurrentRichTextBox) {
                dlgFind.Close();
                dlgFind = null;
            }
            if(dlgReplace != null && dlgReplace.RichText != CurrentRichTextBox) {
                dlgReplace.Close();
                dlgReplace = null;
            }
        }

        private void CreateColorPopup(PopupControlContainer container) {
            cp = new ColorPopup(container, iFontColor, this);
        }
        #region Init
        private void frmMain_Activated(object sender, System.EventArgs e) {
            InitPaste();
        }
        public void UpdateText() {
            ribbonControl1.ApplicationCaption = "Ribbon Simple Pad";
            ribbonControl1.ApplicationDocumentCaption = CurrentDocName + (CurrentModified ? "*" : "");
            //Text = string.Format("Ribbon Simple Pad ({0})", CurrentDocName);
            siDocName.Caption = string.Format("  {0}", CurrentDocName);
        }
        void ChangeActiveForm() {
            UpdateText();
            InitCurrentDocument(CurrentRichTextBox);
            rtPad_SelectionChanged(CurrentRichTextBox, EventArgs.Empty);
            CloseFind();
        }
        private void xtraTabbedMdiManager1_FloatMDIChildActivated(object sender, EventArgs e) {
            ChangeActiveForm();
        }
        private void xtraTabbedMdiManager1_FloatMDIChildDeactivated(object sender, EventArgs e) {
            BeginInvoke(new MethodInvoker(ChangeActiveForm));
        }
        private void frmMain_MdiChildActivate(object sender, System.EventArgs e) {
            ChangeActiveForm();
        }
        void rtPad_SelectionChanged(object sender, System.EventArgs e) {
            ShowHideFormatCategory();
            RichTextBox rtPad = sender as RichTextBox;
            InitFormat();
            int line = 0, col = 0;

            if(rtPad != null) {
                InitEdit(rtPad.SelectionLength > 0);
                line = rtPad.GetLineFromCharIndex(rtPad.SelectionStart) + 1;
                col = rtPad.SelectionStart + 1;
            }
            else {
                InitEdit(false);
            }
            siPosition.Caption = string.Format("   Line: {0}  Position: {1}   ", line, col);
            CurrentFontChanged();
        }
        void rtPad_TextChanged(object sender, System.EventArgs e) {
            if(CurrentForm == null) return;
            CurrentForm.Modified = true;
            InitCurrentDocument(CurrentRichTextBox);
        }

        protected void InitFormat() {
            iBold.Enabled = SelectFont != null;
            iItalic.Enabled = SelectFont != null;
            iUnderline.Enabled = SelectFont != null;
            iFont.Enabled = SelectFont != null;
            iFontColor.Enabled = SelectFont != null;
            if(SelectFont != null) {
                iBold.Down = SelectFont.Bold;
                iItalic.Down = SelectFont.Italic;
                iUnderline.Down = SelectFont.Underline;
            }
            bool enabled = CurrentRichTextBox != null;
            iProtected.Enabled = enabled;
            iBullets.Enabled = enabled;
            iAlignLeft.Enabled = enabled;
            iAlignRight.Enabled = enabled;
            iCenter.Enabled = enabled;
            rgbiFont.Enabled = enabled;
            rgbiFontColor.Enabled = enabled;
            ribbonPageGroup9.ShowCaptionButton = enabled;
            rpgFont.ShowCaptionButton = enabled;
            rpgFontColor.ShowCaptionButton = enabled;
            if(!enabled) ClearFormats();
            if(CurrentRichTextBox != null) {
                iProtected.Down = CurrentRichTextBox.SelectionProtected;
                iBullets.Down = CurrentRichTextBox.SelectionBullet;
                switch(CurrentRichTextBox.SelectionAlignment) {
                    case HorizontalAlignment.Left:
                        iAlignLeft.Down = true;
                        break;
                    case HorizontalAlignment.Center:
                        iCenter.Down = true;
                        break;
                    case HorizontalAlignment.Right:
                        iAlignRight.Down = true;
                        break;
                }
            }
        }

        void ClearFormats() {
            iBold.Down = false;
            iItalic.Down = false;
            iUnderline.Down = false;
            iProtected.Down = false;
            iBullets.Down = false;
            iAlignLeft.Down = false;
            iAlignRight.Down = false;
            iCenter.Down = false;
        }

        protected void InitPaste() {
            bool enabledPase = CurrentRichTextBox != null && CurrentRichTextBox.CanPaste(DataFormats.GetFormat(0));
            iPaste.Enabled = enabledPase;
            sbiPaste.Enabled = enabledPase;
        }

        void InitUndo() {
            iUndo.Enabled = CurrentRichTextBox != null ? CurrentRichTextBox.CanUndo : false;
            iLargeUndo.Enabled = iUndo.Enabled;
        }
        protected void InitEdit(bool enabled) {
            iCut.Enabled = enabled;
            iCopy.Enabled = enabled;
            iClear.Enabled = enabled;
            iSelectAll.Enabled = CurrentRichTextBox != null ? CurrentRichTextBox.CanSelect : false;
            InitUndo();
        }

        void InitNewDocument(RichTextBox rtbControl) {
            rtbControl.SelectionChanged += new System.EventHandler(this.rtPad_SelectionChanged);
            rtbControl.TextChanged += new System.EventHandler(this.rtPad_TextChanged);
        }

        void InitCurrentDocument(RichTextBox rtbControl) {
            bool enabled = rtbControl != null;
            iSaveAs.Enabled = enabled;
            iClose.Enabled = enabled;
            iPrint.Enabled = enabled;
            sbiSave.Enabled = enabled;
            sbiFind.Enabled = enabled;
            iFind.Enabled = enabled;
            iReplace.Enabled = enabled;
            iSave.Enabled = CurrentModified;
            SetModifiedCaption();
            InitPaste();
            InitFormat();
        }

        void SetModifiedCaption() {
            if(CurrentForm == null) {
                siModified.Caption = "";
                return;
            }
            siModified.Caption = CurrentModified ? "   Modified   " : "";
        }
        #endregion
        #region Properties
        frmPad CurrentForm {
            get {
                if(this.ActiveMdiChild == null) return null;
                if(xtraTabbedMdiManager1.ActiveFloatForm != null)
                    return xtraTabbedMdiManager1.ActiveFloatForm as frmPad;
                return this.ActiveMdiChild as frmPad;
            }
        }

        public RichTextBox CurrentRichTextBox {
            get {
                if(CurrentForm == null) return null;
                return CurrentForm.RTBMain;
            }
        }

        string CurrentDocName {
            get {
                if(CurrentForm == null) return "";
                return CurrentForm.DocName;
            }
        }

        bool CurrentModified {
            get {
                if(CurrentForm == null) return false;
                return CurrentForm.Modified;
            }
        }
        #endregion
        #region File
        void idNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            CreateNewDocument();
        }

        void iClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if(CurrentForm != null) CurrentForm.Close();
        }

        void OpenFile() {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Rich Text Files (*.rtf)|*.rtf";
            dlg.Title = "Open";
            if(dlg.ShowDialog() == DialogResult.OK) {
                OpenFile(dlg.FileName);
            }
        }

        void OpenFile(string name) {
            CreateNewDocument(name);
            AddToMostRecentFiles(name, arMRUList);
        }

        void iOpen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            OpenFile();
        }

        private void iPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            XtraMessageBox.Show(this, "Note that you can use the XtraPrinting Library to print the contents of the standard RichTextBox control.\r\nFor more information, see the main XtraPrinting demo.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        void iSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            Save();
        }
        void iSaveAs_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            SaveAs();
        }
        void Save() {
            if(CurrentForm == null) return;
            if(CurrentForm.NewDocument) {
                SaveAs();
            }
            else {
                CurrentRichTextBox.SaveFile(CurrentDocName, RichTextBoxStreamType.RichText);
                CurrentForm.Modified = false;
            }
            SetModifiedCaption();
        }
        void SaveAs() {
            if(CurrentForm != null) {
                string s = CurrentForm.SaveAs();
                if(s != string.Empty)
                    AddToMostRecentFiles(s, arMRUList);
                UpdateText();
            }
        }
        private void iExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            Close();
        }
        private void frmMain_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
        }
        private void ribbonPageGroup1_CaptionButtonClick(object sender, DevExpress.XtraBars.Ribbon.RibbonPageGroupEventArgs e) {
            OpenFile();
        }

        private void ribbonPageGroup9_CaptionButtonClick(object sender, DevExpress.XtraBars.Ribbon.RibbonPageGroupEventArgs e) {
            SaveAs();
        }
        #endregion
        #region Format
        private FontStyle rtPadFontStyle() {
            FontStyle fs = new FontStyle();
            if(iBold.Down) fs |= FontStyle.Bold;
            if(iItalic.Down) fs |= FontStyle.Italic;
            if(iUnderline.Down) fs |= FontStyle.Underline;
            return fs;
        }

        private void iBullets_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if(CurrentRichTextBox == null) return;
            CurrentRichTextBox.SelectionBullet = iBullets.Down;
            InitUndo();
        }

        private void iFontStyle_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if(CurrentRichTextBox == null) return;
            CurrentRichTextBox.SelectionFont = new Font(SelectFont, rtPadFontStyle());
        }

        private void iProtected_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if(CurrentRichTextBox == null) return;
            CurrentRichTextBox.SelectionProtected = iProtected.Down;
        }

        private void iAlign_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if(CurrentRichTextBox == null) return;
            if(iAlignLeft.Down)
                CurrentRichTextBox.SelectionAlignment = HorizontalAlignment.Left;
            if(iCenter.Down)
                CurrentRichTextBox.SelectionAlignment = HorizontalAlignment.Center;
            if(iAlignRight.Down)
                CurrentRichTextBox.SelectionAlignment = HorizontalAlignment.Right;
            InitUndo();
        }


        protected Font SelectFont {
            get {
                if(CurrentRichTextBox != null)
                    return CurrentRichTextBox.SelectionFont;
                return null;
            }
        }
        void ShowFontDialog() {
            if(CurrentRichTextBox == null) return;
            Font dialogFont = null;
            if(SelectFont != null)
                dialogFont = (Font)SelectFont.Clone();
            else dialogFont = CurrentRichTextBox.Font;
            XtraFontDialog dlg = new XtraFontDialog(dialogFont);
            if(dlg.ShowDialog() == DialogResult.OK) {
                CurrentRichTextBox.SelectionFont = dlg.ResultFont;
                beiFontSize.EditValue = dlg.ResultFont.Size;
            }
        }
        private void iFont_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            ShowFontDialog();
        }
        private void iFontColor_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if(CurrentRichTextBox == null) return;
            CurrentRichTextBox.SelectionColor = cp.ResultColor;
        }
        #endregion
        #region Edit
        private void iUndo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if(CurrentRichTextBox == null) return;
            CurrentRichTextBox.Undo();
            CurrentForm.Modified = CurrentRichTextBox.CanUndo;
            SetModifiedCaption();
            InitUndo();
            InitFormat();
        }

        private void iCut_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if(CurrentRichTextBox == null) return;
            CurrentRichTextBox.Cut();
            InitPaste();
        }

        private void iCopy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if(CurrentRichTextBox == null) return;
            CurrentRichTextBox.Copy();
            InitPaste();
        }

        private void iPaste_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if(CurrentRichTextBox == null) return;
            CurrentRichTextBox.Paste();
        }

        private void iClear_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if(CurrentRichTextBox == null) return;
            CurrentRichTextBox.SelectedRtf = "";
        }

        private void iSelectAll_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if(CurrentRichTextBox == null) return;
            CurrentRichTextBox.SelectAll();
        }
        private void ribbonPageGroup2_CaptionButtonClick(object sender, DevExpress.XtraBars.Ribbon.RibbonPageGroupEventArgs e) {
            pmMain.ShowPopup(ribbonControl1.Manager, MousePosition);
        }
        #endregion
        #region SkinGallery
        void InitSkinGallery() {
            SimpleButton imageButton = new SimpleButton();
            foreach(SkinContainer cnt in SkinManager.Default.Skins) {
                imageButton.LookAndFeel.SetSkinStyle(cnt.SkinName);
                GalleryItem gItem = new GalleryItem();
                int groupIndex = 0;
                if(cnt.SkinName.Contains("Office")) groupIndex = 1;
                if(DevExpress.DXperience.Demos.LookAndFeelMenu.IsBonusSkin(cnt.SkinName)) groupIndex = 2;
                rgbiSkins.Gallery.Groups[groupIndex].Items.Add(gItem);
                gItem.Caption = cnt.SkinName;

                gItem.Image = GetSkinImage(imageButton, 32, 17, 2);
                gItem.HoverImage = GetSkinImage(imageButton, 70, 36, 5);
                gItem.Caption = cnt.SkinName;
                gItem.Hint = cnt.SkinName;
            }
            rgbiSkins.Gallery.Groups[1].Visible = false;
            rgbiSkins.Gallery.Groups[2].Visible = false;
        }
        Bitmap GetSkinImage(SimpleButton button, int width, int height, int indent) {
            Bitmap image = new Bitmap(width, height);
            using(Graphics g = Graphics.FromImage(image)) {
                StyleObjectInfoArgs info = new StyleObjectInfoArgs(new GraphicsCache(g));
                info.Bounds = new Rectangle(0, 0, width, height);
                button.LookAndFeel.Painter.GroupPanel.DrawObject(info);
                button.LookAndFeel.Painter.Border.DrawObject(info);
                info.Bounds = new Rectangle(indent, indent, width - indent * 2, height - indent * 2);
                button.LookAndFeel.Painter.Button.DrawObject(info);
            }
            return image;
        }
        private void rgbiSkins_Gallery_ItemClick(object sender, DevExpress.XtraBars.Ribbon.GalleryItemClickEventArgs e) {
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(e.Item.Caption);
        }

        private void rgbiSkins_Gallery_InitDropDownGallery(object sender, DevExpress.XtraBars.Ribbon.InplaceGalleryEventArgs e) {
            e.PopupGallery.CreateFrom(rgbiSkins.Gallery);
            e.PopupGallery.AllowFilter = false;
            e.PopupGallery.ShowItemText = true;
            e.PopupGallery.ShowGroupCaption = true;
            e.PopupGallery.AllowHoverImages = false;
            foreach(GalleryItemGroup galleryGroup in e.PopupGallery.Groups)
                foreach(GalleryItem item in galleryGroup.Items)
                    item.Image = item.HoverImage;
            e.PopupGallery.ColumnCount = 2;
            e.PopupGallery.ImageSize = new Size(70, 36);
        }
        #endregion
        #region FontGallery
        Image GetFontImage(int width, int height, string fontName, int fontSize) {
            Rectangle rect = new Rectangle(0, 0, width, height);
            Image fontImage = new Bitmap(width, height);
            try {
                using(Font fontSample = new Font(fontName, fontSize)) {
                    Graphics g = Graphics.FromImage(fontImage);
                    g.FillRectangle(Brushes.White, rect);
                    using(StringFormat fs = new StringFormat()) {
                        fs.Alignment = StringAlignment.Center;
                        fs.LineAlignment = StringAlignment.Center;
                        g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                        g.DrawString("Aa", fontSample, Brushes.Black, rect, fs);
                        g.Dispose();
                    }
                }
            }
            catch { }
            return fontImage;
        }
        void InitFont(GalleryItemGroup groupDropDown, GalleryItemGroup galleryGroup) {
            FontFamily[] fonts = FontFamily.Families;
            for(int i = 0; i < fonts.Length; i++) {
                if(!FontFamily.Families[i].IsStyleAvailable(FontStyle.Regular)) continue;
                string fontName = fonts[i].Name;
                GalleryItem item = new GalleryItem();
                item.Caption = fontName;
                item.Image = GetFontImage(32, 28, fontName, 12);
                item.HoverImage = item.Image;
                item.Description = fontName;
                item.Hint = fontName;
                try {
                    item.Tag = new Font(fontName, 9);
                    if(DevExpress.Utils.ControlUtils.IsSymbolFont((Font)item.Tag)) {
                        item.Tag = new Font(DevExpress.Utils.AppearanceObject.DefaultFont.FontFamily, 9);
                        item.Description += " (Symbol Font)";
                    }
                }
                catch {
                    continue;
                }
                groupDropDown.Items.Add(item);
                galleryGroup.Items.Add(item);
            }
        }
        void InitFontGallery() {
            InitFont(gddFont.Gallery.Groups[0], rgbiFont.Gallery.Groups[0]);
            beiFontSize.EditValue = 8;
        }
        void SetFont(string fontName, GalleryItem item) {
            if(CurrentRichTextBox == null) return;
            CurrentRichTextBox.SelectionFont = new Font(fontName, Convert.ToInt32(beiFontSize.EditValue), rtPadFontStyle());
            if(item != null) CurrentFontItem = item;
        }
        private void gddFont_Gallery_ItemClick(object sender, DevExpress.XtraBars.Ribbon.GalleryItemClickEventArgs e) {
            SetFont(e.Item.Caption, e.Item);
        }
        private void rpgFont_CaptionButtonClick(object sender, DevExpress.XtraBars.Ribbon.RibbonPageGroupEventArgs e) {
            ShowFontDialog();
        }
        private void rgbiFont_Gallery_ItemClick(object sender, DevExpress.XtraBars.Ribbon.GalleryItemClickEventArgs e) {
            SetFont(e.Item.Caption, e.Item);
        }
        private void gddFont_Gallery_CustomDrawItemText(object sender, GalleryItemCustomDrawEventArgs e) {
            DevExpress.XtraBars.Ribbon.ViewInfo.GalleryItemViewInfo itemInfo = e.ItemInfo as DevExpress.XtraBars.Ribbon.ViewInfo.GalleryItemViewInfo;
            itemInfo.PaintAppearance.ItemDescription.DrawString(e.Cache, e.Item.Description, itemInfo.DescriptionBounds);
            AppearanceObject app = itemInfo.PaintAppearance.ItemCaption.Clone() as AppearanceObject;
            app.Font = (Font)e.Item.Tag;
            try {
                e.Cache.Graphics.DrawString(e.Item.Caption, app.Font, app.GetForeBrush(e.Cache), itemInfo.CaptionBounds);
            }
            catch { }
            e.Handled = true;
        }
        #endregion
        #region ColorGallery
        void InitColorGallery() {
            gddFontColor.BeginUpdate();
            foreach(Color color in DevExpress.XtraEditors.Popup.ColorListBoxViewInfo.WebColors) {
                if(color == Color.Transparent) continue;
                GalleryItem item = new GalleryItem();
                item.Caption = color.Name;
                item.Tag = color;
                item.Hint = color.Name;
                gddFontColor.Gallery.Groups[0].Items.Add(item);
                rgbiFontColor.Gallery.Groups[0].Items.Add(item);
            }
            foreach(Color color in DevExpress.XtraEditors.Popup.ColorListBoxViewInfo.SystemColors) {
                GalleryItem item = new GalleryItem();
                item.Caption = color.Name;
                item.Tag = color;
                gddFontColor.Gallery.Groups[1].Items.Add(item);
            }
            gddFontColor.EndUpdate();
        }
        private void gddFontColor_Gallery_CustomDrawItemImage(object sender, GalleryItemCustomDrawEventArgs e) {
            Color clr = (Color)e.Item.Tag;
            using(Brush brush = new SolidBrush(clr)) {
                e.Cache.FillRectangle(brush, e.Bounds);
                e.Handled = true;
            }
        }
        void SetResultColor(Color color, GalleryItem item) {
            if(CurrentRichTextBox == null) return;
            cp.ResultColor = color;
            CurrentRichTextBox.SelectionColor = cp.ResultColor;
            if(item != null) CurrentColorItem = item;
        }
        private void gddFontColor_Gallery_ItemClick(object sender, DevExpress.XtraBars.Ribbon.GalleryItemClickEventArgs e) {
            SetResultColor((Color)e.Item.Tag, e.Item);
        }
        private void rpgFontColor_CaptionButtonClick(object sender, DevExpress.XtraBars.Ribbon.RibbonPageGroupEventArgs e) {
            if(CurrentRichTextBox == null) return;
            if(cp == null)
                CreateColorPopup(popupControlContainer1);
            popupControlContainer1.ShowPopup(ribbonControl1.Manager, MousePosition);
        }

        private void rgbiFontColor_Gallery_ItemClick(object sender, DevExpress.XtraBars.Ribbon.GalleryItemClickEventArgs e) {
            SetResultColor((Color)e.Item.Tag, e.Item);
        }
        #endregion

        private void iFind_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if(CurrentRichTextBox == null) return;
            if(dlgReplace != null) dlgReplace.Close();
            if(dlgFind != null) dlgFind.Close();
            dlgFind = new frmFind(CurrentRichTextBox, Bounds);
            AddOwnedForm(dlgFind);
            dlgFind.Show();
        }

        private void iReplace_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            if(CurrentRichTextBox == null) return;
            if(dlgReplace != null) dlgReplace.Close();
            if(dlgFind != null) dlgFind.Close();
            dlgReplace = new frmReplace(CurrentRichTextBox, Bounds);
            AddOwnedForm(dlgReplace);
            dlgReplace.Show();
        }

        private void iWeb_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            Process process = new Process();
            process.StartInfo.FileName = "http://www.devexpress.com";
            process.StartInfo.Verb = "Open";
            process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            process.Start();
        }

        private void iAbout_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            DevExpress.Utils.About.frmAbout dlg = new DevExpress.Utils.About.frmAbout("Ribbon Demo for the XtraBars by Developer Express Inc.");
            dlg.ShowDialog();
        }

        string TextByCaption(string caption) {
            return caption.Replace("&", "");
        }

        private void frmMain_Load(object sender, System.EventArgs e) {
            arMRUList = new MRUArrayList(pcAppMenuFileLabels, imageCollection3.Images[0], imageCollection3.Images[1]);
            arMRUList.LabelClicked += new EventHandler(OnLabelClicked);
            InitMostRecentFiles(arMRUList);
            ribbonControl1.ForceInitialize();
            foreach(DevExpress.Skins.SkinContainer skin in DevExpress.Skins.SkinManager.Default.Skins) {
                BarCheckItem item = ribbonControl1.Items.CreateCheckItem(skin.SkinName, false);
                item.Tag = skin.SkinName;
                item.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(OnPaintStyleClick);
                iPaintStyle.ItemLinks.Add(item);
            }
            CreateNewDocument();
            barEditItem1.EditValue = (Bitmap)DevExpress.Utils.ResourceImageHelper.CreateImageFromResources("RibbonSimplePad.online.gif", typeof(frmMain).Assembly);
        }

        void OnPaintStyleClick(object sender, ItemClickEventArgs e) {
            defaultLookAndFeel1.LookAndFeel.SetSkinStyle(e.Item.Tag.ToString());
        }

        private void iPaintStyle_Popup(object sender, System.EventArgs e) {
            foreach(BarItemLink link in iPaintStyle.ItemLinks)
                ((BarCheckItem)link.Item).Checked = link.Item.Caption == defaultLookAndFeel1.LookAndFeel.ActiveSkinName;
        }
        #region GalleryItemsChecked

        GalleryItem GetColorItemByColor(Color color, BaseGallery gallery) {
            foreach(GalleryItemGroup galleryGroup in gallery.Groups)
                foreach(GalleryItem item in galleryGroup.Items)
                    if(item.Caption == color.Name)
                        return item;
            return null;
        }
        GalleryItem GetFontItemByFont(string fontName, BaseGallery gallery) {
            foreach(GalleryItemGroup galleryGroup in gallery.Groups)
                foreach(GalleryItem item in galleryGroup.Items)
                    if(item.Caption == fontName)
                        return item;
            return null;
        }
        GalleryItem CurrentFontItem {
            get { return fCurrentFontItem; }
            set {
                if(fCurrentFontItem == value) return;
                if(fCurrentFontItem != null) fCurrentFontItem.Checked = false;
                fCurrentFontItem = value;
                if(fCurrentFontItem != null) {
                    fCurrentFontItem.Checked = true;
                    MakeFontVisible(fCurrentFontItem);
                }
            }
        }
        void MakeFontVisible(GalleryItem item) {
            gddFont.Gallery.MakeVisible(fCurrentFontItem);
            rgbiFont.Gallery.MakeVisible(fCurrentFontItem);
        }
        GalleryItem CurrentColorItem {
            get { return fCurrentColorItem; }
            set {
                if(fCurrentColorItem == value) return;
                if(fCurrentColorItem != null) fCurrentColorItem.Checked = false;
                fCurrentColorItem = value;
                if(fCurrentColorItem != null) {
                    fCurrentColorItem.Checked = true;
                    MakeColorVisible(fCurrentColorItem);
                }
            }
        }
        void MakeColorVisible(GalleryItem item) {
            gddFontColor.Gallery.MakeVisible(fCurrentColorItem);
            rgbiFontColor.Gallery.MakeVisible(fCurrentColorItem);
        }
        void CurrentFontChanged() {
            if(CurrentRichTextBox == null || CurrentRichTextBox.SelectionFont == null) return;
            CurrentFontItem = GetFontItemByFont(CurrentRichTextBox.SelectionFont.Name, rgbiFont.Gallery);
            CurrentColorItem = GetColorItemByColor(CurrentRichTextBox.SelectionColor, rgbiFontColor.Gallery);
        }
        private void gddFont_Popup(object sender, System.EventArgs e) {
            MakeFontVisible(CurrentFontItem);
        }

        private void gddFontColor_Popup(object sender, System.EventArgs e) {
            MakeColorVisible(CurrentColorItem);
        }
        #endregion
        #region MostRecentFiles
        MRUArrayList arMRUList = null;
        string mrfFileName = "RibbonMRUFiles.ini";
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e) {
            SaveMostRecentFiles(arMRUList);
        }
        void InitMostRecentFiles(MRUArrayList arList) {
            string fileName = Application.StartupPath + "\\" + mrfFileName;
            if(!System.IO.File.Exists(fileName)) {
                AddToMostRecentFiles("Document1.rtf", arList);
                return;
            }
            System.IO.StreamReader sr = System.IO.File.OpenText(fileName);
            for(string s = sr.ReadLine(); s != null; s = sr.ReadLine())
                AddToMostRecentFiles(s, arList);
            sr.Close();
        }

        void SaveMostRecentFiles(MRUArrayList arList) {
            try {
                System.IO.StreamWriter sw = System.IO.File.CreateText(Application.StartupPath + "\\" + mrfFileName);
                for(int i = arList.Count - 1; i >= 0; i--) sw.WriteLine(string.Format("{0},{1}", arList[i].ToString(), arList.GetLabelChecked(arList[i].ToString())));
                sw.Close();
            }
            catch { }
        }

        void AddToMostRecentFiles(string name, MRUArrayList arList) {
            arList.InsertElement(name);
        }
        void OnLabelClicked(object sender, EventArgs e) {
            pmAppMain.HidePopup();
            this.Refresh();
            OpenFile(sender.ToString());
        }
        class MRUArrayList : ArrayList {
            PanelControl container;
            int maxRecentFiles = 9;
            Image imgChecked, imgUncheked;
            public event EventHandler LabelClicked;
            public MRUArrayList(PanelControl cont, Image iChecked, Image iUnchecked)
                : base() {
                this.imgChecked = iChecked;
                this.imgUncheked = iUnchecked;
                this.container = cont;
            }
            public void InsertElement(object value) {
                string[] names = value.ToString().Split(',');
                string name = names[0];
                bool checkedLabel = false;
                if(names.Length > 1) checkedLabel = names[1].ToLower().Equals("true");
                foreach(AppMenuFileLabel ml in container.Controls) {
                    if(ml.Tag.Equals(name)) {
                        checkedLabel = ml.Checked;
                        base.Remove(name);
                        ml.LabelClick -= new EventHandler(OnLabelClick);
                        ml.Dispose();
                        break;
                    }
                }
                bool access = true;
                if(base.Count >= maxRecentFiles)
                    access = RemoveLastElement();
                if(access) {
                    base.Insert(0, name);
                    AppMenuFileLabel ml = new AppMenuFileLabel();
                    container.Controls.Add(ml);
                    ml.Tag = name;
                    ml.Text = GetFileName(name);
                    ml.Checked = checkedLabel;
                    ml.AutoHeight = true;
                    ml.Dock = DockStyle.Top;
                    ml.Image = imgUncheked;
                    ml.SelectedImage = imgChecked;
                    ml.LabelClick += new EventHandler(OnLabelClick);
                    SetElementsRange();
                }
            }
            void OnLabelClick(object sender, EventArgs e) {
                if(LabelClicked != null)
                    LabelClicked(((AppMenuFileLabel)sender).Tag.ToString(), e);
            }
            public bool RemoveLastElement() {
                for(int i = 0; i < container.Controls.Count; i++) {
                    AppMenuFileLabel ml = container.Controls[i] as AppMenuFileLabel;
                    if(!ml.Checked) {
                        base.Remove(ml.Tag);
                        ml.LabelClick -= new EventHandler(OnLabelClick);
                        ml.Dispose();
                        return true;
                    }
                }
                return false;
            }
            string GetFileName(object obj) {
                FileInfo fi = new FileInfo(obj.ToString());
                return fi.Name;
            }
            void SetElementsRange() {
                int i = 0;
                foreach(AppMenuFileLabel ml in container.Controls) {
                    ml.Caption = string.Format("&{0}", container.Controls.Count - i);
                    i++;
                }
            }
            public bool GetLabelChecked(string name) {
                foreach(AppMenuFileLabel ml in container.Controls) {
                    if(ml.Tag.Equals(name)) return ml.Checked;
                }
                return false;
            }
        }
        #endregion

        private void ribbonControl1_ApplicationButtonDoubleClick(object sender, EventArgs e) {
            if(ribbonControl1.RibbonStyle == RibbonControlStyle.Office2007)
                this.Close();
        }

        private void barEditItem1_ItemPress(object sender, ItemClickEventArgs e) {
            System.Diagnostics.Process.Start("http://www.devexpress.com");
        }

        private void biStyle_EditValueChanged(object sender, EventArgs e) {
            ribbonControl1.RibbonStyle = (RibbonControlStyle)biStyle.EditValue;
        }
    }
}
