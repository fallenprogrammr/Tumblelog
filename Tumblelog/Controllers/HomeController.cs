using System.Linq;
using System.Web.Mvc;
using Tumblelog.Models;

namespace Tumblelog.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {
            var postsRepo = new SqlCePostRepository();
            return View(postsRepo.GetHomePageModel());
        }

        public ActionResult About() {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}