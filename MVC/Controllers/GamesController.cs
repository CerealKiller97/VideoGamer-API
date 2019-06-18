using Aplication.Exceptions;
using Aplication.Helpers;
using Aplication.Interfaces;
using Aplication.Searches;
using EntityConfiguration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedModels.DTO.Game;
using SharedModels.Fluent.Game;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Controllers
{
	public class GamesController : Controller
    {
		private readonly IGameService _gameService;
		private readonly VideoGamerDbContext _context;
		
		public GamesController(IGameService gameService, VideoGamerDbContext context)
		{
			_gameService = gameService;
			_context = context;
		}

		// GET: Games
		public async Task<ActionResult> Index([FromQuery] GameSearchRequest request)
        {
			int total = await _gameService.Count();
			var developers = await _gameService.All(request);
			var data = developers.Data;
			ViewData["total"] = total;
			ViewData["pagesCount"] = developers.PagesCount;
			return View(data);
        }

        // GET: Games/Details/5
        public async Task<ActionResult> Details(int id)
        {
			try{
				var game = await _gameService.Find(id);
				return View(game);
			} catch (EntityNotFoundException e) {
				TempData["error"] = e.Message;
				return RedirectToAction(nameof(Index));
			} catch (Exception) {
				TempData["error"] = ServerErrorResponse.Message;
				return RedirectToAction(nameof(Index));
			}
        }

        // GET: Games/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Games/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([FromForm] CreateGameDTODataAnnotations dto)
        {
			if (!ModelState.IsValid)
			{
				return RedirectToAction(nameof(Create));
			}

			try
			{
				var dtoNew = new CreateGameDTO {
					AgeLabel = dto.AgeLabel,
					DeveloperId = dto.DeveloperId,
					Engine = dto.Engine,
					GameMode = dto.GameMode,
					Name = dto.Name,
					Path = dto.Path,
					PublisherId = dto.PublisherId,
					ReleaseDate = dto.ReleaseDate,
					UserId = dto.UserId
				};
				// TODO: Add insert logic here
				await _gameService.Create(dtoNew);
				return RedirectToAction(nameof(Index));
			} catch (Exception)
			{
				TempData["error"] = "Exception";
				return RedirectToAction(nameof(Index));
			}
		}

        // GET: Games/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Games/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Games/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Games/Delete/5
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