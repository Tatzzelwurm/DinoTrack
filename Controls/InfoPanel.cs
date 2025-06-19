using System;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace DinoTrack.Controls
{
    public partial class InfoPanel : UserControl
    {
        public event EventHandler ResetFiltersRequested;
        public event EventHandler ExportFilteredRequested;
        private Guna2GradientButton _activeButton;

        public InfoPanel()
        {
            InitializeComponent();
            var toolTip = new ToolTip();
            toolTip.SetToolTip(btnExportFiltered, "Экспортировать");
            toolTip.SetToolTip(btnResetFilters, "Сбросить фильтры");

            // Скрываем элементы по умолчанию
            btnResetFilters.Visible = false;
            btnExportFiltered.Visible = false;
            filteredLabel.Visible = false;
            SetActiveButton(btnAcitveInventory);

            btnResetFilters.Click += ResetFiltersButton_Click;
            btnExportFiltered.Click += BtnExportFiltered_Click; // Измененный обработчик

            // Убираем автоматическое отображение кнопок фильтрации
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

        public event EventHandler<string> FilterRequested;

        public void UpdateCounts(int totalCount, int? filtered = null)
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
        public string GetActiveFilterType()
        {
            return _activeButton switch
            {
                _ when _activeButton == btnMainMeans => "main",
                _ when _activeButton == btnNotMainMeans => "non_main",
                _ when _activeButton == btnInactiveInventory => "written_off",
                _ => "all"
            };
        }
        public void SetActiveButton(Guna2GradientButton button)
        {
            // Сбрасываем стиль предыдущей активной кнопки
            if (_activeButton != null)
            {
                _activeButton.FillColor = Color.WhiteSmoke;
                _activeButton.FillColor2 = Color.WhiteSmoke;
                _activeButton.ForeColor = Color.Black;
                _activeButton.BorderThickness = 0;
            }

            // Устанавливаем новый активный стиль
            button.FillColor = Color.LightCyan;
            button.FillColor2 = Color.LightCyan;
            button.ForeColor = Color.Black;
            button.BorderColor = Color.DarkSlateGray;
            button.BorderThickness = 1;

            _activeButton = button;
        }
        public void UpdateTotalLabel(int count)
        {
            totalLabel.Text = $"Всего в эксплуатации: {count}";
        }


        private void BtnExportFiltered_Click(object sender, EventArgs e)
        {
            ExportFilteredRequested?.Invoke(this, EventArgs.Empty);
        }

        private void ResetFiltersButton_Click(object sender, EventArgs e)
        {
            // Показываем totalLabel
            totalLabel.Visible = true;

            // Сбрасываем фильтры
            ResetFiltersRequested?.Invoke(this, EventArgs.Empty);
        }
        // В обработчиках кнопок:
        private void btnAcitveInventory_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnAcitveInventory);
            FilterRequested?.Invoke(this, "all");
        }

        private void btnMainMeans_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnMainMeans);
            FilterRequested?.Invoke(this, "main");
        }

        private void btnNotMainMeans_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnNotMainMeans);
            FilterRequested?.Invoke(this, "non_main");
        }

        private void btnInactiveInventory_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnInactiveInventory);
            FilterRequested?.Invoke(this, "written_off");
        }
        public void ResetToDefaultView()
        {
            SetActiveButton(btnAcitveInventory);
            totalLabel.Visible = true;
            filteredLabel.Visible = false;
            btnResetFilters.Visible = false;
            btnExportFiltered.Visible = false;
        }

    }
}