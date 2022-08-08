using Core.Entities;
using Core.Enums;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.ViewModels.BookViewModels;
using Core.ViewModels.BookViewModels.GoogleBookApiRequests;
using F23.StringSimilarity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Application.Services;

public class BookService : IBookService
{
    private readonly IConfiguration _configuration;
    private readonly IHttpClientFactory _clientFactory;
    private readonly IRepository<Book> _bookRepository;

    public BookService(
        IConfiguration configuration, 
        IHttpClientFactory clientFactory,
        IRepository<Book> bookRepository)
    {
        _configuration = configuration;
        _clientFactory = clientFactory;
        _bookRepository = bookRepository;
    }
    
    private async Task<IEnumerable<SearchBookViewModel>> SearchInApiAsync(string uri)
    {
        var httpClient = _clientFactory.CreateClient();
        var requestResult = await httpClient.GetStringAsync(uri);
        var tempResponse = JsonConvert.DeserializeObject<BookApiSearchResponse>(requestResult);
        
        if (tempResponse.TotalItems == 0)
        {
            return Enumerable.Empty<SearchBookViewModel>();
        }
        
        var searchResults = new List<SearchBookViewModel>();
        foreach (var volume in tempResponse.Items)
        {
            var volumeAuthors = string.Empty;
            var authorsList = volume.VolumeInfo.Authors.ToList();
            if(authorsList.Count > 0)
            {
                for (int i = 0; i < authorsList.Count - 1; i++)
                {
                    volumeAuthors = $"{volumeAuthors}{authorsList[i]}, ";
                }
                volumeAuthors = $"{volumeAuthors}{authorsList.Last()}";
            }
            searchResults.Add(new SearchBookViewModel()
            {
                Id = volume.Id,
                ThumbnailLink = volume.VolumeInfo.ImageLinks?.Thumbnail,
                Title = volume.VolumeInfo.Title,
                Authors = volumeAuthors,
                SearchResultType = SearchResultType.GoogleBookApi
            });
        }

        return searchResults;
    }

    private IEnumerable<SearchBookViewModel> MapBooks(List<Book> books)
    {
        if (books.Count == 0)
        {
            return Enumerable.Empty<SearchBookViewModel>();
        }
        
        var searchResults = new List<SearchBookViewModel>();
        foreach (var book in books)
        {
            var bookAuthors = string.Empty;
            var authorsList = book.BookWriters.Select(bw => bw.Writer).ToList();
            if (authorsList.Count > 0)
            {
                bookAuthors = $"{bookAuthors}{authorsList.Last()}";
                for (int i = 0; i < authorsList.Count - 1; i++)
                {
                    bookAuthors = $"{bookAuthors}{authorsList[i]}, ";
                }
            }
            searchResults.Add(new SearchBookViewModel()
            {
                Id = book.Id.ToString(),
                Authors = bookAuthors,
                SearchResultType = SearchResultType.Database,
                ThumbnailLink = book.Pictures.First().FullPath
            });
        }

        return searchResults;
    }

    private async Task<IEnumerable<SearchBookViewModel>> SearchInDbByTitleAsync(string request)
    {
        var jw = new JaroWinkler();
        var query = (await _bookRepository.QueryAsync(
                    include: prop =>
                        prop.Include(b => b.BookWriters)
                            .ThenInclude(ba => ba.Writer))).AsEnumerable();
        
        var books = query.Where(b =>
            jw.Similarity(request, b.Title) > 0.55 || jw.Similarity(request, b.Description) > 0.55).ToList();

        var mappedBooks = MapBooks(books);
        return mappedBooks;
    }

    private async Task<IEnumerable<SearchBookViewModel>> SearchInDbByAuthorAsync(string request)
    {
        var jw = new JaroWinkler();
        var query = (await _bookRepository.QueryAsync(
            include: prop =>
                prop.Include(b => b.BookWriters)
                    .ThenInclude(ba => ba.Writer))).AsEnumerable();
        
        var books = query.Where(b =>
            b.BookWriters.Select(bw => bw.Writer).Any(w => jw.Similarity(request, w.FullName) > 0.55))
            .ToList();

        var mappedBooks = MapBooks(books);
        return mappedBooks;
    }

    public async Task<IEnumerable<SearchBookViewModel>> GetBookSearchResultsAsync(string request)
    {
        var bookRequestUri = $"{_configuration["ApiAddresses:GoogleBooksUrl"]}/?q=intitle:{request}";
        var authorRequestUri = $"{_configuration["ApiAddresses:GoogleBooksUrl"]}/?q=inauthor:{request}";
        var requestResults = await Task.WhenAll(
            SearchInDbByAuthorAsync(request),
            SearchInApiAsync(bookRequestUri),
            SearchInApiAsync(authorRequestUri));
        var result = await SearchInDbByTitleAsync(request);
        foreach (var res in requestResults)
        {
            result = result.Concat(res);
        }
        return result;
    }
}