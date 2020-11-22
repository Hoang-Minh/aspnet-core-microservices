using MediatR;
using Ordering.Application.Responses;
using System;
using System.Collections.Generic;

namespace Ordering.Application.Queries
{
    public class GetOrderByUsernameQuery : IRequest<IEnumerable<OrderResponse>>
    {
        public string Username { get; set; }

        public GetOrderByUsernameQuery(string username)
        {
            Username = username ?? throw new ArgumentNullException(nameof(username));
        }
    }
}
