using Store.Database;
using Store.Models;
using Microsoft.Data.Sqlite;

namespace Store.Repositories;

class SellerRepository
{
    public SellerRepository() { }

    public List<Seller> GetAll()
    {
        var items = new List<Seller>();

        var connection = new SqliteConnection(DatabaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM seller";

        var reader = command.ExecuteReader();

        while (reader.Read())
        {
            var id = reader.GetInt32(0);
            var name = reader.GetString(1);
            var fixedIncome = reader.GetDecimal(2);
            var commissionRank = reader.GetString(3);
            var item = ReaderToSeller(reader);
            items.Add(item);
        }

        connection.Close();
        return items;
    }

    public Seller Save(Seller seller)
    {
        var connection = new SqliteConnection(DatabaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO seller VALUES($seller_id, $name, $fixed_income, $commission_rank)";
        command.Parameters.AddWithValue("$seller_id", seller.SellerId);
        command.Parameters.AddWithValue("$name", seller.Name);
        command.Parameters.AddWithValue("$fixed_income", seller.FixedIncome);
        command.Parameters.AddWithValue("$commission_rank", seller.CommissionRank);

        command.ExecuteNonQuery();
        connection.Close();

        return seller;
    }

    public Seller GetById(int id)
    {
        var connection = new SqliteConnection(DatabaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM seller WHERE (seller_id = $seller_id)";
        command.Parameters.AddWithValue("$seller_id", id);

        var reader = command.ExecuteReader();
        reader.Read();

        var seller = ReaderToSeller(reader);

        connection.Close();
        return seller;
    }

    public Seller Update(Seller seller)
    {
        var connection = new SqliteConnection(DatabaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO seller VALUES ($seller_id, $name, $fixed_income, $commission_rank)";
        command.CommandText = "UPDATE seller SET name = $name, fixed_income = $fixed_income, commission_rank = $commmission_rank WHERE (seller_id = $seller_id)";
        command.Parameters.AddWithValue("$seller_id", seller.SellerId);
        command.Parameters.AddWithValue("$name", seller.Name);
        command.Parameters.AddWithValue("$fixed_income", seller.FixedIncome);
        command.Parameters.AddWithValue("$commission_rank", seller.CommissionRank);

        command.ExecuteNonQuery();
        connection.Close();

        return seller;
    }

    public void Delete(int id)
    {
        var connection = new SqliteConnection(DatabaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM seller WHERE (seller_id = $seller_id)";
        command.Parameters.AddWithValue("$seller_id", id);

        command.ExecuteNonQuery();
        connection.Close();
    }

    public bool ExitsById(int id)
    {
        var connection = new SqliteConnection(DatabaseConfig.ConnectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT count(id) FROM seller WHERE (seller_id = $seller_id)";
        command.Parameters.AddWithValue("$seller_id", id);

        var reader = command.ExecuteReader();
        reader.Read();
        var result = reader.GetBoolean(0);

        return result;
    }

    private Seller ReaderToSeller(SqliteDataReader reader)
    {
        var item = new Seller(reader.GetInt32(0), reader.GetString(1), reader.GetDecimal(2), reader.GetString(3));
        return item;
    }
}