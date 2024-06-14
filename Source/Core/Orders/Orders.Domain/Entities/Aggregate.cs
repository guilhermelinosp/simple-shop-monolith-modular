using MongoDB.Bson.Serialization.Attributes;
using Order.Domain.Events;

namespace Order.Domain.Entities;

public class Aggregate
{
	private readonly List<IDomainEvent> _events = [];

	[BsonIgnore]
	public Guid Id { get; set; } = Guid.NewGuid();
	
	[BsonIgnore]
	public IEnumerable<IDomainEvent> Events => _events;
	
	[BsonIgnore]
	public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
	
	[BsonIgnore]
	public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;

	// Methods
	protected void AddEvent(IDomainEvent @event)
	{
		_events.Add(@event);
	}
}