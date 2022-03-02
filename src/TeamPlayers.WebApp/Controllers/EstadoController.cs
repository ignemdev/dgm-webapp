using Microsoft.AspNetCore.Mvc;

namespace TeamPlayers.WebApp.Controllers
{
    public class EstadoController : Controller
    {
        [Route("estado")]
        public ActionResult Mantenimiento()
        {
            return View();
        }

        #region APIs
        #endregion
    }
}
