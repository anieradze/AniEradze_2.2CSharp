using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web_api.CQRS.Command;
using Web_api.CQRS.Handler;
using Web_api.CQRS.Query;
using Web_api.DTOs;

namespace Web_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BooksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            var query = new CQRS.Handler.GetAllBooks();
            var books = await _mediator.Send(query);
            return Ok(books);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] CreateBookDto dto)
        {
            var command = new CQRS.Command.CreateBook { BookDto = dto };
            await _mediator.Send(command);
            return CreatedAtAction(nameof(GetBooks), null);
        }
    }
}
