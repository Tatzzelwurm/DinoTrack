namespace DinoTrack.Controls
{
    /// <summary>
    /// Всплывающее окно подсказок для поиска.
    /// Отображает список подсказок при вводе текста в поле поиска.
    /// Позволяет выбрать подсказку двойным кликом.
    /// </summary>
    public partial class SuggestionsPopupControl : UserControl
    {
        public event EventHandler<string> SuggestionSelected; // Событие выбора подсказки
        private ListBox suggestionsListBox; // Элемент ListBox для отображения подсказок

        public SuggestionsPopupControl() // Конструктор, инициализирует компоненты и список подсказок
        {
            InitializeComponent();
            InitializeListBox();
            this.Visible = false;
        }

        private void InitializeListBox() // Инициализирует и настраивает ListBox для подсказок
        {
            suggestionsListBox = new ListBox
            {
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.None,
                Font = new Font("Segoe UI", 9F),
                BackColor = Color.White
            };

            // Обработка клика
            suggestionsListBox.MouseDoubleClick += (s, e) =>
            {
                if (suggestionsListBox.SelectedItem != null)
                {
                    SuggestionSelected?.Invoke(this, suggestionsListBox.SelectedItem.ToString());
                }
            };

            // Обработка нажатия Enter
            suggestionsListBox.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter && suggestionsListBox.SelectedItem != null)
                {
                    SuggestionSelected?.Invoke(this, suggestionsListBox.SelectedItem.ToString());
                    e.Handled = true;
                }
            };

            this.Controls.Add(suggestionsListBox);
        }


        public void SetSuggestions(List<string> suggestions) // Устанавливает список подсказок
        {
            suggestionsListBox.DataSource = suggestions;
        }

        public new void Hide() // Скрывает окно с подсказками
        {
            this.Visible = false;
        }
    }
}