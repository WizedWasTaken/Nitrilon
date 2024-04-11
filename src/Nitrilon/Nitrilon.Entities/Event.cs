namespace Nitrilon.Entities
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateOnly Date { get; set; }
        public int Attendees { get; set; }
        public string Description { get; set; }
    }
}
