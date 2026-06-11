namespace FinalExam.Utilities.Extensions
{
    public static class FileUploadExtensios
    {
        public static string SaveImage(this IFormFile formFile, IWebHostEnvironment environment, string folder)
        {
            string path = Path.Combine(environment.WebRootPath, folder);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string fileName = Guid.NewGuid().ToString() + "_" + formFile.FileName;

            string fullPath = Path.Combine(path, fileName);

            using (FileStream stream = new FileStream (fullPath, FileMode.Create))
            {
                formFile.CopyTo (stream);
            }

            return fileName;
        }

        public static string DeleteImage(this string formUrl, IWebHostEnvironment environment, string folder)
        {
            string path = Path.Combine(environment.WebRootPath, folder);
            string fullPath = Path.Combine(path, formUrl);
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
            return formUrl;
        }
    }
}
