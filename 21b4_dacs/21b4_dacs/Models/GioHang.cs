using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class GioHang
    {
        [Key]
        public int Id { get; set; }
        public int SanPhamId { get; set; }
        [ForeignKey("SanPhamId")]
        [ValidateNever]
        public SanPham SanPham { get; set; }
        public int Quantity { get; set; }
        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }
        [NotMapped]
        public double ProductPrice { get; set; }
    }
}
