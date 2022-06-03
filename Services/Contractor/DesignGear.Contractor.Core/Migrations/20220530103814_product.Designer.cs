﻿// <auto-generated />
using System;
using DesignGear.Contractor.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DesignGear.Contractor.Core.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20220530103814_product")]
    partial class product
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("DesignGear.Contractor.Core.Data.Entity.Organization", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("CloudPoints")
                        .HasColumnType("float");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<int>("OrgType")
                        .HasColumnType("int");

                    b.Property<int>("SpaceUsed")
                        .HasColumnType("int");

                    b.Property<Guid?>("TariffId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TariffId");

                    b.ToTable("Organizations");
                });

            modelBuilder.Entity("DesignGear.Contractor.Core.Data.Entity.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CurrentVersionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<Guid>("OrganizationId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CurrentVersionId")
                        .IsUnique()
                        .HasFilter("[CurrentVersionId] IS NOT NULL");

                    b.HasIndex("OrganizationId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("DesignGear.Contractor.Core.Data.Entity.ProductVersion", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("DesignGearVersion")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("InventorVersion")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("SequenceNumber")
                        .HasColumnType("int");

                    b.Property<string>("Version")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductVersions");
                });

            modelBuilder.Entity("DesignGear.Contractor.Core.Data.Entity.Tariff", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Tariffs");
                });

            modelBuilder.Entity("DesignGear.Contractor.Core.Data.Entity.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("FirstName")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("LastName")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Phone")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DesignGear.Contractor.Core.Data.Entity.UserAssignment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OrganizationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("OrganizationId");

                    b.HasIndex("UserId");

                    b.ToTable("UserAssignments");
                });

            modelBuilder.Entity("DesignGear.Contractor.Core.Data.Entity.Organization", b =>
                {
                    b.HasOne("DesignGear.Contractor.Core.Data.Entity.Tariff", "Tariff")
                        .WithMany()
                        .HasForeignKey("TariffId");

                    b.Navigation("Tariff");
                });

            modelBuilder.Entity("DesignGear.Contractor.Core.Data.Entity.Product", b =>
                {
                    b.HasOne("DesignGear.Contractor.Core.Data.Entity.ProductVersion", "CurrentProductVersion")
                        .WithOne("CurrentVersionProduct")
                        .HasForeignKey("DesignGear.Contractor.Core.Data.Entity.Product", "CurrentVersionId");

                    b.HasOne("DesignGear.Contractor.Core.Data.Entity.Organization", "Organization")
                        .WithMany()
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CurrentProductVersion");

                    b.Navigation("Organization");
                });

            modelBuilder.Entity("DesignGear.Contractor.Core.Data.Entity.ProductVersion", b =>
                {
                    b.HasOne("DesignGear.Contractor.Core.Data.Entity.Product", "Product")
                        .WithMany("ProductVersions")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("DesignGear.Contractor.Core.Data.Entity.UserAssignment", b =>
                {
                    b.HasOne("DesignGear.Contractor.Core.Data.Entity.Organization", "Organization")
                        .WithMany("UserAssignments")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DesignGear.Contractor.Core.Data.Entity.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Organization");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DesignGear.Contractor.Core.Data.Entity.Organization", b =>
                {
                    b.Navigation("UserAssignments");
                });

            modelBuilder.Entity("DesignGear.Contractor.Core.Data.Entity.Product", b =>
                {
                    b.Navigation("ProductVersions");
                });

            modelBuilder.Entity("DesignGear.Contractor.Core.Data.Entity.ProductVersion", b =>
                {
                    b.Navigation("CurrentVersionProduct")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}