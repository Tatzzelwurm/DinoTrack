namespace DinoTrack
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        /// 
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            InventoryTable = new DataGridView();
            infoPanel1 = new DinoTrack.Controls.InfoPanel();
            inventoryControlPanel1 = new DinoTrack.Controls.InventoryControlPanel();
            headerControl1 = new DinoTrack.Controls.HeaderControl();
            Menu = new Guna.UI2.WinForms.Guna2ContextMenuStrip();
            toolStripReference = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripMenuItem();
            toolStripMenuItem2 = new ToolStripMenuItem();
            toolStripMenuItem3 = new ToolStripMenuItem();
            toolStripMenuItem4 = new ToolStripMenuItem();
            toolStripMenuItem5 = new ToolStripMenuItem();
            toolStripMenuItem6 = new ToolStripMenuItem();
            toolStripMenuItem7 = new ToolStripMenuItem();
            toolStripMenuItem8 = new ToolStripMenuItem();
            toolStripMenuItem11 = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            toolStripBackup = new ToolStripMenuItem();
            toolStripMenuItem9 = new ToolStripMenuItem();
            toolStripMenuItem10 = new ToolStripMenuItem();
            toolStripSettings = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            toolStripAboutApp = new ToolStripMenuItem();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)InventoryTable).BeginInit();
            Menu.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 3);
            tableLayoutPanel1.Controls.Add(infoPanel1, 0, 2);
            tableLayoutPanel1.Controls.Add(inventoryControlPanel1, 0, 1);
            tableLayoutPanel1.Controls.Add(headerControl1, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(1219, 644);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.BackColor = Color.Teal;
            tableLayoutPanel2.ColumnCount = 3;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.Controls.Add(InventoryTable, 1, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(0, 140);
            tableLayoutPanel2.Margin = new Padding(0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new Size(1219, 504);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // InventoryTable
            // 
            InventoryTable.AllowUserToAddRows = false;
            InventoryTable.AllowUserToDeleteRows = false;
            InventoryTable.AllowUserToResizeColumns = false;
            InventoryTable.AllowUserToResizeRows = false;
            InventoryTable.BackgroundColor = Color.WhiteSmoke;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle1.BackColor = Color.PaleTurquoise;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 10F);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = Color.PaleTurquoise;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            InventoryTable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            InventoryTable.ColumnHeadersHeight = 35;
            InventoryTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = Color.LightCyan;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            InventoryTable.DefaultCellStyle = dataGridViewCellStyle2;
            InventoryTable.Dock = DockStyle.Fill;
            InventoryTable.EditMode = DataGridViewEditMode.EditOnF2;
            InventoryTable.EnableHeadersVisualStyles = false;
            InventoryTable.GridColor = Color.Teal;
            InventoryTable.Location = new Point(25, 0);
            InventoryTable.Margin = new Padding(25, 0, 25, 5);
            InventoryTable.Name = "InventoryTable";
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Control;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Control;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            InventoryTable.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            InventoryTable.RowHeadersWidth = 25;
            InventoryTable.RowTemplate.Height = 40;
            InventoryTable.RowTemplate.ReadOnly = true;
            InventoryTable.ScrollBars = ScrollBars.Vertical;
            InventoryTable.Size = new Size(1169, 499);
            InventoryTable.TabIndex = 0;
            // 
            // infoPanel1
            // 
            infoPanel1.BackColor = Color.Teal;
            infoPanel1.Dock = DockStyle.Fill;
            infoPanel1.Location = new Point(0, 90);
            infoPanel1.Margin = new Padding(0);
            infoPanel1.Name = "infoPanel1";
            infoPanel1.Padding = new Padding(2);
            infoPanel1.Size = new Size(1219, 50);
            infoPanel1.TabIndex = 5;
            // 
            // inventoryControlPanel1
            // 
            inventoryControlPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            inventoryControlPanel1.Location = new Point(0, 40);
            inventoryControlPanel1.Margin = new Padding(0);
            inventoryControlPanel1.Name = "inventoryControlPanel1";
            inventoryControlPanel1.Size = new Size(1219, 50);
            inventoryControlPanel1.TabIndex = 2;
            // 
            // headerControl1
            // 
            headerControl1.BackColor = Color.WhiteSmoke;
            headerControl1.Dock = DockStyle.Fill;
            headerControl1.Location = new Point(0, 0);
            headerControl1.Margin = new Padding(0);
            headerControl1.Name = "headerControl1";
            headerControl1.Size = new Size(1219, 40);
            headerControl1.TabIndex = 4;
            // 
            // Menu
            // 
            Menu.BackColor = Color.WhiteSmoke;
            Menu.DropShadowEnabled = false;
            Menu.Items.AddRange(new ToolStripItem[] { toolStripReference, toolStripSeparator1, toolStripBackup, toolStripSettings, toolStripSeparator2, toolStripAboutApp });
            Menu.Margin = new Padding(100, 150, 0, 0);
            Menu.Name = "guna2ContextMenuStrip1";
            Menu.RenderStyle.ArrowColor = Color.FromArgb(151, 143, 255);
            Menu.RenderStyle.BorderColor = Color.Gainsboro;
            Menu.RenderStyle.ColorTable = null;
            Menu.RenderStyle.RoundedEdges = true;
            Menu.RenderStyle.SelectionArrowColor = Color.White;
            Menu.RenderStyle.SelectionBackColor = Color.LightSeaGreen;
            Menu.RenderStyle.SelectionForeColor = Color.White;
            Menu.RenderStyle.SeparatorColor = Color.Gainsboro;
            Menu.RenderStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            Menu.Size = new Size(201, 107);
            // 
            // toolStripReference
            // 
            toolStripReference.BackColor = Color.WhiteSmoke;
            toolStripReference.DropDownItems.AddRange(new ToolStripItem[] { toolStripMenuItem1, toolStripMenuItem2, toolStripMenuItem3, toolStripMenuItem4, toolStripMenuItem5, toolStripMenuItem6, toolStripMenuItem7, toolStripMenuItem8, toolStripMenuItem11 });
            toolStripReference.Image = (Image)resources.GetObject("toolStripReference.Image");
            toolStripReference.Name = "toolStripReference";
            toolStripReference.Size = new Size(200, 22);
            toolStripReference.Text = "Справка";
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(259, 22);
            toolStripMenuItem1.Text = "Поиск инвентаря";
            toolStripMenuItem1.Click += toolStripMenuItem1_Click;
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new Size(259, 22);
            toolStripMenuItem2.Text = "Сортировка";
            toolStripMenuItem2.Click += toolStripMenuItem2_Click;
            // 
            // toolStripMenuItem3
            // 
            toolStripMenuItem3.Name = "toolStripMenuItem3";
            toolStripMenuItem3.Size = new Size(259, 22);
            toolStripMenuItem3.Text = "Фильтры";
            toolStripMenuItem3.Click += toolStripMenuItem3_Click;
            // 
            // toolStripMenuItem4
            // 
            toolStripMenuItem4.Name = "toolStripMenuItem4";
            toolStripMenuItem4.Size = new Size(259, 22);
            toolStripMenuItem4.Text = "Экспорт";
            toolStripMenuItem4.Click += toolStripMenuItem4_Click;
            // 
            // toolStripMenuItem5
            // 
            toolStripMenuItem5.Name = "toolStripMenuItem5";
            toolStripMenuItem5.Size = new Size(259, 22);
            toolStripMenuItem5.Text = "Добавление инвентаря";
            toolStripMenuItem5.Click += toolStripMenuItem5_Click;
            // 
            // toolStripMenuItem6
            // 
            toolStripMenuItem6.Name = "toolStripMenuItem6";
            toolStripMenuItem6.Size = new Size(259, 22);
            toolStripMenuItem6.Text = "Редактирование инвентаря";
            toolStripMenuItem6.Click += toolStripMenuItem6_Click;
            // 
            // toolStripMenuItem7
            // 
            toolStripMenuItem7.Name = "toolStripMenuItem7";
            toolStripMenuItem7.Size = new Size(259, 22);
            toolStripMenuItem7.Text = "Работа с базой данных";
            toolStripMenuItem7.Click += toolStripMenuItem7_Click;
            // 
            // toolStripMenuItem8
            // 
            toolStripMenuItem8.Name = "toolStripMenuItem8";
            toolStripMenuItem8.Size = new Size(259, 22);
            toolStripMenuItem8.Text = "Перенос системы";
            toolStripMenuItem8.Click += toolStripMenuItem8_Click;
            // 
            // toolStripMenuItem11
            // 
            toolStripMenuItem11.Name = "toolStripMenuItem11";
            toolStripMenuItem11.Size = new Size(259, 22);
            toolStripMenuItem11.Text = "Системные требования (ВАЖНО)";
            toolStripMenuItem11.Click += toolStripMenuItem11_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.BackColor = Color.Black;
            toolStripSeparator1.ForeColor = SystemColors.ActiveCaptionText;
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(197, 6);
            // 
            // toolStripBackup
            // 
            toolStripBackup.AutoSize = false;
            toolStripBackup.DropDownItems.AddRange(new ToolStripItem[] { toolStripMenuItem9, toolStripMenuItem10 });
            toolStripBackup.Image = (Image)resources.GetObject("toolStripBackup.Image");
            toolStripBackup.ImageAlign = ContentAlignment.MiddleRight;
            toolStripBackup.Name = "toolStripBackup";
            toolStripBackup.Size = new Size(201, 22);
            toolStripBackup.Text = "Работа с базой данных";
            toolStripBackup.TextAlign = ContentAlignment.MiddleRight;
            toolStripBackup.TextDirection = ToolStripTextDirection.Horizontal;
            // 
            // toolStripMenuItem9
            // 
            toolStripMenuItem9.Name = "toolStripMenuItem9";
            toolStripMenuItem9.Size = new Size(207, 22);
            toolStripMenuItem9.Text = "Резервное копирование";
            toolStripMenuItem9.Click += toolStripMenuItem9_Click;
            // 
            // toolStripMenuItem10
            // 
            toolStripMenuItem10.Name = "toolStripMenuItem10";
            toolStripMenuItem10.Size = new Size(207, 22);
            toolStripMenuItem10.Text = "Восстановление";
            toolStripMenuItem10.Click += toolStripMenuItem10_Click;
            // 
            // toolStripSettings
            // 
            toolStripSettings.AutoSize = false;
            toolStripSettings.Image = (Image)resources.GetObject("toolStripSettings.Image");
            toolStripSettings.Name = "toolStripSettings";
            toolStripSettings.Size = new Size(229, 25);
            toolStripSettings.Text = "Настройки";
            toolStripSettings.Click += toolStripSettings_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(197, 6);
            // 
            // toolStripAboutApp
            // 
            toolStripAboutApp.Image = (Image)resources.GetObject("toolStripAboutApp.Image");
            toolStripAboutApp.Name = "toolStripAboutApp";
            toolStripAboutApp.Size = new Size(200, 22);
            toolStripAboutApp.Text = "О приложении";
            toolStripAboutApp.Click += toolStripAboutApp_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(1219, 644);
            Controls.Add(tableLayoutPanel1);
            FormBorderStyle = FormBorderStyle.None;
            HelpButton = true;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "DinoTrack";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)InventoryTable).EndInit();
            Menu.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private Controls.InventoryControlPanel inventoryControlPanel1;
        private Controls.HeaderControl headerControl1;
        private Controls.InfoPanel infoPanel1;
        private Guna.UI2.WinForms.Guna2ContextMenuStrip Menu;
        private ToolStripMenuItem toolStripBackup;
        private ToolStripMenuItem toolStripReference;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem toolStripSettings;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem toolStripAboutApp;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem toolStripMenuItem2;
        private ToolStripMenuItem toolStripMenuItem3;
        private ToolStripMenuItem toolStripMenuItem4;
        private ToolStripMenuItem toolStripMenuItem5;
        private ToolStripMenuItem toolStripMenuItem6;
        private ToolStripMenuItem toolStripMenuItem7;
        private ToolStripMenuItem toolStripMenuItem8;
        private ToolStripMenuItem toolStripMenuItem9;
        private ToolStripMenuItem toolStripMenuItem10;
        public DataGridView InventoryTable;
        private ToolStripMenuItem toolStripMenuItem11;
    }
}
