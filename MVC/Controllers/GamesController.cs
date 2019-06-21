using Aplication.Exceptions;
using Aplication.Helpers;
using Aplication.Interfaces;
using Aplication.Searches;
using EntityConfiguration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedModels.DTO;
using SharedModels.DTO.Developer;
using SharedModels.DTO.Game;
using SharedModels.DTO.Publisher;
using SharedModels.Fluent.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Controllers
{
	public class GamesController : Controller
    {
		private readonly IGameService _gameService;
		private readonly IPublisherService _publisherService;
		private readonly IDeveloperService _developerService;

		private readonly VideoGamerDbContext _context;
		
		public GamesController(IGameService gameService, 
			IPublisherService publisherService, 
			IDeveloperService developerService, 
			VideoGamerDbContext context)
		{
			_gameService = gameService;
			_publisherService = publisherService;
			_developerService = developerService;
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
        public async Task<ActionResult> Create()
        {
			var publisherRequest = new PublisherSearchRequest {
				PerPage = 500
			};

			var developerRequest = new DeveloperSearchRequest {
				PerPage = 500
			};

			var publishers = await _publisherService.All(publisherRequest);
			var developers = await _developerService.All(developerRequest);
			ViewData["publishers"] = (List<Publisher>) publishers.Data;
			ViewData["developers"] = (List<Developer>) developers.Data;
			return View();
        }

        // POST: Games/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([FromForm] CreateGameDTODataAnnotations dto, IFormFile Path)
        {
			try
			{
				var dtoNew = new CreateGameDTO {
					AgeLabel = dto.AgeLabel,
					DeveloperId = dto.DeveloperId,
					Engine = dto.Engine,
					GameMode = dto.GameMode,
					Name = dto.Name,
					Path = Path,
					PublisherId = dto.PublisherId,
					ReleaseDate = dto.ReleaseDate,
					UserId = dto.UserId
				};
				// TODO: Add insert logic here
				await _gameService.Create(dtoNew);
				return RedirectToAction(nameof(Index));
			} catch (Exception e)
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