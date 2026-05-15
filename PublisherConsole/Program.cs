// See https://aka.ms/new-console-template for more information
using System;
using Microsoft.EntityFrameworkCore;
using PublisherData;
using PublisherDomain;

PubContext _context = new PubContext();
// this assumes you are working with the populated
// database created in previous module

//QueryFilters();
//QueryFiltersLike();
//AddSomeMoreAuthors();
//SkipAndTakeAuthors();
//SortAuthors();
//SortAuthor2();
//AuthorFilterAndSort();
//QueryAggregate();
//InsertAuthor();
//RetrieveAndUpdateAuthor();
//RetrieveAndUpdateMultipleAuthors();
//VariousOperations();
//CoordinatedRetrieveAndUpdateAuthor();
//DeleteAnAuthor();
//InsertMultipleAuthors();
//GetAuthors();
//InsertNewAuthorWithNewBook();
//InserNewAuthorWith2NewBooks();
//AddNewBookToExistingAuthorInMemory();
//AddNewBookToExistingAuthorInMemoryViaBook();
//EagerLoadBooksWithAuthors();
//Projections();
//ModifyingRelatedDataWhenTracked();
//ModifyingRelatedDataWhenNotTracked();
//CascadeDeleteInActionWhenTracked();
//ConnectExistingArtistAndCoverObjects();
//CreateNewCoverWithExistingArtis();
//CreateNewCoverAndArtistTogether();
//RetrieveAnArtistWithTheirCovers();
//RetrieveAllArtistWhoHaveCovers();
//UnAssignAnArtistFromCover();
//GetAllBooksWithTheirCovers();
//MultiLevelInclude();
//newBookAndCover();
//AddCoverToExistingBookThatHasAnUnTrackedCover();
AddCoverToExistingBookWithTrackedCover();

Console.WriteLine("The Program End");
//All the function start Here
#region Filtering

// Filtering authors
void QueryFilters()
{
    var name = "Josie";
    var authors = _context.Authors.Where(s => s.FirstName == name).ToList();
}

// Query filter using like statement
void QueryFiltersLike()
{
    // good practice always use variables
    var filter = "L%";
    var authors = _context.Authors
        .Where(a => EF.Functions.Like(a.LastName, filter)).ToList();

    foreach (var author in authors)
    {
        Console.WriteLine(author.AuthorId + "." +author.FirstName + " " + author.LastName);
    }
}

// adding some more of authors
void AddSomeMoreAuthors()
{
    _context.Authors.Add(new Author { FirstName = "Rhoda", LastName = "Lerman" });
    _context.Authors.Add(new Author { FirstName = "Don", LastName = "Jones" });
    _context.Authors.Add(new Author { FirstName = "Jim", LastName = "Christopher" });
    _context.Authors.Add(new Author { FirstName = "Stephen", LastName = "Haunts" });
    _context.SaveChanges();
}

// Querying paging page for author
void SkipAndTakeAuthors()
{
    var groupSize = 2;
    for (int i = 0; i < 3; i++)
    {
        var authors = _context.Authors.Skip(groupSize * i).Take(groupSize).ToList();
        Console.WriteLine($"Group {i}:");
        foreach (var author in authors)
        {
            Console.WriteLine($" {author.FirstName} {author.LastName}");
        }
    }
}

#endregion

#region Sorting

// Sorting Authors by LastName
void SortAuthors()
{
    var authorByLastName = _context.Authors.OrderBy(a => a.LastName).ToList();
    authorByLastName.ForEach(a => Console.WriteLine(a.LastName + ", " + a.FirstName));
}

// Sorting Authors by LastName and FirtsName
void SortAuthor2()
{
    var authorByLastName = _context.Authors
        .OrderBy(a => a.LastName)
        .ThenBy(a => a.FirstName).ToList();
    authorByLastName.ForEach(a => Console.WriteLine(a.LastName + ", " + a.FirstName));

    var authorDescending = _context.Authors
        .OrderByDescending(a => a.LastName)
        .ThenByDescending(a => a.FirstName).ToList();
    Console.WriteLine("**Descending Last and First");
    authorDescending.ForEach(a => Console.WriteLine(a.LastName + ", " + a.FirstName));
}

void AuthorFilterAndSort()
{
    var authorFilterAndSort = _context.Authors
        .Where(a => a.LastName == "Lerman")
        .OrderByDescending(a => a.FirstName).ToList();

    authorFilterAndSort.ForEach(a => Console.WriteLine(a.LastName + ", " + a.FirstName));
}

#endregion

#region Aggregate

void QueryAggregate()
{
    // from this filtering query
    //var authors = _context.Authors.Where(a => a.LastName == "Lerman").ToList();

    // using FirstOrDefault 
    //var author = _context.Authors.FirstOrDefault(a=> a.LastName=="Lerman");

    var author = _context.Authors.OrderByDescending(a => a.FirstName).FirstOrDefault(a => a.LastName == "Lerman");

    if (author != null)
    {
        Console.WriteLine(author.FirstName + " " + author.LastName);
    }
    

    // this one is error in runtime
    //var auth = _context.Authors.LastOrDefault(a => a.LastName == "Lerman");
}

#endregion

#region Tracking Or No Tracking Querys

// AsNoTracking() return query as a query, not a DbSet
// can't append DbSet method such as find AsNoTracking however you can use linq method.
var author = _context.Authors.AsNoTracking().FirstOrDefault();


#endregion

#region Inserting simple object
// Add from DbSet or DbContext
// DbSet -> _context.Authors.Add(..)
// DbContext -> _context.Add(..)

void InsertAuthor()
{
    var author = new Author { FirstName = "Frank", LastName = "Herber" };
    _context.Authors.Add(author);
    _context.SaveChanges();
}

#endregion

#region Update Data

// Retrieve and Update Author
void RetrieveAndUpdateAuthor()
{
    var author = _context.Authors.FirstOrDefault(a => a.FirstName == "Julie" && a.LastName == "Lerman");
    if (author != null)
    {
        author.FirstName = "Julia";
        _context.SaveChanges();
    }
}

// Retrieve and Update Multiple Author
void RetrieveAndUpdateMultipleAuthors()
{
    var LermanAuthors = _context.Authors.Where(a => a.LastName == "Lehrman").ToList();
    foreach (var la in LermanAuthors)
    {
        la.LastName = "Lerman";
    }

    Console.WriteLine("Before:" + _context.ChangeTracker.DebugView.ShortView);
    _context.ChangeTracker.DetectChanges();
    Console.WriteLine("After:" + _context.ChangeTracker.DebugView.ShortView
        );

    _context.SaveChanges();
}

// Various Operations
void VariousOperations()
{
    var author = _context.Authors.Find(1);
    author.LastName = "Newfoundland";
    var newauthor = new Author { LastName = "Appleman", FirstName = "Dan" };
    _context.Authors.Add(newauthor);
    _context.SaveChanges();
}

#endregion

#region Updating Untracked Object

void CoordinatedRetrieveAndUpdateAuthor()
{
    var author = FindThatAuthor(3);
    if(author?.FirstName=="Julie")
    {
        author.FirstName = "Julia";
        SaveThatAuthor(author);
    }
}

Author FindThatAuthor(int authorId)
{
    using var shortLivedContect = new PubContext();
    return shortLivedContect.Authors.Find(authorId);
}

void SaveThatAuthor(Author author)
{
    using var anotherShortLivedContext = new PubContext();
    anotherShortLivedContext.Authors.Update(author);
    anotherShortLivedContext.SaveChanges();
}

#endregion

#region Delete Objects

void DeleteAnAuthor()
{
    var extraJL = _context.Authors.Find(2);
    if (extraJL!=null)
    {
        _context.Authors.Remove(extraJL);
        _context.SaveChanges();
    }
}

#endregion

#region Insert multiple Object

void InsertMultipleAuthors()
{
    var newAuthors = new Author[]{
       new Author { FirstName = "Ruth", LastName = "Ozeki" },
       new Author { FirstName = "Sofia", LastName = "Segovia" },
       new Author { FirstName = "Ursula K.", LastName = "LeGuin" },
       new Author { FirstName = "Hugh", LastName = "Howey" },
       new Author { FirstName = "Isabelle", LastName = "Allende" }
    };
    _context.AddRange(newAuthors);
    _context.SaveChanges();
}

void InsertMultipleAuthorPassedId(List<Author> listOfAuthors)
{
    _context.Authors.AddRange(listOfAuthors);
    _context.SaveChanges();
}

void BulkUpdate()
{
    var newAuthors = new Author[]{
    new Author {FirstName = "Tsitsi", LastName = "Dangarembga" },
    new Author { FirstName = "Lisa", LastName = "See" },
    new Author { FirstName = "Zhang", LastName = "Ling" },
    new Author { FirstName = "Marilynne", LastName="Robinson"}
    };
    _context.Authors.AddRange(newAuthors);

    //add another query
    var book = _context.Books.Find(2);
    book.Title = "Programming Entity Framework 2nd Edition";

    //save all changes
    _context.SaveChanges();
}
#endregion

#region Demo Logging

void GetAuthors()
{
    //var authors = _context.Authors.ToList();
    var name = "Ozeki";
    var authors = _context.Authors.Where(a => a.LastName == name).ToList();
    if (authors != null)
    {
        foreach (var author in authors)
        {
            Console.WriteLine(author.AuthorId + " " + author.FirstName);
        }
    }
}

#endregion

#region Inserting Related Data

//insert book from author graph using _context
void InsertNewAuthorWithNewBook()
{
    var author = new Author { FirstName = "Lynda", LastName = "RutLedge" };
    author.Books.Add(
        new Book {
            Title = "West With Giraffes",
            PublishDate = new DateTime(2021, 2, 1)
        });
    _context.Authors.Add(author);
    _context.SaveChanges();
}

//insert multiple book from author graph using _context
void InserNewAuthorWith2NewBooks()
{
    var author = new Author { FirstName = "Don", LastName = "Jones" };
    author.Books.AddRange(new List<Book>
    {
        new Book {Title = "The Never", PublishDate = new DateTime(2019, 12, 1)},
        new Book {Title = "Alabaster", PublishDate = new DateTime(2019, 4, 1)}
    });
    _context.Authors.Add(author);
    _context.SaveChanges();
}

//inserting book for the already exist author in the database
//using saveChanges
void AddNewBookToExistingAuthorInMemory()
{
    var author = _context.Authors.FirstOrDefault(a => a.LastName == "Howey");
    if (author!=null)
    {
        author.Books.Add(new Book { Title = "Wool", PublishDate = new DateTime(2012, 1, 1) });
    }
    _context.SaveChanges();
}

void AddNewBookToExistingAuthorInMemoryViaBook()
{
    var book = new Book
    {
        Title = "shift",
        PublishDate = new DateTime(2012, 1, 1),
        // add foreign key here instead
        AuthorId = 5
    };
    //comment this, add one line above
    //book.Author = _context.Authors.Find(5); //id for Hugh Howey
    _context.Books.Add(book);
    _context.SaveChanges();
}
#endregion

#region Eager Loading Related Data In Queries
//using DbSet Include method to query related data

void EagerLoadBooksWithAuthors()
{

    //var authors = _context.Authors.Include(a => a.Books).ToList();

    //adding filtering
    var pubDateStart = new DateTime(2010, 1, 1);
    var authors = _context.Authors
        .Include(a => a.Books
                    .Where(b => b.PublishDate >= pubDateStart)
                    .OrderBy(b => b.Title))
        .ToList();

    authors.ForEach(a =>
    {
        Console.WriteLine($"{a.LastName} ({a.Books.Count})");
        a.Books.ForEach(b => Console.WriteLine("    " + b.Title));
    });
}

//Query projection
//Projection into undefined ("Anonymous") Type
void Projections()
{
    //Anonymous types are not tracked
    var unknounTypes = _context.Authors
    .Select(a => new
    {
        AuthorId = a.AuthorId,
        Name = a.FirstName.First() + "" + a.LastName,
        Books = a.Books
    }).ToList();

   var debugview = _context.ChangeTracker.DebugView.ShortView;
}

//Loading related data for objects Already in Memory
//explisit loading
void ExplicitLoadCollection()
{
    var author = _context.Authors.FirstOrDefault(a => a.LastName == "Howey");
    _context.Entry(author).Collection(a => a.Books).Load();

    //filter on loading using Query() method
    var newfBook = _context.Entry(author).Collection(a => a.Books)
        .Query().Where(b => b.Title.Contains("Newf")).ToList();
}

//using lazy loading to retrieve related data
void LazyLoadBooksFromAnAuthor()
{
    //requires lazy loading to be setup in your app
    var author = _context.Authors.FirstOrDefault(a => a.LastName == "Howey");
    foreach (var book in author.Books)
    {
        Console.WriteLine(book.Title);
    }

    //enabling lazy loading
    //every navigation in every entity must be virtual
    //e.g public virtual List<Book> Books { get; set; }
}

//using related data to filter object
void FilterUsingRelatedData()
{
    var recentAuthor = _context.Authors
        .Where(a => a.Books.Any(b => b.PublishDate.Year >= 2015))
        .ToList();
}

//Modifying related data
void ModifyingRelatedDataWhenTracked()
{
    var author = _context.Authors.Include(a => a.Books)
        .FirstOrDefault(a => a.AuthorId == 5);
    //author.Books[0].BasePrice = (decimal)10.00;
    author.Books.Remove(author.Books[1]);
    _context.ChangeTracker.DetectChanges();
    var state = _context.ChangeTracker.DebugView.ShortView;
}

void ModifyingRelatedDataWhenNotTracked()
{
    var author = _context.Authors.Include(a => a.Books)
        .FirstOrDefault(a => a.AuthorId == 5);
    author.Books[0].BasePrice = (decimal)12.00;

    var newContext = new PubContext();
    //this will be make all the field updated
    //newContext.Books.Update(author.Books[0]);

    newContext.Entry(author.Books[0]).State = EntityState.Modified;
    var state = newContext.ChangeTracker.DebugView.ShortView;
    newContext.SaveChanges();
}

void CascadeDeleteInActionWhenTracked()
{
    var author = _context.Authors.Include(a => a.Books)
        .FirstOrDefault(a => a.AuthorId == 9);
    _context.Authors.Remove(author);
    var state = _context.ChangeTracker.DebugView.ShortView;
    _context.SaveChanges();
}
#endregion

#region Joining Object in Many to Many Relation

void ConnectExistingArtistAndCoverObjects()
{
    var artistA = _context.Artists.Find(1);
    var artistB = _context.Artists.Find(2);
    var coverA = _context.Covers.Find(1);

    coverA.Artists.Add(artistA);
    coverA.Artists.Add(artistB);
    _context.SaveChanges();
}

void CreateNewCoverWithExistingArtis()
{
    var artistA = _context.Artists.Find(1);
    var cover = new Cover { DesignIdeas = "Author has provided a photo" };
    //adding relationship from cover side
    cover.Artists.Add(artistA);
    //start tracking the new cover
    _context.Covers.Add(cover);
    _context.SaveChanges();
};

void CreateNewCoverAndArtistTogether()
{
    var newArtist = new Artist { FirstName = "Kir", LastName = "Talmage" };
    var newCover = new Cover { DesignIdeas = "We like birds!" };
    newArtist.Covers.Add(newCover);
    _context.Artists.Add(newArtist);
    _context.SaveChanges();
};

void RetrieveAnArtistWithTheirCovers()
{
    //initial code
    //var artistWithCovers = _context.Artists.Include(a => a.Covers).FirstOrDefault(a => a.ArtistId == 1);

    //revise code
    //var artistWithCovers = _context.Artists.Include(a => a.Covers).ToList();
    //foreach (var a in artistWithCovers)
    //{
    //    Console.WriteLine($"{a.FirstName} {a.LastName}, Design to Work on:");
    //    if (a.Covers.Count() == 0)
    //    {
    //        Console.WriteLine("   No covers");
    //    }
    //    else
    //    {
    //        a.Covers.ForEach(c => Console.WriteLine($"  {c.DesignIdeas}"));
    //    }
    //}

    //adding collab artist
    var artistWithCovers = _context.Artists.Include(a => a.Covers).ToList();
    foreach (var a in artistWithCovers)
    {
        Console.WriteLine($"{a.FirstName} {a.LastName}, Design to Work on:");
        var primaryArtistId = a.ArtistId;
        if (a.Covers.Count() == 0)
        {
            Console.WriteLine("   No covers");
        }
        else
        {
            foreach (var c in a.Covers)
            {
                string collabolators = "";
                foreach (var ca in c.Artists.Where(ca => ca.ArtistId != primaryArtistId))
                {
                    collabolators += $"{ca.FirstName} {ca.LastName}";
                }
                if (collabolators.Length > 0)
                {
                    collabolators = $"(with {collabolators})";
                }
                Console.WriteLine($"  *{c.DesignIdeas} {collabolators}");
            }
        }
    }

}

void RetrieveAllArtistWhoHaveCovers()
{
    var artistsWithCovers = _context.Artists.Where(a => a.Covers.Any()).ToList();
}

void UnAssignAnArtistFromCover()
{
    var coverWithArtist = _context.Covers.Include(c => c.Artists.Where(a => a.ArtistId == 1)).FirstOrDefault(c => c.CoverId == 1);
    coverWithArtist.Artists.RemoveAt(0);
    _context.ChangeTracker.DetectChanges();
    var debugview = _context.ChangeTracker.DebugView.ShortView;
    _context.SaveChanges();
}

//Reassign cover from one artist to another artist
void ReassignACover()
{
    var coverWithArtist4 = _context.Covers.Include(c => c.Artists.Where(a => a.ArtistId == 4)).FirstOrDefault(c => c.CoverId == 5);
    coverWithArtist4.Artists.RemoveAt(0);

    var artist3 = _context.Artists.Find(3);
    coverWithArtist4.Artists.Add(artist3);
    _context.ChangeTracker.DetectChanges();
}

#endregion

#region Querying One to One Relationship

void GetAllBooksWithTheirCovers()
{
    var booksAndCovers = _context.Books.Include(b => b.Cover).ToList();
    booksAndCovers.ForEach(book => Console.WriteLine(book.Title + (book.Cover == null ? "No cover yet" : ":" + book.Cover.DesignIdeas)));

    //All Books include cover
    //_context.Books.Include(b => b.Cover).ToList();

    //All books where cover is not null
    //_context.Books.Include(b => b.Cover).Where(b => b.Cover != null).ToList();

    //All books where cover is null
    //_context.Books.Where(b => b.Cover == null).ToList();

    //Project to an anonymous type
    //_context.Books.Where(b => b.Cover != null).Select(b => new { b.Title, b.Cover.DesignIdeas }).ToList();

}

void MultiLevelInclude()
{
    var authorGraph = _context.Authors.AsNoTracking()
        .Include(a => a.Books)
        .ThenInclude(b => b.Cover)
        .ThenInclude(c => c.Artists)
        .FirstOrDefault(a => a.AuthorId == 1);

    Console.WriteLine(authorGraph?.FirstName + " " + authorGraph?.LastName);
    foreach (var book in authorGraph.Books)
    {
        Console.WriteLine("Book:" + book.Title);
        if (book.Cover != null)
        {
            Console.WriteLine("Design Ideas: " + book.Cover.DesignIdeas);
            Console.Write("Artist(s):");
            book.Cover.Artists.ForEach(a => Console.Write(a.LastName + " "));

        }
    }

    //you can always query from the dependent
    //_context.Covers.Include(c => c.Book).ThenInclude(b => b.Author).ToList();
}

//Add new book and cover together
void newBookAndCover()
{
    var book = new Book { AuthorId = 1, Title = "Call Me Ishtar", PublishDate = new DateTime(1973, 1, 1) };
    book.Cover = new Cover { DesignIdeas = "Image of Ishtar?" };
    _context.Books.Add(book);
    _context.SaveChanges();
}

//Add Cover to exiting Book in Memory
void AddCoverToExistingBook()
{
    var book = _context.Books.Find(1004);
    book.Cover = new Cover { DesignIdeas = "A Wool scouring pad" };
    _context.SaveChanges();
}

void AddCoverToExistingBookThatHasAnUnTrackedCover()
{
    // this function will fail because the book already has a cover
    var book = _context.Books.Find(1002);
    book.Cover = new Cover { DesignIdeas = "A Spiral" };
    _context.SaveChanges();
}

void AddCoverToExistingBookWithTrackedCover()
{
    //make sure explisitly add cover in your code so you can find out if the book has cover
    //this code if the book has cover will delete old cover and replace it with new cover
    var book = _context.Books.Include(b => b.Cover)
        .FirstOrDefault(b => b.BookId == 1002);
    book.Cover = new Cover { DesignIdeas = "A Spiral" };
    _context.ChangeTracker.DetectChanges();
    var debugview = _context.ChangeTracker.DebugView.ShortView;
}

//this will protecting from unique FK
void ProtectingFromUniqueFK()
{
    var TheNeverDesignIdeas = "A spirally spiral";
    var book = _context.Books.Include(b => b.Cover)
        .FirstOrDefault(b => b.BookId == 1002);
    if(book.Cover != null)
    {
        book.Cover.DesignIdeas = TheNeverDesignIdeas;
    }
    else
    {
        book.Cover = new Cover { DesignIdeas = "A spirally spiral" };
    }

    //the result, if cover is not null, new cover will be added
    //and if null new cover also will be added
    //another option is to add confirmation what to do
}

//replacing or removing one to one behaviour (intentionally)
//reassigning a cover to Different book


//_context.Covers.Remove(greenCover);
//bookWithCoverGraph.Cover=null;
#endregion