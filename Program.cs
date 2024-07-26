// See https://aka.ms/new-console-template for more information
Console.WriteLine("Books Collection");

LinqQueries queries = new LinqQueries();

// All the collection
PrintValues(queries.AllTheCollection());

// Number of books
Console.WriteLine($"Total Books: {queries.NumberTotalBooks()}");

// Books After 2000
PrintValues(queries.BooksAfter2000());

// Book with more of 250 pages And the title has "in Action" words
PrintValues(queries.BookMore250pagesAndInActionWords());

// Check All book has Status != Empty
Console.WriteLine($"All books has status? -  {queries.AllBookHasStatus()}");

// Check Any book was published in 2005
Console.WriteLine($"Any book was published in 2005? -  {queries.AnyBookWasPublishedIn2005()}");

// Books By Category
PrintValues(queries.BooksByCategory("Python"));

// Books By Category Order By Title
PrintValues(queries.BooksByCategoryOrderByTitleAsc("Java"));

// Books With more than specific pages and order descending
PrintValues(queries.BooksByPageCountAndOrderByPagesDesc(450));

// Books By Category Order By PublishDate And Take three first books 
PrintValues(queries.BooksOrderByAscendingDateSpecificCategoryAndCount("Java", 3));

// Books with more than specific pages and take books 3 and 4
PrintValues(queries.BooksWithMorePagesSpecificAndTake3And4(400));

// Books by dinamic Select and Specific number of books
PrintValues(queries.BooksByDinamicSelectAndNumberOfBooks(3));

// Count Of Books with min and max pages
PrintNumberOfBookWithSpecificRangeOfPages(200, 500);

// Oldest Date Published Book
Console.WriteLine($"The oldest date published book is: {queries.OldestPublishedBookDate()}");

// Oldest Date Published Book
Console.WriteLine($"The book with more pages has: {queries.PagesFromBookWithMorePages()} pages");

// Book with less pages
var bookLessPages = queries.BookwithLessPages();
Console.WriteLine($"The book with less pages is: Title: {bookLessPages.Title}, Pages: {bookLessPages.PageCount}");

// Book with newest published date
var newestBook = queries.NewestPublishedDateBook();
Console.WriteLine($"The book with less pages is: Title: {newestBook.Title}, Pages: {newestBook.PublishedDate}");

// Total pages of book with specific range of pages
PrintTotalPagesWithSpecificRangeOfPages(0, 500);

// Title books after and Specific Year;
PrintTitleBooksAfterSpecificYear(2015);

// AverageTitleLength
Console.WriteLine($"The Average of Title Length is: {queries.AverageTitleLength()}");

// Average Number Pages Books More Than Zero Page Count
Console.WriteLine($"The Average of Pages is: {queries.AveragePageCountBooksMoreThanZero()}");

// Books published after specific year and group by year
PrintGroup(queries.AllBooksSinceYearGroupByYear(1997));

// Dictionary book group by title's first date
var dictionaryLookup = queries.DictionaryBooksByLetter();
PrintDictionary(dictionaryLookup, 'S');

// Filtered Books With Join
PrintValues(queries.BooksAfterYearWithMorePages(2005, 500));


void PrintValues(IEnumerable<Book> bookList)
{
    Console.WriteLine("{0, -60} {1, 15} {2, 151}\n", "Títle", "N. Pages", "Data publish");
    foreach (var item in bookList)
    {
        Console.WriteLine("{0, -60} {1, 15} {2, 15}", item.Title, item.PageCount, item.PublishedDate.ToShortDateString());
    }
}

void PrintNumberOfBookWithSpecificRangeOfPages(int minPages, int maxPages)
{
    Console.WriteLine($"The number of book between {minPages} and {maxPages} pages is: {queries.CountOfBooksWithPagesRangeWithLongCount(minPages, maxPages)}");
}

void PrintTotalPagesWithSpecificRangeOfPages(int minPages, int maxPages)
{
    Console.WriteLine($"The total number of pages in books with {minPages} and {maxPages} pages is: {queries.TotalBookPageBetweenPagesRange(minPages, maxPages)}");
}

void PrintTitleBooksAfterSpecificYear(int year)
{
    Console.WriteLine($"The title of books after {year} are: {queries.TitleOfBooksAfterAndSpecificYear(year)}");
}

void PrintGroup(IEnumerable<IGrouping<int, Book>> bookList)
{
    foreach (var group in bookList)
    {
        Console.WriteLine("");
        Console.WriteLine($"Year: { group.Key }");
        Console.WriteLine("{0, -60} {1, 15} {2, 15}\n", "Title", "Pages", "Publish Date");
        foreach(var item in group)
        {
            Console.WriteLine("{0, -60} {1, 15} {2, 15}\n", item.Title, item.PageCount, item.PublishedDate.Date.ToShortDateString());
        }
    }
}


void PrintDictionary(ILookup<char, Book> bookList, char letter)
{
    Console.WriteLine("{0, -60} {1, 15} {2, 15}\n", "Title", "Pages", "Publish Date");
    foreach (var item in bookList[letter])
    {
        Console.WriteLine("{0, -60} {1, 15} {2, 15}\n", item.Title, item.PageCount, item.PublishedDate.Date.ToShortDateString());
    }
}
