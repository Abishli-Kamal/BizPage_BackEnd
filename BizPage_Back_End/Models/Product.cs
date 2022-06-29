namespace BizPage_Back_End.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }

    }
}
