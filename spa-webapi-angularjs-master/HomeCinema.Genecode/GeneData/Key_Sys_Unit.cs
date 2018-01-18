using FizzWare.NBuilder;
using HomeCinema.Genecode.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeCinema.Genecode.GeneData
{
    public class GenerateData
    {
        public static List <Key_Sys_Unit> GenerateKey_Sys_Units()
        {
            var key_sys_units = Builder<Key_Sys_Unit>.CreateListOfSize(200)
      .All()
           .With(c => c.Encodedomain = Faker.Internet.DomainWord())
           .With(c => c.Key = new Guid().ToString())
       .Build();
            return key_sys_units.ToList();
        }
    }
}