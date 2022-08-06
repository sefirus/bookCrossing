using Core.Interfaces.Services;
using Core.ViewModels.BookViewModels;
using Microsoft.Extensions.Configuration;

namespace Application.Services;

public class BookService : IBookService
{
    private readonly IConfiguration _configuration;

    public BookService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public Task<IEnumerable<SearchBookViewModel>> GetBookSearchResultsAsync(string request)
    {
        throw new NotImplementedException();
    }
}