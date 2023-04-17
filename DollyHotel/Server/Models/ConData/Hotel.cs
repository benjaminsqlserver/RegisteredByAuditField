using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DollyHotel.Server.Models.ConData
{
    [Table("Hotels", Schema = "dbo")]
    public partial class Hotel
    {

        

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long HotelID { get; set; }

        [Required]
        [ConcurrencyCheck]
        public string HotelName { get; set; }

        [Required]
        [ConcurrencyCheck]
        public string Address { get; set; }

        [Required]
        [ConcurrencyCheck]
        public long LocationID { get; set; }

        [Required]
        [ConcurrencyCheck]
        public string Description { get; set; }

        [Required]
        [ConcurrencyCheck]
        public string PhoneNumber1 { get; set; }

        [ConcurrencyCheck]
        public string PhoneNumber2 { get; set; }

        [Required]
        [ConcurrencyCheck]
        public string EmailAddress { get; set; }

        // [Required]
        [ConcurrencyCheck]
        public string? MainPhoto { get; set; } = null;

        [Required]
        [ConcurrencyCheck]
        public string RegisteredBy { get; set; } = "";

        [Required]
        [ConcurrencyCheck]
        public int HotelTypeID { get; set; }

        //I am going to make some properties nullable
        public ICollection<BedType>? BedTypes { get; set; }

        public ICollection<HotelFacility>? HotelFacilities { get; set; }

        public ICollection<HotelRoom>? HotelRooms { get; set; }

        public HotelType? HotelType { get; set; }

        public City? City { get; set; }

        public ICollection<RoomBooking>? RoomBookings { get; set; }

        public ICollection<RoomTypeFacility>? RoomTypeFacilities { get; set; }

        public ICollection<RoomType>? RoomTypes { get; set; }

    }
}