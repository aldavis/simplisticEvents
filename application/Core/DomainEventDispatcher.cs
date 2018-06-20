using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace application.Core
{
	public interface IDomainEventDispatcher
	{
		Task<Exception> Dispatch(Aggregate aggregate);
	}

    public class DomainEventDispatcher:IDomainEventDispatcher
    {
	    readonly ServiceFactory _serviceFactory;

	    public DomainEventDispatcher(ServiceFactory serviceFactory)
	    {
		    _serviceFactory = serviceFactory;
	    }


		public async Task<Exception> Dispatch(Aggregate aggregate)
	    {
		    foreach (var domainEvent in aggregate.Outbox.ToArray())
			{
				try
				{
					var handler = GetHandler(domainEvent);

					await handler.Handle(domainEvent, _serviceFactory);

					aggregate.ProcessDomainEvent(domainEvent);

				}
				catch (Exception ex)
				{
					return ex;
				}
			}

		    return null;
	    }

	    static DomainEventDispatcherHandler GetHandler(IDomainEvent domainEvent)
	    {
		    return (DomainEventDispatcherHandler)
			    Activator.CreateInstance(typeof(DomainEventDispatcherHandler<>).MakeGenericType(domainEvent.GetType()));
	    }

	    abstract class DomainEventDispatcherHandler
	    {
		    public abstract Task Handle(IDomainEvent domainEvent, ServiceFactory factory);
	    }

	    class DomainEventDispatcherHandler<T> : DomainEventDispatcherHandler
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
