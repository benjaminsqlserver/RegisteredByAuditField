using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DollyHotel.Server.Models.ConData
{
    [Table("AspNetUserTokens", Schema = "dbo")]
    public partial class AspNetUserToken
    {

        

        [Key]
        [Required]
        public string UserId { get; set; }

        [Key]
        [Required]
        public string LoginProvider { get; set; }

        [Key]
        [Required]
        public string Name { get; set; }

        [ConcurrencyCheck]
        public string Value { get; set; }

        public AspNetUser AspNetUser { get; set; }

    }
}