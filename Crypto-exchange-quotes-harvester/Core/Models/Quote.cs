namespace Core.Models
{
    public class Quote
    {
        public int Id { get; set; }
        public string Time { get; set; }
        public decimal Bid { get; set; }
        public decimal Ask { get; set; }
        public string Exchange { get; set; }
        public string Name { get; set; }
    }
}
