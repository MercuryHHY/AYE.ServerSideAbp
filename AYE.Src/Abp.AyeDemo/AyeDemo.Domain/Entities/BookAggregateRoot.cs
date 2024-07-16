using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace AyeDemo.Domain.Entities;


//public class BookAggregateRoot : AuditedAggregateRoot<Guid>
public class BookAggregateRoot :IEntity<Guid>
{
    public BookAggregateRoot() { }

    public BookAggregateRoot(Guid id) 
    {
    }

    public BookAggregateRoot(Guid id, string name, DateTime publishDate, short price) : this(id)
    {
        Name = name;
        PublishDate = publishDate;
        Price = price;
    }

    public Guid Id { get;  set; }= Guid.NewGuid();    
    public string Name { get; set; }
    public DateTime PublishDate { get; set; }
    public short Price { get; set; }

    public object?[] GetKeys()
    {
        throw new NotImplementedException();
    }
}
