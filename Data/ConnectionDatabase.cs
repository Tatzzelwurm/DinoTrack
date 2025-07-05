using DinoTrack.Models;
using Npgsql;
using System.Data;
using System.Text;

namespace DinoTrack.Data
{
    namespace SchoolInventoryApp.Data
    {
        /// <summary>
        /// Класс для работы с базой данных PostgreSQL.
        /// Предоставляет методы для выполнения запросов и управления инвентарем.
        /// Обрабатывает подключение, выполнение SQL-команд и преобразование результатов.
        /// </summary>
        internal class ConnectionDatabase
        {
            // Строка подключения к базе данных
            internal readonly string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=17;Database=School_Inventory";

            // Метод для получения подключения к базе данных
            public NpgsqlConnection GetConnection()
            {
                var connection = new NpgsqlConnection(connectionString);
                connection.Open();
                return connection;
            }

            // Метод для выполнения SQL-запроса и возврата результата в виде NpgsqlDataReader
            public NpgsqlDataReader ExecuteReader(string query)
            {
                try
                {
                    using (var conn = GetConnection())
                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        return cmd.ExecuteReader();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }

            // Метод для выполнения SQL-запроса без возврата результата (INSERT, UPDATE, DELETE)
            public void ExecuteQuery(string query)
            {
                try
                {
                    using (var conn = GetConnection())
                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            public string GetCategoryNameById(int subCategoryId) // Получает название категории по ID
            {
                try
                {
                    using (var conn = GetConnection())
                    using (var cmd = new NpgsqlCommand(@"
            SELECT sc.name 
            FROM inventory17.SubCategory sc
            WHERE sc.id = @id", conn))
                    {
                        cmd.Parameters.AddWithValue("@id", subCategoryId);
                        var result = cmd.ExecuteScalar();
                        return result?.ToString();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при получении названия категории: {ex.Message}");
                    return null;
                }
            }

            public string GetLocationNameById(int locationId) // Получает название местоположения по ID
            {
                try
                {
                    using (var conn = GetConnection())
                    using (var cmd = new NpgsqlCommand("SELECT name FROM inventory17.Location WHERE id = @id", conn))
                    {
                        cmd.Parameters.AddWithValue("@id", locationId);
                        var result = cmd.ExecuteScalar();
                        return result?.ToString();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при получении местоположения: {ex.Message}");
                    return null;
                }
            }
            public string GetTypeNameById(int typeId) // Получает название типа инвентаря по ID
            {
                try
                {
                    using (var conn = GetConnection())
                    using (var cmd = new NpgsqlCommand(
                        "SELECT name FROM inventory17.Type WHERE id = @id", conn))
                    {
                        cmd.Parameters.AddWithValue("@id", typeId);
                        return cmd.ExecuteScalar()?.ToString() ?? "Не указан";
                    }
                }
                catch
                {
                    return "Не указан";
                }
            }

            public DataTable GetInventoryHistory(int itemId) // Получает историю перемещений для указанного инвентаря
            {
                var table = new DataTable();

                try
                {
                    using (var conn = GetConnection())
                    {
                        using (var cmd = new NpgsqlCommand(@"
                            SELECT 
                    ROW_NUMBER() OVER (ORDER BY l.date_of_move DESC) as row_num,
                    l.id as log_id,
                    old_loc.name as old_location_name,
                    new_loc.name as new_location_name,
                    l.date_of_move,
                    l.comments
                FROM 
                    inventory17.Logs l
                LEFT JOIN 
                    inventory17.Location old_loc ON l.old_location_id = old_loc.id
                LEFT JOIN 
                    inventory17.Location new_loc ON l.new_location_id = new_loc.id
                WHERE 
                    l.item_id = @item_id
                ORDER BY 
                    l.date_of_move DESC", conn))
                        {
                            cmd.Parameters.AddWithValue("@item_id", itemId);

                            using (var adapter = new NpgsqlDataAdapter(cmd))
                            {
                                adapter.Fill(table);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при загрузке истории: {ex.Message}");
                    throw;
                }

                return table;
            }
            public void LogLocationChange(int itemId, int? oldLocationId, int? newLocationId) // Логирует изменение местоположения

            {
                try
                {
                    using (var conn = GetConnection())
                    using (var cmd = new NpgsqlCommand(@"
            INSERT INTO inventory17.Logs (
                item_id, 
                old_location_id, 
                new_location_id
            ) VALUES (
                @item_id,
                @old_loc,
                @new_loc
            )", conn))
                    {
                        cmd.Parameters.AddWithValue("@item_id", itemId);

                        // Обрабатываем NULL для old_location_id
                        if (oldLocationId.HasValue && oldLocationId.Value > 0)
                        {
                            cmd.Parameters.AddWithValue("@old_loc", oldLocationId.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@old_loc", DBNull.Value);
                        }

                        // Обрабатываем NULL для new_location_id
                        if (newLocationId.HasValue && newLocationId.Value > 0)
                        {
                            cmd.Parameters.AddWithValue("@new_loc", newLocationId.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@new_loc", DBNull.Value);
                        }

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при логировании перемещения: {ex.Message}");
                    throw;
                }
            }



            public List<Inventory> GetAllInventory() // Метод для получения всех записей инвентаря из базы данных
            {
                var inventoryList = new List<Inventory>();

                try
                {
                    using (var conn = GetConnection())
                    using (var cmd = new NpgsqlCommand(@"
            SELECT i.id, i.inventory_number, i.serial_number, i.name, 
                   i.description, i.price, i.comments,
                   i.main_category_id, i.sub_category_id, 
                   i.location_id, i.status_id, i.type_id, i.count
            FROM inventory17.Inventory i", conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var inventory = new Inventory
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("id")),
                                InventoryNumber = reader.IsDBNull(reader.GetOrdinal("inventory_number")) ?
                                 null : reader.GetString(reader.GetOrdinal("inventory_number")),
                                SerialNumber = reader.IsDBNull(reader.GetOrdinal("serial_number")) ?
                                 null : reader.GetString(reader.GetOrdinal("serial_number")),
                                Name = reader.GetString(reader.GetOrdinal("name")),
                                Description = reader.IsDBNull(reader.GetOrdinal("description")) ?
                                             string.Empty : reader.GetString(reader.GetOrdinal("description")),
                                Price = reader.IsDBNull(reader.GetOrdinal("price")) ?
                                    0 : reader.GetDecimal(reader.GetOrdinal("price")),
                                Comments = reader.IsDBNull(reader.GetOrdinal("comments")) ?
                                          string.Empty : reader.GetString(reader.GetOrdinal("comments")),
                                MainCategoryId = reader.GetInt32(reader.GetOrdinal("main_category_id")),
                                SubCategoryId = reader.GetInt32(reader.GetOrdinal("sub_category_id")),
                                LocationId = reader.IsDBNull(reader.GetOrdinal("location_id")) ?
                                            0 : reader.GetInt32(reader.GetOrdinal("location_id")),
                                StatusId = reader.GetInt32(reader.GetOrdinal("status_id")),
                                TypeId = reader.GetInt32(reader.GetOrdinal("type_id")),
                                Count = reader.GetInt32(reader.GetOrdinal("count")),
                            };

                            inventoryList.Add(inventory);
                        }
                    }
                }
                catch (Exception ex)
                {

                }

                return inventoryList;
            }


            public Inventory GetInventoryById(int id) // Метод для получения записи инвентаря по ID
            {
                try
                {
                    using (var conn = GetConnection())
                    using (var cmd = new NpgsqlCommand(@"
            SELECT i.*, t.name as type_name 
            FROM inventory17.Inventory i
            LEFT JOIN inventory17.Type t ON i.type_id = t.id
            WHERE i.id = @id", conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Inventory
                                {
                                    Id = reader.GetInt32("id"),
                                    InventoryNumber = reader.IsDBNull("inventory_number") ? null : reader.GetString("inventory_number"),
                                    SerialNumber = reader.IsDBNull("serial_number") ? null : reader.GetString("serial_number"),
                                    Name = reader.GetString("name"),
                                    Description = reader.IsDBNull("description") ? null : reader.GetString("description"),
                                    Price = reader.IsDBNull("price") ? 0 : reader.GetDecimal("price"), // Или decimal?
                                    Comments = reader.IsDBNull("comments") ? null : reader.GetString("comments"),
                                    MainCategoryId = reader.GetInt32("main_category_id"),
                                    SubCategoryId = reader.GetInt32("sub_category_id"),
                                    LocationId = reader.IsDBNull("location_id") ? (int?)null : reader.GetInt32("location_id"), // NULLable
                                    StatusId = reader.GetInt32("status_id"),
                                    TypeId = reader.GetInt32("type_id"),
                                    Count = reader.GetInt32("count"),
                                    CreatedAt = reader.GetDateTime("created_at"),
                                    UpdatedAt = reader.GetDateTime("updated_at"),
                                    TypeName = reader.IsDBNull("type_name") ? "Не указан" : reader.GetString("type_name")
                                };
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки данных: {ex.Message}");
                    return null;
                }
                return null;
            }
            public DateTime? GetInventoryStartDate(int inventoryId) // Получает дату ввода в эксплуатации инвентаря
            {
                try
                {
                    using (var conn = GetConnection())
                    using (var cmd = new NpgsqlCommand(
                        "SELECT start_date FROM inventory17.Lifetime WHERE inventory_id = @id ORDER BY start_date LIMIT 1",
                        conn))
                    {
                        cmd.Parameters.AddWithValue("@id", inventoryId);
                        var result = cmd.ExecuteScalar();
                        return result != DBNull.Value ? (DateTime?)result : null;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при получении даты начала эксплуатации: {ex.Message}");
                    return null;
                }
            }

            public bool WriteOffInventory(int inventoryId) // Помечает инвентарь как списанный
            {
                try
                {
                    using (var conn = GetConnection())
                    using (var cmd = new NpgsqlCommand(
                        "UPDATE inventory17.Inventory SET status_id = 2 WHERE id = @id", conn))
                    {
                        cmd.Parameters.AddWithValue("@id", inventoryId);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при списании инвентаря: {ex.Message}");
                    return false;
                }
            }

            public bool RestoreInventory(int inventoryId) // Восстанавливает инвентарь (отменяет списание)
            {
                try
                {
                    using (var conn = GetConnection())
                    using (var cmd = new NpgsqlCommand(
                        "UPDATE inventory17.Inventory SET status_id = 1 WHERE id = @id", conn))
                    {
                        cmd.Parameters.AddWithValue("@id", inventoryId);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при восстановлении инвентаря: {ex.Message}");
                    return false;
                }
            }


            public bool DeleteInventory(int inventoryId) // Полностью удаляет инвентарь из БД
            {
                try
                {
                    using (var conn = GetConnection())
                    using (var cmd = new NpgsqlCommand(
                        "DELETE FROM inventory17.Inventory WHERE id = @id", conn))
                    {
                        cmd.Parameters.AddWithValue("@id", inventoryId);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при удалении инвентаря: {ex.Message}");
                    return false;
                }
            }

            // Для основных средств (type_id = 1)
            public List<Inventory> GetMainInventory()
            {
                return GetAllInventory().Where(i => i.TypeId == 1 && i.StatusId == 1).ToList();
            }

            // Для неосновных средств (type_id = 2)
            public List<Inventory> GetNonMainInventory()
            {
                return GetAllInventory().Where(i => i.TypeId == 2 && i.StatusId == 1).ToList();
            }

            // Для списанного инвентаря (status_id = 2)
            public List<Inventory> GetWrittenOffInventory()
            {
                return GetAllInventory().Where(i => i.StatusId == 2).ToList();
            }

        }
    }
}


