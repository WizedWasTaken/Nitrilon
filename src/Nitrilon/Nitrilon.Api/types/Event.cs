namespace Nitrilon.Api.types
{
    public class Event
    {
        public int Id { get; set; } // Temporary, remove when database is implemented (use auto increment)
        public string Name { get; set; }
        public string Date { get; set; }
        public string Attendees { get; set; }
        public string Description { get; set; }
    }
}
