using Order.Domain.Events;

namespace Order.Domain.Entities;

public class Aggregate
{
	private readonly List<IDomainEvent> _events = new();

	// Properties
	public Guid Id { get; set; } = Guid.NewGuid();
	public IEnumerable<IDomainEvent> Events => _events;
	public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
	public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;

	// Methods
	protected void AddEvent(IDomainEvent @event)
	{
		_events.Add(@event);
	}
}