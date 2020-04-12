namespace eLibrary.Models
{
    public class Rate
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
    }
}