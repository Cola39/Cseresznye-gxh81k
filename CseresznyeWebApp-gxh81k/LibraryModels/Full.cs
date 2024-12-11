namespace CseresznyeWebApp_gxh81k.LibraryModels
{
    public class Full
    {
        public int BookId { get; set; }

        public string Title { get; set; } = null!;

        public int? AuthorId { get; set; }

        public string Author { get; set; } = null!;

        public int? GenreId { get; set; }

        public string Genre { get; set; } = null!;
    }
}
