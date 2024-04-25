using System.Text.Json.Serialization;

namespace Nitrilon.Entities
{
    public class Event
    {
        #region Fields
        private int id;
        private string name;
        private DateTime date;
        private int attendees;
        private string description;
        private EventRatingData ratings;
        #endregion

        #region Constructors
        public Event(int id, string name, DateTime date, int attendees, string description)
        {
            Id = id;
            Name = name;
            Date = date;
            Attendees = attendees;
            Description = description;
        }

        public Event(int id, string name, DateTime date, int attendees, string description, EventRatingData ratings) :
            this(id, name, date, attendees, description)
        {
            // If ratings is null, throw an exception.
            this.ratings = ratings ?? throw new ArgumentNullException(nameof(ratings));
        }

        #endregion

        #region Properties
        public int Id
        {
            get => id;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Id must be a positive number.");

                id = value;
            }
        }

        public string Name
        {
            get => name;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Name cannot be empty.");

                name = value;
            }
        }

        public DateTime Date
        {
            get => date;
            set => date = value;
        }

        public int Attendees
        {
            get => attendees;
            set
            {
                if (value < -1)
                    throw new ArgumentException("Attendees must be a positive number.");

                attendees = value;
            }
        }

        public string Description
        {
            get => description;
            set => description = value;
        }

        #endregion

        #region Methods
        // TODO: Think of useful methods for the Event class.
        #endregion
    }
}
