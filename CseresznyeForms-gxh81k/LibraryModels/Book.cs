using System;
using System.Collections.Generic;

namespace CseresznyeForms_gxh81k.LibraryModels;

public partial class Book
{
    public int BookId { get; set; }

    public string Title { get; set; } = null!;

    public int? AuthorId { get; set; }

    public int? GenreId { get; set; }

    public virtual Author? Author { get; set; }

    public virtual Genre? Genre { get; set; }
}
