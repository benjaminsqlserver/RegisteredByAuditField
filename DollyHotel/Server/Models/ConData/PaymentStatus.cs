using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DollyHotel.Server.Models.ConData
{
    [Table("PaymentStatuses", Schema = "dbo")]
    public partial class PaymentStatus
    {

        

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PaymentStatusID { get; set; }

        [Column("PaymentStatus")]
        [Required]
        [ConcurrencyCheck]
        public string PaymentStatus1 { get; set; }

        public ICollection<RoomBooking> RoomBookings { get; set; }

    }
}