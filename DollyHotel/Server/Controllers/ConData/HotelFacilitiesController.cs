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
    [Route("odata/ConData/HotelFacilities")]
    public partial class HotelFacilitiesController : ODataController
    {
        private DollyHotel.Server.Data.ConDataContext context;

        public HotelFacilitiesController(DollyHotel.Server.Data.ConDataContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<DollyHotel.Server.Models.ConData.HotelFacility> GetHotelFacilities()
        {
            var items = this.context.HotelFacilities.AsQueryable<DollyHotel.Server.Models.ConData.HotelFacility>();
            this.OnHotelFacilitiesRead(ref items);

            return items;
        }

        partial void OnHotelFacilitiesRead(ref IQueryable<DollyHotel.Server.Models.ConData.HotelFacility> items);

        partial void OnHotelFacilityGet(ref SingleResult<DollyHotel.Server.Models.ConData.HotelFacility> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/ConData/HotelFacilities(FacilityID={FacilityID})")]
        public SingleResult<DollyHotel.Server.Models.ConData.HotelFacility> GetHotelFacility(long key)
        {
            var items = this.context.HotelFacilities.Where(i => i.FacilityID == key);
            var result = SingleResult.Create(items);

            OnHotelFacilityGet(ref result);

            return result;
        }
        partial void OnHotelFacilityDeleted(DollyHotel.Server.Models.ConData.HotelFacility item);
        partial void OnAfterHotelFacilityDeleted(DollyHotel.Server.Models.ConData.HotelFacility item);

        [HttpDelete("/odata/ConData/HotelFacilities(FacilityID={FacilityID})")]
        public IActionResult DeleteHotelFacility(long key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.HotelFacilities
                    .Where(i => i.FacilityID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<DollyHotel.Server.Models.ConData.HotelFacility>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnHotelFacilityDeleted(item);
                this.context.HotelFacilities.Remove(item);
                this.context.SaveChanges();
                this.OnAfterHotelFacilityDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnHotelFacilityUpdated(DollyHotel.Server.Models.ConData.HotelFacility item);
        partial void OnAfterHotelFacilityUpdated(DollyHotel.Server.Models.ConData.HotelFacility item);

        [HttpPut("/odata/ConData/HotelFacilities(FacilityID={FacilityID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutHotelFacility(long key, [FromBody]DollyHotel.Server.Models.ConData.HotelFacility item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.HotelFacilities
                    .Where(i => i.FacilityID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<DollyHotel.Server.Models.ConData.HotelFacility>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnHotelFacilityUpdated(item);
                this.context.HotelFacilities.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.HotelFacilities.Where(i => i.FacilityID == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Hotel");
                this.OnAfterHotelFacilityUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/ConData/HotelFacilities(FacilityID={FacilityID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchHotelFacility(long key, [FromBody]Delta<DollyHotel.Server.Models.ConData.HotelFacility> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.HotelFacilities
                    .Where(i => i.FacilityID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<DollyHotel.Server.Models.ConData.HotelFacility>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnHotelFacilityUpdated(item);
                this.context.HotelFacilities.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.HotelFacilities.Where(i => i.FacilityID == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Hotel");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnHotelFacilityCreated(DollyHotel.Server.Models.ConData.HotelFacility item);
        partial void OnAfterHotelFacilityCreated(DollyHotel.Server.Models.ConData.HotelFacility item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] DollyHotel.Server.Models.ConData.HotelFacility item)
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

                this.OnHotelFacilityCreated(item);
                this.context.HotelFacilities.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.HotelFacilities.Where(i => i.FacilityID == item.FacilityID);

                Request.QueryString = Request.QueryString.Add("$expand", "Hotel");

                this.OnAfterHotelFacilityCreated(item);

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
