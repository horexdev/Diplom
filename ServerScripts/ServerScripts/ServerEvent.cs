namespace Script;

public class ServerEvent : Attribute
{
    public ServerEvent(string eventName) => EventName = eventName;

    public string EventName { get; set; }
}