namespace NCR_SYSTEM_1
{
    partial class StockAdjustmentRemarks_popup
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
            this.panel6 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.bunifuImageButton1 = new Bunifu.Framework.UI.BunifuImageButton();
            this.remarktxt = new WindowsFormsControlLibrary1.BunifuCustomTextbox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
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
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(112)))), ((int)(((byte)(112)))));
            this.panel6.Location = new System.Drawing.Point(35, 92);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(329, 1);
            this.panel6.TabIndex = 337;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(31, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 23);
            this.label1.TabIndex = 336;
            this.label1.Text = "View Remarks";
            // 
            // bunifuImageButton1
            // 
            this.bunifuImageButton1.BackColor = System.Drawing.Color.Transparent;
            this.bunifuImageButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.bunifuImageButton1.Image = global::NCR_SYSTEM_1.Properties.Resources.icons8_left_96px_1;
            this.bunifuImageButton1.ImageActive = null;
            this.bunifuImageButton1.Location = new System.Drawing.Point(35, 20);
            this.bunifuImageButton1.Name = "bunifuImageButton1";
            this.bunifuImageButton1.Size = new System.Drawing.Size(32, 32);
            this.bunifuImageButton1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.bunifuImageButton1.TabIndex = 334;
            this.bunifuImageButton1.TabStop = false;
            this.bunifuImageButton1.Zoom = 10;
            this.bunifuImageButton1.Click += new System.EventHandler(this.bunifuImageButton1_Click);
            // 
            // remarktxt
            // 
            this.remarktxt.BorderColor = System.Drawing.Color.SeaGreen;
            this.remarktxt.Enabled = false;
            this.remarktxt.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.remarktxt.Location = new System.Drawing.Point(35, 110);
            this.remarktxt.Multiline = true;
            this.remarktxt.Name = "remarktxt";
            this.remarktxt.Size = new System.Drawing.Size(329, 296);
            this.remarktxt.TabIndex = 338;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(95)))), ((int)(((byte)(190)))));
            this.panel2.Location = new System.Drawing.Point(403, -6);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(27, 457);
            this.panel2.TabIndex = 339;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(95)))), ((int)(((byte)(190)))));
            this.panel1.Location = new System.Drawing.Point(-25, -2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(27, 457);
            this.panel1.TabIndex = 340;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(95)))), ((int)(((byte)(190)))));
            this.panel3.Location = new System.Drawing.Point(-5, 429);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(444, 43);
            this.panel3.TabIndex = 340;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(95)))), ((int)(((byte)(190)))));
            this.panel4.Location = new System.Drawing.Point(-9, -19);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(449, 21);
            this.panel4.TabIndex = 341;
            // 
            // StockAdjustmentRemarks_popup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(406, 432);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.remarktxt);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bunifuImageButton1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "StockAdjustmentRemarks_popup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "StockAdjustmentRemarks_popup";
            this.Load += new System.EventHandler(this.StockAdjustmentRemarks_popup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bunifuImageButton1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Bunifu.Framework.UI.BunifuElipse bunifuElipse1;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label1;
        private Bunifu.Framework.UI.BunifuImageButton bunifuImageButton1;
        private WindowsFormsControlLibrary1.BunifuCustomTextbox remarktxt;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
    }
}