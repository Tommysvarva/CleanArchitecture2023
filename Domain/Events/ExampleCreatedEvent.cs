namespace Domain.Events;
public class ExampleCreatedEvent : BaseEvent
{
    public Example Example { get; }
    
    public ExampleCreatedEvent(Example example)
    {
        Example = example;
    }
}