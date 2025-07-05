namespace DinoTrack.Controls
{
    /// <summary>
    /// Ккастомизировання шапка приложения.
    /// Содержит кнопки управления окном (свернуть/развернуть/закрыть) и меню приложения.
    /// Реализует функционал перетаскивания окна.
    /// </summary>
    public partial class HeaderControl : UserControl
    {
        private bool _isDragging = false;
        private Point _startPoint;


        public HeaderControl() // Конструктор шапки, инициализирует компоненты и настройки
        {
            InitializeComponent();
            toolTip1.SetToolTip(BtnMenu, "Меню");
            SetupControls();
            UpdateMaximizeIcon();
            BtnMenu.Click += BtnMenu_Click;
        }

        private void BtnMenu_Click(object sender, EventArgs e) // Обрабатывает клик по кнопке меню, показывает контекстное меню
        {

            var mainForm = this.FindForm() as MainForm;

            if (mainForm?.AppMenu != null)
            {
                mainForm.AppMenu.Show(BtnMenu, new Point(0, BtnMenu.Height));
            }
        }
        public void ShowMenu(Control relativeTo) // Показывает меню приложения
        {
            var location = relativeTo.PointToScreen(new Point(0, relativeTo.Height));
            this.Location = relativeTo.Parent.PointToClient(location);
            this.Width = 200;
            this.BringToFront();
            this.Visible = true;
        }

        private void SetupControls() // Настраивает поведение элементов управления 
        {
            // Перетаскивание формы
            gradientPanel.MouseDown += (sender, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    _isDragging = true;
                    _startPoint = new Point(e.X, e.Y);
                }
            };

            gradientPanel.MouseMove += (sender, e) =>
            {
                if (_isDragging)
                {
                    Form form = this.FindForm();
                    if (form != null)
                    {
                        Point newPoint = form.PointToScreen(new Point(e.X, e.Y));
                        newPoint.Offset(-_startPoint.X, -_startPoint.Y);
                        form.Location = newPoint;
                    }
                }
            };

            gradientPanel.MouseUp += (sender, e) => _isDragging = false;

            // Двойной клик для максимизации
            this.MouseDoubleClick += (sender, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    ToggleMaximize();
                }
            };

            // Кнопка закрытия
            btnClose.Click += (sender, e) =>
            {
                var form = this.FindForm();
                if (form != null) form.Close();
            };

            // Кнопка сворачивания
            btnMinimize.Click += (sender, e) =>
            {
                var form = this.FindForm();
                if (form != null) form.WindowState = FormWindowState.Minimized;
            };

            // Кнопка максимизации
            btnMaximize.Click += (sender, e) => ToggleMaximize();

            // Кнопка "Меню" (три линии)
            BtnMenu.Text = string.Empty;
            BtnMenu.Image = CreateMenuIcon();
            BtnMenu.ImageOffset = new Point(0, -2); // Центрирование иконки

        }

        private void ToggleMaximize() // Переключает состояние окна между нормальным и максимизированным
        {
            var form = this.FindForm();
            if (form != null)
            {
                if (form.WindowState == FormWindowState.Maximized)
                {
                    form.WindowState = FormWindowState.Normal;
                    form.Size = new Size(1200, 700);
                }
                else
                {
                    form.MaximumSize = Screen.PrimaryScreen.WorkingArea.Size;
                    form.WindowState = FormWindowState.Maximized;
                }
                UpdateMaximizeIcon();
            }
        }

        private void UpdateMaximizeIcon() // Обновляет иконку кнопки максимизации в зависимости от состояния окна
        {
            var form = FindForm();
            btnMaximize.Text = form?.WindowState == FormWindowState.Maximized ? "🗗" : "🗖";
        }

        private Image CreateMenuIcon() // Создает изображение иконки меню (три линии)
        {
            Bitmap bmp = new Bitmap(20, 20);
            using (Graphics g = Graphics.FromImage(bmp))
            using (Pen pen = new Pen(Color.White, 2))
            {
                // Три горизонтальные линии
                g.DrawLine(pen, 4, 6, 16, 6);  // Верхняя
                g.DrawLine(pen, 4, 10, 16, 10); // Средняя
                g.DrawLine(pen, 4, 14, 16, 14); // Нижняя
            }
            return bmp;
        }
    }
}