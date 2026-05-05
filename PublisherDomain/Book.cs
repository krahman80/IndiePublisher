namespace PublisherDomain
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public DateTime PublishDate { get; set; }
        public decimal BasePrice { get; set; }

        // author navigation property inference by ef core
        public Author Author { get; set; }

        // this is foreign key
        public int AuthorId { get; set; }

    }
}