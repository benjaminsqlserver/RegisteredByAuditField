using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DollyHotel.Server.Models.ConData
{
    [Table("Cities", Schema = "dbo")]
    public partial class City
    {

       

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CityID { get; set; }

        [Required]
        [ConcurrencyCheck]
        public string CityName { get; set; }

        public ICollection<Hotel> Hotels { get; set; }

        public ICollection<Search> Searches { get; set; }

    }
}