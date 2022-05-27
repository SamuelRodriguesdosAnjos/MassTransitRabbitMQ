//Esse namespace deve ser igual do consumidor
namespace Users
{
    public record UserEvent
    {
        public UserEvent()
        {
        }
        public UserEvent(string name)
        {
            Name = name;
        }

        public Guid Id { get; init; } = Guid.NewGuid();
        public string? Name { get; init; }
    }
}
