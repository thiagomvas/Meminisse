﻿// <auto-generated />
using System;
using Meminisse.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Meminisse.Application.Migrations
{
    [DbContext(typeof(MeminisseDbContext))]
    [Migration("20241215021535_AddMemoryPersonTable")]
    partial class AddMemoryPersonTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0");

            modelBuilder.Entity("Meminisse.Core.Entities.Memory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("PersonId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
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
                        .HasColumnType("TEXT");

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

            modelBuilder.Entity("Meminisse.Core.Entities.Person", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Bio")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ProfilePictureUrl")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("People");
                });

            modelBuilder.Entity("Meminisse.Core.Entities.Tag", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("MemoryTag", b =>
                {
                    b.Property<Guid>("MemoryId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("TagId")
                        .HasColumnType("TEXT");

                    b.HasKey("MemoryId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("MemoryTag");
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
                        .WithMany("Items")
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

            modelBuilder.Entity("MemoryTag", b =>
                {
                    b.HasOne("Meminisse.Core.Entities.Memory", null)
                        .WithMany()
                        .HasForeignKey("MemoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Meminisse.Core.Entities.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Meminisse.Core.Entities.Memory", b =>
                {
                    b.Navigation("Items");

                    b.Navigation("MemoryPeople");
                });

            modelBuilder.Entity("Meminisse.Core.Entities.Person", b =>
                {
                    b.Navigation("Memories");

                    b.Navigation("MemoryPeople");
                });
#pragma warning restore 612, 618
        }
    }
}
