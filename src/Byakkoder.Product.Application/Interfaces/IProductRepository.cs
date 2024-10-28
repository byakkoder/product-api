using Byakkoder.Product.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Byakkoder.Product.Application.Interfaces
{
    public interface IProductRepository
    {
        Task<Domain.Entities.Product> GetById(long id);

        Task<Domain.Entities.Product> GetByName(string name);

        Task Create(Domain.Entities.Product product);

        Task Update(Domain.Entities.Product product);
    }
}
