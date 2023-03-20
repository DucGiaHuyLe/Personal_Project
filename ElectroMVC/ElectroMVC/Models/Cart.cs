using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectroMVC.Models
{
	public class Cart
	{
		[Key]
		public int CartId { get; set; }
		[ForeignKey("ProductId")]
        public int ProductId { get; set; }
        public Product? Product { get; set; }
		[ForeignKey("UserId")]
		public int UserId { get; set; }
		public User? User { get; set; }
		public int Quantity { get; set; }
	}
}
