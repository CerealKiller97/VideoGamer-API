using Aplication.Exceptions;
using Aplication.FileUpload;
using Aplication.Helpers;
using Aplication.Interfaces;
using Aplication.Searches;
using Domain;
using EntityConfiguration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SharedModels.DTO.Developer;
using SharedModels.DTO.Game;
using SharedModels.DTO.Publisher;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVC.Controllers
{
	public class GamesController : Controller
    {
		private readonly IGameService _gameService;
		private readonly IPublisherService _publisherService;
		private readonly IDeveloperService _developerService;
		private readonly IFileService _fileService;

		
		public GamesController(IGameService gameService, 
			IPublisherService publisherService, 
			IDeveloperService developerService, 
			IFileService service)
		{
			_gameService = gameService;
			_publisherService = publisherService;
			_developerService = developerService;
			_fileService = service;
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
			var publisherRequest = new PublisherSearchRequest { PerPage = 500 };

			var developerRequest = new DeveloperSearchRequest { PerPage = 500 };

			var publishers = await _publisherService.All(publisherRequest);
			var developers = await _developerService.All(developerRequest);

			ViewBag.Publishers = (List<SharedModels.DTO.Publisher.Publisher>) publishers.Data;
			ViewBag.Developers = (List<SharedModels.DTO.Developer.Developer>) developers.Data;

			return View();
        }

        // POST: Games/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([FromForm] CreateGameDTODataAnnotations dto, IFormFile Path)
        {
			try
			{
				//var (Server, FilePath) = await _fileService.Upload(Path);

				var dtoNew = new CreateGameDTO {
					AgeLabel = dto.AgeLabel,
					DeveloperId = dto.DeveloperId,
					Engine = dto.Engine,
					GameMode = dto.GameMode,
					Name = dto.Name,
					PublisherId = dto.PublisherId,
					ReleaseDate = dto.ReleaseDate,
					UserId = dto.UserId,
					Path = Path
				};

				await _gameService.Create(dtoNew);
				return RedirectToAction(nameof(Index));
			} catch (Exception e)
			{
				TempData["error"] = e.Message;
				return RedirectToAction(nameof(Index));
			}
		}

        // GET: Games/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
			try {
				var game = await _gameService.Find(id);

				var publisherRequest = new PublisherSearchRequest { PerPage = 500 };

				var developerRequest = new DeveloperSearchRequest { PerPage = 500 };

				var publishers = await _publisherService.All(publisherRequest);
				var developers = await _developerService.All(developerRequest);
				ViewBag.Publishers = (List<SharedModels.DTO.Publisher.Publisher>) publishers.Data;
				ViewBag.Developers = (List<SharedModels.DTO.Developer.Developer>) developers.Data;
				ViewBag.Game = (SharedModels.DTO.Game.Game) game;
				ViewBag.ageLabels = new[] { 3, 7, 12, 16, 18 };

				ViewBag.gameModes = Enum.GetValues(typeof(GameModes));

				return View();
			} catch (EntityNotFoundException e) {
				TempData["error"] = e.Message;
				return RedirectToAction(nameof(Index));
			} catch (Exception e) {
				TempData["error"] = e.Message;
				return RedirectToAction(nameof(Index));
			}
		}

        // POST: Games/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] UpdateGameDataAnnotations dto, IFormFile Path)
        {
			if (!ModelState.IsValid)
			{
				TempData["error"] = "Please fill all blank boxes.";
				return RedirectToAction(nameof(Edit), new { id });
			}

			try
			{
				var newDto = new CreateGameDTO
				{
					AgeLabel = dto.AgeLabel,
					DeveloperId = dto.DeveloperId,
					Engine = dto.Engine,
					GameMode = dto.GameMode,
					Name = dto.Name,
					PublisherId = dto.PublisherId,
					ReleaseDate = dto.ReleaseDate,
					UserId = dto.UserId,
				};

				await _gameService.Update(id, newDto);
				return RedirectToAction(nameof(Index));
			} catch (EntityNotFoundException e) {
				TempData["error"] = e.Message;
				return RedirectToAction(nameof(Index));
			} catch (Exception e) {
				TempData["error"] = "Server error, please try later.";
				return RedirectToAction(nameof(Edit), new { id });
			}
		}

        // GET: Games/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
			try
			{
				await _gameService.Delete(id);
				return RedirectToAction(nameof(Index));
			} catch (EntityNotFoundException e)
			{
				TempData["error"] = e.Message;
				return RedirectToAction(nameof(Index));
			} catch (Exception e)
			{
				TempData["error"] = e.Message;
				return RedirectToAction(nameof(Index));
			}
		}
    }
}
