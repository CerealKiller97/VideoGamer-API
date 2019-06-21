using Aplication.Exceptions;
using Aplication.Helpers;
using Aplication.Interfaces;
using Aplication.Searches;
using EntityConfiguration;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using SharedModels.DTO.Developer;

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
		public async Task<ActionResult> Create([FromForm] CreateDeveloperDTODataAnnotations dto)
        {
			if (!ModelState.IsValid)
			{
				TempData["error"] = "Error: Please fill in blank boxes";
				return RedirectToAction(nameof(Index));
			}
			try
            {
				var newDto = new CreateDeveloperDTO
				{
					Name = dto.Name,
					Website = dto.Website,
					HQ = dto.HQ,
					Founded = dto.Founded
				};

				await _developerService.Create(newDto);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception e)
            {
				string error = e.InnerException.Message;
				if (error.Contains("IX_Developers_Website"))
				{
					TempData["error"] = "Website already exists.";
				} 
				else
				{
					TempData["error"] = "Name already exists.";
				}
				return RedirectToAction(nameof(Create));
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
        public async Task<ActionResult> Edit(int id, [FromForm] CreateDeveloperDTODataAnnotations dto)
        {

			if (!ModelState.IsValid)
			{
				TempData["error"] = "Please fill all blank boxes."; //mapped.ToString();
				return RedirectToAction(nameof(Edit),new { id });

			}
			try {
				var newDto = new CreateDeveloperDTO
				{
					Name = dto.Name,
					Founded = dto.Founded,
					HQ = dto.HQ,
					Website = dto.Website
				};
				// TODO: Add update logic here
				await _developerService.Update(id, newDto);
                return RedirectToAction(nameof(Index));
            } catch (EntityNotFoundException e) {
				TempData["error"] = e.Message;
				return RedirectToAction(nameof(Index));
			} catch(Exception e) {
				string error = e.InnerException.Message;
				if (error.Contains("IX_Developers_Website"))
				{
					TempData["error"] = "Website already exists.";
				} else
				{
					TempData["error"] = "Name already exists.";
				}
				return RedirectToAction(nameof(Edit));
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