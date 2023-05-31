using System.Data;
using Microsoft.Data.SqlClient;
using WeatherMusicBot.Entity;

namespace WeatherMusicBot.Services;

public class DbSenderService
{
    private static SqlConnection connection;

    public DbSenderService()
    {
        var connectionString = "Data Source=DESKTOP-NA9VG7U;Initial Catalog=UniversityDB;TrustServerCertificate=true;Persist Security Info=True;User ID=user1;Password=sa";

        connection = new SqlConnection(connectionString);
        
    }

    public async Task<string> SendSqlRequest(User user)
    {
        connection.Open();
        if (connection.State != ConnectionState.Open)
        {
            return ("Connection failed!");
        }


        using (var command = new SqlCommand("INSERT INTO [dbo].[User]" +
                                            " (Id, UserName, Name, Latitude, Longitude, RegistrationDate)" +
                                            " VALUES (@Id, @Username, @Name, @Latitude, @Longitude, @RegistrationDate)", connection))
        {
            command.Parameters.AddWithValue("@Id", user.ChatId);
            command.Parameters.AddWithValue("@Username", user.Username);
            command.Parameters.AddWithValue("@Name", user.Firstname);
            command.Parameters.AddWithValue("@Latitude", user.Latitude);
            command.Parameters.AddWithValue("@Longitude", user.Longitude);
            command.Parameters.AddWithValue("@RegistrationDate", DateTime.Now);

            command.ExecuteNonQuery();
        }
        return ("ID does not exist in database.");
        
    }

    public async Task<bool> SendAuthorizationRequest(User user)
    {
        try
        {
            var fetchedId = 0;
            await using (var command = new SqlCommand("SELECT Id FROM [dbo].[User] WHERE Id = @Id", connection))
            {
                command.Parameters.AddWithValue("@Id", user.ChatId);
                await connection.OpenAsync();
                await using var reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    fetchedId = (int)reader["Id"];
                }

                await reader.CloseAsync();
            }

            await connection.CloseAsync();

            return fetchedId == user.ChatId;
        }
        catch (Exception ex)
        {
           return false;
        }
    }
    public async Task<(double Latitude, double Longitude)?> GetUserLocation(long chatId)
    {
        try
        {
            double? latitude = null;
            double? longitude = null;

            await using (var command = new SqlCommand("SELECT Latitude, Longitude FROM [dbo].[User] WHERE Id = @Id", connection))
            {
                command.Parameters.AddWithValue("@Id", chatId);
                await connection.OpenAsync();
                await using var reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    latitude = (double)reader["Latitude"];
                    longitude = (double)reader["Longitude"];
                }

                await reader.CloseAsync();
            }

            await connection.CloseAsync();

            if (latitude.HasValue && longitude.HasValue)
                return (latitude.Value, longitude.Value);
        
            return null;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    
    public async Task<string> UpdateUser(User user)
    {
        connection.Open();
        if (connection.State != ConnectionState.Open)
        {
            return "Connection failed!";
        }

        using (var command = new SqlCommand("UPDATE [dbo].[User] SET UserName = @Username, Name = @Name, Latitude = @Latitude, Longitude = @Longitude, RegistrationDate = @RegistrationDate WHERE Id = @Id", connection))
        {
            command.Parameters.AddWithValue("@Id", user.ChatId);
            command.Parameters.AddWithValue("@Username", user.Username);
            command.Parameters.AddWithValue("@Name", user.Firstname);
            command.Parameters.AddWithValue("@Latitude", user.Latitude);
            command.Parameters.AddWithValue("@Longitude", user.Longitude);
            command.Parameters.AddWithValue("@RegistrationDate", DateTime.Now);

            var rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected == 0)
            {
                return "No user found with given ID.";
            }
        }

        return "User updated successfully.";
    }


}