using RestauranteApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestauranteApi.Builders
{
    public class RestauranteFactory : AbstractFactory<Restaurante>
    {
        public override Restaurante Build(Restaurante obj)
        {
            if (obj == null)
            {
                return null;
            }

            return new Restaurante(obj.Id, obj.Nome);
        }
    }
}