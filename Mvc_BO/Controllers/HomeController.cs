using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mvc_BO.Controllers
{
    public class HomeController : Controller
    {
        private AlunoBLL _alunoBLL;

        public HomeController()
        {
            _alunoBLL = new AlunoBLL();
        }
        
        public ActionResult Index()
        {
            IList<Aluno> lista = _alunoBLL.getAlunos();
            return View(lista);
        }
	}
}