using System;
using System.Net;
using System.Data;
using System.Linq;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Formatter;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DollyHotel.Server.Controllers.ConData
{
    [Route("odata/ConData/RoomBookings")]
    public partial class RoomBookingsController : ODataController
    {
        private DollyHotel.Server.Data.ConDataContext context;

        public RoomBookingsController(DollyHotel.Server.Data.ConDataContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<DollyHotel.Server.Models.ConData.RoomBooking> GetRoomBookings()
        {
            var items = this.context.RoomBookings.AsQueryable<DollyHotel.Server.Models.ConData.RoomBooking>();
            this.OnRoomBookingsRead(ref items);

            return items;
        }

        partial void OnRoomBookingsRead(ref IQueryable<DollyHotel.Server.Models.ConData.RoomBooking> items);

        partial void OnRoomBookingGet(ref SingleResult<DollyHotel.Server.Models.ConData.RoomBooking> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/ConData/RoomBookings(BookingID={BookingID})")]
        public SingleResult<DollyHotel.Server.Models.ConData.RoomBooking> GetRoomBooking(long key)
        {
            var items = this.context.RoomBookings.Where(i => i.BookingID == key);
            var result = SingleResult.Create(items);

            OnRoomBookingGet(ref result);

            return result;
        }
        partial void OnRoomBookingDeleted(DollyHotel.Server.Models.ConData.RoomBooking item);
        partial void OnAfterRoomBookingDeleted(DollyHotel.Server.Models.ConData.RoomBooking item);

        [HttpDelete("/odata/ConData/RoomBookings(BookingID={BookingID})")]
        public IActionResult DeleteRoomBooking(long key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.RoomBookings
                    .Where(i => i.BookingID == key)
                    .Include(i => i.RoomBookingDetails)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<DollyHotel.Server.Models.ConData.RoomBooking>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnRoomBookingDeleted(item);
                this.context.RoomBookings.Remove(item);
                this.context.SaveChanges();
                this.OnAfterRoomBookingDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnRoomBookingUpdated(DollyHotel.Server.Models.ConData.RoomBooking item);
        partial void OnAfterRoomBookingUpdated(DollyHotel.Server.Models.ConData.RoomBooking item);

        [HttpPut("/odata/ConData/RoomBookings(BookingID={BookingID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutRoomBooking(long key, [FromBody]DollyHotel.Server.Models.ConData.RoomBooking item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.RoomBookings
                    .Where(i => i.BookingID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<DollyHotel.Server.Models.ConData.RoomBooking>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnRoomBookingUpdated(item);
                this.context.RoomBookings.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.RoomBookings.Where(i => i.BookingID == key);
                Request.QueryString = Request.QueryString.Add("$expand", "BookingStatus,Hotel,AspNetUser,PaymentStatus");
                this.OnAfterRoomBookingUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/ConData/RoomBookings(BookingID={BookingID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchRoomBooking(long key, [FromBody]Delta<DollyHotel.Server.Models.ConData.RoomBooking> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.RoomBookings
                    .Where(i => i.BookingID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<DollyHotel.Server.Models.ConData.RoomBooking>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnRoomBookingUpdated(item);
                this.context.RoomBookings.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.RoomBookings.Where(i => i.BookingID == key);
                Request.QueryString = Request.QueryString.Add("$expand", "BookingStatus,Hotel,AspNetUser,PaymentStatus");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnRoomBookingCreated(DollyHotel.Server.Models.ConData.RoomBooking item);
        partial void OnAfterRoomBookingCreated(DollyHotel.Server.Models.ConData.RoomBooking item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] DollyHotel.Server.Models.ConData.RoomBooking item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (item == null)
                {
                    return BadRequest();
                }

                this.OnRoomBookingCreated(item);
                this.context.RoomBookings.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.RoomBookings.Where(i => i.BookingID == item.BookingID);

                Request.QueryString = Request.QueryString.Add("$expand", "BookingStatus,Hotel,AspNetUser,PaymentStatus");

                this.OnAfterRoomBookingCreated(item);

                return new ObjectResult(SingleResult.Create(itemToReturn))
                {
                    StatusCode = 201
                };
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }
    }
}
