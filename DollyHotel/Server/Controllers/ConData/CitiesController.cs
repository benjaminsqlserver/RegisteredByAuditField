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
using Microsoft.AspNetCore.Authorization;

namespace DollyHotel.Server.Controllers.ConData
{
    [Route("odata/ConData/Cities")]
    [Authorize]
    public partial class CitiesController : ODataController
    {
        private DollyHotel.Server.Data.ConDataContext context;

        public CitiesController(DollyHotel.Server.Data.ConDataContext context)
        {
            this.context = context;
        }

        [AllowAnonymous]
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<DollyHotel.Server.Models.ConData.City> GetCities()
        {
            var items = this.context.Cities.AsQueryable<DollyHotel.Server.Models.ConData.City>();
            this.OnCitiesRead(ref items);

            return items;
        }

        partial void OnCitiesRead(ref IQueryable<DollyHotel.Server.Models.ConData.City> items);

        partial void OnCityGet(ref SingleResult<DollyHotel.Server.Models.ConData.City> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/ConData/Cities(CityID={CityID})")]
        public SingleResult<DollyHotel.Server.Models.ConData.City> GetCity(long key)
        {
            var items = this.context.Cities.Where(i => i.CityID == key);
            var result = SingleResult.Create(items);

            OnCityGet(ref result);

            return result;
        }
        partial void OnCityDeleted(DollyHotel.Server.Models.ConData.City item);
        partial void OnAfterCityDeleted(DollyHotel.Server.Models.ConData.City item);

        [HttpDelete("/odata/ConData/Cities(CityID={CityID})")]
        public IActionResult DeleteCity(long key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.Cities
                    .Where(i => i.CityID == key)
                    .Include(i => i.Hotels)
                    .Include(i => i.Searches)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<DollyHotel.Server.Models.ConData.City>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnCityDeleted(item);
                this.context.Cities.Remove(item);
                this.context.SaveChanges();
                this.OnAfterCityDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnCityUpdated(DollyHotel.Server.Models.ConData.City item);
        partial void OnAfterCityUpdated(DollyHotel.Server.Models.ConData.City item);

        [HttpPut("/odata/ConData/Cities(CityID={CityID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutCity(long key, [FromBody]DollyHotel.Server.Models.ConData.City item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Cities
                    .Where(i => i.CityID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<DollyHotel.Server.Models.ConData.City>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnCityUpdated(item);
                this.context.Cities.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Cities.Where(i => i.CityID == key);
                ;
                this.OnAfterCityUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/ConData/Cities(CityID={CityID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchCity(long key, [FromBody]Delta<DollyHotel.Server.Models.ConData.City> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Cities
                    .Where(i => i.CityID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<DollyHotel.Server.Models.ConData.City>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnCityUpdated(item);
                this.context.Cities.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Cities.Where(i => i.CityID == key);
                ;
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnCityCreated(DollyHotel.Server.Models.ConData.City item);
        partial void OnAfterCityCreated(DollyHotel.Server.Models.ConData.City item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] DollyHotel.Server.Models.ConData.City item)
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

                this.OnCityCreated(item);
                this.context.Cities.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Cities.Where(i => i.CityID == item.CityID);

                ;

                this.OnAfterCityCreated(item);

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
