using Web_api.DTOs;

namespace Web_api.Mapping
{
    internal interface IMapper
    {
        T Map<T>(CreateBookDto bookDto);
    }
}