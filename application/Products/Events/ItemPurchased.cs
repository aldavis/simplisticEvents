using System;
using System.Threading.Tasks;
using application.Core;

namespace application.Products.Events
{
	public class InventoryReceived : IDomainEvent
	{
		public int ProductId { get; set; }
		public int Quantity { get; set; }
		public Guid Id { get; }
	}

	public class ItemPurchasedHandler : IDomainEventHandler<InventoryReceived>
	{
		public Task Handle(InventoryReceived domainEvent)
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
