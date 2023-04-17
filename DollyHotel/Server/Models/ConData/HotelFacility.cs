using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DollyHotel.Server.Models.ConData
{
    [Table("HotelFacilities", Schema = "dbo")]
    public partial class HotelFacility
    {

        

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long FacilityID { get; set; }

        [Required]
        [ConcurrencyCheck]
        public string FacilityName { get; set; }

        [Required]
        [ConcurrencyCheck]
        public long HotelID { get; set; }

        public Hotel Hotel { get; set; }

    }
}