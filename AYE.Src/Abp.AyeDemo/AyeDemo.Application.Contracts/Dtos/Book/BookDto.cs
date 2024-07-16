using Volo.Abp.Application.Dtos;

namespace AyeDemo.Application.Contracts.Dtos.Book
{
    public class BookDto : AuditedEntityDto<Guid>
    {
        public string Name { get; set; }

        public DateTime PublishDate { get; set; }

        public short Price { get; set; }
    }
}
