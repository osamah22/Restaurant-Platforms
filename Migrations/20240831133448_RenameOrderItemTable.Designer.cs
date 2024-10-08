﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Restaurants_Platform.Data;

#nullable disable

namespace Restaurants_Platform.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20240831133448_RenameOrderItemTable")]
    partial class RenameOrderItemTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Restaurants_Platform.Models.FoodItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("RestId")
                        .HasColumnType("uuid");

                    b.HasKey("Id")
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.HasIndex("RestId");

                    b.ToTable("FoodItem", (string)null);
                });

            modelBuilder.Entity("Restaurants_Platform.Models.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Restaurants_Platform.Models.OrderItem", b =>
                {
                    b.Property<Guid>("OrderId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("FoodItemId")
                        .HasColumnType("uuid");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.HasKey("OrderId", "FoodItemId");

                    b.HasIndex("FoodItemId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("Restaurants_Platform.Models.Restaurant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Company")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id")
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.ToTable("Restaurant", (string)null);
                });

            modelBuilder.Entity("Restaurants_Platform.Models.FoodItem", b =>
                {
                    b.HasOne("Restaurants_Platform.Models.Restaurant", "Restaurant")
                        .WithMany("Menu")
                        .HasForeignKey("RestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("Restaurants_Platform.Models.OrderItem", b =>
                {
                    b.HasOne("Restaurants_Platform.Models.FoodItem", "FoodItem")
                        .WithMany()
                        .HasForeignKey("FoodItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Restaurants_Platform.Models.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FoodItem");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Restaurants_Platform.Models.Order", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("Restaurants_Platform.Models.Restaurant", b =>
                {
                    b.Navigation("Menu");
                });
#pragma warning restore 612, 618
        }
    }
}
