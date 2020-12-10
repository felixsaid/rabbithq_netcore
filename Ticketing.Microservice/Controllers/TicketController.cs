using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.Models;
using System.Threading.Tasks;
using Ticketing.Microservice.Tickets;

namespace Ticketing.Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
       
        private readonly IMediator mediator;

        public TicketController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTicket(Ticket ticket)
        {
            if (ticket != null)
            {
                var result = await mediator.Send(new CreateTicketCommand(ticket));

                return Ok(result);
            }

            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTickets()
        {
            var result = await mediator.Send(new ListTicket.GetTicketsQuery());

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTicketById(int id)
        {
            var result = await mediator.Send(new ListTicketById.GetTicketByIdCommand(id));

            return Ok(result);
        }
    }
}
