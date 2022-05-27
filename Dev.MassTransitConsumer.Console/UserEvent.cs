//Esse namespace deve ser igual do produtor
namespace Users
{
    public record UserEvent
    {
        public Guid? Id { get; init; }
        public string? Name { get; set; }
    }
}
