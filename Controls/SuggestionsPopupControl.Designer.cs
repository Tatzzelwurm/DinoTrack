namespace DinoTrack.Controls
{
    partial class SuggestionsPopupControl
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.SuspendLayout();


            // SuggestionsPopupControl
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.White;
            this.BorderStyle = BorderStyle.FixedSingle;
            this.Name = "SuggestionsPopupControl";
            this.Size = new Size(200, 150);
            this.ResumeLayout(false);
        }

        #endregion
    }
}
