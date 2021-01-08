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
            IList<Aluno> lista = _alunoBLL.GetAlunos();
            return View(lista);
        }

        public ActionResult ListaAlunos()
        {
            IList<Aluno> lista = _alunoBLL.GetAlunos();
            return View(lista);
        }

        public ActionResult Create()
        {
            return View();
        }

        /*
        [HttpPost]
        public ActionResult Create(FormCollection formulario)
        {
            Aluno aluno = new Aluno();
            aluno.Nome = formulario["Nome"];
            aluno.Email = formulario["Email"];
            aluno.Idade = Convert.ToInt32(formulario["Idade"]);
            aluno.DataInscricao = Convert.ToDateTime(formulario["DataInscricao"]);
            aluno.Sexo = formulario["Sexo"];

            _alunoBLL.InserirAluno(aluno);
            return RedirectToAction("Index");
        }*/
        
        /*
        [HttpPost]
        [ActionName("Create")]
        public ActionResult CreateAluno()
        {
            Aluno aluno = new Aluno();
            UpdateModel(aluno); // Lança uma exceção quando não preenchido os valores
          
            if (ModelState.IsValid)
            {                
                _alunoBLL.InserirAluno(aluno);
                return RedirectToAction("Index");
            }

            return View();
        }
        */
        /*
        [HttpPost]
        [ActionName("Create")]
        public ActionResult CreateAluno()
        {
            Aluno aluno = new Aluno();
            TryUpdateModel(aluno);

            if (ModelState.IsValid)
            {                
                _alunoBLL.InserirAluno(aluno);
                return RedirectToAction("Index");
            }

            return View();
        }*/

        [HttpPost]
        [ActionName("Create")]
        public ActionResult CreateAluno(Aluno aluno)
        {
            if (ModelState.IsValid)
            {
                _alunoBLL.InserirAluno(aluno);
                return RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult Edit(int id)
        {
            Aluno aluno = _alunoBLL.GetAlunoPorId(id);
            return View(aluno);
        }

        /*[HttpPost]
        public ActionResult Edit(Aluno aluno)
        {
            if (ModelState.IsValid)
            {
                _alunoBLL.AlteararAluno(aluno);
                return RedirectToAction("Index");
            }

            return View();
        }*/

        /*
        [HttpPost, ActionName("Edit")]
        public ActionResult EditAluno(int id)
        {
            Aluno aluno = _alunoBLL.GetAlunoPorId(id);
            //UpdateModel(aluno, includeProperties: new[] { "Id", "Email", "Idade", "DataInscricao", "Sexo" });
            UpdateModel(aluno, null, null, excludeProperties: new[] { "Nome" });

            if (ModelState.IsValid)
            {
                _alunoBLL.AlteararAluno(aluno);
                return RedirectToAction("Index");
            }

            return View();
        }*/

        /*[HttpPost, ActionName("Edit")]
        public ActionResult EditAluno([Bind(Include = "Id,Email,Idade,DataInscricao,Sexo")]int id)
        {
            Aluno aluno = _alunoBLL.GetAlunoPorId(id);
            aluno.Nome = _alunoBLL.GetAlunoPorId(aluno.Id).Nome;

            if (ModelState.IsValid)
            {
                _alunoBLL.AlteararAluno(aluno);
                return RedirectToAction("Index");
            }

            return View();
        }*/

        [HttpPost, ActionName("Edit")]
        public ActionResult EditAluno([Bind(Exclude = "Nome")]int id)
        {
            Aluno aluno = _alunoBLL.GetAlunoPorId(id);
            TryUpdateModel(aluno);
            aluno.Nome = _alunoBLL.GetAlunoPorId(aluno.Id).Nome;
            

            if (ModelState.IsValid)
            {
                _alunoBLL.AlteararAluno(aluno);
                return RedirectToAction("Index");
            }

            return View();
        }

        /*
        public ActionResult Delete(int id)
        {
            Aluno aluno = _alunoBLL.GetAlunoPorId(id);
            return View(aluno);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteAluno(int id)
        {
            _alunoBLL.DeletarAluno(id);
            return RedirectToAction("Index");
        }*/

        [HttpPost]
        public ActionResult Delete(int id)
        {
            _alunoBLL.DeletarAluno(id);
            return RedirectToAction("Index");
        }
    
        
        public ActionResult Details(int id)
        {
            Aluno aluno = _alunoBLL.GetAlunoPorId(id);
            return View(aluno);
        }

        public ActionResult Procurar(string procurarPor, string criterio)
        {
            Aluno aluno = null;
            if (procurarPor.Equals("Nome"))
            {
                aluno = _alunoBLL.GetAlunos().SingleOrDefault(x => x.Nome.Equals(criterio) || criterio == null);
            }
            else
            {
                aluno = _alunoBLL.GetAlunos().SingleOrDefault(x => x.Email.Equals(criterio) || criterio == null);
            }

            return View(aluno);
        }

    }
}