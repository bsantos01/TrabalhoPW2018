using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TrabalhoPW.Models
{
    public class MuseuContext: ApplicationDbContext
    {
       

        public MuseuContext() { 
        /*   try
            {
                Database.SetInitializer<MuseuContext>(new DropCreateDatabaseIfModelChanges<MuseuContext>());
            }
            catch (Exception)
            {
                throw;
            }*/
        }
        public DbSet<Aluguer> Aluguer{ get; set; }
        public DbSet<Mensagem> Mensagem { get; set; }
        public DbSet<Objeto> Objeto { get; set; }
        public DbSet<Tratamento> Tratamento { get; set; }
        public DbSet<Utilizador> Utilizador { get; set; }
        public DbSet<Texts> Texts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        }
}