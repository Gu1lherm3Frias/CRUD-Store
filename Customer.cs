namespace Store.Models;

class Customer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string ZipCode { get; set; }
    public string UF { get; set; }
    public string IE { get; set; }

    public Customer(int id, string name, string address, string city, string zipcode, string uf, string ie)
    {
        Id = id;
        Name = name;    
        Address = address;
        City = city;
        ZipCode = zipcode;
        UF = uf;
        IE = ie;
    }

}