using ClosedXML.Excel;
using DinoTrack.Data.SchoolInventoryApp.Data;
using DinoTrack.Models;
using Guna.UI2.WinForms;
using System.Data;

namespace DinoTrack.Forms
{
    /// <summary>
    /// Форма "Мастер экспорта" - форма для экспорта данных об инвентаре в Excel
    /// </summary>
    public partial class Export : Form
    {
        private readonly ConnectionDatabase _db = new ConnectionDatabase();
        private List<Inventory> _currentInventory;
        private string _currentReportType = "Все данные";
        private List<Inventory> _filteredInventory;
        private Guna2GradientButton _activeButton;

        public Export(List<Inventory> filteredInventory = null) // Конструктор формы экспорта, принимает опционально отфильтрованные данные
        {
            InitializeComponent();

            _filteredInventory = filteredInventory;

            // Кнопка активна, если есть фильтрованные данные
            btnExportFiltered.Enabled = filteredInventory != null && filteredInventory.Count > 0;

            // Настройка DataGridView
            ConfigureDataGridView();

            // Загружает данные по умолчанию (весь инвентарь)
            LoadAllInventory();

            // Назначение обработчиков событий
            btnExportAll.Click += (s, e) => { SetActiveButton(btnExportAll); LoadAllInventory(); };
            btnExportFiltered.Click += (s, e) => { SetActiveButton(btnExportFiltered); LoadFilteredInventory(); };
            btnMainMeans.Click += (s, e) => { SetActiveButton(btnMainMeans); LoadMainInventory(); };
            btnNotMainMeans.Click += (s, e) => { SetActiveButton(btnNotMainMeans); LoadNonMainInventory(); };
            btnInactiveInventory.Click += (s, e) => { SetActiveButton(btnInactiveInventory); LoadWrittenOffInventory(); };

            buttonApply.Click += (s, e) => ExportCurrentData();
            buttonCancel.Click += (s, e) => Close();

            // Устанавливаем активную кнопку по умолчанию
            SetActiveButton(btnExportAll);
        }


        public void ActivateFilteredTab() // Активирует вкладку с фильтрованными данными при открытии
        {
            if (btnExportFiltered.Enabled)
            {
                SetActiveButton(btnExportFiltered);
                LoadFilteredInventory();
            }
        }

        public void SetActiveButton(Guna2GradientButton button) // Устанавливает визуально активную кнопку фильтра
        {
            // Сброс предыдущей активной кнопки
            if (_activeButton != null)
            {
                _activeButton.FillColor = Color.WhiteSmoke;
                _activeButton.FillColor2 = Color.WhiteSmoke;
                _activeButton.ForeColor = Color.Black;
            }

            // Установка новой активной кнопки
            button.FillColor = Color.LightCyan;
            button.FillColor2 = Color.LightCyan;
            button.ForeColor = Color.DarkSlateGray;

            _activeButton = button;
        }

        private void ConfigureDataGridView() // Настраивает внешний вид и колонки DataGridView
        {
            // Настройка внешнего вида DataGridView
            dataGridViewPreview.ThemeStyle.HeaderStyle.BackColor = Color.Teal;
            dataGridViewPreview.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dataGridViewPreview.ThemeStyle.HeaderStyle.ForeColor = Color.White;
            dataGridViewPreview.ThemeStyle.RowsStyle.SelectionBackColor = Color.LightSeaGreen;

            // Добавление колонок
            dataGridViewPreview.Columns.Clear();
            dataGridViewPreview.Columns.Add("ID", "№");
            dataGridViewPreview.Columns.Add("Name", "Наименование");
            dataGridViewPreview.Columns.Add("InventoryNumber", "Инвентарный номер");
            dataGridViewPreview.Columns.Add("StartDate", "Дата ввода");
            dataGridViewPreview.Columns.Add("Location", "Местоположение");
            dataGridViewPreview.Columns.Add("Price", "Цена");
            dataGridViewPreview.Columns.Add("Count", "Количество");

            // Настройка ширины колонок
            dataGridViewPreview.Columns["ID"].Width = 50;
            dataGridViewPreview.Columns["Name"].Width = 200;
            dataGridViewPreview.Columns["InventoryNumber"].Width = 100;
            dataGridViewPreview.Columns["StartDate"].Width = 100;
            dataGridViewPreview.Columns["Location"].Width = 150;
            dataGridViewPreview.Columns["Price"].Width = 100;
            dataGridViewPreview.Columns["Count"].Width = 100;

            // Форматирование цены
            dataGridViewPreview.Columns["Price"].DefaultCellStyle.Format = "N2";

            dataGridViewPreview.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridViewPreview.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridViewPreview.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridViewPreview.Columns[4].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridViewPreview.Columns[5].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridViewPreview.Columns[6].SortMode = DataGridViewColumnSortMode.NotSortable;


        }

        private void PopulateDataGridView() // Заполняет DataGridView текущими данными
        {
            dataGridViewPreview.Rows.Clear();

            int rowNumber = 1;
            foreach (var item in _currentInventory)
            {
                string locationName = item.LocationId.HasValue ? (_db.GetLocationNameById(item.LocationId.Value) ?? "Не указано") : "Не указано";
                DateTime? startDate = _db.GetInventoryStartDate(item.Id);

                dataGridViewPreview.Rows.Add(
                    rowNumber++,
                    item.Name,
                    item.InventoryNumber,
                    startDate?.ToString("dd.MM.yyyy") ?? "Не указана",
                    locationName,
                    item.Price,
                    item.Count
                );
            }

            lblPreviewStatus.Text = $"{_currentReportType}. Записей: {_currentInventory.Count}";
        }

        public void LoadAllInventory() // Загружает весь активный инвентарь (статус = 1)
        {
            _currentInventory = _db.GetAllInventory()
                                 .Where(item => item.StatusId == 1)
                                 .ToList();
            _currentReportType = "Весь инвентарь (в эксплуатации)";
            PopulateDataGridView();
        }

        private void LoadFilteredInventory() // Загружает предварительно отфильтрованные данные
        {
            if (_filteredInventory == null || _filteredInventory.Count == 0)
            {
                MessageBox.Show("Нет фильтрованных данных для отображения", "Информация",
                               MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _currentInventory = _filteredInventory;
            _currentReportType = "Отфильтрованный инвентарь";
            PopulateDataGridView();
        }

        private void LoadMainInventory() // Загружает только основные средства 
        {
            _currentInventory = _db.GetMainInventory();
            _currentReportType = "Основные средства";
            PopulateDataGridView();
        }

        private void LoadNonMainInventory() // Загружает неосновные средства 
        {
            _currentInventory = _db.GetNonMainInventory();
            _currentReportType = "Не основные средства";
            PopulateDataGridView();
        }

        private void LoadWrittenOffInventory() // Загружает списанный инвентарь (статус = 2)
        {
            _currentInventory = _db.GetWrittenOffInventory();
            _currentReportType = "Списанный инвентарь";
            PopulateDataGridView();
        }

        private void ExportCurrentData() // Выполняет экспорт текущих данных в Excel
        {
            if (_currentInventory == null || _currentInventory.Count == 0)
            {
                MessageBox.Show("Нет данных для экспорта", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string fileName = _currentReportType
                .Replace(" ", "_")
                .Replace(".", "") + $"_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";

            ExportToExcel(_currentInventory, fileName);
        }

        private void ExportToExcel(List<Inventory> inventory, string fileName) // Экспортирует переданные данные в Excel файл
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Excel Files|*.xlsx";
                saveFileDialog.Title = "Сохранить файл Excel";
                saveFileDialog.FileName = fileName;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (var workbook = new XLWorkbook())
                        {
                            var worksheet = workbook.Worksheets.Add("Инвентарь");

                            // Заголовки
                            worksheet.Cell(1, 1).Value = "№";
                            worksheet.Cell(1, 2).Value = "Наименование";
                            worksheet.Cell(1, 3).Value = "Инвентарный номер";
                            worksheet.Cell(1, 4).Value = "Дата ввода";
                            worksheet.Cell(1, 5).Value = "Местоположение";
                            worksheet.Cell(1, 6).Value = "Цена";
                            worksheet.Cell(1, 7).Value = "Количество";

                            // Стиль для заголовков
                            var headerRange = worksheet.Range(1, 1, 1, 7);
                            headerRange.Style.Font.Bold = true;
                            headerRange.Style.Fill.BackgroundColor = XLColor.Teal;
                            headerRange.Style.Font.FontColor = XLColor.White;
                            headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                            // Данные
                            int row = 2;
                            foreach (var item in inventory)
                            {
                                string locationName = item.LocationId.HasValue ? (_db.GetLocationNameById(item.LocationId.Value) ?? "Не указано") : "Не указано";
                                DateTime? startDate = _db.GetInventoryStartDate(item.Id);

                                worksheet.Cell(row, 1).Value = row - 1;
                                worksheet.Cell(row, 2).Value = item.Name;
                                worksheet.Cell(row, 3).Value = item.InventoryNumber;
                                worksheet.Cell(row, 4).Value = startDate?.ToString("dd.MM.yyyy") ?? "Не указана";
                                worksheet.Cell(row, 5).Value = locationName;
                                worksheet.Cell(row, 6).Value = item.Price;
                                worksheet.Cell(row, 7).Value = item.Count;

                                // Форматирование цены
                                worksheet.Cell(row, 6).Style.NumberFormat.Format = "0.00";

                                row++;
                            }

                            // Автоподбор ширины столбцов
                            worksheet.Columns().AdjustToContents();

                            // Сохраняем файл
                            workbook.SaveAs(saveFileDialog.FileName);

                            MessageBox.Show($"Данные успешно экспортированы в файл:\n{saveFileDialog.FileName}",
                                "Экспорт завершен", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при экспорте в Excel:\n{ex.Message}",
                            "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}