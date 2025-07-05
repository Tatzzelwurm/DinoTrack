using Guna.UI2.WinForms;

namespace DinoTrack.Controls
{
    partial class InventoryControlPanel
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InventoryControlPanel));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            tableLayoutPanel1 = new TableLayoutPanel();
            Search_InventoryBox = new Guna2TextBox();
            FilterButton = new Guna2Button();
            SortingButton = new Guna2Button();
            ExportButton = new Guna2Button();
            NewInventoryButton = new Guna2Button();
            contextMenuStrip1 = new ContextMenuStrip(components);
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.BackColor = Color.Teal;
            tableLayoutPanel1.ColumnCount = 7;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 24.78792F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 44.6402855F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30.5717888F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140F));
            tableLayoutPanel1.Controls.Add(Search_InventoryBox, 1, 0);
            tableLayoutPanel1.Controls.Add(FilterButton, 2, 0);
            tableLayoutPanel1.Controls.Add(SortingButton, 3, 0);
            tableLayoutPanel1.Controls.Add(ExportButton, 6, 0);
            tableLayoutPanel1.Controls.Add(NewInventoryButton, 5, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Margin = new Padding(3, 0, 3, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(1000, 50);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // Search_InventoryBox
            // 
            Search_InventoryBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            Search_InventoryBox.Animated = true;
            Search_InventoryBox.BorderColor = Color.FromArgb(64, 64, 64);
            Search_InventoryBox.BorderRadius = 10;
            Search_InventoryBox.CustomizableEdges = customizableEdges1;
            Search_InventoryBox.DefaultText = "";
            Search_InventoryBox.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            Search_InventoryBox.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            Search_InventoryBox.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            Search_InventoryBox.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            Search_InventoryBox.FocusedState.BorderColor = Color.FromArgb(64, 64, 64);
            Search_InventoryBox.Font = new Font("Segoe UI", 9F);
            Search_InventoryBox.ForeColor = Color.Black;
            Search_InventoryBox.HoverState.BorderColor = Color.Cyan;
            Search_InventoryBox.IconLeftCursor = Cursors.Hand;
            Search_InventoryBox.IconLeftSize = new Size(30, 30);
            Search_InventoryBox.IconRight = Properties.Resources.Search_Icon;
            Search_InventoryBox.IconRightSize = new Size(30, 30);
            Search_InventoryBox.Location = new Point(112, 5);
            Search_InventoryBox.MaximumSize = new Size(700, 40);
            Search_InventoryBox.MaxLength = 50;
            Search_InventoryBox.MinimumSize = new Size(70, 40);
            Search_InventoryBox.Name = "Search_InventoryBox";
            Search_InventoryBox.PlaceholderForeColor = Color.DimGray;
            Search_InventoryBox.PlaceholderText = "Поиск по инв.номеру / наименованию";
            Search_InventoryBox.SelectedText = "";
            Search_InventoryBox.ShadowDecoration.CustomizableEdges = customizableEdges2;
            Search_InventoryBox.Size = new Size(190, 40);
            Search_InventoryBox.TabIndex = 5;
            Search_InventoryBox.TextChanged += Search_InventoryBox_TextChanged;
            Search_InventoryBox.Click += Search_InventoryBox_Click;
            Search_InventoryBox.MouseLeave += Search_InventoryBox_MouseLeave;
            // 
            // FilterButton
            // 
            FilterButton.Animated = true;
            FilterButton.CustomizableEdges = customizableEdges3;
            FilterButton.DisabledState.BorderColor = Color.DarkGray;
            FilterButton.DisabledState.CustomBorderColor = Color.DarkGray;
            FilterButton.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            FilterButton.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            FilterButton.Dock = DockStyle.Fill;
            FilterButton.FillColor = Color.Teal;
            FilterButton.FocusedColor = Color.Transparent;
            FilterButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            FilterButton.ForeColor = Color.White;
            FilterButton.HoverState.ForeColor = Color.GreenYellow;
            FilterButton.Image = Properties.Resources.free_icon_filter_2852196;
            FilterButton.ImageSize = new Size(25, 25);
            FilterButton.Location = new Point(308, 3);
            FilterButton.Name = "FilterButton";
            FilterButton.PressedColor = Color.FromArgb(0, 192, 192);
            FilterButton.ShadowDecoration.CustomizableEdges = customizableEdges4;
            FilterButton.Size = new Size(134, 44);
            FilterButton.TabIndex = 6;
            FilterButton.Text = "Фильтры";
            FilterButton.Click += FiltersButton_Click;
            // 
            // SortingButton
            // 
            SortingButton.Animated = true;
            SortingButton.CustomizableEdges = customizableEdges5;
            SortingButton.DisabledState.BorderColor = Color.DarkGray;
            SortingButton.DisabledState.CustomBorderColor = Color.DarkGray;
            SortingButton.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            SortingButton.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            SortingButton.Dock = DockStyle.Fill;
            SortingButton.FillColor = Color.Teal;
            SortingButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            SortingButton.ForeColor = Color.White;
            SortingButton.HoverState.ForeColor = Color.GreenYellow;
            SortingButton.Image = Properties.Resources.free_icon_sort_1251670;
            SortingButton.ImageSize = new Size(25, 25);
            SortingButton.Location = new Point(448, 3);
            SortingButton.Name = "SortingButton";
            SortingButton.PressedColor = Color.FromArgb(0, 192, 192);
            SortingButton.ShadowDecoration.CustomizableEdges = customizableEdges6;
            SortingButton.Size = new Size(134, 44);
            SortingButton.TabIndex = 7;
            SortingButton.Text = "Сортировка";
            SortingButton.Click += SortingButton_Click;
            // 
            // ExportButton
            // 
            ExportButton.Animated = true;
            ExportButton.CustomizableEdges = customizableEdges7;
            ExportButton.DisabledState.BorderColor = Color.DarkGray;
            ExportButton.DisabledState.CustomBorderColor = Color.DarkGray;
            ExportButton.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            ExportButton.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            ExportButton.Dock = DockStyle.Fill;
            ExportButton.FillColor = Color.Teal;
            ExportButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            ExportButton.ForeColor = Color.White;
            ExportButton.HoverState.ForeColor = Color.GreenYellow;
            ExportButton.Image = (Image)resources.GetObject("ExportButton.Image");
            ExportButton.ImageAlign = HorizontalAlignment.Left;
            ExportButton.ImageSize = new Size(25, 25);
            ExportButton.Location = new Point(862, 3);
            ExportButton.Name = "ExportButton";
            ExportButton.PressedColor = Color.FromArgb(0, 192, 192);
            ExportButton.ShadowDecoration.CustomizableEdges = customizableEdges8;
            ExportButton.Size = new Size(135, 44);
            ExportButton.TabIndex = 9;
            ExportButton.Text = "Экспорт";
            ExportButton.TextAlign = HorizontalAlignment.Left;
            ExportButton.Click += ExportButton_Click;
            // 
            // NewInventoryButton
            // 
            NewInventoryButton.Animated = true;
            NewInventoryButton.CustomizableEdges = customizableEdges9;
            NewInventoryButton.DisabledState.BorderColor = Color.DarkGray;
            NewInventoryButton.DisabledState.CustomBorderColor = Color.DarkGray;
            NewInventoryButton.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            NewInventoryButton.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            NewInventoryButton.Dock = DockStyle.Fill;
            NewInventoryButton.FillColor = Color.Teal;
            NewInventoryButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            NewInventoryButton.ForeColor = Color.White;
            NewInventoryButton.HoverState.ForeColor = Color.GreenYellow;
            NewInventoryButton.Image = Properties.Resources.free_icon_add_6998878;
            NewInventoryButton.ImageAlign = HorizontalAlignment.Left;
            NewInventoryButton.ImageSize = new Size(30, 30);
            NewInventoryButton.Location = new Point(722, 3);
            NewInventoryButton.Name = "NewInventoryButton";
            NewInventoryButton.PressedColor = Color.FromArgb(0, 192, 192);
            NewInventoryButton.ShadowDecoration.CustomizableEdges = customizableEdges10;
            NewInventoryButton.Size = new Size(134, 44);
            NewInventoryButton.TabIndex = 8;
            NewInventoryButton.Text = "Новый инвентарь";
            NewInventoryButton.TextAlign = HorizontalAlignment.Left;
            NewInventoryButton.Click += NewInventoryButton_Click;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(61, 4);
            // 
            // InventoryControlPanel
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Name = "InventoryControlPanel";
            Size = new Size(1000, 50);
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Guna.UI2.WinForms.Guna2TextBox Search_InventoryBox;
        private Guna.UI2.WinForms.Guna2Button FilterButton;
        private Guna.UI2.WinForms.Guna2Button SortingButton;
        private Guna.UI2.WinForms.Guna2Button NewInventoryButton;
        private Guna.UI2.WinForms.Guna2Button ExportButton;
        private ContextMenuStrip contextMenuStrip1;
    }
}
