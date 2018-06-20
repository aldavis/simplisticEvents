using System;
using System.Threading.Tasks;

namespace application.Core
{
	public interface IDomainEvent
	{
		Guid Id { get; }
	}

	public interface IDomainEventHandler<in T>
		where T : IDomainEvent
	{
		Task Handle(T domainEvent);
	}
}
