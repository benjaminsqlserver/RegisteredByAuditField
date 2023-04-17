using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DollyHotel.Server.Models.ConData
{
    [Table("RoomTypes", Schema = "dbo")]
    public partial class RoomType
    {

        

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long RoomTypeID { get; set; }

        [Required]
        [ConcurrencyCheck]
        public string RoomTypeName { get; set; }

        [Required]
        [ConcurrencyCheck]
        public long HotelID { get; set; }

        [Required]
        [ConcurrencyCheck]
        public decimal RoomPrice { get; set; }

        [Required]
        [ConcurrencyCheck]
        public string RoomTypeImage { get; set; }

        [Required]
        [ConcurrencyCheck]
        public long BedTypeID { get; set; }

        public ICollection<HotelRoom> HotelRooms { get; set; }

        public ICollection<RoomTypeFacility> RoomTypeFacilities { get; set; }

        public BedType BedType { get; set; }

        public Hotel Hotel { get; set; }

    }
}