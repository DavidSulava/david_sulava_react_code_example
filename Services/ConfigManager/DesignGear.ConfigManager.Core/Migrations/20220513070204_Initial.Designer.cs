﻿// <auto-generated />
using System;
using DesignGear.ConfigManager.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DesignGear.ConfigManager.Core.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20220513070204_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.ToTable("AppbBundles");
                });

            modelBuilder.Entity("DesignGear.ConfigManager.Core.Data.Entity.ComponentDefinition", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<Guid>("TemplateConfigurationId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

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

                    b.Property<Guid>("ComponentDefinitionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ModelState")
                        .HasColumnType("int");

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

                    b.Property<Guid>("TargetFileId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ComponentDefinitionId");

                    b.ToTable("Configurations");
                });

            modelBuilder.Entity("DesignGear.ConfigManager.Core.Data.Entity.ConfigurationInstance", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ConfigurationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ParentConfigurationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("X")
                        .HasColumnType("int");

                    b.Property<int>("XX")
                        .HasColumnType("int");

                    b.Property<int>("XY")
                        .HasColumnType("int");

                    b.Property<int>("XZ")
                        .HasColumnType("int");

                    b.Property<int>("Y")
                        .HasColumnType("int");

                    b.Property<int>("YX")
                        .HasColumnType("int");

                    b.Property<int>("YY")
                        .HasColumnType("int");

                    b.Property<int>("YZ")
                        .HasColumnType("int");

                    b.Property<int>("Z")
                        .HasColumnType("int");

                    b.Property<int>("ZX")
                        .HasColumnType("int");

                    b.Property<int>("ZY")
                        .HasColumnType("int");

                    b.Property<int>("ZZ")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ConfigurationId");

                    b.HasIndex("ParentConfigurationId");

                    b.ToTable("ConfigurationInstances");
                });

            modelBuilder.Entity("DesignGear.ConfigManager.Core.Data.Entity.ParameterDefinition", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("AllowCustomValues")
                        .HasColumnType("bit");

                    b.Property<Guid>("ComponentDefinitionId")
                        .HasColumnType("uniqueidentifier");

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

                    b.Property<string>("Units")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<int>("ValueType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ComponentDefinitionId");

                    b.ToTable("ParameterDefinitions");
                });

            modelBuilder.Entity("DesignGear.ConfigManager.Core.Data.Entity.ParameterValue", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ConfigurationId")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ParameterDefinitionId")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.HasKey("Id");

                    b.HasIndex("ConfigurationId");

                    b.HasIndex("ParameterDefinitionId");

                    b.ToTable("ParameterValue");
                });

            modelBuilder.Entity("DesignGear.ConfigManager.Core.Data.Entity.ValueOption", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ParameterDefinitionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ParameterDefinitionId");

                    b.ToTable("ValueOptions");
                });

            modelBuilder.Entity("DesignGear.ConfigManager.Core.Data.Entity.Configuration", b =>
                {
                    b.HasOne("DesignGear.ConfigManager.Core.Data.Entity.ComponentDefinition", "ComponentDefinition")
                        .WithMany("Configurations")
                        .HasForeignKey("ComponentDefinitionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ComponentDefinition");
                });

            modelBuilder.Entity("DesignGear.ConfigManager.Core.Data.Entity.ConfigurationInstance", b =>
                {
                    b.HasOne("DesignGear.ConfigManager.Core.Data.Entity.Configuration", "Configuration")
                        .WithMany("ConfigurationInstances")
                        .HasForeignKey("ConfigurationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DesignGear.ConfigManager.Core.Data.Entity.Configuration", "ParentConfiguration")
                        .WithMany("ParentConfigurationInstances")
                        .HasForeignKey("ParentConfigurationId");

                    b.Navigation("Configuration");

                    b.Navigation("ParentConfiguration");
                });

            modelBuilder.Entity("DesignGear.ConfigManager.Core.Data.Entity.ParameterDefinition", b =>
                {
                    b.HasOne("DesignGear.ConfigManager.Core.Data.Entity.ComponentDefinition", "ComponentDefinition")
                        .WithMany("ParameterDefinitions")
                        .HasForeignKey("ComponentDefinitionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ComponentDefinition");
                });

            modelBuilder.Entity("DesignGear.ConfigManager.Core.Data.Entity.ParameterValue", b =>
                {
                    b.HasOne("DesignGear.ConfigManager.Core.Data.Entity.Configuration", "Configuration")
                        .WithMany("ParameterValues")
                        .HasForeignKey("ConfigurationId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DesignGear.ConfigManager.Core.Data.Entity.ParameterDefinition", "ParameterDefinition")
                        .WithMany("ParameterValues")
                        .HasForeignKey("ParameterDefinitionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Configuration");

                    b.Navigation("ParameterDefinition");
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

                    b.Navigation("ParameterDefinitions");
                });

            modelBuilder.Entity("DesignGear.ConfigManager.Core.Data.Entity.Configuration", b =>
                {
                    b.Navigation("ConfigurationInstances");

                    b.Navigation("ParameterValues");

                    b.Navigation("ParentConfigurationInstances");
                });

            modelBuilder.Entity("DesignGear.ConfigManager.Core.Data.Entity.ParameterDefinition", b =>
                {
                    b.Navigation("ParameterValues");

                    b.Navigation("ValueOptions");
                });
#pragma warning restore 612, 618
        }
    }
}
