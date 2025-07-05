using DinoTrack.Forms;
using Guna.UI2.WinForms;

namespace DinoTrack.Controls
{
    /// <summary>
    /// Панель управления инвентарем - содержит элементы для поиска, фильтрации, сортировки, экспорта и добавления нового инвентаря.
    /// </summary>
    public partial class InventoryControlPanel : UserControl
    {
        public event EventHandler FiltersButtonClick;
        public event EventHandler<string> SearchRequested;
        public event EventHandler<string> SearchTextChanged;
        public event EventHandler ExportButtonClick;

        public Guna2TextBox SearchBox => Search_InventoryBox;
        private ContextMenuStrip sortingContextMenu;
        public event EventHandler<string> OnSortingOptionSelected;

        public InventoryControlPanel() // Конструктор панели управления, инициализирует компоненты и контекстное меню сортировки
        {
            InitializeComponent();
            InitializeSortingContextMenu();
        }
        public void ClearSearchBox() // Очищает поле поиска и возвращает фокус на него
        {
            SearchBox.Text = string.Empty;
            SearchBox.Focus();
        }

        private void InitializeSortingContextMenu() // Инициализирует контекстное меню для сортировки данных
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
        private void FiltersButton_Click(object sender, EventArgs e) // Обрабатывает клик по кнопке фильтры
        {
            FiltersButtonClick?.Invoke(this, EventArgs.Empty);
        }

        private void Search_InventoryBox_Click(object sender, EventArgs e) // Изменяет цвет плейсхолдера при клике на поле поиска
        {
            Search_InventoryBox.PlaceholderForeColor = Color.SeaGreen;
        }

        private void Search_InventoryBox_MouseLeave(object sender, EventArgs e) // Восстанавливает цвет плейсхолдера при уходе мыши
        {
            Search_InventoryBox.PlaceholderForeColor = Color.Gray;
        }

        private void NewInventoryButton_Click(object sender, EventArgs e) // Обрабатывает клик по кнопке добавления нового инвентаря
        {
            var AddInventory = new Add_Inventory();
            AddInventory.ShowDialog();
        }


        private void Search_InventoryBox_TextChanged(object sender, EventArgs e) // Обрабатывает изменение текста в поле поиска
        {
            SearchTextChanged?.Invoke(this, Search_InventoryBox.Text);
        }

        protected override void OnMouseDown(MouseEventArgs e) // Обрабатывает клик мыши на панели
        {
            base.OnMouseDown(e);

        }

        protected virtual void OnSearchRequested(string searchText) // Вызывает событие запроса поиска
        {
            SearchRequested?.Invoke(this, searchText);
        }

        private void ExportButton_Click(object sender, EventArgs e) // Обрабатывает клик по кнопке экспорта
        {
            ExportButtonClick?.Invoke(this, EventArgs.Empty);
        }

        private void SortingButton_Click(object sender, EventArgs e) // Обрабатывает клик по кнопке сортировки
        {
            {
                sortingContextMenu.Show(SortingButton, new Point(0, SortingButton.Height));

            }
        }
    }
}