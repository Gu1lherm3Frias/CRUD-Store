using Store.Database;
using Store.Models;
using Microsoft.Data.Sqlite;

namespace Store.Repositories;

class OrderItemsRepository
{
    public OrderItemsRepository() { }

    public List<OrderItems> GetAll()
    {
        var items = new List<OrderItems>();

        var connection = new SqliteConnection(DatabaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM order_items";

        var reader = command.ExecuteReader();

        while (reader.Read())
        {
            var id = reader.GetInt32(0);
            var orderId = reader.GetInt32(1);
            var productID = reader.GetInt32(2);
            var amount = reader.GetInt32(3);
            var item = ReaderToOrderItems(reader);
            items.Add(item);
        }

        connection.Close();
        return items;
    }

    public OrderItems Save(OrderItems item)
    {
        var connection = new SqliteConnection(DatabaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO order_item VALUES($item_id, $order_id, $product_id, $amount)";
        command.Parameters.AddWithValue("$item_id", item.ItemId);
        command.Parameters.AddWithValue("$order_id", item.OrderId);
        command.Parameters.AddWithValue("$product_id", item.ProductId);
        command.Parameters.AddWithValue("$amount", item.Amount);

        command.ExecuteNonQuery();
        connection.Close();

        return item;
    }

    public OrderItems GetById(int id)
    {
        var connection = new SqliteConnection(DatabaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM order_item WHERE (item_id = $item_id)";
        command.Parameters.AddWithValue("$item_id", id);

        var reader = command.ExecuteReader();
        reader.Read();

        var item = ReaderToOrderItems(reader);

        connection.Close();
        return item;
    }

    public OrderItems Update(OrderItems item)
    {
        var connection = new SqliteConnection(DatabaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO order_item VALUES ($item_id, $order_id, $product_id, $amount)";
        command.CommandText = "UPDATE order_item SET order_id = $order_id, product_id = $product_id, amount = $amount  WHERE (item_id = $item_id)";
        command.Parameters.AddWithValue("$item_id", item.ItemId);
        command.Parameters.AddWithValue("$order_id", item.OrderId);
        command.Parameters.AddWithValue("$product_id", item.ProductId);
        command.Parameters.AddWithValue("$amount", item.Amount);


        command.ExecuteNonQuery();
        connection.Close();

        return item;
    }

    public void Delete(int id)
    {
        var connection = new SqliteConnection(DatabaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM order_item WHERE (item_id = $item_id)";
        command.Parameters.AddWithValue("$item_id", id);

        command.ExecuteNonQuery();
        connection.Close();
    }

    public bool ExistById(int id)
    {
        var connection = new SqliteConnection(DatabaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT count(id) FROM order_item WHERE (item_id = $item_id)";
        command.Parameters.AddWithValue("$item_id", id);

        var reader = command.ExecuteReader();
        reader.Read();
        var result = reader.GetBoolean(0);

        return result;
    }

    private OrderItems ReaderToOrderItems(SqliteDataReader reader)
    {
        var item = new OrderItems(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3));
        return item;
    }
}