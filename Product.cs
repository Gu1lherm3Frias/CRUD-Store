namespace Store.Models;

class Product {
    public int ProductId {set; get;}
    public string Description {set; get;}
    public decimal UnitPrice {set; get;}

    public Product(int productId, string description, decimal unitPrice) {
        ProductId = productId;
        Description = description;
        UnitPrice = unitPrice;
    }
}