using Aplication.Exceptions;
using Aplication.Helpers;
using Aplication.Interfaces;
using Aplication.Searches;
using EntityConfiguration;
using Microsoft.AspNetCore.Mvc;
using SharedModels.DTO;
using SharedModels.Fluent.Developer;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace MVC.Controllers
{
	public class DevelopersController : Controller
    {
		private readonly IDeveloperService _developerService;
		private readonly VideoGamerDbContext _context;

		public DevelopersController(IDeveloperService developerService, VideoGamerDbContext context)
		{
			_developerService = developerService;
			_context = context;
		}

		// GET: Developers
		public async Task<ActionResult> Index([FromQuery] DeveloperSearchRequest request)
        {
			int total = await _developerService.Count();
			var developers = await _developerService.All(request);
			var data = developers.Data;
			ViewData["total"] = total;
			ViewData["pagesCount"] = developers.PagesCount;
            return View(data);
        }

        // GET: Developers/Details/5
        public async Task<ActionResult> Details(int id)
        {
			try {
				var developer = await _developerService.Find(id);
				return View(developer);
			} catch (EntityNotFoundException e) {
				TempData["error"] = e.Message;
				return RedirectToAction(nameof(Index));
			} catch (Exception) {
				TempData["error"] = ServerErrorResponse.Message;
				return RedirectToAction(nameof(Index));
			}
		}

        // GET: Developers/Create
        public async Task<ActionResult> Create()
        {
            return View();
        }

        // POST: Developers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([FromForm] CreateDeveloperDTO dto)
        {
			var validator = new DeveloperFluentValidatior(_context);
			var errors = await validator.ValidateAsync(dto);
			if (!errors.IsValid)
			{
				var mapped = errors.Errors.Select(x => new
				{
					Name = x.PropertyName,
					Error = x.ErrorMessage
				}).ToArray();

				ViewBag["error"] = mapped;
				return RedirectToAction(nameof(Create));
			}
			try
            {
				// TODO: Add insert logic here
				await _developerService.Create(dto);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception)
            {
				TempData["error"] = "Exception";
				return RedirectToAction(nameof(Index));
            }
        }

        // GET: Developers/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
			try {
				var developer = await _developerService.Find(id);
				return View(developer);
			} catch (EntityNotFoundException e) {
				TempData["error"] = e.Message;
				return RedirectToAction(nameof(Index));
			} catch (Exception) {
				TempData["error"] = "Exception";
				return RedirectToAction(nameof(Index));
			}
		}

        // POST: Developers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [FromForm] CreateDeveloperDTO dto)
        {
			var validator = new DevelopUpdateFluentValidator(_context, id);
			var errors = await validator.ValidateAsync(dto);
			if (!errors.IsValid)
			{
				var mapped = errors.Errors.Select(x => new
				{
					Name = x.PropertyName,
					Error = x.ErrorMessage
				}).ToArray();

				TempData["error"] = "Please fill all blank boxes."; //mapped.ToString();
				return RedirectToAction(nameof(Create));
			}
			try {
				// TODO: Add update logic here
				await _developerService.Update(id, dto);
                return RedirectToAction(nameof(Index));
            } catch (EntityNotFoundException e) {
				TempData["error"] = e.Message;
				return RedirectToAction(nameof(Index));
			} catch {
                return View();
            }
        }

        // POST: Developers/Delete/5
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            try {
				// TODO: Add delete logic here
				await _developerService.Delete(id);
                return RedirectToAction(nameof(Index));
            } catch (EntityNotFoundException e) {
				TempData["error"] = e.Message;
				return RedirectToAction(nameof(Index));
			} catch {
                return View();
            }
        }
    }
}