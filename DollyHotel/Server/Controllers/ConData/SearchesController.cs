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
    [Route("odata/ConData/Searches")]
    public partial class SearchesController : ODataController
    {
        private DollyHotel.Server.Data.ConDataContext context;

        public SearchesController(DollyHotel.Server.Data.ConDataContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<DollyHotel.Server.Models.ConData.Search> GetSearches()
        {
            var items = this.context.Searches.AsQueryable<DollyHotel.Server.Models.ConData.Search>();
            this.OnSearchesRead(ref items);

            return items;
        }

        partial void OnSearchesRead(ref IQueryable<DollyHotel.Server.Models.ConData.Search> items);

        partial void OnSearchGet(ref SingleResult<DollyHotel.Server.Models.ConData.Search> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/ConData/Searches(SearchID={SearchID})")]
        public SingleResult<DollyHotel.Server.Models.ConData.Search> GetSearch(long key)
        {
            var items = this.context.Searches.Where(i => i.SearchID == key);
            var result = SingleResult.Create(items);

            OnSearchGet(ref result);

            return result;
        }
        partial void OnSearchDeleted(DollyHotel.Server.Models.ConData.Search item);
        partial void OnAfterSearchDeleted(DollyHotel.Server.Models.ConData.Search item);

        [HttpDelete("/odata/ConData/Searches(SearchID={SearchID})")]
        public IActionResult DeleteSearch(long key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.Searches
                    .Where(i => i.SearchID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<DollyHotel.Server.Models.ConData.Search>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnSearchDeleted(item);
                this.context.Searches.Remove(item);
                this.context.SaveChanges();
                this.OnAfterSearchDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnSearchUpdated(DollyHotel.Server.Models.ConData.Search item);
        partial void OnAfterSearchUpdated(DollyHotel.Server.Models.ConData.Search item);

        [HttpPut("/odata/ConData/Searches(SearchID={SearchID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutSearch(long key, [FromBody]DollyHotel.Server.Models.ConData.Search item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Searches
                    .Where(i => i.SearchID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<DollyHotel.Server.Models.ConData.Search>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnSearchUpdated(item);
                this.context.Searches.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Searches.Where(i => i.SearchID == key);
                Request.QueryString = Request.QueryString.Add("$expand", "City,AspNetUser");
                this.OnAfterSearchUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/ConData/Searches(SearchID={SearchID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchSearch(long key, [FromBody]Delta<DollyHotel.Server.Models.ConData.Search> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.Searches
                    .Where(i => i.SearchID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<DollyHotel.Server.Models.ConData.Search>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnSearchUpdated(item);
                this.context.Searches.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Searches.Where(i => i.SearchID == key);
                Request.QueryString = Request.QueryString.Add("$expand", "City,AspNetUser");
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnSearchCreated(DollyHotel.Server.Models.ConData.Search item);
        partial void OnAfterSearchCreated(DollyHotel.Server.Models.ConData.Search item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] DollyHotel.Server.Models.ConData.Search item)
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

                this.OnSearchCreated(item);
                this.context.Searches.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.Searches.Where(i => i.SearchID == item.SearchID);

                Request.QueryString = Request.QueryString.Add("$expand", "City,AspNetUser");

                this.OnAfterSearchCreated(item);

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
