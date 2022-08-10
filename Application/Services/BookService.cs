using Core.Entities;
using Core.Enums;
using Core.Exceptions;
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
    private readonly IRepository<Category> _categoryRepository;
    private readonly IRepository<Publisher> _publisherRepository;
    private readonly IRepository<Writer> _writerRepository;
    private readonly ILoggerManager _logger;

    public BookService(
        IConfiguration configuration, 
        IHttpClientFactory clientFactory,
        IRepository<Book> bookRepository, 
        IRepository<Category> categoryRepository,
        ILoggerManager logger, 
        IRepository<Publisher> publisherRepository, 
        IRepository<Writer> writerRepository)
    {
        _configuration = configuration;
        _clientFactory = clientFactory;
        _bookRepository = bookRepository;
        _categoryRepository = categoryRepository;
        _logger = logger;
        _publisherRepository = publisherRepository;
        _writerRepository = writerRepository;
    }
    
    private async Task<IEnumerable<SearchBookViewModel>> SearchInApiAsync(string uri)
    {
        var httpClient = _clientFactory.CreateClient();
        var response = await httpClient.GetStringAsync(uri);
        var tempResponse = JsonConvert.DeserializeObject<BookApiSearchResponse>(response);
        
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

    public async Task<IEnumerable<SearchBookViewModel>> SearchBookAsync(string request)
    {
        var bookRequestUri = $"{_configuration["ApiAddresses:GoogleBooksUrl"]}?q=intitle:{request}";
        var authorRequestUri = $"{_configuration["ApiAddresses:GoogleBooksUrl"]}?q=inauthor:{request}";
        var requestResults = await Task.WhenAll(
            SearchInDbByAuthorAsync(request),
            SearchInApiAsync(bookRequestUri),
            SearchInApiAsync(authorRequestUri));
        var result = await SearchInDbByTitleAsync(request);
        _logger.LogInfo($"Found books from api and database");
        foreach (var res in requestResults)
        {
            result = result.Concat(res);
        }
        return result;
    }

    private List<string> FindMissingProperties(VolumeViewModel volumeViewModel)
    {
        var missingPropertiesTemplate = new List<string>();
        // if (!volumeViewModel.VolumeInfo.Authors.Any())
        // {
        //     missingPropertiesTemplate.Add(nameof(volumeViewModel.VolumeInfo.Authors));
        // }
        // if (!volumeViewModel.VolumeInfo.Categories.Any())
        // {
        //     missingPropertiesTemplate.Add(nameof(volumeViewModel.VolumeInfo.Categories));
        // }        
        // if (string.IsNullOrEmpty(volumeViewModel.VolumeInfo.Description))
        // {
        //     missingPropertiesTemplate.Add(nameof(volumeViewModel.VolumeInfo.Description));
        // }
        // if (string.IsNullOrEmpty(volumeViewModel.VolumeInfo.Publisher))
        // {
        //     missingPropertiesTemplate.Add(nameof(volumeViewModel.VolumeInfo.Publisher));
        // }
        // if (string.IsNullOrEmpty(volumeViewModel.VolumeInfo.Language))
        // {
        //     missingPropertiesTemplate.Add(nameof(volumeViewModel.VolumeInfo.Language));
        // }
        // if (string.IsNullOrEmpty(volumeViewModel.VolumeInfo.Title))
        // {
        //     missingPropertiesTemplate.Add(nameof(volumeViewModel.VolumeInfo.Title));
        // }
        // if (string.IsNullOrEmpty(volumeViewModel.VolumeInfo.ImageLinks.GetType().))
        // {
        //     missingPropertiesTemplate.Add(nameof(volumeViewModel.VolumeInfo.Publisher));
        // }
        
        foreach (var prop in volumeViewModel.VolumeInfo.GetType().GetProperties())
        {
            switch (prop.GetValue(volumeViewModel.VolumeInfo))
            {
                case string property:
                    if (string.IsNullOrEmpty(property))
                    {
                        missingPropertiesTemplate.Add(prop.Name);
                    }
                    break;
                case IEnumerable<string> property:
                    if (!property.Any())
                    {
                        missingPropertiesTemplate.Add(prop.Name);
                    }
                    break;
                case ImageLinksViewModel imageLinksProperty:
                    var notNullImages = new Dictionary<string, string>();
                    foreach (var imageLinkProp in imageLinksProperty.GetType().GetProperties())
                    {
                        var value = imageLinkProp.GetValue(imageLinksProperty) as string;
                        if (!string.IsNullOrEmpty(value))
                        {
                            notNullImages.Add(imageLinkProp.Name, value);
                        }
                    }
                    if (!notNullImages.Any())
                    {
                        missingPropertiesTemplate.Add(prop.Name);
                    }
                    //TODO: somehow use these notNullImages
                    break;
                default:
                    Console.WriteLine();
                    break;
            }
        }

        return missingPropertiesTemplate;
    }

    private async Task<IEnumerable<BookCategory>> MapCategories(
        IEnumerable<string> categoryNames, 
        Book creatingBook)
    {
        var result = new List<BookCategory>();
        var categories = await _categoryRepository.QueryAsync();
        foreach (var categoryName in categoryNames)
        {
            var words = categoryName.Normalize().Split('/', StringSplitOptions.RemoveEmptyEntries).Reverse();
            foreach (var word in words)
            {
                foreach (var category in categories)
                {
                    if (category.Name.Normalize().Contains(word))
                    {
                        result.Add(new BookCategory()
                        {
                            Book = creatingBook,
                            CategoryId = category.Id,
                            Category = category
                        });
                    }
                }
            }
        }
        return result;
    }

    private IEnumerable<Picture> MapPictures(
        ImageLinksViewModel imageLinksViewModel,
        Book creatingBook)
    {
        var result = new List<Picture>(); 
        foreach (var imageLinkProp in imageLinksViewModel.GetType().GetProperties())
        {
            var value = imageLinkProp.GetValue(imageLinksViewModel) as string;
            if (!string.IsNullOrEmpty(value))
            {
                // await _pictureRepository.InsertAsync(new Picture()
                // {
                //     Book = creatingBook,
                //     FullPath = value
                // });
                result.Add(new Picture()
                {
                    Book = creatingBook,
                    FullPath = value
                });
            }
        }

        return result;
    }

    private async Task<Publisher> MapPublisher(
        string name,
        Book creatingBook)
    {
        var possiblePublisher = (await _publisherRepository
            .QueryAsync())
            .FirstOrDefault(pub => pub.Name.Normalize() == name.Normalize());
        if (possiblePublisher is null)
        {
            var newPublisher = new Publisher()
            {
                Name = name,
                //Books = new List<Book>(){creatingBook}
            };
            await _publisherRepository.InsertAsync(newPublisher);
            await _publisherRepository.SaveChangesAsync();
            return newPublisher;
        }

        return possiblePublisher;
    }

    private async Task<ICollection<BookWriter>> MapBookWriters(
        IEnumerable<string> bookAuthors,
        Book creatingBook)
    {
        var writers = await _writerRepository.QueryAsync();
        var jw = new JaroWinkler();
        ICollection<BookWriter> resultCollection = new List<BookWriter>();
        foreach (var authorName in bookAuthors)
        {
            bool hasFound = false;
            foreach (var writer in writers)
            {
                if (jw.Similarity(writer.FullName, authorName) > 0.65)
                {
                    hasFound = true;
                    resultCollection.Add(new BookWriter()
                    {
                        Book = creatingBook,
                        WriterId = writer.Id,
                        Writer = writer
                    });
                }
            }

            if (hasFound) continue;
            
            var newWriter = new Writer()
            {
                FullName = authorName
            };
            resultCollection.Add(new BookWriter()
            {
                Book = creatingBook,
                Writer = newWriter
            });
            newWriter.BookWriters = resultCollection;
        }

        return resultCollection;
    }

    private string MapIsbn(IEnumerable<IndustryIdentifier> identifiers)
    {
        var isbn = identifiers.First().Identifier;
        return isbn;
    }

    private async Task<Book> MapVolumeToBook(VolumeViewModel volumeViewModel)
    {
        var missingProperties = FindMissingProperties(volumeViewModel);
        if (missingProperties.Any())
        {
            throw new VolumeIncompleteException(missingProperties, volumeViewModel);
        }
        Book newBook = new Book();
        newBook.Title = volumeViewModel.VolumeInfo.Title;
        newBook.Description = volumeViewModel.VolumeInfo.Description;
        newBook.Language = volumeViewModel.VolumeInfo.Language;
        newBook.Pictures = MapPictures(volumeViewModel.VolumeInfo.ImageLinks!, newBook);
        newBook.Publisher = await MapPublisher(volumeViewModel.VolumeInfo.Publisher, newBook);
        newBook.BookWriters = await MapBookWriters(volumeViewModel.VolumeInfo.Authors, newBook);
        newBook.BookCategories = await MapCategories(volumeViewModel.VolumeInfo.Categories, newBook);
        newBook.Isbn = MapIsbn(volumeViewModel.VolumeInfo.IndustryIdentifiers);
        newBook.PageCount = volumeViewModel.VolumeInfo.PageCount;
        
        return newBook;
    }
    
    public async Task AddBookToLibraryAsync(SearchBookViewModel viewModel)
    {
        if (viewModel.SearchResultType == SearchResultType.Database)
        {
            return;
        }

        var bookUrl = $"{_configuration["ApiAddresses:GoogleBooksUrl"]}/{viewModel.Id}";
        var client = _clientFactory.CreateClient();
        var response = await client.GetStringAsync(bookUrl);
        var volume = JsonConvert.DeserializeObject<VolumeViewModel>(response);
        var newBook = await MapVolumeToBook(volume);
        await _bookRepository.InsertAsync(newBook);
        await _bookRepository.SaveChangesAsync();
    }
}