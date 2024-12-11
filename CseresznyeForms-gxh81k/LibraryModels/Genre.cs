using System;
using System.Collections.Generic;

namespace CseresznyeForms_gxh81k.LibraryModels;

public partial class Genre
{
    public int GenreId { get; set; }

    public string GenreName { get; set; } = null!;

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
