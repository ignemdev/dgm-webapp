using Microsoft.AspNetCore.Mvc;

namespace TeamPlayers.WebApp.Controllers
{
    public class JugadorController : Controller
    {
        [Route("jugador")]
        public ActionResult Mantenimiento()
        {
            return View();
        }

        #region APIs
        #endregion
    }
}
