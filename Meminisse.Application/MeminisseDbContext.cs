using Meminisse.Core.Entities;
using Microsoft.EntityFrameworkCore;

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
    public DbSet<MemoryItem> MemoryItems { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<MemoryPerson> MemoryPeople { get; set; }
    public DbSet<MemoryTag> MemoryTags { get; set; }
    public DbSet<PersonTag> PersonTags { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseSqlite("Data Source=meminisse.db");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(p => p.Id); // Primary key
            entity.Property(p => p.FullName).IsRequired().HasMaxLength(200);
            entity.Property(p => p.Bio).HasMaxLength(1000);
            entity.Property(p => p.ProfilePictureUrl).HasMaxLength(500);
            entity.Property(p => p.Birthday).HasColumnType("datetime");

            // Relationship with MemoryPerson
            entity.HasMany(p => p.MemoryPeople)
                  .WithOne(mp => mp.Person)
                  .HasForeignKey(mp => mp.PersonId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Configure Memory entity
        modelBuilder.Entity<Memory>(entity =>
        {
            entity.HasKey(m => m.Id); // Primary key
            entity.Property(m => m.Title).IsRequired().HasMaxLength(200);
            entity.Property(m => m.Description).HasMaxLength(2000);
            entity.Property(m => m.Date).HasColumnType("datetime");

            // Relationship with MemoryPerson
            entity.HasMany(m => m.MemoryPeople)
                  .WithOne(mp => mp.Memory)
                  .HasForeignKey(mp => mp.MemoryId)
                  .OnDelete(DeleteBehavior.Cascade);

            // Relationship with MemoryItem
            entity.HasMany(m => m.MemoryItems)
                  .WithOne(mi => mi.Memory)
                  .HasForeignKey(mi => mi.MemoryId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Configure MemoryItem entity
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

        // Configure Tag entity
        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(t => t.Id); // Primary key
            entity.Property(t => t.Name).IsRequired().HasMaxLength(100);
        });

        // Configure MemoryPerson entity (junction table for many-to-many)
        modelBuilder.Entity<MemoryPerson>(entity =>
        {
            entity.HasKey(mp => new { mp.MemoryId, mp.PersonId }); // Composite key

            // Relationship with Person
            entity.HasOne(mp => mp.Person)
                  .WithMany(p => p.MemoryPeople)
                  .HasForeignKey(mp => mp.PersonId);

            // Relationship with Memory
            entity.HasOne(mp => mp.Memory)
                  .WithMany(m => m.MemoryPeople)
                  .HasForeignKey(mp => mp.MemoryId);
        });

        // Configure MemoryTag entity (junction table for many-to-many)
        modelBuilder.Entity<MemoryTag>(entity =>
        {
            entity.HasKey(mt => new { mt.MemoryId, mt.TagId }); // Composite key
            // Relationship with Memory
            entity.HasOne(mt => mt.Memory)
                  .WithMany(m => m.MemoryTags)
                  .HasForeignKey(mt => mt.MemoryId);
            // Relationship with Tag
            entity.HasOne(mt => mt.Tag)
                  .WithMany(t => t.MemoryTags)
                  .HasForeignKey(mt => mt.TagId);
        });

        // Configure PersonTag entity (junction table for many-to-many)
        modelBuilder.Entity<PersonTag>(entity =>
        {
            entity.HasKey(pt => new { pt.PersonId, pt.TagId }); // Composite key
            // Relationship with Person
            entity.HasOne(pt => pt.Person)
                  .WithMany(p => p.PersonTags)
                  .HasForeignKey(pt => pt.PersonId);
            // Relationship with Tag
            entity.HasOne(pt => pt.Tag)
                  .WithMany(t => t.PersonTags)
                  .HasForeignKey(pt => pt.TagId);
        });
    }
}
