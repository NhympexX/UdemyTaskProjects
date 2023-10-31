using Microsoft.AspNetCore.Mvc;

namespace BankAppUdemy.Controllers
{
    public class HomeController : Controller
    {
        object account = new
        {
            accountNumber = 1001,
            accountHolderName = "Gunduz",
            currentBalance = 5000
        };

        [Route("/")]
        [HttpGet]
        public IActionResult Index()
        {
            return Ok("Welcome to the Best Bank");

        }

        [Route("/account-details")]
        [HttpGet]
        public JsonResult Details()
        {
            HttpContext.Response.StatusCode = 200;
            return Json(account);
        }
        [Route("/account-statement")]
        [HttpGet]
        public IActionResult Statement() {
            HttpContext.Response.StatusCode=200;
            return File("simple.pdf", "application/pdf");
        }
        [Route("/get-current-balance/{accountNumber:int?}")]
        [HttpGet]
        public IActionResult GetNumber()
        {
            if (!HttpContext.Request.RouteValues.ContainsKey("accountNumber"))
            {
                return StatusCode(404, "AccountNumber should be supplied");
            }
            else if (Convert.ToInt32(HttpContext.Request.RouteValues["accountNumber"]) ==Convert.ToInt32(account.GetType().GetProperty("accountNumber").GetValue(account)))
            {
                return Ok(account.GetType().GetProperty("currentBalance").GetValue(account).ToString());
            }
            else
            {
                return BadRequest("accountNumber should be 1001.");
            }
        }
        
    }
}