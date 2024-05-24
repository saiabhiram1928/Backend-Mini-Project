using VideoStoreManagementApi.Models.Enums;

namespace VideoStoreManagementApi.Models
{
    public class Video
    {
        public int Id { get; set; } 
        public string Tittle { get; set; }
        public Genre Genre { get; set; }
        public string Director { get; set; }
        public DateTime ReleaseDate { get; set; }
        public float Price { get; set; }

    }
}
