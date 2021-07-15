using Newtonsoft.Json;
using Stripe;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Security;

namespace WebApi.Controllers
{
    [RoutePrefix("api/webhook")]
    public class WebHookController : ApiController
    {
        [HttpPost]
        [Route("checkout")]
        public async Task<IHttpActionResult> Post()
        {
            var json = await this.ActionContext.Request.Content.ReadAsStringAsync();
            try
            {
                var stripeEvent = EventUtility.ConstructEvent(json,
                this.ActionContext.Request.Headers.GetValues("Stripe-Signature").ElementAt(0),
					ConfigurationManager.AppSettings["wh_session_successed"]);
                if (stripeEvent.Type == Events.CheckoutSessionCompleted)
                {
                    var checkoutSession = stripeEvent.Data.Object as Stripe.Checkout.Session;
                    //TODO CUSTOM CODE WITH METADATA
					//checkoutSession.Metadata["value1"]
					//checkoutSession.Metadata["value2"]
                    //READ CHECKOUT FOR ITEMS
                }
                return Ok();
            }
            catch (StripeException ex)
            {
                return BadRequest();
            }
            catch (DbUpdateException ex)
            {
                return BadRequest();
            }
        }
    }
}
