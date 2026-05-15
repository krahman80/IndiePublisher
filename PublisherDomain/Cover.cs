using System;
namespace PublisherDomain
{
	public class Cover
	{
        public Cover()
        {
            Artists = new List<Artist>();
        }
        public int CoverId { get; set; }
		public string DesignIdeas { get; set; }
		public bool DigitalOnly { get; set; }
		public List<Artist> Artists { get; set; }

		//navigation property to book
		public Book Book { get; set; }
		//fk key of book
		public int BookId { get; set; }

	}
}

