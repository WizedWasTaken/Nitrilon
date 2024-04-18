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

        // Constructor
        public Event(int id, string name, DateTime date, int attendees, string description)
        {
            Id = id;
            Name = name;
            Date = date;
            Attendees = attendees;
            Description = description;
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
