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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;

namespace game
{
    public partial class Main : Form
    {
        private static string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
        private int resolution = 720;
        private Game game;
        public Main()
        {
            InitializeComponent();
            MainMenu();
        }
        public void ResetForm()
        {
            Form currentForm = Form.ActiveForm;
            if (currentForm == null) return;
            for (int i = currentForm.Controls.Count-1; i >= 0; i--)
            {
                currentForm.Controls[i].Enabled = false;
                currentForm.Controls[i].Visible = false;
            }
            this.Cursor = Cursors.Default;
        }
        private void RelativeSize(Control component, double x = 1.0, double y = 1.0)
        {
            /***
            Redimensiona o componente relativamente ao tamanho do seu componente pai.
            
            Entradas:
                component: componente a ser redimensionado;
                x : valor relativo a largura do componente pai (ex.: x = 1.00, temos 100% da largura do pai);
                y : valor relativo a altura do componente pai (ex.: y = 0.50, temos 50% da altura do pai);
            ***/
            x = (x > 1.0) ? 1.0 : x;
            y = (y > 1.0) ? 1.0 : y;
            component.Size = new Size((int)(component.Parent.Size.Width * x), (int)(component.Parent.Size.Height * y));
        }
        private void RelativeLocation(Control component, double x = 0.0, double y = 0.0)
        {
            /***
            Posiciona o ponto médio do componente relativamente ao tamanho do seu componente pai. 
            
            Entradas:
                component: componente a ser posicionado;
                x : valor relativo a largura do componente pai (ex.: x = 1.00, temos 100% da largura do pai);
                y : valor relativo a altura do componente pai (ex.: y = 0.50, temos 50% da altura do pai);
            ***/
            x = (x > 1.0) ? 1.0 : x;
            y = (y > 1.0) ? 1.0 : y;
            component.Location = new Point(
                (int)(component.Parent.Size.Width * x - component.Size.Width * 0.5), 
                (int)(component.Parent.Size.Height * y - component.Size.Height * 0.5)
                );
        }
        #region MainMenu
        private void MainMenu()
        {
            ResetForm();
            SetMenuDesign(resolution);
            game = null;
        }
        private void SetMenuDesign(int resolution)
        {
            this.Controls.Clear();
            this.Size = new Size((int)(resolution * 16/9 * 1.01), (int)(resolution * 1.05));
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            Label lblMenuTitle = new Label();
            Label lblMenuStart = new Label();
            Label lblMenuHowToPlay = new Label();
            Label lblMenuExit = new Label();
            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;

            lblMenuTitle.Parent = this;
            lblMenuTitle.Text = "Guardiões das Plantas";
            lblMenuTitle.Font = new Font("Arial", (int)(resolution * 0.07), FontStyle.Bold);
            lblMenuTitle.AutoSize = true;
            RelativeLocation(lblMenuTitle, 0.5, 0.15);
            lblMenuTitle.ForeColor = Color.FromArgb(228, 87, 87);
            lblMenuTitle.BackColor = Color.Transparent;
            lblMenuTitle.Visible = true;
            lblMenuTitle.Enabled = true;

            lblMenuStart.Parent = this;
            lblMenuStart.Name = "Start";
            lblMenuStart.Text = "Iniciar";
            lblMenuStart.Font = new Font("Arial", (int)(resolution * 0.04), FontStyle.Bold);
            lblMenuStart.AutoSize = true;
            RelativeLocation(lblMenuStart, 0.5, 0.45);
            lblMenuStart.ForeColor = Color.FromArgb(228, 87, 87);
            lblMenuStart.BackColor = Color.Transparent;
            lblMenuStart.MouseEnter += LabelMenu_MouseEnter;
            lblMenuStart.MouseLeave += LabelMenu_MouseLeave;
            lblMenuStart.MouseClick += MenuStart_MouseClick;
            lblMenuStart.Visible = true;
            lblMenuStart.Enabled = true;

            lblMenuHowToPlay.Parent = this;
            lblMenuHowToPlay.Name = "HowToPlay";
            lblMenuHowToPlay.Text = "Como Jogar";
            lblMenuHowToPlay.Font = new Font("Arial", (int)(resolution * 0.04), FontStyle.Bold);
            lblMenuHowToPlay.AutoSize = true;
            RelativeLocation(lblMenuHowToPlay, 0.5, 0.55);
            lblMenuHowToPlay.ForeColor = Color.FromArgb(228, 87, 87);
            lblMenuHowToPlay.BackColor = Color.Transparent;
            lblMenuHowToPlay.MouseEnter += LabelMenu_MouseEnter;
            lblMenuHowToPlay.MouseLeave += LabelMenu_MouseLeave;
            lblMenuHowToPlay.MouseClick += MenuHowToPlay_MouseClick;
            lblMenuHowToPlay.Visible = true;
            lblMenuHowToPlay.Enabled = true;

            lblMenuExit.Parent = this;
            lblMenuExit.Name = "Exit";
            lblMenuExit.Text = "Sair";
            lblMenuExit.Font = new Font("Arial", (int)(resolution * 0.04), FontStyle.Bold);
            lblMenuExit.AutoSize = true;
            RelativeLocation(lblMenuExit, 0.5, 0.65);
            lblMenuExit.ForeColor = Color.FromArgb(228, 87, 87);
            lblMenuExit.BackColor = Color.Transparent;
            lblMenuExit.MouseEnter += LabelMenu_MouseEnter;
            lblMenuExit.MouseLeave += LabelMenu_MouseLeave;
            lblMenuExit.MouseClick += MenuExit_MouseClick;
            lblMenuExit.Visible = true;
            lblMenuExit.Enabled = true;
        }
        private void MenuStart_MouseClick(object sender, MouseEventArgs e)
        {
            MainGame();
        }
        private void MenuHowToPlay_MouseClick(object sender, MouseEventArgs e)
        {
            //implementar
            MessageBox.Show("Falta implementar!");
        }
        private void MenuExit_MouseClick(object sender, MouseEventArgs e)
        {
            Application.Exit();
        }
        private void LabelMenu_MouseEnter(object sender, EventArgs e)
        {
            Label lblSender = sender as Label;
            lblSender.ForeColor = Color.FromArgb(226,38,38);
            lblSender.Font = new Font(lblSender.Font.FontFamily, (int)(lblSender.Font.Size * 1.5), lblSender.Font.Style);
            switch (lblSender.Name)
            {
                case "Start":
                    RelativeLocation(lblSender, 0.5, 0.45);
                    break;
                case "HowToPlay":
                    RelativeLocation(lblSender, 0.5, 0.55);
                    break;
                case "Exit":
                    RelativeLocation(lblSender, 0.5, 0.65);
                    break;
                default:
                    break;
            }
            this.Cursor = Cursors.Hand;
        }
        private void LabelMenu_MouseLeave(object sender, EventArgs e)
        {
            Label lblSender = sender as Label;
            lblSender.ForeColor = Color.FromArgb(228, 87, 87);
            lblSender.Font = new Font(lblSender.Font.FontFamily, (int)(lblSender.Font.Size / 1.5), lblSender.Font.Style);
            switch (lblSender.Name)
            {
                case "Start":
                    RelativeLocation(lblSender, 0.5, 0.45);
                    break;
                case "HowToPlay":
                    RelativeLocation(lblSender, 0.5, 0.55);
                    break;
                case "Exit":
                    RelativeLocation(lblSender, 0.5, 0.65);
                    break;
                default:
                    break;
            }
            this.Cursor = Cursors.Default;
        }
        #endregion
        #region MainGame
        private void MainGame()
        {
            ResetForm();
            game = new Game(30, gTimerLbl);
            SetGameDesign(resolution);
            UpdateMainGame();
            ProjectStateCheck();
        }
        private void SetGameDesign(int resolution)
        {
            double resX = resolution * 16 / 9, resY = resolution;
            this.Size = new Size((int)(resX * 1.01), (int)(resY * 1.05));
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            #region Game Header
            #region gHeader
            gHeader.Size = new Size((int)(resX * 1.00), (int)(resY * 0.15));
            gHeader.Location = new Point(0, 0);
            gHeader.Enabled = true;
            gHeader.Visible = true;

            gHeaderBack.Size = new Size((int)(resX * 1.00), (int)(resY * 0.15));
            gHeaderBack.Location = new Point(0, 0);
            gHeaderBack.Text = "";
            gHeaderBack.BackColor = Color.FromArgb(228, 87, 87);
            gHeaderBack.Enabled = true;
            gHeaderBack.Visible = true;
            #endregion
            #region gTitle
            gHeaderTitle.Parent = gHeaderBack;
            gHeaderTitle.Size = new Size((int)(resX * 0.52), (int)(resY * 0.15));
            gHeaderTitle.Location = new Point((int)(resX * 0.08), 0);
            gHeaderTitle.Font = new Font("Arial", (float)(resolution * 0.027), FontStyle.Bold);
            gHeaderTitle.BackColor = Color.Transparent;
            gHeaderTitle.ForeColor = Color.White;
            gHeaderTitle.TextAlign = ContentAlignment.MiddleLeft;
            gHeaderTitle.Enabled = true;
            gHeaderTitle.Visible = true;
            #endregion
            #region gTimer
            gTimerPict.Parent = gHeaderBack;
            gTimerPict.Size = new Size((int)(resX * 0.08), (int)(resY * 0.15));
            gTimerPict.Location = new Point((int)(resX * 0.7), 0);
            gTimerPict.Image = Image.FromFile(projectDirectory + "\\img\\hourglass.png");
            gTimerPict.SizeMode = PictureBoxSizeMode.StretchImage;
            gTimerPict.Enabled = true;
            gTimerPict.Visible = true;

            gTimerLbl2.Parent = gHeaderBack;
            gTimerLbl2.Size = new Size((int)(resX * 0.05), (int)(resY * 0.12));
            gTimerLbl2.Location = new Point((int)(resX * 0.846), 0);
            gTimerLbl2.Font = new Font("Arial", (float)(resolution * 0.027), FontStyle.Bold);
            gTimerLbl2.TextAlign = ContentAlignment.BottomLeft;
            gTimerLbl2.BackColor = Color.Transparent;
            gTimerLbl2.ForeColor = Color.White;
            gTimerLbl2.Text = $"/{game.maxTimer}";
            gTimerLbl2.Enabled = true;
            gTimerLbl2.Visible = true;

            gTimerLbl.Parent = gHeaderBack;
            gTimerLbl.Size = new Size((int)(resX * 0.09), (int)(resY * 0.15));
            gTimerLbl.Location = new Point((int)(resX * 0.77), 0);
            gTimerLbl.Font = new Font("Arial", (float)(resolution * 0.072), FontStyle.Bold);
            gTimerLbl.TextAlign = ContentAlignment.MiddleLeft;
            gTimerLbl.BackColor = Color.Transparent;
            gTimerLbl.ForeColor = Color.White;
            gTimerLbl.Text = $"{game.maxTimer}";
            gTimerLbl.Enabled = true;
            gTimerLbl.Visible = true;
            #endregion

            #endregion

            #region Game Right Bar
            gRBar.Size = new Size((int)(resX * 0.3), (int)(resY * 0.85));
            gRBar.Location = new Point((int)(resX * 0.7), (int)(resY * 0.15));
            gRBar.Enabled = true;
            gRBar.Visible = true;

            gRBarBack.Size = new Size((int)(resX * 0.3), (int)(resY * 0.85));
            gRBarBack.Location = new Point(0, 0);
            gRBarBack.BackColor = Control.DefaultBackColor;
            gRBarBack.Text = "";
            gRBarBack.Enabled = true;
            gRBarBack.Visible = true;

            gRBarForm.Parent = gRBarBack;
            gRBarForm.Location = new Point(0, (int)(resY * 0.07));
            gRBarForm.Size = new Size((int)(resX * 0.25), (int)(resY * 0.56));
            gRBarForm.Enabled = true;
            gRBarForm.Visible = true;

            gRBarFormBorder.Location = new Point(0, 0);
            gRBarFormBorder.Size = new Size((int)(resX * 0.25), (int)(resY * 0.56));
            gRBarFormBorder.BackColor = Color.FromArgb(228, 87, 87);
            gRBarFormBorder.Text = "";
            gRBarFormBorder.Enabled = true;
            gRBarFormBorder.Visible = true;

            gRBarFormBack.Location = new Point((int)(resX * 0.007875), (int)(resY * 0.014));
            gRBarFormBack.Size = new Size((int)(resX * 0.25 - gRBarFormBack.Location.X * 2), (int)(resY * 0.56 - gRBarFormBack.Location.Y * 2));
            gRBarFormBack.BackColor = Color.FromArgb(217, 217, 217);
            gRBarFormBack.Text = "";
            gRBarFormBack.Enabled = true;
            gRBarFormBack.Visible = true;

            gCaseRisk.Parent = gRBarFormBack;
            gCaseRisk.Location = new Point((int)(resX * 0.005), (int)(resY * 0.04));
            gCaseRisk.Font = new Font("Arial", 10, FontStyle.Bold);
            gCaseRisk.ForeColor = Color.Black;
            gCaseRisk.Enabled = true;
            gCaseRisk.Visible = true;

            gCaseWeight.Parent = gRBarFormBack;
            gCaseWeight.Location = new Point((int)(resX * 0.112125), (int)(resY * 0.04));
            gCaseWeight.Font = new Font("Arial", 10, FontStyle.Bold);
            gCaseWeight.ForeColor = Color.Black;
            gCaseWeight.Enabled = true;
            gCaseWeight.Visible = true;

            gCaseC.Parent = gRBarFormBack;
            gCaseC.Location = new Point((int)(resX * 0.005), (int)(resY * 0.08));
            gCaseC.Font = new Font("Arial", 12, FontStyle.Bold);
            gCaseC.ForeColor = Color.Black;
            gCaseC.Enabled = true;
            gCaseC.Visible = true;

            gCaseP.Parent = gRBarFormBack;
            gCaseP.Location = new Point((int)(resX * 0.005), (int)(resY * 0.12));
            gCaseP.Font = new Font("Arial", 12, FontStyle.Bold);
            gCaseP.ForeColor = Color.Black;
            gCaseP.Enabled = true;
            gCaseP.Visible = true;

            clOptions.Parent = gRBarFormBack;
            clOptions.Location = new Point((int)(resX * 0.005625), (int)(resY * 0.266));
            clOptions.Size = new Size(gRBarFormBack.Size.Width - clOptions.Location.X, gRBarFormBack.Size.Height - clOptions.Location.Y);
            clOptions.Font = new Font("Arial", (float)(resolution * 0.025), FontStyle.Bold);
            clOptions.BackColor = Color.FromArgb(217, 217, 217);
            clOptions.Enabled = true;
            clOptions.Visible = true;

            btnNext.Parent = gRBarBack;
            btnNext.Location = new Point((int)(resX * 0.005), (int)(gRBarForm.Size.Height + gRBarForm.Location.Y + resY * 0.05));
            btnNext.Size = new Size((int)(resX * 0.24), (int)(resY * 0.1));
            btnNext.BackColor = Color.FromArgb(107, 200, 118);
            btnNext.FlatStyle = FlatStyle.Flat;
            btnNext.FlatAppearance.BorderSize = 0;
            btnNext.Text = "Próximo";
            btnNext.Font = new Font("Arial", (float)(resolution * 0.02), FontStyle.Bold);
            btnNext.Enabled = true;
            btnNext.Visible = true;
            #endregion

            #region Game Center
            gCenter.Size = new Size((int)(resX * 0.62), (int)(resY * 0.71));
            gCenter.Location = new Point((int)(resX * 0.04), (int)(resY * 0.22));
            gCenter.Enabled = true;
            gCenter.Visible = true;

            gCenterBack.Size = new Size((int)(resX * 0.62), (int)(resY * 0.71));
            gCenterBack.Location = new Point(0, 0);
            gCenterBack.BackColor = Control.DefaultBackColor;
            gCenterBack.Text = "";
            gCenterBack.Enabled = true;
            gCenterBack.Visible = true;

            gImage.Parent = gCenterBack;
            gImage.Size = new Size((int)(resX * 0.6), (int)(resY * 0.6));
            gImage.Location = new Point(0, 0);
            gImage.Enabled = true;
            gImage.Visible = true;

            gImageDesc.Parent = gCenterBack;
            gImageDesc.Location = new Point(0, (int)(gImage.Size.Height + gImage.Location.Y + resY * 0.02));
            gImageDesc.Size = new Size((int)(resX * 0.28), (int)(resY * 0.05));
            gImageDesc.Font = new Font("Arial", (float)(resolution * 0.019), FontStyle.Bold);
            gImageDesc.BackColor = Color.Transparent;
            gImageDesc.ForeColor = Color.Black;
            gImageDesc.TextAlign = ContentAlignment.MiddleLeft;
            gImageDesc.Enabled = true;
            gImageDesc.Visible = true;
            #endregion

            this.Controls.Add(this.gCenter);
            this.Controls.Add(this.gRBar);
            this.Controls.Add(this.gHeader);
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
            await Task.Delay(33); //~30fps
            ProjectStateCheck();
        }
        private void NextClick(object sender, EventArgs e)
        {
            game.UpdateCase(clOptions);
            UpdateMainGame();
            if (game.End())
            {
                GameResults();
                game.EndTimer();
            }
        }
        #endregion
        #region GameResults
        private void GameResults()
        {
            SetResultDesign(resolution);
        }
        public void SetResultDesign(int resolution)
        {
            int resX = resolution * 16 / 9, resY = resolution;

            this.Controls.Clear();
            GroupBox gpbResult = new GroupBox();
            Label gpbResultBack = new Label();
            Label gpbResultBorder = new Label();
            Label lblResultado = new Label();

            Panel[] gpbResultCase = new Panel[4] { new Panel(), new Panel(), new Panel(), new Panel() };
            Panel[] gpbResultCaseBorder = new Panel[4] { new Panel(), new Panel(), new Panel(), new Panel() };
            Panel[] gpbResultCaseBack = new Panel[4] { new Panel(), new Panel(), new Panel(), new Panel() };
            Label[] lblCaseName = new Label[4] { new Label(), new Label(), new Label(), new Label() };
            Label[] lblTextos = new Label[4] { new Label(), new Label(), new Label(), new Label() };
            Button btnBack = new Button();

            gpbResult.Parent = this;
            RelativeSize(gpbResult,0.6,0.9);
            gpbResult.Location = new Point((int)(resX * 0.5 - gpbResult.Size.Width * 0.5), (int)(resY * 0.5 - gpbResult.Size.Height * 0.5));
            //RelativeLocation(gpbResult, 0.5, 0.5);
            gpbResult.BackColor = Color.FromArgb(217, 217, 217);

            gpbResultBorder.Parent = gpbResult;
            gpbResultBorder.Location = new Point(0, 0);
            gpbResultBorder.Size = new Size(gpbResult.Size.Width, gpbResult.Size.Height);
            gpbResultBorder.BackColor = Color.FromArgb(228, 87, 87);
            gpbResultBorder.Text = "";

            gpbResultBack.Location = new Point((int)(gpbResult.Size.Width * 0.015*(16/9*0.6)), (int)(gpbResult.Size.Height * 0.015 * 0.9));
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
                RelativeSize(gpbResultCase[i], 0.5, 0.3);
                gpbResultCase[i].BackColor = Color.FromArgb(217, 217, 217);

                gpbResultCaseBorder[i].Parent = gpbResultCase[i];
                gpbResultCaseBorder[i].Location = new Point(0, 0);
                RelativeSize(gpbResultCaseBorder[i]);
                gpbResultCaseBorder[i].BackColor = Color.FromArgb(210, 210, 210);
                gpbResultCaseBorder[i].Text = "";
                gpbResultCaseBorder[i].Visible = false;

                gpbResultCaseBack[i].Parent = gpbResultCase[i];
                gpbResultCaseBack[i].Location = new Point(0, 0);
                RelativeSize(gpbResultCaseBack[i], 0.98, 0.95);
                RelativeLocation(gpbResultCaseBack[i], 0.5, 0.5);
                gpbResultCaseBack[i].BackColor = Color.FromArgb(217, 217, 217);
                gpbResultCaseBack[i].Text = "";

                lblCaseName[i].Parent = gpbResultCaseBack[i];
                lblCaseName[i].AutoSize = true;
                lblCaseName[i].Text = $"{name}";
                lblCaseName[i].Font = new Font("Arial", (int)(resolution * 0.02), FontStyle.Bold);
                lblCaseName[i].BackColor = Color.Transparent;
                lblCaseName[i].ForeColor = Color.Black;
                lblCaseName[i].Location = new Point((int)(gpbResultCase[i].Size.Width * 0.5 - lblCaseName[i].Size.Width * 0.5), (int)(gpbResultCase[i].Size.Height * 0.1));
                gpbResultCaseBack[i].Controls.Add(lblCaseName[i]);

                lblTextos[i].Parent = gpbResultCaseBack[i];
                lblTextos[i].AutoSize = true;
                lblTextos[i].Text = $"Extintores de incêndio: {indexesResults[0]}\nSaídas de emergência: {indexesResults[1]}\nRotas de fuga: {indexesResults[2]}\nAlarmes de incêndio: {indexesResults[3]}";
                lblTextos[i].Font = new Font("Arial", (int)(resolution * 0.015), FontStyle.Bold);
                lblTextos[i].BackColor = Color.Transparent;
                lblTextos[i].ForeColor = Color.Black;
                RelativeLocation(lblTextos[i],0.5,0.5);
                lblTextos[i].TextAlign = ContentAlignment.MiddleCenter;
                gpbResultCaseBack[i].Controls.Add(lblTextos[i]);

                gpbResultCase[i].Controls.Add(gpbResultCaseBack[i]);
                gpbResultCase[i].Controls.Add(gpbResultCaseBorder[i]);
                gpbResultBack.Controls.Add(gpbResultCase[i]);
                if(j==0)
                    j = gpbResultCase[i].Size.Width;
                else if (z == 0)
                {
                    z = gpbResultCase[i].Size.Height;
                    j = 0;
                }
            }

            btnBack.Parent = gpbResult;
            btnBack.Text = "Voltar para o menu";
            btnBack.AutoSize = false;
            RelativeSize(btnBack,0.5,0.1);
            RelativeLocation(btnBack, 0.5, 0.885);
            btnBack.BackColor = Color.FromArgb(107, 200, 118);
            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.FlatAppearance.BorderSize = 0;
            btnBack.Font = new Font("Arial", (float)(resolution * 0.02), FontStyle.Bold);
            btnBack.Click += ResultBack_Click;

            gpbResult.Controls.Add(btnBack);
            gpbResult.Controls.Add(lblResultado);
            gpbResult.Controls.Add(gpbResultBack);
            gpbResult.Controls.Add(gpbResultBorder);

            this.Controls.Add(gpbResult);
        }

        private void ResultBack_Click(object sender, EventArgs e)
        {
            MainMenu();
        }
        #endregion

    }
}
