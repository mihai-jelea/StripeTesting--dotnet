using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stripe;

namespace StripeTesting.Controllers
{
    public class CartController : Controller
    {

        // GET: Cart/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cart/Create
        [HttpPost]
        public ActionResult Create(string stripeToken) 
        {
            try
            {
                // Set your secret key: remember to change this to your live secret key in production
                StripeConfiguration.ApiKey = "sk_test_rtifaQn2xu8zulrcUk83Amhm00jk80eVv7";

                // Token is created using Checkout or Elements!
/*                if (stripeToken == null || stripeToken == "")
                    throw new StripeException(System.Net.HttpStatusCode.BadRequest, StripeError)
*/
                var options = new ChargeCreateOptions
                {
                    Amount = 123,
                    Currency = "usd",
                    Description = "Test charge from .NET | " + DateTime.Now.ToString(),
                    Source = stripeToken,
                };
                var service = new ChargeService();
                Charge charge = service.Create(options);

                ViewBag.Message = charge.Id;

                return View("OrderStatus", (object)charge.Id);
            }
            catch (StripeException e)
            {
                switch (e.StripeError.ErrorType)
                {
                    case "card_error":
                        Console.WriteLine("Code: " + e.StripeError.Code);
                        Console.WriteLine("Message: " + e.StripeError.Message);
                        break;
                    case "api_connection_error":
                        break;
                    case "api_error":
                        break;
                    case "authentication_error":
                        break;
                    case "invalid_request_error":
                        break;
                    case "rate_limit_error":
                        break;
                    case "validation_error":
                        break;
                    default:
                        // Unknown Error Type
                        break;
                }

                return View();
            }
        }

    }
}
