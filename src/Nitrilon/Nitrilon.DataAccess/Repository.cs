using Microsoft.Data.SqlClient;
using Nitrilon.Entities;

namespace Nitrilon.DataAccess
{
    public class Repository
    {
        private readonly string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=NitrilonDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        public int Save(Event newEvent)
        {
            // TODO: handle attendees when the event isn't over.
            string query = $"INSERT INTO Events (Date, Name, Attendees, Description) " +
                           $"VALUES ('{newEvent.Date.ToString("yyyy-MM-dd")}', '{newEvent.Name}', {newEvent.Attendees}, '{newEvent.Description}')";

            // 1: make a SqlConnection object
            SqlConnection connection = new SqlConnection(connectionString);

            // 2: make a SqlCommand object
            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                // 3: open the connection
                connection.Open();

                // 4: execute the command
                int rowsAffected = command.ExecuteNonQuery();

                // 5: close the connection
                connection.Close();

                return rowsAffected;
            }
            catch
            {
                return -1;
            }
            finally
            {
                connection.Close();
            }
        }

        public List<Event> getAllEvents()
        {
            List<Event> events = new List<Event>();
            string query = "SELECT * FROM Events";

            // 1: make a SqlConnection object
            SqlConnection connection = new SqlConnection(connectionString);

            // 2: make a SqlCommand object
            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                // 3: open the connection
                connection.Open();

                // 4: execute the command
                SqlDataReader reader = command.ExecuteReader();

                // 5: read the results
                while (reader.Read())
                {
                    Event e = new Event();
                    e.Id = reader.GetInt32(0);
                    e.Date = reader.GetDateTime(1);
                    e.Name = reader.GetString(2);
                    e.Attendees = reader.GetInt32(3);
                    e.Description = reader.GetString(4);
                    events.Add(e);
                }

                // 6: close the reader
                reader.Close();

                // 7: close the connection
                connection.Close();
            }
            catch
            {
                return null;
            }
            finally
            {
                connection.Close();
            }

            return events;
        }
    }
}
