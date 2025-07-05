namespace DinoTrack.Forms
{
    partial class FiltersForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FiltersForm));
            tabControl = new Guna.UI2.WinForms.Guna2TabControl();
            tabCategories = new TabPage();
            panelSelectAllCategories = new Guna.UI2.WinForms.Guna2Panel();
            guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblSelectedCategoriesCount = new Guna.UI2.WinForms.Guna2HtmlLabel();
            checkBoxSelectAllCategories = new Guna.UI2.WinForms.Guna2CustomCheckBox();
            flowSubCategories = new FlowLayoutPanel();
            flowMainCategories = new FlowLayoutPanel();
            tabLocations = new TabPage();
            panelSelectAllLocations = new Guna.UI2.WinForms.Guna2Panel();
            guna2HtmlLabel2 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblSelectedLocationsCount = new Guna.UI2.WinForms.Guna2HtmlLabel();
            checkBoxSelectAllLocations = new Guna.UI2.WinForms.Guna2CustomCheckBox();
            flowLocations = new FlowLayoutPanel();
            tableLayoutPanel1 = new TableLayoutPanel();
            buttonApply = new Guna.UI2.WinForms.Guna2Button();
            buttonCancel = new Guna.UI2.WinForms.Guna2Button();
            tabControl.SuspendLayout();
            tabCategories.SuspendLayout();
            panelSelectAllCategories.SuspendLayout();
            tabLocations.SuspendLayout();
            panelSelectAllLocations.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl
            // 
            tabControl.Controls.Add(tabCategories);
            tabControl.Controls.Add(tabLocations);
            tabControl.Dock = DockStyle.Fill;
            tabControl.ItemSize = new Size(150, 40);
            tabControl.Location = new Point(0, 0);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(706, 514);
            tabControl.TabButtonHoverState.BorderColor = Color.Empty;
            tabControl.TabButtonHoverState.FillColor = Color.White;
            tabControl.TabButtonHoverState.Font = new Font("Segoe UI Semibold", 10F);
            tabControl.TabButtonHoverState.ForeColor = Color.Black;
            tabControl.TabButtonHoverState.InnerColor = Color.White;
            tabControl.TabButtonIdleState.BorderColor = Color.Empty;
            tabControl.TabButtonIdleState.FillColor = Color.FromArgb(240, 240, 245);
            tabControl.TabButtonIdleState.Font = new Font("Segoe UI Semibold", 10F);
            tabControl.TabButtonIdleState.ForeColor = Color.FromArgb(100, 100, 100);
            tabControl.TabButtonIdleState.InnerColor = Color.FromArgb(240, 240, 245);
            tabControl.TabButtonSelectedState.BorderColor = Color.Empty;
            tabControl.TabButtonSelectedState.FillColor = Color.Teal;
            tabControl.TabButtonSelectedState.Font = new Font("Segoe UI Semibold", 10F);
            tabControl.TabButtonSelectedState.ForeColor = Color.White;
            tabControl.TabButtonSelectedState.InnerColor = Color.GreenYellow;
            tabControl.TabButtonSize = new Size(150, 40);
            tabControl.TabIndex = 0;
            tabControl.TabMenuBackColor = Color.FromArgb(240, 240, 245);
            tabControl.TabMenuOrientation = Guna.UI2.WinForms.TabMenuOrientation.HorizontalTop;
            // 
            // tabCategories
            // 
            tabCategories.BackColor = Color.Azure;
            tabCategories.Controls.Add(panelSelectAllCategories);
            tabCategories.Controls.Add(flowSubCategories);
            tabCategories.Controls.Add(flowMainCategories);
            tabCategories.Location = new Point(4, 44);
            tabCategories.Name = "tabCategories";
            tabCategories.Padding = new Padding(10);
            tabCategories.Size = new Size(698, 466);
            tabCategories.TabIndex = 0;
            tabCategories.Text = "Категории";
            // 
            // panelSelectAllCategories
            // 
            panelSelectAllCategories.BackColor = Color.White;
            panelSelectAllCategories.BorderColor = Color.Black;
            panelSelectAllCategories.BorderThickness = 1;
            panelSelectAllCategories.Controls.Add(guna2HtmlLabel1);
            panelSelectAllCategories.Controls.Add(lblSelectedCategoriesCount);
            panelSelectAllCategories.Controls.Add(checkBoxSelectAllCategories);
            panelSelectAllCategories.CustomizableEdges = customizableEdges3;
            panelSelectAllCategories.Dock = DockStyle.Top;
            panelSelectAllCategories.Location = new Point(328, 10);
            panelSelectAllCategories.Name = "panelSelectAllCategories";
            panelSelectAllCategories.ShadowDecoration.CustomizableEdges = customizableEdges4;
            panelSelectAllCategories.Size = new Size(360, 40);
            panelSelectAllCategories.TabIndex = 2;
            // 
            // guna2HtmlLabel1
            // 
            guna2HtmlLabel1.BackColor = Color.Transparent;
            guna2HtmlLabel1.Location = new Point(36, 13);
            guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            guna2HtmlLabel1.Size = new Size(71, 17);
            guna2HtmlLabel1.TabIndex = 3;
            guna2HtmlLabel1.Text = "Выбрать всё";
            // 
            // lblSelectedCategoriesCount
            // 
            lblSelectedCategoriesCount.BackColor = Color.Transparent;
            lblSelectedCategoriesCount.Font = new Font("Segoe UI", 9F);
            lblSelectedCategoriesCount.Location = new Point(129, 13);
            lblSelectedCategoriesCount.Name = "lblSelectedCategoriesCount";
            lblSelectedCategoriesCount.Size = new Size(65, 17);
            lblSelectedCategoriesCount.TabIndex = 2;
            lblSelectedCategoriesCount.Text = "Выбрано: 0";
            // 
            // checkBoxSelectAllCategories
            // 
            checkBoxSelectAllCategories.Animated = true;
            checkBoxSelectAllCategories.BackColor = Color.Teal;
            checkBoxSelectAllCategories.CheckedState.BorderRadius = 2;
            checkBoxSelectAllCategories.CheckedState.BorderThickness = 0;
            checkBoxSelectAllCategories.CheckedState.FillColor = Color.Teal;
            checkBoxSelectAllCategories.CustomizableEdges = customizableEdges1;
            checkBoxSelectAllCategories.Location = new Point(10, 10);
            checkBoxSelectAllCategories.Name = "checkBoxSelectAllCategories";
            checkBoxSelectAllCategories.ShadowDecoration.CustomizableEdges = customizableEdges2;
            checkBoxSelectAllCategories.Size = new Size(20, 20);
            checkBoxSelectAllCategories.TabIndex = 0;
            checkBoxSelectAllCategories.Text = "Выбрать все подкатегории";
            checkBoxSelectAllCategories.UncheckedState.BorderColor = Color.FromArgb(125, 137, 149);
            checkBoxSelectAllCategories.UncheckedState.BorderRadius = 2;
            checkBoxSelectAllCategories.UncheckedState.BorderThickness = 0;
            checkBoxSelectAllCategories.UncheckedState.FillColor = Color.FromArgb(125, 137, 149);
            // 
            // flowSubCategories
            // 
            flowSubCategories.AutoScroll = true;
            flowSubCategories.BackColor = Color.White;
            flowSubCategories.BorderStyle = BorderStyle.FixedSingle;
            flowSubCategories.Dock = DockStyle.Fill;
            flowSubCategories.FlowDirection = FlowDirection.TopDown;
            flowSubCategories.Location = new Point(328, 10);
            flowSubCategories.Name = "flowSubCategories";
            flowSubCategories.Padding = new Padding(10, 50, 10, 10);
            flowSubCategories.Size = new Size(360, 446);
            flowSubCategories.TabIndex = 1;
            flowSubCategories.WrapContents = false;
            // 
            // flowMainCategories
            // 
            flowMainCategories.BackColor = Color.Azure;
            flowMainCategories.Dock = DockStyle.Left;
            flowMainCategories.FlowDirection = FlowDirection.TopDown;
            flowMainCategories.Location = new Point(10, 10);
            flowMainCategories.Name = "flowMainCategories";
            flowMainCategories.Padding = new Padding(10);
            flowMainCategories.Size = new Size(318, 446);
            flowMainCategories.TabIndex = 0;
            flowMainCategories.WrapContents = false;
            // 
            // tabLocations
            // 
            tabLocations.BackColor = Color.LightCyan;
            tabLocations.Controls.Add(panelSelectAllLocations);
            tabLocations.Controls.Add(flowLocations);
            tabLocations.Location = new Point(4, 44);
            tabLocations.Name = "tabLocations";
            tabLocations.Padding = new Padding(10);
            tabLocations.Size = new Size(698, 466);
            tabLocations.TabIndex = 1;
            tabLocations.Text = "Местоположение";
            // 
            // panelSelectAllLocations
            // 
            panelSelectAllLocations.BackColor = Color.White;
            panelSelectAllLocations.BorderColor = Color.Black;
            panelSelectAllLocations.BorderThickness = 1;
            panelSelectAllLocations.Controls.Add(guna2HtmlLabel2);
            panelSelectAllLocations.Controls.Add(lblSelectedLocationsCount);
            panelSelectAllLocations.Controls.Add(checkBoxSelectAllLocations);
            panelSelectAllLocations.CustomizableEdges = customizableEdges7;
            panelSelectAllLocations.Dock = DockStyle.Top;
            panelSelectAllLocations.Location = new Point(10, 10);
            panelSelectAllLocations.Name = "panelSelectAllLocations";
            panelSelectAllLocations.ShadowDecoration.CustomizableEdges = customizableEdges8;
            panelSelectAllLocations.Size = new Size(678, 40);
            panelSelectAllLocations.TabIndex = 2;
            // 
            // guna2HtmlLabel2
            // 
            guna2HtmlLabel2.BackColor = Color.Transparent;
            guna2HtmlLabel2.Location = new Point(36, 13);
            guna2HtmlLabel2.Name = "guna2HtmlLabel2";
            guna2HtmlLabel2.Size = new Size(71, 17);
            guna2HtmlLabel2.TabIndex = 4;
            guna2HtmlLabel2.Text = "Выбрать всё";
            // 
            // lblSelectedLocationsCount
            // 
            lblSelectedLocationsCount.BackColor = Color.Transparent;
            lblSelectedLocationsCount.Font = new Font("Segoe UI", 9F);
            lblSelectedLocationsCount.Location = new Point(139, 13);
            lblSelectedLocationsCount.Name = "lblSelectedLocationsCount";
            lblSelectedLocationsCount.Size = new Size(65, 17);
            lblSelectedLocationsCount.TabIndex = 2;
            lblSelectedLocationsCount.Text = "Выбрано: 0";
            // 
            // checkBoxSelectAllLocations
            // 
            checkBoxSelectAllLocations.CheckedState.BorderRadius = 2;
            checkBoxSelectAllLocations.CheckedState.BorderThickness = 0;
            checkBoxSelectAllLocations.CheckedState.FillColor = Color.Teal;
            checkBoxSelectAllLocations.CustomizableEdges = customizableEdges5;
            checkBoxSelectAllLocations.Location = new Point(10, 10);
            checkBoxSelectAllLocations.Name = "checkBoxSelectAllLocations";
            checkBoxSelectAllLocations.ShadowDecoration.CustomizableEdges = customizableEdges6;
            checkBoxSelectAllLocations.Size = new Size(20, 20);
            checkBoxSelectAllLocations.TabIndex = 0;
            checkBoxSelectAllLocations.Text = "Выбрать все кабинеты";
            checkBoxSelectAllLocations.UncheckedState.BorderColor = Color.FromArgb(125, 137, 149);
            checkBoxSelectAllLocations.UncheckedState.BorderRadius = 2;
            checkBoxSelectAllLocations.UncheckedState.BorderThickness = 0;
            checkBoxSelectAllLocations.UncheckedState.FillColor = Color.FromArgb(125, 137, 149);
            // 
            // flowLocations
            // 
            flowLocations.AutoScroll = true;
            flowLocations.BackColor = Color.White;
            flowLocations.BorderStyle = BorderStyle.FixedSingle;
            flowLocations.Dock = DockStyle.Fill;
            flowLocations.FlowDirection = FlowDirection.TopDown;
            flowLocations.Location = new Point(10, 10);
            flowLocations.Name = "flowLocations";
            flowLocations.Padding = new Padding(10, 50, 10, 10);
            flowLocations.Size = new Size(678, 446);
            flowLocations.TabIndex = 0;
            flowLocations.WrapContents = false;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.BackColor = Color.FromArgb(240, 240, 245);
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            tableLayoutPanel1.Controls.Add(buttonApply, 1, 0);
            tableLayoutPanel1.Controls.Add(buttonCancel, 2, 0);
            tableLayoutPanel1.Dock = DockStyle.Bottom;
            tableLayoutPanel1.Location = new Point(0, 514);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(706, 40);
            tableLayoutPanel1.TabIndex = 2;
            // 
            // buttonApply
            // 
            buttonApply.BorderRadius = 5;
            buttonApply.CustomizableEdges = customizableEdges9;
            buttonApply.DisabledState.BorderColor = Color.DarkGray;
            buttonApply.DisabledState.CustomBorderColor = Color.DarkGray;
            buttonApply.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            buttonApply.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            buttonApply.Dock = DockStyle.Fill;
            buttonApply.FillColor = Color.White;
            buttonApply.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            buttonApply.ForeColor = Color.Black;
            buttonApply.HoverState.FillColor = Color.Green;
            buttonApply.HoverState.ForeColor = Color.White;
            buttonApply.Location = new Point(509, 3);
            buttonApply.Name = "buttonApply";
            buttonApply.ShadowDecoration.CustomizableEdges = customizableEdges10;
            buttonApply.Size = new Size(94, 34);
            buttonApply.TabIndex = 1;
            buttonApply.Text = "Применить";
            // 
            // buttonCancel
            // 
            buttonCancel.BorderRadius = 5;
            buttonCancel.CustomizableEdges = customizableEdges11;
            buttonCancel.DisabledState.BorderColor = Color.DarkGray;
            buttonCancel.DisabledState.CustomBorderColor = Color.DarkGray;
            buttonCancel.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            buttonCancel.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            buttonCancel.Dock = DockStyle.Fill;
            buttonCancel.FillColor = Color.FromArgb(255, 128, 128);
            buttonCancel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            buttonCancel.ForeColor = Color.White;
            buttonCancel.Location = new Point(609, 3);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.ShadowDecoration.CustomizableEdges = customizableEdges12;
            buttonCancel.Size = new Size(94, 34);
            buttonCancel.TabIndex = 2;
            buttonCancel.Text = "Отменить";
            // 
            // FiltersForm
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(706, 554);
            Controls.Add(tabControl);
            Controls.Add(tableLayoutPanel1);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FiltersForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Фильтры";
            tabControl.ResumeLayout(false);
            tabCategories.ResumeLayout(false);
            panelSelectAllCategories.ResumeLayout(false);
            panelSelectAllCategories.PerformLayout();
            tabLocations.ResumeLayout(false);
            panelSelectAllLocations.ResumeLayout(false);
            panelSelectAllLocations.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2TabControl tabControl;
        private TabPage tabCategories;
        private FlowLayoutPanel flowMainCategories;
        private FlowLayoutPanel flowSubCategories;
        private TabPage tabLocations;
        private FlowLayoutPanel flowLocations;
        private TableLayoutPanel tableLayoutPanel1;
        private Guna.UI2.WinForms.Guna2Button buttonApply;
        private Guna.UI2.WinForms.Guna2Button buttonCancel;
        private Guna.UI2.WinForms.Guna2Panel panelSelectAllCategories;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblSelectedCategoriesCount;
        private Guna.UI2.WinForms.Guna2CustomCheckBox checkBoxSelectAllCategories;
        private Guna.UI2.WinForms.Guna2Panel panelSelectAllLocations;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblSelectedLocationsCount;
        private Guna.UI2.WinForms.Guna2CustomCheckBox checkBoxSelectAllLocations;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel2;
    }
}