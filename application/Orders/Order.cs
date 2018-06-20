using System;
using System.Collections.Generic;
using System.Linq;
using application.Orders.Events;

namespace application.Orders
{
	public enum OrderStatus
	{
		New = 0,
		Submitted = 1,
		Approved = 2
	}

	public class Order : Aggregate
    {

	    public Order()
	    {
		    Status = OrderStatus.New;
			Items = new List<OrderItem>();
	    }

	    public List<OrderItem> Items { get; set; }

	    public OrderStatus Status { get; set; }

	    public decimal Total
	    {
		    get { return Items.Sum(li => li.Subtotal); }
	    }


	    public void Approve()
	    {
		    Status = OrderStatus.Approved;
		    foreach (var lineItem in Items)
		    {
			    Publish(new ItemPurchased
			    {
				    ProductId = lineItem.ProductId,
				    Quantity = lineItem.Quantity,
				    Id = Guid.NewGuid()
			    });
		    }
	    }
	}

	public class OrderItem
	{
		public int Quantity { get; set; }
		public decimal ListPrice { get; set; }
		public int ProductId { get; set; }
		public string ProductName { get; set; }
		public decimal Subtotal => Quantity * ListPrice;
	}
}
