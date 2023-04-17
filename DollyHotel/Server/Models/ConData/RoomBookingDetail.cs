using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DollyHotel.Server.Models.ConData
{
    [Table("RoomBookingDetails", Schema = "dbo")]
    public partial class RoomBookingDetail
    {

        

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long BookingDetailsID { get; set; }

        [Required]
        [ConcurrencyCheck]
        public long BookingID { get; set; }

        [Required]
        [ConcurrencyCheck]
        public long RoomID { get; set; }

        [Required]
        [ConcurrencyCheck]
        public int BookingStatusID { get; set; }

        public RoomBooking RoomBooking { get; set; }

        public BookingStatus BookingStatus { get; set; }

        public HotelRoom HotelRoom { get; set; }

    }
}