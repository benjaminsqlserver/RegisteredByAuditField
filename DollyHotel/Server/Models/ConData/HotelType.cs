using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DollyHotel.Server.Models.ConData
{
    [Table("HotelTypes", Schema = "dbo")]
    public partial class HotelType
    {

        

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HotelTypeID { get; set; }

        [Required]
        [ConcurrencyCheck]
        public string HotelTypeName { get; set; }

        public ICollection<Hotel> Hotels { get; set; }

    }
}