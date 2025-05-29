using MediatR;
using System.Collections.Generic;
using Web_api.DTOs;

namespace Web_api.CQRS.Query
{
    public class GetAllBooks : IRequest<IEnumerable<BookDto>>
    {
    }
}
