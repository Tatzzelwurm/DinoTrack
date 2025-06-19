using System;
using System.Linq;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using DinoTrack.Data.SchoolInventoryApp.Data;
using DinoTrack.Forms;

namespace DinoTrack.Controls
{
    public partial class InventoryControlPanel : UserControl
    {
        public event EventHandler FiltersButtonClick;
        public event EventHandler AddInventoryButtonClick;
        public event EventHandler<string> SearchRequested;
        public event EventHandler<string> SearchTextChanged;
        public event EventHandler ExportButtonClick;

        public Guna2TextBox SearchBox => Search_InventoryBox;
        private ContextMenuStrip sortingContextMenu;
        public event EventHandler<string> OnSortingOptionSelected;

        public InventoryControlPanel()
        {
            InitializeComponent();
            this.Layout += InventoryControlPanel_Layout;
            InitializeSortingContextMenu();
        }
        public void ClearSearchBox()
        {
            SearchBox.Text = string.Empty;
            SearchBox.Focus(); // Дополнительно можно вернуть фокус
        }

        private void InitializeSortingContextMenu()
        {
            sortingContextMenu = new ContextMenuStrip();

            // Пункт "По умолчанию" (сброс сортировки)
            var defaultSortItem = new ToolStripMenuItem("По умолчанию (сброс сортировки)");
            defaultSortItem.Click += (s, e) => OnSortingOptionSelected?.Invoke(this, "default");

            // Пункт "По стоимости (↑)"
            var priceAscItem = new ToolStripMenuItem("По стоимости (↑ по возрастанию)");
            priceAscItem.Click += (s, e) => OnSortingOptionSelected?.Invoke(this, "price_asc");

            // Пункт "По стоимости (↓)"
            var priceDescItem = new ToolStripMenuItem("По стоимости (↓ по убыванию)");
            priceDescItem.Click += (s, e) => OnSortingOptionSelected?.Invoke(this, "price_desc");

            // Пункт "По дате ввода (↓)"
            var StartDateDescItem = new ToolStripMenuItem("По дате ввода (↓ по убыванию)");
            StartDateDescItem.Click += (s, e) => OnSortingOptionSelected?.Invoke(this, "StartDate_desc");

            // Пункт "По дате ввода (↑)"
            var StartDateAscItem = new ToolStripMenuItem("По дате ввода (↑ по возрастанию)");
            StartDateAscItem.Click += (s, e) => OnSortingOptionSelected?.Invoke(this, "StartDate_asc");

            sortingContextMenu.Items.Add(defaultSortItem);
            sortingContextMenu.Items.Add(priceAscItem);
            sortingContextMenu.Items.Add(priceDescItem);
            sortingContextMenu.Items.Add(StartDateAscItem);
            sortingContextMenu.Items.Add(StartDateDescItem);
        }
        private void FiltersButton_Click(object sender, EventArgs e)
        {
            FiltersButtonClick?.Invoke(this, EventArgs.Empty);
        }

        private void Search_InventoryBox_Click(object sender, EventArgs e)
        {
            Search_InventoryBox.PlaceholderForeColor = Color.SeaGreen;
        }

        private void Search_InventoryBox_MouseLeave(object sender, EventArgs e)
        {
            Search_InventoryBox.PlaceholderForeColor = Color.Gray;
        }

        private void NewInventoryButton_Click(object sender, EventArgs e)
        {
            var AddInventory = new Add_Inventory();
            AddInventory.ShowDialog();
        }

        private void InventoryControlPanel_Layout(object sender, LayoutEventArgs e)
        {
            // Теперь позиционирование панели подсказок будет обрабатываться в главной форме
        }

        private void Search_InventoryBox_TextChanged(object sender, EventArgs e)
        {
            SearchTextChanged?.Invoke(this, Search_InventoryBox.Text);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            // Обработка клика вне поля поиска теперь в главной форме
        }

        public void HideSuggestions()
        {
            // Метод для скрытия подсказок (будет вызываться из главной формы)
        }

        protected virtual void OnSearchRequested(string searchText)
        {
            SearchRequested?.Invoke(this, searchText);
        }

        private void ExportButton_Click(object sender, EventArgs e)
        {
            ExportButtonClick?.Invoke(this, EventArgs.Empty);
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void SortingButton_Click(object sender, EventArgs e)
        {
            sortingContextMenu.Show(SortingButton, new Point(0, SortingButton.Height));

        }
    }
}