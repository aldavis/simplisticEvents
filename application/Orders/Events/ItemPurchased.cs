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
		public Task Handle(ItemPurchased domainEvent)
		{
			//get the stock object for the product id(if we were using event sourcing we would not have a stock object, rather we would replay events on inventory to determine the stock)

			//if null then create a new one

			var stock = new Stock()
			{
				ProductId = domainEvent.ProductId,
				QuantityAvailable = domainEvent.Quantity
			};

			stock.Handle(domainEvent);

			//update the datastore with the adjusted stock(again if using ES we would add a stock depleted/decremented event)

			return Task.CompletedTask;
		}
	}
}
