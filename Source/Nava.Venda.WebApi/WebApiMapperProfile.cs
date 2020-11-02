using AutoMapper;
using Nava.Venda.Domain;
using Nava.Venda.WebApi.Dtos;

namespace Nava.Venda.WebApi
{
    public class WebApiMapperProfile : Profile
    {
        public WebApiMapperProfile()
        {
            CreateMap<Domain.Venda, VendaGetResult>();
            CreateMap<Funcionario, FuncionarioDto>();
            CreateMap<Cargo, CargoDto>();
            CreateMap<Item, ItemDto>();
            CreateMap<StatusVenda, StatusVendaDto>();
            CreateMap<VendaPost, Domain.Venda>();
            CreateMap<FuncionarioDto, Funcionario>();
            CreateMap<ItemDto, Item>();
        }
    }
}
