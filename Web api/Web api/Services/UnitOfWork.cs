using System.Threading.Tasks;
using Web_api.Data;
using Web_api.Services.Interfaces;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    public IBookService BookService { get; }

    public UnitOfWork(ApplicationDbContext context, IBookService bookService)
    {
        _context = context;
        BookService = bookService;
    }

    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
