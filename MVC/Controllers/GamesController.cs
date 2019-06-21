using Aplication.Exceptions;
using Aplication.FileUpload;
using Aplication.Helpers;
using Aplication.Interfaces;
using Aplication.Searches;
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
		private readonly IConfiguration _configuration;
		private readonly IFileService _fileService;

		private readonly VideoGamerDbContext _context;
		
		public GamesController(IGameService gameService, 
			IPublisherService publisherService, 
			IDeveloperService developerService, 
			VideoGamerDbContext context, IConfiguration configuration,
			IFileService service)
		{
			_gameService = gameService;
			_publisherService = publisherService;
			_developerService = developerService;
			_context = context;
			_configuration = configuration;
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
				var (Server, FilePath) = await _fileService.Upload(Path);

				var dtoNew = new CreateGameDTO {
					AgeLabel = dto.AgeLabel,
					DeveloperId = dto.DeveloperId,
					Engine = dto.Engine,
					GameMode = dto.GameMode,
					Name = dto.Name,
					Path = Server,
					PublisherId = dto.PublisherId,
					ReleaseDate = dto.ReleaseDate,
					UserId = dto.UserId,
					FilePath = FilePath
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
				var developer = await _gameService.Find(id);
				return View(developer);
			} catch (EntityNotFoundException e) {
				TempData["error"] = e.Message;
				return RedirectToAction(nameof(Index));
			} catch (Exception) {
				TempData["error"] = "Exception";
				return RedirectToAction(nameof(Index));
			}
		}

        // POST: Games/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] CreateGameDTODataAnnotations dto)
        {
			if (!ModelState.IsValid)
			{
				TempData["error"] = "Please fill all blank boxes."; //mapped.ToString();
				return RedirectToAction(nameof(Edit), new { id });
			}
			try
			{
				var newDto = new CreateGameDTO
				{
					
				};
				// TODO: Add update logic here
				await _gameService.Update(id, newDto);
				return RedirectToAction(nameof(Index));
			} catch (EntityNotFoundException e)
			{
				TempData["error"] = e.Message;
				return RedirectToAction(nameof(Index));
			} catch (Exception e)
			{
				string error = e.InnerException.Message;
				
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
