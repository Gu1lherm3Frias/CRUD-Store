using Store.Database;
using Store.Models;
using Microsoft.Data.Sqlite;

namespace Store.Repositories;

class CustomerRepository {
    public CustomerRepository() {}

    public List<Customer> GetAll() {
        var customers = new List<Customer>();

        var connection = new SqliteConnection(DatabaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM customer";

        var reader = command.ExecuteReader();

        while(reader.Read()) {
            var id = reader.GetInt32(0);
            var name = reader.GetString(1);
            var address = reader.GetString(2);
            var city =  reader.GetString(3);
            var zipcode = reader.GetString(4);
            var uf = reader.GetString(5);
            var ie = reader.GetString(6);   
            var customer = ReaderToCustomer(reader);
            customers.Add(customer);
        }

        connection.Close();
        return customers;
    }

    public Customer Save(Customer customer) {
        var connection =  new SqliteConnection(DatabaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO customers VALUES($customer_id, $name $address, $city, $zip_code, $customer_uf, $customer_ie)";
        command.Parameters.AddWithValue("$customer_id", customer.Id);
        command.Parameters.AddWithValue("$name", customer.Name);
        command.Parameters.AddWithValue("$address", customer.Address);
        command.Parameters.AddWithValue("$city", customer.City);
        command.Parameters.AddWithValue("$zip_code", customer.ZipCode);
        command.Parameters.AddWithValue("$customer_uf", customer.UF);
        command.Parameters.AddWithValue("$customer_ie", customer.IE);

        command.ExecuteNonQuery();
        connection.Close();

        return customer;
    }

    public Customer GetById(int id) {
        var connection = new SqliteConnection(DatabaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM customers WHERE (customer_id = $customer_id)";
        command.Parameters.AddWithValue("$customer_id", id);

        var reader = command.ExecuteReader();
        reader.Read();

        var customer = ReaderToCustomer(reader);

        connection.Close();
        return customer;
    }

    public Customer Update(Customer customer) {
        var connection = new SqliteConnection(DatabaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO customers VALUES ($customer_id, $name, $address, $city, $zip_code, $customer_uf, $customer_ie)";
        command.CommandText = "UPDATE customers SET name = $name, address = $address, city = $city, zip_code = $zip_code, uf = $customer_uf, ie = $customer_ie WHERE (customer_id = $id)";
        command.Parameters.AddWithValue("$id", customer.Id);
        command.Parameters.AddWithValue("$name", customer.Name);
        command.Parameters.AddWithValue("$address", customer.Address);
        command.Parameters.AddWithValue("$city", customer.City);
        command.Parameters.AddWithValue("$zip_code", customer.ZipCode);
        command.Parameters.AddWithValue("$customer_uf", customer.UF);
        command.Parameters.AddWithValue("$customer_ie", customer.IE);

        command.ExecuteNonQuery();
        connection.Close();

        return customer;
    }

    public void Delete( int id ){
        var connection = new SqliteConnection(DatabaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM customers WHERE (customer_id = $customer_id)";
        command.Parameters.AddWithValue("$id", id);

        command.ExecuteNonQuery();
        connection.Close();
    }

    public bool ExitsById( int id ) {
        var connection = new SqliteConnection(DatabaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT count(id) FROM customers WHERE (customer_id = $customer_id)";
        command.Parameters.AddWithValue("$customer_id", id);

        var reader = command.ExecuteReader();
        reader.Read();
        var result = reader.GetBoolean(0);

        return result;
    }

    private Customer ReaderToCustomer(SqliteDataReader reader) {
        var customer = new Customer(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6));
        return customer;
    }
}