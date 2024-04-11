namespace Nitrilon.Api.types
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int Attendees { get; set; }
        public string Description { get; set; }
    }
}
