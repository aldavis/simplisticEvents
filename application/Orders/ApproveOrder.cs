using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MediatR.Pipeline;

namespace application.Orders
{
    public class ApproveOrderRequest: IRequest<ApproveOrderResponse>
    {
	    public ApproveOrderRequest(Guid id)
	    {
		    Id = id;
	    }

		public Guid Id { get; }
    }

		public class ApproveOrderResponse
		{
			public ApproveOrderResponse(string message)
			{
				Message = message;
			}

			public string Message { get; }
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
			return Task.FromResult(new ApproveOrderResponse("foo"));
		}
	}


	public class ApproveOrderPostProcessor : IRequestPostProcessor<ApproveOrderRequest, ApproveOrderResponse>
	{
		public Task Process(ApproveOrderRequest request, ApproveOrderResponse response)
		{
			string bar = response.Message;

			return Task.FromResult(response);
		}
	}


}
