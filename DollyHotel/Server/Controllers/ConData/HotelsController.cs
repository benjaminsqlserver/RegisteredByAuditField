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
    [Route("odata/ConData/Hotels")]
    public partial class HotelsController : ODataController
    {
        private DollyHotel.Server.Data.ConDataContext context;

        public HotelsController(DollyHotel.Server.Data.ConDataContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<DollyHotel.Server.Models.ConData.Hotel> GetHotels()
        {
            var items = this.context.Hotels.AsQueryable<DollyHotel.Server.Models.ConData.Hotel>();
            this.OnHotelsRead(ref items);

            return items;
        }

        partial void OnHotelsRead(ref IQueryable<DollyHotel.Server.Models.ConData.Hotel> items);

        partial void OnHotelGet(ref SingleResult<DollyHotel.Server.Models.ConData.Hotel> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/ConData/Hotels(HotelID={HotelID})")]
        public SingleResult<DollyHotel.Server.Models.ConData.Hotel> GetHotel(long key)
        {
            var items = this.context.Hotels.Where(i => i.HotelID == key);
            var result = SingleResult.Create(items);

            OnHotelGet(ref result);

            return result;
        }
        partial void OnHotelDeleted(DollyHotel.Server.Models.ConData.Hotel item);
        partial void OnAfterHotelDeleted(DollyHotel.Server.Models.ConData.Hotel item);

        [HttpDelete("/odata/ConData/Hotels(HotelID={HotelID})")]
        public IActionResult DeleteHotel(long key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.Hotels
                    .Where(i => i.HotelID == key)
                    .Include(i => i.BedTypes)
                    .Include(i => i.HotelFacilities)
                    .Include(i => i.HotelRooms)
                    .Include(i => i.RoomBookings)
                    .Include(i => i.RoomTypeFacilities)
                    .Include(i => i.RoomTypes)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<DollyHotel.Server.Models.ConData.Hotel>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnHotelDeleted(item);
                this.context.Hotels.Remove(item);
                this.context.SaveChanges();
                this.OnAfterHotelDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnHotelUpdated(DollyHotel.Server.Models.ConData.Hotel item);
        partial void OnAfterHotelUpdated(DollyHotel.Server.Models.ConData.Hotel item);

        [HttpPut("/odata/ConData/Hotels(HotelID={HotelID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutHotel(long key, [FromBody]DollyHotel.Server.Models.ConData.Hotel item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Hotels
                    .Where(i => i.HotelID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<DollyHotel.Server.Models.ConData.Hotel>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnHotelUpdated(item);
                this.context.Hotels.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Hotels.Where(i => i.HotelID == key);
                Request.QueryString = Request.QueryString.Add("$expand", "HotelType,City");
                this.OnAfterHotelUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/ConData/Hotels(HotelID={HotelID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchHotel(long key, [FromBody]Delta<DollyHotel.Server.Models.ConData.Hotel> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Hotels
                    .Where(i => i.HotelID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<DollyHotel.Server.Models.ConData.Hotel>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnHotelUpdated(item);
                this.context.Hotels.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Hotels.Where(i => i.HotelID == key);
                Request.QueryString = Request.QueryString.Add("$expand", "HotelType,City");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnHotelCreated(DollyHotel.Server.Models.ConData.Hotel item);
        partial void OnAfterHotelCreated(DollyHotel.Server.Models.ConData.Hotel item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] DollyHotel.Server.Models.ConData.Hotel item)
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

                this.OnHotelCreated(item);
                this.context.Hotels.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Hotels.Where(i => i.HotelID == item.HotelID);

                Request.QueryString = Request.QueryString.Add("$expand", "HotelType,City");

                this.OnAfterHotelCreated(item);

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
