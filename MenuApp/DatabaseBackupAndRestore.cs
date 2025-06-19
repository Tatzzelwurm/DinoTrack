using Npgsql;
using DinoTrack.Data.SchoolInventoryApp.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            private static void PerformBackup(string backupPath) // Выполняет backup через вызов pg_dump.exe
        {
                string pgDumpPath = @"C:\Program Files\PostgreSQL\17\bin\pg_dump.exe";
                var builder = new NpgsqlConnectionStringBuilder(_connectionString);

                var psi = new ProcessStartInfo
                {
                    FileName = pgDumpPath,
                    Arguments = $"-h {builder.Host} -p {builder.Port} -U {builder.Username} -d {builder.Database} " +
                               $"-Fc -Z 5 -v -f \"{backupPath}\"",
                    UseShellExecute = false,
                    RedirectStandardError = true,
                    CreateNoWindow = true,
                    EnvironmentVariables = { ["PGPASSWORD"] = builder.Password }
                };

            ExecuteProcess(psi, "Резервное копирование успешно создано!",
              "Ошибка при создании резервной копии",
              restartAfter: false); 
        }

            private static void PerformRestore(string backupPath) // Выполняет restore через вызов pg_restore.exe
        {
                string pgRestorePath = @"C:\Program Files\PostgreSQL\17\bin\pg_restore.exe";
                var builder = new NpgsqlConnectionStringBuilder(_connectionString);

                var psi = new ProcessStartInfo
                {
                    FileName = pgRestorePath,
                    Arguments = $"-h {builder.Host} -p {builder.Port} -U {builder.Username} -d {builder.Database} " +
                               $"-c -v -Fc \"{backupPath}\"",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true,
                    EnvironmentVariables = { ["PGPASSWORD"] = builder.Password }
                };

            ExecuteProcess(psi, "База данных успешно восстановлена!\nПриложение будет перезапущено.",
             "Ошибка при восстановлении базы данных",
             restartAfter: true); // Перезапуск только после восстановления
        }

        private static void ExecuteProcess(ProcessStartInfo psi, string successMessage, string errorMessage, bool restartAfter = false) // Универсальный метод выполнения консольных команд
        {
            try
            {
                using (var process = new Process { StartInfo = psi })
                {
                    process.Start();

                    // Асинхронное чтение вывода, чтобы избежать deadlock
                    string errors = process.StandardError.ReadToEnd();

                    // Ожидание завершения с таймаутом (30 секунд)
                    if (!process.WaitForExit(30000))
                    {
                        process.Kill();
                        MessageBox.Show("Процесс выполнения превысил время ожидания", "Ошибка",
                                      MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (process.ExitCode == 0)
                    {
                        MessageBox.Show(successMessage, "Успех",
                                      MessageBoxButtons.OK, MessageBoxIcon.Information);

                        if (restartAfter)
                        {
                            // Запуск перезапуска через 1 секунду
                            Task.Delay(1000).ContinueWith(_ =>
                            {
                                Application.Restart();
                                Environment.Exit(0);
                            });
                        }
                    }
                    else
                    {
                        MessageBox.Show($"{errorMessage}:\n{errors}", "Ошибка",
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
    }

    }

