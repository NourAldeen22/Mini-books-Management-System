
class Menu
{
    private Library _library;

    public Menu(Library library)
    {
        _library = library;
    }

    public void Show()
    {

        while (true)
        {


            Console.WriteLine("\n Library menu: ");
            Console.WriteLine("1. Add new book");
            Console.WriteLine("2. Show all books");
            Console.WriteLine("3. Search by title");
            Console.WriteLine("4. Edit book");
            Console.WriteLine("5. Delete book");
            Console.WriteLine("6. Search book by year");
            Console.WriteLine("7. Exite ");
            
            switch (SelectOption())

            {

                case 1:
                    AddBook();
                    break;
                case 2:
                    PrintAllBooks();
                    break;
                case 3:
                    SearchByTitle();
                    break;
                case 4:
                    EditBook();
                    break;
                case 5:
                    DeleteBook();
                    break;
                case 6:
                    SearchCountBooksEfterYear();
                    break;
                case 7:
                    Console.WriteLine("Thanks to use app");
                    return;
                default:
                    Console.WriteLine("Envalid option");
                    break;

            }


        }
    }
    
   /// <summary>
    /// Adds a new book to the library by collecting and validating user input for title, author, pages, and year.
    /// </summary>
    /// <remarks>
    /// This method handles input validation and catches potential ArgumentOutOfRangeExceptions 
    /// from the Book model to ensure the application continues running smoothly.
    /// </remarks>    
    private void AddBook()
    {

       try{
        string title = ReadStringWithErrorMessage("Enter title: ", "\nPlease enter a valid title.");
        
        string author =  ReadStringWithErrorMessage("Enter author: " , "\nPlease enter a valid title.");
        
        int pages = ReadIntWithErrorMessage("Enter number of pages: ", "\nInvalid input! Please enter a valid number of page.");

        int year = ReadIntWithErrorMessage("Enter number of year: ", "\nInvalid input! Please enter a valid number year number.");
        
        Book book = new Book()
        {
        
            Title = title,
            Author = author,
            Pages = pages,
            Year = year
        };

        _library.AddBook(book);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Book added");
        Console.ResetColor();
        WaitAndClear();
       }
       catch(ArgumentOutOfRangeException ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error in : {ex.ParamName}: {ex.Message}");
            Console.ResetColor();
            WaitAndClear();
        } 
        
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Unknown error: {ex.Message}");
            Console.ResetColor();
            WaitAndClear();
        }
       

    }

    /// <summary>
    /// The EditBook method allows editing a book by entering its name and returns "No book found" if none exists, 
    /// and returns "Success" upon completion of the edit.
    /// </summary>
    public void EditBook() 
    {
        var showBooks = _library.PrintAllBooks();

        if (!showBooks)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("There are no books to delete.");
            Console.ResetColor();
            WaitAndClear();
            return;
        }
        try{

        int id = ReadIntWithErrorMessage("Enter current id: ", "Invalid id");

        string title = ReadStringWithErrorMessage("Edit title: ", "\nPlease enter a valid title.");

        string author = ReadStringWithErrorMessage("Edit author: ", "\nPlease enter a valid title.");

        int pages = ReadIntWithErrorMessage("Edit number of pages: ", "\nInvalid input! Please enter a valid number of page.");

        int year = ReadIntWithErrorMessage("Edit number of year: ", "\nInvalid input! Please enter a valid number year number.");

        Book editBook = new Book()
        {
          Title = title,
          Author = author,
          Pages = pages,
          Year = year
        };

        bool result = _library.EditBook(id,editBook);

        if (!result)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Book not found");
            Console.ResetColor();


        }
        else 
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Book updated successfully.");
            Console.ResetColor();
        
        }
        WaitAndClear();
        }

        catch(ArgumentOutOfRangeException ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error in : {ex.ParamName}: {ex.Message}");
            Console.ResetColor();
            WaitAndClear();
        
        }
    }

    private void SearchByTitle()
    {
        string title = ReadStringWithErrorMessage("Enter title to Search: ", "\nPlease enter a valid title.");

        var result = _library.FindBookByName(title);

        if (result != null)
        {
            Console.WriteLine(result.DisplayInfo());
        }
        else
        {
            Console.WriteLine("book not found");
        }

        WaitAndClear();
    }

   
     /// <summary>
    /// Searches for a specific book by its title and displays its information if found.
    /// </summary>
    /// <param name="title">The title of the book to search for.</param>
    private void DeleteBook()
    {
        
        var showBooks = _library.PrintAllBooks();

        if(!showBooks)
        {
            Console.ForegroundColor= ConsoleColor.Yellow;
              Console.WriteLine("There are no books to delete.");
              Console.ResetColor();
              WaitAndClear();
              return;
        }
 
        int id = ReadIntWithErrorMessage("select id book to delete: ", "\nPlease enter a valid id.");

        if (_library.DeleteBook(id))
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Book Deleted successfuly");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor= ConsoleColor.Red;
            Console.WriteLine("book not found");
            Console.ResetColor();
        }

        WaitAndClear();
    }

    
     /// <summary>
    /// Counts and displays the number of books published by authors after a specified year.
    /// </summary>
    /// <remarks>
    /// The process includes:
    /// 1. Validating the input year.
    /// 2. Optionally filtering by author name (case-insensitive).
    /// 3. Retrieving data from the Library service as a Dictionary.
    /// </remarks>
    public void SearchCountBooksEfterYear()
    {
      
        int inputYear = ReadIntWithErrorMessage("Enter year to search and count: ", 
        "\nInvalid year. Please Enter a valid number,");

        Console.WriteLine("Enter author name to filter (or press Enter to skip): ");

        
        string authorInput = Console.ReadLine() ?? "";

        
        var result = _library.GetBookeEfterYear(inputYear);

        var filtered = string.IsNullOrWhiteSpace(authorInput)
                            ? result 
                            : result.Where(r => r.Key.Contains(authorInput, StringComparison.OrdinalIgnoreCase))
                            .ToDictionary(r => r.Key, r => r.Value);

        if (filtered.Count == 0)
        {
            Console.ForegroundColor= ConsoleColor.Red;
            Console.WriteLine("No books found for this author/year.");
            Console.ResetColor();
        }
        
        else
        {
            foreach (var book in filtered)
            {

                Console.WriteLine($"Author name = {book.Key}, Count = {book.Value}");
            }

        }


         WaitAndClear();

    }

    public void PrintAllBooks()
    {
      var print =  _library.PrintAllBooks();

        if (!print)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("There are no books to display.\n Please add a new book first");
            Console.ResetColor();
        }

         WaitAndClear();
        
      
    }

    /// <summary>
    /// Validates and retrieves the user's menu selection, ensuring only numbers within the valid range are accepted.
    /// </summary>
    /// <remarks>
    /// The method continuously prompts the user until a valid integer between 1 and 7 is entered.
    /// </remarks>
    /// <returns>A valid integer representing the selected menu option.</returns>
 
    public int SelectOption()
    {
        int option;

        bool validOption;
        do
        {
            Console.WriteLine("Select an option");
            string input = Console.ReadLine() ?? "";

            validOption = int.TryParse(input, out option);
            if (!validOption || option < 1 || option > 7)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please select from 1 to 7");
                Console.ResetColor();
                validOption = false;
            }
        } 
        
        while(!validOption);
        return option;
            
        
    }

    /// <summary>
    /// Prompts the user to enter an integer and validates the input.
    /// Displays an error message and retries if the input is not a valid number.
    /// <param name="message">The prompt message to display to the user.</param>
    /// <param name="errorMessage">The error message to display if validation fails.</param>
    /// </summary>
    private int ReadIntWithErrorMessage(string message, string errorMessage)
    {
        while (true)
        {
            Console.Write(message);
            string? input = Console.ReadLine();

            if (int.TryParse(input, out int result))
            {
        
                return result;
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(errorMessage);
            Console.ResetColor();

          
        }

        


    }

    /// <summary>
    /// Prompts the user with a message and ensures a non-empty string is entered.
    /// Displays an error message and retries until a valid input is provided.
    /// </summary>
    /// <returns>A valid, non-white-space string entered by the user.</returns>
    private string ReadStringWithErrorMessage(string message, string errorMessage)
    {
        while (true)
        {
            Console.Write(message);
            string? input = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(input))
            {

                return input;
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(errorMessage);
            Console.ResetColor();
        }

    }

    /// <summary>
    /// Pauses the execution until a key is pressed, then clears the console screen.
    /// </summary>
    private void WaitAndClear()
    { 
        Console.WriteLine("\nPress any key to return...");
        Console.ReadKey();
        Console.Clear();
    }
}