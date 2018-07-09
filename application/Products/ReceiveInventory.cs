using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using application.Core;
using application.Orders;
using MediatR;
using MediatR.Pipeline;

namespace application.Products
{


	/*
	 * when inventory is received we must do the following
	 * check for any orders that are waiting on this item, if found apply to oldest order first(addl steps for completing order to come later)
	 * after filling back orders, if more orders still exist, we must determine how much inventory is needed to fill remaining backorders and send request for more inventory to be purchased
	 * check stock amount against low threshold for product if below threshold check for pending request for additional inventory, if no addl requests send new one for however
	 * much is needed to satisfy min threshold plus 15%
	 */

    public class ReceiveInventoryRequest: IRequest<ReceiveInventoryResponse>
    {
	    public ReceiveInventoryRequest(Guid productId,int quantity)
	    {
		    ProductId = productId;
		    Quantity = quantity;
	    }

		[Required]
		public Guid ProductId { get; }

		[Required]
		public int Quantity { get; }

    }

		public class ReceiveInventoryResponse
		{
			public ReceiveInventoryResponse(IList<Stock> stock)
			{
				Stock = stock;
			}

			public IList<Stock> Stock { get; }
		}


	public class ReceiveInventoryPreProcessor : IRequestPreProcessor<ReceiveInventoryRequest>
	{
		public Task Process(ReceiveInventoryRequest request, CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}

	public class ReceiveInventoryHandler : IRequestHandler<ReceiveInventoryRequest,ReceiveInventoryResponse>
	{
		public Task<ReceiveInventoryResponse> Handle(ReceiveInventoryRequest request, CancellationToken cancellationToken)
		{
			var order = new Order();
			order.Items.Add(new OrderItem{ListPrice = 5,ProductId = 3,ProductName = "crackers",Quantity = 45});

			order.Approve();

			return Task.FromResult(new ReceiveInventoryResponse(null));
		}
	}


	public class ReceiveInventoryPostProcessor : IRequestPostProcessor<ReceiveInventoryRequest, ReceiveInventoryResponse>
	{
		readonly IDomainEventDispatcher _eventDispatcher;

		public ReceiveInventoryPostProcessor(IDomainEventDispatcher eventDispatcher)
		{
			_eventDispatcher = eventDispatcher;
		}

		public Task Process(ReceiveInventoryRequest request, ReceiveInventoryResponse response)
		{
			//_eventDispatcher.Dispatch(response.Order);
			
			return Task.FromResult(response);
		}
	}


}
