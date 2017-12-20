using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MySQL.Data.Entity.Extensions;
using RestauranteApi.Models;

namespace RestauranteApi.Context
{
    public class RestauranteDbContext : DbContext
    {
        public DbSet<Restaurante> Restaurantes { get; set; }
        public DbSet<Prato> Pratos { get; set; }

        private IConfigurationRoot _config;
        private static bool _created = false;

        //public RestauranteDbContext(DbContextOptions<RestauranteDbContext> options) : base(options) { }


        public RestauranteDbContext(IConfigurationRoot config, DbContextOptions options) : base(options)
        {
            _config = config;

            if (!_created)
            {
                _created = true;
                Database.EnsureDeleted();
                Database.EnsureCreated();
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseMySQL(_config["ConnectionStrings:MySqlConnection"]);
        }
    }
}