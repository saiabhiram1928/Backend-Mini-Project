using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace VideoStoreManagementApi.Models
{
    public class Rental
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        [ForeignKey(nameof(OrderId))]
        [JsonIgnore]
        public Order Order { get; set; }
        public DateTime RentDate { get; set; }
        private DateTime _dueDate;
        private DateTime _returnDate;
        public DateTime DueDate
        {
            get => _dueDate;
            set
            {
                if (value <= RentDate)
                    throw new ArgumentException("DueDate must be greater than RentDate");
                _dueDate = value;
            }
        }

        
        public DateTime ReturnDate
        {
            get => _returnDate;
            set
            {
                if (value <= RentDate)
                    throw new ArgumentException("ReturnDate must be greater than RentDate");
                _returnDate = value;
            }
        }

        public int TotalQty { get; set; }
        public float TotalPrice { get; set; }   
        public float LateFee { get; set; }  
    }
}
