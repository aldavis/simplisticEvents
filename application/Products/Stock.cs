using application.Orders.Events;
using application.Products.Events;

namespace application.Products
{
    public class Stock:Aggregate
    {
	    public int QuantityAvailable { get; set; }

	    public int ProductId { get; set; }

	    public void Handle(ItemPurchased domainEvent)
	    {
		    Process(domainEvent, e =>
		    {
			    QuantityAvailable -= e.Quantity;
		    });
	    }

	    public void Handle(InventoryReceived domainEvent)
	    {
		    Process(domainEvent, e =>
		    {
			    QuantityAvailable += e.Quantity;
		    });
	    }
	}
}
