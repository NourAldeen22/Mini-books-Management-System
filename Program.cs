namespace small_library;

class Program
{
    static void Main(string[] args)
    {
        Library myLibrary = new Library();
        myLibrary.AddBook(new Book { Title = "The Pragmatic Programmer", Author = "Andrew Hunt", Pages = 352, Year = 1999 });
        myLibrary.AddBook(new Book { Title = "Clean Code", Author = "Robert C. Martin", Pages = 464, Year = 2008 });
        myLibrary.AddBook(new Book { Title = "C# in Depth", Author = "Jon Skeet", Pages = 528, Year = 2019 });
        myLibrary.AddBook(new Book { Title = "Head First Design Patterns", Author = "Eric Freeman", Pages = 694, Year = 2004 });
        myLibrary.AddBook(new Book { Title = "Code Complete", Author = "Steve McConnell", Pages = 960, Year = 2004 });
        Menu menu = new Menu(myLibrary);
        menu.Show();

    }
}
