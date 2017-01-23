using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using OkeTask.Models;
using OkeTask.XmlHelpers;
using PagedList;

namespace OkeTask.Controllers
{

	public class HomeController : Controller
	{
		readonly XmlCountrySerializerHelper _xmlCountryHelper = new XmlCountrySerializerHelper();

		public ActionResult Index(int? page)
		{
			var pageSize = 2;
			var pageNumber = page ?? 1;
			var countries = _xmlCountryHelper.GetAll("Countries.xml");
			return countries == null ? View("EmptyBase") : View(((List<Country>)countries).ToPagedList(pageNumber, pageSize));
		}

		public ActionResult Details(Guid id)
		{
			var country = _xmlCountryHelper.Get("Countries.xml", id);
			return PartialView("_Details", country);
		}

		public ActionResult Insert()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Insert(Country country)
		{
			if (!ModelState.IsValid) return View();

			country.Id = Guid.NewGuid();
			_xmlCountryHelper.Insert("Countries.xml", country);
			return RedirectToAction("Index");
		}

		public ActionResult Import()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Import(HttpPostedFileBase fileUpload)
		{
			_xmlCountryHelper.Import("Countries.xml", fileUpload);

			if (!ModelState.IsValid) return View();
			return RedirectToAction("Index");
		}

		public ActionResult CreateTestsBase()
		{
			var countries = new List<Country>
			{
				new Country
				{
					Id = Guid.NewGuid(),
					Name = "Polska",
					Capital = "Warszawa",
					Language = "Polski",
					Population = 38000,
					Area = 312679
				},
				new Country
				{
					Id = Guid.NewGuid(),
					Name = "Niemcy",
					Capital = "Berlin",
					Language = "Niemiecki",
					Population = 600000,
					Area = 712679
				},
				new Country
				{
					Id = Guid.NewGuid(),
					Name = "Anglia",
					Capital = "Londyn",
					Language = "Angielski",
					Population = 31232,
					Area = 213123
				}
			};
			_xmlCountryHelper.Create("Countries.xml", countries);
			return RedirectToAction("Index");
		}
	}

}
