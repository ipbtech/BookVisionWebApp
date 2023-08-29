namespace BookVisionWebApp.Models
{
    public static class MyHelper
    {
        public static string GetPathToImageFile(IFormFile file)
        {
            return Path.Combine(Environment.CurrentDirectory, "wwwroot\\book_images", file.FileName);
        }
        public static string GetPathToImageFileForRender(string path)
        {
            var data = path.Split("wwwroot");
            return data[1].Replace("\\", "/");
        }
        public static async Task SendFileToServerAsync(Book book)
        {
            using (FileStream fs = new FileStream(book.PathToImageFile, FileMode.Create))
            {
                await book.ImageFile.CopyToAsync(fs);
            }
        }
    }
}
