namespace ProtoActor.PoC.Persistence.Marten
{
    public record struct Event<T>
    {
        public required string ActorName { get; init; }
        public required long Index { get; init; }
        public required T Data { get; init; }
        public required string Id { get; init; }
    }
}
