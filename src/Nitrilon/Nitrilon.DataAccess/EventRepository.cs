using System.Runtime.CompilerServices;
using Microsoft.Data.SqlClient;
using Nitrilon.Entities;

namespace Nitrilon.DataAccess;

public class EventRepository : Repository
{
    #region Constructors
    /// <summary>
    /// Constructor for the EventRepository class.
    /// Calling the base class constructor. - called Constructor Chaining
    /// </summary>
    public EventRepository() : base()
    {
    }
    #endregion

    #region Methods

    public List<Event> GetAllEvents()
    {
        List<Event> events = new List<Event>();

        try
        {
            string query = "SELECT * FROM Events";

            SqlDataReader reader = Execute(query);

            while (reader.Read())
            {
                int id = Convert.ToInt32(reader["EventId"]);
                DateTime date = Convert.ToDateTime(reader["Date"]);
                string name = Convert.ToString(reader["Name"]);
                int attendees = Convert.ToInt32(reader["Attendees"]);
                string description = Convert.ToString(reader["Description"]);

                Event e = new(id, name, date, attendees, description);

                events.Add(e);
            }

            return events;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            CloseConnection();
        }
    }

    public Event GetEventById(int id)
    {
        Event e = new(0, "", DateTime.Now, 0, "");

        try
        {
            string query = $"SELECT * FROM Events WHERE EventId = {id}";

            SqlDataReader reader = Execute(query);

            while (reader.Read())
            {
                e.Id = Convert.ToInt32(reader["EventId"]);
                e.Date = Convert.ToDateTime(reader["Date"]);
                e.Name = Convert.ToString(reader["Name"]);
                e.Attendees = Convert.ToInt32(reader["Attendees"]);
                e.Description = Convert.ToString(reader["Description"]);
            }

            return e;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            CloseConnection();
        }
    }

    public List<Event> GetEventsAfterDate(DateTime date)
    {
        List<Event> events = new List<Event>();

        try
        {
            string query = $"SELECT * FROM Events WHERE Date > '{date}'";

            SqlDataReader reader = Execute(query);

            while (reader.Read())
            {
                int id = Convert.ToInt32(reader["EventId"]);
                DateTime eventDate = Convert.ToDateTime(reader["Date"]);
                string name = Convert.ToString(reader["Name"]);
                int attendees = Convert.ToInt32(reader["Attendees"]);
                string description = Convert.ToString(reader["Description"]);

                Event e = new(id, name, eventDate, attendees, description);

                events.Add(e);
            }

            return events;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            CloseConnection();
        }
    }

    public int CreateEvent(Event e)
    {
        try
        {
            string query =
                $"INSERT INTO Events (Name, Date, Attendees, Description) VALUES ('{e.Name}', '{e.Date}', {e.Attendees}, '{e.Description}'); SELECT SCOPE_IDENTITY";

            SqlDataReader reader = Execute(query);
            while (reader.Read())
            {
                return Convert.ToInt32(reader[0]);
            }

            return 0;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            CloseConnection();
        }
    }

    public Event UpdateEvent(Event e)
    {
        try
        {
            string query =
                $"UPDATE Events SET Name = '{e.Name}', Date = '{e.Date}', Attendees = {e.Attendees}, Description = '{e.Description}' WHERE EventId = {e.Id}";

            Execute(query);

            return e;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            CloseConnection();
        }
    }

    public void DeleteEvent(int id)
    {
        try
        { // Maybe add error handling to if it didn't delete anything.
            string query = $"DELETE FROM Events WHERE EventId = {id}";

            Execute(query);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            CloseConnection();
        }
    }

    // Event Ratings

    public EventRatingData GetRatingsByEvent(int eventId)
    {
        EventRatingData rating = default;
        try
        {
            string query = $"EXEC CountAllowedRatingsForEvent @EventId = {eventId}";

            SqlDataReader reader = Execute(query);

            while (reader.Read())
            {
                int HappyRating = Convert.ToInt32(reader["RatingId1Count"]);
                int NeutralRating = Convert.ToInt32(reader["RatingId2Count"]);
                int SadRating = Convert.ToInt32(reader["RatingId3Count"]);

                rating = new EventRatingData(HappyRating, NeutralRating, SadRating);
            }

            return rating;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            CloseConnection();
        }
    }

    public int CreateRating(int eventId, int ratingId)
    {
        try
        {
            string query = $"INSERT INTO EventRatings (EventId, RatingId) VALUES ({eventId}, {ratingId}); SELECT SCOPE_IDENTITY";

            SqlDataReader reader = Execute(query);
            while (reader.Read())
            {
                return Convert.ToInt32(reader[0]);
            }

            return 0;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            CloseConnection();
        }
    }

    #endregion
}