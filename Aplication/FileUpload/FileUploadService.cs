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
				".jpeg"
			};

			string fileName = Guid.NewGuid() + "_" + file.FileName;

			string path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()) + "/MVC", "wwwroot/images", fileName);
			using (var fileStream = new FileStream(path, FileMode.Create))
			{
				await file.CopyToAsync(fileStream);
			}

			return fileName;
		}
	}
}
