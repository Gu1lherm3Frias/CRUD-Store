using Store.Database;
using Store.Models;
using Microsoft.Data.Sqlite;

namespace Store.Repositories;

class ProductRepository
{
    public ProductRepository() { }

    public List<Product> GetAll()
    {
        var products = new List<Product>();

        var connection = new SqliteConnection(DatabaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM product";

        var reader = command.ExecuteReader();

        while (reader.Read())
        {
            var id = reader.GetInt32(0);
            var description = reader.GetString(1);
            var unitPrice = reader.GetDecimal(2);
            var product = ReaderToProduct(reader);
            products.Add(product);
        }

        connection.Close();
        return products;
    }

    public Product Save(Product product)
    {
        var connection = new SqliteConnection(DatabaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO product VALUES($product_id, $description, $unit_price)";
        command.Parameters.AddWithValue("$id", product.ProductId);
        command.Parameters.AddWithValue("$description", product.Description);
        command.Parameters.AddWithValue("$unit_price", product.UnitPrice);

        command.ExecuteNonQuery();
        connection.Close();

        return product;
    }

    public Product GetById(int id)
    {
        var connection = new SqliteConnection(DatabaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM product WHERE (product_id = $product_id)";
        command.Parameters.AddWithValue("$product_id", id);

        var reader = command.ExecuteReader();
        reader.Read();

        var product = ReaderToProduct(reader);

        connection.Close();
        return product;
    }

    public Product Update(Product product)
    {
        var connection = new SqliteConnection(DatabaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO product VALUES ($product_id, $description, $unit_price)";
        command.CommandText = "UPDATE product SET description = $description, unit_price = $unit_price WHERE (product_id = $product_id)";
        command.Parameters.AddWithValue("$product_id", product.ProductId);
        command.Parameters.AddWithValue("$description", product.Description);
        command.Parameters.AddWithValue("$unit_price", product.UnitPrice);
      

        command.ExecuteNonQuery();
        connection.Close();

        return product;
    }

    public void Delete(int id)
    {
        var connection = new SqliteConnection(DatabaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM product WHERE (product_id = $product_id)";
        command.Parameters.AddWithValue("$product_id", id);

        command.ExecuteNonQuery();
        connection.Close();
    }

    public bool ExitsById(int id)
    {
        var connection = new SqliteConnection(DatabaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT count(id) FROM product WHERE (product_id = $product_id)";
        command.Parameters.AddWithValue("$id", id);

        var reader = command.ExecuteReader();
        reader.Read();
        var result = reader.GetBoolean(0);

        return result;
    }

    private Product ReaderToProduct(SqliteDataReader reader)
    {
        var product = new Product(reader.GetInt32(0), reader.GetString(1), reader.GetDecimal(2));
        return product;
    }
}