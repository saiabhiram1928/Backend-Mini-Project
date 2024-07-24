using GoogleMapsApi.StaticMaps.Entities;

namespace VideoStoreManagementApi.CustomAction
{
    public class Pagination<T>
    {
        public IList<T> Items { get; set; }
        public int TotalItems { get; set; }

    }
}
