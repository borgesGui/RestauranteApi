using RestauranteApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestauranteApi.Builders
{
    public class PratoFactory : AbstractFactory<Prato>
    {
        public override Prato Build(Prato obj)
        {
            if (obj == null)
            {
                return null;
            }

            return new Prato(obj.Id, obj.Nome, obj.Valor, obj.RestauranteId);
        }
    }
}