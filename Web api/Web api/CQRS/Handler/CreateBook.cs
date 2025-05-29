using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;
using Web_api.CQRS.Command;
using Web_api.Services.Interfaces;
using Web_api.DTOs;

namespace Web_api.CQRS.Handler
{
    public class CreateBook : IRequest<CreateBook>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private CreateBookDto BookDto;

        public CreateBook(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(CreateBook request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BookService.AddAsync(request.BookDto);
            await _unitOfWork.SaveAsync();
            return Unit.Value;
        }
    }
}
