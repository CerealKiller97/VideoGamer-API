using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Aplication.FileUpload
{
	public interface IFileService
	{
		Task<(string Server, string FilePath)> Upload(IFormFile file);
		Task Remove(string path);
	}
}
