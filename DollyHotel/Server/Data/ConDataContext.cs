using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using DollyHotel.Server.Models.ConData;

namespace DollyHotel.Server.Data
{
    public partial class ConDataContext : DbContext
    {
        public ConDataContext()
        {
        }

        public ConDataContext(DbContextOptions<ConDataContext> options) : base(options)
        {
        }

        partial void OnModelBuilding(ModelBuilder builder);

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<DollyHotel.Server.Models.ConData.AspNetUserLogin>().HasKey(table => new {
                table.LoginProvider, table.ProviderKey
            });

            builder.Entity<DollyHotel.Server.Models.ConData.AspNetUserRole>().HasKey(table => new {
                table.UserId, table.RoleId
            });

            builder.Entity<DollyHotel.Server.Models.ConData.AspNetUserToken>().HasKey(table => new {
                table.UserId, table.LoginProvider, table.Name
            });

            builder.Entity<DollyHotel.Server.Models.ConData.AspNetRoleClaim>()
              .HasOne(i => i.AspNetRole)
              .WithMany(i => i.AspNetRoleClaims)
              .HasForeignKey(i => i.RoleId)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<DollyHotel.Server.Models.ConData.AspNetUserClaim>()
              .HasOne(i => i.AspNetUser)
              .WithMany(i => i.AspNetUserClaims)
              .HasForeignKey(i => i.UserId)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<DollyHotel.Server.Models.ConData.AspNetUserLogin>()
              .HasOne(i => i.AspNetUser)
              .WithMany(i => i.AspNetUserLogins)
              .HasForeignKey(i => i.UserId)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<DollyHotel.Server.Models.ConData.AspNetUserRole>()
              .HasOne(i => i.AspNetRole)
              .WithMany(i => i.AspNetUserRoles)
              .HasForeignKey(i => i.RoleId)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<DollyHotel.Server.Models.ConData.AspNetUserRole>()
              .HasOne(i => i.AspNetUser)
              .WithMany(i => i.AspNetUserRoles)
              .HasForeignKey(i => i.UserId)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<DollyHotel.Server.Models.ConData.AspNetUserToken>()
              .HasOne(i => i.AspNetUser)
              .WithMany(i => i.AspNetUserTokens)
              .HasForeignKey(i => i.UserId)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<DollyHotel.Server.Models.ConData.BedType>()
              .HasOne(i => i.Hotel)
              .WithMany(i => i.BedTypes)
              .HasForeignKey(i => i.HotelID)
              .HasPrincipalKey(i => i.HotelID);

            builder.Entity<DollyHotel.Server.Models.ConData.HotelFacility>()
              .HasOne(i => i.Hotel)
              .WithMany(i => i.HotelFacilities)
              .HasForeignKey(i => i.HotelID)
              .HasPrincipalKey(i => i.HotelID);

            builder.Entity<DollyHotel.Server.Models.ConData.HotelRoom>()
              .HasOne(i => i.Hotel)
              .WithMany(i => i.HotelRooms)
              .HasForeignKey(i => i.HotelID)
              .HasPrincipalKey(i => i.HotelID);

            builder.Entity<DollyHotel.Server.Models.ConData.HotelRoom>()
              .HasOne(i => i.RoomStatus)
              .WithMany(i => i.HotelRooms)
              .HasForeignKey(i => i.RoomStatusID)
              .HasPrincipalKey(i => i.RoomStatusID);

            builder.Entity<DollyHotel.Server.Models.ConData.HotelRoom>()
              .HasOne(i => i.RoomType)
              .WithMany(i => i.HotelRooms)
              .HasForeignKey(i => i.RoomTypeID)
              .HasPrincipalKey(i => i.RoomTypeID);

            builder.Entity<DollyHotel.Server.Models.ConData.Hotel>()
              .HasOne(i => i.HotelType)
              .WithMany(i => i.Hotels)
              .HasForeignKey(i => i.HotelTypeID)
              .HasPrincipalKey(i => i.HotelTypeID);

            builder.Entity<DollyHotel.Server.Models.ConData.Hotel>()
              .HasOne(i => i.City)
              .WithMany(i => i.Hotels)
              .HasForeignKey(i => i.LocationID)
              .HasPrincipalKey(i => i.CityID);

            builder.Entity<DollyHotel.Server.Models.ConData.RoomBookingDetail>()
              .HasOne(i => i.RoomBooking)
              .WithMany(i => i.RoomBookingDetails)
              .HasForeignKey(i => i.BookingID)
              .HasPrincipalKey(i => i.BookingID);

            builder.Entity<DollyHotel.Server.Models.ConData.RoomBookingDetail>()
              .HasOne(i => i.BookingStatus)
              .WithMany(i => i.RoomBookingDetails)
              .HasForeignKey(i => i.BookingStatusID)
              .HasPrincipalKey(i => i.BookingStatusID);

            builder.Entity<DollyHotel.Server.Models.ConData.RoomBookingDetail>()
              .HasOne(i => i.HotelRoom)
              .WithMany(i => i.RoomBookingDetails)
              .HasForeignKey(i => i.RoomID)
              .HasPrincipalKey(i => i.RoomID);

            builder.Entity<DollyHotel.Server.Models.ConData.RoomBooking>()
              .HasOne(i => i.BookingStatus)
              .WithMany(i => i.RoomBookings)
              .HasForeignKey(i => i.BookingStatusID)
              .HasPrincipalKey(i => i.BookingStatusID);

            builder.Entity<DollyHotel.Server.Models.ConData.RoomBooking>()
              .HasOne(i => i.Hotel)
              .WithMany(i => i.RoomBookings)
              .HasForeignKey(i => i.HotelID)
              .HasPrincipalKey(i => i.HotelID);

            builder.Entity<DollyHotel.Server.Models.ConData.RoomBooking>()
              .HasOne(i => i.AspNetUser)
              .WithMany(i => i.RoomBookings)
              .HasForeignKey(i => i.MadeBy)
              .HasPrincipalKey(i => i.Id);

            builder.Entity<DollyHotel.Server.Models.ConData.RoomBooking>()
              .HasOne(i => i.PaymentStatus)
              .WithMany(i => i.RoomBookings)
              .HasForeignKey(i => i.PaymentStatusID)
              .HasPrincipalKey(i => i.PaymentStatusID);

            builder.Entity<DollyHotel.Server.Models.ConData.RoomTypeFacility>()
              .HasOne(i => i.Hotel)
              .WithMany(i => i.RoomTypeFacilities)
              .HasForeignKey(i => i.HotelID)
              .HasPrincipalKey(i => i.HotelID);

            builder.Entity<DollyHotel.Server.Models.ConData.RoomTypeFacility>()
              .HasOne(i => i.RoomType)
              .WithMany(i => i.RoomTypeFacilities)
              .HasForeignKey(i => i.RoomTypeID)
              .HasPrincipalKey(i => i.RoomTypeID);

            builder.Entity<DollyHotel.Server.Models.ConData.RoomType>()
              .HasOne(i => i.BedType)
              .WithMany(i => i.RoomTypes)
              .HasForeignKey(i => i.BedTypeID)
              .HasPrincipalKey(i => i.BedTypeID);

            builder.Entity<DollyHotel.Server.Models.ConData.RoomType>()
              .HasOne(i => i.Hotel)
              .WithMany(i => i.RoomTypes)
              .HasForeignKey(i => i.HotelID)
              .HasPrincipalKey(i => i.HotelID);

            builder.Entity<DollyHotel.Server.Models.ConData.Search>()
              .HasOne(i => i.City)
              .WithMany(i => i.Searches)
              .HasForeignKey(i => i.LocationID)
              .HasPrincipalKey(i => i.CityID);

            builder.Entity<DollyHotel.Server.Models.ConData.Search>()
              .HasOne(i => i.AspNetUser)
              .WithMany(i => i.Searches)
              .HasForeignKey(i => i.SearchedBy)
              .HasPrincipalKey(i => i.Id);
            this.OnModelBuilding(builder);
        }

        public DbSet<DollyHotel.Server.Models.ConData.AspNetRoleClaim> AspNetRoleClaims { get; set; }

        public DbSet<DollyHotel.Server.Models.ConData.AspNetRole> AspNetRoles { get; set; }

        public DbSet<DollyHotel.Server.Models.ConData.AspNetUserClaim> AspNetUserClaims { get; set; }

        public DbSet<DollyHotel.Server.Models.ConData.AspNetUserLogin> AspNetUserLogins { get; set; }

        public DbSet<DollyHotel.Server.Models.ConData.AspNetUserRole> AspNetUserRoles { get; set; }

        public DbSet<DollyHotel.Server.Models.ConData.AspNetUser> AspNetUsers { get; set; }

        public DbSet<DollyHotel.Server.Models.ConData.AspNetUserToken> AspNetUserTokens { get; set; }

        public DbSet<DollyHotel.Server.Models.ConData.BedType> BedTypes { get; set; }

        public DbSet<DollyHotel.Server.Models.ConData.BookingStatus> BookingStatuses { get; set; }

        public DbSet<DollyHotel.Server.Models.ConData.City> Cities { get; set; }

        public DbSet<DollyHotel.Server.Models.ConData.HotelFacility> HotelFacilities { get; set; }

        public DbSet<DollyHotel.Server.Models.ConData.HotelRoom> HotelRooms { get; set; }

        public DbSet<DollyHotel.Server.Models.ConData.Hotel> Hotels { get; set; }

        public DbSet<DollyHotel.Server.Models.ConData.HotelType> HotelTypes { get; set; }

        public DbSet<DollyHotel.Server.Models.ConData.PaymentStatus> PaymentStatuses { get; set; }

        public DbSet<DollyHotel.Server.Models.ConData.RoomBookingDetail> RoomBookingDetails { get; set; }

        public DbSet<DollyHotel.Server.Models.ConData.RoomBooking> RoomBookings { get; set; }

        public DbSet<DollyHotel.Server.Models.ConData.RoomStatus> RoomStatuses { get; set; }

        public DbSet<DollyHotel.Server.Models.ConData.RoomTypeFacility> RoomTypeFacilities { get; set; }

        public DbSet<DollyHotel.Server.Models.ConData.RoomType> RoomTypes { get; set; }

        public DbSet<DollyHotel.Server.Models.ConData.Search> Searches { get; set; }
    }
}