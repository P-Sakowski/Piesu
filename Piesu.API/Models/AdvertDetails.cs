namespace Piesu.API.Models
{
    public class AdvertDetails
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsNegotiable { get; set; }
        public int LocationId { get; set; }
        public int CategoryId { get; set; }
        public int SubcategoryId { get; set; }
    }
}