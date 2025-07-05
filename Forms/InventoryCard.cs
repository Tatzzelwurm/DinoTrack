using DinoTrack.Data.SchoolInventoryApp.Data;
using DinoTrack.Models;
using Npgsql;
using System.Data;

namespace DinoTrack.Forms
{
    /// <summary>
    /// Форма для просмотра и редактирования полной информации об инвентаре с историей перемещения инвентаря
    /// </summary>
    public partial class InventoryCard : Form
    {
        private Inventory _currentInventory;
        private ConnectionDatabase _db;
        private bool _hasChanges = false;
        private string _originalLocation;
        private int _originalLocationId;
        private bool _locationChanged = false;
        private Dictionary<string, int> _locationMap = new Dictionary<string, int>();
        public event Action DataUpdated;

        public InventoryCard(Inventory inventory) // Конструктор - принимает объект инвентаря и инициализирует данные
        {
            InitializeComponent();
            _currentInventory = inventory;
            _db = new ConnectionDatabase();

            LoadInventoryData();
            LoadLocations();
            LoadInventoryHistory();
            SetupForm();
            SetupEventHandlers();
            PriceBox.KeyPress += PriceBox_KeyPress;
            CountBox.KeyPress += CountBox_KeyPress;
            StartDateBox.KeyPress += StartDateBox_KeyPress;
        }
        private void LoadInventoryData() // Загружает данные инвентаря в поля формы
        {
            InvNumberBox.Text = _currentInventory.InventoryNumber;
            SerialNumberBox.Text = _currentInventory.SerialNumber;
            NameInventoryBox.Text = _currentInventory.Name;
            DescriptionBox.Text = _currentInventory.Description;
            PriceBox.Text = _currentInventory.Price.ToString("N2");
            CommentsBox.Text = _currentInventory.Comments;
            CountBox.Text = _currentInventory.Count.ToString();
            TypeBox.Text = _db.GetTypeNameById(_currentInventory.TypeId);

            string categoryInfo = _db.GetCategoryNameById(_currentInventory.SubCategoryId) ?? "Не указано";
            SubcategoryBox.Text = categoryInfo;

            StatusBox.Text = GetStatusName(_currentInventory.StatusId);
            StartDateBox.Text = _db.GetInventoryStartDate(_currentInventory.Id)?.ToString("dd.MM.yyyy");


            AddDataLbl.Text = $"Дата добавления: {_currentInventory.CreatedAt:dd.MM.yyyy HH:mm}";
            UpdateDataLbl.Text = $"Дата последнего обновления: {_currentInventory.UpdatedAt:dd.MM.yyyy HH:mm}";

            WriteOffButton.Visible = _currentInventory.StatusId == 1;
            RestoreButton.Visible = _currentInventory.StatusId == 2;
            DeleteButton.Visible = _currentInventory.StatusId == 2;
        }

        private void LoadInventoryHistory() // Загружает историю перемещений инвентаря
        {
            try
            {
                var history = _db.GetInventoryHistory(_currentInventory.Id);
                historyDataGridView.Rows.Clear();

                foreach (DataRow row in history.Rows)
                {
                    historyDataGridView.Rows.Add(
                        row["row_num"],
                        "Перемещение",
                        row["old_location_name"],
                        row["new_location_name"],
                        Convert.ToDateTime(row["date_of_move"]).ToString("dd.MM.yyyy HH:mm")

                    );
                }
                historyDataGridView.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
                historyDataGridView.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
                historyDataGridView.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки истории: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void SetupForm() // Настраивает начальное состояние формы (скрывает панель редактирования с кнопками "Сохранить" и "Отмена")
        {
            Text = $"Карточка инвентаря: {_currentInventory.Name}";
            EditPanel.Visible = false;
        }

        private void SetupEventHandlers() // Подписывает все поля формы на событие изменения для отслеживания редактирования
        {
            NameInventoryBox.TextChanged += Field_TextChanged;
            SerialNumberBox.TextChanged += Field_TextChanged;
            CountBox.TextChanged += Field_TextChanged;
            PriceBox.TextChanged += Field_TextChanged;
            StartDateBox.TextChanged += Field_TextChanged;
            CommentsBox.TextChanged += Field_TextChanged;
            DescriptionBox.TextChanged += Field_TextChanged;
            LocationComboBox.SelectedIndexChanged += Field_TextChanged;
            LocationComboBox.Enter += LocationComboBox_Enter;
            LocationComboBox.KeyPress += LocationComboBox_KeyPress;
            ViewAndEditInventory.SelectedIndexChanged += TabControl_SelectedIndexChanged;
        }

        private void TabControl_SelectedIndexChanged(object sender, EventArgs e) // Обработчик переключения между вкладками
        {
            if (ViewAndEditInventory.SelectedTab == HistoryInventory)
            {
                LoadInventoryHistory();
            }
        }

        private void LocationComboBox_Enter(object sender, EventArgs e) // Автоматически выделяет текст при входе в ComboBox местоположения
        {
            LocationComboBox.SelectAll();
        }

        private void LocationComboBox_KeyPress(object sender, KeyPressEventArgs e) // Реализует быстрый поиск по первой букве в ComboBox местоположения
        {
            if (!char.IsControl(e.KeyChar))
            {
                LocationComboBox.Text = e.KeyChar.ToString();
                LocationComboBox.SelectionStart = 1;
                e.Handled = true;
            }
        }

        private void Field_TextChanged(object sender, EventArgs e) // Отмечает наличие изменений при редактировании любых полей
        {
            _hasChanges = true;


            if (sender == LocationComboBox)
            {
                _locationChanged = true;
            }

            EditPanel.Visible = _hasChanges;
        }


        private void ApplyButton_Click(object sender, EventArgs e) // Обработчик кнопки "Применить" - сохраняет изменения
        {
            SaveChanges();
        }

        private void CancelButton_Click(object sender, EventArgs e) // Обработчик кнопки "Отмена" - откатывает изменения
        {
            CancelChanges();
        }

        private void SaveChanges() // Основной метод сохранения изменений с валидацией
        {
            // Валидация полей перед сохранением
            if (!Validation.Validation.ValidateInventoryDetailsFields(
                NameInventoryBox,
                SubcategoryBox,
                TypeBox,
                CountBox))
            {
                return;
            }

            try
            {
                string locationName = LocationComboBox.Text;
                int? newLocationId = GetLocationId(locationName);
                int? oldLocationId = _originalLocationId;

                using (var conn = _db.GetConnection())
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // Обновление данных инвентаря
                        UpdateInventory(conn, newLocationId);

                        // Логирование изменения местоположения только если оно было изменено
                        if (_locationChanged && oldLocationId != newLocationId)
                        {
                            LogLocationChange(oldLocationId, newLocationId);
                        }

                        transaction.Commit();

                        // Обновление состояния формы
                        _hasChanges = false;
                        _locationChanged = false;
                        EditPanel.Visible = false;
                        _originalLocation = locationName;
                        _originalLocationId = newLocationId ?? 0;
                        _currentInventory.UpdatedAt = DateTime.Now;
                        UpdateDataLbl.Text = $"Дата последнего обновления: {_currentInventory.UpdatedAt:dd.MM.yyyy HH:mm}";

                        MessageBox.Show("Данные успешно обновлены", "Успех!",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        LoadInventoryHistory(); // Обновление истории после изменений
                        DataUpdated?.Invoke();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении данных: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateInventory(NpgsqlConnection conn, int? newLocationId) // Обновляет данные инвентаря в БД 
        {
            // Обновление основной таблицы inventory
            using (var cmd = new NpgsqlCommand(
        "UPDATE inventory17.inventory SET name = @name, serial_number = @serial_number, " +
        "location_id = @location_id, count = @count, price = @price, " +
        "description = @description, comments = @comments, updated_at = NOW() " +
        "WHERE id = @id", conn))
            {
                cmd.Parameters.AddWithValue("@id", _currentInventory.Id);
                cmd.Parameters.AddWithValue("@name", NameInventoryBox.Text);
                cmd.Parameters.AddWithValue("@serial_number", SerialNumberBox.Text);


                if (newLocationId.HasValue && newLocationId.Value > 0)
                {
                    cmd.Parameters.AddWithValue("@location_id", newLocationId.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@location_id", DBNull.Value);
                }

                short count;
                if (!short.TryParse(CountBox.Text, out count))
                    throw new ArgumentException("Неверное значение количества");
                cmd.Parameters.AddWithValue("@count", count);

                decimal price;
                if (!decimal.TryParse(PriceBox.Text, out price))
                    throw new ArgumentException("Неверное значение цены");
                cmd.Parameters.AddWithValue("@price", price);

                cmd.Parameters.AddWithValue("@comments", CommentsBox.Text);
                cmd.Parameters.AddWithValue("@description", DescriptionBox.Text);

                cmd.ExecuteNonQuery();
            }

            // Обновление таблицы lifetime
            if (!string.IsNullOrWhiteSpace(StartDateBox.Text) && DateTime.TryParse(StartDateBox.Text, out DateTime startDate))
            {
                using (var cmdLifetime = new NpgsqlCommand(
                    "UPDATE inventory17.lifetime SET start_date = @start_date " +
                    "WHERE inventory_id = @inventory_id", conn))
                {
                    cmdLifetime.Parameters.AddWithValue("@inventory_id", _currentInventory.Id);
                    cmdLifetime.Parameters.AddWithValue("@start_date", startDate);
                    cmdLifetime.ExecuteNonQuery();
                }
            }
        }
        private void LogLocationChange(int? oldLocationId, int? newLocationId) // Логирует изменение местоположения в историю перемещения
        {
            _db.LogLocationChange(_currentInventory.Id, oldLocationId ?? 0, newLocationId ?? 0);
        }

        private int? GetLocationId(string locationName) // Получает ID местоположения по названию из кэша или БД
        {
            if (string.IsNullOrEmpty(locationName))
                return null;

            if (_locationMap.TryGetValue(locationName, out int locationId))
            {
                return locationId;
            }

            try
            {
                using (var conn = _db.GetConnection())
                using (var cmd = new NpgsqlCommand("SELECT id FROM inventory17.Location WHERE name = @name", conn))
                {
                    cmd.Parameters.AddWithValue("@name", locationName);
                    var result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        int id = Convert.ToInt32(result);
                        _locationMap[locationName] = id;
                        return id;
                    }
                }
            }
            catch
            {
                return null;
            }

            return null;
        }

        private void CancelChanges() // Откатывает все изменения к исходным значениям
        {
            NameInventoryBox.Text = _currentInventory.Name;
            DescriptionBox.Text = _currentInventory.Description;
            CommentsBox.Text = _currentInventory.Comments;
            LocationComboBox.Text = _originalLocation;
            SerialNumberBox.Text = _currentInventory.SerialNumber;
            PriceBox.Text = _currentInventory.Price.ToString("N2");
            CountBox.Text = _currentInventory.Count.ToString();
            StartDateBox.Text = _db.GetInventoryStartDate(_currentInventory.Id)?.ToString("dd.MM.yyyy");


            _hasChanges = false;
            EditPanel.Visible = false;
        }

        private void LoadLocations() // Загружает список местоположений в LocationComboBox
        {
            try
            {
                LocationComboBox.Items.Clear();
                _locationMap.Clear();

                using (var conn = _db.GetConnection())
                using (var cmd = new NpgsqlCommand("SELECT id, name FROM inventory17.Location ORDER BY name", conn))
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

                if (_currentInventory.LocationId > 0)
                {
                    using (var conn = _db.GetConnection())
                    using (var cmd = new NpgsqlCommand("SELECT name FROM inventory17.Location WHERE id = @id", conn))
                    {
                        cmd.Parameters.AddWithValue("@id", _currentInventory.LocationId);
                        var locationName = cmd.ExecuteScalar()?.ToString();

                        if (!string.IsNullOrEmpty(locationName))
                        {
                            LocationComboBox.Text = locationName;
                            _originalLocation = locationName;
                            _originalLocationId = _currentInventory.LocationId ?? 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке местоположений: {ex.Message}");
            }
        }

        private string GetStatusName(int statusId) // Возвращает название статуса по его ID
        {
            try
            {
                using (var conn = _db.GetConnection())
                using (var cmd = new NpgsqlCommand("SELECT name FROM inventory17.Status WHERE id = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", statusId);
                    return cmd.ExecuteScalar()?.ToString() ?? "Не указан";
                }
            }
            catch
            {
                return "Не указан";
            }
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
                e.Handled = true;
            }
        }
        private void StartDateBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validation.Validation.FormatDateInput((Guna.UI2.WinForms.Guna2TextBox)sender, e);
        }



        private void WriteOffButton_Click(object sender, EventArgs e) // Обработчик кнопки списания инвентаря
        {
            // Запрашиваем подтверждение у пользователя
            var confirmResult = MessageBox.Show(
                "Вы уверены, что хотите списать этот инвентарь?",
                "Подтверждение списания",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Warning);

            if (confirmResult == DialogResult.OK)
            {
                try
                {
                    // Выполняем списание
                    bool success = _db.WriteOffInventory(_currentInventory.Id);

                    if (success)
                    {
                        MessageBox.Show(
                            "Инвентарь успешно списан",
                            "Успех",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        // Закрываем форму после успешного списания
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(
                            "Не удалось списать инвентарь",
                            "Ошибка",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        $"Ошибка при списании инвентаря: {ex.Message}",
                        "Ошибка",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }
        private void RestoreButton_Click(object sender, EventArgs e) // Обработчик кнопки восстановления инвентаря
        {
            // Запрашиваем подтверждение у пользователя
            var confirmResult = MessageBox.Show(
                "Вы уверены, что хотите восстановить этот инвентарь?",
                "Подтверждение восстановления",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question);

            if (confirmResult == DialogResult.OK)
            {
                try
                {
                    // Выполняем восстановление
                    bool success = _db.RestoreInventory(_currentInventory.Id);

                    if (success)
                    {
                        MessageBox.Show(
                            "Инвентарь успешно восстановлен",
                            "Успех",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        // Закрываем форму после успешного восстановления
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(
                            "Не удалось восстановить инвентарь",
                            "Ошибка",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        $"Ошибка при восстановлении инвентаря: {ex.Message}",
                        "Ошибка",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e) // Обработчик кнопки удаления инвентаря
        {
            // Запрашиваем подтверждение у пользователя
            var confirmResult = MessageBox.Show(
                "Вы уверены, что хотите УДАЛИТЬ этот инвентарь? Это действие нельзя отменить!",
                "Подтверждение удаления",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Warning);

            if (confirmResult == DialogResult.OK)
            {
                try
                {
                    // Выполняем удаление
                    bool success = _db.DeleteInventory(_currentInventory.Id);

                    if (success)
                    {
                        MessageBox.Show(
                            "Инвентарь успешно удален",
                            "Успех",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        // Закрываем форму после успешного удаления
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(
                            "Не удалось удалить инвентарь",
                            "Ошибка",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        $"Ошибка при удалении инвентаря: {ex.Message}",
                        "Ошибка",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }
    }
}