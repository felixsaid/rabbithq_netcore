using MassTransit;
using MediatR;
using Shared.Models.Models;
using System;
using System.Threading;
using System.Threading.Tasks;
using Ticketing.Microservice.Database;

namespace Ticketing.Microservice.Tickets
{
    public class CreateTicketCommand : IRequest<Response>
    {
        public Ticket Ticket { get; set; }

        public CreateTicketCommand(Ticket ticket)
        {
            Ticket = ticket;
        }
    }

        public class CreateTicketCommandHandler : IRequestHandler<CreateTicketCommand, Response>
        {

            private readonly DatabaseContext context;
            private readonly Response _response = new Response();
            private readonly IBus bus;

        public CreateTicketCommandHandler(IBus bus, DatabaseContext context)
         {
              this.context = context;
              this.bus = bus;
        }

            public async Task<Response> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
            {
                request.Ticket.BookedOn = DateTime.Now;

                context.Add(request.Ticket);
                context.SaveChanges();

                Uri uri = new Uri("rabbitmq://localhost/ticketQueue");
                var endPoint = await bus.GetSendEndpoint(uri);
                await endPoint.Send(request.Ticket);

                _response.Error = false;
                _response.Data = request.Ticket;
                _response.Message = "Ticket successfully created.";

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
