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
        private List<Rating> ratings;
        #endregion

        #region Constructors
        public Event(int id, string name, DateTime date, int attendees, string description, List<Rating> ratings)
        {
            Id = id;
            Name = name;
            Date = date;
            Attendees = attendees;
            Description = description;
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
        public void Add(Rating rating)
        {
            // If rating is null, throw an exception.
            if (rating == null)
                throw new ArgumentNullException(nameof(rating));

            ratings.Add(rating);
        }

        public double GetAverageRating()
        {
            // If ratings is null, throw an exception.
            if (ratings == null)
                throw new ArgumentNullException(nameof(ratings));

            // If ratings is empty, return 0.
            if (ratings.Count == 0)
                return 0;

            // Calculate the average rating.
            double sum = 0;
            foreach (Rating rating in ratings)
            {
                sum += rating.RatingValue;
            }

            return sum / ratings.Count;
        }
        #endregion
    }
}
