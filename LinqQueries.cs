using System.Linq;

public class LinqQueries
{
    private List<Book> booksCollection = new List<Book>();
    public LinqQueries()
    {
        using (StreamReader reader = new StreamReader("books.json"))
        {
            string json = reader.ReadToEnd();
            booksCollection = System.Text.Json.JsonSerializer.Deserialize<List<Book>>(json, new System.Text.Json.JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
    }

    public IEnumerable<Book> AllTheCollection()
    {
        return booksCollection;
    }

    public int NumberTotalBooks()
    {
        return booksCollection.Count();
    }

    public IEnumerable<Book> BooksAfter2000()
    {
        //Extension method
        //return booksCollection.Where(book => book.PublishedDate.Year > 2000);

        //Query expresion
        return from book in booksCollection where book.PublishedDate.Year > 2000 select book;
    }

    public IEnumerable<Book> BookMore250pagesAndInActionWords()
    {
        //Extension method
        //return booksCollection.Where(book => book.PageCount > 250 && book.Title.Contains("in Action"));

        //Query Expresion
        return from book in booksCollection where book.PageCount > 250 && book.Title.Contains("in Action") select book;
    }

    public bool AllBookHasStatus()
    {
        return booksCollection.All(book => book.Status != string.Empty);
    }

    public bool AnyBookWasPublishedIn2005()
    {
        return booksCollection.Any(book => book.PublishedDate.Year == 2005);
    }

    public IEnumerable<Book> BooksByCategory(string category)
    {
        return booksCollection.Where(book => book.Categories.Contains(category));
    }

    public IEnumerable<Book> BooksByCategoryOrderByTitleAsc(string category)
    {
        return booksCollection.Where(book => book.Categories.Contains(category)).OrderBy(book => book.Title);
    }

    public IEnumerable<Book> BooksByPageCountAndOrderByPagesDesc(int pages)
    {
        return booksCollection.Where(book => book.PageCount > pages).OrderByDescending(book => book.PageCount);
    }

    public IEnumerable<Book> BooksOrderByDecendingDateSpecificCategoryAndCount(string category, int numberBooks)
    {
        return booksCollection.Where(book => book.Categories.Contains(category)).OrderByDescending(book => book.PublishedDate).Take(numberBooks);
    }

    public IEnumerable<Book> BooksOrderByAscendingDateSpecificCategoryAndCount(string category, int numberBooks)
    {
        return booksCollection.Where(book => book.Categories.Contains(category)).OrderBy(book => book.PublishedDate).TakeLast(numberBooks);
    }

    public IEnumerable<Book> BooksWithMorePagesSpecificAndTake3And4(int pages)
    {
        return booksCollection.Where(book => book.PageCount > pages).Take(4).Skip(2);
    }

    public IEnumerable<Book> BooksByDinamicSelectAndNumberOfBooks(int numberOfBooks)
    {
        return booksCollection
                .Take(numberOfBooks)
                .Select(book => new Book{ Title = book.Title, PageCount = book.PageCount });
    }

    public int CountOfBooksWithPagesRange(int minPages, int maxPages)
    {
        return booksCollection.Where(book => book.PageCount >= minPages && book.PageCount <= maxPages).Count();
    }

    public long CountOfBooksWithPagesRangeWithLongCount(int minPages, int maxPages)
    {
        //return booksCollection.Where(book => book.PageCount >= minPages && book.PageCount <= maxPages).LongCount();
        return booksCollection.LongCount(book => book.PageCount >= minPages && book.PageCount <= maxPages);
    }

    public DateTime OldestPublishedBookDate()
    {
        return booksCollection.Min(book => book.PublishedDate);
    }

    public int PagesFromBookWithMorePages()
    {
        return booksCollection.Max(book => book.PageCount);
    }

    public Book BookwithLessPages()
    {
        return booksCollection.Where(book => book.PageCount > 0).MinBy(book => book.PageCount);
    }

    public Book NewestPublishedDateBook()
    {
        return booksCollection.MaxBy(book => book.PublishedDate);
    }

    public int TotalBookPageBetweenPagesRange(int minPages, int maxPages)
    {
        return booksCollection.Where(book => book.PageCount >= minPages && book.PageCount <= maxPages).Sum(book => book.PageCount);
    }

    public string TitleOfBooksAfterAndSpecificYear(int year)
    {
        return booksCollection.Where(book => book.PublishedDate.Year > year).Aggregate("", (titles, next) => {
            if(titles != string.Empty)
            {
                titles += " - " + next.Title;
            }
            else
            {
                titles += next.Title;
            }
            return titles;
        });
    }

    public double AverageTitleLength()
    {
        return booksCollection.Average(book => book.Title.Length);
    }

    public double AveragePageCountBooksMoreThanZero()
    {
        return booksCollection.Where(book => book.PageCount > 0).Average(book => book.PageCount);
    }

    public IEnumerable<IGrouping<int, Book>> AllBooksSinceYearGroupByYear(int year)
    {
        return booksCollection.Where(book => book.PublishedDate.Year >= year).OrderBy(book => book.PublishedDate.Year).GroupBy(book => book.PublishedDate.Year);
    }

    public ILookup<char, Book> DictionaryBooksByLetter()
    {
        return booksCollection.ToLookup(book => book.Title[0], book => book);
    }

    public IEnumerable<Book> BooksAfterYearWithMorePages(int year, int pages)
    {
        var booksAfterYear = booksCollection.Where(book => book.PublishedDate.Year > year);
        var booksMorePages = booksCollection.Where(book => book.PageCount > pages);
        return booksAfterYear.Join(booksMorePages, bookYear => bookYear.Title, bookPage => bookPage.Title, (bookYear, bookPage) => bookYear);
    }
}