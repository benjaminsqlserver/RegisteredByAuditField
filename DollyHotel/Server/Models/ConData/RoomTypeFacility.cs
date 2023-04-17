using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DollyHotel.Server.Models.ConData
{
    [Table("RoomTypeFacilities", Schema = "dbo")]
    public partial class RoomTypeFacility
    {

        

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long RoomTypeFacilityID { get; set; }

        [Required]
        [ConcurrencyCheck]
        public long RoomTypeID { get; set; }

        [Required]
        [ConcurrencyCheck]
        public long HotelID { get; set; }

        [Required]
        [ConcurrencyCheck]
        public string RoomFacilityName { get; set; }

        public Hotel Hotel { get; set; }

        public RoomType RoomType { get; set; }

    }
}