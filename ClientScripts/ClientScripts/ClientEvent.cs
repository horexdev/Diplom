namespace Script;

public class ClientEvent : Attribute
{
    public ClientEvent(string eventName) => EventName = eventName;

    public string EventName { get; set; }
}