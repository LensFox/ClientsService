using AutoMapper;
using ClientsService.DDL;

namespace ClientsService.BLL
{
    public class EntityToModelProfile : Profile
    {
        public EntityToModelProfile()
        {
            this.CreateMap<ClientEntity, ClientModel>()
                .ReverseMap();
        }
    }
}
