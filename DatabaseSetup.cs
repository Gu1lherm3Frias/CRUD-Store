using Microsoft.Data.Sqlite;
namespace Store.Database;

class DatabaseSetup
{
    public DatabaseSetup()
    {
        CreateCustomerTable();
        CreateOrderTable();
        CreateItemTable();
        CreateProductTable();
        CreateSellerTable();
    }
    private void CreateCustomerTable()
    {
        using var connection = new SqliteConnection(DatabaseConfig.ConnectionString);
        connection.Open();
        if (connection.State == System.Data.ConnectionState.Open)
        {
            var command = connection.CreateCommand();
            command.CommandText = @"
                CREATE TABLE IF NOT EXISTS customer (
                    customer_id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                    name TEXT NOT NULL,
                    address TEXT NOT NULL,
                    city TEXT NOT NULL,
                    zip_code TEXT NOT NULL,
                    customer_uf TEXT NOT NULL,
                    customer_ie TEXT NOT NULL
                );
            ";
            command.ExecuteNonQuery();
            connection.Close();
        }

    }
    private void CreateOrderTable()
    {
        using var connection = new SqliteConnection(DatabaseConfig.ConnectionString);
        connection.Open();
        if (connection.State == System.Data.ConnectionState.Open) {
            var command = connection.CreateCommand();
            command.CommandText = @"
                CREATE TABLE IF NOT EXISTS order_tb (
                    order_id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                    deadline DATETIME NOT NULL,
                    order_date DATETIME NOT NULL,
                    customer_id INTEGER NOT NULL,
                    seller_id INTEGER NOT NULL
                );
            ";
            command.ExecuteNonQuery();
            connection.Close();
        }
        
    }

    private void CreateItemTable()
    {
        using var connection = new SqliteConnection(DatabaseConfig.ConnectionString);
        connection.Open();  
        if (connection.State == System.Data.ConnectionState.Open)
        {
            var command = connection.CreateCommand();
            command.CommandText = @"
                CREATE TABLE IF NOT EXISTS order_items (
                    item_id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                    order_id INTEGER NOT NULL,
                    product_id INTEGET NOT NULL,
                    amount INTEGER NOT NULL
                );
            ";
            command.ExecuteNonQuery();  
            connection.Close();
        }
    }

    private void CreateProductTable()
    {
        using var connection = new SqliteConnection(DatabaseConfig.ConnectionString);
        connection.Open();
        if (connection.State == System.Data.ConnectionState.Open)
        {
            var command = connection.CreateCommand();
            command.CommandText = @"
                CREATE TABLE IF NOT EXISTS product (
                    product_id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                    description TEXT NOT NULL,
                    unit_price DECIMAL(10,5) NOT NULL  
                );
            ";
            command.ExecuteNonQuery();
            connection.Close();
        }
    }

    private void CreateSellerTable()
    {
        using var connection = new SqliteConnection(DatabaseConfig.ConnectionString);
        connection.Open();
        if (connection.State == System.Data.ConnectionState.Open)
        {
            var command = connection.CreateCommand();
            command.CommandText = @"
                CREATE TABLE IF NOT EXISTS seller (
                    seller_id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                    name TEXT NOT NULL,
                    fixed_income DECIMAL(10,5) NOT NULL,
                    commision_rank TEXT NOT NULL
                )
            ";
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}