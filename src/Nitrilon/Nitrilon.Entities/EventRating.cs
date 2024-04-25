namespace Nitrilon.Entities
{
    public class EventRating
    {
        private int id;
        private int eventId;
        private int ratingId;

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

        public int EventId
        {
            get => eventId;
            set
            {
                if (eventId <= 0)
                    throw new ArgumentException("EventId must be above 0");

                eventId = value;
            }
        }

        public int RatingId
        {
            get => ratingId;
            set
            {
                if (ratingId <= 0)
                    throw new ArgumentException("RatingId must be above 0");

                ratingId = value;
            }
        }
    }
}
