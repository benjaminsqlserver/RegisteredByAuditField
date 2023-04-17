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
    [Route("odata/ConData/PaymentStatuses")]
    public partial class PaymentStatusesController : ODataController
    {
        private DollyHotel.Server.Data.ConDataContext context;

        public PaymentStatusesController(DollyHotel.Server.Data.ConDataContext context)
        {
            this.context = context;
        }

    
        [HttpGet]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IEnumerable<DollyHotel.Server.Models.ConData.PaymentStatus> GetPaymentStatuses()
        {
            var items = this.context.PaymentStatuses.AsQueryable<DollyHotel.Server.Models.ConData.PaymentStatus>();
            this.OnPaymentStatusesRead(ref items);

            return items;
        }

        partial void OnPaymentStatusesRead(ref IQueryable<DollyHotel.Server.Models.ConData.PaymentStatus> items);

        partial void OnPaymentStatusGet(ref SingleResult<DollyHotel.Server.Models.ConData.PaymentStatus> item);

        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        [HttpGet("/odata/ConData/PaymentStatuses(PaymentStatusID={PaymentStatusID})")]
        public SingleResult<DollyHotel.Server.Models.ConData.PaymentStatus> GetPaymentStatus(int key)
        {
            var items = this.context.PaymentStatuses.Where(i => i.PaymentStatusID == key);
            var result = SingleResult.Create(items);

            OnPaymentStatusGet(ref result);

            return result;
        }
        partial void OnPaymentStatusDeleted(DollyHotel.Server.Models.ConData.PaymentStatus item);
        partial void OnAfterPaymentStatusDeleted(DollyHotel.Server.Models.ConData.PaymentStatus item);

        [HttpDelete("/odata/ConData/PaymentStatuses(PaymentStatusID={PaymentStatusID})")]
        public IActionResult DeletePaymentStatus(int key)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                var items = this.context.PaymentStatuses
                    .Where(i => i.PaymentStatusID == key)
                    .Include(i => i.RoomBookings)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<DollyHotel.Server.Models.ConData.PaymentStatus>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnPaymentStatusDeleted(item);
                this.context.PaymentStatuses.Remove(item);
                this.context.SaveChanges();
                this.OnAfterPaymentStatusDeleted(item);

                return new NoContentResult();

            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnPaymentStatusUpdated(DollyHotel.Server.Models.ConData.PaymentStatus item);
        partial void OnAfterPaymentStatusUpdated(DollyHotel.Server.Models.ConData.PaymentStatus item);

        [HttpPut("/odata/ConData/PaymentStatuses(PaymentStatusID={PaymentStatusID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PutPaymentStatus(int key, [FromBody]DollyHotel.Server.Models.ConData.PaymentStatus item)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.PaymentStatuses
                    .Where(i => i.PaymentStatusID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<DollyHotel.Server.Models.ConData.PaymentStatus>(Request, items);

                var firstItem = items.FirstOrDefault();

                if (firstItem == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                this.OnPaymentStatusUpdated(item);
                this.context.PaymentStatuses.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.PaymentStatuses.Where(i => i.PaymentStatusID == key);
                ;
                this.OnAfterPaymentStatusUpdated(item);
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        [HttpPatch("/odata/ConData/PaymentStatuses(PaymentStatusID={PaymentStatusID})")]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult PatchPaymentStatus(int key, [FromBody]Delta<DollyHotel.Server.Models.ConData.PaymentStatus> patch)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var items = this.context.PaymentStatuses
                    .Where(i => i.PaymentStatusID == key)
                    .AsQueryable();

                items = Data.EntityPatch.ApplyTo<DollyHotel.Server.Models.ConData.PaymentStatus>(Request, items);

                var item = items.FirstOrDefault();

                if (item == null)
                {
                    return StatusCode((int)HttpStatusCode.PreconditionFailed);
                }
                patch.Patch(item);

                this.OnPaymentStatusUpdated(item);
                this.context.PaymentStatuses.Update(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.PaymentStatuses.Where(i => i.PaymentStatusID == key);
                ;
                return new ObjectResult(SingleResult.Create(itemToReturn));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        partial void OnPaymentStatusCreated(DollyHotel.Server.Models.ConData.PaymentStatus item);
        partial void OnAfterPaymentStatusCreated(DollyHotel.Server.Models.ConData.PaymentStatus item);

        [HttpPost]
        [EnableQuery(MaxExpansionDepth=10,MaxAnyAllExpressionDepth=10,MaxNodeCount=1000)]
        public IActionResult Post([FromBody] DollyHotel.Server.Models.ConData.PaymentStatus item)
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

                this.OnPaymentStatusCreated(item);
                this.context.PaymentStatuses.Add(item);
                this.context.SaveChanges();

                var itemToReturn = this.context.PaymentStatuses.Where(i => i.PaymentStatusID == item.PaymentStatusID);

                ;

                this.OnAfterPaymentStatusCreated(item);

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
