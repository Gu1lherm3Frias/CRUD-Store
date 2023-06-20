namespace Store.Models;

class OrderItems {
    public int ItemId {set; get;}
    public int OrderId {set; get;}
    public int ProductId {set; get;}
    public int Amount {set; get;}

    public OrderItems(int itemId, int orderId, int productId, int amount) {
        ItemId = itemId;
        OrderId = orderId;
        ProductId = productId;
        Amount = amount;
    }
}