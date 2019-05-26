using System;
using System.Collections.Generic;
using System.Text;
using MesajCore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MesajCore.Data
{
    public class ApplicationDbContext : IdentityDbContext<Person>
    {
        public virtual DbSet<Mesaj> Mesajlar { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Mesaj>().HasKey(x => x.Id);
            builder.Entity<Mesaj>().Property(x => x.Yazi).IsRequired();

            builder.Entity<Mesaj>()
                .HasOne(m => m.Alici)
                .WithMany(p => p.AlinanMesajlar)
                .HasForeignKey(x => x.AliciId);

            builder.Entity<Mesaj>()
                .HasOne(m => m.Gonderen)
                .WithMany(p => p.GonderilenMesajlar)
                .HasForeignKey(x => x.GonderenId);

            base.OnModelCreating(builder);
        }
    }
}
