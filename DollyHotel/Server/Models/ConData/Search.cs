using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DollyHotel.Server.Models.ConData
{
    [Table("Searches", Schema = "dbo")]
    public partial class Search
    {

        

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long SearchID { get; set; }

        [Required]
        [ConcurrencyCheck]
        public long LocationID { get; set; }

        [Required]
        [ConcurrencyCheck]
        public int NumberOfAdults { get; set; }

        [Required]
        [ConcurrencyCheck]
        public int NumberOfChildren { get; set; }

        [Required]
        [ConcurrencyCheck]
        public DateTime CheckInDate { get; set; }

        [Required]
        [ConcurrencyCheck]
        public DateTime CheckOutDate { get; set; }

        [Required]
        [ConcurrencyCheck]
        public int NumberOfRooms { get; set; }

        [ConcurrencyCheck]
        public string SearchedBy { get; set; }

        [Required]
        [ConcurrencyCheck]
        public DateTime SearchDate { get; set; }

        public City City { get; set; }

        public AspNetUser AspNetUser { get; set; }

    }
}