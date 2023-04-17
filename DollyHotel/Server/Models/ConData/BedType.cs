using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DollyHotel.Server.Models.ConData
{
    [Table("BedTypes", Schema = "dbo")]
    public partial class BedType
    {

        

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long BedTypeID { get; set; }

        [Required]
        [ConcurrencyCheck]
        public string BedTypeName { get; set; }

        [Required]
        [ConcurrencyCheck]
        public long HotelID { get; set; }

        public Hotel Hotel { get; set; }

        public ICollection<RoomType> RoomTypes { get; set; }

    }
}