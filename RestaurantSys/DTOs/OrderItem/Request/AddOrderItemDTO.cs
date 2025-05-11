namespace RestaurantSys.DTOs.OrderItem.Request
{
    public class AddOrderItemDTO
    {
        public int userID { get; set; } 
        //public int orderItemID { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }

    }
}
