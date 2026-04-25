// See https://aka.ms/new-console-template for more information
using PublisherData;
using PublisherDomain;

// Generate Database on the fly
using (PubContext context = new PubContext())
{
    context.Database.EnsureCreated();
}

GetAuthors();
AddAuthor();
GetAuthors();

void AddAuthor()
{
    var author = new Author { FirstName = "Josie", LastName = "Newf" };
    using var context = new PubContext();
    context.Authors.Add(author);
    context.SaveChanges();
}

void GetAuthors()
{
    using var context = new PubContext();
    var authors = context.Authors.ToList();
    foreach (var author in authors)
    {
        Console.WriteLine(author.FirstName + " " + author.LastName);
    }
}