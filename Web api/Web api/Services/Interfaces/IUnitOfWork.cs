using System.Collections.Generic;
using System.Threading.Tasks;
using Web_api.DTOs;

namespace Web_api.Services.Interfaces
{
    public interface IUnitOfWork
    {
        IBookService BookService { get; }
        Task<int> SaveAsync();
    }
}
