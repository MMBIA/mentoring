namespace DataAccess.DTO
{
    public class Cart
    {
        public int Id { get; set; }
        public bool GiftCard { get; set; }
        public List<Item> Items { get; set; }
        public Cart()
        {
            Items = new List<Item>();
        }
    }
}