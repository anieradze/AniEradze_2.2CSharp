using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Web_api.CQRS.Query;
using Web_api.DTOs;
using Web_api.Services.Interfaces;

namespace Web_api.CQRS.Handler
{
    public class GetAllBooks : IRequestHandler<GetAllBooks, IEnumerable<BookDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllBooks()
        {
        }

        public GetAllBooks(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<BookDto>> Handle(GetAllBooks request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.BookService.GetAllAsync();
        }
    }
}
