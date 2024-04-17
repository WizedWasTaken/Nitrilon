namespace Nitrilon.Entities
{
    public class EventRating
    {
        private int id;

        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Id must be a positive number.");
                }

                id = value;
            }
        }


        private int eventId;

        public int EventId
        {
            get
            {
                return eventId;
            }
            set
            {
                if (eventId <= 0)
                {
                    throw new ArgumentException("EventId must be above 0");
                }

                eventId = value;
            }
        }

        private int ratingId;

        public int RatingId
        {
            get
            {
                return ratingId;
            }
            set
            {
                if (ratingId <= 0)
                {
                    throw new ArgumentException("RatingId must be above 0");
                }

                ratingId = value;
            }
        }
    }
}
