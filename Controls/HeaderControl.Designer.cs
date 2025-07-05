namespace DinoTrack.Controls
{
    partial class HeaderControl
    {
        private System.ComponentModel.IContainer components = null;
        private Guna.UI2.WinForms.Guna2GradientPanel gradientPanel;
        private Guna.UI2.WinForms.Guna2CircleButton btnClose;
        private Guna.UI2.WinForms.Guna2CircleButton btnMinimize;
        private Guna.UI2.WinForms.Guna2CircleButton btnMaximize;
        private Guna.UI2.WinForms.Guna2CirclePictureBox guna2CirclePictureBox1;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2GradientButton BtnMenu;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges14 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges13 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            gradientPanel = new Guna.UI2.WinForms.Guna2GradientPanel();
            BtnMenu = new Guna.UI2.WinForms.Guna2GradientButton();
            guna2CirclePictureBox1 = new Guna.UI2.WinForms.Guna2CirclePictureBox();
            guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            btnClose = new Guna.UI2.WinForms.Guna2CircleButton();
            btnMinimize = new Guna.UI2.WinForms.Guna2CircleButton();
            btnMaximize = new Guna.UI2.WinForms.Guna2CircleButton();
            toolTip1 = new ToolTip(components);
            gradientPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)guna2CirclePictureBox1).BeginInit();
            SuspendLayout();
            // 
            // gradientPanel
            // 
            gradientPanel.Controls.Add(BtnMenu);
            gradientPanel.Controls.Add(guna2CirclePictureBox1);
            gradientPanel.Controls.Add(guna2HtmlLabel1);
            gradientPanel.Controls.Add(btnClose);
            gradientPanel.Controls.Add(btnMinimize);
            gradientPanel.Controls.Add(btnMaximize);
            gradientPanel.CustomizableEdges = customizableEdges14;
            gradientPanel.Dock = DockStyle.Fill;
            gradientPanel.FillColor = Color.FromArgb(0, 64, 64);
            gradientPanel.FillColor2 = Color.FromArgb(0, 192, 192);
            gradientPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            gradientPanel.Location = new Point(0, 0);
            gradientPanel.Name = "gradientPanel";
            gradientPanel.ShadowDecoration.CustomizableEdges = customizableEdges11;
            gradientPanel.Size = new Size(1219, 40);
            gradientPanel.TabIndex = 0;
            // 
            // BtnMenu
            // 
            BtnMenu.Animated = true;
            BtnMenu.BorderStyle = System.Drawing.Drawing2D.DashStyle.Custom;
            BtnMenu.CustomizableEdges = customizableEdges8;
            BtnMenu.DisabledState.BorderColor = Color.DarkGray;
            BtnMenu.DisabledState.CustomBorderColor = Color.DarkGray;
            BtnMenu.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            BtnMenu.DisabledState.FillColor2 = Color.FromArgb(169, 169, 169);
            BtnMenu.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            BtnMenu.FillColor = Color.FromArgb(0, 64, 64);
            BtnMenu.FillColor2 = Color.FromArgb(0, 192, 192);
            BtnMenu.Font = new Font("Segoe UI", 9F);
            BtnMenu.ForeColor = Color.White;
            BtnMenu.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            BtnMenu.ImageSize = new Size(30, 30);
            BtnMenu.Location = new Point(133, 0);
            BtnMenu.Name = "BtnMenu";
            BtnMenu.ShadowDecoration.CustomizableEdges = customizableEdges9;
            BtnMenu.Size = new Size(47, 40);
            BtnMenu.TabIndex = 5;
            BtnMenu.Click += BtnMenu_Click;
            // 
            // guna2CirclePictureBox1
            // 
            guna2CirclePictureBox1.BackColor = Color.Transparent;
            guna2CirclePictureBox1.Image = Properties.Resources._3a614702f04cebb11ca163f6835a5512;
            guna2CirclePictureBox1.ImageRotate = 0F;
            guna2CirclePictureBox1.Location = new Point(10, 5);
            guna2CirclePictureBox1.Name = "guna2CirclePictureBox1";
            guna2CirclePictureBox1.ShadowDecoration.CustomizableEdges = customizableEdges10;
            guna2CirclePictureBox1.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            guna2CirclePictureBox1.Size = new Size(30, 30);
            guna2CirclePictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            guna2CirclePictureBox1.TabIndex = 0;
            guna2CirclePictureBox1.TabStop = false;
            // 
            // guna2HtmlLabel1
            // 
            guna2HtmlLabel1.BackColor = Color.Transparent;
            guna2HtmlLabel1.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            guna2HtmlLabel1.ForeColor = Color.White;
            guna2HtmlLabel1.Location = new Point(46, 5);
            guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            guna2HtmlLabel1.Size = new Size(81, 23);
            guna2HtmlLabel1.TabIndex = 1;
            guna2HtmlLabel1.Text = "DinoTrack";
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClose.BackColor = Color.Transparent;
            btnClose.FillColor = Color.FromArgb(220, 53, 69);
            btnClose.Font = new Font("Segoe UI", 9F);
            btnClose.ForeColor = Color.White;
            btnClose.Location = new Point(1170, 5);
            btnClose.Name = "btnClose";
            btnClose.ShadowDecoration.CustomizableEdges = customizableEdges11;
            btnClose.Size = new Size(30, 30);
            btnClose.TabIndex = 2;
            btnClose.Text = "X";
            // 
            // btnMinimize
            // 
            btnMinimize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnMinimize.BackColor = Color.Transparent;
            btnMinimize.FillColor = Color.White;
            btnMinimize.Font = new Font("Segoe UI", 9F);
            btnMinimize.ForeColor = Color.Black;
            btnMinimize.Location = new Point(1110, 5);
            btnMinimize.Name = "btnMinimize";
            btnMinimize.ShadowDecoration.CustomizableEdges = customizableEdges12;
            btnMinimize.Size = new Size(30, 30);
            btnMinimize.TabIndex = 3;
            btnMinimize.Text = "_";
            // 
            // btnMaximize
            // 
            btnMaximize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnMaximize.BackColor = Color.Transparent;
            btnMaximize.FillColor = Color.White;
            btnMaximize.Font = new Font("Segoe UI", 9F);
            btnMaximize.ForeColor = Color.Black;
            btnMaximize.Location = new Point(1140, 5);
            btnMaximize.Name = "btnMaximize";
            btnMaximize.ShadowDecoration.CustomizableEdges = customizableEdges13;
            btnMaximize.Size = new Size(30, 30);
            btnMaximize.TabIndex = 4;
            btnMaximize.Text = "□";
            // 
            // HeaderControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(gradientPanel);
            Name = "HeaderControl";
            Size = new Size(1219, 40);
            gradientPanel.ResumeLayout(false);
            gradientPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)guna2CirclePictureBox1).EndInit();
            ResumeLayout(false);
        }
        private ToolTip toolTip1;
    }
}