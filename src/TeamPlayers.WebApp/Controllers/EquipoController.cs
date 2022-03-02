using Microsoft.AspNetCore.Mvc;

namespace TeamPlayers.WebApp.Controllers
{
    public class EquipoController : Controller
    {
        [Route("equipo")]
        public ActionResult Mantenimiento()
        {
            return View();
        }

        #region APIs
        #endregion
    }
}
