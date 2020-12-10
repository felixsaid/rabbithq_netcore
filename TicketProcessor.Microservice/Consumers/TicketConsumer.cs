using MassTransit;
using Shared.Models.Models;
using System.Threading.Tasks;
using TicketProcessor.Microservice.Database;

namespace TicketProcessor.Microservice.Consumers
{
    public class TicketConsumer : IConsumer<Ticket>
    {
        private readonly DatabaseContext _context;

        public TicketConsumer(DatabaseContext context)
        {
            this._context = context;
        }

        public Task Consume(ConsumeContext<Ticket> context)
        {
            _context.Add(context.Message);
            _context.SaveChanges();

            return Task.FromResult(context.Message);
        }
    }
}
