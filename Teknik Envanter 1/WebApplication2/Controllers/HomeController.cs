using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<Person> People = new List<Person>();
            People.Add( new Person {Id=0,Name = "Nebi Başkaya", Tckn="1111111111", BirthPlace="Manisa", Phone = "12345679"});
            People.Add(new Person { Id = 1, Name = "Taylan Kayıkçı", Tckn = "2222222222", BirthPlace = "Zonguldak", Phone = "23456677" });

            var PeopleDetailList = new List<Person>();
            PeopleDetailList.Add(new Person { Id = 0, Name = "Leyla Deniz Başkaya", Tckn = "0000000000", BirthPlace = "İstanbul", Phone = "12345679" });
            PeopleDetailList.Add(new Person { Id = 0, Name = "Gözde Başkaya", Tckn = "4444444444", BirthPlace = "Erzurum", Phone = "12345679" });
            PeopleDetailList.Add(new Person { Id = 0, Name = "Ertuğrul Başkaya", Tckn = "55555555", BirthPlace = "İstanbul", Phone = "12345679" });
            People[0].People = PeopleDetailList;
            var PeopleDetailList1 = new List<Person>();
            PeopleDetailList1.Add(new Person { Id = 1, Name = "Berk Kayıkçı", Tckn = "333333333", BirthPlace = "İstanbul", Phone = "12345679" });
            People[1].People = PeopleDetailList1;
            return View(People);
        }

        [HttpGet]
        public IActionResult CallModal(int number)
        {
            return PartialView("Partial/_Call", number);
        }

        [HttpPost]
        public IActionResult PersonInfoModal(Person model)
        {
            return PartialView("Partial/_PersonInfos", model);
        }

    }
}
