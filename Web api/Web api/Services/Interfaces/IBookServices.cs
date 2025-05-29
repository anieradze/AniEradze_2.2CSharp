using System.Collections.Generic;
using System.Threading.Tasks;
using Web_api.DTOs;

namespace Web_api.Services.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<BookDto>> GetAllAsync();
        Task AddAsync(CreateBookDto dto);
        Task AddAsync(object bookDto);
    }

}