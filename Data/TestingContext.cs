using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Medicos.Backend.Data.Models;

public partial class TestingContext : IdentityDbContext<AppUser>
{
    public TestingContext()
    {
    }

    public TestingContext(DbContextOptions<TestingContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Especialidad> Especialidads { get; set; }

    public virtual DbSet<Medico> Medicos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Especialidad>(entity =>
        {
            entity.ToTable("Especialidad");
        });

        modelBuilder.Entity<Medico>(entity =>
        {
            entity.ToTable("Medico");

            entity.HasOne(d => d.EspecialidadNavigation).WithMany(p => p.Medicos)
                .HasForeignKey(d => d.Especialidad)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
