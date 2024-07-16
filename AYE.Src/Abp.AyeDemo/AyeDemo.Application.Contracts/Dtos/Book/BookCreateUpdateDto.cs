using System.ComponentModel.DataAnnotations;

namespace AyeDemo.Application.Contracts.Dtos.Book
{

    public class BookCreateUpdateDto
        {
            [Required]
            [StringLength(128)]
            public string Name { get; set; }

            [Required]
            [DataType(DataType.Date)]
            public DateTime PublishDate { get; set; } = DateTime.Now;

            [Required]
            public short Price { get; set; }
        }
  
}
