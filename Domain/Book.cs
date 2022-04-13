namespace Domain
{
    public class Book
    {
        public int Id { get; set; }
        public string Isbn { get; set; }
        public int Quantity { get; set; }
        public string Subject { get; set; }
        public string Author { get; set; }
        public Publishing_Company Publishing_Company { get; set; }
        
    }
}