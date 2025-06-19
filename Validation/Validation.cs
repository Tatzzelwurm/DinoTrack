using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DinoTrack.Validation
{
    internal static class Validation
    {
        /// <summary>
        /// Проверяет обязательные поля формы Add_Inventory
        /// </summary>
        /// <param name="nameInventory">Поле с наименованием инвентаря</param>
        /// <param name="subcategoryCombo">ComboBox с подкатегорией</param>
        /// <param name="typeCombo">ComboBox с типом инвентаря</param>
        /// <param name="countBox">Поле с количеством</param>
        /// <returns>True если все обязательные поля заполнены, иначе False</returns>
        public static bool ValidateAddInventoryFields(Control nameInventory, ComboBox subcategoryCombo,
                                                    ComboBox typeCombo, Control countBox)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(nameInventory.Text))
                errors.Add("Наименование инвентаря не может быть пустым");

            if (subcategoryCombo.SelectedIndex == -1 || string.IsNullOrWhiteSpace(subcategoryCombo.Text))
                errors.Add("Необходимо выбрать категорию");

            if (typeCombo.SelectedIndex == -1 || string.IsNullOrWhiteSpace(typeCombo.Text))
                errors.Add("Необходимо выбрать тип инвентаря");

            if (string.IsNullOrWhiteSpace(countBox.Text))
                errors.Add("Количество не может быть пустым");
            else if (!short.TryParse(countBox.Text, out _))
                errors.Add("Количество должно быть числом");

            if (errors.Any())
            {
                MessageBox.Show(string.Join("\n", errors), "Ошибка валидации",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }
        /// <summary>
        /// Проверяет обязательные поля формы InventoryDetalisAndEdit
        /// </summary>
        public static bool ValidateInventoryDetailsFields(
            Control nameInventory,
            Control subcategoryBox,
            Control typeBox,
            Control countBox)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(nameInventory.Text))
                errors.Add("Наименование инвентаря не может быть пустым");

            if (string.IsNullOrWhiteSpace(subcategoryBox.Text))
                errors.Add("Категория не может быть пустой");

            if (string.IsNullOrWhiteSpace(typeBox.Text))
                errors.Add("Тип инвентаря не может быть пустым");

            if (string.IsNullOrWhiteSpace(countBox.Text))
                errors.Add("Количество не может быть пустым");
            else if (!short.TryParse(countBox.Text, out _))
                errors.Add("Количество должно быть числом");

            if (errors.Any())
            {
                MessageBox.Show(string.Join("\n", errors), "Ошибка валидации",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Проверяет, что значение является положительным числом
        /// </summary>
        public static bool IsPositiveNumber(string value, out decimal result)
        {
            if (decimal.TryParse(value, out result) && result >= 0)
                return true;

            return false;
        }

        /// <summary>
        /// Проверяет, что значение является корректной датой
        /// </summary>
        public static bool IsValidDate(string value, out DateTime result)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                result = DateTime.MinValue;
                return true; // Пустая дата считается валидной
            }

            return DateTime.TryParse(value, out result);
        }

        /// <summary>
        /// Подсвечивает поле с ошибкой
        /// </summary>
        public static void HighlightErrorField(Control control, bool isValid)
        {
            if (control is Guna.UI2.WinForms.Guna2TextBox textBox)
            {
                textBox.BorderColor = isValid ? Color.FromArgb(193, 200, 207)
                                            : Color.Red;
            }
            // Можно добавить обработку других типов контролов при необходимости
        }
    }
}