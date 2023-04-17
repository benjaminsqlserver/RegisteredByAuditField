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
    [Route("odata/ConData/RoomTypeFacilities")]
    public partial class RoomTypeFacilitiesController : ODataController
    {
        private DollyHotel.Server.Data.ConDataContext context;

        public RoomTypeFacilitiesController(DollyHotel.Server.Data.ConDataContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<DollyHotel.Server.Models.ConData.RoomTypeFacility> GetRoomTypeFacilities()
        {
            var items = this.context.RoomTypeFacilities.AsQueryable<DollyHotel.Server.Models.ConData.RoomTypeFacility>();
            this.OnRoomTypeFacilitiesRead(ref items);

            return items;
        }

        partial void OnRoomTypeFacilitiesRead(ref IQueryable<DollyHotel.Server.Models.ConData.RoomTypeFacility> items);

        partial void OnRoomTypeFacilityGet(ref SingleResult<DollyHotel.Server.Models.ConData.RoomTypeFacility> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/ConData/RoomTypeFacilities(RoomTypeFacilityID={RoomTypeFacilityID})")]
        public SingleResult<DollyHotel.Server.Models.ConData.RoomTypeFacility> GetRoomTypeFacility(long key)
        {
            var items = this.context.RoomTypeFacilities.Where(i => i.RoomTypeFacilityID == key);
            var result = SingleResult.Create(items);

            OnRoomTypeFacilityGet(ref result);

            return result;
        }
        partial void OnRoomTypeFacilityDeleted(DollyHotel.Server.Models.ConData.RoomTypeFacility item);
        partial void OnAfterRoomTypeFacilityDeleted(DollyHotel.Server.Models.ConData.RoomTypeFacility item);

        [HttpDelete("/odata/ConData/RoomTypeFacilities(RoomTypeFacilityID={RoomTypeFacilityID})")]
        public IActionResult DeleteRoomTypeFacility(long key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.RoomTypeFacilities
                    .Where(i => i.RoomTypeFacilityID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<DollyHotel.Server.Models.ConData.RoomTypeFacility>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnRoomTypeFacilityDeleted(item);
                this.context.RoomTypeFacilities.Remove(item);
                this.context.SaveChanges();
                this.OnAfterRoomTypeFacilityDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnRoomTypeFacilityUpdated(DollyHotel.Server.Models.ConData.RoomTypeFacility item);
        partial void OnAfterRoomTypeFacilityUpdated(DollyHotel.Server.Models.ConData.RoomTypeFacility item);

        [HttpPut("/odata/ConData/RoomTypeFacilities(RoomTypeFacilityID={RoomTypeFacilityID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutRoomTypeFacility(long key, [FromBody]DollyHotel.Server.Models.ConData.RoomTypeFacility item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.RoomTypeFacilities
                    .Where(i => i.RoomTypeFacilityID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<DollyHotel.Server.Models.ConData.RoomTypeFacility>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnRoomTypeFacilityUpdated(item);
                this.context.RoomTypeFacilities.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.RoomTypeFacilities.Where(i => i.RoomTypeFacilityID == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Hotel,RoomType");
                this.OnAfterRoomTypeFacilityUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/ConData/RoomTypeFacilities(RoomTypeFacilityID={RoomTypeFacilityID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchRoomTypeFacility(long key, [FromBody]Delta<DollyHotel.Server.Models.ConData.RoomTypeFacility> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.RoomTypeFacilities
                    .Where(i => i.RoomTypeFacilityID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<DollyHotel.Server.Models.ConData.RoomTypeFacility>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnRoomTypeFacilityUpdated(item);
                this.context.RoomTypeFacilities.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.RoomTypeFacilities.Where(i => i.RoomTypeFacilityID == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Hotel,RoomType");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnRoomTypeFacilityCreated(DollyHotel.Server.Models.ConData.RoomTypeFacility item);
        partial void OnAfterRoomTypeFacilityCreated(DollyHotel.Server.Models.ConData.RoomTypeFacility item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] DollyHotel.Server.Models.ConData.RoomTypeFacility item)
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

                this.OnRoomTypeFacilityCreated(item);
                this.context.RoomTypeFacilities.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.RoomTypeFacilities.Where(i => i.RoomTypeFacilityID == item.RoomTypeFacilityID);

                Request.QueryString = Request.QueryString.Add("$expand", "Hotel,RoomType");

                this.OnAfterRoomTypeFacilityCreated(item);

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
