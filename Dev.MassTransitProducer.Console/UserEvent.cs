//Esse namespace deve ser igual do consumidor
namespace Users
{
    public record UserEvent
    {
        public UserEvent()
        {
        }

        public Guid Id { get; init; } = Guid.NewGuid();
        public string? Name { get; init; }
    }
}
