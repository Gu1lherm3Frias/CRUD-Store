namespace Store.Models;

class Order
{
    public int OrderId { get; set; }
    public DateTime Deadline { get; set; }
    public DateTime OrderDate { get; set; }
    public int CustomerId { get; set; }
    public int SellerId { get; set; }

    public Order(int orderId, int customerId, int sellerId)
    {
        OrderId = orderId;
        Deadline = DateTime.UtcNow.AddDays(7);
        OrderDate = DateTime.Now;
        CustomerId = customerId;
        SellerId = sellerId;
    }

}