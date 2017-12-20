using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RestauranteApi.Models
{
    public class Restaurante
    {
        [Key]
        private long _id;

        private string _nome;

        private List<Prato> _pratos;

        public Restaurante()
        {
        }

        public Restaurante(long id, string nome)
        {
            this.Id = id;
            this.Nome = nome;
        }


        public long Id
        {
            get
            {
                return this._id;
            }
            set
            {
                this._id = value;
            }
        }

        public string Nome
        {
            get
            {
                return this._nome;
            }
            set
            {
                this._nome = value;
            }
        }

        public List<Prato> Pratos
        {
            get
            {
                return this._pratos;
            }
            set
            {
                this._pratos = value;
            }
        }
    }
}