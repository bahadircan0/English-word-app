using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using İngilizceKelimeler.Models.Entity;

namespace İngilizceKelimeler.Data;

public partial class EnglishKelimeTakibiContext : DbContext
{
    public EnglishKelimeTakibiContext()
    {
    }

    public EnglishKelimeTakibiContext(DbContextOptions<EnglishKelimeTakibiContext> options)
        : base(options)
    {
    }

    public  DbSet<EnglishKelimeler> EnglishKelimelers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=English_KelimeTakibi;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EnglishKelimeler>(entity =>
        {
            entity.HasKey(e => e.KelimeId).HasName("PK_İngilizce_Kelimeler");

            entity.ToTable("English_Kelimeler");

            entity.Property(e => e.KelimeBilememeSayisi).HasDefaultValue(0);
            entity.Property(e => e.KelimeBilmeSayisi).HasDefaultValue(0);
            entity.Property(e => e.KelimeTur)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.KelimeVisible).HasDefaultValue(true);
            entity.Property(e => e.Kelimeİng)
                .HasMaxLength(100)
                .IsUnicode(false);
           
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
