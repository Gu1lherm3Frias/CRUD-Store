namespace DBlesson.Models;

class Product {
    int ProductId {set; get;}
    string Description {set; get;}
    decimal UnitPrice {set; get;}

    Product(int productId, string description, decimal unitPrice) {
        ProductId = productId;
        Description = description;
        UnitPrice = unitPrice;
    }
}