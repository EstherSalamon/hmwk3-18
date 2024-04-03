using Microsoft.AspNetCore.Mvc;
using PeopleListJSLoading.Web.Models;
using System.Diagnostics;

namespace PeopleListJSLoading.Web.Controllers
{
    public class HomeController : Controller
    {
        private string _connectionString = @"Data Source=.\sqlexpress; Initial Catalog=People; Integrated Security=true;";

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetAllPeople()
        {
            PersonRepository pr = new PersonRepository(_connectionString);
            List<Person> people = pr.GetAllPeople();

            return Json(people);
        }

        [HttpPost]
        public IActionResult AddPerson(Person p)
        {
            PersonRepository repo = new PersonRepository(_connectionString);
            repo.AddPerson(p);

            return Redirect("/");
        }

        [HttpPost]
        public IActionResult DeletePerson(int id)
        {
            PersonRepository repo = new PersonRepository(_connectionString);
            repo.DeletePerson(id);

            return Redirect("/");
        }

        public IActionResult GetByID(int id)
        {
            PersonRepository repo = new PersonRepository(_connectionString);
            Person p = repo.GetByID(id);

            return Json(p);
        }

        [HttpPost]
        public IActionResult UpdatePerson(Person p)
        {
            PersonRepository repo = new PersonRepository(_connectionString);
            repo.EditPerson(p);

            return Redirect("/");
        }
    }
}