using Microsoft.Data.SqlClient;
using System.Data;

namespace Nitrilon.Api;

public class DataContext : IDisposable
{
    private readonly string _connectionString;
    private SqlConnection _connection;

    public DataContext(IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("DefaultConnection"); // Jeg har den i appsettings.json
        _connection = new SqlConnection(connectionString);
    }

    public async Task OpenAsync()
    {
        if (_connection.State != ConnectionState.Open)
            await _connection.OpenAsync();
    }

    public async Task CloseAsync()
    {
        if (_connection.State != ConnectionState.Closed)
            await _connection.CloseAsync();
    }

    public async Task<List<T>> ExecuteQueryAsync<T>(string query, Func<SqlDataReader, T> selector)
    {
        try
        {
            List<T> results = new List<T>();
            await OpenAsync();

            using (var command = new SqlCommand(query, _connection))
            {
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        results.Add(selector(reader));
                    }
                }
            }

            return results;
        }
        catch
        {
            return new List<T>();
        }
        finally
        {
            await CloseAsync();
        }

    }

    public async Task<int> ExecuteNonQueryAsync(string query)
    {
        try
        {
            await OpenAsync();
            using (var command = new SqlCommand(query, _connection))
            {
                return await command.ExecuteNonQueryAsync();
            }
        }
        catch
        {
            return -1;
        }
        finally
        {
            await CloseAsync();
        }
    }

    public void Dispose()
    {
        if (_connection != null)
        {
            _connection.Dispose();
            _connection = null;
        }
    }
}