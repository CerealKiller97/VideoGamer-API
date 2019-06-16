using System;
using System.Threading.Tasks;
using Aplication.Interfaces;
using Aplication.Searches;
using EntityConfiguration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedModels.DTO;
using SharedModels.Fluent.Developer;

namespace MVC.Controllers
{
	public class DevelopersController : Controller
    {

		private readonly IDeveloperService developerService;
		private readonly VideoGamerDbContext _context;


		public DevelopersController(IDeveloperService service, VideoGamerDbContext context)
		{
			developerService = service;
			_context = context;
		}


        // GET: Developers
        public async Task<ActionResult> Index(DeveloperSearchRequest request)
        {
			var developers = await developerService.All(request);
			var data = developers.Data;
            return View(data);
        }

        // GET: Developers/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Developers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Developers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateDeveloperDTO dto)
        {
			var validator = new DeveloperFluentValidatior(_context);
			var errors = validator.Validate(dto);

			if (!errors.IsValid)
			{
				TempData["error"] = "Not valid";
				return RedirectToAction(nameof(Create));
			}
            try
            {
				developerService.Create(dto);

                return RedirectToAction(nameof(Index));
            }
            catch(Exception e)
            {
                return View();
            }
        }

		// GET: Developers/Edit/5
		public async Task<ActionResult> Edit(int id)
        {
			var developer = await developerService.Find(id);
            return View(developer);
        }

        // POST: Developers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, CreateDeveloperDTO dto)
        {

            try
            {
				var validator = new DeveloperFluentValidatior(_context);
				var errors = validator.Validate(dto);

				if (!errors.IsValid)
				{
					TempData["error"] = "Not valid";
					return RedirectToAction(nameof(Edit));
				}

				await developerService.Update(id, dto);

				return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Developers/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Developers/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}