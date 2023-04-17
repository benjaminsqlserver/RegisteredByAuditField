using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

using DollyHotel.Server.Data;

namespace DollyHotel.Server.Controllers
{
    public partial class ExportConDataController : ExportController
    {
        private readonly ConDataContext context;
        private readonly ConDataService service;

        public ExportConDataController(ConDataContext context, ConDataService service)
        {
            this.service = service;
            this.context = context;
        }

        [HttpGet("/export/ConData/aspnetroleclaims/csv")]
        [HttpGet("/export/ConData/aspnetroleclaims/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAspNetRoleClaimsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetAspNetRoleClaims(), Request.Query), fileName);
        }

        [HttpGet("/export/ConData/aspnetroleclaims/excel")]
        [HttpGet("/export/ConData/aspnetroleclaims/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAspNetRoleClaimsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetAspNetRoleClaims(), Request.Query), fileName);
        }

        [HttpGet("/export/ConData/aspnetroles/csv")]
        [HttpGet("/export/ConData/aspnetroles/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAspNetRolesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetAspNetRoles(), Request.Query), fileName);
        }

        [HttpGet("/export/ConData/aspnetroles/excel")]
        [HttpGet("/export/ConData/aspnetroles/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAspNetRolesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetAspNetRoles(), Request.Query), fileName);
        }

        [HttpGet("/export/ConData/aspnetuserclaims/csv")]
        [HttpGet("/export/ConData/aspnetuserclaims/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAspNetUserClaimsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetAspNetUserClaims(), Request.Query), fileName);
        }

        [HttpGet("/export/ConData/aspnetuserclaims/excel")]
        [HttpGet("/export/ConData/aspnetuserclaims/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAspNetUserClaimsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetAspNetUserClaims(), Request.Query), fileName);
        }

        [HttpGet("/export/ConData/aspnetuserlogins/csv")]
        [HttpGet("/export/ConData/aspnetuserlogins/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAspNetUserLoginsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetAspNetUserLogins(), Request.Query), fileName);
        }

        [HttpGet("/export/ConData/aspnetuserlogins/excel")]
        [HttpGet("/export/ConData/aspnetuserlogins/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAspNetUserLoginsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetAspNetUserLogins(), Request.Query), fileName);
        }

        [HttpGet("/export/ConData/aspnetuserroles/csv")]
        [HttpGet("/export/ConData/aspnetuserroles/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAspNetUserRolesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetAspNetUserRoles(), Request.Query), fileName);
        }

        [HttpGet("/export/ConData/aspnetuserroles/excel")]
        [HttpGet("/export/ConData/aspnetuserroles/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAspNetUserRolesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetAspNetUserRoles(), Request.Query), fileName);
        }

        [HttpGet("/export/ConData/aspnetusers/csv")]
        [HttpGet("/export/ConData/aspnetusers/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAspNetUsersToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetAspNetUsers(), Request.Query), fileName);
        }

        [HttpGet("/export/ConData/aspnetusers/excel")]
        [HttpGet("/export/ConData/aspnetusers/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAspNetUsersToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetAspNetUsers(), Request.Query), fileName);
        }

        [HttpGet("/export/ConData/aspnetusertokens/csv")]
        [HttpGet("/export/ConData/aspnetusertokens/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAspNetUserTokensToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetAspNetUserTokens(), Request.Query), fileName);
        }

        [HttpGet("/export/ConData/aspnetusertokens/excel")]
        [HttpGet("/export/ConData/aspnetusertokens/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportAspNetUserTokensToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetAspNetUserTokens(), Request.Query), fileName);
        }

        [HttpGet("/export/ConData/bedtypes/csv")]
        [HttpGet("/export/ConData/bedtypes/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportBedTypesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetBedTypes(), Request.Query), fileName);
        }

        [HttpGet("/export/ConData/bedtypes/excel")]
        [HttpGet("/export/ConData/bedtypes/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportBedTypesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetBedTypes(), Request.Query), fileName);
        }

        [HttpGet("/export/ConData/bookingstatuses/csv")]
        [HttpGet("/export/ConData/bookingstatuses/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportBookingStatusesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetBookingStatuses(), Request.Query), fileName);
        }

        [HttpGet("/export/ConData/bookingstatuses/excel")]
        [HttpGet("/export/ConData/bookingstatuses/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportBookingStatusesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetBookingStatuses(), Request.Query), fileName);
        }

        [HttpGet("/export/ConData/cities/csv")]
        [HttpGet("/export/ConData/cities/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCitiesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetCities(), Request.Query), fileName);
        }

        [HttpGet("/export/ConData/cities/excel")]
        [HttpGet("/export/ConData/cities/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportCitiesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetCities(), Request.Query), fileName);
        }

        [HttpGet("/export/ConData/hotelfacilities/csv")]
        [HttpGet("/export/ConData/hotelfacilities/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportHotelFacilitiesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetHotelFacilities(), Request.Query), fileName);
        }

        [HttpGet("/export/ConData/hotelfacilities/excel")]
        [HttpGet("/export/ConData/hotelfacilities/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportHotelFacilitiesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetHotelFacilities(), Request.Query), fileName);
        }

        [HttpGet("/export/ConData/hotelrooms/csv")]
        [HttpGet("/export/ConData/hotelrooms/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportHotelRoomsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetHotelRooms(), Request.Query), fileName);
        }

        [HttpGet("/export/ConData/hotelrooms/excel")]
        [HttpGet("/export/ConData/hotelrooms/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportHotelRoomsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetHotelRooms(), Request.Query), fileName);
        }

        [HttpGet("/export/ConData/hotels/csv")]
        [HttpGet("/export/ConData/hotels/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportHotelsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetHotels(), Request.Query), fileName);
        }

        [HttpGet("/export/ConData/hotels/excel")]
        [HttpGet("/export/ConData/hotels/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportHotelsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetHotels(), Request.Query), fileName);
        }

        [HttpGet("/export/ConData/hoteltypes/csv")]
        [HttpGet("/export/ConData/hoteltypes/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportHotelTypesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetHotelTypes(), Request.Query), fileName);
        }

        [HttpGet("/export/ConData/hoteltypes/excel")]
        [HttpGet("/export/ConData/hoteltypes/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportHotelTypesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetHotelTypes(), Request.Query), fileName);
        }

        [HttpGet("/export/ConData/paymentstatuses/csv")]
        [HttpGet("/export/ConData/paymentstatuses/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportPaymentStatusesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetPaymentStatuses(), Request.Query), fileName);
        }

        [HttpGet("/export/ConData/paymentstatuses/excel")]
        [HttpGet("/export/ConData/paymentstatuses/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportPaymentStatusesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetPaymentStatuses(), Request.Query), fileName);
        }

        [HttpGet("/export/ConData/roombookingdetails/csv")]
        [HttpGet("/export/ConData/roombookingdetails/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportRoomBookingDetailsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetRoomBookingDetails(), Request.Query), fileName);
        }

        [HttpGet("/export/ConData/roombookingdetails/excel")]
        [HttpGet("/export/ConData/roombookingdetails/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportRoomBookingDetailsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetRoomBookingDetails(), Request.Query), fileName);
        }

        [HttpGet("/export/ConData/roombookings/csv")]
        [HttpGet("/export/ConData/roombookings/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportRoomBookingsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetRoomBookings(), Request.Query), fileName);
        }

        [HttpGet("/export/ConData/roombookings/excel")]
        [HttpGet("/export/ConData/roombookings/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportRoomBookingsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetRoomBookings(), Request.Query), fileName);
        }

        [HttpGet("/export/ConData/roomstatuses/csv")]
        [HttpGet("/export/ConData/roomstatuses/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportRoomStatusesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetRoomStatuses(), Request.Query), fileName);
        }

        [HttpGet("/export/ConData/roomstatuses/excel")]
        [HttpGet("/export/ConData/roomstatuses/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportRoomStatusesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetRoomStatuses(), Request.Query), fileName);
        }

        [HttpGet("/export/ConData/roomtypefacilities/csv")]
        [HttpGet("/export/ConData/roomtypefacilities/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportRoomTypeFacilitiesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetRoomTypeFacilities(), Request.Query), fileName);
        }

        [HttpGet("/export/ConData/roomtypefacilities/excel")]
        [HttpGet("/export/ConData/roomtypefacilities/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportRoomTypeFacilitiesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetRoomTypeFacilities(), Request.Query), fileName);
        }

        [HttpGet("/export/ConData/roomtypes/csv")]
        [HttpGet("/export/ConData/roomtypes/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportRoomTypesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetRoomTypes(), Request.Query), fileName);
        }

        [HttpGet("/export/ConData/roomtypes/excel")]
        [HttpGet("/export/ConData/roomtypes/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportRoomTypesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetRoomTypes(), Request.Query), fileName);
        }

        [HttpGet("/export/ConData/searches/csv")]
        [HttpGet("/export/ConData/searches/csv(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSearchesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetSearches(), Request.Query), fileName);
        }

        [HttpGet("/export/ConData/searches/excel")]
        [HttpGet("/export/ConData/searches/excel(fileName='{fileName}')")]
        public async Task<FileStreamResult> ExportSearchesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetSearches(), Request.Query), fileName);
        }
    }
}
