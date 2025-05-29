using MediatR;
using Web_api.DTOs;

namespace Web_api.CQRS.Command
{
    public class CreateBook : IRequest
    {
        public CreateBookDto BookDto { get; set; } = null!;
    }
}
