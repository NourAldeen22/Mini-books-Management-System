using System.Reflection.Metadata.Ecma335;

class Book
{
        private static int Count = 1;
    public int Id {get; private set;}
    public required string Title { get; set; }
    public required string Author { get; set; }

    private int _pages;
    public int Pages 
    { 
        get => _pages;
        set
        {
           
            if(value <= 0 || value >2000 )
            {
                throw new ArgumentOutOfRangeException(nameof(Pages),"pages must be between 1 and 2000 pages.");
            }
            _pages = value;
        }
    }

    private int _year;
    public int Year 
    {
         get => _year; 
         
         
    set
        {
           
            if(value < 1920 || value > DateTime.Now.Year  )
            {
                throw new ArgumentOutOfRangeException(nameof(Year),$"Year must be between 1920 and {DateTime.Now.Year}");
            }
            _year = value;
        } 
    
    }

    public Book() 
    { 
        Id = Count++;
    
    }

    public string DisplayInfo() =>
    $"--- Book Info ---\n" +
    $"ID    :   {Id}\n"+
    $"Title : {Title}\n" +
    $"Author: {Author}\n" +
    $"Pages : {Pages}\n" +
    $"Year  : {Year}\n";
    

}