using application.Products.Events;

namespace application.Products
{
	public class Item : Aggregate
	{
		public int ProductId { get; set; }

		public void AddToInventory(int quantity)
		{
			//i think the general theme is that domain or aggregates only act upon self contained properites and send out the corresponding events for further actions
			Publish(new InventoryReceived{ProductId = ProductId, Quantity = quantity});
		}
	}
}