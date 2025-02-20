namespace iFood.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class Review
{
    [Key]
    public int ReviewID { get; set; }

    public int? Rating { get; set; } // 1 - 5 stars
    public string? Comment { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    [ForeignKey("Product")]
    public int ProductID { get; set; }
    public Product? Product { get; set; }

    [ForeignKey("AppUser")]
    public string? AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
}
