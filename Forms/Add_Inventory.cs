using DinoTrack.Data.SchoolInventoryApp.Data;
using Npgsql;
using System.Data;

namespace DinoTrack.Forms
{
    /// <summary>
    /// Форма для добавления нового инвентаря в систему
    /// </summary>
    public partial class Add_Inventory : Form
    {
        private ConnectionDatabase _db;
        private Dictionary<string, int> _locationMap = new Dictionary<string, int>();
        private Dictionary<string, int> _typeMap = new Dictionary<string, int>();
        private Dictionary<string, (int mainCatId, int subCatId)> _categoryMap = new Dictionary<string, (int, int)>();

        public Add_Inventory() // Конструктор формы - инициализирует компоненты и загружает справочники
        {
            InitializeComponent();
            bool showTooltips = Properties.Settings.Default.ShowTooltipsInAddForm; // Загружает настройку показа подсказок
            PriceBox.KeyPress += PriceBox_KeyPress;
            CountBox.KeyPress += CountBox_KeyPress;
            StartDateBox.KeyPress += StartDateBox_KeyPress;

            // Настройка видимости кнопок с подсказками
            BtnInfo1.Visible = showTooltips;
            BtnInfo2.Visible = showTooltips;
            BtnInfo3.Visible = showTooltips;
            BtnInfo4.Visible = showTooltips;
            BtnInfo5.Visible = showTooltips;
            BtnInfo6.Visible = showTooltips;

            // Настройка ToolTip только если подсказки включены
            if (showTooltips)
            {
                var toolTip = new ToolTip();
                toolTip.SetToolTip(BtnInfo1, "Это обязательное поле");
                toolTip.SetToolTip(BtnInfo2, "Это обязательное поле");
                toolTip.SetToolTip(BtnInfo3, "Это обязательное поле");
                toolTip.SetToolTip(BtnInfo4, "Это обязательное поле");
                toolTip.SetToolTip(BtnInfo5, "Для быстрого поиска введите в поле первый символ наименования");
                toolTip.SetToolTip(BtnInfo6, "Введите дату без пробелов");
            }
            _db = new ConnectionDatabase();

            // Настройка ComboBox для поиска по первой букве
            SetupComboBoxSearch();

            LoadLocations();
            LoadTypes();
            LoadCategories();
        }
        private void PriceBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Validation.Validation.ValidateDecimalInput(e, (Guna.UI2.WinForms.Guna2TextBox)sender))
            {
                e.Handled = true;
            }
        }

        private void CountBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Validation.Validation.ValidateIntegerInput(e))
            {
                {
                    e.Handled = true;
                }
            }
        }
        private void StartDateBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validation.Validation.FormatDateInput((Guna.UI2.WinForms.Guna2TextBox)sender, e);
        }

        private void SetupComboBoxSearch() // Настраивает ComboBox для поиска по первой букве (автодополнение)
        {
            // Настройка LocationComboBox
            LocationComboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            LocationComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            LocationComboBox.KeyPress += LocationComboBox_KeyPress;

            // Настройка SubcategoryComboBox
            SubcategoryComboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            SubcategoryComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            SubcategoryComboBox.KeyPress += SubcategoryComboBox_KeyPress;

            // Настройка TypeComboBox
            TypeComboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            TypeComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            TypeComboBox.KeyPress += TypeComboBox_KeyPress;
            TypeComboBox.Enter += TypeComboBox_Enter;
        }

        #region ComboBox Event Handlers

        private void LocationComboBox_KeyPress(object sender, KeyPressEventArgs e) // Обработчик нажатия клавиш в ComboBox местоположений
        {
            if (!char.IsControl(e.KeyChar))
            {
                LocationComboBox.Text = e.KeyChar.ToString();
                LocationComboBox.SelectionStart = 1;
                e.Handled = true;
            }
        }



        private void SubcategoryComboBox_KeyPress(object sender, KeyPressEventArgs e) // Обработчик нажатия клавиш в ComboBox подкатегорий
        {
            if (!char.IsControl(e.KeyChar))
            {
                SubcategoryComboBox.Text = e.KeyChar.ToString();
                SubcategoryComboBox.SelectionStart = 1;
                e.Handled = true;
            }
        }

        private void TypeComboBox_Enter(object sender, EventArgs e) // Обработчик входа в ComboBox типа инвентаря
        {
            TypeComboBox.DroppedDown = true;
            TypeComboBox.SelectAll();
        }

        private void TypeComboBox_KeyPress(object sender, KeyPressEventArgs e) // Обработчик нажатия клавиш в ComboBox типа инвентаря
        {
            if (!char.IsControl(e.KeyChar))
            {
                TypeComboBox.Text = e.KeyChar.ToString();
                TypeComboBox.SelectionStart = 1;
                e.Handled = true;
            }
        }
        #endregion

        #region Data Loading Methods
        private void LoadLocations() // Загружает список местоположений из БД в ComboBox
        {
            try
            {
                LocationComboBox.Items.Clear();
                _locationMap.Clear();

                using (var conn = _db.GetConnection())
                using (var cmd = new NpgsqlCommand(
                    "SELECT id, name FROM inventory17.Location ORDER BY name", conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        LocationComboBox.Items.Add(name);
                        _locationMap[name] = id;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки местоположений: {ex.Message}");
            }
        }

        private void LoadTypes() // Загружает список типов инвентаря из БД в ComboBox
        {
            try
            {
                TypeComboBox.Items.Clear();
                _typeMap.Clear();

                using (var conn = _db.GetConnection())
                using (var cmd = new NpgsqlCommand(
                    "SELECT id, name FROM inventory17.Type ORDER BY name", conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        TypeComboBox.Items.Add(name);
                        _typeMap[name] = id;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки типов: {ex.Message}");
            }
        }

        private void LoadCategories() // Загружает иерархию категорий (основные/подкатегории) из БД
        {
            try
            {
                SubcategoryComboBox.Items.Clear();
                _categoryMap.Clear();

                using (var conn = _db.GetConnection())
                using (var cmd = new NpgsqlCommand(@"
                    SELECT mc.id as main_id, mc.name as main_name, 
                           sc.id as sub_id, sc.name as sub_name
                    FROM inventory17.SubCategory sc
                    JOIN inventory17.MainCategory mc ON sc.main_category_id = mc.id
                    ORDER BY mc.name, sc.name", conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int mainId = reader.GetInt32("main_id");
                        string mainName = reader.GetString("main_name");
                        int subId = reader.GetInt32("sub_id");
                        string subName = reader.GetString("sub_name");

                        string displayText = $"{subName}";
                        SubcategoryComboBox.Items.Add(displayText);
                        _categoryMap[displayText] = (mainId, subId);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки категорий: {ex.Message}");
            }
        }
        #endregion
        private void AddInventoryBtn_Click(object sender, EventArgs e) // Обработчик клика по кнопке "Добавить инвентарь" - валидация и сохранение
        {
            if (!Validation.Validation.ValidateAddInventoryFields(NameInventoryBox, SubcategoryComboBox, TypeComboBox, CountBox))
            {
                return;
            }

            try
            {
                using (var conn = _db.GetConnection())
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        if (!_typeMap.TryGetValue(TypeComboBox.Text, out int typeId) ||
                            !_categoryMap.TryGetValue(SubcategoryComboBox.Text, out var categoryIds))
                        {
                            return;
                        }

                        var (mainCatId, subCatId) = categoryIds;
                        int? locationId = LocationComboBox.SelectedIndex == -1 ?
                            null : (int?)_locationMap[LocationComboBox.Text];

                        // 1. Добавляем запись в таблицу inventory
                        int newInventoryId;
                        using (var cmdInventory = new NpgsqlCommand(
                            "INSERT INTO inventory17.inventory (" +
                            "inventory_number, serial_number, name, description, " +
                            "price, comments, main_category_id, sub_category_id, " +
                            "location_id, type_id, count, status_id) " +
                            "VALUES (@inv_num, @serial_num, @name, @desc, " +
                            "@price, @comments, @main_cat, @sub_cat, " +
                            "@loc_id, @type_id, @count, 1) " + // status_id = 1 (активный)
                            "RETURNING id", conn))
                        {
                            cmdInventory.Parameters.AddWithValue("@inv_num",
                                string.IsNullOrEmpty(InvNumberBox.Text) ? DBNull.Value : (object)InvNumberBox.Text);
                            cmdInventory.Parameters.AddWithValue("@serial_num",
                                string.IsNullOrEmpty(SerialNumberBox.Text) ? DBNull.Value : (object)SerialNumberBox.Text);
                            cmdInventory.Parameters.AddWithValue("@name", NameInventoryBox.Text);
                            cmdInventory.Parameters.AddWithValue("@desc",
                                string.IsNullOrEmpty(DescriptionBox.Text) ? DBNull.Value : (object)DescriptionBox.Text);
                            cmdInventory.Parameters.AddWithValue("@price",
                                string.IsNullOrEmpty(PriceBox.Text) ? DBNull.Value : (object)decimal.Parse(PriceBox.Text));
                            cmdInventory.Parameters.AddWithValue("@comments",
                                string.IsNullOrEmpty(CommentsBox.Text) ? DBNull.Value : (object)CommentsBox.Text);
                            cmdInventory.Parameters.AddWithValue("@main_cat", mainCatId);
                            cmdInventory.Parameters.AddWithValue("@sub_cat", subCatId);
                            cmdInventory.Parameters.AddWithValue("@loc_id", locationId ?? (object)DBNull.Value);
                            cmdInventory.Parameters.AddWithValue("@type_id", typeId);
                            cmdInventory.Parameters.AddWithValue("@count", short.Parse(CountBox.Text));

                            newInventoryId = (int)cmdInventory.ExecuteScalar();
                        }

                        // 2. Добавляем запись в таблицу lifetime
                        using (var cmdLifetime = new NpgsqlCommand(
                            "INSERT INTO inventory17.lifetime (" +
                            "inventory_id, start_date) " +
                            "VALUES (@inv_id, @start_date)", conn))
                        {
                            cmdLifetime.Parameters.AddWithValue("@inv_id", newInventoryId);

                            if (DateTime.TryParse(StartDateBox.Text, out DateTime startDate))
                            {
                                cmdLifetime.Parameters.AddWithValue("@start_date", startDate);
                            }
                            else
                            {
                                cmdLifetime.Parameters.AddWithValue("@start_date", DBNull.Value);
                            }

                            cmdLifetime.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        MessageBox.Show("Инвентарь успешно добавлен", "Успех",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show($"Инвентарь с указанным инвентарным номером уже существует", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Критическая ошибка подключения: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}