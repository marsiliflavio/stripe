using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Stripe;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Security;

namespace WebApi.Controllers
{
    public class CheckoutController : ApiController
    {
        [HttpPost]
        public async Task<IHttpActionResult> Checkout(DTO.Basket basket)
        {
            return await Task.Run<IHttpActionResult>(() =>
            {
                SessionCreateOptions sessionCreateOptions = new SessionCreateOptions
                {
                    SuccessUrl = "https://redirect_uri/stato/1",
                    CancelUrl = "https://redirect_uri/stato/0",
                    PaymentMethodTypes = new List<string>
                {
                    "card"
                },
                    LineItems = new List<SessionLineItemOptions>(),
                    Metadata = new Dictionary<string, string>()
                {
                    { "value1", "value1" },
                    { "value2", "value2" }
                },
                    Mode = "payment"
                };
				foreach(DTO.Item item in basket)
					sessionCreateOptions.LineItems.Add(new SessionLineItemOptions()
					{
						PriceData = new SessionLineItemPriceDataOptions()
						{
							Currency = "eur",
							UnitAmount = item.price * 100 //CENT,
							ProductData = new SessionLineItemPriceDataProductDataOptions()
							{
								Name = item.name
							}
						},
						Quantity = item.quantity
					});
                return Ok(Guid.NewGuid().ToString())
            });
        }
    }
}
