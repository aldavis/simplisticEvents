using application.Orders.Events;

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
	}
}
