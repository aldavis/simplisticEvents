using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace application.Core
{
	public interface IDomainEventProcessor
	{
		Task<Exception> ProcessEvents(Aggregate aggregate);
	}

    public class DomainEventProcessor: IDomainEventProcessor
	{
	    readonly ServiceFactory _serviceFactory;

	    public DomainEventProcessor(ServiceFactory serviceFactory)
	    {
		    _serviceFactory = serviceFactory;
	    }


		public async Task<Exception> ProcessEvents(Aggregate aggregate)
	    {
		    foreach (var domainEvent in aggregate.Outbox.ToArray())
			{
				try
				{
					var handler = GetHandler(domainEvent);

					await handler.Handle(domainEvent, _serviceFactory);

					aggregate.ClearEvent(domainEvent);

				}
				catch (Exception ex)
				{
					return ex;
				}
			}

		    return null;
	    }

	    static DomainEventHandler GetHandler(IDomainEvent domainEvent)
	    {
		    return (DomainEventHandler)
			    Activator.CreateInstance(typeof(DomainEventHandler<>).MakeGenericType(domainEvent.GetType()));
	    }

	    abstract class DomainEventHandler
	    {
		    public abstract Task Handle(IDomainEvent domainEvent, ServiceFactory factory);
	    }

	    class DomainEventHandler<T> : DomainEventHandler
		    where T : IDomainEvent
	    {
		    public override Task Handle(IDomainEvent domainEvent, ServiceFactory factory)
		    {
			    return HandleCore((T)domainEvent, factory);
		    }

		    static async Task HandleCore(T domainEvent, ServiceFactory factory)
		    {
			    var handlers = factory.GetInstances<IDomainEventHandler<T>>();
			    foreach (var handler in handlers)
			    {
				    await handler.Handle(domainEvent);
			    }
		    }
	    }
	}


}
