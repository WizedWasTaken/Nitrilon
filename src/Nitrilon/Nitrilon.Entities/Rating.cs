using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nitrilon.Entities
{
    public class Rating
    {
        private int id;
        private int ratingValue;
        private string description;

        public Rating(int id, int ratingValue, string description)
        {
            Id = id;
            RatingValue = ratingValue;
            Description = description;
        }

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

        public int RatingValue
        {
            get => ratingValue;
            set
            {
                if (value < 0 || value > 3)
                    throw new ArgumentException("RatingValue must be between 0 and 3.");

                ratingValue = value;
            }
        }

        public string Description
        {
            get => description;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Description cannot be empty.");

                description = value;
            }
        }

    }
}
