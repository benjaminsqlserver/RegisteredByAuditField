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
    [Route("odata/ConData/BedTypes")]
    public partial class BedTypesController : ODataController
    {
        private DollyHotel.Server.Data.ConDataContext context;

        public BedTypesController(DollyHotel.Server.Data.ConDataContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<DollyHotel.Server.Models.ConData.BedType> GetBedTypes()
        {
            var items = this.context.BedTypes.AsQueryable<DollyHotel.Server.Models.ConData.BedType>();
            this.OnBedTypesRead(ref items);

            return items;
        }

        partial void OnBedTypesRead(ref IQueryable<DollyHotel.Server.Models.ConData.BedType> items);

        partial void OnBedTypeGet(ref SingleResult<DollyHotel.Server.Models.ConData.BedType> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/ConData/BedTypes(BedTypeID={BedTypeID})")]
        public SingleResult<DollyHotel.Server.Models.ConData.BedType> GetBedType(long key)
        {
            var items = this.context.BedTypes.Where(i => i.BedTypeID == key);
            var result = SingleResult.Create(items);

            OnBedTypeGet(ref result);

            return result;
        }
        partial void OnBedTypeDeleted(DollyHotel.Server.Models.ConData.BedType item);
        partial void OnAfterBedTypeDeleted(DollyHotel.Server.Models.ConData.BedType item);

        [HttpDelete("/odata/ConData/BedTypes(BedTypeID={BedTypeID})")]
        public IActionResult DeleteBedType(long key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.BedTypes
                    .Where(i => i.BedTypeID == key)
                    .Include(i => i.RoomTypes)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<DollyHotel.Server.Models.ConData.BedType>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnBedTypeDeleted(item);
                this.context.BedTypes.Remove(item);
                this.context.SaveChanges();
                this.OnAfterBedTypeDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnBedTypeUpdated(DollyHotel.Server.Models.ConData.BedType item);
        partial void OnAfterBedTypeUpdated(DollyHotel.Server.Models.ConData.BedType item);

        [HttpPut("/odata/ConData/BedTypes(BedTypeID={BedTypeID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutBedType(long key, [FromBody]DollyHotel.Server.Models.ConData.BedType item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.BedTypes
                    .Where(i => i.BedTypeID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<DollyHotel.Server.Models.ConData.BedType>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnBedTypeUpdated(item);
                this.context.BedTypes.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.BedTypes.Where(i => i.BedTypeID == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Hotel");
                this.OnAfterBedTypeUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/ConData/BedTypes(BedTypeID={BedTypeID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchBedType(long key, [FromBody]Delta<DollyHotel.Server.Models.ConData.BedType> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.BedTypes
                    .Where(i => i.BedTypeID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<DollyHotel.Server.Models.ConData.BedType>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnBedTypeUpdated(item);
                this.context.BedTypes.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.BedTypes.Where(i => i.BedTypeID == key);
                Request.QueryString = Request.QueryString.Add("$expand", "Hotel");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnBedTypeCreated(DollyHotel.Server.Models.ConData.BedType item);
        partial void OnAfterBedTypeCreated(DollyHotel.Server.Models.ConData.BedType item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] DollyHotel.Server.Models.ConData.BedType item)
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

                this.OnBedTypeCreated(item);
                this.context.BedTypes.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.BedTypes.Where(i => i.BedTypeID == item.BedTypeID);

                Request.QueryString = Request.QueryString.Add("$expand", "Hotel");

                this.OnAfterBedTypeCreated(item);

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
