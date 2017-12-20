using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestauranteApi.Builders
{
    public abstract class AbstractFactory<T>
    {
        public abstract T Build(T obj);
    }
}