namespace CompanyMvc.Utilities
{
    public static class DocumentSetting
    {
        public static string UploadFile(IFormFile file, string folderName)
        {
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Files", folderName);

            var fileName = $"{Guid.NewGuid()}-{file.FileName}";

            var filePath = Path.Combine(folderPath, fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }
            return fileName;
        }

        public static void DeleteFile(string filename, string foldername)
        {
            var filepath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Files", foldername, filename);
            if (File.Exists(filepath))
                File.Delete(filepath);
        }


    }
}
