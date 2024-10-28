using AutoMapper;
using Byakkoder.Product.Application.Products.Commands.Insert;
using Byakkoder.Product.Application.Products.Commands.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Byakkoder.Product.Application.Mappings
{
    public class ApplicationMappingProfile : Profile
    {
        public ApplicationMappingProfile() 
        {
            CreateMap<InsertCommand, Domain.Entities.Product>();
            CreateMap<UpdateCommand, Domain.Entities.Product>();
        }
    }
}
