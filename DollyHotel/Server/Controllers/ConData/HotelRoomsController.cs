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
    [Route("odata/ConData/HotelRooms")]
    public partial class HotelRoomsController : ODataController
    {
        private DollyHotel.Server.Data.ConDataContext context;

        public HotelRoomsController(DollyHotel.Server.Data.ConDataContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<DollyHotel.Server.Models.ConData.HotelRoom> GetHotelRooms()
        {
            var items = this.context.HotelRooms.AsQueryable<DollyHotel.Server.Models.ConData.HotelRoom>();
            this.OnHotelRoomsRead(ref items);

            return items;
        }

        partial void OnHotelRoomsRead(ref IQueryable<DollyHotel.Server.Models.ConData.HotelRoom> items);

        partial void OnHotelRoomGet(ref SingleResult<DollyHotel.Server.Models.ConData.HotelRoom> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/ConData/HotelRooms(RoomID={RoomID})")]
        public SingleResult<DollyHotel.Server.Models.ConData.HotelRoom> GetHotelRoom(long key)
        {
            var items = this.context.HotelRooms.Where(i => i.RoomID == key);
            var result = SingleResult.Create(items);

            OnHotelRoomGet(ref result);

            return result;
        }
        partial void OnHotelRoomDeleted(DollyHotel.Server.Models.ConData.HotelRoom item);
        partial void OnAfterHotelRoomDeleted(DollyHotel.Server.Models.ConData.HotelRoom item);

        [HttpDelete("/odata/ConData/HotelRooms(RoomID={RoomID})")]
        public IActionResult DeleteHotelRoom(long key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.HotelRooms
                    .Where(i => i.RoomID == key)
                    .Include(i => i.RoomBookingDetails)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<DollyHotel.Server.Models.ConData.HotelRoom>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnHotelRoomDeleted(item);
                this.context.HotelRooms.Remove(item);
                this.context.SaveChanges();
                this.OnAfterHotelRoomDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnHotelRoomUpdated(DollyHotel.Server.Models.ConData.HotelRoom item);
        partial void OnAfterHotelRoomUpdated(DollyHotel.Server.Models.ConData.HotelRoom item);

        [HttpPut("/odata/ConData/HotelRooms(RoomID={RoomID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutHotelRoom(long key, [FromBody]DollyHotel.Server.Models.ConData.HotelRoom item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.HotelRooms
                    .Where(i => i.RoomID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<DollyHotel.Server.Models.ConData.HotelRoom>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnHotelRoomUpdated(item);
                this.context.HotelRooms.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.HotelRooms.Where(i => i.RoomID == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Hotel,RoomStatus,RoomType");
                this.OnAfterHotelRoomUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/ConData/HotelRooms(RoomID={RoomID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchHotelRoom(long key, [FromBody]Delta<DollyHotel.Server.Models.ConData.HotelRoom> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.HotelRooms
                    .Where(i => i.RoomID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<DollyHotel.Server.Models.ConData.HotelRoom>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnHotelRoomUpdated(item);
                this.context.HotelRooms.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.HotelRooms.Where(i => i.RoomID == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Hotel,RoomStatus,RoomType");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnHotelRoomCreated(DollyHotel.Server.Models.ConData.HotelRoom item);
        partial void OnAfterHotelRoomCreated(DollyHotel.Server.Models.ConData.HotelRoom item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] DollyHotel.Server.Models.ConData.HotelRoom item)
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

                this.OnHotelRoomCreated(item);
                this.context.HotelRooms.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.HotelRooms.Where(i => i.RoomID == item.RoomID);

                Request.QueryString = Request.QueryString.Add("$expand", "Hotel,RoomStatus,RoomType");

                this.OnAfterHotelRoomCreated(item);

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
