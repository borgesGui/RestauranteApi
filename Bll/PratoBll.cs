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
    public class PratoBll : AbstractBll
    {

        public PratoBll(RestauranteDbContext context) : base(context)
        {
        }

        public PratoBll(RestauranteDbContext context, bool saveChanges) : base(context, saveChanges)
        {
        }

        private void ValidaLista(List<Prato> pratos)
        {
            if (pratos == null)
            {
                throw new ListNoContentException("Nenhum prato encontrado!");
            }
        }

        private void PopulaRestaurante(List<Prato> pratos)
        {
            if (pratos == null)
            {
                return;
            }

            RestauranteBll restauranteBll = new RestauranteBll(_context);
            foreach (Prato prato in pratos)
            {
                prato.Restaurante = restauranteBll.GetById(prato.RestauranteId, false);
            }
        }

        public List<Prato> GetAll()
        {
            List<Prato> pratos = _context.Pratos.ToList();
            ValidaLista(pratos);
            PopulaRestaurante(pratos);

            return pratos;
        }

        public Prato GetById(long id, bool mapRestaurante = true)
        {
            Prato prato = _context.Pratos.FirstOrDefault(t => t.Id == id);
            if (prato == null)
            {
                throw new EntityNotFoundException(String.Format("Prato de id {0} não encontrado!", id));
            }

            // Evitar self reference
            if (!mapRestaurante)
            {
                prato.Restaurante = null;
            }

            return prato;
        }

        public List<Prato> GetByRestauranteId(long id, bool mapRestaurante = true)
        {
            List<Prato> pratos = _context.Pratos.Where(t => t.RestauranteId == id).ToList();
            if (pratos == null)
            {
                throw new ListNoContentException(String.Format("Nenhum prato encontrato para o restaurante de id {0}!", id));
            }

            // Evitar self reference
            if (!mapRestaurante)
            {
                foreach (Prato prato in pratos)
                {
                    prato.Restaurante = null;
                }
            }

            return pratos;
        }

        public Prato Save(Prato obj)
        {
            if (obj == null)
            {
                return obj;
            }

            Prato prato = (new PratoFactory()).Build(obj);

            try
            {
                if (prato.Id == 0)
                {
                    _context.Pratos.Add(prato);
                }
                else
                {
                    _context.Pratos.Update(prato);
                }

            }
            finally
            {
                ExecuteSaveChanges();
            }

            return prato;
        }

        public Prato SaveWithRestauranteValidation(Prato obj)
        {
            if (obj == null)
            {
                return obj;
            }

            RestauranteBll bll = new RestauranteBll(_context);
            bll.IsRestauranteValid(obj.RestauranteId);

            return Save(obj);
        }

        public void Delete(int id)
        {
            Prato prato = this.GetById(id);

            if (prato != null)
            {
                _context.Pratos.Remove(prato);
                _context.SaveChanges();
            }
        }
    }
}