using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Aplication.FileUpload
{
	public class FileUploadService : IFileService
	{
		public async Task Remove(string path)
		{
			string rootFolder = Directory.GetCurrentDirectory();

			File.Delete(Path.Combine(
				Directory.GetParent(rootFolder) + "/MVC", "wwwroot/images", path));
		}
		public async Task<string> Upload(IFormFile file)
		{
			List<string> allowedTypes = new List<string>()
			{
				".jpg",
				".jpeg",
				".png",
				".gif"
			};

			string fileName = Path.GetFileNameWithoutExtension(file.FileName);
			string fileExtension = Path.GetExtension(file.FileName);

			if (!allowedTypes.Contains(fileExtension))
				throw new Exception("Invalid file format.");

			string fullName = $"{Guid.NewGuid()}_{fileName}{fileExtension}";

			string path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()) + "/MVC", "wwwroot/images", fullName);
			using (var fileStream = new FileStream(path, FileMode.Create))
			{
				await file.CopyToAsync(fileStream);
			}

			return path;
		}
	}
}
