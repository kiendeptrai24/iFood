using System.ComponentModel.DataAnnotations;

namespace iFood.Models
{
    public class OrderInfo
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; }
        public string OrderId { get; set; }
        public string OrderInfomation { get; set; }
        public double Amount { get; set; }
        public DateTime DatePaid { get; set; }
    }
}