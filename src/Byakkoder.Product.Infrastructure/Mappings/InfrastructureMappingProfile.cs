using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Byakkoder.Product.Infrastructure.Mappings
{
    public class InfrastructureMappingProfile : Profile
    {
        public InfrastructureMappingProfile() 
        { 
            CreateMap<Domain.Entities.Product, Data.Entities.Product>().ReverseMap();
        }
    }
}
