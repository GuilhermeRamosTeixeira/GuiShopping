

using AutoMapper;
using GuiShopping.CartAPI.Data.ValueObject;
using GuiShopping.CartAPI.Model;
using System.Reflection.PortableExecutable;

namespace GuiShopping.CartAPI.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {

                config.CreateMap<ProductVO, Product>().ReverseMap();
                config.CreateMap<CartVO, Cart>().ReverseMap();
                config.CreateMap<CartDetailVO, CartDetail>().ReverseMap();
                config.CreateMap<CartHeaderVO, CartHeader>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
