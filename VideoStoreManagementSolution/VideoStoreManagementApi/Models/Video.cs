using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VideoStoreManagementApi.Models.Enums;

namespace VideoStoreManagementApi.Models
{
    public class Video
    {
        public int Id { get; set; }
        [Required]
        public string Tittle { get; set; }
        [Column(TypeName = "nvarchar(max)")]
        public string  Description { get; set; }
        [Required]
        public Genre Genre { get; set; }
        [Required]
        public string Director { get; set; }
        [Required]
        public DateTime ReleaseDate { get; set; }
        [Required]
        public float Price { get; set; }

    }
}
