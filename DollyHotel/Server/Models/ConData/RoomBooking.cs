using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DollyHotel.Server.Models.ConData
{
    [Table("RoomBookings", Schema = "dbo")]
    public partial class RoomBooking
    {

        

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long BookingID { get; set; }

        [Required]
        [ConcurrencyCheck]
        public DateTime BookingDate { get; set; }

        [Required]
        [ConcurrencyCheck]
        public string MadeBy { get; set; }

        [Required]
        [ConcurrencyCheck]
        public long HotelID { get; set; }

        [Required]
        [ConcurrencyCheck]
        public int NumberOfAdults { get; set; }

        [Required]
        [ConcurrencyCheck]
        public int NumberOfChildren { get; set; }

        [Required]
        [ConcurrencyCheck]
        public DateTime CheckInDate { get; set; }

        [Required]
        [ConcurrencyCheck]
        public DateTime CheckOutDate { get; set; }

        [Required]
        [ConcurrencyCheck]
        public int NumberOfRooms { get; set; }

        [Required]
        [ConcurrencyCheck]
        public decimal BookingAmountDue { get; set; }

        [Required]
        [ConcurrencyCheck]
        public decimal TaxDue { get; set; }

        [Required]
        [ConcurrencyCheck]
        public decimal TotalAmountDue { get; set; }

        [Required]
        [ConcurrencyCheck]
        public int PaymentStatusID { get; set; }

        [Required]
        [ConcurrencyCheck]
        public int BookingStatusID { get; set; }

        public ICollection<RoomBookingDetail> RoomBookingDetails { get; set; }

        public BookingStatus BookingStatus { get; set; }

        public Hotel Hotel { get; set; }

        public AspNetUser AspNetUser { get; set; }

        public PaymentStatus PaymentStatus { get; set; }

    }
}