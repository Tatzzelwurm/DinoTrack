using DinoTrack.Controls;
using DinoTrack.Data.SchoolInventoryApp.Data;
using DinoTrack.Forms;
using DinoTrack.MenuApp;
using DinoTrack.Models;
using Guna.UI2.WinForms;

namespace DinoTrack
{
    /// <summary>
    /// ������� ����� ���������� - ����������� ��������� ������� ����� ��������� ���������.
    /// </summary>
    public partial class MainForm : Form
    {
        private ConnectionDatabase DB = new ConnectionDatabase();
        private SuggestionsPopupControl _suggestionsPopup;
        private List<Inventory> _allInventory = new List<Inventory>();
        private List<Inventory> _filteredInventory = new List<Inventory>();
        private List<Inventory> _lastFilteredItems;
        public Guna2ContextMenuStrip AppMenu => Menu;

        public MainForm() // ����������� ������� �����, �������������� ���������� � ������� ���������
        {
            InitializeComponent();
            InitializeDataGridView();
            InitializeSuggestionsPopup();
            InitializeEventHandlers();
            LoadInventoryFromDB();
            LoadInventoryFromDB();

            // ������������� ������� ���������� �����������
            DatabaseBackupAndRestore.Initialize(DB.connectionString);

            // �������� ���������������� �������� �� �������
            AllowColumnResizing = Properties.Settings.Default.AllowColumnResizing;
            AllowRowResizing = Properties.Settings.Default.AllowRowResizing;
            HighlightSelectedRow = Properties.Settings.Default.HighlightSelectedRow;

            // �������� �� ������� �������� ����� ��� ��������� ��������
            this.Load += MainForm_Load;
        }
        private void MainForm_Load(object sender, EventArgs e) // ����������� ������������� ����� ����������
        {
            this.Bounds = Screen.PrimaryScreen.WorkingArea;

        }

        private void InitializeDataGridView() // �������������� DataGridView (�������) � ����������� ������������ ������ � ��������
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

            // ������� ������������ �������
            InventoryTable.Columns.Clear();

            // ���������� �������
            InventoryTable.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "RowNumber",
                HeaderText = "�",
                FillWeight = 50,
                MinimumWidth = 20,

                SortMode = DataGridViewColumnSortMode.NotSortable
            });

            InventoryTable.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Name",
                HeaderText = "������������",
                FillWeight = 250,
                MinimumWidth = 200,
                SortMode = DataGridViewColumnSortMode.NotSortable
            });

            InventoryTable.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "InventoryNumber",
                HeaderText = "����������� �����",
                FillWeight = 100,
                MinimumWidth = 50,
                SortMode = DataGridViewColumnSortMode.NotSortable
            });

            InventoryTable.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "StartDate",
                HeaderText = "���� �����",
                FillWeight = 100,
                MinimumWidth = 50,
                SortMode = DataGridViewColumnSortMode.NotSortable
            });

            InventoryTable.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Price",
                HeaderText = "���������",
                FillWeight = 100,
                MinimumWidth = 50,
                SortMode = DataGridViewColumnSortMode.NotSortable,
                DefaultCellStyle = { Format = "N2" }
            });

            InventoryTable.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Location",
                HeaderText = "��������������",
                FillWeight = 100,
                MinimumWidth = 50,
                SortMode = DataGridViewColumnSortMode.NotSortable
            });

            // �������� �� �������
            InventoryTable.CellValueNeeded += DataGridView1_CellValueNeeded;
            InventoryTable.CellDoubleClick += DataGridView1_CellDoubleClick;
            InventoryTable.RowPrePaint += DataGridView1_RowPrePaint;
        }

        private void DataGridView1_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e) // ��������� DataGridView ������� � ����������� ������
        {
            if (e.RowIndex >= 0 && e.RowIndex < _filteredInventory.Count)
            {
                var item = _filteredInventory[e.RowIndex];
                var startDate = DB.GetInventoryStartDate(item.Id);

                switch (e.ColumnIndex)
                {
                    case 0: // � ���������� �����
                        e.Value = e.RowIndex + 1;
                        break;
                    case 1: // ������������
                        e.Value = item.Name;
                        break;
                    case 2: // ���. �
                        e.Value = item.InventoryNumber ?? "-";
                        break;
                    case 3: // ���� �����
                        e.Value = startDate?.ToString("dd.MM.yyyy") ?? "-";
                        break;
                    case 4: // ���������
                        e.Value = item.Price == 0 ? "-" : item.Price.ToString("N2");
                        break;
                    case 5: // ��������������
                        e.Value = item.LocationId.HasValue
                            ? DB.GetLocationNameById(item.LocationId.Value) ?? "-"
                            : "-";
                        break;
                }
            }
        }


        private void DataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e) // ��������� "�����" - ����������� ������ ����� �������
        {
            // ��������� �����
            if (e.RowIndex % 2 == 0)
            {
                InventoryTable.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            }
            else
            {
                InventoryTable.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
            }

        }

        private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e) // ������������ ������� ���� �� ������ - ��������� �������� ���������
        {
            if (e.RowIndex >= 0 && e.RowIndex < _filteredInventory.Count)
            {
                OpenInventoryDetails(_filteredInventory[e.RowIndex].Id);
            }
        }



        private void InitializeSuggestionsPopup() // ������������� ������������ ���� ��������� ��� ������
        {
            _suggestionsPopup = new SuggestionsPopupControl();
            this.Controls.Add(_suggestionsPopup);
            _suggestionsPopup.BringToFront();
            _suggestionsPopup.Visible = false;
            _suggestionsPopup.SuggestionSelected += OnSuggestionSelected;
        }


        private void InitializeEventHandlers() // ������������� �� ������� ��������� ����������
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

        private void OnSortingOptionSelected(object sender, string sortOption) // ������������ ����� ����� ����������
        {
            switch (sortOption)
            {
                case "default":
                    // ����� ����������
                    _filteredInventory = new List<Inventory>(_lastFilteredItems ?? _allInventory);
                    break;

                case "price_desc":
                    // ���������� �� �������� ����
                    _filteredInventory = _filteredInventory
                        .OrderByDescending(x => x.Price)
                        .ToList();
                    break;

                case "price_asc":
                    // ���������� �� �������� ����
                    _filteredInventory = _filteredInventory
                        .OrderBy(x => x.Price)
                        .ToList();
                    break;

                case "StartDate_asc":
                    // ���������� �� ���� �����
                    _filteredInventory = _filteredInventory
                        .OrderBy(x => DB.GetInventoryStartDate(x.Id) ?? DateTime.MinValue)
                        .ToList();
                    break;

                case "StartDate_desc":
                    // ���������� �� ���� �����
                    _filteredInventory = _filteredInventory
                        .OrderByDescending(x => DB.GetInventoryStartDate(x.Id) ?? DateTime.MinValue)
                        .ToList();
                    break;
            }

            InventoryTable.Invalidate();
        }

        private void ShowExportFormWithFiltered(object sender, EventArgs e) // ���������� ����� �������� � ������������������ �������������� �������
        {
            ShowExportForm(sender, e);
        }

        private void ShowExportForm(object sender, EventArgs e) // ���������� ����� "������ ��������"
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
                MessageBox.Show($"������ ��� ���������� ������ ��� ��������:\n{ex.Message}",
                    "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnSearchTextChanged(object sender, string searchText) // ������������ ��������� ������ � ���� ������ (���������� ���������)
        {
            if (string.IsNullOrEmpty(searchText))
            {
                _suggestionsPopup.Hide();
                return;
            }

            UpdateSuggestionsPosition();
            LoadSuggestions(searchText);
        }

        private void UpdateSuggestionsPosition() // ��������� ������� ����������� ��������� ��� ������
        {
            Point screenLocation = inventoryControlPanel1.SearchBox.PointToScreen(Point.Empty);
            Point formLocation = this.PointToClient(screenLocation);

            _suggestionsPopup.Location = new Point(
                formLocation.X,
                formLocation.Y + inventoryControlPanel1.SearchBox.Height);

            _suggestionsPopup.Width = inventoryControlPanel1.SearchBox.Width;
        }

        private void LoadSuggestions(string searchText) // ��������� ��������� ��� ������ �� ���� ������
        {
            var suggestions = DB.GetAllInventory()
                .Where(item => item.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                              (item.InventoryNumber != null &&
                               item.InventoryNumber.Contains(searchText, StringComparison.OrdinalIgnoreCase)))
                .Take(10)
                .Select(item => item.InventoryNumber != null
                    ? $"{item.InventoryNumber} - {item.Name}"
                    : item.Name) // ������ ��������, ���� ��� ������
                .ToList();

            _suggestionsPopup.SetSuggestions(suggestions);
            _suggestionsPopup.Visible = suggestions.Any();
        }

        private void OnSuggestionSelected(object sender, string selectedText) // ������������ ����� ��������� � ������
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


                    if (firstItem.StatusId != 1) // ���� ������ �� "��������"
                    {
                        infoPanel1.SetActiveButton(infoPanel1.btnInactiveInventory);
                    }
                    else if (firstItem.IsMain) // ���� ��� �������� ��������
                    {
                        infoPanel1.SetActiveButton(infoPanel1.btnMainMeans);
                    }
                    else // ���� ���������� ��������
                    {
                        infoPanel1.SetActiveButton(infoPanel1.btnNotMainMeans);
                    }
                }


                inventoryControlPanel1.ClearSearchBox();

                DisplayFilteredItems(filteredItems);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"������ ��� ��������� ������: {ex.Message}",
                              "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _suggestionsPopup.Hide();
            }
        }

        protected override void OnMouseDown(MouseEventArgs e) // �������� ��������� ��� ����� ��� ���� ������
        {
            base.OnMouseDown(e);

            if (_suggestionsPopup.Visible &&
                !inventoryControlPanel1.SearchBox.Bounds.Contains(e.Location) &&
                !_suggestionsPopup.Bounds.Contains(e.Location))
            {
                _suggestionsPopup.Hide();
            }

        }

        private void OnSearchRequested(object sender, string searchText) // ��������� ����� ��� ������� Enter � ���� ������
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



        private void OnFilterRequested(object sender, string filterType) // ������������ ������ ���������� �� InfoPanel
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

        private void ResetAllFilters(object sender, EventArgs e) // ���������� ��� ������� � ���������� ������ ������
        {
            _lastFilteredItems = null;
            LoadInventoryFromDB();
        }

        private void LoadInventoryFromDB() // ��������� ������ ��������� �� ���� ������
        {
            _allInventory = DB.GetAllInventory().Where(i => i.StatusId == 1).ToList();
            _filteredInventory = new List<Inventory>(_allInventory);

            // ��������� DataGridView
            InventoryTable.RowCount = _filteredInventory.Count;
            infoPanel1.UpdateCounts(_allInventory.Count);
            InventoryTable.Invalidate();
        }

        private void OpenInventoryDetails(int inventoryId) // ��������� "����������� ��������" ���������� ���������
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


        private void ResetToDefaultView() // ���������� ��� � ��������� "���� ���������" 
        {
            infoPanel1.SetActiveButton(infoPanel1.btnAcitveInventory);
            infoPanel1.totalLabel.Visible = true;
            infoPanel1.UpdateCounts(_allInventory.Count);
        }

        private void ShowFiltersForm(object sender, EventArgs e) // ���������� ����� �������
        {
            using (var filtersForm = new FiltersForm())
            {
                if (filtersForm.ShowDialog() == DialogResult.OK)
                {
                    _lastFilteredItems = FilterInventory(
                        filtersForm.SelectedSubCategories,
                        filtersForm.SelectedLocations);

                    DisplayFilteredItems(_lastFilteredItems, true);

                    // ��������� InfoPanel
                    infoPanel1.filteredLabel.Visible = true;
                    infoPanel1.btnResetFilters.Visible = true;
                    infoPanel1.btnExportFiltered.Visible = true;
                    infoPanel1.totalLabel.Visible = false; // �������� totalLabel, ���� �����
                    infoPanel1.filteredLabel.Text = $"��������������� ���������: {_lastFilteredItems.Count}";

                    // ������������� �������� ������ "���� ���������"
                    infoPanel1.SetActiveButton(infoPanel1.btnAcitveInventory);
                }
            }
        }

        private List<Inventory> FilterInventory(List<int> subCategories, List<int> locations) // ��������� ��������� �� ���������� � ���������������
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

        private void DisplayFilteredItems(List<Inventory> items, bool showFilterControls = false) // ���������� ��������������� �������� � �������
        {
            _filteredInventory = new List<Inventory>(items);
            InventoryTable.RowCount = _filteredInventory.Count;

            if (showFilterControls)
            {
                infoPanel1.UpdateCounts(items.Count, items.Count);
            }

            InventoryTable.Invalidate();
        }

        private void toolStripAboutApp_Click(object sender, EventArgs e) // ���������� ����� "� ���������"
        {
            MessageBox.Show(
       "�������������� ������� ����� ��������� ���������\n������ 2.0",
       "� ����������",
       MessageBoxButtons.OK,
       MessageBoxIcon.Information);
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            MenuApp.DatabaseBackupAndRestore.CreateBackup(); // ����� ������� "��������� �����������"
        }

        private void toolStripMenuItem10_Click(object sender, EventArgs e) // ����� ������� "�������������� ���� ������"
        {
            MenuApp.DatabaseBackupAndRestore.RestoreBackup();
        }

        private void toolStripSettings_Click(object sender, EventArgs e) // ��������� ����� "���������"
        {
            using (var settingsForm = new SettingsForm(this))
            {
                settingsForm.ShowDialog(this);
            }
        }
        /* ����������� ������ ���������� ������� "�������" */
        private void toolStripMenuItem1_Click(object sender, EventArgs e) => MenuApp.Reference.ShowSearchHelp();
        private void toolStripMenuItem2_Click(object sender, EventArgs e) => MenuApp.Reference.ShowSortingHelp();
        private void toolStripMenuItem4_Click(object sender, EventArgs e) => MenuApp.Reference.ShowExportHelp();
        private void toolStripMenuItem3_Click(object sender, EventArgs e) => MenuApp.Reference.ShowFiltersHelp();
        private void toolStripMenuItem7_Click(object sender, EventArgs e) => MenuApp.Reference.ShowDatabaseHelp();
        private void toolStripMenuItem5_Click(object sender, EventArgs e) => MenuApp.Reference.ShowAddInventoryHelp();
        private void toolStripMenuItem6_Click(object sender, EventArgs e) => MenuApp.Reference.ShowEditInventoryHelp();
        private void toolStripMenuItem8_Click(object sender, EventArgs e) => MenuApp.Reference.ShowSystemMigrationHelp();
        private void toolStripMenuItem11_Click(object sender, EventArgs e) => MenuApp.Reference.ShowTechnicalInfo();


        /* �������� �������� ���������� */
        public bool AllowColumnResizing // �������� ��� ���������� ������������ ��������� ������ ��������
        {
            get => InventoryTable.AllowUserToResizeColumns;
            set => InventoryTable.AllowUserToResizeColumns = value;
        }

        public bool AllowRowResizing // �������� ��� ���������� ������������ ��������� ������ �����
        {
            get => InventoryTable.AllowUserToResizeRows;
            set => InventoryTable.AllowUserToResizeRows = value;
        }

        public bool HighlightSelectedRow // �������� ��� ���������� ���������� ��������� ������
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




