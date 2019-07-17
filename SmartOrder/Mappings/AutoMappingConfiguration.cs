using AutoMapper;
using Model.Models;
using SmartOrder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartOrder.Mappings
{
    public class AutoMappingConfiguration
    {
        public static void Configuration()
        {
          //  Mapper.Initialize(cfg => cfg.CreateMap<Dish, DishViewModel>());
        }
    }
}