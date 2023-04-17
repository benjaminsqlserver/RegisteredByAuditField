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
    [Route("odata/ConData/RoomTypes")]
    public partial class RoomTypesController : ODataController
    {
        private DollyHotel.Server.Data.ConDataContext context;

        public RoomTypesController(DollyHotel.Server.Data.ConDataContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<DollyHotel.Server.Models.ConData.RoomType> GetRoomTypes()
        {
            var items = this.context.RoomTypes.AsQueryable<DollyHotel.Server.Models.ConData.RoomType>();
            this.OnRoomTypesRead(ref items);

            return items;
        }

        partial void OnRoomTypesRead(ref IQueryable<DollyHotel.Server.Models.ConData.RoomType> items);

        partial void OnRoomTypeGet(ref SingleResult<DollyHotel.Server.Models.ConData.RoomType> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/ConData/RoomTypes(RoomTypeID={RoomTypeID})")]
        public SingleResult<DollyHotel.Server.Models.ConData.RoomType> GetRoomType(long key)
        {
            var items = this.context.RoomTypes.Where(i => i.RoomTypeID == key);
            var result = SingleResult.Create(items);

            OnRoomTypeGet(ref result);

            return result;
        }
        partial void OnRoomTypeDeleted(DollyHotel.Server.Models.ConData.RoomType item);
        partial void OnAfterRoomTypeDeleted(DollyHotel.Server.Models.ConData.RoomType item);

        [HttpDelete("/odata/ConData/RoomTypes(RoomTypeID={RoomTypeID})")]
        public IActionResult DeleteRoomType(long key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.RoomTypes
                    .Where(i => i.RoomTypeID == key)
                    .Include(i => i.HotelRooms)
                    .Include(i => i.RoomTypeFacilities)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<DollyHotel.Server.Models.ConData.RoomType>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnRoomTypeDeleted(item);
                this.context.RoomTypes.Remove(item);
                this.context.SaveChanges();
                this.OnAfterRoomTypeDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnRoomTypeUpdated(DollyHotel.Server.Models.ConData.RoomType item);
        partial void OnAfterRoomTypeUpdated(DollyHotel.Server.Models.ConData.RoomType item);

        [HttpPut("/odata/ConData/RoomTypes(RoomTypeID={RoomTypeID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutRoomType(long key, [FromBody]DollyHotel.Server.Models.ConData.RoomType item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.RoomTypes
                    .Where(i => i.RoomTypeID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<DollyHotel.Server.Models.ConData.RoomType>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnRoomTypeUpdated(item);
                this.context.RoomTypes.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.RoomTypes.Where(i => i.RoomTypeID == key);
                Request.QueryString = Request.QueryString.Add("$expand", "BedType,Hotel");
                this.OnAfterRoomTypeUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/ConData/RoomTypes(RoomTypeID={RoomTypeID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchRoomType(long key, [FromBody]Delta<DollyHotel.Server.Models.ConData.RoomType> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.RoomTypes
                    .Where(i => i.RoomTypeID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<DollyHotel.Server.Models.ConData.RoomType>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnRoomTypeUpdated(item);
                this.context.RoomTypes.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.RoomTypes.Where(i => i.RoomTypeID == key);
                Request.QueryString = Request.QueryString.Add("$expand", "BedType,Hotel");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnRoomTypeCreated(DollyHotel.Server.Models.ConData.RoomType item);
        partial void OnAfterRoomTypeCreated(DollyHotel.Server.Models.ConData.RoomType item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] DollyHotel.Server.Models.ConData.RoomType item)
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

                this.OnRoomTypeCreated(item);
                this.context.RoomTypes.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.RoomTypes.Where(i => i.RoomTypeID == item.RoomTypeID);

                Request.QueryString = Request.QueryString.Add("$expand", "BedType,Hotel");

                this.OnAfterRoomTypeCreated(item);

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
