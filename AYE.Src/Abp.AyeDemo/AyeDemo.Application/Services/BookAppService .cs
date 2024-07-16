using AyeDemo.Application.Contracts.Dtos.Book;
using AyeDemo.Application.Contracts.IServices;
using AyeDemo.Domain.Entities;
using Microsoft.Extensions.Logging;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace AyeDemo.Application.Services
{
    public class BookAppService :ApplicationService
           //CrudAppService<
           //    BookAggregateRoot, //The Book entity
           //    BookDto, //Used to show books
           //    Guid, //Primary key of the book entity
           //    PagedAndSortedResultRequestDto, //Used for paging/sorting
           //    BookCreateUpdateDto>, //Used to create/update a book
           //IBookAppService //implement the IBookAppService
    {
        //private ISqlSugarRepository<BookAggregateRoot, Guid> _repository;
        //public BookAppService(ISqlSugarRepository<BookAggregateRoot, Guid> repository)
        //{
        //    _repository = repository;
        //}

        //public BookAppService(IRepository<BookAggregateRoot, Guid> repository) : base(repository)
        //public BookAppService(IRepository<BookAggregateRoot, Guid> repository)
        //{

        //}


        private readonly ILogger<BookAppService> _logger;
        private IRepository<BookAggregateRoot, Guid> _repository;
        public BookAppService(IRepository<BookAggregateRoot, Guid> repository, ILogger<BookAppService> logger)
        {
            _repository = repository;
            _logger = logger;
        }



        public async Task<string> GetAyeHello()
        {
            try
            {
                await _repository.InsertAsync(new BookAggregateRoot { Name = "HHY", PublishDate = DateTime.Now, Price = 100 });


            }
            catch (Exception e)
            {
                _logger.LogDebug(e.ToString());
                //throw;
            }
            return "你好，黄hu也";
        }



       
    }
}
