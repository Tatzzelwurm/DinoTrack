using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace DinoTrack.Controls
{
    public partial class SuggestionsPopupControl : UserControl
    {
        public event EventHandler<string> SuggestionSelected;
        private ListBox suggestionsListBox;

        public SuggestionsPopupControl()
        {
            InitializeComponent();
            InitializeListBox();
            this.Visible = false;
        }

        private void InitializeListBox()
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


        public void SetSuggestions(List<string> suggestions)
        {
            suggestionsListBox.DataSource = suggestions;
        }

        public new void Hide()
        {
            this.Visible = false;
        }
    }
}