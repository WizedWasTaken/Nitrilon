using Microsoft.Data.SqlClient;
using Nitrilon.Entities;

namespace Nitrilon.DataAccess
{
    public class Repository
    {
        private readonly string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=NitrilonDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        public List<Event> GetAllEvents()
        {
            List<Event> events = new List<Event>();

            string query = "SELECT * FROM Events";

            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["EventId"]);
                    DateTime date = Convert.ToDateTime(reader["Date"]);
                    string name = reader["Name"].ToString();
                    int attendees = Convert.ToInt32(reader["Attendees"]);
                    string description = reader["Description"].ToString();

                    Event newEvent = new Event
                    {
                        Id = id,
                        Date = date,
                        Name = name,
                        Attendees = attendees,
                        Description = description
                    };
                    events.Add(newEvent);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            finally
            {
                connection.Close();
            }



            return events;
        }

        public int Save(Event newEvent)
        {
            // TODO: handle attendees when the event isn't over.
            string query = $"INSERT INTO Events (Date, Name, Attendees, Description) " +
                           $"VALUES ('{newEvent.Date.ToString("yyyy-MM-dd")}', '{newEvent.Name}', {newEvent.Attendees}, '{newEvent.Description}')";

            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();

            try
            {
                int rowsAffected = command.ExecuteNonQuery();

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
    }
}
