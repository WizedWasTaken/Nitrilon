namespace Nitrilon.Entities
{
    public class Event
    {
        // Fields
        private int id;
        private string name;
        private DateTime date;
        private int attendees;
        private string description;
        private List<Rating> ratings;

        // Constructor
        public Event(int id, string name, DateTime date, int attendees, string description, List<Rating> ratings)
        {
            Id = id;
            Name = name;
            Date = date;
            Attendees = attendees;
            Description = description;
            // If ratings is null, throw an exception.
            this.ratings = ratings ?? throw new ArgumentException(nameof(ratings));
        }

        // Properties
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
    }
}
