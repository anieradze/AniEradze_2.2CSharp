using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web_api.Data;
using Web_api.DTOs;
using Web_api.Models;
using Web_api.Services.Interfaces;

public class BookService : IBookService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public BookService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<BookDto>> GetAllAsync()
    {
        var books = await _context.Books.Include(b => b.Category).ToListAsync();
        return _mapper.Map<IEnumerable<BookDto>>(books);
    }

    public async Task AddAsync(CreateBookDto dto)
    {
        var book = _mapper.Map<Book>(dto);
        _context.Books.Add(book);
    }

    public Task AddAsync(object bookDto)
    {
        throw new NotImplementedException();
    }
}
