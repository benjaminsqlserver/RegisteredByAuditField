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
    [Route("odata/ConData/BookingStatuses")]
    public partial class BookingStatusesController : ODataController
    {
        private DollyHotel.Server.Data.ConDataContext context;

        public BookingStatusesController(DollyHotel.Server.Data.ConDataContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<DollyHotel.Server.Models.ConData.BookingStatus> GetBookingStatuses()
        {
            var items = this.context.BookingStatuses.AsQueryable<DollyHotel.Server.Models.ConData.BookingStatus>();
            this.OnBookingStatusesRead(ref items);

            return items;
        }

        partial void OnBookingStatusesRead(ref IQueryable<DollyHotel.Server.Models.ConData.BookingStatus> items);

        partial void OnBookingStatusGet(ref SingleResult<DollyHotel.Server.Models.ConData.BookingStatus> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/ConData/BookingStatuses(BookingStatusID={BookingStatusID})")]
        public SingleResult<DollyHotel.Server.Models.ConData.BookingStatus> GetBookingStatus(int key)
        {
            var items = this.context.BookingStatuses.Where(i => i.BookingStatusID == key);
            var result = SingleResult.Create(items);

            OnBookingStatusGet(ref result);

            return result;
        }
        partial void OnBookingStatusDeleted(DollyHotel.Server.Models.ConData.BookingStatus item);
        partial void OnAfterBookingStatusDeleted(DollyHotel.Server.Models.ConData.BookingStatus item);

        [HttpDelete("/odata/ConData/BookingStatuses(BookingStatusID={BookingStatusID})")]
        public IActionResult DeleteBookingStatus(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.BookingStatuses
                    .Where(i => i.BookingStatusID == key)
                    .Include(i => i.RoomBookingDetails)
                    .Include(i => i.RoomBookings)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<DollyHotel.Server.Models.ConData.BookingStatus>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnBookingStatusDeleted(item);
                this.context.BookingStatuses.Remove(item);
                this.context.SaveChanges();
                this.OnAfterBookingStatusDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnBookingStatusUpdated(DollyHotel.Server.Models.ConData.BookingStatus item);
        partial void OnAfterBookingStatusUpdated(DollyHotel.Server.Models.ConData.BookingStatus item);

        [HttpPut("/odata/ConData/BookingStatuses(BookingStatusID={BookingStatusID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutBookingStatus(int key, [FromBody]DollyHotel.Server.Models.ConData.BookingStatus item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.BookingStatuses
                    .Where(i => i.BookingStatusID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<DollyHotel.Server.Models.ConData.BookingStatus>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnBookingStatusUpdated(item);
                this.context.BookingStatuses.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.BookingStatuses.Where(i => i.BookingStatusID == key);
                ;
                this.OnAfterBookingStatusUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/ConData/BookingStatuses(BookingStatusID={BookingStatusID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchBookingStatus(int key, [FromBody]Delta<DollyHotel.Server.Models.ConData.BookingStatus> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.BookingStatuses
                    .Where(i => i.BookingStatusID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<DollyHotel.Server.Models.ConData.BookingStatus>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnBookingStatusUpdated(item);
                this.context.BookingStatuses.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.BookingStatuses.Where(i => i.BookingStatusID == key);
                ;
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnBookingStatusCreated(DollyHotel.Server.Models.ConData.BookingStatus item);
        partial void OnAfterBookingStatusCreated(DollyHotel.Server.Models.ConData.BookingStatus item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] DollyHotel.Server.Models.ConData.BookingStatus item)
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

                this.OnBookingStatusCreated(item);
                this.context.BookingStatuses.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.BookingStatuses.Where(i => i.BookingStatusID == item.BookingStatusID);

                ;

                this.OnAfterBookingStatusCreated(item);

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
