﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TechnoMarket.Infrastructure;

#nullable disable

namespace TechnoMarket.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20240724220422_UpdateStoreForeignKey")]
    partial class UpdateStoreForeignKey
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.7");

            modelBuilder.Entity("TechnoMarket.Domain.Entities.Buyer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Status")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Buyer");
                });

            modelBuilder.Entity("TechnoMarket.Domain.Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Discount")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Offer")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Price")
                        .HasColumnType("TEXT");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Rating")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("SaleId")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("StoreId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("SaleId");

                    b.HasIndex("StoreId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("TechnoMarket.Domain.Entities.Sale", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("BuyerId")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("StoreId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("BuyerId");

                    b.HasIndex("StoreId");

                    b.ToTable("Sales");
                });

            modelBuilder.Entity("TechnoMarket.Domain.Entities.Store", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Rating")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("idOwner")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("idOwner")
                        .IsUnique();

                    b.ToTable("Stores");
                });

            modelBuilder.Entity("TechnoMarket.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Active")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("StoreId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TechnoMarket.Domain.Entities.Product", b =>
                {
                    b.HasOne("TechnoMarket.Domain.Entities.Sale", null)
                        .WithMany("Products")
                        .HasForeignKey("SaleId");

                    b.HasOne("TechnoMarket.Domain.Entities.Store", "Store")
                        .WithMany("Inventory")
                        .HasForeignKey("StoreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Store");
                });

            modelBuilder.Entity("TechnoMarket.Domain.Entities.Sale", b =>
                {
                    b.HasOne("TechnoMarket.Domain.Entities.Buyer", "Buyer")
                        .WithMany()
                        .HasForeignKey("BuyerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TechnoMarket.Domain.Entities.Store", null)
                        .WithMany("Sales")
                        .HasForeignKey("StoreId");

                    b.Navigation("Buyer");
                });

            modelBuilder.Entity("TechnoMarket.Domain.Entities.Store", b =>
                {
                    b.HasOne("TechnoMarket.Domain.Entities.User", "Owner")
                        .WithOne("Store")
                        .HasForeignKey("TechnoMarket.Domain.Entities.Store", "idOwner")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("TechnoMarket.Domain.Entities.Sale", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("TechnoMarket.Domain.Entities.Store", b =>
                {
                    b.Navigation("Inventory");

                    b.Navigation("Sales");
                });

            modelBuilder.Entity("TechnoMarket.Domain.Entities.User", b =>
                {
                    b.Navigation("Store");
                });
#pragma warning restore 612, 618
        }
    }
}
