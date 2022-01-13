namespace NCR_SYSTEM_1
{
    partial class Deductstock_popup
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
            this.components = new System.ComponentModel.Container();
            this.bunifuElipse1 = new Bunifu.Framework.UI.BunifuElipse(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.deductedstocktxt = new Bunifu.Framework.UI.BunifuMetroTextbox();
            this.reasontxt = new System.Windows.Forms.ComboBox();
            this.remarktxt = new WindowsFormsControlLibrary1.BunifuCustomTextbox();
            this.bunifuFlatButton2 = new Bunifu.Framework.UI.BunifuFlatButton();
            this.Finalize_Button = new Bunifu.Framework.UI.BunifuFlatButton();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.currentstocktxt = new Bunifu.Framework.UI.BunifuMetroTextbox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.productnametxt = new Bunifu.Framework.UI.BunifuMetroTextbox();
            this.bunifuImageButton1 = new Bunifu.Framework.UI.BunifuImageButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton1)).BeginInit();
            this.SuspendLayout();
            // 
            // bunifuElipse1
            // 
            this.bunifuElipse1.ElipseRadius = 5;
            this.bunifuElipse1.TargetControl = this;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(27, 264);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 15);
            this.label4.TabIndex = 364;
            this.label4.Text = "Stock Deducted:";
            // 
            // deductedstocktxt
            // 
            this.deductedstocktxt.BorderColorFocused = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(129)))), ((int)(((byte)(255)))));
            this.deductedstocktxt.BorderColorIdle = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.deductedstocktxt.BorderColorMouseHover = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(129)))), ((int)(((byte)(255)))));
            this.deductedstocktxt.BorderThickness = 1;
            this.deductedstocktxt.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.deductedstocktxt.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.deductedstocktxt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.deductedstocktxt.isPassword = false;
            this.deductedstocktxt.Location = new System.Drawing.Point(30, 292);
            this.deductedstocktxt.Margin = new System.Windows.Forms.Padding(4);
            this.deductedstocktxt.Name = "deductedstocktxt";
            this.deductedstocktxt.Size = new System.Drawing.Size(310, 36);
            this.deductedstocktxt.TabIndex = 363;
            this.deductedstocktxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.deductedstocktxt.Enter += new System.EventHandler(this.deductedstocktxt_Enter);
            this.deductedstocktxt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.deductedstocktxt_KeyPress);
            this.deductedstocktxt.Leave += new System.EventHandler(this.deductedstocktxt_Leave);
            // 
            // reasontxt
            // 
            this.reasontxt.DisplayMember = "0";
            this.reasontxt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.reasontxt.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.reasontxt.FormattingEnabled = true;
            this.reasontxt.Items.AddRange(new object[] {
            "Wrong Input",
            "Lost Item",
            "Damaged Item",
            "Replacement"});
            this.reasontxt.Location = new System.Drawing.Point(30, 377);
            this.reasontxt.Name = "reasontxt";
            this.reasontxt.Size = new System.Drawing.Size(310, 25);
            this.reasontxt.TabIndex = 362;
            // 
            // remarktxt
            // 
            this.remarktxt.BorderColor = System.Drawing.Color.SeaGreen;
            this.remarktxt.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.remarktxt.Location = new System.Drawing.Point(30, 444);
            this.remarktxt.Multiline = true;
            this.remarktxt.Name = "remarktxt";
            this.remarktxt.Size = new System.Drawing.Size(310, 158);
            this.remarktxt.TabIndex = 361;
            // 
            // bunifuFlatButton2
            // 
            this.bunifuFlatButton2.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(95)))), ((int)(((byte)(190)))));
            this.bunifuFlatButton2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(129)))), ((int)(((byte)(255)))));
            this.bunifuFlatButton2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bunifuFlatButton2.BorderRadius = 0;
            this.bunifuFlatButton2.ButtonText = "Cancel";
            this.bunifuFlatButton2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bunifuFlatButton2.DisabledColor = System.Drawing.Color.Gray;
            this.bunifuFlatButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuFlatButton2.Iconcolor = System.Drawing.Color.Transparent;
            this.bunifuFlatButton2.Iconimage = null;
            this.bunifuFlatButton2.Iconimage_right = null;
            this.bunifuFlatButton2.Iconimage_right_Selected = null;
            this.bunifuFlatButton2.Iconimage_Selected = null;
            this.bunifuFlatButton2.IconMarginLeft = 0;
            this.bunifuFlatButton2.IconMarginRight = 0;
            this.bunifuFlatButton2.IconRightVisible = true;
            this.bunifuFlatButton2.IconRightZoom = 0D;
            this.bunifuFlatButton2.IconVisible = true;
            this.bunifuFlatButton2.IconZoom = 90D;
            this.bunifuFlatButton2.IsTab = false;
            this.bunifuFlatButton2.Location = new System.Drawing.Point(30, 620);
            this.bunifuFlatButton2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.bunifuFlatButton2.Name = "bunifuFlatButton2";
            this.bunifuFlatButton2.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(129)))), ((int)(((byte)(255)))));
            this.bunifuFlatButton2.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(95)))), ((int)(((byte)(190)))));
            this.bunifuFlatButton2.OnHoverTextColor = System.Drawing.Color.White;
            this.bunifuFlatButton2.selected = false;
            this.bunifuFlatButton2.Size = new System.Drawing.Size(154, 35);
            this.bunifuFlatButton2.TabIndex = 360;
            this.bunifuFlatButton2.Text = "Cancel";
            this.bunifuFlatButton2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.bunifuFlatButton2.Textcolor = System.Drawing.Color.White;
            this.bunifuFlatButton2.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuFlatButton2.Click += new System.EventHandler(this.bunifuFlatButton2_Click);
            // 
            // Finalize_Button
            // 
            this.Finalize_Button.Activecolor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(95)))), ((int)(((byte)(190)))));
            this.Finalize_Button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(89)))), ((int)(((byte)(204)))));
            this.Finalize_Button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Finalize_Button.BorderRadius = 0;
            this.Finalize_Button.ButtonText = "Confirm";
            this.Finalize_Button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Finalize_Button.DisabledColor = System.Drawing.Color.Gray;
            this.Finalize_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Finalize_Button.Iconcolor = System.Drawing.Color.Transparent;
            this.Finalize_Button.Iconimage = null;
            this.Finalize_Button.Iconimage_right = null;
            this.Finalize_Button.Iconimage_right_Selected = null;
            this.Finalize_Button.Iconimage_Selected = null;
            this.Finalize_Button.IconMarginLeft = 0;
            this.Finalize_Button.IconMarginRight = 0;
            this.Finalize_Button.IconRightVisible = true;
            this.Finalize_Button.IconRightZoom = 0D;
            this.Finalize_Button.IconVisible = true;
            this.Finalize_Button.IconZoom = 90D;
            this.Finalize_Button.IsTab = false;
            this.Finalize_Button.Location = new System.Drawing.Point(186, 620);
            this.Finalize_Button.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Finalize_Button.Name = "Finalize_Button";
            this.Finalize_Button.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(89)))), ((int)(((byte)(204)))));
            this.Finalize_Button.OnHovercolor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(95)))), ((int)(((byte)(190)))));
            this.Finalize_Button.OnHoverTextColor = System.Drawing.Color.White;
            this.Finalize_Button.selected = false;
            this.Finalize_Button.Size = new System.Drawing.Size(154, 35);
            this.Finalize_Button.TabIndex = 359;
            this.Finalize_Button.Text = "Confirm";
            this.Finalize_Button.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Finalize_Button.Textcolor = System.Drawing.Color.White;
            this.Finalize_Button.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Finalize_Button.Click += new System.EventHandler(this.Finalize_Button_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(27, 349);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 15);
            this.label6.TabIndex = 358;
            this.label6.Text = "Reason:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(27, 416);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 15);
            this.label3.TabIndex = 357;
            this.label3.Text = "Remarks:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(27, 185);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 15);
            this.label2.TabIndex = 356;
            this.label2.Text = "Current Stock:";
            // 
            // currentstocktxt
            // 
            this.currentstocktxt.BorderColorFocused = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(129)))), ((int)(((byte)(255)))));
            this.currentstocktxt.BorderColorIdle = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.currentstocktxt.BorderColorMouseHover = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(129)))), ((int)(((byte)(255)))));
            this.currentstocktxt.BorderThickness = 1;
            this.currentstocktxt.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.currentstocktxt.Enabled = false;
            this.currentstocktxt.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.currentstocktxt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.currentstocktxt.isPassword = false;
            this.currentstocktxt.Location = new System.Drawing.Point(30, 213);
            this.currentstocktxt.Margin = new System.Windows.Forms.Padding(4);
            this.currentstocktxt.Name = "currentstocktxt";
            this.currentstocktxt.Size = new System.Drawing.Size(310, 36);
            this.currentstocktxt.TabIndex = 355;
            this.currentstocktxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(112)))), ((int)(((byte)(112)))));
            this.panel6.Location = new System.Drawing.Point(30, 89);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(310, 1);
            this.panel6.TabIndex = 354;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(26, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(145, 23);
            this.label1.TabIndex = 353;
            this.label1.Text = "Stock Adjustment";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(27, 106);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 15);
            this.label5.TabIndex = 352;
            this.label5.Text = "Product Name:";
            // 
            // productnametxt
            // 
            this.productnametxt.BorderColorFocused = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(129)))), ((int)(((byte)(255)))));
            this.productnametxt.BorderColorIdle = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.productnametxt.BorderColorMouseHover = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(129)))), ((int)(((byte)(255)))));
            this.productnametxt.BorderThickness = 1;
            this.productnametxt.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.productnametxt.Enabled = false;
            this.productnametxt.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.productnametxt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.productnametxt.isPassword = false;
            this.productnametxt.Location = new System.Drawing.Point(30, 134);
            this.productnametxt.Margin = new System.Windows.Forms.Padding(4);
            this.productnametxt.Name = "productnametxt";
            this.productnametxt.Size = new System.Drawing.Size(310, 36);
            this.productnametxt.TabIndex = 351;
            this.productnametxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // bunifuImageButton1
            // 
            this.bunifuImageButton1.BackColor = System.Drawing.Color.Transparent;
            this.bunifuImageButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.bunifuImageButton1.Image = global::NCR_SYSTEM_1.Properties.Resources.icons8_left_96px_1;
            this.bunifuImageButton1.ImageActive = null;
            this.bunifuImageButton1.Location = new System.Drawing.Point(30, 17);
            this.bunifuImageButton1.Name = "bunifuImageButton1";
            this.bunifuImageButton1.Size = new System.Drawing.Size(32, 32);
            this.bunifuImageButton1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.bunifuImageButton1.TabIndex = 350;
            this.bunifuImageButton1.TabStop = false;
            this.bunifuImageButton1.Zoom = 10;
            this.bunifuImageButton1.Click += new System.EventHandler(this.bunifuImageButton1_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(95)))), ((int)(((byte)(190)))));
            this.panel1.Location = new System.Drawing.Point(366, -5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(16, 690);
            this.panel1.TabIndex = 365;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(95)))), ((int)(((byte)(190)))));
            this.panel2.Location = new System.Drawing.Point(-14, -2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(16, 690);
            this.panel2.TabIndex = 366;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(95)))), ((int)(((byte)(190)))));
            this.panel3.Location = new System.Drawing.Point(-11, -14);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(392, 16);
            this.panel3.TabIndex = 367;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(95)))), ((int)(((byte)(190)))));
            this.panel4.Location = new System.Drawing.Point(-8, 676);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(392, 16);
            this.panel4.TabIndex = 368;
            // 
            // Deductstock_popup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(369, 679);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.deductedstocktxt);
            this.Controls.Add(this.reasontxt);
            this.Controls.Add(this.remarktxt);
            this.Controls.Add(this.bunifuFlatButton2);
            this.Controls.Add(this.Finalize_Button);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.currentstocktxt);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.productnametxt);
            this.Controls.Add(this.bunifuImageButton1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Deductstock_popup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Deductstock_popup";
            this.Load += new System.EventHandler(this.Deductstock_popup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Bunifu.Framework.UI.BunifuElipse bunifuElipse1;
        private System.Windows.Forms.Label label4;
        private Bunifu.Framework.UI.BunifuMetroTextbox deductedstocktxt;
        private System.Windows.Forms.ComboBox reasontxt;
        private WindowsFormsControlLibrary1.BunifuCustomTextbox remarktxt;
        private Bunifu.Framework.UI.BunifuFlatButton bunifuFlatButton2;
        private Bunifu.Framework.UI.BunifuFlatButton Finalize_Button;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private Bunifu.Framework.UI.BunifuMetroTextbox currentstocktxt;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private Bunifu.Framework.UI.BunifuMetroTextbox productnametxt;
        private Bunifu.Framework.UI.BunifuImageButton bunifuImageButton1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
    }
}