using LyroundMVCIntegrationMert.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;


namespace LyroundMVCIntegrationMert.Models.Managers
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("DatabaseContext")
        {
            this.Database.CreateIfNotExists();
            //Database.SetInitializer(new VeritabaniOlusturucu());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public DbSet<Uye> Uyeler { get; set; }
        public DbSet<UyeResim> UyeResimleri { get; set; }
        public DbSet<Sarki> Sarkilar { get; set; }
        public DbSet<HashTag> HashTagler { get; set; }
        public DbSet<Yorum> Yorumlar { get; set; }
        public DbSet<SarkiResim> SarkiResimler { get; set; }
        public DbSet<PaylasilanText> PaylasilanTextler { get; set; }
        public DbSet<Mesaj> Mesajlar { get; set; }
        public DbSet<Begeni> Begeniler { get; set; }
        public DbSet<Arkadas> Arkadaslar { get; set; }

    }

    public class VeritabaniOlusturucu : CreateDatabaseIfNotExists<DatabaseContext>
    {

        public override void InitializeDatabase(DatabaseContext context)
        {
            context.Database.CreateIfNotExists();
            base.InitializeDatabase(context);
        }
    }
}