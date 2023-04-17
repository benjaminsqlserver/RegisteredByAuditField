using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DollyHotel.Server.Models.ConData
{
    [Table("HotelRooms", Schema = "dbo")]
    public partial class HotelRoom
    {

        

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long RoomID { get; set; }

        [Required]
        [ConcurrencyCheck]
        public long RoomTypeID { get; set; }

        [Required]
        [ConcurrencyCheck]
        public long HotelID { get; set; }

        [Required]
        [ConcurrencyCheck]
        public string RoomNumber { get; set; }

        [Required]
        [ConcurrencyCheck]
        public int RoomStatusID { get; set; }

        public Hotel Hotel { get; set; }

        public RoomStatus RoomStatus { get; set; }

        public RoomType RoomType { get; set; }

        public ICollection<RoomBookingDetail> RoomBookingDetails { get; set; }

    }
}