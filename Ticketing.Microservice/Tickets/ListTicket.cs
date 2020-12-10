using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Ticketing.Microservice.Database;

namespace Ticketing.Microservice.Tickets
{
    public class ListTicket
    {
        public class GetTicketsQuery : IRequest<Response> { }

        public class GetTicketsQueryHandler : IRequestHandler<GetTicketsQuery, Response>
        {
            private readonly DatabaseContext context;
            private readonly Response _response = new Response();

            public GetTicketsQueryHandler(DatabaseContext context)
            {
                this.context = context;
            }

            public async Task<Response> Handle(GetTicketsQuery request, CancellationToken cancellationToken)
            {
                var tickets = await context.Tickets.ToListAsync();

                _response.Error = false;
                _response.Data = tickets;
                _response.Message = "all tickets";

                return _response;
            }
        }

        public class Response
        {
            public bool Error { get; set; }
            public object Data { get; set; }
            public string Message { get; set; }
        }
    }
}
