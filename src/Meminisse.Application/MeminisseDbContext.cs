using Meminisse.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Meminisse.Application;

public class MeminisseDbContext : DbContext
{
    public MeminisseDbContext() : base()
    {

    }
    public MeminisseDbContext(DbContextOptions<MeminisseDbContext> options) : base(options)
    {
    }

    public DbSet<Person> People { get; set; }
    public DbSet<Memory> Memories { get; set; }
    public DbSet<MemoryPerson> MemoryPeople { get; set; }
    public DbSet<MemoryTag> MemoryTags { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<PersonTag> PersonTags { get; set; }
    public DbSet<MemoryItem> MemoryItems { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseSqlite("Data Source=meminisse.db");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Person Entity
        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(p => p.Id); // Primary key

            entity.Property(p => p.FullName)
                  .IsRequired()
                  .HasMaxLength(200); // Example constraint for FullName

            entity.Property(p => p.Bio)
                  .HasMaxLength(1000); // Example constraint for Bio

            entity.Property(p => p.ProfilePictureUrl)
                  .HasMaxLength(500); // Example constraint for ProfilePictureUrl

            entity.HasMany(p => p.MemoryPeople)
                  .WithOne(mp => mp.Person)
                  .HasForeignKey(mp => mp.PersonId)
                  .OnDelete(DeleteBehavior.Cascade); // Many-to-many relationship with MemoryPerson
        });

        // Memory Entity
        modelBuilder.Entity<Memory>(entity =>
        {
            entity.HasKey(m => m.Id); // Primary key

            entity.Property(m => m.Title)
                  .IsRequired()
                  .HasMaxLength(200); // Example constraint for Title

            entity.Property(m => m.Description)
                  .HasMaxLength(2000); // Example constraint for Description

            entity.HasMany(m => m.MemoryPeople)
                  .WithOne(mp => mp.Memory)
                  .HasForeignKey(mp => mp.MemoryId)
                  .OnDelete(DeleteBehavior.Cascade); // Many-to-many relationship with MemoryPerson

            entity.HasMany(m => m.MemoryTags)
                  .WithOne(mt => mt.Memory)
                  .HasForeignKey(mt => mt.MemoryId)
                  .OnDelete(DeleteBehavior.Cascade); // One-to-many relationship with MemoryTag

            entity.HasMany(m => m.MemoryItems)
                  .WithOne(mi => mi.Memory)
                  .HasForeignKey(mi => mi.MemoryId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // MemoryPerson Entity (join table for many-to-many relationship)
        modelBuilder.Entity<MemoryPerson>(entity =>
        {
            entity.HasKey(mp => new { mp.MemoryId, mp.PersonId }); // Composite primary key

            entity.HasOne(mp => mp.Memory)
                  .WithMany(m => m.MemoryPeople)
                  .HasForeignKey(mp => mp.MemoryId);

            entity.HasOne(mp => mp.Person)
                  .WithMany(p => p.MemoryPeople)
                  .HasForeignKey(mp => mp.PersonId);
        });

        // Tag Entity
        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(t => t.Id); // Primary key
            entity.Property(t => t.Name)
                  .IsRequired()
                  .HasMaxLength(100); // Example constraint for Name
            entity.HasMany(t => t.MemoryTags)
                  .WithOne(mt => mt.Tag)
                  .HasForeignKey(mt => mt.TagId)
                  .OnDelete(DeleteBehavior.Cascade); // One-to-many relationship with MemoryTag
            entity.HasMany(t => t.PersonTags)
                  .WithOne(pt => pt.Tag)
                  .HasForeignKey(pt => pt.TagId)
                  .OnDelete(DeleteBehavior.Cascade); // One-to-many relationship with PersonTag
        });

        // MemoryTag Entity (join table for many-to-many relationship)
        modelBuilder.Entity<MemoryTag>(entity =>
        {
            entity.HasKey(mt => new { mt.MemoryId, mt.TagId }); // Composite primary key
            entity.HasOne(mt => mt.Memory)
                  .WithMany(m => m.MemoryTags)
                  .HasForeignKey(mt => mt.MemoryId);
            entity.HasOne(mt => mt.Tag)
                  .WithMany(t => t.MemoryTags)
                  .HasForeignKey(mt => mt.TagId);
        });

        // PersonTag Entity (join table for many-to-many relationship)
        modelBuilder.Entity<PersonTag>(entity =>
        {
            entity.HasKey(pt => new { pt.PersonId, pt.TagId }); // Composite primary key
            entity.HasOne(pt => pt.Person)
                  .WithMany(p => p.PersonTags)
                  .HasForeignKey(pt => pt.PersonId);
            entity.HasOne(pt => pt.Tag)
                  .WithMany(t => t.PersonTags)
                  .HasForeignKey(pt => pt.TagId);
        });

        modelBuilder.Entity<MemoryItem>(entity =>
        {
            entity.HasKey(mi => mi.Id); // Primary key
            entity.Property(mi => mi.Content).IsRequired();
            entity.Property(mi => mi.DateAdded).HasColumnType("datetime");
            entity.Property(mi => mi.Type).IsRequired();

            // Relationship with Memory
            entity.HasOne(mi => mi.Memory)
                  .WithMany(m => m.MemoryItems)
                  .HasForeignKey(mi => mi.MemoryId);
        });

    }
}