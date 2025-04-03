namespace PhonemeMachine
{
    partial class IndexUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IndexUI));
            this.IndexUIFrame = new System.Windows.Forms.TableLayoutPanel();
            this.ComboBox_LanguageSelect = new System.Windows.Forms.ComboBox();
            this.PictureBox_IndexBanner = new System.Windows.Forms.PictureBox();
            this.Button_Start = new System.Windows.Forms.Button();
            this.IndexUIFrame.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_IndexBanner)).BeginInit();
            this.SuspendLayout();
            // 
            // IndexUIFrame
            // 
            this.IndexUIFrame.ColumnCount = 1;
            this.IndexUIFrame.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.IndexUIFrame.Controls.Add(this.PictureBox_IndexBanner, 0, 0);
            this.IndexUIFrame.Controls.Add(this.ComboBox_LanguageSelect, 0, 2);
            this.IndexUIFrame.Controls.Add(this.Button_Start, 0, 3);
            this.IndexUIFrame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.IndexUIFrame.Location = new System.Drawing.Point(0, 0);
            this.IndexUIFrame.Name = "IndexUIFrame";
            this.IndexUIFrame.RowCount = 4;
            this.IndexUIFrame.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.IndexUIFrame.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.IndexUIFrame.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.IndexUIFrame.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.IndexUIFrame.Size = new System.Drawing.Size(1242, 713);
            this.IndexUIFrame.TabIndex = 0;
            // 
            // ComboBox_LanguageSelect
            // 
            this.ComboBox_LanguageSelect.FormattingEnabled = true;
            this.ComboBox_LanguageSelect.Location = new System.Drawing.Point(3, 430);
            this.ComboBox_LanguageSelect.Name = "ComboBox_LanguageSelect";
            this.ComboBox_LanguageSelect.Size = new System.Drawing.Size(121, 32);
            this.ComboBox_LanguageSelect.TabIndex = 1;
            // 
            // PictureBox_IndexBanner
            // 
            this.PictureBox_IndexBanner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PictureBox_IndexBanner.Image = global::PhonemeMachine.Properties.Resources.IndexBanner;
            this.PictureBox_IndexBanner.InitialImage = null;
            this.PictureBox_IndexBanner.Location = new System.Drawing.Point(3, 3);
            this.PictureBox_IndexBanner.Name = "PictureBox_IndexBanner";
            this.PictureBox_IndexBanner.Size = new System.Drawing.Size(1236, 386);
            this.PictureBox_IndexBanner.TabIndex = 0;
            this.PictureBox_IndexBanner.TabStop = false;
            // 
            // Button_Start
            // 
            this.Button_Start.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.Button_Start.ForeColor = System.Drawing.SystemColors.Info;
            this.Button_Start.Image = ((System.Drawing.Image)(resources.GetObject("Button_Start.Image")));
            this.Button_Start.Location = new System.Drawing.Point(3, 536);
            this.Button_Start.Name = "Button_Start";
            this.Button_Start.Size = new System.Drawing.Size(291, 100);
            this.Button_Start.TabIndex = 2;
            this.Button_Start.Text = " ";
            this.Button_Start.UseVisualStyleBackColor = false;
            this.Button_Start.Click += new System.EventHandler(this.Button_Start_Click);
            // 
            // IndexUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1242, 713);
            this.Controls.Add(this.IndexUIFrame);
            this.Name = "IndexUI";
            this.Text = "IndexUI";
            this.Load += new System.EventHandler(this.IndexUI_Load);
            this.IndexUIFrame.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_IndexBanner)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel IndexUIFrame;
        private System.Windows.Forms.PictureBox PictureBox_IndexBanner;
        private System.Windows.Forms.ComboBox ComboBox_LanguageSelect;
        private System.Windows.Forms.Button Button_Start;
    }
}