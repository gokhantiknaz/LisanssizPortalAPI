using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Humanity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    LastName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    RefreshToken = table.Column<string>(type: "text", nullable: false),
                    RefreshTokenExpirationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Firma",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirmaAdi = table.Column<string>(type: "text", nullable: false),
                    FirmaUnvan = table.Column<string>(type: "text", nullable: true),
                    VergiDairesi = table.Column<string>(type: "text", nullable: false),
                    Tckn = table.Column<long>(type: "bigint", nullable: true),
                    Vkn = table.Column<long>(type: "bigint", nullable: true),
                    Durum = table.Column<int>(type: "integer", nullable: false),
                    GercekTuzel = table.Column<int>(type: "integer", nullable: false),
                    SorumluAd = table.Column<string>(type: "text", nullable: false),
                    SorumluSoyad = table.Column<string>(type: "text", nullable: false),
                    SorumluTelefon = table.Column<string>(type: "text", nullable: false),
                    SorumluEmail = table.Column<string>(type: "text", nullable: false),
                    OzelkodId1 = table.Column<int>(type: "integer", nullable: true),
                    OzelkodId2 = table.Column<int>(type: "integer", nullable: true),
                    OzelkodId3 = table.Column<int>(type: "integer", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModifiedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Firma", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Iletisim",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CepTel = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Ilid = table.Column<int>(type: "integer", nullable: true),
                    Ilceid = table.Column<int>(type: "integer", nullable: true),
                    Adres = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Iletisim", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Application = table.Column<string>(type: "text", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Level = table.Column<string>(type: "text", nullable: false),
                    Message = table.Column<string>(type: "text", nullable: false),
                    Logger = table.Column<string>(type: "text", nullable: false),
                    Callsite = table.Column<string>(type: "text", nullable: false),
                    Exception = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Musteri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Adi = table.Column<string>(type: "text", nullable: true),
                    Soyadi = table.Column<string>(type: "text", nullable: true),
                    Unvan = table.Column<string>(type: "text", nullable: true),
                    Tckn = table.Column<long>(type: "bigint", nullable: true),
                    Vkn = table.Column<long>(type: "bigint", nullable: true),
                    Durum = table.Column<int>(type: "integer", nullable: false),
                    GercekTuzel = table.Column<int>(type: "integer", nullable: false),
                    OzelkodId1 = table.Column<int>(type: "integer", nullable: true),
                    OzelkodId2 = table.Column<int>(type: "integer", nullable: true),
                    OzelkodId3 = table.Column<int>(type: "integer", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModifiedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Musteri", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MusteriOzelKod",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RootId = table.Column<int>(type: "integer", nullable: false),
                    KodNo = table.Column<int>(type: "integer", nullable: false),
                    Kod = table.Column<string>(type: "text", nullable: false),
                    Ad = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusteriOzelKod", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OwnerConsumpiton",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Json = table.Column<string>(type: "jsonb", nullable: false),
                    SerNo = table.Column<long>(type: "bigint", nullable: false),
                    Donem = table.Column<string>(type: "text", nullable: false),
                    Firma = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OwnerConsumpiton", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FirmaEntegrasyon",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirmaId = table.Column<int>(type: "integer", nullable: false),
                    DagitimFirmaId = table.Column<int>(type: "integer", nullable: false),
                    ServisId = table.Column<int>(type: "integer", nullable: false),
                    ServisKullaniciAdi = table.Column<string>(type: "text", nullable: false),
                    ServisSifre = table.Column<string>(type: "text", nullable: false),
                    ServisAdres = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FirmaEntegrasyon", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FirmaEntegrasyon_Firma_FirmaId",
                        column: x => x.FirmaId,
                        principalTable: "Firma",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FirmaIletisim",
                columns: table => new
                {
                    IletisimId = table.Column<int>(type: "integer", nullable: false),
                    FirmaId = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModifiedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FirmaIletisim", x => new { x.FirmaId, x.IletisimId });
                    table.ForeignKey(
                        name: "FK_FirmaIletisim_Firma_FirmaId",
                        column: x => x.FirmaId,
                        principalTable: "Firma",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FirmaIletisim_Iletisim_IletisimId",
                        column: x => x.IletisimId,
                        principalTable: "Iletisim",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Abone",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MusteriId = table.Column<int>(type: "integer", nullable: false),
                    Adi = table.Column<string>(type: "text", nullable: true),
                    Soyadi = table.Column<string>(type: "text", nullable: true),
                    Unvan = table.Column<string>(type: "text", nullable: true),
                    Tckn = table.Column<long>(type: "bigint", nullable: true),
                    Vkn = table.Column<long>(type: "bigint", nullable: true),
                    Tarife = table.Column<int>(type: "integer", nullable: false),
                    EtsoKodu = table.Column<string>(type: "text", nullable: false),
                    DagitimFirmaId = table.Column<int>(type: "integer", nullable: false),
                    SeriNo = table.Column<long>(type: "bigint", nullable: false),
                    SozlesmeGucu = table.Column<double>(type: "double precision", nullable: false),
                    BaglantiGucu = table.Column<double>(type: "double precision", nullable: false),
                    KuruluGuc = table.Column<double>(type: "double precision", nullable: false),
                    SahisTip = table.Column<int>(type: "integer", nullable: false),
                    Terim = table.Column<int>(type: "integer", nullable: false),
                    Agog = table.Column<int>(type: "integer", nullable: false),
                    OzelkodId1 = table.Column<int>(type: "integer", nullable: true),
                    OzelkodId2 = table.Column<int>(type: "integer", nullable: true),
                    OzelkodId3 = table.Column<int>(type: "integer", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModifiedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    Durum = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abone", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Abone_Musteri_MusteriId",
                        column: x => x.MusteriId,
                        principalTable: "Musteri",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MusteriIletisim",
                columns: table => new
                {
                    IletisimId = table.Column<int>(type: "integer", nullable: false),
                    MusteriId = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModifiedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusteriIletisim", x => new { x.MusteriId, x.IletisimId });
                    table.ForeignKey(
                        name: "FK_MusteriIletisim_Iletisim_IletisimId",
                        column: x => x.IletisimId,
                        principalTable: "Iletisim",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MusteriIletisim_Musteri_MusteriId",
                        column: x => x.MusteriId,
                        principalTable: "Musteri",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AboneIletisim",
                columns: table => new
                {
                    IletisimId = table.Column<int>(type: "integer", nullable: false),
                    AboneId = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModifiedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AboneIletisim", x => new { x.AboneId, x.IletisimId });
                    table.ForeignKey(
                        name: "FK_AboneIletisim_Abone_AboneId",
                        column: x => x.AboneId,
                        principalTable: "Abone",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AboneIletisim_Iletisim_IletisimId",
                        column: x => x.IletisimId,
                        principalTable: "Iletisim",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AboneSayac",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AboneId = table.Column<int>(type: "integer", nullable: false),
                    SayacNo = table.Column<long>(type: "bigint", nullable: false),
                    Marka = table.Column<string>(type: "text", nullable: false),
                    FazAdedi = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AboneSayac", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AboneSayac_Abone_AboneId",
                        column: x => x.AboneId,
                        principalTable: "Abone",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AboneTuketici",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AboneId = table.Column<int>(type: "integer", nullable: false),
                    UreticiAboneId = table.Column<int>(type: "integer", nullable: false),
                    BaslamaZamani = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Durum = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AboneTuketici", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AboneTuketici_Abone_AboneId",
                        column: x => x.AboneId,
                        principalTable: "Abone",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AboneTuketici_Abone_UreticiAboneId",
                        column: x => x.UreticiAboneId,
                        principalTable: "Abone",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AboneUretici",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AboneId = table.Column<int>(type: "integer", nullable: false),
                    UretimSekli = table.Column<int>(type: "integer", nullable: false),
                    LisansBilgisi = table.Column<int>(type: "integer", nullable: false),
                    UretimBaslama = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CagrimektupTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    MahsupTipi = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AboneUretici", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AboneUretici_Abone_AboneId",
                        column: x => x.AboneId,
                        principalTable: "Abone",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MusteriAylikEndeks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AboneId = table.Column<int>(type: "integer", nullable: false),
                    Donem = table.Column<string>(type: "text", nullable: false),
                    TotalTuketimCekis = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalUretimVeris = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalReakIndVeris = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalReakKapVeris = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalReakIndCekis = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalReakKapCekis = table.Column<decimal>(type: "numeric", nullable: false),
                    Carpan = table.Column<decimal>(type: "numeric", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModifiedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusteriAylikEndeks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MusteriAylikEndeks_Abone_AboneId",
                        column: x => x.AboneId,
                        principalTable: "Abone",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MusteriSaatlikEndeks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AboneId = table.Column<int>(type: "integer", nullable: false),
                    ProfilDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CekisTuketim = table.Column<decimal>(type: "numeric", nullable: false),
                    CekisReaktifInduktif = table.Column<decimal>(type: "numeric", nullable: false),
                    CekisReaktifKapasitif = table.Column<decimal>(type: "numeric", nullable: false),
                    Uretim = table.Column<decimal>(type: "numeric", nullable: false),
                    VerisReaktifInduktif = table.Column<decimal>(type: "numeric", nullable: false),
                    VerisReaktifKapasitif = table.Column<decimal>(type: "numeric", nullable: false),
                    Carpan = table.Column<decimal>(type: "numeric", nullable: false),
                    Donem = table.Column<string>(type: "text", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModifiedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusteriSaatlikEndeks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MusteriSaatlikEndeks_Abone_AboneId",
                        column: x => x.AboneId,
                        principalTable: "Abone",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Abone_MusteriId",
                table: "Abone",
                column: "MusteriId");

            migrationBuilder.CreateIndex(
                name: "IX_AboneIletisim_AboneId",
                table: "AboneIletisim",
                column: "AboneId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AboneIletisim_IletisimId",
                table: "AboneIletisim",
                column: "IletisimId");

            migrationBuilder.CreateIndex(
                name: "IX_AboneSayac_AboneId",
                table: "AboneSayac",
                column: "AboneId");

            migrationBuilder.CreateIndex(
                name: "IX_AboneTuketici_AboneId",
                table: "AboneTuketici",
                column: "AboneId");

            migrationBuilder.CreateIndex(
                name: "IX_AboneTuketici_UreticiAboneId",
                table: "AboneTuketici",
                column: "UreticiAboneId");

            migrationBuilder.CreateIndex(
                name: "IX_AboneUretici_AboneId",
                table: "AboneUretici",
                column: "AboneId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FirmaEntegrasyon_FirmaId",
                table: "FirmaEntegrasyon",
                column: "FirmaId");

            migrationBuilder.CreateIndex(
                name: "IX_FirmaIletisim_IletisimId",
                table: "FirmaIletisim",
                column: "IletisimId");

            migrationBuilder.CreateIndex(
                name: "IX_MusteriAylikEndeks_AboneId",
                table: "MusteriAylikEndeks",
                column: "AboneId");

            migrationBuilder.CreateIndex(
                name: "IX_MusteriIletisim_IletisimId",
                table: "MusteriIletisim",
                column: "IletisimId");

            migrationBuilder.CreateIndex(
                name: "IX_MusteriIletisim_MusteriId",
                table: "MusteriIletisim",
                column: "MusteriId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MusteriSaatlikEndeks_AboneId",
                table: "MusteriSaatlikEndeks",
                column: "AboneId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AboneIletisim");

            migrationBuilder.DropTable(
                name: "AboneSayac");

            migrationBuilder.DropTable(
                name: "AboneTuketici");

            migrationBuilder.DropTable(
                name: "AboneUretici");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "FirmaEntegrasyon");

            migrationBuilder.DropTable(
                name: "FirmaIletisim");

            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "MusteriAylikEndeks");

            migrationBuilder.DropTable(
                name: "MusteriIletisim");

            migrationBuilder.DropTable(
                name: "MusteriOzelKod");

            migrationBuilder.DropTable(
                name: "MusteriSaatlikEndeks");

            migrationBuilder.DropTable(
                name: "OwnerConsumpiton");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Firma");

            migrationBuilder.DropTable(
                name: "Iletisim");

            migrationBuilder.DropTable(
                name: "Abone");

            migrationBuilder.DropTable(
                name: "Musteri");
        }
    }
}
