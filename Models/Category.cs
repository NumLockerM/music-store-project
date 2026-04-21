using System.ComponentModel.DataAnnotations;

namespace MusicStore.Models;

public class Category
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Введите название категории")]
    public string Name { get; set; } = "";

    public List<Product> Products { get; set; } = new();
}