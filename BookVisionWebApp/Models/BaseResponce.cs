namespace BookVisionWebApp.Models
{
    public class BaseResponce<T>
    {
        public bool IsOkay { get; set; }
        public string? ErrorMessage { get; set; }
        public T? Data { get; set; }
    }
}
