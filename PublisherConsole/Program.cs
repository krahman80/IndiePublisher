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
QueryAggregate();


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
        Console.WriteLine(author.Id + "." +author.FirstName + " " + author.LastName);
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