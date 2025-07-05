using DinoTrack.Data.SchoolInventoryApp.Data;
using Guna.UI2.WinForms;
using Npgsql;

namespace DinoTrack.Forms
{
    /// <summary>
    /// Форма для фильтрации инвентаря по категориям и местоположениям с подсчетом количества предметов
    /// </summary>
    public partial class FiltersForm : Form
    {
        private readonly ConnectionDatabase _db = new ConnectionDatabase();
        private int selectedSubCategoriesCount = 0;
        private int selectedLocationsCount = 0;
        private bool _isDataLoaded = false;

        public List<int> SelectedSubCategories { get; private set; } = new List<int>();
        public List<int> SelectedLocations { get; private set; } = new List<int>();

        private class SubCategoryInfo
        {
            public int Id { get; set; }
            public int MainCategoryId { get; set; }
            public int ItemCount { get; set; }
        }

        private class CategoryData
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        private class LocationData
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int ItemCount { get; set; }
        }

        public FiltersForm() // Конструктор - инициализирует компоненты и запускает асинхронную загрузку данных
        {
            InitializeComponent();

            tabControl.SelectedIndexChanged += (s, e) => UpdateButtonsState();
            buttonApply.Click += ApplyFilters;
            buttonCancel.Click += ResetAllFilters;

            checkBoxSelectAllCategories.CheckedChanged += SelectAllSubCategories_CheckedChanged;
            checkBoxSelectAllLocations.CheckedChanged += SelectAllLocations_CheckedChanged;

            // Запуск асинхронной загрузки данных
            this.Shown += async (s, e) => await LoadDataAsync();
        }

        private async Task LoadDataAsync() // Асинхронно загружает данные категорий и местоположений без блокировки UI
        {
            if (_isDataLoaded) return;

            try
            {
                // Показывает индикатор загрузки
                Cursor = Cursors.WaitCursor;
                buttonApply.Enabled = false;
                buttonCancel.Enabled = false;

                // Загружает данные в фоновом потоке
                var categoriesTask = Task.Run(() => LoadCategoriesData());
                var locationsTask = Task.Run(() => LoadLocationsData());

                await Task.WhenAll(categoriesTask, locationsTask);

                // Обновляет UI в основном потоке
                this.Invoke((MethodInvoker)delegate
                {
                    PopulateCategories(categoriesTask.Result);
                    PopulateLocations(locationsTask.Result);
                    UpdateButtonsState();
                    UpdateCounters();
                    _isDataLoaded = true;
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private List<CategoryData> LoadCategoriesData() // Загружает данные категорий из БД (основные категории)
        {
            var categories = new List<CategoryData>();

            try
            {
                string query = "SELECT id, name FROM inventory17.MainCategory ORDER BY id";
                using (var conn = _db.GetConnection())
                using (var cmd = new NpgsqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        categories.Add(new CategoryData
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1)
                        });
                    }
                }
            }
            catch
            {

                throw;
            }

            return categories;
        }

        private void PopulateCategories(List<CategoryData> categories) // Отображает загруженные категории в flow-панели
        {
            flowMainCategories.SuspendLayout();
            flowMainCategories.Controls.Clear();

            foreach (var category in categories)
            {
                var checkBox = new Guna2CheckBox
                {
                    Text = category.Name,
                    Tag = category.Id,
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    AutoSize = true,
                    CheckedState = {
                        BorderColor = Color.FromArgb(94, 148, 255),
                        FillColor = Color.Teal,
                    }
                };

                checkBox.CheckedChanged += (sender, e) =>
                {
                    if (checkBox.Checked)
                        LoadSubCategories(category.Id);
                    else
                        RemoveSubCategories(category.Id);
                    UpdateCounters();
                };

                flowMainCategories.Controls.Add(checkBox);
            }

            flowMainCategories.ResumeLayout();
        }

        private List<LocationData> LoadLocationsData() // Загружает данные местоположений из БД с подсчетом предметов
        {
            var locations = new List<LocationData>();

            try
            {
                string query = @"SELECT l.id, l.name, 
                       (SELECT COUNT(*) FROM inventory17.Inventory i 
                        WHERE i.location_id = l.id AND i.status_id = 1) as item_count
                       FROM inventory17.Location l
                       ORDER BY l.name";


                using (var conn = _db.GetConnection())
                using (var cmd = new NpgsqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        locations.Add(new LocationData
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            ItemCount = reader.GetInt32(2)
                        });
                    }
                }
            }
            catch
            {

                throw;
            }

            return locations;
        }

        private void PopulateLocations(List<LocationData> locations) // Отображает местоположения в flow-панели с указанием количества предметов
        {
            flowLocations.SuspendLayout();
            flowLocations.Controls.Clear();

            foreach (var location in locations)
            {
                var checkBox = new Guna2CheckBox
                {
                    Text = $"{location.Name} ({location.ItemCount})",
                    Tag = location.Id,
                    Font = new Font("Segoe UI", 9F),
                    AutoSize = true,
                    CheckedState = {
                        BorderColor = Color.FromArgb(94, 148, 255),
                        FillColor = Color.Teal,
                    }
                };

                checkBox.CheckedChanged += (sender, e) => UpdateCounters();
                flowLocations.Controls.Add(checkBox);
            }

            flowLocations.ResumeLayout();
        }

        private void SelectAllSubCategories_CheckedChanged(object sender, EventArgs e) // Обработчик "Выбрать все" для подкатегорий
        {
            checkBoxSelectAllCategories.CheckedChanged -= SelectAllSubCategories_CheckedChanged;

            bool isChecked = checkBoxSelectAllCategories.Checked;
            foreach (Control control in flowSubCategories.Controls)
            {
                if (control is Guna2CheckBox checkBox)
                {
                    checkBox.Checked = isChecked;
                }
            }

            checkBoxSelectAllCategories.CheckedChanged += SelectAllSubCategories_CheckedChanged;
            UpdateCounters();
        }

        private void SelectAllLocations_CheckedChanged(object sender, EventArgs e) // Обработчик "Выбрать все" для местоположений
        {
            checkBoxSelectAllLocations.CheckedChanged -= SelectAllLocations_CheckedChanged;

            bool isChecked = checkBoxSelectAllLocations.Checked;
            foreach (Control control in flowLocations.Controls)
            {
                if (control is Guna2CheckBox checkBox)
                {
                    checkBox.Checked = isChecked;
                }
            }

            checkBoxSelectAllLocations.CheckedChanged += SelectAllLocations_CheckedChanged;
            UpdateCounters();
        }

        private void UpdateCounters() // Обновляет счетчики выбранных элементов и состояние кнопок
        {
            selectedSubCategoriesCount = 0;
            foreach (Control control in flowSubCategories.Controls)
            {
                if (control is Guna2CheckBox checkBox && checkBox.Checked)
                {
                    selectedSubCategoriesCount++;
                }
            }
            lblSelectedCategoriesCount.Text = $"Выбрано: {selectedSubCategoriesCount}";
            checkBoxSelectAllCategories.Checked = selectedSubCategoriesCount > 0 &&
                selectedSubCategoriesCount == flowSubCategories.Controls.Count;

            selectedLocationsCount = 0;
            foreach (Control control in flowLocations.Controls)
            {
                if (control is Guna2CheckBox checkBox && checkBox.Checked)
                {
                    selectedLocationsCount++;
                }
            }
            lblSelectedLocationsCount.Text = $"Выбрано: {selectedLocationsCount}";
            checkBoxSelectAllLocations.Checked = selectedLocationsCount > 0 &&
                selectedLocationsCount == flowLocations.Controls.Count;

            UpdateButtonsState();
        }

        private void UpdateButtonsState() // Активирует/деактивирует кнопки Apply/Cancel при выборе
        {
            bool hasSelection = selectedSubCategoriesCount > 0 || selectedLocationsCount > 0;
            buttonApply.Enabled = hasSelection;
            buttonCancel.Enabled = hasSelection;
        }

        private void LoadSubCategories(int mainCategoryId) // Загружает подкатегории для выбранной основной категории
        {
            try
            {
                string query = @"
            SELECT sc.id, sc.name, 
                   (SELECT COUNT(*) FROM inventory17.Inventory i 
                    WHERE i.sub_category_id = sc.id AND i.status_id = 1) as item_count
            FROM inventory17.SubCategory sc
            WHERE sc.main_category_id = @mainCategoryId
            ORDER BY sc.id";

                using (var conn = _db.GetConnection())
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@mainCategoryId", mainCategoryId);

                    flowSubCategories.SuspendLayout();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var subCategoryId = reader.GetInt32(0);
                            var subCategoryName = reader.GetString(1);
                            var itemCount = reader.GetInt32(2);

                            var checkBox = new Guna2CheckBox
                            {
                                Text = $"{subCategoryName} ({itemCount})",
                                Tag = new SubCategoryInfo
                                {
                                    Id = subCategoryId,
                                    MainCategoryId = mainCategoryId,
                                    ItemCount = itemCount
                                },
                                Font = new Font("Segoe UI", 9F),
                                AutoSize = true,
                                CheckedState = {
                                    BorderColor = Color.FromArgb(94, 148, 255),
                                    FillColor = Color.Teal,
                                }
                            };

                            checkBox.CheckedChanged += (sender, e) => UpdateCounters();
                            flowSubCategories.Controls.Add(checkBox);
                        }
                    }

                    flowSubCategories.ResumeLayout();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки подкатегорий: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            UpdateCounters();
        }

        private void RemoveSubCategories(int mainCategoryId) // Удаляет подкатегории при снятии выбора с основной категории
        {
            var controlsToRemove = new List<Control>();
            foreach (Control control in flowSubCategories.Controls)
            {
                if (control.Tag is SubCategoryInfo info && info.MainCategoryId == mainCategoryId)
                {
                    controlsToRemove.Add(control);
                }
            }

            flowSubCategories.SuspendLayout();
            foreach (var control in controlsToRemove)
            {
                flowSubCategories.Controls.Remove(control);
            }
            flowSubCategories.ResumeLayout();
        }
        private void ApplyFilters(object sender, EventArgs e) // Применяет выбранные фильтры и закрывает форму
        {
            SelectedSubCategories.Clear();
            foreach (Control control in flowSubCategories.Controls)
            {
                if (control is Guna2CheckBox checkBox && checkBox.Checked && checkBox.Tag is SubCategoryInfo info)
                {
                    SelectedSubCategories.Add(info.Id);
                }
            }

            SelectedLocations.Clear();
            foreach (Control control in flowLocations.Controls)
            {
                if (control is Guna2CheckBox checkBox && checkBox.Checked)
                {
                    SelectedLocations.Add((int)checkBox.Tag);
                }
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void ResetAllFilters(object sender, EventArgs e) // Сбрасывает фильтры без применения
        {
            foreach (Control control in flowMainCategories.Controls)
            {
                if (control is Guna2CheckBox checkBox)
                    checkBox.Checked = false;
            }

            flowSubCategories.Controls.Clear();

            foreach (Control control in flowLocations.Controls)
            {
                if (control is Guna2CheckBox checkBox)
                    checkBox.Checked = false;
            }

            checkBoxSelectAllCategories.Checked = false;
            checkBoxSelectAllLocations.Checked = false;

            SelectedSubCategories.Clear();
            SelectedLocations.Clear();
            UpdateCounters();
        }
    }
}