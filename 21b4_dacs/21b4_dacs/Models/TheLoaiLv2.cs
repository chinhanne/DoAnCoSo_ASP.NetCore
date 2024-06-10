using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class TheLoaiLv2
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime DateUpdated { get; set; } = DateTime.Now;
        [Required]
        public int TheLoaiId { get; set; }
        [ForeignKey("TheLoaiId")]
        [ValidateNever]
        public TheLoai TheLoai { get; set; }
       
    }
}
