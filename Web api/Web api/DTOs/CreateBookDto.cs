namespace Web_api.DTOs
{
    public class CreateBookDto
    {
        public string Title { get; set; } = "";
        public string Author { get; set; } = "";
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
    }
}
