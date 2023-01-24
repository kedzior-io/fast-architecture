namespace FastArchitecture.Domain.Abstractions
{
    public abstract class Entity
    {
        public Guid Id { get; private set; }
    }
}