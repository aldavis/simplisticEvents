using System;
using System.Threading.Tasks;
using application.Core;
using application.Products;
using MediatR;

namespace application.Orders.Events
{
	public class ItemPurchased : IDomainEvent
	{
		public int ProductId { get; set; }
		public int Quantity { get; set; }
		public Guid Id { get; set; }
	}

	public class ItemPurchasedHandler : IDomainEventHandler<ItemPurchased>
	{
		readonly IMediator _mediator;

		public ItemPurchasedHandler(IMediator mediator)
		{
			_mediator = mediator;
		}
		public Task Handle(ItemPurchased domainEvent)
		{
			var stock = new Stock()
			{
				ProductId = domainEvent.ProductId,
				QuantityAvailable = domainEvent.Quantity
			};

			stock.Handle(domainEvent);

			return Task.CompletedTask;
		}
	}
}
