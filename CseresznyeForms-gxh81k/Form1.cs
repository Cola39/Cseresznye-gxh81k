using CseresznyeForms_gxh81k.LibraryModels;
using System.DirectoryServices.ActiveDirectory;

namespace CseresznyeForms_gxh81k
{
    public partial class Form1 : Form
    {
        OpenLibraryContext context = new OpenLibraryContext();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var list = (from x in context.Books
                        select new Full
                        {
                            BookId = x.BookId,
                            Title = x.Title,
                            AuthorId = x.AuthorId,
                            Author = x.Author.Name,
                            GenreId = x.GenreId,
                            Genre = x.Genre.GenreName
                        }).ToList();

            fullBindingSource.DataSource = list;
        }

        private void textBoxFilter_TextChanged(object sender, EventArgs e)
        {
            var list = (from x in context.Books
                        where x.Title.Contains(textBoxFilter.Text)
                        select new Full
                        {
                            BookId = x.BookId,
                            Title = x.Title,
                            AuthorId = x.AuthorId,
                            Author = x.Author.Name,
                            GenreId = x.GenreId,
                            Genre = x.Genre.GenreName
                        }).ToList();

            fullBindingSource.DataSource = list;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult close = MessageBox.Show("Biztos be akarja zárni az ablakot?", "Kilépés", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (close != DialogResult.Yes) { e.Cancel = true; }
        }
    }
}