using System;
using System.Threading;
using System.Threading.Tasks;
using application.Core;
using MediatR;
using MediatR.Pipeline;
using System.ComponentModel.DataAnnotations;

namespace application.Orders
{
    public class ApproveOrderRequest: IRequest<ApproveOrderResponse>
    {
	    public ApproveOrderRequest(Guid id)
	    {
		    Id = id;
	    }

		[Required]
		public Guid Id { get; }

    }

		public class ApproveOrderResponse
		{
			public ApproveOrderResponse(Order order)
			{
				Order = order;
			}

			public Order Order { get; }
		}


	public class ApproveOrderPreProcessor : IRequestPreProcessor<ApproveOrderRequest>
	{
		public Task Process(ApproveOrderRequest request, CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}

	public class ApproveOrderHandler : IRequestHandler<ApproveOrderRequest,ApproveOrderResponse>
	{
		public Task<ApproveOrderResponse> Handle(ApproveOrderRequest request, CancellationToken cancellationToken)
		{
			var order = new Order();
			order.Items.Add(new OrderItem{ListPrice = 5,ProductId = 3,ProductName = "crackers",Quantity = 45});

			order.Approve();

			return Task.FromResult(new ApproveOrderResponse(order));
		}
	}


	public class ApproveOrderPostProcessor : IRequestPostProcessor<ApproveOrderRequest, ApproveOrderResponse>
	{
		readonly IDomainEventProcessor _eventProcessor;

		public ApproveOrderPostProcessor(IDomainEventProcessor eventProcessor)
		{
			_eventProcessor = eventProcessor;
		}

		public Task Process(ApproveOrderRequest request, ApproveOrderResponse response)
		{
			_eventProcessor.ProcessEvents(response.Order);
			
			return Task.FromResult(response);
		}
	}


}
