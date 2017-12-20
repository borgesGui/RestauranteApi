using RestauranteApi.Builders;
using RestauranteApi.Context;
using RestauranteApi.Exceptions;
using RestauranteApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestauranteApi.Bll
{
    public class RestauranteBll : AbstractBll
    {

        public RestauranteBll(RestauranteDbContext context) : base(context)
        {
        }

        private void ValidaLista(List<Restaurante> pratos)
        {
            if (pratos == null)
            {
                throw new ListNoContentException("Nenhum restaurante encontrado!");
            }
        }

        public List<Restaurante> GetAll()
        {
            List<Restaurante> restaurantes = _context.Restaurantes.ToList();
            ValidaLista(restaurantes);

            if (restaurantes != null)
            {
                PratoBll pratoBll = new PratoBll(_context);
                foreach (Restaurante item in restaurantes)
                {
                    GetPratos(pratoBll, item);
                }
            }

            return restaurantes;
        }

        private void GetPratos(PratoBll pratoBll, Restaurante restaurante)
        {
            try
            {
                restaurante.Pratos = pratoBll.GetByRestauranteId(restaurante.Id, false);
            }
            catch (ListNoContentException ex)
            {
                //Log.Debug("");
            }
        }

        public Restaurante GetById(long id, bool populaPratos = true)
        {
            Restaurante restaurante = _context.Restaurantes.FirstOrDefault(t => t.Id == id);
            if (restaurante == null)
            {
                throw new EntityNotFoundException(String.Format("Restaurante de id {0} não encontrado!", id));
            }

            // Evitar self reference
            if (populaPratos)
            {
                PratoBll pratoBll = new PratoBll(_context);
                GetPratos(pratoBll, restaurante);
            }
            else
            {
                restaurante.Pratos = null;
            }

            return restaurante;
        }

        public List<Restaurante> GetByNome(string filtro)
        {
            List<Restaurante> restaurantes = _context.Restaurantes.Where(t => t.Nome.ToUpper().IndexOf(filtro.ToUpper()) >= 0).ToList();
            ValidaLista(restaurantes);

            if (restaurantes != null)
            {
                PratoBll pratoBll = new PratoBll(_context);
                foreach (Restaurante item in restaurantes)
                {
                    GetPratos(pratoBll, item);
                }
            }

            return restaurantes;
        }

        public Restaurante SaveWithoutLists(Restaurante obj)
        {
            Restaurante restaurante = (new RestauranteFactory()).Build(obj);

            if (restaurante != null)
            {
                try
                {

                    if (restaurante.Id == 0)
                    {
                        _context.Restaurantes.Add(restaurante);
                    }
                    else
                    {
                        _context.Restaurantes.Update(restaurante);
                    }
                }
                finally
                {
                    ExecuteSaveChanges();
                }
            }

            return restaurante;
        }

        private List<Prato> SavePratos(List<Prato> pratos, long RestauranteId)
        {
            if (pratos != null)
            {
                PratoBll pratoBll = new PratoBll(_context, false);
                foreach (Prato prato in pratos)
                {
                    prato.RestauranteId = RestauranteId;
                    pratoBll.Save(prato);
                }
            }

            return pratos;
        }

        public Restaurante Save(Restaurante restaurante)
        {
            Restaurante restauranteMerged = null;
            if (restaurante != null)
            {
                try
                {
                    base.SaveChanges = false;
                    restauranteMerged = SaveWithoutLists(restaurante);
                    restaurante.Pratos = SavePratos(restaurante.Pratos, restauranteMerged.Id);
                    base.SaveChanges = true;
                }
                finally
                {
                    ExecuteSaveChanges();
                }
                restaurante.Id = restauranteMerged.Id;
            }

            return restaurante;
        }

        public void Delete(int id)
        {
            Restaurante restaurante = this.GetById(id);

            if (restaurante != null)
            {
                _context.Restaurantes.Remove(restaurante);
                _context.SaveChanges();
            }
        }

        public bool IsRestauranteValid(long restauranteId)
        {
            Restaurante restaurante = _context.Restaurantes.FirstOrDefault(t => t.Id == restauranteId);
            if (restaurante == null)
            {
                throw new BusinessException("Restaurante informado não está cadastrado!");
            }

            return true;
        }
    }
}