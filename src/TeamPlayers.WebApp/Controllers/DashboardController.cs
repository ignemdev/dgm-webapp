using Microsoft.AspNetCore.Mvc;

namespace TeamPlayers.WebApp.Controllers
{
    [Route("/")]
    public class DashboardController : Controller
    {
        [HttpGet("dashboard")]
        [HttpGet("/")]
        [HttpGet("*")]
        public ActionResult Datos()
        {
            return View();
        }
    }
}
