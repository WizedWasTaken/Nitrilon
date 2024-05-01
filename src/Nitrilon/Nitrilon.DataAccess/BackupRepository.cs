using System.Diagnostics;
using Microsoft.Data.SqlClient;
using Nitrilon.Entities;

namespace Nitrilon.DataAccess
{
    public class BackupRepository
    {
        private readonly string connectionString;

        public BackupRepository()
        {
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=NitrilonDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
            }
            else
            {
                connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
            }
        }

        // !! EVENT METHODS !!

        /// <summary>
        /// Method to get all events from the database.
        /// </summary>
        /// <returns>List with events.</returns>
        /// <returns>Null if an exception occurs.</returns
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

                    Event newEvent = new Event(id, name, date, attendees, description);
                    events.Add(newEvent);
                }
            }
            catch
            {
                throw new ArgumentException("An error occurred while getting the events.");
            }
            finally
            {
                connection.Close();
            }

            return events;
        }

        /// <summary>
        /// The method to get a specific event from the database.
        /// </summary>
        /// <param name="id">EventId</param>
        /// <returns>Event</returns>
        public Event GetEventById(int id)
        {
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
                    EventRatingData ratings = GetRatingsByEvent(eventId);

                    Event newEvent = new Event(eventId, name, date, attendees, description, ratings);
                    return newEvent;
                }

                throw new ArgumentException("Event not found.");
            }
            catch
            {
                throw new ArgumentException("An error occurred while getting the event.");
            }
            finally
            {
                connection.Close();
            }
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
                while (SqlDataReader.Read())
                {
                    newId = (int)SqlDataReader.GetDecimal(0);
                }

                return newId;
            }
            catch
            {
                throw new ArgumentException("An error occurred while saving the event.");
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
                throw new ArgumentException("An error occurred while updating the event.");
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
                throw new ArgumentException("An error occurred while deleting the event.");
            }
            finally
            {
                connection.Close();
            }
        }

        // !! RATING METHODS !!

        /// <summary>
        /// Method to get all ratings for an event.
        /// </summary>
        /// <param name="id">EventId</param>
        /// <returns>List of event ratings</returns>
        public EventRatingData GetRatingsByEvent(int eventId)
        {
            EventRatingData ratings = default;

            string query = $"EXEC CountAllowedRatingsForEvent @EventId = {eventId}";

            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int HappyRating = Convert.ToInt32(reader["RatingId1Count"]);
                    int NeutralRating = Convert.ToInt32(reader["RatingId2Count"]);
                    int SadRating = Convert.ToInt32(reader["RatingId3Count"]);

                    ratings = new EventRatingData(HappyRating, NeutralRating, SadRating);
                }

                return ratings;
            }
            catch
            {
                throw new ArgumentException("An error occurred while getting the ratings.");
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Event to get all events after a certain date.
        /// </summary>
        /// <param name="date">Date to search after</param>
        /// <returns>List of events after the date argument</returns>
        public List<Event> GetEventsAfterDate(DateTime date)
        {
            List<Event> events = new List<Event>();

            string query = $"SELECT * FROM Events WHERE Date >= '{date.ToString("yyyy-MM-dd")}'";

            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["EventId"]);
                    DateTime newDate = Convert.ToDateTime(reader["Date"]);
                    string name = reader["Name"].ToString();
                    int attendees = Convert.ToInt32(reader["Attendees"]);
                    string description = reader["Description"].ToString();

                    Event newEvent = new Event(id, name, newDate, attendees, description);
                    events.Add(newEvent);
                }
            }
            catch
            {
                throw new ArgumentException("An error occurred while getting the events.");
            }
            finally
            {
                connection.Close();
            }

            return events;
        }

        /// <summary>
        /// Function to create a new rating for an event.
        /// </summary>
        /// <param name="id">EventId</param>
        /// <param name="grade">Grade to give the event</param>
        /// <returns></returns>
        public int CreateRating(int id, int grade)
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
                throw new ArgumentException("An error occurred while saving the rating.");
            }
            finally
            {
                connection.Close();
            }
        }


        // !! MEMBER METHODS !!

        /// <summary>
        /// Method to create a new member in the database.
        /// </summary>
        /// <param name="member">Member object</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public int Create(Member member)
        {
            int newId = -1;

            string query = $"INSERT INTO Members (Name, PhoneNumber, Email, EnrollmentDate, MembershipId) " +
                           $"VALUES ('{member.Name}', '{member.PhoneNumber}', '{member.Email}', '{member.EnrollmentDate.ToString("yyyy-MM-dd")}', {member.Membership.MembershipId});" +
                           $"SELECT SCOPE_IDENTITY();";

            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();

            try
            {
                SqlDataReader SqlDataReader = command.ExecuteReader();
                while (SqlDataReader.Read())
                {
                    newId = (int)SqlDataReader.GetDecimal(0);
                }

                return newId;
            }
            catch
            {
                throw new ArgumentException("An error occurred while saving the member.");
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Method to get all members from the database with their membership status.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public List<Member> GetAllMembers()
        {
            List<Member> members = new List<Member>();

            // Tak til SSMS for JOIN queryen :)
            string query = @"SELECT dbo.Memberships.Name AS MembershipName, dbo.Memberships.Description AS MembershipDescription, dbo.Members.* FROM dbo.Members INNER JOIN
                         dbo.Memberships ON dbo.Members.MembershipId = dbo.Memberships.MembershipId";

            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["MemberId"]);
                    string name = reader["Name"].ToString();
                    string phoneNumber = reader["PhoneNumber"].ToString();
                    string email = reader["Email"].ToString();
                    bool isDeleted = Convert.ToBoolean(reader["IsDeleted"]);
                    DateTime enrollmentDate = Convert.ToDateTime(reader["EnrollmentDate"]);
                    Membership membership = new Membership(Convert.ToInt32(reader["MembershipId"]), reader["MembershipName"].ToString(), reader["MembershipDescription"].ToString());

                    Member newMember = new Member(id, name, phoneNumber, email, isDeleted, enrollmentDate, membership);
                    members.Add(newMember);
                }
            }
            catch
            {
                throw new ArgumentException("An error occurred while getting the members.");
            }
            finally
            {
                connection.Close();
            }

            return members;
        }


        /// <summary>
        /// Method to get a specific member from the database.
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public Boolean SoftDelete(int MemberId)
        {
            string query = $"UPDATE Members SET IsDeleted = CASE WHEN IsDeleted = 1 THEN 0 ELSE 1 END WHERE MemberId = {MemberId}";

            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();

            try
            {
                command.ExecuteNonQuery();

                return true;
            }
            catch
            {
                throw new ArgumentException("An error occurred while deleting the member.");
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Method to update a member in the database.
        /// </summary>
        /// <param name="updatedMember">New member object</param>
        /// <returns>New member object</returns>
        /// <exception cref="ArgumentException"></exception>
        public Member Update(Member updatedMember)
        {
            string query = $"UPDATE Members " +
                           $"SET Name = '{updatedMember.Name}', " +
                           $"PhoneNumber = '{updatedMember.PhoneNumber}', " +
                           $"Email = '{updatedMember.Email}', " +
                           $"EnrollmentDate = '{updatedMember.EnrollmentDate.ToString("yyyy-MM-dd")}', " +
                           $"MembershipId = {updatedMember.Membership.MembershipId}" +
                           $" WHERE MemberId = {updatedMember.MemberId}";
            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();

            try
            {
                command.ExecuteNonQuery();

                return updatedMember;
            }
            catch
            {
                throw new ArgumentException("An error occurred while updating the member.");
            }
            finally
            {
                connection.Close();
            }
        }
    }
}

