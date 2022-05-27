namespace Dev.MassTransitConsumer
{
    public record UserEvent
    {
        public Guid? Id { get; init; }
        public string? Name { get; set; }
    }
}
