using System.ComponentModel.DataAnnotations;

namespace MusicStore.Models;

public class Product
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Укажите название")]
    public string Name { get; set; } = "";

    public string Description { get; set; } = "";

    [Range(1, 1000000)]
    public decimal Price { get; set; }

    public string ImageUrl { get; set; } = "/images/default.jpg";

    // НОВОЕ ПОЛЕ: Склад
    [Display(Name = "На складе")]
    [Range(0, 1000)]
    public int StockQuantity { get; set; } = 0;

    public int CategoryId { get; set; }
    public Category? Category { get; set; }
}