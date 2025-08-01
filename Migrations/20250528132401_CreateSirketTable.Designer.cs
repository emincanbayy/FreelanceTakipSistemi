﻿// <auto-generated />
using System;
using FreelanceTakipSistemi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FreelanceTakipSistemi.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250528132401_CreateSirketTable")]
    partial class CreateSirketTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FreelanceTakipSistemi.Models.Gorev", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Aciklama")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("AtananKullaniciId")
                        .HasColumnType("int");

                    b.Property<string>("Baslik")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Durum")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("OlusturmaTarihi")
                        .HasColumnType("datetime2");

                    b.Property<string>("Oncelik")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProjeId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("TeslimTarihi")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AtananKullaniciId");

                    b.HasIndex("ProjeId");

                    b.ToTable("Gorevler");
                });

            modelBuilder.Entity("FreelanceTakipSistemi.Models.Kullanici", b =>
                {
                    b.Property<int>("KullaniciId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("KullaniciId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Isim")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Parola")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Rol")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("KullaniciId");

                    b.ToTable("Kullanicilar");
                });

            modelBuilder.Entity("FreelanceTakipSistemi.Models.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("GorevId")
                        .HasColumnType("int");

                    b.Property<bool>("IsRead")
                        .HasColumnType("bit");

                    b.Property<int>("KullaniciId")
                        .HasColumnType("int");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("Id");

                    b.HasIndex("GorevId");

                    b.HasIndex("KullaniciId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("FreelanceTakipSistemi.Models.Proje", b =>
                {
                    b.Property<int>("ProjeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProjeId"));

                    b.Property<string>("Aciklama")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<DateTime>("BaslangicTarihi")
                        .HasColumnType("datetime2");

                    b.Property<string>("Baslik")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime?>("BitisTarihi")
                        .HasColumnType("datetime2");

                    b.Property<int>("KullaniciId")
                        .HasColumnType("int");

                    b.HasKey("ProjeId");

                    b.HasIndex("KullaniciId");

                    b.ToTable("Projeler");
                });

            modelBuilder.Entity("FreelanceTakipSistemi.Models.Sirket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Ad")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Adres")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("OlusturmaTarihi")
                        .HasColumnType("datetime2");

                    b.Property<string>("Telefon")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Sirketler");
                });

            modelBuilder.Entity("FreelanceTakipSistemi.Models.Yorum", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("GorevId")
                        .HasColumnType("int");

                    b.Property<string>("Icerik")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("KullaniciAdi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("OlusturmaTarihi")
                        .HasColumnType("datetime2");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Id");

                    b.HasIndex("GorevId");

                    b.ToTable("Yorumlar");
                });

            modelBuilder.Entity("FreelanceTakipSistemi.Models.Gorev", b =>
                {
                    b.HasOne("FreelanceTakipSistemi.Models.Kullanici", "AtananKullanici")
                        .WithMany()
                        .HasForeignKey("AtananKullaniciId");

                    b.HasOne("FreelanceTakipSistemi.Models.Proje", "Proje")
                        .WithMany("Gorevler")
                        .HasForeignKey("ProjeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AtananKullanici");

                    b.Navigation("Proje");
                });

            modelBuilder.Entity("FreelanceTakipSistemi.Models.Notification", b =>
                {
                    b.HasOne("FreelanceTakipSistemi.Models.Gorev", "Gorev")
                        .WithMany()
                        .HasForeignKey("GorevId");

                    b.HasOne("FreelanceTakipSistemi.Models.Kullanici", "Kullanici")
                        .WithMany()
                        .HasForeignKey("KullaniciId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Gorev");

                    b.Navigation("Kullanici");
                });

            modelBuilder.Entity("FreelanceTakipSistemi.Models.Proje", b =>
                {
                    b.HasOne("FreelanceTakipSistemi.Models.Kullanici", "Kullanici")
                        .WithMany("Projeler")
                        .HasForeignKey("KullaniciId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Kullanici");
                });

            modelBuilder.Entity("FreelanceTakipSistemi.Models.Yorum", b =>
                {
                    b.HasOne("FreelanceTakipSistemi.Models.Gorev", "Gorev")
                        .WithMany("Yorumlar")
                        .HasForeignKey("GorevId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Gorev");
                });

            modelBuilder.Entity("FreelanceTakipSistemi.Models.Gorev", b =>
                {
                    b.Navigation("Yorumlar");
                });

            modelBuilder.Entity("FreelanceTakipSistemi.Models.Kullanici", b =>
                {
                    b.Navigation("Projeler");
                });

            modelBuilder.Entity("FreelanceTakipSistemi.Models.Proje", b =>
                {
                    b.Navigation("Gorevler");
                });
#pragma warning restore 612, 618
        }
    }
}
