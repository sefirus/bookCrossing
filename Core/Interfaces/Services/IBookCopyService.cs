using Core.ViewModels.BookViewModels;

namespace Core.Interfaces.Services;

public interface IBookCopyService
{
    Task AddBookCopy(SearchBookViewModel model);
}