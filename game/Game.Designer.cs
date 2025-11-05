namespace game
{
    partial class Game
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.gHeader = new System.Windows.Forms.GroupBox();
            this.gTimerLbl = new System.Windows.Forms.Label();
            this.gTimerLbl2 = new System.Windows.Forms.Label();
            this.gTimerPict = new System.Windows.Forms.PictureBox();
            this.gHeaderTitle = new System.Windows.Forms.Label();
            this.gHeaderBack = new System.Windows.Forms.Label();
            this.gRBar = new System.Windows.Forms.GroupBox();
            this.btnNext = new System.Windows.Forms.Button();
            this.gRBarForm = new System.Windows.Forms.GroupBox();
            this.gRBarFormBack = new System.Windows.Forms.Label();
            this.gRBarFormBorder = new System.Windows.Forms.Label();
            this.clOptions = new System.Windows.Forms.CheckedListBox();
            this.gRBarBack = new System.Windows.Forms.Label();
            this.gImage = new System.Windows.Forms.PictureBox();
            this.gCenter = new System.Windows.Forms.GroupBox();
            this.gImageDesc = new System.Windows.Forms.Label();
            this.gCenterBack = new System.Windows.Forms.Label();
            this.gHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gTimerPict)).BeginInit();
            this.gRBar.SuspendLayout();
            this.gRBarForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gImage)).BeginInit();
            this.gCenter.SuspendLayout();
            this.SuspendLayout();
            // 
            // gHeader
            // 
            this.gHeader.Controls.Add(this.gTimerLbl);
            this.gHeader.Controls.Add(this.gTimerLbl2);
            this.gHeader.Controls.Add(this.gTimerPict);
            this.gHeader.Controls.Add(this.gHeaderTitle);
            this.gHeader.Controls.Add(this.gHeaderBack);
            this.gHeader.Location = new System.Drawing.Point(1, 10);
            this.gHeader.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gHeader.Name = "gHeader";
            this.gHeader.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gHeader.Size = new System.Drawing.Size(960, 88);
            this.gHeader.TabIndex = 0;
            this.gHeader.TabStop = false;
            // 
            // gTimerLbl
            // 
            this.gTimerLbl.Location = new System.Drawing.Point(824, 58);
            this.gTimerLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.gTimerLbl.Name = "gTimerLbl";
            this.gTimerLbl.Size = new System.Drawing.Size(75, 19);
            this.gTimerLbl.TabIndex = 2;
            this.gTimerLbl.Text = "label1";
            // 
            // gTimerLbl2
            // 
            this.gTimerLbl2.Location = new System.Drawing.Point(838, 36);
            this.gTimerLbl2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.gTimerLbl2.Name = "gTimerLbl2";
            this.gTimerLbl2.Size = new System.Drawing.Size(75, 19);
            this.gTimerLbl2.TabIndex = 0;
            this.gTimerLbl2.Text = "label1";
            // 
            // gTimerPict
            // 
            this.gTimerPict.Location = new System.Drawing.Point(632, 21);
            this.gTimerPict.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gTimerPict.Name = "gTimerPict";
            this.gTimerPict.Size = new System.Drawing.Size(75, 41);
            this.gTimerPict.TabIndex = 1;
            this.gTimerPict.TabStop = false;
            // 
            // gHeaderTitle
            // 
            this.gHeaderTitle.BackColor = System.Drawing.Color.Transparent;
            this.gHeaderTitle.Location = new System.Drawing.Point(124, 49);
            this.gHeaderTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.gHeaderTitle.Name = "gHeaderTitle";
            this.gHeaderTitle.Size = new System.Drawing.Size(33, 13);
            this.gHeaderTitle.TabIndex = 3;
            this.gHeaderTitle.Text = "label1";
            // 
            // gHeaderBack
            // 
            this.gHeaderBack.Location = new System.Drawing.Point(28, 20);
            this.gHeaderBack.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.gHeaderBack.Name = "gHeaderBack";
            this.gHeaderBack.Size = new System.Drawing.Size(75, 19);
            this.gHeaderBack.TabIndex = 0;
            this.gHeaderBack.Text = "label1";
            // 
            // gRBar
            // 
            this.gRBar.Controls.Add(this.btnNext);
            this.gRBar.Controls.Add(this.gRBarForm);
            this.gRBar.Controls.Add(this.clOptions);
            this.gRBar.Controls.Add(this.gRBarBack);
            this.gRBar.Location = new System.Drawing.Point(694, 114);
            this.gRBar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gRBar.Name = "gRBar";
            this.gRBar.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gRBar.Size = new System.Drawing.Size(230, 321);
            this.gRBar.TabIndex = 1;
            this.gRBar.TabStop = false;
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(0, 0);
            this.btnNext.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(56, 19);
            this.btnNext.TabIndex = 6;
            this.btnNext.Text = "button1";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // gRBarForm
            // 
            this.gRBarForm.Controls.Add(this.gRBarFormBack);
            this.gRBarForm.Controls.Add(this.gRBarFormBorder);
            this.gRBarForm.Location = new System.Drawing.Point(37, 144);
            this.gRBarForm.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gRBarForm.Name = "gRBarForm";
            this.gRBarForm.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gRBarForm.Size = new System.Drawing.Size(150, 81);
            this.gRBarForm.TabIndex = 8;
            this.gRBarForm.TabStop = false;
            // 
            // gRBarFormBack
            // 
            this.gRBarFormBack.Location = new System.Drawing.Point(64, 43);
            this.gRBarFormBack.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.gRBarFormBack.Name = "gRBarFormBack";
            this.gRBarFormBack.Size = new System.Drawing.Size(75, 19);
            this.gRBarFormBack.TabIndex = 0;
            this.gRBarFormBack.Text = "label1";
            // 
            // gRBarFormBorder
            // 
            this.gRBarFormBorder.Location = new System.Drawing.Point(0, 0);
            this.gRBarFormBorder.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.gRBarFormBorder.Name = "gRBarFormBorder";
            this.gRBarFormBorder.Size = new System.Drawing.Size(75, 19);
            this.gRBarFormBorder.TabIndex = 1;
            this.gRBarFormBorder.Text = "label1";
            // 
            // clOptions
            // 
            this.clOptions.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.clOptions.CheckOnClick = true;
            this.clOptions.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.clOptions.FormattingEnabled = true;
            this.clOptions.Items.AddRange(new object[] {
            "Elemento",
            "Elemento",
            "Elemento",
            "Elemento",
            "Elemento",
            "Elemento",
            "Elemento",
            "Elemento",
            "Elemento",
            "Elemento"});
            this.clOptions.Location = new System.Drawing.Point(26, 49);
            this.clOptions.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.clOptions.Name = "clOptions";
            this.clOptions.Size = new System.Drawing.Size(100, 50);
            this.clOptions.TabIndex = 5;
            // 
            // gRBarBack
            // 
            this.gRBarBack.Location = new System.Drawing.Point(0, 0);
            this.gRBarBack.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.gRBarBack.Name = "gRBarBack";
            this.gRBarBack.Size = new System.Drawing.Size(75, 19);
            this.gRBarBack.TabIndex = 7;
            this.gRBarBack.Text = "label1";
            // 
            // gImage
            // 
            this.gImage.Location = new System.Drawing.Point(35, 17);
            this.gImage.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gImage.Name = "gImage";
            this.gImage.Size = new System.Drawing.Size(500, 267);
            this.gImage.TabIndex = 2;
            this.gImage.TabStop = false;
            // 
            // gCenter
            // 
            this.gCenter.Controls.Add(this.gImageDesc);
            this.gCenter.Controls.Add(this.gImage);
            this.gCenter.Controls.Add(this.gCenterBack);
            this.gCenter.Location = new System.Drawing.Point(38, 128);
            this.gCenter.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gCenter.Name = "gCenter";
            this.gCenter.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gCenter.Size = new System.Drawing.Size(557, 326);
            this.gCenter.TabIndex = 3;
            this.gCenter.TabStop = false;
            // 
            // gImageDesc
            // 
            this.gImageDesc.Location = new System.Drawing.Point(74, 305);
            this.gImageDesc.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.gImageDesc.Name = "gImageDesc";
            this.gImageDesc.Size = new System.Drawing.Size(75, 19);
            this.gImageDesc.TabIndex = 3;
            this.gImageDesc.Text = "label1";
            // 
            // gCenterBack
            // 
            this.gCenterBack.Location = new System.Drawing.Point(352, 310);
            this.gCenterBack.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.gCenterBack.Name = "gCenterBack";
            this.gCenterBack.Size = new System.Drawing.Size(75, 19);
            this.gCenterBack.TabIndex = 4;
            this.gCenterBack.Text = "label1";
            // 
            // Game
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(946, 547);
            this.Controls.Add(this.gCenter);
            this.Controls.Add(this.gRBar);
            this.Controls.Add(this.gHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Game";
            this.Text = "Jogo";
            this.gHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gTimerPict)).EndInit();
            this.gRBar.ResumeLayout(false);
            this.gRBarForm.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gImage)).EndInit();
            this.gCenter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gHeader;
        private System.Windows.Forms.GroupBox gRBar;
        private System.Windows.Forms.PictureBox gImage;
        private System.Windows.Forms.GroupBox gCenter;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Label gImageDesc;
        private System.Windows.Forms.Label gHeaderTitle;
        private System.Windows.Forms.PictureBox gTimerPict;
        private System.Windows.Forms.Label gTimerLbl;
        private System.Windows.Forms.Label gHeaderBack;
        private System.Windows.Forms.Label gRBarBack;
        private System.Windows.Forms.Label gCenterBack;
        private System.Windows.Forms.Label gTimerLbl2;
        private System.Windows.Forms.GroupBox gRBarForm;
        private System.Windows.Forms.Label gRBarFormBack;
        private System.Windows.Forms.Label gRBarFormBorder;
        private System.Windows.Forms.CheckedListBox clOptions;
    }
}

