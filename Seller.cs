namespace Store.Models;

class Seller
{
    public int SellerId { get; set; }
    public string Name { get; set; }
    public decimal FixedIncome { get; set; }
    public string CommissionRank { get; set; }

    public Seller(int sellerId, string name, decimal fixedIncome, string commisionRank)
    {
        SellerId = sellerId;
        Name = name;
        FixedIncome = fixedIncome;
        CommissionRank = commisionRank;
    }
}