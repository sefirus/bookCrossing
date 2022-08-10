using Core.Interfaces.Services;
using Core.ViewModels.BookViewModels;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/books")]
[ApiController]
public class BookController : ControllerBase
{
    private readonly IBookService _bookService;

    public BookController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    public async Task<IEnumerable<SearchBookViewModel>> SearchBookAsync([FromQuery] string request)
    {
        var searchResults = await _bookService.SearchBookAsync(request);
        return searchResults;
    }

    [HttpPost]
    public async Task PostBookAsync([FromBody] SearchBookViewModel model)
    {
        await _bookService.AddBookToLibraryAsync(model);
    }
}