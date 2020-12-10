using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ticketing.Microservice.Database;

namespace Ticketing.Microservice.Tickets
{
    public class ListTicketById
    {
        public class GetTicketByIdCommand : IRequest<Response>
        {
            public int TicketId { get; set; }

            public GetTicketByIdCommand(int ticketId)
            {
                TicketId = ticketId;
            }
        }

        public class GetTicketByIdHandler : IRequestHandler<GetTicketByIdCommand, Response>
        {

            private readonly DatabaseContext context;
            private readonly Response _response = new Response();

            public GetTicketByIdHandler(DatabaseContext context)
            {
                this.context = context;
            }

            public async Task<Response> Handle(GetTicketByIdCommand request, CancellationToken cancellationToken)
            {
                var ticket = await (from t in context.Tickets
                              select new Ticket()
                              {
                                  TicketId = t.TicketId,
                                  UserName = t.UserName,
                                  BookedOn = t.BookedOn,
                                  Boarding = t.Boarding,
                                  Destination = t.Destination
                              }).FirstOrDefaultAsync(a => a.TicketId == request.TicketId);

                _response.Error = false;
                _response.Data = ticket;
                _response.Message = "returned ticket with id " + request.TicketId;

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
