﻿namespace Nitrilon.Entities
{
    public class EventRating
    {
        public int Id { get; set; } // Temporary, remove when database is implemented (use auto increment)
        public int EventId { get; set; }
        public int RatingId { get; set; }
    }
}