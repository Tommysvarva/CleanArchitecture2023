namespace Domain.Events;

public class ExampleDeletedEvent: BaseEvent
{
    public Example Example { get; }
    
    public ExampleDeletedEvent(Example example)
    {
        Example = example;
    }
}