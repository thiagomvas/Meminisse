﻿// <auto-generated />
using System;
using Meminisse.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Meminisse.Application.Migrations
{
    [DbContext(typeof(MeminisseDbContext))]
    partial class MeminisseDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0");

            modelBuilder.Entity("Meminisse.Core.Entities.Memory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("TEXT");

                    b.Property<int>("Emotion")
                        .HasColumnType("INTEGER");

                    b.Property<Guid?>("PersonId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.ToTable("Memories");
                });

            modelBuilder.Entity("Meminisse.Core.Entities.MemoryItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("datetime");

                    b.Property<Guid>("MemoryId")
                        .HasColumnType("TEXT");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("MemoryId");

                    b.ToTable("MemoryItems");
                });

            modelBuilder.Entity("Meminisse.Core.Entities.MemoryPerson", b =>
                {
                    b.Property<Guid>("MemoryId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("PersonId")
                        .HasColumnType("TEXT");

                    b.HasKey("MemoryId", "PersonId");

                    b.HasIndex("PersonId");

                    b.ToTable("MemoryPeople");
                });

            modelBuilder.Entity("Meminisse.Core.Entities.MemoryTag", b =>
                {
                    b.Property<Guid>("MemoryId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("TagId")
                        .HasColumnType("TEXT");

                    b.HasKey("MemoryId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("MemoryTags");
                });

            modelBuilder.Entity("Meminisse.Core.Entities.Person", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Bio")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("Birthday")
                        .HasColumnType("datetime");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<int>("Gender")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ProfilePictureUrl")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("People");
                });

            modelBuilder.Entity("Meminisse.Core.Entities.PersonTag", b =>
                {
                    b.Property<Guid>("PersonId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("TagId")
                        .HasColumnType("TEXT");

                    b.HasKey("PersonId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("PersonTags");
                });

            modelBuilder.Entity("Meminisse.Core.Entities.Tag", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("Meminisse.Core.Entities.Memory", b =>
                {
                    b.HasOne("Meminisse.Core.Entities.Person", null)
                        .WithMany("Memories")
                        .HasForeignKey("PersonId");
                });

            modelBuilder.Entity("Meminisse.Core.Entities.MemoryItem", b =>
                {
                    b.HasOne("Meminisse.Core.Entities.Memory", "Memory")
                        .WithMany("MemoryItems")
                        .HasForeignKey("MemoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Memory");
                });

            modelBuilder.Entity("Meminisse.Core.Entities.MemoryPerson", b =>
                {
                    b.HasOne("Meminisse.Core.Entities.Memory", "Memory")
                        .WithMany("MemoryPeople")
                        .HasForeignKey("MemoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Meminisse.Core.Entities.Person", "Person")
                        .WithMany("MemoryPeople")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Memory");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("Meminisse.Core.Entities.MemoryTag", b =>
                {
                    b.HasOne("Meminisse.Core.Entities.Memory", "Memory")
                        .WithMany("MemoryTags")
                        .HasForeignKey("MemoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Meminisse.Core.Entities.Tag", "Tag")
                        .WithMany("MemoryTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Memory");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("Meminisse.Core.Entities.PersonTag", b =>
                {
                    b.HasOne("Meminisse.Core.Entities.Person", "Person")
                        .WithMany("PersonTags")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Meminisse.Core.Entities.Tag", "Tag")
                        .WithMany("PersonTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("Meminisse.Core.Entities.Memory", b =>
                {
                    b.Navigation("MemoryItems");

                    b.Navigation("MemoryPeople");

                    b.Navigation("MemoryTags");
                });

            modelBuilder.Entity("Meminisse.Core.Entities.Person", b =>
                {
                    b.Navigation("Memories");

                    b.Navigation("MemoryPeople");

                    b.Navigation("PersonTags");
                });

            modelBuilder.Entity("Meminisse.Core.Entities.Tag", b =>
                {
                    b.Navigation("MemoryTags");

                    b.Navigation("PersonTags");
                });
#pragma warning restore 612, 618
        }
    }
}
