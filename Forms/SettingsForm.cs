namespace DinoTrack.Forms
{
    /// <summary>
    ///  Форма настроек приложения с переключателями визуальных параметров
    /// </summary>
    public partial class SettingsForm : Form
    {
        // Ссылка на главную форму
        private MainForm _mainForm;

        public SettingsForm(MainForm mainForm) // Конструктор - принимает главную форму для управления настройками
        {
            InitializeComponent();
            _mainForm = mainForm;


            LoadSettings();


            // Подписка на события изменения переключателей
            ToggleSwitch.CheckedChanged += ToggleSwitch_CheckedChanged;
            ToggleSwitch1.CheckedChanged += ToggleSwitch1_CheckedChanged;
            ToggleSwitch2.CheckedChanged += ToggleSwitch2_CheckedChanged;
            ToggleSwitch4.CheckedChanged += ToggleSwitch4_CheckedChanged;


        }

        private void LoadSettings() // Загружает текущие настройки из главной формы и конфига
        {

            ToggleSwitch.Checked = _mainForm.AllowColumnResizing;
            ToggleSwitch1.Checked = _mainForm.AllowRowResizing;
            ToggleSwitch2.Checked = _mainForm.HighlightSelectedRow;
            ToggleSwitch4.Checked = Properties.Settings.Default.ShowTooltipsInAddForm;
        }

        private void ToggleSwitch_CheckedChanged(object sender, EventArgs e) // Обработчик переключателя "Изменение ширины столбцов"
        {
            _mainForm.AllowColumnResizing = ToggleSwitch.Checked;
        }

        private void ToggleSwitch1_CheckedChanged(object sender, EventArgs e) // Обработчик переключателя "Изменение высоты строк"
        {
            _mainForm.AllowRowResizing = ToggleSwitch1.Checked;
        }

        private void ToggleSwitch2_CheckedChanged(object sender, EventArgs e) // Обработчик переключателя "Подсветка выбранной строки"
        {
            _mainForm.HighlightSelectedRow = ToggleSwitch2.Checked;

        }
        private void ToggleSwitch4_CheckedChanged(object sender, EventArgs e) // Обработчик переключателя "Показывать подсказки в форме добавления"
        {
            Properties.Settings.Default.ShowTooltipsInAddForm = ToggleSwitch4.Checked;
            Properties.Settings.Default.Save();
        }

        protected override void OnFormClosing(FormClosingEventArgs e) // Сохраняет настройки при закрытии формы
        {
            base.OnFormClosing(e);
            Properties.Settings.Default.AllowColumnResizing = ToggleSwitch.Checked;
            Properties.Settings.Default.AllowRowResizing = ToggleSwitch1.Checked;
            Properties.Settings.Default.HighlightSelectedRow = ToggleSwitch2.Checked;
            Properties.Settings.Default.Save();
        }
    }
}