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
    [Route("odata/ConData/RoomStatuses")]
    public partial class RoomStatusesController : ODataController
    {
        private DollyHotel.Server.Data.ConDataContext context;

        public RoomStatusesController(DollyHotel.Server.Data.ConDataContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<DollyHotel.Server.Models.ConData.RoomStatus> GetRoomStatuses()
        {
            var items = this.context.RoomStatuses.AsQueryable<DollyHotel.Server.Models.ConData.RoomStatus>();
            this.OnRoomStatusesRead(ref items);

            return items;
        }

        partial void OnRoomStatusesRead(ref IQueryable<DollyHotel.Server.Models.ConData.RoomStatus> items);

        partial void OnRoomStatusGet(ref SingleResult<DollyHotel.Server.Models.ConData.RoomStatus> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/ConData/RoomStatuses(RoomStatusID={RoomStatusID})")]
        public SingleResult<DollyHotel.Server.Models.ConData.RoomStatus> GetRoomStatus(int key)
        {
            var items = this.context.RoomStatuses.Where(i => i.RoomStatusID == key);
            var result = SingleResult.Create(items);

            OnRoomStatusGet(ref result);

            return result;
        }
        partial void OnRoomStatusDeleted(DollyHotel.Server.Models.ConData.RoomStatus item);
        partial void OnAfterRoomStatusDeleted(DollyHotel.Server.Models.ConData.RoomStatus item);

        [HttpDelete("/odata/ConData/RoomStatuses(RoomStatusID={RoomStatusID})")]
        public IActionResult DeleteRoomStatus(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.RoomStatuses
                    .Where(i => i.RoomStatusID == key)
                    .Include(i => i.HotelRooms)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<DollyHotel.Server.Models.ConData.RoomStatus>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnRoomStatusDeleted(item);
                this.context.RoomStatuses.Remove(item);
                this.context.SaveChanges();
                this.OnAfterRoomStatusDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnRoomStatusUpdated(DollyHotel.Server.Models.ConData.RoomStatus item);
        partial void OnAfterRoomStatusUpdated(DollyHotel.Server.Models.ConData.RoomStatus item);

        [HttpPut("/odata/ConData/RoomStatuses(RoomStatusID={RoomStatusID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutRoomStatus(int key, [FromBody]DollyHotel.Server.Models.ConData.RoomStatus item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.RoomStatuses
                    .Where(i => i.RoomStatusID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<DollyHotel.Server.Models.ConData.RoomStatus>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnRoomStatusUpdated(item);
                this.context.RoomStatuses.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.RoomStatuses.Where(i => i.RoomStatusID == key);
                ;
                this.OnAfterRoomStatusUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/ConData/RoomStatuses(RoomStatusID={RoomStatusID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchRoomStatus(int key, [FromBody]Delta<DollyHotel.Server.Models.ConData.RoomStatus> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.RoomStatuses
                    .Where(i => i.RoomStatusID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<DollyHotel.Server.Models.ConData.RoomStatus>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnRoomStatusUpdated(item);
                this.context.RoomStatuses.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.RoomStatuses.Where(i => i.RoomStatusID == key);
                ;
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnRoomStatusCreated(DollyHotel.Server.Models.ConData.RoomStatus item);
        partial void OnAfterRoomStatusCreated(DollyHotel.Server.Models.ConData.RoomStatus item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] DollyHotel.Server.Models.ConData.RoomStatus item)
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

                this.OnRoomStatusCreated(item);
                this.context.RoomStatuses.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.RoomStatuses.Where(i => i.RoomStatusID == item.RoomStatusID);

                ;

                this.OnAfterRoomStatusCreated(item);

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
