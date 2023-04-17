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
    [Route("odata/ConData/RoomBookingDetails")]
    public partial class RoomBookingDetailsController : ODataController
    {
        private DollyHotel.Server.Data.ConDataContext context;

        public RoomBookingDetailsController(DollyHotel.Server.Data.ConDataContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<DollyHotel.Server.Models.ConData.RoomBookingDetail> GetRoomBookingDetails()
        {
            var items = this.context.RoomBookingDetails.AsQueryable<DollyHotel.Server.Models.ConData.RoomBookingDetail>();
            this.OnRoomBookingDetailsRead(ref items);

            return items;
        }

        partial void OnRoomBookingDetailsRead(ref IQueryable<DollyHotel.Server.Models.ConData.RoomBookingDetail> items);

        partial void OnRoomBookingDetailGet(ref SingleResult<DollyHotel.Server.Models.ConData.RoomBookingDetail> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/ConData/RoomBookingDetails(BookingDetailsID={BookingDetailsID})")]
        public SingleResult<DollyHotel.Server.Models.ConData.RoomBookingDetail> GetRoomBookingDetail(long key)
        {
            var items = this.context.RoomBookingDetails.Where(i => i.BookingDetailsID == key);
            var result = SingleResult.Create(items);

            OnRoomBookingDetailGet(ref result);

            return result;
        }
        partial void OnRoomBookingDetailDeleted(DollyHotel.Server.Models.ConData.RoomBookingDetail item);
        partial void OnAfterRoomBookingDetailDeleted(DollyHotel.Server.Models.ConData.RoomBookingDetail item);

        [HttpDelete("/odata/ConData/RoomBookingDetails(BookingDetailsID={BookingDetailsID})")]
        public IActionResult DeleteRoomBookingDetail(long key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.RoomBookingDetails
                    .Where(i => i.BookingDetailsID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<DollyHotel.Server.Models.ConData.RoomBookingDetail>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnRoomBookingDetailDeleted(item);
                this.context.RoomBookingDetails.Remove(item);
                this.context.SaveChanges();
                this.OnAfterRoomBookingDetailDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnRoomBookingDetailUpdated(DollyHotel.Server.Models.ConData.RoomBookingDetail item);
        partial void OnAfterRoomBookingDetailUpdated(DollyHotel.Server.Models.ConData.RoomBookingDetail item);

        [HttpPut("/odata/ConData/RoomBookingDetails(BookingDetailsID={BookingDetailsID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutRoomBookingDetail(long key, [FromBody]DollyHotel.Server.Models.ConData.RoomBookingDetail item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.RoomBookingDetails
                    .Where(i => i.BookingDetailsID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<DollyHotel.Server.Models.ConData.RoomBookingDetail>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnRoomBookingDetailUpdated(item);
                this.context.RoomBookingDetails.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.RoomBookingDetails.Where(i => i.BookingDetailsID == key);
                Request.QueryString = Request.QueryString.Add("$expand", "RoomBooking,BookingStatus,HotelRoom");
                this.OnAfterRoomBookingDetailUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/ConData/RoomBookingDetails(BookingDetailsID={BookingDetailsID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchRoomBookingDetail(long key, [FromBody]Delta<DollyHotel.Server.Models.ConData.RoomBookingDetail> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.RoomBookingDetails
                    .Where(i => i.BookingDetailsID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<DollyHotel.Server.Models.ConData.RoomBookingDetail>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnRoomBookingDetailUpdated(item);
                this.context.RoomBookingDetails.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.RoomBookingDetails.Where(i => i.BookingDetailsID == key);
                Request.QueryString = Request.QueryString.Add("$expand", "RoomBooking,BookingStatus,HotelRoom");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnRoomBookingDetailCreated(DollyHotel.Server.Models.ConData.RoomBookingDetail item);
        partial void OnAfterRoomBookingDetailCreated(DollyHotel.Server.Models.ConData.RoomBookingDetail item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] DollyHotel.Server.Models.ConData.RoomBookingDetail item)
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

                this.OnRoomBookingDetailCreated(item);
                this.context.RoomBookingDetails.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.RoomBookingDetails.Where(i => i.BookingDetailsID == item.BookingDetailsID);

                Request.QueryString = Request.QueryString.Add("$expand", "RoomBooking,BookingStatus,HotelRoom");

                this.OnAfterRoomBookingDetailCreated(item);

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
