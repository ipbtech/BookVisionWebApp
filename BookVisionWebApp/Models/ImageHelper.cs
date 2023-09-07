namespace BookVisionWebApp.Models
{
    public static class ImageHelper
    {
        public static string GetPathToImageFileForServer(IFormFile file)
        {
            if (file != null)
            {
                return Path.Combine(Environment.CurrentDirectory, "wwwroot\\book_images", file.FileName);
            }
            return string.Empty;
        }
        public static string GetPathToImageFileForRender(string path)
        {
            if (!string.IsNullOrEmpty(path))
            {
                var data = path.Split("wwwroot");
                return data[1].Replace("\\", "/");
            }
            return string.Empty;
        }
        public static async Task SendImageFileToServerAsync(Book book)
        {
            if (!string.IsNullOrEmpty(book.PathToImageFile))
            {
                FileStream fs = new FileStream(book.PathToImageFile, FileMode.Create));
                await book.ImageFile.CopyToAsync(fs);
                fs.Close();
            }
        }
        public static void DeleteBookImageOnServer(Book book)
        {
            var path = book.PathToImageFile;
            if (!string.IsNullOrEmpty(path))
                File.Delete(path);
        }
    }
}
