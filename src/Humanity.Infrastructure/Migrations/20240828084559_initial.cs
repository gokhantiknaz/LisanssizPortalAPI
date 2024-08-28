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
                name: "AboneSayac",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MusteriId = table.Column<int>(type: "integer", nullable: false),
                    SayacNo = table.Column<long>(type: "bigint", nullable: false),
                    Marka = table.Column<string>(type: "text", nullable: false),
                    FazAdedi = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AboneSayac", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "letisim",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CepTel = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Ilid = table.Column<int>(type: "integer", nullable: true),
                    Ilceid = table.Column<int>(type: "integer", nullable: true),
                    Adres = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_letisim", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Musteri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Adi = table.Column<string>(type: "text", nullable: false),
                    Soyadi = table.Column<string>(type: "text", nullable: false),
                    Unvan = table.Column<string>(type: "text", nullable: true),
                    Tckn = table.Column<long>(type: "bigint", nullable: true),
                    Vkn = table.Column<long>(type: "bigint", nullable: true),
                    Durum = table.Column<int>(type: "integer", nullable: true),
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
                name: "UreitciLisans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MusteriId = table.Column<int>(type: "integer", nullable: false),
                    UretimSekli = table.Column<int>(type: "integer", nullable: false),
                    LisansBilgisi = table.Column<int>(type: "integer", nullable: false),
                    UretimBaslama = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CagrimektupTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    MahsupTipi = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UreitciLisans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    EmailId = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModifiedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Abone",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MusteriId = table.Column<int>(type: "integer", nullable: false),
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
                    LisansBilgileriId = table.Column<int>(type: "integer", nullable: false),
                    AboneSayacId = table.Column<int>(type: "integer", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModifiedOn = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abone", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Abone_AboneSayac_AboneSayacId",
                        column: x => x.AboneSayacId,
                        principalTable: "AboneSayac",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Abone_UreitciLisans_LisansBilgileriId",
                        column: x => x.LisansBilgileriId,
                        principalTable: "UreitciLisans",
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
                });

            migrationBuilder.CreateIndex(
                name: "IX_Abone_AboneSayacId",
                table: "Abone",
                column: "AboneSayacId");

            migrationBuilder.CreateIndex(
                name: "IX_Abone_LisansBilgileriId",
                table: "Abone",
                column: "LisansBilgileriId");

            migrationBuilder.CreateIndex(
                name: "IX_AboneIletisim_AboneId",
                table: "AboneIletisim",
                column: "AboneId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AboneIletisim");

            migrationBuilder.DropTable(
                name: "letisim");

            migrationBuilder.DropTable(
                name: "Musteri");

            migrationBuilder.DropTable(
                name: "MusteriIletisim");

            migrationBuilder.DropTable(
                name: "MusteriOzelKod");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Abone");

            migrationBuilder.DropTable(
                name: "AboneSayac");

            migrationBuilder.DropTable(
                name: "UreitciLisans");
        }
    }
}
