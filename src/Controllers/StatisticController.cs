using Microsoft.AspNetCore.Mvc;

namespace ConfiMicToggler.Controllers
{
    /// <summary>
    /// Contains a statistic
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    public class StatisticController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
    }
}
