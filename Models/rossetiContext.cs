using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Rosseti.Models
{
    public partial class rossetiContext : DbContext
    {
        public rossetiContext()
        {
        }

        public rossetiContext(DbContextOptions<rossetiContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DateOscillograms> DateOscillograms { get; set; }
        public virtual DbSet<Oscillograms> Oscillograms { get; set; }
        public virtual DbSet<RightOscillogramsStep> RightOscillogramsStep { get; set; }
        public virtual DbSet<SprErrorOscillograms> SprErrorOscillograms { get; set; }
        public virtual DbSet<SprErrors> SprErrors { get; set; }
        public virtual DbSet<SprOscillograms> SprOscillograms { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=192.168.116.233;Initial Catalog=rosseti;User ID=sa;Password=@sql2016");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DateOscillograms>(entity =>
            {
                entity.HasIndex(e => e.SprOscillogramId);

                entity.HasOne(d => d.SprOscillogram)
                    .WithMany(p => p.DateOscillograms)
                    .HasForeignKey(d => d.SprOscillogramId);
            });

            modelBuilder.Entity<Oscillograms>(entity =>
            {
                entity.HasIndex(e => e.DateOscillogramId);

                entity.HasOne(d => d.DateOscillogram)
                    .WithMany(p => p.Oscillograms)
                    .HasForeignKey(d => d.DateOscillogramId);
            });

            modelBuilder.Entity<RightOscillogramsStep>(entity =>
            {
                entity.HasIndex(e => e.SprOscillogramId);

                entity.HasOne(d => d.SprOscillogram)
                    .WithMany(p => p.RightOscillogramsStep)
                    .HasForeignKey(d => d.SprOscillogramId);
            });

            modelBuilder.Entity<SprErrorOscillograms>(entity =>
            {
                entity.HasIndex(e => e.SprErrorId);

                entity.HasOne(d => d.SprError)
                    .WithMany(p => p.SprErrorOscillograms)
                    .HasForeignKey(d => d.SprErrorId);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
