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
using System.Timers;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;

namespace game
{
    public partial class Main : Form
    {
        private static string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
        private int resolution = 720;
        private Game game;
        private Image menu_bg;
        private CancellationTokenSource timer;
        private CancellationToken token;

        private enum AnchorPos
        {
            Center,
            TopLeft,
            TopRight,
            BottomLeft,
            BottomRight
        }
        public Main()
        {
            InitializeComponent();
            menu_bg = Image.FromFile(projectDirectory + $"\\img\\menu_bg.jpeg");
            this.Size = new Size((int)(resolution * 16 / 9 * 1.05), (int)(resolution * 1.01));
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            MainMenu();
        }
        private void ResetForm()
        {
            Form currentForm = Form.ActiveForm;
            if (currentForm == null) return;

            for (int i = currentForm.Controls.Count - 1; i >= 0; i--)
            {
                currentForm.Controls[i].Dispose();
            }
            currentForm.Cursor = Cursors.Default;
        }
        public static Control GetComponent(string name)
        {
            Form currentForm = Form.ActiveForm;
            if (currentForm == null) return null;
            if (currentForm.Controls.Find(name, true).Count() == 0) return null;
            return currentForm.Controls.Find(name, true).First();
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
        private void RelativeLocation(Control component, double x = 0.5, double y = 0.5, AnchorPos anchor = AnchorPos.Center)
        {
            /***
            Posiciona o ponto de ancora do componente relativamente ao tamanho do seu componente pai. 
            
            Entradas:
                component: componente a ser posicionado;
                anchor: ponto que ser� usado como ancora para o posicionamento;
                x : valor relativo a largura do componente pai (ex.: x = 1.00, temos 100% da largura do pai);
                y : valor relativo a altura do componente pai (ex.: y = 0.50, temos 50% da altura do pai);
            ***/
            x = (x > 1.0) ? 1.0 : x;
            y = (y > 1.0) ? 1.0 : y;
            switch (anchor)
            {
                case AnchorPos.Center:
                    component.Location = new Point(
                        (int)(component.Parent.Size.Width * x - component.Size.Width * 0.5),
                        (int)(component.Parent.Size.Height * y - component.Size.Height * 0.5)
                        );
                    break;
                case AnchorPos.TopLeft:
                    component.Location = new Point(
                        (int)(component.Parent.Size.Width * x),
                        (int)(component.Parent.Size.Height * y)
                        );
                    break;
                case AnchorPos.TopRight:
                    component.Location = new Point(
                        (int)(component.Parent.Size.Width * x + component.Size.Width),
                        (int)(component.Parent.Size.Height * y)
                        );
                    break;
                case AnchorPos.BottomLeft:
                    component.Location = new Point(
                        (int)(component.Parent.Size.Width * x),
                        (int)(component.Parent.Size.Height * y + component.Size.Height)
                        );
                    break;
                case AnchorPos.BottomRight:
                    component.Location = new Point(
                        (int)(component.Parent.Size.Width * x + component.Size.Width),
                        (int)(component.Parent.Size.Height * y + component.Size.Height)
                        );
                    break;
            }
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
            Panel pnlMenu = new Panel();

            Label lblMenuTitle = new Label();
            Label lblMenuTitleBorder = new Label();
            Label lblMenuStart = new Label();
            Label lblMenuStartBorder = new Label();
            Label lblMenuHowToPlay = new Label();
            Label lblMenuExit = new Label();

            PictureBox pictMenuBg = new PictureBox();

            pictMenuBg.Parent = this;
            RelativeSize(pictMenuBg);
            RelativeLocation(pictMenuBg, 0.5, 0.5);
            pictMenuBg.SizeMode = PictureBoxSizeMode.StretchImage;

            pnlMenu.Parent = pictMenuBg;
            RelativeSize(pnlMenu);
            RelativeLocation(pnlMenu, 0.5, 0.5);
            pnlMenu.BackColor = Color.Transparent;

            lblMenuTitleBorder.Parent = pnlMenu;
            lblMenuTitleBorder.Text = "Guardiões das Regras";
            lblMenuTitleBorder.Font = new Font("Arial", (int)(pnlMenu.Size.Height * 0.100), FontStyle.Bold);
            lblMenuTitleBorder.AutoSize = true;
            RelativeLocation(lblMenuTitleBorder, 0.5, 0.18); //0.5,033
            lblMenuTitleBorder.ForeColor = Color.Black;
            lblMenuTitleBorder.BackColor = Color.Transparent;
            lblMenuTitleBorder.Visible = true;
            lblMenuTitleBorder.Enabled = true;

            lblMenuTitle.Parent = lblMenuTitleBorder;
            lblMenuTitle.Text = "Guardiões das Regras";
            lblMenuTitle.Font = new Font("Arial", (int)(pnlMenu.Size.Height * 0.098), FontStyle.Bold);
            lblMenuTitle.AutoSize = true;
            RelativeLocation(lblMenuTitle, 0.5, 0.5);
            lblMenuTitle.ForeColor = Color.FromArgb(238, 25, 25);
            lblMenuTitle.BackColor = Color.Transparent;
            lblMenuTitle.Visible = true;
            lblMenuTitle.Enabled = true;

            lblMenuStart.Parent = pnlMenu;
            lblMenuStart.Name = "Start";
            lblMenuStart.Text = "Iniciar";
            lblMenuStart.Font = new Font("Arial", (int)(pnlMenu.Size.Height * 0.04), FontStyle.Bold);
            lblMenuStart.AutoSize = true;
            RelativeLocation(lblMenuStart, 0.225, 0.45); //0.5,0.45
            lblMenuStart.ForeColor = Color.FromArgb(238, 60, 60);
            lblMenuStart.BackColor = Color.Transparent;
            lblMenuStart.MouseEnter += LabelMenu_MouseEnter;
            lblMenuStart.MouseLeave += LabelMenu_MouseLeave;
            lblMenuStart.MouseClick += MenuStart_MouseClick;
            lblMenuStart.Visible = true;
            lblMenuStart.Enabled = true;

            lblMenuHowToPlay.Parent = pnlMenu;
            lblMenuHowToPlay.Name = "HowToPlay";
            lblMenuHowToPlay.Text = "Como Jogar";
            lblMenuHowToPlay.Font = new Font("Arial", (int)(pnlMenu.Size.Height * 0.04), FontStyle.Bold);
            lblMenuHowToPlay.AutoSize = true;
            RelativeLocation(lblMenuHowToPlay, 0.225, 0.52); //0.5,0.55
            lblMenuHowToPlay.ForeColor = Color.FromArgb(238, 60, 60);
            lblMenuHowToPlay.BackColor = Color.Transparent;
            lblMenuHowToPlay.MouseEnter += LabelMenu_MouseEnter;
            lblMenuHowToPlay.MouseLeave += LabelMenu_MouseLeave;
            lblMenuHowToPlay.MouseClick += MenuHowToPlay_MouseClick;
            lblMenuHowToPlay.Visible = true;
            lblMenuHowToPlay.Enabled = true;

            lblMenuExit.Parent = pnlMenu;
            lblMenuExit.Name = "Exit";
            lblMenuExit.Text = "Sair";
            lblMenuExit.Font = new Font("Arial", (int)(pnlMenu.Size.Height * 0.04), FontStyle.Bold);
            lblMenuExit.AutoSize = true;
            RelativeLocation(lblMenuExit, 0.225, 0.59); // 0.5,0.65
            lblMenuExit.ForeColor = Color.FromArgb(238, 60, 60);
            lblMenuExit.BackColor = Color.Transparent;
            lblMenuExit.MouseEnter += LabelMenu_MouseEnter;
            lblMenuExit.MouseLeave += LabelMenu_MouseLeave;
            lblMenuExit.MouseClick += MenuExit_MouseClick;
            lblMenuExit.Visible = true;
            lblMenuExit.Enabled = true;

            lblMenuTitleBorder.Controls.Add(lblMenuTitle);

            pnlMenu.Controls.Add(lblMenuExit);
            pnlMenu.Controls.Add(lblMenuHowToPlay);
            pnlMenu.Controls.Add(lblMenuStart);
            pnlMenu.Controls.Add(lblMenuTitleBorder);

            pictMenuBg.Controls.Add(pnlMenu);

            this.Controls.Add(pictMenuBg);

            pictMenuBg.Image = menu_bg;
        }
        private void MenuStart_MouseClick(object sender, MouseEventArgs e)
        {
            MainGame();
        }
        private void MenuHowToPlay_MouseClick(object sender, MouseEventArgs e)
        {
            HowToPlay();
        }
        private void MenuExit_MouseClick(object sender, MouseEventArgs e)
        {
            Application.Exit();
        }
        private void LabelMenu_MouseEnter(object sender, EventArgs e)
        {
            Label lblSender = sender as Label;
            lblSender.BackColor = Color.Transparent;
            lblSender.ForeColor = Color.FromArgb(243, 25, 25);
            this.Cursor = Cursors.Hand;
        }
        private void LabelMenu_MouseLeave(object sender, EventArgs e)
        {
            Label lblSender = sender as Label;
            lblSender.BackColor = Color.Transparent;
            lblSender.ForeColor = Color.FromArgb(238, 60, 60);
            this.Cursor = Cursors.Default;
        }

        private void HowToPlay()
        {
            ResetForm();
            SetHowToPlay(resolution);
        }
        private void SetHowToPlay(int resolution)
        {
            Panel pnlGameFog = new Panel();
            Panel pnlHowToPlay = new Panel();
            Label pnlHowToPlayBorder = new Label();
            Label pnlHowToPlayBack = new Label();
            Label lblHowToPlayTitle = new Label();
            Label lblHowToPlayText = new Label();
            Button btnHowToPlayBack = new Button();

            pnlGameFog.Parent = this;
            RelativeSize(pnlGameFog);
            RelativeLocation(pnlGameFog, 0.5, 0.5);
            pnlGameFog.BackColor = Color.FromArgb(160, 160, 160);

            pnlHowToPlay.Parent = this;
            RelativeSize(pnlHowToPlay, 0.5, 0.7);
            RelativeLocation(pnlHowToPlay, 0.5, 0.5);
            pnlHowToPlay.BackColor = Color.Transparent;

            pnlHowToPlayBorder.Parent = pnlHowToPlay;
            pnlHowToPlayBorder.Text = "";
            pnlHowToPlayBorder.AutoSize = false;
            RelativeSize(pnlHowToPlayBorder);
            RelativeLocation(pnlHowToPlayBorder, 0.5, 0.5);
            pnlHowToPlayBorder.BackColor = Color.FromArgb(228, 87, 87);

            pnlHowToPlayBack.Parent = pnlHowToPlay;
            pnlHowToPlayBack.Text = "";
            pnlHowToPlayBack.AutoSize = false;
            RelativeSize(pnlHowToPlayBack, 0.95 * (16 / 9), 0.95);
            RelativeLocation(pnlHowToPlayBack, 0.5, 0.5);
            pnlHowToPlayBack.BackColor = Color.FromArgb(217, 217, 217);

            lblHowToPlayTitle.Parent = pnlHowToPlay;
            lblHowToPlayTitle.Font = new Font("Arial", (int)(pnlHowToPlay.Size.Height * 0.09), FontStyle.Bold);
            lblHowToPlayTitle.Text = "Como jogar";
            lblHowToPlayTitle.AutoSize = true;
            lblHowToPlayTitle.BackColor = Color.FromArgb(217, 217, 217);
            lblHowToPlayTitle.ForeColor = Color.FromArgb(228, 87, 87);
            RelativeLocation(lblHowToPlayTitle, 0.5, 0.13);

            lblHowToPlayText.Parent = pnlHowToPlay;
            lblHowToPlayText.Font = new Font("Arial", (int)(pnlHowToPlay.Size.Height * 0.02), FontStyle.Bold);
            lblHowToPlayText.Text = "" +
                "Um jogador estará com o Manual do Guardião das Regras em mãos, e os outros\njogadores estarão vendo os casos.\r\n\r\n" +
                "Um não deve conseguir ver o que o outro está fazendo!\r\n\r\nEm cada caso será dada uma planta arquitetônica e algumas informações extras\nsobre a planta em questão. Também, à direita, serão dadas quatro opções, que\nsão elas: \"Extintores de incêndio\", \"Saídas de emergência\", \"Rotas de fuga\" e\n\"Alarmes de incêndio\"." +
                "\r\n\r\nVocê deverá marcar essas opções de acordo com as regras do Manual do\nGuardião das Regras, que estará sendo lido pelo seu companheiro de equipe.\r\n\r\nVocês terão trinta segundos para resolver cada caso, que se não responderem à\ntempo será entregue o que já fizeram. Portanto, comunicação objetiva e\neficiente é essencial!";
            lblHowToPlayText.AutoSize = true;
            lblHowToPlayText.BackColor = Color.FromArgb(217, 217, 217);
            lblHowToPlayText.ForeColor = Color.FromArgb(228, 100, 100);
            RelativeLocation(lblHowToPlayText, 0.5, 0.5);

            btnHowToPlayBack.Parent = pnlHowToPlay;
            btnHowToPlayBack.Font = new Font("Arial", (float)(pnlHowToPlay.Size.Height * 0.025), FontStyle.Bold);
            btnHowToPlayBack.Text = "Voltar";
            btnHowToPlayBack.BackColor = Color.FromArgb(107, 200, 118);
            btnHowToPlayBack.FlatStyle = FlatStyle.Flat;
            btnHowToPlayBack.FlatAppearance.BorderSize = 0;
            btnHowToPlayBack.ForeColor = Color.Black;
            btnHowToPlayBack.Click += HowToPlayBack_Click;
            RelativeSize(btnHowToPlayBack, 0.6, 0.11);
            RelativeLocation(btnHowToPlayBack, 0.5, 0.9);

            pnlHowToPlay.Controls.Add(btnHowToPlayBack);
            pnlHowToPlay.Controls.Add(lblHowToPlayText);
            pnlHowToPlay.Controls.Add(lblHowToPlayTitle);
            pnlHowToPlay.Controls.Add(pnlHowToPlayBack);
            pnlHowToPlay.Controls.Add(pnlHowToPlayBorder);

            pnlGameFog.Controls.Add(pnlHowToPlay);
            this.Controls.Add(pnlGameFog);
            pnlGameFog.BringToFront();
        }
        private void HowToPlayBack_Click(object sender, EventArgs e)
        {
            MainMenu();
        }
        #endregion

        #region MainGame
        private void MainGame()
        {
            ResetForm();
            SetGameDesign(resolution);
            game = new Game(30);
            Label lblDynamicHeaderTimer = GetComponent("lblDynamicHeaderTimer") as Label;
            if (lblDynamicHeaderTimer == null) return;

            timer = new CancellationTokenSource();
            token = timer.Token;
            TimerCounter(lblDynamicHeaderTimer);
            UpdateMainGame();
        }
        private void SetGameDesign(int resolution)
        {
            #region pnlHeader
            Panel pnlHeader = new Panel();
            Label pnlHeaderBack = new Label();
            Label pnlHeaderTitle = new Label();
            PictureBox pictHeaderTimer = new PictureBox();
            Label lblStaticHeaderTimer = new Label();
            Label lblDynamicHeaderTimer = new Label();

            pnlHeader.Parent = this;
            pnlHeader.Name = "pnlHeader";
            RelativeSize(pnlHeader, 1, 0.15);
            RelativeLocation(pnlHeader, 0.5, 0.075);

            pnlHeaderBack.Parent = pnlHeader;
            RelativeSize(pnlHeaderBack);
            RelativeLocation(pnlHeaderBack);
            pnlHeaderBack.BackColor = Color.FromArgb(228, 87, 87);

            pnlHeaderTitle.Text = "";
            pnlHeaderTitle.Name = "pnlHeaderTitle";
            pnlHeaderTitle.AutoSize = false;
            pnlHeaderTitle.Parent = pnlHeaderBack;
            pnlHeaderTitle.AutoSize = false;
            RelativeSize(pnlHeaderTitle, 0.52, 1);
            RelativeLocation(pnlHeaderTitle, 0.34, 0.5);
            pnlHeaderTitle.Font = new Font("Arial", (float)(resolution * 0.027), FontStyle.Bold);
            pnlHeaderTitle.BackColor = Color.Transparent;
            pnlHeaderTitle.ForeColor = Color.White;
            pnlHeaderTitle.TextAlign = ContentAlignment.MiddleLeft;

            pictHeaderTimer.Parent = pnlHeaderBack;
            RelativeSize(pictHeaderTimer, 0.08, 1);
            RelativeLocation(pictHeaderTimer, 0.74, 0.5);
            pictHeaderTimer.Image = Image.FromFile(projectDirectory + "\\img\\hourglass.png");
            pictHeaderTimer.SizeMode = PictureBoxSizeMode.StretchImage;

            lblStaticHeaderTimer.Parent = pnlHeaderBack;
            lblStaticHeaderTimer.Name = "lblStaticHeaderTimer";
            lblStaticHeaderTimer.AutoSize = false;
            RelativeSize(lblStaticHeaderTimer, 0.05, 0.8);
            RelativeLocation(lblStaticHeaderTimer, 0.871, 0.5);
            lblStaticHeaderTimer.Font = new Font("Arial", (float)(resolution * 0.027), FontStyle.Bold);
            lblStaticHeaderTimer.TextAlign = ContentAlignment.BottomLeft;
            lblStaticHeaderTimer.BackColor = Color.Transparent;
            lblStaticHeaderTimer.ForeColor = Color.White;


            lblDynamicHeaderTimer.Parent = pnlHeaderBack;
            lblDynamicHeaderTimer.Name = "lblDynamicHeaderTimer";
            lblDynamicHeaderTimer.AutoSize = false;
            RelativeSize(lblDynamicHeaderTimer, 0.09, 1);
            RelativeLocation(lblDynamicHeaderTimer, 0.815, 0.5);
            lblDynamicHeaderTimer.Font = new Font("Arial", (float)(resolution * 0.072), FontStyle.Bold);
            lblDynamicHeaderTimer.TextAlign = ContentAlignment.MiddleLeft;
            lblDynamicHeaderTimer.BackColor = Color.Transparent;
            lblDynamicHeaderTimer.ForeColor = Color.White;
            #endregion

            #region pnlRBar
            Panel pnlRBar = new Panel();
            Label pnlRBarBack = new Label();
            Panel pnlRBarForm = new Panel();
            Label pnlRBarFormBorder = new Label();
            Label pnlRBarFormBack = new Label();
            Label lblCaseRisk = new Label();
            Label lblCaseWeight = new Label();
            Label lblCaseC = new Label();
            Label lblCaseP = new Label();
            CheckedListBox clCaseOptions = new CheckedListBox();
            Button btnNextCase = new Button();

            pnlRBar.Parent = this;
            pnlRBar.Name = "pnlRBar";
            RelativeSize(pnlRBar, 0.3, 0.85);
            RelativeLocation(pnlRBar, 0.85, 0.575);

            pnlRBarBack.Parent = pnlRBar;
            RelativeSize(pnlRBarBack, 1, 1);
            RelativeLocation(pnlRBarBack);
            pnlRBarBack.BackColor = Control.DefaultBackColor;
            pnlRBarBack.Text = "";
            pnlRBarForm.Parent = pnlRBarBack;
            RelativeSize(pnlRBarForm, 0.8333, 0.6588);
            RelativeLocation(pnlRBarForm, 0.5, 0.4);
            pnlRBarForm.BackColor = Color.Black;

            pnlRBarFormBorder.Parent = pnlRBarForm;
            pnlRBarFormBorder.AutoSize = false;
            RelativeSize(pnlRBarFormBorder);
            RelativeLocation(pnlRBarFormBorder);
            pnlRBarFormBorder.BackColor = Color.FromArgb(228, 87, 87);
            pnlRBarFormBorder.Text = "";

            pnlRBarFormBack.Parent = pnlRBarForm;
            pnlRBarFormBack.AutoSize = false;
            RelativeSize(pnlRBarFormBack, 0.937, 0.95);
            RelativeLocation(pnlRBarFormBack);
            pnlRBarFormBack.BackColor = Color.FromArgb(217, 217, 217);
            pnlRBarFormBack.Text = "";

            lblCaseRisk.Parent = pnlRBarFormBack;
            lblCaseRisk.Name = "lblCaseRisk";
            lblCaseRisk.Font = new Font("Arial", (int)(resolution * 0.0142), FontStyle.Bold);
            lblCaseRisk.AutoSize = true;
            RelativeLocation(lblCaseRisk, 0.02, 0.075, AnchorPos.TopLeft);
            lblCaseRisk.ForeColor = Color.Black;
            lblCaseRisk.BackColor = Color.Transparent;

            lblCaseWeight.Parent = pnlRBarFormBack;
            lblCaseWeight.Name = "lblCaseWeight";
            lblCaseWeight.Font = new Font("Arial", (int)(resolution * 0.0142), FontStyle.Bold);
            lblCaseWeight.AutoSize = true;
            RelativeLocation(lblCaseWeight, 0.478, 0.075, AnchorPos.TopLeft);
            lblCaseWeight.ForeColor = Color.Black;
            lblCaseWeight.BackColor = Color.Transparent;

            lblCaseC.Parent = pnlRBarFormBack;
            lblCaseC.Name = "lblCaseC";
            lblCaseC.Font = new Font("Arial", (int)(resolution * 0.0166), FontStyle.Bold);
            lblCaseC.AutoSize = true;
            RelativeLocation(lblCaseC, 0.02, 0.1503, AnchorPos.TopLeft);
            lblCaseC.ForeColor = Color.Black;
            lblCaseC.BackColor = Color.Transparent;

            lblCaseP.Parent = pnlRBarFormBack;
            lblCaseP.Name = "lblCaseP";
            lblCaseP.Font = new Font("Arial", (int)(resolution * 0.0166), FontStyle.Bold);
            lblCaseP.AutoSize = true;
            RelativeLocation(lblCaseP, 0.02, 0.2255, AnchorPos.TopLeft);
            lblCaseP.ForeColor = Color.Black;
            lblCaseP.BackColor = Color.Transparent;

            clCaseOptions.Parent = pnlRBarFormBack;
            clCaseOptions.Name = "clCaseOptions";
            RelativeSize(clCaseOptions, 0.952, 0.5);
            RelativeLocation(clCaseOptions, 0.024, 0.5, AnchorPos.TopLeft);
            clCaseOptions.Font = new Font("Arial", (int)(resolution * 0.025), FontStyle.Bold);
            clCaseOptions.BackColor = Color.FromArgb(217, 217, 217);
            clCaseOptions.BorderStyle = BorderStyle.None;
            clCaseOptions.CheckOnClick = true;
            clCaseOptions.Items.AddRange(new object[] {
            "Extintores de incêndio",
            "Saídas de emergência",
            "Rotas de fuga",
            "Alarmes de incêndio"});
            clCaseOptions.ItemCheck += CaseOptions_ItemCheck;

            btnNextCase.Parent = pnlRBarBack;
            RelativeSize(btnNextCase, 0.8, 0.1176);
            RelativeLocation(btnNextCase, 0.5, 0.847);
            btnNextCase.BackColor = Color.FromArgb(107, 200, 118);
            btnNextCase.FlatStyle = FlatStyle.Flat;
            btnNextCase.FlatAppearance.BorderSize = 0;
            btnNextCase.Text = "Próximo";
            btnNextCase.Font = new Font("Arial", (float)(resolution * 0.02), FontStyle.Bold);
            btnNextCase.Click += NextCase_Click;
            #endregion

            #region pnlCenter
            Panel pnlCenter = new Panel();
            Label pnlCenterBack = new Label();
            PictureBox pictCaseImage = new PictureBox();
            Label lblCaseImageDesc = new Label();

            pnlCenter.Parent = this;
            pnlCenter.Name = "pnlCenter";
            RelativeSize(pnlCenter, 0.62, 0.71);
            RelativeLocation(pnlCenter, 0.35, 0.575);
            pnlCenter.BackColor = Color.Transparent;

            pnlCenterBack.Parent = pnlCenter;
            pnlCenterBack.AutoSize = false;
            RelativeSize(pnlCenterBack);
            RelativeLocation(pnlCenterBack, 0.5, 0.5);
            pnlCenterBack.BackColor = Control.DefaultBackColor;
            pnlCenterBack.Text = "";

            pictCaseImage.Parent = pnlCenterBack;
            pictCaseImage.Name = "pictCaseImage";
            RelativeSize(pictCaseImage, 0.9677, 0.845);
            RelativeLocation(pictCaseImage, 0, 0, AnchorPos.TopLeft);

            lblCaseImageDesc.Parent = pnlCenterBack;
            lblCaseImageDesc.Name = "lblCaseImageDesc";
            lblCaseImageDesc.AutoSize = false;
            RelativeSize(lblCaseImageDesc, 0.4516, 0.0704);
            RelativeLocation(lblCaseImageDesc, 0.2258, 0.9084);
            lblCaseImageDesc.Text = "Estado do projeto: Reprovado";
            lblCaseImageDesc.Font = new Font("Arial", (int)(resolution * 0.019), FontStyle.Bold);
            lblCaseImageDesc.BackColor = Color.Transparent;
            lblCaseImageDesc.ForeColor = Color.Black;
            lblCaseImageDesc.TextAlign = ContentAlignment.MiddleLeft;
            #endregion

            #region Controls
            pnlCenterBack.Controls.Add(pictCaseImage);
            pnlCenterBack.Controls.Add(lblCaseImageDesc);
            pnlCenter.Controls.Add(pnlCenterBack);

            pnlRBarFormBack.Controls.Add(clCaseOptions);
            pnlRBarFormBack.Controls.Add(lblCaseC);
            pnlRBarFormBack.Controls.Add(lblCaseP);
            pnlRBarFormBack.Controls.Add(lblCaseWeight);
            pnlRBarFormBack.Controls.Add(lblCaseRisk);
            pnlRBarForm.Controls.Add(pnlRBarFormBack);
            pnlRBarForm.Controls.Add(pnlRBarFormBorder);

            pnlRBarBack.Controls.Add(btnNextCase);
            pnlRBarBack.Controls.Add(pnlRBarForm);
            pnlRBar.Controls.Add(pnlRBarBack);

            pnlHeaderBack.Controls.Add(lblStaticHeaderTimer);
            pnlHeaderBack.Controls.Add(lblDynamicHeaderTimer);
            pnlHeaderBack.Controls.Add(pictHeaderTimer);
            pnlHeaderBack.Controls.Add(pnlHeaderTitle);
            pnlHeader.Controls.Add(pnlHeaderBack);

            this.Controls.Add(pnlCenter);
            this.Controls.Add(pnlRBar);
            this.Controls.Add(pnlHeader);
            #endregion
        }
        private void UpdateMainGame()
        {
            Label pnlHeaderTitle = GetComponent("pnlHeaderTitle") as Label;
            Label lblCaseRisk = GetComponent("lblCaseRisk") as Label;
            Label lblCaseC = GetComponent("lblCaseC") as Label;
            Label lblCaseWeight = GetComponent("lblCaseWeight") as Label;
            Label lblCaseP = GetComponent("lblCaseP") as Label;
            Label lblStaticHeaderTimer = GetComponent("lblStaticHeaderTimer") as Label;
            Label lblDynamicHeaderTimer = GetComponent("lblDynamicHeaderTimer") as Label;
            PictureBox pictCaseImage = GetComponent("pictCaseImage") as PictureBox;
            CheckedListBox clCaseOptions = GetComponent("clCaseOptions") as CheckedListBox;
            if (clCaseOptions == null || pictCaseImage == null || lblDynamicHeaderTimer == null || lblStaticHeaderTimer == null || pnlHeaderTitle == null || lblCaseRisk == null || lblCaseWeight == null || lblCaseP == null || lblCaseC == null) return;


            pnlHeaderTitle.Text = $"Caso {game.caseNumber} - {game.currentCase.name}";
            lblCaseRisk.Text = $"Risco: {game.currentCase.RiskText()}";
            lblCaseWeight.Text = $"Gravidade: {game.currentCase.WeightText()}";
            lblCaseC.Text = $"Capacidade de Passagem: {game.currentCase.c}";
            lblCaseP.Text = $"População: {game.currentCase.p}";
            pictCaseImage.Image = Image.FromFile(projectDirectory + $"\\img\\{game.currentCase.imgName}");
            lblStaticHeaderTimer.Text = $"/{game.maxTimer}";
            lblDynamicHeaderTimer.Text = $"{game.maxTimer}";
            while (clCaseOptions.CheckedIndices.Count > 0)
            {
                clCaseOptions.SetItemChecked(clCaseOptions.CheckedIndices[0], false);
            }
        }
        private void CaseOptions_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            Label lblCaseImageDesc = GetComponent("lblCaseImageDesc") as Label;
            CheckedListBox clCaseOptions = sender as CheckedListBox;
            if (lblCaseImageDesc == null) return;

            if (clCaseOptions.CheckedItems.Count == 3 && e.NewValue == CheckState.Checked)
                lblCaseImageDesc.Text = "Estado do projeto: Aprovado";
            else
                lblCaseImageDesc.Text = "Estado do projeto: Reprovado";
        }
        private void NextCase_Click(object sender, EventArgs e)
        {
            UpdateCase();
        }
        private void UpdateCase()
        {
            CheckedListBox clCaseOptions = GetComponent("clCaseOptions") as CheckedListBox;
            if (clCaseOptions == null) return;

            game.UpdateCase(clCaseOptions);
            UpdateMainGame();

            ResetTimer();

            if (game.End())
            {
                GameResults();
                timer.Cancel();
            }
        }
        private void ResetTimer()
        {
            Label lblTimer = GetComponent("lblDynamicHeaderTimer") as Label;
            if (lblTimer == null) return;
            timer.Cancel();
            timer.Dispose();

            timer = new CancellationTokenSource();
            token = timer.Token;
            TimerCounter(lblTimer);
        }
        private async void TimerCounter(Label lblTimer)
        {
            if (game.Timer == 0)
            {
                UpdateCase();
                return;
            }
            try
            {
                await Task.Delay(1000, token);
            }
            catch (OperationCanceledException)
            {
                return;
            }

            game.Timer--;

            lblTimer.Text = $"{game.Timer}";
            TimerCounter(lblTimer);
        }
        #endregion

        #region GameResults
        private void GameResults()
        {
            ResetForm();
            SetResultDesign(resolution);
        }
        public void SetResultDesign(int resolution)
        {
            int resX = resolution * 16 / 9, resY = resolution;

            this.Controls.Clear();
            Panel pnlGameFog = new Panel();
            GroupBox gpbResult = new GroupBox();
            Label gpbResultBack = new Label();
            Label gpbResultBorder = new Label();
            Label lblResultado = new Label();

            Panel[] pnlResultCase = new Panel[4] { new Panel(), new Panel(), new Panel(), new Panel() };
            Panel[] pnlResultCaseBorder = new Panel[4] { new Panel(), new Panel(), new Panel(), new Panel() };
            Panel[] pnlResultCaseBack = new Panel[4] { new Panel(), new Panel(), new Panel(), new Panel() };
            Label[] lblCaseName = new Label[4] { new Label(), new Label(), new Label(), new Label() };
            Label[] lblTextos = new Label[4] { new Label(), new Label(), new Label(), new Label() };
            Button btnBack = new Button();

            pnlGameFog.Parent = this;
            RelativeSize(pnlGameFog);
            RelativeLocation(pnlGameFog, 0.5, 0.5);
            pnlGameFog.BackColor = Color.FromArgb(160, 160, 160);

            gpbResult.Parent = pnlGameFog;
            RelativeSize(gpbResult, 0.6, 0.9);
            RelativeLocation(gpbResult, 0.5, 0.475);
            gpbResult.BackColor = Color.FromArgb(217, 217, 217);

            gpbResultBorder.Parent = gpbResult;
            gpbResultBorder.AutoSize = false;
            RelativeSize(gpbResultBorder);
            RelativeLocation(gpbResultBorder, 0.5, 0.5);
            gpbResultBorder.BackColor = Color.FromArgb(228, 87, 87);
            gpbResultBorder.Text = "";

            gpbResultBack.Parent = gpbResult;
            gpbResultBack.AutoSize = false;
            RelativeSize(gpbResultBack, 0.95 * (16 / 9), 0.95);
            RelativeLocation(gpbResultBack, 0.5, 0.5);
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
            for (int i = 0; i < 4; i++)
            {
                name = game.results.Name();
                indexesResults = game.results.IndexesResults();

                pnlResultCase[i].Parent = gpbResultBack;
                pnlResultCase[i].Location = new Point((int)(j), (int)(gpbResultBack.Size.Height * 0.2 + z));
                RelativeSize(pnlResultCase[i], 0.5, 0.3);
                pnlResultCase[i].BackColor = Color.FromArgb(217, 217, 217);

                pnlResultCaseBorder[i].Parent = pnlResultCase[i];
                pnlResultCaseBorder[i].Location = new Point(0, 0);
                RelativeSize(pnlResultCaseBorder[i]);
                pnlResultCaseBorder[i].BackColor = Color.FromArgb(210, 210, 210);
                pnlResultCaseBorder[i].Text = "";
                pnlResultCaseBorder[i].Visible = false;

                pnlResultCaseBack[i].Parent = pnlResultCase[i];
                pnlResultCaseBack[i].Location = new Point(0, 0);
                RelativeSize(pnlResultCaseBack[i], 0.98, 0.95);
                RelativeLocation(pnlResultCaseBack[i], 0.5, 0.5);
                pnlResultCaseBack[i].BackColor = Color.FromArgb(217, 217, 217);
                pnlResultCaseBack[i].Text = "";

                lblCaseName[i].Parent = pnlResultCaseBack[i];
                lblCaseName[i].AutoSize = true;
                lblCaseName[i].Text = $"{name}";
                lblCaseName[i].Font = new Font("Arial", (int)(resolution * 0.02), FontStyle.Bold);
                lblCaseName[i].BackColor = Color.Transparent;
                lblCaseName[i].ForeColor = Color.Black;
                lblCaseName[i].Location = new Point((int)(pnlResultCase[i].Size.Width * 0.5 - lblCaseName[i].Size.Width * 0.5), (int)(pnlResultCase[i].Size.Height * 0.1));
                pnlResultCaseBack[i].Controls.Add(lblCaseName[i]);

                lblTextos[i].Parent = pnlResultCaseBack[i];
                lblTextos[i].AutoSize = true;
                lblTextos[i].Text = $"Extintores de incêndio: {indexesResults[0]}\nSaídas de emergência: {indexesResults[1]}\nRotas de fuga: {indexesResults[2]}\nAlarmes de incêndio: {indexesResults[3]}";
                lblTextos[i].Font = new Font("Arial", (int)(resolution * 0.015), FontStyle.Bold);
                lblTextos[i].BackColor = Color.Transparent;
                lblTextos[i].ForeColor = Color.Black;
                RelativeLocation(lblTextos[i], 0.5, 0.5);
                lblTextos[i].TextAlign = ContentAlignment.MiddleCenter;
                pnlResultCaseBack[i].Controls.Add(lblTextos[i]);

                pnlResultCase[i].Controls.Add(pnlResultCaseBack[i]);
                pnlResultCase[i].Controls.Add(pnlResultCaseBorder[i]);
                gpbResultBack.Controls.Add(pnlResultCase[i]);
                if (j == 0)
                    j = pnlResultCase[i].Size.Width;
                else if (z == 0)
                {
                    z = pnlResultCase[i].Size.Height;
                    j = 0;
                }
            }

            btnBack.Parent = gpbResult;
            btnBack.Text = "Voltar para o menu";
            btnBack.AutoSize = false;
            RelativeSize(btnBack, 0.5, 0.1);
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

            pnlGameFog.Controls.Add(gpbResult);
            this.Controls.Add(pnlGameFog);
        }

        private void ResultBack_Click(object sender, EventArgs e)
        {
            MainMenu();
        }
        #endregion

    }
}
