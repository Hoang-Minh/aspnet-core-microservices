using AutoMapper;
using BasketApi.Controllers.Entities;
using EventBusRabbitMQ.Events;

namespace BasketApi.Mapping
{
    public class BasketMapping : Profile
    {
        public BasketMapping()
        {
            CreateMap<BasketCheckout, BasketCheckoutEvent>().ReverseMap();
        }
    }
}
