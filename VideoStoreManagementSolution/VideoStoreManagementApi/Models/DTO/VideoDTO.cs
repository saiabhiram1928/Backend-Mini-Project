using System.ComponentModel.DataAnnotations.Schema;
using VideoStoreManagementApi.Models.Enums;

namespace VideoStoreManagementApi.Models.DTO
{
    public class VideoDTO
    {
        public int Id { get; set; }
        public string Tittle { get; set; }
        [Column(TypeName = "nvarchar(max)")]
        public string Description { get; set; }
        public Genre Genre { get; set; }
        public string Director { get; set; }
        public DateTime ReleaseDate { get; set; }
        public float Price { get; set; }
        public int Stock { get; set; }
    }
}
