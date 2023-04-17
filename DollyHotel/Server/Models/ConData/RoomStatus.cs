using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DollyHotel.Server.Models.ConData
{
    [Table("RoomStatuses", Schema = "dbo")]
    public partial class RoomStatus
    {

        

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoomStatusID { get; set; }

        [Required]
        [ConcurrencyCheck]
        public string StatusName { get; set; }

        public ICollection<HotelRoom> HotelRooms { get; set; }

    }
}