namespace DinoTrack.Models
{
    /// <summary>
    ///  Модель инвентарного объекта системы
    /// </summary>
    public class Inventory
    {
        // Основные свойства 
        public int Id { get; set; }
        public string? InventoryNumber { get; set; }
        public string? SerialNumber { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? Comments { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int Count { get; set; }

        // Внешние ключи
        public int MainCategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public int? LocationId { get; set; }
        public int StatusId { get; set; }
        public int TypeId { get; set; }

        // Даты жизненного цикла инвентаря
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        // Навигационные свойства для отображения
        public string MainCategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public string LocationName { get; set; }
        public string StatusName { get; set; }
        public string TypeName { get; set; }

        // Вычисляемые свойства
        public bool IsMain => TypeId == 1;
    }

}