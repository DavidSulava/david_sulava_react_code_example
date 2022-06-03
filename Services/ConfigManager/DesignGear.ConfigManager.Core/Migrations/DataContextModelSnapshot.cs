﻿// <auto-generated />
using System;
using DesignGear.ConfigManager.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DesignGear.ConfigManager.Core.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("DesignGear.ConfigManager.Core.Data.Entity.AppBundle", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

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

                    b.HasKey("Id");

                    b.ToTable("AppBundles");
                });

            modelBuilder.Entity("DesignGear.ConfigManager.Core.Data.Entity.ComponentDefinition", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AppBundleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<Guid>("OrganizationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductVersionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TemplateConfigurationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UniqueId")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("AppBundleId");

                    b.ToTable("ComponentDefinitions");
                });

            modelBuilder.Entity("DesignGear.ConfigManager.Core.Data.Entity.Configuration", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ComponentDefinitionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("ErrorMessage")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<int>("ModelState")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<Guid?>("ParentConfigurationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RootConfigurationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("SvfStatus")
                        .HasColumnType("int");

                    b.Property<Guid?>("TargetFileId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("TemplateConfigurationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("URN")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("UniqueId")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("ComponentDefinitionId");

                    b.HasIndex("TargetFileId")
                        .IsUnique()
                        .HasFilter("[TargetFileId] IS NOT NULL");

                    b.HasIndex("TemplateConfigurationId");

                    b.ToTable("Configurations");
                });

            modelBuilder.Entity("DesignGear.ConfigManager.Core.Data.Entity.ConfigurationInstance", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ConfigurationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("X")
                        .HasColumnType("float");

                    b.Property<double>("XX")
                        .HasColumnType("float");

                    b.Property<double>("XY")
                        .HasColumnType("float");

                    b.Property<double>("XZ")
                        .HasColumnType("float");

                    b.Property<double>("Y")
                        .HasColumnType("float");

                    b.Property<double>("YX")
                        .HasColumnType("float");

                    b.Property<double>("YY")
                        .HasColumnType("float");

                    b.Property<double>("YZ")
                        .HasColumnType("float");

                    b.Property<double>("Z")
                        .HasColumnType("float");

                    b.Property<double>("ZX")
                        .HasColumnType("float");

                    b.Property<double>("ZY")
                        .HasColumnType("float");

                    b.Property<double>("ZZ")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("ConfigurationId");

                    b.ToTable("ConfigurationInstances");
                });

            modelBuilder.Entity("DesignGear.ConfigManager.Core.Data.Entity.FileItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ConfigurationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<bool>("IsModelFile")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("ConfigurationId");

                    b.ToTable("FileItems");
                });

            modelBuilder.Entity("DesignGear.ConfigManager.Core.Data.Entity.ParameterDefinition", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("AllowCustomValues")
                        .HasColumnType("bit");

                    b.Property<Guid>("ConfigurationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<int>("DisplayPriority")
                        .HasColumnType("int");

                    b.Property<bool>("IsHidden")
                        .HasColumnType("bit");

                    b.Property<bool>("IsReadOnly")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("UniqueId")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Units")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<int>("ValueType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ConfigurationId");

                    b.ToTable("ParameterDefinitions");
                });

            modelBuilder.Entity("DesignGear.ConfigManager.Core.Data.Entity.ValueOption", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ParameterDefinitionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ParameterDefinitionId");

                    b.ToTable("ValueOptions");
                });

            modelBuilder.Entity("DesignGear.ConfigManager.Core.Data.Entity.ComponentDefinition", b =>
                {
                    b.HasOne("DesignGear.ConfigManager.Core.Data.Entity.AppBundle", "AppBundle")
                        .WithMany()
                        .HasForeignKey("AppBundleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppBundle");
                });

            modelBuilder.Entity("DesignGear.ConfigManager.Core.Data.Entity.Configuration", b =>
                {
                    b.HasOne("DesignGear.ConfigManager.Core.Data.Entity.ComponentDefinition", "ComponentDefinition")
                        .WithMany("Configurations")
                        .HasForeignKey("ComponentDefinitionId");

                    b.HasOne("DesignGear.ConfigManager.Core.Data.Entity.FileItem", "TargetFileItem")
                        .WithOne("Configuration")
                        .HasForeignKey("DesignGear.ConfigManager.Core.Data.Entity.Configuration", "TargetFileId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("DesignGear.ConfigManager.Core.Data.Entity.Configuration", "TemplateConfiguration")
                        .WithMany()
                        .HasForeignKey("TemplateConfigurationId");

                    b.Navigation("ComponentDefinition");

                    b.Navigation("TargetFileItem");

                    b.Navigation("TemplateConfiguration");
                });

            modelBuilder.Entity("DesignGear.ConfigManager.Core.Data.Entity.ConfigurationInstance", b =>
                {
                    b.HasOne("DesignGear.ConfigManager.Core.Data.Entity.Configuration", "Configuration")
                        .WithMany("ConvigurationInstances")
                        .HasForeignKey("ConfigurationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Configuration");
                });

            modelBuilder.Entity("DesignGear.ConfigManager.Core.Data.Entity.FileItem", b =>
                {
                    b.HasOne("DesignGear.ConfigManager.Core.Data.Entity.Configuration", null)
                        .WithMany("FileItems")
                        .HasForeignKey("ConfigurationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DesignGear.ConfigManager.Core.Data.Entity.ParameterDefinition", b =>
                {
                    b.HasOne("DesignGear.ConfigManager.Core.Data.Entity.Configuration", "Configuration")
                        .WithMany("ParameterDefinitions")
                        .HasForeignKey("ConfigurationId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Configuration");
                });

            modelBuilder.Entity("DesignGear.ConfigManager.Core.Data.Entity.ValueOption", b =>
                {
                    b.HasOne("DesignGear.ConfigManager.Core.Data.Entity.ParameterDefinition", "ParameterDefinition")
                        .WithMany("ValueOptions")
                        .HasForeignKey("ParameterDefinitionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ParameterDefinition");
                });

            modelBuilder.Entity("DesignGear.ConfigManager.Core.Data.Entity.ComponentDefinition", b =>
                {
                    b.Navigation("Configurations");
                });

            modelBuilder.Entity("DesignGear.ConfigManager.Core.Data.Entity.Configuration", b =>
                {
                    b.Navigation("ConvigurationInstances");

                    b.Navigation("FileItems");

                    b.Navigation("ParameterDefinitions");
                });

            modelBuilder.Entity("DesignGear.ConfigManager.Core.Data.Entity.FileItem", b =>
                {
                    b.Navigation("Configuration")
                        .IsRequired();
                });

            modelBuilder.Entity("DesignGear.ConfigManager.Core.Data.Entity.ParameterDefinition", b =>
                {
                    b.Navigation("ValueOptions");
                });
#pragma warning restore 612, 618
        }
    }
}
