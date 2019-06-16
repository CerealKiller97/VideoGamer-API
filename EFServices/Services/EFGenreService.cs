using System.Linq;
using System.Threading.Tasks;
using Aplication.Exceptions;
using Aplication.Helpers.MyComicList.Application.Helpers;
using Aplication.Interfaces;
using Aplication.Pagination;
using Aplication.Searches;
using EntityConfiguration;
using Microsoft.EntityFrameworkCore;
using SharedModels.DTO.Genre;

namespace EFServices.Services
{
	public class EFGenreService : BaseService<Domain.Genre, GenreSearchRequest>, IGenreService
	{
		public EFGenreService(VideoGamerDbContext context) : base(context)
		{
		}

		public async Task<PagedResponse<Genre>> All(GenreSearchRequest request)
		{
			var query = _context.Genres.AsQueryable();

			var buildedQuery = BuildQuery(query, request);

			return buildedQuery.Select(genre => new Genre
			{
				Id = genre.Id,
				Name = genre.Name
			}).Paginate(request.PerPage, request.Page);

		}

		public async Task<int> Count() => await _context.Genres.CountAsync();

		public async Task Create(CreateGenreDTO dto)
		{
			await _context.Genres.AddAsync(new Domain.Genre
			{
				Name = dto.Name
			});

			await _context.SaveChangesAsync();
		}

		public async Task Delete(int id)
		{
			var genre = await _context.Genres.FirstOrDefaultAsync(g => g.Id == id);

			if (genre == null)
			{
				throw new EntityNotFoundException("Genre");
			}

			_context.Remove(genre);
			await _context.SaveChangesAsync();
		}

		public async Task<Genre> Find(int id)
		{
			var genre = await _context.Genres.FirstOrDefaultAsync(g => g.Id == id);

			if (genre == null)
			{
				throw new EntityNotFoundException("Genre");
			}

			return new Genre
			{
				Id = genre.Id,
				Name = genre.Name
			};
		}

		public async Task Update(int id, CreateGenreDTO dto)
		{
			var genre = await _context.Genres.FirstOrDefaultAsync(g => g.Id == id);

			if (genre == null)
			{
				throw new EntityNotFoundException("Genre");
			}

			if (genre.Name != dto.Name)
			{
				genre.Name = dto.Name;
			}

			_context.Entry(genre).State = EntityState.Modified;

			await _context.SaveChangesAsync();
		}

		protected override IQueryable<Domain.Genre> BuildQuery(IQueryable<Domain.Genre> query, GenreSearchRequest request)
		{
			if (request.Name != null)
			{
				string keyword = request.Name.ToLower();
				query = query.Where(q => q.Name.ToLower().Contains(keyword));
			}

			return query;
		}
	}
}
