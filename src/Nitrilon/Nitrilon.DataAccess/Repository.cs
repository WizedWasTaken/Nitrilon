using System.Diagnostics;
using Microsoft.Data.SqlClient;
using Nitrilon.Entities;

namespace Nitrilon.DataAccess
{
    public class Repository
    {
        private readonly string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=NitrilonDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        // !! EVENT METHODS !!

        /// <summary>
        /// Method to get all events from the database.
        /// </summary>
        /// <returns>List with events.</returns>
        /// <returns>Null if an exception occurs.</returns>
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

        public Event GetEventById(int id)
        {
            Event newEvent = new Event();

            string query = $"SELECT * FROM Events WHERE EventId = {id}";

            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int eventId = Convert.ToInt32(reader["EventId"]);
                    DateTime date = Convert.ToDateTime(reader["Date"]);
                    string name = reader["Name"].ToString();
                    int attendees = Convert.ToInt32(reader["Attendees"]);
                    string description = reader["Description"].ToString();

                    newEvent = new Event
                    {
                        Id = eventId,
                        Date = date,
                        Name = name,
                        Attendees = attendees,
                        Description = description
                    };
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

            return newEvent;
        }

        /// <summary>
        /// Method to save a new event to the database.
        /// </summary>
        /// <param name="newEvent">Event sent by controller, that needs to be saved.</param>
        /// <returns>integer of rows affected.</returns>
        /// <returns>-1 if an exception occurs.</returns>
        public int Save(Event newEvent)
        {
            int newId = -1;
            // TODO: handle attendees when the event isn't over.
            string query = $"INSERT INTO Events (Date, Name, Attendees, Description) " +
                           $"VALUES ('{newEvent.Date.ToString("yyyy-MM-dd")}', '{newEvent.Name}', {newEvent.Attendees}, '{newEvent.Description}');" +
                           $"SELECT SCOPE_IDENTITY();";

            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();

            try
            {
                SqlDataReader SqlDataReader = command.ExecuteReader();
                // Get the new id of the event.
                while (SqlDataReader.Read())
                {
                    newId = (int)SqlDataReader.GetDecimal(0);
                }

                return newId;
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

        /// <summary>
        /// Method to update an event in the database.
        /// </summary>
        /// <param name="updatedEvent">new event</param>
        /// <returns>updated event</returns>
        /// <returns>null in case of error</returns>
        public Event Update(Event updatedEvent)
        {
            string query = $"UPDATE Events " +
                           $"SET Date = '{updatedEvent.Date.ToString("yyyy-MM-dd")}', " +
                           $"Name = '{updatedEvent.Name}', " +
                           $"Attendees = {updatedEvent.Attendees}, " +
                           $"Description = '{updatedEvent.Description}'" +
                           $" WHERE EventId = {updatedEvent.Id}";
            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();

            try
            {
                command.ExecuteNonQuery();

                return updatedEvent;
            }
            catch
            {
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Method to delete an event from the database.
        /// </summary>
        /// <param name="id">Id of the event</param>
        /// <returns>the amount of rows affected.</returns>
        public int DeleteEvent(int id)
        {
            string query = $"DELETE FROM Events WHERE EventId = {id}";

            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();

            try
            {
                int rowsAffected = command.ExecuteNonQuery();

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

        // !! RATING METHODS !!
        public List<EventRating> GetRatingsByEvent(int id)
        {
            List<EventRating> ratings = new List<EventRating>();

            string query = $"SELECT * FROM EventRatings WHERE EventId = {id}";

            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    EventRating newEventRating = new EventRating
                    {
                        Id = Convert.ToInt32(reader["EventRatingId"]),
                        EventId = Convert.ToInt32(reader["EventId"]),
                        RatingId = Convert.ToInt32(reader["RatingId"]),
                    };

                    ratings.Add(newEventRating);
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

            return ratings;
        }

        public int Create(int id, int grade)
        {
            int newId = -1;

            string query = $"INSERT INTO EventRatings (EventId, RatingId) " +
                           $"VALUES ({id}, {grade});" +
                           $"SELECT SCOPE_IDENTITY();";

            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();
            Debug.WriteLine("");

            try
            {
                SqlDataReader SqlDataReader = command.ExecuteReader();
                while (SqlDataReader.Read())
                {
                    newId = (int)SqlDataReader.GetDecimal(0);
                    Debug.WriteLine(newId);
                }

                return newId;
            }
            catch
            {
                return newId;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
