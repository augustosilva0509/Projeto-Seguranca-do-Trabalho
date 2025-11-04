using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace game
{
    public partial class Form1 : Form
    {
        // This will get the current WORKING directory(i.e. \bin\Debug)
        public static string workingDirectory = Environment.CurrentDirectory;
        // or: Directory.GetCurrentDirectory() gives the same result

        // This will get the current PROJECT directory
        public static string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName + "\\game";
        public Form1()
        {
            InitializeComponent();
            SetGame();
            gTimerCounter();
        }
        public int gTimer = 30;
        public async void gTimerCounter() {
            if (gTimer == 0)
            {
                MessageBox.Show("Tempo esgotado!");
                Application.Exit();
            }
                
            await Task.Delay(1000);
            gTimer--;
            gTimerLbl.Text = $"{gTimer}";
            gTimerCounter();
        }
        public void SetGame()
        {
            double resY = 720, resX = resY*16/9;
            this.Size = new Size((int)(resX * 1.01), (int)(resY * 1.05));
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            #region Game Header
            #region gHeader
            gHeader.Size = new Size((int)(resX * 1.00), (int)(resY * 0.15));
            gHeader.Location = new Point(0, 0);

            gHeaderBack.Size = new Size((int)(resX * 1.00), (int)(resY * 0.15));
            gHeaderBack.Location = new Point(0, 0);
            gHeaderBack.Text = "";
            gHeaderBack.BackColor = Color.FromArgb(228, 87, 87);
            #endregion
            #region gTitle
            gHeaderTitle.Parent = gHeaderBack;
            gHeaderTitle.Size = new Size((int)(resX * 0.52), (int)(resY * 0.15));
            gHeaderTitle.Location = new Point((int)(resX * 0.08), 0);
            gHeaderTitle.Font = new Font("Arial", (float)(resY*0.027), FontStyle.Bold);
            gHeaderTitle.BackColor = Color.Transparent;
            gHeaderTitle.ForeColor = Color.White;
            gHeaderTitle.Text = "Caso 1- Escritório de Contabilidade";
            gHeaderTitle.TextAlign = ContentAlignment.MiddleLeft;
            #endregion
            #region gTimer
            gTimerPict.Parent = gHeaderBack;
            gTimerPict.Size = new Size((int)(resX * 0.08), (int)(resY * 0.15));
            gTimerPict.Location = new Point((int)(resX * 0.7), 0);
            gTimerPict.Image = Image.FromFile(projectDirectory + "\\img\\hourglass.png");
            gTimerPict.SizeMode = PictureBoxSizeMode.StretchImage;

            gTimerLbl2.Parent = gHeaderBack;
            gTimerLbl2.Size = new Size((int)(resX * 0.05), (int)(resY * 0.12));
            gTimerLbl2.Location = new Point((int)(resX * 0.846), 0);
            gTimerLbl2.Font = new Font("Arial", (float)(resY * 0.027), FontStyle.Bold);
            gTimerLbl2.TextAlign = ContentAlignment.BottomLeft;
            gTimerLbl2.BackColor = Color.Transparent;
            gTimerLbl2.ForeColor = Color.White;
            gTimerLbl2.Text = "/30";

            gTimerLbl.Parent = gHeaderBack;
            gTimerLbl.Size = new Size((int)(resX * 0.09), (int)(resY * 0.15));
            gTimerLbl.Location = new Point((int)(resX * 0.77), 0);
            gTimerLbl.Font = new Font("Arial", (float)(resY * 0.072), FontStyle.Bold);
            gTimerLbl.TextAlign = ContentAlignment.MiddleLeft;
            gTimerLbl.BackColor = Color.Transparent;
            gTimerLbl.ForeColor = Color.White;
            gTimerLbl.Text = "30";
            #endregion

            #endregion

            #region Game Right Bar
            gRBar.Size = new Size((int)(resX * 0.3), (int)(resY * 0.85));
            gRBar.Location = new Point((int)(resX * 0.7), (int)(resY * 0.15));

            
            gRBarBack.Size = new Size((int)(resX * 0.3), (int)(resY * 0.85));
            gRBarBack.Location = new Point(0,0);
            gRBarBack.BackColor = Control.DefaultBackColor;
            gRBarBack.Text = "";

            gRBarForm.Parent = gRBarBack;
            gRBarForm.Location = new Point(0, (int)(resY * 0.07));
            gRBarForm.Size = new Size((int)(resX * 0.25), (int)(resY * 0.56));

            gRBarFormBorder.Location = new Point(0, 0);
            gRBarFormBorder.Size = new Size((int)(resX * 0.25), (int)(resY * 0.56));
            gRBarFormBorder.BackColor = Color.FromArgb(228, 87, 87);
            gRBarFormBorder.Text = "";

            gRBarFormBack.Location = new Point((int)(resX * 0.014*9/16), (int)(resY * 0.014));
            gRBarFormBack.Size = new Size((int)(resX * 0.25 - gRBarFormBack.Location.X * 2), (int)(resY * 0.56 - gRBarFormBack.Location.Y * 2));
            gRBarFormBack.BackColor = Color.FromArgb(217, 217, 217);
            gRBarFormBack.Text = "";

            clOptions.Parent = gRBarFormBack;
            clOptions.Location = new Point((int)(resX * 0.005625), (int)(resY*0.02));
            clOptions.Size = new Size(gRBarFormBack.Size.Width - clOptions.Location.X, gRBarFormBack.Size.Height - clOptions.Location.Y);
            clOptions.Font = new Font("Arial", (float)(resY * 0.027), FontStyle.Bold);
            clOptions.BackColor = Color.FromArgb(217, 217, 217);

            btnNext.Parent = gRBarBack;
            btnNext.Location = new Point((int)(resX * 0.005), (int)(gRBarForm.Size.Height + gRBarForm.Location.Y + resY * 0.05));
            btnNext.Size = new Size((int)(resX * 0.24), (int)(resY * 0.1));
            btnNext.BackColor = Color.FromArgb(107, 200, 118);
            btnNext.FlatStyle = FlatStyle.Flat;
            btnNext.FlatAppearance.BorderSize = 0;
            btnNext.Text = "Próximo";
            btnNext.Font = new Font("Arial", (float)(resY * 0.02), FontStyle.Bold);
            #endregion

            #region Game Center
            gCenter.Size = new Size((int)(resX * 0.62), (int)(resY * 0.71));
            gCenter.Location = new Point((int)(resX * 0.04), (int)(resY * 0.22));

            gCenterBack.Size = new Size((int)(resX * 0.62), (int)(resY * 0.71));
            gCenterBack.Location = new Point(0,0);
            gCenterBack.BackColor = Control.DefaultBackColor;
            gCenterBack.Text = "";

            gImage.Parent = gCenterBack;
            gImage.Size = new Size((int)(resX * 0.62), (int)(resY * 0.6));
            gImage.Location = new Point(0, 0);
            gImage.BackColor = Color.Cyan;

            gImageDesc.Parent = gCenterBack;
            gImageDesc.Location = new Point(0, (int)(gImage.Size.Height + gImage.Location.Y + resY * 0.02));
            gImageDesc.Size = new Size((int)(resX * 0.28), (int)(resY * 0.05));
            gImageDesc.Font = new Font("Arial", (float)(resY * 0.019), FontStyle.Bold);
            gImageDesc.BackColor = Color.Transparent;
            gImageDesc.ForeColor = Color.Black;
            gImageDesc.Text = "Estado do projeto: Reprovado";
            gImageDesc.TextAlign = ContentAlignment.MiddleLeft;

            #endregion
        }


        
    }
}
