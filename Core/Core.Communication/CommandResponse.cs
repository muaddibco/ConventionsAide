using System;

namespace ConventionsAide.Core.Communication
{
    public class CommandResponse<TRequest, TResponse> : CommandMessage<TResponse> where TRequest : class where TResponse : class
    {
        public CommandResponse(Guid correlationId, TRequest request, TResponse response) 
            : base(correlationId, response)
        {
            Request = request;
        }

        public TRequest Request { get; set; }
    }
}
