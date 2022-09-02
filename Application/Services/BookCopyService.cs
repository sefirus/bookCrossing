using Core.Entities;
using Core.Enums;
using Core.Interfaces.Services;
using Core.ViewModels.BookViewModels;

namespace Application.Services;

public class BookCopyService : IBookCopyService
{
    private readonly BookService _bookService;

    public BookCopyService(BookService bookService)
    {
        _bookService = bookService;
    }
    
    public async Task AddBookCopy(SearchBookViewModel model)
    {
        Book book; 
        if (model.SearchResultType == SearchResultType.GoogleBookApi)
        {
            book = await _bookService.AddBookToLibraryAsync(model);
        }
        else
        {
            book = await _bookService.GetBookByViewModel(model);
        }
    }
}