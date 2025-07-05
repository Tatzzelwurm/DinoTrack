using Npgsql;
using System.Diagnostics;

namespace DinoTrack.MenuApp
{
    /// <summary>
    ///  Класс для создания и восстановления резервных копий PostgreSQL
    /// </summary>
    public static class DatabaseBackupAndRestore
    {
        private static string _connectionString;

        public static void Initialize(string connectionString) // Инициализирует строку подключения для работы с БД
        {
            _connectionString = connectionString;
        }

        public static void CreateBackup() // Создает резервную копию через pg_dump с диалогом выбора пути сохранения
        {
            if (string.IsNullOrEmpty(_connectionString))
                throw new InvalidOperationException("Строка подключения не инициализирована.");

            using (var dialog = new SaveFileDialog())
            {
                dialog.Filter = "Backup files (*.backup)|*.backup|All files (*.*)|*.*";
                dialog.FileName = $"backup_DinoTrack_{DateTime.Now:yyyyMMdd_HHmm}.backup";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    PerformBackup(dialog.FileName);
                }
            }
        }

        public static void RestoreBackup() // Восстанавливает БД из backup-файла с подтверждением операции
        {
            if (string.IsNullOrEmpty(_connectionString))
                throw new InvalidOperationException("Строка подключения не инициализирована.");

            using (var openDialog = new OpenFileDialog())
            {
                openDialog.Filter = "Backup files (*.backup)|*.backup|All files (*.*)|*.*";
                openDialog.Title = "Выберите файл резервной копии для восстановления";

                if (openDialog.ShowDialog() == DialogResult.OK)
                {
                    if (ConfirmRestore())
                    {
                        PerformRestore(openDialog.FileName);
                    }
                }
            }
        }

        private static bool ConfirmRestore() // Запрашивает подтверждение восстановления
        {
            return MessageBox.Show(
                "Внимание! Восстановление перезапишет текущую базу данных.\n" +
                "Убедитесь, что вы выбрали правильную резервную копию.\n\n" +
                "Продолжить?",
                "Подтверждение восстановления",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning) == DialogResult.Yes;
        }

        private static void PerformBackup(string backupPath)
        {
            string pgDumpPath = @"C:\Program Files\PostgreSQL\17\bin\pg_dump.exe";
            var builder = new NpgsqlConnectionStringBuilder(_connectionString);

            try
            {
                using (var process = new Process())
                {
                    process.StartInfo = new ProcessStartInfo
                    {
                        FileName = pgDumpPath,
                        Arguments = $"-h {builder.Host} -p {builder.Port} -U {builder.Username} -d {builder.Database} " +
                                   $"-Fc -Z 5 -v -f \"{backupPath}\"",
                        UseShellExecute = false,
                        RedirectStandardError = true,
                        CreateNoWindow = true,
                        EnvironmentVariables = { ["PGPASSWORD"] = builder.Password }
                    };

                    process.Start();
                    string errors = process.StandardError.ReadToEnd();
                    process.WaitForExit();

                    if (process.ExitCode == 0)
                    {
                        MessageBox.Show("Резервное копирование успешно создано!", "Успех",
                                       MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show($"Ошибка при создании резервной копии:\n{errors}", "Ошибка",
                                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Неожиданная ошибка:\n{ex.Message}", "Ошибка",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void PerformRestore(string backupPath)
        {
            string pgRestorePath = @"C:\Program Files\PostgreSQL\17\bin\pg_restore.exe";
            var builder = new NpgsqlConnectionStringBuilder(_connectionString);

            try
            {
                using (var process = new Process())
                {
                    process.StartInfo = new ProcessStartInfo
                    {
                        FileName = pgRestorePath,
                        Arguments = $"-h {builder.Host} -p {builder.Port} -U {builder.Username} -d {builder.Database} " +
                                   $"-c -v -Fc \"{backupPath}\"",
                        UseShellExecute = false,
                        RedirectStandardError = true,
                        CreateNoWindow = true,
                        EnvironmentVariables = { ["PGPASSWORD"] = builder.Password }
                    };

                    process.Start();
                    string errors = process.StandardError.ReadToEnd();
                    process.WaitForExit();

                    if (process.ExitCode == 0)
                    {
                        MessageBox.Show("База данных успешно восстановлена!\nПриложение будет перезапущено.",
                                      "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Перезапуск приложения через 1 секунду
                        Task.Delay(1000).ContinueWith(_ =>
                        {
                            Application.Restart();
                            Environment.Exit(0);
                        });
                    }
                    else
                    {
                        MessageBox.Show($"База данных успешно восстановлена. Перезапустите приложение", "Успех",
                                       MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Неожиданная ошибка:\n{ex.Message}", "Ошибка",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
    }

}

