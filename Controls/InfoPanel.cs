using Guna.UI2.WinForms;

namespace DinoTrack.Controls
{
    /// <summary>
    /// Панель информации и фильтрации инвентаря.
    /// Отображает количество объектов инвентаря (в зависимости от типа), содержит кнопки фильтрации по типам инвентаря,
    /// а также кнопки сброса фильтров и экспорта отфильтрованных данных.
    /// </summary>
    public partial class InfoPanel : UserControl
    {
        public event EventHandler ResetFiltersRequested; // Событие запроса сброса фильтров
        public event EventHandler ExportFilteredRequested; // Событие запроса экспорта отфильтрованных данных
        public event EventHandler<string> FilterRequested; // Событие запроса фильтрации
        private Guna2GradientButton _activeButton; // Текущая активная кнопка фильтра

        public InfoPanel() // Конструктор панели, инициализирует компоненты и настройки панели
        {
            InitializeComponent();
            var toolTip = new ToolTip();
            toolTip.SetToolTip(btnExportFiltered, "Экспортировать");
            toolTip.SetToolTip(btnResetFilters, "Сбросить фильтры");

            btnResetFilters.Visible = false;
            btnExportFiltered.Visible = false;
            filteredLabel.Visible = false;
            SetActiveButton(btnAcitveInventory);

            btnResetFilters.Click += ResetFiltersButton_Click;
            btnExportFiltered.Click += BtnExportFiltered_Click;

            btnNotMainMeans.Click += (s, e) => FilterRequested?.Invoke(this, "non_main");
            btnMainMeans.Click += (s, e) => FilterRequested?.Invoke(this, "main");
            btnInactiveInventory.Click += (s, e) =>
            {
                SetActiveButton(btnInactiveInventory);
                FilterRequested?.Invoke(this, "written_off");
            };
            btnAcitveInventory.Click += (s, e) =>
            {
                SetActiveButton(btnAcitveInventory);
                FilterRequested?.Invoke(this, "all");
            };
        }

        public void UpdateCounts(int totalCount, int? filtered = null) // Обновляет счетчики элементов
        {
            string labelText = _activeButton switch
            {
                _ when _activeButton == btnAcitveInventory => $"Всего в эксплуатации: {totalCount}",
                _ when _activeButton == btnMainMeans => $"Основные средства: {totalCount}",
                _ when _activeButton == btnNotMainMeans => $"Неосновные средства: {totalCount}",
                _ when _activeButton == btnInactiveInventory => $"Списанный инвентарь: {totalCount}",
                _ => $"Всего: {totalCount}"
            };

            totalLabel.Text = labelText;

            if (filtered.HasValue && filtered.Value != totalCount)
            {
                filteredLabel.Text = $"Отфильтровано: {filtered.Value}";
                filteredLabel.Visible = true;
                btnResetFilters.Visible = true;
                btnExportFiltered.Visible = true;
            }
            else
            {
                filteredLabel.Visible = false;
                btnResetFilters.Visible = false;
                btnExportFiltered.Visible = false;
            }
        }
        public string GetActiveFilterType() // Возвращает тип активного фильтра
        {
            return _activeButton switch
            {
                _ when _activeButton == btnMainMeans => "main",
                _ when _activeButton == btnNotMainMeans => "non_main",
                _ when _activeButton == btnInactiveInventory => "written_off",
                _ => "all"
            };
        }
        public void SetActiveButton(Guna2GradientButton button) // Устанавливает активную кнопку фильтра
        {
            if (_activeButton != null)
            {
                _activeButton.FillColor = Color.WhiteSmoke;
                _activeButton.FillColor2 = Color.WhiteSmoke;
                _activeButton.ForeColor = Color.Black;
                _activeButton.BorderThickness = 0;
            }

            button.FillColor = Color.LightCyan;
            button.FillColor2 = Color.LightCyan;
            button.ForeColor = Color.Black;
            button.BorderColor = Color.DarkSlateGray;
            button.BorderThickness = 1;

            _activeButton = button;
        }
        public void UpdateTotalLabel(int count) // Обновляет текст метки с общим количеством
        {
            totalLabel.Text = $"Всего в эксплуатации: {count}";
        }


        private void BtnExportFiltered_Click(object sender, EventArgs e) // Обработчик экспорта отфильтрованных данных
        {
            ExportFilteredRequested?.Invoke(this, EventArgs.Empty);
        }

        private void ResetFiltersButton_Click(object sender, EventArgs e) // Обработчик сброса фильтров
        {
            totalLabel.Visible = true;
            ResetFiltersRequested?.Invoke(this, EventArgs.Empty);
        }

        private void btnAcitveInventory_Click(object sender, EventArgs e) // Обработчик фильтра "Весь активный инвентарь"

        {
            SetActiveButton(btnAcitveInventory);
            FilterRequested?.Invoke(this, "all");
        }

        private void btnMainMeans_Click(object sender, EventArgs e) // Обработчик фильтра "Основные средства"
        {
            SetActiveButton(btnMainMeans);
            FilterRequested?.Invoke(this, "main");
        }

        private void btnNotMainMeans_Click(object sender, EventArgs e) // Обработчик фильтра "Неосновные средства"
        {
            SetActiveButton(btnNotMainMeans);
            FilterRequested?.Invoke(this, "non_main");
        }

        private void btnInactiveInventory_Click(object sender, EventArgs e) // Обработчик фильтра "Списанный инвентарь"
        {
            SetActiveButton(btnInactiveInventory);
            FilterRequested?.Invoke(this, "written_off");
        }
        public void ResetToDefaultView() // Сбрасывает панель к состоянию по умолчанию
        {
            SetActiveButton(btnAcitveInventory);
            totalLabel.Visible = true;
            filteredLabel.Visible = false;
            btnResetFilters.Visible = false;
            btnExportFiltered.Visible = false;
        }

    }
}