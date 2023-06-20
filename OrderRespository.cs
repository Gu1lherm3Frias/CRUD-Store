using Store.Database;
using Store.Models;
using Microsoft.Data.Sqlite;

namespace Store.Repositories;

class OrderRepository
{

    public OrderRepository(){}

    public List<Order> GetAll()
    {
        var orders = new List<Order>();

        var connection = new SqliteConnection(DatabaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM order_tb";

        var reader = command.ExecuteReader();

        while (reader.Read())
        {
            var orderId = reader.GetInt32(0);
            var deadline = reader.GetString(1);
            var orderDate = reader.GetString(2);
            var customerId = reader.GetInt32(3);
            var sellerID = reader.GetInt32(4);
            var Order = ReaderToOrder(reader);
            orders.Add(Order);
        }

        connection.Close();
        return orders;
    }

    public Order Save(Order order)
    {
        var connection = new SqliteConnection(DatabaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO order_tb VALUES($order_id, $deadline, $order_date, $customer_id, $seller_id)";
        command.Parameters.AddWithValue("$order_id", order.OrderId);
        command.Parameters.AddWithValue("$deadline", order.Deadline);
        command.Parameters.AddWithValue("$order_date", order.OrderDate);
        command.Parameters.AddWithValue("$customer_id", order.CustomerId);
        command.Parameters.AddWithValue("$seller_id", order.SellerId);

        command.ExecuteNonQuery();
        connection.Close();

        return order;
    }

    public Order GetById(int id)
    {
        var connection = new SqliteConnection(DatabaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM order_tb WHERE (order_id = $order_id)";
        command.Parameters.AddWithValue("$order_id", id);

        var reader = command.ExecuteReader();
        reader.Read();

        var Order = ReaderToOrder(reader);

        connection.Close();
        return Order;
    }

    public Order Update(Order order)
    {
        var connection = new SqliteConnection(DatabaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO order_tb VALUES ($order_id, $deadline, $order_date, $customer_id, $seller_id)";
        command.CommandText = "UPDATE order_tb SET deadline = $deadline, order_date = $order_date, customer_id = $customer_id, seller_id = $seller_id WHERE (order_id = $order_id)";
        command.Parameters.AddWithValue("$order_id", order.OrderId);
        command.Parameters.AddWithValue("$deadline", order.Deadline);
        command.Parameters.AddWithValue("$order_date", order.OrderDate);
        command.Parameters.AddWithValue("$customer_id", order.CustomerId);
        command.Parameters.AddWithValue("$seller_id", order.SellerId);

        command.ExecuteNonQuery();
        connection.Close();

        return order;
    }

    public void Delete(int id)
    {
        var connection = new SqliteConnection(DatabaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM order_tb WHERE (order_id = $order_id)";
        command.Parameters.AddWithValue("$id", id);

        command.ExecuteNonQuery();
        connection.Close();
    }

    public bool ExitsById(int id)
    {
        var connection = new SqliteConnection(DatabaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT count(id) FROM order_tb WHERE (order_id = $order_id)";
        command.Parameters.AddWithValue("$id", id);

        var reader = command.ExecuteReader();
        reader.Read();
        var result = reader.GetBoolean(0);

        return result;
    }

    private Order ReaderToOrder(SqliteDataReader reader)
    {
        var order = new Order(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2));
        return order;
    }
}