namespace Nitrilon.Entities
{
    public class Event
    {
        private int id;

        public int Id
        {
            get { return id; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Id must be a positive number.");
                }
                id = value;
            }
        }

        private string name;

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Name cannot be empty.");

                bool isNumeric = int.TryParse(value, out int n);
                if (isNumeric)
                    throw new ArgumentException("Name cannot be a number.");

                name = value;
            }
        }

        private DateTime date;

        public DateTime Date
        {
            get
            {
                return date;
            }
            set
            {
                if (value < DateTime.Now)
                    throw new ArgumentException("Date cannot be in the past.");


            }
        }

        public int Attendees { get; set; }
        public string Description { get; set; }
    }
}
