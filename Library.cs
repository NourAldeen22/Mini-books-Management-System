
class Library
{
    private List<Book> books = new List<Book>();

    public Book AddBook(Book book)
    {

        books.Add(book);
        return book;
    }

    public bool PrintAllBooks()
    {
        if (books.Count == 0)
        {

            return false;
        }

        foreach (var book in books)
        {
            Console.WriteLine(book.DisplayInfo());
        }
        return true;
    }
    
    public Book? FindBookByName(string name)
    {
        foreach (Book book in books)
        {
            
            if (book.Title.ToLower().Contains(name.ToLower()))
            {
                return book;
            }
        }
        return null;
    }

    public bool DeleteBook(int id )
    {
        
        var delete = books.FirstOrDefault(b=> b.Id == id);

        if (delete != null)
        {
            books.Remove(delete);
            return true;    
        }
        return false;
    }

    
    public Dictionary<string, int> GetBookeEfterYear (int year)
    {
        var result = books.Where(a => a.Year == year)
            .GroupBy(a => a.Author).
            ToDictionary(g => g.Key, g => g.Count());

            return result;
    }
    
    public bool EditBook(int id  ,Book newBook) 
    {

        var edit = books.FirstOrDefault(x => x.Id == id);

        if (edit == null) 
        {

            return false;
        }
       
 
         edit.Title = newBook.Title;
         edit.Author = newBook.Author;
         edit.Year = newBook.Year;
         edit.Pages = newBook.Pages;
        
      

        return true;

      
    
    
    }
}