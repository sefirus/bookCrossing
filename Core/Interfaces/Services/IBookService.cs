using Core.Entities;
using Core.ViewModels.BookViewModels;

namespace Core.Interfaces.Services;

public interface IBookService
{
    Task<IEnumerable<SearchBookViewModel>> SearchBookAsync(string request);
    Task<Book> AddBookToLibraryAsync(SearchBookViewModel viewModel);
    Task<Book> GetBookByViewModel(SearchBookViewModel viewModel);
}