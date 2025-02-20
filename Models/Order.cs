using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iFood.Models
{
	public class Order
	{
		[Key]
		public int Id { get; set; }
		public string Name { get; set; }
		[ForeignKey("AppUser")]
        public string AppUserId { get; set; }
		public AppUser AppUser { get; set; }
		public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
		public int Status { get; set; }
		public int Quantity { get; set; }
		public decimal Price { get; set; }
		public decimal Total => Quantity * Price;
	}
}