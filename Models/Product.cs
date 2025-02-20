using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using iFood.Data.Enum;

namespace iFood.Models;

public class Product
{
    [Key]
    public int ProductID { get; set; }

    [Required(ErrorMessage = "Yêu cầu nhập tên sản phẩm")]
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int? SoldOut { get; set; }

    [Required(ErrorMessage = "Yêu cầu nhập giá sản phẩm")]
    public decimal Price { get; set; }
    // Purchase Count update database
    // List rate, rate
    
    public int Quantity { get; set; }
    public string? ImageURL { get; set; }
    public ProductCategory? Category { get; set; }
    public DateTime DateSell { get ; set; }
    public decimal Total => Quantity * Price;

    // Khóa ngoại: Người bán
    [ForeignKey("AppUser")]
    public string? AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
}
