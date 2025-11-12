using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;

namespace game
{
    public partial class Main : Form
    {
        private static string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
        private Game game;
        public Main()
        {
            InitializeComponent();
            game = new Game(30, gTimerLbl);
            SetDesign(720);
            UpdateMainGame();
            ProjectStateCheck();
        }
        private void SetDesign(int resolution)
        {
            double resX = resolution * 16 / 9, resY = resolution;
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
            gHeaderTitle.Font = new Font("Arial", (float)(resolution * 0.027), FontStyle.Bold);
            gHeaderTitle.BackColor = Color.Transparent;
            gHeaderTitle.ForeColor = Color.White;
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
            gTimerLbl2.Font = new Font("Arial", (float)(resolution * 0.027), FontStyle.Bold);
            gTimerLbl2.TextAlign = ContentAlignment.BottomLeft;
            gTimerLbl2.BackColor = Color.Transparent;
            gTimerLbl2.ForeColor = Color.White;
            gTimerLbl2.Text = $"/{game.maxTimer}";

            gTimerLbl.Parent = gHeaderBack;
            gTimerLbl.Size = new Size((int)(resX * 0.09), (int)(resY * 0.15));
            gTimerLbl.Location = new Point((int)(resX * 0.77), 0);
            gTimerLbl.Font = new Font("Arial", (float)(resolution * 0.072), FontStyle.Bold);
            gTimerLbl.TextAlign = ContentAlignment.MiddleLeft;
            gTimerLbl.BackColor = Color.Transparent;
            gTimerLbl.ForeColor = Color.White;
            gTimerLbl.Text = $"{game.maxTimer}";
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

            gRBarFormBack.Location = new Point((int)(resX * 0.007875), (int)(resY * 0.014));
            gRBarFormBack.Size = new Size((int)(resX * 0.25 - gRBarFormBack.Location.X * 2), (int)(resY * 0.56 - gRBarFormBack.Location.Y * 2));
            gRBarFormBack.BackColor = Color.FromArgb(217, 217, 217);
            gRBarFormBack.Text = "";

            gCaseRisk.Parent = gRBarFormBack;
            gCaseRisk.Location = new Point((int)(resX * 0.005), (int)(resY * 0.04));
            gCaseRisk.Font = new Font("Arial", 10, FontStyle.Bold);
            gCaseRisk.ForeColor = Color.Black;

            gCaseWeight.Parent = gRBarFormBack;
            gCaseWeight.Location = new Point((int)(resX * 0.112125), (int)(resY * 0.04));
            gCaseWeight.Font = new Font("Arial", 10, FontStyle.Bold);
            gCaseWeight.ForeColor = Color.Black;

            gCaseC.Parent = gRBarFormBack;
            gCaseC.Location = new Point((int)(resX * 0.005), (int)(resY * 0.08));
            gCaseC.Font = new Font("Arial", 12, FontStyle.Bold);
            gCaseC.ForeColor = Color.Black;

            gCaseP.Parent = gRBarFormBack;
            gCaseP.Location = new Point((int)(resX * 0.005), (int)(resY * 0.12));
            gCaseP.Font = new Font("Arial", 12, FontStyle.Bold);
            gCaseP.ForeColor = Color.Black;

            clOptions.Parent = gRBarFormBack;
            clOptions.Location = new Point((int)(resX * 0.005625), (int)(resY*0.266));
            clOptions.Size = new Size(gRBarFormBack.Size.Width - clOptions.Location.X, gRBarFormBack.Size.Height - clOptions.Location.Y);
            clOptions.Font = new Font("Arial", (float)(resolution * 0.025), FontStyle.Bold);
            clOptions.BackColor = Color.FromArgb(217, 217, 217);

            btnNext.Parent = gRBarBack;
            btnNext.Location = new Point((int)(resX * 0.005), (int)(gRBarForm.Size.Height + gRBarForm.Location.Y + resY * 0.05));
            btnNext.Size = new Size((int)(resX * 0.24), (int)(resY * 0.1));
            btnNext.BackColor = Color.FromArgb(107, 200, 118);
            btnNext.FlatStyle = FlatStyle.Flat;
            btnNext.FlatAppearance.BorderSize = 0;
            btnNext.Text = "Próximo";
            btnNext.Font = new Font("Arial", (float)(resolution * 0.02), FontStyle.Bold);
            #endregion

            #region Game Center
            gCenter.Size = new Size((int)(resX * 0.62), (int)(resY * 0.71));
            gCenter.Location = new Point((int)(resX * 0.04), (int)(resY * 0.22));

            gCenterBack.Size = new Size((int)(resX * 0.62), (int)(resY * 0.71));
            gCenterBack.Location = new Point(0,0);
            gCenterBack.BackColor = Control.DefaultBackColor;
            gCenterBack.Text = "";

            gImage.Parent = gCenterBack;
            gImage.Size = new Size((int)(resX * 0.6), (int)(resY * 0.6));
            gImage.Location = new Point(0, 0);

            gImageDesc.Parent = gCenterBack;
            gImageDesc.Location = new Point(0, (int)(gImage.Size.Height + gImage.Location.Y + resY * 0.02));
            gImageDesc.Size = new Size((int)(resX * 0.28), (int)(resY * 0.05));
            gImageDesc.Font = new Font("Arial", (float)(resolution * 0.019), FontStyle.Bold);
            gImageDesc.BackColor = Color.Transparent;
            gImageDesc.ForeColor = Color.Black;
            gImageDesc.TextAlign = ContentAlignment.MiddleLeft;

            #endregion
        }
        private void UpdateMainGame()
        {
            gHeaderTitle.Text = $"Caso {game.caseNumber} - {game.currentCase.name}";
            gCaseRisk.Text = $"Risco: {game.currentCase.RiskText()}";
            gCaseWeight.Text = $"Gravidade: {game.currentCase.WeightText()}";
            gCaseC.Text = $"Capacidade de Passagem: {game.currentCase.c}";
            gCaseP.Text = $"População: {game.currentCase.p}";
            gImage.Image = Image.FromFile(projectDirectory + $"\\img\\{game.currentCase.imgName}");
            gTimerLbl.Text = $"{game.maxTimer}";
            while (clOptions.CheckedIndices.Count > 0)
            {
                clOptions.SetItemChecked(clOptions.CheckedIndices[0], false);
            }
        }
        private async void ProjectStateCheck()
        {
            if (clOptions.CheckedItems.Count == 4)
                gImageDesc.Text = "Estado do projeto: Aprovado";
            else
                gImageDesc.Text = "Estado do projeto: Reprovado";
            await Task.Delay(50);
            ProjectStateCheck();
        }
        private void NextClick(object sender, EventArgs e)
        {
            game.UpdateCase(clOptions);
            UpdateMainGame();
            if (game.End())
                EndGame(720);
        }
        public void EndGame(int resolution)
        {
            int resX = resolution * 16 / 9, resY = resolution;

            this.Controls.Clear();
            GroupBox gpbResult = new GroupBox();
            Label gpbResultBack = new Label();
            Label gpbResultBorder = new Label();
            Label lblResultado = new Label();

            Panel[] gpbResultCase = new Panel[4] { new Panel(), new Panel(), new Panel(), new Panel() };
            Panel[] gpbResultCaseBack = new Panel[4] { new Panel(), new Panel(), new Panel(), new Panel() };
            Label[] lblCaseName = new Label[4] { new Label(), new Label(), new Label(), new Label() };
            Label[] lblTextos = new Label[4] { new Label(), new Label(), new Label(), new Label() };

            gpbResult.Size = new Size((int)(resX * 0.5), (int)(resY * 1));
            gpbResult.Location = new Point((int)(resX * 0.5 - gpbResult.Size.Width * 0.5), (int)(resY * 0));
            gpbResult.BackColor = Color.FromArgb(217, 217, 217);

            gpbResultBorder.Parent = gpbResult;
            gpbResultBorder.Location = new Point(0, 0);
            gpbResultBorder.Size = new Size(gpbResult.Size.Width, gpbResult.Size.Height);
            gpbResultBorder.BackColor = Color.FromArgb(228, 87, 87);
            gpbResultBorder.Text = "";

            gpbResultBack.Location = new Point((int)(gpbResult.Size.Width * 0.0225), (int)(gpbResult.Size.Height * 0.02));
            gpbResultBack.Size = new Size((int)(gpbResult.Size.Width * 1 - gpbResultBack.Location.X * 2), (int)(gpbResult.Size.Height * 1 - gpbResultBack.Location.Y * 2));
            gpbResultBack.BackColor = Color.FromArgb(217, 217, 217);
            gpbResultBack.Text = "";

            lblResultado.Parent = gpbResult;
            lblResultado.Text = "Resultado final";
            lblResultado.Font = new Font("Arial", (int)(resolution * 0.05), FontStyle.Bold);
            lblResultado.BackColor = Color.Transparent;
            lblResultado.ForeColor = Color.FromArgb(228, 60, 60);
            lblResultado.AutoSize = true;
            lblResultado.Location = new Point((int)(gpbResult.Size.Width * 0.5 - lblResultado.Size.Width * 0.5), (int)(gpbResult.Size.Height * 0.05));
            int j = 0, z = 0; string name = ""; string[] indexesResults = new string[4];
            for(int i = 0; i < 4; i++)
            {
                name = game.results.Name();
                indexesResults = game.results.IndexesResults();

                gpbResultCase[i].Parent = gpbResultBack;
                gpbResultCase[i].Location = new Point((int)(j), (int)(gpbResultBack.Size.Height * 0.2 + z));
                gpbResultCase[i].Size = new Size((int)(gpbResultBack.Size.Width * 0.5), (int)(gpbResultBack.Size.Height * 0.3));
                gpbResultCase[i].BackColor = Color.FromArgb(217, 217, 217);

                gpbResultCaseBack[i].Parent = gpbResultCase[i];
                gpbResultCaseBack[i].Location = new Point(0, 0);
                gpbResultCaseBack[i].Size = new Size(gpbResultCase[i].Size.Width, gpbResultCase[i].Size.Height);
                gpbResultCaseBack[i].BackColor = Color.FromArgb(205, 205, 205);
                gpbResultCaseBack[i].Text = "";

                lblCaseName[i].Parent = gpbResultCaseBack[i];
                lblCaseName[i].AutoSize = true;
                lblCaseName[i].Text = $"{name}";
                lblCaseName[i].Font = new Font("Arial", (int)(resolution * 0.02), FontStyle.Bold);
                lblCaseName[i].BackColor = Color.FromArgb(205, 205, 205);
                lblCaseName[i].ForeColor = Color.Black;
                lblCaseName[i].Location = new Point((int)(gpbResultCase[i].Size.Width * 0.5 - lblCaseName[i].Size.Width * 0.5), (int)(gpbResultCase[i].Size.Height * 0.06));
                gpbResultCaseBack[i].Controls.Add(lblCaseName[i]);

                lblTextos[i].Parent = gpbResultCaseBack[i];
                lblTextos[i].AutoSize = true;
                lblTextos[i].Text = $"Extintores de incêndio: {indexesResults[0]}\nSaídas de emergência: {indexesResults[1]}\nRotas de fuga: {indexesResults[2]}\nAlarmes de incêndio: {indexesResults[3]}";
                lblTextos[i].Font = new Font("Arial", (int)(resolution * 0.015), FontStyle.Bold);
                lblTextos[i].BackColor = Color.Transparent;
                lblTextos[i].ForeColor = Color.Black;
                lblTextos[i].Location = new Point((int)(gpbResultCase[i].Size.Width * 0.05), (int)(gpbResultCase[i].Size.Height * 0.3));
                lblTextos[i].TextAlign = ContentAlignment.MiddleRight;
                gpbResultCaseBack[i].Controls.Add(lblTextos[i]);

                gpbResultCase[i].Controls.Add(gpbResultCaseBack[i]);
                gpbResultBack.Controls.Add(gpbResultCase[i]);
                if(j==0)
                    j = gpbResultCase[i].Size.Width;
                else if (z == 0)
                {
                    z = gpbResultCase[i].Size.Height;
                    j = 0;
                }
            }
            gpbResult.Controls.Add(lblResultado);
            gpbResult.Controls.Add(gpbResultBack);
            gpbResult.Controls.Add(gpbResultBorder);

            this.Controls.Add(gpbResult);
        }
    }
}
