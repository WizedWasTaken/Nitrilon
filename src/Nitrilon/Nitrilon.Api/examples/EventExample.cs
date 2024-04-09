using Nitrilon.Api.types;
using Swashbuckle.AspNetCore.Filters;
namespace Nitrilon.Api.schemas;

public class EventExample : IExamplesProvider<Event>
{
    public Event GetExamples()
    {
        return new Event
        {
            Id = 1,
            Name = "Event Name",
            Date = "2024-06-04",
            Attendees = "300",
            Description = "Event description"
        };
    }
}