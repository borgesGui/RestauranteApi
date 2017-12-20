using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestauranteApi.Models
{
    public class Prato
    {
        [Key]
        private long _id;

        private string _nome;

        private double _valor;

        private long _restauranteId;

        [NotMapped]
        private Restaurante _restaurante;

        public Prato()
        {
        }

        public Prato(long id, string nome, double valor, long restauranteId)
        {
            this.Id = id;
            this.Nome = nome;
            this.Valor = valor;
            this.RestauranteId = restauranteId;
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

        public double Valor
        {
            get
            {
                return this._valor;
            }
            set
            {
                this._valor = value;
            }
        }

        public long RestauranteId
        {
            get
            {
                return this._restauranteId;
            }
            set
            {
                this._restauranteId = value;
            }
        }

        public Restaurante Restaurante
        {
            get
            {
                return this._restaurante;
            }
            set
            {
                this._restaurante = value;
            }
        }
    }
}