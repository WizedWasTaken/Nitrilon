using Microsoft.Data.SqlClient;

namespace Nitrilon.DataAccess
{
    public class Repository
    {
        private string connectionString;
        private SqlConnection connection;

        /// <summary>
        /// Constructor for the Repository class.
        /// Checking if the connection to the database can be established.
        /// </summary>
        /// <exception cref="Exception"></exception>
        public Repository()
        {
            // Needed because of the app being deployed to Azure aswell.
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=NitrilonDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
            }
            else
            {
                connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
            }

            // If statement checking if the connection to the database can be established.
            if (!CanConnect())
            {
                throw new Exception("Cannot connect to the database");
            }
        }

        /// <summary>
        /// Method to close the connection to the database.
        /// </summary>
        protected void CloseConnection()
        {
            if (connection != null && connection.State != System.Data.ConnectionState.Closed)
            {
                connection.Close();
                return;
            }

            throw new Exception("Connection is already closed");
        }


        /// <summary>
        /// Method to open a connection to the database.
        /// </summary>
        protected void OpenConnection()
        {
            if (connection != null && connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
                return;
            }

            throw new Exception("Connection is already open");
        }

        /// <summary>
        /// Method to execute a query to the database.
        /// </summary>
        /// <param name="sql">SQL query</param>
        /// <returns>SqlDataReader</returns>
        /// <exception cref="ArgumentNullException"></exception>
        protected SqlDataReader Execute(string sql)
        {
            if (sql is null)
            {
                throw new ArgumentNullException(nameof(sql));
            }

            try
            {
                connection = new(connectionString);
                SqlCommand command = new(sql, connection);
                OpenConnection();
                SqlDataReader reader = command.ExecuteReader();

                return reader;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Method to check if the connection to the database can be established.
        /// </summary>
        /// <returns>Boolean</returns>
        public bool CanConnect()
        {
            try
            {
                SqlConnection sqlConnection = new(connectionString);
                sqlConnection.Open();
                sqlConnection.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}