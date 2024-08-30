﻿// <auto-generated />
using System;
using Humanity.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Humanity.Infrastructure.Migrations
{
    [DbContext(typeof(LisanssizContext))]
    partial class LisanssizContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Humanity.Domain.Entities.Abone", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Agog")
                        .HasColumnType("integer");

                    b.Property<double>("BaglantiGucu")
                        .HasColumnType("double precision");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("DagitimFirmaId")
                        .HasColumnType("integer");

                    b.Property<string>("EtsoKodu")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<double>("KuruluGuc")
                        .HasColumnType("double precision");

                    b.Property<Guid?>("LastModifiedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset?>("LastModifiedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("MusteriId")
                        .HasColumnType("integer");

                    b.Property<int>("SahisTip")
                        .HasColumnType("integer");

                    b.Property<long>("SeriNo")
                        .HasColumnType("bigint");

                    b.Property<double>("SozlesmeGucu")
                        .HasColumnType("double precision");

                    b.Property<int>("Tarife")
                        .HasColumnType("integer");

                    b.Property<int>("Terim")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("MusteriId");

                    b.ToTable("Abone");
                });

            modelBuilder.Entity("Humanity.Domain.Entities.AboneIletisim", b =>
                {
                    b.Property<int>("AboneId")
                        .HasColumnType("integer");

                    b.Property<int?>("IletisimId")
                        .HasColumnType("integer");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<Guid?>("LastModifiedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset?>("LastModifiedOn")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("AboneId", "IletisimId");

                    b.HasIndex("IletisimId");

                    b.ToTable("AboneIletisim");
                });

            modelBuilder.Entity("Humanity.Domain.Entities.AboneSayac", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AboneId")
                        .HasColumnType("integer");

                    b.Property<int>("FazAdedi")
                        .HasColumnType("integer");

                    b.Property<string>("Marka")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("SayacNo")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("AboneId");

                    b.ToTable("AboneSayac");
                });

            modelBuilder.Entity("Humanity.Domain.Entities.AboneTuketici", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AboneId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("BaslamaZamani")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Durum")
                        .HasColumnType("integer");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<int>("UreticiAboneId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AboneId");

                    b.HasIndex("UreticiAboneId");

                    b.ToTable("AboneTuketici");
                });

            modelBuilder.Entity("Humanity.Domain.Entities.AboneUretici", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AboneId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CagrimektupTarihi")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("LisansBilgisi")
                        .HasColumnType("integer");

                    b.Property<int>("MahsupTipi")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UretimBaslama")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("UretimSekli")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AboneId");

                    b.ToTable("AboneUretici");
                });

            modelBuilder.Entity("Humanity.Domain.Entities.CariIletisim", b =>
                {
                    b.Property<int>("CariKartId")
                        .HasColumnType("integer");

                    b.Property<int>("IletisimId")
                        .HasColumnType("integer");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<Guid?>("LastModifiedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset?>("LastModifiedOn")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("CariKartId", "IletisimId");

                    b.HasIndex("IletisimId");

                    b.ToTable("CariIletisim");
                });

            modelBuilder.Entity("Humanity.Domain.Entities.CariKart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Adi")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Durum")
                        .HasColumnType("integer");

                    b.Property<int>("GercekTuzel")
                        .HasColumnType("integer");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<Guid?>("LastModifiedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset?>("LastModifiedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("OzelkodId1")
                        .HasColumnType("integer");

                    b.Property<int?>("OzelkodId2")
                        .HasColumnType("integer");

                    b.Property<int?>("OzelkodId3")
                        .HasColumnType("integer");

                    b.Property<string>("Soyadi")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long?>("Tckn")
                        .HasColumnType("bigint");

                    b.Property<string>("Unvan")
                        .HasColumnType("text");

                    b.Property<long?>("Vkn")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("CariKart");
                });

            modelBuilder.Entity("Humanity.Domain.Entities.Iletisim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Adres")
                        .HasColumnType("text");

                    b.Property<string>("CepTel")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<int?>("Ilceid")
                        .HasColumnType("integer");

                    b.Property<int?>("Ilid")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Iletisim");
                });

            modelBuilder.Entity("Humanity.Domain.Entities.Musteri", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Adi")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("CariKartId")
                        .HasColumnType("integer");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Durum")
                        .HasColumnType("integer");

                    b.Property<int>("GercekTuzel")
                        .HasColumnType("integer");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<Guid?>("LastModifiedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset?>("LastModifiedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("OzelkodId1")
                        .HasColumnType("integer");

                    b.Property<int?>("OzelkodId2")
                        .HasColumnType("integer");

                    b.Property<int?>("OzelkodId3")
                        .HasColumnType("integer");

                    b.Property<string>("Soyadi")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long?>("Tckn")
                        .HasColumnType("bigint");

                    b.Property<string>("Unvan")
                        .HasColumnType("text");

                    b.Property<long?>("Vkn")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("CariKartId");

                    b.ToTable("Musteri");
                });

            modelBuilder.Entity("Humanity.Domain.Entities.MusteriIletisim", b =>
                {
                    b.Property<int>("MusteriId")
                        .HasColumnType("integer");

                    b.Property<int>("IletisimId")
                        .HasColumnType("integer");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<Guid?>("LastModifiedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset?>("LastModifiedOn")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("MusteriId", "IletisimId");

                    b.HasIndex("IletisimId");

                    b.ToTable("MusteriIletisim");
                });

            modelBuilder.Entity("Humanity.Domain.Entities.MusteriOzelKod", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Ad")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Kod")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("KodNo")
                        .HasColumnType("integer");

                    b.Property<int>("RootId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("MusteriOzelKod");
                });

            modelBuilder.Entity("Humanity.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("EmailId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<Guid?>("LastModifiedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset?>("LastModifiedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Humanity.Domain.Entities.Abone", b =>
                {
                    b.HasOne("Humanity.Domain.Entities.Musteri", "Musteri")
                        .WithMany()
                        .HasForeignKey("MusteriId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Musteri");
                });

            modelBuilder.Entity("Humanity.Domain.Entities.AboneIletisim", b =>
                {
                    b.HasOne("Humanity.Domain.Entities.Abone", "Abone")
                        .WithMany()
                        .HasForeignKey("AboneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Humanity.Domain.Entities.Iletisim", "Iletisim")
                        .WithMany()
                        .HasForeignKey("IletisimId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Abone");

                    b.Navigation("Iletisim");
                });

            modelBuilder.Entity("Humanity.Domain.Entities.AboneSayac", b =>
                {
                    b.HasOne("Humanity.Domain.Entities.Abone", "Abone")
                        .WithMany()
                        .HasForeignKey("AboneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Abone");
                });

            modelBuilder.Entity("Humanity.Domain.Entities.AboneTuketici", b =>
                {
                    b.HasOne("Humanity.Domain.Entities.Abone", "Abone")
                        .WithMany()
                        .HasForeignKey("AboneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Humanity.Domain.Entities.Abone", "UreticiAbone")
                        .WithMany()
                        .HasForeignKey("UreticiAboneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Abone");

                    b.Navigation("UreticiAbone");
                });

            modelBuilder.Entity("Humanity.Domain.Entities.AboneUretici", b =>
                {
                    b.HasOne("Humanity.Domain.Entities.Abone", "Abone")
                        .WithMany()
                        .HasForeignKey("AboneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Abone");
                });

            modelBuilder.Entity("Humanity.Domain.Entities.CariIletisim", b =>
                {
                    b.HasOne("Humanity.Domain.Entities.CariKart", "CariKart")
                        .WithMany()
                        .HasForeignKey("CariKartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Humanity.Domain.Entities.Iletisim", "Iletisim")
                        .WithMany()
                        .HasForeignKey("IletisimId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CariKart");

                    b.Navigation("Iletisim");
                });

            modelBuilder.Entity("Humanity.Domain.Entities.Musteri", b =>
                {
                    b.HasOne("Humanity.Domain.Entities.CariKart", "CariKart")
                        .WithMany()
                        .HasForeignKey("CariKartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CariKart");
                });

            modelBuilder.Entity("Humanity.Domain.Entities.MusteriIletisim", b =>
                {
                    b.HasOne("Humanity.Domain.Entities.Iletisim", "Iletisim")
                        .WithMany()
                        .HasForeignKey("IletisimId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Humanity.Domain.Entities.Musteri", "Musteri")
                        .WithMany()
                        .HasForeignKey("MusteriId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Iletisim");

                    b.Navigation("Musteri");
                });
#pragma warning restore 612, 618
        }
    }
}
