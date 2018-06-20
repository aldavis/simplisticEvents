using System;
using System.Threading.Tasks;
using application.Core;

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
		//test DI with these handlers
		public Task Handle(ItemPurchased domainEvent)
		{
			return Task.CompletedTask;
		}
	}
}
