namespace DBLesson.Models;

class OrderItems {
    int ItemId {set; get;}
    int OrderId {set; get;}
    int ProductId {set; get;}
    int Amount {set; get;}

    OrderItems(int itemId, int orderId, int productId, int amount) {
        ItemId = itemId;
        OrderId = orderId;
        ProductId = productId;
        Amount = amount;
    }
}