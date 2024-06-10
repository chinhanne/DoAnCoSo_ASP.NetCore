using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
	public class SanPham
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
        [Range(0, double.MaxValue, ErrorMessage = "Giá không thể là số âm")]
        public double Price { get; set; }
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Giá không thể là số âm")]
        public double PriceSale { get; set; }
        public string? Decription { get; set; }
		public string? ImageUrl { get; set; }
        public string? ImageUrl1 { get; set; }
        public string? ImageUrl2 { get; set; }
        public string? ImageUrl3 { get; set; }
        public string? ImageUrl4 { get; set; }
        
        public string? Title { get; set; }
        public string Tacgia { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;

        public DateTime DateUpdated { get; set; } = DateTime.Now;

        [Required]
        public int TheLoaiLv2Id { get; set; }
        [ForeignKey("TheLoaiLv2Id")]
        [ValidateNever]
        public TheLoaiLv2 TheLoaiLv2 { get; set; }

    }
}