using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using iFood.Data.Enum;

namespace iFood.Models
{
	public class Order
	{
		[Key]
		public int OrderId { get; set; }

		public string UserId { get; set; } // Ai đặt hàng

		public decimal TotalPrice { get; set; }
		public DateTime OrderDate { get; set; }

		public List<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

		public MomoInfo MomoInfo { get; set; } // Thanh toán Momo
	}
}