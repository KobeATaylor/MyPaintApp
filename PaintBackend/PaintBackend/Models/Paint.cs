using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PaintBackend.Models
{
    [Table("Paint")]
    public class Paint
    {
        [Key]
        public int PaintId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? PaintingUrl { get; set; }
    }
}