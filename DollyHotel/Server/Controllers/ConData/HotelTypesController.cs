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
    [Route("odata/ConData/HotelTypes")]
    public partial class HotelTypesController : ODataController
    {
        private DollyHotel.Server.Data.ConDataContext context;

        public HotelTypesController(DollyHotel.Server.Data.ConDataContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<DollyHotel.Server.Models.ConData.HotelType> GetHotelTypes()
        {
            var items = this.context.HotelTypes.AsQueryable<DollyHotel.Server.Models.ConData.HotelType>();
            this.OnHotelTypesRead(ref items);

            return items;
        }

        partial void OnHotelTypesRead(ref IQueryable<DollyHotel.Server.Models.ConData.HotelType> items);

        partial void OnHotelTypeGet(ref SingleResult<DollyHotel.Server.Models.ConData.HotelType> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/ConData/HotelTypes(HotelTypeID={HotelTypeID})")]
        public SingleResult<DollyHotel.Server.Models.ConData.HotelType> GetHotelType(int key)
        {
            var items = this.context.HotelTypes.Where(i => i.HotelTypeID == key);
            var result = SingleResult.Create(items);

            OnHotelTypeGet(ref result);

            return result;
        }
        partial void OnHotelTypeDeleted(DollyHotel.Server.Models.ConData.HotelType item);
        partial void OnAfterHotelTypeDeleted(DollyHotel.Server.Models.ConData.HotelType item);

        [HttpDelete("/odata/ConData/HotelTypes(HotelTypeID={HotelTypeID})")]
        public IActionResult DeleteHotelType(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.HotelTypes
                    .Where(i => i.HotelTypeID == key)
                    .Include(i => i.Hotels)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<DollyHotel.Server.Models.ConData.HotelType>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnHotelTypeDeleted(item);
                this.context.HotelTypes.Remove(item);
                this.context.SaveChanges();
                this.OnAfterHotelTypeDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnHotelTypeUpdated(DollyHotel.Server.Models.ConData.HotelType item);
        partial void OnAfterHotelTypeUpdated(DollyHotel.Server.Models.ConData.HotelType item);

        [HttpPut("/odata/ConData/HotelTypes(HotelTypeID={HotelTypeID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutHotelType(int key, [FromBody]DollyHotel.Server.Models.ConData.HotelType item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.HotelTypes
                    .Where(i => i.HotelTypeID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<DollyHotel.Server.Models.ConData.HotelType>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnHotelTypeUpdated(item);
                this.context.HotelTypes.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.HotelTypes.Where(i => i.HotelTypeID == key);
                ;
                this.OnAfterHotelTypeUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/ConData/HotelTypes(HotelTypeID={HotelTypeID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchHotelType(int key, [FromBody]Delta<DollyHotel.Server.Models.ConData.HotelType> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.HotelTypes
                    .Where(i => i.HotelTypeID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<DollyHotel.Server.Models.ConData.HotelType>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnHotelTypeUpdated(item);
                this.context.HotelTypes.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.HotelTypes.Where(i => i.HotelTypeID == key);
                ;
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnHotelTypeCreated(DollyHotel.Server.Models.ConData.HotelType item);
        partial void OnAfterHotelTypeCreated(DollyHotel.Server.Models.ConData.HotelType item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] DollyHotel.Server.Models.ConData.HotelType item)
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

                this.OnHotelTypeCreated(item);
                this.context.HotelTypes.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.HotelTypes.Where(i => i.HotelTypeID == item.HotelTypeID);

                ;

                this.OnAfterHotelTypeCreated(item);

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
