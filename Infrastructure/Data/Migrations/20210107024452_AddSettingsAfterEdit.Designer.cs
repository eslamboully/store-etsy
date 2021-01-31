﻿// <auto-generated />
using System;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Data.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20210107024452_AddSettingsAfterEdit")]
    partial class AddSettingsAfterEdit
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:Collation", "Latin1_General_100_CI_AI_SC_UTF8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("AdminRole", b =>
                {
                    b.Property<int>("AdminsId")
                        .HasColumnType("int");

                    b.Property<int>("RolesId")
                        .HasColumnType("int");

                    b.HasKey("AdminsId", "RolesId");

                    b.HasIndex("RolesId");

                    b.ToTable("AdminRole");
                });

            modelBuilder.Entity("Core.Areas.Dashboard.Entities.Admin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(180)
                        .HasColumnType("nvarchar(180)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(180)
                        .HasColumnType("nvarchar(180)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(180)
                        .HasColumnType("nvarchar(180)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(180)
                        .HasColumnType("nvarchar(180)");

                    b.HasKey("Id");

                    b.ToTable("Admins");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "eslamboully@gmail.com",
                            FirstName = "عبدالرحمن",
                            LastName = "اسامة",
                            Password = "$2y$12$rt2vHotyKtNEx.lWp.F9Yemtmbq2mu9F5pZNYHaWsyD8ctKLYUGda"
                        });
                });

            modelBuilder.Entity("Core.Areas.Dashboard.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Core.Areas.Dashboard.Entities.CategoryTranslation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Locale")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Words")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("CategoryTranslations");
                });

            modelBuilder.Entity("Core.Areas.Dashboard.Entities.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("Core.Areas.Dashboard.Entities.CityTranslation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<string>("Locale")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("CityTranslations");
                });

            modelBuilder.Entity("Core.Areas.Dashboard.Entities.Color", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Degree")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Colors");
                });

            modelBuilder.Entity("Core.Areas.Dashboard.Entities.ColorTranslation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("ColorId")
                        .HasColumnType("int");

                    b.Property<string>("Locale")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ColorId");

                    b.ToTable("ColorTranslations");
                });

            modelBuilder.Entity("Core.Areas.Dashboard.Entities.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Logo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mob")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("Core.Areas.Dashboard.Entities.CountryTranslation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<string>("Locale")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("CountryTranslations");
                });

            modelBuilder.Entity("Core.Areas.Dashboard.Entities.Mall", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Owner")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Malls");
                });

            modelBuilder.Entity("Core.Areas.Dashboard.Entities.MallTranslation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Locale")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MallId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MallId");

                    b.ToTable("MallTranslations");
                });

            modelBuilder.Entity("Core.Areas.Dashboard.Entities.ManuFact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ContactName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Facebook")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lng")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Logo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Twitter")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ManuFacts");
                });

            modelBuilder.Entity("Core.Areas.Dashboard.Entities.ManuFactTranslation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Locale")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ManuFactId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ManuFactId");

                    b.ToTable("ManuFactTranslations");
                });

            modelBuilder.Entity("Core.Areas.Dashboard.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Core.Areas.Dashboard.Entities.Setting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<bool>("IsStatic")
                        .HasColumnType("bit");

                    b.Property<string>("StaticValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<string>("Var")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("Core.Areas.Dashboard.Entities.SettingTranslation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Locale")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SettingId")
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("SettingId");

                    b.ToTable("SettingTranslations");
                });

            modelBuilder.Entity("Core.Areas.Dashboard.Entities.Shipping", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lng")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Logo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Shippings");
                });

            modelBuilder.Entity("Core.Areas.Dashboard.Entities.ShippingTranslation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Locale")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ShippingId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ShippingId");

                    b.ToTable("ShippingTranslations");
                });

            modelBuilder.Entity("Core.Areas.Dashboard.Entities.Size", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.HasKey("Id");

                    b.ToTable("Sizes");
                });

            modelBuilder.Entity("Core.Areas.Dashboard.Entities.SizeTranslation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Locale")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SizeId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("SizeId");

                    b.ToTable("SizeTranslations");
                });

            modelBuilder.Entity("Core.Areas.Dashboard.Entities.State", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("CountryId");

                    b.ToTable("States");
                });

            modelBuilder.Entity("Core.Areas.Dashboard.Entities.StateTranslation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Locale")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StateId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("StateId");

                    b.ToTable("StateTranslations");
                });

            modelBuilder.Entity("Core.Areas.Dashboard.Entities.TradeMark", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Logo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TradeMarks");
                });

            modelBuilder.Entity("Core.Areas.Dashboard.Entities.TradeMarkTranslation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Locale")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TradeMarkId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TradeMarkId");

                    b.ToTable("TradeMarkTranslations");
                });

            modelBuilder.Entity("Core.Areas.Dashboard.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(180)
                        .HasColumnType("nvarchar(180)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(180)
                        .HasColumnType("nvarchar(180)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(180)
                        .HasColumnType("nvarchar(180)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(180)
                        .HasColumnType("nvarchar(180)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(180)
                        .HasColumnType("nvarchar(180)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(180)
                        .HasColumnType("nvarchar(180)");

                    b.Property<short>("Type")
                        .HasColumnType("smallint");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Core.Areas.Dashboard.Entities.Weight", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.HasKey("Id");

                    b.ToTable("Weights");
                });

            modelBuilder.Entity("Core.Areas.Dashboard.Entities.WeightTranslation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Locale")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WeightId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WeightId");

                    b.ToTable("WeightTranslations");
                });

            modelBuilder.Entity("AdminRole", b =>
                {
                    b.HasOne("Core.Areas.Dashboard.Entities.Admin", null)
                        .WithMany()
                        .HasForeignKey("AdminsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Areas.Dashboard.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Core.Areas.Dashboard.Entities.Category", b =>
                {
                    b.HasOne("Core.Areas.Dashboard.Entities.Category", "Parent")
                        .WithMany("Childs")
                        .HasForeignKey("ParentId");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("Core.Areas.Dashboard.Entities.CategoryTranslation", b =>
                {
                    b.HasOne("Core.Areas.Dashboard.Entities.Category", "Category")
                        .WithMany("Translations")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Core.Areas.Dashboard.Entities.City", b =>
                {
                    b.HasOne("Core.Areas.Dashboard.Entities.Country", "Country")
                        .WithMany("Cities")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("Core.Areas.Dashboard.Entities.CityTranslation", b =>
                {
                    b.HasOne("Core.Areas.Dashboard.Entities.City", "City")
                        .WithMany("Translations")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("Core.Areas.Dashboard.Entities.ColorTranslation", b =>
                {
                    b.HasOne("Core.Areas.Dashboard.Entities.Color", "Color")
                        .WithMany("Translations")
                        .HasForeignKey("ColorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Color");
                });

            modelBuilder.Entity("Core.Areas.Dashboard.Entities.CountryTranslation", b =>
                {
                    b.HasOne("Core.Areas.Dashboard.Entities.Country", "Country")
                        .WithMany("Translations")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("Core.Areas.Dashboard.Entities.MallTranslation", b =>
                {
                    b.HasOne("Core.Areas.Dashboard.Entities.Mall", "Mall")
                        .WithMany("Translations")
                        .HasForeignKey("MallId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Mall");
                });

            modelBuilder.Entity("Core.Areas.Dashboard.Entities.ManuFactTranslation", b =>
                {
                    b.HasOne("Core.Areas.Dashboard.Entities.ManuFact", "ManuFact")
                        .WithMany("Translations")
                        .HasForeignKey("ManuFactId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ManuFact");
                });

            modelBuilder.Entity("Core.Areas.Dashboard.Entities.SettingTranslation", b =>
                {
                    b.HasOne("Core.Areas.Dashboard.Entities.Setting", "Setting")
                        .WithMany("Translations")
                        .HasForeignKey("SettingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Setting");
                });

            modelBuilder.Entity("Core.Areas.Dashboard.Entities.Shipping", b =>
                {
                    b.HasOne("Core.Areas.Dashboard.Entities.User", "User")
                        .WithMany("Shippings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Core.Areas.Dashboard.Entities.ShippingTranslation", b =>
                {
                    b.HasOne("Core.Areas.Dashboard.Entities.Shipping", "Shipping")
                        .WithMany("Translations")
                        .HasForeignKey("ShippingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Shipping");
                });

            modelBuilder.Entity("Core.Areas.Dashboard.Entities.SizeTranslation", b =>
                {
                    b.HasOne("Core.Areas.Dashboard.Entities.Size", "Size")
                        .WithMany("Translations")
                        .HasForeignKey("SizeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Size");
                });

            modelBuilder.Entity("Core.Areas.Dashboard.Entities.State", b =>
                {
                    b.HasOne("Core.Areas.Dashboard.Entities.City", "City")
                        .WithMany("States")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Areas.Dashboard.Entities.Country", "Country")
                        .WithMany("States")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("City");

                    b.Navigation("Country");
                });

            modelBuilder.Entity("Core.Areas.Dashboard.Entities.StateTranslation", b =>
                {
                    b.HasOne("Core.Areas.Dashboard.Entities.State", "State")
                        .WithMany("Translations")
                        .HasForeignKey("StateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("State");
                });

            modelBuilder.Entity("Core.Areas.Dashboard.Entities.TradeMarkTranslation", b =>
                {
                    b.HasOne("Core.Areas.Dashboard.Entities.TradeMark", "TradeMark")
                        .WithMany("Translations")
                        .HasForeignKey("TradeMarkId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TradeMark");
                });

            modelBuilder.Entity("Core.Areas.Dashboard.Entities.WeightTranslation", b =>
                {
                    b.HasOne("Core.Areas.Dashboard.Entities.Weight", "Weight")
                        .WithMany("Translations")
                        .HasForeignKey("WeightId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Weight");
                });

            modelBuilder.Entity("Core.Areas.Dashboard.Entities.Category", b =>
                {
                    b.Navigation("Childs");

                    b.Navigation("Translations");
                });

            modelBuilder.Entity("Core.Areas.Dashboard.Entities.City", b =>
                {
                    b.Navigation("States");

                    b.Navigation("Translations");
                });

            modelBuilder.Entity("Core.Areas.Dashboard.Entities.Color", b =>
                {
                    b.Navigation("Translations");
                });

            modelBuilder.Entity("Core.Areas.Dashboard.Entities.Country", b =>
                {
                    b.Navigation("Cities");

                    b.Navigation("States");

                    b.Navigation("Translations");
                });

            modelBuilder.Entity("Core.Areas.Dashboard.Entities.Mall", b =>
                {
                    b.Navigation("Translations");
                });

            modelBuilder.Entity("Core.Areas.Dashboard.Entities.ManuFact", b =>
                {
                    b.Navigation("Translations");
                });

            modelBuilder.Entity("Core.Areas.Dashboard.Entities.Setting", b =>
                {
                    b.Navigation("Translations");
                });

            modelBuilder.Entity("Core.Areas.Dashboard.Entities.Shipping", b =>
                {
                    b.Navigation("Translations");
                });

            modelBuilder.Entity("Core.Areas.Dashboard.Entities.Size", b =>
                {
                    b.Navigation("Translations");
                });

            modelBuilder.Entity("Core.Areas.Dashboard.Entities.State", b =>
                {
                    b.Navigation("Translations");
                });

            modelBuilder.Entity("Core.Areas.Dashboard.Entities.TradeMark", b =>
                {
                    b.Navigation("Translations");
                });

            modelBuilder.Entity("Core.Areas.Dashboard.Entities.User", b =>
                {
                    b.Navigation("Shippings");
                });

            modelBuilder.Entity("Core.Areas.Dashboard.Entities.Weight", b =>
                {
                    b.Navigation("Translations");
                });
#pragma warning restore 612, 618
        }
    }
}
