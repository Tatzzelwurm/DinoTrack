using DinoTrack.Controls;
using DinoTrack.Data.SchoolInventoryApp.Data;
using DinoTrack.Forms;
using DinoTrack.MenuApp;
using DinoTrack.Models;
using Guna.UI2.WinForms;

namespace DinoTrack
{
    /// <summary>
    /// Главная форма приложения - центральный интерфейс системы учёта школьного инвентаря.
    /// </summary>
    public partial class MainForm : Form
    {
        private ConnectionDatabase DB = new ConnectionDatabase();
        private SuggestionsPopupControl _suggestionsPopup;
        private List<Inventory> _allInventory = new List<Inventory>();
        private List<Inventory> _filteredInventory = new List<Inventory>();
        private List<Inventory> _lastFilteredItems;
        public Guna2ContextMenuStrip AppMenu => Menu;

        public MainForm() // Конструктор главной формы, инициализирует компоненты и базовые настройки
        {
            InitializeComponent();
            InitializeDataGridView();
            InitializeSuggestionsPopup();
            InitializeEventHandlers();
            LoadInventoryFromDB();
            LoadInventoryFromDB();

            // Инициализация системы резервного копирования
            DatabaseBackupAndRestore.Initialize(DB.connectionString);

            // Загрузка пользовательских настроек из конфига
            AllowColumnResizing = Properties.Settings.Default.AllowColumnResizing;
            AllowRowResizing = Properties.Settings.Default.AllowRowResizing;
            HighlightSelectedRow = Properties.Settings.Default.HighlightSelectedRow;

            // Подписка на событие загрузки формы для настройки размеров
            this.Load += MainForm_Load;
        }
        private void MainForm_Load(object sender, EventArgs e) // Настраивает полноэкранный режим приложения
        {
            this.Bounds = Screen.PrimaryScreen.WorkingArea;

        }

        private void InitializeDataGridView() // Инициализирует DataGridView (Таблицу) с настройками виртуального режима и столбцов
        {
            InventoryTable.VirtualMode = true;
            InventoryTable.AllowUserToAddRows = false;
            InventoryTable.AllowUserToDeleteRows = false;
            InventoryTable.RowHeadersVisible = false;
            InventoryTable.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            InventoryTable.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            InventoryTable.BackgroundColor = Color.White;
            InventoryTable.BorderStyle = BorderStyle.None;
            InventoryTable.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            InventoryTable.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            // Очистка существующих колонок
            InventoryTable.Columns.Clear();

            // Добавление колонок
            InventoryTable.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "RowNumber",
                HeaderText = "№",
                FillWeight = 50,
                MinimumWidth = 20,

                SortMode = DataGridViewColumnSortMode.NotSortable
            });

            InventoryTable.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Name",
                HeaderText = "Наименование",
                FillWeight = 250,
                MinimumWidth = 200,
                SortMode = DataGridViewColumnSortMode.NotSortable
            });

            InventoryTable.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "InventoryNumber",
                HeaderText = "Инвентарный номер",
                FillWeight = 100,
                MinimumWidth = 50,
                SortMode = DataGridViewColumnSortMode.NotSortable
            });

            InventoryTable.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "StartDate",
                HeaderText = "Дата ввода",
                FillWeight = 100,
                MinimumWidth = 50,
                SortMode = DataGridViewColumnSortMode.NotSortable
            });

            InventoryTable.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Price",
                HeaderText = "Стоимость",
                FillWeight = 100,
                MinimumWidth = 50,
                SortMode = DataGridViewColumnSortMode.NotSortable,
                DefaultCellStyle = { Format = "N2" }
            });

            InventoryTable.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Location",
                HeaderText = "Местоположение",
                FillWeight = 100,
                MinimumWidth = 50,
                SortMode = DataGridViewColumnSortMode.NotSortable
            });

            // Подписка на события
            InventoryTable.CellValueNeeded += DataGridView1_CellValueNeeded;
            InventoryTable.CellDoubleClick += DataGridView1_CellDoubleClick;
            InventoryTable.RowPrePaint += DataGridView1_RowPrePaint;
        }

        private void DataGridView1_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e) // Заполняет DataGridView данными в виртуальном режиме
        {
            if (e.RowIndex >= 0 && e.RowIndex < _filteredInventory.Count)
            {
                var item = _filteredInventory[e.RowIndex];
                var startDate = DB.GetInventoryStartDate(item.Id);

                switch (e.ColumnIndex)
                {
                    case 0: // № Порядковый номер
                        e.Value = e.RowIndex + 1;
                        break;
                    case 1: // Наименование
                        e.Value = item.Name;
                        break;
                    case 2: // Инв. №
                        e.Value = item.InventoryNumber ?? "-";
                        break;
                    case 3: // Дата ввода
                        e.Value = startDate?.ToString("dd.MM.yyyy") ?? "-";
                        break;
                    case 4: // Стоимость
                        e.Value = item.Price == 0 ? "-" : item.Price.ToString("N2");
                        break;
                    case 5: // Местоположение
                        e.Value = item.LocationId.HasValue
                            ? DB.GetLocationNameById(item.LocationId.Value) ?? "-"
                            : "-";
                        break;
                }
            }
        }


        private void DataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e) // Реализует "зебру" - чередование цветов строк таблицы
        {
            // Подсветка строк
            if (e.RowIndex % 2 == 0)
            {
                InventoryTable.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            }
            else
            {
                InventoryTable.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
            }

        }

        private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e) // Обрабатывает двойной клик по строке - открывает карточку инвентаря
        {
            if (e.RowIndex >= 0 && e.RowIndex < _filteredInventory.Count)
            {
                OpenInventoryDetails(_filteredInventory[e.RowIndex].Id);
            }
        }



        private void InitializeSuggestionsPopup() // Инициализация всплывающего окна подсказок для поиска
        {
            _suggestionsPopup = new SuggestionsPopupControl();
            this.Controls.Add(_suggestionsPopup);
            _suggestionsPopup.BringToFront();
            _suggestionsPopup.Visible = false;
            _suggestionsPopup.SuggestionSelected += OnSuggestionSelected;
        }


        private void InitializeEventHandlers() // Подписывается на события элементов управления
        {
            infoPanel1.FilterRequested += OnFilterRequested;
            inventoryControlPanel1.SearchRequested += OnSearchRequested;
            inventoryControlPanel1.FiltersButtonClick += ShowFiltersForm;
            infoPanel1.ResetFiltersRequested += ResetAllFilters;
            inventoryControlPanel1.SearchTextChanged += OnSearchTextChanged;
            inventoryControlPanel1.ExportButtonClick += ShowExportForm;
            infoPanel1.ExportFilteredRequested += ShowExportFormWithFiltered;
            inventoryControlPanel1.OnSortingOptionSelected += OnSortingOptionSelected;
        }

        private void OnSortingOptionSelected(object sender, string sortOption) // Обрабатывает выбор опции сортировки
        {
            switch (sortOption)
            {
                case "default":
                    // Сброс сортировки
                    _filteredInventory = new List<Inventory>(_lastFilteredItems ?? _allInventory);
                    break;

                case "price_desc":
                    // Сортировка по убыванию цены
                    _filteredInventory = _filteredInventory
                        .OrderByDescending(x => x.Price)
                        .ToList();
                    break;

                case "price_asc":
                    // Сортировка по убыванию цены
                    _filteredInventory = _filteredInventory
                        .OrderBy(x => x.Price)
                        .ToList();
                    break;

                case "StartDate_asc":
                    // Сортировка по дате ввода
                    _filteredInventory = _filteredInventory
                        .OrderBy(x => DB.GetInventoryStartDate(x.Id) ?? DateTime.MinValue)
                        .ToList();
                    break;

                case "StartDate_desc":
                    // Сортировка по дате ввода
                    _filteredInventory = _filteredInventory
                        .OrderByDescending(x => DB.GetInventoryStartDate(x.Id) ?? DateTime.MinValue)
                        .ToList();
                    break;
            }

            InventoryTable.Invalidate();
        }

        private void ShowExportFormWithFiltered(object sender, EventArgs e) // Показывает форму экспорта с предустановленными фильтрованными данными
        {
            ShowExportForm(sender, e);
        }

        private void ShowExportForm(object sender, EventArgs e) // Показывает форму "Мастер экспорта"
        {
            try
            {
                bool hasActiveFilters = _lastFilteredItems != null;
                var itemsToExport = hasActiveFilters ? _lastFilteredItems : _allInventory;

                using (var exportForm = new Export(hasActiveFilters ? itemsToExport : null))
                {
                    if (sender is InfoPanel || hasActiveFilters)
                    {
                        exportForm.ActivateFilteredTab();
                    }
                    else
                    {
                        exportForm.SetActiveButton(exportForm.btnExportAll);
                        exportForm.LoadAllInventory();
                    }
                    exportForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при подготовке данных для экспорта:\n{ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnSearchTextChanged(object sender, string searchText) // Обрабатывает изменение текста в поле поиска (показывает подсказки)
        {
            if (string.IsNullOrEmpty(searchText))
            {
                _suggestionsPopup.Hide();
                return;
            }

            UpdateSuggestionsPosition();
            LoadSuggestions(searchText);
        }

        private void UpdateSuggestionsPosition() // Обновляет позицию всплывающей подсказки при поиске
        {
            Point screenLocation = inventoryControlPanel1.SearchBox.PointToScreen(Point.Empty);
            Point formLocation = this.PointToClient(screenLocation);

            _suggestionsPopup.Location = new Point(
                formLocation.X,
                formLocation.Y + inventoryControlPanel1.SearchBox.Height);

            _suggestionsPopup.Width = inventoryControlPanel1.SearchBox.Width;
        }

        private void LoadSuggestions(string searchText) // Загружает подсказки для поиска из базы данных
        {
            var suggestions = DB.GetAllInventory()
                .Where(item => item.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                              (item.InventoryNumber != null &&
                               item.InventoryNumber.Contains(searchText, StringComparison.OrdinalIgnoreCase)))
                .Take(10)
                .Select(item => item.InventoryNumber != null
                    ? $"{item.InventoryNumber} - {item.Name}"
                    : item.Name) // Просто название, если нет номера
                .ToList();

            _suggestionsPopup.SetSuggestions(suggestions);
            _suggestionsPopup.Visible = suggestions.Any();
        }

        private void OnSuggestionSelected(object sender, string selectedText) // Обрабатывает выбор подсказки в поиске
        {
            try
            {
                List<Inventory> filteredItems;
                Inventory firstItem = null;

                if (selectedText.Contains(" - "))
                {
                    var parts = selectedText.Split(new[] { " - " }, StringSplitOptions.RemoveEmptyEntries);
                    string inventoryNumber = parts[0].Trim();
                    string itemName = parts[1].Trim();

                    filteredItems = DB.GetAllInventory()
                        .Where(item => (item.InventoryNumber != null &&
                                      item.InventoryNumber.Equals(inventoryNumber, StringComparison.OrdinalIgnoreCase)) ||
                                      item.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase))
                        .ToList();
                }
                else
                {
                    filteredItems = DB.GetAllInventory()
                        .Where(item => item.Name.Equals(selectedText, StringComparison.OrdinalIgnoreCase))
                        .ToList();
                }

                if (filteredItems.Any())
                {
                    firstItem = filteredItems.First();


                    if (firstItem.StatusId != 1) // Если статус не "активный"
                    {
                        infoPanel1.SetActiveButton(infoPanel1.btnInactiveInventory);
                    }
                    else if (firstItem.IsMain) // Если это основное средство
                    {
                        infoPanel1.SetActiveButton(infoPanel1.btnMainMeans);
                    }
                    else // Если неосновное средство
                    {
                        infoPanel1.SetActiveButton(infoPanel1.btnNotMainMeans);
                    }
                }


                inventoryControlPanel1.ClearSearchBox();

                DisplayFilteredItems(filteredItems);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обработке выбора: {ex.Message}",
                              "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _suggestionsPopup.Hide();
            }
        }

        protected override void OnMouseDown(MouseEventArgs e) // Скрывает подсказки при клике вне поля поиска
        {
            base.OnMouseDown(e);

            if (_suggestionsPopup.Visible &&
                !inventoryControlPanel1.SearchBox.Bounds.Contains(e.Location) &&
                !_suggestionsPopup.Bounds.Contains(e.Location))
            {
                _suggestionsPopup.Hide();
            }

        }

        private void OnSearchRequested(object sender, string searchText) // Выполняет поиск при нажатии Enter в поле поиска
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                LoadInventoryFromDB();
                return;
            }

            var filteredItems = _allInventory
                .Where(item => item.InventoryNumber.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                              item.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                              item.SerialNumber?.Contains(searchText, StringComparison.OrdinalIgnoreCase) == true)
                .ToList();

            DisplayFilteredItems(filteredItems, false);
        }



        private void OnFilterRequested(object sender, string filterType) // Обрабатывает запрос фильтрации от InfoPanel
        {
            List<Inventory> items = filterType switch
            {
                "main" => DB.GetMainInventory(),
                "non_main" => DB.GetNonMainInventory(),
                "written_off" => DB.GetWrittenOffInventory(),
                _ => DB.GetAllInventory().Where(i => i.StatusId == 1).ToList()
            };

            int totalCount = filterType switch
            {
                "main" => DB.GetMainInventory().Count,
                "non_main" => DB.GetNonMainInventory().Count,
                "written_off" => DB.GetWrittenOffInventory().Count,
                _ => DB.GetAllInventory().Count(i => i.StatusId == 1)
            };

            DisplayFilteredItems(items, false);
            infoPanel1.UpdateCounts(totalCount);
        }

        private void ResetAllFilters(object sender, EventArgs e) // Сбрасывает все фильтры и показывает полный список
        {
            _lastFilteredItems = null;
            LoadInventoryFromDB();
        }

        private void LoadInventoryFromDB() // Загружает данные инвентаря из базы данных
        {
            _allInventory = DB.GetAllInventory().Where(i => i.StatusId == 1).ToList();
            _filteredInventory = new List<Inventory>(_allInventory);

            // Обновляем DataGridView
            InventoryTable.RowCount = _filteredInventory.Count;
            infoPanel1.UpdateCounts(_allInventory.Count);
            InventoryTable.Invalidate();
        }

        private void OpenInventoryDetails(int inventoryId) // Открывает "Инвентарную карточку" выбранного инвентаря
        {
            var inventoryItem = DB.GetInventoryById(inventoryId);
            if (inventoryItem == null) return;

            using (var detailsForm = new InventoryCard(inventoryItem))
            {
                detailsForm.DataUpdated += () =>
                {
                    LoadInventoryFromDB();
                    ResetToDefaultView();
                };

                if (detailsForm.ShowDialog() == DialogResult.OK)
                {
                    LoadInventoryFromDB();
                    ResetToDefaultView();
                }
            }
        }


        private void ResetToDefaultView() // Сбрасывает вид к состоянию "Весь инвентарь" 
        {
            infoPanel1.SetActiveButton(infoPanel1.btnAcitveInventory);
            infoPanel1.totalLabel.Visible = true;
            infoPanel1.UpdateCounts(_allInventory.Count);
        }

        private void ShowFiltersForm(object sender, EventArgs e) // Показывает форму Фильтры
        {
            using (var filtersForm = new FiltersForm())
            {
                if (filtersForm.ShowDialog() == DialogResult.OK)
                {
                    _lastFilteredItems = FilterInventory(
                        filtersForm.SelectedSubCategories,
                        filtersForm.SelectedLocations);

                    DisplayFilteredItems(_lastFilteredItems, true);

                    // Обновляем InfoPanel
                    infoPanel1.filteredLabel.Visible = true;
                    infoPanel1.btnResetFilters.Visible = true;
                    infoPanel1.btnExportFiltered.Visible = true;
                    infoPanel1.totalLabel.Visible = false; // Скрываем totalLabel, если нужно
                    infoPanel1.filteredLabel.Text = $"Отфильтрованный инвентарь: {_lastFilteredItems.Count}";

                    // Устанавливаем активную кнопку "Весь инвентарь"
                    infoPanel1.SetActiveButton(infoPanel1.btnAcitveInventory);
                }
            }
        }

        private List<Inventory> FilterInventory(List<int> subCategories, List<int> locations) // Фильтрует инвентарь по категориям и местоположениям
        {
            var allItems = DB.GetAllInventory()
                .Where(i => i.StatusId == 1)
                .ToList();

            if (subCategories.Count == 0 && locations.Count == 0)
                return allItems;

            return allItems.Where(item =>
                (subCategories.Count == 0 || subCategories.Contains(item.SubCategoryId)) &&
                (
                    locations.Count == 0 ||
                    item.LocationId.HasValue && locations.Contains(item.LocationId.Value)
                )
            ).ToList();
        }

        private void DisplayFilteredItems(List<Inventory> items, bool showFilterControls = false) // Отображает отфильтрованные элементы в таблице
        {
            _filteredInventory = new List<Inventory>(items);
            InventoryTable.RowCount = _filteredInventory.Count;

            if (showFilterControls)
            {
                infoPanel1.UpdateCounts(items.Count, items.Count);
            }

            InventoryTable.Invalidate();
        }

        private void toolStripAboutApp_Click(object sender, EventArgs e) // Показывает форму "О программе"
        {
            MessageBox.Show(
       "Информационная система учёта школьного инвентаря\nВерсия 2.0",
       "О приложении",
       MessageBoxButtons.OK,
       MessageBoxIcon.Information);
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            MenuApp.DatabaseBackupAndRestore.CreateBackup(); // Вызов функции "Резервное копирование"
        }

        private void toolStripMenuItem10_Click(object sender, EventArgs e) // Вызов функции "Восстановление базы данных"
        {
            MenuApp.DatabaseBackupAndRestore.RestoreBackup();
        }

        private void toolStripSettings_Click(object sender, EventArgs e) // Открывает форму "Настройки"
        {
            using (var settingsForm = new SettingsForm(this))
            {
                settingsForm.ShowDialog(this);
            }
        }
        /* Обработчики кликов подпунктов раздела "Справка" */
        private void toolStripMenuItem1_Click(object sender, EventArgs e) => MenuApp.Reference.ShowSearchHelp();
        private void toolStripMenuItem2_Click(object sender, EventArgs e) => MenuApp.Reference.ShowSortingHelp();
        private void toolStripMenuItem4_Click(object sender, EventArgs e) => MenuApp.Reference.ShowExportHelp();
        private void toolStripMenuItem3_Click(object sender, EventArgs e) => MenuApp.Reference.ShowFiltersHelp();
        private void toolStripMenuItem7_Click(object sender, EventArgs e) => MenuApp.Reference.ShowDatabaseHelp();
        private void toolStripMenuItem5_Click(object sender, EventArgs e) => MenuApp.Reference.ShowAddInventoryHelp();
        private void toolStripMenuItem6_Click(object sender, EventArgs e) => MenuApp.Reference.ShowEditInventoryHelp();
        private void toolStripMenuItem8_Click(object sender, EventArgs e) => MenuApp.Reference.ShowSystemMigrationHelp();
        private void toolStripMenuItem11_Click(object sender, EventArgs e) => MenuApp.Reference.ShowTechnicalInfo();


        /* Свойства Настроек приложения */
        public bool AllowColumnResizing // Свойство для управления возможностью изменения ширины столбцов
        {
            get => InventoryTable.AllowUserToResizeColumns;
            set => InventoryTable.AllowUserToResizeColumns = value;
        }

        public bool AllowRowResizing // Свойство для управления возможностью изменения высоты строк
        {
            get => InventoryTable.AllowUserToResizeRows;
            set => InventoryTable.AllowUserToResizeRows = value;
        }

        public bool HighlightSelectedRow // Свойство для управления подсветкой выбранной строки
        {
            get => InventoryTable.DefaultCellStyle.SelectionBackColor == Color.LightCyan;
            set
            {
                if (value)
                {
                    InventoryTable.DefaultCellStyle.SelectionBackColor = Color.LightCyan;
                    InventoryTable.DefaultCellStyle.SelectionForeColor = SystemColors.ControlText;
                }
                else
                {
                    InventoryTable.DefaultCellStyle.SelectionBackColor = Color.White;
                    InventoryTable.DefaultCellStyle.SelectionForeColor = SystemColors.ControlText;
                }
            }
        }
    }
    // :)
}




