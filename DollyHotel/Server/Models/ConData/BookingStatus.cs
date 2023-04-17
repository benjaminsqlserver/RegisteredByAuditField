using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DollyHotel.Server.Models.ConData
{
    [Table("BookingStatuses", Schema = "dbo")]
    public partial class BookingStatus
    {

        

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookingStatusID { get; set; }

        [Required]
        [ConcurrencyCheck]
        public string BookingStatusName { get; set; }

        public ICollection<RoomBookingDetail> RoomBookingDetails { get; set; }

        public ICollection<RoomBooking> RoomBookings { get; set; }

    }
}