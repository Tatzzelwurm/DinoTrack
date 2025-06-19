using System.Windows.Forms;
using System.Drawing;
using Guna.UI2.WinForms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DinoTrack.Controls
{
    public partial class HeaderControl : UserControl
    {
        private bool _isDragging = false;
        private Point _startPoint;
        private Guna2ContextMenuStrip _menu;
        // Заменим все упоминания dropdownMenu на dropdownMenuControl
        public HeaderControl()
        {
            InitializeComponent();
            toolTip1.SetToolTip(BtnMenu, "Меню");
            SetupControls();
            UpdateMaximizeIcon();
            BtnMenu.Click += BtnMenu_Click;
            
            // SetupDropdownMenu();

        }

        private void BtnMenu_Click(object sender, EventArgs e)
        {
            // Получаем главную форму
            var mainForm = this.FindForm() as MainForm;

            if (mainForm?.AppMenu != null)
            {
                // Показываем меню под кнопкой
                mainForm.AppMenu.Show(BtnMenu, new Point(0, BtnMenu.Height));
            }
        }
        public void ShowMenu(Control relativeTo)
        {
            var location = relativeTo.PointToScreen(new Point(0, relativeTo.Height));
            this.Location = relativeTo.Parent.PointToClient(location);
            this.Width = 200; // Явно задаем ширину
            this.BringToFront();
            this.Visible = true;

            // Временная отладочная информация
            MessageBox.Show($"Menu shown at {this.Location}\nParent: {this.Parent.Name}");
        }


        private void SetupControls()
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

            // Кнопка "Прочее" (три линии)
            BtnMenu.Text = string.Empty;
            BtnMenu.Image = CreateMenuIcon();
            BtnMenu.ImageOffset = new Point(0, -2); // Центрирование иконки

        }

        private void ToggleMaximize()
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

        private void UpdateMaximizeIcon()
        {
            var form = FindForm();
            btnMaximize.Text = form?.WindowState == FormWindowState.Maximized ? "🗗" : "🗖";
        }

        private Image CreateMenuIcon()
        {
            // Создаем изображение с тремя линиями (меню)
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

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripTextBox2_Click(object sender, EventArgs e)
        {

        }

        private void HeaderControl_Load(object sender, EventArgs e)
        {

        }
    }
}